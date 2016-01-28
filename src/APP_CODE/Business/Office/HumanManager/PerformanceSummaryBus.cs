using System;
using XBase.Model.Office.HumanManager;
using XBase.Data.Office.HumanManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;
using System.Collections.Generic;

namespace XBase.Business.Office.HumanManager
{
   public  class PerformanceSummaryBus
    {
       public static DataTable SearchTaskInfo(PerformanceTaskModel model)
       {
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //获取公司代码
           model.CompanyCD = userInfo.CompanyCD;
           //执行查询

           return PerformanceSummaryDBHelper.SearchTaskInfo(model);

       }
       public static DataTable SearchSurmmaryCheckInfo(PerformanceTaskModel model)
       {
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //获取公司代码
           model.CompanyCD = userInfo.CompanyCD;
           //执行查询

           return PerformanceSummaryDBHelper.SearchSurmmaryCheckInfo(model);

       }
       public static DataTable SearchSurmmaryInfo(PerformanceTaskModel model)
       {
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //获取公司代码
           model.CompanyCD = userInfo.CompanyCD;
           //执行查询

           return PerformanceSummaryDBHelper.SearchSurmmaryInfo(model);

       }

       public static DataTable CheckSummary(string taskNo)
       {
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //获取公司代码
           string companyCD = userInfo.CompanyCD;
           //执行查询
           return PerformanceSummaryDBHelper.CheckSummary (companyCD, taskNo);

       }

       public static DataTable SearchTaskInfoByTaskNO(string taskNo,string tempalteNo, string  EmployeId)
       {
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //获取公司代码
           string companyCD = userInfo.CompanyCD;
           //执行查询
           return PerformanceSummaryDBHelper.SearchTaskInfoByTaskNO(companyCD, taskNo,tempalteNo, EmployeId);

       }
       public static DataTable SearchTaskInfoByTaskNO(string taskNo )
       {
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //获取公司代码
           string companyCD = userInfo.CompanyCD;
           //执行查询
           return PerformanceSummaryDBHelper.SearchTaskInfoByTaskNO(companyCD, taskNo);

       }
       public static DataTable GetSurmarryInfoByTaskNO(string taskNo)
       {
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //获取公司代码
           string companyCD = userInfo.CompanyCD;
           //执行查询
           return PerformanceSummaryDBHelper.GetSurmarryInfoByTaskNO(companyCD, taskNo);

       }
       public static DataTable GetSurmarryInfoByTaskNOEmployeeID(string taskNo, string employeeID, string templateNo)
       {
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //获取公司代码
           string companyCD = userInfo.CompanyCD;
           //执行查询
           return PerformanceSummaryDBHelper.GetSurmarryInfoByTaskNOEmployeeID(companyCD, taskNo,employeeID ,templateNo );

       }
       public static bool GatherInfo(PerformanceTaskModel model, IList<PerformanceSummaryModel> modleSummaryList)
       {
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
         
           //定义返回变量

           bool isSucc = false;
           //操作日志
           LogInfoModel logModel = InitLogInfo(model.TaskNo);

               try
               {
                   logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                   //执行更新操作
                   
                 ///  isSucc = PerformanceTaskDBHelper.UpdatePerTaskInfo(model, modellist, modleSummaryList);

                   if (PerformanceSummaryDBHelper.UpdateTaskStatusInfo(model))
                   {
                       if (PerformanceSummaryDBHelper.UpdatePerSummaryInfo(modleSummaryList))
                       {
                           isSucc = true;
                       }
                       else
                       {
                           isSucc = false;
                       }
                   }
                   else
                   {
                       isSucc = false;
                   }




               }
               catch (Exception ex)
               {
                   //输出系统日志
                   WriteSystemLog(userInfo, ex);
               }
          

           //更新成功时
           if (isSucc)
           {
               //设置操作成功标识
               logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
           }
           //更新不成功
           else
           {
               //设置操作成功标识 
               logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
           }

           //登陆日志
           LogDBHelper.InsertLog(logModel);

           return isSucc;
       }
       /// <summary>
       /// 初始化日志
       /// </summary>
       /// <param name="no"></param>
       /// <returns></returns>
       private static LogInfoModel InitLogInfo(string no)
       {
           LogInfoModel logModel = new LogInfoModel();
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //设置公司代码
           logModel.CompanyCD = userInfo.CompanyCD;
           //设置登陆用户ID
           logModel.UserID = userInfo.UserID;
           //设置模块ID
           logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_PERFORMANCESUMMARY;
           //设置操作日志类型 修改
           logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PERFORMANCSUMMARY;
           //操作单据编号
           logModel.ObjectID = no;

           return logModel;

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
           logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_PERFORMANCESUMMARY;
           //描述
           logSys.Description = ex.ToString();

           //输出日志
           LogUtil.WriteLog(logSys);
       }
       #endregion
       public static bool GatherSummaryInfo(PerformanceTaskModel model, PerformanceSummaryModel modleSummaryList,bool sign)
       {
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

           //定义返回变量

           bool isSucc = false;
           //操作日志
           LogInfoModel logModel = InitLogInfo(modleSummaryList .TaskNo );

           try
           {
               logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
               //执行更新操作

               ///  isSucc = PerformanceTaskDBHelper.UpdatePerTaskInfo(model, modellist, modleSummaryList);

               if (PerformanceSummaryDBHelper.UpdatePerSummaryInfobyCheck(modleSummaryList))
               {
                   if (sign)
                   {
                       if (PerformanceSummaryDBHelper.UpdateTaskStatusInfo(model))
                       {
                           isSucc = true;
                       }
                       else
                       {
                           isSucc = false;
                       }
                   }
                   else
                   {
                       isSucc = true;
                   }
               }
               else
               {
                   isSucc = false;
               }




           }
           catch (Exception ex)
           {
               //输出系统日志
               WriteSystemLog(userInfo, ex);
           }


           //更新成功时
           if (isSucc)
           {
               //设置操作成功标识
               logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
           }
           //更新不成功
           else
           {
               //设置操作成功标识 
               logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
           }

           //登陆日志
           LogDBHelper.InsertLog(logModel);

           return isSucc;
       }
       public static bool GatherSummaryCheckInfo(PerformanceSummaryModel model)
       {
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

           //定义返回变量

           bool isSucc = false;
           //操作日志
           LogInfoModel logModel = InitLogInfo(model.TaskNo );

           try
           {
               logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
               //执行更新操作

               ///  isSucc = PerformanceTaskDBHelper.UpdatePerTaskInfo(model, modellist, modleSummaryList);

               if (PerformanceSummaryDBHelper.UpdateGatherSummaryCheckInfo(model))
               {
                   isSucc = true;
               }
               else
               {
                   isSucc = false;
               }




           }
           catch (Exception ex)
           {
               //输出系统日志
               WriteSystemLog(userInfo, ex);
           }


           //更新成功时
           if (isSucc)
           {
               //设置操作成功标识
               logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
           }
           //更新不成功
           else
           {
               //设置操作成功标识 
               logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
           }

           //登陆日志
           LogDBHelper.InsertLog(logModel);

           return isSucc;
       }
       public static bool UpdateTaskStatusInfo(PerformanceTaskModel model)
       {
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

           //定义返回变量

           bool isSucc = false;
           //操作日志
           LogInfoModel logModel = InitLogInfo(model.TaskNo);

           try
           {
               logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
               //执行更新操作

               ///  isSucc = PerformanceTaskDBHelper.UpdatePerTaskInfo(model, modellist, modleSummaryList);

           
                 
                       if (PerformanceSummaryDBHelper.UpdateTaskStatusInfo(model))
                       {
                           isSucc = true;
                       }
                       else
                       {
                           isSucc = false;
                       }
                   
             




           }
           catch (Exception ex)
           {
               //输出系统日志
               WriteSystemLog(userInfo, ex);
           }


           //更新成功时
           if (isSucc)
           {
               //设置操作成功标识
               logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
           }
           //更新不成功
           else
           {
               //设置操作成功标识 
               logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
           }

           //登陆日志
           LogDBHelper.InsertLog(logModel);

           return isSucc;
       }
    }
}
