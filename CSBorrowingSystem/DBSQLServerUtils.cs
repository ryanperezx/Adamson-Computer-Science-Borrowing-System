using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
namespace CSBorrowingSystem
{
    class DBSQLServerUtils
    {
        public static SqlCeConnection
        GetDBConnection(string datasource)
        {
            //Data Source=ADMINRG-S0R6T5U\SQLEXPRESS;Initial Catalog=studentDB;Integrated Security=True
            string connString = @"Data Source=" + datasource;
            SqlCeConnection conn = new SqlCeConnection(connString);

            return conn;
        }
    }
}
