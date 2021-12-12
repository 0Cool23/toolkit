/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2021

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using libApplication.toolkit;

namespace RegressionTests.libApplication.toolkit
{
/** @ingroup REF_Testing REF_RealNumber

    @class   RealNumber_test

    @brief   Unit test for @ref REF_RealNumber class.
*/
[TestClass]
//[Ignore]
public class RealNumber_test
    {
    /** @test Test parsing of valid real number strings to match the expected value.
    */
    [TestMethod]
    //[Ignore]
    public void parse()
        {
        var test_values = new Dictionary<string, double>
            {
                {"0",                          0},
                {"1",                          1},
                {"-0",                         0},
                {"+0",                         0},
                {"99",                        99},
                {"-1",                        -1},
                {"2.7182818284590451",       Math.E},
                {"-2.7182818284590451",      -Math.E},
                {"3.1415926535897931",       Math.PI},
                {"-3.1415926535897931",      -Math.PI},
                {"nan",                      double.NaN},
                {"inf",                      double.PositiveInfinity},
                {"-inf",                     double.NegativeInfinity},
                {"+eps",                     double.Epsilon},
                {"-eps",                     -double.Epsilon},
                {"4.94065645841247E-324",    double.Epsilon},
                {"-1.7976931348623157E+308", double.MinValue},
                {"1.7976931348623157E+308",  double.MaxValue},
                // test regex patterns
                {"0.",                       0},
                {"0.e-0",                    0},
                {".0",                       0},
                {".0e+0",                    0},
                {"0.00",                     0},
                {"0.0e0",                    0},
            };

        foreach( var dec_string in test_values.Keys )
            {
            double real_value = RealNumber.parse(dec_string);
            Assert.IsTrue(double.Equals(test_values[dec_string], real_value), "RealNumber parse({0}) does not match expected result.", dec_string);
            }
        }

    /** @test Make sure that parser throws ArgumentNullException if passed decimal number string is a null value.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException expected for null value.")]
    //[Ignore]
    public void null_exception()
        {
        double _ = RealNumber.parse(null);
        }

    /** @test Make sure that parser throws FormatException if passed real number string is containing an
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
            "00",
            "01",
            "+",
            "-",
            };
        foreach( var number_string in number_string_list )
            {
            try
                {
                double _ = RealNumber.parse(number_string);
                Assert.Fail("Must throw FormatException for invalid real number string '{0}'.", number_string);
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
