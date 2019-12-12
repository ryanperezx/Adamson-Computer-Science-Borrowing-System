using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
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

namespace CSBorrowingSystem
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : UserControl
    {
        public Reports()
        {
            InitializeComponent();
            //BindGridItems();

        }

        public void BindGridItems() //fill data grid
        {
            string _ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
            SqlCeConnection _Conn = new SqlCeConnection(_ConnectionString);

            // Open the Database Connection
            _Conn.Open();

            SqlCeDataAdapter _Adapter = new SqlCeDataAdapter("Select * from Tbl_Items", _Conn);

            DataSet _Bind = new DataSet();
            _Adapter.Fill(_Bind, "MyDataBinding");

            dtgRecords.DataContext = _Bind;

            // Close the Database Connection
            _Conn.Close();

        }

        public void BindGridBorrow() //fill data grid
        {
            string _ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
            SqlCeConnection _Conn = new SqlCeConnection(_ConnectionString);

            // Open the Database Connection
            _Conn.Open();

            SqlCeDataAdapter _Adapter = new SqlCeDataAdapter("Select * from Tbl_Borrow", _Conn);

            DataSet _Bind = new DataSet();
            _Adapter.Fill(_Bind, "MyDataBinding");

            dtgRecords.DataContext = _Bind;

            // Close the Database Connection
            _Conn.Close();


        }

        public void BindGridReturn() //fill data grid
        {
            string _ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
            SqlCeConnection _Conn = new SqlCeConnection(_ConnectionString);

            // Open the Database Connection
            _Conn.Open();

            SqlCeDataAdapter _Adapter = new SqlCeDataAdapter("Select * from Tbl_Return", _Conn);

            DataSet _Bind = new DataSet();
            _Adapter.Fill(_Bind, "MyDataBinding");

            dtgRecords.DataContext = _Bind;

            // Close the Database Connection
            _Conn.Close();


        }

        private void ReportName_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            string Rep = ReportName.Text;
            Console.WriteLine(Rep);
            if (Rep == "Inventory")
            {
                BindGridItems();
            }
            if (Rep == "Borrowing Transaction")
            {
                BindGridBorrow();
            }
            if (Rep == "Returned")
            {
                BindGridReturn();
            }
        }

        private void ReportName_DropDownClosed(object sender, EventArgs e)
        {
            string Rep = ReportName.Text;
            
            if (Rep == "Inventory")
            {
                BindGridItems();
            }
            if (Rep == "Borrowing Transaction")
            {
                BindGridBorrow();
            }
            if (Rep == "Returned")
            {
                BindGridReturn();
            }
        }
    }
}
