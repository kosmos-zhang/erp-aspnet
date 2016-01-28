/****************************************
 *创建人：何小武 
 *创建日期：2009-9-7
 *描述：费用管理
 ***************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Model.Personal.Expenses;
using XBase.Data.Personal.Expenses;

namespace XBase.Business.Personal.Expenses
{
    public class ExpensesBus
    {
        /// <summary>
        /// 添加费用申请单
        /// </summary>
        /// <param name="expApplyModel"></param>
        /// <param name="expDetailModelList"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static bool SaveExpensesApply(ExpensesApplyModel expApplyModel, List<ExpDetailsModel> expDetailModelList,out string strMsg)
        {
            return ExpensesDBHelper.SaveExpensesApply(expApplyModel, expDetailModelList, out strMsg);
        }

        /// <summary>
        /// 修改费用申请单
        /// </summary>
        /// <param name="expApplyModel">费用主表Model</param>
        /// <param name="expDetailModelList">费用明细表Model</param>
        /// <param name="strMsg">消息字符串</param>
        /// <returns></returns>
        public static bool UpdateExpensesApply(ExpensesApplyModel expApplyModel, List<ExpDetailsModel> expDetailModelList, out string strMsg)
        {
            return ExpensesDBHelper.UpdateExpensesApply(expApplyModel, expDetailModelList, out strMsg);
        }

        /// <summary>
        /// 确认费用申请单
        /// </summary>
        /// <param name="expCode"></param>
        /// <param name="strCompanyCD"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static bool ConfirmExpApply(string expCode, string strCompanyCD, string EmployeeName, int EmployeeID, string UserID, out string strMsg)
        {
            return ExpensesDBHelper.ConfirmExpApply(expCode, strCompanyCD, EmployeeName, EmployeeID, UserID, out strMsg);
        }

        /// <summary>
        /// 取消确认费用申请单
        /// </summary>
        /// <param name="expCode"></param>
        /// <param name="strCompanyCD"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static bool UnConfirmExpApply(string expCode, string strCompanyCD,string UserID, out string strMsg)
        {
            return ExpensesDBHelper.UnConfirmExpApply(expCode, strCompanyCD,UserID, out strMsg);
        }

        /// <summary>
        /// 报废费用申请单
        /// </summary>
        /// <param name="expCode">费用申请单编号</param>
        /// <param name="strCompanyCD">公司编号</param>
        /// <param name="UserID">用户账号</param>
        /// <param name="strMsg">返回操作信息</param>
        /// <returns></returns>
        public static bool ScrapExpenses(string expCode, string strCompanyCD, string UserID, out string strMsg)
        {
            return ExpensesDBHelper.ScrapExpenses(expCode, strCompanyCD, UserID, out strMsg);
        }

        /// <summary>
        /// 根据单据编号获取单据ID
        /// </summary>
        /// <param name="strCode"></param>
        /// <param name="strCompanyCD"></param>
        /// <returns></returns>
        public static int GetExpApplyID(string strCode, string strCompanyCD)
        {
            return ExpensesDBHelper.GetExpApplyID(strCode, strCompanyCD);
        }

        /// <summary>
        /// 根据查询条件获取费用申请单列表
        /// </summary>
        /// <param name="expApplyModel">ExpensesApplyModel实体</param>
        /// <param name="AriseDate1">申请日期</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetExpensesApplyList(ExpensesApplyModel expApplyModel,int empid, DateTime? AriseDate1,string FlowStatus, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return ExpensesDBHelper.GetExpensesApplyList(expApplyModel,empid, AriseDate1,FlowStatus, pageIndex, pageCount, ord, ref TotalCount);
        }

        #region 根据查询条件，获取所以符合条件记录的金额合计
        /// <summary>
        /// 根据查询条件，获取所以符合条件记录的金额合计
        /// </summary>
        /// <param name="expApplyMOdel">ExpensesApplyModel实体</param>
        /// <param name="empid"></param>
        /// <param name="AriseDate1"></param>
        /// <param name="FlowStatus"></param>
        /// <returns></returns>
        public static DataTable GetExpSumTotal(ExpensesApplyModel expApplyMOdel, int empid, DateTime? AriseDate1, string FlowStatus)
        { 
            return ExpensesDBHelper.GetExpSumTotal(expApplyMOdel, empid, AriseDate1, FlowStatus);
        }
        #endregion

        /// <summary>
        /// 根据查询条件获取历史费用申请单列表
        /// </summary>
        /// <param name="expApplyModel">费用申请主表model</param>
        /// <param name="AriseDate1">申请日期</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetExpensesApplyHistList(ExpensesApplyModel expApplyModel,int empid, DateTime? AriseDate1, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return ExpensesDBHelper.GetExpensesApplyHistList(expApplyModel,empid, AriseDate1, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 根据费用申请单ID获取该申请单主表信息
        /// </summary>
        /// <param name="expID">费用申请单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetExpInfoByID(int expID, string strCompanyCD)
        {
            return ExpensesDBHelper.GetExpInfoByID(expID, strCompanyCD);
        }

        /// <summary>
        /// 根据费用申请单ID获取子表费用明细信息
        /// </summary>
        /// <param name="expID">费用申请单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetExpDetailsByExpID(int expID, string strCompanyCD)
        {
            return ExpensesDBHelper.GetExpDetailsByExpID(expID, strCompanyCD);
        }

        /// <summary>
        /// 获取该公司的费用大类
        /// </summary>
        /// <param name="strCompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetExpBigType(string strCompanyCD,string flagType,string  flagCode)
        {
            return ExpensesDBHelper.GetExpBigType(strCompanyCD, flagType, flagCode);
        }

        /// <summary>
        /// 删除已选择的费用申请单
        /// </summary>
        /// <param name="expID">费用申请单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="strMsg"></param>
        /// <param name="strFieldText"></param>
        /// <returns></returns>
        public static bool DelExpensesApply(string expID, string strCompanyCD, out string strMsg, out string strFieldText)
        {
            return ExpensesDBHelper.DelExpensesApply(expID, strCompanyCD, out strMsg, out strFieldText);
        }

        /// <summary>
        /// 根据公司编号获取该公司的员工Name、ID
        /// </summary>
        /// <param name="strCompany"></param>
        /// <returns></returns>
        public static DataTable GetEmployeeName(string strCompany)
        {
            return ExpensesDBHelper.GetEmployeeName(strCompany);
        }

        /// <summary>
        /// 获取费用申请打印主表信息
        /// </summary>
        /// <param name="expCode">费用申请编号</param>
        /// <param name="strCompany">公司代码</param>
        /// <returns></returns>
        public static DataTable GetRepExpApply(string expCode, string strCompany)
        {
            return ExpensesDBHelper.GetRepExpApply(expCode, strCompany);
        }
        /// <summary>
        /// 获取费用申请打印子表信息
        /// </summary>
        /// <param name="expCode">费用申请编号</param>
        /// <returns></returns>
        public static DataTable GetRepExpApplyDetail(string expCode, string strCompanyCD)
        {
            return ExpensesDBHelper.GetRepExpApplyDetail(expCode, strCompanyCD);
        }

        //根据员工ID获取员工所在部门
        public static DataTable GetDeptIDByEmployeeID(int ApplyorID, string strCompanyCD)
        {
            return ExpensesDBHelper.GetDeptIDByEmployeeID(ApplyorID, strCompanyCD);
        }

        /// 根据查询条件获取已审批的费用申请单列表
        public static DataTable GetExpensesApplyAuditList(ExpensesApplyModel expApplyModel,int empid, DateTime? AriseDate1, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return ExpensesDBHelper.GetExpensesApplyAuditList(expApplyModel,empid, AriseDate1, pageIndex, pageCount, ord, ref TotalCount);
        }
    }
}
