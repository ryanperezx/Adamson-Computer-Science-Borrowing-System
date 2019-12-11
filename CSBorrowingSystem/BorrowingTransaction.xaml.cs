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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace CSBorrowingSystem
{
    /// <summary>
    /// Interaction logic for BorrowingTransaction.xaml
    /// </summary>
    public partial class BorrowingTransaction : UserControl
    {
        ObservableCollection<itemCollection1> list = new ObservableCollection<itemCollection1>();
        public BorrowingTransaction()
        {

            InitializeComponent();
            DtgEquipment.ItemsSource = list;
            LoadCollectionData();
            

        }

        private ObservableCollection<itemCollection1> LoadCollectionData()
        {
            SqlCeConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            list.Clear();
            using (SqlCeCommand cmd = new SqlCeCommand("SELECT it.itemName, it.QuantityOnStock, it.brand, b.qtyBorrowed, b.itemCode, b.subjectName from tbl_Borrow b INNER JOIN tbl_Items it on it.itemCode = b.itemCode", conn))
            {
                using (DbDataReader reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string itemName = reader["itemName"].ToString();
                            string brand = reader["brand"].ToString();
                            string QuantityOnStock = reader["QuantityOnStock"].ToString();
                            string subject = reader["subjectName"].ToString();
                            string itemCode = reader["itemCode"].ToString();
                            int qtyBorrowed = Convert.ToInt32(reader["qtyBorrowed"]);


                            list.Add(new itemCollection1
                            {
                                itemName = itemName,
                                brand = brand,
                                itemCode = itemCode,
                                qty = qtyBorrowed,
                                QuantityOnStock = QuantityOnStock,
                                subject = subject
                            });

                        }
                    }
                }
            }
            return list;
        }


        private void btnProceed_Click(object sender, RoutedEventArgs e)
        {
            SqlCeConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            using (SqlCeCommand cmd1 = new SqlCeCommand("INSERT INTO tbl_Borrow(transactID, FullName, studentID, Professor, GroupNumber, itemCode, DateBorrowed, subjectName, semester, releasedBy, sy, qtyBorrowed) VALUES(@transactID, @FullName, @studentID, @Professor, @GroupNumber, @itemCode, @DateBorrowed, @subjectName, @semester, @releasedBy, @sy, @qtyBorrowed)", conn))
            {

                //get values and insert to query string
                cmd1.Parameters.Clear();
                cmd1.Parameters.AddWithValue("@transactID", "00"); //get number after two zeros and add 1
                cmd1.Parameters.AddWithValue("@FullName", txtFullName.Text);
                cmd1.Parameters.AddWithValue("@studentID", txtStudNo.Text);
                cmd1.Parameters.AddWithValue("@subjectName", txtSubject.Text);
                cmd1.Parameters.AddWithValue("@Professor", txtProf.Text);
                cmd1.Parameters.AddWithValue("@GroupNumber", txtGroupNo.Text);
                cmd1.Parameters.AddWithValue("@DateBorrowed", DateTime.Now.Date.ToString("MM/dd/yyyy"));
                cmd1.Parameters.AddWithValue("@semester", "n/a"); //not in UI
                cmd1.Parameters.AddWithValue("@releasedBy", "n/a"); //not in UI
                cmd1.Parameters.AddWithValue("@sy", "n/a"); //not in UI
                cmd1.Parameters.AddWithValue("@qtyBorrowed", "0"); //get from data grid
                cmd1.Parameters.AddWithValue("@itemCode", "0"); //get from data grid


                try
                {
                    cmd1.ExecuteNonQuery();
                    string sMessageBoxText = "Borrowed Successfully!";
                    string sCaption = "Notification";
                    MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Information;

                    MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                }
                catch (SqlCeException ex)
                {
                    MessageBox.Show("Error! Log has been updated with the error.");
                    return;
                }
            }

            conn.Close();

        }



        private void DtgEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
