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
  public    class DayReportBus
    {
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
        public static DataTable GetTotalInAndOutDetail(ProductInfoModel model, string DailyDate, string EndDate, string BatchNo, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return MonthDayDBHelper.GetTotalInAndOutDetail(model, DailyDate, EndDate, BatchNo, EFIndex, EFDesc, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 进销存汇总表(全部)
        /// <summary>
        /// 进销存汇总表
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
        public static DataTable GetAllStorageInAndOutInfo(ProductInfoModel model, string DailyDate, string EndDate, string BatchNo, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, out DataTable dt, ref int totalCount)
        {
            try
            {
                return MonthDayDBHelper.GetAllStorageInAndOutInfo(model, DailyDate, EndDate, BatchNo, EFIndex, EFDesc, pageIndex, pageCount, OrderBy, out dt, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 进销存汇总表(单品)
        /// <summary>
        /// 进销存汇总表
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
        public static DataTable GetStorageInAndOutTotalInfo(ProductInfoModel model, string DailyDate, string EndDate, string BatchNo, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, out DataTable dt, ref int totalCount)
        {
            try
            {
                return MonthDayDBHelper.GetStorageInAndOutTotalInfo(model, DailyDate, EndDate, BatchNo, EFIndex, EFDesc, pageIndex, pageCount, OrderBy, out dt, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
