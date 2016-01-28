/***********************************************************************
 * 
 * Module Name:XBaseBusiness..Office.SystemManager.CompanyBaseInfoBus.cs
 * Current Version: 1.0 
 * Creator: taochun
 * Auditor:2009-03-03
 * End Date:
 * Description: 获取企业菜单
 * Version History:
 ***********************************************************************/
using XBase.Model.Office.SystemManager;
using XBase.Data.Office.SystemManager;
using System.Data;
using XBase.Common;
namespace XBase.Business.Office.SystemManager
{
   public class CompanyBaseInfoBus
    {
       /// <summary>
       /// 根据企业代码查询企业信息
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable GetCompanyModuleInfo(string CompanyCD)
       {
           if (string.IsNullOrEmpty(CompanyCD)) return null;
           try 
           {
             return CompanyBaseInfoDBHelper.GetCompanyModuleInfo(CompanyCD);
           }
           catch(System .Exception  ex)
           {
               throw ex;
           }
       }

       public static CompanyBaseInfoModel GetCompanyUnitInfo(string CompanyCD,string ComPanyID)
       {
           if (string.IsNullOrEmpty(CompanyCD)&&(string.IsNullOrEmpty(ComPanyID))) return null;
           try
           {
               return CompanyBaseInfoDBHelper.GetCompanyUntiInfo(CompanyCD,ComPanyID);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       /// <summary>
       /// 插入数据
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static bool InsertCompanyBaseInfo(CompanyBaseInfoModel model)
       {
           try
           {
               string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
               model.ModifiedUserID = loginUserID;
               return CompanyBaseInfoDBHelper.InsertCompanyBaseInfo(model);
           }
           catch(System .Exception ex)
           {
               throw ex;
           }
         
       }
    }
}
