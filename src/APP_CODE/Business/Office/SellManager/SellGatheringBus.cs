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
    public class SellGatheringBus
    {
        /// <summary>
        /// 添加回款计划
        /// </summary>
        /// <param name="sellGatheringModel"></param>
        /// <returns></returns>
        public static bool InsertSellGathering(Hashtable ht, SellGatheringModel sellGatheringModel, out string strMsg)
        {
            bool isSuc = false;
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSuc = SellGatheringDBHelper.InsertSellGathering(ht,sellGatheringModel,out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_GATHERING_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellGatheringModel.GatheringNo, ConstUtil.MODULE_ID_GATHERING_ADD, ConstUtil.CODING_RULE_TABLE_GATHERING, remark, ConstUtil.LOG_PROCESS_INSERT);

            return isSuc;

           
        }

         /// <summary>
        /// 修改回款计划
        /// </summary>
        /// <param name="sellGatheringModel"></param>
        /// <returns></returns>
        public static bool UpdateSellGathering(Hashtable ht, SellGatheringModel sellGatheringModel, out string strMsg)
        {
            bool isSuc = false;
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSuc = SellGatheringDBHelper.UpdateSellGathering(ht,sellGatheringModel, out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_GATHERING_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(sellGatheringModel.GatheringNo, ConstUtil.MODULE_ID_GATHERING_ADD, ConstUtil.CODING_RULE_TABLE_GATHERING, remark, ConstUtil.LOG_PROCESS_UPDATE);

            return isSuc;

           
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sellGatheringModel"></param>
        /// <returns></returns>
        public static DataTable GetSellGathering(SellGatheringModel sellGatheringModel, string PlanPrice0,string EFIndex,string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellGatheringDBHelper.GetSellGathering(sellGatheringModel, PlanPrice0, EFIndex, EFDesc, pageIndex, pageCount, ord, ref TotalCount);
        }
         /// <summary>
        /// 删除回款计划
        /// </summary>
        /// <param name="strIDS"></param>
        /// <returns></returns>
        public static bool DelSellGathering(string strIDS, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
          
            strMsg = "";
            try
            {
                isSucc = SellGatheringDBHelper.DelSellGathering(strIDS,out strMsg);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_GATHERING_INFO);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            string[] orderNoS = null;
            orderNoS = strIDS.Split(',');

            for (int i = 0; i < orderNoS.Length; i++)
            {
                SellLogCommon.InsertLog(orderNoS[i], ConstUtil.MODULE_ID_GATHERING_INFO, ConstUtil.CODING_RULE_TABLE_GATHERING, remark, ConstUtil.LOG_PROCESS_DELETE);
            }


            return isSucc;
        }

         /// <summary>
        /// 获取发货单主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            return SellGatheringDBHelper.GetOrderInfo(orderID);
        }

        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            return SellGatheringDBHelper.GetRepOrder(OrderNo);
        }
    }
}
