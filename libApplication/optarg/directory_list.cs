/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_DirectoryList optarg.DirectoryList
    @{
    @details  Parses multiple path option arguments.

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using libApplication.arg;
using libApplication.toolkit;

namespace libApplication.optarg
{
/** @ingroup REF_DirectoryList

    @class   DirectoryList

    @details 
*/
public class DirectoryList
    :   Argument
    {
    public DirectoryList( bool must_exist = true )
        {
        MustExist = must_exist;
        }

    private readonly List<DirectoryInfo> m_directory_list = new();

    public bool MustExist
        {
        get;
        } = true;

    public IReadOnlyList<DirectoryInfo> PathList
        {
        get
            {
            return m_directory_list;
            }
        }

    private void add( DirectoryInfo directory_info )
        {
        foreach( var directory_info_entry in m_directory_list )
            {
            if( directory_info_entry.FullName == directory_info.FullName )
                {
                return;
                }
            }
        m_directory_list.Add(directory_info);
        }

    /** @details   Parses the string representation of path value.
    */
    protected override void update()
        {
        var directory_info = new DirectoryInfo(OptionStr);
        if( MustExist && !(directory_info.Exists) )
            {
            throw new DirectoryNotFoundException(Texting.get_string(Properties.language.DIRECTORY_NOT_FOUND, OptionStr));
            }

        add(directory_info);
        }
    }
}
