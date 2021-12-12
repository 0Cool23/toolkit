/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_ArgShort arg.ArgShort
    @{
    @details  Parses the next short argument from @ref REF_ArgVector.

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
/** @ingroup REF_ArgShort

    @class   ArgShort

    @brief   Parses the next short argument from @ref REF_ArgVector.
*/
internal class ArgShort
    :   ArgBase
    {
    /** @details   Construct a short argument parser for a list of
                   @ref REF_ArgEntry objects.

        @param[in] arg_table List of @ref REF_ArgEntry objects.
    */
    public ArgShort( List<ArgEntry> arg_table )
        :   base(arg_table)
        {
        }

    private static bool is_short_option( string current_arg )
        {
        return !(!current_arg.StartsWith("-") || (current_arg.Length <= 1));
        }

    /** @details   Will return true if the next parameter from @ref REF_ArgVector
                   stack is a short argument. Otherwise false is returned. A short
                   argument always starts with one hyphen characters followed by a
                   digit or letter character.

        @param[in] argv @ref REF_ArgVector object with parameters to examine.
    */
    public static bool is_short_option( ArgVector argv )
        {
        if( !is_short_option(argv.current_arg) )
            {
            return false;
            }

        if( !Char.IsLetterOrDigit(argv.current_arg[1]) )
            {
            return false;
            }

        return true;
        }

    /** @details   Parse the next short option from @ref REF_ArgVector.
                   Short options consist of '-' followed by a sequence made of
                   alphanumeric characters. The option may have an optional
                   argument. To specify an argument for a short option, write
                   '-x=value'.

        @param[in] argv @ref REF_ArgVector object with parameters to parse.
    */
    public override void parse( ArgVector argv )
        {
        while( !argv.end_of_string )
            {
            char option = argv.current_arg[argv.str_pos];
            var arg_entry = find_arg_entry(option);
            handle_option(argv, arg_entry);
            }
        argv.inc_index();
        }

    private ArgEntry find_arg_entry( char option )
        {
        foreach( var arg_entry in m_arg_table )
            {
            if( arg_entry.short_name == option )
                {
                return arg_entry;
                }
            }
        throw new ArgumentException(Texting.get_string(Properties.language.INVALID_OPTION_EXCEPTION, "-" + option));
        }

    delegate string GetOptionFunc( ArgVector argv, ArgEntry arg_entry );

    private string get_option_no_argument( ArgVector argv, ArgEntry arg_entry )
        {
        return null;
        }

    private string get_option_opt_argument( ArgVector argv, ArgEntry arg_entry )
        {
        return get_argument(false, argv);
        }

    private string get_option_required_argument( ArgVector argv, ArgEntry arg_entry )
        {
        return get_argument(true, argv);
        }

    private string get_option( ArgVector argv, ArgEntry arg_entry )
        {
        var get_option_func_map = new Dictionary<ArgType, GetOptionFunc>
            {
                {ArgType.NO_ARGUMENT,       get_option_no_argument},
                {ArgType.OPT_ARGUMENT,      get_option_opt_argument},
                {ArgType.REQUIRED_ARGUMENT, get_option_required_argument},
            };

        if( !get_option_func_map.ContainsKey(arg_entry.type) )
            {
            throw new ArgumentException(Texting.get_string(Properties.language.UNHANDLED_ARG_TYPE_EXCEPTION, "-" + arg_entry.short_name));
            }

        return (get_option_func_map[arg_entry.type])(argv, arg_entry);
        }

    private void handle_option( ArgVector argv, ArgEntry arg_entry )
        {
        ++argv.str_pos;
        var argument = get_option(argv, arg_entry);
        arg_entry.param.parse(argument);
        }

    delegate bool GetArgumentFunc( bool is_required, ArgVector argv, ref string result_string );

    private bool is_successor_argument( bool is_required, ArgVector argv, ref string result_string )
        {
        bool result = argv.end_of_string;
        if( result )
            {
            result_string = get_successor_argument(is_required, argv);
            }
        return result;
        }

    private bool is_assigned_argument( bool is_required, ArgVector argv, ref string result_string )
        {
        bool result = (argv.current_arg[argv.str_pos] == '=');
        if( result )
            {
            result_string = get_current_argument(argv);
            }
        return result;
        }

    private bool is_required_argument( bool is_required, ArgVector argv, ref string result_string )
        {
        if( is_required )
            {
            result_string = get_required_argument(argv);
            }
        return is_required;
        }

    private string get_argument( bool is_required, ArgVector argv )
        {
        var get_argument_func_list = new List<GetArgumentFunc>
            {
            is_successor_argument,
            is_assigned_argument,
            is_required_argument,
            };

        string result_string = null;
        foreach( var get_argument_func in get_argument_func_list )
            {
            if( get_argument_func(is_required, argv, ref result_string) )
                {
                return result_string;
                }
            }
        return get_optional_argument(argv);
        }

    /* If argument is required we are greedy and do not care about format. Otherwise if the argument starts
       with a '-' symbol and has a string length greater than 1, we assume it is either a short or long option
       or a argument stack separator. */
    private static bool is_optional_argument( bool is_required, string argument )
        {
        if( is_required )
            {
            return false;
            }
        return (argument.StartsWith("-") && argument.Length > 1);
        }

    private string get_successor_argument( bool is_required, ArgVector argv )
        {
        string argument = null;
        if( argv.has_successor )
            {
            argument = argv.successor_arg;
            if( is_optional_argument(is_required, argument) )
                {
                return null;
                }
            argv.inc_index();
            argv.str_pos = argv.current_arg.Length;
            }
        return argument;
        }

    private string get_current_argument( ArgVector argv )
        {
        ++argv.str_pos;
        var argument = argv.current_arg.Substring(argv.str_pos);
        argv.str_pos = argv.current_arg.Length;
        return argument;
        }

    private string get_required_argument( ArgVector argv )
        {
        var argument = argv.current_arg.Substring(argv.str_pos);
        argv.str_pos = argv.current_arg.Length;
        return argument;
        }

    private string get_optional_argument( ArgVector argv )
        {
        try
            {
            find_arg_entry(argv.current_arg[argv.str_pos]);
            }
        catch( ArgumentException )
            {
            var argument = argv.current_arg.Substring(argv.str_pos);
            argv.str_pos = argv.current_arg.Length;
            return argument;
            }
        return null;
        }
    }
}
