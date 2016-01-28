/**********************************************
 * 类作用：   红冲出库和红冲出库明细事务层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/23
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
using System.Collections;

namespace XBase.Business.Office.StorageManager
{
    public class StorageOutRedBus
    {
        #region 查询：红冲出库单
        /// <summary>
        /// 查询红冲出库单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageOutRedTableBycondition(StorageOutRedModel model, string timeStart, string timeEnd, string EFIndex, string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return StorageOutRedDBHelper.GetStorageOutRedTableBycondition(model, timeStart, timeEnd,EFIndex,EFDesc, pageIndex, pageCount, ord, ref TotalCount);
        }

        public static DataTable GetStorageOutRedTableBycondition(StorageOutRedModel model, string IndexValue, string TxtValue, string timeStart, string timeEnd,string BatchNo, string orderby)
        {
            return StorageOutRedDBHelper.GetStorageOutRedTableBycondition(model, IndexValue, TxtValue, timeStart, timeEnd,BatchNo, orderby);
        }
        #endregion

        #region 查看：红冲出库单信息及其详细信息(加载页面的时候)
        /// <summary>
        /// 获取红冲出库详细信息(加载页面的时候)
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageOutRedDetailInfo(StorageOutRedModel model)
        {
            return StorageOutRedDBHelper.GetStorageOutRedDetailInfo(model);
        }
        #endregion

        #region 插入红冲出库和红冲出库明细
        /// <summary>
        /// 插入红冲出库和红冲出库明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool InsertStorageOutRed(StorageOutRedModel model,Hashtable ht, List<StorageOutRedDetailModel> modelList, out int IndexIDentity)
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
                isSucc = StorageOutRedDBHelper.InsertStorageOutRed(model,ht, modelList, out IndexIDentity);


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
            LogInfoModel logModel = InitLogInfo(model.OutNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }
        #endregion

        #region 更新红冲出库及红冲出库明细
        /// <summary>
        /// 更新红冲出库及红冲出库明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool UpdateStorageOutRed(StorageOutRedModel model,Hashtable ht, List<StorageOutRedDetailModel> modelList)
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
                isSucc = StorageOutRedDBHelper.UpdateStorageOutRed(model,ht, modelList);
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

        #region 删除：红冲出库信息
        /// <summary>
        /// 删除红冲出库信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteStorageOutRed(string ID, string CompanyCD)
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
                isSucc = StorageOutRedDBHelper.DeleteStorageOutRed(ID, CompanyCD);
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
        public static bool ConfirmBill(StorageOutRedModel model)
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
                isSucc = StorageOutRedDBHelper.ConfirmBill(model);
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
        public static bool CloseBill(StorageOutRedModel model)
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
                isSucc = StorageOutRedDBHelper.CloseBill(model);
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
        public static bool CancelCloseBill(StorageOutRedModel model)
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
                isSucc = StorageOutRedDBHelper.CancelCloseBill(model);
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

        #region 获取出库单列表
        /// <summary>
        /// 获取出库单列表
        /// </summary>
        /// <param name="companycd"></param>
        /// <returns></returns>
        public static DataTable GetStorageOutList(StorageOutRedModel model, string OutType)
        {
            try
            {
                return StorageOutRedDBHelper.GetStorageOutList(model, OutType);
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region 查出对应出库类型的基本信息,弹出层信息
        /// <summary>
        /// 查出对应出库类型的基本信息,弹出层信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="OutType"></param>
        /// <returns></returns>
        public static DataTable GetOutTypeInfo(string CompanyCD, string OutType, string OutNo, string Title)
        {
            return StorageOutRedDBHelper.GetOutTypeInfo(CompanyCD, OutType, OutNo, Title);
        }
        #endregion

        #region 根据的出库编号查询出相关信息及其明细
        /// <summary>
        /// 根据的出库编号查询出相关信息及其明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="OutType"></param>
        /// <param name="OutNo"></param>
        /// <returns></returns>
        public static DataTable GetDetailInfo(string CompanyCD, string OutType, string OutNo)
        {
            return StorageOutRedDBHelper.GetDetailInfo(CompanyCD, OutType, OutNo);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_STORAGE_STORAGEOUTRED_ADD;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_STORAGE_STORAGEOUTRED_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = "StorageOutRed";
            //操作对象
            logModel.ObjectID = ID;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

        #region 单据打印
        public static DataTable GetStorageOutRedInfo(string ID, string CompanyCD)
        {
            try
            {
                return StorageOutRedDBHelper.GetStorageOutRedInfo(ID, CompanyCD);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DataTable GetStorageOutRedDetailInfo(string ID, string CompanyCD)
        {
            try
            {
                return StorageOutRedDBHelper.GetStorageOutRedDetailInfo(ID, CompanyCD);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
