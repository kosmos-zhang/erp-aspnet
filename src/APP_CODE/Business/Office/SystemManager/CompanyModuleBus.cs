
/***********************************************************************
 * 
 * Module Name:XBaseBusiness..Office.SystemManager.CompanyModuleDBHelper.cs
 * Current Version: 1.0 
 * Creator: jiangym
 * Auditor:2009-01-10
 * End Date:
 * Description: 获取企业菜单
 * Version History:
 ***********************************************************************/
using XBase.Model.Office.SystemManager;
using XBase.Data.Office.SystemManager;
using System.Data;
namespace XBase.Business.Office.SystemManager
{
  public class CompanyModuleBus
    {
        /// <summary>
        /// 获取系统菜单信息
        /// </summary>
        /// <returns>DataTable</returns>
      public static DataTable GetCompanyModuleInfo(string CompanyCD)
      {
          if (string.IsNullOrEmpty(CompanyCD)) return null;
          try
          {
              return CompanyModuleDBHelper.GetCompanyModuleInfo(CompanyCD);
          }
          catch (System.Exception ex)
          {
              throw ex;
          }
      }
      /// <summary>
      /// 获取系统顶级菜单信息
      /// </summary>
      /// <returns>DataTable</returns>
      public static DataTable GetParentSysModuleInfo(string CompanyCD)
      {
          if (string.IsNullOrEmpty(CompanyCD)) return null;
          try
          {
              return CompanyModuleDBHelper.GetSysModuleInfo(CompanyCD);
          }
          catch (System.Exception ex)
          {
              throw ex;
          }
      }


       /// <summary>
        /// 获取系统菜单信息2,读取了ParentID字段
        /// </summary>
        /// <returns>DataTable</returns>
      public static DataTable GetCompanyModuleInfo2(string CompanyCD)
      {
          return CompanyModuleDBHelper.GetCompanyModuleInfo2(CompanyCD);
      }
    }
}
