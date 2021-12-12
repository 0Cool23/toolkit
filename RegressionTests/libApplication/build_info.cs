/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;

using libApplication.Generated;

namespace RegressionTests.libApplication.Generated
{
/** @class BuildInfo_test

    @details Unit test for @ref REF_BuildInfo_libApplication class.

    @ingroup GRP_Testing
*/
[TestClass]
//[Ignore]
public class BuildInfo_test
    {
    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void lib_version()
        {
        Assert.IsTrue(BuildInfo.AsmVersion.ToString().StartsWith("1.0.0."));
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void api_version()
        {
        Assert.IsTrue(BuildInfo.ApiVersion.ToString().StartsWith("0.0.0."));
        }
    }
}