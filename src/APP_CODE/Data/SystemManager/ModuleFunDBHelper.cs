/***********************************************************************
 * 
 * Module Name:XBase.Data.SystemManager.ModuleFunDBHelper.cs
 * Current Version: 1.0 
 * Creator: jiangym
 * Auditor:2009-01-12
 * End Date:
 * Description:系统菜单功能数据库层处理
 * Version History:
 ***********************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
namespace XBase.Data.SystemManager
{
    public class ModuleFunDBHelper
    {
        /// <summary>
        /// 根据菜单编码查找功能
        /// </summary>
        ///<param name="ModuleID">菜单编码</param>
        /// <returns>DataTable</returns>
        public static DataTable GetModuleFunInfo(string ModuleID)
        {
            string sql = "select ID,ModuleID,FunctionID,FunctionCD,FunctionName,FunctionType" +
                        " from pubdba.ModuleFunction  where ModuleID  in('" + ModuleID.Replace(",", "','") + "') ";
            SqlParameter[] parms = new SqlParameter[1];
            // parms[0] = SqlHelper.GetParameter("@ModuleID", ModuleID);
            return SqlHelper.ExecuteSql(sql);

            //MN in('" + MN.Replace(",", "','") + "')
        }
        public static DataTable GetModuleFunFunctionType(string ModuleID)
        {
            string sql = "select FunctionType" +
                       " from pubdba.ModuleFunction  where ModuleID  in('" + ModuleID.Replace(",", "','") + "') group by FunctionType ";
            SqlParameter[] parms = new SqlParameter[1];
            // parms[0] = SqlHelper.GetParameter("@ModuleID", ModuleID);
            return SqlHelper.ExecuteSql(sql);
        }
        public static DataTable GetModuleFunInfo(string ModuleID, string FunctionType)
        {
            if (FunctionType != "")
            {
                string sql = "select ID,ModuleID,FunctionID,FunctionCD,FunctionName ,FunctionType" +
                                   " from pubdba.ModuleFunction  where FunctionType=@FunctionType and ModuleID  in('" + ModuleID.Replace(",", "','") + "') ";
                SqlParameter[] parms = new SqlParameter[1];
                parms[0] = SqlHelper.GetParameter("@FunctionType", FunctionType);
                return SqlHelper.ExecuteSql(sql, parms);
            }
            else
            {
                string sql = "select ID,ModuleID,FunctionID,FunctionCD,FunctionName ,FunctionType" +
                                                 " from pubdba.ModuleFunction  where FunctionType in('1','2') and ModuleID  in('" + ModuleID.Replace(",", "','") + "') ";
                return SqlHelper.ExecuteSql(sql);
            }

            //MN in('" + MN.Replace(",", "','") + "')
        }
        /// <summary>
        /// 根据菜单编码查询功能
        /// </summary>
        /// <param name="MuduleID">菜单编码</param>
        /// <returns></returns>
        public static bool IsExistByModuleId(string ModuleID)
        {
            string sql = "select * from ModuleFunction where ModuleID=@ModuleID ";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@ModuleID", ModuleID);
            DataTable dt = SqlHelper.ExecuteSql(sql, parms);
            return dt.Rows.Count > 0 ? true : false;
        }

        public static DataTable GetAllModuleFunInfo()
        {
            string sql = "select ID,ModuleID,FunctionID,FunctionCD,FunctionName,FunctionType" +
                        " from pubdba.ModuleFunction  ";
            
            // parms[0] = SqlHelper.GetParameter("@ModuleID", ModuleID);
            return SqlHelper.ExecuteSql(sql);

            //MN in('" + MN.Replace(",", "','") + "')
        }
    }
}
