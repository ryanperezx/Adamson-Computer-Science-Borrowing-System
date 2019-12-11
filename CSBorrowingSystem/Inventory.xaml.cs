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
using System.Data.Common;
using System.Collections.ObjectModel;
using System.Data.SqlServerCe;

namespace CSBorrowingSystem
{
    /// <summary>s
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : UserControl
    {
        ObservableCollection<itemCollection> list = new ObservableCollection<itemCollection>();

        public Inventory()
        {
            InitializeComponent();
            DtgInventory.ItemsSource = list;
            LoadCollectionData();
        }



        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            string sMessageBoxText = "Are all rows accounted for?";
            string sCaption = "Delete Item(s)";
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult dr = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (dr)
            {
                case MessageBoxResult.Yes:
                    foreach (var row in list)
                    {
                        var check = list.FirstOrDefault(x => (x.isReturned == true));
                        if (check != null)
                        {
                            SqlCeConnection conn = DBUtils.GetDBConnection();
                            conn.Open();
                            using (SqlCeCommand cmd2 = new SqlCeCommand("DELETE from tbl_Items where itemCode = @itemCode", conn))
                            {
                                cmd2.Parameters.AddWithValue("@itemCode", row.itemCode);
                                try
                                {
                                    cmd2.ExecuteNonQuery();
                                }
                                catch (SqlCeException ex)
                                {
                                    MessageBox.Show("Error! Log has been updated with the error." + ex);
                                    return;
                                }
                            }
                            MessageBox.Show("Item(s) has been deleted!");
                        }
                        else
                        {
                            MessageBox.Show("There are no data to be returned!");
                        }
                    }
                    LoadCollectionData();

                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private ObservableCollection<itemCollection> LoadCollectionData()
        {
            SqlCeConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            list.Clear();
            using (SqlCeCommand cmd = new SqlCeCommand("SELECT itemCode, itemName, itemType, brand, remarks, quantityOnStock from tbl_Items", conn))
            {
                using (DbDataReader reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string itemCode = reader["itemCode"].ToString();
                            string itemName = reader["itemName"].ToString();
                            string itemType = reader["itemType"].ToString();
                            string brand = reader["brand"].ToString();
                            string remarks = reader["remarks"].ToString();
                            int quantityOnStock = Convert.ToInt32(reader["quantityOnStock"]);


                            list.Add(new itemCollection
                            {
                                itemCode = itemCode,
                                itemName = itemName,
                                brand = brand,
                                itemType = itemType,
                                qty = quantityOnStock,
                                remarks = remarks,
                            });

                        }
                    }
                }
            }
            return list;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addItem = new AddItem();
            addItem.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadCollectionData();
        }
    }
}
