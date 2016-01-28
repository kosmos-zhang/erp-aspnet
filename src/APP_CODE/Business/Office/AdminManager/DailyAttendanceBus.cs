/**********************************************
 * 类作用：   日常考勤日事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/02
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;
namespace XBase.Business.Office.AdminManager
{
    /// <summary>
    /// 类名：DailyAttendanceBus
    /// 描述：日常考勤日事务层处理
    /// 作者：lysong
    /// 创建时间：2009/04/02
    /// </summary>
   public class DailyAttendanceBus
    {
        #region 插入员工日常考勤签到信息
        /// <summary>
        /// 插入员工日常考勤签到信息
        /// </summary>
        /// <param name="DailyAttendanceM">签到信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddDailyAttendanceSignIn(DailyAttendanceModel DailyAttendanceM)
        {
            return DailyAttendanceDBHelper.AddDailyAttendanceSignIn(DailyAttendanceM);
        }
        #endregion
        #region 获取考勤信息列表
       /// <summary>
       /// 获取考勤信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetDailyAttendanceInfo(string EmployeeNo,string EmployeeName,string Quarter,string AttendanceStartDate,string AttendanceEndDate,string CompanyID,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           try
           {
               return DailyAttendanceDBHelper.GetDailyAttendanceInfo(EmployeeNo, EmployeeName, Quarter, AttendanceStartDate, AttendanceEndDate,CompanyID, pageIndex, pageCount, ord, ref TotalCount);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       #endregion
        #region 判断签到还是签退
       /// <summary>
       /// 判断签到还是签退
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码0</param>
       /// <returns></returns>
       public static int SignInOrOut(int EmployeeID,string CompanyID)
       {
           return DailyAttendanceDBHelper.SignInOrOut(EmployeeID, CompanyID);
       }
       #endregion
        #region 更新用户签退信息
       /// <summary>
       /// 更新用户签退信息
       /// </summary>
       /// <param name="DailyAttendanceM">签退信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateailyAttendanceSignOut(DailyAttendanceModel DailyAttendanceM)
       {
           return DailyAttendanceDBHelper.UpdateailyAttendanceSignOut(DailyAttendanceM);
       }
       #endregion
    }
}
