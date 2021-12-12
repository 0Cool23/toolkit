/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_AppConfig app.AppConfig
    @{
    @details  Container with configuration values for @ref REF_Application class.

    @ingroup  REF_libApplication
    @}
*/

using libApplication.io;

namespace libApplication.app
{
/** @ingroup REF_AppConfig

    @class   AppConfig

    @brief   Container managing @ref REF_Application class configuration values.
*/
public class AppConfig
    {
    private string m_description = string.Empty;

    /** @brief   Phrase with short description of what the application does.

        @details Property is a string value which may contain a short phrase
                 which describes what the application is doing. This phrase
                 is appended to the application info string if present. A null
                 value is replaced with an empty string.
    */
    public string description
        {
        get
            {
            return m_description;
            }
        set
            {
            m_description = value ?? string.Empty;
            }
        }

    /** @brief   Boolean property which defines exception handling.

        @details Property is a boolean value which is used to define the
                 exception handling for an application. The default value is
                 false. This means that the application will handle all
                 exceptions internally. If set to a true value, the caught
                 exceptions are forwarded.
    */
    public bool forward_exceptions
        {
        get;
        set;
        } = false;

    /** @brief   @ref REF_iLogger interface the application will output
                 messages on.

        @details Property takes an instance of an @ref REF_iLogger interface.
                 The default value is null. In this case there will be no
                 output. Otherwise the output is written using the attached
                 interface instance methods.
    */
    public iLogger logger
        {
        get;
        set;
        } = null;
    }
}
