/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_ArgEntry arg.ArgEntry
    @{
    @details  Defines a command line option.

    @ingroup  REF_libApplication
    @}
*/

using System;

using libApplication.toolkit;

namespace libApplication.arg
{
/** @ingroup REF_ArgEntry

    @class   ArgEntry

    @brief   Defines a command line option.
*/
public class ArgEntry
    {
    /** Character which defines the short name of the option. */
    public readonly char short_name;

    /** String which defines the long name of the option. */
    public readonly string long_name;

    /** Defines the type of arguments the option has. */
    public readonly ArgType type;

    /** Defines if the option is mandatory or optional. */
    public readonly bool is_mandatory;

    /** Defines the argument parser for this option. */
    public readonly Argument param;

    /** Defines the default value the option is initialized with. */
    public readonly string initial_value;

    /** Defines the help phrase for this option. */
    public readonly string help_text;

    /** @brief   Defines the longest possible name of the command line option.

        @details For a command line option either the long or a short name is
                 mandatory. If there is a long name defined this property
                 will return --{option long name} or --{option short name}
                 otherwise.
    */
    public string name
        {
        get
            {
            if( long_name != null )
                {
                return "--" + long_name;
                }
            return "-" + short_name;
            }
        }

    /** @brief     Construct an object of a @ref REF_ArgEntry instance.

        @details   The object is only constructed in case all optional and
                   required parameters are valid.

                   - A valid option must have at least a @a short_arg or a
                     @a long_arg which is not an empty or null value.

                   - If a @a short_arg has been passed, it must be a letter or
                     digit character.

                   - If a @a long_arg has been passed, it must start with a
                     letter or digit character. All subsequent characters must
                     be letters, digits, period, hyphen or underscore
                     characters.

                   - An object reference of base type @ref REF_Argument must
                     not be null value.

                   - The help phrase may not be an empty string or a null
                     value. The phase must end with a period character. All
                     other characters must be printabale.

        @param[in] short_arg     Letter or digit character which defines the
                                 options short name.
        @param[in] long_arg      Name which defines the options long name.
        @param[in] arg_type      Defines if the option has additional arguments
                                 required (see @ref REF_ArgType).
        @param[in] is_required   Defines if the argument is mandatory an must
                                 be set.
        @param[in] argument      Instance of base type @ref REF_Argument. This
                                 object must provide the argument parser and
                                 holds the resulting properties.
        @param[in] default_value Default value the @ref REF_Argument instance
                                 is initialized with.
        @param[in] help          Phrase which describes the option.

        @exception ArgumentNullException will be raised in case
                   - @a short_arg and @a long_arg are both empty or null
                     values.
                   - @a argument is a null value.
                   - @a help phrase is an empty of null value.

        @exception ArgumentException will be raised in case
                   - @a short_arg is not a letter or digit character.
                   - @a long_arg does not start with a letter or digit character.
                   - @a long_arg does not end with a period.
                   - @a long_arg contains any not printable characters.
    */
    public ArgEntry( char short_arg, string long_arg, ArgType arg_type, bool is_required, Argument argument, string default_value, string help )
        {
        check_arg_values(short_arg, long_arg);
        check_short_arg_value(short_arg);

        short_name    = short_arg;
        long_name     = check_long_arg(long_arg);
        type          = arg_type;
        is_mandatory  = is_required;
        param         = argument ?? throw new ArgumentNullException(Properties.language.PARAM_NULL_EXCEPTION);
        initial_value = default_value;
        help_text     = check_help_text(help);
        }

    private static void check_arg_values( char short_arg, string long_arg )
        {
        if( string.IsNullOrEmpty(long_arg) && (short_arg == 0))
            {
            throw new ArgumentNullException(Properties.language.INVALID_ARG_NAME_EXCEPTION);
            }
        }

    private static void check_short_arg_value( char short_arg )
        {
        if( (!Char.IsLetterOrDigit(short_arg)) && (short_arg != 0) )
            {
            throw new ArgumentException(Properties.language.INVALID_SHORT_ARG_EXCEPTION);
            }
        }

    private static bool is_valid_symbol( char symbol )
        {
        if( Char.IsLetterOrDigit(symbol) )
            {
            return true;
            }
        if( ".-_".Contains(string.Empty + symbol) )
            {
            return true;
            }
        return false;
        }

    private static bool test_argument_content( string text )
        {
        for( int i = 1; i < text.Length; ++i )
            {
            if( is_valid_symbol(text[i]) )
                {
                continue;
                }
            return false;
            }
        return true;
        }

    private static bool is_argument_string( string text )
        {
        if( !Char.IsLetterOrDigit(text, 0) )
            {
            return false;
            }
        return test_argument_content(text);
        }

    private string check_long_arg( string long_arg )
        {
        if( !string.IsNullOrEmpty(long_arg) )
            {
            if( !is_argument_string(long_arg) )
                {
                throw new ArgumentException(Properties.language.INVALID_ARG_NAME_EXCEPTION);
                }
            }
        return long_arg;
        }

    private void is_valid_help_text( string help_text )
        {
        if( !help_text.EndsWith(".") )
            {
            throw new ArgumentException(Properties.language.INVALID_HELP_TEXT_EXCEPTION);     
            }
        if( !Texting.is_printable_string(help_text) )
            {
            throw new ArgumentException(Properties.language.INVALID_HELP_TEXT_EXCEPTION);     
            }
        }

    private string check_help_text( string help_text )
        {
        if( string.IsNullOrEmpty(help_text) )
            {
            throw new ArgumentNullException(Properties.language.INVALID_HELP_TEXT_EXCEPTION);
            }
        is_valid_help_text(help_text);
        return help_text;
        }
    }
}
