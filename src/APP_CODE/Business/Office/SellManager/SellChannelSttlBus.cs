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
    public  class SellChannelSttlBus
    {
        /// <summary>
        /// 保存销售委托代销单
        /// </summary>
        /// <returns></returns>
        public static bool SaveOrder(Hashtable ht, SellChannelSttlModel sellChannelSttlModel, List<SellChannelSttlDetailModel> sellChannelSttlDetailModellist, out string strMsg)
        {
            bool isSuc = false;
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSuc = SellChannelSttlDBHelper.SaveOrder(ht,sellChannelSttlModel, sellChannelSttlDetailModellist, out  strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCHANNELSTTL_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellChannelSttlModel.SttlNo, ConstUtil.MODULE_ID_SELLCHANNELSTTL_ADD, ConstUtil.CODING_RULE_TABLE_SELLCHANNELSTTL, remark, ConstUtil.LOG_PROCESS_INSERT);

            return isSuc;
        }
          /// <summary>
        /// 更新销售委托代销单
        /// </summary>
        /// <returns></returns>
        public static bool UpdateOrder(Hashtable ht, SellChannelSttlModel sellChannelSttlModel, List<SellChannelSttlDetailModel> sellChannelSttlDetailModellist, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSucc = SellChannelSttlDBHelper.UpdateOrder(ht,sellChannelSttlModel, sellChannelSttlDetailModellist, out  strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCHANNELSTTL_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellChannelSttlModel.SttlNo, ConstUtil.MODULE_ID_SELLCHANNELSTTL_ADD, ConstUtil.CODING_RULE_TABLE_SELLCHANNELSTTL, remark, ConstUtil.LOG_PROCESS_UPDATE);
            return isSucc;
           
        }

        /// <summary>
        /// 获取当前单据的id
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static int GetOrderID(string orderNo)
        {
            return SellChannelSttlDBHelper.GetOrderID(orderNo);
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
                isSucc = SellChannelSttlDBHelper.DelOrder(orderNos, out strMsg, out strFieldText);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCHANNELSTTL_INFO);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }

            string[] orderNoS = null;
            orderNoS = orderNos.Split(',');

            for (int i = 0; i < orderNoS.Length; i++)
            {
                SellLogCommon.InsertLog(orderNoS[i], ConstUtil.MODULE_ID_SELLCHANNELSTTL_INFO, ConstUtil.CODING_RULE_TABLE_SELLCHANNELSTTL, remark, ConstUtil.LOG_PROCESS_DELETE);
            }

            return isSucc;
          
        }


        /// <summary>
        /// 获取单据列表 
        /// </summary>
        /// <param name="sellOfferModel">sellOfferModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(SellChannelSttlModel sellChannelSttlModel, DateTime? dt, int? FlowStatus,string EFIndex,string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellChannelSttlDBHelper.GetOrderList(sellChannelSttlModel, dt, FlowStatus, EFIndex, EFDesc, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 确认单据
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool ConfirmOrder(SellChannelSttlModel sellChannelSttlModel, out string strMsg, out string strFieldText)
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
                isSucc = SellChannelSttlDBHelper.ConfirmOrder(sellChannelSttlModel.SttlNo, out strMsg, out strFieldText);
                //确认成功后调用“自动生成凭证”方法
                if (isSucc == true)
                {
                    string str = "";
                    bool AutoVoucherInsertFlag = XBase.Business.Office.FinanceManager.AutoVoucherBus.AutoVoucherInsert(3, Convert.ToDecimal(sellChannelSttlModel.TotalFee), "officedba.SellChannelSttl," + sellChannelSttlModel.ID, sellChannelSttlModel.CurrencyType + "," + sellChannelSttlModel.Rate, Convert.ToInt32(sellChannelSttlModel.CustID), out str);
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
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCHANNELSTTL_ADD);

                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(sellChannelSttlModel.SttlNo, ConstUtil.MODULE_ID_SELLCHANNELSTTL_ADD, ConstUtil.CODING_RULE_TABLE_SELLCHANNELSTTL, remark, strElement);

            return isSucc;
        }

        /// <summary>
        /// 取消确认单据
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool UnConfirmOrder(SellChannelSttlModel sellChannelSttlModel, out string strMsg)
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
                isSucc = SellChannelSttlDBHelper.UnConfirmOrder(sellChannelSttlModel.SttlNo, out strMsg);
                //取消确认成功后调用“自动生成凭证”方法
                if (isSucc == true)
                {
                    string str = "";
                    bool AutoVoucherInsertFlag = XBase.Business.Office.FinanceManager.AutoVoucherBus.AntiConfirmVoucher("officedba.SellChannelSttl," + sellChannelSttlModel.ID, out str);
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
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCHANNELSTTL_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(sellChannelSttlModel.SttlNo, ConstUtil.MODULE_ID_SELLCHANNELSTTL_ADD, ConstUtil.CODING_RULE_TABLE_SELLCHANNELSTTL, remark, strElement);

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
                isSucc = SellChannelSttlDBHelper.CloseOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCHANNELSTTL_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLCHANNELSTTL_ADD, ConstUtil.CODING_RULE_TABLE_SELLCHANNELSTTL, remark, strElement);

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
                isSucc = SellChannelSttlDBHelper.UnCloseOrder(OrderNO, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLCHANNELSTTL_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //写入日志
            SellLogCommon.InsertLog(OrderNO, ConstUtil.MODULE_ID_SELLCHANNELSTTL_ADD, ConstUtil.CODING_RULE_TABLE_SELLCHANNELSTTL, remark, strElement);

            return isSucc;
        }

        /// <summary>
        /// 获取单据明细信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable GetOrderDetail(string orderNo)
        {
            return SellChannelSttlDBHelper.GetOrderDetail(orderNo);
        }

        /// <summary>
        /// 获取单据主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            return SellChannelSttlDBHelper.GetOrderInfo(orderID);
        }


        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            return SellChannelSttlDBHelper.GetRepOrder(OrderNo);
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderDetail(string OrderNo)
        {
            return SellChannelSttlDBHelper.GetRepOrderDetail(OrderNo);
        }
    }

}
