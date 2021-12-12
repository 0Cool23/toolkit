/** @file,

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_DateValue optarg.DateValue
    @{
    @details  Parses date time string.

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using libApplication.arg;
using libApplication.toolkit;

namespace libApplication.optarg
{
/** @ingroup REF_DateValue

    @class   DateValue

    @details 
*/
public class DateValue
    :   Argument
    {
    public const string DEFAULT_TIMESTAMP   = "1970-01-01T00:00:00+00:00";
    public const string DEFAULT_DATE_FORMAT = "yyyy-MM-ddTHH:mm:ss.FFFFFFFzzz";

    public DateValue()
        {
        }

    private DateTime m_timestamp = DateTime.ParseExact(DEFAULT_TIMESTAMP, DEFAULT_DATE_FORMAT, CultureInfo.InvariantCulture);

    public DateTime Timestamp
        {
        get => m_timestamp;
        }

    private delegate DateTime DispatchFunction();
    private static readonly Dictionary<string, DispatchFunction> the_dispatch_function_map = new Dictionary<string, DispatchFunction>
        {
            {"now",   new DispatchFunction(now)},
            {"today", new DispatchFunction(today)},
        };

    private static DateTime now()
        {
        return DateTime.Now;
        }

    private static DateTime today()
        {
        var current_date = DateTime.Now;
        return new DateTime(current_date.Year, current_date.Month, current_date.Day, 0, 0, 0, 0);
        }

    private static readonly List<string> the_datestamp_format_list = new List<string>
        {
        DEFAULT_DATE_FORMAT,
        "yyyy-MM-dd",
        "yyyy-MM-ddzzz",
        "yyyy-MM-ddTHH:mm:ss.FFFFFFF",
        };

    private void try_parse( string timestamp_string )
        {
        if( DateTime.TryParseExact(timestamp_string, the_datestamp_format_list.ToArray(), CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out DateTime timestamp) )
            {
            m_timestamp = timestamp;
            return;
            }
        throw new ArgumentException(Texting.get_string(Properties.language.INVALID_DATE_FORMAT, timestamp_string));
        }

    /** @details   Parses the string representation of timestamp value.
    */
    protected override void update()
        {
        if( the_dispatch_function_map.ContainsKey(OptionStr))
            {
            m_timestamp = the_dispatch_function_map[OptionStr]();
            return;
            }
        try_parse(OptionStr);
        }
    }
}
