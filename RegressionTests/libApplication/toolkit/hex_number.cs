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
/** @ingroup REF_Testing REF_HexNumber
 
    @class   HexNumber_test

    @brief   Unit test for @ref REF_HexNumber class.    
*/
[TestClass]
//[Ignore]
public class HexNumber_test
    {
    /** @test Test parsing of valid hexadecimal number strings to match the expected value. 
    */
    [TestMethod]
    //[Ignore]
    public void parse()
        {
        var test_values = new Dictionary<string, ulong>
            {
                {"0xabcd_ef10", 0xabcdef10},
                {"0xdeadbeef",  0xdeadbeef},
                {"0xf_0",             0xf0},
                {"0x0",                  0},
                {"0x1",                  1},
                {"0X1_e",             0x1e},
                {"0X0",                  0},
                {"0X1",                  1},
                {"0Xf",                 15},
            };

        foreach( var hex_string in test_values.Keys )
            {
            ulong hex_value = HexNumber.parse(hex_string);
            Assert.AreEqual<ulong>(test_values[hex_string], hex_value, "HexNumber parse({0}) does not match expected result.", hex_string);
            }
        }

    /** @test Make sure that parser throws ArgumentNullException if passed hexadecimal number string is a null value.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException expected for null value.")]
    //[Ignore]
    public void null_exception()
        {
        ulong _ = HexNumber.parse(null);
        }

    /** @test Make sure that parser throws FormatException if passed hexadecimal number string is containing an
              invalid hexadecimal digit or does not start with 0b(0|1).
    */
    [TestMethod]
    //[Ignore]
    public void invalid_digits_exception()
        {
        var number_string_list = new List<string>
            {
            "0",
            "0x",
            "0xg",
            "0xgb",
            "0xa_g_d",
            "0xgb",
            "0xch",
            };
        foreach( var number_string in number_string_list )
            {
            try
                {
                ulong _ = HexNumber.parse(number_string);
                Assert.Fail("Must throw FormatException for invalid hexadecimal string '{0}'.", number_string);
                }
            catch( FormatException )
                {
                }
            catch( Exception )
                {
                Assert.Fail("Unexpected exception type caught expected FormatException for invalid hexadecimal string '{0}'.", number_string);
                }
            }
        }
    }
}
