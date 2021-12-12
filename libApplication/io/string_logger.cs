/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_StringLogger io.StringLogger
    @{
    @details

    @ingroup  REF_libApplication
    @}
*/

using System.Globalization;
using System.Threading;

namespace libApplication.io
{
/** @ingroup REF_StringLogger

    @class   StringLogger

    @brief   Logging facility which writes the messages to a string property.
*/
public class StringLogger
    :   iLogger
    {
    private static readonly Mutex the_mutex = new Mutex();

    /** @details Type of log messages.
    */
    public enum LogType
        :   int
        {
        WRITE   = 0, /**< standard message type */
        CONFIG  = 1, /**< type for configuration events */
        TRACE   = 2, /**< type for progress tracing */
        DEBUG   = 3,
        INFO    = 4,
        NOTE    = 5,
        WARNING = 6,
        ERROR   = 7,
        FATAL   = 8,
        };

    /** @details Constructor setting logging default values.
    */
    public StringLogger()
        {
        is_failure = false;
        type       = LogType.WRITE;
        message    = string.Empty;
        }

    /** @details Message string to log.
    */
    public string message
        {
        get;
        private set;
        } = string.Empty;

    /** @details Is true when the logged message indicates an error state.
    */
    public bool is_failure
        {
        get;
        private set;
        } = false;

    /** @details Type of log message.
    */
    public LogType type
        {
        get;
        private set;
        } = LogType.WRITE;

    #region iLogger
        /** @details Defines the threshold when messages will be logged.
        */
        public int verbosity
            {
            get;
            set;
            } = 0;

        /** @details   Write a standard log message if the verbosity level is reached.

            @param[in] level       Threshold for current message.
            @param[in] log_message Message string to log.
        */
        public void write( int level, string log_message )
            {
            is_failure = false;
            type       = LogType.WRITE;
            set_log_message(level, log_message);
            }

        /** @details   Write a standard log message if the verbosity level is reached.

            @param[in] level  Threshold for current message.
            @param[in] format Message format string to be logged.
            @param[in] args   An object array that contains zero or more objects to be used in format string.
        */
        public void write( int level, string format, params object[] args )
            {
            write(level, string.Format(CultureInfo.CurrentCulture, format, args));
            }

        /** @details   Write a log message related to configuration events if the verbosity level is reached.

            @param[in] level       Threshold for current message.
            @param[in] log_message Message string to log.
        */
        public void config( int level, string log_message )
            {
            is_failure = false;
            type       = LogType.CONFIG;
            set_log_message(level, log_message);
            }

        /** @details   Write a log message related to configuration events if the verbosity level is reached.

            @param[in] level  Threshold for current message.
            @param[in] format Message format string to be logged.
            @param[in] args   An object array that contains zero or more objects to be used in format string.
        */
        public void config( int level, string format, params object[] args )
            {
            config(level, string.Format(CultureInfo.InvariantCulture, format, args));
            }

        /** @details   Write a log message to trace progress if the verbosity level is reached.

            @param[in] level       Threshold for current message.
            @param[in] log_message Message string to log.
        */
        public void trace( int level, string log_message )
            {
            is_failure = false;
            type       = LogType.TRACE;
            set_log_message(level, log_message);
            }

        /** @details   Write a log message to trace progress if the verbosity level is reached.

            @param[in] level  Threshold for current message.
            @param[in] format Message format string to be logged.
            @param[in] args   An object array that contains zero or more objects to be used in format string.
        */
        public void trace( int level, string format, params object[] args )
            {
            trace(level, string.Format(CultureInfo.InvariantCulture, format, args));
            }

        /** @details   Write a log message with debugging information if the verbosity level is reached.

            @param[in] level       Threshold for current message.
            @param[in] log_message Message string to log.
        */
        public void debug( int level, string log_message )
            {
            is_failure = false;
            type       = LogType.DEBUG;
            set_log_message(level, log_message);
            }

        /** @details Write a log message with debugging information if the verbosity level is reached.

            @param[in] level  Threshold for current message.
            @param[in] format Message format string to be logged.
            @param[in] args   An object array that contains zero or more objects to be used in format string.
        */
        public void debug( int level, string format, params object[] args )
            {
            debug(level, string.Format(CultureInfo.InvariantCulture, format, args));
            }

        /** @details Write an informational log message if the verbosity level is reached.

            @param[in] level       Threshold for current message.
            @param[in] log_message Message string to log.
        */
        public void info( int level, string log_message )
            {
            is_failure = false;
            type       = LogType.INFO;
            set_log_message(level, log_message);
            }

        /** @details   Write an informational log message if the verbosity level is reached.

            @param[in] level  Threshold for current message.
            @param[in] format Message format string to be logged.
            @param[in] args   An object array that contains zero or more objects to be used in format string.
        */
        public void info( int level, string format, params object[] args )
            {
            info(level, string.Format(CultureInfo.InvariantCulture, format, args));
            }

        /** @details Write a notify log message if the verbosity level is reached.

            @param[in] level       Threshold for current message.
            @param[in] log_message Message string to log.
        */
        public void note( int level, string log_message )
            {
            is_failure = false;
            type       = LogType.NOTE;
            set_log_message(level, log_message);
            }

        /** @details   Write a notify log message if the verbosity level is reached.

            @param[in] level  Threshold for current message.
            @param[in] format Message format string to be logged.
            @param[in] args   An object array that contains zero or more objects to be used in format string.
        */
        public void note( int level, string format, params object[] args )
            {
            note(level, string.Format(CultureInfo.InvariantCulture, format, args));
            }

        /** @details   Write a warning log message if the verbosity level is reached.

            @param[in] level       Threshold for current message.
            @param[in] log_message Message string to log.
        */
        public void warning( int level, string log_message )
            {
            is_failure = true;
            type       = LogType.WARNING;
            set_log_message(level, log_message);
            }

        /** @details   Write a warning log message if the verbosity level is reached.

            @param[in] level  Threshold for current message.
            @param[in] format Message format string to be logged.
            @param[in] args   An object array that contains zero or more objects to be used in format string.
        */
        public void warning( int level, string format, params object[] args )
            {
            warning(level, string.Format(CultureInfo.InvariantCulture, format, args));
            }

        /** @details   Write an error log message if the verbosity level is reached.

            @param[in] level       Threshold for current message.
            @param[in] log_message Message string to log.
        */
        public void error( int level, string log_message )
            {
            is_failure = true;
            type       = LogType.ERROR;
            set_log_message(level, log_message);
            }

        /** @details   Write an error log message if the verbosity level is reached.

            @param[in] level  Threshold for current message.
            @param[in] format Message format string to be logged.
            @param[in] args   An object array that contains zero or more objects to be used in format string.
        */
        public void error( int level, string format, params object[] args )
            {
            error(level, string.Format(CultureInfo.InvariantCulture, format, args));
            }

        /** @details   Write an error (not recoverable) log message if the verbosity level is reached.

            @param[in] level       Threshold for current message.
            @param[in] log_message Message string to log.
        */
        public void fatal( int level, string log_message )
            {
            is_failure = true;
            type       = LogType.FATAL;
            set_log_message(level, log_message);
            }

        /** @details   Write an error (not recoverable) log message if the verbosity level is reached.

            @param[in] level  Threshold for current message.
            @param[in] format Message format string to be logged.
            @param[in] args   An object array that contains zero or more objects to be used in format string.
        */
        public void fatal( int level, string format, params object[] args )
            {
            fatal(level, string.Format(CultureInfo.InvariantCulture, format, args));
            }
    #endregion

    /** @details This overridable method is intended to log the Message string property to a different media.
    */
    virtual protected void do_log_message()
        {
        }

    private void do_safe_log_message( string log_message )
        {
        the_mutex.WaitOne();
        message = log_message ?? string.Empty;
        do_log_message();
        the_mutex.ReleaseMutex();
        }

    /** @details

        @param[in] level       Threshold for current message.
        @param[in] log_message Message string to log.
    */
    private void set_log_message( int level, string log_message )
        {
        if( level > verbosity )
            {
            log_message = string.Empty;
            }

        do_safe_log_message(log_message);
        }
    }
}
