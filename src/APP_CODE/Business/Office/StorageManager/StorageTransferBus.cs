using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

using XBase.Model.Office.StorageManager;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Common;

namespace XBase.Business.Office.StorageManager
{
    public class StorageTransferBus
    {
        #region 读取产品
        public static DataTable GetProduct(string CompanyCD, int OutDeptID, int OutStorageID, string ProductName, string ProdNo)
        {
            return XBase.Data.Office.StorageManager.StorageTransferDBHelper.GetProduct(CompanyCD, OutDeptID, OutStorageID, ProductName, ProdNo);
        }

        public static DataTable GetProduct(string CompanyCD, int OutDeptID, int OutStorageID, string ProductName, string ProdNo, string EFIndex, string EFDesc)
        {
            return XBase.Data.Office.StorageManager.StorageTransferDBHelper.GetProduct(CompanyCD, OutDeptID, OutStorageID, ProductName, ProdNo, EFIndex, EFDesc);
        }
        #endregion

        #region 添加调拨单及其明细
        public static string AddStorageTransfer(StorageTransfer st, List<StorageTransferDetail> stdList, Hashtable ht)
        {

            //return "1|" + ((SqlCommand)SqlCmdList[0]).Parameters["@ID"].Value.ToString() + "#" + st.TransferNo;

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
                res = XBase.Data.Office.StorageManager.StorageTransferDBHelper.AddStorageTransfer(st, stdList,ht);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res.Split('|')[0] == "1")
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
            LogInfoModel logModel = InitLogInfo(st.TransferNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT; ;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;


        }
        #endregion

        #region 更新调拨单及其明细
        public static string UpdateStorageTransfer(StorageTransfer st, List<StorageTransferDetail> stdList, Hashtable ht)
        {
            //return "1|";
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
                res = XBase.Data.Office.StorageManager.StorageTransferDBHelper.UpdateStorageTransfer(st, stdList,ht);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res.Split('|')[0] == "1")
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
            LogInfoModel logModel = InitLogInfo(st.TransferNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
        }
        #endregion

        #region 更新单据状态
        public static string UpdateStorageTransfer(int stype, Model.Office.StorageManager.StorageTransfer st)
        {
            //return "1|单据确认成功";
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
                res = XBase.Data.Office.StorageManager.StorageTransferDBHelper.UpdateStorageTransfer(stype, st);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res.Split('|')[0] == "1")
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
            LogInfoModel logModel = InitLogInfo(st.TransferNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            if (stype == 1)
                logModel.Element = ConstUtil.LOG_PROCESS_CONFIRM;
            else if (stype == 2)
                logModel.Element = ConstUtil.LOG_PROCESS_COMPLETE;
            else if (stype == 3)
                logModel.Element = ConstUtil.LOG_PROCESS_CONCELCOMPLETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return res;

        }
        #endregion

        #region 读取调拨单
        public static DataTable GetStorageTransferList(string EFIndex, string EFDesc, Hashtable htPara, int PageIndex, int PageSize, string OrderBy, ref int TotalCount)
        {
            return XBase.Data.Office.StorageManager.StorageTransferDBHelper.GetStorageTransferList( EFIndex, EFDesc,htPara, PageIndex, PageSize, OrderBy, ref TotalCount);
        }

        /*不分页*/
        public static DataTable GetStorageTransferList(Hashtable htPara, string OrderBy)
        {
            return XBase.Data.Office.StorageManager.StorageTransferDBHelper.GetStorageTransferList(htPara, OrderBy);
        }
        #endregion

        #region 删除调拨单及其明细
        public static string DelStorageTransfer(string[] ID)
        {
            //if (XBase.Data.Office.StorageManager.StorageTransferDBHelper.DelStorageTransfer(ID))
            //    return "2";
            //else
            //    return "1";

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
                flag = XBase.Data.Office.StorageManager.StorageTransferDBHelper.DelStorageTransfer(ID);
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
            foreach (string str in ID)
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

            if (flag)
                return "2";
            else
                return "1";

        }
        #endregion

        #region 获取调拨单明细
        public static DataTable GetStorageTransferDetailInfo(Model.Office.StorageManager.StorageTransferDetail std)
        {
            return XBase.Data.Office.StorageManager.StorageTransferDBHelper.GetStorageTransferDetailInfo(std);
        }
        #endregion

        #region 获取调拨单信息
        public static DataTable GetStorageTransferInfo(StorageTransfer st)
        {
            return XBase.Data.Office.StorageManager.StorageTransferDBHelper.GetStorageTransferInfo(st);
        }

        /*提供打印*/
        public static DataTable GetStorageTransferInfoPrint(StorageTransfer st)
        {
            return XBase.Data.Office.StorageManager.StorageTransferDBHelper.GetStorageTransferInfoPrint(st);
        }
        #endregion


        #region 调拨出库
        public static string StorageTransferOut(Model.Office.StorageManager.StorageTransfer st, DataTable dtDetail, bool isBatchNo)
        {
            // return "1|";
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
                res = XBase.Data.Office.StorageManager.StorageTransferDBHelper.StorageTransferOut(st, dtDetail, isBatchNo);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res.Split('|')[0] == "1")
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
            LogInfoModel logModel = InitLogInfo(st.TransferNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_TRANSFER_OUT;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;

        }

        #endregion

        #region 调拨入库
        public static string StorageTransferIn(Model.Office.StorageManager.StorageTransfer st, DataTable dtDetail, bool isBatchNo)
        {
            // return "1|";
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
                res = XBase.Data.Office.StorageManager.StorageTransferDBHelper.StorageTransferIn(st, dtDetail, isBatchNo);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (res.Split('|')[0] == "1")
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
            LogInfoModel logModel = InitLogInfo(st.TransferNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_TRANSFER_IN;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;

        }
        #endregion 调拨入库

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
            logSys.ModuleID = ConstUtil.MODULE_ID_STORAGE_TRANSFER_SAVE;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_STORAGE_TRANSFER_SAVE;
            //设置操作日志类型 修改
            logModel.ObjectName = "StorageTransfer";
            //操作对象
            logModel.ObjectID = InNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion


    }
}
