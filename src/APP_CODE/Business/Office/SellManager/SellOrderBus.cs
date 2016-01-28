using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.SellManager;
using XBase.Data.Office.SellManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Business.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.SellManager
{
    public class SellOrderBus
    {

        /// <summary>
        /// 更新开票状态  Added By jiangym 2009-04-22
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns></returns>
        public static bool UpdateisOpenBill(string ID)
        {
            try
            {
                return SellOrderDBHelper.UpdateisOpenBill(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据检索条件检索满足条件的信息 Added By jiangym 2009-04-16
        /// </summary>
        /// <param name="OrderNo">订单号</param>
        /// <param name="Title">主题</param>
        /// <param name="CustName">客户</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">将诶数日期</param>
        /// <returns>DataTable</returns>
        public static DataTable SearchOrderByCondition(string OrderNo, string Title,
            string CustName, string StartDate, string EndDate)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return SellOrderDBHelper.SearchOrderByCondition(CompanyCD, OrderNo, Title, CustName, StartDate, EndDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 根据检索条件检索满足条件的信息 Added By Moshenlin 2010-03-11
        /// </summary>
        /// <param name="OrderNo">订单号</param>
        /// <param name="Title">主题</param>
        /// <param name="CustName">客户</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">将诶数日期</param>
        /// <returns>DataTable</returns>
        public static DataTable SearchOrderByCondition(string OrderNo, string Title,string CustName, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return SellOrderDBHelper.SearchOrderByCondition(CompanyCD, OrderNo, Title, CustName, StartDate, EndDate,pageIndex,pageSize,OrderBy,ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 根据检索条件检索出满足条件的信息 Added By moshenlin 2009-06-29
        /// </summary>
        /// <param name="ids">主表ID集</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable SearchOrderByCondition(string ids)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return SellOrderDBHelper.SearchOrderByCondition(ids, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 添加新单据
        /// <summary>
        /// 添加新单据
        /// </summary>
        /// <returns></returns>
        public static bool Insert(Hashtable ht, SellOrderModel sellOrderModel, List<SellOrderDetailModel> sellOrderDetailModellList,
            List<SellOrderFeeDetailModel> sellOrderFeeDetailModelList, out string strMsg)
        {
            bool isSuc = false;
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSuc = SellOrderDBHelper.Insert(ht,sellOrderModel, sellOrderDetailModellList, sellOrderFeeDetailModelList, out  strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLORDER_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellOrderModel.OrderNo, ConstUtil.MODULE_ID_SELLORDER_ADD, ConstUtil.CODING_RULE_TABLE_SELLORDER, remark, ConstUtil.LOG_PROCESS_INSERT);

            return isSuc;

        }
        #endregion

        #region 保存销售发货单
        /// <summary>
        /// 保存销售发货单
        /// </summary>
        /// <returns></returns>
        public static bool Update(Hashtable ht, SellOrderModel sellOrderModel, List<SellOrderDetailModel> sellOrderDetailModellList,
            List<SellOrderFeeDetailModel> sellOrderFeeDetailModelList, out string strMsg)
        {

            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSucc = SellOrderDBHelper.Update(ht,sellOrderModel, sellOrderDetailModellList, sellOrderFeeDetailModelList, out  strMsg); ;
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLORDER_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellOrderModel.OrderNo, ConstUtil.MODULE_ID_SELLORDER_ADD, ConstUtil.CODING_RULE_TABLE_SELLORDER, remark, ConstUtil.LOG_PROCESS_UPDATE);
            return isSucc;

        }
        #endregion

        #region 获取当前单据的id
        /// <summary>
        /// 获取当前单据的id
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static int GetOrderID(string orderNo)
        {
            return SellOrderDBHelper.GetOrderID(orderNo);
        }
        #endregion

        #region 删除销售发货单
        /// <summary>
        /// 删除销售发货单
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
                isSucc = SellOrderDBHelper.DelOrder(orderNos, out strMsg, out strFieldText);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLORDER_INFO);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            string[] orderNoS = null;
            orderNoS = orderNos.Split(',');

            for (int i = 0; i < orderNoS.Length; i++)
            {
                SellLogCommon.InsertLog(orderNoS[i], ConstUtil.MODULE_ID_SELLORDER_INFO, ConstUtil.CODING_RULE_TABLE_SELLORDER, remark, ConstUtil.LOG_PROCESS_DELETE);
            }


            return isSucc;

        }
        #endregion

        #region 获取单据相关信息
        /// <summary>
        /// 获取单据明细信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable GetOrderDetail(string orderNo)
        {
            return SellOrderDBHelper.GetOrderDetail(orderNo);
        }

        /// <summary>
        /// 获取发货单主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            return SellOrderDBHelper.GetOrderInfo(orderID);
        }
        /// <summary>
        /// 获取销售费用明细
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable GetFee(string orderNo)
        {
            return SellOrderDBHelper.GetFee(orderNo);
        }
        /// <summary>
        /// 获取发货单列表 
        /// </summary>
        /// <param name="sellContractModel">sellContractModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(SellOrderModel sellOrderModel, decimal? TotalPrice1, string SendPro, int? FlowStatus,string EFIndex,string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellOrderDBHelper.GetOrderList(sellOrderModel, TotalPrice1, SendPro, FlowStatus, EFIndex, EFDesc, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion 

        #region 终止订单
        /// <summary>
        /// 终止订单
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
            strElement = ConstUtil.LOG_PROCESS_ENDORDER;
            strMsg = "";

            try
            {
                isSucc = SellOrderDBHelper.EndOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLORDER_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLORDER_ADD, ConstUtil.CODING_RULE_TABLE_SELLORDER, remark, strElement);

            return isSucc;
        }
        #endregion

        #region 确认，取消确认，结单，取消结单
        /// <summary>
        /// 确认单据
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool ConfirmOrder(SellOrderModel sellOrderModel, out string strMsg)
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
                isSucc = SellOrderDBHelper.ConfirmOrder(sellOrderModel.OrderNo, out strMsg);
                //确认成功后调用“自动生成凭证”方法
                if (isSucc == true)
                {
                    string str="";
                    bool AutoVoucherInsertFlag = XBase.Business.Office.FinanceManager.AutoVoucherBus.AutoVoucherInsert(2, Convert.ToDecimal( sellOrderModel.TotalFee), "officedba.sellorder," + sellOrderModel.ID, sellOrderModel.CurrencyType + "," + sellOrderModel.Rate,Convert.ToInt32( sellOrderModel.CustID),out str);
                    //returnValue=0 业务单未设凭证模板,returnValue=1 企业不启用业务单自动生成凭证,returnValue = 2 企业不启用自动生成凭证自动登帐, returnValue = 3 自动生成凭证失败 ，returnValue = "4" 回写业务单登记凭证状态成功，returnValue = "5" 回写业务单登记凭证状态失败
                    if (AutoVoucherInsertFlag == true)
                    {
                        strMsg += str;
                    }
                    else
                    {
                        strMsg += str;
                    }

                }
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLORDER_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(sellOrderModel.OrderNo, ConstUtil.MODULE_ID_SELLORDER_ADD, ConstUtil.CODING_RULE_TABLE_SELLORDER, remark, strElement);

            return isSucc;
        }

        /// <summary>
        /// 取消确认单据
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool UnConfirmOrder(SellOrderModel sellOrderModel, out string strMsg)
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
                isSucc = SellOrderDBHelper.UnConfirmOrder(sellOrderModel.OrderNo, out strMsg);
                //取消确认成功后调用“自动生成凭证”方法
                if (isSucc == true)
                {
                    string str = "";
                    bool AutoVoucherInsertFlag = XBase.Business.Office.FinanceManager.AutoVoucherBus.AntiConfirmVoucher("officedba.sellorder," + sellOrderModel.ID, out str);
                    if (AutoVoucherInsertFlag == true)
                    {
                        //strMsg += str;
                    }
                    else
                    {
                        strMsg += str;
                    }

                }
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLORDER_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(sellOrderModel.OrderNo, ConstUtil.MODULE_ID_SELLORDER_ADD, ConstUtil.CODING_RULE_TABLE_SELLORDER, remark, strElement);

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
                isSucc = SellOrderDBHelper.CloseOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLORDER_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLORDER_ADD, ConstUtil.CODING_RULE_TABLE_SELLORDER, remark, strElement);

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
                isSucc = SellOrderDBHelper.UnCloseOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLORDER_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLORDER_ADD, ConstUtil.CODING_RULE_TABLE_SELLORDER, remark, strElement);

            return isSucc;
        }
        #endregion

        #region 报表相关

        #region 部门业绩周对比

        /// <summary>
        /// 部门业绩周对比
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="strDate">统计的月份的第一天（格式：2009-05-01）</param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndWeek(int? DeptID, string strDate, int? CurrencyType, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellOrderDBHelper.ReportByDeptAndWeek(DeptID, strDate, CurrencyType, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 部门业绩周对比
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="strDate">统计的月份的第一天（格式：2009-05-01）</param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndWeek(int? DeptID, string strDate, int? CurrencyType)
        {
            return SellOrderDBHelper.ReportByDeptAndWeek(DeptID, strDate, CurrencyType);
        }

        #endregion

        #region 部门业绩月度对比

        /// <summary>
        /// 部门业绩月度对比
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="Year">统计年份</param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndMonth(int? DeptID, string Year, int? CurrencyType, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellOrderDBHelper.ReportByDeptAndMonth(DeptID, Year, CurrencyType, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 部门业绩月度对比
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="Year">统计年份</param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndMonth(int? DeptID, string Year, int? CurrencyType)
        {
            return SellOrderDBHelper.ReportByDeptAndMonth(DeptID, Year, CurrencyType);
        }

        #endregion

        #region 部门业绩年度对比

        /// <summary>
        /// 部门业绩年度对比
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="Year">统计年份</param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndYear(int? DeptID, string Year, int? CurrencyType, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellOrderDBHelper.ReportByDeptAndYear(DeptID, Year, CurrencyType, pageIndex, pageCount, ord, ref  TotalCount);

        }

        /// <summary>
        /// 部门业绩年度对比
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="Year">统计年份</param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndYear(int? DeptID, string Year, int? CurrencyType)
        {
            return SellOrderDBHelper.ReportByDeptAndYear(DeptID, Year, CurrencyType);

        }

        #endregion

        #region 业务员销售实绩分析表

        /// <summary>
        /// 业务员销售实绩分析表
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="Year">统计年份</param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndSellerAndDate(int? DeptID, int? Seller, int CurrencyType, DateTime StartDate, DateTime EndDate, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellOrderDBHelper.ReportByDeptAndSellerAndDate(DeptID, Seller, CurrencyType, StartDate, EndDate, SearchModel, pageIndex, pageCount, ord, ref  TotalCount);

        }

        /// <summary>
        /// 业务员销售实绩分析表
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="Year">统计年份</param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndSellerAndDate(int? DeptID, int? Seller, DateTime StartDate, DateTime EndDate, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellOrderDBHelper.ReportByDeptAndSellerAndDate(DeptID, Seller, StartDate, EndDate, SearchModel, pageIndex, pageCount, ord, ref  TotalCount);

        }

        #endregion

        /// <summary>
        /// 部门销售实绩分析表
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="SearchModel">查询的模式:1表示所有数据，2表示分页查处部分数据</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndDate(int? DeptID, DateTime StartDate, DateTime EndDate, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellOrderDBHelper.ReportByDeptAndDate(DeptID, StartDate, EndDate, SearchModel, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 部门销售实绩分析表
        /// </summary>
        /// <param name="DeptID">部门编号</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="SearchModel">查询的模式:1表示所有数据，2表示分页查处部分数据</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndDate(int? DeptID, DateTime StartDate, DateTime EndDate, int CurrencyType, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellOrderDBHelper.ReportByDeptAndDate(DeptID, StartDate, EndDate, CurrencyType, SearchModel, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 部门业绩与成长率分析
        /// </summary>
        /// <param name="DeptID">部门</param>
        /// <param name="iCount">时间的跨度</param>
        /// <param name="CurrencyType">币种</param>
        /// <param name="EndYear">截止的年份</param>
        /// <param name="SearchModel">查询的模式:1表示所有数据，2表示分页查处部分数据</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable ReportByDeptGetGrow(int? DeptID, int iCount, int? CurrencyType, int EndYear, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {

            return SellOrderDBHelper.ReportByDeptGetGrow(DeptID, iCount, CurrencyType, EndYear, SearchModel, pageIndex, pageCount, ord, ref  TotalCount);
        }

        /// <summary>
        /// 部门业绩与成长率分析
        /// </summary>
        /// <param name="DeptID">部门</param>
        /// <param name="iCount">时间的跨度</param>
        /// <param name="CurrencyType">币种</param>
        /// <param name="EndYear">截止的年份</param>
        /// <param name="SearchModel">查询的模式:1表示所有数据，2表示分页查处部分数据</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable ReportByDeptAndSellerGetGrow(int? DeptID, int? Seller, int iCount, int? CurrencyType, int EndYear, string SearchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {

            return SellOrderDBHelper.ReportByDeptAndSellerGetGrow(DeptID, Seller, iCount, CurrencyType, EndYear, SearchModel, pageIndex, pageCount, ord, ref  TotalCount);
        }
        #endregion

        #region 打印
        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            return SellOrderDBHelper.GetRepOrder(OrderNo);
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderDetail(string OrderNo)
        {
            return SellOrderDBHelper.GetRepOrderDetail(OrderNo);
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderFee(string OrderNo)
        {
            return SellOrderDBHelper.GetRepOrderFee(OrderNo);
        }
        #endregion

        #region 新报表相关

        /// <summary>
        /// 销售订单数量部门分布
        /// </summary>
        /// <param name="Status">销售订单状态</param>
        /// <param name="Type">订单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByDeptNum(string Status, string Type, string BeginDate, string EndDate)
        {
            return SellOrderDBHelper.GetOrderByDeptNum(Status, Type, BeginDate, EndDate);
        }

        /// <summary>
        /// 销售订单数量人员分布
        /// </summary>
        /// <param name="Status">销售订单状态</param>
        /// <param name="Type">订单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByPersonNum(string Status, string Type, string BeginDate, string EndDate)
        {
            return SellOrderDBHelper.GetOrderByPersonNum(Status, Type, BeginDate, EndDate);
        }

        /// <summary>
        /// 销售订单数量状态分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByStateNum(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            return SellOrderDBHelper.GetOrderByStateNum(GroupType, DeptOrEmployeeId, BeginDate, EndDate);
        }

        /// <summary>
        /// 销售订单数量类型分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByTypeNum(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            return SellOrderDBHelper.GetOrderByTypeNum(GroupType, DeptOrEmployeeId, BeginDate, EndDate);
        }


        /// <summary>
        /// 销售订单数量区域分布
        /// </summary>
        /// <param name="GroupType">部门或人员</param>
        /// <param name="DeptOrEmployeeId">部门或人员ID</param>
        /// <param name="State">状态</param>
        /// <param name="BusiType">分类</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByAreaNum(string GroupType, string DeptOrEmployeeId, string State, string BusiType, string BeginDate, string EndDate)
        {
            return SellOrderDBHelper.GetOrderByAreaNum(GroupType, DeptOrEmployeeId, State, BusiType, BeginDate, EndDate);
        }



        /// <summary>
        /// 销售订单走势数量分析
        /// </summary>
        /// <param name="FromType">销售来源</param>
        /// <param name="DeptId">所属部门</param>
        /// <param name="EmployeeId">部门人员</param>
        /// <param name="DateType">时间分类</param>
        /// <param name="State">销售订单状态</param>
        /// <param name="BusiType">销售订单类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByTrendNum(string GroupType, string DeptOrEmployeeId, string DateType, string State, string BusiType, string BeginDate, string EndDate, string AreaId)
        {
            return SellOrderDBHelper.GetOrderByTrendNum(GroupType, DeptOrEmployeeId, DateType, State, BusiType, BeginDate, EndDate, AreaId);
        }


        /// <summary>
        /// 销售订单金额部门分布
        /// </summary>
        /// <param name="Status">销售订单状态</param>
        /// <param name="Type">订单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByDeptPrice(string Status, string Type, string BeginDate, string EndDate)
        {
            return SellOrderDBHelper.GetOrderByDeptPrice(Status, Type, BeginDate, EndDate);
        }

        /// <summary>
        /// 销售订单金额人员分布
        /// </summary>
        /// <param name="Status">销售订单状态</param>
        /// <param name="Type">订单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByPersonPrice(string Status, string Type, string BeginDate, string EndDate)
        {
            return SellOrderDBHelper.GetOrderByPersonPrice(Status, Type, BeginDate, EndDate);
        }

        /// <summary>
        /// 销售订单金额状态分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByStatePrice(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            return SellOrderDBHelper.GetOrderByStatePrice(GroupType, DeptOrEmployeeId, BeginDate, EndDate);
        }


        /// <summary>
        /// 销售订单金额类型分布
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <param name="EmployeeId">业务员Id</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByTypePrice(string GroupType, string DeptOrEmployeeId, string BeginDate, string EndDate)
        {
            return SellOrderDBHelper.GetOrderByTypePrice(GroupType, DeptOrEmployeeId, BeginDate, EndDate);
        }

        /// <summary>
        /// 销售订单金额区域分布
        /// </summary>
        /// <param name="GroupType">部门或人员</param>
        /// <param name="DeptOrEmployeeId">部门或人员ID</param>
        /// <param name="State">状态</param>
        /// <param name="BusiType">分类</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByAreaPrice(string GroupType, string DeptOrEmployeeId, string State, string BusiType, string BeginDate, string EndDate)
        {
            return SellOrderDBHelper.GetOrderByAreaPrice(GroupType, DeptOrEmployeeId, State, BusiType, BeginDate, EndDate);
        }


        /// <summary>
        /// 销售订单走势金额分析
        /// </summary>
        /// <param name="FromType">销售来源</param>
        /// <param name="DeptId">所属部门</param>
        /// <param name="EmployeeId">部门人员</param>
        /// <param name="DateType">时间分类</param>
        /// <param name="State">销售订单状态</param>
        /// <param name="BusiType">销售订单类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOrderByTrendPrice(string GroupType, string DeptOrEmployeeId, string DateType, string State, string BusiType, string BeginDate, string EndDate, string AreaId)
        {
            return SellOrderDBHelper.GetOrderByTrendPrice(GroupType, DeptOrEmployeeId, DateType, State, BusiType, BeginDate, EndDate, AreaId);
        }


        /// <summary>
        /// 销售明细
        /// </summary>
        public static DataTable GetSellOrderDetail(string GroupType, string DeptOrEmployeeId, string DateType, string DateValue, string State, string BusiType, string BeginDate, string EndDate, string AreaId, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellOrderDBHelper.GetSellOrderDetail(GroupType, DeptOrEmployeeId, DateType, DateValue, State, BusiType, BeginDate, EndDate, AreaId, pageIndex, pageCount, ord, ref TotalCount);
        }
        /// <summary>
        /// 销售明细
        /// </summary>
        public static DataTable GetSellOrderDetail(string GroupType, string DeptOrEmployeeId, string DateType, string DateValue, string State, string BusiType, string BeginDate, string EndDate, string AreaId)
        {
            return SellOrderDBHelper.GetSellOrderDetail(GroupType, DeptOrEmployeeId, DateType, DateValue, State, BusiType, BeginDate, EndDate, AreaId);
        }
        #endregion
    }
}