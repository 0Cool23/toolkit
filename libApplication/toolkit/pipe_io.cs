/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2021

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_PipeIO toolkit.PipeIO
    @{
    @details

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace libApplication.toolkit
{
/** @ingroup REF_PipeIO

    @class   PipeIO

    @brief   Toolkit function class for named pipe handling.
*/
public static class PipeIO
    {
    /**
    */
    public static bool is_running( string pipe_name )
        {
        return Directory.GetFiles(@"\\.\pipe\").Contains(@"\\.\pipe\" + pipe_name);
        }
    }
}
