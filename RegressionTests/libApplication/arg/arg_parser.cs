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
/** @ingroup REF_Testing REF_ArgParser

    @class   ArgParser_test

    @brief   Unit test for @ref REF_ArgParser class.
*/
[TestClass]
//[Ignore]
public class ArgParser_test
    {
    /** @test Test to make sure that ArgParser is properly constructed.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var _ = new ArgParser();
        }

    /** @test Test to ensure ArgumentNullException is thrown for process method call with null values.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException expected when calling process method with null values.")]
    //[Ignore]
    public void process_params_null_exception()
        {
        ArgParser arg_parser = new();
        arg_parser.process(null, null);
        }

    /** @test Test ArgParser initialization without parameters.
    */
    [TestMethod]
    //[Ignore]
    public void process_params_empty()
        {
        var arg_parser = new ArgParser
            {
            };
        var arg_table = new List<ArgEntry>
            {
            };
        string[] args_empty = Array.Empty<string>();

        arg_parser.process(args_empty, arg_table);

        Assert.AreEqual(null, arg_parser.get_next_argument(), "Stack must be empty and return null for empty args.");
        }

    /** @test Test ArgVector initialization without values.
    */
    [TestMethod]
    //[Ignore]
    public void arg_table_init()
        {
        var arg_parser = new ArgParser
            {
            };
        var parameter = new Argument();
        var arg_table = new List<ArgEntry>
            {
                {new ArgEntry('v', "verbose", ArgType.NO_ARGUMENT, false, parameter, null, "Increase verbosity level.")},
            };
        string[] args_empty = Array.Empty<string>();

        arg_parser.process(args_empty, arg_table);
        }

    /** @test Test ArgVector initialization with values.
    */
    [TestMethod]
    //[Ignore]
    public void init_arg_vector()
        {
        var arg_parser = new ArgParser
            {
            };
        var parameter = new Argument();
        var arg_table = new List<ArgEntry>
            {
                {new ArgEntry('v', "verbose", ArgType.NO_ARGUMENT, false, parameter, null, "Increase verbosity level.")},
            };
        string[] args =
            {
            "-v", "--verbose",
            };

        arg_parser.process(args, arg_table);
        }

    /** @test Test to ensure that ArgumentException is thrown if parameters do not start with '-' or '--'.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "ArgumentException expected if parameters do not start with '-' or '--'.")]
    //[Ignore]
    public void parse_arg_vector()
        {
        var arg_parser = new ArgParser
            {
            };
        var parameter = new Argument();
        var arg_table = new List<ArgEntry>
            {
                {new ArgEntry('v', "verbose", ArgType.NO_ARGUMENT, false, parameter, null, "Increase verbosity level.")},
            };
        string[] args =
            {
            "abc",
            };

        arg_parser.process(args, arg_table);
        }

    /** @test Test ArgStack initialization without values.
    */
    [TestMethod]
    //[Ignore]
    public void init_arg_stack_empty()
        {
        var arg_parser = new ArgParser
            {
            };
        var _ = new Argument();
        var arg_table = new List<ArgEntry>
            {
            };
        string[] args =
            {
            "--",
            };

        arg_parser.process(args, arg_table);

        Assert.AreEqual(null, arg_parser.get_next_argument(), "Stack must be empty and return null for empty args.");
        }

    /** @test Test ArgStack initialization with values.
    */
    [TestMethod]
    //[Ignore]
    public void init_arg_stack_data()
        {
        var arg_parser = new ArgParser
            {
            };
        var _ = new Argument();
        var arg_table = new List<ArgEntry>
            {
            };
        string[] args =
            {
            "--", "abc", "def",
            };

        arg_parser.process(args, arg_table);

        Assert.AreEqual("abc", arg_parser.get_next_argument(), "First stack element 'abc' did not match.");
        Assert.AreEqual("def", arg_parser.get_next_argument(), "Second stack element 'def' did not match.");
        Assert.AreEqual(null, arg_parser.get_next_argument(), "Stack must be empty after getting two elements.");
        }
    }
}
