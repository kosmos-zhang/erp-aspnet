/***********************************************************************
 * 
 * Module Name:XBase.Business.Office.SystemManager.AdversaryInfoBus.cs
 * Current Version: 1.0 
 * Creator: 周军
 * Auditor:2009-01-12
 * End Date:
 * Description: 销售机会业务层处理
 * Version History:
 ***********************************************************************/

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.SellManager;
using XBase.Data.Office.SellManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Business.Common;
using XBase.Data.Common;
using System.Collections;

namespace XBase.Business.Office.SellManager
{
    public class SellChanceBus
    {
        #region 操作
        /// <summary>
        /// 添加销售机会及阶段
        /// </summary>
        /// <param name="sellChanceModel">销售机会表实体</param>
        /// <param name="sellChancePushModel">销售阶段表实体</param>
        /// <returns>是否添加成功</returns>
        public static bool InsertSellChance(Hashtable ht,SellChanceModel sellChanceModel, SellChancePushModel sellChancePushModel)
        {
            bool isSuc = false;
            string remark = string.Empty;

            try
            {
                isSuc = SellChanceDBHelper.InsertSellChance(ht,sellChanceModel, sellChancePushModel);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCHANCE_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellChanceModel.ChanceNo, ConstUtil.MODULE_ID_SELLCHANCE_ADD, ConstUtil.CODING_RULE_TABLE_SELLCHANCE, remark, ConstUtil.LOG_PROCESS_INSERT);

            return isSuc;
           
        }

        /// <summary>
        /// 添加销售机会及阶段
        /// </summary>
        /// <param name="sellChanceModel">销售机会表实体</param>
        /// <param name="sellChancePushModel">销售阶段表实体</param>
        /// <returns>是否添加成功</returns>
        public static bool UpdateSellChance(Hashtable ht,SellChanceModel sellChanceModel, SellChancePushModel sellChancePushModel)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;

            try
            {
                isSucc = SellChanceDBHelper.UpdateSellChance(ht,sellChanceModel, sellChancePushModel);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCHANCE_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellChanceModel.ChanceNo, ConstUtil.MODULE_ID_SELLCHANCE_ADD, ConstUtil.CODING_RULE_TABLE_SELLCHANCE, remark, ConstUtil.LOG_PROCESS_UPDATE);
            return isSucc;
           
        }

        /// <summary>
        /// 获取机会推进历史
        /// </summary>
        /// <param name="chanceNo"></param>
        /// <returns></returns>
        public static DataTable GetPush(string chanceNo)
        {
            return SellChanceDBHelper.GetPush(chanceNo);
        }

        /// <summary>
        /// 获取销售机会列表
        /// </summary>
        /// <param name="ChanceNo"></param>
        /// <param name="Title"></param>
        /// <param name="CustID"></param>
        /// <param name="Seller"></param>
        /// <param name="Phase"></param>
        /// <param name="State"></param>
        /// <param name="ChanceType"></param>
        /// <param name="HapSource"></param>
        /// <param name="FindDate"></param>
        /// <param name="FindDate1"></param>
        /// <returns></returns>
        public static DataTable GetChanceList(string EFIndex, string EFDesc, string ChanceNo, string Title, int? CustID, int? Seller, string Phase, string State, int? ChanceType, int? HapSource, DateTime? FindDate, DateTime? FindDate1, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellChanceDBHelper.GetChanceList(EFIndex,EFDesc,ChanceNo, Title, CustID, Seller, Phase, State, ChanceType, HapSource, FindDate, FindDate1, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 销售机会控件列表
        /// </summary>
        /// <param name="ChanceNo"></param>
        /// <param name="Title"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetChanceList(string ChanceNo, string Title, string CustName, string model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellChanceDBHelper.GetChanceList(ChanceNo, Title, CustName, model, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 删除销售机会
        /// </summary>
        /// <param name="orderNos"></param>
        /// <returns></returns>
        public static bool DelOrder(string orderNos)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;

            try
            {
                isSucc = SellChanceDBHelper.DelOrder(orderNos);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCHANCE_INFO);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(orderNos, ConstUtil.MODULE_ID_SELLCHANCE_INFO, ConstUtil.CODING_RULE_TABLE_SELLCHANCE, remark, ConstUtil.LOG_PROCESS_DELETE);

            return isSucc;

          
        }

        /// <summary>
        /// 获取报价详细信息
        /// </summary>
        /// <param name="chanceNo"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            return SellChanceDBHelper.GetOrderInfo(orderID);
        }

        #endregion

        #region 报表相关

        #region 按发现时间月份统计
        /// <summary>
        /// 按发现时间月份统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="total">统计方式</param>
        /// <returns></returns>
        public static DataTable GetChance(DateTime FindDate, DateTime FindDate1, string total, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellChanceDBHelper.GetChance(FindDate, FindDate1,total,  pageIndex,  pageCount,  ord, ref  TotalCount);
        }

        /// <summary>
        /// 按发现时间月份统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="total">统计方式</param>
        /// <returns></returns>
        public static DataTable GetChance(DateTime FindDate, DateTime FindDate1, string total)
        {
            return SellChanceDBHelper.GetChance(FindDate, FindDate1, total);
        }

        #endregion


        #region 按业务员统计
        
        /// <summary>
        /// 按业务员统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <returns></returns>
        public static DataTable GetChanceBySeller(DateTime FindDate, DateTime FindDate1, int SellerID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellChanceDBHelper.GetChanceBySeller(FindDate, FindDate1, SellerID, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 按业务员统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <returns></returns>
        public static DataTable GetChanceBySeller(DateTime FindDate, DateTime FindDate1, int SellerID)
        {
            return SellChanceDBHelper.GetChanceBySeller(FindDate, FindDate1, SellerID);
        }


        #endregion

        #region 按业务员与阶段统计

        /// <summary>
        /// 按业务员与阶段统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <param name="Phase">阶段</param>
        /// <returns></returns>
        public static DataTable GetChanceBySellerAndPhase(DateTime FindDate, DateTime FindDate1, int? SellerID, string Phase, 
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellChanceDBHelper.GetChanceBySellerAndPhase(FindDate, FindDate1, SellerID, Phase, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 按业务员与阶段统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <param name="Phase">阶段</param>
        /// <returns></returns>
        public static DataTable GetChanceBySellerAndPhase(DateTime FindDate, DateTime FindDate1, int? SellerID, string Phase)
        {
            return SellChanceDBHelper.GetChanceBySellerAndPhase(FindDate, FindDate1, SellerID, Phase);
        }

        #endregion

        #region 按业务员与状态统计

        /// <summary>
        /// 按业务员与状态统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <param name="Phase">阶段</param>
        /// <returns></returns>
        public static DataTable GetChanceOfState(DateTime FindDate, DateTime FindDate1, int? SellerID, string State)
        {
            return SellChanceDBHelper.GetChanceOfState(FindDate, FindDate1, SellerID, State);
        }

        /// <summary>
        /// 按业务员与状态统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <param name="Phase">阶段</param>
        /// <returns></returns>
        public static DataTable GetChanceOfState(DateTime FindDate, DateTime FindDate1, int? SellerID, string State,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellChanceDBHelper.GetChanceOfState(FindDate, FindDate1, SellerID, State, pageIndex, pageCount, ord, ref  TotalCount);
        }


        #endregion

        #region 按来源统计

        /// <summary>
        /// 按来源统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="HapSource">机会来源</param>
        /// <returns></returns>
        public static DataTable GetChanceByHapSource(DateTime FindDate, DateTime FindDate1, int? HapSource,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellChanceDBHelper.GetChanceByHapSource(FindDate, FindDate1, HapSource, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 按来源统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="HapSource">机会来源</param>
        /// <returns></returns>
        public static DataTable GetChanceByHapSource(DateTime FindDate, DateTime FindDate1, int? HapSource)
        {
            return SellChanceDBHelper.GetChanceByHapSource(FindDate, FindDate1, HapSource);
        }

        #endregion

        #region 按可能性与状态统计

        /// <summary>
        /// 按可能性与状态统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="Feasibility">机会可能性</param>
        /// <param name="State">状态</param>
        /// <returns></returns>
        public static DataTable GetChanceOfStateAndFeasibility(DateTime FindDate, DateTime FindDate1, int? Feasibility, string State,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellChanceDBHelper.GetChanceOfStateAndFeasibility(FindDate, FindDate1, Feasibility, State, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 按可能性与状态统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="Feasibility">机会可能性</param>
        /// <param name="State">状态</param>
        /// <returns></returns>
        public static DataTable GetChanceOfStateAndFeasibility(DateTime FindDate, DateTime FindDate1, int? Feasibility, string State)
        {
            return SellChanceDBHelper.GetChanceOfStateAndFeasibility(FindDate, FindDate1, Feasibility, State);
        }


        #endregion

        #region 按阶段与可能性统计

        /// <summary>
        /// 按阶段与可能性统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="Feasibility">阶段</param>
        /// <param name="Phase">阶段</param>
        /// <returns></returns>
        public static DataTable GetChanceOfPhaseAndFeasibility(DateTime FindDate, DateTime FindDate1, int? Feasibility, string Phase,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellChanceDBHelper.GetChanceOfPhaseAndFeasibility(FindDate, FindDate1, Feasibility, Phase, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 按阶段与可能性统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="Feasibility">阶段</param>
        /// <param name="Phase">阶段</param>
        /// <returns></returns>
        public static DataTable GetChanceOfPhaseAndFeasibility(DateTime FindDate, DateTime FindDate1, int? Feasibility, string Phase)
        {
            return SellChanceDBHelper.GetChanceOfPhaseAndFeasibility(FindDate, FindDate1, Feasibility, Phase);
        }

        #endregion

        #region 按预计签单月份统计

        /// <summary>
        /// 按预计签单月份统计
        /// </summary>
        /// <param name="IntendDate">开始日期</param>
        /// <param name="IntendDate1">结束日期</param>
        /// <returns></returns>
        public static DataTable GetChanceOfIntendDate(DateTime IntendDate, DateTime IntendDate1,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellChanceDBHelper.GetChanceOfIntendDate(IntendDate, IntendDate1, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 按预计签单月份统计
        /// </summary>
        /// <param name="IntendDate">开始日期</param>
        /// <param name="IntendDate1">结束日期</param>
        /// <returns></returns>
        public static DataTable GetChanceOfIntendDate(DateTime IntendDate, DateTime IntendDate1)
        {
            return SellChanceDBHelper.GetChanceOfIntendDate(IntendDate, IntendDate1);
        }
        #endregion

        #region 按创建时间统计

        /// <summary>
        /// 按创建时间统计
        /// </summary>
        /// <param name="CreateDate">开始日期</param>
        /// <param name="CreateDate1">结束日期</param>
        /// <returns></returns>
        public static DataTable GetChanceOfCreateDate(DateTime CreateDate, DateTime CreateDate1,int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellChanceDBHelper.GetChanceOfCreateDate(CreateDate, CreateDate1, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 按创建时间统计
        /// </summary>
        /// <param name="CreateDate">开始日期</param>
        /// <param name="CreateDate1">结束日期</param>
        /// <returns></returns>
        public static DataTable GetChanceOfCreateDate(DateTime CreateDate, DateTime CreateDate1)
        {
            return SellChanceDBHelper.GetChanceOfCreateDate(CreateDate, CreateDate1);
        }

        #endregion

        #region 按业务员与创建时间统计

        /// <summary>
        /// 按业务员与创建时间统计
        /// </summary>
        /// <param name="CreateDate">开始日期</param>
        /// <param name="CreateDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <returns></returns>
        public static DataTable GetChanceOfCreateDateAndSeller(DateTime CreateDate, DateTime CreateDate1, int? SellerID, 
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellChanceDBHelper.GetChanceOfCreateDateAndSeller(CreateDate, CreateDate1, SellerID, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 按业务员与创建时间统计
        /// </summary>
        /// <param name="CreateDate">开始日期</param>
        /// <param name="CreateDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <returns></returns>
        public static DataTable GetChanceOfCreateDateAndSeller(DateTime CreateDate, DateTime CreateDate1, int? SellerID)
        {
            return SellChanceDBHelper.GetChanceOfCreateDateAndSeller(CreateDate, CreateDate1, SellerID);
        }

        #endregion
        #endregion

        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            return SellChanceDBHelper.GetRepOrder(OrderNo);
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderDetail(string OrderNo)
        {
            return SellChanceDBHelper.GetRepOrderDetail(OrderNo);
        }

        public static void GetChanceType(string companycd, System.Web.UI.WebControls.DropDownList ddl)
        {
            SellChanceDBHelper.GetChanceType(companycd, ddl);
        }

        public static DataTable GetChanceByDept(string companycd, int ChanceType, string Phase, string State, string begindate, string enddate)
        {
            return SellChanceDBHelper.GetChanceByDept(companycd, ChanceType, Phase, State, begindate, enddate);
        }

        public static DataTable GetChanceByDeptDetials(int ChanceType, string Phase, string State, string begindate, string enddate, string getvalue, string order, string flag,int pageindex,int pagesize,ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellChanceDBHelper.GetChanceByDeptDetials(companyCD, ChanceType, Phase, State, begindate, enddate, getvalue, order, flag,pageindex,pagesize, ref recordCount);
        }

        public static void GetDeptByCompanyCD(string companycd, System.Web.UI.WebControls.DropDownList ddl)
        {
            SellChanceDBHelper.GetDeptByCompanyCD(companycd,ddl);
        }

        public static DataTable GetChanceByDept(string companycd, int ChanceType, string Phase, string State, string begindate, string enddate, string deptcode)
        {
            return SellChanceDBHelper.GetChanceByDept(companycd, ChanceType, Phase, State, begindate, enddate, deptcode);
        }

        public static DataTable GetChanceState(string companycd, int deptcode, int seller, string begindate, string enddate)
        {
            return SellChanceDBHelper.GetChanceState(companycd, deptcode, seller, begindate, enddate);
        }

        public static DataTable GetChanceByStateDetials(int State, string begindate, string enddate, string order,int pageindex,int pagesize, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellChanceDBHelper.GetChanceByStateDetials(companyCD, State, begindate, enddate, order,pageindex,pagesize, ref recordCount);
        }

        public static DataTable GetChancePhase(int deptcode, int seller, string begindate, string enddate)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellChanceDBHelper.GetChancePhase(companyCD, deptcode, seller, begindate, enddate);
        }

        public static DataTable GetChanceByPhaseDetials(int Phase, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellChanceDBHelper.GetChanceByPhaseDetials(companyCD, Phase, begindate, enddate, order,pageindex,pagesize, ref recordCount);
        }

        public static DataTable GetChanceSetUp(int chanceType, string Phase, string State, int deptcode, int seller, int timetype, string begindate, string enddate)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellChanceDBHelper.GetChanceSetUp(companyCD,chanceType, Phase, State, deptcode, seller, timetype, begindate, enddate);
        }

        public static DataTable GetChanceBySetUpDetials(string timeType, string timestr, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellChanceDBHelper.GetChanceBySetUpDetials(companyCD, timeType, timestr, begindate, enddate, order,pageindex,pagesize, ref recordCount);
        }

        public static DataTable GetActiveMoneyByDept(string companyCD,string begindate, string enddate)
        {
            return SellChanceDBHelper.GetActiveMoneyByDept(companyCD, begindate, enddate);
        }

        public static DataTable GetActiveMoneyByPerson(string companycd, string begindate, string enddate)
        {
            return SellChanceDBHelper.GetActiveMoneyByPerson(companycd, begindate, enddate);
        }

        #region 根据单据编号获取单据ID
        /// <summary>
        /// 根据单据编号获取单据ID
        /// </summary>
        /// <param name="billNo"></param>
        /// <param name="strCompanyCD"></param>
        /// <returns>对应单据ID</returns>
        public static int GetBillIDByBillNo(string billNo, string strCompanyCD)
        {
            return SellChanceDBHelper.GetBillIDByBillNo(billNo, strCompanyCD);
        }
        #endregion
    }
}
