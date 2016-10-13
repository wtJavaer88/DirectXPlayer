using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SaleSupport.database.tran
{
    public interface IDbExecTran
    {
        object conn1{get;set;}
        object tran1{get;set;}

        string tranCode1{get;set;}

         void beginTran();
         void rollTran();
         void commitTran();
         Dictionary<int, Dictionary<string, string>> ExecuteSelectSql(string selectSql);
         string GetFirstValueSelectSql(string selectsql);
         Dictionary<string, string> GetFirstValueMapSelectSql(string selectsql);
         DataTable GetDataTable(string selectSql);
         DataTable GetDataTableByPage(string selectSql, int page);
         int ExecuteUpdateSql(string updateSql);
         bool ExecuteUpdateOnlyOneSql(string updateSql);
         bool IsExistData(string selectSql);
    }
}
