/**********************************************
 * 类作用：   社会保险录入
 * 建立人：   吴志强
 * 建立时间： 2009/05/12
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

namespace XBase.Business.Office.HumanManager
{
    /// <summary>
    /// 类名：InsuEmployeeBus
    /// 描述：社会保险录入
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/12
    /// 最后修改时间：2009/05/12
    /// </summary>
    ///
    public class InsuEmployeeBus
    {

        #region 编辑社会保险录入信息
        /// <summary>
        /// 编辑社会保险录入信息
        /// </summary>
        /// <param name="lstEdit">社会保险录入信息</param>
        /// <returns></returns>
        public static bool SaveInsuEmployeeInfo(ArrayList lstEdit)
        {
            //定义返回变量
            bool isSucc = true;
            //信息存在时，进行操作
            if (lstEdit != null && lstEdit.Count > 0)
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //执行保存操作
                try
                {
                    //执行保存操作
                    isSucc = InsuEmployeeDBHelper.SaveInsuEmployeeInfo(lstEdit, userInfo.CompanyCD, userInfo.UserID);
                }
                catch (Exception ex)
                {
                    //输出系统日志
                    WriteSystemLog(userInfo, ex);
                }
                //操作日志
                LogInfoModel logModel = InitLogInfo(userInfo.CompanyCD);
                //设置关键元素
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

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
            }

            return isSucc;
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
            logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_SALARY_INPUT;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion

        #region 查询社会保险录入信息
        /// <summary>
        /// 查询社会保险录入信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetInsuEmployeeInfo()
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //执行查询
            return InsuEmployeeDBHelper.SearchInsuEmployeeInfo(userInfo.CompanyCD);
        }
        #endregion

        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <param name="no">申请编号</param>
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
            logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_SALARY_INPUT;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_INSU_SOCIAL_EMPL;//操作对象
            //
            logModel.ObjectID = no;

            return logModel;

        }
        #endregion
    }
}
