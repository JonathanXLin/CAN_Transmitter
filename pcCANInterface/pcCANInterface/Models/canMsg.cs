using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcCANInterface
{

    //struct for accessing reactively
    class canMsg : ReactiveObject
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

        public byte[] message;
        public byte[] Message
        {
            get => message;
            set => this.RaiseAndSetIfChanged(ref message, value);
        }

        private DateTime time;
        public DateTime Time
        {
            get => time;
            set => this.RaiseAndSetIfChanged(ref time, value);
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
