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
   public  class PerformanceBetterBus
    {
       public static DataSet GetRectPlanInfoWithID(string planNo)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //获取公司代码
         string   companyCD = userInfo.CompanyCD;
         return PerformanceBetterDBHelper.GetRectPlanInfoWithID(companyCD, planNo);
       }
       public static DataTable SearchPlanInfo(PerformanceBetterModel model)
       {
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //获取公司代码
           model.CompanyCD = userInfo.CompanyCD;
           //执行查询

           return PerformanceBetterDBHelper.SearchBetterInfo (model);

       }
       public static bool SaveBetterInfo(PerformanceBetterModel  model, IList<PerformanceBetterDetailModel > modleSummaryList)
       {
           //获取登陆用户信息
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

           //定义返回变量

           bool isSucc = false;
           //操作日志
           LogInfoModel logModel = InitLogInfo(model.PlanNo );

           try
           {
               logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
               //执行更新操作

               ///  isSucc = PerformanceTaskDBHelper.UpdatePerTaskInfo(model, modellist, modleSummaryList);
               if (model.EditFlag == ConstUtil.EDIT_FLAG_INSERT)
               {

                   if (PerformanceBetterDBHelper.InsertBetterInfo(model))
                   {
                       if (PerformanceBetterDBHelper.InsertBetterDetaiInfo(modleSummaryList))
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
               else
               {
                   if (PerformanceBetterDBHelper.UpdateBetterInfobyPlanNo(model))
                   {
                       if (PerformanceBetterDBHelper.DeleteBetterDetatilInfobyPlanNo(model.PlanNo, model.CompanyCD))
                       {
                           if (PerformanceBetterDBHelper.InsertBetterDetaiInfo(modleSummaryList))
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
                   else
                   {
                       isSucc = false;
                   }
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
           logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_PERFORMANCEBETTER;
           //设置操作日志类型 修改
           logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PERFORMANCBETTER;
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
           logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_PERFORMANCEBETTER;
           //描述
           logSys.Description = ex.ToString();

           //输出日志
           LogUtil.WriteLog(logSys);
       }
       #endregion
       public static bool DeletePlanInfo(IList <PerformanceBetterModel > model)
       {

           //执行删除操作
           bool isSucc = PerformanceBetterDBHelper.DeleteBetterInfo(model);
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

           //操作日志
           LogInfoModel logModel = InitLogInfo(model [0].PlanNo );
           //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
           logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
           //设置操作成功标识
           logModel.Remark = remark;

           //登陆日志
           LogDBHelper.InsertLog(logModel);

           return isSucc;
       }
    }
}
