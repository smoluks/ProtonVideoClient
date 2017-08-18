using System.Threading;

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
        CancellationTokenSource _cancelTokenSource;
        CancellationToken _breakToken;

        public PackageStateDispatcher(UartDispatcher uart, PackageDataDispatcher packageDataDispatcher, PackageConnectDispatcher packageConnectDispatcher, ObjectConfig objectConfig, ObjectState objectState)
        {
            _uart = uart;
            _packageDataDispatcher = packageDataDispatcher;
            _packageConnectDispatcher = packageConnectDispatcher;
            //
            _packageProcesser = new PackageProcesser(objectConfig, objectState);
            //
            _cancelTokenSource = new CancellationTokenSource();
            _breakToken = _cancelTokenSource.Token;
        }

        /// <summary>
        /// Сборка пакетов
        /// </summary>
        public async void StartCollect()
        {
            while (true)
            {
                //Адрес
                var addr = await _uart.ReadByteAsync(_breakToken);
                if (!_packageDataDispatcher.ProcessAddress(addr))
                    break;
                _packageConnectDispatcher.CorrectAddressReceived((addr & 0x80) == 0);
                //Длина
                var lenght = await _uart.ReadByteAsync(_breakToken);
                if (!_packageDataDispatcher.ProcessFrameLength(lenght))
                    break;
                //Данные
                var data = await _uart.ReadAsync(--lenght, _breakToken);
                if (!_packageDataDispatcher.ProcessPacket(data))
                    break;
                //отправить это на обработку
                var answer = _packageProcesser.ProcessCommand(_packageDataDispatcher.Packet);
                if (answer != null)
                    await _uart.WriteAsync(answer, _breakToken);
            }
        }


    }
}
