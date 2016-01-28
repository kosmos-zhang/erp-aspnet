/**********************************************
 * 类作用：   期初库存和期初库存明细事务层处理
 * 建立人：   肖合明
 * 建立时间： 2009/06/03
 ***********************************************/

using System;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Business.Common;
using System.Collections.Generic;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.StorageManager
{
    public class StockAccountBus
    {

        #region 产品出入库报表表
        public static DataTable GetProductInOutInfo(StockAccountModel model)
        {
            try
            {
                return StockAccountDBHelper.GetProductInOutInfo(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region 产品销售信息（销售出库）
        /// <summary>
        /// 产品销售信息（销售出库）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetInfoByOutSell(StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            try
            {
                return StockAccountDBHelper.GetInfoByOutSell(model, pageIndex, pageCount, ord, ref TotalCount, iflist);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region 产品入库信息（采购入库）
        /// <summary>
        /// 产品入库信息（采购入库）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetInfoByInPurchase(StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            try
            {
                return StockAccountDBHelper.GetInfoByInPurchase(model, pageIndex, pageCount, ord, ref TotalCount, iflist);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


        #region 产品库存余额
        public static DataTable GetProductStockTotalPrice(StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            try
            {
                return StockAccountDBHelper.GetProductStockTotalPrice(model, pageIndex, pageCount, ord, ref TotalCount, iflist);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region 库存结构分析

        public static DataTable GetStockStructAnalysis(StockAccountModel model, string ordercolumn, string ordertype)
        {
            try
            {
                return StockAccountDBHelper.GetStockStructAnalysis(model, ordercolumn, ordertype);
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion


        #region  库存单品分析

        //获取物品的销售和购进数量、总额、金额、税额
        public static DataTable GetProdcutSellandPurchaseInfo(StockAccountModel model)
        {
            try
            {
                return StockAccountDBHelper.GetProdcutSellandPurchaseInfo(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //获取当前物品的库存信息和价格信息
        public static DataTable GetProductStockAndPriceInfo(StockAccountModel model)
        {
            try
            {
                return StockAccountDBHelper.GetProductStockAndPriceInfo(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //获取指定物品的损益信息
        public static DataTable GetProdcutLoseInfo(StockAccountModel model)
        {
            try
            {
                return StockAccountDBHelper.GetProdcutLoseInfo(model);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        //库存分析:仓库、仓库数量、成本单价、仓库金额库存上限、库存下限
        public static DataTable GetStoProductInfo(StockAccountModel model)
        {
            try
            {
                return StockAccountDBHelper.GetStoProductInfo(model);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        //购进明细：单据编号、日期、单位编号、单位名称、仓库、单位、数量
        public static DataTable GetPurchaseDetail(StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            try
            {
                return StockAccountDBHelper.GetPurchaseDetail(model, pageIndex, pageCount, ord, ref TotalCount, iflist);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //销往明细：单据编号、日期、单位编号、单位名称、货位编号、货位名称、批号、包装单位、包装数量、零散数量、数量、含税价
        public static DataTable GetSellDetail(StockAccountModel model, int pageIndex, int pageCount, string ord, ref int TotalCount, bool iflist)
        {
            try
            {
                return StockAccountDBHelper.GetSellDetail(model, pageIndex, pageCount, ord, ref TotalCount, iflist);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
