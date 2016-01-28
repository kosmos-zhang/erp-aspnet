/**********************************************
 * 类作用：   工资项设置
 * 建立人：   吴志强
 * 建立时间： 2009/05/04
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
    /// 类名：SalaryItemBus
    /// 描述：工资项设置
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/04
    /// 最后修改时间：2009/05/04
    /// </summary>
    ///
    public class SalaryItemBus
    {


        public static DataTable SearchEmplInfo(EmployeeSearchModel model)
        {
            //查询并返回人员信息
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询并返回值
            return SalaryItemDBHelper.SearchEmplInfo(model);
        }


        #region 编辑工资项信息
        /// <summary>
        /// 编辑工资项信息
        /// </summary>
        /// <param name="lstEdit">工资项信息</param>
        /// <returns></returns>
        public static bool SaveSalaryItemInfo(ArrayList lstEdit)
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
                    isSucc = SalaryItemDBHelper.SaveSalaryItemInfo(lstEdit, userInfo.CompanyCD);
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
        public static bool InsertSalaryItem(SalaryItemModel  model)
        {
            //定义返回变量
            bool isSucc = true;
            //信息存在时，进行操作
            if (model != null )
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //执行保存操作
                try
                {
                    //执行保存操作
                    isSucc = SalaryItemDBHelper.InsertSalaryItem(model);
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
        public static DataTable GetOutEmployeeInfo(EmployeeSearchModel model)
        {

            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //查询并返回人员信息
            return SalaryItemDBHelper.GetOutEmployeeInfo (model);
        }
        #region 查询工资项信息
        /// <summary>
        /// 查询工资项信息
        /// </summary>
        /// <param name="isUsed">启用状态</param>
        /// <returns></returns>
        public static DataTable GetSalaryItemInfo(bool isUsed)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //执行查询
            return SalaryItemDBHelper.SearchSalaryItemInfo(userInfo.CompanyCD, isUsed);
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
            logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_SALARY_SET;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_SALARY_ITEM;//操作对象
            //
            logModel.ObjectID = no;

            return logModel;

        }
        #endregion


        //public static bool  DeleteInfo(string ItemNo)
        //{
        //    //获取登陆用户信息
        //    UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //    string companyCD = userInfo.CompanyCD;
        //    //执行查询
        //    return SalaryItemDBHelper.DeleteInfo(ItemNo, companyCD);
        //}

        #region 校验提成工资项是否被使用
        /// <summary>
        /// 校验提成工资项是否被使用
        /// </summary>
        /// <param name="itemNo">提成工资项编号</param>
        /// <returns></returns>
        public static bool IsUsedItem(string itemNo)
        {
            //获取登陆用户信息
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //进行校验
            return SalaryItemDBHelper.IsUsedItem(itemNo, companyCD);
        }
        #endregion

    }
}
