using System;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Common;
using System.Collections;
using XBase.Data.Common;
using XBase.Model.Common;
using XBase.Business.Common;
using System.Collections.Generic;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.StorageManager;

namespace XBase.Business.Office.StorageManager
{
   public   class DayEndBus
    {
       /// <summary>
       /// 从分仓存量表中获取改公司的所有产品信息
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable GetCompanyProductList(string CompanyCD)
       {
           return DayEndDBHelper.GetCompanyProductList(CompanyCD);
       }
       /// <summary>
       /// 获得前一天的某个物品的结存量
       /// </summary>
       /// <param name="ProductID"></param>
       /// <param name="BatchNo"></param>
       /// <param name="StorageID"></param>
       /// <param name="DailyDate"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static decimal GetFrontDayCount(string ProductID, string BatchNo, string StorageID, string DailyDate, string CompanyCD)
       {
           return DayEndDBHelper.GetFrontDayCount(ProductID ,BatchNo ,StorageID ,DailyDate ,CompanyCD );
       }

       /// <summary>
       /// 获得分仓存量表里的数据
       /// </summary>
       /// <param name="ProductID"></param>
       /// <param name="BatchNo"></param>
       /// <param name="StorageID"></param>
       /// <param name="DailyDate"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static decimal GetFirstDayCount(string ProductID, string BatchNo, string StorageID, string CompanyCD)
       {
           return DayEndDBHelper.GetFirstDayCount(ProductID, BatchNo, StorageID, CompanyCD);
       }
       /// <summary>
       /// 获得库存流水账表中取得当天的某个物品的各种单据数量信息
       /// </summary>
       /// <param name="ProductID"></param>
       /// <param name="BatchNo"></param>
       /// <param name="StorageID"></param>
       /// <param name="DailyDate"></param>
       /// <param name="CompanyCD"></param>
       /// <param name="ItemsType"></param>
       /// <returns></returns>
       public static decimal GetDayItemsCount(string ProductID, string BatchNo, string StorageID, string DailyDate, string CompanyCD, int ItemsType)
       {
           return DayEndDBHelper.GetDayItemsCount(ProductID, BatchNo, StorageID, DailyDate, CompanyCD, ItemsType);
       }
       /// <summary>
       /// 获得库存流水账表中取得当天的某个物品的各种单据金额信息
       /// </summary>
       /// <param name="ProductID"></param>
       /// <param name="BatchNo"></param>
       /// <param name="StorageID"></param>
       /// <param name="DailyDate"></param>
       /// <param name="CompanyCD"></param>
       /// <param name="ItemsType"></param>
       /// <returns></returns>
       public static decimal GetDayItemsPrice(string ProductID, string BatchNo, string StorageID, string DailyDate, string CompanyCD, int ItemsType)
       {
           return DayEndDBHelper.GetDayItemsPrice(ProductID, BatchNo, StorageID, DailyDate, CompanyCD, ItemsType);
       }

       /// <summary>
       ///  从其他出库单据中分离出当天采购退货的信息
       /// </summary>
       /// <param name="ProductID"></param>
       /// <param name="BatchNo"></param>
       /// <param name="StorageID"></param>
       /// <param name="DailyDate"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable GetPurchaseRejectInfo(string ProductID, string BatchNo, string StorageID, string DailyDate, string CompanyCD)
       {
           return DayEndDBHelper.GetPurchaseRejectInfo(ProductID,BatchNo ,StorageID ,DailyDate ,CompanyCD );
       }


       #region 查询日结列表所需数据
       /// <summary>
       /// 查询日结列表所需数据
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageCount"></param>
       /// <param name="orderBy"></param>
       /// <param name="TotalCount"></param>
       /// <param name="day"></param>
       /// <returns></returns>
       public static DataTable SelectDayInfo(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string day )
       {
           try
           {
               return DayEndDBHelper.SelectDayInfo(pageIndex, pageCount, orderBy, ref TotalCount, day);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion
       /// <summary>
       /// 防止重复日结某一天，出现数据冗余，先删除当天在日结表中的数据
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <param name="day"></param>
       /// <returns></returns>
       public static bool DeleteDay(string CompanyCD, string day)
       {
           return DayEndDBHelper.DeleteDay(CompanyCD,day );
       }


       /// <summary>
       /// 判断前一天是否做日结
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <param name="day"></param>
       /// <returns></returns>
       public static bool CheckDay(string CompanyCD, string day)
       {
           return DayEndDBHelper.CheckDay(CompanyCD,  day);
       }

       /// <summary>
       /// 判断前一天是否做日结(false 是第一次日结)
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <param name="day"></param>
       /// <returns></returns>
       public static bool CheckFirstOperate(string CompanyCD)
       {
           return DayEndDBHelper.CheckFirstOperate(CompanyCD);
       }




       /// <summary>
       /// 判断流水账表是否有当天的业务
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <param name="day"></param>
       /// <returns></returns>
       public static bool isHaveData(string CompanyCD, string day)
       {
           return DayEndDBHelper.isHaveData(CompanyCD, day);
       }
       /// <summary>
       /// 获取日结明细信息
       /// </summary>
       /// <param name="ProductID"></param>
       /// <param name="BatchNo"></param>
       /// <param name="DeptID"></param>
       /// <param name="DailyDate"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable GetStorageDailyInfo(string ProductID, string BatchNo, string StorageID, string DailyDate, string CompanyCD)
       {
           return DayEndDBHelper.GetStorageDailyInfo(ProductID, BatchNo, StorageID, DailyDate, CompanyCD);
       }

      /// <summary>
       /// 返回第一次日结的日期(FirstDailyDate)和最后一次做日结的日期(LastDailyDate)
      /// </summary>
      /// <param name="CompanyCD"></param>
      /// <returns></returns>
       public static DataTable GetOperateDateInfo( string CompanyCD)
       {
           return DayEndDBHelper.GetOperateDateInfo(  CompanyCD);
       }

    }
}
