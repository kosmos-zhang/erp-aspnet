/**********************************************
 * 类作用：   期初库存和期初库存明细事务层处理
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
using XBase.Model.Office.SellManager;
using System.Collections;

namespace XBase.Business.Office.StorageManager
{
    public class StorageOutSellBus
    {
        #region 查询：销售出库单
        /// <summary>
        /// 查询销售出库单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageOutSellTableBycondition(StorageOutSellModel model, string timeStart, string timeEnd, string SendNo,string EFIndex,string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return StorageOutSellDBHelper.GetStorageOutSellTableBycondition(model, timeStart, timeEnd, SendNo, EFIndex,EFDesc,pageIndex, pageCount, ord, ref TotalCount);
        }

        public static DataTable GetStorageOutSellTableBycondition(StorageOutSellModel model,string IndexValue,string TxtValue, string timeStart, string timeEnd, string SendNo,string BatchNo, string orderby)
        {
            return StorageOutSellDBHelper.GetStorageOutSellTableBycondition(model, IndexValue, TxtValue, timeStart, timeEnd, SendNo,BatchNo, orderby);
        }
        #endregion

        #region 查看：销售出库单信息及其详细信息(加载页面的时候)
        /// <summary>
        /// 获取销售出库详细信息(加载页面的时候)
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageOutSellDetailInfo(StorageOutSellModel model)
        {
            return StorageOutSellDBHelper.GetStorageOutSellDetailInfo(model);
        }
        #endregion

        #region 插入销售出库和销售出库明细
        /// <summary>
        /// 插入销售出库和销售出库明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool InsertStorageOutSell(StorageOutSellModel model, List<StorageOutSellDetailModel> modelList,Hashtable ht, out int IndexIDentity)
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
                isSucc = StorageOutSellDBHelper.InsertStorageOutSell(model, modelList,ht, out IndexIDentity);


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

        #region 更新销售出库及销售出库明细
        /// <summary>
        /// 更新销售出库及销售出库明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool UpdateStorageOutSell(StorageOutSellModel model,Hashtable ht, List<StorageOutSellDetailModel> modelList)
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
                isSucc = StorageOutSellDBHelper.UpdateStorageOutSell(model,ht, modelList);
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

        #region 删除：销售出库信息
        /// <summary>
        /// 删除销售出库信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteStorageOutSell(string ID, string CompanyCD)
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
                isSucc = StorageOutSellDBHelper.DeleteStorageOutSell(ID, CompanyCD);
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
        public static bool ConfirmBill(StorageOutSellModel model,out string retval)
        {
            retval = "";
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
                isSucc = StorageOutSellDBHelper.ConfirmBill(model, out retval);
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
        public static bool CloseBill(StorageOutSellModel model)
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
                isSucc = StorageOutSellDBHelper.CloseBill(model);
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
        public static bool CancelCloseBill(StorageOutSellModel model)
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
                isSucc = StorageOutSellDBHelper.CancelCloseBill(model);
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

        #region 获取销售发货通知单
        /// <summary>
        /// 获取销售发货通知单
        /// </summary>
        /// <param name="companycd"></param>
        /// <returns></returns>
        public static DataTable GetSellSendList(SellSendModel model)
        {
            try
            {
                return StorageOutSellDBHelper.GetSellSendList(model);
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region 销售发货明细列表（弹出层显示）
        /// <summary>
        /// 销售发货明细列表(弹出层显示)
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSSDetailInfo(string CompanyCD, string SendNo, string Title)
        {
            return StorageOutSellDBHelper.GetSSDetailInfo(CompanyCD, SendNo, Title);
        }
        #endregion

        #region 根据销售发货单明细中ID数组来获取信息（填充出库单中的明细）
        /// <summary>
        /// 根据销售退货单明细中ID数组来获取信息（填充入库单中的明细)
        /// </summary>
        /// <param name="strDetailIDList"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetInfoByDetalIDList(string strDetailIDList, string CompanyCD)
        {
            return StorageOutSellDBHelper.GetInfoByDetalIDList(strDetailIDList, CompanyCD);
        }
        #endregion

        #region 是否可以被确认，判断明细中出库数量是否小于源单明细未出库数量
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns></returns>
        public static bool ISConfirmBill(StorageOutSellModel model)
        {
            return StorageOutSellDBHelper.ISConfirmBill(model);
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
            logSys.ModuleID = ConstUtil.MODULE_ID_STORAGE_STORAGEOUTSELL_ADD;
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
            logModel.ModuleID = ConstUtil.MODULE_ID_STORAGE_STORAGEOUTSELL_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = "StorageOutSell";
            //操作对象
            logModel.ObjectID = ID;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion


        #region 确认的时候判断,当物品不允许负库存，是否大于可用库存
        /// <summary>
        /// 查找出当前单据中明细，所有不允许的负库存的物品，然后判断是否出库数量大于负库存
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns>string:行号数组|对应行号的可用库存</returns>
        public static string ISBigUseCountWhenCant(StorageOutSellModel model)
        {
            return StorageOutSellDBHelper.ISBigUseCountWhenCant(model);
        }
        #endregion

        #region 确认的时候判断,当物品允许负库存，是否大于可用库存
        /// <summary>
        /// 查找出当前单据中明细，所有允许的负库存的物品，然后判断是否出库数量大于负库存
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns>string:行号数组|对应行号的可用库存</returns>
        public static string ISBigUseCountWhenCan(StorageOutSellModel model)
        {
            return StorageOutSellDBHelper.ISBigUseCountWhenCan(model);
        }
        #endregion


        #region 单据打印
        public static DataTable GetStorageOutSellInfo(string ID, string CompanyCD)
        {
            try
            {
                return StorageOutSellDBHelper.GetStorageOutSellInfo(ID, CompanyCD);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DataTable GetStorageOutSellDetailInfo(string ID, string CompanyCD)
        {
            try
            {
                return StorageOutSellDBHelper.GetStorageOutSellDetailInfo(ID, CompanyCD);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region 根据仓库，商品ID获取批次列表
        /// <summary>
        /// 根据仓库，商品ID获取批次列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetBatchNoList(string StorageID, string ProductID, string CompanyCD)
        {
            return StorageOutSellDBHelper.GetBatchNoList(StorageID,ProductID,CompanyCD);
        }
        #endregion
    }
}
