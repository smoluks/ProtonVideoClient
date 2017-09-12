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
    public class PackageStateDispatcher
    {
        private readonly HardwareLevelDispatcher _uart;
        private readonly PackageDataDispatcher _packageDataDispatcher;
        private readonly PackageConnectDispatcher _packageConnectDispatcher;
        private readonly PackageProcesser _packageProcesser;

        CancellationTokenSource _cancelTokenSource;
        CancellationToken _breakToken;

        public PackageStateDispatcher(HardwareLevelDispatcher uart, PackageDataDispatcher packageDataDispatcher, PackageConnectDispatcher packageConnectDispatcher)
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
                if (!await AddressAsync())
                    break;
                //длина
                if (!await LengthAsync())
                    break;
                //Данные
                if (!await DataAsync())
                    break;
                //Crc
                if (!await CRCAsync())
                    break;
                //отправить это на обработку
                await ProcessAsync();
            }
        }

        async Task<bool> AddressAsync()
        {
            var address = await _uart.ReadByteAsync(_breakToken);
            if (!_packageDataDispatcher.ProcessAddress(address)) return false;
            //инкапсулировать (address & 0x80) == 0               
            _packageConnectDispatcher.CorrectAddressReceived(PackageStaticMethods.isAddressInSearch(address));
            return true;
        }

        async Task<bool> LengthAsync()
        {
            var length = await _uart.ReadByteAsync(_breakToken);
            return _packageDataDispatcher.ProcessFrameLength(length);
        }

        async Task<bool> DataAsync()
        {
            var data = await _uart.ReadAsync(_packageDataDispatcher.package.Length - 2, _breakToken);
            return _packageDataDispatcher.ProcessPacket(data);
        }

        async Task<bool> CRCAsync()
        {
            var crc = await _uart.ReadByteAsync(_breakToken);
            return _packageDataDispatcher.ProcessCRC(crc);
        }

        async Task ProcessAsync()
        {
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

        public void KillTask()
        {
            _cancelTokenSource.Cancel();
        }

    }
}
