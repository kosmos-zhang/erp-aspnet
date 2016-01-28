using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;

namespace XBase.Data.OperatingModel.DecisionData
{
    public class BusinessDynamicAnalysisDBHelper
    {
        #region 对客户应收款总额
        /// <summary>
        /// 对客户应收款总额
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static string GetCustomMoneyBack(string CompanyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT   ");
            sbSql.AppendLine("(ISNULL((SELECT SUM(TotalPrice) AS TotalCost   ");
            sbSql.AppendLine("FROM officedba.SellOrder  ");
            sbSql.AppendLine("WHERE BillStatus<>'1'  ");
            sbSql.AppendLine("AND CompanyCD=@CompanyCD  --销售订单金额  ");
            sbSql.AppendLine("),0)-  ");
            sbSql.AppendLine("ISNULL((SELECT  SUM(TotalPrice) AS BackTotalCost   ");
            sbSql.AppendLine("FROM officedba.SellBack  ");
            sbSql.AppendLine("WHERE BillStatus<>'1'  ");
            sbSql.AppendLine("AND CompanyCD=@CompanyCD  --销售退货单金额  ");
            sbSql.AppendLine("),0)-  ");
            sbSql.AppendLine("ISNULL((SELECT SUM(YAccounts) AS InComeCost  ");
            sbSql.AppendLine("FROM Officedba. BlendingDetails  ");
            sbSql.AppendLine("WHERE BillingType='1'  ");
            sbSql.AppendLine("AND CompanyCD=@CompanyCD  --收款单金额  ");
            sbSql.AppendLine("),0)) AS GetMoneyBack --应收款金额  ");

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);

            DataTable dtRes = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
            if (dtRes != null && dtRes.Rows.Count > 0)
            {
                return dtRes.Rows[0]["GetMoneyBack"] != null ? dtRes.Rows[0]["GetMoneyBack"].ToString() : "0";
            }
            else
                return "0";
        }

        #endregion

        #region 对供应商应付款
        public static string PaymentAmoutForProvider(string companyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT  ");
            sbSql.AppendLine(" (ISNULL((SELECT SUM(TotalPrice) FROM officedba.PurchaseOrder ");
            sbSql.AppendLine(" WHERE BillStatus<>'1' AND CompanyCD=@CompanyCD --采购订单金额 ");
            sbSql.AppendLine(" ),0)- ");
            sbSql.AppendLine(" ISNULL((SELECT SUM(TotalPrice) FROM officedba.PurchaseReject ");
            sbSql.AppendLine(" WHERE BillStatus<>'1' AND CompanyCD=@CompanyCD --采购退货单金额 ");
            sbSql.AppendLine(" ),0)- ");
            sbSql.AppendLine(" ISNULL((SELECT SUM(YAccounts) FROM Officedba. BlendingDetails ");
            sbSql.AppendLine(" WHERE BillingType='2' AND CompanyCD=@CompanyCD --付款金额 ");
            sbSql.AppendLine(" ),0)) AS PaymentAmount ");


            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = SqlHelper.GetParameter("CompanyCD", companyCD);

            DataTable dtRes = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
            if (dtRes != null && dtRes.Rows.Count > 0)
            {
                return dtRes.Rows[0]["PaymentAmount"] != null ? dtRes.Rows[0]["PaymentAmount"].ToString() : "0";
            }
            else
                return "0";
        }
        #endregion

        #region  上周销售金额与上上周金额上升比率
        public static string GetTwoWeeksRate(string CompanyCD)
        {
            DateTime now = DateTime.Now;
            DateTime preWeekStart = now.AddDays(-7);
            DateTime prePreWeekStart = now.AddDays(-14);
            DateTime prePreWeekEnd = now.AddDays(-8);
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT   ");
            sbSql.AppendLine(" (SELECT SUM(TotalPrice) FROM officedba.StorageOutSell  ");
            sbSql.AppendLine(" WHERE   ");
            sbSql.AppendLine(" ConfirmDate>=@PreWeekStart  ");
            sbSql.AppendLine(" AND ConfirmDate<=@Now   ");
            sbSql.AppendLine(" AND BillStatus<>'1'  ");
            sbSql.AppendLine(" AND CompanyCD=@CompanyCD) AS PreWeekData,  ");
            sbSql.AppendLine(" (SELECT SUM(TotalPrice) FROM officedba.StorageOutSell  ");
            sbSql.AppendLine(" WHERE   ");
            sbSql.AppendLine(" ConfirmDate>=@prePreWeekStart  ");
            sbSql.AppendLine(" AND ConfirmDate<=@prePreWeekEnd  ");
            sbSql.AppendLine(" AND BillStatus<>'1'  ");
            sbSql.AppendLine(" AND CompanyCD=@CompanyCD) AS prePreWeekData  ");

            SqlParameter[] sqlParams = new SqlParameter[5];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@PreWeekStart", preWeekStart);
            sqlParams[index++] = SqlHelper.GetParameter("@Now", now);
            sqlParams[index++] = SqlHelper.GetParameter("@prePreWeekStart", prePreWeekStart);
            sqlParams[index++] = SqlHelper.GetParameter("@prePreWeekEnd", prePreWeekEnd);
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);


            DataTable dtRes = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
            decimal preWeekData = decimal.Zero;
            decimal prePreWeekData = decimal.Zero;
            if (dtRes != null && dtRes.Rows.Count > 0)
            {
                preWeekData = Convert.ToDecimal(!string.IsNullOrEmpty(dtRes.Rows[0]["PreWeekData"].ToString()) ? dtRes.Rows[0]["PreWeekData"].ToString() : "0");
                prePreWeekData = Convert.ToDecimal(!string.IsNullOrEmpty(dtRes.Rows[0]["prePreWeekData"].ToString()) ? dtRes.Rows[0]["prePreWeekData"].ToString() : "0");
            }

            if (prePreWeekData != Convert.ToDecimal("0"))
            {
                return ((preWeekData / prePreWeekData) - 1).ToString();
            }
            else
                return "0";


        }
        #endregion

        #region 指定天数内未销售的物品之和
        public static string GetNotSaleProductCount(string companyCD, int days)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT COUNT(ID) AS NotSaleProductCount FROM officedba.ProductInfo ");
            sbSql.AppendLine(" WHERE ID NOT IN ");
            sbSql.AppendLine(" ( ");
            sbSql.AppendLine(" SELECT  distinct(a.ProductID) ");
            sbSql.AppendLine(" FROM officedba.StorageOutSellDetail AS a ");
            sbSql.AppendLine(" LEFT JOIN officedba.StorageOutSell AS b ON a.OutNo=b.OutNo ");
            sbSql.AppendLine(" WHERE  ");
            sbSql.AppendLine(" CONVERT(DATETIME, convert(varchar(11),b.ConfirmDate))>@StartDate ");
            sbSql.AppendLine(" AND CONVERT(DATETIME, convert(varchar(11),b.ConfirmDate))<@EndDate ");
            sbSql.AppendLine(" AND a.CompanyCD=@CompanyCD ");
            sbSql.AppendLine(" AND b.BillStatus<>'1' ");
            sbSql.AppendLine(" ) ");

            SqlParameter[] sqlParams = new SqlParameter[3];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@StartDate", DateTime.Now.ToString("yyyy-MM-dd"));
            sqlParams[index++] = SqlHelper.GetParameter("@EndDate", DateTime.Now.AddDays((-1 * days)).ToString("yyyy-MM-dd"));
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            DataTable dtRes = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
            if (dtRes != null && dtRes.Rows.Count > 0)
                return dtRes.Rows[0]["NotSaleProductCount"] != null ? dtRes.Rows[0]["NotSaleProductCount"].ToString() : "0";
            else
                return "0";
        }
        #endregion

        #region 指定天数内未销售的物品信息  分页
        public static DataTable GetNotSaleProductList(string companyCD, int days, int pageSize, int pageIndex, string OrderBy, ref int totalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("  SELECT a.ProductName,a.ProdNo,a.Specification,b.CodeName AS UnitName FROM officedba.ProductInfo AS a  ");
            sbSql.AppendLine("  LEFT JOIN officedba.CodeUnitType AS b ON a.UnitID=b.ID  ");
            sbSql.AppendLine("  WHERE a.ID NOT IN  ");
            sbSql.AppendLine("  (  ");
            sbSql.AppendLine("  SELECT  distinct(a.ProductID)  ");
            sbSql.AppendLine("  FROM officedba.StorageOutSellDetail AS a  ");
            sbSql.AppendLine("  LEFT JOIN officedba.StorageOutSell AS b ON a.OutNo=b.OutNo  ");
            sbSql.AppendLine("  WHERE   ");
            sbSql.AppendLine("  CONVERT(DATETIME, convert(varchar(11),b.ConfirmDate))>@StartDate  ");
            sbSql.AppendLine("  AND CONVERT(DATETIME, convert(varchar(11),b.ConfirmDate))<@EndDate  ");
            sbSql.AppendLine("  AND a.CompanyCD=@CompanyCD  ");
            sbSql.AppendLine("  AND b.BillStatus<>'1'  ");
            sbSql.AppendLine("  )  ");

            SqlParameter[] sqlParams = new SqlParameter[3];

            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@StartDate", DateTime.Now.ToString("yyyy-MM-dd"));
            sqlParams[index++] = SqlHelper.GetParameter("@EndDate", DateTime.Now.AddDays((-1 * days)).ToString("yyyy-MM-dd"));
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), pageIndex, pageSize, OrderBy, sqlParams, ref totalCount);

        }
        #endregion

        #region 指定天数内无销售记录的客户
        public static string GetNotSaleCostomCount(string companyCD, int days)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("  SELECT COUNT(a.ID) AS NotSaleCustomCount FROM officedba.CustInfo AS a  ");
            sbSql.AppendLine("  WHERE a.ID NOT IN   ");
            sbSql.AppendLine("  (  ");
            sbSql.AppendLine("  SELECT distinct(CustID) FROM officedba.StorageOutSell AS a  ");
            sbSql.AppendLine("  LEFT JOIN  officedba. SellSend AS b ON a.FromBillID=b.ID  ");
            sbSql.AppendLine("  WHERE   ");
            sbSql.AppendLine("  CONVERT(DATETIME, convert(varchar(11),b.ConfirmDate))>@StartDate  ");
            sbSql.AppendLine("  AND CONVERT(DATETIME, convert(varchar(11),b.ConfirmDate))<@EndDate  ");
            sbSql.AppendLine("  AND a.CompanyCD=@CompanyCD  ");
            sbSql.AppendLine("  AND a.BillStatus<>'1'  ");
            sbSql.AppendLine("  )  ");
            sbSql.AppendLine(" AND a.BigType='1'  ");


            SqlParameter[] sqlParams = new SqlParameter[3];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@StartDate", DateTime.Now.ToString("yyyy-MM-dd"));
            sqlParams[index++] = SqlHelper.GetParameter("@EndDate", DateTime.Now.AddDays((-1 * days)).ToString("yyyy-MM-dd"));
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            DataTable dtRes = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
            if (dtRes != null && dtRes.Rows.Count > 0)
                return dtRes.Rows[0]["NotSaleCustomCount"] != null ? dtRes.Rows[0]["NotSaleCustomCount"].ToString() : "0";
            else
                return "0";

        }
        #endregion

        #region 指定天数内无销售记录的客户 信息 分页
        public static DataTable GetNotSaleCustomList(string companyCD, int days, int pageSize, int pageIndex, string OrderBy, ref int totalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("  SELECT a.CustNo,a.CustName AS NotSaleCustomCount FROM officedba.CustInfo AS a  ");
            sbSql.AppendLine("  WHERE a.ID NOT IN   ");
            sbSql.AppendLine("  (  ");
            sbSql.AppendLine("  SELECT distinct(CustID) FROM officedba.StorageOutSell AS a  ");
            sbSql.AppendLine("  LEFT JOIN  officedba. SellSend AS b ON a.FromBillID=b.ID  ");
            sbSql.AppendLine("  --WHERE   ");
            sbSql.AppendLine("  --CONVERT(DATETIME, convert(varchar(11),b.ConfirmDate))>@StartDate  ");
            sbSql.AppendLine("  --AND CONVERT(DATETIME, convert(varchar(11),b.ConfirmDate))<@EndDate  ");
            sbSql.AppendLine("  --AND a.CompanyCD=@CompanyCD  ");
            sbSql.AppendLine("  --AND a.BillStatus<>'1'  ");
            sbSql.AppendLine("  )  ");
            sbSql.AppendLine("  AND a.BigType='1'  ");

            SqlParameter[] sqlParams = new SqlParameter[3];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@StartDate", DateTime.Now.ToString("yyyy-MM-dd"));
            sqlParams[index++] = SqlHelper.GetParameter("@EndDate", DateTime.Now.AddDays((-1 * days)).ToString("yyyy-MM-dd"));
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);


            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), pageIndex, pageSize, OrderBy, sqlParams, ref totalCount);
        }
        #endregion

        #region 昨日成交单数、昨日成交金额、回款
        public static DataTable GetContractInfo(string companyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("   SELECT   ");
            sbSql.AppendLine("  (SELECT COUNT(*)  FROM officedba.StorageOutSell  ");
            sbSql.AppendLine("  WHERE CONVERT(DATETIME, convert(varchar(11),ConfirmDate))=@Date  ");
            sbSql.AppendLine("  AND CompanyCD=@CompanyCD AND BillStatus<>'1'  ");
            sbSql.AppendLine("  ) AS OutCount,  ");
            sbSql.AppendLine("  (SELECT ISNULL(SUM(TotalPrice),0) FROM officedba.StorageOutSell  ");
            sbSql.AppendLine("  WHERE CONVERT(DATETIME, convert(varchar(11),ConfirmDate))=@Date  ");
            sbSql.AppendLine("  AND CompanyCD=@CompanyCD AND BillStatus<>'1'  ");
            sbSql.AppendLine("  ) AS OutPrice,  ");
            sbSql.AppendLine("  (SELECT ISNULL(SUM(YAccounts),0) FROM Officedba. BlendingDetails  ");
            sbSql.AppendLine("  WHERE CONVERT(DATETIME, convert(varchar(11),CreateDate))=@Date  ");
            sbSql.AppendLine("  AND CompanyCD=@CompanyCD  ");
            sbSql.AppendLine("  )  AS GetMoney  ");


            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@Date", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));

            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
        }
        #endregion

        #region  30天内收入总收入
        public static string GetTotalPriceIn30Days(string companyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT  ");
            sbSql.AppendLine(" ( ISNULL(  (SELECT SUM(TotalPrice) FROM officedba.SellOrder  ");
            sbSql.AppendLine(" WHERE BillStatus<>'1' AND CompanyCD=@CompanyCD  ");
            sbSql.AppendLine(" AND CONVERT(DATETIME, convert(varchar(11),ConfirmDate))>=@Date  ");
            sbSql.AppendLine(" ) ,0) -  ");
            sbSql.AppendLine(" ISNULL( (SELECT SUM(TotalPrice) FROM officedba.SellBack  ");
            sbSql.AppendLine(" WHERE BillStatus<>'1' AND CompanyCD=@CompanyCD  ");
            sbSql.AppendLine(" AND CONVERT(DATETIME, convert(varchar(11),ConfirmDate))>=@Date  ");
            sbSql.AppendLine(" ),0)) AS TotalPrice  ");


            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@Date", DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"));

            DataTable dtRes = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
            if (dtRes != null && dtRes.Rows.Count > 0)
                return dtRes.Rows[0]["TotalPrice"] != null ? dtRes.Rows[0]["TotalPrice"].ToString() : "0";
            else
                return "0";
        }
        #endregion

        #region 物品销售金额TOP10
        public static DataTable GetSaleProductCostTop10(string companyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT a.TotalPrice,b.ID AS ProductID,b.ProdNo,b.ProductName FROM (");
            sbSql.AppendLine(" SELECT TOP 10  ProductID,TotalPrice FROM  ");
            sbSql.AppendLine(" (SELECT SUM(a.TotalPrice) AS TotalPrice,ProductID FROM officedba.StorageOutSellDetail AS a  ");
            sbSql.AppendLine(" LEFT JOIN officedba.StorageOutSell AS b ON a.OutNo=b.OutNo  ");
            sbSql.AppendLine(" WHERE  CONVERT(DATETIME, convert(varchar(11),b.ConfirmDate))>=@Date  ");
            sbSql.AppendLine(" AND a.CompanyCD=@CompanyCD AND b.BillStatus<>'1'  ");
            sbSql.AppendLine(" GROUP BY ProductID) AS a  ");
            sbSql.AppendLine(" ORDER BY a.TotalPrice DESC  ");
            sbSql.AppendLine(") AS a ");
            sbSql.AppendLine(" LEFT JOIN officedba.ProductInfo AS b ON a.ProductID=b.ID ");

            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@Date", DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"));

            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
        }
        #endregion

        #region 物品销售数量TOP10
        public static DataTable GetSaleProductCountTop10(string companyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT a.TotalCount,b.ID AS ProductID,b.ProdNo,b.ProductName FROM (");
            sbSql.AppendLine(" SELECT TOP 10  ProductID,TotalCount FROM  ");
            sbSql.AppendLine(" (SELECT SUM(a.ProductCount) AS TotalCount,ProductID FROM officedba.StorageOutSellDetail AS a  ");
            sbSql.AppendLine(" LEFT JOIN officedba.StorageOutSell AS b ON a.OutNo=b.OutNo  ");
            sbSql.AppendLine(" WHERE  CONVERT(DATETIME, convert(varchar(11),b.ConfirmDate))>=@Date  ");
            sbSql.AppendLine(" AND a.CompanyCD=@CompanyCD AND b.BillStatus<>'1'  ");
            sbSql.AppendLine(" GROUP BY ProductID) AS a  ");
            sbSql.AppendLine(" ORDER BY a.TotalCount DESC  ");
            sbSql.AppendLine(") AS a ");
            sbSql.AppendLine(" LEFT JOIN officedba.ProductInfo AS b ON a.ProductID=b.ID ");

            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@Date", DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"));

            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
        }
        #endregion 

        #region 客户销售额TOP10
        public static DataTable GetCustomSaleTop10(string companyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("  SELECT TOP 10 a.CustID,a.TotalPrice,b.CustName,b.CustNo FROM ");
            sbSql.AppendLine("  (SELECT SUM(a.TotalPrice) AS TotalPrice,CustID FROM officedba.SellOrderDetail AS a ");
            sbSql.AppendLine("  LEFT JOIN officedba.SellOrder AS b ON a.OrderNo=b.OrderNo ");
            sbSql.AppendLine("  WHERE  CONVERT(DATETIME, convert(varchar(11),b.ConfirmDate))>=@Date ");
            sbSql.AppendLine("  AND a.CompanyCD=@CompanyCD AND b.BillStatus<>'1' ");
            sbSql.AppendLine("  GROUP BY b.CustID) AS a ");
            sbSql.AppendLine("  LEFT JOIN officedba.CustInfo AS b ON a.CustID=b.ID ");
            sbSql.AppendLine("  ORDER BY a.TotalPrice DESC ");

            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@Date", DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"));

            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);

        }
        #endregion

        #region 7天内销售金额
        public static DataTable Get7DaysSaleInfo(string companyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("  SELECT SUM(TotalPrice) AS TotalPrice,ConfirmDate FROM  ");
            sbSql.AppendLine("  (  ");
            sbSql.AppendLine("  SELECT TotalPrice,CONVERT(DATETIME, CONVERT(varchar(11),ConfirmDate)) AS ConfirmDate  ");
            sbSql.AppendLine("  FROM officedba.StorageOutSell  ");
            sbSql.AppendLine("  WHERE BillStatus<>'1' AND  TotalPrice>0 AND  ");
            sbSql.AppendLine("  CONVERT(DATETIME, convert(varchar(11),ConfirmDate))>=@Date AND CompanyCD=@CompanyCD  ");
            sbSql.AppendLine(" AND CONVERT(DATETIME, convert(varchar(11),ConfirmDate))<CONVERT(DATETIME, convert(varchar(11),GETDATE()))");
            sbSql.AppendLine("  ) AS a  ");
            sbSql.AppendLine("  GROUP BY ConfirmDate  ");

            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@Date", DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));

            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
        }
        #endregion

    }
}
