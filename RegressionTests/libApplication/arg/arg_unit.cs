/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using libApplication.arg;

namespace RegressionTests.libApplication.arg
{
/** @ingroup REF_Testing REF_ArgUnit
  
    @class   ArgUnit_test

    @brief   Unit test for @ref REF_ArgUnit class.  
*/
[TestClass]
//[Ignore]
public class ArgUnit_test
    {
    public enum eControlValue
        :   uint
        {
        PREFIX_NONE,
        PREFIX_m,
        PREFIX_M,
        PREFIX_XYZ,
        };

    private struct TestEntry
        {
        public readonly string        OptionStr;
        public readonly eControlValue ControlValue;
        public readonly string        ValueStr;

        public TestEntry( string options_str, eControlValue control_value, string value_str )
            {
            OptionStr    = options_str;
            ControlValue = control_value;
            ValueStr     = value_str;
            }
        }

    private class ArgUnitTest
        :   ArgUnit
        {
        public ArgUnitTest( string symbol )
            :   base( symbol )
            {
            }

        public class MagnitudeEntry
            :   iMagnitudeEntry
            {
            public bool isNormalized
                {
                get;
                private set;
                }
            public eControlValue ControlValue;

            public MagnitudeEntry( bool is_normalized, eControlValue control_value )
                {
                isNormalized = is_normalized;
                ControlValue = control_value;
                }
            };

        private static readonly Dictionary<string, iMagnitudeEntry> the_magnitude_map = new()
            {
                {"m",   new MagnitudeEntry(true, eControlValue.PREFIX_m)},
                {"M",   new MagnitudeEntry(true, eControlValue.PREFIX_M)},
                {"Xyz", new MagnitudeEntry(true, eControlValue.PREFIX_XYZ)},
            };

        public readonly Argument TestArg = new();

        private void parse_test( string option_str, eControlValue control_value, string value_str )
            {
            parse(TestArg, option_str, the_magnitude_map, out string magnitude);
            var result_value = (string.IsNullOrEmpty(magnitude)) ? eControlValue.PREFIX_NONE: (the_magnitude_map[magnitude] as MagnitudeEntry).ControlValue;
            Assert.AreEqual(value_str, TestArg.OptionStr, "OptionStr did not match expected value '{0}'", value_str);
            Assert.AreEqual(control_value, result_value, "ControlValue did not match");
            }

        public void parse_test( TestEntry test_values )
            {
            parse_test(test_values.OptionStr, test_values.ControlValue, test_values.ValueStr);
            }
        }

    /** @test Test that ArgUnit throws an ArgumentNullException when symbol is an empty string value.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException expected when symbol is an empty string.")]
    //[Ignore]
    public void construction_empty()
        {
        var _ = new ArgUnitTest(string.Empty);
        }

    /** @test Test that ArgUnit throws an ArgumentNullException when symbol is a null string value.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException expected when symbol is a null value.")]
    //[Ignore]
    public void construction_null()
        {
        var _ = new ArgUnitTest(null);
        }

    /** @test Test ArgUnit construction and defaults.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var arg_unit = new ArgUnitTest("Xyz");
        Assert.AreEqual("Xyz", arg_unit.Symbol, "Symbol property has unexpected value after construction.");
        }

    /** @test Test ArgUnit throws ArgumentNullException exception when the option string is null value.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException expected when symbol is a null value.")]
    //[Ignore]
    public void parse_null()
        {
        var arg_unit = new ArgUnitTest("Xyz");
        arg_unit.parse_test(new TestEntry(null, eControlValue.PREFIX_m, null));
        }

    /** @test Test ArgUnit throws ArgumentNullException exception when the option string is an empty string value.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException expected when symbol is an empty string value.")]
    //[Ignore]
    public void parse_empty()
        {
        var arg_unit = new ArgUnitTest("Xyz");
        arg_unit.parse_test(new TestEntry(string.Empty, eControlValue.PREFIX_m, null));
        }

    /** @test Test ArgUnit throws ArgumentException exception when the option string does not match the expected symbol.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "ArgumentNullException expected when symbol is an empty string value.")]
    //[Ignore]
    public void parse_mismatch()
        {
        var arg_unit = new ArgUnitTest("Xyz");
        arg_unit.parse_test(new TestEntry("yz", eControlValue.PREFIX_m, null));
        }

    private static readonly List<TestEntry> the_test_list = new()
        {
        new TestEntry("Xyz",    eControlValue.PREFIX_NONE, string.Empty),
        new TestEntry("mXyz",   eControlValue.PREFIX_m,    string.Empty),
        new TestEntry("mMXyz",  eControlValue.PREFIX_M,   "m"),
        new TestEntry("m MXyz", eControlValue.PREFIX_M,   "m"),
        };

    /** @test Test ArgUnit properties are set and parsed as expected.
    */
    [TestMethod]
    //[Ignore]
    public void parse_no_prefix()
        {

        var arg_unit = new ArgUnitTest("Xyz");
        foreach( var test_entry in the_test_list )
            {
            arg_unit.parse_test(test_entry);
            }
        }
    }
}
