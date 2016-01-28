/**********************************************
 * 类作用：   考勤设置事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/03/30
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;

namespace XBase.Business.Office.AdminManager
{
    /// <summary>
    /// 类名：AttendanceSetBus
    /// 描述：考勤设置事务层处理
    /// 作者：lysong
    /// 创建时间：2009/03/30
    /// </summary>
   public class AttendanceSetBus
    {
        #region 工作日设置
        /// <summary>
        /// 工作日设置
        /// </summary>
        /// <param name="AttendanceInfo">工作日信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool AddAttendanceSet(string AttendanceInfo,string CompanyID,string UserID)
        {
            return AttendanceSetDBHelper.InsertAttendanceInfo(AttendanceInfo, CompanyID, UserID);
        }
        #endregion
        #region  获取工作日初始化页面
        /// <summary>
        /// 获取工作日初始化页面
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetAttcedanceSet()
        {
            try
            {
                return AttendanceSetDBHelper.GetAttcedanceSet();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
