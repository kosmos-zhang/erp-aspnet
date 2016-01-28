/***********************************************
 * 类作用：   门店管理事务层处理               *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/05/22                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Model.Office.SubStoreManager;
using XBase.Data.Office.SubStoreManager;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;
using System.Data.SqlTypes;
using System.Collections;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Data.Office.FinanceManager;

namespace XBase.Business.Office.SubStoreManager
{
    // <summary>
    /// 类名：SubSellBackBus
    /// 描述：门店管理库存事务层处理
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/05/22
    /// </summary>
    public class SubSellBackBus
    {
        #region 绑定门店销售退货币种
        /// <summary>
        /// 绑定门店退货币种
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCurrenyType()
        {
            DataTable dt = CurrTypeSettingDBHelper.GetCurrenyType();
            return dt;
        }
        #endregion

        #region 绑定门店销售退货仓库
        /// <summary>
        /// 绑定门店仓库
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetdrpStorageID()
        {
            DataTable dt = SubSellBackDBHelper.GetdrpStorageID();
            return dt;
        }
        #endregion

        #region 新建门店销售退货单
        /// <summary>
        /// 新建门店销售退货单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <param name="DetailProductID"></param>
        /// <param name="DetailProductNo"></param>
        /// <param name="DetailProductName"></param>
        /// <param name="DetailUnitID"></param>
        /// <param name="DetailProductCount"></param>
        /// <param name="DetailBackCount"></param>
        /// <param name="DetailUnitPrice"></param>
        /// <param name="DetailTaxPrice"></param>
        /// <param name="DetailDiscount"></param>
        /// <param name="DetailTaxRate"></param>
        /// <param name="DetailTotalPrice"></param>
        /// <param name="DetailTotalFee"></param>
        /// <param name="DetailTotalTax"></param>
        /// <param name="DetailStorageID"></param>
        /// <param name="DetailFromBillID"></param>
        /// <param name="DetailFromLineNo"></param>
        /// <param name="DetailRemark"></param>
        /// <param name="length"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool InsertSubSellBack(SubSellBackModel model, Hashtable htExtAttr, string DetailProductID, string DetailProductNo, string DetailProductName, string DetailUnitID, string DetailProductCount, string DetailBackCount, string DetailUnitPrice, string DetailTaxPrice, string DetailDiscount, string DetailTaxRate, string DetailTotalPrice, string DetailTotalFee, string DetailTotalTax, string DetailStorageID, string DetailFromBillID, string DetailFromLineNo, string DetailRemark, string DetailUsedUnitID, string DetailUsedUnitCount, string DetailUsedPrice, string DetailExRate, string DetailBatchNo, string length, out string ID)
        {
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.BackNo);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                //设置模块ID 模块ID请在ConstUtil中定义，以便维护
                logModel.ModuleID = ConstUtil.MODULE_ID_SUBSTOREMANAGER_SUBSELLBACKADD;
                succ = SubSellBackDBHelper.InsertSubSellBack(model, htExtAttr, DetailProductID, DetailProductNo, DetailProductName, DetailUnitID, DetailProductCount, DetailBackCount, DetailUnitPrice, DetailTaxPrice, DetailDiscount, DetailTaxRate, DetailTotalPrice, DetailTotalFee, DetailTotalTax, DetailStorageID, DetailFromBillID, DetailFromLineNo, DetailRemark, DetailUsedUnitID, DetailUsedUnitCount, DetailUsedPrice, DetailExRate, DetailBatchNo, length, out ID);
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

        #region 更新门店初始化入库单
        public static bool UpdateSubSellBack(SubSellBackModel model, Hashtable htExtAttr, string DetailProductID, string DetailProductNo, string DetailProductName, string DetailUnitID, string DetailProductCount, string DetailBackCount, string DetailUnitPrice, string DetailTaxPrice, string DetailDiscount, string DetailTaxRate, string DetailTotalPrice, string DetailTotalFee, string DetailTotalTax, string DetailStorageID, string DetailFromBillID, string DetailFromLineNo, string DetailRemark, string DetailUsedUnitID, string DetailUsedUnitCount, string DetailUsedPrice, string DetailExRate, string DetailBatchNo, string length, string no)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            if (model.ID <= 0)
            {
                return false;
            }
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(no);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                //设置模块ID 模块ID请在ConstUtil中定义，以便维护
                logModel.ModuleID = ConstUtil.MODULE_ID_SUBSTOREMANAGER_SUBSELLBACKADD;
                succ = SubSellBackDBHelper.UpdateSubSellBack(model, htExtAttr, DetailProductID, DetailProductNo, DetailProductName, DetailUnitID, DetailProductCount, DetailBackCount, DetailUnitPrice, DetailTaxPrice, DetailDiscount, DetailTaxRate, DetailTotalPrice, DetailTotalFee, DetailTotalTax, DetailStorageID, DetailFromBillID, DetailFromLineNo, DetailRemark, DetailUsedUnitID, DetailUsedUnitCount, DetailUsedPrice, DetailExRate, DetailBatchNo, length, no);
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

        #region 确认门店销售退货单
        public static bool ConfirmSubSellBack(SubSellBackModel model, string DetailBackCount, string DetailFromBillNo, string DetailFromLineNo, string length)
        {
            return SubSellBackDBHelper.ConfirmSubSellBack(model, DetailBackCount, DetailFromBillNo, DetailFromLineNo, length);
        }
        #endregion

        #region 取消确认门店销售退货单
        public static bool QxConfirmSubSellBack(SubSellBackModel model, string DetailBackCount, string DetailFromBillNo, string DetailFromLineNo, string length)
        {
            return SubSellBackDBHelper.QxConfirmSubSellBack(model, DetailBackCount, DetailFromBillNo, DetailFromLineNo, length);
        }
        #endregion

        #region 入库门店销售退货单
        public static bool RukuSubSellBack(SubSellBackModel model, string DetailBackCount, string DetailStorageID, string DetailProductID, string DetailUnitPrice, string DetailBatchNo, string length)
        {
            return SubSellBackDBHelper.RukuSubSellBack(model, DetailBackCount, DetailStorageID, DetailProductID, DetailUnitPrice, DetailBatchNo, length);
        }
        #endregion

        #region 结算门店销售退货单
        public static bool JiesuanubSellBack(SubSellBackModel model)
        {
            return SubSellBackDBHelper.JiesuanubSellBack(model);
        }
        #endregion

        #region 选择门店销售订单
        public static DataTable GetSubSellBackDetail(int DeptID, string OrderNo, string SendMode, string CompanyCD, int CurrencyTypeID, string Rate)
        {
            try
            {
                return SubSellBackDBHelper.GetSubSellBackDetail(DeptID, OrderNo, SendMode, CompanyCD, CurrencyTypeID, Rate);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 查询门店销售退货列表所需数据
        public static DataTable SelectSubSellBack(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string BackNo, string Title, string OrderID, string CustName, string CustTel, string DeptID, string Seller, string BusiStatus, string BillStatus, string CustAddr, string EFIndex, string EFDesc)
        {
            try
            {
                return SubSellBackDBHelper.SelectSubSellBack(pageIndex, pageCount, orderBy, ref TotalCount, BackNo, Title, OrderID, CustName, CustTel, DeptID, Seller, BusiStatus, BillStatus, CustAddr, EFIndex, EFDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取门店销售退货单记录
        public static DataTable SubSellBack(int ID)
        {
            try
            {
                return SubSellBackDBHelper.SubSellBack(ID);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 获取门店销售退货单明细
        public static DataTable Details(int ID)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return SubSellBackDBHelper.Details(ID, CompanyCD);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 删除门店销售退货单
        public static bool DeleteSubSellBack(string IDs, string BackNos, string CompanyCD)
        {
            var BIANHAO = BackNos.Replace("'", "");//此方法当编号规则里不存在'时可以
            LogInfoModel logModel = InitLogInfo(BIANHAO);
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_SUBSTOREMANAGER_SUBSELLBACKLIST;

            ArrayList lstDelete = new ArrayList();

            SqlCommand deletePri = SubSellBackDBHelper.DeleteSubSellBack(IDs);
            lstDelete.Add(deletePri);

            SqlCommand deleteDeteil = SubSellBackDBHelper.DeleteSubSellBackDetail(BackNos, CompanyCD);
            lstDelete.Add(deleteDeteil);

            bool isSucc = SqlHelper.ExecuteTransWithArrayList(lstDelete);
            string remark;
            //成功时
            if (isSucc)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
                logModel.Remark = remark;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
                logModel.Remark = remark;
            }
            LogDBHelper.InsertLog(logModel);
            return isSucc;
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
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_SUBSTOREMANAGER_SUBSELLBACK;
            //操作对象
            logModel.ObjectID = InNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;
        }
        #endregion
    }
}
