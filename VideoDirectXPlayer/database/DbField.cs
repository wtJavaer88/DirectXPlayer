using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using basic;

namespace SaleSupport.database
{
    public class DbField
    {
        public static String TYPE_STRING = "STRING";
        public static String TYPE_NUMBER = "NUMBER";
        public static String TYPE_ORIGINAL = "ORIGINAL";
        public static String TYPE_SQL = "SQL";
        public String fieldCode;
        public String fieldValue;
        public String fieldType;

        public DbField(String paramString1, String paramString2)
        {
            this.fieldCode = paramString1;
            this.fieldValue = paramString2;
            this.fieldType = "STRING";
        }

        public DbField(String paramString1, String paramString2, String paramString3)
        {
            this.fieldCode = paramString1;
            this.fieldValue = paramString2;
            this.fieldType = paramString3;
        }

        public String getFieldCode()
        {
            return this.fieldCode;
        }

        public String getFieldType()
        {
            return this.fieldType;
        }

        public String getFieldValue()
        {
            return this.fieldValue;
        }

        public String getSelectFieldString()
        {
            String str = null;

            if (!BasicStringUtil.isNullInString(this.fieldCode, this.fieldType))
            {
                if (this.fieldValue != null)
                {
                    if (BasicStringUtil.equalsIgnoreCase("STRING", this.fieldType))
                    {
                        str = this.fieldCode + "='" + this.fieldValue + "'";
                    }
                    else if (BasicStringUtil.equalsIgnoreCase("ORIGINAL", this.fieldType))
                    {
                        str = this.fieldCode + " " + this.fieldValue;
                    }
                    else if (BasicStringUtil.equalsIgnoreCase("NUMBER", this.fieldType))
                    {
                        str = this.fieldCode + "=" + this.fieldValue;
                    }
                    else if (BasicStringUtil.equalsIgnoreCase("SQL", this.fieldType))
                    {
                        str = this.fieldValue;
                    }
                    else
                    {
                        str = this.fieldCode + "='" + this.fieldValue + "'";
                    }

                }
                else
                {
                    str = this.fieldCode + "=null";
                }
            }

            return str;
        }

        public void setFieldCode(String paramString)
        {
            this.fieldCode = paramString;
        }

        public void setFieldType(String paramString)
        {
            this.fieldType = paramString;
        }

        public void setFieldValue(String paramString)
        {
            this.fieldValue = paramString;
        }
    }
}
