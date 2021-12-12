/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2021

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using RegressionTests.Generated;

using libApplication.app;
using libApplication.io;

namespace RegressionTests.libApplication.app
{
/** @ingroup REF_Testing REF_AsmLicense

    @class   AsmLicense_test

    @brief   Unit test for @ref REF_AsmLicense class.
*/
[TestClass]
//[Ignore]
public class AsmLicense_test
    {
    /** @test Test construction of @ref REF_AsmLicense instance.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var _ = new AsmLicense();
        }

    private class AsmVerify
        :   AsmInfo
        {
        public AsmVerify()
            :   base(".Generated.License")
            {
            }

        private static readonly List<string> the_license_fields = new()
            {
            "ShortText",
            "LongText",
            };

        protected override void log_basic( FieldInfo[] field_info_list )
            {
            var results = init_field_name_map(field_info_list, the_license_fields);

            Assert.AreEqual(License.ShortText, results["ShortText"], "License.ShortText did not match expected value.");
            Assert.AreEqual(License.LongText,  results["LongText"],  "License.LongText did not match expected value.");
            }
        }

    /** @test Test the returned property values match.
    */
    [TestMethod]
    //[Ignore]
    public void value_check()
        {
        var assembly   = Assembly.Load("RegressionTests");
        var asm_verify = new AsmVerify();
        var _ = AsmInfo_test.usage_output(asm_verify, assembly, 0);
        }
    }
}
