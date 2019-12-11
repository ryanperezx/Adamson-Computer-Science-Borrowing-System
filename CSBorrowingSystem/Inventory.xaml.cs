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


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private ObservableCollection<itemCollection> LoadCollectionData()
        {
            SqlCeConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            list.Clear();
            using (SqlCeCommand cmd = new SqlCeCommand("SELECT it.itemID, it.itemName, it.itemType, it.brand, b.qtyBorrowed, b.itemCode, b.transactID, b.dateBorrowed, b.subjectName from tbl_Borrow b INNER JOIN tbl_Student s on s.studentID = b.studentID INNER JOIN tbl_Items it on it.itemCode = b.itemCode", conn))
            {
                using (DbDataReader reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string itemID = reader["itemID"].ToString();
                            string itemName = reader["itemName"].ToString();
                            string itemType = reader["itemType"].ToString();
                            string brand = reader["brand"].ToString();
                            string remarks = reader["remarks"].ToString();
                            int qtyBorrowed = Convert.ToInt32(reader["qtyBorrowed"]);


                            list.Add(new itemCollection
                            {
                                itemID = itemID,
                                itemName = itemName,
                                brand = brand,
                                itemType = itemType,
                                qty = qtyBorrowed,
                                remarks = remarks,
                            });

                        }
                    }
                }
            }
            return list;
        }
    }
}
