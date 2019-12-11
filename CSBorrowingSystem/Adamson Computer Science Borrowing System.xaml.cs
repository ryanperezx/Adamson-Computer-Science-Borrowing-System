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
using System.Data.SqlServerCe;


namespace CSBorrowingSystem
{
    /// <summary>
    /// Interaction logic for Adamson_Computer_Science_Borrowing_System.xaml
    /// </summary>
    public partial class Adamson_Computer_Science_Borrowing_System : Window
    {
        public Adamson_Computer_Science_Borrowing_System()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            int zOrder = Panel.GetZIndex(Accounts) + 1;
            int zOrder2 = Panel.GetZIndex(BTransaction) + 1;
            int zOrder3 = Panel.GetZIndex(Inventory) + 1;
            int zOrder4 = Panel.GetZIndex(Reports) + 1;
            int zOrder5 = zOrder + zOrder2 + zOrder3 + zOrder4;

            Panel.SetZIndex(Home, zOrder5);
        }

        private void BtnBTransact_Click(object sender, RoutedEventArgs e)
        {
            int zOrder = Panel.GetZIndex(Accounts) + 1;
            int zOrder2 = Panel.GetZIndex(Home) + 1;
            int zOrder3 = Panel.GetZIndex(Inventory) + 1;
            int zOrder4 = Panel.GetZIndex(Reports) + 1;
            int zOrder5 = zOrder + zOrder2 + zOrder3 + zOrder4;

            Panel.SetZIndex(BTransaction, zOrder5);
        }

        private void BtnInventory_Click(object sender, RoutedEventArgs e)
        {
            int zOrder = Panel.GetZIndex(Accounts) + 1;
            int zOrder2 = Panel.GetZIndex(BTransaction) + 1;
            int zOrder3 = Panel.GetZIndex(Home) + 1;
            int zOrder4 = Panel.GetZIndex(Reports) + 1;
            int zOrder5 = zOrder + zOrder2 + zOrder3 + zOrder4;

            Panel.SetZIndex(Inventory, zOrder5);
        }

        private void BtnAccounts_Click(object sender, RoutedEventArgs e)
        {
            int zOrder = Panel.GetZIndex(Inventory) + 1;
            int zOrder2 = Panel.GetZIndex(BTransaction) + 1;
            int zOrder3 = Panel.GetZIndex(Home) + 1;
            int zOrder4 = Panel.GetZIndex(Reports) + 1;
            int zOrder5 = zOrder + zOrder2 + zOrder3 + zOrder4;

            Panel.SetZIndex(Accounts, zOrder5);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string sMessageBoxText = "Do you want to exit the application?";
            string sCaption = "Exit";
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult dr = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (dr)
            {
                case MessageBoxResult.Yes:
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void btnReportsClick(object sender, RoutedEventArgs e)
        {
            int zOrder = Panel.GetZIndex(Inventory) + 1;
            int zOrder2 = Panel.GetZIndex(BTransaction) + 1;
            int zOrder3 = Panel.GetZIndex(Home) + 1;
            int zOrder4 = Panel.GetZIndex(Accounts) + 1;
            int zOrder5 = zOrder + zOrder2 + zOrder3 + zOrder4;

            Panel.SetZIndex(Reports, zOrder5);
        }
    }
}
