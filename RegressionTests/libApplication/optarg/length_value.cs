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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using libApplication.optarg;

namespace RegressionTests.libApplication.optarg
{
/** @ingroup REF_Testing REF_LengthValue
  
    @class   LengthValue_test

    @brief   Unit test for @ref REF_LengthValue class.  
*/
[TestClass]
//[Ignore]
public class LengthValue_test
    {
    /** @test Test default construction and property values.
    */
    [TestMethod]
    public void contructor()
        {
        var length_value = new LengthValue();
        Assert.AreEqual(0.0, length_value.Value, "Value property is expected to be 0 after construction.");
        Assert.AreEqual("0m", length_value.ValueStr, "ValueStr property is expected to be 0 after construction.");
        Assert.AreEqual("0m", length_value.NormalizedStr, "NormalizedStr property is expected to be 0 after construction.");
        }

    private class TestEntry
        {
        public readonly string ArgumentStr;
        public readonly double Value;
        public readonly string ValueStr;
        public readonly string NormalizedStr;

        public TestEntry( string argument_str, double value, string value_str, string normalized_str )
            {
            ArgumentStr   = argument_str;
            Value         = value;
            ValueStr      = value_str;
            NormalizedStr = normalized_str;
            }
        }

    private static readonly string PI_STRING = Math.PI.ToString("r", CultureInfo.InvariantCulture) + "m";
    private static readonly List<TestEntry> the_test_list = new()
        {
            new TestEntry("0m",            0,      "0m",   "0m"),
            new TestEntry("1m",            1,      "1m",   "1m"),
            new TestEntry("2km",        2000,   "2000m",  "2km"),
            new TestEntry("2000m",      2000,   "2000m",  "2km"),
            new TestEntry("-4um",  -0.000004, "-4e-06m", "-4\u03bcm"),
            // 
            new TestEntry(PI_STRING, Math.PI, PI_STRING, PI_STRING),
        };

    /** @test Test usage.
    */
    [TestMethod]
    public void usage_passed()
        {
        foreach( var test_entry in the_test_list )
            {
            var length = new LengthValue();
            length.parse(test_entry.ArgumentStr);
            Assert.AreEqual(test_entry.Value,         length.Value,         "Value property did not match for argument '{0}'.", test_entry.ArgumentStr);
            Assert.AreEqual(test_entry.ValueStr,      length.ValueStr,      "ValueStr property did not match for argument '{0}'.", test_entry.ArgumentStr);
            Assert.AreEqual(test_entry.NormalizedStr, length.NormalizedStr, "NormalizedStr property did not match for argument '{0}'.", test_entry.ArgumentStr);
            };
        }
    }
}
