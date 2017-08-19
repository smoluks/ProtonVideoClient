using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProtonRS485Client
{
    /// <summary>
    /// Класс сборки пакетов
    /// </summary>
    class PackageStateDispatcher
    {
        private readonly UartDispatcher _uart;
        private readonly PackageDataDispatcher _packageDataDispatcher;
        private readonly PackageConnectDispatcher _packageConnectDispatcher;
        private readonly PackageProcesser _packageProcesser;
        CancellationToken _breakToken;

        public PackageStateDispatcher(UartDispatcher uart, PackageDataDispatcher packageDataDispatcher, PackageConnectDispatcher packageConnectDispatcher, ObjectConfig objectConfig, ObjectState objectState, CancellationToken breakToken)
        {
            _uart = uart;
            _packageDataDispatcher = packageDataDispatcher;
            _packageConnectDispatcher = packageConnectDispatcher;
            //
            _packageProcesser = new PackageProcesser(objectConfig, objectState);
            //
            _breakToken = breakToken;
        }

        /// <summary>
        /// Сборка пакетов
        /// </summary>
        public async Task StartCollect()
        {
            LogDispatcher.Write("StartCollect");
            while (true)
            {
                //Адрес
                var addr = await _uart.ReadByteAsync(_breakToken);
                MessageBox.Show("ReadByteAsync return " + addr.ToString());
                if (_breakToken.IsCancellationRequested)
                    return;
                if (!_packageDataDispatcher.ProcessAddress(addr))
                    break;
                _packageConnectDispatcher.CorrectAddressReceived((addr & 0x80) == 0);
                //Длина
                var lenght = await _uart.ReadByteAsync(_breakToken);
                if (_breakToken.IsCancellationRequested)
                    return;
                if (!_packageDataDispatcher.ProcessFrameLength(lenght))
                    break;
                //Данные
                var data = await _uart.ReadAsync(--lenght, _breakToken);
                if (_breakToken.IsCancellationRequested)
                    return;
                if (!_packageDataDispatcher.ProcessPacket(data))
                    break;
                //отправить это на обработку
                var answer = _packageProcesser.ProcessCommand(_packageDataDispatcher.Packet);
                if (_breakToken.IsCancellationRequested)
                    return;
                if (answer != null)
                    await _uart.WriteAsync(answer, _breakToken);
            }
        }
    }
}
