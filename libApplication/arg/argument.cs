/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_Argument arg.Argument
    @{
    @brief

    @details

    @ingroup REF_libApplication
    @}
*/

using System;
using System.Globalization;

using libApplication.toolkit;

namespace libApplication.arg
{
/** @ingroup REF_Argument

    @class   Argument

    @brief
*/
public class Argument
    {
    /** @details
    */
    public int Count
        {
        get;
        private set;
        } = 0;

    /** @details
    */
    public string OptionStr
        {
        get;
        protected set;
        } = null;

    public string Name
        {
        get;
        private set;
        } = null;

    private ArgType m_arg_type = ArgType.REQUIRED_ARGUMENT;

    /** @details

        @param[in] entry
    */
    public void init( ArgEntry entry )
        {
        if( entry == null )
            {
            throw new ArgumentNullException(Properties.language.ENTRY_NULL_EXCEPTION);
            }
        Name       = entry.name;
        m_arg_type = entry.type;
        if( entry.initial_value != null )
            {
            parse(entry.initial_value);
            }
        // reset counter after initialization
        Count = 0;
        }

    /** @details

        @param[in] initial_value
    */
    public void init( string initial_value )
        {
        if( initial_value == null )
            {
            throw new ArgumentNullException(Properties.language.VALUE_NULL_EXCEPTION);
            }
        m_arg_type = ArgType.REQUIRED_ARGUMENT;
        parse(initial_value);
        // reset counter after initialization
        Count = 0;
        }

    /** @details
    */
    protected virtual void update()
        {
        }

    private string trim_option_string( string argument_str )
        {
        if( argument_str != null )
            {
            argument_str = argument_str.Trim();
            }
        return argument_str;
        }


    private void parse_no_argument( string option_str )
        {
        if( !string.IsNullOrEmpty(option_str) )
            {
            throw new ArgumentException(Texting.get_string(Properties.language.NO_ARGUMENT_EXCEPTION, Name));
            }
        }

    private void parse_required_argument( string option_str )
        {
        if( option_str == null )
            {
            throw new ArgumentException(Texting.get_string(Properties.language.REQUIRED_ARGUMENT_EXCEPTION, Name));
            }
        }

    /** @details

        @param[in] argument_str
    */
    public void parse( string argument_str )
        {
        ++Count;
        OptionStr = trim_option_string(argument_str);
        switch( m_arg_type )
            {
            case ArgType.NO_ARGUMENT:
                parse_no_argument(OptionStr);
                break;
            case ArgType.REQUIRED_ARGUMENT:
                parse_required_argument(OptionStr);
                break;
            default:
                break;
            }
        update();
        }
    }
}
