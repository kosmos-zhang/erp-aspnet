/**********************************************
 * 类作用：   库存查询事务层处理
 * 建立人：   肖合明
 * 建立时间： 2009/06/26
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.StorageManager;
using XBase.Common;
using System.Data;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.StorageManager
{
    public class StockSearchBus
    {
        #region 库存流水账查询
        //库存流水账查询
        public static DataTable GetLineInfo(string Confirmor, string type, StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            try
            {
                return StockSearchDBHelper.GetLineInfo(Confirmor, type, model, pageIndex, pageCount, ord, ref TotalCount, iflist);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


        //入库查询
        public static DataTable InStockSearch(string Confirmor, StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            try
            {
                return StockSearchDBHelper.InStockSearch(Confirmor, model, pageIndex, pageCount, ord, ref TotalCount, iflist);
            }
            catch (Exception)
            {

                throw;
            }
        }


        //出库查询

        public static DataTable OutStockSearch(string Confirmor, StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            try
            {
                return StockSearchDBHelper.OutStockSearch(Confirmor, model, pageIndex, pageCount, ord, ref TotalCount, iflist);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region 查询：库存查询
        /// <summary>
        /// 查询库存报损单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetProductStorageTableBycondition(StorageProductModel model, string ProductNo, string ProductName, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            try
            {
                return StockSearchDBHelper.GetProductStorageTableBycondition(model, ProductNo, ProductName, pageIndex, pageCount, ord, ref TotalCount, iflist);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


        #region 查询：库存限量报警
        /// <summary>
        /// 库存限量报警
        /// </summary>
        /// <param name="AlarmType">0-全部，1-上限报警，2-下限报警</param>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageProductAlarm(string AlarmType, StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            try
            {
                return StockSearchDBHelper.GetStorageProductAlarm(AlarmType, model, pageIndex, pageCount, ord, ref TotalCount, iflist);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


        #region 月结查询
        public static DataTable GetMothlyInfo(string MonthNo, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            try
            {
                return StockSearchDBHelper.GetMothlyInfo(MonthNo, CompanyCD, pageIndex, pageCount, ord, ref TotalCount, iflist);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion
    }

}
