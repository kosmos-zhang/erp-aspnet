/**********************************************
 * 描述：     固定资产折旧业务处理
 * 建立人：   莫申林
 * 建立时间： 2009/05/09
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;
using XBase.Business.Common;
using System.Collections;
using XBase.Model.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.FinanceManager
{
   public class FixAssetDeprDetailBus
    {

       /// <summary>
      /// 获取固定资产折旧明细信息
      /// </summary>
      /// <param name="ID">主键</param>
      /// <param name="FixName">资产名称</param>
      /// <param name="FixType">资产类别</param>
      /// <param name="StartDate">开始日期</param>
      /// <param name="EndDate">结束日期</param>
      /// <returns></returns>
       public static DataTable GetFixAssetDeprDetailInfo(string ID, string FixName, string FixType, string StartDate, string EndDate)
       {
           try
           {
               return FixAssetDeprDetailDBHelper.GetFixAssetDeprDetailInfo(ID,FixName,FixType,StartDate,EndDate);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

      /// <summary>
      /// 获取折旧汇总记录
      /// </summary>
      /// <param name="FixName"></param>
      /// <param name="FixType"></param>
      /// <param name="StartDate"></param>
      /// <param name="EndDate"></param>
      /// <returns></returns>
      public static string GetAssetDeprTotalIDS(string FixName, string FixType, string StartDate, string EndDate)
       {
           try
           {
               return FixAssetDeprDetailDBHelper.GetAssetDeprTotalIDS(FixName, FixType, StartDate, EndDate);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
      /// 获取固定资产折旧汇总信息
      /// </summary>
      /// <param name="ids"></param>
      /// <returns></returns>
      public static DataTable GetFixAssetDeprTotal(string ids)
      {
          try
          {
              return FixAssetDeprDetailDBHelper.GetFixAssetDeprTotal(ids);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
    }
}
