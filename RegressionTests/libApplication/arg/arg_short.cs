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
/** @ingroup REF_Testing REF_ArgShort

    @class   ArgShort_test

    @brief   Unit test for @ref REF_ArgShort class.
*/
[TestClass]
//[Ignore]
public class ArgShort_test
    {
    /** @test Test ArgShort object construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var arg_table = new List<ArgEntry>()
            {
            };
        var _ = new ArgShort(arg_table);
        }

    /** @test Test valid short options are detected.
    */
    [TestMethod]
    //[Ignore]
    public void is_short_option_true()
        {
        var argv = new ArgVector();
        argv.add("-v");

        var result = ArgShort.is_short_option(argv);
        Assert.AreEqual(true, result, "is_short_option for '-v' must return true.");
        Assert.AreEqual(0, argv.str_pos, "str_pos must be 0 after short arg has been detected.");
        }

    /** @test Test invalid short options are detected.
   */
    [TestMethod]
    //[Ignore]
    public void is_short_option_false()
        {
        var argv = new ArgVector();
        argv.add("-");
        argv.add("x");
        argv.add("-=");

        var result = ArgShort.is_short_option(argv);
        Assert.AreEqual(false, result, "is_short_option for '-' must return false.");
        Assert.AreEqual(0, argv.str_pos, "str_pos must be 0 if short arg was not detected.");

        argv.inc_index();
        result = ArgShort.is_short_option(argv);
        Assert.AreEqual(false, result, "is_short_option for 'x' must return false.");
        Assert.AreEqual(0, argv.str_pos, "str_pos must be 0 if short arg was not detected.");

        argv.inc_index();
        result = ArgShort.is_short_option(argv);
        Assert.AreEqual(false, result, "is_short_option for '-=' must return false.");
        Assert.AreEqual(0, argv.str_pos, "str_pos must be 0 if short arg was not detected.");
        }

    /** @test Test to ensure optional argument counter works as intended.
    */
    [TestMethod]
    //[Ignore]
    public void no_argument()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('v', null, ArgType.NO_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("-v");
        argv.add("-v");
        argv.add("-vv");
        argument.init(arg_table[0]);

        var arg_short = new ArgShort(arg_table);

        argv.str_pos = 1;
        arg_short.parse(argv);

        Assert.AreEqual(1, argument.Count);
        Assert.AreEqual(null, argument.OptionStr);

        argv.str_pos = 1;
        arg_short.parse(argv);

        Assert.AreEqual(2, argument.Count);
        Assert.AreEqual(null, argument.OptionStr);

        argv.str_pos = 1;
        arg_short.parse(argv);
        Assert.AreEqual(4, argument.Count);
        Assert.AreEqual(null, argument.OptionStr);
        }

    /** @test Test to ensure optional argument counter and value assignment work as intended.
    */
    [TestMethod]
    //[Ignore]
    public void opt_argument_no_arg()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('v', null, ArgType.OPT_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("-v");
        argument.init(arg_table[0]);

        var arg_short = new ArgShort(arg_table);

        argv.str_pos = 1;
        arg_short.parse(argv);

        Assert.AreEqual(1, argument.Count);
        Assert.AreEqual(null, argument.OptionStr);
        }

    /** @test Test to ensure optional argument values are assigned as intended.
    */
    [TestMethod]
    //[Ignore]
    public void opt_argument_one_value()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('v', null, ArgType.OPT_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("-v");
        argv.add("12345");
        argument.init(arg_table[0]);

        var arg_short = new ArgShort(arg_table);

        argv.str_pos = 1;
        arg_short.parse(argv);

        Assert.AreEqual(1, argument.Count);
        Assert.AreEqual("12345", argument.OptionStr);
        }

    /** @test Test to ensure optional argument counter works as intended.
    */
    [TestMethod]
    //[Ignore]
    public void opt_argument_two_values()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('v', null, ArgType.OPT_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("-v");
        argv.add("-v");
        argument.init(arg_table[0]);

        var arg_short = new ArgShort(arg_table);

        argv.str_pos = 1;
        arg_short.parse(argv);

        Assert.AreEqual(1, argument.Count);
        Assert.AreEqual(null, argument.OptionStr);

        argv.str_pos = 1;
        arg_short.parse(argv);
        Assert.AreEqual(2, argument.Count);
        Assert.AreEqual(null, argument.OptionStr);
        }

    /** @test Test to ensure optional argument values are assigned and overwritten.
    */
    [TestMethod]
    //[Ignore]
    public void opt_argument_assigned_value()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('v', null, ArgType.OPT_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("-v=12345");
        argv.add("-v=");
        argv.add("-vx");
        argv.add("-vvy123");
        argument.init(arg_table[0]);

        var arg_short = new ArgShort(arg_table);

        argv.str_pos = 1;
        arg_short.parse(argv);

        Assert.AreEqual(1, argument.Count);
        Assert.AreEqual("12345", argument.OptionStr);

        argv.str_pos = 1;
        arg_short.parse(argv);

        Assert.AreEqual(2, argument.Count);
        Assert.AreEqual(string.Empty, argument.OptionStr);

        argv.str_pos = 1;
        arg_short.parse(argv);

        Assert.AreEqual(3, argument.Count);
        Assert.AreEqual("x", argument.OptionStr);

        argv.str_pos = 1;
        arg_short.parse(argv);
        Assert.AreEqual(5, argument.Count);
        Assert.AreEqual("y123", argument.OptionStr);
        }

    /** @test Test to ensure required argument values are assigned.
    */
    [TestMethod]
    //[Ignore]
    public void required_argument_one_value()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('v', null, ArgType.REQUIRED_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("-v");
        argv.add("--swallow");
        argv.add("-vx");
        argument.init(arg_table[0]);

        var arg_short = new ArgShort(arg_table);

        argv.str_pos = 1;
        arg_short.parse(argv);

        Assert.AreEqual(1, argument.Count);
        Assert.AreEqual("--swallow", argument.OptionStr);

        argv.str_pos = 1;
        arg_short.parse(argv);
        Assert.AreEqual(2, argument.Count);
        Assert.AreEqual("x", argument.OptionStr);
        }

    /** @test Test to ensure required argument values are assigned and overwritten.
    */
    [TestMethod]
    //[Ignore]
    public void required_argument_assigned_value()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('v', "verbose", ArgType.REQUIRED_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("-v=12345");
        argv.add("-v=");
        argv.add("-vx");
        argument.init(arg_table[0]);

        var arg_short = new ArgShort(arg_table);

        argv.str_pos = 1;
        arg_short.parse(argv);

        Assert.AreEqual(1, argument.Count);
        Assert.AreEqual("12345", argument.OptionStr);

        argv.str_pos = 1;
        arg_short.parse(argv);

        Assert.AreEqual(2, argument.Count);
        Assert.AreEqual(string.Empty, argument.OptionStr);

        argv.str_pos = 1;
        arg_short.parse(argv);

        Assert.AreEqual(3, argument.Count);
        Assert.AreEqual("x", argument.OptionStr);
        }

    /** @test Test to ensure that ArgumentException is thrown for unknown argument.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "ArgumentException expected for unknown argument '-h'.")]
    //[Ignore]
    public void argument_exception()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('v', null, ArgType.NO_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("-h");
        argument.init(arg_table[0]);

        var arg_short = new ArgShort(arg_table);

        argv.str_pos = 1;
        arg_short.parse(argv);
        }

    /** @test Test to ensure that ArgumentException is thrown for invalid ArgType value.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "ArgumentException expected for invalid ArgType value.")]
    //[Ignore]
    public void arg_type_exception()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('v', null, (ArgType)int.MaxValue, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("-v");
        argv.add("-v");
        argument.init(arg_table[0]);

        argv.str_pos = 1;
        var arg_short = new ArgShort(arg_table);
        arg_short.parse(argv);
        }
    }
}
