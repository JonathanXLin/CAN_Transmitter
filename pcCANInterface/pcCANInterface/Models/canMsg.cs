using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcCANInterface
{

    //struct for accessing reactively
    public class canMsg : ReactiveObject
    {
        public static int NUMATTRIBUTES = 3; //update for each piece of information stored; currently id, data, and time
        public static int MESSAGESIZE = 8;
        public static int RAWNUMBYTES = 10;

        private int id;
        public int Id
        {
            get => id;
            set => this.RaiseAndSetIfChanged(ref id, value);
        }

        //implemented this way because couldn't display an array properly as a property in a datagrid
        //TODO: more elegant/extensible solution
        public byte data0;
        public byte D0
        {
            get => data0;
            set => this.RaiseAndSetIfChanged(ref data0, value);
        }

        public byte data1;
        public byte D1
        {
            get => data1;
            set => this.RaiseAndSetIfChanged(ref data1, value);
        }

        public byte data2;
        public byte D2
        {
            get => data2;
            set => this.RaiseAndSetIfChanged(ref data2, value);
        }

        public byte data3;
        public byte D3
        {
            get => data3;
            set => this.RaiseAndSetIfChanged(ref data3, value);
        }

        public byte data4;
        public byte D4
        {
            get => data4;
            set => this.RaiseAndSetIfChanged(ref data4, value);
        }

        public byte data5;
        public byte D5
        {
            get => data5;
            set => this.RaiseAndSetIfChanged(ref data5, value);
        }

        public byte data6;
        public byte D6
        {
            get => data6;
            set => this.RaiseAndSetIfChanged(ref data6, value);
        }

        public byte data7;
        public byte D7
        {
            get => data7;
            set => this.RaiseAndSetIfChanged(ref data7, value);
        }


        private DateTime time;
        public DateTime Time
        {
            get => time;
            set => this.RaiseAndSetIfChanged(ref time, value);
        }

        public canMsg(int initId, byte[] initMsg, DateTime initTime)
        {
            Id = initId;
            //TODO: update this if different size messages are given as a parameter
            try
            {
                D0 = initMsg[0];
                D1 = initMsg[1];
                D2 = initMsg[2];
                D3 = initMsg[3];
                D4 = initMsg[4];
                D5 = initMsg[5];
                D6 = initMsg[6];
                D7 = initMsg[7];
            }
            catch(Exception ex)
            {
                //in case there's an argument out of range
            }
            Time = initTime;
        }
    }

    //struct for casting struct in Arduino
    public struct rawCANMsg
    {
        UInt32 id;
        byte[] message;

        public static implicit operator rawCANMsg(byte[] v) => new rawCANMsg(v);

        public rawCANMsg(byte[] v)
        {
            message = new byte[canMsg.MESSAGESIZE];
            int i = 0;
            for(; i < canMsg.MESSAGESIZE; i++)
            {
                message[i] = v[i];
            }

            //extract Id info
            id = (UInt32)(v[9] << 16 + v[10]);

        }
    };
}
