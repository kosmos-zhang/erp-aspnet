/**********************************************
 * 描述：     内部转账业务处理
 * 建立人：   莫申林
 * 建立时间： 2009/04/28
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
    public class InSideChangeAccoBus
    {


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
             * 但还是考虑将异常日志的变量定义放在catch里面
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
            logSys.ModuleID = ConstUtil.MODULE_ID_INSIDECHANGEACCO_ADD;
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
        private static LogInfoModel InitLogInfo(string wcno)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_INSIDECHANGEACCO_ADD;


            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;
            return logModel;
        }
        #endregion


        /// <summary>
        /// 添加内部转账信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool InSertInsideChangeAccoInfo(InSideChangeAccoModel model, out int ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool suuc = InSideChangeAccoDBHelper.InSertInsideChangeAccoInfo(model,out ID);


                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                LogInfoModel logModel = InitLogInfo(ID.ToString());
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_INSIDECHANGEACCO;
                if (suuc)
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;

                LogDBHelper.InsertLog(logModel);

                return suuc;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex);
                throw ex;
            }
        }

        /// <summary>
        /// 修改内部转账信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateInsideChangeAccoInfo(InSideChangeAccoModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool suuc = InSideChangeAccoDBHelper.UpdateInsideChangeAccoInfo(model);


                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                LogInfoModel logModel = InitLogInfo(model.ID.ToString());
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_INSIDECHANGEACCO;
                if (suuc)
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;

                LogDBHelper.InsertLog(logModel);

                return suuc;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex);
                throw ex;
            }
        }
        /// <summary>
       /// 根据查询条件查询部转账单信息
       /// </summary>
       /// <param name="queryStr"></param>
       /// <returns></returns>
       public static DataTable GetInsideChangeAccoInfo(string queryStr)
        {
            try
            {
                return InSideChangeAccoDBHelper.GetInsideChangeAccoInfo(queryStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       /// <summary>
       /// 根据查询条件查询部转账单信息
       /// </summary>
       /// <param name="queryStr"></param>
       /// <returns></returns>
       public static DataTable GetInsideChangeAccoInfo(string queryStr, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
       {
           try
           {
               return InSideChangeAccoDBHelper.GetInsideChangeAccoInfo(queryStr,pageIndex,pageSize,OrderBy,ref totalCount);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }



       /// <summary>
       /// 判断内部转账单是否能删除，返回提示
       /// </summary>
       /// <param name="ids">内部转账单ID集</param>
       /// <returns></returns>
       public static string IsCanDel(string ids)
       {
            try
            {
                return InSideChangeAccoDBHelper.IsCanDel(ids);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       /// <summary>
       /// 内部转账单确认
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool Autidt(string ids)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool suuc = InSideChangeAccoDBHelper.Autidt(ids);
                string[] idsStr = ids.Split(',');
                for (int i = 0; i < idsStr.Length; i++)
                {
                    string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    LogInfoModel logModel = InitLogInfo(idsStr[i].ToString());
                    logModel.ModuleID = ConstUtil.MODULE_ID_INSIDECHANGEACCO_LIST;

                    logModel.Element = ConstUtil.LOG_PROCESS_CONFIRM;

                    logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_INSIDECHANGEACCO;
                    if (suuc)
                        logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                    else
                        logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;

                    LogDBHelper.InsertLog(logModel);
                }

                return suuc;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex);
                throw ex;
            }
        }


       /// <summary>
       /// 内部转账单反确认
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool AntiAutidt(string ids)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool suuc = InSideChangeAccoDBHelper.AntiAutidt(ids);
                string[] idsStr = ids.Split(',');
                for (int i = 0; i < idsStr.Length; i++)
                {

                    string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    LogInfoModel logModel = InitLogInfo(idsStr[i].ToString());
                    logModel.ModuleID = ConstUtil.MODULE_ID_INSIDECHANGEACCO_LIST;

                    logModel.Element = ConstUtil.LOG_PROCESS_ANTIAUDIT;

                    logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_INSIDECHANGEACCO;
                    if (suuc)
                        logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                    else
                        logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;

                    LogDBHelper.InsertLog(logModel);
                }

                return suuc;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex);
                throw ex;
            }
        }


       /// <summary>
       /// 内部转账单登记凭证状态
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool AccountInSideChangeAcco(string ids, int AttestBillID)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           try
           {
               bool suuc = InSideChangeAccoDBHelper.AccountInSideChangeAcco(ids,AttestBillID);
               string[] idsStr = ids.Split(',');
               for (int i = 0; i < idsStr.Length; i++)
               {

                   string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                   LogInfoModel logModel = InitLogInfo(idsStr[i].ToString());
                   logModel.ModuleID = ConstUtil.MODULE_ID_INSIDECHANGEACCO_LIST;

                   logModel.Element = ConstUtil.LOG_PROCESS_ACCOUNT;

                   logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_INSIDECHANGEACCO;
                   if (suuc)
                       logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                   else
                       logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;

                   LogDBHelper.InsertLog(logModel);
               }

               return suuc;
           }
           catch (Exception ex)
           {
               WriteSystemLog(userInfo, ex);
               throw ex;
           }
       }

        /// <summary>
       /// 删除内部转账单信息
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool DeleteInSideChangeAcco(string ids)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           try
           {
               bool suuc = InSideChangeAccoDBHelper.DeleteInSideChangeAcco(ids);
               string[] idsStr = ids.Split(',');
               for (int i = 0; i < idsStr.Length; i++)
               {

                   string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                   LogInfoModel logModel = InitLogInfo(idsStr[i].ToString());
                   logModel.ModuleID = ConstUtil.MODULE_ID_INSIDECHANGEACCO_LIST;

                   logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                   logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_INSIDECHANGEACCO;
                   if (suuc)
                       logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                   else
                       logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;

                   LogDBHelper.InsertLog(logModel);
               }

               return suuc;
           }
           catch (Exception ex)
           {
               WriteSystemLog(userInfo, ex);
               throw ex;
           }
       }

        /// <summary>
       /// 是否允许确认
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static string IsCanAudit(string ids)
       {
           try
           {
               return InSideChangeAccoDBHelper.IsCanAudit(ids);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
       /// 是否反允许确认
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static string IsCanAntiAudit(string ids)
       {
           try
           {
               return InSideChangeAccoDBHelper.IsCanAntiAudit(ids);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

        /// <summary>
       /// 判断内部转帐单单据编码是否重复
       /// </summary>
       /// <param name="ChangeNo">转账单编码</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <param name="ID">单据主键</param>
       /// <returns>bool true OR false</returns>
       public static bool IsDiffInsideNo(string ChangeNo, string ID)
       {
           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               return InSideChangeAccoDBHelper.IsDiffInsideNo(ChangeNo, CompanyCD, ID);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

    }    
}