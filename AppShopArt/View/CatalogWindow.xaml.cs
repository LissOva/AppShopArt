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
        public CatalogWindow()
        {
            InitializeComponent();
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
    }
}
