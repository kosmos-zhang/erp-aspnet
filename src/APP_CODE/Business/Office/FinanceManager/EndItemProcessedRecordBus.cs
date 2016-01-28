/**********************************************
 * 描述：     期末项目检索业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/05、04
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
namespace XBase.Business.Office.FinanceManager
{
  public  class EndItemProcessedRecordBus
    {

      #region 根据企业查找企业固定资产期末最大期数
      public static int GetMaxPeriodNum(int ItemID)
      {
          try
          {
              string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
              return EndItemProcessedRecordDBHelper.GetMaxPeriodNum(CompanyCD,ItemID);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      #endregion

      #region 查询当期项目是否进行期末处理
      public static string CheckCurrentPeriodIsProced(string PeriodNum,int ItemId)
      {
          string result = string.Empty; 
      
          try
          {
              string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

              result = EndItemProcessedRecordDBHelper.CheckCurrentPeriodIsProced(CompanyCD, PeriodNum, ItemId);
     
              return result;
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      #endregion

      #region  检查固定资产当期是否计提
      public static bool CheckFixAssetIsJT(string PeriodNum, int ItemID)
      {
          try
          {
              string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
              return EndItemProcessedRecordDBHelper.CheckFixAssetIsJT(PeriodNum,ItemID,CompanyCD);

          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      #endregion
    }


}
