using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using XBase.Common;

namespace XBase.Business.Office.FinanceManager
{
   public class VoucherTemaplateBus
    {
        



        #region 添加新单据
        /// <summary>
        /// 添加新单据
        /// </summary>
        /// <returns></returns>
       public static bool Insert(VoucherTemplateModel voucherTemplateModel, List<VoucherTemplateDetailModel> voucherTemplateDetailModellList, out string strMsg, out int Id)
        {
            bool isSuc = false;
            string remark = string.Empty;
            strMsg = "";
            Id = 0;
            try
            {
                isSuc = VoucherTemaplateDBHelper.Insert(voucherTemplateModel, voucherTemplateDetailModellList, out  strMsg,out Id);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch 
            {
                ////输出日志
                //SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLORDER_ADD);
                ////设置操作成功标识 
                //remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //SellLogCommon.InsertLog(voucherTemplateModel.OrderNo, ConstUtil.MODULE_ID_SELLORDER_ADD, ConstUtil.CODING_RULE_TABLE_SELLORDER, remark, ConstUtil.LOG_PROCESS_INSERT);

            return isSuc;
        }
        #endregion

        #region 修改
        public static bool Update(VoucherTemplateModel voucherTemplateModel, List<VoucherTemplateDetailModel> voucherTemplateDetailModellList, out string strMsg)
        {

            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSucc = VoucherTemaplateDBHelper.Update(voucherTemplateModel, voucherTemplateDetailModellList, out  strMsg); ;
                //设置操作成功标识
                //remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch 
            {
                ////输出日志
                //SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLORDER_ADD);
                ////设置操作成功标识 
                //remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //SellLogCommon.InsertLog(voucherTemplateModel.OrderNo, ConstUtil.MODULE_ID_SELLORDER_ADD, ConstUtil.CODING_RULE_TABLE_SELLORDER, remark, ConstUtil.LOG_PROCESS_UPDATE);
            return isSucc;
        }
        #endregion

        #region 凭证模板列表
        public static DataTable GetFeesBySearch(string CompanyCD, VoucherTemplateModel FeesM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return VoucherTemaplateDBHelper.GetFeesBySearch(CompanyCD, FeesM, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 删除
        public static bool Del(string IDs, out string strMsg, out string strFieldText)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            strFieldText = "";
            strMsg = "";
            try
            {
                isSucc = VoucherTemaplateDBHelper.Del(IDs, out strMsg, out strFieldText);
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            catch 
            {
                ////输出日志
                //SellLogCommon.WriteSystemLog(ex, LogInfo.LogType.SYSTEM, LogInfo.SystemLogKind.SYSTEM_ERROR, ConstUtil.MODULE_ID_SELLORDER_INFO);
                ////设置操作成功标识 
                //remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //string[] orderNoS = null;
            //orderNoS = orderNos.Split(',');

            //for (int i = 0; i < orderNoS.Length; i++)
            //{
            //    SellLogCommon.InsertLog(orderNoS[i], ConstUtil.MODULE_ID_SELLORDER_INFO, ConstUtil.CODING_RULE_TABLE_SELLORDER, remark, ConstUtil.LOG_PROCESS_DELETE);
            //}


            return isSucc;

        }
        #endregion

        #region 根据ID获取详细信息
        public static DataTable GetFeesById(int ID)
        {
            return VoucherTemaplateDBHelper.GetOrderInfo(ID);
        }
        #endregion

        #region 获取明细
        public static DataTable GetFeeDetail(string CompanyCD, string FeesNo)
        {
            return VoucherTemaplateDBHelper.GetFeeDetail(CompanyCD, FeesNo);
        }
        #endregion


        #region 导出费用票据列表
        public static DataTable ExportFeesBySearch(string CompanyCD, VoucherTemplateModel FeesM)
        {
            return VoucherTemaplateDBHelper.ExportFeesBySearch(CompanyCD, FeesM);
        }
        #endregion

        #region 根据ID获取详细
        public static DataTable GetOrderInfoByNo(string BillNo)
        {
            return VoucherTemaplateDBHelper.GetOrderInfoByNo(BillNo);
        }
        #endregion
    }
}
