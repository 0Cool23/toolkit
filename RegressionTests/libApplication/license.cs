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
/** @class License_test

    @details Unit test for @ref REF_License_libApplication class.

    @ingroup GRP_Testing
*/
[TestClass]
//[Ignore]
public class License_test
    {
    /** @test
    */
    [TestMethod]
    public void short_text()
        {
        Assert.IsFalse(string.IsNullOrEmpty(License.ShortText), "ShortText property must not be null value or an empty string.");
        }

    /** @test
    */
    [TestMethod]
    public void long_text()
        {
        Assert.IsFalse(string.IsNullOrEmpty(License.LongText), "LongText property must not be null value or an empty string.");
        }
    }
}