/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2021

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_RealNumber toolkit.RealNumber
    @{
    @details

    @ingroup REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace libApplication.toolkit
{
/** @ingroup REF_RealNumber

    @class   RealNumber

    @brief   Parses real numeral format option argument string.
*/
public static class RealNumber
    {
    /** @details

        @param[in] number_str

        @return
    */
    public static double parse( string number_str )
        {
        if( string.IsNullOrEmpty(number_str) )
            {
            throw new ArgumentNullException(Properties.language.NUMBER_PARSE_NULL);
            }

        number_str = update_real_number_sign(number_str.Trim());
        return check_real_number_format(number_str);
        }

    private static string update_real_number_sign( string number_str )
        {
        if( number_str.StartsWith("+") || number_str.StartsWith("-") )
            {
            return number_str;
            }
        return "+" + number_str;
        }

    private static readonly Dictionary<string, double> the_special_number_map = new()
        {
            {"+nan", double.NaN},
            {"+inf", double.PositiveInfinity},
            {"-inf", double.NegativeInfinity},
            {"+eps", double.Epsilon},
            {"-eps", -double.Epsilon},
            {"+e",   Math.E},
            {"-e",   -Math.E},
            {"+pi",  Math.PI},
            {"-pi",  -Math.PI},
        };

    private static readonly string the_separator = Regex.Escape(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);

    private static readonly List<string> the_real_format_pattern_list = new()
        {
        @"^(\+|-)(0|[1-9][0-9]*)" + @"(" + the_separator + @")?" + @"(e(\+|-)?(0|[1-9][0-9]*))?$",
        @"^(\+|-)" + @"(" + the_separator + @")" + @"([0-9]+)" + @"(e(\+|-)?(0|[1-9][0-9]*))?$",
        @"^(\+|-)(0|[1-9][0-9]*)" + @"(" + the_separator + @")" + @"([0-9]+)" + @"(e(\+|-)?(0|[1-9][0-9]*))?$",
        };

    private static double check_real_number_regex( string number_str )
        {
        number_str = number_str.Replace("_", string.Empty).ToLower();
        foreach( var real_format_pattern in the_real_format_pattern_list )
            {
            if( Regex.Match(number_str, real_format_pattern).Success )
                {
                return Convert.ToDouble(number_str, CultureInfo.InvariantCulture);
                }
            }
        throw new FormatException(Texting.get_string(Properties.language.INVALID_REAL_NUMBER_FORMAT));
        }

    private static double check_real_number_format( string number_str )
        {
        // check for fixed named values
        if( the_special_number_map.ContainsKey(number_str) )
            {
            return the_special_number_map[number_str];
            }
        
        return check_real_number_regex(number_str);
        }
    }
}
