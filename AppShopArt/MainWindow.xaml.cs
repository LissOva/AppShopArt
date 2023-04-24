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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using AppShopArt.View;

namespace AppShopArt
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Application.ResourceAssembly = typeof(MainWindow).Assembly;
            InitializeComponent();
            Random random= new Random();
            int n = random.Next(1000, 5000);
            App.myMoney = Convert.ToDouble(n);
            try 
            {
                App.excelApp = new Excel.Application();
                App.excelApp.Visible = false;
            }
            catch {
                MessageBox.Show("Нет Excel");
                this.Close();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.excelApp.Quit();
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(App.excelApp);
            GC.Collect();
        }
        private void butExit_Click(object sender, RoutedEventArgs e) // Закрытие приложения
        {
            App.excelApp.Quit();
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(App.excelApp);
            GC.Collect();
            this.Close();
        }

        private void butPriceList_Click(object sender, RoutedEventArgs e) // Показать каталог
        {
            View.CatalogWindow catalogWindow = new View.CatalogWindow();
            this.Hide();
            catalogWindow.Show();
        }

        private void butOrder_Click(object sender, RoutedEventArgs e) // Показать заказ
        {
            Random random = new Random();
            double sum = random.Next(0, 50000);
            View.OrderWindow orderWindow = new View.OrderWindow(sum);
            this.Hide();
            orderWindow.Show();
        }

        private void butManager_Click(object sender, RoutedEventArgs e) // Показать редактор каталога
        {
            View.LogInWindow logInWindow = new View.LogInWindow();
            logInWindow.Show();
        }
    }
}
