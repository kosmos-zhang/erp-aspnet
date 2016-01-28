/**********************************************
 * 类作用：   办公用品事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/05/07
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;

namespace XBase.Business.Office.AdminManager
{
    /// <summary>
    /// 类名：OfficeThingsInfoBus
    /// 描述：办公用品事务层处理
    /// 作者：lysong
    /// 创建时间：2009/05/07
    /// </summary>
   public class OfficeThingsStorageBus
    {
        #region 添加入库库存信息
        /// <summary>
        /// 添加入库库存信息
        /// </summary>
        /// <param name="OfficeThingsBuyM">入库单主信息</param>
        /// <param name="OfficeThingsInStorageInfos">入库详细信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool InserInstorageInfo(OfficeThingsBuyModel OfficeThingsBuyM, string OfficeThingsInStorageInfos)
        {
            return OfficeThingsStorageDBHelper.InserInstorageInfo(OfficeThingsBuyM, OfficeThingsInStorageInfos);
        }
        #endregion
        #region 修改入库库存信息
        /// <summary>
        /// 修改入库库存信息
        /// </summary>
        /// <param name="OfficeThingsBuyM">入库单主信息</param>
        /// <param name="OfficeThingsInStorageInfos">入库详细信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool UpdateInstorageInfo(OfficeThingsBuyModel OfficeThingsBuyM, string OfficeThingsInStorageInfos)
        {
            return OfficeThingsStorageDBHelper.UpdateInstorageInfo(OfficeThingsBuyM, OfficeThingsInStorageInfos);
        }
        #endregion
        #region 入库确认
        /// <summary>
        /// 入库确认
        /// </summary>
        /// <param name="OfficeThingsBuyM">入库单主信息</param>
        /// <param name="OfficeThingsInStorageInfos">入库详细信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool ConfirmInstorageInfo(OfficeThingsBuyModel OfficeThingsBuyM, string OfficeThingsInStorageInfos)
        {
            return OfficeThingsStorageDBHelper.ConfirmInstorageInfo(OfficeThingsBuyM, OfficeThingsInStorageInfos);
        }
        #endregion
        #region  根据入库ID获取入库信息查看或修改
        /// <summary>
        /// 根据入库ID获取入库信息查看或修改
        /// </summary>
        /// <param name="ID">入库单ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsStorageInfoByID(string ID, string CompanyID)
        {
            try
            {
                return OfficeThingsStorageDBHelper.GetOfficeThingsStorageInfoByID(ID,CompanyID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  根据入库NO获取入库信息查看或修改
        /// <summary>
        /// 根据入库NO获取入库信息查看或修改
        /// </summary>
        /// <param name="NO">入库单NO</param>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsStorageInfoByNO(string NO, string CompanyID)
        {
            try
            {
                return OfficeThingsStorageDBHelper.GetOfficeThingsStorageInfoByNO(NO, CompanyID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 获取办公用品入库列表
        /// <summary>
        /// 获取办公用品入库列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsInstorageInfoList(string BuyRecordNo, string Title, string ThingName, string txtJoinUser, string BuyDeptID, string StartDate, string EndDate, string CompanyID, int pageIndex, int pageCount, string ord, ref int TotalCount, out decimal TotalMoeny, out decimal SumCount)
        {
            try
            {
                return OfficeThingsStorageDBHelper.GetOfficeThingsInstorageInfoList(BuyRecordNo, Title, ThingName, txtJoinUser, BuyDeptID, StartDate, EndDate, CompanyID, pageIndex, pageCount, ord, ref TotalCount, out TotalMoeny, out SumCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
