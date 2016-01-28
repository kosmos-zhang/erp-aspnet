/**********************************************
 * 类作用：   期初库存和期初库存明细事务层处理
 * 建立人：   肖合明
 * 建立时间： 2009/03/14
 ***********************************************/

using System;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Business.Common;
using System.Collections.Generic;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using System.Collections;
namespace XBase.Business.Office.StorageManager
{
    public class StorageInitailBus
    {
        /// <summary>
        /// 查询期初库存列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageInitailTableBycondition(string BatchNo,string EFIndex,string EFDesc,StorageInitailModel model, string timeStart, string timeEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return StorageInitailInfoDBHelper.GetStorageInitailTableBycondition(BatchNo,EFIndex, EFDesc, model, timeStart, timeEnd, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetStorageInitailTableBycondition(string BatchNo,StorageInitailModel model, string timeStart, string timeEnd, string orderby)
        {
            try
            {
                return StorageInitailInfoDBHelper.GetStorageInitailTableBycondition(BatchNo,model, timeStart, timeEnd, orderby);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取期初库存和期初库存详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageInitailDetailInfo(StorageInitailModel model)
        {
            try
            {
                return StorageInitailInfoDBHelper.GetStorageInitailDetailInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 期初库存插入（插入期初库存和期初库存详细）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertStorageInitail(StorageInitailModel model, List<StorageInitailDetailModel> modelList,Hashtable ht, out int IndexIDentity)
        {
            IndexIDentity = 0;
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
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            //执行删除操作
            try
            {
                //执行更新
                isSucc = StorageInitailInfoDBHelper.InsertStorageInitail(model, modelList,ht, out IndexIDentity);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
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
            LogInfoModel logModel = InitLogInfo(model.InNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;

        }


        /// <summary>
        /// 期初库存更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateStorageInitail(StorageInitailModel model, List<StorageInitailDetailModel> modelList,Hashtable ht)
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
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            //执行删除操作
            try
            {
                //执行更新
                isSucc = StorageInitailInfoDBHelper.UpdateStorageInitail(model, modelList,ht);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
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
            LogInfoModel logModel = InitLogInfo(model.ID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ConfirmBill(StorageInitailModel model)
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
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            //执行删除操作
            try
            {
                //执行更新
                isSucc = StorageInitailInfoDBHelper.ConfirmBill(model);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }

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
            LogInfoModel logModel = InitLogInfo(model.ID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = "确认";
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }


        /// <summary>
        /// 结单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool CloseBill(StorageInitailModel model)
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
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            //执行删除操作
            try
            {
                //执行更新
                isSucc = StorageInitailInfoDBHelper.CloseBill(model);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }

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
            LogInfoModel logModel = InitLogInfo(model.ID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = "结单";
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }


        public static bool CancelCloseBill(StorageInitailModel model)
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
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            //执行删除操作
            try
            {
                //执行更新
                isSucc = StorageInitailInfoDBHelper.CancelCloseBill(model);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }

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
            LogInfoModel logModel = InitLogInfo(model.ID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = "取消结单";
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }

        public static bool DeleteStorageInitail(string ID, string CompanyCD)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            bool isSucc = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            //执行删除操作
            try
            {
                //执行更新
                isSucc = StorageInitailInfoDBHelper.DeleteStorageInitail(ID, CompanyCD);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }

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
            LogInfoModel logModel = InitLogInfo(ID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_STORAGE_STORAGEINITAIL_ADD;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_STORAGE_STORAGEINITAIL_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = "StorageInitail";
            //操作对象
            logModel.ObjectID = InNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        #region 期初入库是否可新建和确认了
        public static bool ISADD(string CompanyCD)
        {
            return StorageInitailInfoDBHelper.ISADD(CompanyCD);
        }
        #endregion

        #region 导出
        public static DataTable GetStorageInitailInfo(string ID, string CompanyCD)
        {
            try
            {
                return StorageInitailInfoDBHelper.GetStorageInitailInfo(ID, CompanyCD);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DataTable GetStorageInitailDetailInfo(string ID, string CompanyCD)
        {
            try
            {
                return StorageInitailInfoDBHelper.GetStorageInitailDetailInfo(ID, CompanyCD);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
