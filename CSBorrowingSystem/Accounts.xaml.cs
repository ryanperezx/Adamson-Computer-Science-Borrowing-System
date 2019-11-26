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
    /// Interaction logic for Accounts.xaml
    /// </summary>
    public partial class Accounts : UserControl
    {
        public Accounts()
        {
            InitializeComponent();
            SAList_Load();
        }

        private void AddSA_Click(object sender, RoutedEventArgs e)
        {
            AddSAForm.Visibility = Visibility.Visible;
        }

        private void SAList_Load()
        {
          
             SqlCeConnection conn = DBUtils.GetDBConnection();
               conn.Open();
              int a = 0;
              using (SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM tbl_User Where UserLevel = 'Student Assistant'", conn))
                {
                    using (DbDataReader reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string Lname = reader["LName"].ToString();
                                string Fname = reader["FName"].ToString();
                                string Mname = reader["MName"].ToString();
                                string Uname = reader["Username"].ToString();
                                string Upass = reader["UserPW"].ToString();
                                string Ulevel = reader["UserLevel"].ToString();

                                System.Windows.Controls.TextBlock newTxtBk = new TextBlock();
                                newTxtBk.Text = "   " + Fname + " " + Mname + " " + Lname;
                                newTxtBk.Name = "UserTextBlock" + a.ToString();
                                newTxtBk.Height = 20;
                                newTxtBk.FontSize = 16;
                                StudASP.Children.Add(newTxtBk);
                                
                                //still no image
                                //need to put source
                                /*System.Windows.Controls.Image newImg = new Image();
                                newImg.Stretch = Stretch.Fill;
                                newImg.Height = 100;
                                newImg.Width = 100;
                                StudASP.Children.Add(newImg);*/

                                System.Windows.Controls.Label newTxtLbl = new Label();
                                newTxtLbl.Content = "   " + Uname + "\n    " + Ulevel;
                                newTxtLbl.Name = "UserInfoLabel" + a.ToString();
                                newTxtLbl.VerticalContentAlignment = VerticalAlignment.Top;
                                newTxtLbl.Height = 30;

                                StudASP.Children.Add(newTxtLbl);

                                a++;
                            }
                        }
                    }
                }

                int i = 0;
                using (SqlCeCommand cmd1 = new SqlCeCommand("SELECT * FROM User Where UserLevel = 'Administrator'", conn))
                {
                    using (DbDataReader reader = cmd1.ExecuteResultSet(ResultSetOptions.Scrollable))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string ALname = reader["LName"].ToString();
                                string AFname = reader["FName"].ToString();
                                string AMname = reader["MName"].ToString();
                                string AUname = reader["Username"].ToString();
                                string AUlevel = reader["UserLevel"].ToString();

                                System.Windows.Controls.TextBlock newTxtBk1 = new TextBlock();
                                newTxtBk1.Text = "   " + AFname + " " + AMname + " " + ALname;
                                newTxtBk1.Name = "AdminTextBlock" + i.ToString();
                                newTxtBk1.Height = 20;
                                newTxtBk1.FontSize = 16;
                                AdminSP.Children.Add(newTxtBk1);
                                
                                /*System.Windows.Controls.Image newImg1 = new Image();
                                newImg1.Stretch = Stretch.Fill;
                                newImg1.Height = 100;
                                newImg1.Width = 100;
                                StudASP.Children.Add(newImg1);*/

                                System.Windows.Controls.Label newTxtLbl1 = new Label();
                                newTxtLbl1.Content = "   " + AUname + "\n    " + AUlevel;
                                newTxtLbl1.Name = "AdminInfoLabel" + i.ToString();
                                newTxtLbl1.VerticalContentAlignment = VerticalAlignment.Top;
                                newTxtLbl1.Height = 30;

                                AdminSP.Children.Add(newTxtLbl1);

                                i++;
                            }
                        }
                    }
                }
                conn.Close();

            
        }
    }
}
