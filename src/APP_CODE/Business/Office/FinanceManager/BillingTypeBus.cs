/**********************************************
 * 描述：     开票类别业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/04/14
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using System.Data.SqlClient;
using XBase.Common;
namespace XBase.Business.Office.FinanceManager
{
  public  class BillingTypeBus
    {

          /// <summary>
         /// 添加开票类型
         /// </summary>
        /// <param name="CompanyCD">名称</param>
       /// <param name="UsedStatus">使用状态</param>
       /// <param name="Remark">备注</param>
       /// <returns>true 成功，false失败</returns>
      public static bool InsertBillingType(string Name, string UsedStatus, string Remark,out int ID)
      {
          string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          try
          {
              return BillingTypeDBHelper.InsertBillingType(CompanyCD,Name,UsedStatus,Remark,out ID);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

        /// <summary>
       /// 修改开票类型
       /// </summary>
       /// <param name="ID">主键</param>
       /// <param name="Name">名称</param>
       /// <param name="UsedStatus">使用状态</param>
       /// <param name="Remark">备注</param>
       /// <returns>true 成功，false失败</returns>
      public static bool UpdateBillingType(string ID, string Name, string UsedStatus, string Remark)
      {
          try
          {
              return BillingTypeDBHelper.UpdateBillingType(ID,Name,UsedStatus,Remark);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

        /// <summary>
       /// 删除开票类别
       /// </summary>
       /// <param name="ID">主键</param>
       /// <returns>true 成功，false失败</returns>
      public static bool DelBillingType(string ID)
      {
          try
          {
              return BillingTypeDBHelper.DelBillingType(ID);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

        /// <summary>
       /// 获取开票类型所有信息
       /// </summary>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns>DataTable</returns>
      public static DataTable GetBillingTypeInfo()
      {
          string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          try
          {
              return BillingTypeDBHelper.GetBillingTypeInfo(CompanyCD);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

       /// <summary>
       /// 获取开票类型状态为启用状态的信息
       /// </summary>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns>DataTable</returns>
      public static DataTable GetBillingTypeIsUsed()
      {
          string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          try
          {
              return BillingTypeDBHelper.GetBillingTypeIsUsed(CompanyCD);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

    }
}
