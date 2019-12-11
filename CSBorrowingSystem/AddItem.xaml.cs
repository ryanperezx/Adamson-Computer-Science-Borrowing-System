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
using System.Data.Common;
using System.Collections.ObjectModel;
using System.Data.SqlServerCe;

namespace CSBorrowingSystem
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        public AddItem()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SqlCeConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            using(SqlCeCommand cmd = new SqlCeCommand("INSERT into tbl_Items (ItemCode, ItemName, ItemType, QuantityOnStock, Brand, Remarks) VALUES (@itemCode, @itemName, @itemType, @qty, @itemBrand, @remarks)", conn))
            {
                cmd.Parameters.AddWithValue("@itemCode", txtItemCode.Text);
                cmd.Parameters.AddWithValue("@itemBrand", txtItemBrand.Text);
                cmd.Parameters.AddWithValue("@itemType", cmbItemType.Text);
                cmd.Parameters.AddWithValue("@itemName", txtItemName.Text);
                cmd.Parameters.AddWithValue("@qty", txtQty.Text);
                cmd.Parameters.AddWithValue("@remarks", txtRemarks.Text);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item added successfully");
                    emptyFields();
                }
                catch(SqlCeException ex)
                {
                    MessageBox.Show("Error! Log has been updated with the error." + ex);
                    return;
                }
            }
        }

        private void emptyFields()
        {
            txtItemBrand.Text = null;
            txtItemCode.Text = null;
            txtItemName.Text = null;
            txtQty.Text = null;
            txtRemarks.Text = null;
        }
    }
}
