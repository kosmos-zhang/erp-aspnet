using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

using XBase.Data.DBHelper;
namespace XBase.Data.Common
{
   public  class CheckQuoteDBHelper
   {
       #region 验证单据是否被指定表引用
       /// <summary>
       /// 验证单据是否被指定表引用
       /// </summary>
       /// <param name="tableName">表名 不带架构名称</param>
       /// <param name="colName">列名</param>
       /// <param name="value">值</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns></returns>
       public static bool CheckBill(string tableName, string colName, object  value,string CompanyCD)
       {
           
           StringBuilder sbSql = new StringBuilder();
           sbSql.Append("SELECT @Count=COUNT(*)  FROM ");
           sbSql.Append("officedba."+tableName);
           sbSql.Append(" WHERE CompanyCD=@CompanyCD AND  ");
           sbSql.Append(colName + "=@Value ");

           SqlParameter[] paras = { 
                                  new SqlParameter("@Count",SqlDbType.Int),
                                  new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                  new SqlParameter("@Value",(value is Int32)?SqlDbType.Int:SqlDbType.VarChar)};

           paras[0].Direction = ParameterDirection.Output;
           paras[1].Value = CompanyCD;
           paras[2].Value = value;
           SqlHelper.ExecuteSql(sbSql.ToString(), paras);
           int res=(int)paras[0].Value;
           if (res > 0)
               return true;
           else
               return false;
           

       }
       #endregion
   }
}
