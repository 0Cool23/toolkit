/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_ConsoleLogger io.ConsoleLogger
    @{
    @brief

    @details

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;

namespace libApplication.io
{
/** @ingroup REF_ConsoleLogger

    @class   ConsoleLogger

    @brief   Logging facility which writes the messages to a console.
    */
public class ConsoleLogger
    :   StringLogger
    {
    private static readonly ConsoleColor DEFAULT_COLOR = Console.ForegroundColor;

    private static readonly Dictionary<LogType, ConsoleColor> CONSOLE_COLORS = new Dictionary<LogType, ConsoleColor>
        {
            {LogType.WRITE,   ConsoleColor.Gray},
            {LogType.CONFIG,  ConsoleColor.DarkGray},
            {LogType.TRACE,   ConsoleColor.DarkBlue},
            {LogType.DEBUG,   ConsoleColor.Cyan},
            {LogType.INFO,    ConsoleColor.Blue},
            {LogType.NOTE,    ConsoleColor.Green},
            {LogType.WARNING, ConsoleColor.Yellow},
            {LogType.ERROR,   ConsoleColor.Red},
            {LogType.FATAL,   ConsoleColor.DarkRed},
        };

    /** @details
    */
    public ConsoleLogger()
        :   base()
        {
        }

    /** @details Override to log the Message string property to a console.
    */
    protected override void do_log_message()
        {
        var old_foreground_color = Console.ForegroundColor;
        Console.ForegroundColor  = DEFAULT_COLOR;

        // A color should alway be defined, this may happen when a new LogType
        // is introduced in StringLogger and CONSOLE_COLORS did define a color
        // for this type. Code coverage will only get 1 of 2 possible branches.
        if( CONSOLE_COLORS.ContainsKey(type) )
            {
            Console.ForegroundColor = CONSOLE_COLORS[type];
            }

        if( is_failure )
            {
            Console.Error.WriteLine(message);
            }
        else
            {
            Console.WriteLine(message);
            }
        Console.ForegroundColor = old_foreground_color;
        }
    }
}
