using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoDirectXPlayer;

namespace SaleSupport.database
{
    public class IDbConnection
    {
        public static String connString = @Program.DbConnStr;
        //"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=cqdzda;Password=1988;";

        public static int maxCon = 10; // 最大连接数       
    }
}
