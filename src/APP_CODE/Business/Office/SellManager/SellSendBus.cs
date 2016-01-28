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
    public  class SellSendBus
    {
         /// <summary>
        /// 保存销售发货单
        /// </summary>
        /// <returns></returns>
        public static bool SaveSellSend(Hashtable ht, SellSendModel sellSendModel, List<SellSendDetailModel> sellSendDetailModellList, out string strMsg)
        {
            bool isSuc = false;
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSuc = SellSendDBHelper.SaveSellSend(ht,sellSendModel, sellSendDetailModellList, out  strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLSEND_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellSendModel.SendNo, ConstUtil.MODULE_ID_SELLSEND_ADD, ConstUtil.CODING_RULE_TABLE_SELLSEND, remark, ConstUtil.LOG_PROCESS_INSERT);

            return isSuc;
            
        }

        /// <summary>
        /// 更新销售发货单
        /// </summary>
        /// <returns></returns>
        public static bool UpdateSellSend(Hashtable ht, SellSendModel sellSendModel, List<SellSendDetailModel> sellSendDetailModellList, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSucc = SellSendDBHelper.UpdateSellSend(ht,sellSendModel, sellSendDetailModellList, out  strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLSEND_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellSendModel.SendNo, ConstUtil.MODULE_ID_SELLSEND_ADD, ConstUtil.CODING_RULE_TABLE_SELLSEND, remark, ConstUtil.LOG_PROCESS_UPDATE);
            return isSucc;
          
        }

        /// <summary>
        /// 获取当前单据的id
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static int GetOrderID(string orderNo)
        {
            return SellSendDBHelper.GetOrderID(orderNo);
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
                isSucc = SellSendDBHelper.DelOrder(orderNos, out strMsg, out strFieldText);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLSEND_INFO);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            
            string[] orderNoS = null;
            orderNoS = orderNos.Split(',');

            for (int i = 0; i < orderNoS.Length; i++)
            {
                SellLogCommon.InsertLog(orderNoS[i], ConstUtil.MODULE_ID_SELLSEND_INFO, ConstUtil.CODING_RULE_TABLE_SELLSEND, remark, ConstUtil.LOG_PROCESS_DELETE);
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
        public static bool ConfirmOrder(SellSendModel sellSendModel, out string strMsg, out string strFieldText)
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
                isSucc = SellSendDBHelper.ConfirmOrder(sellSendModel.SendNo, out strMsg, out strFieldText);
                //确认成功后调用“自动生成凭证”方法
                if (isSucc == true)
                {
                    string str = "";
                    bool AutoVoucherInsertFlag = XBase.Business.Office.FinanceManager.AutoVoucherBus.AutoVoucherInsert(11, Convert.ToDecimal(sellSendModel.TotalFee), "officedba.sellsend," + sellSendModel.ID, sellSendModel.CurrencyType + "," + sellSendModel.Rate, Convert.ToInt32(sellSendModel.CustID), out str);
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
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLSEND_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(sellSendModel.SendNo, ConstUtil.MODULE_ID_SELLSEND_ADD, ConstUtil.CODING_RULE_TABLE_SELLSEND, remark, strElement);

            return isSucc;
        }

        /// <summary>
        /// 取消确认单据
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool UnConfirmOrder(SellSendModel sellSendModel, out string strMsg)
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
                isSucc = SellSendDBHelper.UnConfirmOrder(sellSendModel.SendNo, out strMsg);
                //取消确认成功后调用“自动生成凭证”方法
                if (isSucc == true)
                {
                    string str = "";
                    bool AutoVoucherInsertFlag = XBase.Business.Office.FinanceManager.AutoVoucherBus.AntiConfirmVoucher("officedba.sellsend," + sellSendModel.ID, out str);
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
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLSEND_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(sellSendModel.SendNo, ConstUtil.MODULE_ID_SELLSEND_ADD, ConstUtil.CODING_RULE_TABLE_SELLSEND, remark, strElement);

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
                isSucc = SellSendDBHelper.CloseOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLSEND_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLSEND_ADD, ConstUtil.CODING_RULE_TABLE_SELLSEND, remark, strElement);

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
                isSucc = SellSendDBHelper.UnCloseOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLSEND_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLSEND_ADD, ConstUtil.CODING_RULE_TABLE_SELLSEND, remark, strElement);

            return isSucc;
        }
        #endregion

        /// <summary>
        /// 获取单据列表 
        /// </summary>
        /// <param name="sellOfferModel">sellOfferModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(SellSendModel sellSendModel, int? FlowStatus, string EFIndex, string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellSendDBHelper.GetOrderList(sellSendModel, FlowStatus, EFIndex, EFDesc, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 获取单据明细信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable GetOrderDetail(string orderNo)
        {
            return SellSendDBHelper.GetOrderDetail(orderNo);
        }

        /// <summary>
        /// 获取单据主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            return SellSendDBHelper.GetOrderInfo(orderID);
        }

        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            return SellSendDBHelper.GetRepOrder(OrderNo);
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderDetail(string OrderNo)
        {
            return SellSendDBHelper.GetRepOrderDetail(OrderNo);
        }

        /// <summary>
        /// 验证现有库存数量
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="strMsg"></param>
        /// <param name="strFieldText"></param>
        public static void CheckProCount(Hashtable ht, out string strMsg, out string strFieldText)
        {
            strFieldText = "";
            strMsg = "";
            SellSendDBHelper.CheckProCount(ht, out  strMsg, out  strFieldText);
        }
    }
}
