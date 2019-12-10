﻿using pcCANInterface.ViewModels.Commands;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using ReactiveUI.Wpf;

namespace pcCANInterface.ViewModels
{
    public class ViewModelMain : ReactiveObject
    {
        public serialCAN serial { get; private set; }
        public debug dbgString { get; private set; }
        public updatePortsCommand updatePorts { get; private set; }
        public connectCommand connect { get; private set; }
        public ViewModelMain()
        {
            dbgString = new debug();
            serial = new serialCAN(dbgString);
            updatePorts = new updatePortsCommand(serial.updatePortNames);
            connect = new connectCommand(serial.connect);
        }

    }
}
