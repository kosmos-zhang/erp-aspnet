using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using XBase.Data.DBHelper;


namespace XBase.Data.Office.SellReport
{
    public class SalesSummaryStatisticsDBHelper
    {
        public static DataTable GetSalesSummary(string startTime, string endTime, string sumType, int pageIndex, int pageSize, string orderBy, ref int totalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            switch (sumType)
            {
                case "byDept":
                    sbSql.AppendLine("SELECT a.SellNumTotal,a.SellPriceTotal,b.DeptName FROM ( ");
                    sbSql.AppendLine("SELECT SUM(sellNum) AS sellNumTotal,SUM(sellPrice) AS sellPriceTotal,SellDept ");
                    sbSql.AppendLine("FROM officedba.SellReport ");
                    sbSql.AppendLine("WHERE  CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))>=@StartDate ");
                    sbSql.AppendLine("AND CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))<=@EndDate  ");
                    sbSql.AppendLine("GROUP BY SellDept) AS a LEFT JOIN officedba.DeptInfo AS b ON ");
                    sbSql.AppendLine("a.SellDept=b.ID ");
                    break;
                case "bySeller":
                    sbSql.AppendLine(" SELECT a.SellNumTotal,a.SellPriceTotal,b.EmployeeName ");
                    sbSql.AppendLine(" FROM ( ");
                    sbSql.AppendLine(" SELECT SUM(sellNum) AS sellNumTotal,SUM(sellPrice) AS sellPriceTotal,sellerID ");
                    sbSql.AppendLine(" FROM ( ");
                    sbSql.AppendLine(" SELECT a.sellerID,b.sellNum,b.sellPrice,b.CreateDate  ");
                    sbSql.AppendLine(" FROM officedba.sellerRate AS a ");
                    sbSql.AppendLine(" LEFT JOIN officedba.SellReport AS b ON  ");
                    sbSql.AppendLine(" a.sellreportID=b.ID ) AS a  ");
                    sbSql.AppendLine(" WHERE  CONVERT(DATETIME,  CONVERT(VARCHAR(11),a.createdate))>=@StartDate ");
                    sbSql.AppendLine(" AND CONVERT(DATETIME, CONVERT(VARCHAR(11),a.createdate))<=@EndDate   ");
                    sbSql.AppendLine(" GROUP BY SellerID ) AS a LEFT JOIN officedba.EmployeeInfo AS b ");
                    sbSql.AppendLine(" ON a.sellerID =b.ID ");
                    break;
                case "byProduct":
                    sbSql.AppendLine(" SELECT SUM(sellNum) AS SellNumTotal,SUM(sellPrice) AS SellPriceTotal,ProductName ");
                    sbSql.AppendLine(" FROM officedba.SellReport ");
                    sbSql.AppendLine(" WHERE  CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))>=@StartDate ");
                    sbSql.AppendLine(" AND CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))<=@EndDate  ");
                    sbSql.AppendLine(" GROUP BY ProductName ");
                    break;
            }

            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@StartDate", startTime);
            sqlParams[index++] = SqlHelper.GetParameter("@EndDate", endTime);

            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), pageIndex, pageSize, orderBy, sqlParams, ref totalCount);

            //   return SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);

        }


        public static DataTable GetSalesSummary(string startTime, string endTime, string sumType)
        {
            StringBuilder sbSql = new StringBuilder();
            switch (sumType)
            {
                case "byDept":
                    sbSql.AppendLine("SELECT a.SellNumTotal,a.SellPriceTotal,b.DeptName FROM ( ");
                    sbSql.AppendLine("SELECT SUM(sellNum) AS sellNumTotal,SUM(sellPrice) AS sellPriceTotal,SellDept ");
                    sbSql.AppendLine("FROM officedba.SellReport ");
                    sbSql.AppendLine("WHERE  CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))>=@StartDate ");
                    sbSql.AppendLine("AND CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))<=@EndDate  ");
                    sbSql.AppendLine("GROUP BY SellDept) AS a LEFT JOIN officedba.DeptInfo AS b ON ");
                    sbSql.AppendLine("a.SellDept=b.ID ");
                    break;
                case "bySeller":
                    sbSql.AppendLine(" SELECT a.SellNumTotal,a.SellPriceTotal,b.EmployeeName ");
                    sbSql.AppendLine(" FROM ( ");
                    sbSql.AppendLine(" SELECT SUM(sellNum) AS sellNumTotal,SUM(sellPrice) AS sellPriceTotal,sellerID ");
                    sbSql.AppendLine(" FROM ( ");
                    sbSql.AppendLine(" SELECT a.sellerID,b.sellNum,b.sellPrice,b.CreateDate  ");
                    sbSql.AppendLine(" FROM officedba.sellerRate AS a ");
                    sbSql.AppendLine(" LEFT JOIN officedba.SellReport AS b ON  ");
                    sbSql.AppendLine(" a.sellreportID=b.ID ) AS a  ");
                    sbSql.AppendLine(" WHERE  CONVERT(DATETIME, CONVERT(VARCHAR(11),a.createdate))>=@StartDate ");
                    sbSql.AppendLine(" AND CONVERT(DATETIME, CONVERT(VARCHAR(11),a.createdate))<=@EndDate   ");
                    sbSql.AppendLine(" GROUP BY SellerID ) AS a LEFT JOIN officedba.EmployeeInfo AS b ");
                    sbSql.AppendLine(" ON a.sellerID =b.ID ");
                    break;
                case "byProduct":
                    sbSql.AppendLine(" SELECT SUM(sellNum) AS sellNumTotal,SUM(sellPrice) AS sellPriceTotal,ProductName ");
                    sbSql.AppendLine(" FROM officedba.SellReport ");
                    sbSql.AppendLine(" WHERE  CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))>=@StartDate ");
                    sbSql.AppendLine(" AND CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))<=@EndDate  ");
                    sbSql.AppendLine(" GROUP BY ProductName ");
                    break;
            }

            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@StartDate", startTime);
            sqlParams[index++] = SqlHelper.GetParameter("@EndDate", endTime);

          //  return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), pageIndex, pageSize, orderBy, sqlParams, ref totalCount);

              return SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
        }

        public static DataTable GetSalesAnalysis(string startTime, string endTime, string groupType)
        {
            StringBuilder sbSql = new StringBuilder();

            switch (groupType)
            {
                case "byWeek":
                    sbSql.AppendLine("  SELECT SUM(SellNum) AS SellNumTotal,SUM(SellPrice) AS SellPriceTotal,CreateWeek  ");
                    sbSql.AppendLine(" FROM ( ");
                    sbSql.AppendLine(" SELECT SellNum,SellPrice, ");
                    sbSql.AppendLine(" DATEPART(WK,CreateDate)  AS CreateWeek ");
                    sbSql.AppendLine(" FROM officedba.SellReport ");
                    sbSql.AppendLine(" WHERE  CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))>=@StartDate ");
                    sbSql.AppendLine(" AND CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))<=@EndDate ");
                    sbSql.AppendLine(" ) AS a  ");
                    sbSql.AppendLine(" GROUP BY CreateWeek ");
                    break;
                case "byMonth":
                    sbSql.AppendLine(" SELECT SUM(SellNum) AS SellNumTotal,SUM(SellPrice) AS SellPriceTotal,CreateMonth  ");
                    sbSql.AppendLine(" FROM ( ");
                    sbSql.AppendLine(" SELECT SellNum,SellPrice, ");
                    sbSql.AppendLine(" DATEPART(MM,CreateDate)  AS CreateMonth ");
                    sbSql.AppendLine(" FROM officedba.SellReport ");
                    sbSql.AppendLine(" WHERE  CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))>=@StartDate ");
                    sbSql.AppendLine(" AND CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))<=@EndDate ");
                    sbSql.AppendLine(" ) AS a  ");
                    sbSql.AppendLine(" GROUP BY CreateMonth ");
                    break;
                case "byYear":
                    sbSql.AppendLine(" SELECT SUM(SellNum) AS SellNumTotal,SUM(SellPrice) AS SellPriceTotal,CreateYear  ");
                    sbSql.AppendLine(" FROM ( ");
                    sbSql.AppendLine(" SELECT SellNum,SellPrice, ");
                    sbSql.AppendLine(" DATEPART(YEAR,CreateDate)  AS CreateYear ");
                    sbSql.AppendLine(" FROM officedba.SellReport ");
                    sbSql.AppendLine(" WHERE  CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))>=@StartDate ");
                    sbSql.AppendLine(" AND CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))<=@EndDate ");
                    sbSql.AppendLine(" ) AS a  ");
                    sbSql.AppendLine(" GROUP BY CreateYear ");
                    break;

            }
            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@StartDate", startTime);
            sqlParams[index++] = SqlHelper.GetParameter("@EndDate", endTime);

          //  return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), pageIndex, pageSize, orderBy, sqlParams, ref totalCount);
            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
        }
        
        
        public static DataTable GetSalesAnalysis(string startTime, string endTime, string groupType, int pageIndex, int pageSize, string orderBy, ref int totalCount)
        {
            StringBuilder sbSql = new StringBuilder();

            switch (groupType)
            {
                case "byWeek":
                    sbSql.AppendLine("  SELECT SUM(SellNum) AS SellNumTotal,SUM(SellPrice) AS SellPriceTotal,CreateWeek  ");
                    sbSql.AppendLine(" FROM ( ");
                    sbSql.AppendLine(" SELECT SellNum,SellPrice, ");
                    sbSql.AppendLine(" DATEPART(WK,CreateDate)  AS CreateWeek ");
                    sbSql.AppendLine(" FROM officedba.SellReport ");
                    sbSql.AppendLine(" WHERE  CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))>=@StartDate ");
                    sbSql.AppendLine(" AND CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))<=@EndDate ");
                    sbSql.AppendLine(" ) AS a  ");
                    sbSql.AppendLine(" GROUP BY CreateWeek ");
                    break;
                case "byMonth":
                    sbSql.AppendLine(" SELECT SUM(SellNum) AS SellNumTotal,SUM(SellPrice) AS SellPriceTotal,CreateMonth  ");
                    sbSql.AppendLine(" FROM ( ");
                    sbSql.AppendLine(" SELECT SellNum,SellPrice, ");
                    sbSql.AppendLine(" DATEPART(MM,CreateDate)  AS CreateMonth ");
                    sbSql.AppendLine(" FROM officedba.SellReport ");
                    sbSql.AppendLine(" WHERE  CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))>=@StartDate ");
                    sbSql.AppendLine(" AND CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))<=@EndDate ");
                    sbSql.AppendLine(" ) AS a  ");
                    sbSql.AppendLine(" GROUP BY CreateMonth ");
                    break;
                case "byYear":
                    sbSql.AppendLine(" SELECT SUM(SellNum) AS SellNumTotal,SUM(SellPrice) AS SellPriceTotal,CreateYear  ");
                    sbSql.AppendLine(" FROM ( ");
                    sbSql.AppendLine(" SELECT SellNum,SellPrice, ");
                    sbSql.AppendLine(" DATEPART(YEAR,CreateDate)  AS CreateYear ");
                    sbSql.AppendLine(" FROM officedba.SellReport ");
                    sbSql.AppendLine(" WHERE  CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))>=@StartDate ");
                    sbSql.AppendLine(" AND CONVERT(DATETIME, CONVERT(VARCHAR(11),createdate))<=@EndDate ");
                    sbSql.AppendLine(" ) AS a  ");
                    sbSql.AppendLine(" GROUP BY CreateYear ");
                    break;

            }
            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@StartDate", startTime);
            sqlParams[index++] = SqlHelper.GetParameter("@EndDate", endTime);

            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), pageIndex, pageSize, orderBy, sqlParams, ref totalCount);
        }


    }
}
