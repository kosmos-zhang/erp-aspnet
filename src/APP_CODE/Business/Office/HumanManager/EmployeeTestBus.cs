/**********************************************
 * 类作用：   考试记录表格操作
 * 建立人：   吴志强
 * 建立时间： 2009/04/08
 ***********************************************/
using System;
using XBase.Model.Office.HumanManager;
using XBase.Data.Office.HumanManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;

namespace XBase.Business.Office.HumanManager
{
    /// <summary>
    /// 类名：EmployeeTestBus
    /// 描述：考试记录表格操作
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/08
    /// 最后修改时间：2009/04/08
    /// </summary>
    ///
    public class EmployeeTestBus
    {

        #region 编辑考试记录信息
        /// <summary>
        /// 编辑考试记录信息
        /// </summary>
        /// <param name="model">保存信息</param>
        /// <returns></returns>
        public static bool SaveTestInfo(EmployeeTestModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            model.ModifiedUserID = userInfo.UserID;
            //定义返回变量
            bool isSucc = false;
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.TestNo);

            //更新
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {
                try
                {
                    logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                    //执行更新操作
                    isSucc = EmployeeTestDBHelper.UpdateEmployeeTestInfo(model);
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
                    isSucc = EmployeeTestDBHelper.InsertEmployeeTestInfo(model);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_TEST_EDIT;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion

        #region 通过ID查询考试信息
        /// <summary>
        /// 查询考试信息
        /// </summary>
        /// <param name="testID">考试ID</param>
        /// <returns></returns>
        public static DataSet GetTestInfoWithID(string testID)
        {
            return EmployeeTestDBHelper.GetTestInfoWithID(testID);
        }
        #endregion

        #region 通过ID查询考试信息
        /// <summary>
        /// 查询考试信息
        /// </summary>
        /// <param name="testID">考试ID</param>
        /// <returns></returns>
        public static DataSet GetTestInfoByID(string testID)
        {
            return EmployeeTestDBHelper.GetTestInfoByID(testID);
        }

        public static DataSet GetTestInfoByNO(string NO, string CompanyCD)
        {
            return EmployeeTestDBHelper.GetTestInfoByNO(NO, CompanyCD);
        }

        #endregion

        #region 通过检索条件查询培训考核信息
        /// <summary>
        /// 查询培训考核信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchTestInfo(EmployeeTestSearchModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            return EmployeeTestDBHelper.SearchTestInfo(model);
        }
        #endregion

        #region 删除考试信息
        /// <summary>
        /// 删除考试信息
        /// </summary>
        /// <param name="testNo">考试编号</param>
        /// <returns></returns>
        public static bool DeleteTestInfo(string testNo)
        {
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //执行删除操作
            bool isSucc = EmployeeTestDBHelper.DeleteTestInfo(testNo, companyCD);
            //获取删除的编号列表
            string[] asseNoList = testNo.Split(',');
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
            //遍历所有编号，登陆操作日志
            for (int i = 0; i < asseNoList.Length; i++)
            {
                //获取编号
                string no = asseNoList[i];
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

            return isSucc;
        }
        #endregion

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
            logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_TEST_EDIT;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_TEST;
            //操作单据编号
            logModel.ObjectID = no;

            return logModel;

        }
        #endregion

        public static DataTable EmployeeExaminationReportInfo(EmployeeTestSearchModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            return EmployeeTestDBHelper.EmployeeExaminationReportInfo(model);
        }
        public static string  getDept(string DeptID  )
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
          string  CompanyCD = userInfo.CompanyCD;
          return EmployeeTestDBHelper.getDept(DeptID, CompanyCD);
        }
        /// <summary>
        /// 根据日期和部门，返回部门招聘人数
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="monthStartDate"></param>
        /// <param name="monthEndDate"></param>
        /// <returns></returns>
        public static DataTable GetRequireNum(  string DeptID,string monthStartDate,string monthEndDate)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
             string  CompanyCD = userInfo.CompanyCD;
             return EmployeeTestDBHelper.GetRequireNum(DeptID, monthStartDate,monthEndDate, CompanyCD);
        }
        /// <summary>
        /// 根据日期和部门，返回部门面试人数
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="monthStartDate"></param>
        /// <param name="monthEndDate"></param>
        /// <returns></returns>
        public static DataTable GetInterviewNum(string DeptID, string monthStartDate, string monthEndDate)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string CompanyCD = userInfo.CompanyCD;
            return EmployeeTestDBHelper.GetInterviewNum(DeptID, monthStartDate, monthEndDate, CompanyCD);
        }
        /// <summary>
        /// 根据日期和部门，返回部门报道人数
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="monthStartDate"></param>
        /// <param name="monthEndDate"></param>
        /// <returns></returns>
        public static DataTable GetReportedNum(string DeptID, string monthStartDate, string monthEndDate)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string CompanyCD = userInfo.CompanyCD;
            return EmployeeTestDBHelper.GetReportedNum(DeptID, monthStartDate, monthEndDate, CompanyCD);
        }
        /// <summary>
        /// 根据日期和部门，返回部门迟到人数
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="monthStartDate"></param>
        /// <param name="monthEndDate"></param>
        /// <returns></returns>
        public static DataTable GetLatedNum(string DeptID, string monthStartDate, string monthEndDate)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string CompanyCD = userInfo.CompanyCD;
            return EmployeeTestDBHelper.GetLatedNum(DeptID, monthStartDate, monthEndDate, CompanyCD);
        }

        /// <summary>
        /// 根据日期和部门，返回部门早退人数
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="monthStartDate"></param>
        /// <param name="monthEndDate"></param>
        /// <returns></returns>
        public static DataTable GetLeaveEarlyNum(string DeptID, string monthStartDate, string monthEndDate)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string CompanyCD = userInfo.CompanyCD;
            return EmployeeTestDBHelper.GetLeaveEarlyNum(DeptID, monthStartDate, monthEndDate, CompanyCD);
        }
        /// <summary>
        /// 根据日期和部门，返回部门旷工人次
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="monthStartDate"></param>
        /// <param name="monthEndDate"></param>
        /// <returns></returns>
        public static DataTable GetAbsenteeismNum(string DeptID, string monthStartDate, string monthEndDate)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string CompanyCD = userInfo.CompanyCD;
            return EmployeeTestDBHelper.GetAbsenteeismNum(DeptID, monthStartDate, monthEndDate, CompanyCD);
        }
        /// <summary>
        /// 根据日期和部门，返回部门请假次数
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="monthStartDate"></param>
        /// <param name="monthEndDate"></param>
        /// <returns></returns>
        public static DataTable GetLeaveNum(string DeptID, string monthStartDate, string monthEndDate)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string CompanyCD = userInfo.CompanyCD;
            return EmployeeTestDBHelper.GetLeaveNum(DeptID, monthStartDate, monthEndDate, CompanyCD);
        }
        /// <summary>
        /// 获取公司部门
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDeptInfo( string DeptID)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string CompanyCD = userInfo.CompanyCD;
            return EmployeeTestDBHelper.GetDeptInfo(CompanyCD,DeptID );
        }
        /// <summary>
        /// 部门调入人数
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="monthStartDate"></param>
        /// <param name="monthEndDate"></param>
        /// <returns></returns>
        public static DataTable GetDeptInNum(string DeptID, string monthStartDate, string monthEndDate)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string CompanyCD = userInfo.CompanyCD;
            return EmployeeTestDBHelper.GetDeptInNum(DeptID, monthStartDate, monthEndDate, CompanyCD);
        }
        /// <summary>
        /// 部门调出人数
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="monthStartDate"></param>
        /// <param name="monthEndDate"></param>
        /// <returns></returns>
        public static DataTable GetDeptOutNum(string DeptID, string monthStartDate, string monthEndDate)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string CompanyCD = userInfo.CompanyCD;
            return EmployeeTestDBHelper.GetDeptOutNum(DeptID, monthStartDate, monthEndDate, CompanyCD);
        }
        /// <summary>
        ///部门 离职人数
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="monthStartDate"></param>
        /// <param name="monthEndDate"></param>
        /// <returns></returns>
        public static DataTable GetSeparationNum(string DeptID, string monthStartDate, string monthEndDate)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string CompanyCD = userInfo.CompanyCD;
            return EmployeeTestDBHelper.GetSeparationNum(DeptID, monthStartDate, monthEndDate, CompanyCD);
        }
    }
}
