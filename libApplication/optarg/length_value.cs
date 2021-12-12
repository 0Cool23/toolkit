/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_LengthValue optarg.LengthValue
    @{
    @details  Length entity argument parsing.

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace libApplication.optarg
{
/** @ingroup REF_LengthValue

    @class   LengthValue

    @brief   Length entity argument parsing.
*/
public class LengthValue
    :   SiValue
    {
    public LengthValue()
        :   this(double.NaN)
        {
        }

    public LengthValue( double upper_limit )
        :   this(double.NaN, upper_limit)
        {
        }

    public LengthValue( double v1, double v2 )
        :   base("m", v1, v2)
        {
        }
    }
}
