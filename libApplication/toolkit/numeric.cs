/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_Numeric toolkit.Numeric
    @{
    @details

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Globalization;

namespace libApplication.toolkit
{
/** @ingroup REF_Numeric

    @class   Numeric

    @brief   Toolkit functions class for numeral parsing.
*/
public static class Numeric
    {
    /** @details

        @param[in] unsigned_value

        @return
    */
    public static long to_long( ulong unsigned_value )
        {
        return unchecked((long)unsigned_value);
        }

    /** @details

        @param[in] signed_value

        @return
    */
    public static ulong to_ulong( long signed_value )
        {
        return unchecked((ulong)signed_value);
        }


    /** @details

        @param[in] digit_char

        @return
    */
    public static bool is_bin_digit( char digit_char )
        {
        if( (digit_char == '0') || (digit_char == '1') )
            {
            return true;
            }
        return false;
        }

    /** @details

        @param[in] digit_char

        @return
    */
    public static bool is_oct_digit( char digit_char )
        {
        if( ('0' <= digit_char) && (digit_char <= '7') )
            {
            return true;
            }
        return false;
       }

    /** @details

        @param[in] digit_char

        @return
    */
    public static bool is_dec_digit( char digit_char )
        {
        if( ('0' <= digit_char) && (digit_char <= '9') )
            {
            return true;
            }
        return false;
        }

    /** @details

        @param[in] digit_char

        @return
    */
    public static bool is_hex_digit( char digit_char )
        {
        if( is_dec_digit(digit_char) )
            {
            return true;
            }
        digit_char = char.ToLower(digit_char);
        if( ('a' <= digit_char) && (digit_char <= 'f') )
            {
            return true;
            }
        return false;
       }

    /** @details

        @param[in] value
        @param[in] minimum
        @param[in] maximum

        @return
    */
    public static bool is_within<T>( this T value, T minimum, T maximum ) where T
        :   IComparable<T>
        {
        if( value.CompareTo(minimum) < 0 )
            {
            return false;
            }
        if( value.CompareTo(maximum) > 0 )
            {
            return false;
            }
        return true;
        }
    }
}
