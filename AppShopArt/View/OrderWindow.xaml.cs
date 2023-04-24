using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
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
using Button = System.Windows.Controls.Button;
using Window = System.Windows.Window;
using Word = Microsoft.Office.Interop.Word;

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
            this.allSum = sum;
            //orderSumText.Text = "На карте: " + allSum.ToString();
            this.orderSum = random.Next(100000);
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            gridOrder.Items.Refresh();
            myCartText.Text = App.myMoney.ToString();
            amountFinish.Text = App.amountOrder.ToString();
            gridOrder.ItemsSource = App.listItemInOrder;
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
            Word.Application wordApp;
            Word.Document wordDoc;
            Word.Paragraph wordPar;
            Word.Range wordRange;
            Word.InlineShape wordShape;
            Word.Table wordTable;
            App.checkFlag = false;
            try
            {
                wordApp = new Word.Application();
                wordApp.Visible = false;
            }
            catch
            {
                MessageBox.Show("Товарный чек в Word создать не удалось.", "Чек", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (App.myMoney < App.amountOrder) {
                MessageBox.Show("У вас недостаточно средств.", "Недостаточно средств", MessageBoxButton.OK, MessageBoxImage.Error);
                App.lastTransact = false;
                return;
            }
            App.lastTransact = true;
            App.myMoney -= App.amountOrder;
            DateTime dtNow = DateTime.Now;

            wordDoc = wordApp.Documents.Add();
            wordDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait;
            wordDoc.Content.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordDoc.Content.Font.Size = 14;

            wordPar = wordDoc.Paragraphs.Add();     
            wordRange = wordPar.Range;      
            wordShape = wordDoc.InlineShapes.AddPicture(App.pathExe +@"\image\painting-tools.png", Type.Missing, Type.Missing, wordRange);
            wordShape.Width = 70;
            wordShape.Height = 70;

            wordPar = wordDoc.Paragraphs.Add();
            wordRange = wordPar.Range;
            wordRange.Text = "Чек - " + dtNow.Ticks.ToString();
            wordRange.Font.Color = Word.WdColor.wdColorBlack;
            wordRange.Font.Size = 25;
            wordRange.InsertParagraphAfter();

            wordPar = wordDoc.Paragraphs.Add();
            wordRange = wordPar.Range;
            wordRange.Text = "Дата заказа: " + dtNow.ToLongDateString();
            wordRange.Font.Color = Word.WdColor.wdColorBlack;
            wordRange.Font.Size = 16;
            wordRange.InsertParagraphAfter();

            wordPar = wordDoc.Paragraphs.Add();
            wordRange = wordPar.Range;   
            wordTable = wordDoc.Tables.Add(wordRange, App.listItemInOrder.Count + 1, 4) ;
            wordTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleDouble;
            Word.Range cellRange;
            for (int col = 1; col <= 4; col++)
            {
                cellRange = wordTable.Cell(1, col).Range;
                cellRange.Font.Size = 14;
                cellRange.Text = gridOrder.Columns[col - 1].Header.ToString();    
            }
            for (int row = 2; row <= App.listItemInOrder.Count; row++)
            {
                cellRange = wordTable.Cell(row, 1).Range;
                cellRange.Font.Size = 14;
                cellRange.Text = App.listItemInOrder[row - 2].name;
                cellRange = wordTable.Cell(row, 2).Range;
                cellRange.Font.Size = 14;
                cellRange.Text = App.listItemInOrder[row - 2].price.ToString();
                cellRange = wordTable.Cell(row, 3).Range;
                cellRange.Font.Size = 14;
                cellRange.Text = App.listItemInOrder[row - 2].count.ToString();
                cellRange = wordTable.Cell(row, 4).Range;
                cellRange.Text = App.listItemInOrder[row - 2].amount.ToString();
            }

            wordPar = wordDoc.Paragraphs.Add();
            wordRange = wordPar.Range;
            wordRange.Text = "Итого: " + amountFinish.Text.ToString();
            wordRange.Font.Color = Word.WdColor.wdColorBlack;
            wordRange.InsertParagraphAfter();

            wordDoc.Saved = true;
            string pathDoc = @"C:\Users\lizik\OneDrive\Рабочий стол\" + "Чек" + dtNow.Year + "_" + dtNow.Month + "_" + dtNow.Day + "_" +dtNow.Ticks;

            wordDoc.SaveAs(pathDoc + ".pdf", Word.WdExportFormat.wdExportFormatPDF);
            wordDoc.Close(true, null, null);
            wordDoc = null;

            MessageBox.Show("Чек создан на вашем рабочем столе", "Чек", MessageBoxButton.OK, MessageBoxImage.Information);
            App.checkFlag = true;
            wordApp.Quit();
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(wordApp);
            GC.Collect();

            //View.ConfirmWindow confirmWindow = new View.ConfirmWindow();
            //confirmWindow.Owner = this;
            //this.Hide();
            //confirmWindow.Show();
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

        private void actionWithItem(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Classes.ItemInOrder item = button.DataContext as Classes.ItemInOrder;
            int index = App.listItemInOrder.FindIndex(x => x.name == item.name);
            switch (button.Name.ToString())
            {
                case "add":
                    App.listItemInOrder[index].count++;
                    App.listItemInOrder[index].amount += App.listItemInOrder[index].price;
                    App.amountOrder += App.listItemInOrder[index].price;
                    amountFinish.Text = App.amountOrder.ToString();
                    break;
                case "sub":
                    if (App.listItemInOrder[index].count - 1 > 0)
                    {
                        App.listItemInOrder[index].count--;
                        App.listItemInOrder[index].amount -= App.listItemInOrder[index].price;
                        App.amountOrder -= App.listItemInOrder[index].price;
                        amountFinish.Text = App.amountOrder.ToString();
                    }
                    else
                    {
                        App.amountOrder -= App.listItemInOrder[index].price;
                        amountFinish.Text = App.amountOrder.ToString();
                        App.listItemInOrder.Remove(App.listItemInOrder[index]);
                    }
                    break;
                case "del":
                    App.amountOrder -= App.listItemInOrder[index].amount;
                    amountFinish.Text = App.amountOrder.ToString();
                    App.listItemInOrder.Remove(App.listItemInOrder[index]);
                    break;
            }
            gridOrder.Items.Refresh();
        }
    }
}
