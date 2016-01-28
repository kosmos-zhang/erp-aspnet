using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Office.CustManager;
using XBase.Model.Office.CustManager;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Common;
using XBase.Data.Common;
using System.Collections;
using XBase.Business.Office.SystemManager;
namespace XBase.Business.Office.CustManager
{
    public class CustAdviceBus
    {
        /// <summary>
        /// 添加客户建议
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddCustAdvice(CustAdviceModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {

                bool result = false;
                LogInfoModel logModel = InitLogInfo(model.AdviceNo);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                result = CustAdvice.AddCustAdvice(model);
                if (!result)
                {
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                }
                else
                {
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                    model.ID = IDIdentityUtil.GetIDIdentity("officedba.CustAdvice");
                }
                LogDBHelper.InsertLog(logModel);
                return result;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex);
                return false;
            }

        }
        /// <summary>
        /// 修改建议
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpCustAdvice(CustAdviceModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {

                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.AdviceNo);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                succ = CustAdvice.UpCustAdvice(model);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                    model.ID = IDIdentityUtil.GetIDIdentity("officedba.CustAdvice");
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex);
                return false;
            }          
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetAllCustAdvice(string CanUserID, CustAdviceModel model, int pageIndex, int pageSize, string OrderBy, string BeginTime, string EndTime, string CompanyCD, ref int TotalCount)
        {
            return CustAdvice.GetCustAdvice(CanUserID, model, pageIndex, pageSize, OrderBy, BeginTime, EndTime, CompanyCD, ref TotalCount);
        }
        /// <summary>
        /// 获取一个单据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetOneCustAdvice(CustAdviceModel model)
        {
            return CustAdvice.GetOneCustAdvice(model);
        }
        /// <summary>
        /// 单据打印需要
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetOneCustAdviceInfo(CustAdviceModel model)
        {
            return CustAdvice.GetOneCustAdviceInfo(model);
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="model"></param>
        /// <param name="OrderBy"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public static DataTable GetAllCustAdvice(string CanUserID,CustAdviceModel model, string OrderBy, string BeginTime, string EndTime)
        {
            return CustAdvice.GetCustAdvice(CanUserID,model, OrderBy, BeginTime, EndTime);
        }
        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool DelCust(string ID)
        {
            return CustAdvice.DelCust(ID);
        }

        #region 输出系统日志
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">异常信息</param>
        private static void WriteSystemLog(UserInfoUtil userInfo, Exception ex)
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
            logSys.ModuleID = ConstUtil.MODULE_ID_CustAdvice_ADD;
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
        private static LogInfoModel InitLogInfo(string prodno)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_CustAdvice_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.MODULE_CODING_RULE_TABLE_CustAdvice;
            //操作对象
            logModel.ObjectID = prodno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion
    }
}
