/**********************************************
 * 类作用：   班组设置事务层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/08
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Common;
using System.Data;
namespace XBase.Business.Office.AdminManager
{
    /// <summary>
    /// 类名：WorkGroupBus
    /// 描述：班组设置事务层处理
    /// 作者：lysong
    /// 创建时间：2009/04/08
    /// </summary>
    public class WorkGroupBus
    {
        #region 添加班组信息
       /// <summary>
        /// 添加班组信息
       /// </summary>
       /// <param name="WorkGroupInfos">班组信息</param>
       /// <param name="userID">用户ID</param>
       /// <param name="CompanyID">公司代码</param>
       /// <returns>成功：true,失败:false</returns>
        public static string AddWorkGroupInfo(string WorkGroupInfos, string userID, string CompanyID)
        {
            return WorkGroupDBHelper.AddWorkGroupInfo(WorkGroupInfos, userID, CompanyID);
        }
        #endregion
        #region 查询班组列表
        /// <summary>
        /// 查询班组列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetWorkGroupInfo(string WorkGroupNo, string WorkGroupName, string CompanyID)
        {
            try
            {
                return WorkGroupDBHelper.GetWorkGroupInfo(WorkGroupNo, WorkGroupName, CompanyID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 删除班组信息
        /// <summary>
        /// 删除班组信息
        /// </summary>
        /// <param name="WorkGroupIds">班组编号</param>
        /// <returns>删除是否成功 false:失败，true:成功</returns>
        public static bool DelWorkGroupInfo(string WorkGroupIds, string CompanyID)
        {
            return WorkGroupDBHelper.DelWorkGroupInfo(WorkGroupIds, CompanyID);
        }
        #endregion
        #region 班组下拉列表
        /// <summary>
        /// 班组下拉列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetWorkGroupList(string CompanyCD)
        {
            return WorkGroupDBHelper.GetWorkGroupList(CompanyCD);
        }
        #endregion
        #region 班组下拉列表(包括正常班)
        /// <summary>
        /// 班组下拉列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetWorkGroupList1(string CompanyCD)
        {
            return WorkGroupDBHelper.GetWorkGroupList1(CompanyCD);
        }
        #endregion
    }
}
