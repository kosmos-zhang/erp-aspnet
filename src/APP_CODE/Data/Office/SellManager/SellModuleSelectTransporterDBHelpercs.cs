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
    public  class SellModuleSelectTransporterDBHelpercs
    {
        /// <summary>
        /// 获取运输商详细信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTransporter(string OrderNo, string Title, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号

                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;
            strSql = "SELECT ID,isnull( CustNo,'') as CustNo,isnull( CustName,'') as CustName,isnull( CustNote,'') as CustNote "
                     + " FROM officedba.OtherCorpInfo "
                     + " WHERE (CompanyCD = @CompanyCD ) AND (BigType = '6') and UsedStatus='1'";

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (OrderNo != null)
            {
                strSql += " and CustNo like  '%" + OrderNo + "%'";
            }
            if (Title != null)
            {
                strSql += " and CustName like  '%" + Title + "%'";

            }
            

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }
    }
}
