using System;
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

        bool _dataLogging; 

        //token переместить в CollectPackets
        //ObjectConfig сделать статическим
        public PackageStateDispatcher(UartDispatcher uart, PackageDataDispatcher packageDataDispatcher, PackageConnectDispatcher packageConnectDispatcher, ObjectConfig objectConfig, ObjectState objectState, CancellationToken breakToken)
        {
            _uart = uart;
            _packageDataDispatcher = packageDataDispatcher;
            _packageConnectDispatcher = packageConnectDispatcher;
            //
            _packageProcesser = new PackageProcesser(objectConfig, objectState);
            _dataLogging = objectConfig.dataLogging;
            //
            _breakToken = breakToken;
        }

        /// <summary>
        /// Сборка пакетов
        /// </summary>
        /// Разделить операции (см. комментарии в Events)
        public async Task CollectPacketsAsync()
        {
            while (!_breakToken.IsCancellationRequested)
            {
                var address = await _uart.ReadByteAsync(_breakToken);
                if (!_packageDataDispatcher.ProcessAddress(address)) return;
                //инкапсулировать (address & 0x80) == 0
                _packageConnectDispatcher.CorrectAddressReceived((address & 0x80) == 0);

                var length = await _uart.ReadByteAsync(_breakToken);
                if (!_packageDataDispatcher.ProcessFrameLength(length)) return;
                //Данные
                var dataTask = await _uart.ReadAsync(length - 1, _breakToken);
                if (!_packageDataDispatcher.ProcessPacket(dataTask)) return;
                //отправить это на обработку
                if (_dataLogging)
                    LogDispatcher.WriteData("Packet received: ", _packageDataDispatcher.Packet);
                var answerTask = _packageProcesser.ProcessCommand(_packageDataDispatcher.Packet);
                if (answerTask != null)
                {
                    await _uart.WriteAsync(answerTask, _breakToken);
                    await _uart.WriteByteAsync(PackageAlgs.GetCrc(answerTask), _breakToken);
                    if (_dataLogging)
                        LogDispatcher.WriteData("Packet transmitted: ", answerTask);
                }
            }
            /*return Task.Run(() =>
            {
                while (!_breakToken.IsCancellationRequested)
                {
                    try
                    {
                        //Адрес
                        var addrTask = _uart.ReadByteAsync(_breakToken);
                        addrTask.Wait();
                        if (!_packageDataDispatcher.ProcessAddress(addrTask.Result))
                            break;
                        _packageConnectDispatcher.CorrectAddressReceived((addrTask.Result & 0x80) == 0);
                        //Длина
                        var lenght = _uart.ReadByteAsync(_breakToken);
                        lenght.Wait();
                        if (!_packageDataDispatcher.ProcessFrameLength(lenght.Result))
                            break;
                        //Данные
                        var dataTask = _uart.ReadAsync(lenght.Result - 1, _breakToken);
                        dataTask.Wait();
                        if (!_packageDataDispatcher.ProcessPacket(dataTask.Result))
                            break;
                        //отправить это на обработку
                        if (_dataLogging)
                            LogDispatcher.WriteData("Packet received: ", _packageDataDispatcher.Packet);
                        var answerTask = _packageProcesser.ProcessCommand(_packageDataDispatcher.Packet);
                        if (answerTask != null)
                        {
                            var writeTask = _uart.WriteAsync(answerTask, _breakToken);
                            writeTask.Wait();
                            var writeCRCTask = _uart.WriteByteAsync(PackageAlgs.GetCrc(answerTask), _breakToken);
                            writeCRCTask.Wait();
                            if (_dataLogging)
                                LogDispatcher.WriteData("Packet transmitted: ", answerTask);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogDispatcher.Write("CollectPacketsAsync ended by exception "+ex.Message);
                        return;
                    }
                }
            });*/
        }
    }
}
