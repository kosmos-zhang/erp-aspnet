/**********************************************
 * 描述：     会计科目业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/03/09
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
   public  class AccountSubjectsBus 
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
           logSys.ModuleID = ConstUtil.MODULE_ID_ACCOUNTSUBJECTS_SETTING;
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
           logModel.ModuleID = ConstUtil.MODULE_ID_ACCOUNTSUBJECTS_SETTING;
   
           //设置操作日志类型 修改
           logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_ACCOUNTSUBJECTS;
           //操作对象
           logModel.ObjectID = wcno;
           //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
           logModel.Element = string.Empty;
           return logModel;
       }
       #endregion

       #region 根据获取编码获取科目信息
       public static DataTable GetSubjectsInfoByCD(string SubjectsCD)
       {
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           try
           {
               return AccountSubjectsDBHelper.GetSubjectsInfo(CompanyCD,SubjectsCD);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 根据科目编码模糊查询科目
       /// <summary>
       /// 根据科目编码模糊查询科目
       /// </summary>
       /// <param name="SubjectsCD">科目编码</param>
       /// <returns>DataTable</returns>
       public static DataTable QuerySubjectsBySubjectsCD(string SubjectsCD)
       {
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           if (string.IsNullOrEmpty(CompanyCD) && string.IsNullOrEmpty(SubjectsCD)) return null;
           try
           {
               return AccountSubjectsDBHelper.QuerySubjectsBySubjectsCD(CompanyCD,SubjectsCD);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 更新科目期初值
       /// <summary>
       /// 更新科目期初值
       /// </summary>
       /// <param name="Model">科目实体</param>
       /// <returns>True 成功，false失败</returns>
       public static bool UpdateBeginMoney(AccountSubjectsModel Model)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           if (Model == null) return false;
           if (Model.CompanyCD == null) Model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           try
           {
               return AccountSubjectsDBHelper.UpdateBeginMoney(Model);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 根据科目类别获取科目期初值
       /// <summary>
       /// 根据科目类别获取科目期初值
       /// </summary>
       /// <param name="TypeID">科目类别</param>
       /// <returns>DataTable</returns>
       public static DataTable GetSubjectsInitAmountByTypeID(string TypeID, string ParentID)
       {
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           try
           {
               DataTable dt= AccountSubjectsDBHelper.GetSubjectsInitAmountByTypeID(CompanyCD,TypeID,ParentID);
               if (dt != null && dt.Rows.Count > 0)
               {
                   foreach (DataRow rows in dt.Rows)
                   {
                       if (rows["BlanceDire"].ToString() == ConstUtil.SUBJECTS_DIRE_J_CODE)
                       {
                           rows["BlanceDire"] = ConstUtil.SUBJECTS_DIRE_J_NAME;
                       }
                       else if (rows["BlanceDire"].ToString() == ConstUtil.SUBJECTS_DIRE_D_CODE)
                       {
                           rows["BlanceDire"] = ConstUtil.SUBJECTS_DIRE_D_NAME;
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
       #endregion

       #region 获取会计科目信息
       /// <summary>
       /// 获取会计科目信息
       /// </summary>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns>DataTable</returns>
       public static DataTable GetAccountSubjects()
       {
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD; 
           try
           {
               DataTable dt= AccountSubjectsDBHelper.GetAccountSubjects(CompanyCD);
               if (dt != null && dt.Rows.Count > 0)
               {
                   foreach (DataRow rows in dt.Rows)
                   {
                       if (rows["BlanceDire"].ToString() == ConstUtil.SUBJECTS_DIRE_J_CODE)
                       {
                           rows["BlanceDire"] = ConstUtil.SUBJECTS_DIRE_J_NAME;
                       }
                       else if (rows["BlanceDire"].ToString() == ConstUtil.SUBJECTS_DIRE_D_CODE)
                       {
                           rows["BlanceDire"] = ConstUtil.SUBJECTS_DIRE_D_NAME;
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
       #endregion

       #region  新增会计科目信息
       /// <summary>
       /// 新增会计科目信息
       /// </summary>
       /// <param name="Model">科目实体</param>
       /// <returns>True 成功，false失败</returns>
       public static bool InsertAccountSubjects(AccountSubjectsModel Model)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           if(Model==null) return  false;
           if(Model.CompanyCD == null) Model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
         
           try
           {
               bool succ = false;
               string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
               LogInfoModel logModel = InitLogInfo(Model.SubjectsCD);
               logModel.Element = ConstUtil.LOG_PROCESS_INSERT;

               succ= AccountSubjectsDBHelper.InsertAccountSubjects(Model);
               if (!succ)
                   logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
               else
                   logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
               LogDBHelper.InsertLog(logModel);
               return succ;
           }
           catch(Exception ex)
           {
               WriteSystemLog(userInfo,ex);
               throw ex;
           }
       }
       #endregion

       #region 判断该结点下，是否还有子结点
       /// <summary>
       /// 判断该结点下，是否还有子结点
       /// </summary>
       /// <param name="ParentCode">上级编码</param>
       /// <returns>大于0还有子节点，否则无子节点</returns>
       public static int ChildrenCount(string ParentCode)
       {
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           try
           {
               return AccountSubjectsDBHelper.ChildrenCount(ParentCode,CompanyCD);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 修改会计科目信息
       /// <summary>
       /// 修改会计科目信息
       /// </summary>
       /// <param name="Model">科目实体</param>
       /// <returns>True 成功，false失败</returns>
       public static bool UpdateAccountSubjects(AccountSubjectsModel Model)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           if (Model == null) return false;
           if (Model.CompanyCD == null) Model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

           try
           {
               bool succ = false;
               string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
               LogInfoModel logModel = InitLogInfo(Model.SubjectsCD);
               logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
               succ = AccountSubjectsDBHelper.UpdateAccountSubjects(Model);

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

       #region 删除会计科目
       /// <summary>
       /// 删除会计科目
       /// </summary>
       /// <param name="ID">主键编码</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns>True 成功，false失败</returns>
       public static string DeleteAccountSubjects(string ID,string SubjectsCode, string CompanyCD)
       {
           try
           {
               bool isSucc = false;


               UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

               string[] sql =null;//sql语句数组
               string Execresult ="";//执行结果
               DataTable dt=  AccountSubjectsDBHelper.GetAccountsByID(CompanyCD,ID);
               if(dt!=null &&  dt.Rows.Count>0)
               {
                   if (dt.Rows[0]["UsedStatus"].ToString().Trim() == ConstUtil.USED_STATUS_ON)
                   {
                       Execresult =ConstUtil.USED_STATUS_ON;
                   }
                   else
                   {
                       //查找当前科目是否有子节点
                       int count = AccountSubjectsDBHelper.ChildrenCount(SubjectsCode, CompanyCD);
                       if (count > 0)
                       {
                           sql = new string[2];
                           sql[0] = "delete from officedba.AccountSubjects where  CompanyCD='" + CompanyCD + "' and ID='" + ID + "'";
                           sql[1] = "delete from officedba.AccountSubjects where  CompanyCD='" + CompanyCD + "' and ParentID='" + SubjectsCode + "'";
                       }
                       else
                       {
                           sql = new string[1];
                           sql[0] = "delete from officedba.AccountSubjects where  CompanyCD='" + CompanyCD + "' and ID='" + ID + "'";
                       }
                       Execresult = AccountSubjectsDBHelper.DelAccountSubjects(sql) == true ? ConstUtil.EXEC_RESULT_SUCCESS_NAME : ConstUtil.EXEC_RESULT_FAIL_NAME;
                       if (Execresult == ConstUtil.EXEC_RESULT_SUCCESS_NAME)
                       {
                           isSucc = true;
                       }
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
                   }
               }
               return Execresult;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 根据企业编码和主键科目详情
       /// <summary>
       /// 根据企业编码和主键科目详情
       /// </summary>
       /// <param name="CompanyCD">企业编码</param>
       /// <param name="ID">主键</param>
       /// <returns>DataTable</returns>
       public static DataTable GetAccountsByID(string ID)
       {
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           if (string.IsNullOrEmpty(CompanyCD) && string.IsNullOrEmpty(ID)) return null;
           try
           {
               return AccountSubjectsDBHelper.GetAccountsByID(CompanyCD,ID);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 根据企业编码获取科目信息
       /// <summary>
       /// 根据企业编码获取科目信息
       /// </summary>
       /// <param name="CompanyCD">企业编码</param>
       /// <returns>DataTable</returns>
       public static DataTable GetSubjectsInfo(string ParentID)
       {
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           if (string.IsNullOrEmpty(CompanyCD) && string.IsNullOrEmpty(ParentID)) return null;
           try
           {

               return AccountSubjectsDBHelper.GetASubjectsByCompanyCD(CompanyCD,ParentID);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 判断科目编码是否存在
       /// <summary>
       /// 判断科目编码是否存在
       /// </summary>
       /// <param name="SubjectsCD">科目编码</param>
       /// <returns>true 是，false 否</returns>
       public static bool SubjectsCDIsExist(string SubjectsCD)
       {
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           try
           {
               return AccountSubjectsDBHelper.SubjectsCDIsExist(CompanyCD, SubjectsCD);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region  根据科目类别获取科目信息
       /// <summary>
       /// 根据科目类别获取科目信息
       /// </summary>
       /// <param name="TypeID">科目类别</param>
       /// <returns>DataTable</returns>
       public static DataTable GetSubjectsByTypeID(string TypeID,string ParentID)
       {
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           if (string.IsNullOrEmpty(CompanyCD) && string.IsNullOrEmpty(TypeID)) return null;
           DataTable dt=new DataTable();
           try
           {
                dt=AccountSubjectsDBHelper.GetSubjectsByTypeID(CompanyCD,TypeID,ParentID);
           }
           catch (Exception ex)
           {
               //定义变量
               LogInfo logSys = new LogInfo();
               //设置日志类型 需要指定为系统日志
               logSys.Type = LogInfo.LogType.SYSTEM;
               //指定系统日志类型 出错信息
               logSys.SystemKind = LogInfo.SystemLogKind.SYSTEM_ERROR;
               //指定登陆用户信息
              // logSys.UserInfo = UserInfo;
               //设定模块ID
             // logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_ADD;
               //描述
               logSys.Description = ex.ToString();

               //输出日志
               LogUtil.WriteLog(logSys);
           }
           return dt;
       }
       #endregion
   }
}
