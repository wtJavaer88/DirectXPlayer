using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data;
using System.Windows.Forms;
using SaleSupport.database;
using System.Data.OleDb;
using SaleSupport.enums;
using log4net;
using basic;
using System.Collections;


using System.Reflection;
using VideoDirectXPlayer;
namespace SaleSupport.database
{
    public class DBExecMgr
    {
        public static ILog log = log4net.LogManager.GetLogger("SaleSupport.MyDataViewControl");
        static string refStr = "";
        static Type type = null;
        static DBExecMgr()
        {

            Console.WriteLine("ddd" + DbSourceType.SQLITE);
            Console.WriteLine("ppp" + Program.dbSourceType);
            if (Program.dbSourceType == DbSourceType.SQLITE)
            {
                refStr = "SaleSupport.database.SQLiteExecMgr";
            }
            type = Type.GetType(refStr);
        }
        public static Dictionary<int, Dictionary<string, string>> ExecuteSelectSql(string selectSql)
        {
            Console.WriteLine("ttt" + type + " refStr:" + refStr);
            return (Dictionary<int, Dictionary<string, string>>)type.InvokeMember("ExecuteSelectSql", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { selectSql });
        }
        /// <summary>
        /// 返回结果集第一行的第一个字段值
        /// </summary>
        /// <param name="selectsql"></param>
        /// <returns></returns>
        public static string GetFirstValueSelectSql(string selectSql)
        {
            return (string)type.InvokeMember("GetFirstValueSelectSql", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { selectSql });
        }
        /// <summary>
        /// 该方法中可以有多个段,如ID,NAME,然后用map["NAME"]即可
        /// 加入map时,Key要是大写的
        /// </summary>
        /// <param name="selectsql"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetFirstValueMapSelectSql(string selectSql)
        {
            return (Dictionary<string, string>)type.InvokeMember("GetFirstValueMapSelectSql", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { selectSql });
        }
        public static DataTable GetDataTable(string selectSql)
        {
            return (DataTable)type.InvokeMember("GetDataTable", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { selectSql });
        }
        /// <summary>
        /// Mysql分页
        /// </summary>
        /// <param name="selectSql"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static DataTable GetDataTableByPage(string selectSql, int page)
        {
            return (DataTable)type.InvokeMember("GetDataTableByPage", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { selectSql, page });
        }

        public static int ExecuteUpdateSql(string updateSql)
        {
            return (int)type.InvokeMember("ExecuteUpdateSql", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { updateSql });
        }

        public static bool ExecuteUpdateOnlyOneSql(string updateSql)
        {
            return (bool)type.InvokeMember("ExecuteUpdateOnlyOneSql", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { updateSql });
        }

        public static bool IsExistData(string selectSql)
        {
            return (bool)type.InvokeMember("IsExistData", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { selectSql });
        }

        public static string GetInsertSqlBeforeDel(string tableName, DbField field)
        {
            return (string)type.InvokeMember("GetInsertSqlBeforeDel", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { tableName, field });
        }

    }
}
