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
using System.Data.SqlClient;
using System.Data.SQLite;
using SaleSupport.database.tran;

namespace SaleSupport.database
{
    public class SQLiteExecTranMgr : IDbExecTran
    {
        public static ILog log = log4net.LogManager.GetLogger("SaleSupport.database.SQLiteExecTranMgr");
        private object conn;
        public object conn1
        {
            get
            {
                return conn;
            }
            set
            {
                conn = value;
            }
        }
        private string tranCode;
        public string tranCode1
        {
            get
            {
                return tranCode;
            }
            set
            {
                tranCode = value;
            }
        }
        private object tran;
        public object tran1
        {
            get
            {
                return tran;
            }
            set
            {
                tran = value;
            }
        }

        public SQLiteExecTranMgr(string tranCode)
        {
            conn = DBConnectionMgr.getConnection();
            this.tranCode = tranCode;
        }
        public void beginTran()
        {
            tran = (SQLiteTransaction)((SQLiteConnection)conn).BeginTransaction();

        }
        public void rollTran()
        {
            try
            {
                ((SQLiteTransaction)(tran)).Rollback();

                DBConnectionMgr.returnConnection(conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine("已经回滚!" + ex);
            }
        }
        public void commitTran()
        {
            try
            {
                ((SQLiteTransaction)(tran)).Commit();

            }
            catch (Exception ex)
            {
                Console.WriteLine("已经提交!" + ex);
            }
        }
        /// <summary>
        /// 返回查询到的Dictionary, 外层以数字(从1开始)为key
        /// </summary>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public Dictionary<int, Dictionary<string, string>> ExecuteSelectSql(string selectSql)
        {
            Dictionary<int, Dictionary<string, string>> map = new Dictionary<int, Dictionary<string, string>>();

            SQLiteConnection conn_SQLite = (SQLiteConnection)conn;


            if (conn_SQLite.State == ConnectionState.Closed)
            {
                log.Warn("数据库连接已关闭!");
                return map;
            }
            if (BasicStringUtil.isNullString(selectSql))
            {
                log.Warn("查询语句不能为空!");
                return map;
            }

            Console.WriteLine(selectSql + "----selectsql DBExecMgr");
            SQLiteCommand cmd = null;
            SQLiteDataReader reader = null;
            try
            {
                cmd = new SQLiteCommand(selectSql, conn_SQLite);
                cmd.Transaction = ((SQLiteTransaction)(tran));
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
                                Console.WriteLine(fields[i] + "----fields[i]");
                                fieldMap.Add(fields[i], reader.GetValue(i).ToString());
                            }
                        }
                        map.Add(mapindex++, fieldMap);
                    }
                    reader.Close();//关闭reader

                }
                else
                {
                    log.Error("无查询结果,请检查sql或者是否存在该表!");
                }

            }
            catch (Exception ex)
            {
                rollTran();
            }

            return map;
        }
        /// <summary>
        /// 返回结果集第一行的第一个字段值
        /// </summary>
        /// <param name="selectsql"></param>
        /// <returns></returns>
        public string GetFirstValueSelectSql(string selectsql)
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
        /// </summary>
        /// <param name="selectsql"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetFirstValueMapSelectSql(string selectsql)
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
                    //Key都是大写
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
        /// <summary>
        /// 根据sql语句获得一个DataTable
        /// </summary>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string selectSql)
        {
            DataTable dt = new DataTable();             //新建DataTable对象

                if (((SQLiteConnection)conn).State == ConnectionState.Closed)
                {
                    log.Warn("数据库连接已关闭!");
                    return dt;
                }
                if (BasicStringUtil.isNullString(selectSql))
                {
                    log.Warn("查询语句不能为空!");
                    return dt;
                }

                SQLiteCommand cmd = null;
                SQLiteDataReader reader = null;
                try
                {
                    cmd = new SQLiteCommand(selectSql, (SQLiteConnection)conn);
                    cmd.Transaction = (SQLiteTransaction)tran;
                    reader = cmd.ExecuteReader();

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
                        reader.Close();//关闭reader
                    }
                    else
                    {
                        log.Error("无查询结果,请检查sql或者是否存在该表!");
                    }
                }
                catch (Exception ex)
                {
                    rollTran();
                }
           
            return dt;
        }
        /// <summary>
        /// SQLite分页
        /// </summary>
        /// <param name="selectSql"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataTable GetDataTableByPage(string selectSql, int page)
        {
            DataTable dt = new DataTable();             //新建DataTable对象
           
                SQLiteConnection conn = (SQLiteConnection)DBConnectionMgr.getConnection();

                Console.WriteLine(conn.GetHashCode() + "--------------------GetDataTableByPage------------------------------conn---DBConnectionMgr.getConnectionCount()" + DBConnectionMgr.getConnectionCount());
                selectSql += " limit " + 10 * (page - 1) + ",10 ";
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
                        reader.Close();
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
                    cmd.Dispose();
                    DBConnectionMgr.returnConnection(conn);
                }
           
            return dt;
        }

        public int ExecuteUpdateSql(string updateSql)
        {
            int iRows = 0;
            
                SQLiteCommand cmd = new SQLiteCommand(updateSql, (SQLiteConnection)conn);
                Console.WriteLine(conn.GetHashCode() + "------------------------------ExecuteUpdateSql--------------------conn---DBConnectionMgr.getConnectionCount()" + DBConnectionMgr.getConnectionCount());
                cmd.Transaction = (SQLiteTransaction)tran;
                log.Info("updateSql-1--" + updateSql);
                iRows = cmd.ExecuteNonQuery();
                cmd.Dispose();

            
            return iRows;
        }

        public bool ExecuteUpdateOnlyOneSql(string updateSql)
        {
            bool b = false;
            if (ExecuteUpdateSql(updateSql) == 1)
            {
                b = true;
            }
            Console.WriteLine("执行结果:bool--" + b);

            return b;
        }
        /// <summary>
        /// 检查select选出的集合是否有结果
        /// </summary>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public bool IsExistData(string selectSql)
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
    }
}
