/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/
using Microsoft.VisualStudio.TestTools.UnitTesting;

using libApplication.arg;

namespace RegressionTests.libApplication.arg
{
/** @ingroup REF_Testing REF_ArgStack
  
    @class   ArgStack_test

    @brief   Unit test for @ref REF_ArgStack class.  
*/
[TestClass]
//[Ignore]
public class ArgStack_test
    {
    /** @test Test construction of ArgStack class.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var _ = new ArgStack();
        }

    /** @test Test standard usage of ArgStack class.
    */
    [TestMethod]
    //[Ignore]
    public void usage()
        {
        var stack = new ArgStack();

        var element = stack.get_next_argument();
        Assert.AreEqual(null, element, "stack must return null for empty stack.");

        stack.add("xyz");
        element = stack.get_next_argument();
        Assert.AreEqual("xyz", element, "stack must return element 'xyz' from stack.");
        element = stack.get_next_argument();
        Assert.AreEqual(null, element, "stack must return null at end of stack.");
        }
    }
}
