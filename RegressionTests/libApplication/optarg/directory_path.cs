/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using RegressionTests.Generated;

using libApplication.app;
using libApplication.arg;
using libApplication.optarg;

namespace RegressionTests.libApplication.optarg
{
/** @ingroup REF_Testing REF_DirectoryPath

    @class   DirectoryPath_test

    @brief   Unit test for @ref REF_DirectoryPath class.
*/
[TestClass]
//[Ignore]
public class DirectoryPath_test
    {
    /** @test Test to make sure that @ref REF_DirectoryPath defaults are properly set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction_default()
        {
        var directory_path = new DirectoryPath();
        Assert.IsTrue(directory_path.MustExist, "MustExist property must default to true value.");
        Assert.AreEqual(string.Empty, directory_path.Path, "Path property must default to empty string value.");
        Assert.IsFalse(directory_path.PathExists, "PathExists property must be false after construction.");
        }

    /** @test Test to make sure that @ref REF_DirectoryPath defaults are properly set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction_false()
        {
        var directory_path = new DirectoryPath(false);
        Assert.IsFalse(directory_path.MustExist, "MustExist property must be false when set in constructor.");
        Assert.AreEqual(string.Empty, directory_path.Path, "Path property must default to empty string value.");
        Assert.IsFalse(directory_path.PathExists, "PathExists property must be false after construction.");
        }

    /** @test Test to make sure that @ref REF_DirectoryPath properly set for a valid directory.
    */
    [TestMethod]
    //[Ignore]
    public void update_valid()
        {
        var directory_path = new DirectoryPath(false);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  directory_path, null, "xxx dummy.");
        directory_path.init(arg_entry);
        directory_path.parse(SolutionInfo.ProjectDir.FullName);
        Assert.IsFalse(directory_path.MustExist, "MustExist property must have false value.");
        Assert.AreEqual(SolutionInfo.ProjectDir.FullName, directory_path.Path, "Path property must match configured value.");
        Assert.IsTrue(directory_path.PathExists, "PathExists property must be true for an existing path.");
        }

    /** @test Test to make sure that @ref REF_DirectoryPath properly set for an invalid directory.
    */
    [TestMethod]
    //[Ignore]
    public void update_invalid()
        {
        var directory_path = new DirectoryPath(false);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  directory_path, null, "xxx dummy.");
        directory_path.init(arg_entry);

        var not_exiting_path = SolutionInfo.ProjectDir.FullName + @"not_exiting";
        directory_path.parse(not_exiting_path);
        Assert.IsFalse(directory_path.MustExist, "MustExist property must have false value.");
        Assert.AreEqual(not_exiting_path, directory_path.Path, "Path property must match configured value.");
        Assert.IsFalse(directory_path.PathExists, "PathExists property must be false for not existing path values.");
        }

    /** @test Test to make sure that @ref REF_DirectoryPath update throws an exception when exiting path is mandatory.
    */
    [TestMethod]
    [ExpectedException(typeof(DirectoryNotFoundException), "DirectoryNotFoundException expected when calling parse for mandatory path with not existing path value.")]
    //[Ignore]
    public void update_exception()
        {
        var directory_path = new DirectoryPath(true);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  directory_path, null, "xxx dummy.");
        directory_path.init(arg_entry);

        var not_exiting_path = SolutionInfo.ProjectDir.FullName + @"not_exiting";
        directory_path.parse(not_exiting_path);
        }
    }
}
