using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using XBase.Data.Office.ProjectBudget;
using XBase.Model.Office.ProjectBudget;
namespace XBase.Business.Office.ProjectBudget
{
    public class ProjectBudgetBus
    {
        public static void BindUnit(System.Web.UI.WebControls.DropDownList ddl, XBase.Common.UserInfoUtil userinfo)
        {
            ProjectBudgetDBHelper.BindUnit(ddl, userinfo);
        }

        public static void BindProject(System.Web.UI.WebControls.DropDownList ddl, XBase.Common.UserInfoUtil userinfo, bool flag)
        {
            ProjectBudgetDBHelper.BindProject(ddl, userinfo, flag);
        }

        public static void BindProject(System.Web.UI.WebControls.DropDownList ddl, XBase.Common.UserInfoUtil userinfo)
        {
            ProjectBudgetDBHelper.BindProject(ddl, userinfo);
        }
        #region 绑定项目(同时绑定对应的项目编号)
        /// <summary>
        /// 绑定项目(同时绑定对应的项目编号)
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="userinfo"></param>
        /// <param name="bindProjectNo">用来重载的任意字符串</param>
        public static void BindProject(System.Web.UI.WebControls.DropDownList ddl, XBase.Common.UserInfoUtil userinfo, string bindProjectNo)
        {
            ProjectBudgetDBHelper.BindProject(ddl, userinfo, bindProjectNo);
        }
        #endregion
        public static int AddBudgetInfo(BudgetSummary budgetSummary, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.AddBudgetInfo(budgetSummary, userinfo);
        }

        public static int EditBudgetInfo(BudgetSummary budgetSummary, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.EditBudgetInfo(budgetSummary, userinfo);
        }

        public static DataSet ReadEexcel(string FilePath, string companycd)
        {
            return ProjectBudgetDBHelper.ReadEexcel(FilePath, companycd);
        }

        public static bool ChargeCodeUnit(string codename, string compid)
        {
            return ProjectBudgetDBHelper.ChargeCodeUnit(codename, compid);
        }

        public static int ListInput(string companycd, string projectid)
        {
            return ProjectBudgetDBHelper.ListInput(companycd, projectid);
        }

        public static int AddBudgetPriceInfo(BudgetPrice budgetPrice, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.AddBudgetPriceInfo(budgetPrice, userinfo);
        }
        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="budgetPrice">BudgetPrice实体</param>
        /// <returns></returns>
        public static int UpdateBudgetPriceInfo(BudgetPrice budgetPrice)
        {
            return ProjectBudgetDBHelper.UpdateBudgetPriceInfo(budgetPrice);
        }
        #endregion

        #region 判断同一项目中是否有同名的 预算价格摘要名称
        /// <summary>
        /// 判断同一项目中是否有同名的 预算价格摘要名称
        /// </summary>
        /// <param name="name">预算价格摘要名称</param>
        /// <param name="projectID">所属项目ID</param>
        /// <returns>true：有重复，false:无重复的</returns>
        public static bool IsRepeatedNameOneProject(string budgetpriceID, string name, string projectID)
        {
            return ProjectBudgetDBHelper.IsRepeatedNameOneProject(budgetpriceID, name, projectID);
        }
        #endregion

        public static DataSet GetBudgetItem(string projectid, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.GetBudgetItem(projectid, userinfo);
        }

        public static DataSet GetBudgetSummary(string projectid, XBase.Common.UserInfoUtil userinfo, int dotnum)
        {
            return ProjectBudgetDBHelper.GetBudgetSummary(projectid, userinfo, dotnum);
        }

        public static DataSet GetBudgetSummary(string projectid, XBase.Common.UserInfoUtil userinfo, int dotnum, string subid)
        {
            return ProjectBudgetDBHelper.GetBudgetSummary(projectid, userinfo, dotnum, subid);
        }

        public static DataSet GetPriceData(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.GetPriceData(projectID, userinfo);
        }

        public static int AddProjectBudgetInfo(ProjectBudgetInfo projectBudgetInfo, XBase.Common.UserInfoUtil userinfo, string valuelist, string basevaluelist)
        {
            return ProjectBudgetDBHelper.AddProjectBudgetInfo(projectBudgetInfo, userinfo, valuelist, basevaluelist);
        }

        public static int CheckInsertData(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.CheckInsertData(projectID, userinfo);
        }

        public static string GetItemData(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.GetItemData(projectID, userinfo);
        }

        /// <summary>
        /// 项目预算分析列表查询函数
        /// </summary>
        /// <param name="pageindex">页数</param>
        /// <param name="pagecount">行数</param>
        /// <param name="projectName">项目名称</param>
        /// <param name="OrderBy">排序</param>
        /// <param name="userinfo">登录信息</param>
        /// <param name="totalCount">返回总数</param>
        /// <returns></returns>
        public static DataTable GetProjectBudgetPriceList(int pageindex, int pagecount, string projectName, string OrderBy, XBase.Common.UserInfoUtil userinfo, ref int totalCount)
        {
            return ProjectBudgetDBHelper.GetProjectBudgetPriceList(pageindex, pagecount, projectName, OrderBy, userinfo, ref totalCount);
        }

        /// <summary>
        /// 删除项目预算分析
        /// </summary>
        /// <param name="ID">ID集合</param>
        /// <returns></returns>
        public static int DeletProjectBudgetPrice(string ID)
        {
            return ProjectBudgetDBHelper.DeletProjectBudgetPrice(ID);
        }

        public static DataTable GetProjectBudgetList(int pageindex, int pagecount, string projectid, string summaryname, string OrderBy, XBase.Common.UserInfoUtil userinfo, ref int totalCount)
        {
            return ProjectBudgetDBHelper.GetProjectBudgetList(pageindex, pagecount, projectid, summaryname, OrderBy, userinfo, ref totalCount);
        }

        public static DataSet GetBudgetSummaryDetails(string itemID)
        {
            return ProjectBudgetDBHelper.GetBudgetSummaryDetails(itemID);
        }

        public static int DeLProjectBudgetInfo(string budgetID)
        {
            return ProjectBudgetDBHelper.DeLProjectBudgetInfo(budgetID);
        }
        #region 获取预算价格摘要列表
        /// <summary>
        /// 获取预算价格摘要列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagecount"></param>
        /// <param name="projectid"></param>
        /// <param name="summaryname"></param>
        /// <param name="OrderBy"></param>
        /// <param name="userinfo"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetProjectBudgetPriceList(int pageindex, int pagecount, string projectid, string summaryname, string OrderBy, XBase.Common.UserInfoUtil userinfo, ref int totalCount)
        {
            return ProjectBudgetDBHelper.GetProjectBudgetPriceList(pageindex, pagecount, projectid, summaryname, OrderBy, userinfo, ref totalCount);
        }
        #endregion
        #region 删除预算价格摘要
        /// <summary>
        /// 删除预算价格摘要
        /// </summary>
        /// <param name="budgetID"></param>
        /// <returns></returns>
        public static int DeLProjectBudgetPriceInfo(string budgetID)
        {
            return ProjectBudgetDBHelper.DeLProjectBudgetPriceInfo(budgetID);
        }
        #endregion

        public static string GetEmployeeByID(string userlist)
        {
            return ProjectBudgetDBHelper.GetEmployeeByID(userlist);
        }

        public static int OPDataSure(string projectID, XBase.Common.UserInfoUtil userinfo, string status)
        {
            return ProjectBudgetDBHelper.OPDataSure(projectID, userinfo, status);
        }
        #region 取消确认
        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="userInfo"></param>
        /// <param name="status"></param>
        /// <param name="typeflag"></param>
        /// <param name="typecode"></param>
        /// <returns></returns>
        public static int OPDataUnSure(string projectid, XBase.Common.UserInfoUtil userInfo, string status, int typeflag, int typecode)
        {
            return ProjectBudgetDBHelper.OPDataUnSure(projectid, userInfo, status, typeflag, typecode);
        }
        #endregion
        public static string ProjectBudgetState(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.ProjectBudgetState(projectID, userinfo);
        }

        public static DataTable GetBudgetPrice(string budgetpriceID, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.GetBudgetPrice(budgetpriceID, userinfo);
        }

        public static int ChargeProjectSummaryName(string projectID, string subBudgetID, string projectName, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.ChargeProjectSummaryName(projectID, subBudgetID, projectName, userinfo);
        }

        public static DataSet GetSummaryList(string projectID, string targetprojectid, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.GetSummaryList(projectID, targetprojectid, userinfo);
        }

        public static int CopySourceData(string projectid, string summarylist, string subBudgetID, string budgetNameList, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.CopySourceData(projectid, summarylist, subBudgetID, budgetNameList, userinfo);
        }

        /// <summary>
        /// 根据项目获得所有分项预算概要
        /// </summary>
        /// <param name="userinfo">用户信息</param>
        /// <param name="projectID">项目代码</param>
        public static DataTable GetSubBedget(XBase.Common.UserInfoUtil userinfo, int projectID)
        {
            return ProjectBudgetDBHelper.GetSubBedget(userinfo, projectID);
        }

        public static DataTable GetSubBedgetGroup(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.GetSubBedgetGroup(projectID, userinfo);
        }

        public static string GetBedgetList(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.GetBedgetList(projectID, userinfo);
        }

        #region 获取审批流程
        /// <summary>
        /// 获取审批流程
        /// </summary>
        /// <param name="ProjectID">项目ID</param>
        /// <param name="userInfo">Session用户信息</param>
        /// <returns></returns>
        public static DataTable GetBudgetFlowStatus(string ProjectID, XBase.Common.UserInfoUtil userInfo)
        {
            return ProjectBudgetDBHelper.GetBudgetFlowStatus(ProjectID, userInfo);
        }
        #endregion

        #region 根据项目ID获取项目编号
        /// <summary>
        /// 根据项目ID获取项目编号
        /// </summary>
        /// <param name="projectID">项目ID</param>
        /// <returns>datatable</returns>
        public static DataTable GetProjectNoByID(string projectID)
        {
            return ProjectBudgetDBHelper.GetProjectNoByID(projectID);
        }
        #endregion

        public static DataTable GetPriceList(string projectid, string summaryid,string baseNum, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.GetPriceList(projectid, summaryid,baseNum, userinfo);
        }

        public static DataTable GetSubBedgetGroupList(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            return ProjectBudgetDBHelper.GetSubBedgetGroupList(projectID, userinfo);
        }

        public static string getProjectName(string projectID)
        {
            return ProjectBudgetDBHelper.getProjectName(projectID);
        }

    }
}
