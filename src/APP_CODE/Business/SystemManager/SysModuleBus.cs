/***********************************************************************
 * 
 * Module Name:XBase.Business.SystemManager.SysModuleBus.cs
 * Current Version: 1.0 
 * Creator: jiangym
 * Auditor:
 * Create Date:2009-01-07
 * End Date:
 * Description:获取系统菜
 * Version History:
 ***********************************************************************/
using System;
using XBase.Data.SystemManager;
using System.Data;
namespace XBase.Business.SystemManager
{
   public class SysModuleBus
    {
      
        /// <summary>
        /// 获取系统菜单信息
        /// </summary>
        /// <returns>DataTable</returns>
       public static DataTable GetSysModuleInfo(string ModuleID)
       {
           if (string.IsNullOrEmpty(ModuleID)) return null;
           try
           {
               return SysModuleDBHelper.GetSysModuleInfo(ModuleID);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }     
        /// <summary>
       /// 获取子菜单信息
       /// </summary>
       /// <param name="ParentId">菜单编码<param>
       /// <returns>DataTable</returns>
       public static DataTable GetChildModuleByParentId(string ParentId)
       {
           if (string.IsNullOrEmpty(ParentId)) return null;
           try
           {
               return SysModuleDBHelper.GetChildModuleByParentId(ParentId);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
    }
}
