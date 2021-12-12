/** @file,

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_TimeValue optarg.TimeValue
    @{
    @details  Parses time string.

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using libApplication.arg;
using libApplication.toolkit;

namespace libApplication.optarg
{
/** @ingroup REF_TimeValue

    @class   TimeValue

    @details 
*/
public class TimeValue
    :   SiValue
    {
    public TimeValue()
        :   this(double.NaN)
        {
        }

    public TimeValue( double upper_limit )
        :   this(double.NaN, upper_limit)
        {
        }

    public TimeValue( double v1, double v2 )
        :   base("s", v1, v2)
        {
        }

    private TimeSpan m_timespan = new TimeSpan(0);
    public TimeSpan Timespan
        {
        get => m_timespan;
        }

    private static readonly Dictionary<long, string> the_nomalization_map = new Dictionary<long, string>
        {
            {TimeSpan.TicksPerDay,    @"d\:hh\:mm\:ss\.ffffff\d"},
            {TimeSpan.TicksPerHour,   @"h\:mm\:ss\.ffffff\h"},
            {TimeSpan.TicksPerMinute, @"m\:ss\.ffffff\m\i\n"},
        };

    protected override void value_update_callback()
        {
        m_timespan = new TimeSpan((long)(Value * TimeSpan.TicksPerSecond));
        foreach( var max_ticks in the_nomalization_map.Keys.OrderBy(x => x).Reverse() )
            {
            if( Timespan.Ticks >= max_ticks )
                {
                NormalizedStr = string.Format("{0}", Timespan.ToString(the_nomalization_map[max_ticks], CultureInfo.InvariantCulture));
                break;
                }
            }
        }

    private delegate void TryParseFunction( ref TimeSpan time_span );
    private class TryParseEntry
        {
        public readonly string           FormatStr     = null;
        public readonly TryParseFunction ParseFunction = null;

        public TryParseEntry( string format_str, TryParseFunction parse_function )
            {
            FormatStr     = format_str;
            ParseFunction = parse_function;
            }
        };

    private static void parse_minutes( ref TimeSpan time_span )
        {
        var ticks_frac = (time_span.Ticks % TimeSpan.TicksPerSecond) * 60;
        time_span = new TimeSpan(0, 0, time_span.Days, 0);
        time_span = time_span.Add(new TimeSpan(ticks_frac));
        }

    private static void parse_minutes_seconds( ref TimeSpan time_span )
        {
        var ticks_frac = (time_span.Ticks % TimeSpan.TicksPerSecond);
        time_span = new TimeSpan(0, 0, time_span.Days, time_span.Seconds);
        time_span = time_span.Add(new TimeSpan(ticks_frac));
        }

    private static void parse_hours( ref TimeSpan time_span )
        {
        var ticks_frac = (time_span.Ticks % TimeSpan.TicksPerSecond) * 3600;
        time_span = new TimeSpan(0, time_span.Days, 0, 0);
        time_span = time_span.Add(new TimeSpan(ticks_frac));
        }

    private static void parse_hours_minutes( ref TimeSpan time_span )
        {
        var ticks_frac = (time_span.Ticks % TimeSpan.TicksPerSecond) * 60;
        time_span = new TimeSpan(0, time_span.Days, time_span.Minutes, 0);
        time_span = time_span.Add(new TimeSpan(ticks_frac));
        }

    private static void parse_hours_minutes_seconds( ref TimeSpan time_span )
        {
        var ticks_frac = (time_span.Ticks % TimeSpan.TicksPerSecond);
        time_span = new TimeSpan(0, time_span.Days, time_span.Minutes, time_span.Seconds);
        time_span = time_span.Add(new TimeSpan(ticks_frac));
        }

    private static void parse_days( ref TimeSpan time_span )
        {
        var ticks_frac = (time_span.Ticks % TimeSpan.TicksPerSecond) * 86400;
        time_span = new TimeSpan(time_span.Days, 0, 0, 0);
        time_span = time_span.Add(new TimeSpan(ticks_frac));
        }

    private static void parse_days_hours( ref TimeSpan time_span )
        {
        var ticks_frac = (time_span.Ticks % TimeSpan.TicksPerSecond) * 3600;
        time_span = new TimeSpan(time_span.Days, time_span.Hours, 0, 0);
        time_span = time_span.Add(new TimeSpan(ticks_frac));
        }

    private static void parse_days_hours_minutes( ref TimeSpan time_span )
        {
        var ticks_frac = (time_span.Ticks % TimeSpan.TicksPerSecond) * 60;
        time_span = new TimeSpan(time_span.Days, time_span.Hours, time_span.Minutes, 0);
        time_span = time_span.Add(new TimeSpan(ticks_frac));
        }

    private static void parse_days_hours_minutes_seconds( ref TimeSpan time_span )
        {
        var ticks_frac = (time_span.Ticks % TimeSpan.TicksPerSecond);
        time_span = new TimeSpan(time_span.Days, time_span.Hours, time_span.Minutes, time_span.Seconds);
        time_span = time_span.Add(new TimeSpan(ticks_frac));
        }

    private static string try_parse( string option_str, string symbol, List<TryParseEntry> parse_entry_list )
        {
        var value_str = option_str.Substring(0, option_str.Length - symbol.Length);
        foreach( var parse_entry in parse_entry_list )
            {
            if( TimeSpan.TryParseExact(value_str, parse_entry.FormatStr, CultureInfo.InvariantCulture, out TimeSpan time_span) )
                {
                parse_entry.ParseFunction(ref time_span);
                return string.Format(CultureInfo.InvariantCulture, "{0:r}s", (double)time_span.Ticks / (double)TimeSpan.TicksPerSecond);
                }
            }
        throw new FormatException(Texting.get_string(Properties.language.TIME_VALUE_FORMAT, option_str));
        }

    private static readonly Dictionary<string, List<TryParseEntry>> the_preparse_map = new Dictionary<string, List<TryParseEntry>>
        {
            {
                "min", new List<TryParseEntry>
                    {
                    new TryParseEntry(@"d\:ss\.FFFFFF",         parse_minutes_seconds),
                    new TryParseEntry(@"d\:ss",                 parse_minutes_seconds),
                    new TryParseEntry(@"d\.FFFFFF",             parse_minutes),
                    new TryParseEntry(@"c",                     parse_minutes),
                    }
            },
            {
                "h", new List<TryParseEntry>
                    {
                    new TryParseEntry(@"d\:mm\:ss\.FFFFFF",     parse_hours_minutes_seconds),
                    new TryParseEntry(@"d\:mm\:ss",             parse_hours_minutes_seconds),
                    new TryParseEntry(@"d\:mm\.FFFFFF",         parse_hours_minutes),
                    new TryParseEntry(@"d\:mm",                 parse_hours_minutes),
                    new TryParseEntry(@"d\.FFFFFF",             parse_hours),
                    new TryParseEntry(@"c",                     parse_hours),
                    }
            },
            {
                "d", new List<TryParseEntry>
                    {
                    new TryParseEntry(@"d\:hh\:mm\:ss\.FFFFFF", parse_days_hours_minutes_seconds),
                    new TryParseEntry(@"d\:hh\:mm\:ss",         parse_days_hours_minutes_seconds),
                    new TryParseEntry(@"d\:hh\:mm\.FFFFFF",     parse_days_hours_minutes),
                    new TryParseEntry(@"d\:hh\:mm",             parse_days_hours_minutes),
                    new TryParseEntry(@"d\:hh\.FFFFFF",         parse_days_hours),
                    new TryParseEntry(@"d\:hh",                 parse_days_hours),
                    new TryParseEntry(@"d\.FFFFFF",             parse_days),
                    new TryParseEntry(@"c",                     parse_days),
                    }
            },
        };

    private string preparse()
        {
        var option_str = OptionStr.Trim();
        foreach( var preparse_symbol in the_preparse_map.Keys.OrderBy(x => x.Length).Reverse() )
            {
            if( option_str.EndsWith(preparse_symbol) )
                {
                return try_parse(option_str, preparse_symbol, the_preparse_map[preparse_symbol]);
                }
            }
        return option_str;
        }

    /** @details   Parses the string representation of time string value.
    */
    protected override void update()
        {
        var option_str = preparse();
        update(option_str);
        }
    }
}
