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
    public class SellContractBus
    {
        /// <summary>
        /// 添加销售合同
        /// </summary>
        /// <param name="sellChanceModel"></param>
        /// <param name="sellChancePushModel"></param>
        /// <returns>是否添加成功</returns>
        public static bool InsertOrder(Hashtable ht, SellContractModel sellContractModel,
            List<SellContractDetailModel> SellContractDetailModelList, out string strMsg)
        {
            bool isSuc = false;
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSuc = SellContractDBHelper.InsertOrder(ht,sellContractModel, SellContractDetailModelList, out  strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCONTRANCT_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellContractModel.ContractNo, ConstUtil.MODULE_ID_SELLCONTRANCT_ADD, ConstUtil.CODING_RULE_TABLE_SELLCONTRANCT, remark, ConstUtil.LOG_PROCESS_INSERT);

            return isSuc;

        }

        /// <summary>
        /// 修改销售合同
        /// </summary>
        /// <param name="sellChanceModel">销售机会表实体</param>
        /// <param name="sellChancePushModel">销售阶段表实体</param>
        /// <returns>是否添加成功</returns>
        public static bool UpdateOrder(Hashtable ht,SellContractModel sellContractModel,
            List<SellContractDetailModel> SellContractDetailModelList, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSucc = SellContractDBHelper.UpdateOrder(ht,sellContractModel, SellContractDetailModelList, out  strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCONTRANCT_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellContractModel.ContractNo, ConstUtil.MODULE_ID_SELLCONTRANCT_ADD, ConstUtil.CODING_RULE_TABLE_SELLCONTRANCT, remark, ConstUtil.LOG_PROCESS_UPDATE);
            return isSucc;

        }

        /// <summary>
        /// 获取当前单据的id
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static int GetOrderID(string orderNo)
        {
            return SellContractDBHelper.GetOrderID(orderNo);
        }

        /// <summary>
        /// 删除合同
        /// </summary>
        /// <param name="orderNos"></param>
        /// <returns></returns>
        public static bool DelOrder(string orderNos, out string strMsg, out string strFieldText)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            strFieldText = "";
            strMsg = "";
            try
            {
                isSucc = SellContractDBHelper.DelOrder(orderNos, out strMsg, out strFieldText);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCONTRANCT_INFO);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            string[] orderNoS = null;
            orderNoS = orderNos.Split(',');

            for (int i = 0; i < orderNoS.Length; i++)
            {
                SellLogCommon.InsertLog(orderNoS[i], ConstUtil.MODULE_ID_SELLCONTRANCT_INFO, ConstUtil.CODING_RULE_TABLE_SELLCONTRANCT, remark, ConstUtil.LOG_PROCESS_DELETE);
            }
            return isSucc;

        }


        /// <summary>
        /// 获取合同列表 
        /// </summary>
        /// <param name="sellOfferModel">sellOfferModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(string EFIndex,string EFDesc,SellContractModel sellContractModel, int? FlowStatus, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellContractDBHelper.GetOrderList(EFIndex, EFDesc, sellContractModel, FlowStatus, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 终止合同
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static bool EndOrder(string OrderNO, out string strMsg)
        {
            string strElement = string.Empty;
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;

            //操作名称
            strElement = ConstUtil.LOG_PROCESS_ENDCONTRACT;
            strMsg = "";

            try
            {
                isSucc = SellContractDBHelper.EndOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCONTRANCT_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLCONTRANCT_ADD, ConstUtil.CODING_RULE_TABLE_SELLCONTRANCT, remark, strElement);

            return isSucc;
        }

        /// <summary>
        /// 确认单据
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool ConfirmOrder(string OrderNO, out string strMsg)
        {
            string strElement = string.Empty;
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;

            //操作名称
            strElement = ConstUtil.LOG_PROCESS_CONFIRM;
            strMsg = "";

            try
            {
                isSucc = SellContractDBHelper.ConfirmOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCONTRANCT_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLCONTRANCT_ADD, ConstUtil.CODING_RULE_TABLE_SELLCONTRANCT, remark, strElement);

            return isSucc;
        }

        /// <summary>
        /// 取消确认单据
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool UnConfirmOrder(string OrderNO, out string strMsg)
        {
            string strElement = string.Empty;
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;

            //操作名称
            strElement = ConstUtil.LOG_PROCESS_UNCONFIRM;
            strMsg = "";

            try
            {
                isSucc = SellContractDBHelper.UnConfirmOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCONTRANCT_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLCONTRANCT_ADD, ConstUtil.CODING_RULE_TABLE_SELLCONTRANCT, remark, strElement);

            return isSucc;
        }

        /// <summary>
        /// 结单
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool CloseOrder(string OrderNO, out string strMsg)
        {
            string strElement = string.Empty;
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;

            //操作名称
            strElement = ConstUtil.LOG_PROCESS_COMPLETE;
            strMsg = "";

            try
            {
                isSucc = SellContractDBHelper.CloseOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCONTRANCT_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLCONTRANCT_ADD, ConstUtil.CODING_RULE_TABLE_SELLCONTRANCT, remark, strElement);

            return isSucc;
        }

        /// <summary>
        /// 取消结单
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool UnCloseOrder(string OrderNO, out string strMsg)
        {
            string strElement = string.Empty;
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;

            //操作名称
            strElement = ConstUtil.LOG_PROCESS_CONCELCOMPLETE;
            strMsg = "";

            try
            {
                isSucc = SellContractDBHelper.UnCloseOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCONTRANCT_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLCONTRANCT_ADD, ConstUtil.CODING_RULE_TABLE_SELLCONTRANCT, remark, strElement);

            return isSucc;
        }

        /// <summary>
        /// 获取单据明细信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable GetOrderDetail(string orderNo)
        {
            return SellContractDBHelper.GetOrderDetail(orderNo);
        }

        /// <summary>
        /// 获取合同主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            return SellContractDBHelper.GetOrderInfo(orderID);
        }

        #region 报表相关


        /// <summary>
        /// 合同订单状态分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <returns></returns>
        public static DataTable GetTotal(DateTime startDate, DateTime endDate, bool isCon, bool isOrd)
        {
            return SellContractDBHelper.GetTotal(startDate, endDate, isCon, isOrd);
        }

        #region 合同订单状态/金额分布

        /// <summary>
        /// 合同订单状态/金额分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <returns></returns>
        public static DataTable GetTotalByState(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? CurrencyType,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellContractDBHelper.GetTotalByState(startDate, endDate, isCon, isOrd, CurrencyType, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 合同订单状态/金额分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <returns></returns>
        public static DataTable GetTotalByState(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? CurrencyType)
        {
            return SellContractDBHelper.GetTotalByState(startDate, endDate, isCon, isOrd, CurrencyType);
        }

        #endregion

        #region 合同订单业务员/金额分布

        /// <summary>
        /// 合同订单业务员/金额分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotalBySeller(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? SellerID, int? CurrencyType,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellContractDBHelper.GetTotalBySeller(startDate, endDate, isCon, isOrd, SellerID, CurrencyType, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 合同订单业务员/金额分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotalBySeller(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? SellerID, int? CurrencyType)
        {
            return SellContractDBHelper.GetTotalBySeller(startDate, endDate, isCon, isOrd, SellerID, CurrencyType);
        }

        #endregion

        #region 合同订单类别分布

        /// <summary>
        /// 合同订单类别分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <param name="SellType">销售类别</param>
        /// <returns></returns>
        public static DataTable GetTotalBySellerAndSellType(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? SellerID, int? SellType, int? CurrencyType,
             int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellContractDBHelper.GetTotalBySellerAndSellType(startDate, endDate, isCon, isOrd, SellerID, SellType, CurrencyType, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 合同订单类别分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <param name="SellType">销售类别</param>
        /// <returns></returns>
        public static DataTable GetTotalBySellerAndSellType(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? SellerID, int? SellType, int? CurrencyType)
        {
            return SellContractDBHelper.GetTotalBySellerAndSellType(startDate, endDate, isCon, isOrd, SellerID, SellType, CurrencyType);
        }

        #endregion

        #region 合同订单状态/业务员统计

        /// <summary>
        /// 合同订单类别分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotalBySellerAndState(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? SellerID, int? CurrencyType,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellContractDBHelper.GetTotalBySellerAndState(startDate, endDate, isCon, isOrd, SellerID, CurrencyType, pageIndex, pageCount, ord, ref  TotalCount);
        }


        /// <summary>
        /// 合同订单类别分布
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotalBySellerAndState(DateTime startDate, DateTime endDate, bool isCon, bool isOrd, int? SellerID, int? CurrencyType)
        {
            return SellContractDBHelper.GetTotalBySellerAndState(startDate, endDate, isCon, isOrd, SellerID, CurrencyType);
        }

        #endregion

        #region 合同订单签约月份/金额统计

        /// <summary>
        /// 合同订单签约月份/金额统计
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotalBySellerAndDate(string startDate, string endDate, bool isCon, bool isOrd, int? SellerID, int? CurrencyType,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellContractDBHelper.GetTotalBySellerAndDate(startDate, endDate, isCon, isOrd, SellerID, CurrencyType, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 合同订单签约月份/金额统计
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isCon">是否按合同统计</param>
        /// <param name="isOrd">是否按订单统计</param>
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotalBySellerAndDate(string startDate, string endDate, bool isCon, bool isOrd, int? SellerID, int? CurrencyType)
        {
            return SellContractDBHelper.GetTotalBySellerAndDate(startDate, endDate, isCon, isOrd, SellerID, CurrencyType);
        }

        #endregion

        #region 合同订单未尽收款金额按签约月份统计

        /// <summary>
        /// 合同订单未尽收款金额按签约月份统计
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// 
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotal(string startDate, string endDate, int? SellerID, int? CurrencyType,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellContractDBHelper.GetTotal(startDate, endDate, SellerID, CurrencyType, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 合同订单未尽收款金额按签约月份统计
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// 
        /// <param name="Seller">业务员</param>
        /// <returns></returns>
        public static DataTable GetTotal(string startDate, string endDate, int? SellerID, int? CurrencyType)
        {
            return SellContractDBHelper.GetTotal(startDate, endDate, SellerID, CurrencyType);
        }

        #endregion

        #region 开票月度统计

        /// <summary>
        /// 开票月度统计
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="strType">发票类型</param>
        /// <returns></returns>
        public static DataTable GetTotalBybill(string startDate, string endDate, string strType, int? CurrencyType,
             int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellContractDBHelper.GetTotalBybill(startDate, endDate, strType, CurrencyType, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 开票月度统计
        /// </summary>
        /// <param name="stratDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="strType">发票类型</param>
        /// <returns></returns>
        public static DataTable GetTotalBybill(string startDate, string endDate, string strType, int? CurrencyType)
        {
            return SellContractDBHelper.GetTotalBybill(startDate, endDate, strType, CurrencyType);
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
            return SellContractDBHelper.GetRepOrder(OrderNo);
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderDetail(string OrderNo)
        {
            return SellContractDBHelper.GetRepOrderDetail(OrderNo);
        }

        #region 新报表相关

        /// <summary>
        /// 销售合同数量部门分布
        /// </summary>
        /// <param name="Status">销售合同状态</param>
        /// <param name="Type">合同业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByDeptNum(string Status, string Type, string BeginDate, string EndDate)
        {
            return SellContractDBHelper.GetContractByDeptNum(Status, Type, BeginDate, EndDate);
        }

        /// <summary>
        /// 销售合同数量人员分布
        /// </summary>
        /// <param name="Status">销售合同状态</param>
        /// <param name="Type">合同业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByPersonNum(string Status, string Type, string BeginDate, string EndDate)
        {
            return SellContractDBHelper.GetContractByPersonNum(Status, Type, BeginDate, EndDate);
        }

        /// <summary>
        /// 销售合同数量状态分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByStateNum(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            return SellContractDBHelper.GetContractByStateNum(GroupType, DeptOrEmployeeId, BeginDate, EndDate);
        }

        /// <summary>
        /// 销售合同数量类型分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByTypeNum(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            return SellContractDBHelper.GetContractByTypeNum(GroupType, DeptOrEmployeeId, BeginDate, EndDate);
        }


        /// <summary>
        /// 销售合同走势数量分析
        /// </summary>
        /// <param name="FromType">销售来源</param>
        /// <param name="DeptId">所属部门</param>
        /// <param name="EmployeeId">部门人员</param>
        /// <param name="DateType">时间分类</param>
        /// <param name="State">销售合同状态</param>
        /// <param name="BusiType">销售合同类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByTypeTrend(string GroupType, string DeptOrEmployeeId, string DateType, string State, string BusiType, string BeginDate, string EndDate)
        {
            return SellContractDBHelper.GetContractByTypeTrend(GroupType, DeptOrEmployeeId, DateType, State, BusiType, BeginDate, EndDate);
        }


        /// <summary>
        /// 销售合同金额部门分布
        /// </summary>
        /// <param name="Status">销售合同状态</param>
        /// <param name="Type">合同业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByDeptPrice(string Status, string Type, string BeginDate, string EndDate)
        {
            return SellContractDBHelper.GetContractByDeptPrice(Status, Type, BeginDate, EndDate);
        }

        /// <summary>
        /// 销售合同金额人员分布
        /// </summary>
        /// <param name="Status">销售合同状态</param>
        /// <param name="Type">合同业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByPersonPrice(string Status, string Type, string BeginDate, string EndDate)
        {
            return SellContractDBHelper.GetContractByPersonPrice(Status, Type, BeginDate, EndDate);
        }

        /// <summary>
        /// 销售合同数量状态分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByStatePrice(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            return SellContractDBHelper.GetContractByStatePrice(GroupType, DeptOrEmployeeId, BeginDate, EndDate);
        }


        /// <summary>
        /// 销售合同数量类型分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByTypePrice(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            return SellContractDBHelper.GetContractByTypePrice(GroupType, DeptOrEmployeeId, BeginDate, EndDate);
        }

        /// <summary>
        /// 销售合同走势数量分析
        /// </summary>
        /// <param name="FromType">销售来源</param>
        /// <param name="DeptId">所属部门</param>
        /// <param name="EmployeeId">部门人员</param>
        /// <param name="DateType">时间分类</param>
        /// <param name="State">销售合同状态</param>
        /// <param name="BusiType">销售合同类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetContractByTrendPrice(string GroupType, string DeptOrEmployeeId, string DateType, string State, string BusiType, string BeginDate, string EndDate)
        {
            return SellContractDBHelper.GetContractByTrendPrice(GroupType, DeptOrEmployeeId, DateType, State, BusiType, BeginDate, EndDate);
        }
        /// 销售合同金额人员分布
        /// </summary>
        /// <param name="Status">销售合同状态</param>
        /// <param name="Type">合同业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetSellContractDetail(int Status, int Type, string Name, string DeptAndPerson, string BeginDate, string EndDate, string DateType1, string GroupType, string DeptOrEmployeeId, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellContractDBHelper.GetSellContractDetail(Status, Type, Name, DeptAndPerson, BeginDate, EndDate, DateType1, GroupType, DeptOrEmployeeId, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 销售合同金额人员分布
        /// </summary>
        /// <param name="Status">销售合同状态</param>
        /// <param name="Type">合同业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetSellContractDetail(int Status, int Type, string Name, string DeptAndPerson, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellContractDBHelper.GetSellContractDetail(Status, Type, Name, DeptAndPerson, BeginDate, EndDate, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 销售合同金额人员分布
        /// </summary>
        /// <param name="Status">销售合同状态</param>
        /// <param name="Type">合同业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetSellContractDetail(int Status, int Type, string Name, string DeptAndPerson, string BeginDate, string EndDate)
        {
            return SellContractDBHelper.GetSellContractDetail(Status, Type, Name, DeptAndPerson, BeginDate, EndDate);
        }
        #endregion


        #region 若是执行状态的销售合同已超过限期时，更新销售合同状态为“已到期”
        /// <summary>
        /// 若是执行状态的销售合同已超过限期时，更新销售合同状态为“已到期”
        /// 只对单据状态为执行或结单，合同状态为执行中 的单据进行处理
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        public static bool UpDateStatus(string strCompanyCD)
        {
           return  SellContractDBHelper.UpDateStatus(strCompanyCD);
        }
        #endregion
    }
}
