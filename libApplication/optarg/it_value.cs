using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using libApplication.arg;

namespace libApplication.optarg
{
public abstract class ItValue
    :   ArgUnit
    {
    private readonly IntValue m_argument = null;

    private ulong m_value = 0;
    public ulong Value
        {
        get         => m_value;
        private set => update_properties(value);
        }

    public string ValueStr
        {
        get;
        private set;
        }

    public string NormalizedStr
        {
        get;
        private set;
        }

    protected virtual void value_update_callback()
        {
        }

    private bool get_normalized_str( KeyValuePair<string, iMagnitudeEntry> magnitude_entry, ref string normalized_str )
        {
        if( !magnitude_entry.Value.isNormalized )
            {
            return false;
            }
        double tmp_value = (double)Value / (double)(the_it_magnitude_map[magnitude_entry.Key] as ItMagnitudeEntry).Magnitude;
        if( (1.0 <= tmp_value) && (tmp_value < 1000.0) )
            {
            normalized_str = string.Format(CultureInfo.InvariantCulture, "{0:r}{1}{2}", tmp_value, magnitude_entry.Key, Symbol);
            }
        return (tmp_value == (ulong)tmp_value);
        }

    private string get_normalized_str()
        {
        var normalized_str = ValueStr;
        foreach( var magnitude_entry in the_it_magnitude_map.OrderBy(x => (x.Value as ItMagnitudeEntry).Magnitude).Reverse() )
            {
            if( get_normalized_str(magnitude_entry, ref normalized_str) )
                {
                break;
                }
            }
        return normalized_str;
        }

    private void update_properties()
        {
        ValueStr      = string.Format(CultureInfo.InvariantCulture, "{0}{1}", Value, Symbol);
        NormalizedStr = get_normalized_str();
        value_update_callback();
        }

    private void update_properties( ulong value )
        {
        if( m_value == value )
            {
            return;
            }
        m_value = value;
        update_properties();
        }

    public ItValue( string symbol, ulong lower_limit, ulong upper_limit )
        :   base(symbol)
        {
        m_argument = new IntValue(lower_limit, upper_limit);
        update_properties();
        }

    private class ItMagnitudeEntry
        :   iMagnitudeEntry
        {
        public bool isNormalized
            {
            get;
            private set;
            }

        public readonly ulong Magnitude;

        public ItMagnitudeEntry( bool is_normalized, ulong magnitude )
            {
            isNormalized = is_normalized;
            Magnitude    = magnitude;
            }
        }

    private static readonly Dictionary<string, iMagnitudeEntry> the_it_magnitude_map = new Dictionary<string, iMagnitudeEntry>
        {
            {"k",   new ItMagnitudeEntry(true,                1000ul)},  // kilo
            {"Ki",  new ItMagnitudeEntry(true,                1024ul)},  // kibi
            {"M",   new ItMagnitudeEntry(true,             1000000ul)},  // mega
            {"Mi",  new ItMagnitudeEntry(true,             1048576ul)},  // mebi
            {"G",   new ItMagnitudeEntry(true,          1000000000ul)},  // giga
            {"Gi",  new ItMagnitudeEntry(true,          1073741824ul)},  // gibi
            {"T",   new ItMagnitudeEntry(true,       1000000000000ul)},  // tera
            {"Ti",  new ItMagnitudeEntry(true,       1099511627776ul)},  // tebi
            {"P",   new ItMagnitudeEntry(true,    1000000000000000ul)},  // peta
            {"Pi",  new ItMagnitudeEntry(true,    1125899906842624ul)},  // pebi
            {"E",   new ItMagnitudeEntry(true, 1000000000000000000ul)},  // exa
            {"Ei",  new ItMagnitudeEntry(true, 1152921504606846976ul)},  // exbi
            /* 64-Bit (ulong)  won't fit any more
            {"Z",   new ItMagnitudeEntry(true, ulong_pow(1000, 7))},  // zetta
            {"Zi",  new ItMagnitudeEntry(true, ulong_pow(1024, 7))},  // zebi
            {"Y",   new ItMagnitudeEntry(true, ulong_pow(1000, 8))},  // yotta
            {"Yi",  new ItMagnitudeEntry(true, ulong_pow(1024, 8))},  // yobi
            */
        };

    protected override void update()
        {
        parse(m_argument, OptionStr, the_it_magnitude_map, out string magnitude);

        var multiplyer = 1ul;
        if( !string.IsNullOrEmpty(magnitude) )
            {
            multiplyer = (the_it_magnitude_map[magnitude] as ItMagnitudeEntry).Magnitude;
            }
        Value = m_argument.Unsigned * multiplyer;
        }
    }
}
