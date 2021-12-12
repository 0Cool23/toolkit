/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/
using Microsoft.VisualStudio.TestTools.UnitTesting;

using libApplication.io;

namespace RegressionTests.libApplication.io
{
/** @ingroup REF_Testing REF_StringLogger

    @class StringLogger_test

    @brief Unit test for @ref REF_StringLogger class.
*/
[TestClass]
//[Ignore]
public class StringLogger_test
    {
    /** @test Test to make sure that StringLogger defaults are properly set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var logger = new StringLogger();

        Assert.AreEqual<int>(0, logger.verbosity, "verbosity must be 0 after construction.");
        Assert.AreEqual<string>(string.Empty, logger.message, "message must be empty after construction.");
        Assert.AreEqual<bool>(false, logger.is_failure, "is_failure must be false after construction.");
        Assert.AreEqual<StringLogger.LogType>(StringLogger.LogType.WRITE, logger.type, "type must be LogType.WRITE after construction.");
        }

    private static void test_logger_results( StringLogger logger, int verbosity, string message, bool failure_flag, StringLogger.LogType type )
        {
        Assert.AreEqual<int>(verbosity, logger.verbosity, "verbosity expected to be {0} after logging '{1}'.", verbosity, message);
        Assert.AreEqual<string>(message, logger.message, "message mismatch after logging '{0}'.", message);
        Assert.AreEqual<bool>(failure_flag, logger.is_failure, "is_failure must be false after logging '{0}'.", message);
        Assert.AreEqual<StringLogger.LogType>(type, logger.type, "expected type {0} did not match after logging '{1}'.", type, message);
        }

    /** @test Test StringLogger properties when using a sequence of string logging methods.
    */
    [TestMethod]
    //[Ignore]
    public void message_null()
        {
        var logger = new StringLogger();

        logger.write(0, null);
        test_logger_results(logger, 0, string.Empty, false, StringLogger.LogType.WRITE);
        }

    /** @test Test StringLogger properties when using a sequence of string logging methods.
    */
    [TestMethod]
    //[Ignore]
    public void min_verbosity()
        {
        var logger = new StringLogger();

        logger.write(0, "log write level 0");
        test_logger_results(logger, 0, "log write level 0", false, StringLogger.LogType.WRITE);

        logger.config(0, "log config level 0");
        test_logger_results(logger, 0, "log config level 0", false, StringLogger.LogType.CONFIG);

        logger.trace(0, "log trace level 0");
        test_logger_results(logger, 0, "log trace level 0", false, StringLogger.LogType.TRACE);

        logger.debug(0, "log debug level 0");
        test_logger_results(logger, 0, "log debug level 0", false, StringLogger.LogType.DEBUG);

        logger.info(0, "log info level 0");
        test_logger_results(logger, 0, "log info level 0", false, StringLogger.LogType.INFO);

        logger.note(0, "log note level 0");
        test_logger_results(logger, 0, "log note level 0", false, StringLogger.LogType.NOTE);

        logger.warning(0, "log warning level 0");
        test_logger_results(logger, 0, "log warning level 0", true, StringLogger.LogType.WARNING);

        logger.error(0, "log error level 0");
        test_logger_results(logger, 0, "log error level 0", true, StringLogger.LogType.ERROR);

        logger.fatal(0, "log fatal level 0");
        test_logger_results(logger, 0, "log fatal level 0", true, StringLogger.LogType.FATAL);
        }

    /** @test Test StringLogger properties when using a sequence of format string logging methods.
    */
    [TestMethod]
    //[Ignore]
    public void min_verbosity_with_format()
        {
        var logger = new StringLogger();

        logger.write(0, "log write level {0}", "1");
        test_logger_results(logger, 0, "log write level 1", false, StringLogger.LogType.WRITE);

        logger.config(0, "log config level {0}", "1");
        test_logger_results(logger, 0, "log config level 1", false, StringLogger.LogType.CONFIG);

        logger.trace(0, "log trace level {0}", "1");
        test_logger_results(logger, 0, "log trace level 1", false, StringLogger.LogType.TRACE);

        logger.debug(0, "log debug level {0}", "1");
        test_logger_results(logger, 0, "log debug level 1", false, StringLogger.LogType.DEBUG);

        logger.info(0, "log info level {0}", "1");
        test_logger_results(logger, 0, "log info level 1", false, StringLogger.LogType.INFO);

        logger.note(0, "log note level {0}", "1");
        test_logger_results(logger, 0, "log note level 1", false, StringLogger.LogType.NOTE);

        logger.warning(0, "log warning level {0}", "1");
        test_logger_results(logger, 0, "log warning level 1", true, StringLogger.LogType.WARNING);

        logger.error(0, "log error level {0}", "1");
        test_logger_results(logger, 0, "log error level 1", true, StringLogger.LogType.ERROR);

        logger.fatal(0, "log fatal level {0}", "1");
        test_logger_results(logger, 0, "log fatal level 1", true, StringLogger.LogType.FATAL);
        }

    /** @test Test StringLogger properties when verbosity level is less than logging level.
    */
    [TestMethod]
    //[Ignore]
    public void max_verbosity()
        {
        var logger = new StringLogger
            {
            verbosity = 1
            };

        logger.fatal(2, "fault message level 2");
        test_logger_results(logger, 1, string.Empty, true, StringLogger.LogType.FATAL);

        logger.warning(2, "warn message level 2");
        test_logger_results(logger, 1, string.Empty, true, StringLogger.LogType.WARNING);

        logger.note(2, "note message level 2");
        test_logger_results(logger, 1, string.Empty, false, StringLogger.LogType.NOTE);

        logger.info(2, "info message level 2");
        test_logger_results(logger, 1, string.Empty, false, StringLogger.LogType.INFO);

        logger.write(2, "log message level 2");
        test_logger_results(logger, 1, string.Empty, false, StringLogger.LogType.WRITE);
        }

    /** @test Test StringLogger properties when altering verbosity level and logging level.
    */
    [TestMethod]
    //[Ignore]
    public void message_overwrite()
        {
        var logger = new StringLogger
            {
            verbosity = 1
            };

        logger.fatal(2, "log fatal message level 3");
        test_logger_results(logger, 1, string.Empty, true, StringLogger.LogType.FATAL);

        logger.warning(1, "log warning level 3");
        test_logger_results(logger, 1, "log warning level 3", true, StringLogger.LogType.WARNING);

        logger.note(2, "log note level 3");
        test_logger_results(logger, 1, string.Empty, false, StringLogger.LogType.NOTE);

        logger.info(1, "log info level 3");
        test_logger_results(logger, 1, "log info level 3", false, StringLogger.LogType.INFO);

        logger.write(2, "log write level 3");
        test_logger_results(logger, 1, string.Empty, false, StringLogger.LogType.WRITE);
        }
    }
}
