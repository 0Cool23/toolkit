/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_EventLogger io.EventLogger
    @{
    @details

    @ingroup  REF_libApplication
    @}
*/

using System.Collections.Generic;
using System.Diagnostics;

namespace libApplication.io
{
/** @ingroup REF_EventLogger

    @class   EventLogger

    @brief
*/
public class EventLogger
    :   StringLogger
    {
    private readonly string m_source = null;

    private static readonly EventLogEntryType DEFAULT_EVENT_TYPE = EventLogEntryType.Information;
    private static readonly Dictionary<LogType, EventLogEntryType> EVENT_TYPES = new Dictionary<LogType, EventLogEntryType>
        {
            {LogType.WRITE,   EventLogEntryType.Information},
            {LogType.CONFIG,  EventLogEntryType.Information},
            {LogType.TRACE,   EventLogEntryType.Information},
            {LogType.DEBUG,   EventLogEntryType.Information},
            {LogType.INFO,    EventLogEntryType.Information},
            {LogType.NOTE,    EventLogEntryType.Information},
            {LogType.WARNING, EventLogEntryType.Warning},
            {LogType.ERROR,   EventLogEntryType.Error},
            {LogType.FATAL,   EventLogEntryType.Error},
        };

    /** @details

        @param[in] source_name
        @param[in] log_name

        @exception Will forward all exceptions from EventLog.SourceExists and
                   EventLog.CreateEventSource.
    */
    public EventLogger( string source_name, string log_name )
        :   base()
        {
        if( !EventLog.SourceExists(source_name) )
            {
            EventLog.CreateEventSource(source_name, log_name);
            }
        m_source = source_name;
        }

    /** @details
    */
    protected override void do_log_message()
        {

        var event_type = DEFAULT_EVENT_TYPE;
        // A log entry type should alway be defined, this may happen when a new
        // LogType is introduced in StringLogger and EVENT_TYPES did define an
        // entry type. Code coverage will only get 1 of 2 possible branches.
        if( EVENT_TYPES.ContainsKey(type) )
            {
            event_type = EVENT_TYPES[type];
            }

        EventLog.WriteEntry(m_source, message, event_type);
        }
    }
}
