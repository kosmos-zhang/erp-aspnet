/**********************************************
 * 描述：     收款单业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/04/13
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
 

namespace XBase.Business.Office.FinanceManager
{
    public class IncomeBillBus
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
                logSys.ModuleID = ConstUtil.MODULE_ID_INCOMEBILL_ADD;
            }
            else
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_INCOMEBILL_LIST;
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
                logModel.ModuleID = ConstUtil.MODULE_ID_INCOMEBILL_ADD;
            }
            else
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_INCOMEBILL_LIST;
            }
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_INCOMEBILL;
            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;
            //操作时间
            logModel.OperateDate = DateTime.Now;
            return logModel;
        }
        #endregion

        #region 判断收款单号是否已存在
        public static bool IncomeNoisExist(string IncomeNo)
        {
            try
            {
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                return IncomeBillDBHelper.IncomeNoisExist(CompanyCD,IncomeNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region 更新收款单登记凭证状态
        /// <summary>
        /// 根据主键更新收款单登记凭证状态
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>true 成功,false 失败</returns>
        public static bool UpdateAccountStatus(string ID)
        {
            int Accountor=((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            try
            {
                return IncomeBillDBHelper.UpdateAccountStatus(ID, Accountor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据主键获取收款单信息
        /// <summary>
        /// 根据主键获取收款单信息
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetIncomeBillInfoByID(string ID)
        {
            if (string.IsNullOrEmpty(ID)) return null;
            try
            {
                return IncomeBillDBHelper.GetIncomeBillInfoByID(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 添加收款单信息
        /// <summary>
        /// 添加收款单信息
        /// </summary>
        /// <param name="model">收款单实体</param>
        /// <returns>true 成功，false失败</returns>
        public static bool InsertIncomeBill(IncomeBillModel model, out int ID)
        {
            ID = 0;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
           

            try
            {

                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.InComeNo, 0);

                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

                succ = IncomeBillDBHelper.InsertIncomeBill(model, out ID);
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

        #region 修改收款单信息
        /// <summary>
        /// 修改收款单信息
        /// </summary>
        /// <param name="model">收款单实体</param>
        /// <returns>true 成功，false失败</returns>
        public static bool UpdateIncomeBill(IncomeBillModel model,decimal OldPrice)
        {
            if (model == null) return false;
            model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
   

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {

                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.InComeNo, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

                succ = IncomeBillDBHelper.UpdateIncomeBill(model, OldPrice);
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

        #region 检索收款单信息
        /// <summary>
        /// 根据检索条件检索收款单信息
        /// </summary>
        /// <param name="IncomeBillNo">收款单号</param>
        /// <param name="AcceWay">收款方式</param>
        /// <param name="TotalPrice">收款金额</param>
        /// <param name="ConfirmStatus">确认状态</param>
        /// <param name="IsVoucher">是否登记凭证</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns>DataTable</returns>
        public static DataTable SearchIncomeInfo(string ProjectID,string IncomeBillNo, string AcceWay, string TotalPrice
            , string ConfirmStatus, string IsVoucher, string StartDate, string EndDate,
            int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return IncomeBillDBHelper.SearchIncomeInfo(ProjectID,CompanyCD, IncomeBillNo, AcceWay,
                    TotalPrice, ConfirmStatus, IsVoucher, StartDate, EndDate, pageIndex, pageSize,OrderBy, ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除收款单
        /// <summary>
        /// 删除收款单
        /// </summary>
        /// <returns>true 成功，false失败</returns>
        public static bool DeleteIncomeBill(string ID, string BillingID, string Price)
        {
       
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            bool isSucc = false;
            string SetBillingID = string.Empty;
            try
            {
                isSucc = IncomeBillDBHelper.DeleteIncomeBill(ID, BillingID, Price);
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
                    logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                    //设置操作成功标识
                    logModel.Remark = remark;
                    //登陆日志
                    LogDBHelper.InsertLog(logModel);
                }

                if (isSucc)
                {
                    DataTable dt = BillingDBHelper.GetBillingYAccountsDT(BillingID);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            if (Convert.ToDecimal(rows["YAccounts"]) == 0)
                            {
                                SetBillingID += rows["ID"].ToString() + ",";
                            }
                        }
                    }
                    //如果存在已付金额为0则更新结算状态
                    if (SetBillingID.Length > 0)
                    {
                        SetBillingID = SetBillingID.TrimEnd(new char[] { ',' });
                        isSucc = BillingDBHelper.UpdateAccountStatusByID(SetBillingID, "0");
                    }

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




        /// <summary>
        /// 删除付款单信息级联更新对应的业务单的已付金额和未付金额
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteInComeBillInfo(string ids)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {

                bool suuc = IncomeBillDBHelper.DeleteInComeBillInfo(ids);
              
                return suuc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region  确认收款单
        /// <summary>
        /// 确认收款单
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="Confirmor">确认人</param>
        /// <returns>true成功,false失败</returns>
        public static bool ConfirmIncomeBill(string ID)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            bool isSucc = false;
            try
            {

                int Confirmor = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;

                isSucc = IncomeBillDBHelper.ConfirmIncomeBill(ID, Confirmor.ToString());
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

        #region 反确认收款单
        /// <summary>
        /// 反确认收款单
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="Confirmor">确认人</param>
        /// <returns>true成功,false失败</returns>
        public static bool ReConfirmIncomeBill(string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            bool isSucc = false;
            try
            {
                isSucc = IncomeBillDBHelper.ReConfirmIncomeBill(ID);
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
    }
}
