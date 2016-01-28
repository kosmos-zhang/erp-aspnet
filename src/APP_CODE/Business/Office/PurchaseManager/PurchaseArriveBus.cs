/***********************************************
 * 类作用：   采购管理事务层处理               *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/04/16                       *
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
    /// 创建时间：2009/04/16
    /// </summary>
    public class PurchaseArriveBus
    {
        #region 绑定采购类别
        /// <summary>
        /// 绑定采购类别
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetddlTypeID()
        {
            DataTable dt = PurchaseArriveDBHelper.GetddlTypeID();
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

        #region 绑定币种
        /// <summary>
        /// 绑定采购币种
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetDrpCurrencyType()
        {
            DataTable dt = PurchaseArriveDBHelper.GetDrpCurrencyType();
            return dt;
        }
        #endregion

        #region 新建到货通知单
        public static bool InsertPurchaseArrive(PurchaseArriveModel model, string DetailProductID, string DetailProductNo, string DetailProductName, string DetailUnitID, string DetailProductCount, string DetailRequireDate, string DetailUnitPrice, string DetailTaxPrice, string DetailDiscount, string DetailTaxRate, string DetailTotalPrice, string DetailTotalFee, string DetailTotalTax, string DetailRemark, string DetailFromBillID, string DetailFromLineNo, string DetailUsedUnitCount, string DetailUsedUnitID, string DetailUsedPrice, string length, out string ID, Hashtable ht)
        {
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.ArriveNo);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                //设置模块ID 模块ID请在ConstUtil中定义，以便维护
                logModel.ModuleID = ConstUtil.MODULE_ID_PurchaseArrive_Add;
                succ = PurchaseArriveDBHelper.InsertPurchaseArrive(model, DetailProductID, DetailProductNo, DetailProductName, DetailUnitID, DetailProductCount, DetailRequireDate, DetailUnitPrice, DetailTaxPrice, DetailDiscount, DetailTaxRate, DetailTotalPrice, DetailTotalFee, DetailTotalTax, DetailRemark, DetailFromBillID, DetailFromLineNo, DetailUsedUnitCount, DetailUsedUnitID, DetailUsedPrice, length, out ID, ht);
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

        #region 更新到货通知单
        public static bool UpdatePurchaseArrive(PurchaseArriveModel model, string DetailProductID, string DetailProductNo, string DetailProductName, string DetailUnitID, string DetailProductCount, string DetailRequireDate, string DetailUnitPrice, string DetailTaxPrice, string DetailDiscount, string DetailTaxRate, string DetailTotalPrice, string DetailTotalFee, string DetailTotalTax, string DetailRemark, string DetailFromBillID, string DetailFromBillNo, string DetailFromLineNo, string DetailUsedUnitCount, string DetailUsedUnitID, string DetailUsedPrice, string length, string fflag2, string no, Hashtable ht)
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
                logModel.ModuleID = ConstUtil.MODULE_ID_PurchaseArrive_Add;
                succ = PurchaseArriveDBHelper.UpdatePurchaseArrive(model, DetailProductID, DetailProductNo, DetailProductName, DetailUnitID, DetailProductCount, DetailRequireDate, DetailUnitPrice, DetailTaxPrice, DetailDiscount, DetailTaxRate, DetailTotalPrice, DetailTotalFee, DetailTotalTax, DetailRemark, DetailFromBillID, DetailFromBillNo, DetailFromLineNo, DetailUsedUnitCount, DetailUsedUnitID, DetailUsedPrice, length, fflag2, no, ht);
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

        #region 确认到货通知
        public static bool ConfirmPurchaseArrive(PurchaseArriveModel Model, string DetailProductCount, string DetailFromBillNo, string DetailFromLineNo, string length, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            try
            {
                isSucc = PurchaseArriveDBHelper.ConfirmPurchaseArrive(Model, DetailProductCount, DetailFromBillNo, DetailFromLineNo, length, out strMsg);
                //确认成功后调用“自动生成凭证”方法
                if (isSucc == true)
                {
                    string str = "";
                    XBase.Business.Office.FinanceManager.AutoVoucherBus.AutoVoucherInsert(12, Model.TotalFee, "officedba.PurchaseArrive," + Model.ID, Model.CurrencyType + "," + Model.Rate, Model.ProviderID, out str);
                    //returnValue=0 业务单未设凭证模板,returnValue=1 企业不启用业务单自动生成凭证,returnValue = 2 企业不启用自动生成凭证自动登帐, returnValue = 3 自动生成凭证失败 ，returnValue = "4" 回写业务单登记凭证状态成功，returnValue = "5" 回写业务单登记凭证状态失败
                    strMsg += str;
                }
            }
            catch (Exception e)
            {
                strMsg += e.Message;
            }
            return isSucc;
        }
        #endregion

        #region 取消确认到货通知
        public static bool CancelConfirmPurchaseArrive(PurchaseArriveModel Model, string DetailProductCount, string DetailFromBillNo, string DetailFromLineNo, string length, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            try
            {
                isSucc = PurchaseArriveDBHelper.CancelConfirmPurchaseArrive(Model, DetailProductCount, DetailFromBillNo, DetailFromLineNo, length, out strMsg);
                //确认成功后调用“自动生成凭证”方法
                if (isSucc == true)
                {
                    string str = "";
                    XBase.Business.Office.FinanceManager.AutoVoucherBus.AntiConfirmVoucher("officedba.PurchaseArrive," + Model.ID, out str);
                    strMsg += str;
                }
            }
            catch (Exception e)
            {
                strMsg += e.Message;
            }
            return isSucc;
        }
        #endregion

        #region 结单到货通知
        public static bool ClosePurchaseArrive(PurchaseArriveModel Model)
        {
            return PurchaseArriveDBHelper.ClosePurchaseArrive(Model);
        }
        #endregion

        #region 取消结单到货通知
        public static bool CancelClosePurchaseArrive(PurchaseArriveModel Model)
        {
            return PurchaseArriveDBHelper.CancelClosePurchaseArrive(Model);
        }
        #endregion

        #region 查询采购到货通知列表所需数据
        public static DataTable SelectPurchaseArrive(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ArriveNo, string Title, string TypeID, string Purchaser, string FromType, string ProviderID, string BillStatus, string UsedStatus, string ProjectID, string EFIndex, string EFDesc)
        {
            try
            {
                return PurchaseArriveDBHelper.SelectArriveList(pageIndex, pageCount, orderBy, ref TotalCount, ArriveNo, Title, TypeID, Purchaser, FromType, ProviderID, BillStatus, UsedStatus, ProjectID, EFIndex, EFDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 选择采购订单源单数据
        public static DataTable GetPurchaseOrderDetail(string ProductNo, string ProductName, string FromBillNo, string CompanyCD, int ProviderID, int CurrencyTypeID, string Rate, string PurchaseArriveEFIndex, string PurchaseArriveEFDesc)
        {
            try
            {
                return PurchaseArriveDBHelper.GetPurchaseOrderDetail(ProductNo, ProductName, FromBillNo, CompanyCD, ProviderID, CurrencyTypeID, Rate, PurchaseArriveEFIndex, PurchaseArriveEFDesc);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 删除采购到货通知
        public static bool DeletePurchaseArriveAll(string DetailNo)
        {
            LogInfoModel logModel = InitLogInfo(DetailNo);
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_PurchaseArriveInfo;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string CompanyCD = userInfo.CompanyCD;

            //string[] sql = new string[2];
            //int index = 0;
            //PurchaseArriveDBHelper.DeletePurchasePlanPrimary(ArriveNo, ref sql, index++);
            //PurchaseArriveDBHelper.DeletePurchasePlanDetail(ArriveNo, ref sql, index++);

            //SqlHelper.ExecuteTransForListWithSQL(sql);
            //bool isSucc = SqlHelper.Result.OprateCount > 0 ? true : false;

            bool isSucc = PurchaseArriveDBHelper.DeletePurchasePlanPrimary(DetailNo);

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

        #region 获取单个到货记录
        public static DataTable SelectArrive(int ID)
        {
            try
            {
                return PurchaseArriveDBHelper.SelectArrive(ID);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 获取到货明细
        public static DataTable Details(int ID)
        {
            try
            {
                return PurchaseArriveDBHelper.Details(ID);
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
        private static LogInfoModel InitLogInfo(string ArriveNo)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            ////设置模块ID 模块ID请在ConstUtil中定义，以便维护
            //logModel.ModuleID = ConstUtil.MODULE_ID_PurchaseArrive_Add;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PURCHASEARRIVE;
            //操作对象
            logModel.ObjectID = ArriveNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;
        }
        #endregion

        #region 确定单据有没有被引用
        public static bool IsCitePurchaseArrive(int ID)
        {
            try
            {
                return PurchaseArriveDBHelper.IsCitePurchaseArrive(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购报表到货汇总查询
        /// <summary>
        /// 采购报表到货汇总查询(包括打印功能)
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="orderBy"></param>
        /// <param name="TotalCount"></param>
        /// <param name="ProviderID"></param>
        /// <param name="ProductID"></param>
        /// <param name="StartConfirmDate"></param>
        /// <param name="EndConfirmDate"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="isDate"></param>
        /// <param name="isPrint"></param>
        /// <returns></returns>
        public static DataTable PurchaseArriveCollect(int pageIndex, int pageCount, string orderBy
            , ref int TotalCount, string ProviderID, string ProductID, string StartConfirmDate
            , string EndConfirmDate, string CompanyCD, bool isDate, bool isPrint)
        {
            return PurchaseArriveDBHelper.PurchaseArriveCollect(pageIndex, pageCount, orderBy
                                    , ref TotalCount, ProviderID, ProductID
                                    , StartConfirmDate, EndConfirmDate, CompanyCD, isDate, isPrint);
        }


        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseArriveCollect(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProviderID, string ProductID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            try
            {
                return PurchaseArriveDBHelper.PurchaseArriveCollect(pageIndex, pageCount, orderBy, ref TotalCount, ProviderID, ProductID, StartConfirmDate, EndConfirmDate, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static DataTable PurchaseOrderCollectList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProviderID, string ProductID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            try
            {
                return PurchaseArriveDBHelper.PurchaseOrderCollectList(pageIndex, pageCount, orderBy, ref TotalCount, ProviderID, ProductID, StartConfirmDate, EndConfirmDate, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable PurchaseArriveCollectList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProviderID, string ProductID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            try
            {
                return PurchaseArriveDBHelper.PurchaseArriveCollectList(pageIndex, pageCount, orderBy, ref TotalCount, ProviderID, ProductID, StartConfirmDate, EndConfirmDate, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购报表到货汇总查询打印
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseArriveCollectPrint(string ProviderID, string ProductID, string StartConfirmDate, string EndConfirmDate, string CompanyCD, string orderBy)
        {
            try
            {
                return PurchaseArriveDBHelper.PurchaseArriveCollectPrint(ProviderID, ProductID, StartConfirmDate, EndConfirmDate, CompanyCD, orderBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable PurchaseOrderCollectListPrint(string ProviderID, string ProductID, string StartConfirmDate, string EndConfirmDate, string CompanyCD, string orderBy)
        {
            try
            {
                return PurchaseArriveDBHelper.PurchaseOrderCollectListPrint(ProviderID, ProductID, StartConfirmDate, EndConfirmDate, CompanyCD, orderBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable PurchaseArriveCollectListPrint(string ProviderID, string ProductID, string StartConfirmDate, string EndConfirmDate, string CompanyCD, string orderBy)
        {
            try
            {
                return PurchaseArriveDBHelper.PurchaseArriveCollectListPrint(ProviderID, ProductID, StartConfirmDate, EndConfirmDate, CompanyCD, orderBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion

        #region 采购报表采购到货查询
        public static DataTable PurchaseArriveQuery(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProviderID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            try
            {
                return PurchaseArriveDBHelper.PurchaseArriveQuery(pageIndex, pageCount, orderBy, ref TotalCount, ProviderID, StartConfirmDate, EndConfirmDate, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购报表采购到货查询打印
        public static DataTable PurchaseArriveQueryPrint(string ProviderID, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            try
            {
                return PurchaseArriveDBHelper.PurchaseArriveQueryPrint(ProviderID, StartConfirmDate, EndConfirmDate, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
