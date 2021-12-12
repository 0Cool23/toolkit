/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2021

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Threading;

using libApplication.toolkit;

namespace RegressionTests.libApplication.toolkit
{
/** @ingroup REF_Testing REF_PipeIO

    @class   PipeIO_test

    @brief   Unit test for @ref REF_PipeIO class.
*/
[TestClass]
//[Ignore]
public class PipeIO_test
    {
    private const string REGRESSION_TEST_PIPE_NAME = "RegressionTests.PandorasBox.Styx";

    private static NamedPipeServerStream create_named_pipe_server_stream()
        {
        if( RuntimeInformation.IsOSPlatform(OSPlatform.Windows) )
            {
            return new NamedPipeServerStream(REGRESSION_TEST_PIPE_NAME, PipeDirection.InOut, 1, PipeTransmissionMode.Message);
            }
        return new NamedPipeServerStream(REGRESSION_TEST_PIPE_NAME, PipeDirection.InOut, 1);
        }

    /** @test Test the named pipe is_running status works as intended.
    */
    [TestMethod]
    //[Ignore]
    public void is_running_status()
        {
        // server task
        Assert.IsFalse(PipeIO.is_running(REGRESSION_TEST_PIPE_NAME), "Named pipe '{0}' must not exist at test start.");
        using( var server_stream = create_named_pipe_server_stream() )
            {
            Assert.IsTrue(PipeIO.is_running(REGRESSION_TEST_PIPE_NAME), "Named pipe '{0}' exist after start.");
            }
        Assert.IsFalse(PipeIO.is_running(REGRESSION_TEST_PIPE_NAME), "Named pipe '{0}' must not exist after using.");
        }

    private const string REQUEST  = "hello";
    private const string RESPONSE = "world";

    private void client_task()
        {
        using var client_stream = new NamedPipeClientStream(REGRESSION_TEST_PIPE_NAME);
        var stream_reader = new StreamReader(client_stream);
        var stream_writer = new StreamWriter(client_stream);

        client_stream.Connect(1000 /* ms */);

        stream_writer.WriteLine(REQUEST);
        stream_writer.Flush();
        if( RuntimeInformation.IsOSPlatform(OSPlatform.Windows) )
            {
            client_stream.WaitForPipeDrain();
            }

        var response = stream_reader.ReadLine();
        Assert.AreEqual(RESPONSE, response, "Client did not receive expected response.");
        }

    /** @test Test named pipe server/client communication example.
    */
    [TestMethod]
    //[Ignore]
    public void communication_example()
        {
        Thread client_thread = null;
        try
            {
            client_thread = new Thread(new ThreadStart(client_task));

            using var server_stream = create_named_pipe_server_stream();
            client_thread.Start();

            server_stream.WaitForConnection();

            var stream_reader = new StreamReader(server_stream);
            var stream_writer = new StreamWriter(server_stream);

            var request = stream_reader.ReadLine();
            Assert.AreEqual(REQUEST, request, "Server did not receive expected request.");

            stream_writer.WriteLine(RESPONSE);
            stream_writer.Flush();
            if( RuntimeInformation.IsOSPlatform(OSPlatform.Windows) )
                {
                server_stream.WaitForPipeDrain();
                }
            }
        finally
            {
            client_thread?.Join();
            }
        }
    }
}
