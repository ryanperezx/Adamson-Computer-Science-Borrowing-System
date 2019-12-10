using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;

namespace CSBorrowingSystem
{
    class DBUtils
    {
        public static SqlCeConnection GetDBConnection()
        {
            string user = Environment.UserName;

            string datasource = @"C:\Users\" + user + @"\Documents\GitHub\Adamson-Computer-Science-Borrowing-System\CSBorrowingSystem\bin\Debug\borrowingDB.sdf";

          //  string datasource = @"C:\Users\" + user + @"\source\repos\Adamson-Computer-Science-Borrowing-System\CSBorrowingSystem\bin\Debug\borrowingDB.sdf";

            return DBSQLServerUtils.GetDBConnection(datasource);
        }
    }
}
