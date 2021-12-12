/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_DecNumber toolkit.DecNumber
    @{
    @details

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Text.RegularExpressions;

namespace libApplication.toolkit
{
/** @ingroup REF_DecNumber

    @class   DecNumber

    @brief   Parses decimal numeral option argument string.
*/
public static class DecNumber
    {
    /** @details

        @param[in] number_str

        @return
    */
    public static long parse( string number_str )
        {
        if( string.IsNullOrEmpty(number_str) )
            {
            throw new ArgumentNullException();
            }

        number_str = update_decimal_sign(number_str.Trim());
        number_str = number_str.Replace("_", string.Empty);
        check_decimal_format(number_str);
 
        if( number_str[0] == '+' )
            {
            return Numeric.to_long(Convert.ToUInt64(number_str, 10));
            }
        return Convert.ToInt64(number_str, 10);
        }

    private static string update_decimal_sign( string number_str )
        {
        if( number_str.StartsWith("+") || number_str.StartsWith("-") )
            {
            return number_str;
            }
        return "+" + number_str;
        }

    private static void check_decimal_format( string number_str )
        {
        if( !Regex.Match(number_str, @"^(\+|-)(0|[1-9][0-9]*)$").Success )
            {
            throw new FormatException(Texting.get_string(Properties.language.FORMAT_INVALID_DECIMAL_DIGITS));
            }
        }
    }
}
