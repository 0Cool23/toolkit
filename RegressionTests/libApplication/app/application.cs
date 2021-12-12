/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

using libApplication.app;
using libApplication.arg;
using libApplication.io;

namespace RegressionTests.libApplication.App
{
/** @ingroup REF_Testing REF_Application

    @class   Application_test

    @brief   Unit test for @ref REF_Application class.
*/
[TestClass]
//[Ignore]
public class Application_test
    {
    /** @test Make sure that @ref REF_Application is properly constructed.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var app = new Application();
        Assert.IsFalse(app.ForwardExceptions, "forward_exceptions is expected to default to false.");
        Assert.IsNull(app.Description, "description is expected to default to null value.");
        Assert.IsNull(app.Logger, "logger is expected to default to null value.");
        }

    internal class DummyApp
        :   Application
        {
        public Version API
            {
            get
                {
                return ApiVersion;
                }
            }

        public DummyApp()
            {
            }
        }

    /** @test Make sure that @ref REF_Application returns API version.
    */
    [TestMethod]
    //[Ignore]
    public void api_version()
        {
        var app = new DummyApp();
        Assert.IsTrue(app.API.ToString().Length > 0);
        }

   /** @test Test to ensure ArgumentNullException is caught silently and ExitCode is properly set.
    */
    [TestMethod]
    //[Ignore]
    public void main_exception_no_config_test()
        {
        var rc = Application.main(null, null, null);
        Assert.AreEqual(1,  Environment.ExitCode, "ArgumentNullException should be caught by default, and exit code properly set.");
        Assert.AreEqual(rc, Environment.ExitCode, "Exit code and main result did not match.");
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void main_exception_arg_parser_test()
        {
        var app_config = new AppConfig();
        var rc = Application.main(null, null, app_config);
        Assert.AreEqual(1,  Environment.ExitCode, "ArgumentNullException (args || arg_table) should from ArgParser, and exit code properly set.");
        Assert.AreEqual(rc, Environment.ExitCode, "Exit code and main result did not match.");
        }

    /** @test
    */
    [TestMethod()]
    //[Ignore]
    public void main_empty()
        {
        var args = Array.Empty<string>();
        var arg_table = new List<ArgEntry>();
        var app_config = new AppConfig();
        var rc = Application.main(args, arg_table, app_config);
        Assert.AreEqual(0, Environment.ExitCode, "Should exit graceful for empty settings.");
        Assert.AreEqual(rc, Environment.ExitCode, "Exit code and main result did not match.");
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void main_empty_logger()
        {
        var args = Array.Empty<string>();
        var arg_table = new List<ArgEntry>();
        var app_config = new AppConfig
            {
            logger = new StringLogger()
            };
        var rc = Application.main(args, arg_table, app_config);
        Assert.AreEqual(0,  Environment.ExitCode, "Should exit graceful for empty settings.");
        Assert.AreEqual(rc, Environment.ExitCode, "Exit code and main result did not match.");
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void attach_test()
        {
        var logger = new StringLogger();
        var application = new Application();

        /* verbosity will be overwritten by internal application m_arg_verbose count value. */
        logger.verbosity = 5;
        application.attach(logger);
        Assert.AreEqual<int>(0, application.Logger.verbosity, "Application logger verbosity does not match.");
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void help_test()
        {
        var args = new string[]{"--help"};
        var arg_table = new List<ArgEntry>();
        var app_config = new AppConfig();
        var logger = new StringLogger();
        app_config.logger = logger;

        var rc = Application.main(args, arg_table, app_config);
        Assert.AreEqual(2, Environment.ExitCode, "Should exit graceful and print license (short) to logger.");
        Assert.AreEqual(rc, Environment.ExitCode, "Exit code and main result did not match.");
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void help_null_test()
        {
        var args = new string[]{"--help"};
        var arg_table = new List<ArgEntry>();
        var app_config = new AppConfig();

        var rc = Application.main(args, arg_table, app_config);
        Assert.AreEqual(2, Environment.ExitCode, "Should exit graceful and print license (short) to logger.");
        Assert.AreEqual(rc, Environment.ExitCode, "Exit code and main result did not match.");
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void help_mandatory_short_test()
        {
        var args = new string[]{"--help"};
        var argument = new Argument();
        var arg_table = new List<ArgEntry>
            {
                {new ArgEntry('m', null, ArgType.NO_ARGUMENT, true, argument, "", "Mandatory help.")}
            };
        var app_config = new AppConfig();
        var logger = new StringLogger();
        app_config.logger = logger;

        var rc = Application.main(args, arg_table, app_config);
        Assert.AreEqual(2, Environment.ExitCode, "Should exit graceful and print license (short) to logger.");
        Assert.AreEqual(rc, Environment.ExitCode, "Exit code and main result did not match.");
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void help_mandatory_long_test()
        {
        var args = new string[]{"--help"};
        var argument = new Argument();
        var arg_table = new List<ArgEntry>
            {
                {new ArgEntry('\0', "mandatory", ArgType.NO_ARGUMENT, true, argument, "", "Mandatory help.")}
            };
        var app_config = new AppConfig();
        var logger = new StringLogger();
        app_config.logger = logger;

        var rc = Application.main(args, arg_table, app_config);
        Assert.AreEqual(2, Environment.ExitCode, "Should exit graceful and print license (short) to logger.");
        Assert.AreEqual(rc, Environment.ExitCode, "Exit code and main result did not match.");
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void license_short_test()
        {
        var args = new string[]{"--license"};
        var arg_table = new List<ArgEntry>();
        var app_config = new AppConfig();
        var logger = new StringLogger();
        app_config.logger = logger;

        var rc = Application.main(args, arg_table, app_config);
        Assert.AreEqual(2, Environment.ExitCode, "Should exit graceful and print help to logger.");
        Assert.AreEqual(rc, Environment.ExitCode, "Exit code and main result did not match.");
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void license_long_test()
        {
        var args = new string[]{"-ll"};
        var arg_table = new List<ArgEntry>();
        var app_config = new AppConfig();
        var logger = new StringLogger();
        app_config.logger = logger;

        var rc = Application.main(args, arg_table, app_config);
        Assert.AreEqual(2, Environment.ExitCode, "Should exit graceful and print help to logger.");
        Assert.AreEqual(rc, Environment.ExitCode, "Exit code and main result did not match.");
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void license_null_test()
        {
        var args = new string[]{"-ll"};
        var arg_table = new List<ArgEntry>();
        var app_config = new AppConfig();

        var rc = Application.main(args, arg_table, app_config);
        Assert.AreEqual(2, Environment.ExitCode, "Should exit graceful and print help to logger.");
        Assert.AreEqual(rc, Environment.ExitCode, "Exit code and main result did not match.");
        }

    /** @test Test to ensure that ArgumentNullException is forwarded (thrown).
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException exception should be forwarded when args is null.")]
    //[Ignore]
    public void forward_exception()
        {
        var app_config = new AppConfig
            {
            forward_exceptions = true
            };

        var _ = Application.main(null, null, app_config);
        }

    /** @test
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "ArgumentNullException exception should be forwarded when args is null.")]
    //[Ignore]
    public void collision_short_arg_exception_test()
        {
        var args = Array.Empty<string>();
        var argument = new Argument();
        var arg_table = new List<ArgEntry>
            {
                {new ArgEntry('h', null, ArgType.NO_ARGUMENT, false, argument, null, "Duplicate short option -h.")},
            };
        var app_config = new AppConfig
            {
            forward_exceptions = true
            };

        var _ = Application.main(args, arg_table, app_config);
        }

    /** @test
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "ArgumentException exception should be forwarded when args is null.")]
    //[Ignore]
    public void collision_long_arg_exception_test()
        {
        var args = Array.Empty<string>();
        var argument = new Argument();
        var arg_table = new List<ArgEntry>
            {
                {new ArgEntry('\0', "help", ArgType.NO_ARGUMENT, false, argument, null, "Duplicate short option -h.")},
            };
        var app_config = new AppConfig
            {
            forward_exceptions = true
            };

        var _ = Application.main(args, arg_table, app_config);
        }

    /** @test
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "ArgumentException exception should be forwarded when mandatory are was not present.")]
    //[Ignore]
    public void mandatory_test()
        {
        var args = Array.Empty<string>();
        var argument = new Argument();
        var arg_table = new List<ArgEntry>
            {
                {new ArgEntry('m', null, ArgType.NO_ARGUMENT, true, argument, "", "Mandatory help.")}
            };
        var app_config = new AppConfig
            {
            forward_exceptions = true
            };

        var _ = Application.main(args, arg_table, app_config);
        }

    private class TestApplication
        :   Application
        {
        public void asm_version_test()
            {
            Assert.AreEqual("0.0.0.0", AsmVersion.ToString(), "AsmVersion default mismatch after construction.");
            AsmVersion = new Version(1, 2, 3, 4);
            Assert.AreEqual("1.2.3.4", AsmVersion.ToString(), "AsmVersion version mismatch.");
            }

        public void api_version_test()
            {
            Assert.AreEqual("0.0.0.0", ApiVersion.ToString(), "ApiVersion default mismatch after construction.");
            ApiVersion = new Version(4, 3, 2, 1);
            Assert.AreEqual("4.3.2.1", ApiVersion.ToString(), "ApiVersion version mismatch.");
            }

        private static readonly AppConfig the_app_config = new()
            {
            description = "TestApplication",
            forward_exceptions = false,
            logger = new ConsoleLogger(),
            };

        protected override void display_usage_example()
            {
            base.display_usage_example();
            Logger.write(0, "usage example");
            }

        public void usage_test()
            {
            var rc = 0;
            var console_output = string.Empty;
            using ( var stream_writer = new StringWriter() )
                {
                Console.SetOut(stream_writer);
                rc = run(new List<string>{"-h"}.ToArray() , null, the_app_config);
                console_output = stream_writer.ToString();
                }
            console_output = console_output.TrimEnd();
            Assert.IsTrue(console_output.EndsWith("usage example"), "Console output '{0}' for usage test did not match.", console_output);
            Assert.AreEqual(2, rc, "Exit code did not match expected value.");
            }

        public void build_info_test()
            {
            var rc = 0;
            using (var stream_writer = new StringWriter())
                {
                Console.SetOut(stream_writer);
                rc = run(new List<string> {"--build-info" }.ToArray(), null, the_app_config);
                var console_output = stream_writer.ToString();
                }
            Assert.AreEqual(2, rc, "Exit code did not match expected value.");
            }

        private static readonly Argument       the_dummy_argument = new();
        private static readonly List<ArgEntry> the_test_arg_table = new()
            {
                new ArgEntry('x',  "xx",       ArgType.NO_ARGUMENT, false, the_dummy_argument, string.Empty, "xx dummy value."),
                new ArgEntry('\0', "xy",       ArgType.NO_ARGUMENT, false, the_dummy_argument, string.Empty, "xy dummy value."),
                new ArgEntry('y',  "yy",       ArgType.NO_ARGUMENT, false, the_dummy_argument, string.Empty, "yy dummy value."),
                new ArgEntry('z',  string.Empty, ArgType.NO_ARGUMENT, false, the_dummy_argument, string.Empty, "z dummy value."),
            };

        public void arg_table_test()
            {
            var rc = run(new List<string> {"--help" }.ToArray(), the_test_arg_table, the_app_config);
            Assert.AreEqual(2, rc, "Exit code did not match expected value.");
            }
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void application_version()
        {
        var app = new TestApplication();
        app.asm_version_test();
        app.api_version_test();
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void application_usage()
        {
        var app = new TestApplication();
        app.usage_test();
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void application_build_info()
        {
        var app = new TestApplication();
        app.build_info_test();
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void application_arg_table()
        {
        var app = new TestApplication();
        app.arg_table_test();
        }
    }
}
