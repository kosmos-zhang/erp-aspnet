/********************************************** 
 * 类作用：   红冲出库和红冲出库明细事务层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/29
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

namespace XBase.Business.Office.StorageManager
{
    public class StorageSearchBus
    {
        #region 查询：库存报损单
        /// <summary>
        /// 查询库存报损单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetProductStorageTableBycondition(StorageProductModel model, XBase.Model.Office.SupplyChain.ProductInfoModel pdtModel, string ProductCount1, string EFIndex, string EFDesc, int pageIndex, int pageCount, string ord,string BatchNo, ref int TotalCount)
        {
            return StorageSearchDBHelper.GetProductStorageTableBycondition(model, pdtModel, ProductCount1, EFIndex, EFDesc, pageIndex, pageCount, ord,BatchNo, ref TotalCount);
        }

        public static DataTable GetProductStorageTableBycondition(StorageProductModel model, XBase.Model.Office.SupplyChain.ProductInfoModel pdtModel, string ProductCount1, string EFIndex, string EFDesc, string orderby, string BatchNo)
        {
            return StorageSearchDBHelper.GetProductStorageTableBycondition(model, pdtModel, ProductCount1, EFIndex, EFDesc, orderby,BatchNo);
        }


        /// <summary>
        /// 现有存量汇总
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pdtModel"></param>
        /// <param name="ProductCount1"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <param name="orderby"></param>
        /// <param name="BatchNo"></param>
        /// <returns></returns>
        public static string GetSumStorageInfo(StorageProductModel model, XBase.Model.Office.SupplyChain.ProductInfoModel pdtModel, string ProductCount1, string EFIndex, string EFDesc, string orderby, string BatchNo)
        {
            return StorageSearchDBHelper.GetSumStorageInfo(model, pdtModel, ProductCount1, EFIndex, EFDesc, orderby, BatchNo);
        }
        #endregion

        /// <summary>
        /// 获取对应的批次
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ProductNo"></param>
        /// <param name="StorageID"></param>
        /// <returns></returns>
        public static string GetBatchNo(string ProductNo, string StorageID)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return StorageSearchDBHelper.GetBatchNo(CompanyCD, ProductNo, StorageID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取门店对应的批次
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ProductNo"></param>
        /// <param name="StorageID"></param>
        /// <returns></returns>
        public static string GetSubBatchNo()
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return StorageSearchDBHelper.GetSubBatchNo(CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
