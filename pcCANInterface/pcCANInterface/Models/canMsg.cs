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
        public static int RAWNUMBYTES = 18; // this value should theoretically be 12 not 18: 8 bytes for data and 4 bytes for id packed into the communication array
        //TODO: however, for some reason there is 6 bytes of packing on the end of the Arduino serial message, don't know why it's there, this consistent error is solved by reading all 18 bytes and ignoring the last 6

        private UInt32 id;
        public UInt32 Id
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

        public canMsg(UInt32 initId, byte newD0, byte newD1, byte newD2, byte newD3, byte newD4, byte newD5, byte newD6, byte newD7, DateTime initTime)
        {
            Id = initId;
            //TODO: update this if different size messages are given as a parameter
       
            D0 = newD0;
            D1 = newD1;
            D2 = newD2;
            D3 = newD3;
            D4 = newD4;
            D5 = newD5;
            D6 = newD6;
            D7 = newD7;

        }
    }
}
