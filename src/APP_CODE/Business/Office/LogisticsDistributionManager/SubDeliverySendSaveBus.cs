using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Common;
using XBase.Model.Office.LogisticsDistributionManager;
namespace XBase.Business.Office.LogisticsDistributionManager
{
    public class SubDeliverySendSaveBus
    {
        #region 添加配送单
        public static string AddSubDeliverySend(SubDeliverySend model, List<SubDeliverySendDetail> modellist, Hashtable htExtAttr)
        {
            //定义返回变量
            string res = string.Empty;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取当前用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //执行操作
            try
            {
                //执行操作
                res = XBase.Data.Office.LogisticsDistributionManager.SubDeliverySendSaveDBHelper.AddSubDeliverySend(model, modellist, htExtAttr);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res != string.Empty)
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
            LogInfoModel logModel = InitLogInfo(model.SendNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT; ;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;

        }
        #endregion

        #region 更新配送明细单
        public static bool UpdateSubDeliverySend(SubDeliverySend model, List<SubDeliverySendDetail> modelList, Hashtable htExtAttr)
        {
            //定义返回变量
            bool res = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取当前用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //执行操作
            try
            {
                //执行操作
                res = XBase.Data.Office.LogisticsDistributionManager.SubDeliverySendSaveDBHelper.UpdateSubDeliverySend(model, modelList, htExtAttr);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res)
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
            LogInfoModel logModel = InitLogInfo(model.SendNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
        }
        #endregion

        #region 更新状态
        public static bool UpdateStatus(SubDeliverySend model, int stype)
        {
            //定义返回变量
            bool res = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取当前用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //执行操作
            try
            {
                //执行操作
                res = XBase.Data.Office.LogisticsDistributionManager.SubDeliverySendSaveDBHelper.UpdateStatus(model, stype);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res)
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
            LogInfoModel logModel = InitLogInfo(model.SendNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            string msg = string.Empty;
            switch (stype)
            {
                case 1:
                    /*确认*/
                    msg = ConstUtil.LOG_PROCESS_CONFIRM; 
                    break;
                case 2:
                    /*结单*/
                    msg = ConstUtil.LOG_PROCESS_COMPLETE; 
                    break;
                case 3:
                    /*取消结单*/
                    msg = ConstUtil.LOG_PROCESS_CONCELCOMPLETE;
                    break;
                case 4:
                    /*取消确认*/
                    msg = ConstUtil.LOG_PROCESS_UNCONFIRM;
                    break;
            }
            logModel.Element = msg; ;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
        }
        #endregion

        #region 读取配送单信息
        public static DataTable GetSubDeliverySendInfo(SubDeliverySend model)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliverySendSaveDBHelper.GetSubDeliverySendInfo(model);
        }

        /*打印使用*/
        public static DataTable GetSubDeliverySendInfoPrint(SubDeliverySend model)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliverySendSaveDBHelper.GetSubDeliverySendInfoPrint(model);
        }

        #endregion

        #region 读取配送单明细
        public static DataTable GetSubDeliverySendDetail(SubDeliverySendDetail model,int DeptID)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliverySendSaveDBHelper.GetSubDeliverySendDetail(model, DeptID);
        }

        /*打印使用*/
        public static DataTable GetSubDeliverySendDetailPrint(SubDeliverySendDetail model, int DeptID)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliverySendSaveDBHelper.GetSubDeliverySendDetailPrint(model, DeptID);
        }
        #endregion

        #region  实时获取可用库存量
        public static string GetProductUseCount(SubDeliverySendDetail model)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliverySendSaveDBHelper.GetProductUseCount(model);
        }
        #endregion

        #region 执行配送出库操作
        public static bool RunSubDeliverySendOut(SubDeliverySend model, List<SubDeliverySendDetail> modelList)
        {
            //定义返回变量
            bool res = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取当前用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //执行操作
            try
            {
                //执行操作
                res = XBase.Data.Office.LogisticsDistributionManager.SubDeliverySendSaveDBHelper.RunSubDeliverySendOut(model, modelList);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res)
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
            LogInfoModel logModel = InitLogInfo(model.SendNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
        }
        #endregion

        #region 执行验货入库操作
        public static bool RunSubDeliverySendIn(SubDeliverySend model)
        {
            //定义返回变量
            bool res = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取当前用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            //执行操作
            try
            {
                //执行操作
                res = XBase.Data.Office.LogisticsDistributionManager.SubDeliverySendSaveDBHelper.RunSubDeliverySendIn(model);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res)
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
            LogInfoModel logModel = InitLogInfo(model.SendNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
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
            logSys.ModuleID = ConstUtil.MODULE_ID_SUBDELIVERYSEND_SAVE;
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
        private static LogInfoModel InitLogInfo(string InNo)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_SUBDELIVERYSEND_SAVE;
            //设置操作日志类型 修改
            logModel.ObjectName = "SubDeliverySend";
            //操作对象
            logModel.ObjectID = InNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion
    }
}
