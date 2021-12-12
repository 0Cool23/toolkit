/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2021

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_Texting toolkit.Texting
    @{
    @details

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("RegressionTests")]
namespace libApplication.toolkit
{
/** @ingroup REF_Texting

    @class   Texting

    @brief   Toolkit function class for string handling.
*/
public static class Texting
    {
    /** @details Retrieve the name the library (assembly) as string.

        @return  Library name as string.

        @code{.cs}
        @endcode
    */
    public static readonly string LIBRARY_NAME = typeof(Texting).Assembly.GetName().Name;

    /** @details

        @param[in] digit
        @param[in] min_digit
        @param[in] max_digit

        @return    Will return true is all criteria are met, otherwise false will be returned.
    */
    internal static bool is_digit_char( char digit, char min_digit, char max_digit )
        {
        if( (min_digit <= digit) && (digit <= max_digit) )
            {
            return true;
            }
        return false;
        }

    /** @details   This functions tests if a given character is a digit in a defined interval
                   starting from '0' having the upper boundary '9' as default value. The upper
                   boundary may be overwritten to limit the allowed range of valid digits.

        @param[in] digit     Character to be tested.
        @param[in] max_digit Upper boundary digit.

        @return    Will return true is all criteria are met, otherwise false will be returned.
    */
    internal static bool is_digit_char( char digit, char max_digit = '9' )
        {
        return is_digit_char(digit, '0', max_digit);
        }

    /** @details   This function checks that a given string is not a null object or an empty string and
                   consists only of digits.

        @param[in] numeric_string String object to be checked.

        @return    Will return true is all criteria are met, otherwise false will be returned.
    */
    public static bool is_digit_string( string numeric_string )
        {
        if( string.IsNullOrEmpty(numeric_string) )
            {
            return false;
            }

        for( int i = 0; i < numeric_string.Length; ++i )
            {
            if( !Char.IsDigit(numeric_string[i]) )
                {
                return false;
                }
            }
        return true;
        }

    /** @details   This function checks that a given string is not a null object or an empty string and
                   consists only of digit characters. Any non digit character within the string will 
                   result in a false value. In addition the string may only have a
                   maximum length.

        @param[in] numeric_string  String object to be checked.
        @param[in] expected_length Number of characters the string object must have.

        @return    Will return true is all criteria are met, otherwise false will be returned.

    */
    public static bool is_digit_string( string numeric_string, int expected_length )
        {
        if( !is_digit_string(numeric_string) )
            {
            return false;
            }

        if( numeric_string.Length == expected_length )
            {
            return true;
            }

        return false;
        }

    private static bool is_printable_string( string text, int start_index, int end_index )
        {
        for( int i = start_index; i < end_index; ++i )
            {
            if( Char.IsControl(text, i) )
                {
                return false;
                }
            }
        return true;
        }

    /** @details   The function returns true if the string passed does not
                   contain Unicode characters that are categorized as control
                   characters. A control code character whose Unicode value
                   is U+007F or is in the range between U+0000 and U+001F or
                   between U+0080 and U+009F will lead to a false result.

        @param[in] text String object to be checked.

        @return    Will return true if text is a valid not empty string object
                   which does not contain any control characters. Otherwise
                   false will be returned.
    */
    public static bool is_printable_string( string text )
        {
        if( string.IsNullOrEmpty(text) )
            {
            return false;
            }

        return is_printable_string(text, 0, text.Length);
        }

    /** @details   This function checks that a given string is not a null object or an empty string and
                   consists only of printable characters. Any control character or whitespace character
                   within the string will return a false value. In addition the string may only have a
                   maximum length.

        @param[in] text       String object to be checked.
        @param[in] max_length Maximum number of characters the string object may have.

        @return    Will return true is all criteria are met, otherwise false will be returned.
   */
   public static bool is_printable_string( string text, int max_length )
        {
        if( !is_printable_string(text) )
            {
            return false;
            }

        if( text.Length > max_length )
            {
            return false;
            }

        return true;
        }

    /** @details   This function checks that a given string is not a null object and consists only of
                   printable characters. Any control character or whitespace character within the string
                   will return a false value. In addition the string may only have a maximum length.

        @param[in] text       String object to be checked.
        @param[in] max_length Maximum number of characters the string object may have.

        @return    Will return true is all criteria are met, otherwise false will be returned.
   */
   public static bool is_printable_string_or_empty( string text, int max_length )
        {
        if( text == null )
            {
            return false;
            }

        if( text.Length == 0 )
            {
            return true;
            }

        return is_printable_string(text, max_length);
        }

    /** @details   This function will remove leading zero digits from a string object which is not empty or
                   a null object. For a given string with more than one character it is guaranteed that
                   the resulting string will have a size of at least one character.

        @param[in] text  String object where leading zero digit characters are to be removed.

        @return    Will return the untouched string object if it is a null object or an empty string. Otherwise
                   a string object is returned having at least one character and all leading zero digit characters
                   removed.

        @code{.cs}
        ...
        digit_string = Texting.remove_leading_zeros(digit_string);
        ...
        @endcode
    */
    public static string remove_leading_zeros( string text )
        {
        if( string.IsNullOrEmpty(text) )
            {
            return text;
            }

        for( ; text[0] == '0'; text = text.Substring(1) )
            {
            if( text.Length == 1 )
                {
                break;
                }
            }
        return text;
        }


    /** @details

        @param[in] text

        @return
    */
    public static string get_string( string text )
        {
        return string.Format(CultureInfo.InvariantCulture, text);
        }

    /** @details

        @param[in] format_string
        @param[in] args

        @return
    */
    public static string get_string( string format_string, params object[] args )
        {
        return string.Format(CultureInfo.InvariantCulture, format_string, args);
        }

    /** @details   Convert a string into a little endian UTF-16 encoded byte array.

        @param[in] text String to be converted.

        @return
    */
    public static byte[] to_byte_array( string text )
        {
        return Encoding.Unicode.GetBytes(text);
        }

    /** @details   Convert a little endian UTF-16 encoded byte array into a string.

        @param[in] byte_array Little endian UTF-16 encoded byte array to be converted.

        @return    
    */
    public static string from_byte_array( byte[] byte_array )
        {
        return Encoding.Unicode.GetString(byte_array);
        }
    }
}
