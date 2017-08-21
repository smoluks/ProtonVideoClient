using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonRS485Client
{
    public static class Events
    {
        //События подключения и отключения мастера
        public delegate void ConnectionDelegate();
        public static event ConnectionDelegate ConnectEvent;
        public static event ConnectionDelegate DisconnectEvent;

        //Событие приема команды оповещения
        public delegate void CommandDelegate();
        public static event CommandDelegate CommandEvent;
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
