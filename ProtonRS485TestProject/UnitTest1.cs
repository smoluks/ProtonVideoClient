using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtonRS485Client.PackageCreate;

namespace ProtonRS485TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            PackageStateDispatcher stateMachine = new PackageStateDispatcher(uart, new ProtonRS485Client.PackageDataDispatcher(), new PackageConnectDispatcher());
        }
    }
}
