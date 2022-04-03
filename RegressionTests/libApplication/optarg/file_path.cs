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
/** @ingroup REF_Testing REF_FilePath

    @class   FilePath_test

    @brief   Unit test for @ref REF_FilePath class.
*/
[TestClass]
//[Ignore]
public class FilePath_test
    {
    /** @test Test to make sure that @ref REF_FilePath defaults are properly set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction_default()
        {
        var file_path = new FilePath();
        Assert.IsTrue(file_path.MustExist, "MustExist property must default to true value.");
        Assert.IsNull(file_path.Info, "Info property must default to null value.");
        Assert.AreEqual(string.Empty, file_path.FileName, "Filename property must default to empty string value.");
        Assert.IsFalse(file_path.FileExists, "FileExists property must be false after construction.");
        }

    private static readonly string EXISTING_FILE     = SolutionInfo.SolutiontDir.FullName + @"README.md";
    private static readonly string NOT_EXISTING_FILE = SolutionInfo.SolutiontDir.FullName + @"not_exiting.md";

    /** @test Test to make sure that @ref REF_FilePath defaults are properly set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction_false()
        {
        var file_path = new FilePath(false);
        Assert.IsFalse(file_path.MustExist, "MustExist property must be false when set in constructor.");
        Assert.IsNull(file_path.Info, "Info property must default to null value.");
        Assert.AreEqual(string.Empty, file_path.FileName, "Filename property must default to empty string value.");
        Assert.IsFalse(file_path.FileExists, "FileExists property must be false after construction.");
        }

    /** @test Test to make sure that @ref REF_FilePath properly set for a valid file.
    */
    [TestMethod]
    //[Ignore]
    public void update_valid()
        {
        var file_path = new FilePath(false);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  file_path, null, "xxx dummy.");
        file_path.init(arg_entry);
        file_path.parse(EXISTING_FILE);
        Assert.IsFalse(file_path.MustExist, "MustExist property must have false value.");
        Assert.IsNotNull(file_path.Info, "Info property must not default to null value.");
        Assert.IsInstanceOfType(file_path.Info, typeof(FileInfo), "Info property type does not match expected value.");
        Assert.AreEqual(EXISTING_FILE, file_path.FileName, "Filename property must match configured value.");
        Assert.IsTrue(file_path.FileExists, "FileExists property must be true for an existing file.");
        }

    /** @test Test to make sure that @ref REF_FilePath properly set for an invalid file.
    */
    [TestMethod]
    //[Ignore]
    public void update_invalid()
        {
        var file_path = new FilePath(false);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  file_path, null, "xxx dummy.");
        file_path.init(arg_entry);

        file_path.parse(NOT_EXISTING_FILE);
        Assert.IsFalse(file_path.MustExist, "MustExist property must have false value.");
        Assert.IsNotNull(file_path.Info, "Info property must not default to null value.");
        Assert.IsInstanceOfType(file_path.Info, typeof(FileInfo), "Info property type does not match expected value.");
        Assert.AreEqual(NOT_EXISTING_FILE, file_path.FileName, "Filename property must match configured value.");
        Assert.IsFalse(file_path.FileExists, "FileExists property must be false for not existing file values.");
        }

    /** @test Test to make sure that @ref REF_FilePath update throws an exception when exiting file is mandatory.
    */
    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException), "FileNotFoundException expected when calling parse for mandatory files with not existing file value.")]
    //[Ignore]
    public void update_exception()
        {
        var file_path = new FilePath(true);
        var arg_entry  = new ArgEntry('x', "xxx", ArgType.REQUIRED_ARGUMENT, false,  file_path, null, "xxx dummy.");
        file_path.init(arg_entry);

        file_path.parse(NOT_EXISTING_FILE);
        }
    }
}
