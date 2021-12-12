/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_ArgType arg.ArgType
    @{
    @details

    @ingroup  REF_libApplication
    @}
*/

namespace libApplication.arg
{
/** @ingroup REF_ArgType

    @enum    ArgType

    @brief
*/
public enum ArgType
    :   int
    {
    NO_ARGUMENT,
    OPT_ARGUMENT,
    REQUIRED_ARGUMENT,
    }
}
