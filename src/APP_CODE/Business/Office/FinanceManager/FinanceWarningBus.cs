/**********************************************
 * 描述：     预警项目业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/03/24
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;
namespace XBase.Business.Office.FinanceManager
{
   public class FinanceWarningBus
    {
      /// <summary>
     /// 根据企业编码获取预警信息
     /// </summary>
     /// <param name="CompanyCD">企业编码</param>
     /// <returns>DataTable</returns>
       public static DataTable GetFinenceWarningInfo()
       {
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           //string CompanyCD = "1001";
           try
           {
               DataTable dt = FinanceWarningDBHelper.GetFinenceWarningInfo(CompanyCD);
               if (dt != null && dt.Rows.Count > 0)
               {
                   foreach (DataRow rows in dt.Rows)
                   {
                       if (rows["UsedStatus"].ToString() == ConstUtil.USED_STATUS_OFF)
                       {
                           rows["UsedStatus"] = ConstUtil.USED_STATUS_OFF_NAME;
                       }
                       else if (rows["UsedStatus"].ToString() == ConstUtil.USED_STATUS_ON)
                       {
                           rows["UsedStatus"] = ConstUtil.USED_STATUS_ON_NAME;
                       }

                       if (rows["WarningWay"].ToString() == ConstUtil.WARNING_MOBILEPHONE_MESSAGE_CODE)
                       {
                           rows["WarningWay"] = ConstUtil.WARNING_MOBILEPHONE_MESSAGE_NAME;
                       }
                       else if (rows["WarningWay"].ToString() == ConstUtil.WARNING_SITEMESSAGE_MESSAGE_CODE)
                       {
                           rows["WarningWay"] = ConstUtil.WARNING_SITEMESSAGE_MESSAGE_NAME;
                       }
                   }
               }
               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

    

          /// <summary>
         /// 添加预警信息
         /// </summary>
         /// <param name="Model">预警实体</param>
         /// <returns>true 成功,false 失败</returns>
       public static bool InsertWarningInfo(FinanceWarningModel Model)
       {
           if (Model == null) return false;
           //Model.CompanyCD = "1001";
           if (Model.CompanyCD == null) Model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           try
           {
              return   FinanceWarningDBHelper.InsertWarningInfo(Model);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

          /// <summary>
         /// 更新预警信息
         /// </summary>
         /// <param name="Model">预警实体</param>
         /// <returns>true 成功,false 失败</returns>
       public static bool UpdateWarningInfo(FinanceWarningModel Model)
       {
           if (Model == null) return false;
           //Model.CompanyCD = "1001";
           if (Model.CompanyCD == null) Model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           try
           {
               return FinanceWarningDBHelper.UpdateWarningInfo(Model);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

          /// <summary>
         /// 根据指定条件删除预警信息
         /// </summary>
         /// <param name="CompanyCD">企业编码</param>
         /// <param name="ID">主键ID</param>
         /// <returns>true 成功,false 失败</returns>
       public static bool DelWaringInfo(string ID)
       {
           if (string.IsNullOrEmpty(ID)) return false;
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           //string CompanyCD = "1001";
           try
           {
               return FinanceWarningDBHelper.DelWaringInfo(CompanyCD,ID);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }



   }
}
