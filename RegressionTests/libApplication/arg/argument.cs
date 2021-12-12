/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using libApplication.arg;

namespace RegressionTests.libApplication.App
{
/** @ingroup REF_Testing REF_Argument

    @class   Argument_test

    @brief   Unit test for @ref REF_Argument class.
*/
[TestClass]
//[Ignore]
public class Argument_test
    {
    /** @test Test to make sure that Argument is properly constructed for some valid cases.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        Argument parameter = new();

        Assert.AreEqual(0, parameter.Count, "count must be 0 after construction.");
        Assert.IsNull(parameter.OptionStr, "");
        Assert.IsNull(parameter.Name, "");
        }

    /** @test Test to make sure that Argument.init will throw an exception if @p arg_entry is null.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "init must throw exception when arg_entry is null.")]
    //[Ignore]
    public void init_arg_entry_exception()
        {
        Argument parameter = new();
        parameter.init(null as ArgEntry);
        }

    /** @test Test to make sure that Argument.init will throw an exception if @p initial_string is null.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "init must throw exception when initial_string is null.")]
    //[Ignore]
    public void init_string_exception()
        {
        Argument parameter = new();
        parameter.init(null as string);
        }

    /** @test Test to make sure that Argument.init (NO_ARGUMENT) will work properly.
    */
    [TestMethod]
    //[Ignore]
    public void init_arg_entry()
        {
        Argument parameter = new();
        ArgEntry arg_entry = new('h', "help", ArgType.NO_ARGUMENT, false, parameter, null, "help message.");
        parameter.init(arg_entry);
        Assert.AreEqual(0, parameter.Count, "count must be 0 after init() call.");
        Assert.AreEqual(arg_entry.name, parameter.Name, "unexpected parameter name.");
        }

    /** @test Test to make sure that Argument.init (string) will work properly.
    */
    [TestMethod]
    //[Ignore]
    public void init_stringy()
        {
        Argument parameter = new();
        parameter.init("dummy value");
        Assert.AreEqual(0, parameter.Count, "count must be 0 after init() call.");
        Assert.AreEqual("dummy value", parameter.OptionStr, "unexpected parameter value.");
        }

    /** @test Test to make sure that Argument.parse (NO_ARGUMENT) will throw an exception if argument is passed.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "parse must throw exception if option string is passed.")]
    //[Ignore]
    public void no_argument_string_exception()
        {
        Argument parameter = new();
        ArgEntry arg_entry = new('h', "help", ArgType.NO_ARGUMENT, false, parameter, null, "help message.");
        parameter.init(arg_entry);
        parameter.parse("abcde");
        }

    /** @test Test to make sure that Argument.parse (NO_ARGUMENT) will work properly.
    */
    [TestMethod]
    //[Ignore]
    public void no_argument()
        {
        Argument parameter = new();
        ArgEntry arg_entry = new('h', "help", ArgType.NO_ARGUMENT, false, parameter, null, "help message.");

        parameter.init(arg_entry);

        parameter.parse(null);
        Assert.AreEqual(1, parameter.Count, "count must be one after parsing null (NO_ARGUMENT).");

        parameter.parse(string.Empty);
        Assert.AreEqual(2, parameter.Count, "count must be two after parsing empty string (NO_ARGUMENT).");
        }

    /** @test Test to make sure that Argument.parse (REQUIRED_ARGUMENT) will throw an exception if null argument is passed.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "parse must throw exception if null option string is passed.")]
    //[Ignore]
    public void required_argument_null_exception()
        {
        Argument parameter = new();
        ArgEntry arg_entry = new('v', "verbose", ArgType.REQUIRED_ARGUMENT, false, parameter, string.Empty, "increase verbosity.");
        parameter.init(arg_entry);
        parameter.parse(null);
        }

    /** @test Test to make sure that Argument.parse (REQUIRED_ARGUMENT) will work properly.
    */
    [TestMethod]
    //[Ignore]
    public void required_argument()
        {
        Argument parameter = new();
        ArgEntry arg_entry = new('v', "verbose", ArgType.REQUIRED_ARGUMENT, false, parameter, string.Empty, "increase verbosity.");

        parameter.init(arg_entry);
        parameter.parse(string.Empty);
        Assert.AreEqual(1, parameter.Count);
        parameter.parse("def");
        Assert.AreEqual(2, parameter.Count);
        }

    /** @test Test to make sure that Argument.parse (OPT_ARGUMENT) will work properly.
    */
    [TestMethod]
    //[Ignore]
    public void parse_optional_argument()
        {
        Argument parameter = new();
        ArgEntry arg_entry = new('o', "optional", ArgType.OPT_ARGUMENT, false, parameter, null, "increase verbosity.");

        parameter.init(arg_entry);
        parameter.parse(null);
        Assert.AreEqual(1, parameter.Count);
        parameter.parse(string.Empty);
        Assert.AreEqual(2, parameter.Count);
        parameter.parse("def");
        Assert.AreEqual(3, parameter.Count);
        }
    }
}
