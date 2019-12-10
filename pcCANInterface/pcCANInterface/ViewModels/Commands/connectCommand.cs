using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace pcCANInterface.ViewModels.Commands
{
    public class connectCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<debug> execute;
        public connectCommand(Action<debug> newExecute)
        {
            execute = newExecute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var param = parameter as debug;
            execute.Invoke(param);
        }
    }
}
