/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_FileList optarg.FileList
    @{
    @details  Parses multiple file path option arguments.

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
/** @ingroup REF_FileList

    @class   FileList

    @details 
*/
public class FileList
    :   Argument
    {
    public FileList( bool must_exist = true )
        {
        MustExist = must_exist;
        }

    private readonly List<FileInfo> m_filename_list = new List<FileInfo>();

    public bool MustExist
        {
        get;
        } = true;

    public IReadOnlyList<FileInfo> FilenameList
        {
        get
            {
            return m_filename_list;
            }
        }

    private void add( FileInfo file_info )
        {
        foreach( var filename_entry in m_filename_list )
            {
            if( filename_entry.FullName == file_info.FullName )
                {
                return;
                }
            }
        m_filename_list.Add(file_info);
        }

    /** @details   Parses the string representation of file path value.
    */
    protected override void update()
        {
        var file_info = new FileInfo(OptionStr);
        if( MustExist && !(file_info.Exists) )
            {
            throw new FileNotFoundException(Texting.get_string(Properties.language.FILE_NOT_FOUND, OptionStr));
            }
        add(file_info);
        }
    }
}
