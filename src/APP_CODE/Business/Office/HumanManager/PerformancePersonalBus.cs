using System;
using XBase.Model.Office.HumanManager;
using XBase.Data.Office.HumanManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;
using System.Collections.Generic;
namespace XBase.Business.Office.HumanManager
{
  public   class PerformancePersonalBus
    {
        /// <summary>
        /// 保存评分项目信息
        /// </summary>
        /// <param name="model"> 评分项目实体</param>
        /// <returns></returns>
        public static bool SaveProPersonalInfo(PerformancePersonalModel  model)
        {
            
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //设置最后修改者
            model.ModifiedUserID = userInfo.UserID;
            //定义返回变量

            bool isSucc = false;
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.TaskNo);

            //更新
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {
                try
                {
                    logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                    //执行更新操作
                    isSucc = PerformancePersonalDBHelper.UpdatePerPersonalInfo (model);
                }
                catch (Exception ex)
                {
                    //输出系统日志
                    WriteSystemLog(userInfo, ex);
                }
            }
            //插入
            else
            {
                try
                {
                    logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                    //执行插入操作

                    isSucc =  PerformancePersonalDBHelper .InsertPerPersonalInfo (model);

                    logModel.ObjectID = model.TaskNo;
                }
                catch (Exception ex)
                {
                    //输出系统日志
                    WriteSystemLog(userInfo, ex);
                }
            }
            //更新成功时
            if (isSucc)
            {
                //设置操作成功标识
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

        public static bool UpdateProPersonalInfo(PerformancePersonalModel model)
        {

            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //设置最后修改者
            model.ModifiedUserID = userInfo.UserID;
            //定义返回变量

            bool isSucc = false;
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.TaskNo);

            //更新
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {
                try
                {
                    logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                    //执行更新操作
                    isSucc = PerformancePersonalDBHelper.CheckPerPersonalInfo (model);
                }
                catch (Exception ex)
                {
                    //输出系统日志
                    WriteSystemLog(userInfo, ex);
                }
            }
            //插入
            else
            {
                try
                {
                    logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                    //执行插入操作

                    isSucc = false ;

                    logModel.ObjectID = model.TaskNo;
                }
                catch (Exception ex)
                {
                    //输出系统日志
                    WriteSystemLog(userInfo, ex);
                }
            }
            //更新成功时
            if (isSucc)
            {
                //设置操作成功标识
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
        /// 获取设置为启用的模板信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetTemplateInfoo()
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string CompanyCD = userInfo.CompanyCD;
            //执行查询
            return PerformanceTaskDBHelper.GetTemplateInfo(CompanyCD);
        }
        /// <summary>
        /// 获取被模板设置的考核人信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetEmployeeInfo()
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string CompanyCD = userInfo.CompanyCD;
            //执行查询
            return PerformanceTaskDBHelper.GetEmployeeInfo(CompanyCD);
        }
        /// <summary>
        ///根据考核模板编号获取被考核人的列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetEmployeeInfoByTemplateNo(string templateNo)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string CompanyCD = userInfo.CompanyCD;
            //执行查询
            return PerformanceTaskDBHelper.GetEmployeeInfoByTemplateNo(templateNo, CompanyCD);
        }

        /// <summary>
        ///根据考核模板编号获取考核流程信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetTemplatebyNO(string templateNo)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string CompanyCD = userInfo.CompanyCD;
            //执行查询
            return PerformanceTaskDBHelper.GetTemplatebyNO(templateNo,"", CompanyCD);
        }
        /// <summary>
        /// 初始化日志
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        private static LogInfoModel InitLogInfo(string no)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID
            logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_PERFORMANCEPERSONALCHECK;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PERFORMANCPERSONAL;
            //操作单据编号
            logModel.ObjectID = no;

            return logModel;

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
            logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_PERFORMANCETASKCHECK;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion
        public static DataTable SearchTaskInfo(PerformancePersonalModel  model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询
            return PerformancePersonalDBHelper.SearchTaskInfo(model);

        }
        public static DataTable SearchTaskInfoByTaskNO(string taskNo)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行查询
            return PerformancePersonalDBHelper.SearchTaskInfoByTaskNO(companyCD, taskNo);

        }
        public static bool IsExist(string TaskNo)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行删除操作
            bool isSucc = PerformancePersonalDBHelper.IsExist(TaskNo, companyCD);

            return isSucc;
        }
        public static bool IsCheck(string TaskNo)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行删除操作
            bool isSucc = PerformancePersonalDBHelper.IsCheck(TaskNo, companyCD);

            return isSucc;
        }
        public static bool DeletePerTypeInfo(string elemID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行删除操作
            bool isSucc = PerformancePersonalDBHelper.DeletePerTypeInfo(elemID, companyCD);
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

            //操作日志
            LogInfoModel logModel = InitLogInfo(elemID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;
        }
    }
}
