using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using libApplication.arg;

namespace libApplication.optarg
{
public abstract class SiValue
    :   ArgUnit
    {
    private readonly RealValue m_argument = null;

    private double m_value = 0.0;
    public double Value
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
        protected set;
        }

    protected virtual void value_update_callback()
        {
        }

    private void get_normalized_str( KeyValuePair<string, iMagnitudeEntry> magnitude_entry, ref string normalized_str )
        {
        if( !magnitude_entry.Value.isNormalized )
            {
            return;
            }
        var tmp_value = Math.Abs(Value) / (the_si_magnitude_map[magnitude_entry.Key] as SiMagnitudeEntry).Magnitude;
        if( (1.0 <= tmp_value) && (tmp_value < 1000.0) )
            {
            tmp_value = Value / (the_si_magnitude_map[magnitude_entry.Key] as SiMagnitudeEntry).Magnitude;
            normalized_str = string.Format(CultureInfo.InvariantCulture, "{0:r}{1}{2}", tmp_value, magnitude_entry.Key, Symbol);
            }
        }

    private string get_normalized_str()
        {
        var normalized_str = ValueStr;
        foreach( var magnitude_entry in the_si_magnitude_map )
            {
            get_normalized_str(magnitude_entry, ref normalized_str);
            }
        return normalized_str;
        }

    private void update_properties()
        {
        ValueStr      = string.Format(CultureInfo.InvariantCulture, "{0:r}{1}", Value, Symbol);
        NormalizedStr = get_normalized_str();
        value_update_callback();
        }

    private void update_properties( double value )
        {
        if( m_value == value )
            {
            return;
            }
        m_value = value;
        update_properties();
        }

    protected SiValue( string symbol, double v1, double v2 )
        :   base(symbol)
        {
        m_argument = new RealValue(v1, v2);
        update_properties();
        }

    private class SiMagnitudeEntry
        :   iMagnitudeEntry
        {
        public bool isNormalized
            {
            get;
            private set;
            }

        public readonly double Magnitude;

        public SiMagnitudeEntry( bool is_normalized, double magnitude )
            {
            isNormalized = is_normalized;
            Magnitude    = magnitude;
            }
        }

    private static readonly Dictionary<string, iMagnitudeEntry> the_si_magnitude_map = new Dictionary<string, iMagnitudeEntry>
        {
            {"Y",      new SiMagnitudeEntry(true,  Math.Pow(10,  24))},   // yotta
            {"Z",      new SiMagnitudeEntry(true,  Math.Pow(10,  21))},   // zetta
            {"E",      new SiMagnitudeEntry(true,  Math.Pow(10,  18))},   // exa
            {"P",      new SiMagnitudeEntry(true,  Math.Pow(10,  15))},   // peta
            {"T",      new SiMagnitudeEntry(true,  Math.Pow(10,  12))},   // tera
            {"G",      new SiMagnitudeEntry(true,  Math.Pow(10,   9))},   // giga
            {"M",      new SiMagnitudeEntry(true,  Math.Pow(10,   6))},   // mega
            {"k",      new SiMagnitudeEntry(true,  Math.Pow(10,   3))},   // kilo
            {"h",      new SiMagnitudeEntry(false, Math.Pow(10,   2))},   // hecto
            {"da",     new SiMagnitudeEntry(false, Math.Pow(10,   1))},   // deca
            {"d",      new SiMagnitudeEntry(false, Math.Pow(10,  -1))},   // deci
            {"c",      new SiMagnitudeEntry(false, Math.Pow(10,  -2))},   // centi
            {"m",      new SiMagnitudeEntry(true,  Math.Pow(10,  -3))},   // milli
            {"u",      new SiMagnitudeEntry(false, Math.Pow(10,  -6))},   // micro
            {"\u03bc", new SiMagnitudeEntry(true,  Math.Pow(10,  -6))},   // micro
            {"n",      new SiMagnitudeEntry(true,  Math.Pow(10,  -9))},   // nano
            {"p",      new SiMagnitudeEntry(true,  Math.Pow(10, -12))},   // pico
            {"f",      new SiMagnitudeEntry(true,  Math.Pow(10, -15))},   // femto
            {"a",      new SiMagnitudeEntry(true,  Math.Pow(10, -18))},   // atto
            {"z",      new SiMagnitudeEntry(true,  Math.Pow(10, -21))},   // zepto
            {"y",      new SiMagnitudeEntry(true,  Math.Pow(10, -24))},   // yocto
        };

    protected void update( string option_str )
        {
        parse(m_argument, option_str, the_si_magnitude_map, out string magnitude);

        var multiplyer = 1.0;
        if( !string.IsNullOrEmpty(magnitude) )
            {
            multiplyer = (the_si_magnitude_map[magnitude] as SiMagnitudeEntry).Magnitude;
            }
        Value = m_argument.Value * multiplyer;
        }

    protected override void update()
        {
        update(OptionStr);
        }
    }
}
