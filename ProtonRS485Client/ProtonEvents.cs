using ProtonRS485Client.Data;

namespace ProtonRS485Client
{
    public static class ProtonEvents
    {
        //События подключения и отключения мастера
        public delegate void ConnectionDelegate();
        public static event ConnectionDelegate ConnectEvent;
        public static event ConnectionDelegate DisconnectEvent;

        public static void Connect()
        {
            ConnectEvent.Invoke();
        }

        public static void Disconnect()
        {
            DisconnectEvent.Invoke();
        }

        //Событие приема команды оповещения
        public delegate void CommandDelegate(ProtonMessage message);
        public static event CommandDelegate CommandEvent;

        public static void Command(ProtonMessage message)
        {
            CommandEvent.Invoke(message);
        }
    }

    

    /*
    private async Task RunConnect(){
       while(true) {
           var result = await ConnectAsync();
           HandleResult(result);
       }
    }

    private async Task RunRead(){
        while(true) {
            var result = await ReadAsync();
            HandleRead(result);
        }
    }
     
     */
}
