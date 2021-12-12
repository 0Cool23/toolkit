/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2021

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_RealValue optarg.RealValue
    @{
    @details  Parses numerical real value option argument string.

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;
using System.Text;

using libApplication.arg;
using libApplication.toolkit;

namespace libApplication.optarg
{
/** @ingroup REF_RealValue
 
    @class   RealValue

    @brief   Parses numeral real value option argument string.

    @details Numeral real arguments may have various formats. They are parsed
             in a defined order. For better reading the digits may be separated
             using an '_' literal.

             - Defined constant values
                * 'nan' or '+nan' for "not a number" value.
                * 'inf' or '+inf' for "positive infinite" value.
                * '-inf' for "negative infinite" value.
                * 'eps' or '+eps' for smallest positive value which is greater than 0.
                * '-eps' for smallest negative value which is smaller than 0.
                * 'pi' or '+pi' for constant equal to a circle's circumference divided by its diameter.
                * '-pi' for negative constant equal to a circle's circumference divided by its diameter.
                * 'e' or '+e' for the Euler's number as base of the natural logarithm.
                * '-e' for the negative Euler's number.
             - 
*/

public class RealValue
    :   Argument
    {
    public RealValue()
        {
        }

    public RealValue( double upper_limit )
        :   this(double.NaN, upper_limit)
        {
        }

    public RealValue( double v1, double v2 )
        {
        if( (!double.IsNaN(v1)) && (!double.IsNaN(v2)) )
            {
            min_value = Math.Min(v1, v2);
            max_value = Math.Max(v1, v2);
            }
        else
            {
            min_value = v1;
            max_value = v2;
            }
        }

    private readonly double min_value = double.NaN;
    private readonly double max_value = double.NaN;

    /** @brief Unsigned representation of the parsed value. */
    public double Value
        {
        get;
        private set;
        } = 0.0;

    private delegate bool RangeFunc( double value, double min, double max );
    private static readonly List<RangeFunc> the_range_func_list = new List<RangeFunc>
        {
        new RangeFunc(no_range_check),
        new RangeFunc(is_lesser_check),
        new RangeFunc(is_greater_check),
        new RangeFunc(is_within_check),
        };

    private static bool no_range_check( double value, double min, double max )
        {
        /* if there are no boundaries any value is valid */
        return (double.IsNaN(min) && double.IsNaN(max));
        }

    private static bool is_lesser_check( double value, double min, double max )
        {
        if( double.IsNaN(min) && (!double.IsNaN(max)) )
            {
            return (value.CompareTo(max) <= 0);
            }
        return false;
        }

    private static bool is_greater_check( double value, double min, double max )
        {
        if( (!double.IsNaN(min)) && double.IsNaN(max) )
            {
            return (value.CompareTo(min) >= 0);
            }
        return false;
        }

    private static bool is_within_check( double value, double min, double max )
        {
        if( (!double.IsNaN(min)) && (!double.IsNaN(max)) )
            {
            return value.is_within<double>(min, max);
            }
        return false;
        }

    protected virtual void check_range()
        {
        foreach( var range_func in the_range_func_list )
            {
            if( range_func(Value, min_value, max_value) )
                {
                return;
                }
            }
        throw new ArgumentOutOfRangeException(Texting.get_string(Properties.language.NUMBER_OUT_OF_RANGE, Name, min_value, max_value));
        }

    /** @brief   Parses the string representation of the numeric value.

        @details If the value cannot be parsed properly an exception is thrown.

        @exception FormatException is thrown if parsing does not match one of
                   the literal formats.

                   ArgumentNullException is thrown if parser passes a null value.

                   ArgumentException is thrown when non of the formats apply.
    */
    protected override void update()
        {
        string number_str = OptionStr.Trim();
        Value = RealNumber.parse(number_str);
        check_range();
        }
    }
}
