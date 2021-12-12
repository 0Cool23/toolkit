/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;

using libApplication.app;
using libApplication.io;

namespace RegressionTests.libApplication.App
{
/** @ingroup REF_Testing REF_AppConfig
 
    @class AppConfig_test

    @brief Unit test for @ref REF_AppConfig class.
*/
[TestClass]
//[Ignore]
public class AppConfig_test
    {
    /** @test Test to make sure that @ref REF_AppConfig defaults are properly
              set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var application_config = new AppConfig();

        Assert.AreEqual(string.Empty, application_config.description);
        Assert.AreEqual(false, application_config.forward_exceptions);
        Assert.AreEqual(null, application_config.logger);
        }

    /** @test Test to make sure that @ref REF_AppConfig property description
              is never null.
    */
    [TestMethod]
    //[Ignore]
    public void description()
        {
        var application_config = new AppConfig
            {
            description = null
            };

        Assert.AreEqual(string.Empty, application_config.description, "description should be '' after setting null string value.");

        application_config.description = "Test description";
        Assert.AreEqual("Test description", application_config.description, "description did not match after calling setter.");

        application_config.description = string.Empty;
        Assert.AreEqual(string.Empty, application_config.description, "description should be '' after setting empty string value.");

        application_config.forward_exceptions = true;
        Assert.IsTrue(application_config.forward_exceptions, "forward_exceptions should be true after setting value.");
        }

    /** @test Test to make sure that @ref REF_AppConfig property logger
              works by setting and resetting.
    */
    [TestMethod]
    //[Ignore]
    public void set_logger()
        {
        var application_config = new AppConfig();


        Assert.IsNull(application_config.logger, "logger property should have null value after construction.");
        application_config.logger = new StringLogger();;
        Assert.IsInstanceOfType(application_config.logger, typeof(iLogger), "logger property must be an iLogger interface type.");
        Assert.IsNotNull(application_config.logger, "logger property may not have null value after assignment.");
        }
    }
}
