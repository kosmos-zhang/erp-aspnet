/***********************************************************************
 * 
 * Module Name:XBase.Data.Office.SystemManager.CompanyModuleDBHelper.cs
 * Current Version: 1.0 
 * Creator: jiangym
 * Auditor:2009-01-10
 * End Date:
 * Description: 企业菜单数据库层处理
 * Version History:
 ***********************************************************************/
using System;
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
namespace XBase.Data.Office.SystemManager
{
   public class CompanyModuleDBHelper
    {
        /// <summary>
        /// 获取系统菜单信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCompanyModuleInfo(string CompanyCD)
        {
            string sql = "select ID,CompanyCD,ModuleID,OrderNO,ModifiedDate,ModifiedUserID from CompanyModule where" +
                         " CompanyCD=@CompanyCD ";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            return SqlHelper.ExecuteSql(sql,parms);
        }


        /// <summary>
        /// 获取系统菜单信息2,读取了ParentID字段
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCompanyModuleInfo2(string CompanyCD)
        {
            string sql = "select a.ID,a.ModuleID,b.ModuleName,b.ParentID from pubdba.CompanyModule as a  left join pubdba.SysModule as b on a.ModuleID=b.ModuleID where" +
                         " CompanyCD=@CompanyCD ";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            return SqlHelper.ExecuteSql(sql, parms);
        }


        /// <summary>
        /// 获取系统的顶级菜单
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public static DataTable GetSysModuleInfo(string CompanyCD)
        {
            string sql = "select ID,CompanyCD,ModuleID,OrderNO,ModifiedDate,ModifiedUserID from CompanyModule where" +
                         "  CompanyCD=@CompanyCD and len(ModuleID)<2";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            return SqlHelper.ExecuteSql(sql, parms);
        }
    }
}
