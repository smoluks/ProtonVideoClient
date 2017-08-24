using ProtonRS485Client.Data;
using ProtonRS485Client.PackageProcess;
using ProtonRS485Client.Uart;
using System.Threading;
using System.Threading.Tasks;

namespace ProtonRS485Client.PackageCreate
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


        public PackageStateDispatcher(UartDispatcher uart, PackageDataDispatcher packageDataDispatcher, PackageConnectDispatcher packageConnectDispatcher)
        {
            _uart = uart;
            _packageDataDispatcher = packageDataDispatcher;
            _packageConnectDispatcher = packageConnectDispatcher;
            //
            _packageProcesser = new PackageProcesser();
            //
            _cancelTokenSource = new CancellationTokenSource();
            _breakToken = _cancelTokenSource.Token;
        }

        /// <summary>
        /// Сборка пакетов
        /// </summary>
        /// Разделить операции (см. комментарии в Events)
        public async Task CollectPacketsAsync()
        {
            LogDispatcher.Write("CollectPacketsAsync start");
            while (!_breakToken.IsCancellationRequested)
            {
                //адрес                
                var address = await _uart.ReadByteAsync(_breakToken);
                if (!_packageDataDispatcher.ProcessAddress(address)) return;
                //инкапсулировать (address & 0x80) == 0               
                _packageConnectDispatcher.CorrectAddressReceived(PackageStaticMethods.isAddressInSearch(address));
                
                //длина
                var length = await _uart.ReadByteAsync(_breakToken);
                if (!_packageDataDispatcher.ProcessFrameLength(length)) return;
                //Данные
                var data = await _uart.ReadAsync(length - 1, _breakToken);
                if (!_packageDataDispatcher.ProcessPacket(data)) return;
                //Crc
                var crc = await _uart.ReadByteAsync(_breakToken);
                if (!_packageDataDispatcher.ProcessCRC(crc)) return;
                //отправить это на обработку
                if (ObjectConfig.DataLogging)
                    LogDispatcher.WriteData("Packet received: ", _packageDataDispatcher.package.GetPacket());
                var answerTask = _packageProcesser.ProcessCommand(_packageDataDispatcher.package.GetPacket());
                if (answerTask != null)
                {
                    await _uart.WriteAsync(answerTask, _breakToken);
                    await _uart.WriteByteAsync(PackageStaticMethods.GetCrc(answerTask), _breakToken);
                    if (ObjectConfig.DataLogging)
                        LogDispatcher.WriteData("Packet transmitted: ", answerTask);
                }
            }
        }

        public void KillTask()
        {
            _cancelTokenSource.Cancel();
        }
        
    }
}
