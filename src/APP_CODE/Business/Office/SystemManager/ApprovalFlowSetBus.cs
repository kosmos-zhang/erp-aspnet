using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.Office.SystemManager;
using XBase.Model.Office.SystemManager;
using System.Data;
using System.Data.SqlClient;
using XBase.Data;
using XBase.Data.DBHelper;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
namespace XBase.Business.Office.SystemManager
{
   public class ApprovalFlowSetBus
    {
        /// <summary>
        ///流程信息插入
        /// </summary>
        /// <param name="model">流程信息</param>
        /// <returns>插入成功与否</returns>
       public static bool InsertFlowInfo(FlowModel model, string StepNo,string StepName,string StepActor)
        {
            if (model == null)
                return false ;
            if (model.FlowNo=="")
            {
                return false;
            }
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.FlowNo);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;

                succ = ApprovalFlowSetDBHelper.InsertFlowInfo(model,StepNo,StepName,StepActor);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex);
                return false;
            }





            //try
            //{
            //    StringBuilder sqlflow = new StringBuilder();
            //    sqlflow.AppendLine("INSERT INTO officedba.Flow");
            //    sqlflow.AppendLine("		(CompanyCD      ");
            //    sqlflow.AppendLine("		,DeptID         ");
            //    sqlflow.AppendLine("		,FlowNo         ");
            //    sqlflow.AppendLine("		,FlowName         ");
            //    sqlflow.AppendLine("		,BillTypeFlag         ");
            //    sqlflow.AppendLine("		,BillTypeCode         ");
            //    sqlflow.AppendLine("		,UsedStatus         ");
            //    sqlflow.AppendLine("		,ModifiedDate         ");
            //    sqlflow.AppendLine("		,ModifiedUserID)        ");
            //    sqlflow.AppendLine("VALUES                  ");
            //    sqlflow.AppendLine("		('"+model.CompanyCD+"',     ");
            //    sqlflow.AppendLine("		'" + model.DeptID + "',     ");
            //    sqlflow.AppendLine("		'" + model.FlowNo + "',     ");
            //    sqlflow.AppendLine("		'" + model.FlowName + "',     ");
            //    sqlflow.AppendLine("		'" + model.BillTypeFlag + "',     ");
            //    sqlflow.AppendLine("		'" + model.BillTypeCode + "',     ");
            //    sqlflow.AppendLine("		'" + model.UsedStatus + "'     ");
            //    sqlflow.AppendLine("		,getdate()     ");
            //    sqlflow.AppendLine("		,'" +model.ModifiedUserID+ "' )       ");
            //    #region 流程步骤SQL语句拼写
            //    string[] sql = null;
            //    try
            //    {
            //        #region 工艺明细添加SQL语句
            //        if (!string.IsNullOrEmpty(StepName))
            //        {
            //            int sql_index = 0;
            //            string[] Stepno = StepNo.Split(',');
            //            string[] Stepname = StepName.Split(',');
            //            string[] Stepactor = StepActor.Split(',');
            //            sql = new string[Stepname.Length + 1];

            //            if (Stepname.Length >= 1)
            //            {
            //                for (int i = 0; i < Stepname.Length; i++)
            //                {
            //                    System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
            //                    cmdsql.AppendLine(" Insert into  officedba.FlowStepActor(CompanyCD,FlowNo,StepNo,StepName,Actor,ModifiedDate,ModifiedUserID )");
            //                    cmdsql.AppendLine(" Values('" + model.CompanyCD + "','" + model.FlowNo + "'," + Stepno[i] + ",'" + Stepname[i].ToString() + "','" + Stepactor[i].ToString() + "','" + System.DateTime.Now + "','" + model.ModifiedUserID + "')");
            //                    sql[sql_index] = cmdsql.ToString();
            //                    sql_index++;
            //                }
            //            }

            //            sql[Stepname.Length] = sqlflow.ToString();
            //        #endregion

            //            if (ApprovalFlowSetDBHelper.InsertFlow(sql))
            //            {
            //                result = true;
            //            }
            //        }
            //        else
            //        {
            //            sql = new string[1];
            //            sql[0] = sqlflow.ToString();
            //            if (ApprovalFlowSetDBHelper.InsertFlow(sql))
            //            {
            //                result = true;
            //            }
            //        }
            //        return result;
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //    #endregion
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
           
        }
       /// <summary>
       /// 修改审批流程
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static bool UpdateFlowInfo(FlowModel model, string StepNo, string StepName, string StepActor)
       {
           if (model == null)
               return false;
           if (model.FlowNo == "")
           {
               return false;
           }
           //try
           //{
           //    ApprovalFlowSetDBHelper.DelFlow(model.CompanyCD, Convert.ToString(model.FlowNo));
           //    ApprovalFlowSetDBHelper.DelFlowstep(model.CompanyCD, Convert.ToString(model.FlowNo));
           //    return ApprovalFlowSetDBHelper.InsertFlowInfo(model, StepNo, StepName, StepActor);
           //}
           //catch (Exception)
           //{
           //    return false;
           //}



           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //登陆日志
           try
           {
               bool succ = false;
               LogInfoModel logModel = InitLogInfo(model.FlowNo);
               logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
               ApprovalFlowSetDBHelper.DelFlow(model.CompanyCD, Convert.ToString(model.FlowNo));
               ApprovalFlowSetDBHelper.DelFlowstep(model.CompanyCD, Convert.ToString(model.FlowNo));
               succ = ApprovalFlowSetDBHelper.InsertFlowInfo(model, StepNo, StepName, StepActor);
               if (!succ)
                   logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
               else
                   logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
               LogDBHelper.InsertLog(logModel);
               return succ;
           }
           catch (Exception ex)
           {
               WriteSystemLog(userInfo, ex);
               throw;
           }


       }
       /// <summary>
       /// 根据树节点获取单据信息
       /// </summary>
       /// <param name="BillTypeFlag"></param>
       /// <param name="BillTypeCode"></param>
       /// <returns></returns>
       public static DataTable GetFlowInfo(string BillTypeFlag, string BillTypeCode, string UseStatus, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
       {
           if (string.IsNullOrEmpty(BillTypeCode) || string.IsNullOrEmpty(BillTypeFlag))
               return null;
           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               //string CompanyCD="AAAAAA";
               return ApprovalFlowSetDBHelper.GetFlowInfo(BillTypeFlag, BillTypeCode, CompanyCD, UseStatus, pageIndex, pageCount, OrderBy, ref totalCount);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       /// <summary>
       /// 根据查询条件返回流程信息
       /// </summary>
       /// <param name="FlowName"></param>
       /// <param name="DeptID"></param>
       /// <param name="UsedStatus"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       //public static DataTable GetFlowInfoByConditions(string FlowName, string DeptID, string UsedStatus)
       //{
       //    try
       //    {
       //        string CompanyCD = "AAAAAA";
       //        return ApprovalFlowSetDBHelper.GetFlowInfoByConditions(FlowName, DeptID, UsedStatus,CompanyCD);
       //    }
       //    catch (Exception ex)
       //    {
       //        throw ex;
       //    }
       //}
       /// <summary>
       /// 删除流程步骤信息
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
       public static bool DeleteFlowStepInfo(string ID)
       {
           bool succ = false;
           string allID = "";
           System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] sql =new string[2];
            string[] IdS = null;
            ID = ID.Substring(0, ID.Length);
            IdS = ID.Split(',');

            for (int i = 0; i < IdS.Length; i++)
            {
                IdS[i] = "'" + IdS[i] + "'";
                sb.Append(IdS[i]);
            }
            allID = sb.ToString().Replace("''", "','");
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           if (string.IsNullOrEmpty(ID))
               return false;
           try
           {
               sql[0] = "delete from officedba.Flow where CompanyCD='" + CompanyCD + "' and FlowNo IN (" + allID + ") and UsedStatus='0'";
               sql[1] = "delete from officedba.FlowStepActor where CompanyCD='" + CompanyCD + "' and FlowNo IN (" + allID + ")";
               succ= ApprovalFlowSetDBHelper.DeleteFlow(sql); 
               LogInfoModel logModel = InitLogInfo(allID);
                   //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                   logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
               if (succ)
               {                  
                   //设置操作成功标识
                   logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                   //登陆日志
               }
               else
               {
                   logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
               }
               LogDBHelper.InsertLog(logModel);
               return succ;
               
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       /// <summary>
       /// 获取流程步骤表
       /// </summary>
       /// <param name="FlowNo"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable GetFlowStepInfoByFlowId(string FlowNo, string CompanyCD)
       {
           if (string.IsNullOrEmpty(FlowNo))
               return null;
           try
           {
               return ApprovalFlowSetDBHelper.GetFlowStepInfoByFlowId(FlowNo, CompanyCD);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       /// <summary>
       /// 获取流程信息
       /// </summary>
       /// <param name="FlowNo"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable GetFlowInfoByFlowId(string FlowNo, string CompanyCD)
       {
           if (string.IsNullOrEmpty(FlowNo))
               return null;
           try
           {
               return ApprovalFlowSetDBHelper.GetFlowInfoByFlowId(FlowNo, CompanyCD);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       /// <summary>
       /// 获取流程使用状态
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <param name="BillTypeFlag"></param>
       /// <param name="BillTypeCode"></param>
       /// <returns></returns>
       public static int GetFlowUsedStatus(string CompanyCD, int BillTypeFlag, int BillTypeCode)
       {
           if (string.IsNullOrEmpty(CompanyCD)) return 0;
           try
           {
               return ApprovalFlowSetDBHelper.GetFlowUsedStatus(CompanyCD, BillTypeFlag, BillTypeCode);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       /// <summary>
       /// 发布流程|停止流程
       /// </summary>
       /// <returns></returns>
       public static bool PublishFlow(string UsedStatus,string FlowNo,string CompanyCD)
       {
           if (string.IsNullOrEmpty(UsedStatus)) return false;
           try
           {
               return ApprovalFlowSetDBHelper.PublishFlow(UsedStatus,FlowNo,CompanyCD);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       /// <summary>
       /// 获取流程实例表里的审批状态
       /// </summary>
       /// <param name="FlowNo"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static DataTable GetFlowStatusbyFlowNo(string FlowNo)
       {
           if (string.IsNullOrEmpty(FlowNo)) return null;
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           try
           {
               return ApprovalFlowSetDBHelper.GetFlowStatusbyFlowNo(FlowNo, CompanyCD);
           }
           catch (Exception ex)
           {
               throw ex;
           }
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
           logSys.ModuleID = ConstUtil.Menu_Flow;
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
           logModel.ModuleID = ConstUtil.Menu_Flow;
           //设置操作日志类型 修改
           logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_Flow;
           //操作对象
           logModel.ObjectID = prodno;
           //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
           logModel.Element = string.Empty;

           return logModel;

       }

       #endregion
    }
}
