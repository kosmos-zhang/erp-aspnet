/**********************************************
 * 描述：     付款单业务处理
 * 建立人：   莫申林
 * 建立时间： 2009/04/25
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
   public class PayBillBus
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
            logSys.ModuleID = ConstUtil.MODULE_ID_PAYBILL_ADD;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_PAYBILL_ADD;


            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;
            return logModel;
        }
        #endregion



       /// <summary>
       /// 添加付款单信息
       /// </summary>
       /// <param name="Model"></param>
       /// <param name="ID"></param>
       /// <returns></returns>
       public static bool InSertIntoPayBillInfo(PayBillModel model, out int IntID)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           try
           {
               bool suuc = PayBillDBHelper.InSertIntoPayBillInfo(model, out IntID);

              
               string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
               LogInfoModel logModel = InitLogInfo(IntID.ToString());
               logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
               //设置操作日志类型 修改
               logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PAYBILL;
               if (suuc)
                   logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
               else
                   logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;

               LogDBHelper.InsertLog(logModel);

               LogInfoModel logModell = InitLogInfo(model.BillingID.ToString());
               logModell.Element = ConstUtil.LOG_PROCESS_UPDATE;
               logModell.ObjectName = ConstUtil.CODING_RULE_TABLE_BILLING;

               if (suuc)
                   logModell.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
               else
                   logModell.Remark = ConstUtil.LOG_PROCESS_FAILED;
               LogDBHelper.InsertLog(logModell);

               return suuc;
           }
           catch (Exception ex)
           {
               WriteSystemLog(userInfo, ex);
               throw ex;
           }
       }

       /// <summary>
       /// 根据查询条件获取付款单表信息
       /// </summary>
       /// <param name="queryStr"></param>
       /// <returns></returns>
       public static DataTable GetPayBillInfo(string queryStr, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
       {
           try
           {
               return PayBillDBHelper.GetPayBillInfo(queryStr,pageIndex,pageSize,OrderBy,ref totalCount);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       /// <summary>
       /// 根据查询条件获取付款单表信息
       /// </summary>
       /// <param name="queryStr"></param>
       /// <returns></returns>
       public static DataTable GetPayBillInfo(string queryStr)
       {
           try
           {
               return PayBillDBHelper.GetPayBillInfo(queryStr);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       /// <summary>
       /// 判断付款单是否确认
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
       public static bool GetStatus(int ID)
       {
           try
           {
               return PayBillDBHelper.GetStatus(ID);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       /// <summary>
       /// 更新付款单信息
       /// </summary>
       /// <param name="model">实体</param>
       /// <param name="DiffAmount">本次输入的付款金额与修改前的付款金额之差</param>
       /// <returns></returns>
       public static bool UpdatePayBill(PayBillModel model, decimal DiffAmount)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           try
           {

               bool suuc =PayBillDBHelper.UpdatePayBill(model,DiffAmount);

               
               string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
               LogInfoModel logModel = InitLogInfo(model.ID.ToString());
               logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
               //设置操作日志类型 修改
               logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PAYBILL;
               if (suuc)
                   logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
               else
                   logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;

               LogDBHelper.InsertLog(logModel);

               LogInfoModel logModell = InitLogInfo(model.BillingID.ToString());
               logModell.Element = ConstUtil.LOG_PROCESS_UPDATE;
               logModell.ObjectName = ConstUtil.CODING_RULE_TABLE_BILLING;

               if (suuc)
                   logModell.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
               else
                   logModell.Remark = ConstUtil.LOG_PROCESS_FAILED;
               LogDBHelper.InsertLog(logModell);


               return suuc;
           }
           catch (Exception ex)
           {
               WriteSystemLog(userInfo, ex);
               throw ex;
           }
       }

       /// <summary>
       /// 删除付款单信息级联更新对应的业务单的已付金额和未付金额
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool DeletePayBill(string ids)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           try
           {

               bool suuc = PayBillDBHelper.DeletePayBill(ids);
               string[] idsStr = ids.Split(',');
               for (int i = 0; i < idsStr.Length; i++)
               {
                   
                   string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                   LogInfoModel logModel = InitLogInfo(idsStr[i].ToString());
                   logModel.ModuleID = ConstUtil.MODULE_ID_PAYBILL_LIST;

                   logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                   logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PAYBILL;
                   if (suuc)
                       logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                   else
                       logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;

                   LogDBHelper.InsertLog(logModel);

                   LogInfoModel logModell = InitLogInfo("");
                   logModell.ModuleID = ConstUtil.MODULE_ID_PAYBILL_LIST;
                   logModell.Element = ConstUtil.LOG_PROCESS_UPDATE;
                   logModell.ObjectName = ConstUtil.CODING_RULE_TABLE_BILLING;

                   if (suuc)
                       logModell.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                   else
                       logModell.Remark = ConstUtil.LOG_PROCESS_FAILED;
                   LogDBHelper.InsertLog(logModell);
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
       /// 付款单确认
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool Autidt(string ids)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           try
           {
               
               string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
               string [] idsStr=ids.Split(',');
               bool suuc=PayBillDBHelper.Autidt(ids);
               for (int i = 0; i < idsStr.Length; i++)
               {
                   LogInfoModel logModel = InitLogInfo(idsStr[i].ToString());
                   logModel.ModuleID = ConstUtil.MODULE_ID_PAYBILL_LIST;

                   logModel.Element = ConstUtil.LOG_PROCESS_CONFIRM;
                   logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PAYBILL;
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
       /// 判断付款单是否能删除，返回提示
       /// </summary>
       /// <param name="ids">付款单ID集</param>
       /// <returns></returns>
       public static string IsCanDel(string ids)
       {
           try
           {
               return PayBillDBHelper.IsCanDel(ids);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       /// <summary>
       /// 付款单反确认
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool AntiAutidt(string ids)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           try
           {
               
               string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
               string[] idsStr = ids.Split(',');
               bool suuc = PayBillDBHelper.AntiAutidt(ids);
               for (int i = 0; i < idsStr.Length; i++)
               {
                   LogInfoModel logModel = InitLogInfo(idsStr[i].ToString());
                   logModel.ModuleID = ConstUtil.MODULE_ID_PAYBILL_LIST;

                   logModel.Element = ConstUtil.LOG_PROCESS_ANTIAUDIT;
                   logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PAYBILL;
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
       /// 付款单登记凭证状态
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool AccountPayBill(string ids,int AttestBillID)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           try
           {
               
               string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
               string[] idsStr = ids.Split(',');
               bool suuc = PayBillDBHelper.AccountPayBill(ids,AttestBillID);
               for (int i = 0; i < idsStr.Length; i++)
               {
                   LogInfoModel logModel = InitLogInfo(idsStr[i].ToString());
                   logModel.ModuleID = ConstUtil.MODULE_ID_PAYBILL_LIST;

                   logModel.Element = ConstUtil.LOG_PROCESS_ACCOUNT;
                   logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PAYBILL;
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
               return PayBillDBHelper.IsCanAudit(ids);
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
               return PayBillDBHelper.IsCanAntiAudit(ids);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }



       /// <summary>
       /// 判断付款单单据编码是否重复
       /// </summary>
       /// <param name="ChangeNo">付款单编码</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <param name="ID">单据主键</param>
       /// <returns>bool true OR false</returns>
       public static bool IsDiffInsideNo(string PayNo, string ID)
       {
           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               return PayBillDBHelper.IsDiffInsideNo(PayNo, CompanyCD, ID);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

    }
}
