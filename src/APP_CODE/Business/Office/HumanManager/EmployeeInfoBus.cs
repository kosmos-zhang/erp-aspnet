/**********************************************
 * 类作用：   员工信息事务层处理
 * 建立人：   吴志强
 * 建立时间： 2009/03/09
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
    /// 类名：EmployeeInfoBus
    /// 描述：员工信息事务层处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/09
    /// 最后修改时间：2009/03/09
    /// </summary>
    ///
    public class EmployeeInfoBus
    {

        #region 查询人员信息

        #region 通过人员ID查询人员信息
        /// <summary>
        /// 通过人员ID查询人员信息
        /// </summary>
        /// <param name="employeeID">人员ID</param>
        /// <returns></returns>
        public static EmployeeInfoModel GetEmployeeInfoWithID(int employeeID)
        {
            //查询并返回人员信息
            return EmployeeInfoDBHelper.GetEmployeeInfoWithID(employeeID);
        }
        #endregion

        #region 查询在职人员列表
        /// <summary>
        /// 查询在职人员列表
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchEmployeeWorkInfo(EmployeeSearchModel model, int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            return EmployeeInfoDBHelper.SearchEmployeeWorkInfo(model, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 查询离职人员列表
        /// <summary>
        /// 查询离职人员列表
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchEmployeeLeaveInfo(EmployeeSearchModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            return EmployeeInfoDBHelper.SearchEmployeeLeaveInfo(model);
        }
        #endregion

        #region 查询人才储备列表
        /// <summary>
        /// 查询人才储备列表
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchEmployeeReserveInfo(EmployeeSearchModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            return EmployeeInfoDBHelper.SearchEmployeeReserveInfo(model);
        }
        #endregion

        #region 查询人力档案回收站
        /// <summary>
        /// 查询人力档案回收站
        /// </summary>
        /// <param name="searchModel">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchEmployeeCallBack(EmployeeSearchModel model,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            return EmployeeInfoDBHelper.SearchEmployeeCallBack(model,pageIndex,pageCount,ord,ref TotalCount);
        }
        #endregion

        #region 查询待入职人员列表
        /// <summary>
        /// 查询待入职人员列表
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchWaitEnterInfo(EmployeeSearchModel model,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            return EmployeeInfoDBHelper.SearchWaitEnterInfo(model, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #endregion

        #region 编辑人员信息
        /// <summary>
        /// 编辑人员信息
        /// </summary>
        /// <param name="model">人员信息</param>
        /// <returns></returns>
        public static bool SaveEmployeeInfo(EmployeeInfoModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //定义返回变量
            bool isSucc = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.EmployeeNo);

            //ID存在时，更新
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {
                try
                {
                    //设置操作日志类型 修改
                    logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;//操作对象

                    //执行更新操作
                    isSucc = EmployeeInfoDBHelper.UpdateEmployeeInfo(model);
                }
                catch (Exception ex)
                {
                    //输出日志
                    WriteSystemLog(userInfo, ex);
                }
            }
            //插入
            else
            {
                try
                {
                    //设置操作日志类型 新建
                    logModel.Element = ConstUtil.LOG_PROCESS_INSERT;//操作对象
                    //执行插入操作
                    isSucc = EmployeeInfoDBHelper.InsertEmployeeInfo(model);
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
                //设置操作成功标识
                logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;

                //操作前相片路径
                //string photoUrl = model.PhotoURL;
                //相片存在时，删除相片
                //if (!string.IsNullOrEmpty(photoUrl) && !model.PhotoURL.Equals(model.PagePhotoURL))
                //{
                //    //删除文件 System.Web.HttpContext.Current.Server.MapPath("Photo") + 

                //    FileUtil.DeleteFile(photoUrl);
                //}
                //操作前简历路径
                string resumeUrl = model.Resume;
                //简历存在时，删除简历
                if (!string.IsNullOrEmpty(resumeUrl) && !model.Resume.Equals(model.PageResume))
                {
                    //删除文件
                    FileUtil.DeleteFile(resumeUrl);
                }
            }
            //更新不成功
            else
            {
                //设置操作成功标识 
                logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;

                ////删除上传的相片 新的相片路径
                //string pagePhotoUrl = model.PagePhotoURL;
                ////相片存在时，删除相片
                //if (!string.IsNullOrEmpty(pagePhotoUrl))
                //{
                //    //删除文件
                //    FileUtil.DeleteFile(ConstUtil.SERVER_PATH + "\\" + pagePhotoUrl);
                //}
                ////操作前简历路径
                //string pageResumeUrl = model.PageResume;
                ////简历存在时，删除简历
                //if (!string.IsNullOrEmpty(pageResumeUrl))
                //{
                //    //删除文件
                //    FileUtil.DeleteFile(ConstUtil.SERVER_PATH + "\\" + pageResumeUrl);
                //}
            }

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;
        }
        #endregion

        #region 删除人员信息
        /// <summary>
        /// 删除人员信息
        /// </summary>
        /// <param name="employeeNo">人员编号</param>
        /// <returns></returns>
        public static bool DeleteEmployeeInfo(string employeeNo)
        {
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            //执行删除操作
            bool isSucc = EmployeeInfoDBHelper.DeleteEmployeeInfo(employeeNo, companyCD);

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
            string[] noList = employeeNo.Split(',');
            //遍历所有编号，登陆操作日志
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

            return isSucc;
        }
        #endregion

        #region 还原人员信息
        /// <summary>
        /// 还原人员信息
        /// </summary>
        /// <param name="employeeNo">人员编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool CallBack(string employeeNo)
        {
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //执行删除操作
            return EmployeeInfoDBHelper.CallBack(employeeNo, companyCD);
        }
        #endregion

        #region 彻底删除人员信息
        /// <summary>
        /// 彻底删除人员信息
        /// </summary>
        /// <param name="employeeNo">人员编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteEmp(string employeeNo)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return EmployeeInfoDBHelper.DeleteEmp(employeeNo, companyCD);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_ADD;
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
        private static LogInfoModel InitLogInfo(string employeeNo)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_EMPLOYYEEINFO;
            //操作对象
            logModel.ObjectID = employeeNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            //logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        #region 通过人员ID查询在职相关信息
        /// <summary>
        /// 通过人员ID查询在职相关信息
        /// </summary>
        /// <param name="employeeID">人员ID</param>
        /// <returns></returns>
        public static DataTable GetEmplDeptInfoWithID(string employeeID)
        {
            //查询并返回人员信息
            return EmployeeInfoDBHelper.GetEmplDeptInfoWithID(employeeID);
        }
        #endregion

        #region 通过检索条件查询在职人员相关信息
        /// <summary>
        /// 通过检索条件查询在职人员相关信息
        /// </summary>
        /// <param name="model">检索条件</param>
        /// <returns></returns>
        public static DataTable SearchEmplInfo(EmployeeSearchModel model)
        {
            //查询并返回人员信息
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询并返回值
            return EmployeeInfoDBHelper.SearchEmplInfo(model);
        }
        #endregion

        #region 入职处理更新数据库
        /// <summary>
        /// 入职处理更新数据库
        /// </summary>
        /// <param name="model">人员信息</param>
        /// <returns></returns>
        public static bool UpdateEnterInfo(EmployeeSearchModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //定义返回变量
            bool isSucc = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.EmployeeNo);
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.LOG_PROCESS_UPDATE;
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

            try
            {
                //执行更新
                isSucc = EmployeeInfoDBHelper.UpdateEnterInfo(model);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
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

            //返回
            return isSucc;
        }
        #endregion

        #region 查询在职部门岗位等相关信息
        /// <summary>
        /// 查询在职部门岗位等相关信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetWorkEmplInfo(EmployeeSearchModel model)
        {

            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //查询并返回人员信息
            return EmployeeInfoDBHelper.GetWorkEmplInfo(model);
        }
        #endregion

         /// <summary>
        /// 获取联系方式和一些基本信息
        /// </summary>
        /// <param name="idList">ID列表</param>
        /// <returns></returns>
        public static DataTable GetContact(string idList)
        {
            return EmployeeInfoDBHelper.GetContact(idList);
        }

        #region 根据ID获取员工手机号码的方法
        /// <summary>
        /// 根据ID获取员工手机号码的方法
        /// </summary>
        /// <param name="idList">ID列表</param>
        /// <returns>手机号码列表</returns>
        public static DataTable GetModileByID(string idList)
        {
            return EmployeeInfoDBHelper.GetModileByID(idList);
        }
        #endregion
         
        #region 在职员工性别统计
        /// <summary>
        /// 在职员工性别统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetEmployeeSex(string CompanyCD, string DeptID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return EmployeeInfoDBHelper.GetEmployeeSex(CompanyCD, DeptID, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 在职员工性别统计打印
        /// <summary>
        /// 在职员工性别统计打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetEmployeeSexPrint(string CompanyCD, string DeptID, string ord)
        {
            return EmployeeInfoDBHelper.GetEmployeeSexPrint(CompanyCD, DeptID, ord);
        }
        #endregion

        #region 人员信息打印
        /// <summary>
        /// 人员信息打印
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static DataTable PrintEmployee(string CompanyCD,string EmployeeNo)
        {
            return EmployeeInfoDBHelper.PrintEmployee(CompanyCD, EmployeeNo);
        }
        #endregion

        #region 工作经历/教育经历打印
        /// <summary>
        /// 工作经历/教育经历打印
        /// </summary>
        /// <param name="companyCD"></param>
        /// <param name="employeeNo"></param>
        /// <returns></returns>
        public static DataTable PrintHistory(string companyCD, string employeeNo)
        {
            return EmployeeInfoDBHelper.PrintHistory(companyCD, employeeNo);
        }
        #endregion

        #region 技能信息打印
        /// <summary>
        /// 技能信息打印
        /// </summary>
        /// <param name="companyCD"></param>
        /// <param name="employeeNo"></param>
        /// <returns></returns>
        public static DataTable PrintSkill(string companyCD, string employeeNo)
        {
            return EmployeeInfoDBHelper.PrintSkill(companyCD, employeeNo);
        }
        #endregion

        #region 导出在职人员列表
        /// <summary>
        /// 导出在职人员列表
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportEmployeeWorkInfo(EmployeeSearchModel model,string BeginDate,string EndDate, string ord)
        {
            try
            { 
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置公司代码
                model.CompanyCD = userInfo.CompanyCD;
                return EmployeeInfoDBHelper.ExportEmployeeWorkInfo(model,BeginDate,EndDate,ord);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 导出人员储备列表
        /// <summary>
        /// 导出人员储备列表
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static DataTable ExportEmployeeReserve(EmployeeSearchModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;

            return EmployeeInfoDBHelper.ExportEmployeeReserve(model);
        }
        #endregion

        public static DataSet ReadEexcel(string FilePath,string companycd)
        {
            return EmployeeInfoDBHelper.ReadEexcel(FilePath,companycd);
        }

        public static bool ChargeQuarterInfo(string codename,string quarterno,string deptName,string deptNo, string compid)
        {
            return EmployeeInfoDBHelper.ChargeQuarterInfo(codename,quarterno,deptName,deptNo, compid);
        }
        public static DataTable ChargeQuarterInfoNum(string codename,string quarterno, string deptName, string deptNo, string compid)
        {
            return EmployeeInfoDBHelper.ChargeQuarterInfoNum(codename,quarterno, deptName, deptNo, compid);
        }

        public static int ChargeDeptInfoNum(string codename,string deptno, string compid)
        {
            return EmployeeInfoDBHelper.ChargeDeptInfoNum(codename,deptno, compid);
        }

        public static DataTable GetDeptInfo(string codename, string compid)
        {
            return EmployeeInfoDBHelper.GetDeptInfo(codename, compid);
        }

        public static bool ChargeDeptInfo(string codename,string deptno, string compid)
        {
            return EmployeeInfoDBHelper.ChargeDeptInfo(codename,deptno, compid);
        }

        public static bool ChargeHyInfo(string codename, string compid)
        {
            return EmployeeInfoDBHelper.ChargeHyInfo(codename, compid);
        }

        public static bool ChargeEmployeeInfo(string codename, string compid)
        {
            return EmployeeInfoDBHelper.ChargeEmployeeInfo(codename, compid);
        }

        public static int GetExcelToEmployeeInfo(string companycd)
        {
            return EmployeeInfoDBHelper.GetExcelToEmployeeInfo(companycd);
        }

        public static DataSet GetNullEmployeeList(string companycd)
        {
            return EmployeeInfoDBHelper.GetNullEmployeeList(companycd);
        }

        public static int GetCodeRuleID(string companycd)
        {
            return EmployeeInfoDBHelper.GetCodeRuleID(companycd);
        }

        public static void UpdateEmployeeInfo(string companycd, string employeeNo, string ID)
        {
            EmployeeInfoDBHelper.UpdateEmployeeInfo(companycd, employeeNo, ID);
        }
    }
}
