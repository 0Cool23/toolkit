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
/** @ingroup REF_Testing REF_IntValue

    @class   IntValue_test

    @brief   Unit test for @ref REF_IntValue class.
*/
[TestClass]
//[Ignore]
public class IntValue_test
    {
    /** @test Test to make sure that IntValue defaults are properly set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var _ = new IntValue();
        }

    /** @test Test to make sure that IntValue is construction and parser accepts range.
    */
    [TestMethod]
    //[Ignore]
    public void construction_long()
        {
        var arg_number = new IntValue((long)255);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  arg_number, "0b0", "xxx dummy.");

        arg_number.init(arg_entry);
        arg_number.parse("0x80");
        Assert.AreEqual<ulong>(0x80, arg_number.Unsigned, "IntValue did not match expected result.");
        }

    /** @test Test to make sure that IntValue is construction and parser out of range exception is thrown.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException), "ArgumentOutOfRangeException expected for invalid integer value.")]
    //[Ignore]
    public void construction_long_exception()
        {
        var arg_number = new IntValue((long)255);
        var arg_entry = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  arg_number, "0b0", "xxx dummy.");

        arg_number.init(arg_entry);
        arg_number.parse("0x100");
        Assert.AreEqual<ulong>(0x80, arg_number.Unsigned, "IntValue did not match expected result.");
        }

    /** @test Test to make sure that IntValue is construction and parser accepts range.
    */
    [TestMethod]
    //[Ignore]
    public void construction_long_range()
        {
        var _ = new IntValue((long)127, (long)-1);
        }

    /** @test Test to make sure that IntValue is construction and parser accepts range.
    */
    [TestMethod]
    //[Ignore]
    public void construction_ulong()
        {
        var _ = new IntValue((ulong)255);
        }

    /** @test Test to make sure that IntValue is construction and parser accepts range.
    */
    [TestMethod]
    //[Ignore]
    public void construction_ulong_range()
        {
        var _ = new IntValue((ulong)511, (ulong)255);
        }

    /** @test Test
    */
    [TestMethod]
    //[Ignore]
    public void binary_number()
        {
        var arg_number = new IntValue();
        var arg_entry = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  arg_number, "0b0", "xxx dummy.");

        arg_number.init(arg_entry);
        arg_number.parse("0b11111111_11111111_11111111_11111111_11111111_11111111_11111111_11111111");
        Assert.AreEqual<ulong>(0xffffffffffffffff, arg_number.Unsigned, "Unsigned value did not match.");
        Assert.AreEqual<long>(-1, arg_number.Signed, "Signed value did not match.");
        }

    /** @test Test
    */
    [TestMethod]
    //[Ignore]
    public void hexadecimal_number()
        {
        var arg_number = new IntValue();
        var arg_entry = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  arg_number, "0x0", "xxx dummy.");

        arg_number.init(arg_entry);
        arg_number.parse("0xffffffffffffffff");
        Assert.AreEqual<ulong>(0xffffffffffffffff, arg_number.Unsigned, "Unsigned value did not match.");
        Assert.AreEqual<long>(-1, arg_number.Signed, "Signed value did not match.");
        }

    /** @test Test
    */
    [TestMethod]
    //[Ignore]
    public void octal_number()
        {
        var arg_number = new IntValue();
        var arg_entry = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  arg_number, "0", "xxx dummy.");

        arg_number.init(arg_entry);
        arg_number.parse("01777777777777777777777");
        Assert.AreEqual<ulong>(0xffffffffffffffff, arg_number.Unsigned, "Unsigned value did not match.");
        Assert.AreEqual<long>(-1, arg_number.Signed, "Signed value did not match.");
        }

    /** @test Test
     */
    [TestMethod]
    //[Ignore]
    public void decimal_number()
        {
        var arg_number = new IntValue();
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  arg_number, "0", "xxx dummy.");

        arg_number.init(arg_entry);
        arg_number.parse("18446744073709551615");
        Assert.AreEqual<ulong>(0xffffffffffffffff, arg_number.Unsigned, "Unsigned value did not match.");
        Assert.AreEqual<long>(-1, arg_number.Signed, "Signed value did not match.");
        }

    /** @test Test
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "ArgumentException expected for invalid number value.")]
    //[Ignore]
    public void invalid_number_exception()
        {
        var arg_number = new IntValue();
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  arg_number, "0", "xxx dummy.");

        arg_number.init(arg_entry);
        arg_number.parse("a");
        }
    }
}
