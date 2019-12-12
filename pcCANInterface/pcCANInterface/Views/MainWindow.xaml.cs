using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using pcCANInterface.ViewModels;

namespace pcCANInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            DataContext = new ViewModelMain(this);

            //hide reactive properties from table
            

            //addTable(readViewer);
            //addTable(writeViewer);

            //Table writeTable = new Table();
        }

        //private void createMsgGrid(Grid parent, canList list)
        //{
        //    DataGrid dataGrid = new DataGrid();
        //    Grid.SetRow(dataGrid, 1);
        //    parent.Children.Add(dataGrid);
        //    var data = list;
        //    dataGrid.AutoGenerateColumns = false;
        //    for(int i = 0; i < canMsg.NUMATTRIBUTES; i++)
        //    {
        //        var col = new DataGridColumn();
        //        var binding = new Binding 
        //    }
        //}

        //format datagrid
        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Changing" || e.PropertyName=="Changed" || e.PropertyName=="ThrownExceptions")
            {
                e.Column = null;
            }
            if(e.PropertyName == "Time")
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "HH:mm:ss:ff";
            }
        }
    }
}
