/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2021

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_Uuid toolkit.Uuid
    @{
    @details

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace libApplication.toolkit
{
/** @ingroup REF_Uuid

    @class   Uuid

    @brief   
*/
public static class Uuid
    {
    /** @details   Create a Guid from class type name including namespace. The
                   Guid id is computed from the md5 hash value of the
                   class type name. The md5 hash value has exactly 16 bytes
                   which is ideal for Guid creation.

        @param[in] class_type Type of the class/interface to compute Guid.

        @return    Guid from md5 hash value.
    */
    public static Guid create_uuid( Type class_type )
        {
        var md5_sum = MD5.Create();
        md5_sum.ComputeHash(Texting.to_byte_array(class_type.FullName));
        var guid = new Guid(md5_sum.Hash);
        return guid;
        }
    }
}
