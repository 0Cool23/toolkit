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
/** @ingroup REF_Testing REF_ArgEntry

    @class   ArgEntry_test

    @brief   Unit test for @ref REF_ArgEntry class.
*/
[TestClass]
//[Ignore]
public class ArgEntry_test
    {
    private static void must_pass( char short_arg, string long_arg, ArgType type, bool is_mandatory, Argument param, string initial_value, string help )
        {
        ArgEntry arg_entry = new(short_arg, long_arg, type, is_mandatory, param, initial_value, help);

        Assert.AreEqual(short_arg, arg_entry.short_name, "short_name assignment failed.");
        Assert.AreEqual(long_arg, arg_entry.long_name, "long_name assignment failed.");
        Assert.AreEqual(type, arg_entry.type, "ArgType assignment failed.");
        Assert.AreEqual(is_mandatory, arg_entry.is_mandatory, "is_mandatory assignment failed.");
        Assert.AreEqual(param, arg_entry.param, "param assignment failed.");
        Assert.AreEqual(initial_value, arg_entry.initial_value, "initial_value assignment failed.");
        Assert.AreEqual(help, arg_entry.help_text, "help_text assignment failed.");  
        }

    private static void catch_argument_exception( char short_arg, string long_arg, ArgType type, bool is_mandatory, Argument param, string initial_value, string help )
        {
        try
            {
            ArgEntry arg_entry = new(short_arg, long_arg, type, is_mandatory, param, initial_value, help);
            }
        catch( ArgumentException except )
            {
            Console.WriteLine("Caught expected (ArgumentException) exception: " + except.Message);
            return;
            }
        catch( Exception except )
            {
            Assert.Fail("Unexpected exception caught: " + except.Message);
            }
        Assert.Fail("No exception was thrown, but construction should have failed.");
        }

    private static void catch_argument_null_exception( char short_arg, string long_arg, ArgType type, bool is_mandatory, Argument param, string initial_value, string help )
        {
        try
            {
            ArgEntry arg_entry = new(short_arg, long_arg, type, is_mandatory, param, initial_value, help);
            }
        catch( ArgumentNullException except )
            {
            Console.WriteLine("Caught expected (ArgumentNullException) exception: " + except.Message);
            return;
            }
        catch( Exception except )
            {
            Assert.Fail("Unexpected exception caught: " + except.Message);
            }
        Assert.Fail("No exception was thrown, but construction should have failed.");
        }

    /** @test Test to make sure that ArgEntry is properly constructed for some valid cases.
    */
    [TestMethod]
    //[Ignore]
    public void valid_arg_construction()
        {
        Argument param = new();

        must_pass('v',     "version",     ArgType.NO_ARGUMENT,       false, param, null,         "Display version number.");
        must_pass((char)0, "1-ver.si_on", ArgType.OPT_ARGUMENT,      false, param, string.Empty, "Display version number.");
        must_pass('v',     null,          ArgType.REQUIRED_ARGUMENT, true,  param, "a",          "Display version number.");
        must_pass('v',     string.Empty,  ArgType.NO_ARGUMENT,       true,  param, "abc",        "Display version number.");
        }

    /** @test Test to make sure that ArgEntry throws an exception for some invalid cases.
              - short argument is invalid character.
              - long argument does not start with letter or digit.
              - long argument contains invalid characters.
              - help text does not end with '.' character.
              - help text contains control characters.
    */
    [TestMethod]
    //[Ignore]
    public void invalid_arg_construction()
        {
        Argument param = new();
        // malformed short argument
        catch_argument_exception('@', null, ArgType.REQUIRED_ARGUMENT, true,  param, "a", "Display version number.");

        // malformed long arguments
        catch_argument_exception((char)0, "-version",   ArgType.OPT_ARGUMENT, false, param, string.Empty, "Display version number.");
        catch_argument_exception((char)0, "1-version#", ArgType.OPT_ARGUMENT, false, param, string.Empty, "Display version number.");

        // test malformed help text
        catch_argument_exception((char)0, "version", ArgType.OPT_ARGUMENT, false, param, string.Empty, "Display version number");
        catch_argument_exception((char)0, "version", ArgType.OPT_ARGUMENT, false, param, string.Empty, "Display\t version number.");
        }

    /** @test Test to make sure that ArgEntry throws an exception for some invalid cases.
              - short argument is 0x00 and long argument is empty or null string.
              - param is null.
              - help test is empty of null string.
    */
    [TestMethod]
    //[Ignore]
    public void invalid_arg_null_construction()
        {
        Argument param = new();

        // no or empty long and short arguments
        catch_argument_null_exception((char)0, null, ArgType.OPT_ARGUMENT, false, param, string.Empty, "Dummy help text.");
        catch_argument_null_exception((char)0, string.Empty,   ArgType.OPT_ARGUMENT, false, param, string.Empty, "Dummy help text.");

        // param is null object
        catch_argument_null_exception('a', null,  ArgType.NO_ARGUMENT, true, null, null, null);
        catch_argument_null_exception('a', "abc", ArgType.NO_ARGUMENT, true, null, null, null);

        // help text is null or empty string
        catch_argument_null_exception((char)0, "version", ArgType.OPT_ARGUMENT, false, param, string.Empty, null);
        catch_argument_null_exception((char)0, "version", ArgType.OPT_ARGUMENT, false, param, string.Empty, string.Empty);
        }

    /** @test Test to make sure that ArgEntry name property is properly set.
    */
    [TestMethod]
    //[Ignore]
    public void property_version()
        {
        var param = new Argument();

        var arg_entry = new ArgEntry('v', "version", ArgType.NO_ARGUMENT, false, param, null, "Display version number.");
        Assert.AreEqual("--version", arg_entry.name);

        arg_entry = new ArgEntry('v', null, ArgType.NO_ARGUMENT, false, param, null, "Display version number.");
        Assert.AreEqual("-v", arg_entry.name);
        }
    }
}
