/**********************************************
 * 类作用：   角色管理处理层
 * 建立人：   吴成好
 * 建立时间： 2009/01/10
 ***********************************************/

using System;
using XBase.Model.Office.SystemManager;
using XBase.Data.Office.SystemManager;
using XBase.Common;
using System.Data;
using XBase.Business.Common;
using XBase.Data;
using XBase.Model.Common;
using XBase.Data.Common;
namespace XBase.Business.Office.SystemManager
{
    public class RoleInfoBus
    {
        /// <summary>
        /// 获取角色信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetRoleInfo(string CompanyCD)
        {
            try
            {
                return RoleInfoDBHelper.GetRoleInfo(CompanyCD);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetRoleInfoByID(int ID)
        {
            try
            {
                return RoleInfoDBHelper.GetRoleInfoByID(ID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取角色信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetRoleInfo(string CompanyCD, string RoleName, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return RoleInfoDBHelper.GetRoleInfo(CompanyCD, RoleName,pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取角色对应功能信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetRoleFunction(string CompanyCD, string RoleID, string Mod, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return RoleInfoDBHelper.GetRoleFunction(CompanyCD, RoleID, Mod, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #region 通过登录帐号，获取当前用户的角色,通过角色获取所赋予的菜单
        /// <summary>
        /// 通过登录帐号，获取当前用户的角色,通过角色获取所赋予的菜单
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static DataTable GetRoleModuleByUser(string CompanyCD, string UserID)
        {
            try
            {
                return RoleInfoDBHelper.GetRoleModuleByUser(CompanyCD, UserID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <returns>DataTable</returns>
        //public static void DeleteRoleInfo(string RoleIDArray)
        //{
        //    try
        //    {
        //        RoleInfoDBHelper.DeleteRoleInfo(RoleIDArray);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// 删除角色记录
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool DeleteRoleInfo(string RoleId, string CompanyCD)
        {

            if (string.IsNullOrEmpty(RoleId))
                return false;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //string CompanyCD = "AAAAAA";
            bool isSucc = RoleInfoDBHelper.DeleteRoleInfo(RoleId, CompanyCD);
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
            string[] noList = RoleId.Split(',');
            //遍历所有编号，登陆操作日志
            for (int i = 0; i < noList.Length; i++)
            {
                //获取编号
                string no = noList[i];
                //替换两边的 '
                no = no.Replace("'", string.Empty);

                //操作日志
                LogInfoModel logModel = InitLogInfo("角色ID：" + no);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                //设置操作成功标识
                logModel.Remark = remark;

                //登陆日志
                LogDBHelper.InsertLog(logModel);
            }
            return isSucc;



        }
        /// <summary>
        /// 角色信息插入
        /// </summary>
        /// <param name="model">角色信息</param>
        /// <returns>插入成功与否</returns>
        public static bool InsertRoleInfo(RoleInfoModel model,out string ID)
        {
            ID="0";
            //获取登陆用户ID
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;// 待修改
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //登陆日志
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.RoleName);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                succ = RoleInfoDBHelper.InsertRoleInfo(model, loginUserID, out ID);
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

        /// <summary>
        /// 角色信息修改
        /// </summary>
        /// <param name="model">角色信息</param>
        /// <returns>更新成功与否</returns>
        public static bool UpdateRoleInfo(RoleInfoModel model,int RoleID)
        {
            //获取登陆用户ID
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; //带修改
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //登陆日志
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.RoleName);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                succ = RoleInfoDBHelper.UpdateRoleInfo(model, loginUserID, RoleID);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        ///// <summary>
        ///// 根据角色名称获取角色ID
        ///// </summary>
        ///// <param name="CompanyCD"></param>
        ///// <returns></returns>
        //public static int GetRoleInfoId(string RoleName)
        //{
        //   string CompanyCD = "AAAAAA";                     ///待修改
        //    return RoleInfoDBHelper.GetRoleInfoId(CompanyCD, RoleName);
        //}
        
        /// <summary>
        /// 获取刚刚添加的角色ID
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMaxRoleId()
        {
            return RoleInfoDBHelper.GetMaxRoleId();
        }
        /// <summary>
        /// 获取公司最大的角色数
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetMaxRoleCount(string CompanyCD)
        {
            try
            {
                return RoleInfoDBHelper.GetMaxRoleCount(CompanyCD);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 获取部门角色信息
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 部门角色信息</returns>
        public static int[] GetRoleInfoArray(string userID, string companyCD)
        {
            //定义返回的角色数组
            int[] role = new int[0];
            //获取角色信息
            DataTable dtRole = LoginDBHelper.GetRoleInfo(userID, companyCD);
            //当角色存在的时候，设置角色到数组中
            if (dtRole != null && dtRole.Rows.Count > 0)
            {
                //获取角色个数
                int roleCount = dtRole.Rows.Count;
                role = new int[roleCount];
                //遍历所有角色，并设值
                for (int i = 0; i < roleCount; i++)
                {
                    //设置角色
                    role[i] = (int)dtRole.Rows[i]["RoleID"];
                }
            }
            return role;
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
            logModel.ModuleID = ConstUtil.Menu_AddRoleInfo;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_RoleInfo;
            //操作对象
            logModel.ObjectID = prodno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion
    }
}
