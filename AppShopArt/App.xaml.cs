using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace AppShopArt
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string login = "Admin";
        public static string password = "@dminPas";

        public static string pathExe = "C:\\D-drive-67390\\Files\\Учеба\\2курс\\МДК01.01\\AppShopArt\\AppShopArt\\bin\\Debug\\";
        public static string fileCatalog = "C:\\D-drive-67390\\Files\\Учеба\\2курс\\МДК01.01\\AppShopArt\\AppShopArt\\bin\\Debug\\Catalog.xlsx";

        public static Excel.Application excelApp;
        public static Excel.Workbook excelBook;
        public static Excel.Worksheet excelSheet;
        public static Excel.Range excelCells;

        public static Word.Application wordApp;
        public static Word.Document wordDoc;
        public static Word.Paragraph wordPar;
        public static Word.Range wordRange;
        public static bool checkFlag = false;

        public static double amountOrder = 0;
        public static double myMoney = 0;
        public static bool lastTransact = true;
        public static List<Classes.ItemInOrder> listItemInOrder = new List<Classes.ItemInOrder>();
    }
}
