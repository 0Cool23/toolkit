/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_iLogger io.iLogger
    @{
    @details

    @ingroup  REF_libApplication
    @}
*/

namespace libApplication.io
{
/** @ingroup   REF_iLogger

    @interface iLogger

    @brief     Interface for logger classes.
*/
public interface iLogger
    {
    /**  @details Sets the threshold for what messages will be logged.
    */
    int verbosity
        {
        get;
        set;
        }

    /** @details   Write a log message related to configuration events if the verbosity level is reached.

        @param[in] level   Threshold for current message.
        @param[in] message Message string to log.
    */
    void write( int level, string message );
    
    /** @details   Write a log message related to configuration events if the verbosity level is reached.

        @param[in] level  Threshold for current message.
        @param[in] format
        @param[in] args   An object array that contains zero or more objects to be used in format string.
    */
    void write( int level, string format, params object[] args );

    /** @details   Write a log message related to configuration events if the verbosity level is reached.

        @param[in] level   Threshold for current message.
        @param[in] message Message string to log.
    */
    void config( int level, string message );
    
    /** @details   Write a log message related to configuration events if the verbosity level is reached.

        @param[in] level  Threshold for current message.
        @param[in] format
        @param[in] args   An object array that contains zero or more objects to be used in format string.
    */
    void config( int level, string format, params object[] args );

    /** @details   Write a log message to trace progress if the verbosity level is reached.

        @param[in] level   Threshold for current message.
        @param[in] message Message string to log.
    */
    void trace( int level, string message );
    
    /** @details   Write a log message to trace progress if the verbosity level is reached.

        @param[in] level  Threshold for current message.
        @param[in] format
        @param[in] args   An object array that contains zero or more objects to be used in format string.
    */
    void trace( int level, string format, params object[] args );

    /** @details   Write a log message with debugging information if the verbosity level is reached.

        @param[in] level   Threshold for current message.
        @param[in] message Message string to log.
    */
    void debug( int level, string message );
    
    /** @details   Write a log message with debugging information if the verbosity level is reached.

        @param[in] level  Threshold for current message.
        @param[in] format
        @param[in] args   An object array that contains zero or more objects to be used in format string.
    */
    void debug( int level, string format, params object[] args );

    /** @details   Write an informational log message if the verbosity level is reached.

        @param[in] level   Threshold for current message.
        @param[in] message Message string to log.
    */
    void info( int level, string message );

    /** @details   Write an informational log message if the verbosity level is reached.

        @param[in] level  Threshold for current message.
        @param[in] format
        @param[in] args   An object array that contains zero or more objects to be used in format string.
    */
    void info( int level, string format, params object[] args );

    /** @details   Write a notify log message if the verbosity level is reached.

        @param[in] level   Threshold for current message.
        @param[in] message Message string to log.
    */
    void note( int level, string message );
    
    /** @details   Write a notify log message if the verbosity level is reached.

        @param[in] level  Threshold for current message.
        @param[in] format
        @param[in] args   An object array that contains zero or more objects to be used in format string.
    */
    void note( int level, string format, params object[] args );

    /** @details   Write a warning log message if the verbosity level is reached.

        @param[in] level   Threshold for current message.
        @param[in] message Message string to log.
    */
    void warning( int level, string message );

    /** @details   Write a warning log message if the verbosity level is reached.

        @param[in] level  Threshold for current message.
        @param[in] format
        @param[in] args   An object array that contains zero or more objects to be used in format string.
    */
    void warning( int level, string format, params object[] args );

    /** @details   Write an error log message if the verbosity level is reached.

        @param[in] level   Threshold for current message.
        @param[in] message Message string to log.
    */
    void error( int level, string message );
    
    /** @details   Write an error log message if the verbosity level is reached.


        @param[in] level  Threshold for current message.
        @param[in] format
        @param[in] args   An object array that contains zero or more objects to be used in format string.
    */
    void error( int level, string format, params object[] args );

    /** @details   Write an error (not recoverable) log message if the verbosity level is reached.

        @param[in] level   Threshold for current message.
        @param[in] message Message string to log.
    */
    void fatal( int level, string message );

    /** @details   Write an error (not recoverable) log message if the verbosity level is reached.

        @param[in] level  Threshold for current message.
        @param[in] format
        @param[in] args   An object array that contains zero or more objects to be used in format string.
    */
    void fatal( int level, string format, params object[] args );
    }
}
