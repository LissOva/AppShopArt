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

namespace AppShopArt.View
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        Random random = new Random();
        public double orderSum = 0, allSum = 0;
        public OrderWindow()
        {
            InitializeComponent();
        }

        public OrderWindow(double sum)//перегрузка на передачу данных суммы
        {
            InitializeComponent();
            this.orderSum = sum;
            orderSumText.Text = "Сумма заказа: " + orderSum.ToString();
            this.allSum = random.Next(100000);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)//закрыть окно, вернуть ся в главное меню
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window is MainWindow)
                {
                    window.Show();
                }
            }
        }

        private void butConfirm_Click(object sender, RoutedEventArgs e)//открыть окно подтверждения
        {
            View.ConfirmWindow confirmWindow = new View.ConfirmWindow();
            confirmWindow.Owner = this;
            this.Hide();
            confirmWindow.Show();
        }

        private void butBack_Click(object sender, RoutedEventArgs e)//вернуться в главное меню
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
    }
}
