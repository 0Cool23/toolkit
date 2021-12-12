/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using libApplication.arg;

namespace RegressionTests.libApplication.arg
{
/** @ingroup REF_Testing REF_ArgVector

    @class ArgVector_test

    @brief Unit test for @ref REF_ArgVector class.
*/
[TestClass]
//[Ignore]
public class ArgVector_test
    {
    /** @test Test ArgVector class construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var _ = new ArgVector();
        }

    /** @test Test ArgVector properties for standard usage.
    */
    [TestMethod]
    //[Ignore]
    public void usage()
        {
        var vector = new ArgVector();

        Assert.AreEqual(0, vector.str_pos, "str_pos is expected to be 0 after construction.");
        Assert.AreEqual(true, vector.end_of_list, "end_of_list is expected to be true after construction.");
        Assert.AreEqual(false, vector.has_successor, "has_successor is expected to be false after construction.");

        vector.add("12345");
        Assert.AreEqual(false, vector.has_successor, "has_successor is expected to be false after adding one element.");
        Assert.AreEqual("12345", vector.current_arg, "current arg does not match expected result.");

        vector.str_pos = 2;
        vector.add("abcde");
        Assert.AreEqual(true, vector.has_successor, "has_successor is expected to be true after adding another element.");
        Assert.AreEqual("345", vector.current_arg[vector.str_pos..], "current arg substring does not match expected result.");
        Assert.AreEqual("abcde", vector.successor_arg, "current arg does not match expected result.");

        vector.inc_index();
        Assert.AreEqual(false, vector.has_successor, "has_successor is expected to be true after index incrementation.");
        Assert.AreEqual("abcde", vector.current_arg, "current arg does not match expected result after incrementation.");
        }

    /** @test Test ArgumentOutOfRangeException is thrown if accessing end_of_string on empty ArgVector.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException), "ArgumentOutOfRangeException should be thrown when accessing end_of_string on empty ArgVector.")]
    //[Ignore]
    public void end_of_string_exception()
        {
        var vector = new ArgVector();

        var _ = vector.end_of_string;
        }

    /** @test Test ArgumentOutOfRangeException is thrown if accessing current_arg on empty ArgVector.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException), "ArgumentOutOfRangeException should be thrown when accessing current_arg on empty ArgVector.")]
    //[Ignore]
    public void current_arg_exception()
        {
        var vector = new ArgVector();

        var _ = vector.current_arg;
        }

    /** @test Test ArgumentOutOfRangeException is thrown if accessing successor_arg on empty ArgVector.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException), "ArgumentOutOfRangeException should be thrown when accessing successor_arg on empty ArgVector.")]
    //[Ignore]
    public void successor_arg_exception()
        {
        var vector = new ArgVector();

        var _ = vector.successor_arg;
        }
    }
}
