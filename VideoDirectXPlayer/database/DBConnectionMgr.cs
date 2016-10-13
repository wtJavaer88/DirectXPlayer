using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.Odbc;
using SaleSupport.enums;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Reflection;

namespace SaleSupport.database
{
    public class DBConnectionMgr
    {
        static string refStr = "";
        static Type type = null;
        public static void initType(DbSourceType dbSourceType)
        {
            if (dbSourceType == DbSourceType.SQLITE)
            {
                refStr = "SaleSupport.database.SQLiteConnectionMgr";
            }
            Console.WriteLine(refStr);
            type = Type.GetType(refStr);
        }
        public static void returnConnection(Object paramConnection)
        {
            type.InvokeMember("returnConnection", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { paramConnection });
        }

        public static Object getConnection()
        {
            return type.InvokeMember("getConnection", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { });
        }
        public static int getConnectionCount()
        {
            return (int)type.InvokeMember("getConnectionCount", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { });
        }
        public static bool initConnection()
        {
            return (bool)type.InvokeMember("initConnection", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { });
        }
        private static object createConnection()
        {
            return type.InvokeMember("createConnection", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { });
        }
        public static bool closeConnect(Object paramConnection)
        {
            return (bool)type.InvokeMember("closeConnect", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { paramConnection });
        }
    }
}
