/**********************************************
 * 类作用：   班次设置事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/07
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;

namespace XBase.Business.Office.AdminManager
{
    /// <summary>
    /// 类名：HolidayBus
    /// 描述：班次设置事务层处理
    /// 作者：lysong
    /// 创建时间：2009/04/07
    /// </summary>
   public class WorkShiftBus
    {
        #region 添加班次信息
        /// <summary>
        /// 添加班次信息
        /// </summary>
        /// <param name="WorkShiftSetM">班次信息</param>
        /// <param name="WorkShiftTimeInfos">班段信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool AddWorkShiftInfo(WorkShiftSetModel WorkShiftSetM,string WorkShiftTimeInfos)
        {
            return WorkShiftDBHelper.AddWorkShiftInfo(WorkShiftSetM, WorkShiftTimeInfos);
        }
        #endregion
        #region 更新班次信息
        /// <summary>
        /// 更新班次信息
        /// </summary>
        /// <param name="WorkShiftSetM">班次信息</param>
        /// <param name="WorkShiftTimeInfos">班段信息</param>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateWorkShiftInfo(WorkShiftSetModel WorkShiftSetM, string WorkShiftTimeInfos)
        {
            return WorkShiftDBHelper.UpdateEquipMnetAndFitInfo(WorkShiftSetM, WorkShiftTimeInfos);
        }
        #endregion
        #region 查询班次列表
        /// <summary>
        /// 查询班次列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetWorkShiftInfo(string WorkShiftNo,string WorkShiftName,string CompanyCD)
        {
            try
            {
                return WorkShiftDBHelper.GetWorkShiftInfo(WorkShiftNo, WorkShiftName, CompanyCD);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region  根据recordno获取班次信息以供查看或修改
        /// <summary>
        /// 根据recordno获取班次信息以供查看或修改
        /// </summary>
        /// <param name="WorkShiftNo">班次编号</param>
        /// <returns>DataTable</returns>
        public static DataTable GetWorkShiftInfoByWorkshiftNo(string WorkShiftNo)
        {
            try
            {
                return WorkShiftDBHelper.GetWorkShiftInfoByWorkshiftNo(WorkShiftNo);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 删除班次信息
        /// <summary>
        /// 删除班次信息
        /// </summary>
        /// <param name="WorkShiftIds">班次编号</param>
        /// <returns>删除是否成功 false:失败，true:成功</returns>
        public static bool DelWorkShiftInfo(string WorkShiftIds)
        {
            return WorkShiftDBHelper.DelWorkShiftInfo(WorkShiftIds);
        }
        #endregion
        #region 获取班次下拉列表
        /// <summary>
        /// 获取班次下拉列表
        /// </summary>
        /// <returns></returns>
        public static DataTable BindWorkShift(string CompanyCD)
        {
            return WorkShiftDBHelper.BindWorkShift(CompanyCD);
        }
        #endregion
    }
}
