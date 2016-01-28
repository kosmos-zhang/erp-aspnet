using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.SellManager;
using XBase.Data.Office.SellManager;
using XBase.Common;
using System.Data;
using XBase.Model.Common;
using XBase.Business.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.SellManager
{
    public class AdversarySellBus
    {
        /// <summary>
        /// 选择竞争对手
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetAdversaryInfo(int? id)
        {
            return AdversarySellDBHelper.GetAdversaryInfo(id);
        }

        /// <summary>
        /// 选择竞争对手控件列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetAdversaryInfo(string Title,string OrderNO, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return AdversarySellDBHelper.GetAdversaryInfo(Title, OrderNO, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="adversarySellModel"></param>
        /// <returns></returns>
        public static int? Insert(AdversarySellModel adversarySellModel)
        {
            int? id = null;
           
            string remark = string.Empty;

            try
            {
                id = AdversarySellDBHelper.Insert(adversarySellModel);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_ADVERSARYSELL_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(adversarySellModel.CustNo, ConstUtil.MODULE_ID_ADVERSARYSELL_ADD, ConstUtil.CODING_RULE_TABLE_ADVERSARYSELL, remark, ConstUtil.LOG_PROCESS_INSERT);
              
            return id;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="adversarySellModel"></param>
        public static bool Update(AdversarySellModel adversarySellModel)
        {
           
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            
            try
            {
                isSucc = AdversarySellDBHelper.Update(adversarySellModel);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_ADVERSARYSELL_ADD);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(adversarySellModel.CustNo, ConstUtil.MODULE_ID_ADVERSARYSELL_ADD, ConstUtil.CODING_RULE_TABLE_ADVERSARYSELL, remark, ConstUtil.LOG_PROCESS_UPDATE);
            return isSucc;
        }

        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="orderNos"></param>
        /// <returns></returns>
        public static bool DelOrder(string orderIDs)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;

            try
            {
                isSucc = AdversarySellDBHelper.DelOrder(orderIDs);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch (Exception ex)
            {
                //输出日志
                SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_ADVERSARYSELL_INFO);
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            SellLogCommon.InsertLog(orderIDs, ConstUtil.MODULE_ID_ADVERSARYSELL_INFO, ConstUtil.CODING_RULE_TABLE_ADVERSARYSELL, remark, ConstUtil.LOG_PROCESS_DELETE);

            return isSucc;

        }

        /// <summary>
        /// 获取单据列表 
        /// </summary>
        /// <param name="sellContractModel">sellContractModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(AdversarySellModel adversarySellModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return AdversarySellDBHelper.GetOrderList(adversarySellModel, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            return AdversarySellDBHelper.GetRepOrder(OrderNo);
        }

        /// <summary>
        /// 获取单据主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            return AdversarySellDBHelper.GetOrderInfo(orderID);
        }


    }
}
