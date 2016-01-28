/**********************************************
 * 类作用：   考勤排班设置事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/10
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;

namespace XBase.Business.Office.AdminManager
{
    /// <summary>
    /// 类名：WorkPlanBus
    /// 描述：考勤排班设置事务层处理
    /// 作者：lysong
    /// 创建时间：2009/04/10
    /// </summary>
    public class WorkPlanBus
    {
        #region 添加排班设置信息
        /// <summary>
        /// 添加排班设置信息
        /// </summary>
        /// <param name="WorkPlanSetM">添加排班设置信息</param>
        /// <param name="workplanarraryinfo">排班设置信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool AddWorkPlanInfo(WorkPlanModel WorkPlanSetM, string workplanarraryinfo)
        {
            return WorkPlanDBHelper.AddWorkPlanInfo(WorkPlanSetM, workplanarraryinfo);
        }
        #endregion
        #region 更新排班信息根据开始时间（当天）
        /// <summary>
        /// 更新排班信息根据开始时间
        /// </summary>
        /// <param name="WorkPlanSetM">更新排班信息</param>
        /// <param name="workplanarraryinfo">排班设置信息</param>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateWorkPlanInfoByDate(WorkPlanModel WorkPlanSetM, string workplanarraryinfo)
        {
            return WorkPlanDBHelper.UpdateWorkPlanInfoByDate(WorkPlanSetM, workplanarraryinfo);
        }
        #endregion
        #region 更新排班信息根据(更新上一条)
        /// <summary>
        /// 更新排班信息根据(更新上一条)
        /// </summary>
        /// <param name="WorkPlanSetM">更新排班信息</param>
        /// <param name="workplanarraryinfo">排班设置信息</param>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateWorkPlanInfo(WorkPlanModel WorkPlanSetM, string workplanarraryinfo)
        {
            return WorkPlanDBHelper.UpdateWorkPlanInfo(WorkPlanSetM, workplanarraryinfo);
        }
        #endregion
    }
}
