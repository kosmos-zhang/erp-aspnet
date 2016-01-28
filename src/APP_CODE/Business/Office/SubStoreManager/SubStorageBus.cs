/***********************************************
 * 类作用：   门店管理事务层处理               *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/05/21                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Model.Office.SubStoreManager;
using XBase.Data.Office.SubStoreManager;
using System.Data.SqlClient;
using System.Collections;
using XBase.Data.DBHelper;
using XBase.Common;
using System.Data.SqlTypes;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.SubStoreManager
{
    // <summary>
    /// 类名：SubStorageBus
    /// 描述：门店管理库存事务层处理
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/05/21
    /// </summary>
    public class SubStorageBus
    {
        #region 绑定门店仓库
        /// <summary>
        /// 绑定门店仓库
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetdrpStorageName()
        {
            DataTable dt = SubStorageDBHelper.GetdrpStorageName();
            return dt;
        }
        #endregion

        #region 新建门店初始化入库单
        public static bool InsertSubStorageIn(SubStorageInModel model, string DetailProductID, string DetailSendCount, string DetailUsedUnitID, string DetailUsedUnitCount, string DetailUsedPrice, string DetailExRate, string DetailBatchNo, string length, out string ID, Hashtable htExtAttr)
        {
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.InNo);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                //设置模块ID 模块ID请在ConstUtil中定义，以便维护
                logModel.ModuleID = ConstUtil.MODULE_ID_SUBSTOREMANAGER_SUBSTORAGEINIT;
                succ = SubStorageDBHelper.InsertSubStorageIn(model, DetailProductID, DetailSendCount, DetailUsedUnitID, DetailUsedUnitCount, DetailUsedPrice, DetailExRate, DetailBatchNo, length, out ID, htExtAttr);
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
        public static bool UpdateSubStorageIn(SubStorageInModel model, string DetailProductID, string DetailSendCount, string DetailUsedUnitID, string DetailUsedUnitCount, string DetailUsedPrice, string DetailExRate, string DetailBatchNo, string length, string no, Hashtable htExtAttr)
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
                logModel.ModuleID = ConstUtil.MODULE_ID_SUBSTOREMANAGER_SUBSTORAGEINIT;
                succ = SubStorageDBHelper.UpdateSubStorageIn(model, DetailProductID, DetailSendCount, DetailUsedUnitID, DetailUsedUnitCount, DetailUsedPrice, DetailExRate, DetailBatchNo, length, no, htExtAttr);
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

        #region 确认门店初始化入库单
        public static bool ConfirmSubStorageIn(SubStorageInModel Model, string DetailProductID, string DetailSendCount, string DetailUnitPrice, string DetailBatchNo, string length)
        {
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(Model.InNo);
                logModel.Element = ConstUtil.LOG_PROCESS_CONFIRM;
                logModel.ModuleID = ConstUtil.MODULE_ID_SUBSTOREMANAGER_SUBSTORAGEINIT;
                succ = SubStorageDBHelper.ConfirmSubStorageIn(Model, DetailProductID, DetailSendCount, DetailUnitPrice, DetailBatchNo, length);
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

        #region 查询门店分店期初库存列表所需数据
        public static DataTable SelectSubStorageInitList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string DeptID, string ProductID, string ProductName, string EFIndex, string EFDesc, string BatchNo)
        {
            try
            {
                return SubStorageDBHelper.SelectSubStorageInitList(pageIndex, pageCount, orderBy, ref TotalCount, DeptID, ProductID, ProductName, EFIndex, EFDesc, BatchNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取门店期初库存录入
        public static DataTable SubStorageIn(int ID)
        {
            try
            {
                return SubStorageDBHelper.SubStorageIn(ID);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 获取门店期初库存录入明细
        public static DataTable Details(int ID, string DeptID)
        {
            try
            {
                return SubStorageDBHelper.Details(ID, DeptID);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 查询门店分店库存列表所需数据
        public static DataTable SelectSubStorageProduct(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string DeptID, string ProductID, string ProductName, string BatchNo)
        {
            try
            {
                return SubStorageDBHelper.SelectSubStorageProduct(pageIndex, pageCount, orderBy, ref TotalCount, DeptID, ProductID, ProductName, BatchNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询门店总部库存列表所需数据
        public static DataTable SelectStorageProduct(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string StorageID, string ProductID, string ProductName, string BatchNo)
        {
            try
            {
                return SubStorageDBHelper.SelectStorageProduct(pageIndex, pageCount, orderBy, ref TotalCount, StorageID, ProductID, ProductName, BatchNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除门店入库单
        public static bool DeleteSubStorageIn(string IDs, string InNos, string CompanyCD)
        {
            var BIANHAO = InNos.Replace("'", "");//此方法当编号规则里不存在'时可以
            LogInfoModel logModel = InitLogInfo(BIANHAO);
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_SUBSTOREMANAGER_SUBSTORAGEINITLIST;

            ArrayList lstDelete = new ArrayList();

            SqlCommand deletePri = SubStorageDBHelper.DeleteSubStorageIn(IDs);
            lstDelete.Add(deletePri);

            SqlCommand deleteDeteil = SubStorageDBHelper.DeleteSubStorageInDetail(InNos, CompanyCD);
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
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_SUBSTOREMANAGER_SUBSTORAGEIN;
            //操作对象
            logModel.ObjectID = InNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;
        }
        #endregion

        #region 报表相关操作
        /// <summary>
        /// 根据销售时间生成报表
        /// </summary>
        /// <param name="Querytime"></param>
        /// <returns></returns>
        public static DataTable GetSubSellInfo(DateTime Querytime, DateTime QueryEndtime)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetSubSellInfo(Querytime, QueryEndtime, strCompanyCD);
        }
        /// <summary>
        /// 门店销售额按部门分析-导出明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public static DataTable GetSubSellList(string CompanyCD, string BeginDate, string EndDate, string DeptID)
        {
            SqlCommand comm = SubStorageDBHelper.SelectSubSellOrder(CompanyCD, BeginDate, EndDate, DeptID, "");
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 门店销售额按部门分析-点击查看详细信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSubSellDetail(string CompanyCD, string BeginDate, string EndDate, string DeptID, string OrderBy, int PageIndex, int PageCount, ref int TotalCount)
        {
            SqlCommand comm = SubStorageDBHelper.SelectSubSellOrder(CompanyCD, BeginDate, EndDate, DeptID, "");
            //执行查询
            return SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, OrderBy, ref TotalCount);
        }
        /// <summary>
        /// 根据销售时间，产品编号生成报表(查询)
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="strCompanyCD"></param>
        /// <param name="ProID"></param>
        /// <returns></returns>
        public static DataTable GetSubProductSellInfo(DateTime Querytime, DateTime Endtime, string proid, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetSubProductSellInfo(Querytime, Endtime, strCompanyCD, proid, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        /// <summary>
        /// 根据销售时间，产品编号生成报表(打印)
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="strCompanyCD"></param>
        /// <param name="ProID"></param>
        /// <returns></returns>
        public static DataTable GetSubProductSellInfo(DateTime Querytime, DateTime Endtime, string proid, string ordercolumn, string ordertype)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetSubProductSellInfo(Querytime, Endtime, strCompanyCD, proid, ordercolumn, ordertype);
        }
        /// <summary>
        /// 业务员/部门
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="Endtime"></param>
        /// <param name="Dept"></param>
        /// <returns></returns>
        public static DataTable GetSubProductSellInfoByDept(DateTime Querytime, DateTime Endtime, string Dept, string Flag, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetSubProductSellInfoByDept(Querytime, Endtime, strCompanyCD, Dept, Flag, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        public static DataTable GetSubProductSellInfoByDept(DateTime Querytime, DateTime Endtime, string Dept, string Flag, string ordercolumn, string ordertype)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetSubProductSellInfoByDept(Querytime, Endtime, strCompanyCD, Dept, Flag, ordercolumn, ordertype);
        }
        #endregion

        /// <summary>
        /// 门店销售明细
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="Endtime"></param>
        /// <param name="strCompanyCD"></param>
        /// <param name="Dept"></param>
        /// <returns></returns>
        public static DataTable GetSubSellDetaileInfoByDept(DateTime Querytime, DateTime Endtime, string Dept, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetSubSellDetaileInfoByDept(Querytime, Endtime, strCompanyCD, Dept, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        public static DataTable GetSubSellDetaileInfoByDept(DateTime Querytime, DateTime Endtime, string Dept, string ordercolumn, string ordertype)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetSubSellDetaileInfoByDept(Querytime, Endtime, strCompanyCD, Dept, ordercolumn, ordertype);
        }
        /// <summary>
        /// 业务员销售明细
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="Endtime"></param>
        /// <param name="strCompanyCD"></param>
        /// <param name="Dept"></param>
        /// <returns></returns>
        public static DataTable GetSubSellDetaileInfoBySeller(DateTime Querytime, DateTime Endtime, string Dept, string Seller, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetSubSellDetaileInfoBySeller(Querytime, Endtime, strCompanyCD, Dept, Seller, pageIndex, pageCount, OrderBy, ref totalCount);
        }

        public static DataTable GetSubSellDetaileInfoBySeller(DateTime Querytime, DateTime Endtime, string Dept, string Seller, string ordercolumn, string ordertype)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetSubSellDetaileInfoBySeller(Querytime, Endtime, strCompanyCD, Dept, Seller, ordercolumn, ordertype);
        }
        /// <summary>
        /// 产品销售汇总（查询）
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataTable GetProductTotalInfo(DateTime Querytime, DateTime Endtime, string Dept, string prod, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetProductTotalInfo(Querytime, Endtime, strCompanyCD, Dept, prod, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        /// <summary>
        /// 产品销售汇总(打印)
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataTable GetProductTotalInfo(DateTime Querytime, DateTime Endtime, string Dept, string prod, string ordercolumn, string ordertype)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetProductTotalInfo(Querytime, Endtime, strCompanyCD, Dept, prod, ordercolumn, ordertype);
        }

        /// <summary>
        /// 产品销售明细
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataTable GetProductSellDetailInfo(DateTime Querytime, DateTime Endtime, string Dept, string ProID, string ordercolumn, string ordertype)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetProductSellDetailInfo(Querytime, Endtime, strCompanyCD, Dept, ProID, ordercolumn, ordertype);
        }
        /// <summary>
        /// 产品销售明细
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataTable GetProductSellDetailInfo(DateTime Querytime, DateTime Endtime, string Dept, string ProID, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetProductSellDetailInfo(Querytime, Endtime, strCompanyCD, Dept, ProID, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        /// <summary>
        /// 收款员业务汇总（查询）
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataTable GetSellerBusTotal(DateTime Querytime, DateTime Endtime, string Dept, string Seller, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetSellerBusTotal(Querytime, Endtime, strCompanyCD, Dept, Seller, pageIndex, pageCount, OrderBy, ref totalCount);

        }
        /// <summary>
        /// 收款员业务汇总(打印)
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataTable GetSellerBusTotal(DateTime Querytime, DateTime Endtime, string Dept, string Seller, string ordercolumn, string ordertype)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetSellerBusTotal(Querytime, Endtime, strCompanyCD, Dept, Seller, ordercolumn, ordertype);
        }
        /// <summary>
        /// 收款员业务明细(查询)
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataTable GetSellerBusDetail(DateTime Querytime, DateTime Endtime, string Dept, string Seller, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetSellerBusDetail(Querytime, Endtime, strCompanyCD, Dept, Seller, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        /// <summary>
        /// 收款员业务明细（打印）
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataTable GetSellerBusDetail(DateTime Querytime, DateTime Endtime, string Dept, string Seller, string ordercolumn, string ordertype)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            return SubStorageDBHelper.GetSellerBusDetail(Querytime, Endtime, strCompanyCD, Dept, Seller, ordercolumn, ordertype);
        }

        //-----------------------------改版报表
        /// <summary>
        /// 门店销售额走势
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="Method"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static DataTable GetSubStorageSellTen(string CompanyCD, string Method, string BeginDate, string EndDate, string DeptID)
        {
            return SubStorageDBHelper.GetSubStorageSellTen(CompanyCD, Method, BeginDate, EndDate, DeptID);
        }

        /// <summary>
        /// 门店销售额走势-查看明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="Method"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <param name="XValue"></param>
        /// <returns></returns>
        public static DataTable GetSubStorageSellTenDetail(string CompanyCD, string Method, string BeginDate, string EndDate, string DeptID, string OrderBy, string XValue, int PageIndex, int PageCount, ref int TotalCount)
        {
            SqlCommand comm = SubStorageDBHelper.GetSubStorageSellTenDetail(CompanyCD, Method, BeginDate, EndDate, DeptID, OrderBy, XValue);
            return SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, OrderBy, ref TotalCount);
        }

        /// <summary>
        ///  门店销售额走势-导出
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="Method"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <param name="XValue"></param>
        /// <returns></returns>
        public static DataTable GetSubStorageSellTenOut(string CompanyCD, string Method, string BeginDate, string EndDate, string DeptID, string OrderBy, string XValue)
        {
            SqlCommand comm = SubStorageDBHelper.GetSubStorageSellTenDetail(CompanyCD, Method, BeginDate, EndDate, DeptID, OrderBy, XValue);
            return SqlHelper.ExecuteSearch(comm);
        }


        /// <summary>
        /// 门店销数量走势
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="Method"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static DataTable GetSubSellCountTen(string CompanyCD, string Method, string BeginDate, string EndDate, string DeptID)
        {
            return SubStorageDBHelper.GetSubSellCountTen(CompanyCD, Method, BeginDate, EndDate, DeptID);
        }

        /// <summary>
        /// 门店销数量走势-查看明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="Method"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <param name="XValue"></param>
        /// <returns></returns>
        public static DataTable GetSubSellCountTenDetail(string CompanyCD, string Method, string BeginDate, string EndDate, string DeptID, string OrderBy, string XValue, int PageIndex, int PageCount, ref int TotalCount)
        {
            SqlCommand comm = SubStorageDBHelper.GetSubSellCountTenDetail(CompanyCD, Method, BeginDate, EndDate, DeptID, OrderBy, XValue);
            return SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, OrderBy, ref TotalCount);
        }

        /// <summary>
        ///  门店销数量走势-导出
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="Method"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <param name="XValue"></param>
        /// <returns></returns>
        public static DataTable GetSubSellCountTenDetailOut(string CompanyCD, string Method, string BeginDate, string EndDate, string DeptID, string OrderBy, string XValue)
        {
            SqlCommand comm = SubStorageDBHelper.GetSubSellCountTenDetail(CompanyCD, Method, BeginDate, EndDate, DeptID, OrderBy, XValue);
            return SqlHelper.ExecuteSearch(comm);
        }

        /// <summary>
        ///  门店销售额按部门分析
        /// </summary>
        /// <param name="Querytime"></param>
        /// <returns></returns>
        public static DataTable GetSubSellInfoByDept(DateTime Querytime, DateTime QueryEndtime, string ProductID)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {

            }
            return SubStorageDBHelper.GetSubSellInfoByDept(Querytime, QueryEndtime, strCompanyCD, ProductID);
        }
        /// <summary>
        /// 门店销售额按部门分析-导出明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public static DataTable GetSubSellListByDept(string CompanyCD, string BeginDate, string EndDate, string ProductID, string DeptID)
        {
            SqlCommand comm = SubStorageDBHelper.SelectSubSellOrderByDeptID(CompanyCD, BeginDate, EndDate, ProductID, DeptID);
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 门店销售额按部门分析-查看详细信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSubSellDetailByDept(string CompanyCD, string BeginDate, string EndDate, string ProductID, string DeptID, string OrderBy, int PageIndex, int PageCount, ref int TotalCount)
        {
            SqlCommand comm = SubStorageDBHelper.SelectSubSellOrderByDeptID(CompanyCD, BeginDate, EndDate, ProductID, DeptID);
            //执行查询
            return SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, OrderBy, ref TotalCount);
        }

        /// <summary>
        ///  门店销售数量产品分析
        /// </summary>
        /// <param name="Querytime"></param>
        /// <returns></returns>
        public static DataTable GetSubSellInfoByProduct(DateTime Querytime, DateTime QueryEndtime, string DepID)
        {
            if (Querytime == null)
                return null;
            string strCompanyCD = string.Empty;
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {

            }
            return SubStorageDBHelper.GetSubSellInfoByProduct(Querytime, QueryEndtime, strCompanyCD, DepID);
        }
        /// <summary>
        /// 门店销售数量产品分析-导出明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public static DataTable GetSubSellListByProduct(string CompanyCD, string BeginDate, string EndDate, string ProductID, string DeptID)
        {
            SqlCommand comm = SubStorageDBHelper.GetSubSellInfoByProduct(CompanyCD, BeginDate, EndDate, ProductID, DeptID);
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 门店销售数量产品分析-查看详细信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSubSellDetailByProduct(string CompanyCD, string BeginDate, string EndDate, string ProductID, string DeptID, string OrderBy, int PageIndex, int PageCount, ref int TotalCount)
        {
            SqlCommand comm = SubStorageDBHelper.GetSubSellInfoByProduct(CompanyCD, BeginDate, EndDate, ProductID, DeptID);
            //执行查询
            return SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, OrderBy, ref TotalCount);
        }

        #region 分店期初批量导入

        /// <summary>
        /// 批量导入数据
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="userInfo">登录信息</param>
        /// <returns></returns>
        public static bool ImportData(DataTable dt, UserInfoUtil userInfo)
        {
            return SubStorageDBHelper.ImportData(dt, userInfo);
        }

        #endregion
    }
}
