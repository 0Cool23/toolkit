/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_ArgBase arg.ArgBase
    @{
    @details  Abstract base class for argument parsing.

    @ingroup  REF_libApplication
    @}
*/

using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RegressionTests")]
namespace libApplication.arg
{
/** @ingroup REF_ArgBase

    @class   ArgBase

    @brief   Abstract base class for argument parsing.
*/
internal abstract class ArgBase
    {
    /** @details List of @ref REF_ArgEntry objects.
    */
    protected List<ArgEntry> m_arg_table = null;

    /** @details Construct instance with a List of @ref REF_ArgEntry objects.

        @param[in] arg_table List of @ref REF_ArgEntry objects. Each @ref REF_ArgEntry
                             object is a possible argument for the application.
    */
    protected ArgBase( List<ArgEntry> arg_table )
        {
        m_arg_table = arg_table;
        }

    /** @details   Abstract method which parses the next value from
                   @ref REF_ArgVector.

        @param[in] argv
    */
    public abstract void parse( ArgVector argv );
    }
}
