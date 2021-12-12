/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_FilePath optarg.FilePath
    @{
    @details  Parses file path option arguments.

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
/** @ingroup REF_FilePath

    @class   FilePath

    @details 
*/
public class FilePath
    :   Argument
    {
    public FilePath( bool must_exist = true )
        {
        MustExist = must_exist;
        }

    private FileInfo m_file_info = null;

    public bool MustExist
        {
        get;
        } = true;

    public string Filename
        {
        get
            {
            return m_file_info?.FullName ?? string.Empty;
            }
        }

    public bool FileExists
        {
        get
            {
            return m_file_info?.Exists ?? false;
            }
        }

    /** @details   Parses the string representation of path value.
    */
    protected override void update()
        {
        var file_info = new FileInfo(OptionStr);
        if( MustExist && !(file_info.Exists) )
            {
            throw new FileNotFoundException(Texting.get_string(Properties.language.FILE_NOT_FOUND, OptionStr));
            }
        m_file_info = file_info;
        }
    }
}
