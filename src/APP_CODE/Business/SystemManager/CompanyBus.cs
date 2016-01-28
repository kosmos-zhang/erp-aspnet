/**********************************************
 * 描述：     公司业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/01/21
 * 类名：CompanyBus
 ***********************************************/
using System;
using XBase.Data.SystemManager;
using XBase.Model.SystemManager;
using XBase.Common;
using System.Data;
namespace XBase.Business.SystemManager
{
   public class CompanyBus
    {
         /// <summary>
        /// 增加公司信息
        /// </summary>
        /// <returns>增加是否成功</returns>
       public static bool AddCompany(CompanyModel Model)
       {
           if (Model == null) return false;
           if (Model.ModifiedUserID == null) Model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
           try
           {
               return CompanyDBHelper.AddCompany(Model);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       
        /// <summary>
        /// 根据公司编码查询公司信息
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
       public static DataTable GetCompanyByCD(string StrWhere)
       {
           if (string.IsNullOrEmpty(StrWhere)) return null;
           try
           {
               return CompanyDBHelper.GetCompanyByCD(StrWhere);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }

        /// <summary>
        /// 修改公司信息
        /// </summary>
        /// <param name="Model">Company实体</param>
        /// <returns>更新是否成功</returns>
       public static bool ModifyCompany(CompanyModel Model)
       {
           if (Model == null) return false;
           if (Model.ModifiedUserID == null) Model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
           try
           {
               return CompanyDBHelper.ModifyCompany(Model);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }

       /// <summary>
       /// 删除公司信息
       /// </summary>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns>删除是否成功</returns>
       public static bool DelCompany(string CompanyCD)
       {
           if (string.IsNullOrEmpty(CompanyCD)) return false;
           try
           {
               return CompanyDBHelper.DelCompany(CompanyCD);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }

         /// <summary>
        /// 判断公司编码是否存在
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns>是否存在</returns>
       public static bool IsExist(string CompanyCD)
       {
           if (string.IsNullOrEmpty(CompanyCD)) return false;
           try
           {
               return CompanyDBHelper.IsExist(CompanyCD);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <returns>DataTable</returns>
       public static DataTable GetCompanyInfo()
       {
           try
           {
               return CompanyDBHelper.GetCompanyInfo();
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
    }
}
