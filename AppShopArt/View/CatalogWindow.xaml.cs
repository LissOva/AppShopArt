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
using System.Windows.Shapes;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace AppShopArt.View
{
    /// <summary>
    /// Логика взаимодействия для CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        List<string> listSec;
        List<Classes.Item> listIt;
        private void GetSheetsName()
        {
            List<string> listSec = new List<string>();
            if (File.Exists(App.fileCatalog))
            {
                App.excelBook = App.excelApp.Workbooks.Open(App.fileCatalog);
                listSection.Items.Clear();
                int n = App.excelBook.Worksheets.Count;
                foreach (Excel.Worksheet it in App.excelBook.Worksheets)
                {
                    listSec.Add(it.Name);
                }
                listSection.ItemsSource = listSec;
            }
            else
            {
                MessageBox.Show("Неудалось найти файл!", "Файл не найден", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetItems(string nameSec)
        {
            App.excelSheet = (Excel.Worksheet)App.excelBook.Worksheets.get_Item(nameSec);
            //App.excelCells = App.excelSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);
            App.excelCells = App.excelSheet.UsedRange;
            listIt = new List<Classes.Item>(); 
            string paint = "Краски", pastel = "Пастель";
            if (nameSec.Contains(paint)) { 
                for (int i = 2; i <= App.excelCells.Rows.Count; ++i) {
                    Classes.Item item = new Classes.Item();
                    item.name = App.excelCells[i,1].Value2.ToString();
                    item.size = App.excelCells[i,2].Value2.ToString();
                    item.price = Convert.ToDouble(App.excelCells[i,3].Value2);
                    item.level = App.excelCells[i,4].Value2.ToString();
                    listIt.Add(item);
                }
            }
            else if (nameSec.Contains(pastel))
            {
                for (int i = 2; i <= App.excelCells.Rows.Count; ++i)
                {
                    Classes.Item item = new Classes.Item();
                    item.name = App.excelCells[i, 1].Value2.ToString();
                    item.price = Convert.ToDouble(App.excelCells[i, 2].Value2);
                    item.level = App.excelCells[i, 3].Value2.ToString();
                    listIt.Add(item);
                }
            }
            else
            {
                for (int i = 2; i <= App.excelCells.Rows.Count; ++i)
                {
                    Classes.Item item = new Classes.Item();
                    item.name = App.excelCells[i, 1].Value2.ToString();
                    item.price = Convert.ToDouble(App.excelCells[i, 2].Value2);
                    listIt.Add(item);
                }
            }
            listItem.ItemsSource= listIt;
        }
        public CatalogWindow()
        {
            InitializeComponent();
            GetSheetsName();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window is MainWindow)
                {
                    window.Show();
                }
            }
        }

        private void butBack_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window is MainWindow)
                {
                    this.Close();
                    window.Show();
                }
            }
        }

        private void openExcel_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(App.fileCatalog))
            {
                App.excelBook = App.excelApp.Workbooks.Open(App.fileCatalog);
                App.excelApp.Visible = true;
            }
            else
            {
                MessageBox.Show("Неудалось найти файл!", "Файл не найден", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listSection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetItems(listSection.SelectedItem.ToString());
        }
    }
}
