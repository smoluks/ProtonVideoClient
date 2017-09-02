using ProtonRS485Client.PackageCreate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProtonRS485UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async System.Threading.Tasks.Task TestMethod1Async()
        {
            FakeUart uart = new FakeUart();
            uart.SetDataIn(new byte[] { 0x78, 0x04, 0xCC, 0x00, 0xD3 });
            PackageStateDispatcher stateDispatcher = new PackageStateDispatcher(uart, new ProtonRS485Client.PackageDataDispatcher(), new PackageConnectDispatcher());
            await stateDispatcher.CollectPacketsAsync();
            while (!uart.packetIsComleted) { }
            Assert.AreEqual(13, uart.dataOut);
        }
    }
}
