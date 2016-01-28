using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using System.Data.Sql;
using XBase.Model.Office.SellManager;
using XBase.Common;


namespace XBase.Data.Office.SellManager
{
    public  class SellModuleSelectCurrencyDBHelper
    {

        /// <summary>
        /// 获取币种
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCurrencyTypeSetting(int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号

           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = "SELECT ID,ExchangeRate,CurrencySymbol,CurrencyName FROM officedba.CurrencyTypeSetting where CompanyCD='" + strCompanyCD + "' and UsedStatus='1'";
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
        }

        /// <summary>
        /// 获取币种
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCurrencyTypeSetting()
        {
            string strCompanyCD = string.Empty;//单位编号

            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          
            string strSql = "SELECT ID,ExchangeRate,CurrencySymbol,CurrencyName FROM officedba.CurrencyTypeSetting where CompanyCD='" + strCompanyCD + "'";
            return SqlHelper.ExecuteSql(strSql);
        }
    }
}
