/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_ArgLong arg.ArgLong
    @{
    @details  Parses the next long argument from @ref REF_ArgVector.

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using libApplication.toolkit;

[assembly: InternalsVisibleTo("RegressionTests")]
namespace libApplication.arg
{
/** @ingroup REF_ArgLong
 
    @class   ArgLong

    @brief   Parses the next long argument from @ref REF_ArgVector.
*/
internal class ArgLong
    :   ArgBase
    {
    /** @details Construct a long argument parser for a list of
                 @ref REF_ArgEntry objects.

        @param[in] arg_table List of @ref REF_ArgEntry objects.
    */
    public ArgLong( List<ArgEntry> arg_table )
        : base(arg_table)
        {
        }

    private static bool is_no_option( string current_arg )
        {
        if( !current_arg.StartsWith("--") || (current_arg.Length <= 2) )
            {
            return true;
            }
        return false;
        }

    /** @details   Will return true if the next parameter from @ref REF_ArgVector
                   stack is a long argument. Otherwise false is returned. A long
                   argument always starts with two hyphen characters followed by a
                   digit or letter character.

        @param[in] argv @ref REF_ArgVector object with parameters to examine.
    */
    public static bool is_long_option( ArgVector argv )
        {
        if( is_no_option(argv.current_arg) )
            {
            return false;
            }

        if( !Char.IsLetterOrDigit(argv.current_arg[2]) )
            {
            return false;
            }

        return true;
        }

    private static string get_option( ArgVector argv, out string argument )
        {
        argument = null;
        int index = argv.current_arg.IndexOf("=");
        if( index == -1 )
            {
            return argv.current_arg.Substring(argv.str_pos);
            }

        argument = argv.current_arg.Substring(index + 1);
        return argv.current_arg.Substring(argv.str_pos, index - argv.str_pos);
        }

    /** @details   Parse the next long option from @ref REF_ArgVector.
                   Long options consist of '--' followed by a name made of
                   alphanumeric characters, underscores and dashes. The long
                   option is split into the name and a optional argument. To
                   specify an argument for a long option, write
                   '--name=value'.

        @param[in] argv @ref REF_ArgVector object with parameters to parse.
    */
    public override void parse( ArgVector argv )
        {
        var option = get_option(argv, out string argument);
        var arg_entry = find_arg_entry(option);
        handle_option(argv, arg_entry, argument);
        }

    private ArgEntry find_arg_entry( string option )
        {
        foreach( var arg_entry in m_arg_table )
            {
            if( arg_entry.long_name == option )
                {
                return arg_entry;
                }
            }
        throw new ArgumentException(Texting.get_string(Properties.language.INVALID_OPTION_EXCEPTION, "--" + option));
        }

    private void handle_option( ArgVector argv, ArgEntry arg_entry, string argument )
        {
        argv.inc_index();
        if( argument == null && (!argv.end_of_list) )
            {
            argument = get_argument(argv, arg_entry);
            }
        arg_entry.param.parse(argument);
        }

    private static string opt_argument( ArgVector argv )
        {
        if (!argv.current_arg.StartsWith("-"))
            {
            // successor is not an option therefore we assume it is an argument
            return required_argument(argv);
            }
        return null;
        }

    private static string required_argument( ArgVector argv )
        {
        // as successor exists, we take it as argument
        var argument = argv.current_arg;
        argv.inc_index();
        return argument;
        }

    private string get_argument_no_argument( ArgVector argv )
        {
        return null;
        }

    private string get_argument_opt_argument( ArgVector argv )
        {
        return opt_argument(argv);
        }

    private string get_argument_required_argument( ArgVector argv )
        {
        return required_argument(argv);
        }

    private delegate string GetOptionFunc( ArgVector argv );

    private string get_argument( ArgVector argv, ArgEntry arg_entry )
        {
        var get_option_func_map  = new Dictionary<ArgType, GetOptionFunc>
            {
                {ArgType.NO_ARGUMENT,       get_argument_no_argument},
                {ArgType.OPT_ARGUMENT,      get_argument_opt_argument},
                {ArgType.REQUIRED_ARGUMENT, get_argument_required_argument},
            };
        if( !get_option_func_map.ContainsKey(arg_entry.type) )
            {
            throw new ArgumentException(Texting.get_string(Properties.language.UNHANDLED_ARG_TYPE_EXCEPTION, "--" + arg_entry.long_name));
            }

        return (get_option_func_map[arg_entry.type])(argv);
        }
    }
}
