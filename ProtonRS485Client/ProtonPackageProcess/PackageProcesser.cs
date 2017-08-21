using System.Collections.Generic;

namespace ProtonRS485Client
{
    /// <summary>
    /// ����� - ���������� ������� �������
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

        //������ ��������� ������� �� ������� � ��������
        public void SetMessageToSend(Message message)
        {
            _messageBuffer.Enqueue(message);
        }

        /// <summary>
        /// ��������� ������������ ������
        /// </summary>
        /// <param name="data">�����</param>
        /// <returns>�������� �����</returns>
        public byte[] ProcessCommand(byte[] data)
        {
            switch (data[2])
            {
                //�����������
                case 0xCC:
                    return MakeRegistrationAnswer();
                //����� �������� ���
                case 0x00:
                    //������������ ���������
                    return MakeStateAnswer(data);
                //ACK
                case 0x02:
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
                //---�������� ���������---
                byte[] buffer = new byte[11];
                buffer[0] = (byte)(_objectConfig.deviceAddress | 0x80);
                buffer[1] = 11; //�����
                buffer[2] = 0x00; //�������
                buffer[3] = 0x04; //ByteExt
                buffer[4] = (byte)((_objectConfig.objectNumber - 1) >> 8);
                buffer[5] = (byte)(_objectConfig.objectNumber - 1);
                buffer[6] = 0x0F; //��� ���������
                buffer[7] = (byte)((ushort)currentMessage.CommandCode);
                buffer[8] = (byte)((ushort)currentMessage.CommandCode >> 8);
                buffer[9] = currentMessage.Argument;
                buffer[10] = 0x00;
                return buffer;
            }
            else
            {
                //���� �������
                byte[] buffer = new byte[8];
                buffer[0] = (byte)(_objectConfig.deviceAddress | 0x80);
                buffer[1] = 8;
                buffer[2] = 0x01; //�������
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
                //�����. ������ �������
                buffer[6] = (byte)_objectState.CommandAnswer;
                _objectState.CommandAnswer = 0;
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
            }
        }
    }
}
