/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2021

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using libApplication.app;
using libApplication.io;

namespace RegressionTests.libApplication.app
{
/** @ingroup REF_Testing REF_AsmInfo

    @class   AsmInfo_test

    @brief   Unit test for @ref REF_AsmInfo class.
*/
[TestClass]
//[Ignore]
public class AsmInfo_test
    {
    public static string usage_output( AsmInfo asm_info, Assembly assembly, int verbosity )
        {
        var console_output = string.Empty;
        var console_logger = new ConsoleLogger();
        using( var stream_writer = new StringWriter() )
            {
            Console.SetOut(stream_writer);
            asm_info.log(console_logger, assembly, verbosity);
            console_output = stream_writer.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(console_output), "Console output must not be an empty string or null value.");
            }
        return console_output;
        }

    private class AsmException
        :   AsmInfo
        {
        public AsmException()
            :   base(string.Empty)
            {
            }
        }

    /** @test Test checks that an ArgumentException exception is thrown when required build_type_name string for @ref REF_AsmInfo base class
              is an empty string or null value.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Constructor must throw an exception when build_type_name is an empty string or null value.")]
    //[Ignore]
    public void construnction_exception()
        {
        var _ = new AsmException();
        }

    private class AsmVerification
        :   AsmInfo
        {
        public AsmVerification()
            :   base(".Generated.BuildInfo")
            {
            }

        protected override void log_basic( FieldInfo[] field_info_list )
            {
            base.log_basic(field_info_list);
            m_logger.write(0, "basic");
            }

        protected override void log_extended( FieldInfo[] field_info_list )
            {
            base.log_extended(field_info_list);
            m_logger.write(0, "extended");
            }
        }

    /** @test Test checks that returned console output is an empty string if logger is a null value.
    */
    [TestMethod]
    //[Ignore]
    public void logger_null()
        {
        var asm_verification = new AsmVerification();
        using var stream_writer = new StringWriter();
        Console.SetOut(stream_writer);
        asm_verification.log(null, null, 0);
        var console_output = stream_writer.ToString();
        Assert.AreEqual(string.Empty, console_output, "Console output must be an empty string if logger argument is a null value.");
        }

    /** @test Test ensures that an ArgumentNullException is raised when argument entry_assembly is a null value.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "ArgumentNullException exception expected for ")]
    //[Ignore]
    public void assembly_null()
        {
        var console_logger = new ConsoleLogger();
        var asm_verification = new AsmVerification();
        asm_verification.log(console_logger, null, 0);
        }

    private static void compare_output( int level, string current, string previous )
        {
        current = current.TrimEnd();
        previous = previous.TrimEnd();
        Assert.IsTrue(current.StartsWith(previous),     "Console output ({0}) '{1}' must start with the console output ({2}) '{3}' from previous verbosity level.", level, current, level - 1, previous);
        Assert.IsTrue(current.Length > previous.Length, "Console output ({0}) '{1}' length must be greater than the console output ({2}) '{3}' from the previous.", level, current, level - 1, previous);
        }

    private class AsmFieldVerification
        :   AsmInfo
        {
        public AsmFieldVerification()
            :   base(".Generated.BuildInfo")
            {
            }

        private static void init_field_name_map_exception()
            {
            // init_field_name_map exceptions (field_info_list == null)
            try
                {
                var _ = init_field_name_map(null, null);
                Assert.Fail("init_field_name_map must throw ArgumentNullException if argument field_info_list is null.");
                }
            catch( ArgumentNullException )
                {
                }
            catch( Exception )
                {
                Assert.Fail("init_field_name_map exception mismatch, expected ArgumentNullException for argument field_info_list as null.");
                }
            }

        private static void init_field_name_map_exception( FieldInfo[] field_info_list )
            {
            // init_field_name_map exceptions (field_name_list == null)
            try
                {
                var _ = init_field_name_map(field_info_list, null);
                Assert.Fail("init_field_name_map must throw ArgumentException if argument field_name_list is null.");
                }
            catch( ArgumentException )
                {
                }
            catch( Exception )
                {
                Assert.Fail("init_field_name_map exception mismatch, expected ArgumentException for argument field_name_list as null.");
                }
            }

        private static void init_field_name_map_overwrite( FieldInfo[] field_info_list, List<string> field_name_list )
            {
            var _ = init_field_name_map(field_info_list, field_name_list);
            }

        private static void get_field_value_exception()
            {
            // get_field_value exceptions (field_info_list == null)
            try
                {
                var _ = get_field_value(null, null);
                Assert.Fail("get_field_value must throw ArgumentNullException if argument field_info_list is null.");
                }
            catch( ArgumentNullException )
                {
                }
            catch( Exception )
                {
                Assert.Fail("get_field_value exception mismatch, expected ArgumentNullException for argument field_info_list as null.");
                }
            }

        private static void get_field_value_exception( FieldInfo[] field_info_list )
            {
            // get_field_value exceptions (invalid field_name argument)
            try
                {
                var _ = get_field_value(field_info_list, null);
                Assert.Fail("get_field_value must throw ArgumentException if argument field_name an empty string or null value.");
                }
            catch( ArgumentException )
                {
                }
            catch( Exception )
                {
                Assert.Fail("get_field_value exception mismatch, expected ArgumentException for argument field_name as empty string or null value.");
                }
            }

        private static void get_field_value_not_existing( FieldInfo[] field_info_list, string field_name )
            {
            var empty_string = get_field_value(field_info_list, field_name);
            Assert.AreEqual(string.Empty, empty_string, "get_field_value result must be an empty string for not existing structure element value.");
            }

        protected override void log_basic( FieldInfo[] field_info_list )
            {
            init_field_name_map_exception();
            init_field_name_map_exception(field_info_list);

            init_field_name_map_overwrite(field_info_list, new List<string>{"ProjectName", "ProjectName"});

            get_field_value_exception();
            get_field_value_exception(field_info_list);
            get_field_value_exception(field_info_list);

            get_field_value_not_existing(field_info_list, "NotExistingFieldPropertyName");
            }

        protected override void log_extended( FieldInfo[] field_info_list )
            {
            }
        }

    /** @test Test ensures that init_field_name_map and get_field_value handles exception cases correctly.
    */
    [TestMethod]
    //[Ignore]
    public void field_name_map_exceptions()
        {
        var assembly = Assembly.Load("RegressionTests");
        var asm_verification = new AsmFieldVerification();
        var _ = usage_output(asm_verification, assembly, 0);
        }
    }
}
