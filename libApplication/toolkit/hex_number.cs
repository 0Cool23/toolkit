/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_HexNumber toolkit.HexNumber
    @{
    @details

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Text.RegularExpressions;

namespace libApplication.toolkit
{
/** @ingroup REF_HexNumber

    @class   HexNumber

    @brief   Parses hexadecimal numeral format option argument string.
*/
public static class HexNumber
    {
    /** @details

        @param[in] number_str

        @return
    */
    public static ulong parse( string number_str )
        {
        if( string.IsNullOrEmpty(number_str) )
            {
            throw new ArgumentNullException(Properties.language.NUMBER_PARSE_NULL);
            }

        number_str = number_str.Trim().ToLower();
        number_str = remove_hexadecimal_literal(number_str);
        number_str = number_str.Replace("_", string.Empty);

        if( !Regex.Match(number_str, @"^[0-9a-f]+$").Success )
            {
            throw new FormatException(Texting.get_string(Properties.language.FORMAT_INVALID_HEXADECIMAL_DIGITS, number_str));
            }

        return Convert.ToUInt64(number_str, 16);
        }

    private static string remove_hexadecimal_literal( string number_str )
        {
        if( !number_str.StartsWith("0x") )
            {
            throw new FormatException(Properties.language.FORMAT_INVALID_HEXADECIMAL_PREFIX);
            }

        return number_str.Substring(2);
        }
    }
}
