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
    public class OfficeThingsInfoBus
    {
        #region 添加办公用品信息
        /// <summary>
        /// 添加办公用品信息
        /// </summary>
        /// <param name="OfficeThingsInfoM">办公用品信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool AddOfficeThingsInfo(OfficeThingsInfoModel OfficeThingsInfoM)
        {
            return OfficeThingsInfoDBHelper.AddOfficeThingsInfo(OfficeThingsInfoM);
        }
        #endregion

        #region 查询办公用品档案列表
        /// <summary>
        /// 查询办公用品档案列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsInfoList(string ThingNo, string ThingName, string TypeIDHidden, string CompanyID,int pageIndex,int pageCount,string ord,ref int TotalCount)
        {
            try
            {
                return OfficeThingsInfoDBHelper.GetOfficeThingsInfoList(ThingNo, ThingName, TypeIDHidden, CompanyID, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  根据办公用品ID获取办公用品档案
        /// <summary>
        /// 根据办公用品ID获取办公用品档案
        /// </summary>
        /// <param name="ID">用品ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsInfoById(string ID)
        {
            try
            {
                return OfficeThingsInfoDBHelper.GetOfficeThingsInfoById(ID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  根据办公用品NO获取办公用品档案
        /// <summary>
        /// 根据办公用品NO获取办公用品档案
        /// </summary>
        /// <param name="NO">用品NO</param>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsInfoByNO(string NO,string CompanyCD)
        {
            try
            {
                return OfficeThingsInfoDBHelper.GetOfficeThingsInfoByNO(NO,CompanyCD);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 修改用品档案信息
        /// <summary>
        /// 修改用品档案信息
        /// </summary>
        /// <param name="OfficeThingsInfoM">用品档案信息</param>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateOfficeThingsInfo(OfficeThingsInfoModel OfficeThingsInfoM)
        {
            return OfficeThingsInfoDBHelper.UpdateOfficeThingsInfo(OfficeThingsInfoM);
        }
        #endregion

        #region 新报表 
        public static DataTable ThingByDetails1(string BeginDate, string EndDate, string DeptID, string TypeID, string CompanyCD, string DateType, string DateValue, int pageIndex, int pageCount, string ord, ref int TotalCount)
          {
              return OfficeThingsInfoDBHelper.ThingByDetails1(BeginDate, EndDate, DeptID, TypeID, CompanyCD,DateType,DateValue,pageIndex, pageCount, ord, ref TotalCount);
          }

        public static DataTable ThingByDetails(string BeginDate, string EndDate, string TypeID,string ThingNO, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return OfficeThingsInfoDBHelper.ThingByDetails(BeginDate, EndDate, TypeID,ThingNO,CompanyCD, pageIndex, pageCount, ord, ref  TotalCount);
        }

        public static DataTable ThingsByBuy(string BeginDate, string EndDate, string TypeID, string CompanyCD)
        {
            return OfficeThingsInfoDBHelper.ThingsByBuy(BeginDate, EndDate, TypeID, CompanyCD);
        }

        public static DataTable ThingsByDept(string BeginDate, string EndDate, string TypeID, string CompanyCD)
        {
            return OfficeThingsInfoDBHelper.ThingsByDept(BeginDate, EndDate, TypeID, CompanyCD);
        }

        public static DataTable ThingsByTrend(string BeginDate, string EndDate, string DeptID, string TypeID,string DateType, string CompanyCD)
        {
            return OfficeThingsInfoDBHelper.ThingsByTrend(BeginDate, EndDate, DeptID, TypeID,DateType,CompanyCD);
        } 
        #endregion
    }
}
