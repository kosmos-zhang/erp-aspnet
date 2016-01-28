using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.CustManager;
using System.Data.SqlTypes;

namespace XBase.Data.Office.FinanceManager
{
    public class FinanceReportDBHelper
    {


        #region  销货成本明细表_报表
        /// <summary>
        /// 销货成本明细表
        /// </summary>
        /// <param name="Year">年</param>
        /// <param name="Month">月</param>
        /// <returns></returns>
        public static DataTable GetSellCostDetail(string Year,string Month,string CompanyCD,int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select convert(varchar(100),VoucherDate,23) VoucherDate ,b.AttestNo,ProductName,ProductCount, TotalPrice,a.StandardCost, ");
                sql.Append(" (TotalPrice-a.StandardCost) as Profit from ( ");
                sql.Append(" select sum(ProductCount) as ProductCount,sum(TotalPrice) as TotalPrice,sum(StandardCost) as StandardCost,AttestId,ProductId  ");
                sql.Append(" from ( ");
                sql.Append(" select sum(ProductCount) as ProductCount,sum(TotalPrice) as TotalPrice, ");
                sql.Append(" sum((case when StandardCost is not null then StandardCost when TaxBuy is not null then TaxBuy  when StandardSell is not null then StandardSell else 0 end)*ProductCount ) as StandardCost,OutNo,ProductId from officedba.StorageOutSellDetail a  ");
                sql.Append(" inner join officedba.productInfo b on a.ProductId=b.Id where a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' group by OutNo,ProductId ");
                sql.Append(" ) a inner join officedba.RunningBillDetail b on a.OutNO=b. RunningBillCD  ");
                sql.Append(" group by b.AttestId,a.ProductId ");
                sql.Append(" ) a inner join Officedba.AttestBill b on a.AttestId=b.Id inner join officedba.productInfo c ");
                sql.Append(" on a.ProductId=c.Id where b.status=3 ");
                if (CompanyCD != "") 
                {
                    sql.Append(" and b.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }
                if (Year != "") 
                {
                    sql.Append(" and year(VoucherDate)='");
                    sql.Append(Year);
                    sql.Append("' ");
                }

                if (Month != "") 
                {
                    sql.Append(" and month(VoucherDate)='");
                    sql.Append(Month);
                    sql.Append("' ");
                }

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }


        /// <summary>
        /// 销货成本明细表
        /// </summary>
        /// <param name="Year">年</param>
        /// <param name="Month">月</param>
        /// <returns></returns>
        public static DataTable GetSellCostDetailPrint(string Year, string Month, string CompanyCD, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select convert(varchar(100),VoucherDate,23) VoucherDate ,b.AttestNo,ProductName,ProductCount, TotalPrice,a.StandardCost, ");
                sql.Append(" (TotalPrice-a.StandardCost) as Profit from ( ");
                sql.Append(" select sum(ProductCount) as ProductCount,sum(TotalPrice) as TotalPrice,sum(StandardCost) as StandardCost,AttestId,ProductId  ");
                sql.Append(" from ( ");
                sql.Append(" select sum(ProductCount) as ProductCount,sum(TotalPrice) as TotalPrice, ");
                sql.Append(" sum((case when StandardCost is not null then StandardCost when TaxBuy is not null then TaxBuy  when StandardSell is not null then StandardSell else 0 end)*ProductCount ) as StandardCost,OutNo,ProductId from officedba.StorageOutSellDetail a  ");
                sql.Append(" inner join officedba.productInfo b on a.ProductId=b.Id where a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' group by OutNo,ProductId ");
                sql.Append(" ) a inner join officedba.RunningBillDetail b on a.OutNO=b. RunningBillCD  ");
                sql.Append(" group by b.AttestId,a.ProductId ");
                sql.Append(" ) a inner join Officedba.AttestBill b on a.AttestId=b.Id inner join officedba.productInfo c ");
                sql.Append(" on a.ProductId=c.Id where b.status=3 ");
                if (CompanyCD != "")
                {
                    sql.Append(" and b.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }
                if (Year != "")
                {
                    sql.Append(" and year(VoucherDate)='");
                    sql.Append(Year);
                    sql.Append("' ");
                }

                if (Month != "")
                {
                    sql.Append(" and month(VoucherDate)='");
                    sql.Append(Month);
                    sql.Append("' ");
                }

                sql.Append("Order by ");
                sql.Append(" "+ord+" desc " );

                return SqlHelper.ExecuteSql(sql.ToString());

            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        #endregion

        #region  销货收入月报表_报表
        /// <summary>
        /// 销货收入月报表
        /// </summary>
        /// <param name="Year">年</param>
        /// <param name="Month">月</param>
        /// <returns></returns>
        public static DataTable GetSellMonthIncome(string Year, string Month, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select Convert(varchar(20),CreateDate,23) CreateDate,BillCD,ContactUnits,TotalPrice,isnull(YAccounts,'0.00') YAccounts,NAccounts from officedba.billing where billingtype=1  ");
                if (CompanyCD != "")
                {
                    sql.Append(" and CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }
                if (Year != "")
                {
                    sql.Append(" and year(CreateDate)='");
                    sql.Append(Year);
                    sql.Append("' ");
                }

                if (Month != "")
                {
                    sql.Append(" and month(CreateDate)='");
                    sql.Append(Month);
                    sql.Append("' ");
                }

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 销货成本明细表
        /// </summary>
        /// <param name="Year">年</param>
        /// <param name="Month">月</param>
        /// <returns></returns>
        public static DataTable GetSellMonthIncomePrint(string Year, string Month, string CompanyCD, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select Convert(varchar(20),CreateDate,23) CreateDate,BillCD,ContactUnits,TotalPrice,isnull(YAccounts,'0.00') YAccounts,NAccounts from officedba.billing where billingtype=1  ");
                if (CompanyCD != "")
                {
                    sql.Append(" and CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }
                if (Year != "")
                {
                    sql.Append(" and year(CreateDate)='");
                    sql.Append(Year);
                    sql.Append("' ");
                }

                if (Month != "")
                {
                    sql.Append(" and month(CreateDate)='");
                    sql.Append(Month);
                    sql.Append("' ");
                }

                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());

            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        #endregion

        #region 获取账簿中不同的客户--应收帐款
        /// <summary>
        /// 获取账簿中不同的客户--应收帐款
        /// </summary>
        /// <param name="CurryType">币种</param>
        /// <param name="SubjectsCD">会计科目</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetDistinctCusFromAcountBook(string CurryType, string SubjectsCD, string CompanyCD)
        {
            try
            {
                string SubjectsCDList = VoucherDBHelper.GetSubjectsNextCD(SubjectsCD);
                string sql = string.Empty;
                if (CurryType.LastIndexOf(",") == -1)
                {
                    sql = string.Format(@"select distinct [SubjectsDetails],[FormTBName],[FileName] from officedba.AcountBook where CurrencyTypeID='{0}' and SubjectsCD in ( " + SubjectsCDList + " ) and CompanyCD='{1}' and SubjectsDetails<>'' and SubjectsDetails is not null  ", CurryType, CompanyCD);
                }
                else
                {
                    sql = string.Format(@"select distinct [SubjectsDetails],[FormTBName],[FileName] from officedba.AcountBook where SubjectsCD in ( " + SubjectsCDList + " ) and CompanyCD='{0}' and SubjectsDetails<>'' and SubjectsDetails is not null", CompanyCD);
                }

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取某会计期间某科目某币种对应的详细信息
        /// <summary>
        /// 获取某会计期间某科目某币种对应的详细信息
        /// </summary>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="CurryType">币种</param>
        /// <param name="SubjectsCD">会计科目</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="SubjectsDetails">辅助核算主键</param>
        /// <param name="FormTBName">来源表</param>
        /// <param name="FileName">来源表字段名</param>
        /// <returns></returns>
        public static DataTable GetAccountBookInfo(string StartDate, string EndDate, string CurryType,string SubjectsCD, string CompanyCD, string SubjectsDetails, string FormTBName, string FileName)
        {
            try
            {
                string SubjectsCDList = VoucherDBHelper.GetSubjectsNextCD(SubjectsCD);
                string sql = string.Empty;
                if (CurryType.LastIndexOf(",") == -1)
                {
                    sql = string.Format(@"select a.Abstract,a.VoucherDate,a.CurrencyTypeID,a.ExchangeRate,a.OriginalAmount,a.ForeignBeginAmount,a.ForeignThisDebit,a.ForeignThisCredit,a.ForeignEndAmount,a.BeginAmount,a.ThisDebit,a.ThisCredit,a.EndAmount,b.FromTbale,b.FromValue from officedba.AcountBook a left outer join officedba.AttestBill b on a.AttestBillID=b.ID where a.VoucherDate>='{0}' and a.VoucherDate<='{1}' and a.CurrencyTypeID='{2}' and a.SubjectsCD in ( {3} ) and a.CompanyCD='{4}' and b.CompanyCD='{5}' and a.SubjectsDetails='{6}' and a.FormTBName='{7}' and a.FileName='{8}'", StartDate, EndDate, CurryType, SubjectsCDList, CompanyCD, CompanyCD, SubjectsDetails, FormTBName, FileName);
                }
                else
                {
                    sql = string.Format(@"select a.Abstract,a.VoucherDate,a.CurrencyTypeID,a.ExchangeRate,a.OriginalAmount,a.ForeignBeginAmount,a.ForeignThisDebit,a.ForeignThisCredit,a.ForeignEndAmount,a.BeginAmount,a.ThisDebit,a.ThisCredit,a.EndAmount,b.FromTbale,b.FromValue from officedba.AcountBook a left outer join officedba.AttestBill b on a.AttestBillID=b.ID where a.VoucherDate>='{0}' and a.VoucherDate<='{1}'  and a.SubjectsCD in ( {2} ) and a.CompanyCD='{3}' and b.CompanyCD='{4}' and a.SubjectsDetails='{5}' and a.FormTBName='{6}' and a.FileName='{7}'", StartDate, EndDate, SubjectsCDList, CompanyCD, CompanyCD, SubjectsDetails, FormTBName, FileName);
                }

                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 客户账龄分析表读取视图
        /// <summary>
        /// 客户账龄分析表读取视图
        /// </summary>
        /// <param name="CustID">客户主键</param>
        /// <param name="CurryTypeID">币种</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetCusSalesAging(string CustID, string CurryTypeID,string CompanyCD)
        {
            StringBuilder SqlStr = new StringBuilder();
            SqlStr.AppendLine("select a.ID,a.CompanyCD,a.CustName,a.OrderNo,a.Title,a.ResultDate,a.PlayWay,a.MoneyWay,a.CurrencyName,");
            SqlStr.AppendLine("a.FromTBName,a.FileName,a.CustID,a.isOpenbill,a.Status,a.CurrencyType,a.Rate,a.SellType ,");
            SqlStr.AppendLine("CASE WHEN a.SellType='1' then '销售订单' when a.SellType='3' then '销售退货单' when a.SellType='4' then '代销结算单' end as SellTypeName,");
            SqlStr.AppendLine("case when b.CreditManage='2' then b.MaxCreditDate  when b.CreditManage='1' then '0' end as MaxCreditDate,");
            SqlStr.AppendLine("a.RealTotal,b.CustNo,");
            SqlStr.AppendLine("CASE WHEN  (select count(ID) from officedba.BlendingDetails  where BillCD=a.OrderNo and CompanyCD=a.CompanyCD and BillingType=a.SellType)=1 THEN ");
            SqlStr.AppendLine("(select  NAccounts from officedba.BlendingDetails  where BillCD=a.OrderNo and CompanyCD=a.CompanyCD and BillingType=a.SellType ) else  a.RealTotal end as NAccounts,");
            SqlStr.AppendLine("CASE WHEN  (select count(ID) from officedba.BlendingDetails  where BillCD=a.OrderNo and CompanyCD=a.CompanyCD and BillingType=a.SellType)=1 THEN ");
            SqlStr.AppendLine("(select  YAccounts from officedba.BlendingDetails  where BillCD=a.OrderNo and CompanyCD=a.CompanyCD and BillingType=a.SellType ) else  '0' end as YAccounts");
            SqlStr.AppendLine("from officedba.GetSellOrBackOrderInfo a ");
            SqlStr.AppendLine("LEFT OUTER JOIN officedba.CustInfo b");
            SqlStr.AppendLine("on a.CustID=b.ID  ");
            SqlStr.AppendLine("where a.CompanyCD='{0}' and b.CompanyCD='{0}' {1} ");
            SqlStr.AppendLine("order by a.CustID asc, a.selltype asc,a.ResultDate asc  ");

            string QueryStr = string.Empty;
            if (CurryTypeID.LastIndexOf(",") == -1)
            {
                QueryStr += " and a.CurrencyType='"+CurryTypeID+"' ";
            }
            if (CustID.Trim().Length > 0)
            {
                QueryStr += " and a.CustID='"+CustID+"' ";
            }

            string ExceSQL = string.Format(SqlStr.ToString(), CompanyCD, QueryStr);
            return SqlHelper.ExecuteSql(ExceSQL);
        }
        #endregion

        #region 获取客户账龄视图不同客户
        /// <summary>
        /// 获取客户账龄视图不同客户
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="CurryTypeID">币种</param>
        /// <returns></returns>
        public static DataTable GetDistinctCustFromView(string CompanyCD,string CurryTypeID)
        {
            string SQL = "SELECT DISTINCT CustID from officedba.GetSellOrBackOrderInfo where CompanyCD=@CompanyCD {0}";

            string QueryStr = string.Empty;
            if (CurryTypeID.LastIndexOf(",") == -1)
            {
                QueryStr += " and CurrencyType='" + CurryTypeID + "'";
            }

            SQL = string.Format(SQL, QueryStr);

            SqlParameter[] parmss = 
                {
                     new SqlParameter("@CompanyCD",CompanyCD)
                };
            return SqlHelper.ExecuteSql(SQL, parmss);

        }
        #endregion
    }
}
