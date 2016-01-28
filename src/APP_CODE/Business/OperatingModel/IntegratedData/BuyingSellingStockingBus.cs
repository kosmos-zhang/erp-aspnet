/**********************************************
 * 类作用：   进销存分析
 * 建立人：   王玉贞
 * 建立时间： 2010/05/05
 ***********************************************/

using System;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;
using XBase.Model.Office.SupplyChain;
using XBase.Data.OperatingModel.IntegratedData;

namespace XBase.Business.OperatingModel.IntegratedData
{
    public class BuyingSellingStockingBus
    {
        #region 进销存日报表
        /// <summary>
        /// 进销存日报表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="DailyDate"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetBuyingSellingStockingByDay(ProductInfoModel model, string DailyDate, string BatchNo,string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return BuyingSellingStockingDBHelper.GetBuyingSellingStockingByDay(model,DailyDate, BatchNo,EFIndex, EFDesc, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 总计：进销存日报表
        /// <summary>
        /// 进销存日报表总计
        /// </summary>
        /// <param name="model"></param>
        /// <param name="DailyDate"></param>
        /// <param name="BatchNo"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <returns></returns>
        public static DataTable GetSumBuyingSellingStockingByDay(ProductInfoModel model, string DailyDate, string BatchNo, string EFIndex, string EFDesc)
        {
            try
            {
                return BuyingSellingStockingDBHelper.GetSumBuyingSellingStockingByDay(model, DailyDate, BatchNo, EFIndex, EFDesc);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 进销存日报表明细
        /// <summary>
        /// 进销存日报表明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="HappenDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetBuyingSellingStockingByDayDetail(ProductInfoModel model, string DailyDate, string BatchNo, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return BuyingSellingStockingDBHelper.GetBuyingSellingStockingByDayDetail(model, DailyDate, BatchNo, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 进销存汇总表明细
        /// <summary>
        /// 进销存汇总表明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="HappenDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetTotalInAndOutDetail(ProductInfoModel model, string DailyDate,string EndDate, string BatchNo, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return BuyingSellingStockingDBHelper.GetTotalInAndOutDetail(model, DailyDate,EndDate, BatchNo, EFIndex, EFDesc, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 门店进销存日报表（分店报表）
        /// <summary>
        /// 门店进销存日报表（分店报表）
        /// </summary>
        /// <param name="model">检索条件实体</param>
        /// <param name="EFIndex">扩展属性</param>
        /// <param name="EFDesc">扩展属性</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetSubStoreInvoicingDateRptData(XBase.Model.Office.OperatingModel.SubStoreInvoicingDateRptModel model, string EFIndex, string EFDesc, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return BuyingSellingStockingDBHelper.GetSubStoreInvoicingDateRptData(model, EFIndex, EFDesc, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

        #region 总计：门店进销存日报表（分店报表）
        /// <summary>
        /// 总计：门店进销存日报表（分店报表）
        /// </summary>
        /// <param name="model"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <returns></returns>
        public static DataTable GetSumSubStoreInvoicingDateRptData(XBase.Model.Office.OperatingModel.SubStoreInvoicingDateRptModel model, string EFIndex, string EFDesc)
        {
            return BuyingSellingStockingDBHelper.GetSumSubStoreInvoicingDateRptData(model, EFIndex, EFDesc);
        }
        #endregion  

        #region 门店进销存月报表
        public static DataTable GetSubStoreMonthReport(string currentMonth, string preMonth, string CompanyCD, Hashtable htParams, string EFIndex, bool IsAll, int pageIndex, int pageSize, string OrderBy, ref int TotalCount)
        {

            return BuyingSellingStockingDBHelper.GetSubStoreMonthReport(currentMonth, preMonth, CompanyCD, htParams, EFIndex, IsAll,pageIndex,pageSize,OrderBy, ref TotalCount);
           
        }



        public static DataTable GetSubStoreMonthReport(string currentMonth, string preMonth, string CompanyCD, Hashtable htParams, string EFIndex, bool IsAll, string OrderBy)
        {

            return BuyingSellingStockingDBHelper.GetSubStoreMonthReport(currentMonth, preMonth, CompanyCD, htParams, EFIndex, IsAll, OrderBy);
        }

        #endregion

        #region 门店进销存月报表(分店报表 add by ellen)
        /// <summary>
        /// 门店进销存月报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="htParams"></param>
        /// <param name="StartDailyDate"></param>
        /// <param name="EndDailyDate"></param>
        /// <param name="EFIndex"></param>
        /// <param name="IsAll"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSubStoreMonthReportList(bool isAll,string CompanyCD, Hashtable htParams, string StartDailyDate, string EndDailyDate, string EFIndex, bool IsAll, int pageIndex, int pageSize, string OrderBy, ref int TotalCount)
        {
            return BuyingSellingStockingDBHelper.GetSubStoreMonthReportList(isAll,htParams, CompanyCD, StartDailyDate, EndDailyDate, EFIndex, pageIndex, pageSize, OrderBy, ref TotalCount);
        }
        #endregion

        #region 门店进销存月报表合计(分店报表 add by ellen)
        /// <summary>
        /// 门店进销存月报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="htParams"></param>
        /// <param name="StartDailyDate"></param>
        /// <param name="EndDailyDate"></param>
        /// <param name="EFIndex"></param>
        /// <param name="IsAll"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSubStoreMonthReportSum(bool isAll, string CompanyCD, Hashtable htParams, string StartDailyDate, string EndDailyDate, string EFIndex, bool IsAll, int pageIndex, int pageSize, string OrderBy, ref int TotalCount)
        {
            return BuyingSellingStockingDBHelper.GetSubStoreMonthReportSum(isAll, htParams, CompanyCD, StartDailyDate, EndDailyDate, EFIndex, pageIndex, pageSize, OrderBy, ref TotalCount);
        }
        #endregion

    }
}
