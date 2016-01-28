using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Office.StorageManager;
using System.Collections;


namespace XBase.Business.Office.StorageManager
{
    public class StorageCostBus
    {
        /// <summary>
        /// 获取最后存货成本的计算日期
        /// </summary>
        /// <param name="yearMonth"></param>
        /// <param name="companyCD"></param>
        /// <returns></returns>
        public static string GetLastCalculationDate(string yearMonth, string companyCD)
        {
            return StorageCostDBHelper.GetLastCalculationDate(yearMonth, companyCD);
        }

        /// <summary>
        /// 计算存货成本
        /// </summary>
        /// <param name="companyCD"></param>
        /// <param name="currentYearMonth"></param>
        /// <param name="preYearMonth"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static string CalculationStorageCost(string companyCD, string currentYearMonth, string preYearMonth, string startDate, string endDate, int UserID)
        {
            return StorageCostDBHelper.CalculationStorageCost(companyCD, currentYearMonth, preYearMonth, startDate, endDate, UserID);
        }


        #region 读取存货成本列表
        /// <summary>
        /// 读取存货成本列表
        /// </summary>
        /// <param name="htParams">查询参数集</param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetStorageCostList(Hashtable htParams, int PageIndex, int PageSize, string OrderBy, ref int TotalCount)
        {
            return Data.Office.StorageManager.StorageCostDBHelper.GetStorageCostList(htParams, PageIndex, PageSize, OrderBy, ref TotalCount);
        }
        #endregion

        #region 修改指定的期末成本
        /// <summary>
        /// 修改制定的期末成本
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool EditPeriodEndCost(decimal cost, int ID)
        {
            return Data.Office.StorageManager.StorageCostDBHelper.EditPeriodEndCost(cost, ID);
        }
        #endregion

    }

}
