/**********************************************
 * 类作用：   请假事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/14
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;

namespace XBase.Business.Office.AdminManager
{
    /// <summary>
    /// 类名：AttendanceApplyBus
    /// 描述：请假事务层处理
    /// 作者：lysong
    /// 创建时间：2009/04/14
    /// </summary>
    public class AttendanceApplyBus
    {
        #region 添加请假信息
        /// <summary>
        /// 添加请假信息
        /// </summary>
        /// <param name="AttendanceApplyM">请假信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool InsertAttendanceApplyData(AttendanceApplyModel AttendanceApplyM,out int RetValID)
        {
            return AttendanceApplyDBHelper.InsertAttendanceApplyData(AttendanceApplyM, out RetValID);
        }
        #endregion
        #region 查询请假列表
        /// <summary>
        /// 查询请假列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetAttendanceApplyInfo(string JoinUser, string ApplyDate, string LeaveType, string ApplyStatus, string Flag, string CompanyID,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            try
            {
                return AttendanceApplyDBHelper.GetAttendanceApplyInfo(JoinUser, ApplyDate, LeaveType, ApplyStatus, Flag, CompanyID, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region  根据ID获取人员请假信息供查看或修改
        /// <summary>
        /// 根据ID获取人员请假信息供查看或修改
        /// </summary>
        /// <param name="ID">人员请假ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetAttendanceApplyByID(string ID, string LeaveType)
        {
            try
            {
                return AttendanceApplyDBHelper.GetAttendanceApplyByID(ID, LeaveType);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 修改请假信息
        /// <summary>
        /// 添加请假信息
        /// </summary>
        /// <param name="AttendanceApplyM">请假信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool UpdateAttendanceApplyData(AttendanceApplyModel AttendanceApplyM,string ID)
        {
            return AttendanceApplyDBHelper.UpdateAttendanceApplyData(AttendanceApplyM,ID);
        }
        #endregion
        #region 添加加班信息
        /// <summary>
        /// 添加加班信息
        /// </summary>
        /// <param name="AttendanceApplyM">加班信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool InsertOverTimeData(AttendanceApplyModel AttendanceApplyM,out int RetValID)
        {
            return AttendanceApplyDBHelper.InsertOverTimeData(AttendanceApplyM, out RetValID);
        }
        #endregion
        #region 修改加班信息
        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="AttendanceApplyM">加班信息</param>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateOverTimeData(AttendanceApplyModel AttendanceApplyM, string ID)
        {
            return AttendanceApplyDBHelper.UpdateOverTimeData(AttendanceApplyM, ID);
        }
        #endregion
        #region 添加外出信息
        /// <summary>
        /// 添加外出信息
        /// </summary>
        /// <param name="AttendanceApplyM">外出信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool InsertBeOutData(AttendanceApplyModel AttendanceApplyM,out int RetValID)
        {
            return AttendanceApplyDBHelper.InsertBeOutData(AttendanceApplyM, out RetValID);
        }
        #endregion
        #region 修改外出信息
        /// <summary>
        /// 修改外出信息
        /// </summary>
        /// <param name="AttendanceApplyM">外出信息</param>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateBeOutData(AttendanceApplyModel AttendanceApplyM, string ID)
        {
            return AttendanceApplyDBHelper.UpdateBeOutData(AttendanceApplyM, ID);
        }
        #endregion

        #region 添加出差信息
        /// <summary>
        /// 添加出差信息
        /// </summary>
        /// <param name="AttendanceApplyM">出差信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool InsertBusinessData(AttendanceApplyModel AttendanceApplyM,out int RetValID)
        {
            return AttendanceApplyDBHelper.InsertBusinessData(AttendanceApplyM, out RetValID);
        }
        #endregion
        #region 修改出差信息
        /// <summary>
        /// 修改出差信息
        /// </summary>
        /// <param name="AttendanceApplyM">出差信息</param>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateBusinessData(AttendanceApplyModel AttendanceApplyM, string ID)
        {
            return AttendanceApplyDBHelper.UpdateBusinessData(AttendanceApplyM, ID);
        }
        #endregion

        #region 添加替班信息
        /// <summary>
        /// 添加替班信息
        /// </summary>
        /// <param name="AttendanceApplyM">替班信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool InsertInsteadData(AttendanceApplyModel AttendanceApplyM,out int RetValID)
        {
            return AttendanceApplyDBHelper.InsertInsteadData(AttendanceApplyM, out RetValID);
        }
        #endregion

        #region 修改替班信息
        /// <summary>
        /// 修改替班信息
        /// </summary>
        /// <param name="AttendanceApplyM">替班信息</param>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateInsteadData(AttendanceApplyModel AttendanceApplyM, string ID)
        {
            return AttendanceApplyDBHelper.UpdateInsteadData(AttendanceApplyM, ID);
        }
        #endregion

        #region 添加年休信息
        /// <summary>
        /// 添加年休信息
        /// </summary>
        /// <param name="AttendanceApplyM">年休信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool InsertYearHolidayData(AttendanceApplyModel AttendanceApplyM,out int RetValID)
        {
            return AttendanceApplyDBHelper.InsertYearHolidayData(AttendanceApplyM, out RetValID);
        }
        #endregion

        #region 修改年休信息
        /// <summary>
        /// 修改年休信息
        /// </summary>
        /// <param name="AttendanceApplyM">年休信息</param>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateYearHolidayApplyData(AttendanceApplyModel AttendanceApplyM, string ID)
        {
            return AttendanceApplyDBHelper.UpdateYearHolidayApplyData(AttendanceApplyM,ID);
        }
        #endregion
        #region 查询考勤统计报表根据页面传入的ReportNo
        /// <summary>
        /// 查询考勤统计报表根据页面传入的ReportNo
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetAttendanceReportInfoByNo(string ReportNo,string CompanyID)
        {
            try
            {
                return AttendanceApplyDBHelper.GetAttendanceReportInfoByNo(ReportNo, CompanyID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 查询考勤统计月报表
        /// <summary>
        /// 查询考勤统计报表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetAttendanceMonthReportInfo(string StartDate, string EndDate, string CompanyID)
        {
            try
            {
                return AttendanceApplyDBHelper.GetAttendanceMonthReportInfo(StartDate, EndDate, CompanyID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 查询考勤统计详细报表
        /// <summary>
        /// 查询考勤统计详细报表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetAttendanceDetailReportInfo(string EmpID, string StartDate, string EndDate, string CompanyID, string AttendanceType)
        {
            try
            {
                return AttendanceApplyDBHelper.GetAttendanceDetailReportInfo(EmpID, StartDate, EndDate, CompanyID, AttendanceType);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
