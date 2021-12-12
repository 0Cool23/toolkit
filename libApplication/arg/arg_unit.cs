/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_ArgUnit arg.ArgUnit
    @{
    @details  Abstract base class for argument parsing with scalable entity.

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using libApplication.toolkit;

namespace libApplication.arg
{
/** @ingroup REF_ArgUnit

    @class   ArgUnit

    @brief   Abstract base class for argument parsing with scalable entity.
*/
public abstract class ArgUnit
    :   Argument
    {
    public readonly string Symbol = string.Empty;

    /** @details
    
        @param[in] symbol

        @exception Throws ArgumentNullException
    */
    public ArgUnit( string symbol )
        {
        if( string.IsNullOrEmpty(symbol) )
            {
            throw new ArgumentNullException(Properties.language.SYMBOL_NULL_EXCEPTION);
            }
        Symbol = symbol;
        }

    /** @details    
    */
    protected interface iMagnitudeEntry
        {
        bool isNormalized
            {
            get;
            }
        }

    /** @details    

        @param[in] option_str

        @return

        @exception Throws ArgumentNullException
        @exception Throws ArgumentException
    */
    protected string remove_symbol( string option_str )
        {
        if( string.IsNullOrEmpty(option_str) )
            {
            throw new ArgumentNullException(Properties.language.OPTION_STR_EXCEPTION);
            }

        if( !option_str.EndsWith(Symbol) )
            {
            throw new ArgumentException(Texting.get_string(Properties.language.OPTION_SYMBOL_EXCEPTION, Symbol));
            }
        return option_str.Substring(0, option_str.Length - Symbol.Length);
        }

    private string remove_prefix( string option_str, Dictionary<string, iMagnitudeEntry> magnitude_map, out string magnitude )
        {
        var magnitude_list = magnitude_map.Keys.OrderBy(x => x.Length).Reverse();

        magnitude = string.Empty;
        foreach( var mangnitude_entry in magnitude_list )
            {
            if( option_str.EndsWith(mangnitude_entry) )
                {
                magnitude  = mangnitude_entry;
                option_str = option_str.Substring(0, option_str.Length - mangnitude_entry.Length);
                break;
                }
            }

        return option_str;
        }

    /** @details    

        @param[in]  argument
        @param[in]  option_str
        @param[in]  magnitude_map
        @param[out] magnitude

        @return
    */
    protected void parse( Argument argument, string option_str, Dictionary<string, iMagnitudeEntry> magnitude_map, out string magnitude )
        {
        option_str = remove_symbol(option_str);
        option_str = remove_prefix(option_str, magnitude_map, out magnitude);
        option_str = option_str.Trim();
        argument.parse(option_str);
        }
    }
}
