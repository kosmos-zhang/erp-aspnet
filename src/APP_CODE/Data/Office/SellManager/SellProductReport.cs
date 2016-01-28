using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using System.Data.Sql;
using XBase.Model.Office.SellManager;
using XBase.Common;

namespace XBase.Data.Office.SellManager //Method---1列表2报表
{
    public class SellProductReport
    {
        #region 物品销售汇总

        /// <summary>
        /// 物品销售汇总 报表需要
        /// </summary>
        ///  <returns></returns>
        public static DataTable GetSellProductReport(string method, int pageIndex, int pageSize, string OrderBy, string BeginTime, string EndTime, string CompanyCD, string CurrencyType, ref string TotalProductCount, ref string TotalTaxBuy, ref string TotalStandardBuy, ref int TotalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            #region
            sql.AppendLine(" select T1.*,isnull(c.ProdNo,'') as ProNo,isnull(c.ProductName,'') as ProductName,isnull(c.Specification,'') as Specification                                                              ");
            sql.AppendLine(" ,isnull(d.CodeName,'') as TypeName,isnull(e.CodeName,'') as CodeName                                                                                                                      ");
            sql.AppendLine(" ,case e.Flag when '1' then '数量' when '2' then '重量' when '3' then '长度' when '4' then '面积' when '5' then '体积' else '' end as Flag  ");
            sql.AppendLine(" from (  select                                                                                                           ");
            sql.AppendLine(" isnull(sum(a.TotalPrice*b.Discount/100{0}),0) as TaxBuy "); 
            sql.AppendLine("  ,isnull(sum(a.ProductCount),0) as ProductCount                                                                          ");
            sql.AppendLine(" ,isnull(sum(a.TotalFee*b.Discount/100{0}),0) as StandardBuy,a.ProductID as ID                                                                                                            ");
            sql.AppendLine(" ,(select sum(a1.TotalFee*b1.Discount/100{1}) from   officedba.SellOrderDetail as a1 left join officedba.SellOrder as b1 on a1.OrderNo=b1.OrderNo and a1.CompanyCD=b1.CompanyCD where ");
            sql.AppendLine("   a1.CompanyCD=@CompanyCD and b1.Status!='3' and b1.BillStatus!='1' {3}");
            if (!string.IsNullOrEmpty(BeginTime))
            {
                sql.AppendLine(" and b1.OrderDate>=@BeginTime");
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                sql.AppendLine(" and b1.OrderDate<=@EndDate");
            }
            sql.AppendLine(" )as Tot                                                                                                                  ");
            sql.AppendLine(" from   officedba.SellOrderDetail as a left join officedba.SellOrder as b on a.OrderNo=b.OrderNo and a.CompanyCD=b.CompanyCD                                                               ");
            sql.AppendLine(" where  a.CompanyCD=@CompanyCD and b.Status!='3' and b.BillStatus!='1' {2}                                                                                                                       ");
           
            if (!string.IsNullOrEmpty(BeginTime))
            {
                sql.AppendLine(" and b.OrderDate>=@BeginTime");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginTime", BeginTime));
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                sql.AppendLine(" and b.OrderDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndTime));
            }

            sql.AppendLine(" group by a.ProductID ) as T1                                                                                                                                                              ");
            sql.AppendLine(" left join officedba.ProductInfo as c on T1.ID=c.ID                                                                                                                                 ");
            sql.AppendLine(" left join officedba.CodeProductType as d on c.TypeID=d.ID                                                                                                                                 ");
            sql.AppendLine(" left join officedba.CodeUnitType as e on c.UnitID=e.ID                                                                                                                                    ");
            if (method == "1")
            {
                sql.AppendLine(" order by " + OrderBy);
            }
            #endregion
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            string mysql ="";
            if (!string.IsNullOrEmpty(CurrencyType) && CurrencyType != "0")//币种需要
            {
                string[] arg = new string[4] { "", "", " and b.CurrencyType=@CurrencyType", " and b1.CurrencyType=@CurrencyType" };
                mysql = String.Format(sql.ToString(),arg);
                comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType",CurrencyType));
            }
            else
            {
                string[] arg = new string[4] { "*b.Rate", "*b1.Rate", "", "" };
                mysql = String.Format(sql.ToString(), arg);
            }
            comm.CommandText =mysql;
            DataTable dt = new DataTable();
            if (method == "0")
            {
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref TotalCount);
            }
            if (method == "1")
            {
                dt = SqlHelper.ExecuteSearch(comm);
            }
            if (method == "1")
            {
                SqlCommand TotalComm = new SqlCommand();
                StringBuilder TotalSql = new StringBuilder();
                #region
                TotalSql.AppendLine("select ");
                TotalSql.AppendLine("	   isnull(sum(a.ProductCount),0) as TotalProductCount,isnull(sum(a.TotalPrice*d.Discount/100{0}),0) as TotalTaxBuy,             ");
                TotalSql.AppendLine("      isnull(sum(isnull(a.TotalFee,0){0}*d.Discount/100),0) as TotalStandardBuy                                                      ");
                TotalSql.AppendLine("from  officedba.SellOrderDetail as a  ");
                TotalSql.AppendLine("      left join officedba.SellOrder   d on a.OrderNo=d.OrderNO  and a.CompanyCD=d.CompanyCD   ");
                TotalSql.AppendLine(" where  a.CompanyCD=@CompanyCD and d.BillStatus!='1'  and d.Status!='3' {1}");
                #endregion
                if (!string.IsNullOrEmpty(BeginTime))
                {
                    TotalSql.AppendLine(" and d.OrderDate>=@BeginTime");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@BeginTime", BeginTime));
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    TotalSql.AppendLine(" and d.OrderDate<=@EndDate");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndTime));
                }
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                string myTotalSql ="";
                if (!string.IsNullOrEmpty(CurrencyType) && CurrencyType != "0")
                {
                    myTotalSql = String.Format(TotalSql.ToString(), "", " and d.CurrencyType=@CurrencyType");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", CurrencyType));
                }
                else
                {
                    myTotalSql = String.Format(TotalSql.ToString(), "*d.Rate", " ");
                }
                TotalComm.CommandText =myTotalSql;
                DataTable TotalDt = SqlHelper.ExecuteSearch(TotalComm);
                TotalProductCount = "0";
                TotalStandardBuy = "0";
                TotalTaxBuy = "0";

                if (TotalDt.Rows.Count > 0)
                {
                    TotalTaxBuy = TotalDt.Rows[0]["TotalTaxBuy"].ToString();
                    TotalProductCount = TotalDt.Rows[0]["TotalProductCount"].ToString();
                    TotalStandardBuy = TotalDt.Rows[0]["TotalStandardBuy"].ToString();
                }
            }
            return dt;
        }

        #endregion

        #region 物品分类销售汇总
        public static DataTable GetProductType(string Method, int pageIndex, int pageSize, string OrderBy, string BeginTime, string EndTime, string CompanyCD, string CurrencyType,ref string TotalProductCount, ref string TotalStandardSell, ref string TotalSellTax, ref string TotalTax, ref int TotalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select d.ID,isnull(d.CodeName,'') as CodeName,isnull(sum(a.TotalPrice*b.Discount/100*b.Rate),0) as StandardSell                                     ");
            sql.AppendLine("       ,isnull(sum(a.ProductCount),0) as ProductCount                                                                                               ");
            sql.AppendLine("       ,isnull(sum(a.TotalFee*b.Discount/100{0}),0) as SellTax                                                                                  ");
            sql.AppendLine("       ,isnull(sum(a.TotalTax*b.Discount/100{0}),0) as TotalTax                                                                                 ");
            sql.AppendLine("       ,(select sum(a1.TotalFee*b1.Discount/100{1}) from   officedba.SellOrderDetail as a1 left join officedba.SellOrder as b1                 ");
            sql.AppendLine("       on a1.OrderNo=b1.OrderNo and a1.CompanyCD=b1.CompanyCD where a1.CompanyCD=@CompanyCD and b1.Status!='3' and b1.BillStatus!='1' {2}");
            if (!string.IsNullOrEmpty(BeginTime))
            {
                sql.AppendLine(" and b1.OrderDate>=@BeginTime1");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginTime1", BeginTime));
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                sql.AppendLine(" and b1.OrderDate<=@EndDate1");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate1", EndTime));
            }
            sql.AppendLine("       )as AllTotalFee  ");
            sql.AppendLine("from   officedba.SellOrderDetail as a left join officedba.SellOrder as b on a.OrderNo=b.OrderNo and a.CompanyCD=b.CompanyCD                         ");
            sql.AppendLine("       left join officedba.ProductInfo as c on a.ProductID=c.ID left join officedba.CodeProductType as d on c.TypeID=d.ID                             ");
            sql.AppendLine("where  a.CompanyCD=@CompanyCD and b.Status!='3' and b.BillStatus!='1'  {3}      ");
            if (!string.IsNullOrEmpty(BeginTime))
            {
                sql.AppendLine(" and b.OrderDate>=@BeginTime");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginTime", BeginTime));
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                sql.AppendLine(" and b.OrderDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndTime));
            }
            sql.AppendLine(" group by d.ID,d.CodeName  ");
            if (Method == "1")
            {
                sql.AppendLine(" order by " + OrderBy);
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            string mysql = "";
            if (!string.IsNullOrEmpty(CurrencyType) && CurrencyType != "0")
            {
                string[] agr = new string[4] { "", "", " and b1.CurrencyType=@CurrencyType", " and b.CurrencyType=@CurrencyType" };                
                mysql = String.Format(sql.ToString(),agr);
                comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType",CurrencyType));
            }
            else
            {
                string[] agr = new string[4] { "*b.Rate", "*b1.Rate", "", "" };
                mysql = String.Format(sql.ToString(), agr);
            }
            comm.CommandText = mysql;
            DataTable dt = new DataTable();
            if (Method == "0")
            {
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref TotalCount);
            }
            if (Method == "1")
            {
                dt = SqlHelper.ExecuteSearch(comm);
            }
            if (Method == "1")
            {
                SqlCommand TotalComm = new SqlCommand();
                StringBuilder TotalSql = new StringBuilder();
                TotalSql.AppendLine("select isnull(sum(a.TotalPrice*b.Discount/100{0}),0) as TotalStandardSell                                                 ");
                TotalSql.AppendLine("       ,isnull(sum(a.ProductCount),0) as TotalProductCount                                                                    ");
                TotalSql.AppendLine("       ,isnull(sum(a.TotalFee*b.Discount/100{0}),0) as TotalSellTax                                                       ");
                TotalSql.AppendLine("       ,isnull(sum(a.TotalTax*b.Discount/100{0}),0) as TotalTax                                                           ");
                TotalSql.AppendLine("from   officedba.SellOrderDetail as a left join officedba.SellOrder as b on a.OrderNo=b.OrderNo and a.CompanyCD=b.CompanyCD   ");
                TotalSql.AppendLine("where  a.CompanyCD=@CompanyCD and b.Status!='3' and b.BillStatus!='1'      {1}                                                      ");

                if (!string.IsNullOrEmpty(BeginTime))
                {
                    TotalSql.AppendLine(" and b.OrderDate>=@BeginTime");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@BeginTime", BeginTime));
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    TotalSql.AppendLine(" and b.OrderDate<=@EndDate");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndTime));
                }
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                string myTotalSql = "";
                if (!string.IsNullOrEmpty(CurrencyType) && CurrencyType != "0")
                {
                    myTotalSql = String.Format(TotalSql.ToString(), "", " and b.CurrencyType=@CurrencyType");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", CurrencyType));
                }
                else
                {
                    myTotalSql = String.Format(TotalSql.ToString(), "*b.Rate", "");
                }
                TotalComm.CommandText = myTotalSql;
                DataTable TotalDt = SqlHelper.ExecuteSearch(TotalComm);
                TotalProductCount = "0";
                TotalSellTax = "0";
                TotalStandardSell = "0";
                TotalTax = "0";
                if (TotalDt.Rows.Count > 0)
                {
                    TotalProductCount = TotalDt.Rows[0]["TotalProductCount"].ToString();
                    TotalSellTax = TotalDt.Rows[0]["TotalSellTax"].ToString();
                    TotalStandardSell = TotalDt.Rows[0]["TotalStandardSell"].ToString();
                    TotalTax = TotalDt.Rows[0]["TotalTax"].ToString();
                }
            }
            return dt;

        }
        #endregion

        #region 客户购买年度分析
        public static DataTable GetCustBuyYear(string Method, int pageIndex, int pageSize, string OrderBy, string CompanyCD, string Cust, string Year, string CurrencyType, ref string TotalNowMonth, ref string TotalNowYear, ref string TotalLastMonth, ref string TotalLastYear, ref string TotalPrice, ref int TotalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select distinct isnull(a.CustID,0) as ID,isnull(b.CustName,'') as CustName,isnull(sum(a.TotalPrice{0}*a.Discount/100),0) as AllTotalPrice   ");
            sql.AppendLine("        ,isnull((select sum(c.TotalPrice{1}*c.Discount/100) from  officedba.SellOrder as c where c.CompanyCD=@CompanyCD and Month(c.OrderDate)=@NowMonth and Year(c.OrderDate)=@NowYear and c.CustID=a.CustID and c.BillStatus!='1' and c.Status!='3' {5}),0) as NowMonth     ");
            sql.AppendLine("        ,isnull((select sum(e.TotalPrice{2}*e.Discount/100) from  officedba.SellOrder as e where e.CompanyCD=@CompanyCD and YEAR(e.OrderDate)=@NowYear and e.CustID=a.CustID and e.BillStatus!='1' and e.Status!='3' {6} ),0) as NowYear        ");
            sql.AppendLine(" 	    ,isnull((select sum(g.TotalPrice{3}*g.Discount/100) from  officedba.SellOrder as g where g.CompanyCD=@CompanyCD and Month(g.OrderDate)=@NowMonth and Year(g.OrderDate)=@LastYear and g.CustID=a.CustID and g.BillStatus!='1' and g.Status!='3' {7} ),0) as LastMonth      ");
            sql.AppendLine("        ,isnull((select sum(i.TotalPrice{4}*i.Discount/100) from  officedba.SellOrder as i where i.CompanyCD=@CompanyCD and YEAR(i.OrderDate)=@LastYear and i.CustID=a.CustID and i.BillStatus!='1' and i.Status!='3' {8}),0)  as LastYear       ");
            sql.AppendLine(" from   officedba.SellOrder as a left join officedba.CustInfo as b on b.ID=a.CustID  ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD and b.CompanyCD=@CompanyCD and a.BillStatus!='1' and a.Status!='3' {9} ");
            if (Cust != "0" && Cust != "")
            {
                sql.AppendLine(" and a.CustID=@Cust");
                comm.Parameters.Add(SqlHelper.GetParameter("@Cust", Cust));
            }
            if (!string.IsNullOrEmpty(Year))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@NowYear", Year));
                comm.Parameters.Add(SqlHelper.GetParameter("@LastYear", Convert.ToInt32(Year) - 1));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@NowYear", DateTime.Now.Year));
                comm.Parameters.Add(SqlHelper.GetParameter("@LastYear", DateTime.Now.Year - 1));
            }
            sql.AppendLine(" group by a.CustID,b.CustName ");
            if (Method == "1")
            {
                sql.AppendLine(" order by " + OrderBy);
            }
            string NowMonth = DateTime.Now.Month.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@NowMonth", NowMonth));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            string mysql = "";
            if (!string.IsNullOrEmpty(CurrencyType) && CurrencyType != "0")
            {
                string[] agr = new string[10];
                agr[0] = "";
                agr[1] = "";
                agr[2] = "";
                agr[3] = "";
                agr[4] = "";
                agr[5] =" and c.CurrencyType=@CurrencyType";
                agr[6] =" and e.CurrencyType=@CurrencyType";
                agr[7] =" and g.CurrencyType=@CurrencyType";
                agr[8] =" and i.CurrencyType=@CurrencyType";
                agr[9] =" and a.CurrencyType=@CurrencyType";
                mysql = String.Format(sql.ToString(), agr);
                comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", CurrencyType));
            }
            else
            {
                string[] agr = new string[10];
                agr[0] = "*a.Rate";
                agr[1] = "*c.Rate";
                agr[2] = "*e.Rate";
                agr[3] = "*g.Rate";
                agr[4] = "*i.Rate";
                agr[5] = "";
                agr[6] = "";
                agr[7] = "";
                agr[8] = "";
                agr[9] = "";
                mysql = String.Format(sql.ToString(), agr);
            }
            comm.CommandText = mysql;
            DataTable dt = new DataTable();
            if (Method == "0")
            {
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref TotalCount);
            }
            if (Method == "1")
            {
                dt = SqlHelper.ExecuteSearch(comm);
            }
            if (Method == "1")
            {
                StringBuilder TotalSql1 = new StringBuilder();
                TotalSql1.AppendLine(" select isnull(sum(a.TotalPrice{0}*a.Discount/100),0) as AllTotalPrice ,");
                SqlCommand TotalComm = new SqlCommand();
                TotalSql1.AppendLine(" isnull((select sum(c.TotalPrice{1}*c.Discount/100) from  officedba.SellOrder as c  where c.CompanyCD=@CompanyCD and Month(c.OrderDate)=@NowMonth and Year(c.OrderDate)=@NowYear and c.BillStatus!='1' and c.Status!='3' {5} ");
                if (Cust != "0" && Cust != "")
                {
                    TotalSql1.AppendLine(" and c.CustID=@Cust");
                }
                TotalSql1.AppendLine("  ),0) as TotalNowMonth ");
                TotalSql1.AppendLine(" ,isnull((select sum(e.TotalPrice{2}*e.Discount/100) from  officedba.SellOrder as e where e.CompanyCD=@CompanyCD and  YEAR(e.OrderDate)=@NowYear and e.BillStatus!='1' and e.Status!='3' {6} ");
                if (Cust != "0" && Cust != "")
                {
                    TotalSql1.AppendLine(" and e.CustID=@Cust");
                }
                TotalSql1.AppendLine("  ),0) as TotalNowYear");
                TotalSql1.AppendLine(" ,isnull((select sum(g.TotalPrice{3}*g.Discount/100) from  officedba.SellOrder as g where g.CompanyCD=@CompanyCD and Month(g.OrderDate)=@NowMonth and Year(g.OrderDate)=@LastYear and g.BillStatus!='1' and g.Status!='3' {7} ");
                if (Cust != "0" && Cust != "")
                {
                    TotalSql1.AppendLine(" and g.CustID=@Cust");
                }
                TotalSql1.AppendLine("  ),0) as TotalLastMonth");
                TotalSql1.AppendLine(" ,isnull((select sum(i.TotalPrice{4}*i.Discount/100) from  officedba.SellOrder as i where  i.CompanyCD=@CompanyCD and YEAR(i.OrderDate)=@LastYear and i.BillStatus!='1' and i.Status!='3' {8} ");
                if (Cust != "0" && Cust != "")
                {
                    TotalSql1.AppendLine(" and i.CustID=@Cust");
                }
                TotalSql1.AppendLine(" ),0) as TotalLastYear");
                TotalSql1.AppendLine(" from   officedba.SellOrder as a left join officedba.CustInfo as b on b.ID=a.CustID ");
                TotalSql1.AppendLine(" where a.CompanyCD=@CompanyCD and b.CompanyCD=@CompanyCD and a.BillStatus!='1' and a.Status!='3' {9}");
                if (Cust != "0" && Cust != "")
                {
                    TotalSql1.AppendLine(" and a.CustID=@Cust");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@Cust", Cust));
                }
                if (!string.IsNullOrEmpty(Year))
                {

                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@NowYear", Year));
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@LastYear", Convert.ToInt32(Year) - 1));
                }
                else
                {
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@NowYear", DateTime.Now.Year));
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@LastYear", DateTime.Now.Year - 1));
                }
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@NowMonth", NowMonth));
                string myTotalSql = "";
                if (!string.IsNullOrEmpty(CurrencyType) && CurrencyType != "0")
                {
                    string[] agr = new string[10];
                    agr[0] = "";
                    agr[1] = "";
                    agr[2] = "";
                    agr[3] = "";
                    agr[4] = "";
                    agr[5] = " and c.CurrencyType=@CurrencyType";
                    agr[6] = " and e.CurrencyType=@CurrencyType";
                    agr[7] = " and g.CurrencyType=@CurrencyType";
                    agr[8] = " and i.CurrencyType=@CurrencyType";
                    agr[9] = " and a.CurrencyType=@CurrencyType";
                    myTotalSql = String.Format(TotalSql1.ToString(), agr);
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", CurrencyType));
                }
                else
                {
                    string[] agr = new string[10];
                    agr[0] = "*a.Rate";
                    agr[1] = "*c.Rate";
                    agr[2] = "*e.Rate";
                    agr[3] = "*g.Rate";
                    agr[4] = "*i.Rate";
                    agr[5] = "";
                    agr[6] = "";
                    agr[7] = "";
                    agr[8] = "";
                    agr[9] = "";
                    myTotalSql = String.Format(TotalSql1.ToString(), agr);
                }
                TotalComm.CommandText = myTotalSql;

                DataTable TotalDt = SqlHelper.ExecuteSearch(TotalComm);
                TotalLastMonth = "0"; TotalLastYear = "0"; TotalNowMonth = "0"; TotalNowYear = "0"; TotalPrice = "0";
                if (TotalDt.Rows.Count > 0)
                {
                    TotalNowMonth = TotalDt.Rows[0]["TotalNowMonth"].ToString();
                    TotalNowYear = TotalDt.Rows[0]["TotalNowYear"].ToString();
                    TotalLastMonth = TotalDt.Rows[0]["TotalLastMonth"].ToString();
                    TotalLastYear = TotalDt.Rows[0]["TotalLastYear"].ToString();
                    TotalPrice = TotalDt.Rows[0]["AllTotalPrice"].ToString();
                }
            }
            return dt;

        }
        #endregion

        #region 客户购买季度分析
        public static DataTable GetCustBuyQuarter(string Method, int pageIndex, int pageSize, string OrderBy, string CompanyCD, string Cust, string quarter, string CurrencyType, ref string TotalPrice, ref string TotalPrice1, ref string TotalPrice2, ref string TotalPrice3, ref string TotalPrice4, ref int TotalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select distinct isnull(a.CustID,0) as ID,isnull(b.CustName,'') as CustName,isnull(sum(a.TotalPrice{0}*a.Discount/100),0) as AllTotalPrice ");
            sql.AppendLine("        ,isnull((select sum(isnull(c.TotalPrice,0){1}*c.Discount/100)  from  officedba.SellOrder as c where c.CompanyCD=@CompanyCD and c.BillStatus!='1' and c.Status!='3' and  Month(c.OrderDate)>=@BeginNowMonth and Month(c.OrderDate)<=@EndNowMonth and Year(c.OrderDate)=@NowYear and c.CustID=a.CustID {5}),0) as TotalPrice1     ");
            sql.AppendLine("        ,isnull((select sum(isnull(e.TotalPrice,0){2}*e.Discount/100)  from  officedba.SellOrder as e where e.CompanyCD=@CompanyCD and e.BillStatus!='1' and e.Status!='3' and Month(e.OrderDate)>=@BeginNowMonth2 and Month(e.OrderDate)<=@EndNowMonth2 and YEAR(e.OrderDate)=@NowYear and e.CustID=a.CustID {6}),0) as TotalPrice2       ");
            sql.AppendLine(" 	    ,isnull((select sum(isnull(g.TotalPrice,0){3}*g.Discount/100)  from  officedba.SellOrder as g where g.CompanyCD=@CompanyCD and g.BillStatus!='1' and g.Status!='3' and Month(g.OrderDate)>=@BeginNowMonth3 and Month(g.OrderDate)<=@EndNowMonth3 and Year(g.OrderDate)=@NowYear and g.CustID=a.CustID {7}),0) as TotalPrice3     ");
            sql.AppendLine("        ,isnull((select sum(isnull(i.TotalPrice,0){4}*i.Discount/100)  from  officedba.SellOrder as i where i.CompanyCD=@CompanyCD and i.BillStatus!='1' and i.Status!='3' and Month(i.OrderDate)>=@BeginNowMonth4 and Month(i.OrderDate)<=@EndNowMonth4 and YEAR(i.OrderDate)=@NowYear and i.CustID=a.CustID {8}),0)  as TotalPrice4       ");
            sql.AppendLine("        ,min(a.TotalPrice*a.Discount/100{0}) as MinPrice");
            sql.AppendLine("        ,max(a.TotalPrice*a.Discount/100{0}) as MaxPrice");
            sql.AppendLine("        ,avg(a.TotalPrice*a.Discount/100{0}) as avgPrice ");
            sql.AppendLine(" from   officedba.SellOrder as a left join officedba.CustInfo as b on b.ID=a.CustID  ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD and a.BillStatus!='1' and a.Status!='3' {9} ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth", 1));
            comm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth", 3));
            comm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth2", 4));
            comm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth2", 6));
            comm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth3", 7));
            comm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth3", 9));
            comm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth4", 10));
            comm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth4", 12));
            if (Cust != "" && Cust != "0")
            {
                sql.AppendLine(" and a.CustID=@CustID");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustID", Cust));
            }
            if (!string.IsNullOrEmpty(quarter))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@NowYear", quarter));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@NowYear", DateTime.Now.Year));
            }
            sql.AppendLine(" group by a.CustID,b.CustName ");
            if (Method == "1")
            {
                sql.AppendLine(" order by " + OrderBy);
            }
            string mySql = "";
            if (!string.IsNullOrEmpty(CurrencyType) && CurrencyType != "0")
            {
                string[] agr = new string[13];
                agr[0] = "";
                agr[1] = "";
                agr[2] = "";
                agr[3] = "";
                agr[4] = "";
                agr[5] = " and c.CurrencyType=@CurrencyType";
                agr[6] = " and e.CurrencyType=@CurrencyType";
                agr[7] = " and g.CurrencyType=@CurrencyType";
                agr[8] = " and i.CurrencyType=@CurrencyType";
                agr[9] = " and a.CurrencyType=@CurrencyType";
                mySql = String.Format(sql.ToString(), agr);
                comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", CurrencyType));
            }
            else
            {
                string[] agr = new string[10];
                agr[0] = "*a.Rate";
                agr[1] = "*c.Rate";
                agr[2] = "*e.Rate";
                agr[3] = "*g.Rate";
                agr[4] = "*i.Rate";
                agr[5] = "";
                agr[6] = "";
                agr[7] = "";
                agr[8] = "";
                agr[9] = "";
                mySql = String.Format(sql.ToString(), agr);
            }
            comm.CommandText = mySql;
            DataTable dt = new DataTable();
            if (Method == "0")
            {
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref TotalCount);
            }
            if (Method == "1")
            {
                dt = SqlHelper.ExecuteSearch(comm);
            }
            if (Method == "1")//----------------------打印页需要的合计信息----------------------
            {
                SqlCommand TotalComm = new SqlCommand();
                StringBuilder TotalSql = new StringBuilder();
                TotalSql.AppendLine("    select    isnull(sum(a.TotalPrice{0}*a.Discount/100),0) as AllTotalPrice, ");
                TotalSql.AppendLine("        isnull((select sum(c.TotalPrice{1}*c.Discount/100) from  officedba.SellOrder as c  where c.CompanyCD=@CompanyCD and c.BillStatus!='1'  and c.Status!='3' and Month(c.OrderDate)>=@BeginNowMonth and Month(c.OrderDate)<=@EndNowMonth and Year(c.OrderDate)=@NowYear {5}");
                if (Cust != "" && Cust != "0")
                {
                    TotalSql.AppendLine(" and c.CustID=@CustID");
                }
                TotalSql.AppendLine("  ),0) as TotalPrice1     ");
                TotalSql.AppendLine("        ,isnull((select sum(e.TotalPrice{2}*e.Discount/100) from  officedba.SellOrder as e  where e.CompanyCD=@CompanyCD and e.BillStatus!='1' and e.Status!='3' and Month(e.OrderDate)>=@BeginNowMonth2 and Month(e.OrderDate)<=@EndNowMonth2 and YEAR(e.OrderDate)=@NowYear {6} ");
                if (Cust != "" && Cust != "0")
                {
                    TotalSql.AppendLine(" and e.CustID=@CustID");
                }
                TotalSql.AppendLine("  ),0) as TotalPrice2       ");
                TotalSql.AppendLine(" 	     ,isnull((select sum(g.TotalPrice{3}*g.Discount/100) from  officedba.SellOrder as g  where g.CompanyCD=@CompanyCD and g.BillStatus!='1' and g.Status!='3' and Month(g.OrderDate)>=@BeginNowMonth3 and Month(g.OrderDate)<=@EndNowMonth3 and Year(g.OrderDate)=@NowYear {7}");
                if (Cust != "" && Cust != "0")
                {
                    TotalSql.AppendLine(" and g.CustID=@CustID");
                }
                TotalSql.AppendLine("  ),0) as TotalPrice3     ");
                TotalSql.AppendLine("        ,isnull((select sum(i.TotalPrice{4}*i.Discount/100) from  officedba.SellOrder as i  where i.CompanyCD=@CompanyCD and i.BillStatus!='1' and i.Status!='3' and Month(i.OrderDate)>=@BeginNowMonth4 and Month(i.OrderDate)<=@EndNowMonth4 and YEAR(i.OrderDate)=@NowYear {8}");
                if (Cust != "" && Cust != "0")
                {
                    TotalSql.AppendLine(" and i.CustID=@CustID");
                }
                TotalSql.AppendLine("  ),0)  as TotalPrice4       ");
                TotalSql.AppendLine(" from   officedba.SellOrder as a left join officedba.CustInfo as b on b.ID=a.CustID ");
                TotalSql.AppendLine(" where a.BillStatus!='1' and a.Status!='3' and a.CompanyCD=@CompanyCD {9} ");
                if (Cust != "" && Cust != "0")
                {
                    TotalSql.AppendLine(" and a.CustID=@CustID");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@CustID", Cust));
                }
                if (!string.IsNullOrEmpty(quarter))
                {
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@NowYear", quarter));
                }
                else
                {
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@NowYear", DateTime.Now.Year));
                }
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth", 1));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth", 3));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth2", 4));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth2", 6));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth3", 7));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth3", 9));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth4", 10));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth4", 12));
                string myTotalSql = "";
                if (!string.IsNullOrEmpty(CurrencyType) && CurrencyType != "0")
                {
                    string[] agr = new string[13];
                    agr[0] = "";
                    agr[1] = "";
                    agr[2] = "";
                    agr[3] = "";
                    agr[4] = "";
                    agr[5] = " and c.CurrencyType=@CurrencyType";
                    agr[6] = " and e.CurrencyType=@CurrencyType";
                    agr[7] = " and g.CurrencyType=@CurrencyType";
                    agr[8] = " and i.CurrencyType=@CurrencyType";
                    agr[9] = " and a.CurrencyType=@CurrencyType";
                    myTotalSql = String.Format(TotalSql.ToString(), agr);
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", CurrencyType));
                }
                else
                {
                    string[] agr = new string[10];
                    agr[0] = "*a.Rate";
                    agr[1] = "*c.Rate";
                    agr[2] = "*e.Rate";
                    agr[3] = "*g.Rate";
                    agr[4] = "*i.Rate";
                    agr[5] = "";
                    agr[6] = "";
                    agr[7] = "";
                    agr[8] = "";
                    agr[9] = "";
                    myTotalSql = String.Format(TotalSql.ToString(), agr);
                }
                TotalComm.CommandText = myTotalSql;
                DataTable TotalDt = SqlHelper.ExecuteSearch(TotalComm);
                TotalPrice = "0";
                TotalPrice1 = "0";
                TotalPrice2 = "0";
                TotalPrice3 = "0";
                TotalPrice4 = "0";
                if (TotalDt.Rows.Count > 0)
                {
                    TotalPrice = TotalDt.Rows[0]["AllTotalPrice"].ToString();
                    TotalPrice1 = TotalDt.Rows[0]["TotalPrice1"].ToString();
                    TotalPrice2 = TotalDt.Rows[0]["TotalPrice2"].ToString();
                    TotalPrice3 = TotalDt.Rows[0]["TotalPrice3"].ToString();
                    TotalPrice4 = TotalDt.Rows[0]["TotalPrice4"].ToString();

                }
            }
            return dt;
        }
        #endregion

        #region 客户购买明细分析
        public static DataTable GetCustBuyDetail(string Method, int pageIndex, int pageSize, string OrderBy, string CompanyCD, string Cust, string Product, string BeginDate, string EndDate, string CurrencyType, ref string TotalProductCount, ref string TotalPrice, ref string TotalTax, ref string TotalBackNumber, ref string TotalBackTotalPrice, ref string TotalFee, ref int TotalCount)
        {

            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select tt.*,p.ProductName,p.ProdNo as ProNo,cust.CustName from                                         ");
            sql.AppendLine(" (select isnull(t1.ProductID,t2.ProductID) as ProductID ,isnull(t1.CustID,t2.CustID) as CustID,         ");
            sql.AppendLine(" isnull(t1.ProductCount,0)ProductCount,isnull(t1.TotalFee,0)TotalFee,isnull(t1.TotalPrice,0)TotalPrice  ");
            sql.AppendLine(" ,isnull(t1.TotalTax,0)TotalTax                                                                         ");
            sql.AppendLine(" ,isnull(t2.BackNumber,0)BackNumber,isnull(t2.BackTotalPrice,0)BackTotalPrice from (                    ");
            sql.AppendLine(" select so.ProductID,s.CustID,isnull(sum(so.ProductCount),0) as ProductCount                            ");
            sql.AppendLine(" ,sum(isnull(so.TotalFee,0){0}* s.Discount*0.01) as TotalFee,                                    ");
            sql.AppendLine(" sum(isnull(so.TotalPrice,0){0}* s.Discount*0.01) as TotalPrice,                                   ");
            sql.AppendLine(" sum(isnull(so.TotalTax,0){0}* s.Discount*0.01) as TotalTax                                       ");
            sql.AppendLine(" from officedba. SellOrderDetail as so left join                                                        ");
            sql.AppendLine(" officedba. SellOrder as s on so.CompanyCD=s.CompanyCD and so.OrderNo=s.OrderNo                         ");
            sql.AppendLine(" where so.CompanyCD=@CompanyCD and s.BillStatus<>'1' and s.Status<>'3' {2}                                    ");
            if (Cust != "" && Cust != "0")
            {
                sql.AppendLine(" and s.CustID=@CustID ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustID", Cust));
            }
            if (Product != "" && Product != "0")
            {
                sql.AppendLine(" and so.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", Product));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and s.OrderDate>=@StartDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and s.OrderDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            sql.AppendLine(" group by so.ProductID,s.CustID) as t1 full join                                                        ");
            sql.AppendLine(" (                                                                                                      ");
            sql.AppendLine(" SELECT   a1.CustID, b1.ProductID, SUM(isnull(b1.BackNumber,0)) AS BackNumber                           ");
            sql.AppendLine(" ,SUM(isnull(b1.TotalPrice,0){1}*a1.Discount*0.01) AS BackTotalPrice                              ");
            sql.AppendLine(" FROM     officedba.SellBackDetail AS b1 LEFT OUTER JOIN                                                ");
            sql.AppendLine(" officedba.SellBack AS a1 ON a1.BackNo = b1.BackNo AND a1.CompanyCD = b1.CompanyCD                      ");
            sql.AppendLine(" WHERE     (a1.CompanyCD =@CompanyCD and a1.BillStatus<>'1')     {3}                                         ");
            if (Cust != "" && Cust != "0")
            {
                sql.AppendLine(" and a1.CustID=@CustID1");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustID1", Cust));
            }
            if (Product != "" && Product != "0")
            {
                sql.AppendLine(" and b1.ProductID=@ProductID1");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID1", Product));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and a1.BackDate>=@StartDate1");
                comm.Parameters.Add(SqlHelper.GetParameter("@StartDate1", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and a1.BackDate<=@EndDate1");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate1", EndDate));
            }
            sql.AppendLine(" GROUP BY a1.CustID, b1.ProductID                                                                       ");
            sql.AppendLine(" ) as t2 on t1.CustID=t2.CustID and t1.ProductID=t2.ProductID) as tt                                    ");
            sql.AppendLine(" left join officedba.ProductInfo as p on tt.ProductID=p.ID                                              ");
            sql.AppendLine(" left join officedba.CustInfo as cust on tt.CustID=cust.ID                                              ");
            if (Method == "1")
            {
                sql.AppendLine(" order by " + OrderBy);
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            #region 币种处理
            string mySql = "";
            if (!string.IsNullOrEmpty(CurrencyType) && CurrencyType != "0")
            {
                string[] agr = new string[4];
                agr[0] = "";
                agr[1] = "";
                agr[2] = " and s.CurrencyType=@CurrencyType";
                agr[3] = " and a1.CurrencyType=@CurrencyType";
                mySql = String.Format(sql.ToString(), agr);
                comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", CurrencyType));
            }
            else
            {
                string[] agr = new string[4];
                agr[0] = "*s.Rate";
                agr[1] = "*a1.Rate";
                agr[2] = "";
                agr[3] = "";
                mySql = String.Format(sql.ToString(), agr);
            }
            #endregion
            comm.CommandText = mySql;

            DataTable dt = new DataTable();
            if (Method == "0")
            {
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref TotalCount);
            }
            if (Method == "1")
            {
                dt = SqlHelper.ExecuteSearch(comm);
            }
            if (Method == "1")
            {
                StringBuilder TotalSql = new StringBuilder();
                SqlCommand totalcomm = new SqlCommand();
                TotalSql.AppendLine(" select '' as ProductID,'' as CustID,isnull(sum(tt.ProductCount),0) as TotalProductCount,isnull(sum(tt.TotalFee),0) as  TotalFee   ");
                TotalSql.AppendLine(" ,isnull(sum(tt.TotalPrice),0) as TotalPrice,isnull(sum(tt.TotalTax),0)as TotalTax,isnull(sum(tt.BackNumber),0) as TotalBackNumber           ");
                TotalSql.AppendLine(" ,isnull(sum(tt.BackTotalPrice),0) TotalBackTotalPrice                                                                 ");
                TotalSql.AppendLine(" ,'' as ProductName,'' as ProNo,'合计' as CustName from                                                 ");
                TotalSql.AppendLine(" (select isnull(t1.ProductID,t2.ProductID) as ProductID ,isnull(t1.CustID,t2.CustID) as CustID,         ");
                TotalSql.AppendLine(" isnull(t1.ProductCount,0)ProductCount,isnull(t1.TotalFee,0)TotalFee,isnull(t1.TotalPrice,0)TotalPrice  ");
                TotalSql.AppendLine(" ,isnull(t1.TotalTax,0)TotalTax                                                                         ");
                TotalSql.AppendLine(" ,isnull(t2.BackNumber,0)BackNumber,isnull(t2.BackTotalPrice,0)BackTotalPrice from (                    ");
                TotalSql.AppendLine(" select so.ProductID,s.CustID,isnull(sum(so.ProductCount),0) as ProductCount                            ");
                TotalSql.AppendLine(" ,isnull(sum(isnull(so.TotalFee,0){0} * s.Discount*0.01),0) as TotalFee,                                    ");
                TotalSql.AppendLine(" isnull(sum(isnull(so.TotalPrice,0){0}* s.Discount*0.01),0) as TotalPrice,                                   ");
                TotalSql.AppendLine(" isnull(sum(isnull(so.TotalTax,0){0}* s.Discount*0.01),0) as TotalTax                                       ");
                TotalSql.AppendLine(" from officedba. SellOrderDetail as so left join                                                        ");
                TotalSql.AppendLine(" officedba. SellOrder as s on so.CompanyCD=s.CompanyCD and so.OrderNo=s.OrderNo                         ");
                TotalSql.AppendLine(" where so.CompanyCD=@CompanyCD and s.BillStatus<>'1' and s.Status<>'3'   {2}                                  ");
                if (Cust != "" && Cust != "0")
                {
                    TotalSql.AppendLine(" and s.CustID=@CustID2");
                    totalcomm.Parameters.Add(SqlHelper.GetParameter("@CustID2", Cust));
                }
                if (Product != "" && Product != "0")
                {
                    TotalSql.AppendLine(" and so.ProductID=@ProductID2");
                    totalcomm.Parameters.Add(SqlHelper.GetParameter("@ProductID2", Product));
                }
                if (!string.IsNullOrEmpty(BeginDate))
                {
                    TotalSql.AppendLine(" and s.OrderDate>=@StartDate2");
                    totalcomm.Parameters.Add(SqlHelper.GetParameter("@StartDate2", BeginDate));
                }
                if (!string.IsNullOrEmpty(EndDate))
                {
                    TotalSql.AppendLine(" and s.OrderDate<=@EndDate2");
                    totalcomm.Parameters.Add(SqlHelper.GetParameter("@EndDate2", EndDate));
                }
                TotalSql.AppendLine(" group by so.ProductID,s.CustID) as t1 full join                                                        ");
                TotalSql.AppendLine(" (                                                                                                      ");
                TotalSql.AppendLine(" SELECT   a1.CustID, b1.ProductID, isnull(SUM(isnull(b1.BackNumber,0)),0) AS BackNumber                           ");
                TotalSql.AppendLine(" ,isnull(SUM(isnull(b1.TotalPrice,0){1}*a1.Discount*0.01),0) AS BackTotalPrice                              ");
                TotalSql.AppendLine(" FROM     officedba.SellBackDetail AS b1 LEFT OUTER JOIN                                                ");
                TotalSql.AppendLine(" officedba.SellBack AS a1 ON a1.BackNo = b1.BackNo AND a1.CompanyCD = b1.CompanyCD                      ");
                TotalSql.AppendLine(" WHERE     (a1.CompanyCD =@CompanyCD and a1.BillStatus<>'1')    {3}                                          ");
                if (Cust != "" && Cust != "0")
                {
                    TotalSql.AppendLine(" and a1.CustID=@CustID3");
                    totalcomm.Parameters.Add(SqlHelper.GetParameter("@CustID3", Cust));
                }
                if (Product != "" && Product != "0")
                {
                    TotalSql.AppendLine(" and b1.ProductID=@ProductID3");
                    totalcomm.Parameters.Add(SqlHelper.GetParameter("@ProductID3", Product));
                }
                if (!string.IsNullOrEmpty(BeginDate))
                {
                    TotalSql.AppendLine(" and a1.BackDate>=@StartDate3");
                    totalcomm.Parameters.Add(SqlHelper.GetParameter("@StartDate3", BeginDate));
                }
                if (!string.IsNullOrEmpty(EndDate))
                {
                    TotalSql.AppendLine(" and a1.BackDate<=@EndDate3");
                    totalcomm.Parameters.Add(SqlHelper.GetParameter("@EndDate3", EndDate));
                }
                TotalSql.AppendLine(" GROUP BY a1.CustID, b1.ProductID                                                                       ");
                TotalSql.AppendLine(" ) as t2 on t1.CustID=t2.CustID and t1.ProductID=t2.ProductID) as tt                                    ");
                TotalSql.AppendLine(" left join officedba.ProductInfo as p on tt.ProductID=p.ID                                              ");
                TotalSql.AppendLine(" left join officedba.CustInfo as cust on tt.CustID=cust.ID                                              ");

                totalcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                #region 币种处理
                string myTotalSql = "";
                if (!string.IsNullOrEmpty(CurrencyType) && CurrencyType != "0")
                {
                    string[] agr = new string[4];
                    agr[0] = "";
                    agr[1] = "";
                    agr[2] = " and s.CurrencyType=@CurrencyType";
                    agr[3] = " and a1.CurrencyType=@CurrencyType";
                    myTotalSql = String.Format(TotalSql.ToString(), agr);
                    totalcomm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", CurrencyType));
                }
                else
                {
                    string[] agr = new string[4];
                    agr[0] = "*s.Rate";
                    agr[1] = "*a1.Rate";
                    agr[2] = "";
                    agr[3] = "";
                    myTotalSql = String.Format(TotalSql.ToString(), agr);
                }
                #endregion
                totalcomm.CommandText = myTotalSql;
                DataTable totaldt = SqlHelper.ExecuteSearch(totalcomm);
                TotalProductCount = "0"; TotalPrice = "0"; TotalTax = "0"; TotalBackNumber = "0"; TotalBackTotalPrice = "0"; TotalFee = "0";
                if (totaldt.Rows.Count > 0)
                {
                    TotalProductCount = totaldt.Rows[0]["TotalProductCount"].ToString();
                    TotalPrice = totaldt.Rows[0]["TotalPrice"].ToString();
                    TotalFee = totaldt.Rows[0]["TotalFee"].ToString();
                    TotalTax = totaldt.Rows[0]["TotalTax"].ToString();
                    TotalBackNumber = totaldt.Rows[0]["TotalBackNumber"].ToString();
                    TotalBackTotalPrice = totaldt.Rows[0]["TotalBackTotalPrice"].ToString();
                }
            }
            return dt;
        }
        #endregion

        #region 物品销售年度分析
        public static DataTable GetProductYear(string Method, int pageIndex, int pageSize, string OrderBy, string CompanyCD, string ProductID, string Year, ref string TotalNowMonth, ref string TotalNowYear, ref string TotalLastMonth, ref string TotalLastYear, ref string TotalPrice, ref int TotalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select distinct isnull(b.ProductName,'') as ProductName ");
            sql.AppendLine("        ,isnull(sum(a.TotalPrice*c.Discount/100*isnull(c.Rate,1)),0) as TotalPrice ");
            sql.AppendLine("        ,isnull((select sum(c.TotalPrice*isnull(m.Rate,1)*m.Discount/100) from  officedba.SellOrderDetail as c left join officedba.SellOrder as m on c.OrderNo=m.OrderNo and c.CompanyCD=m.CompanyCD where m.CompanyCD=@CompanyCD and m.BillStatus!='1' and m.Status!='3' and Month(m.OrderDate)=@NowMonth and Year(m.OrderDate)=@NowYear and c.ProductID=a.ProductID),0) as NowMonth     ");
            sql.AppendLine("        ,isnull((select sum(e.TotalPrice*isnull(n.Rate,1)*n.Discount/100) from  officedba.SellOrderDetail as e left join officedba.SellOrder as n on e.OrderNo=n.OrderNo and e.CompanyCD=n.CompanyCD where n.CompanyCD=@CompanyCD and n.BillStatus!='1' and n.Status!='3' and YEAR(n.OrderDate)=@NowYear and e.ProductID=a.ProductID),0) as NowYear        ");
            sql.AppendLine(" 	    ,isnull((select sum(g.TotalPrice*isnull(x.Rate,1)*x.Discount/100) from  officedba.SellOrderDetail as g left join officedba.SellOrder as x on x.OrderNo=g.OrderNo and g.CompanyCD=x.CompanyCD where x.CompanyCD=@CompanyCD and x.BillStatus!='1' and x.Status!='3' and Month(x.OrderDate)=@NowMonth and Year(x.OrderDate)=@LastYear and g.ProductID=a.ProductID),0) as LastMonth      ");
            sql.AppendLine("        ,isnull((select sum(i.TotalPrice*isnull(y.Rate,1)*y.Discount/100) from  officedba.SellOrderDetail as i left join officedba.SellOrder as y on y.OrderNo=i.OrderNo and i.CompanyCD=y.CompanyCD where y.CompanyCD=@CompanyCD and y.BillStatus!='1' and y.Status!='3' and YEAR(y.OrderDate)=@LastYear and i.ProductID=a.ProductID),0)  as LastYear       ");
            sql.AppendLine(" from   officedba.SellOrderDetail as a      ");
            sql.AppendLine("        left join officedba.SellOrder as c on a.OrderNo=c.OrderNo and a.CompanyCD=c.CompanyCD   left join officedba.ProductInfo as b on b.ID=a.ProductID ");
            sql.AppendLine(" where  a.CompanyCD=@CompanyCD and c.BillStatus!='1'  and c.Status!='3'");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            if (ProductID != "" && ProductID != "0")
            {
                sql.AppendLine("  and a.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            if (Year != "" && Year != "0")
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@NowYear", Convert.ToInt32(Year)));
                comm.Parameters.Add(SqlHelper.GetParameter("@LastYear", Convert.ToInt32(Year) - 1));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@NowYear", DateTime.Now.Year));
                comm.Parameters.Add(SqlHelper.GetParameter("@LastYear", DateTime.Now.Year - 1));
            }
            sql.AppendLine(" group by b.ProductName,a.ProductID ");
            if (Method == "1")
            {
                sql.AppendLine(" order by " + OrderBy);
            }
            comm.CommandText = sql.ToString();
            string NowMonth = DateTime.Now.Month.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@NowMonth", NowMonth));

            DataTable dt = new DataTable();
            if (Method == "0")
            {
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref TotalCount);
            }
            if (Method == "1")
            {
                dt = SqlHelper.ExecuteSearch(comm);
            }
            if (Method == "1")
            {
                SqlCommand TotalComm = new SqlCommand();
                StringBuilder TotalSql = new StringBuilder();
                TotalSql.AppendLine(" select isnull(sum(a.TotalPrice*isnull(c.Rate,1)*c.Discount/100),0) as TotalPrice ");
                TotalSql.AppendLine("  ,(select isnull(sum(c.TotalPrice*isnull(m.Rate,1)*m.Discount/100),0) from  officedba.SellOrderDetail as c left join officedba.SellOrder as m on c.OrderNo=m.OrderNo and c.CompanyCD=m.CompanyCD where m.CompanyCD=@CompanyCD and m.BillStatus!='1' and m.Status!='3' and Month(m.OrderDate)=@NowMonth and Year(m.OrderDate)=@NowYear ");
                if (ProductID != "" && ProductID != "0")
                {
                    TotalSql.AppendLine("  and c.ProductID=@ProductID");

                }
                TotalSql.AppendLine("  ) as TotalNowMonth ");
                TotalSql.AppendLine("  ,(select isnull(sum(e.TotalPrice*isnull(n.Rate,1)*n.Discount/100),0) from  officedba.SellOrderDetail as e left join officedba.SellOrder as n on e.OrderNo=n.OrderNo and e.CompanyCD=n.CompanyCD where n.CompanyCD=@CompanyCD and n.BillStatus!='1' and n.Status!='3' and YEAR(n.OrderDate)=@NowYear ");
                if (ProductID != "" && ProductID != "0")
                {
                    TotalSql.AppendLine("  and e.ProductID=@ProductID");

                }
                TotalSql.AppendLine("  ) as TotalNowYear                               ");
                TotalSql.AppendLine("  ,(select isnull(sum(g.TotalPrice*isnull(x.Rate,1)*x.Discount/100),0) from  officedba.SellOrderDetail as g left join officedba.SellOrder as x on x.OrderNo=g.OrderNo and g.CompanyCD=x.CompanyCD where x.CompanyCD=@CompanyCD and x.BillStatus!='1' and x.Status!='3' and Month(x.OrderDate)=@NowMonth and Year(x.OrderDate)=@LastYear ");
                if (ProductID != "" && ProductID != "0")
                {
                    TotalSql.AppendLine("  and g.ProductID=@ProductID");

                }
                TotalSql.AppendLine("  ) as TotalLastMonth ");
                TotalSql.AppendLine("  ,(select isnull(sum(i.TotalPrice*y.Rate*y.Discount/100),0) from  officedba.SellOrderDetail as i left join officedba.SellOrder as y on y.OrderNo=i.OrderNo and i.CompanyCD=y.CompanyCD where y.CompanyCD=@CompanyCD and y.BillStatus!='1' and y.Status!='3' and YEAR(y.OrderDate)=@LastYear ");
                if (ProductID != "" && ProductID != "0")
                {
                    TotalSql.AppendLine("  and i.ProductID=@ProductID");

                }
                TotalSql.AppendLine("  )  as TotalLastYear                                       ");
                TotalSql.AppendLine("  from   officedba.SellOrderDetail as a                                                             ");
                TotalSql.AppendLine("  left join officedba.SellOrder as c on a.OrderNo=c.OrderNo and a.CompanyCD=c.CompanyCD   ");
                TotalSql.AppendLine("  where  a.CompanyCD=@CompanyCD                                                                         ");
                TotalSql.AppendLine(" and c.BillStatus!='1'   and c.Status!='3'                                                           ");
                if (ProductID != "" && ProductID != "0")
                {
                    TotalSql.AppendLine("  and a.ProductID=@ProductID");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
                }
                if (Year != "" && Year != "0")
                {
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@NowYear", Convert.ToInt32(Year)));
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@LastYear", Convert.ToInt32(Year) - 1));
                }
                else
                {
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@NowYear", DateTime.Now.Year));
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@LastYear", DateTime.Now.Year - 1));
                }
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@NowMonth", NowMonth));
                TotalComm.CommandText = TotalSql.ToString();
                DataTable TotalDt = SqlHelper.ExecuteSearch(TotalComm);
                TotalLastMonth = "0"; TotalLastYear = "0"; TotalNowMonth = "0"; TotalNowYear = "0"; TotalPrice = "0";
                if (TotalDt.Rows.Count > 0)
                {
                    TotalNowYear = TotalDt.Rows[0]["TotalNowYear"].ToString();
                    TotalLastMonth = TotalDt.Rows[0]["TotalLastMonth"].ToString();
                    TotalNowMonth = TotalDt.Rows[0]["TotalNowMonth"].ToString();
                    TotalLastYear = TotalDt.Rows[0]["TotalLastYear"].ToString();
                    TotalPrice = TotalDt.Rows[0]["TotalPrice"].ToString();
                }
            }
            return dt;
        }
        #endregion

        #region 物品销售季度分析
        public static DataTable GetProductBuyQuarter(string Method, int pageIndex, int pageSize, string OrderBy, string CompanyCD, string ProductID, string quarter, ref string TotalPrice, ref string TotalPrice1, ref string TotalPrice2, ref string TotalPrice3, ref string TotalPrice4, ref int TotalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select distinct isnull(b.ID,0) as ID,isnull(b.ProductName,'') as ProductName,isnull(sum(a.TotalPrice*c.Rate*c.Discount/100),0) as TotalPrice                                                                                ");
            sql.AppendLine("        ,isnull((select sum(c1.TotalPrice*isnull(m.Rate,1)*m.Discount/100) from  officedba.SellOrderDetail as c1  left join officedba.SellOrder as m on m.OrderNo=c1.OrderNo and m.CompanyCD=c1.CompanyCD where m.BillStatus!='1' and m.Status!='3' and m.CompanyCD=@CompanyCD and Month(m.OrderDate)>=@BeginNowMonth and Month(m.OrderDate)<=@EndNowMonth   and Year(m.OrderDate)=@NowYear and c1.ProductID=a.ProductID),0) as TotalPrice1     ");
            sql.AppendLine("        ,isnull((select sum(e.TotalPrice*isnull(n.Rate,1)*n.Discount/100) from  officedba.SellOrderDetail as e  left join officedba.SellOrder as n on n.OrderNo=e.OrderNo and n.CompanyCD=e.CompanyCD where n.BillStatus!='1' and n.Status!='3' and n.CompanyCD=@CompanyCD and Month(n.OrderDate)>=@BeginNowMonth2 and Month(n.OrderDate)<=@EndNowMonth2 and YEAR(n.OrderDate)=@NowYear and e.ProductID=a.ProductID),0) as TotalPrice2       ");
            sql.AppendLine(" 	    ,isnull((select sum(g.TotalPrice*isnull(x.Rate,1)*x.Discount/100) from  officedba.SellOrderDetail as g  left join officedba.SellOrder as x on x.OrderNo=g.OrderNo and x.CompanyCD=g.CompanyCD where x.BillStatus!='1' and x.Status!='3' and x.CompanyCD=@CompanyCD and Month(x.OrderDate)>=@BeginNowMonth3 and Month(x.OrderDate)<=@EndNowMonth3 and Year(x.OrderDate)=@NowYear and g.ProductID=a.ProductID),0) as TotalPrice3     ");
            sql.AppendLine("        ,isnull((select sum(i.TotalPrice*isnull(y.Rate,1)*y.Discount/100) from  officedba.SellOrderDetail as i  left join officedba.SellOrder as y on y.OrderNo=i.OrderNo and y.CompanyCD=i.CompanyCD where y.BillStatus!='1' and y.Status!='3' and y.CompanyCD=@CompanyCD and Month(y.OrderDate)>=@BeginNowMonth4 and Month(y.OrderDate)<=@EndNowMonth4 and YEAR(y.OrderDate)=@NowYear and i.ProductID=a.ProductID),0)  as TotalPrice4       ");
            sql.AppendLine("        ,min(a.TotalPrice*isnull(c.Rate,1)*c.Discount/100) as MinPrice");
            sql.AppendLine("        ,max(a.TotalPrice*isnull(c.Rate,1)*c.Discount/100) as MaxPrice");
            sql.AppendLine("        ,avg(a.TotalPrice*isnull(c.Rate,1)*c.Discount/100) as avgPrice  ");
            sql.AppendLine(" from   officedba.SellOrderDetail as a ");
            sql.AppendLine("        left join officedba.SellOrder as c on a.OrderNo=c.OrderNo and c.CompanyCD=a.CompanyCD left join officedba.ProductInfo as b on b.ID=a.ProductID ");
            sql.AppendLine(" where c.CompanyCD=@CompanyCD and c.BillStatus!='1' and c.Status!='3' ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth", 1));
            comm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth", 3));
            comm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth2", 4));
            comm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth2", 6));
            comm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth3", 7));
            comm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth3", 9));
            comm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth4", 10));
            comm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth4", 12));
            if (ProductID != "" && ProductID != "0")
            {
                sql.AppendLine(" and a.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            if (!string.IsNullOrEmpty(quarter))
            {

                comm.Parameters.Add(SqlHelper.GetParameter("@NowYear", quarter));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@NowYear", DateTime.Now.Year));

            }
            sql.AppendLine(" group by b.ID,b.ProductName,a.ProductID");
            if (Method == "1")
            {
                sql.AppendLine(" order by  " + OrderBy);
            }
            comm.CommandText = sql.ToString();
            DataTable dt = new DataTable();
            if (Method == "0")
            {
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref TotalCount);
            }
            if (Method == "1")
            {
                dt = SqlHelper.ExecuteSearch(comm);
            }
            SqlCommand TotalComm = new SqlCommand();

            StringBuilder TotalSql = new StringBuilder();
            if (Method == "1")
            {
                TotalSql.AppendLine("  select      sum(a.TotalPrice*c.Rate*c.Discount/100) as TotalPrice ");
                TotalSql.AppendLine("        ,isnull((select sum(c1.TotalPrice*isnull(m.Rate,1)*m.Discount/100) from  officedba.SellOrderDetail as c1  left join officedba.SellOrder as m on m.OrderNo=c1.OrderNo and m.CompanyCD=c1.CompanyCD where m.BillStatus!='1' and m.Status!='3' and m.CompanyCD=@CompanyCD and Month(m.OrderDate)>=@BeginNowMonth and Month(m.OrderDate)<=@EndNowMonth and Year(m.OrderDate)=@NowYear ");
                if (ProductID != "" && ProductID != "0")
                {
                    TotalSql.AppendLine("  and c1.ProductID=@ProductID");

                }
                TotalSql.AppendLine("  ),0) as TotalPrice1     ");
                TotalSql.AppendLine("        ,isnull((select sum(e.TotalPrice*isnull(n.Rate,1)*n.Discount/100) from  officedba.SellOrderDetail as e  left join officedba.SellOrder as n on n.OrderNo=e.OrderNo and n.CompanyCD=e.CompanyCD where n.BillStatus!='1' and n.Status!='3' and n.CompanyCD=@CompanyCD and Month(n.OrderDate)>=@BeginNowMonth2 and Month(n.OrderDate)<=@EndNowMonth2 and YEAR(n.OrderDate)=@NowYear ");
                if (ProductID != "" && ProductID != "0")
                {
                    TotalSql.AppendLine("  and e.ProductID=@ProductID");

                }
                TotalSql.AppendLine("  ),0) as TotalPrice2       ");
                TotalSql.AppendLine(" 	     ,isnull((select sum(g.TotalPrice*isnull(x.Rate,1)*x.Discount/100) from  officedba.SellOrderDetail as g left join officedba.SellOrder as x on x.OrderNo=g.OrderNo and x.CompanyCD=g.CompanyCD where x.BillStatus!='1' and x.Status!='3' and x.CompanyCD=@CompanyCD and Month(x.OrderDate)>=@BeginNowMonth3 and Month(x.OrderDate)<=@EndNowMonth3 and Year(x.OrderDate)=@NowYear ");
                if (ProductID != "" && ProductID != "0")
                {
                    TotalSql.AppendLine("  and g.ProductID=@ProductID");

                }
                TotalSql.AppendLine("  ),0) as TotalPrice3     ");
                TotalSql.AppendLine("        ,isnull((select sum(i.TotalPrice*isnull(y.Rate,1)*y.Discount/100) from  officedba.SellOrderDetail as i  left join officedba.SellOrder as y on y.OrderNo=i.OrderNo and y.CompanyCD=i.CompanyCD where y.BillStatus!='1' and y.Status!='3' and y.CompanyCD=@CompanyCD and Month(y.OrderDate)>=@BeginNowMonth4 and Month(y.OrderDate)<=@EndNowMonth4 and YEAR(y.OrderDate)=@NowYear ");
                if (ProductID != "" && ProductID != "0")
                {
                    TotalSql.AppendLine("  and i.ProductID=@ProductID");

                }
                TotalSql.AppendLine("  ),0)  as TotalPrice4       ");
                TotalSql.AppendLine(" from   officedba.SellOrderDetail as a ");
                TotalSql.AppendLine("        left join officedba.SellOrder as c on a.OrderNo=c.OrderNo and a.CompanyCD=c.CompanyCD ");
                TotalSql.AppendLine(" where a.CompanyCD=@CompanyCD and c.BillStatus!='1' and c.Status!='3' ");
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));

                TotalComm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth", 01));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth", 03));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth2", 04));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth2", 06));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth3", 07));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth3", 09));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@BeginNowMonth4", 10));
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@EndNowMonth4", 12));
                TotalPrice = "0";
                TotalPrice1 = "0";
                TotalPrice2 = "0";
                TotalPrice3 = "0";
                TotalPrice4 = "0";
                if (ProductID != "" && ProductID != "0")
                {
                    TotalSql.AppendLine(" and a.ProductID=@ProductID");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
                }
                if (!string.IsNullOrEmpty(quarter))
                {

                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@NowYear", quarter));
                }
                else
                {
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@NowYear", DateTime.Now.Year));

                }
                TotalComm.CommandText = TotalSql.ToString();
                DataTable TotalDt = SqlHelper.ExecuteSearch(TotalComm);
                if (TotalDt.Rows.Count > 0)
                {
                    TotalPrice = TotalDt.Rows[0]["TotalPrice"].ToString();
                    TotalPrice1 = TotalDt.Rows[0]["TotalPrice1"].ToString();
                    TotalPrice2 = TotalDt.Rows[0]["TotalPrice2"].ToString();
                    TotalPrice3 = TotalDt.Rows[0]["TotalPrice3"].ToString();
                    TotalPrice4 = TotalDt.Rows[0]["TotalPrice4"].ToString();

                }
            }
            return dt;
        }
        #endregion

        #region 物品销售明细分析
        public static DataTable GetProBuyDetail(string Method, int pageIndex, int pageSize, string OrderBy, string CompanyCD, string Product, string BeginDate, string EndDate, ref string TotalProductCount, ref string TotalPrice, ref string TotalBackCount, ref string TotalBackPrice, ref int TotalCount)
        {

            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select isnull(t2.ProductID,t1.ProductID)as ProductID,isnull(t1.BackCount,0) as BackCount,isnull(t2.ProductCount,0)as ProductCount");
            sql.AppendLine(" ,isnull(t2.TotalPrice,0) as TotalPrice,                                                                                          ");
            sql.AppendLine(" isnull(t1.BackPrice,0)as BackPrice ,isnull(t3.ProductName,'') as ProductName ,isnull(t3.ProdNo,'')as ProNo                       ");
            sql.AppendLine(" from ( ");
            sql.AppendLine(" select c.ProductID,isnull(sum(c.ProductCount),0) as ProductCount,isnull(sum(c.TotalPrice*d.Rate*d.DisCount/100),0)as TotalPrice                      ");
            sql.AppendLine(" from officedba.SellOrderDetail as c left join officedba.SellOrder as d                                                           ");
            sql.AppendLine(" on c.OrderNo=d.OrderNo and c.CompanyCD=d.CompanyCD                                                                               ");
            sql.AppendLine(" where c.CompanyCD=@CompanyCD and d.BillStatus!='1' and d.Status!='3'                                                                ");
            if (Product != "" && Product != "0")
            {
                sql.AppendLine(" and c.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", Product));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and d.OrderDate>=@StartDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and d.OrderDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            sql.AppendLine(" group by c.ProductID)as t2                                                                                                       ");
            sql.AppendLine(" full join                                                                                                                        ");
            sql.AppendLine("   ( ");
            sql.AppendLine(" select a.ProductID,isnull(sum(a.BackNumber),0)as BackCount                                                                                 ");
            sql.AppendLine(" ,isnull(sum(a.TotalPrice*b.Rate*b.Discount/100),0) as BackPrice                                                                            ");
            sql.AppendLine(" from officedba.SellBackDetail as a                                                                                               ");
            sql.AppendLine(" left join officedba.SellBack as b on a.BackNo=b.BackNo and a.CompanyCD=b.CompanyCD                                               ");
            sql.AppendLine(" where b.BillStatus!='1'  and b.CompanyCD=@CompanyCD		                                                                              ");
            if (Product != "" && Product != "0")
            {
                sql.AppendLine(" and a.ProductID=@ProductID1");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID1", Product));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and b.BackDate>=@StartDate1");
                comm.Parameters.Add(SqlHelper.GetParameter("@StartDate1", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and b.BackDate<=@EndDate1");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate1", EndDate));
            }
            sql.AppendLine(" group by a.ProductID ");
            sql.AppendLine(" ) as t1 on t1.ProductID=t2.ProductID ");
            sql.AppendLine(" left join officedba.ProductInfo as t3 on isnull(t2.ProductID,t1.ProductID)=t3.ID ");
            if (Method == "1")
            {
                sql.AppendLine(" order by  " + OrderBy);
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            DataTable dt = new DataTable();
            if (Method == "0")
            {
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref TotalCount);
            }
            if (Method == "1")
            {
                dt = SqlHelper.ExecuteSearch(comm);
            }
            StringBuilder TotalSql = new StringBuilder();
            if (Method == "1")
            {
                SqlCommand TotalComm = new SqlCommand();
                TotalSql.AppendLine("select isnull(sum(t1.BackCount),0) as TotalBackCount,isnull(sum(t2.ProductCount),0)as TotalProductCount     ");
                TotalSql.AppendLine(",isnull(sum(t2.TotalPrice),0) as TotalPrice,                                                                ");
                TotalSql.AppendLine("isnull(sum(t1.BackPrice),0)as TotalBackPrice                                                                ");
                TotalSql.AppendLine("from (                                                                                                      ");
                TotalSql.AppendLine("select c.ProductID,sum(c.ProductCount) as ProductCount,sum(c.TotalPrice*d.Rate*d.DisCount/100)as TotalPrice ");
                TotalSql.AppendLine("from officedba.SellOrderDetail as c left join officedba.SellOrder as d                                      ");
                TotalSql.AppendLine("on c.OrderNo=d.OrderNo and c.CompanyCD=d.CompanyCD                                                          ");
                TotalSql.AppendLine("where c.CompanyCD=@CompanyCD and d.BillStatus!='1' and d.Status!='3'                                        ");
                if (Product != "" && Product != "0")
                {
                    TotalSql.AppendLine(" and c.ProductID=@ProductID");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@ProductID", Product));
                }
                if (!string.IsNullOrEmpty(BeginDate))
                {
                    TotalSql.AppendLine(" and d.OrderDate>=@StartDate");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@StartDate", BeginDate));
                }
                if (!string.IsNullOrEmpty(EndDate))
                {
                    TotalSql.AppendLine(" and d.OrderDate<=@EndDate");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
                }
                TotalSql.AppendLine("group by c.ProductID)as t2                                                                                  ");
                TotalSql.AppendLine("full join                                                                                                   ");
                TotalSql.AppendLine("  (                                                                                                         ");
                TotalSql.AppendLine("select a.ProductID,sum(a.BackNumber)as BackCount                                                            ");
                TotalSql.AppendLine(",sum(a.TotalPrice*b.Rate*b.Discount/100) as BackPrice                                                       ");
                TotalSql.AppendLine("from officedba.SellBackDetail as a                                                                          ");
                TotalSql.AppendLine("left join officedba.SellBack as b on a.BackNo=b.BackNo and a.CompanyCD=b.CompanyCD                          ");
                TotalSql.AppendLine("where b.BillStatus!='1' and b.CompanyCD='T0004'		                                                     ");
                if (Product != "" && Product != "0")
                {
                    TotalSql.AppendLine(" and a.ProductID=@ProductID1");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@ProductID1", Product));
                }
                if (!string.IsNullOrEmpty(BeginDate))
                {
                    TotalSql.AppendLine(" and b.BackDate>=@StartDate1");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@StartDate1", BeginDate));
                }
                if (!string.IsNullOrEmpty(EndDate))
                {
                    TotalSql.AppendLine(" and b.BackDate<=@EndDate1");
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@EndDate1", EndDate));
                }
                TotalSql.AppendLine("group by a.ProductID                                                                                        ");
                TotalSql.AppendLine(") as t1 on t1.ProductID=t2.ProductID                                                                        ");
                TotalSql.AppendLine("left join officedba.ProductInfo as t3 on t2.ProductID=t3.ID                                                 ");
                TotalComm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                TotalComm.CommandText = TotalSql.ToString();
                DataTable TotalDt = SqlHelper.ExecuteSearch(TotalComm);
                TotalProductCount = "0"; TotalPrice = "0"; TotalBackCount = "0"; TotalBackPrice = "0";
                if (TotalDt.Rows.Count > 0)
                {
                    TotalProductCount = TotalDt.Rows[0]["TotalProductCount"].ToString();
                    TotalPrice = TotalDt.Rows[0]["TotalPrice"].ToString();
                    TotalBackCount = TotalDt.Rows[0]["TotalBackCount"].ToString();
                    TotalBackPrice = TotalDt.Rows[0]["TotalBackPrice"].ToString();
                }
            }
            return dt;
        }
        #endregion

        #region 物品ABC分析
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Method"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ABCType">0所有A类B类C类</param>
        /// <param name="Product"></param>
        /// <param name="Column">1销售金额2销售数量</param>
        /// <param name="ValueA">A类物品值</param>
        /// <param name="ValueB">B类物品值</param>
        /// <param name="ValueC">C类物品值</param>
        /// <param name="Sift">过滤C类 1所有C类物品2有动销物品3无动销物品</param>
        /// <param name="TotalPrice"></param>
        /// <param name="TotalProductCount"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetABCPro(string Method, int pageIndex, int pageSize, string OrderBy, string CompanyCD, string BeginDate, string EndDate, string ABCType, string Product, string Column, string ValueA, string ValueB, string ValueC, string Sift, ref string TotalPrice, ref string TotalProductCount, ref int TotalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select ID,ABCType,ProductName ");
            if (Sift != "3") //无动销物品没有销售类的检索
            {
                sql.AppendLine(" ,ProductCount,TotalPrice ");
            }
            else
            {
                sql.AppendLine(",0 as ProductCount,0 as TotalPrice");
            }
            sql.AppendLine(" from ( ");
            sql.AppendLine("         select  distinct isnull(a.ProductID,0) as  ID,isnull(b.ProductName,'') as ProductName,");
            sql.AppendLine("         isnull(b.ABCType,'') as ABCType ");
            if (Sift != "3")
            {
                sql.AppendLine("         ,(select isnull(sum(a1.TotalPrice*isnull(c1.Rate,1)*c1.Discount/100),0) from   officedba.SellOrderDetail as a1 left join officedba.ProductInfo as b1 on a1.ProductID=b1.ID        ");
                sql.AppendLine("         left join officedba.SellOrder as c1 on a1.OrderNo=c1.OrderNo and a1.CompanyCD=c1.CompanyCD where a1.ProductID=b.ID and c1.CompanyCD=@CompanyCD and c1.BillStatus!='1' and c1.Status!='3' ");
                if (!string.IsNullOrEmpty(BeginDate))
                {
                    sql.AppendLine("    and c1.OrderDate>=@BeginDate ");
                    comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
                }
                if (!string.IsNullOrEmpty(EndDate))
                {
                    sql.AppendLine("  and   c1.OrderDate<=@EndDate ");
                    comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
                }
                sql.AppendLine(" ) as TotalPrice   ");
                sql.AppendLine("         ,(select isnull(sum(a2.ProductCount),0) from officedba.SellOrderDetail as a2 left join officedba.ProductInfo as b2 on a2.ProductID=b2.ID        ");

                sql.AppendLine("         left join officedba.SellOrder as c2 on a2.OrderNo=c2.OrderNo and a2.CompanyCD=c2.CompanyCD where a2.ProductID=b.ID and c2.CompanyCD=@CompanyCD and c2.BillStatus!='1' and c2.Status!='3' ");
                if (!string.IsNullOrEmpty(BeginDate))
                {
                    sql.AppendLine("  and   c2.OrderDate>=@BeginDate2 ");
                    comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate2", BeginDate));
                }
                if (!string.IsNullOrEmpty(EndDate))
                {
                    sql.AppendLine("   and  c2.OrderDate<=@EndDate ");
                    comm.Parameters.Add(SqlHelper.GetParameter("@EndDate2", EndDate));
                }
                sql.AppendLine(" ) as ProductCount ");
                if (Column == "1")  //根据销售额
                {
                    if (!string.IsNullOrEmpty(ValueA))
                    {
                        sql.AppendLine("         ,((select isnull(sum(aa.TotalPrice*isnull(ca.Rate,1)*ca.Discount/100),0) from officedba.SellOrderDetail as aa left join officedba.ProductInfo as ba on aa.ProductID=ba.ID  ");
                        sql.AppendLine("         left join officedba.SellOrder as ca on aa.OrderNo=ca.OrderNo and ca.CompanyCD=aa.CompanyCD where  ca.Status!='1' and ca.Status!='3' and aa.CompanyCD=@CompanyCD and aa.ProductID=b.ID and ba.ABCType='A'  )/(select sum(a3.TotalPrice*c3.Rate*c3.Discount/100) ");
                        sql.AppendLine("         from officedba.SellOrderDetail as a3         ");
                        sql.AppendLine("         left join officedba.SellOrder as c3 on a3.OrderNo=c3.OrderNo and a3.CompanyCD=c3.CompanyCD where c3.BillStatus!='1' and c3.Status!='3' and  a3.CompanyCD=@CompanyCD )) as APrice ");
                    }
                    if (!string.IsNullOrEmpty(ValueB))
                    {
                        sql.AppendLine("         ,((select isnull(sum(ab.TotalPrice*cb.Rate*cb.Discount/100),0) from officedba.SellOrderDetail as ab left join officedba.ProductInfo as bb on ab.ProductID=bb.ID  ");
                        sql.AppendLine("         left join officedba.SellOrder as cb on ab.OrderNo=cb.OrderNo and ab.CompanyCD=cb.CompanyCD where cb.Status!='3' and cb.BillStatus!='1' and  ab.ProductID=b.ID and bb.ABCType='B' and ab.CompanyCD=@CompanyCD )/(select sum(a4.TotalPrice*c4.Rate*c4.Discount/100)  ");
                        sql.AppendLine("         from officedba.SellOrderDetail as a4       ");
                        sql.AppendLine("         left join officedba.SellOrder as c4 on a4.OrderNo=c4.OrderNo and a4.CompanyCD=c4.CompanyCD where c4.BillStatus!='1' and c4.Status!='3' and a4.CompanyCD=@CompanyCD )) as BPrice ");
                    }
                    if (!string.IsNullOrEmpty(ValueC))
                    {
                        sql.AppendLine("         ,((select isnull(sum(ac.TotalPrice*cc.Rate*cc.Discount/100),0) from officedba.SellOrderDetail as ac left join officedba.ProductInfo as bc on ac.ProductID=bc.ID  ");
                        sql.AppendLine("         left join officedba.SellOrder as cc on ac.OrderNo=cc.OrderNo and ac.CompanyCD=cc.CompanyCD where cc.BillStatus!='1' and cc.Status!='3' and ac.ProductID=b.ID and bc.ABCType='C' and ac.CompanyCD=@CompanyCD )/(select sum(a5.TotalPrice*c5.Rate*c5.Discount/100)  ");
                        sql.AppendLine("         from officedba.SellOrderDetail as a5         ");
                        sql.AppendLine("         left join officedba.SellOrder as c5 on a5.OrderNo=c5.OrderNo and a5.CompanyCD=c5.CompanyCD where c5.BillStatus!='1' and c5.Status!='3' and a5.CompanyCD=@CompanyCD )) as CPrice ");
                    }
                }
                else             //根据销售数量
                {
                    if (!string.IsNullOrEmpty(ValueA))
                    {
                        sql.AppendLine("         ,((select isnull(sum(aa.ProductCount),0) from officedba.SellOrderDetail as aa left join officedba.ProductInfo as ba on aa.ProductID=ba.ID  ");
                        sql.AppendLine("         left join officedba.SellOrder as ca on aa.OrderNo=ca.OrderNo and ca.CompanyCD=aa.CompanyCD where ca.BillStatus!='1' and ca.Status!='3' and aa.ProductID=b.ID and ba.ABCType='A' and aa.CompanyCD=@CompanyCD)/(select sum(a3.ProductCount) ");
                        sql.AppendLine("         from officedba.SellOrderDetail as a3        ");
                        sql.AppendLine("         left join officedba.SellOrder as c3 on a3.OrderNo=c3.OrderNo and a3.CompanyCD=c3.CompanyCD where c3.BillStatus!='1' and c3.Status!='3' and  a3.CompanyCD=@CompanyCD)) as APrice ");
                    }
                    if (!string.IsNullOrEmpty(ValueB))
                    {
                        sql.AppendLine("         ,((select isnull(sum(ab.ProductCount),0) from officedba.SellOrderDetail as ab left join officedba.ProductInfo as bb on ab.ProductID=bb.ID  ");
                        sql.AppendLine("         left join officedba.SellOrder as cb on ab.OrderNo=cb.OrderNo and cb.CompanyCD=ab.CompanyCD where cb.Status!='3' and cb.BillStatus!='1' and ab.ProductID=b.ID and bb.ABCType='B' and ab.CompanyCD=@CompanyCD)/(select sum(a4.ProductCount)  ");
                        sql.AppendLine("         from officedba.SellOrderDetail as a4     ");
                        sql.AppendLine("         left join officedba.SellOrder as c4 on a4.OrderNo=c4.OrderNo and a4.CompanyCD=c4.CompanyCD where c4.BillStatus!='1' and c4.Status!='3' and a4.CompanyCD=@CompanyCD)) as BPrice ");
                    }
                    if (!string.IsNullOrEmpty(ValueC))
                    {
                        sql.AppendLine("         ,((select isnull(sum(ac.ProductCount),0) from officedba.SellOrderDetail as ac left join officedba.ProductInfo as bc on ac.ProductID=bc.ID  ");
                        sql.AppendLine("         left join officedba.SellOrder as cc on ac.OrderNo=cc.OrderNo and ac.CompanyCD=cc.CompanyCD where cc.BillStatus!='1' and cc.Status!='3' and ac.ProductID=b.ID and bc.ABCType='C' and ac.CompanyCD=@CompanyCD)/(select sum(a5.ProductCount)  ");
                        sql.AppendLine("         from officedba.SellOrderDetail as a5       ");
                        sql.AppendLine("         left join officedba.SellOrder as c5 on a5.OrderNo=c5.OrderNo and a5.CompanyCD=c5.CompanyCD where c5.BillStatus!='1' and c5.Status!='3' AND  a5.CompanyCD=@CompanyCD)) as CPrice ");
                    }
                }
            }
            if (Sift == "3")
            {
                sql.AppendLine(" from    officedba.ProductInfo  as b left join officedba.SellOrderDetail as a on a.ProductID=b.ID ");
            }
            else
            {
                sql.AppendLine(" from    officedba.SellOrderDetail as a left join officedba.ProductInfo as b on a.ProductID=b.ID ");
            }
            sql.AppendLine("         left join officedba.SellOrder as c on a.OrderNo=c.OrderNo and a.CompanyCD=c.CompanyCD ");
            sql.AppendLine(" where b.CompanyCD=@CompanyCD  and c.BillStatus!='1'  and c.Status!='3' ");
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and c.OrderDate>=@BeginDatee");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDatee",BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and c.OrderDate<=@EndDatee");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDatee",EndDate));
            }
            if (ABCType != "" && ABCType != "0") //ABC类别
            {
                sql.AppendLine(" and b.ABCType=@ABCType");
                comm.Parameters.Add(SqlHelper.GetParameter("@ABCType", ABCType));
            }
            if (Product != "" && Product != "0") //物品 
            {
                sql.AppendLine(" and a.ProductID=@ProductID ");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", Product));
            }
            if (Sift != "" && Sift != "0")
            {
                if (Sift == "1") //Sift过滤值:1对所C类物品2有动销物品3未动销物品
                {
                    sql.AppendLine(" and b.ABCType='C'");
                }
                else
                {
                    if (Sift == "2")
                    {
                        sql.AppendLine(" and a.ProductID ");
                        sql.AppendLine(" in ");
                        sql.AppendLine(" (select Sifta.ProductID from officedba.SellOrderDetail as Sifta left join officedba.ProductInfo as Siftb on Sifta.ProductID=Siftb.ID ");
                        sql.AppendLine(" left join officedba.SellOrder as Siftc on Sifta.OrderNo=Siftc.OrderNo and Sifta.CompanyCD=Siftc.CompanyCD where Sifta.CompanyCD=@CompanyCD and Siftc.BillStatus!='1' and Siftc.Status!='3')");
                    }
                }
            }
            sql.AppendLine("         group by a.ProductID,b.ID,b.ProductName,b.ABCType ");
            sql.AppendLine(" ) as Info  where 1=1");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            if (Sift != "3")
            {
                if (!string.IsNullOrEmpty(ValueA))
                {
                    sql.AppendLine(" and round(APrice,0)=@ValueA");
                    comm.Parameters.Add(SqlHelper.GetParameter("@ValueA", ValueA));
                }
                if (!string.IsNullOrEmpty(ValueB))
                {
                    sql.AppendLine(" and round(BPrice,0)=@ValueB");
                    comm.Parameters.Add(SqlHelper.GetParameter("@ValueB", ValueB));
                }
                if (!string.IsNullOrEmpty(ValueC))
                {
                    sql.AppendLine(" and round(CPrice,0)=@ValueC");
                    comm.Parameters.Add(SqlHelper.GetParameter("@ValueC", ValueC));
                }
            }
            if (Method == "1")
            {
                sql.AppendLine(" order by " + OrderBy);
            }
            comm.CommandText = sql.ToString();
            DataTable dt = new DataTable();
            if (Method == "0")
            {
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref TotalCount);
            }
            if (Method == "1")
            {
                dt = SqlHelper.ExecuteSearch(comm);
            }
            if (Method == "1")
            {
                #region ------------------------------------------合计信息

                if (Sift != "3" && Sift != "0")
                {
                    StringBuilder TotalSql = new StringBuilder();
                    SqlCommand TotalComm = new SqlCommand();
                    TotalSql.AppendLine("select TotalPrice,TotalProductCount from (");
                    TotalSql.AppendLine(" select  isnull(sum(a.TotalPrice*c.Rate*c.Discount/100),0)as TotalPrice,isnull(sum(a.ProductCount),0) as TotalProductCount ");
                    if (Column == "1")  //根据销售额
                    {
                        if (!string.IsNullOrEmpty(ValueA))
                        {
                            TotalSql.AppendLine("         ,isnull(((select isnull(sum(aa.TotalPrice*isnull(ca.Rate,1)*ca.Discount/100),0) from officedba.SellOrderDetail as aa  join officedba.ProductInfo as ba on aa.ProductID=ba.ID  ");
                            TotalSql.AppendLine("         left join officedba.SellOrder as ca on aa.OrderNo=ca.OrderNo and aa.CompanyCD=ca.CompanyCD where ca.BillStatus!='1' and ca.Status!='3' and  aa.ProductID=b.ID and ba.ABCType='A')/(select sum(a3.TotalPrice*c3.Rate*c3.Discount/100) ");
                            TotalSql.AppendLine("         from officedba.SellOrderDetail as a3     ");
                            TotalSql.AppendLine("         left join officedba.SellOrder as c3 on a3.OrderNo=c3.OrderNo and a3.CompanyCD=c3.CompanyCD where c3.BillStatus!='1' and c3.Status!='3' a3.CompanyCD=@CompanyCD)),0) as APrice ");
                        }
                        if (!string.IsNullOrEmpty(ValueB))
                        {
                            TotalSql.AppendLine("         ,isnull(((select isnull(sum(ab.TotalPrice*isnull(cb.Rate,1)*cb.Discount/100),0) from officedba.SellOrderDetail as ab left join officedba.ProductInfo as bb on ab.ProductID=bb.ID  ");
                            TotalSql.AppendLine("         left join officedba.SellOrder as cb on ab.OrderNo=cb.OrderNo and ab.CompanyCD=cb.CompanyCD where cb.BillStatus!='1' and cb.Status!=='3' and ab.ProductID=b.ID and bb.ABCType='B' )/(select sum(a4.TotalPrice*c4.Rate*c4.Discount/100)  ");
                            TotalSql.AppendLine("         from officedba.SellOrderDetail as a4      ");
                            TotalSql.AppendLine("         left join officedba.SellOrder as c4 on a4.OrderNo=c4.OrderNo and a4.CompanyCD=c4.CompanyCD where c4.BillStatus!='1' and c4.Status!='3' and a4.CompanyCD=@CompanyCD)),0) as BPrice ");
                        }
                        if (!string.IsNullOrEmpty(ValueC))
                        {
                            TotalSql.AppendLine("         ,isnull(((select isnull(sum(ac.TotalPrice*isnull(cc.Rate,1)*cc.Discount/100),0) from officedba.SellOrderDetail as ac left join officedba.ProductInfo as bc on ac.ProductID=bc.ID  ");
                            TotalSql.AppendLine("         left join officedba.SellOrder as cc on ac.OrderNo=cc.OrderNo and ac.CompanyCD=cc.CompanyCD where cc.BillStatus!='1' and cc.Status!='3' and ac.ProductID=b.ID and bc.ABCType='C' )/(select sum(a5.TotalPrice*c5.Rate*c5.Discount/100)  ");
                            TotalSql.AppendLine("         from officedba.SellOrderDetail as a5     ");
                            TotalSql.AppendLine("         left join officedba.SellOrder as c5 on a5.OrderNo=c5.OrderNo and c5.CompanyCD=a5.CompanyCD where c5.BillStatus!='1' and c5.Status!='3' and a5.CompanyCD=@CompanyCD)),0) as CPrice ");
                        }
                    }
                    else             //根据销售数量
                    {
                        if (!string.IsNullOrEmpty(ValueA))
                        {
                            TotalSql.AppendLine("         ,isnull(((select isnull(sum(aa.ProductCount),0) from officedba.SellOrderDetail as aa left join officedba.ProductInfo as ba on aa.ProductID=ba.ID  ");
                            TotalSql.AppendLine("         left join officedba.SellOrder as ca on aa.OrderNo=ca.OrderNo and ca.CompanyCD=aa.CompanyCD where ca.BillStatus!='1' and ca.Status!='3' and  aa.ProductID=b.ID and ba.ABCType='A' and aa.CompanyCD=@CompanyCD)/(select sum(a3.ProductCount) ");
                            TotalSql.AppendLine("         from officedba.SellOrderDetail as a3   ");
                            TotalSql.AppendLine("         left join officedba.SellOrder as c3 on a3.OrderNo=c3.OrderNo and c3.CompanyCD=a3.CompanyCD where c3.BillStatus!='1' and c3.Status!='3' and  c3.CompanyCD=@CompanyCD)),0) as APrice ");
                        }
                        if (!string.IsNullOrEmpty(ValueB))
                        {
                            TotalSql.AppendLine("         ,isnull(((select isnull(sum(ab.ProductCount),0) from officedba.SellOrderDetail as ab left join officedba.ProductInfo as bb on ab.ProductID=bb.ID  ");
                            TotalSql.AppendLine("         left join officedba.SellOrder as cb on ab.OrderNo=cb.OrderNo and ab.CompanyCD=cb.CompanyCD where cb.BillStatus!='1' and cb.Status!='3' and  ab.ProductID=b.ID and bb.ABCType='B' and cb.CompanyCD=@CompanyCD)/(select sum(a4.ProductCount)  ");
                            TotalSql.AppendLine("         from officedba.SellOrderDetail as a4    ");
                            TotalSql.AppendLine("         left join officedba.SellOrder as c4 on a4.OrderNo=c4.OrderNo and a4.CompanyCD=c4.CompanyCD where c4.BillStatus!='1' and c4.Status!='3' and c4.CompanyCD=@CompanyCD)),0) as BPrice ");
                        }
                        if (!string.IsNullOrEmpty(ValueC))
                        {
                            TotalSql.AppendLine("         ,isnull(((select isnull(sum(ac.ProductCount),0) from officedba.SellOrderDetail as ac left join officedba.ProductInfo as bc on ac.ProductID=bc.ID  ");
                            TotalSql.AppendLine("         left join officedba.SellOrder as cc on ac.OrderNo=cc.OrderNo and ac.CompanyCD=cc.CompanyCD where cc.BillStatus!='1' and cc.Status!='3' and ac.ProductID=b.ID and bc.ABCType='C' and cc.CompanyCD=@CompanyCD)/(select sum(a5.ProductCount)  ");
                            TotalSql.AppendLine("         from officedba.SellOrderDetail as a5    ");
                            TotalSql.AppendLine("         left join officedba.SellOrder as c5 on a5.OrderNo=c5.OrderNo and c5.CompanyCD=a5.CompanyCD where c5.BillStatus!='1' and c5.Status!='3' and c5.CompanyCD=@CompanyCD)),0) as CPrice ");
                        }
                    }
                    TotalSql.AppendLine(" from    officedba.SellOrderDetail as a left join officedba.ProductInfo as b on a.ProductID=b.ID     ");
                    TotalSql.AppendLine("         left join officedba.SellOrder as c on a.OrderNo=c.OrderNo   and c.CompanyCD=a.CompanyCD                       ");
                    TotalSql.AppendLine(" where a.CompanyCD=@CompanyCD  and c.BillStatus!='1' and c.Status!='3'");

                    if (!string.IsNullOrEmpty(BeginDate))
                    {
                        TotalSql.AppendLine(" and c.OrderDate>=@BeginDate");
                        TotalComm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
                    }
                    if (!string.IsNullOrEmpty(EndDate))
                    {
                        TotalSql.AppendLine(" and c.OrderDate<=@EndDate");
                        TotalComm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
                    }
                    if (ABCType != "" && ABCType != "0") //ABC类别
                    {
                        TotalSql.AppendLine(" and b.ABCType=@ABCType");
                        TotalComm.Parameters.Add(SqlHelper.GetParameter("@ABCType", ABCType));
                    }
                    //TotalSql += " group by b.ID";
                    TotalSql.AppendLine(" ) as TotalInfo where 1=1 ");
                    if (!string.IsNullOrEmpty(ValueA))
                    {
                        TotalSql.AppendLine(" and round(APrice,0)=@ValueA");
                        TotalComm.Parameters.Add(SqlHelper.GetParameter("@ValueA", ValueA));
                    }
                    if (!string.IsNullOrEmpty(ValueB))
                    {
                        TotalSql.AppendLine(" and round(BPrice,0)=@ValueB");
                        TotalComm.Parameters.Add(SqlHelper.GetParameter("@ValueB", ValueB));
                    }
                    if (!string.IsNullOrEmpty(ValueC))
                    {
                        TotalSql.AppendLine(" and round(CPrice,0)=@ValueC");
                        TotalComm.Parameters.Add(SqlHelper.GetParameter("@ValueC", ValueC));
                    }
                    TotalComm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    TotalComm.CommandText = TotalSql.ToString();
                    DataTable TotalDt = SqlHelper.ExecuteSearch(TotalComm);
                    TotalProductCount = "0";
                    TotalPrice = "0";
                    if (TotalDt.Rows.Count > 0)
                    {
                        TotalProductCount = TotalDt.Rows[0]["TotalProductCount"].ToString();
                        TotalPrice = TotalDt.Rows[0]["TotalPrice"].ToString();
                    }


                }
                #endregion
            }
            return dt;

        }
        #endregion

        #region 销售价格比较
        public static DataTable GetSellPrice(string QueryStr, string CompanyCD, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT  T1.*,isnull(c.ProdNo,'') ProNo,isnull(c.ProductName,'') as ProductName,isnull(d.CustName,'') as CustName from  ");
            sql.AppendLine("        (select isnull(SUM(a.TotalFee*ISNULL(b.Rate,1)*b.Discount/100),0) AS TotalFee ");
            sql.AppendLine("        ,isnull(SUM(a.ProductCount),0) AS ProductCount,a.ProductID,b.CustID  from     ");
            sql.AppendLine("        officedba.SellOrderDetail AS a LEFT  JOIN                                     ");
            sql.AppendLine("        officedba.SellOrder AS b ON a.OrderNo = b.OrderNo                             ");
            sql.AppendLine("  WHERE  b.BillStatus <> '1' AND b.Status <> '3' and   a.CompanyCD=@CompanyCD and    ");
            sql.AppendLine(" {0} ");
            sql.AppendLine(" GROUP BY a.ProductID,b.CustID                             ");
            sql.AppendLine("        ) as T1  LEFT  JOIN                                                           ");
            sql.AppendLine("        officedba.ProductInfo AS c ON c.ID = T1.ProductID LEFT  JOIN                  ");
            sql.AppendLine("        officedba.CustInfo AS d ON d.ID = T1.CustID                                   ");
            string selectSQL = string.Format(sql.ToString(), QueryStr.Trim().Length > 0 ? QueryStr : "");

            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = selectSQL;
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);

        }
        public static DataTable GetSellPrice(string QueryStr, string CompanyCD, string OrderBy)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT  T1.*,isnull(c.ProdNo,'') Title,isnull(c.ProductName,'') as OrderNo,isnull(d.CustName,'') as FromType from  ");
            sql.AppendLine("        (select isnull(SUM(a.TotalFee*ISNULL(b.Rate,1)*b.Discount/100),0) AS TotalFee ");
            sql.AppendLine("        ,isnull(SUM(a.ProductCount),0) AS Tax,a.ProductID,b.CustID as CustID1  from     ");
            sql.AppendLine("        officedba.SellOrderDetail AS a LEFT  JOIN                                     ");
            sql.AppendLine("        officedba.SellOrder AS b ON a.OrderNo = b.OrderNo                             ");
            sql.AppendLine("  WHERE  b.BillStatus <> '1' AND b.Status <> '3' and   a.CompanyCD=@CompanyCD and    ");
            sql.AppendLine(" {0} ");
            sql.AppendLine(" GROUP BY a.ProductID,b.CustID                             ");
            sql.AppendLine("        ) as T1  LEFT  JOIN                                                           ");
            sql.AppendLine("        officedba.ProductInfo AS c ON c.ID = T1.ProductID LEFT  JOIN                  ");
            sql.AppendLine("        officedba.CustInfo AS d ON d.ID = T1.CustID1                                   ");

            sql.AppendLine(OrderBy);
            string selectSQL = string.Format(sql.ToString(), QueryStr.Trim().Length > 0 ? QueryStr : "");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = selectSQL;
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        #region  销售区业绩分析
        public static DataTable GetSellAchievementGrowing(string CodeFalg, string CodeType, string QueryStr, string CompanyCD, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  top 3  ISNULL(c.TypeName, '') AS TypeName, SUM(a.TotalPrice * ISNULL(a.Rate, 1) * a.Discount / 100) AS TotalPrice ");
            sql.AppendLine("FROM         officedba.SellOrder AS a LEFT OUTER JOIN                                                                ");
            sql.AppendLine("             officedba.CustInfo AS b ON a.CustID = b.ID LEFT OUTER JOIN                                              ");
            sql.AppendLine("             officedba.CodePublicType AS c ON b.AreaID = c.ID                                                        ");
            sql.AppendLine("WHERE  a.CompanyCD=@CompanyCD and a.BillStatus <> '1' AND a.Status <> '3' and c.TypeFlag=@TypeFlag and c.TypeCode=@TypeCode and           ");
            sql.AppendLine(" {0} ");
            sql.AppendLine("group by  c.TypeName                                                                                                 ");
            sql.AppendLine("ORDER BY TotalPrice DESC                                                                                             ");
            string selectSQL = string.Format(sql.ToString(), QueryStr.Trim().Length > 0 ? QueryStr : "");

            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@TypeFlag", CodeFalg));
            comm.Parameters.Add(SqlHelper.GetParameter("@TypeCode", CodeType));
            comm.CommandText = selectSQL.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);

        }
        public static DataTable GetSellAchievementGrowingPrint(string CodeFalg, string CodeType, string QueryStr, string CompanyCD, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  top 3  ISNULL(c.TypeName, '') AS Title, SUM(a.TotalPrice * ISNULL(a.Rate, 1) * a.Discount / 100) AS TotalPrice ");
            sql.AppendLine("FROM         officedba.SellOrder AS a LEFT OUTER JOIN                                                                ");
            sql.AppendLine("             officedba.CustInfo AS b ON a.CustID = b.ID LEFT OUTER JOIN                                              ");
            sql.AppendLine("             officedba.CodePublicType AS c ON b.AreaID = c.ID                                                        ");
            sql.AppendLine("WHERE  a.CompanyCD=@CompanyCD and a.BillStatus <> '1' AND a.Status <> '3' and c.TypeFlag=@TypeFlag and c.TypeCode=@TypeCode and           ");
            sql.AppendLine(" {0} ");
            sql.AppendLine("group by  c.TypeName                                                                                                 ");
            sql.AppendLine("ORDER BY TotalPrice DESC                                                                                             ");
            string selectSQL = string.Format(sql.ToString(), QueryStr.Trim().Length > 0 ? QueryStr : "");

            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@TypeFlag", CodeFalg));
            comm.Parameters.Add(SqlHelper.GetParameter("@TypeCode", CodeType));
            comm.CommandText = selectSQL;
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);

        }
        public static DataTable GetArea(string CompanyCD, string CodeFalg, string CodeType)
        {
            SqlCommand comm = new SqlCommand();
            string sql = "select TypeName,ID from officedba.CodePublicType where UsedStatus='1' and CompanyCD=@CompanyCD and TypeFlag=@CodeFalg and TypeCode=@CodeType ";
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@CodeFalg", CodeFalg));
            comm.Parameters.Add(SqlHelper.GetParameter("@CodeType", CodeType));
            comm.CommandText = sql;
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 同期比较分析
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Method">0为列表1为打印页需要</param>
        /// <param name="CompanyCD"></param>
        /// <param name="Compare1">比较项目：1物品2客户3部门4业务员</param>
        /// <param name="Compare2">比较对象：1销售额2销售数量</param>
        /// <param name="CurrencyID">币种</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetSameTerm(string Method, string CompanyCD, string Compare1, string Compare2, string CurrencyID, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder samesql = new StringBuilder();
            StringBuilder sql = new StringBuilder();
            #region 按照物品比较
            if (Compare1 == "1")
            {
                if (Compare2 == "1") //销售额
                {
                    if (CurrencyID != "0" && CurrencyID != "")
                    {
                        samesql.AppendLine("  select a.ProductID as ID,isnull(sum(a.TotalPrice*b.Discount/100),0) as {0} ");
                    }
                    else
                    {
                        samesql.AppendLine("  select a.ProductID as ID,isnull(sum(a.TotalPrice*b.Rate*b.Discount/100),0) as {0} ");
                    }
                }
                else
                {
                    samesql.AppendLine("  select a.ProductID as ID,isnull(sum(a.ProductCount),0)  as {0} ");
                }
                samesql.AppendLine(" from   officedba.SellOrderDetail as a left join officedba.SellOrder as b on a.OrderNo=b.OrderNo  and a.CompanyCD=b.CompanyCD");
                samesql.AppendLine(" where  a.CompanyCD=@CompanyCD and b.BillStatus<>'1' and b.Status<>'3' {1}");
                if (CurrencyID != "0" && CurrencyID != "")
                {
                    samesql.AppendLine(" and b.CurrencyType=@CurrencyType ");
                }
                samesql.AppendLine(" group by a.ProductID ");

                sql.AppendLine(" select isnull(ProductName,'') as Title,isnull(ProdNO,'') as FromType,p.ID ");//Title替代ProductName   FromType替代ProdNO
                sql.AppendLine(" ,isnull(TotalPrice,0) as TotalPrice");
                sql.AppendLine(" ,isnull(Tax,0) as Tax");
                sql.AppendLine(" ,isnull(TotalFee,0) as TotalFee");
                sql.AppendLine(" ,isnull(RealTotal,0) as RealTotal");
                sql.AppendLine(" from officedba.ProductInfo as p ");
                sql.AppendLine(" left join (");
                sql.AppendLine(String.Format(samesql.ToString(), "TotalPrice", " and Month(b.OrderDate)=Month(getdate()) and Year(b.OrderDate)=Year(getdate()) "));// 本期
                sql.AppendLine(" ) as T1 on T1.ID=p.ID  left join (");
                sql.AppendLine(String.Format(samesql.ToString(), "Tax", " and Month(b.OrderDate)=Month(getdate()) and Year(b.OrderDate)=Year(b.OrderDate)-1"));//去年同期
                sql.AppendLine(" ) as T2 on T2.ID=p.ID left join (");
                sql.AppendLine(String.Format(samesql.ToString(), "TotalFee", " and Year(b.OrderDate)=Year(getdate())"));//本年
                sql.AppendLine(" ) as T3 on T3.ID=p.ID left join ( ");
                sql.AppendLine(String.Format(samesql.ToString(), "RealTotal", " and Year(b.OrderDate)=Year(getdate())-1"));//去年
                sql.AppendLine(" ) as T4 on T4.ID=p.ID ");
                sql.AppendLine(" where p.CompanyCD=@CompanyCD and p.UsedStatus='1' ");
                if (Method == "1")
                {
                    sql.AppendLine(OrderBy);
                }
            }
            #endregion

            #region 按照客户比较
            if (Compare1 == "2")//
            {
                if (Compare2 == "1") //销售额
                {
                    if (CurrencyID != "0" && CurrencyID != "")
                    {
                        samesql.AppendLine("  select b.CustID as ID,isnull(sum(a.TotalPrice*b.Discount/100),0) as {0} ");
                    }
                    else
                    {
                        samesql.AppendLine("  select b.CustID as ID,isnull(sum(a.TotalPrice*b.Rate*b.Discount/100),0) as {0} ");
                    }
                }
                else
                {
                    samesql.AppendLine("  select b.CustID as ID,isnull(sum(a.ProductCount),0)  as {0} ");
                }
                samesql.AppendLine(" from   officedba.SellOrderDetail as a left join officedba.SellOrder as b on a.OrderNo=b.OrderNo  and a.CompanyCD=b.CompanyCD");
                samesql.AppendLine(" where  a.CompanyCD=@CompanyCD and b.BillStatus<>'1' and b.Status<>'3' {1}");
                if (CurrencyID != "0" && CurrencyID != "")
                {
                    samesql.AppendLine(" and b.CurrencyType=@CurrencyType ");
                }
                samesql.AppendLine(" group by b.CustID ");

                sql.AppendLine(" select isnull(CustName,'') as Title,p.ID  ");//Title替代CustName
                sql.AppendLine(" ,isnull(TotalPrice,0) as TotalPrice");
                sql.AppendLine(" ,isnull(Tax,0) as Tax");
                sql.AppendLine(" ,isnull(TotalFee,0) as TotalFee");
                sql.AppendLine(" ,isnull(RealTotal,0) as RealTotal");
                sql.AppendLine(" from officedba.CustInfo as p ");
                sql.AppendLine(" left join (");
                sql.AppendLine(String.Format(samesql.ToString(), "TotalPrice", " and Month(b.OrderDate)=Month(getdate()) and Year(b.OrderDate)=Year(getdate()) "));// 本期
                sql.AppendLine(" ) as T1 on T1.ID=p.ID  left join (");
                sql.AppendLine(String.Format(samesql.ToString(), "Tax", " and Month(b.OrderDate)=Month(getdate()) and Year(b.OrderDate)=Year(b.OrderDate)-1"));//去年同期
                sql.AppendLine(" ) as T2 on T2.ID=p.ID left join (");
                sql.AppendLine(String.Format(samesql.ToString(), "TotalFee", " and Year(b.OrderDate)=Year(getdate())"));//本年
                sql.AppendLine(" ) as T3 on T3.ID=p.ID left join ( ");
                sql.AppendLine(String.Format(samesql.ToString(), "RealTotal", " and Year(b.OrderDate)=Year(getdate())-1"));//去年
                sql.AppendLine(" ) as T4 on T4.ID=p.ID ");
                sql.AppendLine(" where p.CompanyCD=@CompanyCD and p.UsedStatus='1' ");
                if (Method == "1")
                {
                    sql.AppendLine(OrderBy);
                }
            }
            #endregion

            #region 按照部门比较
            if (Compare1 == "3")//
            {
                if (Compare2 == "1") //销售额
                {
                    if (CurrencyID != "0" && CurrencyID != "")
                    {
                        samesql.AppendLine("  select b.SellDeptId as ID,isnull(sum(a.TotalPrice*b.Discount/100),0) as {0} ");
                    }
                    else
                    {
                        samesql.AppendLine("  select b.SellDeptId as ID,isnull(sum(a.TotalPrice*b.Rate*b.Discount/100),0) as {0} ");
                    }
                }
                else
                {
                    samesql.AppendLine("  select b.SellDeptId as ID,isnull(sum(a.ProductCount),0)  as {0} ");
                }
                samesql.AppendLine(" from   officedba.SellOrderDetail as a left join officedba.SellOrder as b on a.OrderNo=b.OrderNo  and a.CompanyCD=b.CompanyCD");
                samesql.AppendLine(" where  a.CompanyCD=@CompanyCD and b.BillStatus<>'1' and b.Status<>'3' {1}");
                if (CurrencyID != "0" && CurrencyID != "")
                {
                    samesql.AppendLine(" and b.CurrencyType=@CurrencyType ");
                }
                samesql.AppendLine(" group by b.SellDeptId ");

                sql.AppendLine(" select isnull(DeptName,'') as Title,p.ID ");//Title替代DeptName   
                sql.AppendLine(" ,isnull(TotalPrice,0) as TotalPrice");
                sql.AppendLine(" ,isnull(Tax,0) as Tax");
                sql.AppendLine(" ,isnull(TotalFee,0) as TotalFee");
                sql.AppendLine(" ,isnull(RealTotal,0) as RealTotal");
                sql.AppendLine(" from officedba.DeptInfo as p ");
                sql.AppendLine(" left join (");
                sql.AppendLine(String.Format(samesql.ToString(), "TotalPrice", " and Month(b.OrderDate)=Month(getdate()) and Year(b.OrderDate)=Year(getdate()) "));// 本期
                sql.AppendLine(" ) as T1 on T1.ID=p.ID  left join (");
                sql.AppendLine(String.Format(samesql.ToString(), "Tax", " and Month(b.OrderDate)=Month(getdate()) and Year(b.OrderDate)=Year(b.OrderDate)-1"));//去年同期
                sql.AppendLine(" ) as T2 on T2.ID=p.ID left join (");
                sql.AppendLine(String.Format(samesql.ToString(), "TotalFee", " and Year(b.OrderDate)=Year(getdate())"));//本年
                sql.AppendLine(" ) as T3 on T3.ID=p.ID left join ( ");
                sql.AppendLine(String.Format(samesql.ToString(), "RealTotal", " and Year(b.OrderDate)=Year(getdate())-1"));//去年
                sql.AppendLine(" ) as T4 on T4.ID=p.ID ");
                sql.AppendLine(" where p.CompanyCD=@CompanyCD  and p.UsedStatus='1' ");
                if (Method == "1")
                {
                    sql.AppendLine(OrderBy);
                }

            }
            #endregion

            #region 按照业务员比较
            if (Compare1 == "4")//
            {
                if (Compare2 == "1") //销售额
                {
                    if (CurrencyID != "0" && CurrencyID != "")
                    {
                        samesql.AppendLine("  select b.Seller as ID,isnull(sum(a.TotalPrice*b.Discount/100),0) as {0} ");
                    }
                    else
                    {
                        samesql.AppendLine("  select b.Seller as ID,isnull(sum(a.TotalPrice*b.Rate*b.Discount/100),0) as {0} ");
                    }
                }
                else
                {
                    samesql.AppendLine("  select b.Seller as ID,isnull(sum(a.ProductCount),0)  as {0} ");
                }
                samesql.AppendLine(" from   officedba.SellOrderDetail as a left join officedba.SellOrder as b on a.OrderNo=b.OrderNo  and a.CompanyCD=b.CompanyCD");
                samesql.AppendLine(" where  a.CompanyCD=@CompanyCD and b.BillStatus<>'1' and b.Status<>'3' {1}");
                if (CurrencyID != "0" && CurrencyID != "")
                {
                    samesql.AppendLine(" and b.CurrencyType=@CurrencyType ");
                }
                samesql.AppendLine(" group by b.Seller ");

                sql.AppendLine(" select isnull(q.EmployeeName,'') as Title,isnull(p.DeptName,'') as FromType,q.ID ");//FromType替代DeptName   
                sql.AppendLine(" ,isnull(TotalPrice,0) as TotalPrice");
                sql.AppendLine(" ,isnull(Tax,0) as Tax");
                sql.AppendLine(" ,isnull(TotalFee,0) as TotalFee");
                sql.AppendLine(" ,isnull(RealTotal,0) as RealTotal");
                sql.AppendLine(" from officedba.EmployeeInfo as q left join  officedba.DeptInfo as p on q.DeptID=p.ID ");
                sql.AppendLine(" left join (");
                sql.AppendLine(String.Format(samesql.ToString(), "TotalPrice", " and Month(b.OrderDate)=Month(getdate()) and Year(b.OrderDate)=Year(getdate()) "));// 本期
                sql.AppendLine(" ) as T1 on T1.ID=p.ID  left join (");
                sql.AppendLine(String.Format(samesql.ToString(), "Tax", " and Month(b.OrderDate)=Month(getdate()) and Year(b.OrderDate)=Year(b.OrderDate)-1"));//去年同期
                sql.AppendLine(" ) as T2 on T2.ID=p.ID left join (");
                sql.AppendLine(String.Format(samesql.ToString(), "TotalFee", " and Year(b.OrderDate)=Year(getdate())"));//本年
                sql.AppendLine(" ) as T3 on T3.ID=p.ID left join ( ");
                sql.AppendLine(String.Format(samesql.ToString(), "RealTotal", " and Year(b.OrderDate)=Year(getdate())-1"));//去年
                sql.AppendLine(" ) as T4 on T4.ID=p.ID ");
                sql.AppendLine(" where p.CompanyCD=@CompanyCD  and p.UsedStatus='1' ");
                if (Method == "1")
                {
                    sql.AppendLine(OrderBy);
                }

            }
            #endregion
            comm.Parameters.Add(SqlHelper.GetParameter("CompanyCD", CompanyCD));
            if (CurrencyID != "0" && CurrencyID != "")
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@CurrencyType", CurrencyID));
            }
            comm.CommandText = sql.ToString();
            DataTable dt = new DataTable();
            if (Method == "0")
            {
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);
            }
            if (Method == "1")
            {
                dt = SqlHelper.ExecuteSearch(comm);
            }
            return dt;
        }
        #endregion

        #region 客户应收款查询
        /// <summary>
        /// 列表页需要
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CustNo"></param>
        /// <param name="CustName"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetReceivables(string CompanyCD, string CustNo, string CustName, string BeginDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select a.ID,isnull(a.TotalPrice,0) as TotalPrice,isnull(a.NAccounts,0) as NAccounts,isnull(e.CustName,'') as CustName ");
            sql.AppendLine("        ,isnull(e.CustNo,'') as CustNo,b.OrderNo as OtherNo ");
            sql.AppendLine("        ,'销售订单'  as BillType ");
            sql.AppendLine("        ,CONVERT(varchar,b.ConfirmDate,120) as ConfirmDate,isnull(g.EmployeeName,'') as EmployeeName,CONVERT(varchar,dateadd(d,e.MaxCreditDate,b.ConfirmDate),120) as LastDate ");
            sql.AppendLine(" from   officedba.BlendingDetails as a left join officedba.SellOrder as b on a.BillCD=b.OrderNo  and a.CompanyCD=b.CompanyCD ");
            sql.AppendLine("        left join officedba.CustInfo as e on b.CustID=e.ID ");
            sql.AppendLine("        left join officedba.EmployeeInfo as g on b.Seller=g.ID ");
            sql.AppendLine(" where a.TotalPrice!=a.YAccounts and a.CompanyCD=@CompanyCD and b.BillStatus!='1' and b.Status!='3'   ");
            if (!string.IsNullOrEmpty(CustNo) && CustNo != "0")
            {
                sql.AppendLine("  and b.CustID=@CustNo1 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustNo1", CustNo));//用客户编号替代ID
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and b.OrderDate>=@OrderDate11 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@OrderDate11", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and b.OrderDate<=@OrderDate12 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@OrderDate12", EndDate));
            }
            sql.AppendLine(" union                                                                                                                           ");
            sql.AppendLine(" select c.ID,isnull(c.TotalPrice,0)as TotalPrice,isnull(c.NAccounts,0) as NAccounts,isnull(f.CustName,'') as CustName                ");
            sql.AppendLine("        ,isnull(f.CustNo,'') as CustNo,d.BackNo as OtherNo                                                                      ");
            sql.AppendLine("        ,'销售退货单'  as BillType                         ");
            sql.AppendLine("        ,CONVERT(varchar,d.ConfirmDate,120) as ConfirmDate,isnull(h.EmployeeName,'') as EmployeeName,CONVERT(varchar,dateadd(d,isnull(f.MaxCreditDate,0),d.ConfirmDate),120)  as LastDate  ");
            sql.AppendLine(" from  officedba.BlendingDetails as c left join officedba.SellBack as d on c.BillCD=d.BackNo  and c.CompanyCD=d.CompanyCD                                           ");
            sql.AppendLine("       left join officedba.CustInfo as f on d.CustID=f.ID                                                                        ");
            sql.AppendLine("       left join officedba.EmployeeInfo as h on d.Seller=f.ID  ");
            sql.AppendLine(" where c.YAccounts!=c.YAccounts  and c.CompanyCD=@CompanyCD  and d.BillStatus!='1'  ");
            if (!string.IsNullOrEmpty(CustNo) && CustNo != "0")
            {
                sql.AppendLine(" and d.CustID=@CustNo2 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustNo2", CustNo));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and d.BackDate>=@OrderDate22 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@OrderDate22", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and d.BackDate<=@OrderDate23 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@OrderDate23", EndDate));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);

        }
        /// <summary>
        /// 获取查询标识为：明细信息-报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CustNo"></param>
        /// <param name="CustName"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetReceivablesReport(string CompanyCD, string CustNo, string CustName, string BeginDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select a.ID,isnull(a.TotalPrice,0) as TotalPrice,isnull(a.NAccounts,0) as TotalFee,isnull(e.CustName,'') as Title ");
            sql.AppendLine("        ,isnull(e.CustNo,'') as DeliverRemark,b.OrderNo as OrderNo ");
            sql.AppendLine("        ,'销售订单'  as FromType ");
            sql.AppendLine("        ,substring(CONVERT(varchar,b.ConfirmDate,120),0,11) as ConfirmDate,isnull(g.EmployeeName,'') as Seller,CONVERT(varchar,dateadd(d,e.MaxCreditDate,b.ConfirmDate),120)  as CreateDate ");
            sql.AppendLine(" from   officedba.BlendingDetails as a left join officedba.SellOrder as b on a.BillCD=b.OrderNo  and a.CompanyCD=b.CompanyCD ");
            sql.AppendLine("        left join officedba.CustInfo as e on b.CustID=e.ID ");
            sql.AppendLine("        left join officedba.EmployeeInfo as g on b.Seller=g.ID ");
            sql.AppendLine(" where a.TotalPrice!=a.YAccounts and a.CompanyCD=@CompanyCD  and b.BillStatus!='1' and b.Status!='3'   ");
            if (!string.IsNullOrEmpty(CustNo) && CustNo != "0")
            {
                sql.AppendLine("  and b.CustID=@CustNo ");

            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine("  and b.OrderDate>=@OrderDate1 ");

            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and b.OrderDate<=@OrderDate2 ");
            }
            sql.AppendLine(" union                                                                                                                           ");
            sql.AppendLine(" select c.ID,isnull(c.TotalPrice,0)as TotalPrice,isnull(c.NAccounts,0) as TotalFee,isnull(f.CustName,'') as Title                ");
            sql.AppendLine("        ,isnull(f.CustNo,'') as DeliverRemark,c.BillCD as OrderNo                                                                      ");
            sql.AppendLine("        ,'销售退货单'  as FromType                         ");
            sql.AppendLine("        ,substring(CONVERT(varchar,d.ConfirmDate,120),0,11) as ConfirmDate,isnull(h.EmployeeName,'') as Seller,CONVERT(varchar,dateadd(d,isnull(f.MaxCreditDate,0),d.ConfirmDate),120)  as CreateDate  ");
            sql.AppendLine(" from  officedba.BlendingDetails as c left join officedba.SellBack as d on c.BillCD=d.BackNo  and c.CompanyCD=d.CompanyCD                                           ");
            sql.AppendLine("       left join officedba.CustInfo as f on d.CustID=f.ID                                                                        ");
            sql.AppendLine("       left join officedba.EmployeeInfo as h on d.Seller=f.ID  ");
            sql.AppendLine(" where c.YAccounts!=c.YAccounts  and c.CompanyCD=@CompanyCD and d.BillStatus!='1' ");
            if (!string.IsNullOrEmpty(CustNo) && CustNo != "0")
            {
                sql.AppendLine(" and d.CustID=@CustNo ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustNo", CustNo)); //CustNo代替CustID
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and d.BackDate>=@OrderDate1 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@OrderDate1", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and d.BackDate<=@OrderDate2 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@OrderDate2", EndDate));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);

        }
        /// <summary>
        /// 获取查询标识为：汇总信息-报表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CustNo"></param>
        /// <param name="CustName"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetReceivalesAll(string CompanyCD, string CustNo, string CustName, string BeginDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine(" select b.CustID as ID,isnull(sum(a.NAccounts),0) as TotalPrice,isnull(e.CustName,'') as Title,isnull(e.CustNo,'') as DeliverRemark,b.OrderDate   ");
            sql.AppendLine(",isnull(DATEDIFF(day,b.OrderDate,getdate())-e.MaxCreditDate,0) as CompanyCD                                              ");
            sql.AppendLine(" from  officedba.BlendingDetails as a left join officedba.SellOrder as b on a.BillCD=b.OrderNo and a.CompanyCD=b.CompanyCD    ");
            sql.AppendLine(" left join officedba.CustInfo as e on b.CustID=e.ID                                                                           ");
            sql.AppendLine(" where a.TotalPrice!=a.YAccounts and a.CompanyCD=@CompanyCD  and b.BillStatus!='1' and b.Status!='3'     ");
            if (!string.IsNullOrEmpty(CustNo) && CustNo != "0")
            {
                sql.AppendLine("  and b.CustID=@CustNo1 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustNo1", CustNo));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and b.OrderDate>=@OrderDate11 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@OrderDate11", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and b.OrderDate<=@OrderDate12 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@OrderDate12", EndDate));
            }
            sql.AppendLine(" group by e.CustName,e.CustNo,b.OrderDate,e.MaxCreditDate,b.CustID                                                                     ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);
        }
        #endregion

        #region 客户台帐查询
        public static DataTable GetCustSell(string CompanyCD, string CustNo, string CustName, string BeginDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select a.ID,isnull(d.CustNo,'') as CustNo,isnull(d.CustName,'') as CustName,a.BillCD as OtherNO                   ");
            sql.AppendLine("        ,'销售订单' as BillingType,CONVERT(varchar,c.ConfirmDate,120)  as ConfirmDate ");
            sql.AppendLine("        ,isnull(e.EmployeeName,'') as EmployeeName,isnull(a.TotalPrice,0) as TotalPrice                            ");
            sql.AppendLine(" from   officedba.BlendingDetails  a ");
            sql.AppendLine("        left join officedba.SellOrder c on a.BillCD=c.OrderNo and c.CompanyCD=a.CompanyCD                          ");
            sql.AppendLine("        left join officedba.CustInfo d on c.CustID=d.ID left  join officedba.EmployeeInfo e on e.ID=c.Seller       ");
            sql.AppendLine(" where  a.BillingType='1' and  c.CompanyCD=@CompanyCD and c.BillStatus!='1'and  c.Status!='3' ");
            if (!string.IsNullOrEmpty(CustNo) && CustNo != "0")
            {
                sql.AppendLine(" and c.CustID=@CustNo1 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustNo1", CustNo));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and c.OrderDate>=@BeginDate1 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate1", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and c.OrderDate<=@EndDate1 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate1", EndDate));
            }
            sql.AppendLine(" union ");
            sql.AppendLine(" select a1.ID,isnull(d1.CustNo,'') as CustNo,isnull(d1.CustName,'')as CustName,a1.BillCD as OtherNO                ");
            sql.AppendLine("        ,'销售退货单' as BillingType, CONVERT(varchar,c1.ConfirmDate,120) as ConfirmDate ");
            sql.AppendLine("        ,isnull(e1.EmployeeName,'') as EmployeeName,isnull(a1.TotalPrice,0) as TotalPrice ");
            sql.AppendLine(" from   officedba.BlendingDetails  a1 ");
            sql.AppendLine("        left join officedba.SellBack c1 on a1.BillCD=c1.BackNo and c1.CompanyCD=a1.CompanyCD ");
            sql.AppendLine("        left join officedba.CustInfo d1 on c1.CustID=d1.ID left  join officedba.EmployeeInfo e1 on e1.ID=c1.Seller ");
            sql.AppendLine(" where  a1.BillingType='3'  and a1.CompanyCD=@CompanyCD and c1.BillStatus!='1' ");
            if (!string.IsNullOrEmpty(CustNo) && CustNo != "0")
            {
                sql.AppendLine(" and c1.CustID=@CustNo2 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustNo2", CustNo));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and c1.BackDate>=@BeginDate2 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate2", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and c1.BackDate<=@EndDate2 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate2", EndDate));
            }
            sql.AppendLine(" union                                                                                                             ");
            sql.AppendLine(" select a2.ID,isnull(c2.CustNo,'') as CustNo,isnull(c2.CustName,'') as CustName,a2.SendNo as OtherNo               ");
            sql.AppendLine("        ,'发货通知单' as BillingType, CONVERT(varchar,a2.ConfirmDate,120) as ConfirmDate  ");
            sql.AppendLine("        ,isnull(b2.EmployeeName,'') as EmployeeName,isnull(a2.TotalPrice*a2.Rate,0) as TotalPrice    ");
            sql.AppendLine(" from   officedba.SellSendDetail d2 left join  officedba.SellSend a2 on a2.SendNo=d2.SendNo left join              ");
            sql.AppendLine("        officedba.EmployeeInfo b2 on a2.Seller=b2.ID                                                         ");
            sql.AppendLine("        left join officedba.CustInfo c2 on a2.CustID=c2.ID                                                         ");
            sql.AppendLine(" where a2.BillStatus!='1' and d2.CompanyCD=@CompanyCD and a2.BillStatus!='1' ");
            if (!string.IsNullOrEmpty(CustNo) && CustNo != "0")
            {
                sql.AppendLine(" and a2.CustID=@CustNo ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustNo", CustNo));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and a2.ConfirmDate>=@BeginDate3 ");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate3", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and a2.ConfirmDate<=@EndDate3");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate3", EndDate));
            }

            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            DataTable dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);
            return dt;
        }

        /// <summary>
        /// 报表需要
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CustNo"></param>
        /// <param name="CustName"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetCustSellReport(string CompanyCD, string CustNo, string CustName, string BeginDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select a.ID,isnull(d.CustNo,'') as DeliverRemark,isnull(d.CustName,'') as Title,c.OrderNo as OrderNo ");
            sql.AppendLine("        ,'销售订单' as FromType,substring(CONVERT(varchar,c.ConfirmDate,120),0,11)  as ConfirmDate  ");
            sql.AppendLine("        ,isnull(e.EmployeeName,'') as Seller,isnull(a.TotalPrice,0) as TotalPrice   ");
            sql.AppendLine(" from   officedba.BlendingDetails  a  ");
            sql.AppendLine("        left join officedba.SellOrder c on a.BillCD=c.OrderNo and c.CompanyCD=a.CompanyCD ");
            sql.AppendLine("        left join officedba.CustInfo d on c.CustID=d.ID left  join officedba.EmployeeInfo e on e.ID=c.Seller ");
            sql.AppendLine(" where  a.BillingType='1' and  c.CompanyCD=@CompanyCD and c.BillStatus!='1'and  c.Status!='3' ");
            if (!string.IsNullOrEmpty(CustNo) && CustNo != "0")
            {
                sql.AppendLine(" and c.CustID=@CustNo ");

            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine("  and c.OrderDate>=@BeginDate ");

            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine("  and c.OrderDate<=@EndDate ");

            }
            sql.AppendLine(" union ");
            sql.AppendLine(" select a1.ID,isnull(d1.CustNo,'') as DeliverRemark,isnull(d1.CustName,'')as Title,c1.BackNo as OrderNo                ");
            sql.AppendLine("        ,'销售退货单' as FromType,substring(CONVERT(varchar,c1.ConfirmDate,120),0,11) as ConfirmDate ");
            sql.AppendLine("        ,isnull(e1.EmployeeName,'') as Seller,isnull(a1.TotalPrice,0) as TotalPrice ");
            sql.AppendLine(" from   officedba.BlendingDetails  a1  ");
            sql.AppendLine("        left join officedba.SellBack c1 on a1.BillCD=c1.BackNo and c1.CompanyCD=a1.CompanyCD ");
            sql.AppendLine("        left join officedba.CustInfo d1 on c1.CustID=d1.ID left  join officedba.EmployeeInfo e1 on e1.ID=c1.Seller ");
            sql.AppendLine(" where  a1.BillingType='3' and c1.CompanyCD=@CompanyCD  and a1.CompanyCD=@CompanyCD and c1.BillStatus!='1' ");
            if (!string.IsNullOrEmpty(CustNo) && CustNo != "0")
            {
                sql.AppendLine("  and c1.CustID=@CustNo ");

            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and c1.BackDate>=@BeginDate ");

            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine("  and c1.BackDate<=@EndDate ");

            }
            sql.AppendLine(" union ");
            sql.AppendLine(" select a2.ID,isnull(c2.CustNo,'') as DeliverRemark,isnull(c2.CustName,'') as Title,a2.SendNo as OrderNo               ");
            sql.AppendLine("        ,'发货通知单' as FromType,substring(CONVERT(varchar,a2.ConfirmDate,120),0,11) as ConfirmDate  ");
            sql.AppendLine("        ,isnull(b2.EmployeeName,'') as Seller,isnull(a2.TotalPrice*a2.Rate,0) as TotalPrice    ");
            sql.AppendLine(" from   officedba.SellSendDetail d2 left join  officedba.SellSend a2 on a2.SendNo=d2.SendNo left join              ");
            sql.AppendLine("        officedba.EmployeeInfo b2 on a2.Seller=b2.ID                                                         ");
            sql.AppendLine("        left join officedba.CustInfo c2 on a2.CustID=c2.ID                                                         ");
            sql.AppendLine(" where a2.BillStatus!='1' and d2.CompanyCD=@CompanyCD and a2.BillStatus!='1' and d2.CompanyCD=@CompanyCD ");
            if (!string.IsNullOrEmpty(CustNo) && CustNo != "0")
            {
                sql.AppendLine("  and a2.CustID=@CustNo ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustNo", CustNo));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine("  and a2.ConfirmDate>=@BeginDate ");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine("  and a2.ConfirmDate<=@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));

            comm.CommandText = sql.ToString();
            DataTable dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);
            return dt;
        }
        #endregion

        #region 今日进销快照
        public static DataTable GetIn(string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select isnull(sum(c.TotalPrice),0)   as TotalPrice ");
            sql.AppendLine("      ,'今日购进' as Title ");
            sql.AppendLine(" from   officedba.BlendingDetails c left join officedba.PurchaseOrder a on a.OrderNo=c.BillCD  and c.CompanyCD=a.CompanyCD              ");
            sql.AppendLine(" where  DateDiff(d,getdate(),c.CreateDate)>=0 and DateDiff(d,getdate(),c.CreateDate)<=1 and c.BillingType='2'  and c.CompanyCD=@CompanyCD ");

            sql.AppendLine(" union ");

            sql.AppendLine(" select isnull(sum(c2.TotalPrice),0)    as TotalPrice ");
            sql.AppendLine(" ,'今日销出' as Title ");
            sql.AppendLine(" from   officedba.BlendingDetails c2 left join officedba.SellOrder a2 on a2.OrderNo=c2.BillCD and c2.CompanyCD=a2.CompanyCD   ");
            sql.AppendLine(" where  DateDiff(d,getdate(),c2.CreateDate)>=0 and DateDiff(d,getdate(),c2.CreateDate)<=1 and c2.BillingType='1' and c2.CompanyCD=@CompanyCD ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);


        }
        #endregion

        #region 今日应付快照
        public static DataTable GetPurchase(string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select isnull(sum(c.TotalPrice),0) as TotalPrice,'今日采购' as Title ");
            sql.AppendLine(" from    officedba.BlendingDetails c  left join officedba.PurchaseOrder a on a.OrderNo=c.BillCD and c.CompanyCD=a.CompanyCD  ");
            sql.AppendLine(" where  DateDiff(d,getdate(),c.CreateDate)>=0 and DateDiff(d,getdate(),c.CreateDate)<=1 and c.BillingType='2' AND ");
            sql.AppendLine(" c.CompanyCD=@CompanyCD ");
            sql.AppendLine(" union ");
            sql.AppendLine(" (select isnull(Sum(b1.TotalPrice),0)as TotalPrice ");
            sql.AppendLine("        ,'今日支出' as Title ");
            sql.AppendLine(" from   officedba.BlendingDetails b1 ");
            sql.AppendLine(" 	   left  join officedba.PurchaseOrder as c1 on b1.BillCD=c1.OrderNO and b1.CompanyCD=c1.CompanyCD ");
            sql.AppendLine(" where  b1.BillingType='2' and DateDiff(d,getdate(),b1.CreateDate)>=0 and DateDiff(d,getdate(),b1.CreateDate)<=1 ");
            sql.AppendLine(" AND  b1.CompanyCD=@CompanyCD) ");
            sql.AppendLine(" union ");
            sql.AppendLine(" (select isnull(Sum(b2.NAccounts),0)   as TotalPrice ");
            sql.AppendLine("        ,'今日应付' as Title ");
            sql.AppendLine(" from   officedba.BlendingDetails b2 ");
            sql.AppendLine(" 	   left  join officedba.PurchaseOrder as c2 on b2.BillCD=c2.OrderNO and b2.CompanyCD=c2.CompanyCD ");
            sql.AppendLine(" where  b2.BillingType='2' and DateDiff(d,getdate(),b2.CreateDate)>=0 and DateDiff(d,getdate(),b2.CreateDate)<=1 ");
            sql.AppendLine(" AND b2.CompanyCD=@CompanyCD) ");
            sql.AppendLine(" union ");
            sql.AppendLine(" select sum(isnull(a3.ProductCount,0)+isnull(a3.InCount,0)+isnull(a3.RoadCount,0)-isnull(a3.OutCount,0)-isnull(a3.OrderCount,0)) as TotalPrice,'当前库存' as Title ");
            sql.AppendLine(" from   officedba.StorageProduct a3 ");

            sql.AppendLine(" where  a3.CompanyCD=@CompanyCD ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        #region 今日应收快照
        public static DataTable GetSell(string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select isnull(sum(c.TotalPrice),0)     as TotalPrice ");
            sql.AppendLine(" ,'今日销出' as Title ");
            sql.AppendLine(" from   officedba.BlendingDetails c left join officedba.SellOrder a on a.OrderNo=c.BillCD    and a.CompanyCD=c.CompanyCD ");
            sql.AppendLine(" where  DateDiff(d,getdate(),c.CreateDate)>=0 and DateDiff(d,getdate(),c.CreateDate)<=1 and c.BillingType='1' AND c.CompanyCD=@CompanyCD ");
            sql.AppendLine(" union ");
            sql.AppendLine(" select isnull(sum(c.YAccounts),0)   as TotalPrice ");
            sql.AppendLine(" ,'今日收入' as Title ");
            sql.AppendLine(" from   officedba.BlendingDetails c left join officedba.SellOrder a on a.OrderNo=c.BillCD and a.CompanyCD=c.CompanyCD ");
            sql.AppendLine(" where  DateDiff(d,getdate(),c.CreateDate)>=0 and DateDiff(d,getdate(),c.CreateDate)<=1 and c.BillingType='1' AND c.CompanyCD=@CompanyCD ");
            sql.AppendLine(" union ");
            sql.AppendLine(" select isnull(sum(c.NAccounts),0) ");
            sql.AppendLine(" as TotalPrice ");
            sql.AppendLine(" ,'今日应收' as Title ");
            sql.AppendLine(" from   officedba.BlendingDetails c left join officedba.SellOrder a on a.OrderNo=c.BillCD and a.CompanyCd=c.CompanyCD ");
            sql.AppendLine(" where  DateDiff(d,getdate(),c.CreateDate)>=0 and DateDiff(d,getdate(),c.CreateDate)<=1 and c.BillingType='1' AND c.CompanyCD=@CompanyCD ");

            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

    }
}
