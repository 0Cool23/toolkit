/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using libApplication.toolkit;

namespace RegressionTests.libApplication.toolkit
{
/** @ingroup REF_Testing REF_BinNumber

    @class   BinNumber_test

    @brief   Unit test for @ref REF_BinNumber class.
*/
[TestClass]
//[Ignore]
public class BinNumber_test
    {
    /** @test Test parsing of valid binary number strings to match the expected value. 
    */
    [TestMethod]
    //[Ignore]
    public void parse()
        {
        var test_values = new Dictionary<string, ulong>
            {
                {"0b1111_0101", 0xf5},
                {"0b1_0",          2},
                {"0b0",            0},
                {"0b1",            1},
                {"0B1_0",          2},
                {"0B0",            0},
                {"0B1",            1},
            };

        foreach( var bin_string in test_values.Keys )
            {
            ulong bin_value = BinNumber.parse(bin_string);
            Assert.AreEqual<ulong>(test_values[bin_string], bin_value, "BinNumber parse({0}) does not match expected result.", bin_string);
            }
        }

    /** @test Make sure that parser throws ArgumentNullException if passed binary number string is a null value.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException expected for null value.")]
    //[Ignore]
    public void null_exception()
        {
        ulong _ = BinNumber.parse(null);
        }

    /** @test Make sure that parser throws FormatException if passed binary number string is containing an
              invalid binary digit or does not start with 0b(0|1).
    */
    [TestMethod]
    //[Ignore]
    public void invalid_digits_exception()
        {
        var number_string_list = new List<string>
            {
            "0",
            "0b",
            "0b2",
            "0b1_2_0",
            "0b1_0_2",
            };
        foreach( var number_string in number_string_list )
            {
            try
                {
                ulong _ = BinNumber.parse(number_string);
                Assert.Fail("Must throw FormatException for invalid binary string '{0}'.", number_string);
                }
            catch( FormatException )
                {
                }
            catch( Exception )
                {
                Assert.Fail("Unexpected exception type caught expected FormatException for invalid binary string '{0}'.", number_string);
                }
            }
        }
    }
}
