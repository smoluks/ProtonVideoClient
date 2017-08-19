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
        public Task CollectPacketsAsync()
        {
            return Task.Run(() =>
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
                        var data = _uart.ReadAsync(lenght.Result - 1, _breakToken);
                        data.Wait();
                        if (!_packageDataDispatcher.ProcessPacket(data.Result))
                            break;
                        //отправить это на обработку
                        var answer = _packageProcesser.ProcessCommand(_packageDataDispatcher.Packet);
                        if (answer != null)
                            _uart.WriteAsync(answer, _breakToken);
                    }
                    catch (Exception ex)
                    {
                        LogDispatcher.Write("CollectPacketsAsync ended by exception "+ex.Message);
                        return;
                    }
                }
            });
        }
    }
}
