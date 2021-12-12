/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/
using Microsoft.VisualStudio.TestTools.UnitTesting;

using libApplication.arg;

namespace RegressionTests.libApplication.App
{
/** @ingroup REF_Testing REF_ArgBase

    @class ArgBase_Test

    @brief Unit test for @ref REF_ArgBase class.
*/
[TestClass]
//[Ignore]
public class ArgBase_test
    {
    private class ArgClass
        :   ArgBase
        {
        public ArgClass()
            :   base(null)
            {
            }

        public override void parse( ArgVector argv )
            {
            }
        }

    /** @test Test abstract base class construction.
    */
    [TestMethod()]
    public void construction()
        {
        var _ = new ArgClass();
        }

    /** @test Test abstract base class parse method.
    */
    [TestMethod()]
    public void parse()
        {
        var arg_class = new ArgClass();
        arg_class.parse(null);
        }
    }
}
