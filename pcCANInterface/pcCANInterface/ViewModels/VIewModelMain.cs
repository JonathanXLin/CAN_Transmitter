using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcCANInterface.ViewModels
{
    class ViewModelMain
    {
        public serialCAN serial { get; set; }
        public debug dbgString { get; set; }

        public ViewModelMain()
        {
            dbgString = new debug();
            serial = new serialCAN(dbgString);
        }
    }
}
