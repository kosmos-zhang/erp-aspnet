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
    public class SellModuleSelectEmployeeDBHelper
    {

        /// <summary>
        /// 获取客户详细信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEmployee()
        {
            string strCompanyCD = string.Empty;//单位编号

            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            string strSql = string.Empty;
            strSql = "SELECT j.EmployeesID as ID, e.EmployeeNo, e.EmployeeName, j.NowDeptID, d.DeptName "
                     + " FROM officedba.EmployeeJob AS j INNER JOIN "
                     + " officedba.EmployeeInfo AS e ON j.EmployeesID = e.ID INNER JOIN "
                     + " officedba.DeptInfo AS d ON j.NowDeptID = d.ID"
                     + " where j.CompanyCD='" + strCompanyCD + "'";
            return SqlHelper.ExecuteSql(strSql);
        }
    }
}
