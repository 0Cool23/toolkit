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
/** @ingroup REF_Testing REF_ArgLong

    @class   ArgLong_test

    @brief   Unit test for @ref REF_ArgLong class.
*/
[TestClass]
//[Ignore]
public class ArgLong_test
    {
    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var arg_table = new List<ArgEntry>()
            {
            };
        var _ = new ArgLong(arg_table);
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void is_long_option_true()
        {
        var argv = new ArgVector();
        argv.add("--verbose");

        var result = ArgLong.is_long_option(argv);
        Assert.AreEqual(true, result, "is_long_option for '--verbose' must return true.");
        Assert.AreEqual(0, argv.str_pos, "str_pos must be 0 after long arg has been detected.");
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void is_long_option_false()
        {
        var argv = new ArgVector();
        argv.add("-verbose");
        argv.add("xy");
        argv.add("--=");
        
        var result = ArgLong.is_long_option(argv);
        Assert.AreEqual(false, result, "is_long_option for '--verbose' must return false.");
        Assert.AreEqual(0, argv.str_pos, "str_pos must be 0 if long arg has not been detected.");

        argv.inc_index();
        result = ArgLong.is_long_option(argv);
        Assert.AreEqual(false, result, "is_long_option for 'xy' must return false.");
        Assert.AreEqual(0, argv.str_pos, "str_pos must be 0 if short arg was not detected.");

        argv.inc_index();
        result = ArgLong.is_long_option(argv);
        Assert.AreEqual(false, result, "is_long_option for '--=' must return false.");
        Assert.AreEqual(0, argv.str_pos, "str_pos must be 0 if short arg was not detected.");
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void no_argument()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('\0', "verbose", ArgType.NO_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("--verbose");
        argv.add("--verbose");
        argument.init(arg_table[0]);

        var arg_long = new ArgLong(arg_table);

        argv.str_pos = 2;
        arg_long.parse(argv);

        Assert.AreEqual(1, argument.Count);
        Assert.AreEqual(null, argument.OptionStr);

        argv.str_pos = 2;
        arg_long.parse(argv);

        Assert.AreEqual(2, argument.Count);
        Assert.AreEqual(null, argument.OptionStr);
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void opt_argument_no_arg()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('\0', "verbose", ArgType.OPT_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("--verbose");
        argument.init(arg_table[0]);

        var arg_long = new ArgLong(arg_table);

        argv.str_pos = 2;
        arg_long.parse(argv);

        Assert.AreEqual(1, argument.Count);
        Assert.AreEqual(null, argument.OptionStr);
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void opt_argument_one_arg()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('\0', "verbose", ArgType.OPT_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("--verbose");
        argv.add("12345");
        argument.init(arg_table[0]);

        var arg_long = new ArgLong(arg_table);

        argv.str_pos = 2;
        arg_long.parse(argv);

        Assert.AreEqual(1, argument.Count);
        Assert.AreEqual("12345", argument.OptionStr);
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void opt_argument_two_arg()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('\0', "verbose", ArgType.OPT_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("--verbose");
        argv.add("--verbose");
        argument.init(arg_table[0]);

        var arg_long = new ArgLong(arg_table);

        argv.str_pos = 2;
        arg_long.parse(argv);

        Assert.AreEqual(1, argument.Count);
        Assert.AreEqual(null, argument.OptionStr);

        argv.str_pos = 2;
        arg_long.parse(argv);
        Assert.AreEqual(2, argument.Count);
        Assert.AreEqual(null, argument.OptionStr);
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void opt_argument_assigned_arg()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('\0', "verbose", ArgType.OPT_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("--verbose=12345");
        argv.add("--verbose=");
        argument.init(arg_table[0]);

        var arg_long = new ArgLong(arg_table);

        argv.str_pos = 2;
        arg_long.parse(argv);

        Assert.AreEqual(1, argument.Count);
        Assert.AreEqual("12345", argument.OptionStr);

        argv.str_pos = 2;
        arg_long.parse(argv);
        Assert.AreEqual(2, argument.Count);
        Assert.AreEqual(string.Empty, argument.OptionStr);
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void required_argument_one_arg()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('\0', "verbose", ArgType.REQUIRED_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("--verbose");
        argv.add("--swallow");
        argument.init(arg_table[0]);

        var arg_long = new ArgLong(arg_table);

        argv.str_pos = 2;
        arg_long.parse(argv);

        Assert.AreEqual(1, argument.Count);
        Assert.AreEqual("--swallow", argument.OptionStr);
        }

    /** @test Test 
    */
    [TestMethod]
    //[Ignore]
    public void required_argument_assigned_arg()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('\0', "verbose", ArgType.REQUIRED_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("--verbose=12345");
        argv.add("--verbose=");
        argument.init(arg_table[0]);

        var arg_long = new ArgLong(arg_table);
        argv.str_pos = 2;
        arg_long.parse(argv);

        Assert.AreEqual(1, argument.Count);
        Assert.AreEqual("12345", argument.OptionStr);

        argv.str_pos = 2;
        arg_long.parse(argv);

        Assert.AreEqual(2, argument.Count);
        Assert.AreEqual(string.Empty, argument.OptionStr);
        }

    /** @test Test ensures that ArgumentException is thrown for unknown argument.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Exception expected for unknown argument '--help'.")]
    //[Ignore]
    public void argument_exception()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('\0', "verbose", ArgType.NO_ARGUMENT, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("--help");
        argument.init(arg_table[0]);

        var arg_long = new ArgLong(arg_table);

        argv.str_pos = 2;
        arg_long.parse(argv);
        }

    /** @test Test ensures that ArgumentException is thrown for an invalid ArgType value.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "ArgumentException expected for invalid ArgType.")]
    //[Ignore]
    public void type_exception()
        {
        var argument = new Argument();
        var arg_table = new List<ArgEntry>()
            {
                {new ArgEntry('\0', "verbose", (ArgType)int.MaxValue, false, argument, string.Empty, "Increase verbosity level.")},
            };

        /* As internal class setting precondition manually for testing is okay. All classes
           which use internal classes are responsible for doing hte same. */
        var argv = new ArgVector();
        argv.add("--verbose");
        argv.add("--verbose");
        argument.init(arg_table[0]);

        var arg_long = new ArgLong(arg_table);

        argv.str_pos = 2;
        arg_long.parse(argv);
        }
    }
}
