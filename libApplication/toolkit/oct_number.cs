/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_OctNumber toolkit.OctNumber
    @{
    @details

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Text.RegularExpressions;

namespace libApplication.toolkit
{
/** @ingroup REF_OctNumber

    @class   OctNumber

    @brief   Parses octal numeral format option argument string.
*/
public static class OctNumber
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

        check_octal_literal(number_str);
        number_str = number_str.Replace("_", string.Empty);

        if( !Regex.Match(number_str, @"^[0-7]+$").Success )
            {
            throw new FormatException(Texting.get_string(Properties.language.FORMAT_INVALID_OCTAL_DIGITS, number_str));
            }

        return Convert.ToUInt64(number_str, 8);
        }

    private static void check_octal_literal( string number_str )
        {
        if( !number_str.StartsWith("0") )
            {
            throw new FormatException(Properties.language.FORMAT_INVALID_OCTAL_PREFIX);
            }
        }
    }
}
