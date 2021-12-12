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
/** @ingroup REF_Testing REF_AsmBuildInfo

    @class   AsmBuildInfo_test

    @brief   Unit test for @ref REF_AsmBuildInfo class.
*/
[TestClass]
//[Ignore]
public class AsmBuildInfo_test
    {
    /** @test Test construction of @ref REF_AsmBuildInfo instance.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var _ = new AsmBuildInfo();
        }

    /** @test Test usage of @ref REF_AsmBuildInfo instance with basic console output.
    */
    [TestMethod]
    //[Ignore]
    public void usage_basic()
        {
        var assembly = Assembly.Load("libApplication");
        var asm_build_info = new AsmBuildInfo();
        var _ = AsmInfo_test.usage_output(asm_build_info, assembly, 0);
        }

    /** @test Test usage of @ref REF_AsmBuildInfo instance with extended console output.
    */
    [TestMethod]
    //[Ignore]
    public void usage_extended()
        {
        var assembly = Assembly.Load("libApplication");
        var asm_build_info = new AsmBuildInfo();
        var _ = AsmInfo_test.usage_output(asm_build_info, assembly, 1);
        }

    private class AsmVerify
        :   AsmInfo
        {
        public AsmVerify()
            :   base(".Generated.BuildInfo")
            {
            }

        private static readonly List<string> the_build_info_fields = new()
            {
            "ProjectName",

            "BuildHost",
            "BuildType",
            "BuildDate",

            "BranchName",
            "CommitHash",
            "CommitDate",
            "IsClean",
            "InSync",

            "AsmVersion",
            "ApiVersion",
            };

        protected override void log_basic( FieldInfo[] field_info_list )
            {
            var results = init_field_name_map(field_info_list, the_build_info_fields);

            Assert.AreEqual(BuildInfo.ProjectName, results["ProjectName"], "BuildInfo.ProjectName did not match expected value.");

            Assert.AreEqual(BuildInfo.BuildHost,             results["BuildHost"], "BuildInfo.BuildHost did not match expected value.");
            Assert.AreEqual(BuildInfo.BuildType,             results["BuildType"], "BuildInfo.BuildType did not match expected value.");
            Assert.AreEqual(BuildInfo.BuildDate.ToString(),  results["BuildDate"], "BuildInfo.BuildDate did not match expected value.");

            Assert.AreEqual(BuildInfo.BranchName,            results["BranchName"], "BuildInfo.BranchName did not match expected value.");
            Assert.AreEqual(BuildInfo.CommitHash,            results["CommitHash"], "BuildInfo.CommitHash did not match expected value.");
            Assert.AreEqual(BuildInfo.CommitDate.ToString(), results["CommitDate"], "BuildInfo.CommitDate did not match expected value.");
            Assert.AreEqual(BuildInfo.IsClean,    bool.Parse(results["IsClean"]),   "BuildInfo.IsClean did not match expected value.");
            Assert.AreEqual(BuildInfo.InSync,     bool.Parse(results["InSync"]),    "BuildInfo.InSync did not match expected value.");

            Assert.AreEqual(BuildInfo.AsmVersion.ToString(), results["AsmVersion"], "BuildInfo.AsmVersion did not match expected value.");
            Assert.AreEqual(BuildInfo.ApiVersion.ToString(), results["ApiVersion"], "BuildInfo.ApiVersion did not match expected value.");
            }
        }

    /** @test Test the returned property values match.
    */
    [TestMethod]
    //[Ignore]
    public void value_check()
        {
        var assembly = Assembly.Load("RegressionTests");
        var asm_verify = new AsmVerify();
        var _ = AsmInfo_test.usage_output(asm_verify, assembly, 0);
        }
    }
}
