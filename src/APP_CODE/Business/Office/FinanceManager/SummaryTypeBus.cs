/**********************************************
 * 描述：     摘要类别设置业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/03/10
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using System.Data;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
namespace XBase.Business.Office.FinanceManager
{
  public  class SummaryTypeBus
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
            logSys.ModuleID = ConstUtil.MODULE_ID_SUMMARYTYPE_LIST;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_SUMMARYTYPE_LIST;

            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_SUMMARYTYPE;
            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;
            return logModel;
        }
        #endregion

        #region 判断名称是否存在
      public static bool NameIsExist(string Name)
      {
          string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          try
          {
             return   SummaryTypeDBHelper.NameIsExist(CompanyCD,Name);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      } 


      #endregion 根据企业编码获取摘要类别信息

      public static DataTable GetSummaryTypeByCD()
      {
         string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
         if (string.IsNullOrEmpty(CompanyCD)) return null;
         try
         {
              DataTable dt= SummaryTypeDBHelper.GetSummaryTypeByCD(CompanyCD);
              if (dt != null && dt.Rows.Count > 0)
              {
                  foreach (DataRow Rows in dt.Rows)
                  {
                      if (Rows["UsedStatus"].ToString() == ConstUtil.USED_STATUS_OFF)
                      {
                          Rows["UsedStatus"] = ConstUtil.USED_STATUS_OFF_NAME;
                      }
                      else if (Rows["UsedStatus"].ToString() == ConstUtil.USED_STATUS_ON)
                      {
                          Rows["UsedStatus"] = ConstUtil.USED_STATUS_ON_NAME;
                      }
                  }
              }
              return dt;
         }
         catch (Exception ex)
         {
             throw ex;
         }
      }


      #region  判断摘要类别是否被引用
      public static bool isSummaryTypeReference(int SummaryTypeID)
      {
          try
          {
              return SummaryTypeDBHelper.isSummaryTypeReference(SummaryTypeID);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      #endregion

      #region 添加类别信息
      public static bool InsertSummaryType(string Name, string UsedStatus)
      {
          UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

          string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          try
          {
              bool succ = false;
              string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
              LogInfoModel logModel = InitLogInfo(Name);
              logModel.Element = ConstUtil.LOG_PROCESS_INSERT;

              succ= SummaryTypeDBHelper.InsertSummaryType(CompanyCD,Name,UsedStatus);

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
              throw ex;
          }
      }
      #endregion

      #region 更新类别信息
      public static bool UpdateSummaryType(int ID, string Name, string UsedStatus)
      {
          string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
          try
          {
            
              bool succ = false;
              string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
              LogInfoModel logModel = InitLogInfo(ID.ToString());

              logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

              succ= SummaryTypeDBHelper.UpdateSummaryType(CompanyCD, ID, Name, UsedStatus);

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
              throw ex;
          }
      }
      #endregion

      #region 删除类别信息
      public static bool DelSummaryType(string ID)
      {
          bool isSucc = false;

          UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

          string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          try
          {
              isSucc= SummaryTypeDBHelper.DelSummaryType(CompanyCD,ID);
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
                  LogInfoModel logModel = InitLogInfo(no);
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
              WriteSystemLog(userInfo,ex);
              throw ex;
          }
      }
      #endregion

      #region 根据ID获取摘要类别名称
      public string GetSummaryTypeName(int id)
      {
          try
          {
              return SummaryTypeDBHelper.GetSummaryTypeName(id);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      #endregion

    }
}
