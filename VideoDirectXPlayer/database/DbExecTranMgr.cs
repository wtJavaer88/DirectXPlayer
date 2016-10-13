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
using SaleSupport.database.tran;
using VideoDirectXPlayer;

namespace SaleSupport.database
{
    public class DbExecTranMgr
    {
        static IDbExecTran dbTran = null;
        public static IDbExecTran getDbTran(String code)
        {
            switch (Program.dbSourceType)
            {
                case DbSourceType.SQLITE:
                    dbTran = new SQLiteExecTranMgr(code);
                    break;
            }
            return dbTran;
        }
    }
}
