/**********************************************
 * 类作用：   仓库信息事务层处理
 * 建立人：   肖合明
 * 建立时间： 2009/03/12
 ***********************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.StorageManager;
using XBase.Common;
using System.Data;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Model.Office.SupplyChain;

namespace XBase.Business.Office.StorageManager
{

    public class StorageBus
    {

        public static DataTable GetLitBycondition(StorageModel model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return StorageDBHelper.GetLitBycondition(model, pageIndex, pageCount, ord, ref TotalCount);
        }

        /// <summary>
        /// 查询仓库信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageListBycondition(StorageModel model, string orderby)
        {
            try
            {
                return StorageDBHelper.GetStorageTableBycondition(model, orderby);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetStorageListBycondition(StorageModel model)
        {
            try
            {
                return StorageDBHelper.GetStorageTableBycondition(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        public static DataTable GetStorageListByRed(StorageModel model)
        {
            try
            {
                return StorageDBHelper.GetStorageTableByRed(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 获取仓库详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageDetailInfo(string CompanyCD, int ID)
        {
            try
            {
                return StorageDBHelper.GetStorage(CompanyCD, ID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 仓库插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertStorage(StorageModel model, out int IndexIDentity)
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
                isSucc = StorageDBHelper.InsertStorage(model, out IndexIDentity);
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
            LogInfoModel logModel = InitLogInfo(model.StorageNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;

        }


        /// <summary>
        /// 更新仓库信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool UpdateStorage(StorageModel model)
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
                isSucc = StorageDBHelper.UpdateStorage(model);
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
                remark = ConstUtil.LOG_PROCESS_SUCCESS
;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.StorageNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }

        public static bool DeleteStorage(string ID, string CompanyCD)
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
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            //执行删除操作
            try
            {
                //执行更新
                isSucc = StorageDBHelper.DeleteStorage(ID, CompanyCD);
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
                remark = ConstUtil.LOG_PROCESS_SUCCESS
;
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
            logSys.ModuleID = ConstUtil.MODULE_ID_STORAGE_STORAGEINFO;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_STORAGE_STORAGEINFO;
            //设置操作日志类型 修改
            logModel.ObjectName = "StorageInfo";
            //操作对象
            logModel.ObjectID = ID;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion


        #region 是否可以删除
        public static bool IsDeleteStorage(string ID, string CompanyCD)
        {
            return StorageDBHelper.IsDeleteStorage(ID, CompanyCD);
        }
        #endregion

        public static void BindStorateInfo(System.Web.UI.WebControls.DropDownList ddl, string companycd)
        {
            StorageDBHelper.BindStorateInfo(ddl, companycd);
        }

        public static DataTable GetStorageSetUp(string companycd, int storageID, int ProductID, string begindate, string enddate, int timeType,int ByTimeType)
        {
            return StorageDBHelper.GetStorageSetUp(companycd, storageID, ProductID, begindate, enddate, timeType, ByTimeType);
        }

        public static DataTable GetStorageSetUpDetails(int storageID, int ProductID, string begindate, string enddate, string order, int pageindex, int pagesize, int timeType,int ByTimeType, string timestr, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetStorageSetUpDetails(companyCD, storageID, ProductID, begindate, enddate, order, pageindex, pagesize, timeType,ByTimeType,timestr, ref recordCount);
        }
        public static DataTable GetStorageCompare(int storageID, string begindate, string enddate,int ByTimeType)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetStorageCompare(companyCD, storageID, begindate, enddate, ByTimeType);
        }

        public static DataTable GetStorageCompareDetails(int storageID, int ProductID, string begindate, string enddate,int ByTimeType, string order, int pageindex, int pagesize, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetStorageCompareDetails(companyCD, storageID, ProductID, begindate, enddate,ByTimeType, pageindex, pagesize, ref recordCount);
        }

        public static DataTable GetStorageInSetUp(int storageID, int ProductID, string begindate, string enddate, int timeType, int ByTimeType)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetStorageInSetUp(companyCD, storageID, ProductID, begindate, enddate, timeType,ByTimeType);

        }

        public static DataTable GetStorageInSetUpDetails(int storageID, int ProductID, string begindate, string enddate, string order, int pageindex, int pagesize, int timeType,int ByTimeType, string timestr, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetStorageInSetUpDetails(companyCD, storageID, ProductID, begindate, enddate, order, pageindex, pagesize, timeType,ByTimeType,timestr, ref recordCount);
        }

        public static DataTable GetStorageInCompare(int storageID, string begindate, string enddate,int ByTimeType)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetStorageInCompare(companyCD, storageID, begindate, enddate, ByTimeType);
        }

        public static DataTable GetStorageInCompareDetails(int storageID, int ProductID, string begindate, string enddate,int ByTimeType, int pageindex, int pagesize, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetStorageInCompareDetails(companyCD, storageID, ProductID, begindate, enddate,ByTimeType, pageindex, pagesize, ref recordCount);
        }

        public static DataTable GetStorageNowCompare(int ProductID)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetStorageNowCompare(companyCD, ProductID);
        }

        public static DataTable GetStorageNowCompareDetails(int storageID, int pageindex, int pagesize, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetStorageNowCompareDetails(companyCD, storageID, pageindex, pagesize, ref recordCount);
        }

        public static DataTable GetProductStorage(int StorageID)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetProductStorage(companyCD, StorageID);
        }

        public static DataTable GetProductStorageDetails(int productID, int pageindex, int pagesize, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetProductStorageDetails(companyCD, productID, pageindex, pagesize, ref recordCount);
        }

        public static DataTable GetStorageLossSetUp(int storageID, int ProductID, string begindate, string enddate, int timeType, int ByTimeType)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetStorageLossSetUp(companyCD, storageID, ProductID, begindate, enddate, timeType, ByTimeType);
        }

        public static DataTable GetStorageLossSetUpDetails(int storageID, int ProductID, string begindate, string enddate, int pageindex, int pagesize, int timeType,int ByTimeType, string timestr, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetStorageLossSetUpDetails(companyCD, storageID, ProductID, begindate, enddate, pageindex, pagesize, timeType,ByTimeType, timestr, ref recordCount);
        }

        public static DataTable GetStorageLossAnalysis(int storageID, string begindate, string endDate,int ByTimeType)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetStorageLossAnalysis(companyCD, storageID, begindate, endDate, ByTimeType);
        }

        public static DataTable GetStorageLossAnalysisDetails(int productID, string begindate, string enddate, int ByTimeType, int pageindex, int pagesize, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetStorageLossAnalysisDetails(companyCD, productID, begindate, enddate,ByTimeType, pageindex, pagesize, ref recordCount);
        }

        public static DataTable GetStorageProductLossAnalysis(int ProductID, string begindate, string enddate, int ByTimeType)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetStorageProductLossAnalysis(companyCD, ProductID, begindate, enddate, ByTimeType);
        }

        public static DataTable GetStorageProductLossDetails(int storageID, string begindate, string enddate,int ByTimeType, int pageindex, int pagesize, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return StorageDBHelper.GetStorageProductLossDetails(companyCD, storageID, begindate, enddate,ByTimeType, pageindex, pagesize, ref recordCount);
        }

        #region 获取指定人员可查看仓库的ID串
        /// <summary>
        /// 获取指定人员可查看仓库的ID串(以逗号隔开形式)
        /// add by hexw 2010-3-10
        /// </summary>
        /// <param name="empid">当前登录人ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>仓库id串</returns>
        public static string GetStorageIDStr(int empid, string strCompanyCD)
        {
            return StorageDBHelper.GetStorageIDStr(empid, strCompanyCD);
        }
        #endregion

        #region 根据查询条件获取仓库列表(库存查询中用到，可查看人员过滤仓库)
        /// <summary>
        /// 根据查询条件获取仓库列表(库存查询中用到，可查看人员过滤仓库)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetStorageListBycondition2(StorageModel model)
        {
            try
            {
                return StorageDBHelper.GetStorageTableBycondition2(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 进销存汇总表(单品)
        /// <summary>
        /// 进销存汇总表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="DailyDate"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetStorageInAndOutTotalInfo(ProductInfoModel model, string DailyDate, string EndDate, string BatchNo, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, out DataTable dt, ref int totalCount)
        {
            try
            {
                return StorageDBHelper.GetStorageInAndOutTotalInfo(model, DailyDate, EndDate, BatchNo, EFIndex, EFDesc, pageIndex, pageCount, OrderBy, out dt, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 进销存汇分析(决策模式)
        /// <summary>
        /// 进销存汇分析(决策模式)
        /// </summary>
        /// <param name="StorageID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="ProductName"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="dt"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetStorageInAndOutTotalInfo(string StorageID,string BatchNo,string ProductName,string StartDate,string EndDate,int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return StorageDBHelper.GetStorageInAndOutTotalInfo(StorageID, BatchNo, ProductName, StartDate, EndDate, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 进销存汇总表(全部)
        /// <summary>
        /// 进销存汇总表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="DailyDate"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetAllStorageInAndOutInfo(ProductInfoModel model, string DailyDate, string EndDate, string BatchNo, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, out DataTable dt, ref int totalCount)
        {
            try
            {
                return StorageDBHelper.GetAllStorageInAndOutInfo(model, DailyDate, EndDate, BatchNo, EFIndex, EFDesc, pageIndex, pageCount, OrderBy, out dt, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 总计：进销存日报表
        /// <summary>
        /// 进销存日报表总计
        /// </summary>
        /// <param name="model"></param>
        /// <param name="DailyDate"></param>
        /// <param name="BatchNo"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <returns></returns>
        public static DataTable GetAllTotal(ProductInfoModel model, string DailyDate, string EndDate, string BatchNo, string EFIndex, string EFDesc, bool flag)
        {
            try
            {
                return StorageDBHelper.GetAllTotal(model, DailyDate, EndDate, BatchNo, EFIndex, EFDesc, flag);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 库存数量验证
        public static string ValidateStorageCount(string[] ProductID, string[] StorageID, string[] BatchNo, string CompanyCD)
        {
            return StorageDBHelper.ValidateStorageCount(ProductID, StorageID, BatchNo, CompanyCD);
        }
        #endregion

        #region 根据物品名称 物品编号 判断是否启用批次
        public static bool IsBatchByProductNameAndNo(string ProductNo, string ProductName, string CompanyCD)
        {
            return StorageDBHelper.IsBatchByProductNameAndNo(ProductNo, ProductName, CompanyCD);
        }
        #endregion
    }
}
