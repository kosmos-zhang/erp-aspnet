/**********************************************
 * 类作用：   组织机构表格操作
 * 建立人：   吴志强
 * 建立时间： 2009/04/09
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
    /// 类名：DeptInfoBus
    /// 描述：组织机构表格操作
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/09
    /// 最后修改时间：2009/04/09
    /// </summary>
    ///
    public class DeptInfoBus
    {

        #region 根据主键获取部门名称
        /// <summary>
        /// 根据主键获取部门名称
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>部门名称</returns>
        public static string GetDeptNameByID(string ID)
        {
             

            if (string.IsNullOrEmpty(ID)) return ""; 
            DataTable dt = DeptInfoDBHelper.GetDeptNameByID(ID );
            string DeptName = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                DeptName = dt.Rows[0]["DeptName"].ToString();
            }
            return DeptName;
        }
        #endregion
        #region 根据部门编号获取部门信息
        /// <summary>
        /// 根据主键获取部门名称
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>部门名称</returns>
        public static DataTable  GetDeptinfoByNo(string deptNo)
        {
             return  DeptInfoDBHelper.GetDeptinfoByNo(deptNo);
        }
        #endregion

        #region 编辑组织机构信息
        /// <summary>
        /// 编辑组织机构信息
        /// </summary>
        /// <param name="model">保存信息</param>
        /// <returns></returns>
        public static bool SaveDeptInfo(DeptModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            model.ModifiedUserID = userInfo.UserID;
            //定义返回变量
            bool isSucc = false;
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.DeptNO);

            //更新
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {
                try
                {
                    logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                    //执行更新操作
                    isSucc = DeptInfoDBHelper.UpdateDeptInfo(model);
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
                    isSucc = DeptInfoDBHelper.InsertDeptInfo(model);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_DEPT_EDIT;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion

        #region 通过ID查询组织机构信息
        /// <summary>
        /// 查询组织机构信息
        /// </summary>
        /// <param name="deptID">组织机构ID</param>
        /// <returns></returns>
        public static DataTable GetDeptInfoWithID(string deptID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            return DeptInfoDBHelper.GetDeptInfoWithID(deptID,companyCD );
        }
        #endregion

        #region 通过检索条件查询组织机构信息
        /// <summary>
        /// 查询组织机构信息
        /// </summary>
        /// <param name="deptID">组织机构ID</param>
        /// <returns></returns>
        public static DataTable SearchDeptInfo(string deptID)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行查询
            return DeptInfoDBHelper.SearchDeptInfo(companyCD, deptID);
        }

        #endregion
        /// <summary>
        /// 是否被岗位表里引用
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public static DataTable ISDequter(string deptID)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行查询
            return DeptInfoDBHelper.ISDequter(companyCD, deptID);
        }
        /// <summary>
        /// 判断此部门是否有人员
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public static int  ISHavePerson(string deptID)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行查询
            return DeptInfoDBHelper.ISHavePerson(companyCD, deptID);
        }
        #region 删除组织机构信息
        /// <summary>
        /// 删除组织机构信息
        /// </summary>
        /// <param name="deptID">组织机构ID</param>
        /// <returns></returns>
        public static bool DeleteDeptInfo(string deptID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码

            string CompanyCD = userInfo.CompanyCD;
            //执行删除操作
            bool isSucc = DeptInfoDBHelper.DeleteDeptInfo(deptID,CompanyCD );
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
            LogInfoModel logModel = InitLogInfo(deptID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

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
            logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_DEPT_EDIT;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_DEPT;
            //操作单据编号
            logModel.ObjectID = no;

            return logModel;

        }
        #endregion
    }
}
