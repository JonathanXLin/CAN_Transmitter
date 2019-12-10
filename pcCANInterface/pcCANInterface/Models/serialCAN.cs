using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using ReactiveUI;

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
        private static int MESSAGESIZE = 8;
        public SerialPort port { get; set; }

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
     
            }
            else
            {
                Console.WriteLine(debug.badDebugParameter);
            }


        }

        private void readMessages(debug dbgStr)
        {
            string msg;
            if(dbgStr != null)
            {
                try
                {
                    msg = port.ReadLine();
                }
                catch (TimeoutException ex)
                {
                    //this just means there was no message, it's not a problem
                    return;
                }
                catch(InvalidOperationException ex)
                {
                    dbgStr.setMessage(debug.triedToReadClosedPort);
                    return;
                }
                //got a good message, no exceptions
                parseCANId(msg);
                parseCANMsg(msg);
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
