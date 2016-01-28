/**********************************************
 * 类作用：   用户管理事务层处理
 * 建立人：   吴志强
 * 建立时间： 2009/01/10
 ***********************************************/

using System;
using XBase.Model.Office.SystemManager;
using XBase.Data.Office.SystemManager;
using XBase.Common;
using System.Data;
using XBase.Model.Common;
using XBase.Business.Common;
using XBase.Data.Common;
namespace XBase.Business.Office.SystemManager
{
    /// <summary>
    /// 类名：UserInfoBus
    /// 描述：用户管理事务层处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/01/10
    /// 最后修改时间：2009/01/10
    /// </summary>
    ///
    public class UserInfoBus
    {
        /// <summary>
        /// 用户信息更新或者插入
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <returns>更新成功与否</returns>
        public static bool ModifyUserInfo(UserInfoModel model)
        {

            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //定义返回变量
            bool isSucc = false;
            //获取登陆用户ID
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; //待修改
            //string loginUserID = "admin";
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.UserID);
            if (model.IsInsert)
            {
                try
                {
                    //设置操作日志类型 新建
                    logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_UserInfo;//操作对象
                    logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                    //执行插入操作
                    isSucc = UserInfoDBHelper.ModifyUserInfo(model, loginUserID); ;
                }
                catch (Exception ex)
                {
                    //输出日志
                    WriteSystemLog(userInfo, ex);
                }
            }
            else
            {
                try
                {
                    //设置操作日志类型 修改
                    logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_UserInfo;//操作对象
                    logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                    //执行更新操作
                    isSucc = UserInfoDBHelper.ModifyUserInfo(model, loginUserID); ;
                }
                catch (Exception ex)
                {
                    //输出日志
                    WriteSystemLog(userInfo, ex);
                }
            }
              //更新成功时，删除原来文件
            if (isSucc)
            {
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
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ModifyUserInfoPwd(string userID, string psd, string CommanyCD, string hfuserid)
        {

            //UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ////设置公司代码
            //string userid = userInfo.UserID;
            //获取登陆用户ID
          //  string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; //待修改
            LogInfoModel logModel = InitLogInfo(userID);
            bool flag = false;
            try
            {
                if (!string.IsNullOrEmpty(userID) || !string.IsNullOrEmpty(psd))
                {
                    flag= UserInfoDBHelper.ModifyUserInfoPwd(userID, psd, CommanyCD);
                    if (flag)
                    {
                        try
                        {
                            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_UserInfo;//操作对象
                            logModel.Element = "修改密码";
                            logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                            logModel.ModuleID = "";
                            LogDBHelper.InsertLog(logModel);
                            return UserInfoDBHelper.InsertPasswordHistory(CommanyCD, userID, psd, hfuserid);
                        }
                        catch (Exception ex)
                        {
                            return flag;
                            throw ex;
                        }
                    }
                    else
                    {
                        return flag;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
    
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static DataTable GetUserInfo(UserInfoModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            return UserInfoDBHelper.GetUserInfo(model,pageIndex, pageCount, OrderBy, ref totalCount);
        }


        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static DataTable GetUserInfoByID(string UserId,string CompanyCD)
        {
            return UserInfoDBHelper.GetUserInfoByID(UserId, CompanyCD);
        }
        /// <summary>
        /// 查询是否存在此用户ID
        /// </summary>
        /// <param name="companyCD"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static int GetUserCount(string companyCD, string UserId)
        {
            return UserInfoDBHelper.GetUserCount(companyCD, UserId);
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static int IsMaxUserCount(string companyCD)
        {
            //获取用户数
            int userCount = UserInfoDBHelper.GetCompanyUserCount(companyCD);
            //获取公司最大用户数
            int userMaxCount = UserInfoDBHelper.GetCompanyMaxUserNum(companyCD);

            return userCount < userMaxCount ? 0 :1;
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户信息</returns>
        public static bool DeleteUserInfo(string userID)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //string CompanyCD = "AAAAAA";
            bool isSucc = UserInfoDBHelper.DeleteUserInfo(userID, companyCD);
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
            string[] noList = userID.Split(',');
            //遍历所有编号，登陆操作日志
            for (int i = 0; i < noList.Length; i++)
            {
                //获取编号
                string no = noList[i];
                //替换两边的 '
                no = no.Replace("'", string.Empty);

                //操作日志
                LogInfoModel logModel = InitLogInfo("用户名：" + no);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                //设置操作成功标识
                logModel.Remark = remark;

                //登陆日志
                LogDBHelper.InsertLog(logModel);
            }
            return isSucc;

        }
        public static bool ModifyUserInfoPwdLog(string userID, string psd, string companyCD, string SessionUserID)
        {
            try
            {
                return UserInfoDBHelper.ModifyUserInfoPwdLog(userID, psd, companyCD, SessionUserID);
            }
            catch 
            {
                return false;
            }
           
        }
        public static  DataTable GetEmployeeInfo( string ComPanyCD)
        {
            if (string.IsNullOrEmpty(ComPanyCD))
            {
                return null;
            }
            return UserInfoDBHelper.GetEmployeeInfo(ComPanyCD);
        }
        /// <summary>
        /// 根据公司名称获取用户姓名
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetUserInfo(string CompanyCD, string UserID)
        {
            //SQL拼写
            //string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //string sql = "select UserID,UserName from officedba.UserInfo where CompanyCD=@CompanyCD ";
            //SqlParameter[] parms = new SqlParameter[1];
            //parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //return SqlHelper.ExecuteSql(sql, parms);
            try
            {
                if (string.IsNullOrEmpty(CompanyCD))
                    return null;
                else
                    return UserInfoDBHelper.GetUserInfo(CompanyCD, UserID);
            }
            catch 
            {
                return null;
            }
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        public static DataTable GetUserList()
        {
            return UserInfoDBHelper.GetUserList();
        }

        public static DataTable GetUserList(bool flag)
        {
            return UserInfoDBHelper.GetUserList(flag);
        }

                /// <summary>
        /// 验证公司是否启用USBKEY设备
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool IsOpenValidateByCompany(string CompanyCD)
        {
            return UserInfoDBHelper.IsOpenValidateByCompany(CompanyCD);
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
            logSys.ModuleID = ConstUtil.Menu_AddRoleInfo;
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
            logModel.ModuleID = ConstUtil.Menu_AddUserInfo;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_UserInfo;
            //操作对象
            logModel.ObjectID = prodno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion


      
    }
}
