/**********************************************
 * 类作用：   采购入库和采购入库明细事务层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/02
 ***********************************************/

using System;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Common;
using XBase.Model.Office.ProductionManager;
using System.Collections;


namespace XBase.Business.Office.StorageManager
{
    public class StorageInProcessBus
    {
        #region 查询：生产完工入库单
        /// <summary>
        /// 查询仓库
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageInProcessTableBycondition(string BatchNo,StorageInProcessModel model, string timeStart, string timeEnd, string StorageID, string FromBillNo, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return StorageInProcessDBHelper.GetStorageInProcessTableBycondition(BatchNo,model, timeStart, timeEnd, StorageID, FromBillNo, pageIndex, pageCount, ord, ref TotalCount);
        }

        public static DataTable GetStorageInProcessTableBycondition(string BatchNo,StorageInProcessModel model, string timeStart, string timeEnd, string StorageID, string FromBillNo, string orderby)
        {
            return StorageInProcessDBHelper.GetStorageInProcessTableBycondition(BatchNo,model, timeStart, timeEnd, StorageID, FromBillNo, orderby);
        }
        #endregion

        #region 查看：生产完工入库单信息及其详细信息(加载页面的时候)
        /// <summary>
        /// 获取采购入库详细信息(加载页面的时候)
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageInProcessDetailInfo(StorageInProcessModel model)
        {
            return StorageInProcessDBHelper.GetStorageInProcessDetailInfo(model);
        }
        #endregion


        #region 添加：生产完工入库单信息及其详细信息
        /// <summary>
        /// 添加生产完工入库
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool InsertStorageInProcess(StorageInProcessModel model, System.Collections.Generic.List<StorageInProcessDetailModel> modellist, Hashtable htExtAttr, out int IndexIDentity)
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
                isSucc = StorageInProcessDBHelper.InsertStorageInProcess(model, modellist, htExtAttr, out IndexIDentity);

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
        #endregion

        #region 修改：生产完工入库信息（生产完工入库信息和详细信息）
        /// <summary>
        /// 修改：生产完工入库信息（生产完工入库信息和详细信息）
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modellist">明细modle数组</param>
        /// <returns></returns>
        public static bool UpdateStorageInProcess(StorageInProcessModel model, List<StorageInProcessDetailModel> modellist, Hashtable htExtAttr)
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
                isSucc = StorageInProcessDBHelper.UpdateStorageInProcess(model, modellist, htExtAttr);
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
        #endregion


        #region 删除：生产完工入库信息
        /// <summary>
        /// 删除生产完工入库信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteStorageInProcess(string ID, string CompanyCD)
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
                isSucc = StorageInProcessDBHelper.DeleteStorageInProcess(ID, CompanyCD);
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
        #endregion

        #region 确认
        public static bool ConfirmBill(StorageInProcessModel model)
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
                isSucc = StorageInProcessDBHelper.ConfirmBill(model);
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
        #endregion


        #region 结单
        public static bool CloseBill(StorageInProcessModel model)
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
                isSucc = StorageInProcessDBHelper.CloseBill(model);
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
        #endregion


        #region 取消结单
        public static bool CancelCloseBill(StorageInProcessModel model)
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
                isSucc = StorageInProcessDBHelper.CancelCloseBill(model);
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
        #endregion

        #region 获取生产任务单
        /// <summary>
        /// 获取生产任务单
        /// </summary>
        /// <param name="companycd"></param>
        /// <returns></returns>
        public static DataTable GetMTList(ManufactureTaskModel model)
        {
            try
            {
                return StorageInProcessDBHelper.GetMTList(model);
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region 是否可以被确认，判断明细中入库数量是否小于源单明细未入库数量
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns></returns>
        public static bool ISConfirmBill(StorageInProcessModel model)
        {
            return StorageInProcessDBHelper.ISConfirmBill(model);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_STORAGE_STORAGEINPROCESS_ADD;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_STORAGE_STORAGEINPROCESS_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = "StorageInProcess";
            //操作对象
            logModel.ObjectID = ID;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        public static DataTable GetStorageInProcessInfo(string ID, string CompanyCD)
        {
            try
            {
                return StorageInProcessDBHelper.GetStorageInProcessInfo(ID, CompanyCD);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DataTable GetStorageInProcessDetailInfo(string ID, string CompanyCD)
        {
            try
            {
                return StorageInProcessDBHelper.GetStorageInProcessDetailInfo(ID, CompanyCD);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
