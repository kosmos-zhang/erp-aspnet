/**********************************************
 * 类作用：   人员薪资结构设置
 * 建立人：   肖合明
 * 建立时间： 2009/09/07
 ***********************************************/
using System;
using XBase.Model.Office.HumanManager;
using XBase.Data.Office.HumanManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;
using System.Collections;
using System.Collections.Generic;


namespace XBase.Business.Office.HumanManager
{
    public class SalaryEmployeeStructureSetBus
    {

        #region 通过人员ID获取当前的薪资结构设置
        /// <summary>
        /// 通过人员ID获取当前的薪资结构设置
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <param name="CompanyCD">防止其他公司的人员通过改参数获取信息</param>
        /// <returns></returns>
        public static DataTable GetUserInfo(string EmployeeID)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                return SalaryEmployeeStructureSetDBHelper.GetUserInfo(EmployeeID, userInfo.CompanyCD);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


        #region 执行保存操作

        /// <summary>
        /// 执行保存操作
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="ModelList"></param>
        /// <returns></returns>
        public static bool SaveInfo(string EmployeeID, SalaryEmployeeStructureSetModel Model)
        {
            //定义返回变量
            bool isSucc = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            try
            {
                isSucc = SalaryEmployeeStructureSetDBHelper.SaveInfo(EmployeeID, Model);
            }
            catch (Exception ex)
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

                //输出日志
                WriteSystemLog(userInfo, ex);
            }
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
            //操作日志
            LogInfoModel logModel = InitLogInfo(EmployeeID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }
        #endregion

        #region 获取人员信息
        public static DataTable GetUserInfo()
        {
            try
            {
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return SalaryEmployeeStructureSetDBHelper.GetUserInfo(companyCD);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取部门信息
        public static DataTable GetDeptInfoByCompanyCD()
        {
            try
            {
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                return UserDeptSelectDBHelper.GetDeptInfo(companyCD);

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
            logSys.ModuleID = "2011701";
            //logSys.ModuleID = ConstUtil.MODULE_ID_STORAGE_STORAGELOSS_ADD;
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
        private static LogInfoModel InitLogInfo(string ID)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护

            logModel.ModuleID = "2011701";
            //设置操作日志类型 修改
            logModel.ObjectName = "SalaryEmployeeStructureSet";
            //操作对象
            logModel.ObjectID = ID;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion
    }
}
