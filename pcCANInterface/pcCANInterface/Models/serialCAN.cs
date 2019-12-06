using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace pcCANInterface
{
    public  class serialCAN
    {
        public serialCAN(debug dbgStr)
        {
            port = new SerialPort();
            updatePortNames(dbgStr);
        }
        public SerialPort port { get; set; }
        public string[] portNames { get; set; }

        public void updatePortNames(debug dbgStr)
        {
            portNames = SerialPort.GetPortNames();
            Console.WriteLine(portNames.Length);
            if (portNames.Length == 0)
                dbgStr.setMessage(debug.noPorts);
            else
                dbgStr.setMessage(debug.foundPort);
        }

    }

}
