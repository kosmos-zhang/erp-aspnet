/**********************************************
 * 描述：     币种类别业务处理
 * 建立人：   莫申林
 * 建立时间： 2009/04/07
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
  public class CurrTypeSettingBus
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
          logSys.ModuleID = ConstUtil.MODULE_ID_CURRENCYTYOE_LIST;
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
          logModel.ModuleID = ConstUtil.MODULE_ID_CURRENCYTYOE_LIST;

          //设置操作日志类型 修改
          logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_CURRENCETYPE;
          //操作对象
          logModel.ObjectID = wcno;
          //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
          logModel.Element = string.Empty;
          return logModel;
      }
      #endregion

      #region 修改期末汇率信息
      /// <summary>
      /// 修改币种期末汇率信息
      /// </summary>
      /// <param name="ID">主键ID</param>
      /// <param name="EndRate">汇率</param>
      /// <returns>true 成功,false 失败</returns>
      public static bool UpdateEndRate(string ID, string EndRate)
      {
          bool result = false;
          try
          {
              //拆分主键ID
              string[] arrayID = ID.Split(',');
              //拆分期末利率
              string[] arrayEndRate=EndRate.Split(',');
              if (arrayID.Length > 0 && arrayEndRate.Length > 0)
              {
                  int j=0;
                  for (int i = 0; i < arrayID.Length; i++)
                  {
                      string _Id=arrayID[i].ToString();
                      decimal _Rate=Convert.ToDecimal(arrayEndRate[j]);
                      result = CurrTypeSettingDBHelper.UpdateEndRate(_Id,_Rate);
                      j++;
                  }
              }
              return result;
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      #endregion
      
      #region 获取非本币的信息 
        /// <summary>
        ///  获取非本币的信息
        /// </summary>
        /// <param name="CompanyCD">企业编码</param>
        /// <returns>DataTable</returns>
      public static DataTable GetNotMasterCurrency()
      {
          string CompanyCD=((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          if (string.IsNullOrEmpty(CompanyCD)) return null;
          try
          {
            return  CurrTypeSettingDBHelper.GetNotMasterCurrency(CompanyCD);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      #endregion

      #region  获取本币的信息
        /// <summary>
        ///  获取本币的信息
        /// </summary>
        /// <param name="CompanyCD">企业编码</param>
        /// <returns>DataTable</returns>
      public static DataTable GetMasterCurrency()
      {
          string CompanyCD = string.Empty;
          try
          {
               CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          }
          catch(Exception ext)  {
              throw ext;
          }
          if (string.IsNullOrEmpty(CompanyCD)) return null;
          try
          {
              return CurrTypeSettingDBHelper.GetMasterCurrency(CompanyCD);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      #endregion

      #region 获取币种类别信息
      /// <summary>
      /// 获取币种类别信息
      /// </summary>
      /// <param name="CompanyCD">公司编码</param>
      /// <returns>DataTable</returns>
      public static DataTable GetCurrTypeByCompanyCD()
      {
          string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          if (string.IsNullOrEmpty(CompanyCD)) return null;
          try
          {
              return CurrTypeSettingDBHelper.GetCurrTypeByCompanyCD(CompanyCD);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      #endregion
       
      #region 获取币种的汇率
      public static DataTable GetCurrencyRate(string ID)
      {
          try
          {
              return CurrTypeSettingDBHelper.GetCurrencyRate(ID);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
     #endregion


      #region 插入币种明细表
      /// <summary>
      /// 插入币种明细表
      /// </summary>
      /// <param name="Model"></param>
      /// <returns></returns>
      public static bool InSertCurrTypeSetting(CurrencyTypeSettingModel Model)
      {
          UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

          if (Model == null) return false;
          if (Model.CompanyCD == null) Model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          try
          {
              bool succ = false;
              string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
              LogInfoModel logModel = InitLogInfo(Model.CurrencyName);
              logModel.Element = ConstUtil.LOG_PROCESS_INSERT;

              succ= CurrTypeSettingDBHelper.InSertCurrTypeSetting(Model);
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

      #region 更新币种信息
      /// <summary>
      /// 更新币种信息
      /// </summary>
      /// <param name="Model"></param>
      /// <returns></returns>
      public static bool UpdateCurrTypeSetting(CurrencyTypeSettingModel Model)
      {
          UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

          if (Model == null) return false;
          if (Model.CompanyCD == null) Model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          try
          {
              bool succ = false;
              string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
              LogInfoModel logModel = InitLogInfo(Model.ID.ToString());

              logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;


              succ = CurrTypeSettingDBHelper.UpdateCurrTypeSetting(Model);

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

      #region 删除币种信息操作
      /// <summary>
      /// 删除币种信息操作
      /// </summary>
     /// <param name="CompanyCD">公司编码</param>
     /// <param name="ID">主键标识ID</param>
     /// <returns>true 删除成功 : false 删除失败</returns>
      public static bool DeleteCurrTypeSetting(string CompanyCD, string ID)
      {
          bool isSucc = false;

          UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

          if (string.IsNullOrEmpty(ID)) return false;
          try
          {
              isSucc = CurrTypeSettingDBHelper.DeleteCurrTypeSetting(CompanyCD, ID);

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
              WriteSystemLog(userInfo, ex);
              throw ex;
          }
      }
      #endregion

      #region 根据ID获取币种名称
      /// <summary>
        /// 根据ID获取币种名称
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
      public static string GetCuerryTypeName(int ID)
      {
          try
          {
              return CurrTypeSettingDBHelper.GetCuerryTypeName(ID);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      #endregion



      #region 是否已存在本位币
      /// <summary>
      /// 是否已存在本位币
      /// </summary>
      /// <param name="CompanyCD">公司编码</param>
      /// <returns></returns>
      public static bool IsExitsMasterCurrency(string ID)
      {
          
          string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          try
          {
              if (CompanyCD == null || CompanyCD == "")
              {
                  return false;
              }
              else
              {
                  return CurrTypeSettingDBHelper.IsExitsMasterCurrency(CompanyCD,ID);
              }
          }
          catch (Exception ex)
          {
              throw ex;
          }
          
      }
      #endregion


      #region 判断币种名称或币种符是否重复
        /// <summary>
      /// 判断币种名称或币种符是否重复
        /// </summary>
        /// <param name="CurrencyName">币种名称</param>
        /// <param name="CurrencySymbol">币种符</param>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
      public static int IsSame(string CurrencyName, string CurrencySymbol, string ID)
      {
          string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          try
          {
              if (CompanyCD == null || CompanyCD == "")
              {
                  return -1;
              }
              else
              {
                  return CurrTypeSettingDBHelper.IsSame(CurrencyName, CurrencySymbol, CompanyCD, ID);
              }
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      #endregion


       #region 获取启用状态的币种类别信息
      /// <summary>
      /// 获取币种类别信息
      /// </summary>
      /// <returns>DataTable</returns>
      public static DataTable GetUsedCurrTypeByCompanyCD()
      {
          string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          if (string.IsNullOrEmpty(CompanyCD)) return null;
          try
          {
              return CurrTypeSettingDBHelper.GetUsedCurrTypeByCompanyCD(CompanyCD);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
       #endregion
  }

}
