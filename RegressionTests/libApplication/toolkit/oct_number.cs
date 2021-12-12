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
/** @ingroup REF_Testing REF_OctNumber

    @class OctNumber_test

    @brief Unit test for @ref REF_OctNumber class.
*/
[TestClass]
//[Ignore]
public class OctNumber_test
    {
    /** @test Test parsing of valid octal number strings to match the expected value. 
    */
    [TestMethod]
    //[Ignore]
    public void parse_test()
        {
        var test_values = new Dictionary<string, ulong>
            {
                {"0", 0},
                {"01", 1},
                {"00", 0},
                {"07", 7},
                {"010", 8},
            };

        foreach( var oct_string in test_values.Keys )
            {
            ulong oct_value = OctNumber.parse(oct_string);
            Assert.AreEqual<ulong>(test_values[oct_string], oct_value, "OctNumber parse({0}) does not match expected result.", oct_string);
            }
        }

    /** @test Make sure that parser throws ArgumentNullException if passed octal number string is a null value.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException expected for null value.")]
    //[Ignore]
    public void null_exception()
        {
        ulong _ = OctNumber.parse(null);
        }

    /** @test Make sure that parser throws FormatException if passed octal number string is containing an
              invalid octal digit.
    */
    [TestMethod]
    //[Ignore]
    public void invalid_digits_exception()
        {
        var number_string_list = new List<string>
            {
            "1",
            "08",
            "07_8_6",
            "087",
            "078",
            };
        foreach( var number_string in number_string_list )
            {
            try
                {
                ulong _ = OctNumber.parse(number_string);
                Assert.Fail("Must throw FormatException for invalid octal string '{0}'.", number_string);
                }
            catch( FormatException )
                {
                }
            catch( Exception )
                {
                Assert.Fail("Unexpected exception type caught expected FormatException for invalid octal string '{0}'.", number_string);
                }
            }
        }
    }
}
