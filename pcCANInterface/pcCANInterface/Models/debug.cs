using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcCANInterface
{
    public class debug
    {
        public debug()
        {
            debugMsg = defaultDbg;
        }

        public string debugMsg { get; private set; }
        public static string defaultDbg = "Welcome to the serial CAN interface";
        public static string noPorts = "plug in your device no serial port found";
        public static string foundPort = "found serial port";

        public void setMessage(string newMessage)
        {
            debugMsg = newMessage;
        }

    }
}
