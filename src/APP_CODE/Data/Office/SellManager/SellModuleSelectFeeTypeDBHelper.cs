using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using XBase.Model.Office.SellManager;
using XBase.Common;

namespace XBase.Data.Office.SellManager
{
    public class SellModuleSelectFeeTypeDBHelper
    {
        /// <summary>
        /// 获取费用类别
        /// </summary>
        /// <returns></returns>
        public static DataTable GetFeeType(int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号

            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;
            strSql = "SELECT f.ID, f.CodeName, c.TypeName, f.Description "
                     + " FROM officedba.CodeFeeType AS f INNER JOIN "
                     + " officedba.CodePublicType AS c ON f.Flag = c.ID AND f.CompanyCD = '"+strCompanyCD+"' AND f.UsedStatus = '1' ";
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
        }
    }
}
