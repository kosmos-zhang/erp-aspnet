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
    public class SubDeliveryTransBus
    {
        #region 保存门店调拨单 及其明细
        public static string AddSubDeliveryTrans(SubDeliveryTrans model, List<SubDeliveryTransDetail> modellist, Hashtable htExtAttr)
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
                res = XBase.Data.Office.LogisticsDistributionManager.SubDeliveryTransDBHelper.AddSubDeliveryTrans(model, modellist, htExtAttr);
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
            LogInfoModel logModel = InitLogInfo(model.TransNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT; ;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;

        }
        #endregion

        #region 更新门店调拨单 及其明细
        public static bool UpdateSubDeliveryTrans(SubDeliveryTrans model, List<SubDeliveryTransDetail> modelList, Hashtable htExtAttr)
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
                res = XBase.Data.Office.LogisticsDistributionManager.SubDeliveryTransDBHelper.UpdateSubDeliveryTrans(model, modelList, htExtAttr);
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
            LogInfoModel logModel = InitLogInfo(model.TransNo);
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
        public static bool UpdateStatus(SubDeliveryTrans model, int stype)
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
                res = XBase.Data.Office.LogisticsDistributionManager.SubDeliveryTransDBHelper.UpdateStatus(model, stype);
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
            LogInfoModel logModel = InitLogInfo(model.TransNo);
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
            logModel.Element = msg;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
        }
        #endregion

        #region 读取门店调拨单列表
        public static DataTable GetSubDeliveryTransList(Hashtable htPara, int PageIndex, int PageSize, string OrderBy, string EFIndex, string EFDesc, ref int TotalCount)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryTransDBHelper.GetSubDeliveryTransList(htPara, PageIndex, PageSize, OrderBy, EFIndex, EFDesc, ref TotalCount);
        }

        /*不分页*/
        public static DataTable GetSubDeliveryTransList(Hashtable htPara, string OrderBy, string EFIndex, string EFDesc)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryTransDBHelper.GetSubDeliveryTransList(htPara, OrderBy, EFIndex, EFDesc);
        }
        #endregion

        #region 删除
        public static bool DelSubDeliveryBack(string[] IDList)
        {
            bool flag = false;

            //string res = string.Empty;
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
                flag = XBase.Data.Office.LogisticsDistributionManager.SubDeliveryTransDBHelper.DelSubDeliveryBack(IDList);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (flag)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            string idInfo = string.Empty;
            foreach (string str in IDList)
            {
                idInfo += str + ",";
            }
            //操作日志
            LogInfoModel logModel = InitLogInfo(idInfo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return flag;
        }
        #endregion

        #region 读取门店调拨单信息
        public static DataTable GetSubDeliveryTransInfo(SubDeliveryTrans model)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryTransDBHelper.GetSubDeliveryTransInfo(model);
        }

        /*打印使用*/
        public static DataTable GetSubDeliveryTransInfoPrint(SubDeliveryTrans model)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryTransDBHelper.GetSubDeliveryTransInfoPrint(model);
        }

        #endregion

        #region 读取门店掉调拨单明细信息
        public static DataTable GetSubDeliveryTransDetailInfo(SubDeliveryTransDetail model)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryTransDBHelper.GetSubDeliveryTransDetailInfo(model);
        }
        #endregion

        #region 门店出库
        public static bool RunSubDeliveryTransOut(SubDeliveryTrans model)
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
                res = XBase.Data.Office.LogisticsDistributionManager.SubDeliveryTransDBHelper.RunSubDeliveryTransOut(model);
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
            LogInfoModel logModel = InitLogInfo(model.TransNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
        }
        #endregion

        #region 门店入库
        public static bool RunSubDeliveryTransIn(SubDeliveryTrans model)
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
                res = XBase.Data.Office.LogisticsDistributionManager.SubDeliveryTransDBHelper.RunSubDeliveryTransIn(model);
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
            LogInfoModel logModel = InitLogInfo(model.TransNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
        }
        #endregion

        #region 门店调拨统计报表
        /*分页*/
        public static DataTable GetSubDeliveryTransReport(Hashtable htPara, int PageIndex, int PageSize, string OrderBy, ref int TotalCount, string InOut)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryTransDBHelper.GetSubDeliveryTransReport(htPara, PageIndex, PageSize, OrderBy, ref TotalCount, InOut);
        }

        /*不分页*/
        public static DataTable GetSubDeliveryTransReport(Hashtable htPara, string OrderBy, string InOut)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryTransDBHelper.GetSubDeliveryTransReport(htPara, OrderBy, InOut);
        }
        #endregion

        #region 门店调拨明细表
        /*分页*/
        public static DataTable GetSubDeliveryTransDetailReport(Hashtable htPara, int PageIndex, int PageSize, string OrderBy, ref int TotalCount)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryTransDBHelper.GetSubDeliveryTransDetailReport(htPara, PageIndex, PageSize, OrderBy, ref TotalCount);
        }

        /*不分页*/
        public static DataTable GetSubDeliveryTransDetailReport(Hashtable htPara, string OrderBy)
        {
            return XBase.Data.Office.LogisticsDistributionManager.SubDeliveryTransDBHelper.GetSubDeliveryTransDetailReport(htPara, OrderBy);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_SUBDELIVERYTRANS_SAVE;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_SUBDELIVERYTRANS_SAVE;
            //设置操作日志类型 修改
            logModel.ObjectName = "SubDeliveryTrans";
            //操作对象
            logModel.ObjectID = InNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

    }
}
