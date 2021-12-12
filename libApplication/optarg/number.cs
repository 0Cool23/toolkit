/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_Number optarg.Number
    @{
    @details  Parses numerical option arguments.

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using libApplication.arg;
using libApplication.toolkit;

namespace libApplication.optarg
{
/** @ingroup REF_Number

    @class   Number

    @brief   Parses numeral option arguments.

    @details Numeral arguments may have various formats. They are parsed in
             a defined order. For better reading the digits may be separated
             using an '_' literal.

             - A binary-literal is the character sequence 0b
               or the character sequence 0B followed by at least one or more
               binary digits (0, 1).
             - A hex-literal is the character sequence 0x or the character
               sequence 0X followed by at least one or more hexadecimal digits
               (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, a, A, b, B, c, C, d, D, e, E, f,
               F).
             - A octal-literal is the digit zero (0) followed by zero or more
               octal digits (0, 1, 2, 3, 4, 5, 6, 7).
             - decimal-literal is a decimal digit (0, 1, 2, 3, 4, 5, 6,
               7, 8, 9), followed by zero or more decimal digits (0, 1, 2, 3,
               4, 5, 6, 7, 8, 9). A decimal-literal may start with '+' or '-'
               defining the value as signed type.
*/
public class Number
    :   Argument
    {
    public Number()
        {
        }

    public Number( long upper_limit )
        :   this((ulong)0, Numeric.to_ulong(upper_limit))
        {
        }

    public Number( long lower_limit, long upper_limit )
        :   this(Numeric.to_ulong(lower_limit), Numeric.to_ulong(upper_limit))
        {
        }

    public Number( ulong upper_limit )
        :   this((ulong)0, upper_limit)
        {
        }

    public Number( ulong v1, ulong v2 )
        {
        min_value = (v1 < v2) ? v1 : v2;
        max_value = (v1 > v2) ? v1 : v2;
        }

    /** @brief Signed representation of the parsed value. */
    public long signed
        {
        get;
        private set;
        }

    /** @brief Unsigned representation of the parsed value. */
    public ulong unsigned
        {
        get;
        private set;
        }

    private readonly ulong min_value = ulong.MinValue;
    private readonly ulong max_value = ulong.MaxValue;

    protected virtual void check_range()
        {
        if( !unsigned.is_within<ulong>(min_value, max_value) )
            {
            throw new ArgumentOutOfRangeException(Texting.get_string(Properties.language.NUMBER_OUT_OF_RANGE, Name, min_value, max_value));
            }
        }

    /** @brief   Parses the string representation of the numeric value.

        @details If the value cannot be parsed properly an exception is thrown.
                 Allowed values may be given in binary-literal, hex-literal,
                 octal-literal or decimal-literal representation.

        @exception ArgumentException is thrown if parsing does not match one of
                   the literal formats.
    */
    protected override void update()
        {
        do
            {
            string number_str = OptionStr.Trim();
            try
                {
                unsigned = BinNumber.parse(number_str);
                signed   = Numeric.to_long(unsigned);
                break;
                }
            catch{ /* String is not binary format, try next one */ }
            try
                {
                unsigned = HexNumber.parse(number_str);
                signed   = Numeric.to_long(unsigned);
                break;
                }
            catch{ /* String is not hexadecimal format, try next one */ }
            try
                {
                unsigned = OctNumber.parse(number_str);
                signed   = Numeric.to_long(unsigned);
                break;
                }
            catch{ /* String is not octal format, try next one */ }
            try
                {
                signed   = DecNumber.parse(number_str);
                unsigned = Numeric.to_ulong(signed);
                break;
                }
            catch{ /* String is not decimal format, try next one */ }
            throw new ArgumentException();
            }
        while( false );
        check_range();
        }
    }
}
