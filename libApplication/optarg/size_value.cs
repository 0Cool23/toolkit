/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_SizeValue optarg.SizeValue
    @{
    @details  Size entity argument parsing.

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace libApplication.optarg
{
/** @ingroup REF_SizeValue

    @class   SizeValue

    @brief   Size entity argument parsing.
*/
public class SizeValue
    :   ItValue
    {
    public SizeValue()
        :   this(ulong.MaxValue)
        {
        }

    public SizeValue( ulong upper_limit )
        :   this(ulong.MinValue, upper_limit)
        {
        }

    public SizeValue( ulong v1, ulong v2 )
        :   base("B", v1, v2)
        {
        }
    }
}
