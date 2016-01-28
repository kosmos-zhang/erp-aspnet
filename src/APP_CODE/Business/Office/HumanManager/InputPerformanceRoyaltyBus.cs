/**********************************************
 * 类作用：   绩效工资录入
 * 建立人：   肖合明
 * 建立时间： 2009/09/09
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
    public class InputPerformanceRoyaltyBus
    {

        #region 查询：读取列表信息
        public static DataTable GetInfo(InputPerformanceRoyaltyModel model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            //获取登陆用户信息
            //UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                return InputPerformanceRoyaltyDBHelper.GetInfo(model, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region 同步总方法
        /// <summary>
        /// 同步总方法
        /// </summary>
        /// <returns></returns>
        public static bool DoInsert()
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                return InputPerformanceRoyaltyDBHelper.DoInsert(userInfo.CompanyCD, userInfo.UserID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(InputPerformanceRoyaltyModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //定义返回变量
            bool isSucc = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            try
            {
                isSucc = InputPerformanceRoyaltyDBHelper.Update(model);
            }
            catch (Exception ex)
            {

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
            LogInfoModel logModel = InitLogInfo(model.ID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除事件（通过ID数组删除）
        /// </summary>
        /// <param name="StrID"></param>
        /// <returns></returns>
        public static bool Delete(string StrID)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //定义返回变量
            bool isSucc = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            try
            {
                isSucc = InputPerformanceRoyaltyDBHelper.Delete(StrID, userInfo.CompanyCD);
            }
            catch (Exception ex)
            {

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
            LogInfoModel logModel = InitLogInfo(StrID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }
        #endregion

        #region 判断员工(通过考核类型)绩效基数是否设置
        /// <summary>
        /// 判断员工(通过考核类型)绩效基数是否设置
        /// </summary>
        /// <param name="TaskFlag"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns>Ture:已经设置；false:没有设置（提示）</returns>
        public static bool IsBaseMoneySet(string CompanyCD)
        {
            return InputPerformanceRoyaltyDBHelper.IsBaseMoneySet(CompanyCD);
        }

        #endregion

        #region 判断员工绩效系数是否设置
        public static bool IsConfficentSet(string CompanyCD)
        {
            return InputPerformanceRoyaltyDBHelper.IsConfficentSet(CompanyCD);
        }
        #endregion

        //#region 查询绩效系数
        ///// <summary>
        ///// 查询绩效系数
        ///// </summary>
        ///// <param name="RealScore"> 实际得分</param>
        ///// <returns></returns>
        //public static DataTable GetConfficent(string RealScore, string EmployeeID)
        //{

        //    try
        //    {
        //        return InputPerformanceRoyaltyDBHelper.GetConfficent(RealScore, EmployeeID);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //#endregion

        //#region 获取某个员工的最大系数（当超过所有区间的时候取他的最大系数）
        ///// <summary>
        ///// 获取某个员工的最大系数（当超过所有区间的时候取他的最大系数）
        ///// </summary>
        ///// <param name="EmployeeID">员工编号</param>
        ///// <returns></returns>
        //public static DataTable GetLastConfficent(string EmployeeID)
        //{

        //    try
        //    {
        //        return InputPerformanceRoyaltyDBHelper.GetLastConfficent(EmployeeID);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //#endregion

        //#region 获取某个公司的默认系数
        ///// <summary>
        ///// 获取某个员工的最大系数
        ///// </summary>
        ///// <param name="EmployeeID">员工编号</param>
        ///// <returns></returns>
        //public static DataTable GetDefaultConfficent(string RealScore, string CompanyCD)
        //{
        //    //获取登陆用户信息
        //    UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //    try
        //    {
        //        return InputPerformanceRoyaltyDBHelper.GetDefaultConfficent(RealScore, userInfo.CompanyCD);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //#endregion

        //#region 获取某个公司默认的最大系数（当取默认值的时候，超过所有区间的时候取他的最大系数）
        ///// <summary>
        ///// 获取某个公司默认的最大系数（当取默认值的时候，超过所有区间的时候取他的最大系数）
        ///// </summary>
        ///// <param name="CompanyCD">公司编号</param>
        ///// <returns></returns>
        //public static DataTable GetLastDefaultConfficent(string CompanyCD)
        //{
        //    //获取登陆用户信息
        //    UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //    try
        //    {
        //        return InputPerformanceRoyaltyDBHelper.GetLastDefaultConfficent(userInfo.CompanyCD);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //#endregion

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
            logSys.ModuleID = "2011702";
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

            logModel.ModuleID = "2011702";
            //设置操作日志类型 修改
            logModel.ObjectName = "InputPerformanceRoyalty";
            //操作对象
            logModel.ObjectID = ID;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion


    }
}
