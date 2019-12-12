using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcCANInterface
{
    

    public abstract class canList : ReactiveObject
    {
        public static int LISTLENGTH = 32;
        private List<canMsg> messages;
        public List<canMsg> Messages
        {
            get => messages;
            set => this.RaiseAndSetIfChanged(ref messages, value);
        }
        public canList()
        {
            messages = new List<canMsg>(LISTLENGTH);
        }
    }
}
