using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.OperatingModel.IntegratedData;
using XBase.Model.Office.SupplyChain;

namespace XBase.Business.OperatingModel.IntegratedData
{
    public class SotreBuySellStockByDayBus
    {
        #region 根据条件获取门店进销存日报表_单个
        public static DataTable GetBuySellStockByDayList(string Color, string FromAddr, string Manufacturer, string Size, string Material, string BarCode,
            string DeptID, string EnterDate, string ProductNo, string ProductName, string Specification,
            string EFIndex, string EFDesc, string BatchNo, string StorageID, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            return SotreBuySellStockByDayDBHelper.GetBuySellStockByDayList(Color, FromAddr, Manufacturer, Size, Material, BarCode,
                DeptID, EnterDate, ProductNo, ProductName, Specification,
                EFIndex, EFDesc, BatchNo, StorageID, pageIndex, pageCount, OrderBy, ref  totalCount);
        }

        //求和
        public static DataTable GetBuySellStockByDay_Sum(string Color, string FromAddr, string Manufacturer, string Size, string Material, string BarCode,
            string DeptID, string EnterDate, string ProductNo, string ProductName, string Specification,
            string EFIndex, string EFDesc, string BatchNo, string StorageID)
        {
            return SotreBuySellStockByDayDBHelper.GetBuySellStockByDay_Sum(Color, FromAddr, Manufacturer, Size, Material, BarCode,
                DeptID, EnterDate, ProductNo, ProductName, Specification,
                EFIndex, EFDesc, BatchNo, StorageID);
        }
        #endregion

        #region 根据条件获取门店进销存日报表_汇总
        public static DataTable GetBuySellStockByDayList_hz(string DeptID, string EnterDate, string BatchNo,
                                                       int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            return SotreBuySellStockByDayDBHelper.GetBuySellStockByDayList_hz(DeptID, EnterDate, BatchNo, pageIndex, pageCount, OrderBy, ref  totalCount);
        }

        public static DataTable GetBuySellStockByDayList_Sum(string DeptID, string EnterDate, string BatchNo)
        {
            return SotreBuySellStockByDayDBHelper.GetBuySellStockByDayList_Sum(DeptID, EnterDate, BatchNo);
        }
        #endregion

        #region 根据条件获取门店进销存日报表_单个_导出
        public static DataTable GetBuySellStockByDayList_Export(string Color, string FromAddr, string Manufacturer, string Size, string Material, string BarCode,
            string DeptID, string EnterDate, string ProductNo, string ProductName, string Specification,
           string EFIndex, string EFDesc, string BatchNo, string OrderBy)
        {
            try
            {
                DataTable dt = SotreBuySellStockByDayDBHelper.GetBuySellStockByDayList_Export(Color, FromAddr, Manufacturer,
                    Size, Material, BarCode, DeptID, EnterDate, ProductNo, ProductName, Specification, EFIndex, EFDesc,
                    BatchNo, OrderBy);
                return dt;
            }
            catch
            {
                return null;
            }

        }
        #endregion

        #region 根据条件获取门店进销存日报表_汇总_导出
        public static DataTable GetBuySellStockByDayList_hz_Export(string DeptID, string EnterDate, string BatchNo, string OrderBy)
        {
            try
            {
                return SotreBuySellStockByDayDBHelper.GetBuySellStockByDayList_hz_Export(DeptID, EnterDate, BatchNo, OrderBy);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 根据条件获取门店进销存日报表_合计
        /// <summary>
        /// 根据条件获取门店进销存日报表
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="EnterDate"></param>
        /// <param name="BatchNo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetBuySellStockByDayList_Sum(string Color, string FromAddr, string Size, string Material, string BarCode,
            string ProductNo, string ProductName, string Specification, string Manufacturer, string DeptID, string EnterDate, string BatchNo, string OrderBy)
        {
            return SotreBuySellStockByDayDBHelper.GetBuySellStockByDayList_Sum(Color, FromAddr, Size, Material, BarCode,
             ProductNo, ProductName, Specification, Manufacturer, DeptID, EnterDate, BatchNo, OrderBy);
        }
        #endregion

    }

    /// <summary>
    /// 门店进销存月报表 
    /// </summary>
    public class SubStoreMonthReportBus
    {
        /// <summary>
        /// 门店进销存月报表
        /// </summary>
        /// <param name="model">产品信息</param>
        /// <param name="SumModel">汇总方式</param>
        /// <param name="SubStoreID">部门</param>
        /// <param name="dStime">开始时间</param>
        /// <param name="dEtime">结束时间</param>
        /// <param name="BatchNo">批次</param>
        /// <param name="EFIndex">扩展索引</param>
        /// <param name="EFDesc">扩展条件</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageCount">每页数</param>
        /// <param name="OrderBy">排序</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        public static DataTable GetSubStoreMonthReport(ProductInfoModel model, bool SumModel, string SubStoreID
            , DateTime dStime, DateTime dEtime, string BatchNo, string EFIndex, string EFDesc
            , int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            return SubStoreMonthReportDBHelper.GetSubStoreMonthReport(model, SumModel, SubStoreID, dStime, dEtime, BatchNo, EFIndex, EFDesc
                , pageIndex, pageCount, OrderBy, ref totalCount);
        }

        /// <summary>
        /// 门店进销存月报表汇总信息
        /// </summary>
        /// <param name="model">产品信息</param>
        /// <param name="SumModel">汇总方式</param>
        /// <param name="SubStoreID">部门</param>
        /// <param name="dStime">开始时间</param>
        /// <param name="dEtime">结束时间</param>
        /// <param name="BatchNo">批次</param>
        /// <param name="EFIndex">扩展索引</param>
        /// <param name="EFDesc">扩展条件</param>
        /// <returns></returns>
        public static DataTable GetSubStoreMonthReportTotal(ProductInfoModel model, bool SumModel, string SubStoreID
            , DateTime dStime, DateTime dEtime, string BatchNo, string EFIndex, string EFDesc)
        {
            return SubStoreMonthReportDBHelper.GetSubStoreMonthReportTotal(model, SumModel, SubStoreID, dStime, dEtime
                , BatchNo, EFIndex, EFDesc);
        }

        /// <summary>
        /// 门店进销存月报表(导出使用)
        /// </summary>
        /// <param name="model">产品信息</param>
        /// <param name="SumModel">汇总方式</param>
        /// <param name="SubStoreID">部门</param>
        /// <param name="dStime">开始时间</param>
        /// <param name="dEtime">结束时间</param>
        /// <param name="BatchNo">批次</param>
        /// <param name="EFIndex">扩展索引</param>
        /// <param name="EFDesc">扩展条件</param>
        /// <returns></returns>
        public static DataTable GetSubReportToExel(ProductInfoModel model, bool SumModel, string SubStoreID
            , DateTime dStime, DateTime dEtime, string BatchNo, string EFIndex, string EFDesc)
        {
            return SubStoreMonthReportDBHelper.GetSubReportToExel(model, SumModel
                , SubStoreID, dStime, dEtime, BatchNo, EFIndex, EFDesc);
        }
    }
}
