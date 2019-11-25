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
            using (SqlCeCommand cmd = new SqlCeCommand("SELECT (s.fName + ' ' + s.mName + ' ' + s.lName) as fullName, it.itemName, it.brand, b.qtyBorrowed, b.itemCode, b.transactID, b.dateBorrowed, b.subjectName from tbl_Borrow b INNER JOIN tbl_Student s on s.studentID = b.studentID INNER JOIN tbl_Items it on it.itemCode = b.itemCode", conn))
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
                            string subject = reader["subjectName"].ToString();
                            string itemCode = reader["itemCode"].ToString();
                            string transactNo = reader["transactID"].ToString();
                            int qtyBorrowed = Convert.ToInt32(reader["qtyBorrowed"]);


                            list.Add(new itemCollection
                            {
                                fullName = fullName,
                                itemName = itemName,
                                brand = brand,
                                itemCode = itemCode,
                                transactNo = transactNo,
                                qty = qtyBorrowed,
                                dateBorrowed = dateBorrowed,
                                subject = subject
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
                            using (SqlCeCommand cmd = new SqlCeCommand("UPDATE tbl_Items set quantityOnStock = quantityOnStock + @qty where ItemCode = @itemCode", conn))
                            {
                                cmd.Parameters.AddWithValue("@qty", row.qty);
                                cmd.Parameters.AddWithValue("@itemCode", row.itemCode);
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                    using (SqlCeCommand cmd1 = new SqlCeCommand("INSERT into tbl_Return (StudentId, ItemCode, BorrowID, DateReturned, ReceivedBy, Remarks, qtyReturned) SELECT StudentID, ItemCode, BorrowID, @dateReturned, @ReceivedBy, @Remarks, @qtyReturned) from tbl_Borrow where transactID = @transactID", conn))
                                    {
                                        cmd1.Parameters.AddWithValue("@transactID", row.transactNo);
                                        cmd1.Parameters.AddWithValue("@dateReturned", DateTime.Today);
                                        cmd1.Parameters.AddWithValue("@receivedBy", "dummy");
                                        cmd1.Parameters.AddWithValue("@qtyReturned", row.qty);
                                        if (string.IsNullOrEmpty(row.remarks))
                                        {
                                            cmd1.Parameters.AddWithValue("@remarks", "none");
                                        }
                                        else
                                        {
                                            cmd1.Parameters.AddWithValue("@remarks", row.remarks);
                                        }
                                        try
                                        {
                                            cmd1.ExecuteNonQuery();
                                            using(SqlCeCommand cmd2 = new SqlCeCommand("DELETE from tbl_Borrow where transactID = @transactID",conn))
                                            {
                                                cmd2.Parameters.AddWithValue("@transactID", row.transactNo);
                                                try
                                                {
                                                    cmd2.ExecuteNonQuery();
                                                }
                                                catch(SqlCeException ex)
                                                {
                                                    MessageBox.Show("Error! Log has been updated with the error." + ex);
                                                    return;
                                                }
                                            }
                                            MessageBox.Show("Borrowed item(s) has been returned!");
                                            LoadCollectionData();
                                        }
                                        catch (SqlCeException ex)
                                        {
                                            MessageBox.Show("Error! Log has been updated with the error." + ex);
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
                        }
                        else
                        {
                            MessageBox.Show("There are no data to be returned!");
                        }
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }

        }
    }
}
