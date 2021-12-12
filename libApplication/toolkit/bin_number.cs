/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_BinNumber toolkit.BinNumber
    @{
    @details

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Text.RegularExpressions;

namespace libApplication.toolkit
{
/** @ingroup REF_BinNumber

    @class   BinNumber

    @brief   Parses binary numeral format option argument string.
*/
public static class BinNumber
    {
    /** @details

        @param[in] number_str

        @return
    */
    public static ulong parse( string number_str )
        {
        if( string.IsNullOrEmpty(number_str) )
            {
            throw new ArgumentNullException();
            }

        number_str = number_str.Trim().ToLower();
        number_str = remove_binary_literal(number_str);
        number_str = number_str.Replace("_", string.Empty);

        if( !Regex.Match(number_str, @"^[0-1]+$").Success )
            {
            throw new FormatException(Texting.get_string(Properties.language.FORMAT_INVALID_BINARY_DIGITS));
            }
        return Convert.ToUInt64(number_str, 2);
        }

    private static string remove_binary_literal( string number_str )
        {
        if( !number_str.StartsWith("0b") )
            {
            throw new FormatException(Properties.language.FORMAT_INVALID_BINARY_PREFIX);
            }

        return number_str.Substring(2);
        }
    }
}
