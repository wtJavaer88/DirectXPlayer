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
using System.Data.SQLite;

namespace SaleSupport.database
{
    public class SQLiteExecMgr
    {
        public static ILog log = log4net.LogManager.GetLogger("SaleSupport.MyDataViewControl");

        public static Dictionary<int, Dictionary<string, string>> ExecuteSelectSql(string selectSql)
        {
            SQLiteConnection conn = (SQLiteConnection)DBConnectionMgr.getConnection();

            if (conn.State == ConnectionState.Closed)
            {
                log.Warn("数据库连接已关闭!");
                return null;
            }
            if (BasicStringUtil.isNullString(selectSql))
            {
                log.Warn("查询语句不能为空!");
                return null;
            }
            Dictionary<int, Dictionary<string, string>> map = new Dictionary<int, Dictionary<string, string>>();
            Console.WriteLine(selectSql + "----selectsql DBExecMgr");
            SQLiteCommand cmd = null;
            SQLiteDataReader reader = null;
            try
            {
                cmd = new SQLiteCommand(selectSql, conn);
                reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    string[] fields = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        fields[i] = (reader.GetName(i));
                    }

                    int mapindex = 1;
                    while (reader.Read())
                    {
                        Dictionary<string, string> fieldMap = new Dictionary<string, string>();
                        for (int i = 0; i < fields.Length; i++)
                        {
                            if (reader.IsDBNull(i))
                            {
                                fieldMap.Add(fields[i], "");
                            }
                            else
                            {
                                fieldMap.Add(fields[i], reader.GetValue(i).ToString());
                            }
                        }
                        map.Add(mapindex++, fieldMap);
                    }
                }
                else
                {
                    log.Error("无查询结果,请检查sql或者是否存在该表!");
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                DBConnectionMgr.returnConnection(conn);
            }
            return map;
        }
        /// <summary>
        /// 返回结果集第一行的第一个字段值
        /// </summary>
        /// <param name="selectsql"></param>
        /// <returns></returns>
        public static string GetFirstValueSelectSql(string selectsql)
        {
            Dictionary<int, Dictionary<string, string>> map = ExecuteSelectSql(selectsql);
            if (map.Count > 0)
            {
                Dictionary<string, string> fieldMap = map[1];
                foreach (KeyValuePair<string, string> a in fieldMap)
                {
                    return a.Value;
                }

            }
            return null;
        }
        /// <summary>
        /// 该方法中可以有多个段,如ID,NAME,然后用map["NAME"]即可
        /// 加入map时,Key要是大写的
        /// </summary>
        /// <param name="selectsql"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetFirstValueMapSelectSql(string selectsql)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();

            DataTable dt = GetDataTable(selectsql);
            ArrayList fields = new ArrayList();
            foreach (DataColumn c in dt.Columns)
            {
                fields.Add(c.ColumnName.ToUpper());
            }
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                foreach (string str in fields)
                {
                    map.Add(str, row[str].ToString());
                }
            }
            else
            {
                ///注意如果结果集为空,也返回一个有效map, 往里面填空字符串
                foreach (string str in fields)
                {
                    map.Add(str, "");
                }
            }
            return map;
        }
        public static DataTable GetDataTable(string selectSql)
        {
            SQLiteConnection conn = (SQLiteConnection)DBConnectionMgr.getConnection();

            DataTable dt = new DataTable();             //新建DataTable对象

            SQLiteCommand cmd = new SQLiteCommand(selectSql, conn);
            SQLiteDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();    //初始化Reader对象
                if (reader != null)
                {
                    string[] fields = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        fields[i] = reader.GetName(i);
                        dt.Columns.Add(reader.GetName(i), typeof(string));  //初始化每个列名
                    }
                    while (reader.Read())
                    {
                        //加入每一列
                        DataRow dr = dt.NewRow();

                        for (int i = 0; i < fields.Length; i++)
                        {
                            if (reader.IsDBNull(i))
                            {
                                dr[fields[i]] = "";
                            }
                            else
                            {
                                dr[fields[i]] = reader.GetValue(i).ToString();
                            }
                        }

                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    log.Error("无查询结果,请检查sql或者是否存在该表!");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();//关闭reader
                }
                cmd.Dispose();  //销毁cmd
                DBConnectionMgr.returnConnection(conn);
            }
            return dt;
        }
        /// <summary>
        /// SQLite分页
        /// </summary>
        /// <param name="selectSql"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static DataTable GetDataTableByPage(string selectSql, int page)
        {
            SQLiteConnection conn = (SQLiteConnection)DBConnectionMgr.getConnection();
            DataTable dt = new DataTable();             //新建DataTable对象
            Console.WriteLine(conn.GetHashCode() + "--------------------GetDataTableByPage------------------------------conn---DBConnectionMgr.getConnectionCount()" + DBConnectionMgr.getConnectionCount());
            selectSql += " limit 10 offset " + 10 * (page - 1);
            Console.WriteLine("selectSql:--" + selectSql);
            SQLiteCommand cmd = new SQLiteCommand(selectSql, conn);
            SQLiteDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();    //初始化Reader对象
                if (reader != null)
                {
                    string[] fields = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        fields[i] = (reader.GetName(i));
                        dt.Columns.Add(reader.GetName(i), typeof(string));  //初始化每个列名
                    }
                    while (reader.Read())
                    {
                        //加入每一列
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < fields.Length; i++)
                        {
                            if (reader.IsDBNull(i))
                            {
                                dr[fields[i]] = "";
                            }
                            else
                            {
                                dr[fields[i]] = reader.GetValue(i).ToString();
                            }
                        }

                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    log.Error("无查询结果,请检查sql或者是否存在该表!");
                }
            }
            catch (Exception ex)
            {
                log.Info("224----DBExecMgr.Datatable" + ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();//关闭reader
                }
                cmd.Dispose();
                DBConnectionMgr.returnConnection(conn);
            }

            return dt;
        }
        
        public static int ExecuteUpdateSql(string updateSql)
        {
            int iRows = 0;
            SQLiteConnection conn = (SQLiteConnection)DBConnectionMgr.getConnection();
            SQLiteCommand cmd = new SQLiteCommand(updateSql, conn);

            Console.WriteLine("updateSql-1--" + updateSql);
            try
            {
                iRows = cmd.ExecuteNonQuery();
                cmd.Dispose();
                DBConnectionMgr.returnConnection(conn);
            }
            catch (Exception ex)
            {
            }
            return iRows;
        }

        public static bool ExecuteUpdateOnlyOneSql(string updateSql)
        {
            bool b = false;
            if (ExecuteUpdateSql(updateSql) == 1)
            {
                b = true;
            }
            Console.WriteLine("ExecuteUpdateOnlyOneSql执行结果:bool--" + b);

            return b;
        }

        public static bool IsExistData(string selectSql)
        {
            bool b = false;
            try
            {
                if (GetDataTable(selectSql).Rows.Count > 0)
                {
                    b = true;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return false;
            }
            Console.WriteLine("IsExistData执行结果:bool--" + b);

            return b;
        }

        public static string GetInsertSqlBeforeDel(string tableName, DbField field)
        {
            string retStr = "";

            DbFieldSqlUtil dsUtil = new DbFieldSqlUtil(tableName, "");
            dsUtil.addWhereField(field);

            Console.WriteLine(dsUtil.getSelectSql() + "------------------11111111111111111111111111111111111111111111111111111111111111111111111111--------dsUtil.getSelectField()");


            Dictionary<string, string> map = GetFirstValueMapSelectSql(dsUtil.getSelectSql());

            foreach (KeyValuePair<string, string> kp in map)
            {
                DbField f = new DbField(kp.Key.Trim(), kp.Value.Trim());
                dsUtil.addInsertField(f);
            }
            retStr = dsUtil.getInsertSql();
            return retStr;
        }

        public static int GetIdentity(string table, string keyFieldName)
        {
            string key = GetFirstValueSelectSql("SELECT MAX(" + keyFieldName + ") FROM " + table);
            if (BasicStringUtil.isNullString(key))
            {
                key = "0";
            }
            return Convert.ToInt32(key) + 1; ;
        }
    }
}
