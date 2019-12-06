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
            DataContext = new ViewModelMain();
            
            addTable(readViewer);
            addTable(writeViewer);

            Table writeTable = new Table();
        }

        public void addTable(FlowDocumentScrollViewer parent)
        {
            FlowDocument flowDoc = new FlowDocument();
            parent.Document = flowDoc;
            Table table = new Table();
            table.CellSpacing = 10;
            table.Background = Brushes.White;
            flowDoc.Blocks.Add(table);
            table.SetValue(Grid.RowProperty, 1);
            for (int i = 0; i < canMsg.NUMATTRIBUTES; i++)
            {
                table.Columns.Add(new TableColumn());
                if (i % 2 == 0)
                    table.Columns[i].Background = Brushes.Beige;
                else
                    table.Columns[i].Background = Brushes.LightSteelBlue;
            }
            table.RowGroups.Add(new TableRowGroup());
            table.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = table.RowGroups[0].Rows[0];
            currentRow.Background = Brushes.Silver;
            currentRow.FontSize = 10;
            currentRow.FontWeight = System.Windows.FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Time"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Id"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Message"))));
        }
    }
}
