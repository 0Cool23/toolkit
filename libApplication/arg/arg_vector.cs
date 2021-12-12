/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_ArgVector arg.ArgVector
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
/** @ingroup REF_ArgVector

    @class   ArgVector

    @brief
*/
internal class ArgVector
    {
    private readonly List<string> m_list = new List<string>();

    private int m_index  = 0;
    
    /** @details Current position index within argument string.
    */
    public int str_pos
        {
        get;
        set;
        } = 0;

    /** @details Flag indicates that end of string is reached for current argument string.
    */
    public bool end_of_string
        {
        get{
            return (str_pos >= m_list[m_index].Length);
            }
        }
        
    /** @details Flag indicates that end of list is reached in this argument list. 
    */
    public bool end_of_list
        {
        get
            {
            return (m_index >= m_list.Count);
            }
        }

    /** @details Property is true if the current arguement int the list has a successor argument.
    */
    public bool has_successor
        {
        get
            {
            return ((m_index + 1) < m_list.Count);
            }
        }

    /** @details Property contains the current argument.
    */
    public string current_arg
        {
        get
            {
            return m_list[m_index];
            }
        }

    /** @details Property contains the succeeding argument.
    */
    public string successor_arg
        {
        get
            {
            return m_list[m_index + 1];
            }
        }

    /** @details Increase the index and move to the next argument in the list.
    */
    public void inc_index()
        {
        ++m_index;
        }

    /** @details   Add another element to the argument list

        @param[in] element
    */
    public void add( string element )
        {
        m_list.Add(element);
        }
    }
}
