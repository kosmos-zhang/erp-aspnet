/**********************************************
 * 描述：     报表项目业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/03/09
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using System.Data.SqlClient;
using XBase.Common;
namespace XBase.Business.Office.FinanceManager
{
  public  class ReportItemSettingBus
    {
       /// <summary>
      /// 新增报表项目信息
      ///</summary>
      ///<param name="Model">实体</param>
     ///<returns>true 成功，false失败</returns>
      public static bool InsertReportItem(ReportItemSettingModel Model)
      {
          if (Model == null) return false;
          if (Model.CompanyCD == null) Model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          try
          {
              return ReportItemSettingDBHelper.InsertReportItem(Model);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

      /// <summary>
      /// 修改报表项目信息
      /// </summary>
      /// <param name="Model">实体</param>
      /// <returns>true 成功，false失败</returns>
      public static bool UpdateReportItem(ReportItemSettingModel Model)
      {
          if (Model == null) return false;
          if (Model.CompanyCD == null) Model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          try
          {
              return ReportItemSettingDBHelper.UpdateReportItem(Model);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

       /// <summary>
      /// 删除报表项目信息
      /// </summary>
      /// <param name="ID">主键</param>
      /// <returns>true 成功，false失败</returns>
      public static bool DelReportItem(string ID)
      {
          string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          try
          {
              return ReportItemSettingDBHelper.DelReportItem(CompanyCD,ID);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
       /// <summary>
      /// 获取报表项目信息
      /// </summary>
      /// <param name="CompanyCD">公司编码</param>
      /// <returns>DataTable</returns>
      public static DataTable GetReportItemInfo()
      {
          string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          if (string.IsNullOrEmpty(CompanyCD)) return null;
          try
          {
                DataTable dt=ReportItemSettingDBHelper.GetReportItemInfo(CompanyCD);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow rows in dt.Rows)
                    {
                        if (rows["ItemType"].ToString() == ConstUtil.ITEMTYPE_XJ_CODE)
                        {
                            rows["ItemType"] = ConstUtil.ITEMTYPE_XJ_NAME;
                        }
                        else if (rows["ItemType"].ToString() == ConstUtil.ITEMTYPE_ZC_CODE)
                        {
                            rows["ItemType"] = ConstUtil.ITEMTYPE_ZC_NAME;
                        }
                         if (rows["UsedStatus"].ToString() == ConstUtil.USED_STATUS_OFF)
                        {
                            rows["UsedStatus"] = ConstUtil.USED_STATUS_OFF_NAME;
                        }
                        else if (rows["UsedStatus"].ToString() == ConstUtil.USED_STATUS_ON)
                        {
                            rows["UsedStatus"] = ConstUtil.USED_STATUS_ON_NAME;
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
    }
}
