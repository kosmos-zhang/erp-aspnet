/***********************************************************************
 * 
 * Module Name:XBase.Business.SystemManager.ModuleFunBus.cs
 * Current Version: 1.0 
 * Creator: jiangym
 * Auditor:2009-01-12
 * End Date:
 * Description:系统菜单功能
 * Version History:
 ***********************************************************************/
using System;
using XBase.Data.SystemManager;
using System.Data;
namespace XBase.Business.SystemManager
{
    public class ModuleFunBus
    {
        /// <summary>
        /// 根据菜单编码查找功能
        /// </summary>
        ///<param name="ModuleID">菜单编码</param>
        /// <returns>DataTable</returns>
        public static DataTable GetModuleFunInfo(string ModuleID)
        {
            if (string.IsNullOrEmpty(ModuleID)) return null;
            try
            {
                return ModuleFunDBHelper.GetModuleFunInfo(ModuleID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetModuleFunFunctionType(string ModuleID)
        {
            if (string.IsNullOrEmpty(ModuleID)) return null;
            try
            {
                return ModuleFunDBHelper.GetModuleFunFunctionType(ModuleID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetModuleFunInfo(string ModuleID, string FunctionType)
        {
            if (string.IsNullOrEmpty(ModuleID)) return null;
            try
            {
                return ModuleFunDBHelper.GetModuleFunInfo(ModuleID, FunctionType);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据菜单编码查询功能
        /// </summary>
        /// <param name="MuduleID">菜单编码</param>
        /// <returns></returns>
        public static bool IsExistByModuleId(string ModuleID)
        {
            if (string.IsNullOrEmpty(ModuleID)) return false;
            try
            {
                return ModuleFunDBHelper.IsExistByModuleId(ModuleID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        public static DataTable GetAllModuleFunInfo()
        {
            return ModuleFunDBHelper.GetAllModuleFunInfo();
        }

    }
}
