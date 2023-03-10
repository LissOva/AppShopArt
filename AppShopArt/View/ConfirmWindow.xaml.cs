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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace AppShopArt.View
{
    /// <summary>
    /// Логика взаимодействия для ConfirmWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : Window
    {
        OrderWindow orderWindow;
        public double allSum = 0, orderSum = 0;
        public ConfirmWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)//закрытие окна, возвращение в окно заказа
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window is OrderWindow)
                {
                    window.Show();
                }
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            orderWindow = this.Owner as OrderWindow;
            //orderWindow = (OrderWindow)this.Owner;
            this.orderSum = orderWindow.orderSum;
            this.allSum =orderWindow.allSum;
            orderSumText.Text = "Cумма заказа: " + orderSum.ToString();
            allSumText.Text = "На карте: " + allSum.ToString();
        }

        private void butBack_Click(object sender, RoutedEventArgs e)//вернуться в окно заказа
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window is OrderWindow)
                {
                    this.Close();
                    window.Show();
                }
            }
        }
    }
}
