/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_ArgParser arg.ArgParser
    @{
    @details  Parse an argument vector to match a list of @ref REF_ArgEntry
              objects.

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;

using libApplication.toolkit;

namespace libApplication.arg
{
/** @ingroup REF_ArgParser

    @class   ArgParser

    @brief   Parse an argument vector to match a list of @ref REF_ArgEntry
             objects.
*/
public class ArgParser
    {
    private ArgVector m_vector = null;
    private ArgStack m_stack = null;

    private List<ArgEntry> m_arg_table = null;

    /** @details
    */
    public ArgParser()
        {
        }

    /** @details   Parse an argument vector as array of strings to
                   match a list of @ref REF_ArgEntry objects. The argument
                   parser first separates the array into two
                   categories. These are separated by a '--' argument. The
                   first category is parsed to match the list of
                   @ref REF_ArgEntry objects. The second category is seen
                   as stack of arguments the application may pop of value by
                   value.

        @param[in] args      An array of string arguments to be parsed.
        @param[in] arg_table A list of @ref REF_ArgEntry objects.

        @exception ArgumentNullException (ARGS_NULL_EXCEPTION) is thrown
                   if one of the arguments is a null value.
    */
    public void process( string[] args, List<ArgEntry> arg_table )
        {
        if( (args == null) || (arg_table == null) )
            {
            throw new ArgumentNullException(Properties.language.ARGS_NULL_EXCEPTION);
            }

        init_entries(arg_table);
        init_arguments(args);

        parse_arg_vector();
        }

    private void init_entries( List<ArgEntry> arg_table )
        {
        m_arg_table = arg_table;
        foreach( var arg_entry in m_arg_table )
            {
            arg_entry.param.init(arg_entry);
            }
        }

    private void init_arguments( string[] args )
        {
        m_vector = new ArgVector();
        m_stack  = new ArgStack();

        var index = init_arg_vector(args);
        _ = init_arg_stack(args, index);
        }

    private int init_arg_vector( string[] args )
        {
        var index = 0;
        for( ; index < args.Length; ++index )
            {
            if( args[index] == "--" )
                {
                break;
                }
            m_vector.add(args[index]);
            }
        return index;
        }

    private int init_arg_stack( string[] args, int index )
        {
        for( ++index; index < args.Length; ++index )
            {
            m_stack.add(args[index]);
            }

        return index;
        }

    /** @details This function will pop the next string value from the
                 argument stack.

        @return  Will return the next string value from the argument stack
                 or null if there are no more values.
    */
    public string get_next_argument()
        {
        return m_stack.get_next_argument();
        }

    private ArgBase get_argument()
        {
        if( ArgLong.is_long_option(m_vector) )
            {
            m_vector.str_pos = 2;
            return new ArgLong(m_arg_table);
            }
        else if( ArgShort.is_short_option(m_vector) )
            {
            m_vector.str_pos = 1;
            return new ArgShort(m_arg_table);
            }
        throw new ArgumentException(Texting.get_string(Properties.language.INVALID_OPTION_EXCEPTION, m_vector.current_arg));
        }

    private void parse_arg_vector()
        {
        ArgBase argument;
        while( !m_vector.end_of_list )
            {
            argument = get_argument();
            argument.parse(m_vector);
            }
        }
    }
}
