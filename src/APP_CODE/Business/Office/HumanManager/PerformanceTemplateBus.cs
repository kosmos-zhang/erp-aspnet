/**********************************************
 * 类作用：   考核模板业务处理
 * 建立人：   王保军
 * 建立时间： 2009/05/07
 ***********************************************/
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
    public class PerformanceTemplateBus
    {
        /// <summary>
        ///获取UsedStatus为可用（1）模板列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPerformanceElemList()
        {


            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行查询
            return PerformanceTemplateDBHelper.GetPerformanceElemList(companyCD);
        }
        public static bool IsExist(string TemplateNo)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行删除操作
            bool isSucc = PerformanceTemplateDBHelper.IsExist(TemplateNo, companyCD);

            return isSucc;
        }

        /// <summary>
        /// 获取考核类型表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPerformanceType()
        {


            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行查询
            return PerformanceTemplateDBHelper.GetPerformanceType(companyCD);
        }
        public static bool InsertPerformenceTemplate(IList<PerformanceTemplateElemModel> modeList, PerformanceTemplateModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            bool isSucc = false;
            //操作日志
            LogInfoModel logModel = InitLogInfo(modeList[0].ModifiedUserID);

            //更新
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(modeList[0].EditFlag))
            {
                try
                {
                    logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                    //执行更新操作
                    //  isSucc = PerformanceTemplateEmpDBHelper.UpdatePerformenceTempElm(modeList, templateList);
                    if (PerformanceTemplateDBHelper.UpdatePerformenceTemplate(model))
                    {
                        if (PerformanceTemplateDBHelper.DeletePerformenceTemElem(model))
                        {
                            if (PerformanceTemplateDBHelper.InsertPerformenceTempElm(modeList))
                            {
                                isSucc = true;


                            }
                            else
                            {
                                isSucc = false;
                            }
                        }
                        else
                        {

                            isSucc = false;
                        }

                    }
                    else
                    {
                        isSucc = false;
                    }






                    logModel.ObjectID = modeList[0].ModifiedUserID;
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

                    if (PerformanceTemplateDBHelper.InsertPerformenceTemplate(model))
                    {

                        if (PerformanceTemplateDBHelper.InsertPerformenceTempElm(modeList))
                        {
                            isSucc = true;


                        }
                        else
                        {
                            isSucc = false;
                        }

                    }
                    else
                    {
                        isSucc = false;
                    }

                    logModel.ObjectID = modeList[0].ModifiedUserID;
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

        public static DataTable SearchFlowInfo(PerformanceTemplateModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询

            return PerformanceTemplateDBHelper.SearchTemplateInfo(model);

        }
        public static DataTable GetPerformanceElemInfo(PerformanceTemplateModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询

            return PerformanceTemplateDBHelper.GetPerformanceElemInfo(model);

        }

        public static bool IsTemplateUsed(string TemplateNo)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行删除操作
            bool isSucc = PerformanceTemplateDBHelper.IsTemplateUsed(TemplateNo, companyCD);

            return isSucc;
        }
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
            logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_PERFORMANCECHECK;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PERFORMANCETEMPLATE;
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
            logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_PERFORMANCECHECK;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion

        public static bool DeletePerTemplateInfo(string TemplateNo)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行删除操作
            bool isSucc = PerformanceTemplateDBHelper.DeletePerTemplateInfo(TemplateNo, companyCD);
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
            LogInfoModel logModel = InitLogInfo(TemplateNo);
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
