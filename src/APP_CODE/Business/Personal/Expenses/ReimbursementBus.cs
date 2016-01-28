/****************************************
 *创建人：何小武 
 *创建日期：2009-9-14
 *描述：费用报销
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
    public class ReimbursementBus
    {
        /// <summary>
        /// 保存费用报销单
        /// </summary>
        /// <param name="reimbModel">费用报销主表实体</param>
        /// <param name="reimbDetailModelList">费用报销明细表实体列表</param>
        /// <param name="strMsg"></param>
        public static bool SaveReimbursement(ReimbursementModel reimbModel, List<ReimbDetailsModel> reimbDetailModelList, out string strMsg)
        {
            return ReimbursementDBHelper.SaveReimbursement(reimbModel, reimbDetailModelList, out strMsg);
        }

        /// <summary>
        /// 修改费用报销单
        /// </summary>
        /// <param name="reimbModel">费用报销主表实体</param>
        /// <param name="reimbDetailModelList">费用报销明细表实体列表</param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static bool UpdateReimbursement(ReimbursementModel reimbModel, List<ReimbDetailsModel> reimbDetailModelList, out string strMsg)
        {
            return ReimbursementDBHelper.UpdateReimbursement(reimbModel, reimbDetailModelList, out strMsg);
        }

        /// <summary>
        /// 根据ID删除费用报销单
        /// </summary>
        /// <param name="reimbIDs">报销单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="strMsg">返回信息</param>
        /// <param name="strFieldText"></param>
        /// <returns></returns>
        public static bool DelReimbursementByIDs(string reimbIDs, string strCompanyCD, out string strMsg, out string strFieldText)
        {
            return ReimbursementDBHelper.DelReimbursementByIDs(reimbIDs, strCompanyCD, out strMsg, out strFieldText);
        }

        /// <summary>
        /// 确认报销单
        /// </summary>
        /// <param name="reimbNo">报销单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="EmployeeName">当前登录人名称</param>
        /// <param name="EmployeeID">当前登录人ID</param>
        /// <param name="UserID">当前登录人用户ID</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>返回是否成功标志</returns>
        public static bool ConfirmReimbursement(ReimbursementModel reimbModel,List<ReimbDetailsModel> reimbDetailModelList, string strExpID ,string strCompanyCD, string EmployeeName, int EmployeeID, string UserID, out string strMsg)
        {
            return ReimbursementDBHelper.ConfirmReimbursement(reimbModel, reimbDetailModelList, strExpID, strCompanyCD, EmployeeName, EmployeeID, UserID, out strMsg);
        }
        /// <summary>
        /// 取消确认报销单
        /// </summary>
        /// <param name="reimbNo">报销单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="UserID">当前登录人用户ID</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>返回是否成功标志</returns>
        public static bool UnConfirmReimbursement(string reimbNo, string strExpID ,string AttestBillID,string strCompanyCD, string UserID, out string strMsg)
        {
            return ReimbursementDBHelper.UnConfirmReimbursement(reimbNo, strExpID, AttestBillID,strCompanyCD, UserID, out strMsg);
        }

        /// <summary>
        /// 报废报销单
        /// </summary>
        /// <param name="reimbNo">报销单编号</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="UserID">当前用户ID</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>返回是否成功标志</returns>
        public static bool ScrapReimbursement(string reimbNo, string strExpID ,string AttestBillID,string strCompanyCD, string UserID, out string strMsg)
        {
            return ReimbursementDBHelper.ScrapReimbursement(reimbNo, strExpID, AttestBillID,strCompanyCD, UserID, out strMsg);
        }

        /// <summary>
        /// 根据报销单编号获取报销单ID
        /// </summary>
        /// <param name="reimbNo">报销单编号</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>报销单ID</returns>
        public static int GetReimbursementID(string reimbNo, string strCompanyCD)
        {
            return ReimbursementDBHelper.GetReimbursementID(reimbNo, strCompanyCD);
        }

        /// <summary>
        /// 根据查询条件获取费用报销单列表
        /// </summary>
        /// <param name="reimbModel">费用报销主表实体</param>
        /// <param name="ReimbDate1">报销日期</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetReimbursementList(ReimbursementModel reimbModel,int empid, DateTime? ReimbDate1, string FlowStatus,decimal? ReimbAmount1, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return ReimbursementDBHelper.GetReimbursementList(reimbModel,empid, ReimbDate1, FlowStatus,ReimbAmount1, pageIndex, pageCount, ord,ref TotalCount);
        }

        #region 根据检索条件获取报销单据金额合计
        /// <summary>
        /// 根据检索条件获取报销单据金额合计
        /// </summary>
        /// <param name="reimbModel">费用报销主表实体</param>
        /// <param name="empid"></param>
        /// <param name="ReimbDate1"></param>
        /// <param name="FlowStatus"></param>
        /// <param name="ReimbAmount1"></param>
        /// <returns></returns>
        public static DataTable GetReimbSumTotal(ReimbursementModel reimbModel, int empid, DateTime? ReimbDate1, string FlowStatus, decimal? ReimbAmount1)
        {
            return ReimbursementDBHelper.GetReimbSumTotal(reimbModel, empid, ReimbDate1, FlowStatus, ReimbAmount1);
        }
        #endregion

        /// <summary>
        /// 根据报销单ID获取报销单详细信息
        /// </summary>
        /// <param name="reimbID">报销单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>报销单详细信息datatable</returns>
        public static DataTable GetReimbInfoByID(int reimbID, string strCompanyCD)
        {
            return ReimbursementDBHelper.GetReimbInfoByID(reimbID, strCompanyCD);
        }

        /// <summary>
        /// 根据报销单ID获取报销单明细信息
        /// </summary>
        /// <param name="reimbID">报销单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>报销单明细信息datatable</returns>
        public static DataTable GetReimbDetailsByReimbID(int reimbID, string strCompanyCD)
        {
            return ReimbursementDBHelper.GetReimbDetailsByReimbID(reimbID, strCompanyCD);
        }

        /// <summary>
        /// 根据条件获取历史报销单列表
        /// </summary>
        /// <param name="reimbModel">报销单主表实体</param>
        /// <param name="ReimbDate1">报销日期</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页显示记录数</param>
        /// <param name="ord">排序</param>
        /// <param name="TotalCount">总记录数</param>
        /// <returns>返回历史报销单列表datatable</returns>
        public static DataTable GetHisReimbursementList(ReimbursementModel reimbModel,int empid, DateTime? ReimbDate1, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return ReimbursementDBHelper.GetHisReimbursementList(reimbModel,empid, ReimbDate1, pageIndex, pageCount, ord, ref TotalCount);
        }

        #region 打印
        /// <summary>
        /// 获取费用报销单打印主表信息
        /// </summary>
        /// <param name="reimbNo"></param>
        /// <param name="strCompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetRepReimbursement(string reimbNo, string strCompanyCD)
        {
            return ReimbursementDBHelper.GetRepReimbursement(reimbNo, strCompanyCD);
        }

        /// <summary>
        /// 获取费用报销单打印子表信息
        /// </summary>
        /// <param name="reimbNo"></param>
        /// <param name="strCompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetRepReimbursementDetail(string reimbNo, string strCompanyCD)
        {
            return ReimbursementDBHelper.GetRepReimbursementDetail(reimbNo, strCompanyCD);
        }
        #endregion

        #region 根据费用类别获取科目信息
        /// <summary>
        /// 根据费用类别获取科目信息
        /// </summary>
        /// <param name="expType">类别ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetSubjectByExpType(int expType, string strCompanyCD)
        {
            return ReimbursementDBHelper.GetSubjectByExpType(expType, strCompanyCD);
        }
        #endregion
    }
}
