using System;
using System.Collections.Generic;

namespace ProtonRS485Client
{
    class CommandLevel
    {
        SObjectConfig _objectConfig;
        ProcessCommandDelegate _processCommand;

        public SObjectState ObjectState;
        public CommandLevel(SObjectConfig objectConfig, ProcessCommandDelegate processCommand)
        {
            _objectConfig = objectConfig;
            _processCommand = processCommand;
        }

        Queue<Message> MessageBuffer = new Queue<Message>();
        //ставит сообщение на очередь в отправку
        public void SetMessageToSend(EObjectMessages command, byte arg)
        {
            MessageBuffer.Enqueue(new Message(command, arg));
        }

        bool isNoiseCommandAccepted = false;

        //обработка поступившей команды
        public byte[] ProcessCommand(byte[] data)
        {
            switch (data[2])
            {
                //регистрация
                case 0xCC:
                    return makeregdata();
                //опрос ведомого ППК
                case 0x00:
                    //сформировать состояние
                    return makestate(data);
                //ACK
                case 0x02:
                    return null;
                //
                default:
                    return null;
            }
        }

        //ответ на запрос регистрации
        byte[] makeregdata()
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

        //ответ на опрос
        byte[] makestate(byte[] data)
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
                    isNoiseCommandAccepted = tryProcessNoiseCommand(data);
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
            else if (MessageBuffer.Count>0)
            {
                Message currentMessage = MessageBuffer.Dequeue();
                //---отправка сообщения---
                byte[] buffer = new byte[11];
                buffer[0] = (byte)(_objectConfig.deviceAddress | 0x80);
                buffer[1] = 11; //длина
                buffer[2] = 0x00; //Команда
                buffer[3] = 0x04; //ByteExt
                buffer[4] = (byte)((_objectConfig.objectNumber - 1) >> 8);
                buffer[5] = (byte)(_objectConfig.objectNumber - 1);
                buffer[6] = 0x0F; //Тип сообщения
                buffer[7] = (byte)((ushort)currentMessage.MessageCode);
                buffer[8] = (byte)((ushort)currentMessage.MessageCode >> 8);
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
                    buffer[5] = ObjectState.waitingTime;
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
        bool tryProcessNoiseCommand(byte[] data)
        {
            if(data[5]!= lastCommandNumber)
            {
                lastCommandNumber = data[5];
                _processCommand?.Invoke(data[4], data[11], data[10] != 2);
            }
            return true;
        } 
    }
}
