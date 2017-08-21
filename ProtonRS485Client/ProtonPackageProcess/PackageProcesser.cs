using System.Collections.Generic;

namespace ProtonRS485Client
{
    /// <summary>
    /// Класс - обработчик пакетов протона
    /// </summary>
    /// processor
    class PackageProcesser
    {
        ObjectConfig _objectConfig;
        ObjectState _objectState;
        Queue<Message> _messageBuffer = new Queue<Message>();

        public PackageProcesser(ObjectConfig objectConfig, ObjectState objectState)
        {
            _objectConfig = objectConfig;
            _objectState = objectState;
        }

        //ставит сообщение серверу на очередь в отправку
        public void SetMessageToSend(Message message)
        {
            _messageBuffer.Enqueue(message);
        }

        /// <summary>
        /// Обработка поступившего пакета
        /// </summary>
        /// <param name="data">пакет</param>
        /// <returns>Ответный пакет</returns>
        public byte[] ProcessCommand(byte[] data)
        {
            switch (data[2])
            {
                //регистрация
                case 0xCC:
                    return MakeRegistrationAnswer();
                //опрос ведомого ППК
                case 0x00:
                    //сформировать состояние
                    return MakeStateAnswer(data);
                //ACK
                case 0x02:
                    return null;
                //
                default:
                    LogDispatcher.Write("Мастер прислал пакет с неизвестной командой: " + data[2]);
                    return null;
            }
        }

        //ответ на запрос регистрации
        byte[] MakeRegistrationAnswer()
        {
            byte[] buffer = new byte[12];
            buffer[0] = _objectConfig.deviceAddress;
            buffer[1] = 0x0C;
            buffer[2] = 0xCC;
            buffer[3] = 0x80;
            buffer[4] = (byte)((_objectConfig.objectNumber - 1) >> 8);
            buffer[5] = (byte)(_objectConfig.objectNumber - 1);
            buffer[6] = 0x26;
            buffer[7] = _objectConfig.madeYear;
            buffer[8] = (byte)(_objectConfig.serialNumber >> 8);
            buffer[9] = (byte)(_objectConfig.serialNumber);
            buffer[10] = _objectConfig.softwareVersion;
            buffer[11] = _objectConfig.softwareReleaseVersion;
            return buffer;
        }

        bool isNoiseCommandAccepted = false;

        //ответ на опрос
        byte[] MakeStateAnswer(byte[] data)
        {
            //-----обработка полученной команды-----
            switch (data[3])
            {
                //стандартный опрос
                case 0:
                    break;
                //---команда ППК---
                case 1:
                    break;
                //---команда оповещения---
                case 0x90:
                    ProcessNoiseCommand(data);
                    isNoiseCommandAccepted = true;
                    break;
            }
            //-----формирование ответа-----
            if (isNoiseCommandAccepted)
            {
                //---подтверждение получения команды---
                byte[] buffer = new byte[4];
                buffer[0] = (byte)(_objectConfig.deviceAddress | 0x80);
                buffer[1] = 4;
                buffer[2] = 0;
                buffer[3] = 0;
                isNoiseCommandAccepted = false;
                return buffer;
            }
            else if (_messageBuffer.Count > 0)
            {
                Message currentMessage = _messageBuffer.Dequeue();
                //---отправка сообщения---
                byte[] buffer = new byte[11];
                buffer[0] = (byte)(_objectConfig.deviceAddress | 0x80);
                buffer[1] = 11; //длина
                buffer[2] = 0x00; //Команда
                buffer[3] = 0x04; //ByteExt
                buffer[4] = (byte)((_objectConfig.objectNumber - 1) >> 8);
                buffer[5] = (byte)(_objectConfig.objectNumber - 1);
                buffer[6] = 0x0F; //Тип сообщения
                buffer[7] = (byte)((ushort)currentMessage.CommandCode);
                buffer[8] = (byte)((ushort)currentMessage.CommandCode >> 8);
                buffer[9] = currentMessage.Argument;
                buffer[10] = 0x00;
                return buffer;
            }
            else
            {
                //куча шлейфов
                byte[] buffer = new byte[8];
                buffer[0] = (byte)(_objectConfig.deviceAddress | 0x80);
                buffer[1] = 8;
                buffer[2] = 0x01; //Команда
                buffer[3] = _objectConfig.RazdelNumber;
                //---pstat0---
                buffer[4] = 0;
                if (_objectState.Fire)
                    buffer[4] |= 0x80;
                if (_objectState.Panic)
                    buffer[4] |= 0x40;
                if (_objectState.Alarm)
                    buffer[4] |= 0x20;
                if (_objectState.Error)
                    buffer[4] |= 0x10;
                buffer[4] |= (byte)((byte)_objectState.RazdelState & 0x0F);
                //pstat1
                if (_objectState.WaitingArmedChangeState)
                    buffer[5] = _objectState.waitingTime;
                else
                {
                    buffer[5] = (byte)(((byte)_objectState.LedState & 0x03) << 6);
                    if (_objectState.TamperOff)
                        buffer[5] |= 0x08;
                    buffer[5] = (byte)(((byte)_objectState.BatteryState & 0x03) << 1);
                    if (_objectState.MainPowerError)
                        buffer[5] |= 0x01;
                }
                //подтв. приема команды
                buffer[6] = (byte)_objectState.CommandAnswer;
                _objectState.CommandAnswer = 0;
                //тип данных - ШС 1-16
                buffer[7] = 0x0;
                //
                return buffer;
            }
        }

        int lastCommandNumber = -1;

        /// <summary>
        /// Обработка команды оповещения
        /// </summary>
        /// <param name="data">пакет с командой</param>
        void ProcessNoiseCommand(byte[] data)
        {
            if (data[5] != lastCommandNumber)
            {
                lastCommandNumber = data[5];
                ///Здесь вызов эвента события оповещения
            }
        }
    }
}
