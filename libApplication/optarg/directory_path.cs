/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_DirectoryPath optarg.DirectoryPath
    @{
    @details  Parses path option arguments.

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
/** @ingroup REF_DirectoryPath

    @class   DirectoryPath

    @details 
*/
public class DirectoryPath
    :   Argument
    {
    public DirectoryPath( bool must_exist = true )
        {
        MustExist = must_exist;
        }

    private DirectoryInfo m_directory_info = null;

    public bool MustExist
        {
        get;
        } = true;

    public DirectoryInfo Info
        {
        get => m_directory_info;
        }

    public string PathName
        {
        get => m_directory_info?.FullName ?? string.Empty;
        }

    public bool PathExists
        {
        get
            {
            return m_directory_info?.Exists ?? false;
            }
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
        m_directory_info = directory_info;
        }
    }
}
