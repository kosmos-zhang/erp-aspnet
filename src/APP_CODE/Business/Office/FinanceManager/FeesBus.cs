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
    public class FeesBus
    {
        #region 获取(1.采购订单2.采购到货通知单3.采购退货单4.销售订单5.销售发货通知单6.销售退货单7.费用报销单8.销售出库单9.其他出库单)票据信息
        /// <summary>
        /// 获取采购订单票据信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="SourceNo">单据编号</param>
        /// <param name="ContactUnitName">往来单位名称</param>
        /// <param name="ContactType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSourceInfoByContactType(string CompanyCD, string SourceNo, string ContactUnitName, string ContactType, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            DataTable dt = null;
            try
            {
                switch (ContactType)
                {
                    case "1":
                        dt = FeesDBHelper.GetSourceInfoByCGDD(CompanyCD, SourceNo, ContactUnitName, pageIndex, pageCount, ord, ref TotalCount);
                        break;
                    case "2":
                        dt = FeesDBHelper.GetSourceInfoByDHTZD(CompanyCD, SourceNo, ContactUnitName, pageIndex, pageCount, ord, ref TotalCount);
                        break;
                    case "3":
                        dt = FeesDBHelper.GetSourceInfoByCGTHD(CompanyCD, SourceNo, ContactUnitName, pageIndex, pageCount, ord, ref TotalCount);
                        break;
                    case "4":
                        dt = FeesDBHelper.GetSourceInfoByXSDD(CompanyCD, SourceNo, ContactUnitName, pageIndex, pageCount, ord, ref TotalCount);
                        break;
                    case "5":
                        dt = FeesDBHelper.GetSourceInfoByXSFHTZD(CompanyCD, SourceNo, ContactUnitName, pageIndex, pageCount, ord, ref TotalCount);
                        break;
                    case "6":
                        dt =  FeesDBHelper.GetSourceInfoByXSTHD(CompanyCD, SourceNo, ContactUnitName, pageIndex, pageCount, ord, ref TotalCount);
                        break;
                    case "7":
                        dt = FeesDBHelper.GetSourceInfoByFYBXD(CompanyCD, SourceNo, ContactUnitName, pageIndex, pageCount, ord, ref TotalCount);
                        break;
                    case "8":
                        dt = FeesDBHelper.GetSourceInfoByXSCKD(CompanyCD, SourceNo, ContactUnitName, pageIndex, pageCount, ord, ref TotalCount);
                        break;
                    case "9":
                        dt = FeesDBHelper.GetSourceInfoByQTCKD(CompanyCD, SourceNo, ContactUnitName, pageIndex, pageCount, ord, ref TotalCount);
                        break;
                    default:
                        dt = null;
                        break;
                }

                return dt;
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据销售订单编号获取费用明细
        public static DataTable GetSourceDetailByXSDD(string CompanyCD, string[] OrderNos)
        {
            return FeesDBHelper.GetSourceDetailByXSDD(CompanyCD, OrderNos);
        }
        #endregion

        #region 根据销售订单编号获取费用明细
        public static DataTable GetFeeReturnByXSDD(string CompanyCD, string[] OrderNos)
        {
            return FeesDBHelper.GetFeeReturnByXSDD(CompanyCD, OrderNos);
        }
        #endregion

        #region 添加新单据
        /// <summary>
        /// 添加新单据
        /// </summary>
        /// <returns></returns>
        public static bool Insert(FeesModel sellOrderModel, List<FeesDetailModel> sellOrderDetailModellList, out string strMsg,out int Id)
        {
            bool isSuc = false;
            string remark = string.Empty;
            strMsg = "";
            Id = 0;
            try
            {
                isSuc = FeesDBHelper.Insert(sellOrderModel, sellOrderDetailModellList, out  strMsg,out Id);
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
            //SellLogCommon.InsertLog(sellOrderModel.OrderNo, ConstUtil.MODULE_ID_SELLORDER_ADD, ConstUtil.CODING_RULE_TABLE_SELLORDER, remark, ConstUtil.LOG_PROCESS_INSERT);

            return isSuc;
        }
        #endregion

        #region 修改
        public static bool Update(FeesModel sellOrderModel, List<FeesDetailModel> sellOrderDetailModellList, out string strMsg)
        {

            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            strMsg = "";
            try
            {
                isSucc = FeesDBHelper.Update(sellOrderModel, sellOrderDetailModellList, out  strMsg); ;
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
            //SellLogCommon.InsertLog(sellOrderModel.OrderNo, ConstUtil.MODULE_ID_SELLORDER_ADD, ConstUtil.CODING_RULE_TABLE_SELLORDER, remark, ConstUtil.LOG_PROCESS_UPDATE);
            return isSucc;
        }
        #endregion

        #region 费用票据列表
        public static DataTable GetFeesBySearch(string CompanyCD, FeesModel FeesM, string DateBegin, string DateEnd, string PriceB, string PriceE, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return FeesDBHelper.GetFeesBySearch(CompanyCD, FeesM, DateBegin, DateEnd, PriceB, PriceE, pageIndex, pageCount, ord, ref TotalCount);
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
                isSucc = FeesDBHelper.Del(IDs, out strMsg, out strFieldText);
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
            return FeesDBHelper.GetOrderInfo(ID);
        }
        #endregion

        #region 获取明细
        public static DataTable GetFeeDetail(string CompanyCD, string FeesNo)
        {
            return FeesDBHelper.GetFeeDetail(CompanyCD, FeesNo);
        }
        #endregion

        #region 确认
        public static bool ConfirmFees(string OrderNO, FeesModel FeesM, List<FeesDetailModel> FeesDetailM, out string strMsg)
        {
            return FeesDBHelper.ConfirmFees(OrderNO,FeesM,FeesDetailM, out strMsg);
        }
        #endregion

        #region 导出费用票据列表
        public static DataTable ExportFeesBySearch(string CompanyCD, FeesModel FeesM, string DateBegin, string DateEnd, string PriceB, string PriceE)
        {
            return FeesDBHelper.ExportFeesBySearch(CompanyCD, FeesM, DateBegin, DateEnd, PriceB, PriceE);
        }
        #endregion

        #region 根据ID获取详细
        public static DataTable GetOrderInfoByNo(string BillNo)
        {
            return FeesDBHelper.GetOrderInfoByNo(BillNo);
        }
        #endregion
    }
}
