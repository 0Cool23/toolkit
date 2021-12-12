/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using libApplication.io;

namespace RegressionTests.libApplication.io
{
/** @ingroup REF_Testing REF_ConsoleLogger
 
    @class ConsoleLogger_test

    @brief Unit test for @ref REF_ConsoleLogger class.  
*/
[TestClass]
//[Ignore]
public class ConsoleLogger_test
    {
    /** @test Test to make sure that ConsoleLogger defaults are properly set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var logger = new ConsoleLogger();

        Assert.AreEqual<int>(0, logger.verbosity, "verbosity must be 0 after construction.");
        Assert.AreEqual<string>(string.Empty, logger.message, "message must be empty after construction.");
        Assert.AreEqual<bool>(false, logger.is_failure, "is_failure must be false after construction.");
        Assert.AreEqual<ConsoleLogger.LogType>(ConsoleLogger.LogType.WRITE, logger.type, "type must be LogType.WRITE after construction.");
        }

    private static void test_logger_results( ConsoleLogger logger, int verbosity, string message, bool failure_flag, StringLogger.LogType type, ConsoleColor color )
        {
        Assert.AreEqual<int>(verbosity, logger.verbosity, "verbosity expected to be {0} after logging '{1}'.", verbosity, message);
        Assert.AreEqual<string>(message, logger.message, "message mismatch after logging '{0}'.", message);
        Assert.AreEqual<bool>(failure_flag, logger.is_failure, "is_failure must be false after logging '{0}'.", message);
        Assert.AreEqual<ConsoleLogger.LogType>(type, logger.type, "expected type {0} did not match after logging '{1}'.", type, message);
        Assert.AreEqual<ConsoleColor>(color, Console.ForegroundColor, "expected color {0} did not match after logging '{1}'.", color, message);
        }

    /** @test Test to cover overwritten do_log_message method of ConsoleLogger class.
    */
    [TestMethod]
    //[Ignore]
    public void set_log_message()
        {
        var logger = new ConsoleLogger();

        var old_foreground_color = Console.ForegroundColor;

        logger.write(0, null);
        test_logger_results(logger, 0, string.Empty, false, ConsoleLogger.LogType.WRITE, old_foreground_color);

        logger.write(0, string.Empty);
        test_logger_results(logger, 0, string.Empty, false, ConsoleLogger.LogType.WRITE, old_foreground_color);

        logger.write(0, "Logging test message");
        test_logger_results(logger, 0, "Logging test message", false, ConsoleLogger.LogType.WRITE, old_foreground_color);

        logger.fatal(0, "failure fatal message");
        test_logger_results(logger, 0, "failure fatal message", true, ConsoleLogger.LogType.FATAL, old_foreground_color);

        logger.fatal(1, "Logging fatal message");
        test_logger_results(logger, 0, string.Empty, true, ConsoleLogger.LogType.FATAL, old_foreground_color);
        }
    }
}
