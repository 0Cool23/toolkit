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
/** @ingroup REF_Testing REF_DirectoryList

    @class   DirectoryList_test

    @brief   Unit test for @ref REF_DirectoryList class.
*/
[TestClass]
//[Ignore]
public class DirectoryList_test
    {
    /** @test Test to make sure that @ref REF_DirectoryList defaults are properly set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction_default()
        {
        var directory_list = new DirectoryList();
        Assert.IsTrue(directory_list.MustExist, "MustExist property must default to true value.");
        Assert.AreEqual(0, directory_list.PathList.Count, "PathList property Count must default 0 for an empty list.");
        Assert.IsNull(directory_list.Name, "Name property must be string null value after construction.");
        }

    /** @test Test to make sure that @ref REF_DirectoryList defaults are properly set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction_false()
        {
        var directory_list = new DirectoryList(false);
        Assert.IsNull(directory_list.Name, "Name property must be string null value after construction.");
        Assert.IsFalse(directory_list.MustExist, "MustExist property must be false when set in constructor.");
        Assert.AreEqual(0, directory_list.PathList.Count, "PathList property Count must default 0 for an empty list.");
        }

    private static void check_valid( DirectoryList directory_list )
        {
        Assert.AreEqual("--xxx", directory_list.Name, "Name property must be 'xxx'.");
        Assert.IsFalse(directory_list.MustExist, "MustExist property must have false value.");
        Assert.AreEqual(1, directory_list.PathList.Count, "PathList property Count must be 1 for list after adding an existing path.");
        Assert.AreEqual(SolutionInfo.ProjectDir.FullName, directory_list.PathList[0].FullName, "PathList[0] property must match configured value.");
        Assert.IsTrue(directory_list.PathList[0].Exists, "Exists property for PathList[0] must be true for an existing path.");
        }

    /** @test Test to make sure that @ref REF_DirectoryList properly set for a valid directory.
    */
    [TestMethod]
    //[Ignore]
    public void update_valid()
        {
        var directory_list = new DirectoryList(false);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  directory_list, null, "xxx dummy.");
        directory_list.init(arg_entry);
        directory_list.parse(SolutionInfo.ProjectDir.FullName);
        check_valid(directory_list);
        }

    /** @test Test to make sure that @ref REF_DirectoryList properly set for a valid directory and only added once.
    */
    [TestMethod]
    //[Ignore]
    public void update_valid_multiple_existing()
        {
        var directory_list = new DirectoryList(false);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  directory_list, null, "xxx dummy.");
        directory_list.init(arg_entry);
        directory_list.parse(SolutionInfo.ProjectDir.FullName);
        check_valid(directory_list);

        directory_list.parse(SolutionInfo.ProjectDir.FullName);
        check_valid(directory_list);
        }

    /** @test Test to make sure that @ref REF_DirectoryList is properly set for directories added.
    */
    [TestMethod]
    //[Ignore]
    public void update_valid_multiple_mixed()
        {
        var directory_list = new DirectoryList(false);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  directory_list, null, "xxx dummy.");
        directory_list.init(arg_entry);
        directory_list.parse(SolutionInfo.ProjectDir.FullName);
        check_valid(directory_list);

        directory_list.parse(SolutionInfo.ProjectDir.FullName);
        check_valid(directory_list);

        var not_exiting_path = SolutionInfo.ProjectDir.FullName + @"not_exiting";
        directory_list.parse(not_exiting_path);
        Assert.AreEqual("--xxx", directory_list.Name, "Name property must be 'xxx'.");
        Assert.IsFalse(directory_list.MustExist, "MustExist property must have false value.");
        Assert.AreEqual(2, directory_list.PathList.Count, "PathList property Count must be 2 for list after adding not existing directory.");
        Assert.AreEqual(SolutionInfo.ProjectDir.FullName, directory_list.PathList[0].FullName, "PathList[0] property must match configured value.");
        Assert.AreEqual(not_exiting_path, directory_list.PathList[1].FullName, "PathList[1] property must match configured value.");
        Assert.IsTrue(directory_list.PathList[0].Exists, "Exists property for PathList[0] must be true for an existing path.");
        Assert.IsFalse(directory_list.PathList[1].Exists, "Exists property for PathList[1] must be false for an existing path.");
        }

    /** @test Test to make sure that @ref REF_DirectoryList properly set for an invalid directory.
    */
    [TestMethod]
    //[Ignore]
    public void update_invalid()
        {
        var directory_list = new DirectoryList(false);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  directory_list, null, "xxx dummy.");
        directory_list.init(arg_entry);
        var not_exiting_path = SolutionInfo.ProjectDir.FullName + @"not_exiting";
        directory_list.parse(not_exiting_path);
        Assert.IsFalse(directory_list.MustExist, "MustExist property must have false value.");
        Assert.AreEqual(1, directory_list.PathList.Count, "PathList property Count must be 1 for list after adding an existing path.");
        Assert.AreEqual(not_exiting_path, directory_list.PathList[0].FullName, "PathList[0] property must match configured value.");
        Assert.AreEqual("--xxx", directory_list.Name, "Name property must be 'xxx'.");
        Assert.IsFalse(directory_list.PathList[0].Exists, "Exists property for PathList[0] must be false for an existing path.");
        }

    /** @test Test to make sure that @ref REF_DirectoryList update throws an exception when exiting path is mandatory.
    */
    [TestMethod]
    [ExpectedException(typeof(DirectoryNotFoundException), "DirectoryNotFoundException expected when calling parse for mandatory path with not existing path value.")]
    //[Ignore]
    public void update_exception()
        {
        var directory_list = new DirectoryList(true);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  directory_list, null, "xxx dummy.");
        directory_list.init(arg_entry);

        var not_exiting_path = SolutionInfo.ProjectDir.FullName + @"not_exiting";
        directory_list.parse(not_exiting_path);
        }
    }
}
