/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using libApplication.app;
using libApplication.arg;
using libApplication.optarg;

namespace RegressionTests.libApplication.optarg
{
/** @ingroup REF_Testing REF_RealValue

    @class   RealValue_test

    @brief   Unit test for @ref REF_RealValue class.
*/
[TestClass]
//[Ignore]
public class RealValue_test
    {
    /** @test Test to make sure that RealValue defaults are properly set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var _ = new RealValue();
        }

    /** @test Test to make sure that RealValue is construction and parser accepts upper boundary.
    */
    [TestMethod]
    //[Ignore]
    public void construction_double_upper()
        {
        var arg_number = new RealValue(255.0);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  arg_number, "0.0", "xxx dummy.");

        arg_number.init(arg_entry);
        arg_number.parse("255.0");
        Assert.IsTrue(double.Equals(255.0, arg_number.Value), "RealValue did not match expected result.");
        }

    /** @test Test to make sure that RealValue throws exception when parser upper boundary is violated.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException), "ArgumentOutOfRangeException exception expected if lower boundary is violated.")]
    //[Ignore]
    public void exception_double_upper()
        {
        var arg_number = new RealValue(255.0);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  arg_number, "255.0", "xxx dummy.");

        arg_number.init(arg_entry);
        arg_number.parse("256.0");
        }

    /** @test Test to make sure that RealValue throws exception when parser upper boundary is violated.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException), "ArgumentOutOfRangeException exception expected if lower boundary is violated.")]
    //[Ignore]
    public void exception_range_upper()
        {
        var arg_number = new RealValue(double.NaN, 255.0);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  arg_number, "255.0", "xxx dummy.");

        arg_number.init(arg_entry);
        arg_number.parse("256.0");
        }

    /** @test Test to make sure that RealValue is construction and parser accepts range.
    */
    [TestMethod]
    //[Ignore]
    public void construction_range()
        {
        var arg_number = new RealValue(1.0, 255.0);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  arg_number, "255.0", "xxx dummy.");

        arg_number.init(arg_entry);
        arg_number.parse("1.0");
        Assert.IsTrue(double.Equals(1.0, arg_number.Value), "RealValue (lower) did not match expected result.");

        arg_number.parse("255.0");
        Assert.IsTrue(double.Equals(255.0, arg_number.Value), "RealValue (upper) did not match expected result.");
        }

    /** @test Test to make sure that RealValue is construction and parser accepts lower boundary.
    */
    [TestMethod]
    //[Ignore]
    public void construction_double_lower()
        {
        var arg_number = new RealValue(255.0, double.NaN);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  arg_number, "256.0", "xxx dummy.");

        arg_number.init(arg_entry);
        arg_number.parse("255.0");
        Assert.IsTrue(double.Equals(255.0, arg_number.Value), "RealValue did not match expected result.");
        }

    /** @test Test to make sure that RealValue throws exception when parser lower boundary is violated.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException), "ArgumentOutOfRangeException exception expected if lower boundary is violated.")]
    //[Ignore]
    public void exception_double_lower()
        {
        var arg_number = new RealValue(255.0, double.NaN);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  arg_number, "255.0", "xxx dummy.");

        arg_number.init(arg_entry);
        arg_number.parse("254.0");
        }
    }
}
