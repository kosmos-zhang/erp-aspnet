/***********************************************
 * 类作用：   采购管理事务层处理               *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/04/26                       *
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

namespace XBase.Business.Office.PurchaseManager
{
    // <summary>
    /// 类名：ProviderInfoBus
    /// 描述：采购管理事务层处理
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/26
    /// </summary>
    public class ProviderInfoBus
    {
        #region 绑定采购供应商类别
        /// <summary>
        /// 绑定采购供应商类别
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetdrpCustType()
        {
            DataTable dt = ProviderInfoDBHelper.GetdrpCustType();
            return dt;
        }
        #endregion

        #region 绑定采购供应商分类
        /// <summary>
        /// 绑定采购供应商分类
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetdrpCustClass()
        {
            DataTable dt = ProviderInfoDBHelper.GetdrpCustClass();
            return dt;
        }
        #endregion

        #region 绑定采购供应商优质级别
        /// <summary>
        /// 绑定采购供应商优质级别
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetdrpCreditGrade()
        {
            DataTable dt = ProviderInfoDBHelper.GetdrpCreditGrade();
            return dt;
        }
        #endregion

        #region 绑定采购供应商所在区域
        /// <summary>
        /// 绑定采购供应商所在区域
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetdrpAreaID()
        {
            DataTable dt = ProviderInfoDBHelper.GetdrpAreaID();
            return dt;
        }
        #endregion

        #region 绑定采购供应商联络期限
        /// <summary>
        /// 绑定采购供应商联络期限
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetdrpLinkCycle()
        {
            DataTable dt = ProviderInfoDBHelper.GetdrpLinkCycle();
            return dt;
        }
        #endregion

        #region 绑定国家
        /// <summary>
        /// 绑定采购交货方式
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetdrpCountryID()
        {
            DataTable dt = ProviderInfoDBHelper.GetdrpCountryID();
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
            DataTable dt = ProviderInfoDBHelper.GetDrpTakeType();
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
            DataTable dt = ProviderInfoDBHelper.GetDrpCarryType();
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
            DataTable dt = ProviderInfoDBHelper.GetDrpPayType();
            return dt;
        }
        #endregion

        //#region 绑定采购支付方式
        ///// <summary>
        ///// 绑定采购结算方式
        ///// </summary>
        ///// <returns>DataTable</returns>

        //public static DataTable GetDrpMoneyType()
        //{
        //    DataTable dt = ProviderInfoDBHelper.GetDrpMoneyType();
        //    return dt;
        //}
        //#endregion

        #region 绑定采购供应商币种
        /// <summary>
        /// 绑定采购供应商币种
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetdrpCurrencyType()
        {
            DataTable dt = ProviderInfoDBHelper.GetdrpCurrencyType();
            return dt;
        }
        #endregion


        #region 新建供应商档案
        public static bool InsertProviderInfo(ProviderInfoModel model, out string ID)
        {
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.CustNo);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                //设置模块ID 模块ID请在ConstUtil中定义，以便维护
                logModel.ModuleID = ConstUtil.MODULE_ID_PROVIDERINFO_ADD;
                succ = ProviderInfoDBHelper.InsertProviderInfo(model, out ID);
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

        #region 更新供应商档案
        //public static bool UpdateProviderInfo(ProviderInfoModel model, string no)
        public static bool UpdateProviderInfo(ProviderInfoModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            if (model.ID <= 0)
            {
                return false;
            }
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.CustNo);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                //设置模块ID 模块ID请在ConstUtil中定义，以便维护
                logModel.ModuleID = ConstUtil.MODULE_ID_PROVIDERINFO_ADD;
                succ = ProviderInfoDBHelper.UpdateProviderInfo(model);
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


        #region 查询采购供应商档案列表所需数据
        public static DataTable SelectProviderInfo(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string CustNo, string CustName, string CustNam, string PYShort, string CustType, string CustClass, string AreaID, string CreditGrade, string Manager, string StartCreateDate, string EndCreateDate)
        {
            try
            {
                return ProviderInfoDBHelper.SelectProviderInfoList(pageIndex, pageCount, orderBy, ref TotalCount, CustNo, CustName, CustNam, PYShort, CustType, CustClass, AreaID, CreditGrade, Manager, StartCreateDate, EndCreateDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除采购供应商相关信息
        public static bool DeleteProviderInfoAll(string CustNo, string CompanyCD)
        {
            LogInfoModel logModel = InitLogInfo(CustNo);
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_PROVIDERINFOINFO;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //string[] sql = new string[3];
            //int index = 0;
            SqlCommand[] cmdList = new SqlCommand[3];
         cmdList [0]=   ProviderInfoDBHelper.DeleteProviderInfo(CustNo,CompanyCD);//供应商信息
         cmdList[1] = ProviderInfoDBHelper.DeleteProviderLinkMan(CustNo, CompanyCD);//供应商联系人
         cmdList[2] = ProviderInfoDBHelper.DeleteProviderProduct(CustNo, CompanyCD);//供应商产品

         SqlHelper.ExecuteTransForList(cmdList );
            bool isSucc = SqlHelper.Result.OprateCount > 0 ? true : false;
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

        #region  
        public static DataTable GetProviderInfo(ProviderInfoModel ProviderInfoM, int pageIndex, int pageCount, string OrderBy, out int totalCount)
        {
            try
            {
                return ProviderInfoDBHelper.GetProviderInfo(ProviderInfoM,pageIndex,pageCount,OrderBy,out totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public static DataTable GetProviderSelect(ProviderInfoModel ProviderInfoM)
        {
            return ProviderInfoDBHelper.GetProviderSelect(ProviderInfoM);
        }
        #endregion

        #region 获取当前企业的供应商信息
        /// <summary>
        /// 获取当前企业的供应商信息 Add by jiangym
        /// </summary>
        /// <param name="CompanyCD">企业编码</param>
        /// <returns>DataTable</returns>
        public static DataTable GetProviderInfo()
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            if (string.IsNullOrEmpty(CompanyCD)) return null;
            try
            {
                return ProviderInfoDBHelper.GetProviderInfo(CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 先判断采购订单中有没有符合条件的供应商的相关信息
        public static bool isValue(int ID)
        {
            try
            {
                return ProviderInfoDBHelper.isValue(ID);
            }
            catch 
            {
                throw;
            }
        }
        #endregion

        #region 先判断采购退货中有没有符合条件的供应商的相关信息
        public static bool isValues(int ID)
        {
            try
            {
                return ProviderInfoDBHelper.isValues(ID);
            }
            catch 
            {
                throw;
            }
        }
        #endregion

        #region 获取单个供应商档案订单中有符合条件数据
        public static DataTable SelectProviderInfo(int ID)
        {
            try
            {
                return ProviderInfoDBHelper.SelectProviderInfo(ID);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 获取单个供应商档案订单中没有符合条件数据
        public static DataTable SelectProviderInfo2(int ID)
        {
            try
            {
                return ProviderInfoDBHelper.SelectProviderInfo2(ID);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 供应商分类
        public static DataTable GetProviderClass()
        {
            try
            {
                return ProviderInfoDBHelper.GetProviderClass();
            }
            catch 
            {
                return null;
                throw;
            }
        }
        #endregion

        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <returns></returns>
        private static LogInfoModel InitLogInfo(string CustNo)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            ////设置模块ID 模块ID请在ConstUtil中定义，以便维护
            //logModel.ModuleID = ConstUtil.MODULE_ID_PROVIDERINFO_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PROVIDERINFO;
            //操作对象
            logModel.ObjectID = CustNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;
        }
        #endregion

        #region 采购报表供应商统计表
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseProviderCount(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProviderID, string StartOrderDate, string EndOrderDate)
        {
            try
            {
                return ProviderInfoDBHelper.PurchaseProviderCount(pageIndex, pageCount, orderBy, ref TotalCount, ProviderID, StartOrderDate, EndOrderDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购报表供应商统计表打印
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseProviderCountPrint(string ProviderID, string StartOrderDate, string EndOrderDate, string CompanyCD, string orderBy)
        {
            try
            {
                return ProviderInfoDBHelper.PurchaseProviderCountPrint(ProviderID, StartOrderDate, EndOrderDate, CompanyCD, orderBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购报表供应商应付款查询
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseProviderPayment(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProviderID, string ProviderName, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            try
            {
                return ProviderInfoDBHelper.PurchaseProviderPayment(pageIndex, pageCount, orderBy, ref TotalCount, ProviderID, ProviderName, StartConfirmDate, EndConfirmDate, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购报表供应商应付款查询打印
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseProviderPaymentPrint(string ProviderID, string ProviderName, string StartConfirmDate, string EndConfirmDate, string CompanyCD, string orderBy)
        {
            try
            {
                return ProviderInfoDBHelper.PurchaseProviderPaymentPrint(ProviderID, ProviderName, StartConfirmDate, EndConfirmDate, CompanyCD, orderBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购报表供应商应台帐查询
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseProviderAccount(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string ProviderID, string ProviderName, string StartConfirmDate, string EndConfirmDate, string CompanyCD)
        {
            try
            {
                return ProviderInfoDBHelper.PurchaseProviderAccount(pageIndex, pageCount, orderBy, ref TotalCount, ProviderID, ProviderName, StartConfirmDate, EndConfirmDate, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购报表供应商台帐查询打印
        /// <summary>
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable PurchaseProviderAccountPrint(string ProviderID, string ProviderName, string StartConfirmDate, string EndConfirmDate, string CompanyCD, string orderBy)
        {
            try
            {
                return ProviderInfoDBHelper.PurchaseProviderAccountPrint(ProviderID, ProviderName, StartConfirmDate, EndConfirmDate, CompanyCD, orderBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
