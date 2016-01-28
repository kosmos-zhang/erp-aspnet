using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.SellManager;
using XBase.Data.Office.SellManager;
using XBase.Common;
using XBase.Business.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.SellManager
{
    public class SellBackBus
    {
        /// <summary>
        /// 保存销售发货单
        /// </summary>
        /// <returns></returns>
        public static bool SaveSellBack(Hashtable ht, SellBackModel sellBackModel, List<SellBackDetailModel> sellBackDetailModellList, out string strMsg)
        {
            bool isSuc = false;
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSuc = SellBackDBHelper.SaveSellBack(ht,sellBackModel, sellBackDetailModellList, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLBACK_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellBackModel.BackNo, ConstUtil.MODULE_ID_SELLBACK_ADD, ConstUtil.CODING_RULE_TABLE_SELLBACK, remark, ConstUtil.LOG_PROCESS_INSERT);

            return isSuc;

        }

        /// <summary>
        /// 获取退货原因信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetReasonType()
        {
            return SellBackDBHelper.GetReasonType();
        }

        /// <summary>
        /// 保存销售发货单
        /// </summary>
        /// <returns></returns>
        public static bool UpdateSellBack(Hashtable ht, SellBackModel sellBackModel, List<SellBackDetailModel> sellBackDetailModellList, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSucc = SellBackDBHelper.UpdateSellBack(ht,sellBackModel, sellBackDetailModellList, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLBACK_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellBackModel.BackNo, ConstUtil.MODULE_ID_SELLBACK_ADD, ConstUtil.CODING_RULE_TABLE_SELLBACK, remark, ConstUtil.LOG_PROCESS_UPDATE);
            return isSucc;

        }
        /// <summary>
        /// 获取当前单据的id
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static int GetOrderID(string orderNo)
        {
            return SellBackDBHelper.GetOrderID(orderNo);
        }

        /// <summary>
        /// 删除单据
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
                isSucc = SellBackDBHelper.DelOrder(orderNos, out strMsg, out strFieldText);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLBACK_INFO);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }

            string[] orderNoS = null;
            orderNoS = orderNos.Split(',');

            for (int i = 0; i < orderNoS.Length; i++)
            {
                SellLogCommon.InsertLog(orderNoS[i], ConstUtil.MODULE_ID_SELLBACK_INFO, ConstUtil.CODING_RULE_TABLE_SELLBACK, remark, ConstUtil.LOG_PROCESS_DELETE);
            }

            return isSucc;

        }

        #region 确认，取消确认，结单，取消结单
        /// <summary>
        /// 确认单据
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool ConfirmOrder(SellBackModel sellBackModel, out string strMsg, out string strFieldText)
        {
            string strElement = string.Empty;
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;

            //操作名称
            strElement = ConstUtil.LOG_PROCESS_CONFIRM;
            strFieldText = "";
            strMsg = "";

            try
            {
                isSucc = SellBackDBHelper.ConfirmOrder(sellBackModel.BackNo, out strMsg, out strFieldText);
                //确认成功后调用“自动生成凭证”方法
                if (isSucc == true)
                {
                    string str = "";
                    bool AutoVoucherInsertFlag = XBase.Business.Office.FinanceManager.AutoVoucherBus.AutoVoucherInsert(4, Convert.ToDecimal(sellBackModel.TotalFee), "officedba.sellback," + sellBackModel.ID, sellBackModel.CurrencyType + "," + sellBackModel.Rate, Convert.ToInt32(sellBackModel.CustID), out str);
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
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLBACK_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(sellBackModel.BackNo, ConstUtil.MODULE_ID_SELLBACK_ADD, ConstUtil.CODING_RULE_TABLE_SELLBACK, remark, strElement);

            return isSucc;
        }

        /// <summary>
        /// 取消确认单据
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool UnConfirmOrder(SellBackModel sellBackModel, out string strMsg)
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
                isSucc = SellBackDBHelper.UnConfirmOrder(sellBackModel.BackNo, out strMsg);
                //取消确认成功后调用“自动生成凭证”方法
                if (isSucc == true)
                {
                    string str = "";
                    bool AutoVoucherInsertFlag = XBase.Business.Office.FinanceManager.AutoVoucherBus.AntiConfirmVoucher("officedba.sellback," + sellBackModel.ID, out str);
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
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLBACK_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(sellBackModel.BackNo, ConstUtil.MODULE_ID_SELLBACK_ADD, ConstUtil.CODING_RULE_TABLE_SELLBACK, remark, strElement);

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
                isSucc = SellBackDBHelper.CloseOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLBACK_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLBACK_ADD, ConstUtil.CODING_RULE_TABLE_SELLBACK, remark, strElement);

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
                isSucc = SellBackDBHelper.UnCloseOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLBACK_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLBACK_ADD, ConstUtil.CODING_RULE_TABLE_SELLBACK, remark, strElement);

            return isSucc;
        }
        #endregion

        #region 获取单据相关信息
        /// <summary>
        /// 获取单据列表 
        /// </summary>
        /// <param name="sellOfferModel">sellOfferModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(SellBackModel sellBackModel, DateTime? dt, int? Reason, int? FlowStatus,string EFIndex,string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellBackDBHelper.GetOrderList(sellBackModel, dt, Reason, FlowStatus, EFIndex, EFDesc, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 获取单据明细信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable GetOrderDetail(string orderNo)
        {
            return SellBackDBHelper.GetOrderDetail(orderNo);
        }

        /// <summary>
        /// 获取单据主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            return SellBackDBHelper.GetOrderInfo(orderID);
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
            return SellBackDBHelper.GetRepOrder(OrderNo);
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderDetail(string OrderNo)
        {
            return SellBackDBHelper.GetRepOrderDetail(OrderNo);
        }
        #endregion

        #region 新报表

        /// <summary>
        /// 退货数量部门分布
        /// </summary>
        /// <param name="Type">退货单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetBackByDeptNum(string Type, string BeginDate, string EndDate)
        {
            return SellBackDBHelper.GetBackByDeptNum(Type, BeginDate, EndDate);
        }

        /// <summary>
        /// 退货数量人员分布
        /// </summary>
        /// <param name="Type">订单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetBackByPersonNum(string Type, string BeginDate, string EndDate)
        {
            return SellBackDBHelper.GetBackByPersonNum(Type, BeginDate, EndDate);
        }


        /// <summary>
        /// 退货数量区域分布
        /// </summary>
        /// <param name="BusiType">分类</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetBackByAreaNum(string BusiType, string BeginDate, string EndDate)
        {
            return SellBackDBHelper.GetBackByAreaNum(BusiType, BeginDate, EndDate);
        }

        /// <summary>
        /// 退货走势数量分析
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
        public static DataTable GetBackByTrendNum(string GroupType, string DeptOrEmployeeId, string DateType, string State, string BusiType, string BeginDate, string EndDate, string AreaId)
        {
            return SellBackDBHelper.GetBackByTrendNum(GroupType, DeptOrEmployeeId, DateType, State, BusiType, BeginDate, EndDate, AreaId);
        }

        /// <summary>
        /// 退货金额部门分布
        /// </summary>
        /// <param name="Type">退货单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetBackByDeptPrice(string Type, string BeginDate, string EndDate)
        {
            return SellBackDBHelper.GetBackByDeptPrice(Type, BeginDate, EndDate);
        }

        /// <summary>
        /// 退货金额人员分布
        /// </summary>
        /// <param name="Type">订单业务类型</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetBackByPersonPrice(string Type, string BeginDate, string EndDate)
        {
            return SellBackDBHelper.GetBackByPersonPrice(Type, BeginDate, EndDate);
        }



        /// <summary>
        /// 退货金额区域分布
        /// </summary>
        /// <param name="BusiType">分类</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetBackByAreaPrice(string BusiType, string BeginDate, string EndDate)
        {
            return SellBackDBHelper.GetBackByAreaPrice(BusiType, BeginDate, EndDate);
        }

        /// <summary>
        /// 退货走势金额分析
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
        public static DataTable GetBackByTrendPrice(string GroupType, string DeptOrEmployeeId, string DateType, string State, string BusiType, string BeginDate, string EndDate, string AreaId)
        {
            return SellBackDBHelper.GetBackByTrendPrice(GroupType, DeptOrEmployeeId, DateType, State, BusiType, BeginDate, EndDate, AreaId);
        }


        /// <summary>
        /// 图表明细列表 
        /// </summary>
        public static DataTable GetSellBackDetail(string GroupType, string DeptOrEmployeeId, string DateType, string DateValue, string State, string BusiType, string BeginDate, string EndDate, string AreaId)
        {
            return SellBackDBHelper.GetSellBackDetail(GroupType, DeptOrEmployeeId, DateType, DateValue, State, BusiType, BeginDate, EndDate, AreaId);
        }

        /// <summary>
        /// 图表明细列表 
        /// </summary>
        public static DataTable GetSellBackDetail(string GroupType, string DeptOrEmployeeId, string DateType, string DateValue, string State, string BusiType, string BeginDate, string EndDate, string AreaId, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellBackDBHelper.GetSellBackDetail(GroupType, DeptOrEmployeeId, DateType, DateValue, State, BusiType, BeginDate, EndDate, AreaId, pageIndex, pageCount, ord, ref TotalCount);
        }

        #endregion
    }
}
