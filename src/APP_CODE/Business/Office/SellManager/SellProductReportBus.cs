using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.SellManager;
using XBase.Data.Office.SellManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Business.Common;
using XBase.Data.Common;
namespace XBase.Business.Office.SellManager
{
    public class SellProductReportBus  //----------------------------Method 0列表页1打印页
    {
        #region  产品销售汇总-销售统计
        /// <summary>
        /// 产品销售汇总-销售报表
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSellProductReport(string method, int pageIndex, int pageSize, string OrderBy, string BeginTime, string EndTime, string CompanyCD, string CurrencyType, ref string TotalProductCount, ref string TotalTaxBuy, ref string TotalStandardBuy, ref int TotalCount)
        {
            return SellProductReport.GetSellProductReport(method,pageIndex,pageSize,OrderBy,BeginTime, EndTime, CompanyCD,CurrencyType,ref TotalProductCount, ref TotalTaxBuy, ref TotalStandardBuy,ref TotalCount);
        }
        #endregion

        #region 产品分类销售汇总-销售统计 
        public static DataTable GetSellProductTypeReport(string Method, string BeginTime, int pageIndex, int pageSize, string OrderBy, string EndTime, string CompanyCD, string CurrencyType, ref string TotalProductCount, ref string TotalStandardSell, ref string TotalSellTax, ref string TotalTax, ref int TotalCount)
        {
            return SellProductReport.GetProductType(Method, pageIndex, pageSize, OrderBy, BeginTime, EndTime, CompanyCD, CurrencyType, ref TotalProductCount, ref TotalStandardSell, ref TotalSellTax, ref TotalTax, ref TotalCount);
        }
        #endregion

        #region 客户购买年度统计-销售报表
        public static DataTable GetCustBuyYear(string method, int pageIndex, int pageSize, string OrderBy, string CompanyCD, string Cust, string Year, string CurrencyType,ref string TotalNowMonth, ref string TotalNowYear, ref string TotalLastMonth, ref string TotalLastYear, ref string TotalPrice, ref int TotalCount)
        {
            return SellProductReport.GetCustBuyYear(method,pageIndex,pageSize,OrderBy,CompanyCD, Cust, Year,CurrencyType, ref TotalNowMonth, ref TotalNowYear, ref TotalLastMonth, ref TotalLastYear, ref TotalPrice,ref TotalCount);
        }
        #endregion

        #region 客户购买季度统计-销售报表
        public static DataTable GetCustBuyQuarter(string method, int pageIndex, int pageSize, string OrderBy, string CompanyCD, string Cust, string quarter, string CurrencyType, ref string TotalPrice, ref string TotalPrice1, ref string TotalPrice2, ref string TotalPrice3, ref string TotalPrice4, ref int TotalCount)
        {
            return SellProductReport.GetCustBuyQuarter(method, pageIndex, pageSize, OrderBy, CompanyCD, Cust, quarter, CurrencyType, ref TotalPrice, ref TotalPrice1, ref TotalPrice2, ref TotalPrice3, ref TotalPrice4, ref TotalCount);
        }
        #endregion

        #region  客户购买明细统计-销售报表
        public static DataTable GetCustBuyDetail(string Method, int pageIndex, int pageSize, string OrderBy, string Cust, string Product, string BeginDate, string EndDate, string CurrencyType, ref string TotalProductCount, ref string TotalPrice, ref string TotalTax, ref string TotalBackNumber, ref string TotalBackTotalPrice, ref string TotalFee, ref int TotalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellProductReport.GetCustBuyDetail(Method, pageIndex, pageSize, OrderBy, CompanyCD, Cust, Product, BeginDate, EndDate, CurrencyType, ref TotalProductCount, ref TotalPrice, ref TotalTax, ref TotalBackNumber, ref TotalBackTotalPrice, ref TotalFee, ref TotalCount);
        }
        #endregion

        #region 产品销售年度统计
        public static DataTable GetProductYear(string Method, int pageIndex, int pageSize, string OrderBy, string ProductID, string Year, ref string TotalNowMonth, ref string TotalNowYear, ref string TotalLastMonth, ref string TotalLastYear, ref string TotalPrice,ref int TotalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellProductReport.GetProductYear(Method,pageIndex,pageSize,OrderBy,CompanyCD,ProductID, Year, ref TotalNowMonth, ref TotalNowYear, ref TotalLastMonth, ref TotalLastYear, ref TotalPrice,ref TotalCount);
        }
        #endregion

        #region 产品销售季度统计
        public static DataTable GetProductBuyQuarter(string Method, int pageIndex, int pageSize, string OrderBy,string ProductID, string quarter, ref string TotalPrice, ref string TotalPrice1, ref string TotalPrice2, ref string TotalPrice3, ref string TotalPrice4,ref int TotalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellProductReport.GetProductBuyQuarter(Method,pageIndex,pageSize,OrderBy,CompanyCD,ProductID, quarter, ref TotalPrice, ref TotalPrice1, ref TotalPrice2, ref TotalPrice3, ref TotalPrice4,ref TotalCount);
        }
        #endregion

        #region  产品销售明细统计
        public static DataTable GetProBuyDetail(string Method, int pageIndex, int pageSize, string OrderBy, string Product, string BeginDate, string EndDate, ref string TotalProductCount, ref string TotalPrice, ref string TotalBackCount, ref string TotalBackPrice,ref int TotalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellProductReport.GetProBuyDetail(Method,pageIndex,pageSize,OrderBy,CompanyCD, Product, BeginDate, EndDate, ref TotalProductCount, ref TotalPrice, ref TotalBackCount, ref TotalBackPrice,ref TotalCount);
        }
        #endregion

        #region  产品ABC统计
        public static DataTable GetABCType(string Method, int pageIndex, int pageSize, string OrderBy, string BeginDate, string EndDate, string ABCType, string Product, string Column, string ValueA, string ValueB, string ValueC, string Sift, ref string TotalPrice, ref string TotalProductCount,ref int TotalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellProductReport.GetABCPro(Method,pageIndex,pageSize,OrderBy,CompanyCD, BeginDate, EndDate, ABCType, Product, Column, ValueA, ValueB, ValueC, Sift,ref TotalPrice,ref TotalProductCount,ref TotalCount);
        }

        #endregion

        #region 销售价格比较
        /// <summary>
        /// 销售价格比较
        /// </summary>
        /// <param name="Product"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static DataTable GetSellPriceDB(string QueryStr, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellProductReport.GetSellPrice(QueryStr, CompanyCD, pageIndex, pageSize, OrderBy, ref totalCount);
        }
        public static DataTable GetSellPriceDB(string QueryStr,string OrderBy)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellProductReport.GetSellPrice(QueryStr, CompanyCD,OrderBy);
        }
        #endregion

        #region 销售区业绩分析
        public static DataTable GetSellAchievementGrowing(string QueryStr, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string CodeFlag = ConstUtil.SellAreaTypeFlag;
            string CodeType = ConstUtil.SellAreaCodeType;
            return SellProductReport.GetSellAchievementGrowing(CodeFlag,CodeType,QueryStr, CompanyCD, pageIndex, pageSize, OrderBy, ref totalCount);
        }
        public static DataTable GetSellAchievementGrowingPrint(string QueryStr, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string CodeFlag = ConstUtil.SellAreaTypeFlag;
            string CodeType = ConstUtil.SellAreaCodeType;
            return SellProductReport.GetSellAchievementGrowingPrint(CodeFlag, CodeType, QueryStr, CompanyCD, pageIndex, pageSize, OrderBy, ref totalCount);
        }
        //public static DataTable GetSellAchievementGrowing(string QueryStr, string OrderBy)
        //{
        //    string CodeFlag = ConstUtil.SellAreaTypeFlag;
        //    string CodeType = ConstUtil.SellAreaCodeType;
        //    string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //    return SellProductReport.GetSellAchievementGrowing(CodeFlag,CodeType,QueryStr, CompanyCD, OrderBy);
        //}
        public static DataTable GetArea()
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string CodeFlag = ConstUtil.SellAreaTypeFlag;
            string CodeType = ConstUtil.SellAreaCodeType;
            return SellProductReport.GetArea(CompanyCD,CodeFlag,CodeType);
        }

        #endregion

        #region 同期比较分析
        /// <summary>
        /// 同期比较分析
        /// </summary>
        /// <param name="Method">0列表页1打印页需要</param>
        /// <param name="Compare1"></param>
        /// <param name="Compare2"></param>
        /// <param name="CurrencyID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetSameTerm(string Method,string Compare1, string Compare2, string CurrencyID, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
              string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
              return SellProductReport.GetSameTerm(Method,CompanyCD, Compare1, Compare2, CurrencyID,pageIndex,pageSize,OrderBy,ref totalCount);
        }
        #endregion 

        #region 客户应收款查询
        public static DataTable GetReceivables(string CustNo, string CustName, string BeginDate, string EndDate,int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellProductReport.GetReceivables(CompanyCD,CustNo,CustName,BeginDate,EndDate,pageIndex,pageSize,OrderBy,ref totalCount);
        }

        public static DataTable GetReceivablesReport(string CustNo, string CustName, string BeginDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellProductReport.GetReceivablesReport(CompanyCD, CustNo, CustName, BeginDate, EndDate, pageIndex, pageSize, OrderBy, ref totalCount);
        }
        /// <summary>
        /// 获取汇总信息
        /// </summary>
        /// <param name="CustNo"></param>
        /// <param name="CustName"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetReceivablesAll(string CustNo, string CustName, string BeginDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellProductReport.GetReceivalesAll(CompanyCD, CustNo, CustName, BeginDate, EndDate, pageIndex, pageSize, OrderBy, ref totalCount);
        }
        #endregion

        #region  客户台帐查询
        public static DataTable GetCustSell(string CustNo, string CustName, string BeginDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellProductReport.GetCustSell(CompanyCD, CustNo, CustName, BeginDate, EndDate, pageIndex, pageSize, OrderBy, ref totalCount);
        }
        public static DataTable GetCustSellReport(string CustNo, string CustName, string BeginDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellProductReport.GetCustSellReport(CompanyCD, CustNo, CustName, BeginDate, EndDate, pageIndex, pageSize, OrderBy, ref totalCount);
        }
        #endregion 

        #region 今日进销快照
        public static DataTable GetIn()
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellProductReport.GetIn(CompanyCD);
        }
        #endregion

        #region 今日应付快照
        public static DataTable GetPurchase()
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellProductReport.GetPurchase(CompanyCD);
        }
        #endregion 

        #region 今日应收快照
        public static DataTable GetSell()
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellProductReport.GetSell(CompanyCD);
        }
        #endregion
    }
}
