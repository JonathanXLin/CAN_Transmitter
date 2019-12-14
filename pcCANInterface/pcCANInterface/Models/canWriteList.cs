using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcCANInterface
{
    public class canWriteList : canList
    {
        public canWriteList() : base() { this.Messages.Add(new canMsg(5, 5,5,5,5,5,5,5,5, DateTime.Now)); }

    }
}
