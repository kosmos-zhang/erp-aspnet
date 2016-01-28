using System;
using System.Data;
using System.Collections.Generic;

namespace XBase.Business.Decision
{
    public class SellAnalysis
    {
        private readonly XBase.Data.Decision.SellAnalysis dal = new XBase.Data.Decision.SellAnalysis();

        /// <summary>
        /// 按客户获取销售量_月
        /// </summary>
        public DataSet GetSellMonthList(string StartDate, string EndDate, string CompanyCD, string CustId) 
        {
            return  dal.GetSellMonthList(StartDate,EndDate,CompanyCD,CustId);
        }
        /// <summary>
        /// 按客户获取销售量_年
        /// </summary>
        public DataSet GetSellYearList(string StartDate, string EndDate, string CompnayCD, string CustId)
        {
            return dal.GetSellYearList(StartDate, EndDate, CompnayCD, CustId);
        }

        /// <summary>
        /// 按产品获取销售量_月
        /// </summary>
        public DataSet GetSellMonthListByProduct(string StartDate, string EndDate, string ProductId, string CompanyCD)
        {
            return dal.GetSellMonthListByProduct(StartDate, EndDate, ProductId, CompanyCD);
        }
        /// <summary>
        /// 按产品获取销售量_年
        /// </summary>
        public DataSet GetSellYearListByProduct(string StartDate, string EndDate, string ProductId, string CompanyCD)
        {
            return dal.GetSellYearListByProduct(StartDate, EndDate, ProductId, CompanyCD);
        }


        /// <summary>
        /// 按区域获取销售量_月
        /// </summary>
        public DataSet GetSellMonthListByArea(string StartDate, string EndDate, string AreaId, string CompanyCD)
        {
            return dal.GetSellMonthListByArea(StartDate, EndDate, AreaId, CompanyCD);
        }
        /// <summary>
        /// 按区域获取销售量_年
        /// </summary>
        public DataSet GetSellYearListByArea(string StartDate, string EndDate, string AreaId, string CompanyCD)
        {
            return dal.GetSellYearListByArea(StartDate, EndDate, AreaId, CompanyCD);
        }
    }
}
