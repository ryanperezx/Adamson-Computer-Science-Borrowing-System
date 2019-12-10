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
        public BorrowingTransaction()
        {
            InitializeComponent();
            FillDataGrid();
        }

        private void btnProceed_Click(object sender, RoutedEventArgs e)
        {
            SqlCeConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            //insert 

            List<checkedBoxIte> item = new List<checkedBoxIte>();

                using (SqlCeCommand cmd1 = new SqlCeCommand("INSERT INTO Borrow(TransactID, FullName, StudentID, Professor, GroupNumber, ItemCode, DateBorrowed, SubjectName, Semester, ReleasedBy, SY, QtyBorrowed) VALUES(@TransactID, @FullName, @StudentID, @Professor, @GroupNumber, @ItemCode, @DateBorrowed, @SubjectName, @Semester, @ReleasedBy, @SY, @QtyBorrowed)", conn))
                {

                for (int i = 0; i < 5; i++)
                {
                    checkedBoxIte ite = new checkedBoxIte();
                    ite.Itm = i.ToString();
                    item.Add(ite);
                }
                DtgEquipment.ItemsSource = item;
                                //get values and insert to query string
                                cmd1.Parameters.Clear();
                                cmd1.Parameters.AddWithValue("@TransactID", "TRNS00"); 
                                cmd1.Parameters.AddWithValue("@FullName", txtFullName.Text); 
                                cmd1.Parameters.AddWithValue("@StudentID", txtStudNo.Text);
                                cmd1.Parameters.AddWithValue("@SubjectName", txtSubject.Text);
                                cmd1.Parameters.AddWithValue("@Professor", txtProf.Text);     //not in the Borrow DB
                                cmd1.Parameters.AddWithValue("@GroupNumber", txtGroupNo.Text);   // not in the Borrow DB
                                cmd1.Parameters.AddWithValue("@ItemCode", item); 
                                cmd1.Parameters.AddWithValue("@DateBorrowed", DateTime.Now.Date.ToString("MM/dd/yyyy"));  //insert date
                                
                       
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

        private void FillDataGrid()
        {
            

            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            
            {
                CmdString = "SELECT ItemCode, ItemName, ItemType, QuantityOnStock, Brand FROM Tbl_Items";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                DtgEquipment.ItemsSource = dt.DefaultView;
            }
        }

        public class checkedBoxIte    //Datagrid checkbox
        {
            
            public bool CBox { get; set; }
            public string Itm { get; set; }
        }
        private void DtgEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
