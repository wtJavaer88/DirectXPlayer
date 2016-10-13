
using System;
using basic;
using System.Collections.Generic;
namespace SaleSupport.database
{
    public class DbFieldSqlUtil
    {
        public string tableCode;
        public string selectField;
        public string orderSql;
        public string addWhereFieldSql;

        public List<DbField> vcWhereField;
        public List<DbField> vcUpdateField;
        public List<DbField> vcInsertField;

        private Dictionary<string, string> map1;
        private Dictionary<string, string> map2;
        private Dictionary<string, string> map3;

        public DbFieldSqlUtil(string paramString1, string paramString2)
        {
            this.vcWhereField = new List<DbField>();
            this.vcUpdateField = new List<DbField>();
            this.vcInsertField = new List<DbField>();

            this.map1 = new Dictionary<string, string>();
            this.map2 = new Dictionary<string, string>();
            this.map3 = new Dictionary<string, string>();

            this.tableCode = paramString1;

            this.orderSql = paramString2;
            this.addWhereFieldSql = "";

        }

        public bool addSelectField(DbField paramDbField)
        {
            bool b = false;
            if ((paramDbField != null) && !(BasicStringUtil.isNullInString(paramDbField.fieldCode, paramDbField.fieldType)))
            {
                if (!this.map3.ContainsKey(paramDbField.fieldCode))
                {
                    if (BasicStringUtil.isNullString(paramDbField.fieldValue))
                    {
                        selectField += paramDbField.fieldCode + ",";
                    }
                    else
                    {
                        selectField += paramDbField.fieldCode + " AS " + paramDbField.fieldValue.Replace("'", "") + ",";
                    }
                    this.map3.Add(paramDbField.fieldCode, "true");
                    b = true;
                }
                else
                {
                }

            }
            else
            {
            }
            return b;
        }

        public bool addInsertField(DbField paramDbField)
        {
            int i = 0;

            if ((paramDbField != null) && !(BasicStringUtil.isNullInString(paramDbField.fieldCode, paramDbField.fieldType)))
            {
                if (!this.map1.ContainsKey(paramDbField.fieldCode))
                {
                    this.vcInsertField.Add(paramDbField);
                    this.map1.Add(paramDbField.fieldCode, "true");
                    i = 1;
                }
                else
                {
                }

            }
            else
            {
            }

            return i == 0 ? false : true;
        }

        public bool addUpdateField(DbField paramDbField)
        {
            int i = 0;

            if ((paramDbField != null) && !(BasicStringUtil.isNullInString(paramDbField.fieldCode, paramDbField.fieldType)))
            {
                if (!this.map2.ContainsKey(paramDbField.fieldCode))
                {
                    this.vcUpdateField.Add(paramDbField);
                    this.map2.Add(paramDbField.fieldCode, "true");
                    i = 1;
                }
                else
                {
                }

            }
            else
            {
            }

            return i == 0 ? false : true;
        }

        public bool addWhereField(DbField paramDbField)
        {
            int i = 0;

            if ((paramDbField != null) && !(BasicStringUtil.isNullInString(paramDbField.fieldCode, paramDbField.fieldType)))
            {
                this.vcWhereField.Add(paramDbField);
                i = 1;
            }
            else
            {
                Console.WriteLine("部分参数不能为空!");
            }

            return i == 0 ? false : true;
        }

        public string getDeleteSql()
        {
            string str1 = null;
            string str2 = null;

            try
            {
                if (!BasicStringUtil.isNullString(this.tableCode))
                {
                    str2 = getWhereSql();

                    str1 = "DELETE FROM " + this.tableCode;

                    if (!BasicStringUtil.isNullString(str2))
                    {
                        str1 = str1 + " WHERE " + str2;
                    }
                }
            }
            catch (Exception localException)
            {
                Console.WriteLine(localException);
            }

            return str1;
        }

        public string getInsertSql()
        {
            string str1 = null;

            string str2 = "";
            string str3 = "";

            DbField localDbField = null;

            for (int i = 0; i < this.vcInsertField.Count; ++i)
            {
                localDbField = (DbField)this.vcInsertField[i];
                if (!BasicStringUtil.isNullInString(localDbField.fieldCode, localDbField.fieldType))
                {
                    if (localDbField.fieldValue != null)
                    {
                        if (BasicStringUtil.equalsIgnoreCase("STRING", localDbField.fieldType))
                        {
                            str2 = str2 + localDbField.fieldCode + ",";
                            str3 = str3 + "'" + localDbField.fieldValue + "',";
                        }
                        else if (BasicStringUtil.equalsIgnoreCase("ORIGINAL", localDbField.fieldType))
                        {
                            str2 = str2 + localDbField.fieldCode + ",";
                            str3 = str3 + localDbField.fieldValue + ",";
                        }
                        else if (BasicStringUtil.equalsIgnoreCase("NUMBER", localDbField.fieldType))
                        {
                            str2 = str2 + localDbField.fieldCode + ",";
                            str3 = str3 + localDbField.fieldValue + ",";
                        }
                        else
                        {
                            str2 = str2 + localDbField.fieldCode + ",";
                            str3 = str3 + "'" + localDbField.fieldValue + "',";
                        }

                    }
                    else
                    {
                        str2 = str2 + localDbField.fieldCode + ",";
                        str3 = str3 + "null,";
                    }
                }
                else
                {
                    return null;
                }
            }

            if (!BasicStringUtil.isNullInString(this.tableCode, str2, str3))
            {
                str1 = "INSERT INTO " + this.tableCode + "(" + str2.Substring(0, str2.Length - 1) + ")VALUES(";
                str1 = str1 + str3.Substring(0, str3.Length - 1) + ")";
            }

            return str1;
        }

        public string getOrderSql()
        {
            return this.orderSql;
        }
        /// <summary>
        /// 获取查询的某些字段
        /// </summary>
        /// <returns></returns>
        public string getSelectField()
        {
            return this.selectField;
        }

        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <returns></returns>
        public string getSelectSql()
        {
            string str1 = null;
            string str2 = null;
            try
            {
                if (!BasicStringUtil.isNullString(this.tableCode))
                {
                    str2 = getWhereSql();

                    if (!BasicStringUtil.isNullString(this.selectField))
                        str1 = "SELECT " + this.selectField.Remove(selectField.Length - 1) + " FROM " + this.tableCode;
                    else
                    {
                        str1 = "SELECT * FROM " + this.tableCode;
                    }
                    if (!BasicStringUtil.isNullString(str2))
                    {
                        str1 = str1 + " WHERE " + str2;
                    }
                    if (!BasicStringUtil.isNullString(this.orderSql))
                    {
                        str1 = str1 + " ORDER BY " + this.orderSql;
                    }
                }

            }
            catch (Exception localException)
            {
                Console.WriteLine(localException);
            }

            return str1;
        }


        public string getTableCode()
        {
            return this.tableCode;
        }

        public string getUpdateSql()
        {
            string str1 = null;
            Object localObject = null;
            string str2 = "";
            string str3 = null;

            DbField localDbField = null;
            try
            {
                if (!BasicStringUtil.isNullString(this.tableCode))
                {
                    for (int i = 0; i < this.vcUpdateField.Count; ++i)
                    {
                        if (!BasicStringUtil.isNullString(str3 = (
                          localDbField = (DbField)this.vcUpdateField[i])
                          .getSelectFieldString()))
                        {
                            if (!BasicStringUtil.isNullString((String)localObject))
                                localObject = localObject + "," + str3;
                            else
                                localObject = str3;
                        }
                        else
                        {
                            return null;
                        }
                    }

                    if (!BasicStringUtil.isNullString((String)localObject))
                    {
                        str2 = getWhereSql();

                        str1 = "UPDATE " + this.tableCode + " SET " + (String)localObject;

                        if (!BasicStringUtil.isNullString(str2))
                        {
                            str1 = str1 + " WHERE " + str2;
                        }
                    }
                }
            }
            catch (Exception localException)
            {
                Console.WriteLine(localException);
            }

            return (String)str1;
        }

        public string getWhereSql()
        {
            Object localObject = null;
            string str = null;

            DbField localDbField = null;

            for (int i = 0; i < this.vcWhereField.Count; ++i)
            {
                if (!BasicStringUtil.isNullString(str = (
                  localDbField = (DbField)this.vcWhereField[i])
                  .getSelectFieldString()))
                {
                    if (!BasicStringUtil.isNullString((String)localObject))
                    {
                        if (BasicStringUtil.equalsIgnoreCase("SQL", localDbField.getFieldType()))
                            localObject = localObject + str;
                        else
                            localObject = localObject + " AND " + str;
                    }
                    else
                        localObject = str;
                }
                else
                {
                    throw new Exception("where语句参数不全!");
                }
            }

            if (!BasicStringUtil.isNullString(this.addWhereFieldSql))
            {
                if (!BasicStringUtil.isNullString((String)localObject))
                {
                    localObject = this.addWhereFieldSql + " AND " + (String)localObject;
                }
                else
                {
                    localObject = this.addWhereFieldSql;
                }
            }

            return (String)localObject;
        }

        public void setOrderField(string paramString)
        {
            this.orderSql = paramString;
        }

        public void setSelectField(string paramString)
        {
            this.selectField = paramString;
        }

        public void setTableCode(string paramString)
        {
            this.tableCode = paramString;
        }
    }
}