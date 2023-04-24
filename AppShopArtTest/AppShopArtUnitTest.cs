using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NUnit.Framework;
using System.Windows;
using AppShopArt;
using AppShopArt.View;
using System.Windows.Controls;
using Assert = NUnit.Framework.Assert;
using System.Threading;


namespace AppShopArtTest
{
    [Apartment(ApartmentState.STA)]
    [TestFixture]
    [TestClass]
    public class AppShopArtUnitTest
    {
        [TestMethod]
        public void LogInTest_ValidateValue() //проверка на вход с валидными значениями
        {
            //Arrange
            var logIn = new LogInWindow();
            var button = (Button)logIn.FindName("butLogIn");
            var login = (TextBox)logIn.FindName("enterLogin");
            var password = (PasswordBox)logIn.FindName("enterPassword");
            login.Text = "Admin";
            password.Password = "@dminPas";
            //Act
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            var loginCurrent = (TextBox)logIn.FindName("enterLogin");
            var passwordCurrent = (PasswordBox)logIn.FindName("enterPassword");
            //var edi
            //Assert
            Assert.True(loginCurrent.Text == App.login && passwordCurrent.Password == App.password);

        }

        [TestMethod]
        public void MainWindowTest_ExcelFileOpen() //проверка, что нужный эксель файл открывается
        {
            //Arrange
            var main = new MainWindow();
            main.Show();
            var catalog = new CatalogWindow();
            var button = (Button)catalog.FindName("openExcel");
            //Act
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            //Assert
            Assert.True(App.excelBook.Name == "Catalog.xlsx" && App.excelApp.Visible == true);
            App.excelApp.Visible = false;
        }

        [TestMethod]
        public void CatalogTest_Open() //проверка на скрытие главного меню при открытии каталога
        {
            //Arrange
            var main = new MainWindow();
            main.Show();
            var button = (Button)main.FindName("butPriceList");
            //Act
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            //Assert
            Assert.True(main.Visibility == Visibility.Hidden);
        }

        [TestMethod]
        public void OrderWindowTest_ConfirmOrder_InvalidValue() //проверка соверщения покупки с балансом меньше суммы заказа
        {
            //Arrange
            App.myMoney = 1;
            App.amountOrder = 100;
            var orderWin = new OrderWindow();
            var button = (Button)orderWin.FindName("butConfirm");
            //Act
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            //Assert
            Assert.False(App.lastTransact);
        }

        [TestMethod]
        public void OrderWindowTest_ConfirmOrderCheckPDF() //проверка формирования чека
        {
            App.myMoney = 100;
            App.amountOrder = 1;
            var orderWin = new OrderWindow();
            var button = (Button)orderWin.FindName("butConfirm");
            //Act
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            //Assert
            Assert.True(App.checkFlag);
        }
    }
}
