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

namespace CSBorrowingSystem
{
    /// <summary>
    /// Interaction logic for AddSA.xaml
    /// </summary>
    public partial class AddSA : UserControl
    {
        public AddSA()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            SqlCeConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            
              //Check if record exists
              
              using (SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM tbl_User WHERE AdminID = @UserID",conn))
              {
                  cmd.Parameters.Clear();
                  cmd.Parameters.AddWithValue("@UserID", SAUserID.Text);
                  using (DbDataReader reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                  {

                        if (!reader.HasRows)
                        {



                            //insert values to database

                            using (SqlCeCommand cmd1 = new SqlCeCommand("INSERT INTO User(UserID, LName, FName, MName, Username, UserPW, UserLevel) VALUES(@UserID, @LName, @FName, @MName, @Username, @UserPW, @UserLevel) ", conn))
                            {
                                //get values and insert to query string
                                cmd1.Parameters.Clear();
                                cmd1.Parameters.AddWithValue("@UserID", SAUserID.Text);
                                cmd1.Parameters.AddWithValue("@LName", SALname.Text);
                                cmd1.Parameters.AddWithValue("@FName", SAFname.Text);
                                cmd1.Parameters.AddWithValue("@MName", SAMname.Text);
                                cmd1.Parameters.AddWithValue("@Username", SAUname.Text);
                                cmd1.Parameters.AddWithValue("@UserPW", SAPass.Text);
                                cmd1.Parameters.AddWithValue("@UserLevel", SAUlevel.Text);

                                try
                                {
                                    cmd.ExecuteNonQuery();
                                    string sMessageBoxText = "Successfully Created!";
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

                        }
                        else
                        {
                            string sMessageBoxText = "Record Already Exist!";
                            string sCaption = "Notification";
                            MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                            MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                        }
                            reader.Close();
                            reader.Dispose();
                  }
            

                  //For Log
                  //string fLog = "added a Student Assistant";
                  //query
              }
              conn.Close();

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
