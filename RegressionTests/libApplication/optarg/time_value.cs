/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;

using libApplication.arg;
using libApplication.optarg;

namespace RegressionTests.libApplication.optarg
{
/** @ingroup GRP_Testing

    @class   TimeValue_test

    @details Unit test for @ref REF_TimeValue class.
*/
[TestClass]
//[Ignore]
public class TimeValue_test
    {
    /** @test Test to make sure that @ref REF_TimeValue defaults are properly set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var time_value = new TimeValue();
        Assert.AreEqual(0ul, time_value.Value, "Value property is expected to be 0 after construction.");
        Assert.AreEqual("0s", time_value.ValueStr, "ValueStr property is expected to be 0s after construction.");
        Assert.AreEqual("0s", time_value.NormalizedStr, "NormalizedStr property is expected to be 0s after construction.");
        Assert.AreEqual(0, time_value.Timespan.Ticks, "Timespan.Ticks property is expected to be 0 after construction.");
        }

    private class VerificationResult
        {
        public readonly double Value         = double.NaN;
        public readonly string ValueStr      = null;
        public readonly string NormalizedStr = null;
        public readonly long   Ticks         = long.MinValue;

        public VerificationResult( double value, string value_str, string normalized_str, long ticks )
            {
            Value         = value;
            ValueStr      = value_str;
            NormalizedStr = normalized_str;
            Ticks         = ticks;
            }
        }

    private static void verify_results( TimeValue time_value, string parsed_str, VerificationResult expected )
        {
        Assert.AreEqual(expected.Value,         time_value.Value, "Value property did not match expected after parsing {0}.", parsed_str);
        Assert.AreEqual(expected.ValueStr,      time_value.ValueStr, "ValueStr property did not match expected after parsing {0}.", parsed_str);
        Assert.AreEqual(expected.NormalizedStr, time_value.NormalizedStr, "NormalizedStr property did not match expected after parsing {0}.", parsed_str);
        Assert.AreEqual(expected.Ticks,         time_value.Timespan.Ticks, "Timespan.Ticks property did not match expected after parsing {0}.", parsed_str);
        }

    private static readonly Dictionary<string, VerificationResult> the_test_map = new()
        {
            // no preparsing
            {"1s",         new VerificationResult(        1,     "1s",             "1s", TimeSpan.TicksPerSecond)},
            {"325us",      new VerificationResult( 0.000325,  "0.000325s", "325\u03bcs", 3250)},
            // minutes
            {"2min",       new VerificationResult(  120,   "120s", "2:00.000000min", 1200000000)},
            {"1.5min",     new VerificationResult(   90,    "90s", "1:30.000000min",  900000000)},
            {"1:30min",    new VerificationResult(   90,    "90s", "1:30.000000min",  900000000)},
            {"1:30.25min", new VerificationResult(90.25, "90.25s", "1:30.250000min",  902500000)},
            // hours
            {"3h",         new VerificationResult(   10800,   "10800s", "3:00:00.000000h", 108000000000)},
            {"3.2h",       new VerificationResult(   11520,   "11520s", "3:12:00.000000h", 115200000000)},
            {"3:12h",      new VerificationResult(   11520,   "11520s", "3:12:00.000000h", 115200000000)},
            {"3:12.5h",    new VerificationResult(   11550,   "11550s", "3:12:30.000000h", 115500000000)},
            {"3:12:30h",   new VerificationResult(   11550,   "11550s", "3:12:30.000000h", 115500000000)},
            {"3:12:30.5h", new VerificationResult( 11550.5, "11550.5s", "3:12:30.500000h", 115505000000)},
            // days
            {"1d",            new VerificationResult(     86400,     "86400s", "1:00:00:00.000000d",  864000000000)},
            {"1.25d",         new VerificationResult(    108000,    "108000s", "1:06:00:00.000000d", 1080000000000)},
            {"1:06d",         new VerificationResult(    108000,    "108000s", "1:06:00:00.000000d", 1080000000000)},
            {"1:06.25d",      new VerificationResult(    108900,    "108900s", "1:06:15:00.000000d", 1089000000000)},
            {"1:06:15d",      new VerificationResult(    108900,    "108900s", "1:06:15:00.000000d", 1089000000000)},
            {"1:06:15.2d",    new VerificationResult(    108912,    "108912s", "1:06:15:12.000000d", 1089120000000)},
            {"1:06:15:12d",   new VerificationResult(    108912,    "108912s", "1:06:15:12.000000d", 1089120000000)},
            {"1:06:15:12.3d", new VerificationResult(  108912.3,  "108912.3s", "1:06:15:12.300000d", 1089123000000)},
        };


    /** @test Test to make sure that @ref REF_TimeValue properties are properly set after parsing.
    */
    [TestMethod]
    //[Ignore]
    public void parse_test()
        {
        foreach( var test_entry in the_test_map )
            {
            var time_value = new TimeValue();
            time_value.parse(test_entry.Key);
            verify_results(time_value, test_entry.Key, test_entry.Value);
            }
        }

    private static readonly List<string> the_error_format_list = new()
        {
        "1:60min",
        "1:60h",
        "1:59:60h",
        "1:25:60d",
        "1:25:59:60d",
        };

    /** @test Test to make sure that @ref REF_TimeValue throws FormatException when option_string cannot be parsed.
    */
    [TestMethod]
    //[Ignore]
    public void parse_exception_test()
        {
        foreach( var error_format in the_error_format_list )
            {
            try
                {
                var time_value = new TimeValue();
                time_value.parse(error_format);
                Assert.Fail("TimeValue did not throw expected FormatException for '{0}'.", error_format);
                }
            catch( FormatException )
                {
                }
            }
        }
    }
}
