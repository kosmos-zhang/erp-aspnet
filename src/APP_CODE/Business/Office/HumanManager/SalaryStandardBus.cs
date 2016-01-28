/**********************************************
 * 类作用：   工资标准设置
 * 建立人：   吴志强
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
using System.Collections;

namespace XBase.Business.Office.HumanManager
{
    /// <summary>
    /// 类名：SalaryStandardBus
    /// 描述：工资标准设置
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/07
    /// 最后修改时间：2009/05/07
    /// </summary>
    ///
    public class SalaryStandardBus
    {
        #region 通过检索条件查询工资标准信息
        /// <summary>
        /// 查询工资标准信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchSalaryStandardInfo(SalaryStandardModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询
            return SalaryStandardDBHelper.SearchSalaryStandardInfo(model);
        }
        #endregion
        #region 查询工资标准产生报表信息
        /// <summary>
        /// 查询工资标准信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchSalaryStandardReport(SalaryStandardModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询
            return SalaryStandardDBHelper.SearchSalaryStandardReport(model);
        }
        #endregion
        #region 查询产生工资汇总报表信息
        /// <summary>
        /// 查询工资标准信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchSalarySummaryReport(SalaryStandardModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询
            return SalaryStandardDBHelper.SearchSalarySummaryReport(model);
        }
        #endregion
        #region 查询产生工资明细报表
        /// <summary>
        /// 查询工资标准信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchSalaryDetailsReport(SalaryStandardModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询
            return SalaryStandardDBHelper.SearchSalaryDetailsReport(model);
        }
        #endregion
        #region 通过员工ID产生返回工资信息
        /// <summary>
        /// 通过员工ID产生返回工资信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable GetEmployeeDetails(string employeeID, string deptID, string ReprotNo)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string companyCD = userInfo.CompanyCD;
            //执行查询
            return SalaryStandardDBHelper.GetEmployeeDetails(employeeID, deptID, companyCD,  ReprotNo);
        }
        #endregion
        #region 通过月份和年度和部门ID产生返回实发工资人数、实发工资合计
        /// <summary>
        /// 通过月份和年度和部门ID产生返回实发工资人数、实发工资合计
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable GetMonthlyInfo(string year, string deptID,string month)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string companyCD = userInfo.CompanyCD;
            //执行查询
            return SalaryStandardDBHelper.GetMonthlyInfo(year, deptID,month, companyCD);
        }
        #endregion
        #region 返回已确认的报表的部门ID序列
        /// <summary>
        ///  返回已确认的报表的部门ID序列
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable GetDeptInfo()
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string companyCD = userInfo.CompanyCD;
            //执行查询
            return SalaryStandardDBHelper.GetDeptInfo(companyCD);
        }
        #endregion
        #region 通过员工ID产生返回员工计时金额
        /// <summary>
        /// 通过员工ID产生返回工资信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable GetEmployeeDetailsOutTime(string employeeID, string deptID,string ReprotNo )
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string companyCD = userInfo.CompanyCD;
            //执行查询
            return SalaryStandardDBHelper.GetEmployeeDetailsOutTime(employeeID, deptID, companyCD, ReprotNo);
        }
        #endregion
        #region 通过员工ID产生返回员工计件金额
        /// <summary>
        /// 通过员工ID产生返回工资信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable GetEmployeeDetailsOutPiece(string employeeID, string deptID, string ReprotNo)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string companyCD = userInfo.CompanyCD;
            //执行查询
            return SalaryStandardDBHelper.GetEmployeeDetailsOutPiece(employeeID, deptID, companyCD, ReprotNo);
        }
        #endregion

        #region 根据部门ID获取所在部门名称
        /// <summary>
        /// 根据部门ID获取所在部门名称
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static string GetNameByDeptID(string DeptID)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string companyCD = userInfo.CompanyCD;
            //执行查询
            return SalaryStandardDBHelper.GetNameByDeptID(DeptID, companyCD);
        }
        #endregion

        #region 校验工资标准信息
        /// <summary>
        /// 校验工资标准信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static bool CheckSalaryStandardInfo(SalaryStandardModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询
            return SalaryStandardDBHelper.CheckSalaryStandardInfo(model);
        }
        #endregion

        #region 编辑工资标准信息
        /// <summary>
        /// 编辑工资标准信息
        /// </summary>
        /// <param name="model">保存信息</param>
        /// <returns></returns>
        public static bool SaveSalaryStandard(SalaryStandardModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //定义返回变量
            bool isSucc = false;
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.ID);

            //更新
            if (!string.IsNullOrEmpty(model.ID))
            {
                try
                {
                    logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                    //执行更新操作
                    isSucc = SalaryStandardDBHelper.UpdateSalaryStandard(model);
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
                    isSucc = SalaryStandardDBHelper.InsertSalaryStandard(model);
                    //操作单据编号
                    logModel.ObjectID = model.ID;
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
            logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_SALARY_SET;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion

        #region 删除工资标准信息
        /// <summary>
        /// 删除工资标准信息
        /// </summary>
        /// <param name="standardID">工资标准ID</param>
        /// <returns></returns>
        public static bool DeleteSalaryStandard(string standardID)
        {
            //执行删除操作
            bool isSucc = SalaryStandardDBHelper.DeleteSalaryStandard(standardID);
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
            LogInfoModel logModel = InitLogInfo(standardID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;
        }
        #endregion
        public static bool DeleteAllSalaryInfo(string standardID)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行删除操作
            bool isSucc = SalaryStandardDBHelper.DeleteAllSalaryInfo (standardID,companyCD );
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
            LogInfoModel logModel = InitLogInfo(standardID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;
        }
        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
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
            logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_SALARY_SET;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_SALARY_STANDARD;
            //操作单据编号
            logModel.ObjectID = no;

            return logModel;

        }
        #endregion
    }
}
