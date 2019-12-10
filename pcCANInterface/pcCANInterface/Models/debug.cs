using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcCANInterface
{
    public class debug : ReactiveObject
    {
        public debug()
        {
            debugMsg = defaultDbg;
        }

        private string debugMsg;
        public string DebugMsg
        {
            get => debugMsg;
            private set => this.RaiseAndSetIfChanged(ref debugMsg, value);
        }
        public static string defaultDbg = "Welcome to the serial CAN interface";
        public static string noPorts = "plug in your device then click search for ports no serial port found";
        public static string foundPort = "found serial port";
        public static string connectFailed = "serial connect failed, select an active serial port";
        public static string connectGood = "connected to CAN transceiver";

        public static string badDebugParameter = "bad debug parameter";
        public static string triedToReadClosedPort = "tried to read from closed port";

        public void setMessage(string newMessage)
        {
            DebugMsg = newMessage;
        }

    }
}
