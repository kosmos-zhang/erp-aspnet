/**********************************************
 * 类作用：   角色与用户关联处理层
 * 建立人：   吴成好
 * 建立时间： 2009/01/10
 ***********************************************/

using System;
using XBase.Model.Office.SystemManager;
using XBase.Data.Office.SystemManager;
using XBase.Common;
using System.Data;
using System.Collections;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
namespace XBase.Business.Office.SystemManager
{
    public class UserRoleBus
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
            logSys.ModuleID = ConstUtil.Menu_SerchUserRole;
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
        private static LogInfoModel InitLogInfo(string[] roleList)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.Menu_AddUserRole;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_UserRole;
            //操作对象
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        /// <summary>
        /// 获取用户的角色
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns>DataTable</returns>
        //public static DataTable GetUserRoleWithUserID(string UserID)
        //{
        //    try
        //    {
        //        return UserRoleDBHelper.GetUserRoleWithUserID(UserID);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// 删除角色与用户关联信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool DeleteUserRole(string UserID, string RoleID)
        {
            try
            {
              return  UserRoleDBHelper.DeleteUserRole(UserID, RoleID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 角色与用户关联信息插入
        /// </summary>
        /// <param name="model">角色与用户关联信息</param>
        /// <returns>插入成功与否</returns>
        public static bool InsertUserRole(UserRoleModel model)
        {
            //获取登陆用户ID
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            return UserRoleDBHelper.InsertUserRole(model, loginUserID);
        }

        /// <summary>
        /// 登陆用户的角色
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="roleList">角色集</param>
        /// <returns>插入成功与否</returns>
        public static bool InsertUserRoleWithList(string userID, string companyCD, string[] roleList)
        {
            //获取登陆用户ID
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           //登陆日志
           try
           {
               bool succ = false;
               LogInfoModel logModel = InitLogInfo(roleList);
               logModel.Element = ConstUtil.LOG_PROCESS_INSERT;

               string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; // 待修改userrole
               ArrayList arr = new ArrayList();
               for (int i = 0; i < roleList.Length; i++)
               {
                   if (roleList[i] != "0")
                   {
                       arr.Add(roleList[i]);
                   }
               }
               string[] roleidlist = (string[])arr.ToArray(typeof(string));//讲array转换为string[]
               succ = UserRoleDBHelper.InsertUserRoleWithList(userID, companyCD, roleidlist, loginUserID);
               if (!succ)
                   logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
               else
                   logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
               for (int i = 0; i < roleList.Length; i++)
               {
                   if (roleList[i] != "0")
                   {
                       logModel.ObjectID = roleList[i];
                       LogDBHelper.InsertLog(logModel);
                   }
               }
                  
               return succ;
           }
           catch (Exception ex)
           {
               WriteSystemLog(userInfo, ex);
               return false;
           }
        }

        /// <summary>
        /// 角色与用户关联信息修改
        /// </summary>
        /// <param name="model">角色与用户关联信息</param>
        /// <returns>更新成功与否</returns>
        public static bool UpdateUserRole(UserRoleModel model)
        {
            //获取登陆用户ID
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            return UserRoleDBHelper.UpdateUserRole(model, loginUserID);
        }
        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static DataTable GetUserRoleInfo(string  UserID)
        {
            DataTable dt_UserInfo=null;
            if (!string.IsNullOrEmpty(UserID))
            {
                dt_UserInfo = UserRoleDBHelper.GetUserRoleInfo(UserID);
                return dt_UserInfo;
            }
            return dt_UserInfo;
        }
        /// <summary>
        /// 根据查询条件返回结果集
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="UserID"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static DataTable GetUserRoleByConditions(string RoleID, string UserID, string UserName, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return UserRoleDBHelper.GetUserRoleByConditions(companyCD, RoleID, UserID, UserName, pageIndex, pageCount, OrderBy, ref totalCount);
        }

    }
}
