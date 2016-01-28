using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using XBase.Common;

namespace XBase.Data.Decision
{
    public class SellAnalysis
    {
        public SellAnalysis() 
        { }

        #region 按客户获取销售量

        /// <summary>
        /// 按客户获取销售量_月计算
        /// </summary>
        public DataSet GetSellMonthList(string StartDate,string EndDate,string CompanyCD,string CustId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select sum(RealTotal*rate) RealTotal ");
            strSql.Append(" ,cast(datepart(yyyy,orderdate)as varchar(50))+'-'+cast(datepart(MM,orderdate)as varchar(50)) OrderDate ");
            strSql.Append(" from officedba.SellOrder where 1=1 and BillStatus <> 1 ");
            if (CompanyCD != "") 
            {
                strSql.Append("and CompanyCD='");
                strSql.Append(CompanyCD);
                strSql.Append("' ");
            }

            if (StartDate != "") 
            {
                strSql.Append(" and OrderDate>='");
                strSql.Append(StartDate);
                strSql.Append("' ");
            }

            if (EndDate != "")
            {
                strSql.Append(" and OrderDate<='");
                strSql.Append(EndDate);
                strSql.Append("' ");
            }
            if (CustId != "") 
            {
                strSql.Append(" and CustId=");
                strSql.Append(CustId);
            }

            strSql.Append(" group by cast(datepart(yyyy,orderdate)as varchar(50))+'-'+cast(datepart(MM,orderdate)as varchar(50)) ");
            return Database.RunSql(strSql.ToString());
        }
       
      
        /// <summary>
        /// 按客户获取销售量_年计算
        /// </summary>
        public DataSet GetSellYearList(string StartDate, string EndDate, string CompanyCD, string CustId) 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select sum(RealTotal*rate) RealTotal ");
            strSql.Append(" ,cast(datepart(yyyy,orderdate)as varchar(50)) OrderDate ");
            strSql.Append(" from officedba.SellOrder  where 1=1 and  BillStatus <> 1");
            if (CompanyCD != "")
            {
                strSql.Append("and CompanyCD='");
                strSql.Append(CompanyCD);
                strSql.Append("' ");
            }

            if (StartDate != "")
            {
                strSql.Append(" and OrderDate>='");
                strSql.Append(StartDate);
                strSql.Append("' ");
            }

            if (EndDate != "")
            {
                strSql.Append(" and OrderDate<='");
                strSql.Append(EndDate);
                strSql.Append("' ");
            }
            if (CustId != "")
            {
                strSql.Append(" and CustId=");
                strSql.Append(CustId);
            }
            strSql.Append(" group by cast(datepart(yyyy,orderdate)as varchar(50)) ");
            return Database.RunSql(strSql.ToString());
        }
        #endregion

        #region 按产品获取销售量

        /// <summary>
        /// 按产品获取销售量_月计算
        /// </summary>
        public DataSet GetSellMonthListByProduct(string StartDate, string EndDate, string ProductId,string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select sum(a.totalFee*b.rate) RealTotal ");
            strSql.Append(" ,cast(datepart(yyyy,orderdate)as varchar(50))+'-'+cast(datepart(MM,orderdate)as varchar(50)) OrderDate ");
            strSql.Append(" from officedba.sellorderdetail a left join officedba.sellorder b on a.orderNo=b.OrderNO and a.CompanyCD=b.CompanyCD where 1=1 and b.BillStatus <> 1");
            if (CompanyCD != "")
            {
                strSql.Append(" and a.CompanyCD='");
                strSql.Append(CompanyCD);
                strSql.Append("' ");
            }

            if (StartDate != "")
            {
                strSql.Append(" and b.OrderDate>='");
                strSql.Append(StartDate);
                strSql.Append("' ");
            }

            if (EndDate != "")
            {
                strSql.Append(" and b.OrderDate<='");
                strSql.Append(EndDate);
                strSql.Append("' ");
            }
            if (ProductId != "")
            {
                strSql.Append(" and a.ProductId=");
                strSql.Append(ProductId);
            }
            strSql.Append(" group by cast(datepart(yyyy,orderdate)as varchar(50))+'-'+cast(datepart(MM,orderdate)as varchar(50)) ");
            return Database.RunSql(strSql.ToString());
        }


        /// <summary>
        /// 按产品获取销售量_年计算
        /// </summary>
        public DataSet GetSellYearListByProduct(string StartDate, string EndDate, string ProductId, string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select sum(a.totalFee*b.rate) RealTotal ");
            strSql.Append(" ,cast(datepart(yyyy,orderdate) as varchar(50)) OrderDate ");
            strSql.Append(" from officedba.sellorderdetail a left join officedba.sellorder b on a.orderNo=b.OrderNO and a.CompanyCD=b.CompanyCD where 1=1 and b.BillStatus <> 1");
            if (CompanyCD != "")
            {
                strSql.Append("and a.CompanyCD='");
                strSql.Append(CompanyCD);
                strSql.Append("' ");
            }

            if (StartDate != "")
            {
                strSql.Append(" and b.OrderDate>='");
                strSql.Append(StartDate);
                strSql.Append("' ");
            }

            if (EndDate != "")
            {
                strSql.Append(" and b.OrderDate<='");
                strSql.Append(EndDate);
                strSql.Append("' ");
            }
            if (ProductId != "")
            {
                strSql.Append(" and a.ProductId=");
                strSql.Append(ProductId);
            }
            strSql.Append(" group by cast(datepart(yyyy,orderdate)as varchar(50)) ");
            return Database.RunSql(strSql.ToString());
        }
        #endregion

        #region 按区域获取销售量
       
        /// <summary>
        /// 按区域获取销售量_月计算
        /// </summary>
        public DataSet GetSellMonthListByArea(string StartDate, string EndDate, string AreaId, string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select sum(a.RealTotal*a.rate) RealTotal, ");
            strSql.Append(" cast(datepart(yyyy,orderdate)as varchar(50))+'-'+cast(datepart(MM,orderdate)as varchar(50)) OrderDate ");
            strSql.Append(" from officedba.sellorder a inner join officedba.custinfo b on a.custId=b.Id ");
            strSql.Append(" inner join  officedba.CodePublicType c on b.AreaId=c.Id where 1=1 and a.BillStatus <> 1 ");
            if (CompanyCD != "")
            {
                strSql.Append(" and a.CompanyCD='");
                strSql.Append(CompanyCD);
                strSql.Append("' ");
            }

            if (StartDate != "")
            {
                strSql.Append(" and a.OrderDate>='");
                strSql.Append(StartDate);
                strSql.Append("' ");
            }

            if (EndDate != "")
            {
                strSql.Append(" and a.OrderDate<='");
                strSql.Append(EndDate);
                strSql.Append("' ");
            }
            if (AreaId != "")
            {
                strSql.Append(" and b.AreaId=");
                strSql.Append(AreaId);
            }
            strSql.Append(" group by cast(datepart(yyyy,orderdate)as varchar(50))+'-'+cast(datepart(MM,orderdate)as varchar(50)) ");
            return Database.RunSql(strSql.ToString());
        }


        /// <summary>
        /// 按区域获取销售量_年计算
        /// </summary>
        public DataSet GetSellYearListByArea(string StartDate, string EndDate, string AreaId, string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select sum(a.RealTotal*a.rate) RealTotal, ");
            strSql.Append(" cast(datepart(yyyy,orderdate)as varchar(50)) OrderDate ");
            strSql.Append(" from officedba.sellorder a inner join officedba.custinfo b on a.custId=b.Id ");
            strSql.Append(" inner join  officedba.CodePublicType c on b.AreaId=c.Id where 1=1 and a.BillStatus <> 1 ");
            if (CompanyCD != "")
            {
                strSql.Append(" and a.CompanyCD='");
                strSql.Append(CompanyCD);
                strSql.Append("' ");
            }

            if (StartDate != "")
            {
                strSql.Append(" and a.OrderDate>='");
                strSql.Append(StartDate);
                strSql.Append("' ");
            }

            if (EndDate != "")
            {
                strSql.Append(" and a.OrderDate<='");
                strSql.Append(EndDate);
                strSql.Append("' ");
            }
            if (AreaId != "")
            {
                strSql.Append(" and b.AreaId=");
                strSql.Append(AreaId);
            }
            strSql.Append(" group by cast(datepart(yyyy,orderdate)as varchar(50)) ");
            return Database.RunSql(strSql.ToString());
        }
        #endregion



    }
}
