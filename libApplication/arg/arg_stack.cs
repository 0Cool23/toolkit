/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_ArgStack arg.ArgStack
    @{
    @details

    @ingroup  REF_libApplication
    @}
*/

using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RegressionTests")]
namespace libApplication.arg
{
/** @ingroup REF_ArgStack

    @class   ArgStack

    @brief   Class to manage an argument list as a stack.
*/
internal class ArgStack
    {
    private readonly List<string> m_list = new List<string>();

    private int m_index = 0;

    /** @details

        @param[in] element
    */
    public void add( string element )
        {
        m_list.Add(element);
        }

    /** @details

        @return
    */
    public string get_next_argument()
        {
        if( m_index < m_list.Count )
            {
            return m_list[m_index++];
            }
        return null;
        }
    }
}
