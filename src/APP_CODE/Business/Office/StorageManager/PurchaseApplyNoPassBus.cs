using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using XBase.Data.DBHelper;
using XBase.Data.Office.StorageManager;
using XBase.Business.Office.SystemManager;
using XBase.Model.Office.StorageManager;
using XBase.Business.Common;

namespace XBase.Business.Office.StorageManager
{
    public class PurchaseApplyNoPassBus
    {
        #region 采购不合格品统计表
        public static DataTable SearchPurNoPass(string Method,string BeginDate, string EndDate, string CustID,string myOrder,int pageIndex,int pageCount,ref string TotalPro,ref string TotalNotPass,ref int TotalCount)
        {
            //string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //return PurchaseApplyNoPassDBHelper.SearchPurApplyNoPass(Method,companyCD, BeginDate, EndDate, CustID, myOrder, pageIndex, pageCount, ref TotalPro, ref TotalNotPass, ref TotalCount);
            return null;
        }
        #endregion

        #region 生产不合格品统计表
        public static DataTable SearchManNoPass(string Method,string BeginDate, string EndDate,string DeptID,string myOrder,int pageIndex,int pageCount, ref string TotalPro, ref string TotalNotPass,ref int TotalCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return PurchaseApplyNoPassDBHelper.GetManNoPass(Method,companyCD,BeginDate, EndDate, DeptID,myOrder,pageIndex,pageCount, ref TotalPro, ref TotalNotPass,ref TotalCount);
        }
        #endregion

        #region 不合格品处置统计表
        public static DataTable SearchNoPass(string Method,string BeginDate,string EndDate,string ReasonID,string myOrder,int PageIndex,int PageCount,ref string TotalNotPassNum,ref int TotalCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return PurchaseApplyNoPassDBHelper.GetNoPass(Method,companyCD,BeginDate,EndDate,ReasonID,myOrder,PageIndex,PageCount,ref TotalNotPassNum,ref TotalCount);
        }
        #endregion


        #region 采购不合格按供应商分析
        /// <summary>
        /// 采购不合格按供应商分析
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="CustID"></param>
        /// <returns></returns>
        public static DataTable GetNoPassByProvider(string BeginDate, string EndDate, string ProductID)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return PurchaseApplyNoPassDBHelper.GetNoPassByProvider(companyCD, BeginDate, EndDate, ProductID);
        }
        /// <summary>
        /// 采购不合格按供应商分析-导出
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static DataTable GetNoPassByProviderOut(string BeginDate, string EndDate, string ProductID, string ProviderID)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            SqlCommand comm = PurchaseApplyNoPassDBHelper.SearchPurApplyNoPass(companyCD, BeginDate, EndDate, ProductID,"");
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 采购不合格按供应商分析-详细信息列表
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetNoPassByProviderList(string BeginDate, string EndDate, string ProductID,string ProviderID,string OrderBy,int PageIndex,int PageCount,ref int TotalCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            SqlCommand comm = PurchaseApplyNoPassDBHelper.SearchPurApplyNoPass(companyCD, BeginDate, EndDate, ProductID,ProviderID);
            return SqlHelper.PagerWithCommand(comm,PageIndex,PageCount,OrderBy,ref TotalCount);
        }
        #endregion

        #region 采购不合格按产品分析
        /// <summary>
        /// 采购不合格按产品分析
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="CustID"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static DataTable GetNoPassByProduct(string BeginDate, string EndDate, string CustID)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return PurchaseApplyNoPassDBHelper.GetNoPassByProduct(companyCD, BeginDate, EndDate, CustID);
        }

        /// <summary>
        /// 采购不合格按产品分析-详细信息列表
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="ProviderID"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetNoPassByProductList(string BeginDate, string EndDate, string ProductID, string ProviderID, string OrderBy, int PageIndex, int PageCount, ref int TotalCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            SqlCommand comm = PurchaseApplyNoPassDBHelper.SearchPurApplyNoPassByProduct(companyCD, BeginDate, EndDate, ProductID, ProviderID);
            return SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, OrderBy, ref TotalCount);
        }

        /// <summary>
        /// 采购不合格按产品分析-导出详细信息
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="ProviderID"></param>
        /// <returns></returns>
        public static DataTable GetNoPassByProductListOut(string BeginDate, string EndDate, string ProductID, string ProviderID)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            SqlCommand comm = PurchaseApplyNoPassDBHelper.SearchPurApplyNoPassByProduct(companyCD, BeginDate, EndDate, ProductID, ProviderID);
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion



        #region 采购不合格走势
        /// <summary>
        /// 采购不合格走势
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="CustID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static DataTable GetNoPassTendency(string BeginDate, string EndDate, string ProductID,string CustID,string Type)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return PurchaseApplyNoPassDBHelper.GetNoPassTendency(companyCD, BeginDate, EndDate, ProductID,CustID,Type);
 
        }
        /// <summary>
        /// 采购不合格走势-导出
        /// </summary>
        /// <param name="Method"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="CustID"></param>
        /// <param name="XValue"></param>
        /// <returns></returns>
        public static DataTable GetNoPassTendencyListOut(string Method,string BeginDate, string EndDate, string ProductID, string CustID, string XValue)
        {
            SqlCommand comm=PurchaseApplyNoPassDBHelper.GetNoPassTendencyDetail(Method,BeginDate,EndDate, ProductID, CustID, XValue);
            return SqlHelper.ExecuteSearch(comm);

        }
        /// <summary>
        /// 采购不合格走势-列表
        /// </summary>
        /// <param name="Method"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="CustID"></param>
        /// <param name="XValue"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetNoPassTendencyList(string Method, string BeginDate, string EndDate, string ProductID, string CustID, string XValue,string OrderBy,int PageIndex,int PageCount,ref int TotalCount)
        {
            SqlCommand comm=PurchaseApplyNoPassDBHelper.GetNoPassTendencyDetail(Method, BeginDate, EndDate, ProductID, CustID, XValue);
            return SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, OrderBy, ref TotalCount);

        } 
        #endregion

        #region 生产不合格按部门分析
        /// <summary>
        /// 生产不合格按部门分析
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static DataTable GetManNoPassByDept(string BeginDate, string EndDate, string ProductID)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return PurchaseApplyNoPassDBHelper.GetManNoPassByDept(companyCD, BeginDate, EndDate, ProductID);
        }
        /// <summary>
        /// 生产不合格按部门分析-导出
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static DataTable GetManNoPassByDeptOut(string BeginDate, string EndDate, string ProductID,string DeptID)
        {
    
            SqlCommand comm= PurchaseApplyNoPassDBHelper.GetManNoPassByDeptDetail(BeginDate, EndDate, ProductID,DeptID);
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 生产不合格按部门分析-列表
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetManNoPassByDeptList(string BeginDate, string EndDate, string ProductID, string DeptID,string OrderBy,int PageIndex,int PageCount,ref int TotalCount)
        {
            SqlCommand comm = PurchaseApplyNoPassDBHelper.GetManNoPassByDeptDetail(BeginDate, EndDate, ProductID, DeptID);
            return SqlHelper.PagerWithCommand(comm,PageIndex,PageCount,OrderBy,ref TotalCount);
        }
        #endregion

        #region 生产不合格按产品分析
        /// <summary>
        /// 生产不合格按产品分析
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static DataTable GetManNoPassByProduct(string BeginDate, string EndDate, string DeptID)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return PurchaseApplyNoPassDBHelper.GetManNoPassByProduct(companyCD, BeginDate, EndDate, DeptID);
        }
        /// <summary>
        /// 生产不合格按产品分析-导出
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static DataTable GetManNoPassByProductOut(string BeginDate, string EndDate, string ProductID, string DeptID)
        {

            SqlCommand comm = PurchaseApplyNoPassDBHelper.GetManNoPassByProductDetail(BeginDate, EndDate, ProductID, DeptID);
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 生产不合格按产品分析-明细
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetManNoPassByProductList(string BeginDate, string EndDate, string ProductID, string DeptID, string OrderBy, int PageIndex, int PageCount, ref int TotalCount)
        {
            SqlCommand comm = PurchaseApplyNoPassDBHelper.GetManNoPassByProductDetail(BeginDate, EndDate, ProductID, DeptID);
            return SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, OrderBy, ref TotalCount);
        }
        #endregion

        #region 生产不合格走势
        /// <summary>
        /// 生产不合格走势
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="DeptID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static DataTable GetManNoPassTendency(string BeginDate, string EndDate, string ProductID, string DeptID, string Type)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return PurchaseApplyNoPassDBHelper.GetManNoPassTendency(companyCD, BeginDate, EndDate, ProductID,DeptID,Type);
        }
        /// <summary>
        /// 生产不合格走势-导出
        /// </summary>
        /// <param name="TimeType"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="DeptID"></param>
        /// <param name="XValue"></param>
        /// <returns></returns>
        public static DataTable GetManNoPassTendencyOut(string TimeType,string BeginDate,string EndDate,string ProductID,string DeptID,string XValue)
        {
            SqlCommand comm = PurchaseApplyNoPassDBHelper.GetManNoPassTendencyDetail(TimeType, BeginDate, EndDate, ProductID, DeptID, XValue);
            return SqlHelper.ExecuteSearch(comm);


        }
        /// <summary>
        /// 生产不合格走势-列表
        /// </summary>
        /// <param name="TimeType"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="DeptID"></param>
        /// <param name="XValue"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetManNoPassTendencyList(string TimeType, string BeginDate, string EndDate, string ProductID, string DeptID, string XValue,string OrderBy,int PageIndex,int PageCount,ref int TotalCount)
        {
            SqlCommand comm = PurchaseApplyNoPassDBHelper.GetManNoPassTendencyDetail(TimeType, BeginDate, EndDate, ProductID, DeptID, XValue);
            return SqlHelper.PagerWithCommand(comm,PageIndex,PageCount,OrderBy,ref TotalCount);


        }
        #endregion

        #region 不合格产品处置分布
        /// <summary>
        /// 不合格产品处置分布
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static DataTable GetNoPassNum( string BeginDate, string EndDate, string ProductID)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return PurchaseApplyNoPassDBHelper.GetNoPassNum(companyCD, BeginDate, EndDate, ProductID);
        }
        #endregion

        #region  不合格产品处置分布-导出
        /// <summary>
        /// 不合格产品处置分布-导出
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetOutNoPass(string BeginDate, string EndDate, string ProductID)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return PurchaseApplyNoPassDBHelper.GetOutNoPassNum(companyCD, BeginDate, EndDate, ProductID);
        }
        #endregion

        #region 不合格产品处置分布-链接到详细信息
        /// <summary>
        /// 不合格产品处置分布-链接到详细信息
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="ProcessWay"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetNoPassDetail(string BeginDate, string EndDate, string ProductID, string ProcessWay, int PageIndex, int PageCount, string OrderBy, ref int TotalCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return PurchaseApplyNoPassDBHelper.GetNoPassDetail(companyCD, BeginDate, EndDate, ProductID, ProcessWay, PageIndex, PageCount, OrderBy, ref TotalCount);
        }
        /// <summary>
        /// 不合格产品处置分布-链接到详细信息的导出
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="ProcessWay"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public static DataTable GetNoPassDetailOut(string BeginDate, string EndDate, string ProductID, string ProcessWay, string OrderBy)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return PurchaseApplyNoPassDBHelper.GetNoPassDetailOut(companyCD, BeginDate, EndDate, ProductID, ProcessWay,OrderBy);
        }
        #endregion


    }
}
