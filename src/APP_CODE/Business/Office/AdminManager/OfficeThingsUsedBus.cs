/**********************************************
 * 类作用：   办公用品领用事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/05/09
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;

namespace XBase.Business.Office.AdminManager
{
    /// <summary>
    /// 类名：OfficeThingsUsedBus
    /// 描述：办公用品领用事务层处理
    /// 作者：lysong
    /// 创建时间：2009/05/09
    /// </summary>
   public class OfficeThingsUsedBus
   {
        #region 添加办公用品领用信息
        /// <summary>
        /// 添加办公用品领用信息
        /// </summary>
        /// <param name="OfficeThingsUsedM">领用单主信息</param>
        /// <param name="OfficeThingsUsedInfos">领用详细信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool InserOfficeThingsUsedInfo(OfficeThingsUsedModel OfficeThingsUsedM, string OfficeThingsUsedInfos)
        {
            return OfficeThingsUsedDBHelper.InserOfficeThingsUsedInfo(OfficeThingsUsedM, OfficeThingsUsedInfos);
        }
        #endregion
        #region 修改用品领用单信息
        /// <summary>
        /// 修改用品领用单信息
        /// </summary>
        /// <param name="OfficeThingsBuyM">领用单主信息</param>
        /// <param name="OfficeThingsUsedInfos">领用单详细信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool UpdateOfficeThingsUsedInfo(OfficeThingsUsedModel OfficeThingsUsedM, string OfficeThingsUsedInfos)
        {
            return OfficeThingsUsedDBHelper.UpdateOfficeThingsUsedInfo(OfficeThingsUsedM, OfficeThingsUsedInfos);
        }
        #endregion
        #region 用品领用确认
        /// <summary>
        /// 用品领用确认
        /// </summary>
        /// <param name="OfficeThingsUsedM">领用单主信息</param>
        /// <param name="OfficeThingsUsedInfos">领用单详细信息</param>
        /// <returns>确认是否成功 false:失败，true:成功</returns>
        public static bool ConfirmUsedInfo(OfficeThingsUsedModel OfficeThingsUsedM,string OfficeThingsUsedInfos)
        {
            return OfficeThingsUsedDBHelper.ConfirmUsedInfo(OfficeThingsUsedM, OfficeThingsUsedInfos);
        }
        #endregion

        #region 获取办公用品领用列表
        /// <summary>
        /// 获取办公用品领用列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsUsedInfoList(string BuyRecordNo, string Title, string txtJoinUser, string BuyDeptID, string StartDate, string EndDate, string CompanyID,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            try
            {
                return OfficeThingsUsedDBHelper.GetOfficeThingsUsedInfoList(BuyRecordNo, Title, txtJoinUser, BuyDeptID, StartDate, EndDate, CompanyID, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  根据领用单ID获取入库信息查看或修改
        /// <summary>
        /// 根据领用单ID获取入库信息查看或修改
        /// </summary>
        /// <param name="ID">领用单ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsUsedInfoByID(string ID)
        {
            try
            {
                return OfficeThingsUsedDBHelper.GetOfficeThingsUsedInfoByID(ID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region  根据领用单NO获取入库信息查看或修改
        /// <summary>
        /// 根据领用单ID获取入库信息查看或修改
        /// </summary>
        /// <param name="ID">领用单ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsUsedInfoByNO(string NO,string CompanyCD)
        {
            try
            {
                return OfficeThingsUsedDBHelper.GetOfficeThingsUsedInfoByNO(NO,CompanyCD);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
