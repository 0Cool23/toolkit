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
/** @class EventLogger_test

    @brief Unit test for @ref REF_EventLogger class.

    @details This file provides the unit test for EventLogger class. 

    @note    If run for the first time it will be necessary create the event log. To do so start
             a PowerShell in administartor mode and run the following command  at the shell prompt.

             @code
             > New-EventLog -LogName "libapplication.log" --Source "libApplication EventLogger"
             @endcode

             The events may be viewed using <tt>Event Viewer</tt>. In the left window expand 
             <tt>Applications and Services Logs</tt>. Click  <tt>libapplication.log</tt> to view
             event log messages.

    @ingroup GRP_Testing
*/
[TestClass]
//[Ignore]
public class EventLogger_test
    {
    private const string EVENT_LOGGER_NAME = "libApplication EventLogger";
    private const string EVENT_LOGGER_FILE = "libapplication.log";

    /** @test Test to make sure that EventLogger defaults are properly set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var logger = new EventLogger(EVENT_LOGGER_NAME, EVENT_LOGGER_FILE);

        Assert.AreEqual<int>(0, logger.verbosity, "verbosity must be 0 after construction.");
        Assert.AreEqual<string>(string.Empty, logger.message, "message must be empty after construction.");
        Assert.AreEqual<bool>(false, logger.is_failure, "is_failure must be false after construction.");
        Assert.AreEqual<EventLogger.LogType>(EventLogger.LogType.WRITE, logger.type, "type must be LogType.WRITE after construction.");
        }

    /** @test Test to make sure that SecurityException is thrown for not existing EventLogger instance.
      
        @note If run in Adminstartor mode this test will created the event and will fail. To remove run power
              shell as administrator and run the following command at the shell prompt.

              @code
              > "Remove-EventLog -LogName "not_existing.log"
              @endcode
              at the prompt.
    */
    [TestMethod]
    [ExpectedException(typeof(System.Security.SecurityException), "EventLogger instance should not exist.")]
    //[Ignore]
    public void construct_not_exsisting()
        {
        /* EventLog.SourceExists will throw this for not existing EventLogger instance. */
        var _ = new EventLogger("not existing EventLogger", "not_existing.log");
        }

    private static void test_logger_results( EventLogger logger, int verbosity, string message, bool failure_flag, StringLogger.LogType type, ConsoleColor color )
        {
        Assert.AreEqual<int>(verbosity, logger.verbosity, "verbosity expected to be {0} after logging '{1}'.", verbosity, message);
        Assert.AreEqual<string>(message, logger.message, "message mismatch after logging '{0}'.", message);
        Assert.AreEqual<bool>(failure_flag, logger.is_failure, "is_failure must be false after logging '{0}'.", message);
        Assert.AreEqual<EventLogger.LogType>(type, logger.type, "expected type {0} did not match after logging '{1}'.", type, message);
        Assert.AreEqual<ConsoleColor>(color, Console.ForegroundColor, "expected color {0} did not match after logging '{1}'.", color, message);
        }

    /** @test Test to cover overwritten do_log_message method of ConsoleLogger class.
    */
    [TestMethod]
    //[Ignore]
    public void set_log_message()
        {
        var logger = new EventLogger(EVENT_LOGGER_NAME, EVENT_LOGGER_FILE);

        var old_foreground_color = Console.ForegroundColor;

        logger.write(0, null);
        test_logger_results(logger, 0, string.Empty, false, EventLogger.LogType.WRITE, old_foreground_color);

        logger.write(0, string.Empty);
        test_logger_results(logger, 0, string.Empty, false, EventLogger.LogType.WRITE, old_foreground_color);

        logger.write(0, "Logging test message");
        test_logger_results(logger, 0, "Logging test message", false, EventLogger.LogType.WRITE, old_foreground_color);

        logger.fatal(0, "failure fatal message");
        test_logger_results(logger, 0, "failure fatal message", true, EventLogger.LogType.FATAL, old_foreground_color);

        logger.fatal(1, "Logging fatal message");
        test_logger_results(logger, 0, string.Empty, true, EventLogger.LogType.FATAL, old_foreground_color);
        }
    }
}
