using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcCANInterface
{
    public class canWriteList : canList
    {
        public canWriteList() : base() { byte[] tst = new byte[] { 5, 6,3,2,0xFF,0x93,0x1,0x8 }; this.Messages.Add(new canMsg(5, tst, DateTime.Now)); }

    }
}
