using ProtonRS485Client.PackageCreate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ProtonRS485UnitTestProject
{
    [TestClass]
    public class PackageStateDispatcherTest
    {
        [TestMethod]
        public void TestMethod1Async()
        {            
            FakeUart uart = new FakeUart();     
            PackageStateDispatcher stateDispatcher = new PackageStateDispatcher(uart, new ProtonRS485Client.PackageDataDispatcher(), new PackageConnectDispatcher());
            Task task = RunStateDispatcherAsync(stateDispatcher);
            //поиск и авторизация
            uart.SetDataIn(new byte[] { 0x78, 0x04, 0xCC, 0x00, 0xD3 });
            while (!uart.packetIsCompleted) { }
            Assert.IsTrue(CompareByteArray(uart.ReceivedPacket, new byte[] {120, 12, 204, 128, 0, 12, 38, 23, 0 , 1, 1, 1, 4}));
            //опрос
            uart.SetDataIn(new byte[] { 0xF8, 0x04, 0x00, 0x00, 0xF3 });
            while (!uart.packetIsCompleted) { }
            Assert.IsTrue(CompareByteArray(uart.ReceivedPacket, new byte[] { 248, 8, 1, 120, 0, 0, 0, 0, 152}));
        }

        async Task RunStateDispatcherAsync(PackageStateDispatcher stateDispatcher)
        {
            await stateDispatcher.CollectPacketsAsync();
        }


        /// <summary>
        /// Сравнените двух байтовых массивов на равенство содержимого
        /// </summary>
        /// <param name="a">первый массив</param>
        /// <param name="b">второй массив</param>
        /// <returns></returns>
        bool CompareByteArray(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            for (int i = 0; i > a.Length; i++)
                if (a[i] != b[i])
                    return false;
            return true;
        }
    }
}
