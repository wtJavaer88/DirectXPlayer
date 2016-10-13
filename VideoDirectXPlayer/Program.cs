using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SaleSupport.enums;
using SaleSupport.database;

namespace VideoDirectXPlayer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        public static DbSourceType dbSourceType = DbSourceType.SQLITE;
        public static string workpath = System.Windows.Forms.Application.StartupPath + "\\";
        public static string dbpath="D:\\Users\\wnc\\oral\\字幕\\";
        public static string DbConnStr = "Data Source=" + dbpath + "srt.db";

        [STAThread]
        static void Main()
        {
            DBConnectionMgr.initType(DbSourceType.SQLITE);
            DBConnectionMgr.initConnection();   //初始化数据库连接

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
