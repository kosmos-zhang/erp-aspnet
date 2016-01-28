using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using XBase.Model.Office.StorageManager;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Common;
using System.Collections;
namespace XBase.Business.Office.StorageManager
{
    public class StorageBorrowBus
    {

        #region 获取部门信息
        public static DataTable GetDept(string CompanyCD)
        {
            try
            {
                return Data.Office.StorageManager.StorageBorrowDBHelper.GetDept(CompanyCD);
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        #endregion

        #region 获取仓库信息
        public static DataTable GetDepot(string CompanyCD)
        {
            try
            {
                return Data.Office.StorageManager.StorageBorrowDBHelper.GetDepot(CompanyCD);
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        #endregion

        #region 读取物品信息
        public static DataTable GetProductInfo(string CompanyCD, int BorrowDeptID, int StorageID, string ProductName, string ProdNo)
        {
            try
            {
                return XBase.Data.Office.StorageManager.StorageBorrowDBHelper.GetProductInfo(CompanyCD, BorrowDeptID, StorageID,ProductName,ProdNo);
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }


        public static DataTable GetProductInfo(string CompanyCD, int BorrowDeptID, int StorageID, string ProductName, string ProdNo, string EFIndex, string EFDesc)
        {
            try
            {
                 return XBase.Data.Office.StorageManager.StorageBorrowDBHelper.GetProductInfo(CompanyCD, BorrowDeptID, StorageID,ProductName,ProdNo,EFIndex,EFDesc);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion

        #region 插入：添加借货单和明细
        public static string SetStorageBorrow(StorageBorrow sborrow, List<StorageBorrowDetail> detailsList, Hashtable ht)
        {
            //定义返回变量
            string Result = string.Empty;
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
                Result = XBase.Data.Office.StorageManager.StorageBorrowDBHelper.SetBorrowStorage(sborrow, detailsList,ht);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }

            //定义变量
            string remark;
            //成功时
            if (Result.Split('|')[0] == "1")
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
            LogInfoModel logModel = InitLogInfo(sborrow.BorrowNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return Result;


        }
        #endregion

        #region 获取借货原因
        public static DataTable GetBorrowReason(string CompanyCD, int Flag)
        {
            return XBase.Data.Office.StorageManager.StorageBorrowDBHelper.GetBorrowReason(Flag, CompanyCD);

        }

        #endregion

        #region 读取借货单明细
        public static DataTable GetStorageBorrowDetail(string CompanyCD, string BorrowNo)
        {
            return XBase.Data.Office.StorageManager.StorageBorrowDBHelper.GetStorageBorrowDetail(BorrowNo, CompanyCD);
        }
        #endregion

        #region 更新借货单 及其明细
        public static string UpdateStorageBorrow(StorageBorrow sborrow, List<StorageBorrowDetail> borrowlist, Hashtable ht)
        {
            //定义返回变量
            string Result = string.Empty;
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
                Result = XBase.Data.Office.StorageManager.StorageBorrowDBHelper.UpdateStorageBorrow(sborrow, borrowlist,ht);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (Result.Split('|')[0] == "1")
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
            LogInfoModel logModel = InitLogInfo(sborrow.BorrowNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return Result;

        }
        #endregion

        #region 更新单据状态
        public static string SetBillStatus(StorageBorrow borrow)
        {
            //定义返回变量
            string Result = string.Empty;
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
                Result = XBase.Data.Office.StorageManager.StorageBorrowDBHelper.SetBillStatus(borrow);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (Result.Split('|')[0] == "1")
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
            LogInfoModel logModel = InitLogInfo(borrow.BorrowNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_BILLOPERATE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return Result;

        }
        #endregion

        #region 读取单据列表
        public static DataTable GetStorageList(string EFIndex, string EFDesc,int PageIndex, int PageSzie, string OrderBy, ref int TotalCount, StorageBorrow borrow, DateTime StartTime, DateTime EndTime, int SubmitStatus)
        {
            return XBase.Data.Office.StorageManager.StorageBorrowDBHelper.GetStorageList(EFIndex, EFDesc,PageIndex, PageSzie, OrderBy, ref TotalCount, borrow, StartTime, EndTime, SubmitStatus);
        }
        /*不分页*/
        public static DataTable GetStorageList(string EFIndex, string EFDesc, StorageBorrow borrow, string OrderBy, DateTime StartTime, DateTime EndTime, int SubmitStatus)
        {
            return XBase.Data.Office.StorageManager.StorageBorrowDBHelper.GetStorageList( EFIndex,  EFDesc,borrow,OrderBy,StartTime, EndTime, SubmitStatus);
        }
        #endregion

        #region 删除借货单
        public static string DeleteStorageBorrow(string[] ID)
        {
            //定义返回变量
            string Result = string.Empty;
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
                Result = XBase.Data.Office.StorageManager.StorageBorrowDBHelper.DeleteStorageBorrow(ID);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
            //定义变量
            string remark;
            //成功时
            if (Result == "1")
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
            return Result;

        }

        #endregion

        #region 取消确认
        public static bool CancelConfirm(StorageBorrow borrow)
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
                res = XBase.Data.Office.StorageManager.StorageBorrowDBHelper.CancelConfirm(borrow);
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
            LogInfoModel logModel = InitLogInfo(borrow.BorrowNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UNCONFIRM;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
        }
        #endregion

        #region 删除明细
        public static bool DelStorageDetails(string[] IDList)
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
                res = XBase.Data.Office.StorageManager.StorageBorrowDBHelper.DelStorageDetails(IDList);
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

            string idInfo = string.Empty;
            foreach (string str in IDList)
            {
                idInfo += str + ",";
            }

            //操作日志
            LogInfoModel logModel = InitLogInfo(idInfo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_DELDETAIL;

            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return res;
        }
        #endregion

        #region
        public static DataTable GetStorageBorrowByID(int ID)
        {
            return XBase.Data.Office.StorageManager.StorageBorrowDBHelper.GetStorageBorrowByID(ID);
        }

        public static DataTable GetStorageBorrowInfo(int ID)
        {
            return XBase.Data.Office.StorageManager.StorageBorrowDBHelper.GetStorageBorrowInfo(ID);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_STORAGE_BORROW_SAVE;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_STORAGE_BORROW_SAVE;
            //设置操作日志类型 修改
            logModel.ObjectName = "StorageBorrow";
            //操作对象
            logModel.ObjectID = InNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion



    }
}
