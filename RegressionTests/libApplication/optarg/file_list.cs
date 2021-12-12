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
/** @ingroup REF_Testing REF_FileList

    @class   FileList_test

    @brief   Unit test for @ref REF_FileList class.
*/
[TestClass]
//[Ignore]
public class FileList_test
    {
    /** @test Test to make sure that @ref REF_FileList defaults are properly set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction_default()
        {
        var file_list = new FileList();
        Assert.IsTrue(file_list.MustExist, "MustExist property must default to true value.");
        Assert.AreEqual(0, file_list.FilenameList.Count, "FilenameList property Count must default 0 for an empty list.");
        Assert.IsNull(file_list.Name, "Name property must be string null value after construction.");
        }

    /** @test Test to make sure that @ref REF_FileList defaults are properly set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction_false()
        {
        var file_list = new FileList(false);
        Assert.IsNull(file_list.Name, "Name property must be string null value after construction.");
        Assert.IsFalse(file_list.MustExist, "MustExist property must be false when set in constructor.");
        Assert.AreEqual(0, file_list.FilenameList.Count, "FilenameList property Count must default 0 for an empty list.");
        }

    private static readonly string EXISTING_FILE     = SolutionInfo.SolutiontDir.FullName + @"README.md";
    private static readonly string NOT_EXISTING_FILE = SolutionInfo.SolutiontDir.FullName + @"not_exiting.md";

    private static void check_valid( FileList file_list )
        {
        Assert.AreEqual("--xxx", file_list.Name, "Name property must be 'xxx'.");
        Assert.IsFalse(file_list.MustExist, "MustExist property must have false value.");
        Assert.AreEqual(1, file_list.FilenameList.Count, "FilenameList property Count must be 1 for list after adding an existing file.");
        Assert.AreEqual(EXISTING_FILE, file_list.FilenameList[0].FullName, "FilenameList[0] property must match configured value.");
        Assert.IsTrue(file_list.FilenameList[0].Exists, "Exists property for FilenameList[0] must be true for an existing file.");
        }

    /** @test Test to make sure that @ref REF_FileList properly set for a valid file.
    */
    [TestMethod]
    //[Ignore]
    public void update_valid()
        {
        var file_list = new FileList(false);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  file_list, null, "xxx dummy.");
        file_list.init(arg_entry);
        file_list.parse(EXISTING_FILE);
        check_valid(file_list);
        }

    /** @test Test to make sure that @ref REF_FileList properly set for a valid file and only added once.
    */
    [TestMethod]
    //[Ignore]
    public void update_valid_multiple_existing()
        {
        var file_list = new FileList(false);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  file_list, null, "xxx dummy.");
        file_list.init(arg_entry);
        file_list.parse(EXISTING_FILE);
        check_valid(file_list);

        file_list.parse(EXISTING_FILE);
        check_valid(file_list);
        }

    /** @test Test to make sure that @ref REF_FileList is properly set for files added.
    */
    [TestMethod]
    //[Ignore]
    public void update_valid_multiple_mixed()
        {
        var file_list = new FileList(false);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  file_list, null, "xxx dummy.");
        file_list.init(arg_entry);
        file_list.parse(EXISTING_FILE);
        check_valid(file_list);

        file_list.parse(EXISTING_FILE);
        check_valid(file_list);

        var not_exiting_file = NOT_EXISTING_FILE;
        file_list.parse(not_exiting_file);
        Assert.AreEqual("--xxx", file_list.Name, "Name property must be 'xxx'.");
        Assert.IsFalse(file_list.MustExist, "MustExist property must have false value.");
        Assert.AreEqual(2, file_list.FilenameList.Count, "FilenameList property Count must be 2 for list after adding not existing file.");
        Assert.AreEqual(EXISTING_FILE, file_list.FilenameList[0].FullName, "FilenameList[0] property must match configured value.");
        Assert.AreEqual(not_exiting_file, file_list.FilenameList[1].FullName, "FilenameList[1] property must match configured value.");
        Assert.IsTrue(file_list.FilenameList[0].Exists, "Exists property for FilenameList[0] must be true for an existing file.");
        Assert.IsFalse(file_list.FilenameList[1].Exists, "Exists property for FilenameList[1] must be false for an existing file.");
        }

    /** @test Test to make sure that @ref REF_FileList properly set for an invalid file.
    */
    [TestMethod]
    //[Ignore]
    public void update_invalid()
        {
        var file_list = new FileList(false);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  file_list, null, "xxx dummy.");
        file_list.init(arg_entry);
        file_list.parse(NOT_EXISTING_FILE);
        Assert.IsFalse(file_list.MustExist, "MustExist property must have false value.");
        Assert.AreEqual(1, file_list.FilenameList.Count, "FilenameList property Count must be 1 for list after adding an existing file.");
        Assert.AreEqual(NOT_EXISTING_FILE, file_list.FilenameList[0].FullName, "FilenameList[0] property must match configured value.");
        Assert.AreEqual("--xxx", file_list.Name, "Name property must be 'xxx'.");
        Assert.IsFalse(file_list.FilenameList[0].Exists, "Exists property for FilenameList[0] must be false for an existing file.");
        }

    /** @test Test to make sure that @ref REF_FileList update throws an exception when exiting file is mandatory.
    */
    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException), "FileNotFoundException expected when calling parse for mandatory file with not existing file value.")]
    //[Ignore]
    public void update_exception()
        {
        var file_list = new FileList(true);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  file_list, null, "xxx dummy.");
        file_list.init(arg_entry);

        var not_exiting_file = NOT_EXISTING_FILE;
        file_list.parse(not_exiting_file);
        }
    }
}
