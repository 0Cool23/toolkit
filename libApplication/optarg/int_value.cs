/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2021

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_IntValue optarg.IntValue
    @{
    @details  Parses numerical integer value option argument string.

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;

using libApplication.arg;
using libApplication.toolkit;

namespace libApplication.optarg
{
/** @ingroup REF_IntValue
 
    @class IntValue

    @brief   Parses numeral integer value option argument string.

    @details Numeral integer arguments may have various formats. They are parsed
             in a defined order. For better reading the digits may be separated
             using an '_' literal.

             - A binary-literal is the character sequence 0b or the character
               sequence 0B followed by at least one or more binary digits (0, 1).
             - A hex-literal is the character sequence 0x or the character
               sequence 0X followed by at least one or more hexadecimal digits
               (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, a, A, b, B, c, C, d, D, e, E, f, F).
             - A octal-literal is the digit zero (0) followed by zero or more
               octal digits (0, 1, 2, 3, 4, 5, 6, 7).
             - decimal-literal is the decimal digit 0 or one of the digits 1, 2,
               3, 4, 5, 6, 7, 8, 9), followed by zero or more decimal digits
               (0, 1, 2, 3, 4, 5, 6, 7, 8, 9). A decimal-literal may start with
               '+' or '-' defining the value as signed type.
*/

public class IntValue
    :   Argument
    {
    public IntValue()
        {
        }

    public IntValue( long upper_limit )
        :   this((ulong)0, Numeric.to_ulong(upper_limit))
        {
        }

    public IntValue( long lower_limit, long upper_limit )
        :   this(Numeric.to_ulong(lower_limit), Numeric.to_ulong(upper_limit))
        {
        }

    public IntValue( ulong upper_limit )
        :   this((ulong)0, upper_limit)
        {
        }

    public IntValue( ulong v1, ulong v2 )
        {
        min_value = (v1 < v2) ? v1 : v2;
        max_value = (v1 > v2) ? v1 : v2;
        }

    /** @brief Signed representation of the parsed value. */
    public long Signed
        {
        get;
        private set;
        } = 0;

    /** @brief Unsigned representation of the parsed value. */
    public ulong Unsigned
        {
        get;
        private set;
        } = 0;

    private readonly ulong min_value = ulong.MinValue;
    private readonly ulong max_value = ulong.MaxValue;

    protected virtual void check_range()
        {
        if( !Unsigned.is_within<ulong>(min_value, max_value) )
            {
            throw new ArgumentOutOfRangeException(Texting.get_string(Properties.language.NUMBER_OUT_OF_RANGE, Name, min_value, max_value));
            }
        }

    private delegate long SingendFunc( string number_str ); 
    private delegate ulong UnsingendFunc( string number_str ); 

    private void parse_number( object parse_func, string number_str )
        {
        if( parse_func is UnsingendFunc )
            {
            Unsigned = (parse_func as UnsingendFunc)(number_str);
            Signed = Numeric.to_long(Unsigned);
            }
        else
            {
            Signed = (parse_func as SingendFunc)(number_str);
            Unsigned = Numeric.to_ulong(Signed);
            }
        }

    private static readonly List<object> the_function_list = new List<object>
        {
        (UnsingendFunc)BinNumber.parse, /* 0b[0-1]+ */
        (UnsingendFunc)HexNumber.parse, /* 0x[0-9a-f]+ */
        (UnsingendFunc)OctNumber.parse, /* 0[0-7]* */
        (SingendFunc)DecNumber.parse,   /* [+|-]?(0|[1-9][0-9]*) */
        };

    private bool parse_number_safe( object function, string number_str )
        {
        try
            {
            parse_number(function, number_str);
            return true;
            }
        catch /* number_str format does not match, try next one */
            {
            }
        return false;
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
        string number_str = OptionStr.Trim();
        foreach( var function in the_function_list )
            {
            if( parse_number_safe(function, number_str) )
                {
                check_range();
                return;
                }
            }
        throw new ArgumentException();
        }
    }
}
