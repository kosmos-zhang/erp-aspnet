using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Office.CustManager;
using XBase.Model.Office.CustManager;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Common;
using XBase.Data.Common;
using System.Collections;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
namespace XBase.Business.Office.CustManager
{
    public class CustSellBus
    {
        /// <summary>
        /// 客户购买金额走势
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="TimeType"></param>
        /// <param name="ProductID"></param>
        /// <param name="CustID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="BeginCount"></param>
        /// <param name="EndCount"></param>
        /// <param name="BeginPrice"></param>
        /// <param name="EndPrice"></param>
        /// <returns></returns>
        public static DataTable GetCustSellInfo(string CompanyCD, string TimeType, string ProductID, string CustID, string BeginDate, string EndDate, string BeginCount, string EndCount, string BeginPrice, string EndPrice)
        {
            return CustSellDBHelper.GetCustSellInfo(CompanyCD, TimeType, ProductID, CustID, BeginDate, EndDate, BeginCount, EndCount, BeginPrice, EndPrice);
        }

        /// <summary>
        /// 客户购买金额走势-导出
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="TimeType"></param>
        /// <param name="ProductID"></param>
        /// <param name="CustID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="BeginCount"></param>
        /// <param name="EndCount"></param>
        /// <param name="BeginPrice"></param>
        /// <param name="EndPrice"></param>
        /// <param name="XValue"></param>
        /// <returns></returns>
        public static DataTable GetCustSellInfoDetail(string CompanyCD, string TimeType, string ProductID, string CustID, string BeginDate, string EndDate, string BeginCount, string EndCount, string BeginPrice, string EndPrice,string XValue)
        {
            SqlCommand comm=CustSellDBHelper.GetCustSellInfoDetail(CompanyCD,TimeType, ProductID, CustID, BeginDate, EndDate, BeginCount, EndCount, BeginPrice, EndPrice,XValue);
            return SqlHelper.ExecuteSearch(comm);
        }

        public static DataTable GetCustSellInfoDetailList(string CompanyCD, string TimeType, string ProductID, string CustID, string BeginDate, string EndDate, string BeginCount, string EndCount, string BeginPrice, string EndPrice, string XValue, string OrderBy, int PageIndex, int PageCount, ref int TotalCount)
        {
            SqlCommand comm = CustSellDBHelper.GetCustSellInfoDetail(CompanyCD, TimeType, ProductID, CustID, BeginDate, EndDate, BeginCount, EndCount, BeginPrice, EndPrice, XValue);
            return SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, OrderBy, ref TotalCount);
        }

    }
}
