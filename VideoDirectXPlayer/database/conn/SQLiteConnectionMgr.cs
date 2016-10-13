using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.Odbc;
using System.Data.SQLite;

namespace SaleSupport.database
{
    public class SQLiteConnectionMgr : IDbConnection
    {
        private static Stack<SQLiteConnection> vector = new Stack<SQLiteConnection>();

        public static void returnConnection(SQLiteConnection paramConnection)
        {
            Console.WriteLine("returnConnection-1--paramConnection.State---" + paramConnection.State);

            if (paramConnection != null)
            {
                if (paramConnection.State == ConnectionState.Open)
                {
                    paramConnection.Close();
                }
                vector.Push(paramConnection);
                Console.WriteLine(getConnectionCount() + "-----------------归还后,现有数据库连接数");
            }

        }
        public static SQLiteConnection getConnection()
        {
            if (vector.Count > 0)
            {
                SQLiteConnection conn = vector.Pop();
                conn.Open();        //在这儿打开
                return conn;
            }
            return null;

        }
        public static int getConnectionCount()
        {
            return vector.Count;
        }

        public static bool initConnection()
        {
            Console.WriteLine("initCon");
            for (int i = 0; i < maxCon; ++i)
            {
                SQLiteConnection localConnection;
                if ((localConnection = createConnection()) != null)
                {
                    vector.Push(localConnection);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public static SQLiteConnection createConnection()
        {
            SQLiteConnection localConnection = null;
            try
            {
                //string connString = "Provider=OraOLEDB.SQLite.1;User ID=cqdzda;Password=1988;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 127.0.0.1)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = orcl)))";

                localConnection = new SQLiteConnection(connString);
            }

            catch (Exception localException)
            {
                Console.WriteLine("createConnection() is Exception:"
                        + localException.GetBaseException());

                return null;
            }

            Console.WriteLine("createConnection()-2-创建一个新的数据连接:");

            return localConnection;
        }
        public static bool closeConnect(SQLiteConnection paramConnection)
        {
            int i = 0;
            try
            {
                if ((paramConnection != null) && (!(paramConnection.State == ConnectionState.Closed)))
                {
                    paramConnection.Close();
                    paramConnection = null;
                    i = 1;
                }
                else
                {
                }
            }
            catch (Exception localException2)
            {
                Console.WriteLine(localException2);
            }

            return i == 0 ? false : true;
        }
    }
}
