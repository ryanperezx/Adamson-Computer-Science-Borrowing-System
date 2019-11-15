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
        }

        private ObservableCollection<itemCollection> LoadCollectionData()
        {
            SqlCeConnection conn = new SqlCeConnection();
            conn.Open();
            list.Clear();
            using (SqlCeCommand cmd = new SqlCeCommand("SELECT (br.fName + br.mName + br.lName) as fullName, it.itemName, it.brand, pr.dateBorrowed, pr.subject, pr.schedule, pr.remarks from tbl_pendingReturn pr INNER JOIN tbl_borrower br on pr.idNo = br.idNo INNER JOIN tbl_items it on it.itemCode = pr.itemCode", conn))
            {
                using (DbDataReader reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string fullName = reader["fullName"].ToString();
                            string itemName = reader["it.itemName"].ToString();
                            string brand = reader["it.brand"].ToString();
                            string dateBorrowed = reader["pr.dateBorrowed"].ToString();
                            string subject = reader["pr.subject"].ToString();
                            string schedule = reader["pr.schedule"].ToString();
                            string remarks = reader["pr.remarks"].ToString();

                            list.Add(new itemCollection
                            {
                                fullName = fullName,
                                itemName = itemName,
                                brand = brand,
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

        }
    }
}
