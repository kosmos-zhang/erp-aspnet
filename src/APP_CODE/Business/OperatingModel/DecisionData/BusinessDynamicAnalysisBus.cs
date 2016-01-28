using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XBase.Business.OperatingModel.DecisionData
{
  public   class BusinessDynamicAnalysisBus
    {
        #region 对客户应收款总额
        /// <summary>
        /// 对客户应收款总额
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static string GetCustomMoneyBack(string CompanyCD)
        {
            return XBase.Data.OperatingModel.DecisionData.BusinessDynamicAnalysisDBHelper.GetCustomMoneyBack(CompanyCD);
        }

        #endregion

        #region 对供应商应付款
        public static string PaymentAmoutForProvider(string companyCD)
        {
            return XBase.Data.OperatingModel.DecisionData.BusinessDynamicAnalysisDBHelper.PaymentAmoutForProvider(companyCD);
        }
        #endregion

        #region  上周销售金额与上上周金额上升比率
        public static string GetTwoWeeksRate(string CompanyCD)
        {
            return XBase.Data.OperatingModel.DecisionData.BusinessDynamicAnalysisDBHelper.GetTwoWeeksRate(CompanyCD);


        }
        #endregion

        #region 指定天数内未销售的物品之和
        public static string GetNotSaleProductCount(string companyCD, int days)
        {
            return XBase.Data.OperatingModel.DecisionData.BusinessDynamicAnalysisDBHelper.GetNotSaleProductCount(companyCD, days);
        }
        #endregion

        #region 指定天数内未销售的物品信息  分页
        public static DataTable GetNotSaleProductList(string companyCD, int days, int pageSize, int pageIndex, string OrderBy, ref int totalCount)
        {
            return XBase.Data.OperatingModel.DecisionData.BusinessDynamicAnalysisDBHelper.GetNotSaleProductList(companyCD, days, pageSize, pageIndex, OrderBy, ref totalCount);

        }
        #endregion

        #region 指定天数内无销售记录的客户
        public static string GetNotSaleCostomCount(string companyCD, int days)
        {
            return XBase.Data.OperatingModel.DecisionData.BusinessDynamicAnalysisDBHelper.GetNotSaleCostomCount(companyCD, days);
        }
        #endregion

        #region 指定天数内无销售记录的客户 信息 分页
        public static DataTable GetNotSaleCustomList(string companyCD, int days, int pageSize, int pageIndex, string OrderBy, ref int totalCount)
        {
            return XBase.Data.OperatingModel.DecisionData.BusinessDynamicAnalysisDBHelper.GetNotSaleCustomList(companyCD, days, pageSize, pageIndex, OrderBy, ref totalCount);
        }
        #endregion

        #region 昨日成交单数、昨日成交金额、回款
        public static DataTable GetContractInfo(string companyCD)
        {
            return XBase.Data.OperatingModel.DecisionData.BusinessDynamicAnalysisDBHelper.GetContractInfo(companyCD);
        }
        #endregion

        #region  30天内收入总收入
        public static string GetTotalPriceIn30Days(string companyCD)
        {
            return XBase.Data.OperatingModel.DecisionData.BusinessDynamicAnalysisDBHelper.GetTotalPriceIn30Days(companyCD);
        }
        #endregion

        #region 物品销售金额TOP10
        public static DataTable GetSaleProductCostTop10(string companyCD)
        {
            return XBase.Data.OperatingModel.DecisionData.BusinessDynamicAnalysisDBHelper.GetSaleProductCostTop10(companyCD);
        }
        #endregion

        #region 物品销售数量TOP10
        public static DataTable GetSaleProductCountTop10(string companyCD)
        {
            return XBase.Data.OperatingModel.DecisionData.BusinessDynamicAnalysisDBHelper.GetSaleProductCountTop10(companyCD);
        }
        #endregion

        #region 客户销售额TOP10
        public static DataTable GetCustomSaleTop10(string companyCD)
        {
            return XBase.Data.OperatingModel.DecisionData.BusinessDynamicAnalysisDBHelper.GetCustomSaleTop10(companyCD);
        }
        #endregion

        #region 7天内销售金额
        public static DataTable Get7DaysSaleInfo(string companyCD)
        {
            return XBase.Data.OperatingModel.DecisionData.BusinessDynamicAnalysisDBHelper.Get7DaysSaleInfo(companyCD);
        }
        #endregion
    }
}
