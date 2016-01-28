/***********************************************
 * 类作用：   采购管理事务层处理               *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/04/28                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Model.Office.PurchaseManager;
using XBase.Data.Office.PurchaseManager;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;
using System.Data.SqlTypes;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.PurchaseManager
{
    // <summary>
    /// 类名：ProviderProductBus
    /// 描述：采购管理事务层处理
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/28
    /// 最后修改时间：2009/04/28
    /// </summary>
    public class ProviderProductBus
    {
        #region 新建供应商物品推荐
        public static bool InsertProviderProduct(ProviderProductModel model, out string ID)
        {
            try
            {
                bool succ = false;
                succ = ProviderProductDBHelper.InsertProviderProduct(model, out ID);
                string ProviderProductID = ID;
                LogInfoModel logModel = InitLogInfo("推荐物品ID：" + ProviderProductID);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                //设置模块ID 模块ID请在ConstUtil中定义，以便维护
                logModel.ModuleID = ConstUtil.MODULE_ID_PROVIDERPRODUCT_ADD;
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 更新供应商联系人
        public static bool UpdateProviderProduct(ProviderProductModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            if (model.ID <= 0)
            {
                return false;
            }
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo("推荐物品ID：" + Convert.ToString(model.ID));
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                //设置模块ID 模块ID请在ConstUtil中定义，以便维护
                logModel.ModuleID = ConstUtil.MODULE_ID_PROVIDERPRODUCT_ADD;
                succ = ProviderProductDBHelper.UpdateProviderProduct(model);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 查询采购供应商物品推荐列表所需数据
        public static DataTable SelectProviderProduct(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string CustNo, string ProductID, string Grade, string Joiner, string StartJoinDate, string EndJoinDate)
        {
            try
            {
                return ProviderProductDBHelper.SelectProviderProductList(pageIndex, pageCount, orderBy, ref TotalCount, CustNo, ProductID, Grade, Joiner, StartJoinDate, EndJoinDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除采购供应商物品推荐
        public static bool DeleteProviderProduct(string ID, string CompanyCD)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            try
            {
                bool isSucc = ProviderProductDBHelper.DeleteProviderProduct(ID, CompanyCD);
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
                    LogInfoModel logModel = InitLogInfo("推荐物品ID：" + no);
                    //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                    logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                    //设置模块ID 模块ID请在ConstUtil中定义，以便维护
                    logModel.ModuleID = ConstUtil.MODULE_ID_PROVIDERPRODUCTINFO;
                    //设置操作成功标识
                    logModel.Remark = remark;

                    //登陆日志
                    LogDBHelper.InsertLog(logModel);
                }
                return isSucc;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        #endregion

        #region 获取单个供应商物品推荐
        public static DataTable SelectProviderProduct(int ID)
        {
            try
            {
                return ProviderProductDBHelper.SelectProviderProduct(ID);
            }
            catch
            {
                return null;
            }
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
            ////设置模块ID 模块ID请在ConstUtil中定义，以便维护
            //logModel.ModuleID = ConstUtil.MODULE_ID_PROVIDERPRODUCT_ADD;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PROVIDERPRODUCT;
            //操作对象
            logModel.ObjectID = ID;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;
        }
        #endregion

        #region 校验物品ID是否存在于表中
        public static DataTable ISCunzaiProviderProduct(int ProductID, string CompanyCD)
        {
            try
            {
                return ProviderProductDBHelper.ISCunzaiProviderProduct(ProductID, CompanyCD);
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
