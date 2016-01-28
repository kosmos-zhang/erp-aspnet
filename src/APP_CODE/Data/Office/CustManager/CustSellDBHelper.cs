using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.CustManager;
using System.Data.SqlTypes;
using XBase.Common;
using System.Collections;

namespace XBase.Data.Office.CustManager
{
    public class CustSellDBHelper
    {
        /// <summary>
        /// 客户购买金额走势
        /// </summary>
        /// <param name="TimeType"></param>
        /// <param name="ProductID"></param>
        /// <param name="CustName"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="BeginCount"></param>
        /// <param name="EndCount"></param>
        /// <param name="BeginPrice"></param>
        /// <param name="EndPrice"></param>
        /// <returns></returns>
        public static DataTable GetCustSellInfo(string CompanyCD,string TimeType, string ProductID, string CustName, string BeginDate, string EndDate, string BeginCount, string EndCount, string BeginPrice, string EndPrice)
        {
            string ColumnName = "";
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            if (TimeType == "3")//周
            {
                ColumnName = "(datename(yyyy,a.OrderDate)+'年第'+datename(week,a.OrderDate)+'周')";
            }
            if (TimeType == "2")//月
            {
                ColumnName = "(datename(yyyy,a.OrderDate)+'年'+datename(mm,a.OrderDate)+'月')";
            }
            if (TimeType == "1")//年
            {
                ColumnName = "(datename(yyyy,a.OrderDate)+'年')";
            }
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select ");
            if (!string.IsNullOrEmpty(ColumnName))
            {
                sql.AppendLine(" (" + ColumnName + ") as TheDate, ");
            }
            sql.AppendLine("  Convert(numeric(16," + point + "),Sum(b.TotalFee*a.Discount/100*a.Rate)) as TotalPrice,Convert(numeric(16," + point + "),sum(b.ProductCount)) as ProductCount  ");
            sql.AppendLine(" from officedba.SellOrder as a right join officedba.SellOrderDetail as b on a.OrderNo=b.OrderNo    ");
            sql.AppendLine("  and a.CompanyCD=b.CompanyCD   left join officedba.CustInfo as c on c.ID=a.CustID                                                                   ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD and a.Status<>'3' and a.BillStatus<>'1'");
            if (!string.IsNullOrEmpty(CustName))
            {
                sql.AppendLine(" and c.CustName like @CustName ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustName", "%" + CustName + "%"));
            }
            if (ProductID != "" && ProductID != "0")
            {
                sql.AppendLine(" and b.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and a.OrderDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and a.OrderDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (!string.IsNullOrEmpty(BeginPrice))
            {
                sql.AppendLine(" and b.UnitPrice>=@BeginPrice");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginPrice", BeginPrice));
            }
            if (!string.IsNullOrEmpty(EndPrice))
            {
                sql.AppendLine(" and b.UnitPrice<=@EndPrice");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndPrice", EndPrice));
            }

            if (!string.IsNullOrEmpty(BeginCount))
            {
                sql.AppendLine(" and b.ProductCount>=@BeginCount");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginCount", BeginCount));
            }
            if (!string.IsNullOrEmpty(EndCount))
            {
                sql.AppendLine(" and b.ProductCount<=@EndCount");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndCount", EndCount));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            sql.AppendLine(" group by " + ColumnName);
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        /// <summary>
        /// 客户购买金额走势-明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="TimeType"></param>
        /// <param name="ProductID"></param>
        /// <param name="CustName"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="BeginCount"></param>
        /// <param name="EndCount"></param>
        /// <param name="BeginPrice"></param>
        /// <param name="EndPrice"></param>
        /// <param name="XValue"></param>
        /// <returns></returns>
        public static SqlCommand GetCustSellInfoDetail(string CompanyCD, string TimeType, string ProductID, string CustName, string BeginDate, string EndDate, string BeginCount, string EndCount, string BeginPrice, string EndPrice,string XValue)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            string ColumnName = "";
            if (TimeType == "3")//周
            {
                ColumnName = "(datename(yyyy,T2.OrderDate)+'年第'+datename(week,T2.OrderDate)+'周')";
            }
            if (TimeType == "2")//月
            {
                ColumnName = "(datename(yyyy,T2.OrderDate)+'年'+datename(mm,T2.OrderDate)+'月')";
            }
            if (TimeType == "1")//年
            {
                ColumnName = "(datename(yyyy,T2.OrderDate)+'年')";
            }

            sql.AppendLine(" select (" + ColumnName + ")as TheDate,T1.ProductID,T1.OrderNo,Convert(char(20),Convert(numeric(16," + point + "),(T1.TotalFee*T2.Discount/100*T2.Rate)))+'&nbsp;' as TotalPrice,Convert(char(20),Convert(numeric(16," + point + "),T1.ProductCount))+'&nbsp;' ProductCount ");
            sql.AppendLine(" ,isnull(T3.ProductName,'') as ProductName,isnull(T5.CodeName,'') as CodeName,isnull(T3.Specification,'') as Specification from ");
            sql.AppendLine(" (select a.ProductID,a.OrderNo,Sum(a.TotalFee) as TotalFee,sum(a.ProductCount) as ProductCount   ");
            sql.AppendLine(" from officedba.SellOrderDetail as a ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD ");
            #region
            if (ProductID != "" && ProductID != "0")
            {
                sql.AppendLine(" and a.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            if (!string.IsNullOrEmpty(BeginPrice))
            {
                sql.AppendLine(" and a.UnitPrice>=@BeginPrice");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginPrice", BeginPrice));
            }
            if (!string.IsNullOrEmpty(EndPrice))
            {
                sql.AppendLine(" and a.UnitPrice<=@EndPrice");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndPrice", EndPrice));
            }

            if (!string.IsNullOrEmpty(BeginCount))
            {
                sql.AppendLine(" and a.ProductCount>=@BeginCount");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginCount", BeginCount));
            }
            if (!string.IsNullOrEmpty(EndCount))
            {
                sql.AppendLine(" and a.ProductCount<=@EndCount");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndCount", EndCount));
            }
            #endregion
            sql.AppendLine(" group by a.OrderNo,a.ProductID ) as T1");
            sql.AppendLine(" left join officedba.SellOrder as T2 on T1.OrderNo=T2.OrderNo ");
            sql.AppendLine(" left join officedba.ProductInfo as T3 on T3.ID=T1.ProductID ");
            sql.AppendLine(" left join officedba.CustInfo as T4 on T4.ID=T2.CustID ");
            sql.AppendLine(" left join officedba.CodeUnitType as T5 on T5.ID=T3.UnitID                                                           ");
            sql.AppendLine(" where T2.CompanyCD=@CompanyCD and T2.Status<>'3' and T2.BillStatus<>'1'");
            #region
            if (!string.IsNullOrEmpty(CustName))
            {
                sql.AppendLine(" and T4.CustName like @CustName ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustName","%"+CustName+"%"));
            }
          
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and T2.OrderDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and T2.OrderDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
        
            if (XValue.Trim() != "")
            {
                sql.AppendLine(" and  "+ColumnName+"=@XValue");
                comm.Parameters.Add(SqlHelper.GetParameter("@XValue",XValue));
            }
            #endregion
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD",CompanyCD));
            comm.CommandText = sql.ToString();
            return comm;
        }

    }
}
