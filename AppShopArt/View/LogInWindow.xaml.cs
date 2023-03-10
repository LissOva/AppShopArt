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
    /// Логика взаимодействия для LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
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
        private void butLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (enterLogin.Text == App.login && enterPassword.Password == App.password)
            {
                View.EditCatalogWindow editCatalogWindow = new View.EditCatalogWindow();
                this.Close();
                editCatalogWindow.Show();
            }
        }
    }
}
