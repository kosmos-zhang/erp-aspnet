/**********************************************
 * 描述：     期末项目设置业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/03/27
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;


using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;

namespace XBase.Business.Office.FinanceManager
{
 public  class EndItemSettingBus
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
         logSys.ModuleID = ConstUtil.MODULE_ID_ENDITEM_LIST;
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
         logModel.ModuleID = ConstUtil.MODULE_ID_ENDITEM_LIST;

         //设置操作日志类型 修改
         logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_ENDITEMSETTING;
         //操作对象
         logModel.ObjectID = wcno;
         //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
         logModel.Element = string.Empty;
         return logModel;
     }
     #endregion

     #region 获取为已启用的期末项目
      /// <summary>
      /// 获取为已启用的期末项目
      /// </summary>
      /// <param name="CompanyCD">企业编码</param>
      /// <returns>DataTable</returns>
     public static DataTable GetEndItemIsUsed()
     {
         string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
         try
         {
              DataTable dt = EndItemSettingDBHelper.GetEndItemIsUsed(CompanyCD);
              //if (dt != null && dt.Rows.Count > 0)
              //{
              //    foreach (DataRow rows in dt.Rows)
              //    {
              //        if (rows["ItemName"].ToString().Trim() == "固定资产折旧")
              //        {
              //            string PeriodNum =  DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
              //            if (!EndItemProcessing.CheckPeriodIsexist(PeriodNum, CompanyCD))
              //            {
              //                rows["IsAccount"] = "未处理";
              //                break;
              //            }
              //        }
              //    }
              //}
              return dt;
         }
         catch (Exception ex)
         {
             throw ex;
         }
     }
     #endregion

     #region 添加期末项目
     /// <summary>
      /// 添加期末项目
      /// </summary>
      /// <param name="CompanyCD">企业编码</param>
      /// <param name="ItemName">项目名称</param>
      /// <param name="UsedStatus">使用状态</param>
      /// <returns>true 成功，false失败</returns>
     public static bool InsertEndItemSetting(string ItemName, string UsedStatus)
     {
         UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

         int ShowIndex = 0;
         string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
         try
         {
             bool succ = false;
             string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
             LogInfoModel logModel = InitLogInfo(ItemName);
             logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
             if (ItemName.Trim() == "固定资产折旧")
             {
                 ShowIndex = 1;
             }
             else if (ItemName.Trim() == "期末调汇")
             {
                 ShowIndex = 2;
             }
             else if (ItemName.Trim() == "损益结转")
             {
                 ShowIndex = 3;
             }


             succ=EndItemSettingDBHelper.InsertEndItemSetting(CompanyCD,ItemName,UsedStatus,ShowIndex);

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

     #region 修改期末项目
     /// <summary>
      /// 修改期末项目
      /// </summary>
      /// <param name="CompanyCD">企业编码</param>
      /// <param name="ID">主键</param>
      /// <param name="UsedStatus">使用状态</param>
      /// <returns>true 成功，false失败</returns>
     public static bool UpdateEndItemSetting( string ID, string UsedStatus)
     {
         UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

         string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
         try
         {
             bool succ = false;
             string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
             LogInfoModel logModel = InitLogInfo(ID);

             logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

            succ=  EndItemSettingDBHelper.UpdateEndItemSetting(CompanyCD,ID,UsedStatus);
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

     #region 更新期末项目
     public static bool UpdateEndItemByName( string ItemName, string UsedStatus)
     {
         UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

         string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
         try
         {
             bool succ = false;
             string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
             LogInfoModel logModel = InitLogInfo(ItemName);

             logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

             succ = EndItemSettingDBHelper.UpdateEndItemByName(CompanyCD, ItemName, UsedStatus);
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




     #region 根据企业编码查看企业项目设置
     /// <summary>
      /// 根据企业编码查看企业项目设置
      /// </summary>
      /// <param name="CompanyCD">企业编码</param>
      /// <returns>DataTable</returns>
     public static DataTable GetEndItemSettingInfo()
     {
         string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
         try
         {
             return EndItemSettingDBHelper.GetEndItemSettingInfo(CompanyCD);
         }
         catch (Exception ex)
         {
             throw ex;
         }
     }
     #endregion


     #region 查看当前企业是否存在期末项目
     public static bool Isexist(string EndItemName)
     {
         try
         {
             string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
             return EndItemSettingDBHelper.Isexist(CompanyCD,EndItemName);

         }
         catch (Exception ex)
         {
             throw ex;
         }
     }
     #endregion



 }
}
