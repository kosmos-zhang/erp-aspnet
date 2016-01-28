using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XBase.Business.Office.SellReport
{
    public class SalesSummaryStatisticsBus
    {
        public static DataTable GetSalesSummary(string startTime, string endTime, string sumType, int pageIndex, int pageSize, string orderBy, ref int totalCount)
        {
            return XBase.Data.Office.SellReport.SalesSummaryStatisticsDBHelper.GetSalesSummary(startTime, endTime, sumType, pageIndex, pageSize, orderBy, ref totalCount);
        }
        public static DataTable GetSalesSummary(string startTime, string endTime, string sumType)
        {
            return XBase.Data.Office.SellReport.SalesSummaryStatisticsDBHelper.GetSalesSummary(startTime, endTime, sumType);
        }
        

        public static DataTable GetSalesAnalysis(string startTime, string endTime, string groupType, int pageIndex, int pageSize, string orderBy, ref int totalCount)
        {
            return XBase.Data.Office.SellReport.SalesSummaryStatisticsDBHelper.GetSalesAnalysis(startTime, endTime, groupType, pageIndex, pageSize, orderBy, ref totalCount);
        }

        public static DataTable GetSalesAnalysis(string startTime, string endTime, string groupType)
        {
            return XBase.Data.Office.SellReport.SalesSummaryStatisticsDBHelper.GetSalesAnalysis(startTime, endTime, groupType);
        }
    }
}
