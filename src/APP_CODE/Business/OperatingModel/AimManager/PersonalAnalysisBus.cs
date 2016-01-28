using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;

namespace XBase.Business.OperatingModel.AimManager
{
    /// <summary>
    /// 执行分析
    /// </summary>
    public class PersonalAnalysisBus
    {
        /// <summary>
        /// 获得日志状态
        /// </summary>
        /// <param name="userInfo">当前用户信息</param>
        /// <param name="userList">查询人员列表</param>
        /// <param name="dtS">开始时间</param>
        /// <param name="dtE">结束时间</param>
        /// <returns></returns>
        public static DataTable GetPersonalNote(UserInfoUtil userInfo, string userList, Nullable<DateTime> dtS, Nullable<DateTime> dtE)
        {
            return XBase.Data.OperatingModel.AimManager.PersonalAnalysisDBHelper.GetPersonalNote(userInfo, userList, dtS, dtE);
        }

        /// <summary>
        /// 获得任务状态
        /// </summary>
        /// <param name="userInfo">当前用户信息</param>
        /// <param name="userList">查询人员列表</param>
        /// <param name="dtS">开始时间</param>
        /// <param name="dtE">结束时间</param>
        /// <returns></returns>
        public static DataTable GetTask(UserInfoUtil userInfo, string userList, Nullable<DateTime> dtS, Nullable<DateTime> dtE)
        {
            return XBase.Data.OperatingModel.AimManager.PersonalAnalysisDBHelper.GetTask(userInfo, userList, dtS, dtE);
        }

        /// <summary>
        /// 获得日程状态
        /// </summary>
        /// <param name="userInfo">当前用户信息</param>
        /// <param name="userList">查询人员列表</param>
        /// <param name="dtS">开始时间</param>
        /// <param name="dtE">结束时间</param>
        /// <returns></returns>
        public static DataTable GetArrange(UserInfoUtil userInfo, string userList, Nullable<DateTime> dtS, Nullable<DateTime> dtE)
        {
            return XBase.Data.OperatingModel.AimManager.PersonalAnalysisDBHelper.GetArrange(userInfo, userList, dtS, dtE);
        }

        /// <summary>
        /// 获得目标状态
        /// </summary>
        /// <param name="userInfo">当前用户信息</param>
        /// <param name="userList">查询人员列表</param>
        /// <param name="dtS">开始时间</param>
        /// <param name="dtE">结束时间</param>
        /// <returns></returns>
        public static DataTable GetAim(UserInfoUtil userInfo, string userList, Nullable<DateTime> dtS, Nullable<DateTime> dtE)
        {
            return XBase.Data.OperatingModel.AimManager.PersonalAnalysisDBHelper.GetAim(userInfo, userList, dtS, dtE);
        }

        /// <summary>
        /// 获得人员岗位权限
        /// </summary>
        /// <param name="userInfo">当前用户信息</param>
        /// <param name="userList">查询人员列表</param>
        /// <returns></returns>
        public static DataTable GetQuarter(UserInfoUtil userInfo, string userList)
        {
            return XBase.Data.OperatingModel.AimManager.PersonalAnalysisDBHelper.GetQuarter(userInfo, userList);
        }
    }
}
