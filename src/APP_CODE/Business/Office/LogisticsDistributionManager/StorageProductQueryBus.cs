using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace XBase.Business.Office.LogisticsDistributionManager
{
    public class StorageProductQueryBus
    {
        #region 读取分店存量表
        public static DataTable GetSubStorageProductList(int PageIndex, int PageSize, string OrderBy, Hashtable htPara, ref int TotalCount)
        {
            return XBase.Data.Office.LogisticsDistributionManager.StorageProductQueryDBHelper.GetSubStorageProductList(PageIndex, PageSize, OrderBy, htPara, ref TotalCount);
        }

        /*不分页*/
        public static DataTable GetSubStorageProductList(string OrderBy, Hashtable htPara)
        {
            return XBase.Data.Office.LogisticsDistributionManager.StorageProductQueryDBHelper.GetSubStorageProductList(OrderBy, htPara);
        }
        #endregion

        #region 读取分仓存量表
        public static DataTable GetStorageProductList(int PageIndex, int PageSize, string OrderBy, Hashtable htPara, ref int TotalCount)
        {
            return XBase.Data.Office.LogisticsDistributionManager.StorageProductQueryDBHelper.GetStorageProductList(PageIndex, PageSize, OrderBy, htPara, ref TotalCount);
        }

        /*不分页*/
        public static DataTable GetStorageProductList( string OrderBy, Hashtable htPara)
        {
            return XBase.Data.Office.LogisticsDistributionManager.StorageProductQueryDBHelper.GetStorageProductList(OrderBy, htPara);
        }
        #endregion

        /// <summary>
        /// 读取分店存量表物品批次
        /// </summary>
        /// <param name="CompanyCD">机构</param>
        /// <param name="ProductID">物品ID</param>
        /// <param name="DeptID">分店ID</param>
        /// <returns></returns>
        public static DataTable GetSubBatchNo(string CompanyCD, int ProductID, int DeptID)
        {
            return XBase.Data.Office.LogisticsDistributionManager.StorageProductQueryDBHelper.GetSubBatchNo(CompanyCD, ProductID, DeptID);
        }
    }
}
