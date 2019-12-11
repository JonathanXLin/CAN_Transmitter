using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using ReactiveUI;
using System.Threading;

namespace pcCANInterface
{
    public class serialCAN : ReactiveObject
    {
        public serialCAN(debug dbgStr)
        {
            port = new SerialPort();
            selectedPort = null;
            updatePortNames(dbgStr);
        }

        private static int baud = 256000;
        public SerialPort port { get; set; }
        public static int MAXMESSAGES = 10;

        private int readId;
        public int ReadId
        {
            get => readId;
            set => this.RaiseAndSetIfChanged(ref readId, value);
        }
        private int[] readMessage;
        public int[] ReadMessage
        {
            get => readMessage;
            set => this.RaiseAndSetIfChanged(ref readMessage, value);
        }


        private string selectedPort;
        public string SelectedPort
        {
            get => selectedPort;
            set => this.RaiseAndSetIfChanged(ref selectedPort, value);
        }

        private string[] portNames;
        public string[] PortNames
        {
            get => portNames;
            set => this.RaiseAndSetIfChanged(ref portNames, value);
        }

        public void updatePortNames(debug dbgStr)
        {
            PortNames = SerialPort.GetPortNames();
            Console.WriteLine(PortNames.Length);
            if(dbgStr != null)
            {
                if (PortNames.Length == 0)
                    dbgStr.setMessage(debug.noPorts);
                else
                    dbgStr.setMessage(debug.foundPort);
            }
            else
            {
                Console.WriteLine(debug.badDebugParameter);
            }
        }

        public void connect(debug dbgStr)
        {
            if(dbgStr != null)
            {
                try
                {
                    port.PortName = selectedPort;
                    port.BaudRate = baud;
                    port.Open();
              
                }
                catch (Exception ex)
                {
                    dbgStr.setMessage(debug.connectFailed);
                    return;
                }
                dbgStr.setMessage(debug.connectGood);
                //start a task to check the buffer
                Timer timer = new Timer(readMessages, dbgStr, TimeSpan.FromSeconds(0), TimeSpan.FromMilliseconds(0.1));
     
            }
            else
            {
                Console.WriteLine(debug.badDebugParameter);
            }


        }

        private void readMessages(object dbgStr)
        {
            var dbg = dbgStr as debug;
            byte[] rawMsg = new byte[canMsg.RAWNUMBYTES];
            if (dbgStr != null)
            {
                try
                {
                    port.Read(rawMsg, 0, canMsg.RAWNUMBYTES);
                }
                catch (TimeoutException ex)
                {
                    //this just means there was no message, it's not a problem
                    return;
                }
                catch (InvalidOperationException ex)
                {
                    dbg.setMessage(debug.triedToReadClosedPort);
                    return;
                }
                //got a good message, no exceptions
                //don't show it in the debug textbox since it's so often
                //just show it on the read section
                rawCANMsg msg = (rawCANMsg)rawMsg;

               
            }
            else
            {
                Console.WriteLine("bad debug string");
            }
        }

        void parseCANId(string message)
        {

        }

        void parseCANMsg(string message)
        {

        }
    }

}
