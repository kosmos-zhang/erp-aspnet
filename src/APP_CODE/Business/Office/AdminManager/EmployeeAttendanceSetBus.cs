/**********************************************
 * 类作用：   人员考勤设置设置事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/12
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;

namespace XBase.Business.Office.AdminManager
{
    /// <summary>
    /// 类名：EmployeeAttendanceSetBus
    /// 描述：人员考勤设置设置事务层处理
    /// 作者：lysong
    /// 创建时间：2009/04/12
    /// </summary>
   public class EmployeeAttendanceSetBus
   {
       #region 添加人员考勤设置信息
       /// <summary>
       /// 添加人员考勤设置信息
        /// </summary>
       /// <param name="EmployeeAttendanceSetM">人员考勤信息</param>
       /// <param name="Employees">人员信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddEmployeeAttendance(EmployeeAttendanceSetModel EmployeeAttendanceSetM, string Employees)
        {
            return EmployeeAttendanceSetDBHelper.AddEmployeeAttendance(EmployeeAttendanceSetM, Employees);
        }
        #endregion
       #region 更新人员设置信息根据开始时间（当天）
       /// <summary>
       /// 更新人员设置信息根据开始时间
       /// </summary>
       /// <param name="EmployeeAttendanceSetM">人员考勤信息</param>
       /// <param name="Employees">人员信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateEmployeeAttendanceByDate(EmployeeAttendanceSetModel EmployeeAttendanceSetM, string Employees)
       {
           return EmployeeAttendanceSetDBHelper.UpdateEmployeeAttendanceByDate(EmployeeAttendanceSetM, Employees);
       }
       #endregion
       #region 更新排班信息根据(更新上一条)
       /// <summary>
       /// 更新排班信息根据(更新上一条)
       /// </summary>
       /// <param name="EmployeeAttendanceSetM">人员考勤信息</param>
       /// <param name="Employees">人员信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateEmployeeAttendanceInfo(EmployeeAttendanceSetModel EmployeeAttendanceSetM, string Employees)
       {
           return EmployeeAttendanceSetDBHelper.UpdateEmployeeAttendanceInfo(EmployeeAttendanceSetM, Employees);
       }
       #endregion
    }
}
