/**********************************************
 * 描述：     开票业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/04/17
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using System.Data.SqlClient;
using XBase.Common;

using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;

namespace XBase.Business.Office.FinanceManager
{
    public class BillingBus
    {


        #region 输出系统日志
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">异常信息</param>
        private static void WriteSystemLog(UserInfoUtil userInfo, int ModuleType, Exception ex)
        {
            /* 
             * 出现异常时，输出系统日志到文本文件 
             * 考虑出现异常情况比较少，尽管一个方法可能多次异常，
             *      但还是考虑将异常日志的变量定义放在catch里面
             */
            //定义变量
            LogInfo logSys = new LogInfo();
            //设置日志类型 需要指定为系统日志
            logSys.Type = LogInfo.LogType.SYSTEM;
            //指定系统日志类型 出错信息
            logSys.SystemKind = LogInfo.SystemLogKind.SYSTEM_ERROR;
            //指定登陆用户信息
            logSys.UserInfo = userInfo;
            //设定模块ID
            if (ModuleType == 0)
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_BILLING_ADD;
            }
            else
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_BILLING_LIST;
            }
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion

        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <returns></returns>
        private static LogInfoModel InitLogInfo(string wcno, int ModuleType)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            if (ModuleType == 0)
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_BILLING_ADD;
            }
            else
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_BILLING_LIST;
            }
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_BILLING;
            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;
            //操作时间
            logModel.OperateDate = DateTime.Now;
            return logModel;
        }
        #endregion

        #region 判断开票号是否存在
        public static bool BillingNumIsexist(string BillingNum)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                return BillingDBHelper.BillingNumIsexist(CompanyCD, BillingNum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 更新是否登记凭证状态
        /// <summary>
       /// 更新是否登记凭证状态
       /// </summary>
       /// <param name="ID">主键</param>
       /// <returns>true 成功，false失败</returns>
        public static bool UpdateVoucherStatus(string ID)
        {
            try
            {
                return BillingDBHelper.UpdateVoucherStatus(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据公司编码获取开票信息
        /// <summary>
         /// 根据公司编码获取开票信息
         /// </summary>
         /// <returns>DataTable</returns>
        public static DataTable GetBillingInfoByCompanyCD()
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return BillingDBHelper.GetBillingInfoByCompanyCD(CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据主键获取业务单信息
        /// <summary>
        /// 根据主键获取业务单信息
        /// </summary>
        /// <param name="ID">主键ID</param>
       /// <returns>DataTable</returns>
        public static DataTable GetBillingInfoByID(string ID)
        {
            try
            {
                return BillingDBHelper.GetBillingInfoByID(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据主键获取业务单信息_打印
        /// <summary>
        /// 根据主键获取业务单信息
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetBillingInfoByIDPrint(string ID)
        {
            try
            {
                return BillingDBHelper.GetBillingInfoByIDPrint(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  审核开票
        /// <summary>
        /// 审核开票信息
        /// </summary>
        /// <param name="ID">主键</param>
        /// <param name="Auditor">审核人</param>
        /// <param name="AuditDate">审核时间</param>
        /// <returns>true审核成功，false审核失败</returns>
        public static bool AuditBilling(string ID)
        {
      
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            string Auditor = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            DateTime AuditDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

            bool isSucc = false;
            try
            {


                isSucc = BillingDBHelper.AuditBilling(ID, Auditor, AuditDate);
                //定义变量
                string remark;
                //成功时
                if (isSucc)
                {
                    //设置操作成功标识
                    remark = ConstUtil.LOG_PROCESS_SUCCESS;
                }
                else
                {
                    //设置操作成功标识 
                    remark = ConstUtil.LOG_PROCESS_FAILED;
                }
                string[] noList = ID.Split(',');
                for (int i = 0; i < noList.Length; i++)
                {
                    //获取编号
                    string no = noList[i];
                    //替换两边的 '
                    no = no.Replace("'", string.Empty);
                    //操作日志
                    LogInfoModel logModel = InitLogInfo(no, 1);
                    //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                    logModel.Element = ConstUtil.LOG_PROCESS_CONFIRM;
                    //设置操作成功标识
                    logModel.Remark = remark;
                    //登陆日志
                    LogDBHelper.InsertLog(logModel);
                }
                return isSucc;

            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, 1, ex);
                return false;
            }

        }
        #endregion

        #region 反审批开票
        /// <summary>
        /// 反审批开票信息
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public static bool ReverseAuditBilling(string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            bool isSucc = false;
            try
            {
                isSucc = BillingDBHelper.ReverseAuditBilling(ID);
                //定义变量
                string remark;
                //成功时
                if (isSucc)
                {
                    //设置操作成功标识
                    remark = ConstUtil.LOG_PROCESS_SUCCESS;
                }
                else
                {
                    //设置操作成功标识 
                    remark = ConstUtil.LOG_PROCESS_FAILED;
                }
                string[] noList = ID.Split(',');
                for (int i = 0; i < noList.Length; i++)
                {
                    //获取编号
                    string no = noList[i];
                    //替换两边的 '
                    no = no.Replace("'", string.Empty);
                    //操作日志
                    LogInfoModel logModel = InitLogInfo(no, 1);
                    //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                    logModel.Element = ConstUtil.LOG_PROCESS_ANTIAUDIT;
                    //设置操作成功标识
                    logModel.Remark = remark;
                    //登陆日志
                    LogDBHelper.InsertLog(logModel);
                }
                return isSucc;

            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, 1, ex);
                return false;
            }
        }
        #endregion

        #region 删除发票信息
        /// <summary>
       /// 删除发票信息
       /// </summary>
       /// <param name="ID">主键</param>
       /// <returns>true成功，false</returns>
        public static bool DeleteBilling(string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            bool isSucc = false;
            try
            {
  

                isSucc = BillingDBHelper.DeleteBilling(ID);
                //定义变量
                string remark;
                //成功时
                if (isSucc)
                {
                    //设置操作成功标识
                    remark = ConstUtil.LOG_PROCESS_SUCCESS;
                }
                else
                {
                    //设置操作成功标识 
                    remark = ConstUtil.LOG_PROCESS_FAILED;
                }
                string[] noList = ID.Split(',');
                for (int i = 0; i < noList.Length; i++)
                {
                    //获取编号
                    string no = noList[i];
                    //替换两边的 '
                    no = no.Replace("'", string.Empty);
                    //操作日志
                    LogInfoModel logModel = InitLogInfo(no,1);
                    //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                    logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                    //设置操作成功标识
                    logModel.Remark = remark;
                    //登陆日志
                    LogDBHelper.InsertLog(logModel);
                }
                return isSucc; 

            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo,1,ex);
                return false;
            }
        }
        #endregion

        #region 检索销售退货单信息
        public static DataTable SellBackInfo(string BackNo, string Title,
             string CustName, string StartDate, string EndDate)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return BillingDBHelper.SellBackInfo(CompanyCD, BackNo, Title, CustName, StartDate, EndDate);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 检索销售退货单信息 add by moshenlin 
        public static DataTable SellBackInfo(string BackNo, string Title,
             string CustName, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return BillingDBHelper.SellBackInfo(CompanyCD, BackNo, Title, CustName, StartDate, EndDate,pageIndex,pageSize,OrderBy,ref totalCount);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



       #region 检索采购到货通知单  add by moshenlin 2010-06-21
       public static DataTable GetPurchaseIncomeInfo(string ids)
       {
           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               return BillingDBHelper.SellBackInfo(ids,CompanyCD);

           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
       #endregion

       #region 检索销售发货通知单  add by moshenlin 2010-06-21
       public static DataTable GetSellSendInfo(string ids)
       {
           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               return BillingDBHelper.GetSellSendInfo(ids,CompanyCD);

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion


       /// <summary>
       /// 根据检索条件检索出满足条件的信息 Added By moshenlin 2009-06-29
       /// </summary>
       /// <param name="ids"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable SellBackInfo(string ids)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return BillingDBHelper.SellBackInfo(ids,CompanyCD);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region 检索代销结算单
        public static DataTable GetSellChannelSttlInfo(string SttlNo, string Title,
             string CustName, string StartDate, string EndDate)
        {

            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return BillingDBHelper.GetSellChannelSttlInfo(CompanyCD, SttlNo, Title, CustName, StartDate, EndDate);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
       /// 根据检索条件检索出满足条件的信息 Added By moshenlin 2009-06-29
       /// </summary>
       /// <param name="ids"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable GetSellChannelSttlInfo(string ids)
        {

            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return BillingDBHelper.GetSellChannelSttlInfo(ids,CompanyCD);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


       #region 检索代销结算单  add by moshenlin 
       public static DataTable GetSellChannelSttlInfo(string SttlNo, string Title,
          string CustName, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
       {

           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               return BillingDBHelper.GetSellChannelSttlInfo(CompanyCD, SttlNo, Title, CustName, StartDate, EndDate);

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion


       #region 检索采购退货单信息
       public static DataTable GetPurchaseRejectInfo(string RejectNo, string Title,
             string CustName, string StartDate, string EndDate)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return BillingDBHelper.GetPurchaseRejectInfo(CompanyCD,RejectNo,Title,CustName,StartDate,EndDate);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       #endregion


       #region 检索采购退货单信息  add by moshenlin 
       public static DataTable GetPurchaseRejectInfo(string RejectNo, string Title,
             string CustName, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
       {
           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               return BillingDBHelper.GetPurchaseRejectInfo(CompanyCD, RejectNo, Title, CustName, StartDate, EndDate,pageIndex,pageSize,OrderBy,ref totalCount);

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

        #region 检索采购退货单信息
        public static DataTable GetPurchaseRejectInfo(string ids)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return BillingDBHelper.GetPurchaseRejectInfo(ids,CompanyCD);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion





        #region 检索采购到货通知单  add by moshenlin
        public static DataTable GetPurchaseIncomeInfo(string CodeNo, string Title,
           string CustName, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {

            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return BillingDBHelper.GetPurchaseIncomeInfo(CompanyCD, CodeNo, Title, CustName, StartDate, EndDate, pageIndex, pageSize, OrderBy, ref totalCount);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 检索销售发货通知单  add by moshenlin
        public static DataTable GetSellSendInfo(string CodeNo, string Title,
           string CustName, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {

            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return BillingDBHelper.GetSellSendInfo(CompanyCD, CodeNo, Title, CustName, StartDate, EndDate, pageIndex, pageSize, OrderBy, ref totalCount);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 根据检索条件检索出满足条件的信息
        /// <summary>
       /// 根据检索条件检索出满足条件的信息
       /// </summary>
       /// <param name="CompanyCD">公司编码</param>
       /// <param name="OrderCD">订单编码</param>
       /// <param name="BillingNum">订单号</param>
       /// <param name="BillingType">开票类型</param>
       /// <param name="AccountsStatus">结算状态</param>
       /// <param name="StartDate">开始日期</param>
       /// <param name="EndDate">结束日期</param>
       /// <returns>DataTable</returns>
        public static DataTable SearchBillingInfo(string OrderCD, string BillingNum, string BillingType,
          string AccountsStatus, string StartDate, string EndDate, string AuditStatus, string IsVoucher, string ContactUnits,
              int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try 
            {

                return BillingDBHelper.SearchBillingInfo(CompanyCD,OrderCD,BillingNum,BillingType,AccountsStatus,StartDate,EndDate,
                    AuditStatus, IsVoucher, ContactUnits, pageIndex, pageSize, OrderBy, ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据主键获取业务单总金额
        /// <summary>
        /// 根据主键获取业务单总金额
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>decimal</returns>
        public static decimal GetBillingTotalAccounts(string ID)
        {
            try
            {
                return BillingDBHelper.GetBillingTotalAccounts(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据主键获取业务单未付金额
        /// <summary>
       /// 根据主键获取业务单未付金额
       /// </summary>
       /// <param name="ID">主键ID</param>
       /// <returns>decimal</returns>
        public static decimal GetBillingNAccounts(string ID)
        {
            try
            {
                return BillingDBHelper.GetBillingNAccounts(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 更新销售退货单建单状态
        public static bool UpdateSellBackIsOpen(string ID)
        {
            try
            {
                return BillingDBHelper.UpdateSellBackIsOpen(ID);
            }
            catch
            {
                return false;
            }
        }
        #endregion


        #region 更新代销结算单建单状态
        public static bool UpdateSellChannelSttlIsOpen(string ID)
        {
            try
            {
                return BillingDBHelper.UpdateSellChannelSttlIsOpen(ID);
            }
            catch
            {
                return false;
            }
        }
        #endregion


        #region 更新采购退货单单建单状态
        public static bool UpdatePurchaseRejectIsOpen(string ID)
        {
            try
            {
                return BillingDBHelper.UpdatePurchaseRejectIsOpen(ID);
            }
            catch
            {
                return false;
            }
        }
        #endregion



        #region 根据主键获取业务单已付金额
        /// <summary>
        /// 根据主键获取业务单已付金额
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>decimal</returns>
        public static decimal GetBillingYAccounts(string ID)
        {
            return BillingDBHelper.GetBillingYAccounts(ID);
        }
        #endregion

        #region 添加开票信息
        /// <summary>
        /// 添加开票信息
        /// </summary>
        /// <param name="model">开票实体</param>
        /// <returns>true 成功，false失败</returns>
        public static bool InsertBillingInfo(BillingModel model, out int ID)
        {
            ID = 0;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            if (model.CompanyCD == null) model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {

                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.BillingNum, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

                succ =  BillingDBHelper.InsertBillingInfo(model, out ID);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;

 
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, 0, ex);

                return false;
            }
        }
        #endregion

        #region 修改开票信息
        /// <summary>
         /// 修改开票信息
         /// </summary>
         /// <param name="model">开票实体</param>
        /// <returns>true 成功，false失败</returns>
        public static bool UpdateBillingInfo(BillingModel model)
        {
            if (model == null) return false;

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
               
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.BillingNum, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

                succ = BillingDBHelper.UpdateBillingInfo(model);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;

            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, 0, ex);
                return false;
            }
        }
        #endregion




        #region 判断销售订单的销售发货单是否建立业务单
        public static string CheckChooseInfo(string SourceID, string BillCD)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {

                return BillingDBHelper.CheckChooseInfo(SourceID,BillCD,CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 判断采购订单的采购到货单是否建立业务单
        public static string CheckChooseSellSendInfo(string SourceID, string BillCD)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {

                return BillingDBHelper.CheckChooseSellSendInfo(SourceID, BillCD, CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 判断采购到货单对应的源单采购订单是否建立业务单
        public static string CheckChoosePucharArriveInfo(string SourceID, string BillCD)
        {
            try
            {

                return BillingDBHelper.CheckChoosePucharArriveInfo(SourceID, BillCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 判断销售发货单对应的源单销售订单是否建立业务单
        public static string CheckChooseSellInfo(string SourceID, string BillCD)
        {
            try
            {

                return BillingDBHelper.CheckChooseSellInfo(SourceID, BillCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
