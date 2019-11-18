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
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        ObservableCollection<itemCollection> list = new ObservableCollection<itemCollection>();
        public Home()
        {
            InitializeComponent();
            dgList.ItemsSource = list;
            LoadCollectionData();
        }

        private ObservableCollection<itemCollection> LoadCollectionData()
        {
            SqlCeConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            list.Clear();
            using (SqlCeCommand cmd = new SqlCeCommand("SELECT (br.fName + br.mName + br.lName) as fullName, it.itemName, it.brand, pr.qty, pr.itemCode, pr.transactNo, pr.dateBorrowed, pr.subject, pr.schedule, pr.remarks from tbl_pendingReturn pr INNER JOIN tbl_borrower br on pr.idNo = br.idNo INNER JOIN tbl_items it on it.itemCode = pr.itemCode where pr.status = 'no'", conn))
            {
                using (DbDataReader reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string fullName = reader["fullName"].ToString();
                            string itemName = reader["itemName"].ToString();
                            string brand = reader["brand"].ToString();
                            string dateBorrowed = reader["dateBorrowed"].ToString();
                            string subject = reader["subject"].ToString();
                            string schedule = reader["schedule"].ToString();
                            string remarks = reader["remarks"].ToString();
                            string itemCode = reader["itemCode"].ToString();
                            string transactNo = reader["transactNo"].ToString();
                            int qty = Convert.ToInt32(reader["qty"]);


                            list.Add(new itemCollection
                            {
                                fullName = fullName,
                                itemName = itemName,
                                brand = brand,
                                itemCode = itemCode,
                                transactNo = transactNo,
                                qty = qty,
                                dateBorrowed = dateBorrowed,
                                subject = subject,
                                schedule = schedule,
                                status = remarks
                            });

                        }
                    }
                }
            }
           return list;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            string sMessageBoxText = "Are all rows accounted for?";
            string sCaption = "Return borrowed items";
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
                            using (SqlCeCommand cmd = new SqlCeCommand("UPDATE tbl_items set qty = qty + @qty where itemCode = @itemCode", conn))
                            {
                                cmd.Parameters.AddWithValue("@qty", row.qty);
                                cmd.Parameters.AddWithValue("@itemCode", row.itemCode);
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                    using (SqlCeCommand cmd1 = new SqlCeCommand("UPDATE tbl_pendingReturn set status = 'returned' where transactNo = @transactNo", conn))
                                    {
                                        cmd1.Parameters.AddWithValue("@transactNo", row.transactNo);
                                        try
                                        {
                                            cmd1.ExecuteNonQuery();
                                            MessageBox.Show("Borrowed item(s) has been returned!");
                                            LoadCollectionData();
                                        }
                                        catch (SqlCeException ex)
                                        {
                                            MessageBox.Show("Error! Log has been updated with the error.");
                                            return;
                                        }
                                    }
                                }
                                catch (SqlCeException ex)
                                {
                                    MessageBox.Show("Error! Log has been updated with the error.");
                                    return;
                                }
                            }
                            MessageBox.Show("Existing!");
                        }
                        else
                        {
                            MessageBox.Show("Not existing!");
                        }
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }

        }
    }
}
