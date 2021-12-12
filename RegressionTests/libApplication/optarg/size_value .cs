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
/** @ingroup REF_Testing REF_SizeValue
  
    @class   SizeValue_test

    @brief   Unit test for @ref REF_SizeValue class.  
*/
[TestClass]
//[Ignore]
public class SizeValue_test
    {
    /** @test Test default construction and property values.
    */
    [TestMethod]
    public void contructor()
        {
        var size_value = new SizeValue();
        Assert.AreEqual(0ul, size_value.Value, "Value property is expected to be 0 after construction.");
        Assert.AreEqual("0B", size_value.ValueStr, "ValueStr property is expected to be 0B after construction.");
        Assert.AreEqual("0B", size_value.NormalizedStr, "NormalizedStr property is expected to be 0B after construction.");
        }

    private class TestEntry
        {
        public readonly string ArgumentStr;
        public readonly ulong  Value;
        public readonly string ValueStr;
        public readonly string NormalizedStr;

        public TestEntry( string argument_str, ulong value, string value_str, string normalized_str )
            {
            ArgumentStr   = argument_str;
            Value         = value;
            ValueStr      = value_str;
            NormalizedStr = normalized_str;
            }
        }

    private static readonly List<TestEntry> the_test_list = new()
        {
            new TestEntry("0B",          0,       "0B",   "0B"),
            new TestEntry("1B",          1,       "1B",   "1B"),
            new TestEntry("2kB",      2000,    "2000B",  "2kB"),
            new TestEntry("2000B",    2000,    "2000B",  "2kB"),
            new TestEntry("4MB",   4000000, "4000000B",  "4MB"),
            new TestEntry("1MiB",  1048576, "1048576B", "1MiB"),
        };

    /** @test Test usage.
    */
    [TestMethod]
    public void usage_passed()
        {
        foreach( var test_entry in the_test_list )
            {
            var size = new SizeValue();
            size.parse(test_entry.ArgumentStr);
            Assert.AreEqual(test_entry.Value,         size.Value,         "Value property did not match for argument '{0}'.", test_entry.ArgumentStr);
            Assert.AreEqual(test_entry.ValueStr,      size.ValueStr,      "ValueStr property did not match for argument '{0}'.", test_entry.ArgumentStr);
            Assert.AreEqual(test_entry.NormalizedStr, size.NormalizedStr, "NormalizedStr property did not match for argument '{0}'.", test_entry.ArgumentStr);
            };
        }
    }
}
