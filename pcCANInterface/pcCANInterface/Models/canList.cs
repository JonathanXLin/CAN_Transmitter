using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcCANInterface
{
    

    public abstract class canList : ReactiveObject
    {
        public static int LISTLENGTH = 32;
        private ObservableCollection<canMsg> messages;
        public ObservableCollection<canMsg> Messages
        {
            get => messages;
            set => this.RaiseAndSetIfChanged(ref messages, value);
        }
        public canList()
        {
            messages = new ObservableCollection<canMsg>();
        }

        public void addMessage(canMsg msg, debug dbg)
        {
            Console.WriteLine(Messages.Count);
            //TODO: Group messages with same ID
            //current it throws out the previous message with the same id, rather than storing it in a list
            //this can be a new feature
            for(int i = 0; i < Messages.Count; i++)
            {
                if(Messages[i].Id==msg.Id)
                {
                    App.Current.Dispatcher.Invoke((Action)delegate //go to the thread that the observable collection lives in 
                    {
                        Messages[i] = msg;
                    }); //update message of that ID
                    return;
                }
            }
            //add a new messages
            if(Messages.Count == canList.LISTLENGTH)
            {
                dbg.setMessage(debug.tooManyMessages);
            }
            else
            {
                App.Current.Dispatcher.Invoke((Action)delegate //go to the thread that the observable collection lives in 
                {
                    Messages.Add(msg);
                });
            }
        }
    }
}
