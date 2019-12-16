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
        private debug dbg;
        public serialCAN(debug dbgStr)
        {
            ReadList = new canReadList();
            WriteList = new canWriteList();
            port = new SerialPort();
            port.StopBits = StopBits.One;
            port.Parity = Parity.None;
            port.DataBits = 8;
            dbg = dbgStr;
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            selectedPort = null;
            updatePortNames(dbg);
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Eof)
                return; //ignore the fake 'end of frame' message
            parseMessage(ReadList, dbg);
        }

        private static int baud = 256000;
        public SerialPort port { get; set; }
        public static int MAXMESSAGES = 10;

        private canReadList readList;
        public canReadList ReadList
        {
            get => readList;
            set => this.RaiseAndSetIfChanged(ref readList, value);
        }

        private canWriteList writeList;
        public canWriteList WriteList
        {
            get => writeList;
            set => this.RaiseAndSetIfChanged(ref writeList, value);
        }

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
            }
            else
            {
                Console.WriteLine(debug.badDebugParameter);
            }
        }

        private void parseMessage(canList list, debug dbg)
        {
            byte[] rawMsg = new byte[canMsg.RAWNUMBYTES];


            try
            {
                port.Read(rawMsg, 0, canMsg.RAWNUMBYTES);
               

            }
            catch (TimeoutException ex)
            {
                //this just means there was no message, it's not a problem
                Console.WriteLine("there was no message");
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
            //Console.WriteLine("starting msg");
            //for (int i = 0; i < 12; i++)
            //{
            //    Console.WriteLine(rawMsg[i]);
            //}

            byte d0 = rawMsg[0];
            byte d1 = rawMsg[1];
            byte d2 = rawMsg[2];
            byte d3 = rawMsg[3];
            byte d4 = rawMsg[4];
            byte d5 = rawMsg[5];
            byte d6 = rawMsg[6];
            byte d7 = rawMsg[7];
            //Console.WriteLine("ending msg");

            UInt32 id = (UInt32)((rawMsg[8] << 24) + (rawMsg[9] << 16) + (rawMsg[10] << 8) + rawMsg[11]);
            //Console.WriteLine(id);
            Console.WriteLine(DateTime.Now);
            var message = new canMsg(id, d0, d1, d2, d3, d4, d5, d6, d7, DateTime.Now);
            list.addMessage(message, dbg);
               
        }
    }

}
