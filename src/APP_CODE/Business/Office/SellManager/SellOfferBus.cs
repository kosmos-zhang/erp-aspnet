/***********************************************************************
 * 
 * Module Name:XBase.Business.Office.SystemManager.AdversaryInfoBus.cs
 * Current Version: 1.0 
 * Creator: 周军
 * Auditor:2009-01-12
 * End Date:
 * Description: 销售报价单业务层处理
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
    public class SellOfferBus
    {
        /// <summary>
        /// 添加销售报价单
        /// </summary>
        /// <param name="sellChanceModel">销售机会表实体</param>
        /// <param name="sellChancePushModel">销售阶段表实体</param>
        /// <returns>是否添加成功</returns>
        public static bool InsertOrder(Hashtable ht,SellOfferModel sellOfferModel, List<SellOfferDetailModel> SellOrderDetailModelList,
            List<SellOfferHistoryModel> sellOfferHistoryModelList, out string strMsg)
        {

            bool isSuc = false;
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSuc = SellOfferDBHelper.InsertOrder(ht,sellOfferModel, SellOrderDetailModelList, sellOfferHistoryModelList,out  strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLOFFER_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellOfferModel.OfferNo, ConstUtil.MODULE_ID_SELLOFFER_ADD, ConstUtil.CODING_RULE_TABLE_SELLOFFER, remark, ConstUtil.LOG_PROCESS_INSERT);

            return isSuc;

        }

        /// <summary>
        /// 修改销售报价单
        /// </summary>
        /// <param name="sellChanceModel">销售机会表实体</param>
        /// <param name="sellChancePushModel">销售阶段表实体</param>
        /// <returns>是否添加成功</returns>
        public static bool UpdateOrder(Hashtable ht,SellOfferModel sellOfferModel, List<SellOfferDetailModel> SellOrderDetailModelList,
            List<SellOfferHistoryModel> sellOfferHistoryModelList, out string strMsg)
        {

            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSucc = SellOfferDBHelper.UpdateOrder(ht,sellOfferModel, SellOrderDetailModelList, sellOfferHistoryModelList, out  strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLOFFER_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellOfferModel.OfferNo, ConstUtil.MODULE_ID_SELLOFFER_ADD, ConstUtil.CODING_RULE_TABLE_SELLOFFER, remark, ConstUtil.LOG_PROCESS_UPDATE);
            return isSucc;

        }

        /// <summary>
        /// 获取报价单列表 
        /// </summary>
        /// <param name="sellOfferModel">sellOfferModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(string EFIndex, string EFDesc, SellOfferModel sellOfferModel, int? FlowStatus, DateTime? dt, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellOfferDBHelper.GetOrderList(EFIndex, EFDesc,sellOfferModel, FlowStatus, dt, pageIndex, pageCount, ord, ref TotalCount);
        }


        /// <summary>
        /// 获取当前单据的id
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static int GetOrderID(string orderNo)
        {
            return SellOfferDBHelper.GetOrderID(orderNo);
        }

        /// <summary>
        /// 删除销售报价单
        /// </summary>
        /// <param name="OfferNOs"></param>
        /// <returns></returns>
        public static bool DelOrder(string OfferNOs, out string strMsg, out string strFieldText)
        {

            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            strFieldText = "";
            strMsg = "";
            try
            {
                isSucc = SellOfferDBHelper.DelOrder(OfferNOs, out strMsg, out strFieldText);

                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLOFFER_INFO);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            string[] orderNoS = null;
            orderNoS = OfferNOs.Split(',');

            for (int i = 0; i < orderNoS.Length; i++)
            {
                SellLogCommon.InsertLog(orderNoS[i], ConstUtil.MODULE_ID_SELLOFFER_INFO, ConstUtil.CODING_RULE_TABLE_SELLOFFER, remark, ConstUtil.LOG_PROCESS_DELETE);
            }
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
                isSucc = SellOfferDBHelper.ConfirmOrder(OrderNO,out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLOFFER_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLOFFER_ADD, ConstUtil.CODING_RULE_TABLE_SELLOFFER, remark, strElement);

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
                isSucc = SellOfferDBHelper.UnConfirmOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLOFFER_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLOFFER_ADD, ConstUtil.CODING_RULE_TABLE_SELLOFFER, remark, strElement);

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
                isSucc = SellOfferDBHelper.CloseOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLOFFER_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLOFFER_ADD, ConstUtil.CODING_RULE_TABLE_SELLOFFER, remark, strElement);

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
                isSucc = SellOfferDBHelper.UnCloseOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLOFFER_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLOFFER_ADD, ConstUtil.CODING_RULE_TABLE_SELLOFFER, remark, strElement);

            return isSucc;
        }

        /// <summary>
        /// 获取报价单主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            return SellOfferDBHelper.GetOrderInfo(orderID);
        }

        /// <summary>
        /// 获取报价记录
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataTable GetSellOfferHistory(string orderNo)
        {
            return SellOfferDBHelper.GetSellOfferHistory(orderNo);
        }

        /// <summary>
        /// 获取单据明细信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable GetOrderDetail(string orderNo)
        {
            return SellOfferDBHelper.GetOrderDetail(orderNo);
        }

        /// <summary>
        /// 获取报价记录详细信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="OfferTime"></param>
        /// <returns></returns>
        public static DataTable GetSellOfferHistoryDetail(string orderNo, int OfferTime)
        {
            return SellOfferDBHelper.GetSellOfferHistoryDetail(orderNo, OfferTime);
        }

        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            return SellOfferDBHelper.GetRepOrder(OrderNo);
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderDetail(string OrderNo)
        {
            return SellOfferDBHelper.GetRepOrderDetail(OrderNo);
        }
    }
}
