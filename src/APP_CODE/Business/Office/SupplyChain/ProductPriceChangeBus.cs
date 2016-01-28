using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.SupplyChain;
using XBase.Data.Office.SupplyChain;
using XBase.Common;
using System.Data;
using XBase.Model.Common;
using XBase.Business.Common;
using XBase.Data.Common;
namespace XBase.Business.Office.SupplyChain
{
   public class ProductPriceChangeBus
    {
        /// <summary>
        /// 插入物品变更信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertProductPriceInfo(ProductPriceChangeModel model, out string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ID = "0";
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.ChangeNo);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                succ = ProductPriceChangeDBHelper.InsertProductPriceInfo(model, out ID);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;

            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex);
                return false;
            }
        }
        /// <summary>
        /// 修改物品档案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateProductPriceInfo(ProductPriceChangeModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.ChangeNo);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                succ = ProductPriceChangeDBHelper.UpdateProductPriceInfo(model);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex);
                return false;
            }
        }
        /// <summary>
        /// 查询获取物品信息
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static DataTable GetProductPriceInfo(ProductPriceChangeModel Model, string Starttime, string Endtime, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return ProductPriceChangeDBHelper.GetProductPriceInfo(Model, Starttime, Endtime, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        /// <summary>
        /// 根据ID获取物品信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetProductPriceInfoByID(int ID)
        {
            if (ID == 0)
                return null;
            try
            {
                return ProductPriceChangeDBHelper.GetProductPriceInfoByID(ID);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        /// <summary>
        /// 删除物品信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteProductPriceInfo(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string CompanyCD = userInfo.CompanyCD;
            //string CompanyCD = "AAAAAA";
            bool isSucc = ProductPriceChangeDBHelper.DeleteProductPriceInfo(ID, CompanyCD);
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
            //获取删除的编号列表
            string[] noList = ID.Split(',');
            //遍历所有编号，登陆操作日志
            for (int i = 0; i < noList.Length; i++)
            {
                //获取编号
                string no = noList[i];
                //替换两边的 '
                no = no.Replace("'", string.Empty);

                //操作日志
                LogInfoModel logModel = InitLogInfo("变更单ID：" + no);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                //设置操作成功标识
                logModel.Remark = remark;

                //登陆日志
                LogDBHelper.InsertLog(logModel);
            }
            return isSucc;

        }

        public static bool UpdateStatus(int ID, string BillStatus, int ProductID, string StandardSell, string SellTax, string Confirmor, string ConfirmDate, string TaxRateNew, string DiscountNew)
        { 
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string ModifiedUserID = userInfo.UserID;
            if (string.IsNullOrEmpty(BillStatus)) return false;
            try
            {
                return ProductPriceChangeDBHelper.UpdateStatus(ID, BillStatus,ProductID,StandardSell,SellTax, Confirmor, ConfirmDate,TaxRateNew,DiscountNew,ModifiedUserID);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
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
            logSys.ModuleID = ConstUtil.Menu_AddProductPrice;
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
        private static LogInfoModel InitLogInfo(string prodno)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.Menu_AddProductPrice;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PRODUCTINFOPRICE;
            //操作对象
            logModel.ObjectID = prodno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion
    }
}
