/**********************************************
 * 类作用：   采购入库和采购入库明细事务层处理
 * 建立人：   肖合明
 * 建立时间： 2009/03/17
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
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Common;
using XBase.Model.Office.PurchaseManager;
using System.Collections;

namespace XBase.Business.Office.StorageManager
{
    public class StorageInPurchaseBus
    {
        #region 查询采购入库列表
        /// <summary>
        /// 查询采购入库列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageInPurchaseTableBycondition(string BatchNo, StorageInPurchaseModel model, string timeStart, string timeEnd, string FromBillNo, string StorageID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                return StorageInPurchaseDHHelper.GetStorageInPurchaseTableBycondition(BatchNo, model, timeStart, timeEnd, FromBillNo, StorageID, pageIndex, pageCount, ord, ref TotalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        public static DataTable GetStorageInPurchaseTableBycondition(string BatchNo, StorageInPurchaseModel model, string timeStart, string timeEnd, string FromBillNo, string StorageID, string orderby)
        {
            try
            {
                return StorageInPurchaseDHHelper.GetStorageInPurchaseTableBycondition(BatchNo, model, timeStart, timeEnd, FromBillNo, StorageID, orderby);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取采购入库及详细信息
        /// <summary>
        /// 获取采购入库及详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageInPurchaseDetailInfo(StorageInPurchaseModel model)
        {
            try
            {
                return StorageInPurchaseDHHelper.GetStorageInPurchaseDetailInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 采购入库单插入（插入采购入库单及其详细）
        /// <summary>
        /// 采购入库单插入（插入采购入库单及其详细）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertStorageInPurchase(StorageInPurchaseModel model, List<StorageInPurchaseDetailModel> modelList, Hashtable ht, out int IndexIDentity)
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
                isSucc = StorageInPurchaseDHHelper.InsertStorageInPurchase(model, modelList, ht, out IndexIDentity);

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

        #region 更新采购入库单及其详细
        /// <summary>
        /// 更新采购入库单及其详细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateStorageInPurchase(StorageInPurchaseModel model, List<StorageInPurchaseDetailModel> modelList, Hashtable ht)
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
                isSucc = StorageInPurchaseDHHelper.UpdateStorageInPurchase(model, modelList, ht);
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

        public static bool DeleteStorageInPurchase(string ID, string CompanyCD)
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
                isSucc = StorageInPurchaseDHHelper.DeleteStorageInPurchase(ID, CompanyCD);
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

        public static bool ConfirmBill(StorageInPurchaseModel model, out string Msg)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //定义返回变量
            bool isSucc = false;
            Msg = "";
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
                isSucc = StorageInPurchaseDHHelper.ConfirmBill(model, out Msg);
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

        public static bool CloseBill(StorageInPurchaseModel model)
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
                isSucc = StorageInPurchaseDHHelper.CloseBill(model);
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

        public static bool CancelCloseBill(StorageInPurchaseModel model)
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
                isSucc = StorageInPurchaseDHHelper.CancelCloseBill(model);
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

        #region 获取采购到货通知单
        /// <summary>
        /// 获取采购到货通知单
        /// </summary>
        /// <param name="companycd"></param>
        /// <returns></returns>
        public static DataTable GetPurchaseList(PurchaseArriveModel model)
        {
            try
            {
                return StorageInPurchaseDHHelper.GetPurchaseList(model);
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
        public static bool ISConfirmBill(StorageInPurchaseModel model)
        {
            return StorageInPurchaseDHHelper.ISConfirmBill(model);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_STORAGE_STORAGEINPURCHASE_ADD;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_STORAGE_STORAGEINPURCHASE_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = "StorageInPurchase";
            //操作对象
            logModel.ObjectID = ID;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        #region 导出
        public static DataTable GetStorageInPurchaseInfo(string ID, string CompanyCD)
        {
            try
            {
                return StorageInPurchaseDHHelper.GetStorageInPurchaseInfo(ID, CompanyCD);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DataTable GetStorageInPurchaseDetailInfo(string ID, string CompanyCD)
        {
            try
            {
                return StorageInPurchaseDHHelper.GetStorageInPurchaseDetailInfo(ID, CompanyCD);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 超账期应付款查询
        /// <summary>
        /// 超账期应付款查询
        /// </summary>
        /// <param name="ProviderID">供应商</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页记录数</param>
        /// <param name="orderBy">排序方法</param>
        /// <param name="TotalCount">总记录数</param>
        /// <returns></returns>
        public static DataTable GetOutDateAccount(string CompanyCD, string ProviderID
            , int pageIndex, int pageCount, string orderBy, ref int TotalCount)
        {
            return StorageInPurchaseDHHelper.GetOutDateAccount(CompanyCD, ProviderID
                , pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion
    }
}
