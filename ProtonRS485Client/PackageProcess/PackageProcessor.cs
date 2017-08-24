using ProtonRS485Client.Data;
using ProtonRS485Client.PackageCreate;
using System;
using System.Collections.Generic;
using static ProtonRS485Client.Data.ProtonMessage;

namespace ProtonRS485Client.PackageProcess
{
    /// <summary>
    /// Класс - обработчик пакетов протона
    /// </summary>
    /// processor
    class PackageProcesser
    {
        enum Command : byte { SearchPPK = 0xCC, SearchPU = 0xCD, ExchangePPK = 0x00, ExchangePU = 0x01, ACK = 0x02 };  
        
        /// <summary>
        /// Обработка поступившего пакета
        /// </summary>
        /// <param name="data">пакет</param>
        /// <returns>Ответный пакет</returns>
        public byte[] ProcessCommand(byte[] data)
        {
            switch (data[2])
            {
                //поиск ППК
                case (byte)Command.SearchPPK:
                    return MakeRegistrationAnswer();
                //Поиск ПУ
                case (byte)Command.SearchPU:
                    throw new Exception("PU not realized yet");
                //опрос ведомого ППК
                case (byte)Command.ExchangePPK:
                    //сформировать состояние
                    return MakeStateAnswer(data);
                //опрос ПУ
                case (byte)Command.ExchangePU:
                    throw new Exception("PU not realized yet");
                //ACK
                case (byte)Command.ACK:
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
            buffer[0] = ObjectConfig.DeviceAddress;
            buffer[1] = 0x0C;
            buffer[2] = (byte)Command.SearchPPK;
            buffer[3] = 0x80;
            buffer[4] = (byte)((ObjectConfig.ObjectNumber - 1) >> 8);
            buffer[5] = (byte)(ObjectConfig.ObjectNumber - 1);
            buffer[6] = 0x26;
            buffer[7] = ObjectConfig.MadeYear;
            buffer[8] = (byte)(ObjectConfig.SerialNumber >> 8);
            buffer[9] = (byte)(ObjectConfig.SerialNumber);
            buffer[10] = ObjectConfig.SoftwareVersion;
            buffer[11] = ObjectConfig.SoftwareReleaseVersion;
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
                buffer[0] = ObjectConfig.DeviceAddressRegistered;
                buffer[1] = 4;
                buffer[2] = (byte)Command.ExchangePPK;
                buffer[3] = 0;
                isNoiseCommandAccepted = false;
                return buffer;
            }
            else if (ObjectState.MessageBuffer.Count > 0)
            {
                //---отправка сообщения---
                ProtonMessage currentMessage = ObjectState.MessageBuffer.Dequeue();                
                byte[] buffer = new byte[11];
                buffer[0] = ObjectConfig.DeviceAddressRegistered;
                buffer[1] = 11; //длина
                buffer[2] = (byte)Command.ExchangePPK; //Команда
                buffer[3] = 0x04; //ByteExt
                buffer[4] = (byte)((ObjectConfig.ObjectNumber - 1) >> 8);
                buffer[5] = (byte)(ObjectConfig.ObjectNumber - 1);
                buffer[6] = 0x0F; //Тип сообщения
                buffer[7] = (byte)((ushort)currentMessage.CommandCode);
                buffer[8] = (byte)((ushort)currentMessage.CommandCode >> 8);
                buffer[9] = currentMessage.Argument;
                buffer[10] = PackageStaticMethods.GetCommandCrc();
                return buffer;
            }
            else
            {
                //куча шлейфов
                byte[] buffer = new byte[8];
                buffer[0] = ObjectConfig.DeviceAddressRegistered;
                buffer[1] = 8;
                buffer[2] = (byte)Command.ExchangePU; //Нет, это не ошибка, а прикол протокола
                buffer[3] = ObjectConfig.RazdelNumber;
                //---pstat0---
                buffer[4] = 0;
                if (ObjectState.Fire)
                    buffer[4] |= 0x80;
                if (ObjectState.Panic)
                    buffer[4] |= 0x40;
                if (ObjectState.Alarm)
                    buffer[4] |= 0x20;
                if (ObjectState.Error)
                    buffer[4] |= 0x10;
                buffer[4] |= (byte)((byte)ObjectState.RazdelState & 0x0F);
                //pstat1
                if (ObjectState.WaitingArmedChangeState)
                    buffer[5] = ObjectState.WaitingTime;
                else
                {
                    buffer[5] = (byte)(((byte)ObjectState.LedState & 0x03) << 6);
                    if (ObjectState.TamperOff)
                        buffer[5] |= 0x08;
                    buffer[5] = (byte)(((byte)ObjectState.BatteryState & 0x03) << 1);
                    if (ObjectState.MainPowerError)
                        buffer[5] |= 0x01;
                }
                //подтв. приема команды
                buffer[6] = (byte)ObjectState.CommandAnswer;
                ObjectState.CommandAnswer = 0;
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
                ProtonEvents.Command(new ProtonMessage((CommandCodeEnum)data[4], (data[10] == 2 ? CommandCodePrefixEnum.On : CommandCodePrefixEnum.Off), data[11]));
            }
        }
    }
}
