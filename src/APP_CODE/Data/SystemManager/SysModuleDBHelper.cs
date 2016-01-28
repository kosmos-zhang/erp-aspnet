/***********************************************************************
 * 
 * Module Name:XBase.Data.SystemManager.SysModuleDBHelper.cs
 * Current Version: 1.0 
 * Creator: jiangym
 * Auditor:2009-01-10
 * End Date:
 * Description:系统菜单数据库层处理
 * Version History:
 ***********************************************************************/
using System;
using XBase.Data.DBHelper;
using System.Data;
using System.Data.SqlClient;
namespace XBase.Data.SystemManager
{
   public class SysModuleDBHelper
    {
        /// <summary>
        /// 获取系统菜单信息
        /// </summary>
        /// <returns>DataTable</returns>
       public static DataTable GetSysModuleInfo(string ModuleID)
        {
            string sql = "select ModuleID,ModuleName,ParentID,ModuleType,PropertyType,"+
                         " PropertyValue from pubdba.SysModule where  ModuleID=@ModuleID";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@ModuleID", ModuleID);
            return SqlHelper.ExecuteSql(sql, parms);
        }
       /// <summary>
       /// 获取子菜单信息
       /// </summary>
       /// <param name="ParentId">菜单编码<param>
       /// <returns>DataTable</returns>
       public static DataTable GetChildModuleByParentId(string ParentId)
       {
           string sql = "select ModuleID,ModuleName,ParentID,ModuleType,PropertyType," +
                       " PropertyValue from pubdba.SysModule where  ParentID=@ParentID";
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@ParentID", ParentId);
           return SqlHelper.ExecuteSql(sql, parms);
 
       }
    }
}
