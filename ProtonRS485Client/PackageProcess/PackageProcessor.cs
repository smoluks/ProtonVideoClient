using ProtonRS485Client.Data;
using ProtonRS485Client.PackageCreate;
using System;
using System.Collections.Generic;
using static ProtonRS485Client.Data.ProtonMessage;

namespace ProtonRS485Client.PackageProcess
{
    /// <summary>
    /// ����� - ���������� ������� �������
    /// </summary>
    /// processor
    class PackageProcesser
    {
        enum Command : byte { SearchPPK = 0xCC, SearchPU = 0xCD, ExchangePPK = 0x00, ExchangePU = 0x01, ACK = 0x02 };  
        
        /// <summary>
        /// ��������� ������������ ������
        /// </summary>
        /// <param name="data">�����</param>
        /// <returns>�������� �����</returns>
        public byte[] ProcessCommand(byte[] data)
        {
            switch (data[2])
            {
                //����� ���
                case (byte)Command.SearchPPK:
                    return MakeRegistrationAnswer();
                //����� ��
                case (byte)Command.SearchPU:
                    throw new Exception("PU not realized yet");
                //����� �������� ���
                case (byte)Command.ExchangePPK:
                    //������������ ���������
                    return MakeStateAnswer(data);
                //����� ��
                case (byte)Command.ExchangePU:
                    throw new Exception("PU not realized yet");
                //ACK
                case (byte)Command.ACK:
                    return null;
                //
                default:
                    LogDispatcher.Write("������ ������� ����� � ����������� ��������: " + data[2]);
                    return null;
            }
        }

        //����� �� ������ �����������
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

        //����� �� �����
        byte[] MakeStateAnswer(byte[] data)
        {
            //-----��������� ���������� �������-----
            switch (data[3])
            {
                //����������� �����
                case 0:
                    break;
                //---������� ���---
                case 1:
                    break;
                //---������� ����������---
                case 0x90:
                    ProcessNoiseCommand(data);
                    isNoiseCommandAccepted = true;
                    break;
            }
            //-----������������ ������-----
            if (isNoiseCommandAccepted)
            {
                //---������������� ��������� �������---
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
                //---�������� ���������---
                ProtonMessage currentMessage = ObjectState.MessageBuffer.Dequeue();                
                byte[] buffer = new byte[11];
                buffer[0] = ObjectConfig.DeviceAddressRegistered;
                buffer[1] = 11; //�����
                buffer[2] = (byte)Command.ExchangePPK; //�������
                buffer[3] = 0x04; //ByteExt
                buffer[4] = (byte)((ObjectConfig.ObjectNumber - 1) >> 8);
                buffer[5] = (byte)(ObjectConfig.ObjectNumber - 1);
                buffer[6] = 0x0F; //��� ���������
                buffer[7] = (byte)((ushort)currentMessage.CommandCode);
                buffer[8] = (byte)((ushort)currentMessage.CommandCode >> 8);
                buffer[9] = currentMessage.Argument;
                buffer[10] = PackageStaticMethods.GetCommandCrc();
                return buffer;
            }
            else
            {
                //���� �������
                byte[] buffer = new byte[8];
                buffer[0] = ObjectConfig.DeviceAddressRegistered;
                buffer[1] = 8;
                buffer[2] = (byte)Command.ExchangePU; //���, ��� �� ������, � ������ ���������
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
                //�����. ������ �������
                buffer[6] = (byte)ObjectState.CommandAnswer;
                ObjectState.CommandAnswer = 0;
                //��� ������ - �� 1-16
                buffer[7] = 0x0;
                //
                return buffer;
            }
        }

        int lastCommandNumber = -1;
        /// <summary>
        /// ��������� ������� ����������
        /// </summary>
        /// <param name="data">����� � ��������</param>
        void ProcessNoiseCommand(byte[] data)
        {
            if (data[5] != lastCommandNumber)
            {
                lastCommandNumber = data[5];
                ///����� ����� ������ ������� ����������
                ProtonEvents.Command(new ProtonMessage((CommandCodeEnum)data[4], (data[10] == 2 ? CommandCodePrefixEnum.On : CommandCodePrefixEnum.Off), data[11]));
            }
        }
    }
}
