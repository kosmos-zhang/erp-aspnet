/**********************************************
 * 描述：     存取款单单业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/04/18
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
   public  class StoreFetchBillBus
    {
       #region 判断收付款单号是否已存在
       public static bool SFNoisExist(string SFNo)
       {
           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

               return StoreFetchBillDBHelper.SFNoisExist(CompanyCD, SFNo);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

        #region 输出系统日志
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">异常信息</param>
        private static void WriteSystemLog(UserInfoUtil userInfo, int ModuleType, Exception ex)
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
            if (ModuleType == 0)
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_STOREFETCHBILL_ADD;
            }
            else
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_STOREFETCHBILL_LIST;
            }
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
        private static LogInfoModel InitLogInfo(string wcno, int ModuleType)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            if (ModuleType == 0)
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_STOREFETCHBILL_ADD;
            }
            else
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_STOREFETCHBILL_LIST;
            }
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_STOREFETCH;
            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;
            //操作时间
            logModel.OperateDate = DateTime.Now;
            return logModel;
        }
        #endregion

        #region 更新收款单登记凭证状态
      /// <summary>
      /// 根据主键更新存取款单登记凭证状态
      /// </summary>
      /// <param name="ID">主键</param>
      /// <returns>true 成功,false 失败</returns>
       public static bool UpdateAccountStatus(string ID)
       {
           int Accountor = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
           try
           {
               return StoreFetchBillDBHelper.UpdateAccountStatus(ID, Accountor);
           }
           catch (Exception ex)
           {
               throw ex;  
           }
       }
       #endregion

        #region  确认存取款单
       /// <summary>
        /// 确认存取款单
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="Confirmor">确认人</param>
        /// <returns>true成功,false失败</returns>
       public static bool ConfirmStoreFetchBill(string ID)
       {
           int Confirmor = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;

           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

           bool isSucc = false;
           try
           {
               isSucc = StoreFetchBillDBHelper.ConfirmStoreFetchBill(ID, Confirmor.ToString());
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
                   LogInfoModel logModel = InitLogInfo(no, 1);
                   //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                   logModel.Element = ConstUtil.LOG_PROCESS_CONFIRM;
                   //设置操作成功标识
                   logModel.Remark = remark;
                   //登陆日志
                   LogDBHelper.InsertLog(logModel);
               }
               return isSucc;
           }
           catch (Exception ex)
           {
               WriteSystemLog(userInfo, 1, ex);
               return false;
           }
        }
        #endregion

        #region 反确存取款单
        /// <summary>
        /// 反确存取款单
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="Confirmor">确认人</param>
        /// <returns>true成功,false失败</returns>
        public static bool ReConfirmStoreFetchBill(string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            bool isSucc = false;
            try
            {
                isSucc = StoreFetchBillDBHelper.ReConfirmStoreFetchBill(ID);
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
                    LogInfoModel logModel = InitLogInfo(no, 1);
                    //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                    logModel.Element = ConstUtil.LOG_PROCESS_ANTIAUDIT;
                    //设置操作成功标识
                    logModel.Remark = remark;
                    //登陆日志
                    LogDBHelper.InsertLog(logModel);
                }
                return isSucc;

            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, 1, ex);
                return false;
            }
         
        }
        #endregion

        #region 添加存取款单信息
      /// <summary>
      /// 添加存取款单信息
      /// </summary>
      /// <param name="model">存取款单实体</param>
      /// <param name="IntID">返回添加后的主键ID</param>
      /// <returns>true 成功,false失败</returns>
       public static bool InsertStoreFetchBill(StoreFetchBillModel model, out int IntID)
       {
           IntID = 0;
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
          
           try
           {
               model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

               bool succ = false;
               string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

               LogInfoModel logModel = InitLogInfo(model.SFNo, 0);
               logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

               succ = StoreFetchBillDBHelper.InsertStoreFetchBill(model, out IntID);
               if (!succ)
                   logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
               else
                   logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
               LogDBHelper.InsertLog(logModel);
               return succ;
           }
           catch (Exception ex)
           {
               WriteSystemLog(userInfo, 0, ex);
               return false;
           }
       }
      #endregion

        #region  根据检索条件检错存取款单信息
      /// <summary>
      /// 根据检索条件检错存取款单信息
      /// </summary>
      /// <param name="SFNo">收付款单号</param>
      /// <param name="BillNum">票号</param>
      /// <param name="Type">类别</param>
      /// <param name="Price">金额</param>
      /// <param name="ConfirmStatus">确认状态</param>
      /// <param name="IsAccount">是否登记凭证</param>
      /// <param name="StartDate">开始日期</param>
      /// <param name="EndDate">结束日期</param>
      /// <returns>DataTable</returns>
       public static DataTable SearchStoreFetchBill(string SFNo, string SFBillNum, string Type,
           string Price, string ConfirmStatus, string IsAccount, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
       {
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           try
           {
               return StoreFetchBillDBHelper.SearchStoreFetchBill(CompanyCD, SFNo, SFBillNum,
                   Type, Price, ConfirmStatus, IsAccount, StartDate, EndDate,pageIndex,pageSize,OrderBy,ref totalCount);
                  
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        #endregion

        #region 修改存取款单
       /// <summary>
      /// 修改存取款单信息
      /// </summary>
      /// <param name="model">存取款单实体</param>
      /// <returns>true 成功,false失败</returns>
       public static bool UpdateStoreFetchBill(StoreFetchBillModel model)
       {
           model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
 
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           try
           {

               bool succ = false;
               string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

               LogInfoModel logModel = InitLogInfo(model.SFNo, 0);
               logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

               succ = StoreFetchBillDBHelper.UpdateStoreFetchBill(model);
               if (!succ)
                   logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
               else
                   logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
               LogDBHelper.InsertLog(logModel);
               return succ;

           }
           catch (Exception ex)
           {
               WriteSystemLog(userInfo, 0, ex);
               return false;
           }
       }
      #endregion

        #region 删除存取款单
      /// <summary>
      /// 删除存取款单
      /// </summary>
      /// <param name="ID">主键ID</param>
      /// <returns></returns>
       public static bool DeleteStoreFetchBill(string ID)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           bool isSucc = false;

           try
           {


               isSucc = StoreFetchBillDBHelper.DeleteStoreFetchBill(ID);
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
                   LogInfoModel logModel = InitLogInfo(no, 1);
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
               WriteSystemLog(userInfo, 1, ex);
               return false;
           }
       }
      #endregion

        #region  根据主键ID获取存取款单信息
      /// <summary>
      /// 根据主键ID获取存取款单信息
      /// </summary>
      /// <param name="ID">主键ID</param>
      /// <returns>DataTable</returns>
       public static DataTable GetStoreFetchInfoByID(string ID)
       {
           try
           {
               return StoreFetchBillDBHelper.GetStoreFetchInfoByID(ID);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion
    }
}
