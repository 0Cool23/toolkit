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
/** @ingroup REF_Testing REF_DecNumber

    @class   DecNumber_test

    @brief   Unit test for @ref REF_DecNumber class.
*/
[TestClass]
//[Ignore]
public class DecNumber_test
    {
    /** @test Test parsing of valid decimal number strings to match the expected value.
    */
    [TestMethod]
    //[Ignore]
    public void parse()
        {
        var test_values = new Dictionary<string, long>
            {
                {"0", 0},
                {"1", 1},
                {"-0", 0},
                {"+0", 0},
                {"99", 99},
                {"18_446_744_073_709_551_615", -1},
                {"-9_223_372_036_854_775_808", long.MinValue},
                {"9_223_372_036_854_775_807", long.MaxValue},
            };

        foreach( var dec_string in test_values.Keys )
            {
            long dec_value = DecNumber.parse(dec_string);
            Assert.AreEqual<long>(test_values[dec_string], dec_value, "DecNumber parse({0}) does not match expected result.", dec_string);
            }
        }

    /** @test Make sure that parser throws ArgumentNullException if passed decimal number string is a null value.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException expected for null value.")]
    //[Ignore]
    public void null_exception()
        {
        long _ = DecNumber.parse(null);
        }

    /** @test Make sure that parser throws FormatException if passed decimal number string is containing an
              invalid octal digit.
    */
    [TestMethod]
    //[Ignore]
    public void invalid_digits_exception()
        {
        var number_string_list = new List<string>
            {
            "a",
            "0a",
            "7_t_6",
            "1r7",
            "27s",
            "01",
            "+",
            "-",
            };
        foreach( var number_string in number_string_list )
            {
            try
                {
                long _ = DecNumber.parse(number_string);
                Assert.Fail("Must throw FormatException for invalid decimal string '{0}'.", number_string);
                }
            catch( FormatException )
                {
                }
            catch( Exception )
                {
                Assert.Fail("Unexpected exception type caught expected FormatException for invalid decimal string '{0}'.", number_string);
                }
            }
        }
    }
}
