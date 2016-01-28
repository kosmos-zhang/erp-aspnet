/***********************************************
 * 类作用：   采购管理事务层处理               *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/04/21                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Model.Office.PurchaseManager;
using XBase.Data.Office.PurchaseManager;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;
using System.Data.SqlTypes;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using System.Collections;
namespace XBase.Business.Office.PurchaseManager
{
    // <summary>
    /// 类名：PurchaseContractBus
    /// 描述：采购管理事务层处理
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/21
    /// </summary>
    public class PurchaseRejectBus
    {
        #region 绑定采购类别
        /// <summary>
        /// 绑定采购类别
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetddlTypeID()
        {
            DataTable dt = PurchaseRejectDBHelper.GetddlTypeID();
            return dt;
        }
        #endregion

        #region 绑定采购交货方式
        /// <summary>
        /// 绑定采购交货方式
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetDrpTakeType()
        {
            DataTable dt = PurchaseArriveDBHelper.GetDrpTakeType();
            return dt;
        }
        #endregion

        #region 绑定采购运送方式
        /// <summary>
        /// 绑定采购运送方式
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetDrpCarryType()
        {
            DataTable dt = PurchaseArriveDBHelper.GetDrpCarryType();
            return dt;
        }
        #endregion

        #region 绑定采购结算方式
        /// <summary>
        /// 绑定采购结算方式
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetDrpPayType()
        {
            DataTable dt = PurchaseArriveDBHelper.GetDrpPayType();
            return dt;
        }
        #endregion

        #region 绑定采购支付方式
        /// <summary>
        /// 绑定采购结算方式
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetDrpMoneyType()
        {
            DataTable dt = PurchaseArriveDBHelper.GetDrpMoneyType();
            return dt;
        }
        #endregion

        #region 原因
        public static DataTable GetDrpApplyReason()
        {
            DataTable dt = PurchaseRejectDBHelper.GetDrpApplyReason();
            return dt;
        }
        #endregion

        #region 新建退货单
        public static bool InsertPurchaseReject(PurchaseRejectModel model, string DetailProductID, string DetailProductNo, string DetailProductName, string DetailUnitID, string DetailProductCount, string DetailBackCount, string DetailApplyReason, string DetailUnitPrice, string DetailTaxPrice, string DetailDiscount, string DetailTaxRate, string DetailTotalPrice, string DetailTotalFee, string DetailTotalTax, string DetailRemark, string DetailFromBillID, string DetailFromLineNo, string DetailUsedUnitCount, string DetailUsedUnitID, string DetailUsedPrice, string length, out string ID, Hashtable hd)
        {
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.RejectNo);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                //设置模块ID 模块ID请在ConstUtil中定义，以便维护
                logModel.ModuleID = ConstUtil.MODULE_ID_PurchaseReject_Add;
                succ = PurchaseRejectDBHelper.InsertPurchaseReject(model, DetailProductID, DetailProductNo, DetailProductName, DetailUnitID, DetailProductCount, DetailBackCount, DetailApplyReason, DetailUnitPrice, DetailTaxPrice, DetailDiscount, DetailTaxRate, DetailTotalPrice, DetailTotalFee, DetailTotalTax, DetailRemark, DetailFromBillID, DetailFromLineNo, DetailUsedUnitCount, DetailUsedUnitID, DetailUsedPrice, length, out ID, hd);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 更新退货单
        public static bool UpdatePurchaseReject(PurchaseRejectModel model, string DetailProductID, string DetailProductNo, string DetailProductName, string DetailUnitID, string DetailProductCount, string DetailBackCount, string DetailApplyReason, string DetailUnitPrice, string DetailTaxPrice, string DetailDiscount, string DetailTaxRate, string DetailTotalPrice, string DetailTotalFee, string DetailTotalTax, string DetailRemark, string DetailFromBillID, string DetailFromBillNo, string DetailFromLineNo, string DetailUsedUnitCount, string DetailUsedUnitID, string DetailUsedPrice, string length, string fflag2, string no, Hashtable ht)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            if (model.ID <= 0)
            {
                return false;
            }
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(no);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                //设置模块ID 模块ID请在ConstUtil中定义，以便维护
                logModel.ModuleID = ConstUtil.MODULE_ID_PurchaseReject_Add;
                succ = PurchaseRejectDBHelper.UpdatePurchaseReject(model, DetailProductID, DetailProductNo, DetailProductName, DetailUnitID, DetailProductCount, DetailBackCount, DetailApplyReason, DetailUnitPrice, DetailTaxPrice, DetailDiscount, DetailTaxRate, DetailTotalPrice, DetailTotalFee, DetailTotalTax, DetailRemark, DetailFromBillID, DetailFromBillNo, DetailFromLineNo, DetailUsedUnitCount, DetailUsedUnitID, DetailUsedPrice, length, fflag2, no, ht);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region 确认退货单
        public static bool ConfirmPurchaseReject(PurchaseRejectModel Model, string DetailBackCount, string DetailFromBillNo, string DetailFromLineNo, string DetailProductNo, string length, out string strMsg)
        {
            return PurchaseRejectDBHelper.ConfirmPurchaseReject(Model, DetailBackCount, DetailFromBillNo, DetailFromLineNo, DetailProductNo, length, out strMsg);
        }
        #endregion

        #region 取消确认退货单
        public static bool CancelConfirmPurchaseReject(PurchaseRejectModel Model, string DetailBackCount, string DetailFromBillNo, string DetailFromLineNo, string DetailProductNo, string length, out string strMsg)
        {
            return PurchaseRejectDBHelper.CancelConfirmPurchaseReject(Model, DetailBackCount, DetailFromBillNo, DetailFromLineNo, DetailProductNo, length, out strMsg);
        }
        #endregion

        #region 结单退货单
        public static bool ClosePurchaseReject(PurchaseRejectModel Model)
        {
            return PurchaseRejectDBHelper.ClosePurchaseReject(Model);
        }
        #endregion

        #region 取消结单退货单
        public static bool CancelClosePurchaseReject(PurchaseRejectModel Model)
        {
            return PurchaseRejectDBHelper.CancelClosePurchaseReject(Model);
        }
        #endregion

        #region 查询采购到货通知列表所需数据
        public static DataTable SelectPurchaseReject(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string RejectNo, string Title, string TypeID, string Purchaser, string FromType, string ProviderID, string BillStatus, string UsedStatus, string DeptID, string ProjectID, string EFIndex, string EFDesc)
        {
            try
            {
                return PurchaseRejectDBHelper.SelectRejectList(pageIndex, pageCount, orderBy, ref TotalCount, RejectNo, Title, TypeID, Purchaser, FromType, ProviderID, BillStatus, UsedStatus, DeptID, ProjectID, EFIndex, EFDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 选择采购入库源单数据
        public static DataTable GetPurchaseStorageDetail(string ProductNo, string ProductName, string FromBillNo, string CompanyCD, int ProviderID, int CurrencyTypeID, string Rate, string StorageEFIndex, string StorageEFDesc)
        {
            try
            {
                return PurchaseRejectDBHelper.GetPurchaseStorageDetail(ProductNo, ProductName, FromBillNo, CompanyCD, ProviderID, CurrencyTypeID, Rate, StorageEFIndex, StorageEFDesc);
            }
            catch 
            {
                return null;
            }
        }
        #endregion
        #region 选择采购到货源单数据
        public static DataTable GetPurchaseRejectDetail(string ProductNo, string ProductName, string FromBillNo, string CompanyCD, int ProviderID, int CurrencyTypeID, string Rate, string PurchaseArriveEFIndex, string PurchaseArriveEFDesc)
        {
            try
            {
                return PurchaseRejectDBHelper.GetPurchaseRejectDetail(ProductNo, ProductName, FromBillNo, CompanyCD, ProviderID, CurrencyTypeID, Rate, PurchaseArriveEFIndex, PurchaseArriveEFDesc);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 删除采购退货通知单
        public static bool DeletePurchaseRejectAll(string DetailNo)
        {
            LogInfoModel logModel = InitLogInfo(DetailNo);
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_PurchaseRejectInfo;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string CompanyCD = userInfo.CompanyCD;

            //string[] sql = new string[2];
            //int index = 0;
            //PurchaseRejectDBHelper.DeletePurchaseRejectPrimary(DetailNo, ref sql, index++);
            //PurchaseRejectDBHelper.DeletePurchaseRejectDetail(DetailNo, ref sql, index++);

            //SqlHelper.ExecuteTransForListWithSQL(sql);

            bool isSucc = PurchaseRejectDBHelper.DeletePurchaseRejectPrimary(DetailNo);
            //bool isSucc = SqlHelper.Result.OprateCount > 0 ? true : false;
            string remark;
            //成功时
            if (isSucc)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
                logModel.Remark = remark;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
                logModel.Remark = remark;
            }
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }
        #endregion

        #region 获取单个退货记录
        public static DataTable SelectReject(int ID)
        {
            try
            {
                return PurchaseRejectDBHelper.SelectReject(ID);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 获取退货明细
        public static DataTable Details(int ID, string FormType)
        {
            try
            {
                return PurchaseRejectDBHelper.Details(ID, FormType);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <returns></returns>
        private static LogInfoModel InitLogInfo(string RejectNo)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            ////设置模块ID 模块ID请在ConstUtil中定义，以便维护
            //logModel.ModuleID = ConstUtil.MODULE_ID_PurchaseReject_Add;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PURCHASEREJECT;
            //操作对象
            logModel.ObjectID = RejectNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;
        }
        #endregion

        #region 确定单据有没有被引用
        public static bool IsCitePurchaseReject(int ID)
        {
            try
            {
                return PurchaseRejectDBHelper.IsCitePurchaseReject(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购报表退货汇总表
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable SelectPurchaseRejectOverview(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProductID, string Reason, string StartRejectDate, string EndRejectDate)
        {
            try
            {
                return PurchaseRejectDBHelper.SelectPurchaseRejectOverview(pageIndex, pageCount, orderBy, ref TotalCount, ProductID, Reason, StartRejectDate, EndRejectDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购报表退货汇总表打印
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable SelectPurchaseRejectOverviewPrint(string ProductID, string Reason, string StartRejectDate, string EndRejectDate, string orderBy)
        {
            try
            {
                return PurchaseRejectDBHelper.SelectPurchaseRejectOverviewPrint(ProductID, Reason, StartRejectDate, EndRejectDate, orderBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  采购报表采购退货查询
        public static DataTable PurchaseRejectQuery(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProviderID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            try
            {
                return PurchaseRejectDBHelper.PurchaseRejectQuery(pageIndex, pageCount, orderBy, ref TotalCount, ProviderID, StartConfirmDate, EndConfirmDate, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  采购报表采购退货查询打印
        public static DataTable PurchaseRejectQueryPrint(string ProviderID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            try
            {
                return PurchaseRejectDBHelper.PurchaseRejectQueryPrint(ProviderID, StartConfirmDate, EndConfirmDate, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static DataTable GetPurchaseRejectType(string companycd, string billStatus, string FromType, int shState, string begindate, string enddate)
        {
            return PurchaseRejectDBHelper.GetPurchaseRejectType(companycd, billStatus, FromType, shState, begindate, enddate);
        }

        public static DataTable GetPurchaseRejectTypeDetails(string companycd, string billstatus, string fromtype, int shstate, int typeid, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            return PurchaseRejectDBHelper.GetPurchaseRejectTypeDetails(companycd, billstatus, fromtype, shstate, typeid, begindate, enddate, order, pageindex, pagesize, ref recordCount);
        }

        public static DataTable GetPurchaseRejectTypeDept(string companycd, string billStatus, string FromType, int shState, string begindate, string enddate)
        {
            return PurchaseRejectDBHelper.GetPurchaseRejectTypeDept(companycd, billStatus, FromType, shState, begindate, enddate);
        }

        public static DataTable GetPurchaseRejectDeptDetails(string companycd, string billstatus, string fromtype, int shstate, int deptid, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            return PurchaseRejectDBHelper.GetPurchaseRejectDeptDetails(companycd, billstatus, fromtype, shstate, deptid, begindate, enddate, order, pageindex, pagesize, ref recordCount);
        }

        public static DataTable GetPurchaseRejectProvider(string companycd, string billStatus, string FromType, int shState, string begindate, string enddate)
        {
            return PurchaseRejectDBHelper.GetPurchaseRejectProvider(companycd, billStatus, FromType, shState, begindate, enddate);
        }

        public static DataTable GetPurchaseRejectProviderDetails(string companycd, string billstatus, string fromtype, int shstate, int providerID, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            return PurchaseRejectDBHelper.GetPurchaseRejectProviderDetails(companycd, billstatus, fromtype, shstate, providerID, begindate, enddate, order, pageindex, pagesize, ref recordCount);
        }

        public static DataTable GetPurchaseRejectReason(string companycd, string billStatus, string FromType, int shState, string begindate, string enddate)
        {
            return PurchaseRejectDBHelper.GetPurchaseRejectReason(companycd, billStatus, FromType, shState, begindate, enddate);
        }

        public static DataTable GetPurchaseReasonDetails(string companycd, string billstatus, string fromtype, int shstate, int reasonid, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            return PurchaseRejectDBHelper.GetPurchaseReasonDetails(companycd, billstatus, fromtype, shstate, reasonid, begindate, enddate, order, pageindex, pagesize, ref recordCount);
        }

        public static DataTable GetPurchaseRejectSetUp(string companycd, string billStatus, string FromType, int shState, string begindate, string enddate, int timeType)
        {
            return PurchaseRejectDBHelper.GetPurchaseRejectSetUp(companycd, billStatus, FromType, shState, begindate, enddate, timeType);
        }

        public static DataTable GetPurchaseRejectSetUpDetails(string companycd, string billstatus, string fromtype, int shstate, string begindate, string enddate, string order, int pageindex, int pagesize, int timeType, string timestr, ref int recordCount)
        {
            return PurchaseRejectDBHelper.GetPurchaseRejectSetUpDetails(companycd, billstatus, fromtype, shstate, begindate, enddate, order, pageindex, pagesize, timeType, timestr, ref recordCount);
        }

        /// <summary>
        /// 退货物品分布
        /// </summary>
        public static DataTable GetRejectByProduct(string ProviderID, string DeptID, string BeginDate, string EndDate, string StatType)
        {
            return PurchaseRejectDBHelper.GetRejectByProduct(ProviderID, DeptID, BeginDate, EndDate, StatType);
        }

        /// <summary>
        /// 退货物品走势
        /// </summary>
        public static DataTable GetRejectByProductTrend(string ProviderID, string DeptID, string BeginDate, string EndDate, string DateType, string StatType, string ProductID)
        {
            return PurchaseRejectDBHelper.GetRejectByProductTrend(ProviderID, DeptID, BeginDate, EndDate, DateType, StatType, ProductID);
        }

        public static DataTable GetPurchaseRejectProductDetail(string ProviderID, string DeptID, string BeginDate, string EndDate, string DateType, string StatType, string ProductID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return PurchaseRejectDBHelper.GetPurchaseRejectProductDetail(ProviderID, DeptID, BeginDate, EndDate, DateType, StatType, ProductID, pageIndex, pageCount, ord, ref TotalCount);
        }
    }
}
