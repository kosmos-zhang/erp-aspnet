using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using XBase.Data.DBHelper;
using XBase.Data.Office.StorageManager;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using System.Collections;
namespace XBase.Business.Office.StorageManager
{
  public  class StorageAdjustBus
    {
      /// <summary>
      /// 获取仓库信息
      /// </summary>
      /// <returns></returns>
      public static DataTable GetStorageInfo()
      {
          UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
          string CompanyCD = userInfo.CompanyCD;
          return StorageAdjustDBHelper.GetStorage(CompanyCD);
      }

      public static DataTable GetReason(string Flag)
      {
          UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
          string CompanyCD = userInfo.CompanyCD;
          return StorageAdjustDBHelper.GetReason(Flag,CompanyCD);
      }

      public static bool AddAdjust(StorageAdjustModel model, List<StorageAdjustDetail> detail, Hashtable ht)
      {
          UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
          try
          {
             
              bool succ = false;
              LogInfoModel logModel = InitLogInfo(model.AdjustNo);
              logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
              succ = StorageAdjustDBHelper.AddAdjust(model,detail,ht);
              if (!succ)
              {
                  logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
              }
              else
              {
                  logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                  model.ID = IDIdentityUtil.GetIDIdentity("officedba.StorageAdjust");
              }
              LogDBHelper.InsertLog(logModel);
              return succ;
          }
          catch (Exception ex)
          {
              WriteSystemLog(userInfo, ex);
              return false;
          }

      }
      public static bool UpdateAdjust(StorageAdjustModel model, List<StorageAdjustDetail> detail, string[] SortID, Hashtable ht)
      {
          UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
          try
          {
             
              bool succ = false;
              LogInfoModel logModel = InitLogInfo(model.AdjustNo);
              logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
              succ = StorageAdjustDBHelper.UpdateAdjust(model, detail, SortID,ht);
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
      }

      public static bool DelAdjust(string ID,string CompanyCD)
      {
         UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
         //string CompanyCD = "AAAAAA";
         bool isSucc =StorageAdjustDBHelper.DelAdjust(ID, CompanyCD);
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
         //获取删除的编号列表
         string[] noList = ID.Split(',');
         //遍历所有编号，登陆操作日志
         for (int i = 0; i < noList.Length; i++)
         {
             //获取编号
             string no = noList[i];
             //替换两边的 '
             no = no.Replace("'", string.Empty);

             //操作日志
             LogInfoModel logModel = InitLogInfo("日常调整ID：" + no);
             //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
             logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
             //设置操作成功标识
             logModel.Remark = remark;

             //登陆日志
             LogDBHelper.InsertLog(logModel);
         }
         return isSucc;
      }


      public static bool ConfirmBill(StorageAdjustModel model,List<StorageAdjustDetail> detail)
      {
          return StorageAdjustDBHelper.ConfirmBill(model,detail);
      }
      public static bool CloseBill(StorageAdjustModel model, string method)
      {
          return StorageAdjustDBHelper.CloseBill(model, method);
      }

      public static DataTable GetAllAdjust(StorageAdjustModel model,string EFIndex,string EFDesc, string BetinTime, string EndTime, string FlowStatus,ref int TotalCount)
      {
          return StorageAdjustDBHelper.GetAllAdjust(model, EFIndex, EFDesc,BetinTime,EndTime,FlowStatus,ref TotalCount);
      }
      public static DataTable GetAdjustInfo(StorageAdjustModel model)
      {
          return StorageAdjustDBHelper.GetAdjustInfo(model);
      }
      /// <summary>
      /// 取消确认
      /// </summary>
      /// <param name="model"></param>
      /// <param name="detail"></param>
      /// <returns></returns>
      public static bool UnConfirmBill(StorageAdjustModel model,List<StorageAdjustDetail> detail)
      {
          return StorageAdjustDBHelper.UnConfirmBill(model, detail);
      }
      public static DataTable GetAdjustDetail(StorageAdjustModel model)
      {
          return StorageAdjustDBHelper.GetAdjustDetail(model);
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
          logSys.ModuleID = ConstUtil.MODULE_CODING_RULE_TABLE_AdjustInfo;
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
          logModel.ModuleID = ConstUtil.MODULE_ID_STORAGE_ADJUST_ADD;
          //设置操作日志类型 修改
          logModel.ObjectName = ConstUtil.MODULE_CODING_RULE_TABLE_AdjustInfo;
          //操作对象
          logModel.ObjectID = prodno;
          //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
          logModel.Element = string.Empty;

          return logModel;

      }
      #endregion

      //-------------------------------------------------------------页面打印需要
      public static DataTable GetAdjustInfo(int ID)
      {
          return StorageAdjustDBHelper.GetAdjustInfo(ID);
      }
      public static DataTable GetAdjustDetailInfo(int ID)
      {
          return StorageAdjustDBHelper.GetAdjustDetail(ID);
      }
    }
}
