using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;
using XBase.Data.DBHelper;
using XBase.Model.Office.StorageManager;
namespace XBase.Data.Office.StorageManager
{
    public class PurchaseApplyNoPassDBHelper
    {
        #region 采购不合格品统计表
        public static SqlCommand SearchPurApplyNoPass(string CompanyCD, string BeginDate, string EndDate, string ProductID, string ProviderID)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            //sql.AppendLine(" select A.*,B.CodeName as UnitName from                                              ");
            //sql.AppendLine(" (                                                                                   ");
            //sql.AppendLine(" select b.ProviderID,isnull(sum(a.NotPassCount),0)as NotPassNum,d.UnitID,d.ProductName            ");
            //sql.AppendLine(" ,d.Specification                                                         ");
            //sql.AppendLine(" ,isnull(Sum(a.ProductCount),0)as ProductCount                                       ");
            //sql.AppendLine(" from officedba.PurchaseArriveDetail as a left join                                  ");
            //sql.AppendLine(" officedba.PurchaseArrive as b on a.ArriveNo=b.ArriveNo and a.CompanyCD=b.CompanyCD  ");
            //sql.AppendLine(" left join officedba.ProviderInfo as c on b.ProviderID=c.ID                          ");
            //sql.AppendLine(" left join officedba.ProductInfo as d on a.ProductID=d.ID                            ");
            sql.AppendLine(" select Info.*,c.CustName,e.CodeName as UnitName,d.ProductName,d.Specification,b.ProviderID from             ");
            sql.AppendLine(" (                                                                                   ");
            sql.AppendLine("  select a.ProductID,a.ArriveNo,Convert(char(20),Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0)))+'&nbsp;' as NotPassNum            ");
            sql.AppendLine(" ,Convert(char(20),Convert(numeric(16," + point + "),isnull(Sum(a.ProductCount),0)))+'&nbsp;' as ProductCount                                       ");
            sql.AppendLine(" from officedba.PurchaseArriveDetail as a                                             ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD and a.NotPassCount>0 ");
            if (ProductID!="" && ProductID!="0")
            {
                sql.AppendLine(" and a.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID",ProductID));
            }
            sql.AppendLine(" group by a.ArriveNo,a.ProductID ) as Info");
            sql.AppendLine(" left join officedba.PurchaseArrive as b on Info.ArriveNo=b.ArriveNo ");
            sql.AppendLine(" left join officedba.ProviderInfo as c on b.ProviderID=c.ID                          ");
            sql.AppendLine(" left join officedba.ProductInfo as d on Info.ProductID=d.ID                            ");
            sql.AppendLine(" left join officedba.CodeUnitType as e on d.UnitID=e.ID                              ");
            sql.AppendLine(" where b.CompanyCD=@CompanyCD  and b.BillStatus<>'1' ");
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and b.ArriveDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and b.ArriveDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate",Convert.ToDateTime(EndDate).AddDays(1)));
            }
            if (ProviderID != "" && ProviderID != "0")
            {
                sql.AppendLine(" and b.ProviderID<=@ProviderID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProviderID", ProviderID));
            }   
         
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 生产不合格品统计表-----------------------Method=1为打印
        public static DataTable GetManNoPass(string Method, string CompanyCD, string BeginDate, string EndDate, string DeptID, string MyOrder, int PageIndex, int PageCount, ref string TotalProductCount, ref string TotalNotPassCount, ref int TotalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            //sql += " select a.ID,isnull((a.NotPassNum),0) as NotPassNum,isnull(d.CodeName,'') as CodeName,isnull(e.ProductName,'') as ProductName,  ";
            //sql += "        isnull(e.Specification,'') as Specification,isnull(f.CodeName,'') as UnitName,isnull(g.ProductCount,0) as ProductCount, ";
            //sql += " 	   isnull(i.DeptName,'') as DeptName                                                                                        ";
            //sql += " from   officedba.CheckNotPassDetail as a                                                                                       ";
            //sql += "        left join officedba.CheckNotPass as c on a.ProcessNo=c.ProcessNo                                                        ";
            //sql += "        left join officedba.QualityCheckReport as b on c.ReportID=b.ID                                                          ";
            //sql += "        left join officedba.CodeReasonType as d on a.ReasonID=d.ID                                                              ";
            //sql += "        left join officedba.ProductInfo as e on b.ProductID=e.ID                                                                ";
            //sql += "        left join officedba.CodeUnitType as f on e.UnitId=f.ID                                                                  ";
            //sql += "        left join officedba.ManufactureTask as h on  b.ReportID=h.ID                                                            ";
            //sql += "        left join officedba.ManufactureTaskDetail as g on g.TaskNo=h.TaskNo                                                     ";
            //sql += "        left join officedba.DeptInfo as i on h.DeptID=a.ID                                                                      ";
            //sql += " where 1=1  and (b.FromType='1' or  b.FromType='3') and c.CompanyCD=@CompanyCD and b.CompanyCD=@CompanyCD";
            sql.AppendLine("select A.*,B.CodeName as UnitName from                                                      ");
            sql.AppendLine("(                                                                               ");
            sql.AppendLine("select isnull(sum(a.NotPassCount),0)as NotPassNum,d.UnitID,c.DeptName         ");
            sql.AppendLine(",isnull(Sum(a.ProductedCount),0)as ProductCount,d.ProductName,d.Specification ");
            sql.AppendLine("from officedba.ManufactureTaskDetail as a left join                             ");
            sql.AppendLine("officedba.ManufactureTask as b on a.TaskNo=b.TaskNo and a.CompanyCD=b.CompanyCD ");
            sql.AppendLine("left join officedba.DeptInfo as c on b.DeptID=c.ID                              ");
            sql.AppendLine("left join officedba.ProductInfo as d on a.ProductID=d.ID                        ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD and a.NotPassCount>0 and b.BillStatus<>'1' ");
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and b.ConfirmDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and b.ConfirmDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (DeptID != "0" && DeptID != "" && DeptID != null)
            {
                sql.AppendLine(" and b.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", DeptID));
            }
            sql.AppendLine(" group by d.ProductName,d.UnitID,c.DeptName,d.Specification) as A                ");
            sql.AppendLine("left join officedba.CodeUnitType as B on A.UnitID=b.ID                          ");
            if (Method == "1")
            {
                sql.AppendLine(" order by " + MyOrder);
            }

            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));

            comm.CommandText = sql.ToString();
            DataTable myDt = new DataTable();
            if (Method == "1")
            {
                myDt = SqlHelper.ExecuteSearch(comm);
            }
            else
            {
                myDt = SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, MyOrder, ref TotalCount);
            }
            SqlCommand totalComm = new SqlCommand();
            StringBuilder TotalSql = new StringBuilder();
            TotalSql.AppendLine(" select isnull(sum(a.NotPassCount),0)as TotalNotPassNum                              ");
            TotalSql.AppendLine(" ,isnull(Sum(a.ProductedCount),0)as TotalProductCount                                ");
            TotalSql.AppendLine(" from officedba.ManufactureTaskDetail as a left join                              ");
            TotalSql.AppendLine(" officedba.ManufactureTask as b on a.TaskNo=b.TaskNo and a.CompanyCD=b.CompanyCD  ");
            TotalSql.AppendLine(" left join officedba.DeptInfo as c on b.DeptID=c.ID                               ");
            TotalSql.AppendLine(" left join officedba.ProductInfo as d on a.ProductID=d.ID                         ");
            TotalSql.AppendLine(" where a.CompanyCD=@CompanyCD and a.NotPassCount>0                                   ");

            if (!string.IsNullOrEmpty(BeginDate))
            {
                TotalSql.AppendLine(" and b.ConfirmDate>=@BeginDate");
                totalComm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                TotalSql.AppendLine(" and b.ConfirmDate<=@EndDate");
                totalComm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (DeptID != "0" && DeptID != "" && DeptID != null)
            {
                TotalSql.AppendLine(" and b.DeptID=@DeptID");
                totalComm.Parameters.Add(SqlHelper.GetParameter("@DeptID", DeptID));
            }
            totalComm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            totalComm.CommandText = TotalSql.ToString();
            DataTable theDt = SqlHelper.ExecuteSearch(totalComm);
            TotalNotPassCount = "0";
            TotalProductCount = "0";
            if (theDt.Rows.Count > 0)
            {
                TotalNotPassCount = theDt.Rows[0]["TotalNotPassNum"].ToString();
                TotalProductCount = theDt.Rows[0]["TotalProductCount"].ToString();
            }
            return myDt;

        }
        #endregion

        #region 不合格品处置统计表
        public static DataTable GetNoPass(string Method, string CompanyCD, string BeginDate, string EndDate, string ReasonID, string myOrder, int PageIndex, int PageCount, ref string TotalNotPassCount, ref int TotalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select isnull(a.NotPassNum,0) as NotPassNum,                       ");
            sql.AppendLine("        isnull(b.ProductName,'') as ProductName,                    ");
            sql.AppendLine(" 	    isnull(c.CodeName,'') as CodeName,                           ");
            sql.AppendLine("        isnull(b.Specification,'') as Specification,                ");
            sql.AppendLine("        case a.ProcessWay when 1 then '拒收' when 2 then '报废' when 3 then '降级' when 4 then '销毁' else '' end  as ProcessWay,    ");
            sql.AppendLine("        a.ID                                                        ");
            sql.AppendLine(" from   officedba.CheckNotPassDetail as a                           ");
            sql.AppendLine("        left join officedba.CheckNotPass as e on a.ProcessNo=e.ProcessNo and a.CompanyCD=e.CompanyCD ");
            sql.AppendLine("        left join officedba.QualityCheckReport as f  on f.ID=e.ReportID");
            sql.AppendLine("        left join officedba.ProductInfo as b on b.ID=f.ProductID    ");
            sql.AppendLine("        left join officedba.CodeUnitType as c on c.ID=b.UnitID      ");
            sql.AppendLine(" where  a.CompanyCD=@CompanyCD and e.BillStatus!='1'");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            if (BeginDate != "")
            {
                sql.AppendLine(" and e.ProcessDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (EndDate != "")
            {
                sql.AppendLine(" and e.ProcessDate<=@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (ReasonID != "0")
            {
                sql.AppendLine(" and a.ProcessWay=@ProcessWay");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProcessWay", ReasonID));
            }
            if (Method == "1")
            {
                sql.AppendLine(" order by " + myOrder);
            }

            comm.CommandText = sql.ToString();
            DataTable myDt = new DataTable();
            if (Method == "1")
            {
                myDt = SqlHelper.ExecuteSearch(comm);
            }
            else
            {
                myDt = SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, myOrder, ref TotalCount);
            }
            SqlCommand totalcomm = new SqlCommand();
            StringBuilder totalsql = new StringBuilder();
            totalsql.AppendLine(" select isnull(sum(a.NotPassNum),0) as TotalNotPassNum             ");
            totalsql.AppendLine(" from   officedba.CheckNotPassDetail as a                          ");
            totalsql.AppendLine("        left join officedba.CheckNotPass as e on a.ProcessNo=e.ProcessNo and e.CompanyCD=a.CompanyCD ");
            totalsql.AppendLine(" where  a.CompanyCD=@CompanyCD and e.BillStatus!='1' ");
            if (BeginDate != "")
            {
                totalsql.AppendLine(" and e.ProcessDate>=@BeginDate");
                totalcomm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (EndDate != "")
            {
                totalsql.AppendLine(" and e.ProcessDate<=@EndDate ");
                totalcomm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (ReasonID != "0")
            {
                totalsql.AppendLine(" and a.ProcessWay=@ProcessWay");
                totalcomm.Parameters.Add(SqlHelper.GetParameter("@ProcessWay", ReasonID));
            }
            totalcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            totalcomm.CommandText = totalsql.ToString();
            DataTable theDt = SqlHelper.ExecuteSearch(totalcomm);
            TotalNotPassCount = "0";
            if (theDt.Rows.Count > 0)
            {
                TotalNotPassCount = theDt.Rows[0]["TotalNotPassNum"].ToString();
            }
            return myDt;

        }
        #endregion




        #region 采购不合格按供应商分析
        public static DataTable GetNoPassByProvider(string CompanyCD, string BeginDate, string EndDate, string ProductID)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine(" select c.CustName,('采购数量：'+Convert(varchar,Convert(numeric(16," + point + "),isnull(Sum(a.ProductCount),0))))+',不合格数量：'+convert(varchar,Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0))) as TotalStr,Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0))as NotPassNum                          ");
            sql.AppendLine(" ,b.ProviderID                ");
            sql.AppendLine(" ,Convert(numeric(16," + point + "),isnull(Sum(a.ProductCount),0))as ProductCount                         ");
            sql.AppendLine(" from officedba.PurchaseArriveDetail as a left join                                  ");
            sql.AppendLine(" officedba.PurchaseArrive as b on a.ArriveNo=b.ArriveNo and a.CompanyCD=b.CompanyCD  ");
            sql.AppendLine(" left join officedba.ProviderInfo as c on b.ProviderID=c.ID                          ");
            sql.AppendLine(" left join officedba.ProductInfo as d on a.ProductID=d.ID                            ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD  and a.NotPassCount>0 and b.BillStatus<>'1' ");
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and b.ArriveDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and b.ArriveDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate",Convert.ToDateTime(EndDate).AddDays(1)));
            }
            if (ProductID != "0" && ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" and a.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            sql.AppendLine(" group by c.CustName,b.ProviderID           ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);


        }
        #endregion

        #region 采购不合格按产品分析
        public static DataTable GetNoPassByProduct(string CompanyCD, string BeginDate, string EndDate, string CustID)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine(" select isnull(d.ProductName,'') as ProductName,('采购数量：'+Convert(varchar,Convert(numeric(16," + point + "),isnull(Sum(a.ProductCount),0))))+',不合格数量：'+convert(varchar,Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0))) as TotalStr,Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0))as NotPassNum                          ");
            sql.AppendLine(" ,Convert(numeric(16," + point + "),isnull(Sum(a.ProductCount),0))as ProductCount,a.ProductID                         ");
            sql.AppendLine(" from officedba.PurchaseArriveDetail as a left join                                  ");
            sql.AppendLine(" officedba.PurchaseArrive as b on a.ArriveNo=b.ArriveNo and a.CompanyCD=b.CompanyCD  ");
            sql.AppendLine(" left join officedba.ProductInfo as d on a.ProductID=d.ID                            ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD  and a.NotPassCount>0 and b.BillStatus<>'1' ");
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and b.ArriveDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and b.ArriveDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate",Convert.ToDateTime(EndDate).AddDays(1)));
            }
            if (CustID != "0" && CustID != "")
            {
                sql.AppendLine(" and b.ProviderID=@ProviderID ");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProviderID", CustID));
            }
            sql.AppendLine(" group by d.ProductName,a.ProductID           ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 采购不合格按产品分析-详细信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="ProviderID"></param>
        /// <returns></returns>
        public static SqlCommand SearchPurApplyNoPassByProduct(string CompanyCD, string BeginDate, string EndDate, string ProductID, string ProviderID)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select A.*,B.CodeName as UnitName from                                              ");
            sql.AppendLine(" (                                                                                   ");
            sql.AppendLine(" select a.ProductID,Convert(char(20),Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0)))+'&nbsp;' as NotPassNum,d.UnitID,d.ProductName            ");
            sql.AppendLine(" ,d.Specification                                                         ");
            sql.AppendLine(" ,Convert(char(20),Convert(numeric(16," + point + "),isnull(Sum(a.ProductCount),0)))+'&nbsp;' as ProductCount                                       ");
            sql.AppendLine(" from officedba.PurchaseArriveDetail as a left join                                  ");
            sql.AppendLine(" officedba.PurchaseArrive as b on a.ArriveNo=b.ArriveNo and a.CompanyCD=b.CompanyCD  ");
            sql.AppendLine(" left join officedba.ProductInfo as d on a.ProductID=d.ID                            ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD  and a.NotPassCount>0 and b.BillStatus<>'1' ");
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and b.ArriveDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and b.ArriveDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate",Convert.ToDateTime(EndDate).AddDays(1)));
            }
            if (ProviderID != "" && ProviderID != "0")
            {
                sql.AppendLine(" and b.ProviderID<=@ProviderID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProviderID", ProviderID));
            }
            if (ProductID != "" && ProductID != "0")
            {
                sql.AppendLine(" and a.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            sql.AppendLine(" group by a.ProductID,d.ProductName,d.UnitID,d.Specification) as A            ");
            sql.AppendLine(" left join officedba.CodeUnitType as B on a.UnitID=b.ID                              ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion


        #region 采购不合格走势
        public static DataTable GetNoPassTendency(string CompanyCD, string BeginDate, string EndDate, string ProductID, string CustID, string Type)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            string Column = "";
            sql.AppendLine(" select * from ( select                          ");
            if (Type == "3")//周
            {
                Column = "datename(yyyy,b.ArriveDate)+'年第'+datename(week,b.ArriveDate)+'周'";
            }
            if (Type == "2")//月
            {
                Column = "datename(yyyy,b.ArriveDate)+'年'+datename(mm,b.ArriveDate)+'月'";
            }
            if (Type == "1")//年
            {
                Column = "datename(yyyy,b.ArriveDate)+'年'";
            }
            sql.AppendLine("(" + Column + ") as TheDate,('采购数量：'+Convert(varchar,Convert(numeric(16," + point + "),isnull(Sum(a.ProductCount),0))))+',不合格数量：'+convert(varchar,Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0))) as TotalStr ");
            sql.AppendLine(" ,Convert(numeric(16," + point + "),isnull(Sum(a.ProductCount),0))as ProductCount,Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0)) as NotPassNum                          ");
            sql.AppendLine(" from officedba.PurchaseArriveDetail as a left join                                  ");
            sql.AppendLine(" officedba.PurchaseArrive as b on a.ArriveNo=b.ArriveNo and a.CompanyCD=b.CompanyCD  ");
            sql.AppendLine(" left join officedba.ProviderInfo as c on b.ProviderID=c.ID                          ");
            sql.AppendLine(" left join officedba.ProductInfo as d on a.ProductID=d.ID                            ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD  and a.NotPassCount>0 and b.BillStatus<>'1' ");
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and b.ArriveDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and b.ArriveDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (ProductID != "0" && ProductID != "")
            {
                sql.AppendLine(" and a.ProductID=@ProductID ");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            if (CustID != "" && CustID != "0")
            {
                sql.AppendLine(" and b.ProviderID=@ProviderID ");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProviderID", CustID));
            }
            sql.AppendLine(" group by " + Column+" ) as TT");
            sql.AppendLine(" order by TheDate asc ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 采购不合格走势-明细
        /// </summary>
        /// <param name="Method"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="ProviderID"></param>
        /// <param name="XValue"></param>
        /// <returns></returns>
        public static SqlCommand GetNoPassTendencyDetail(string Method, string BeginDate, string EndDate, string ProductID, string ProviderID, string XValue)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            string ColumnName = "";
            if (Method == "1")
            {
                ColumnName = "datename(yyyy,T2.ArriveDate)+'年'";
            }
            if (Method == "2")
            {
                ColumnName = "datename(yyyy,T2.ArriveDate)+'年'+datename(mm,T2.ArriveDate)+'月'";
            }
            if (Method == "3")
            {
                ColumnName = "datename(yyyy,T2.ArriveDate)+'年第'+datename(week,T2.ArriveDate)+'周'";
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select T1.*,isnull(T3.EmployeeName,'') as PurchaserName,isnull(T4.CustName,'') as ProviderName  ");
            if (ColumnName.Trim() != "")
            {
                sql.AppendLine(",(" + ColumnName + ") as TheDate");
            }
            sql.AppendLine(" ,isnull(T2.Title,'') as Title from (                                                                                ");
            sql.AppendLine(" select a.ArriveNo,Convert(char(20),Convert(numeric(16," + point + "),sum(ISNULL(a.ProductCount,0))))+'&nbsp;' as ProductCount,Convert(char(20),Convert(numeric(16," + point + "),sum(ISNULL(a.NotPassCount,0))))+'&nbsp;' as NoPassCount         ");
            sql.AppendLine(" from officedba.PurchaseArriveDetail as a                                                        ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD    and a.NotPassCount>0                                                                 ");
            if (!string.IsNullOrEmpty(ProductID) && ProductID != "0")
            {
                sql.AppendLine(" and  a.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            sql.AppendLine(" group by a.ArriveNo                                                                             ");
            sql.AppendLine(" ) as T1                                                                                         ");
            sql.AppendLine(" left join officedba.PurchaseArrive as T2 on T1.ArriveNo=T2.ArriveNo                             ");
            sql.AppendLine(" left join officedba.EmployeeInfo as T3 on T3.ID=T2.Purchaser                                    ");
            sql.AppendLine(" left join officedba.ProviderInfo as T4 on T2.ProviderID=T4.ID                                   ");
            sql.AppendLine("                                                                                                 ");
            sql.AppendLine(" where T2.CompanyCD=@CompanyCD  and T2.BillStatus<>'1'                                                                 ");
            #region 条件
            if (!string.IsNullOrEmpty(XValue))
            {
                sql.AppendLine(" and " + ColumnName + "=@XValue");
                comm.Parameters.Add(SqlHelper.GetParameter("@XValue", XValue));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and T2.ArriveDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and T2.ArriveDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate",Convert.ToDateTime(EndDate).AddDays(1)));
            }
            if (!string.IsNullOrEmpty(ProviderID) && ProviderID != "0")
            {
                sql.AppendLine(" and T2.ProviderID=@ProviderID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProviderID", ProviderID));
            }
            #endregion
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 生产不合格按部门分析
        public static DataTable GetManNoPassByDept(string CompanyCD, string BeginDate, string EndDate, string ProductID)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine("select c.DeptName,('生产数量：'+Convert(varchar,Convert(numeric(16," + point + "),isnull(Sum(a.ProductedCount),0))))+',不合格数量：'+convert(varchar,Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0))) as TotalStr,Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0))as NotPassNum,b.DeptID         ");
            sql.AppendLine(",Convert(numeric(16," + point + "),isnull(Sum(a.ProductedCount),0))as ProductCount ");
            sql.AppendLine("from officedba.ManufactureTaskDetail as a left join                             ");
            sql.AppendLine("officedba.ManufactureTask as b on a.TaskNo=b.TaskNo and a.CompanyCD=b.CompanyCD ");
            sql.AppendLine("left join officedba.DeptInfo as c on b.DeptID=c.ID                              ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD and a.NotPassCount>0 and b.BillStatus<>'1' ");
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and b.ConfirmDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and b.ConfirmDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (ProductID != "0" && ProductID != "" && ProductID != null)
            {
                sql.AppendLine(" and a.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            sql.AppendLine(" group by c.DeptName,b.DeptID    ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 生产不合格按部门分析-详细信息
        /// </summary>
        /// <param name="Method"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="XValue"></param>
        /// <returns></returns>
        public static SqlCommand GetManNoPassByDeptDetail(string BeginDate, string EndDate, string ProductID, string DeptID)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select T1.*,isnull(T3.EmployeeName,'') as EmployeeName,isnull(T4.DeptName,'') as DeptName  ");

            sql.AppendLine(" ,isnull(T2.Subject,'') as Title,T2.DeptID,case T2.ManufactureType when '0' then '普通' when '1' then '返修' when '2' then '拆件' else '' end as ManufactureTypeName from (                                                                                ");
            sql.AppendLine(" select a.TaskNo,Convert(char(20),Convert(numeric(16," + point + "),isnull(sum(a.ProductedCount),0)))+'&nbsp;' as ProductCount,Convert(char(20),Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0)))+'&nbsp;' as NoPassCount         ");
            sql.AppendLine(" from officedba.ManufactureTaskDetail as a                                                        ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD    and a.NotPassCount>0                                                                 ");
            if (!string.IsNullOrEmpty(ProductID) && ProductID != "0")
            {
                sql.AppendLine(" and  a.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            sql.AppendLine(" group by a.TaskNo                                                                             ");
            sql.AppendLine(" ) as T1                                                                                         ");
            sql.AppendLine(" left join officedba.ManufactureTask as T2 on T1.TaskNo=T2.TaskNo                              ");
            sql.AppendLine(" left join officedba.EmployeeInfo as T3 on T3.ID=T2.Principal                                    ");
            sql.AppendLine(" left join officedba.DeptInfo as T4 on T2.DeptID=T4.ID                                   ");
            sql.AppendLine("                                                                                                 ");
            sql.AppendLine(" where T2.CompanyCD=@CompanyCD  and T2.BillStatus<>'1'                                                                 ");
            #region 条件
            if (!string.IsNullOrEmpty(DeptID) && DeptID != "0")
            {
                sql.AppendLine(" and T2.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", DeptID));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and T2.ConfirmDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and T2.ConfirmDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            #endregion
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 生产不合格按产品分析
        public static DataTable GetManNoPassByProduct(string CompanyCD, string BeginDate, string EndDate, string DeptID)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine("select d.ProductName,('生产数量：'+Convert(varchar,Convert(numeric(16," + point + "),isnull(Sum(a.ProductedCount),0))))+',不合格数量：'+convert(varchar,Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0))) as TotalStr,Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0)) as NotPassNum,a.ProductID         ");
            sql.AppendLine(",Convert(numeric(16," + point + "),isnull(Sum(a.ProductedCount),0)) as ProductCount ");
            sql.AppendLine("from officedba.ManufactureTaskDetail as a left join                             ");
            sql.AppendLine("officedba.ManufactureTask as b on a.TaskNo=b.TaskNo and a.CompanyCD=b.CompanyCD ");
            sql.AppendLine("left join officedba.ProductInfo as d on a.ProductID=d.ID                        ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD and a.NotPassCount>0 and b.BillStatus<>'1' ");
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and b.ConfirmDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and b.ConfirmDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (DeptID != "0" && DeptID != "" && DeptID != null)
            {
                sql.AppendLine(" and b.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", DeptID));
            }
            sql.AppendLine(" group by d.ProductName,a.ProductID    ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 生产不合格按产品分析-详细信息
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static SqlCommand GetManNoPassByProductDetail(string BeginDate, string EndDate, string ProductID, string DeptID)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select T1.*,isnull(T3.EmployeeName,'') as EmployeeName,isnull(T4.DeptName,'') as DeptName,T5.ProductName  ");

            sql.AppendLine(" ,isnull(T2.Subject,'') as Title,T2.DeptID,case T2.ManufactureType when '0' then '普通' when '1' then '返修' when '2' then '拆件' else '' end as ManufactureTypeName from (                                                                                ");
            sql.AppendLine(" select a.TaskNo,a.ProductID,Convert(char(20),Convert(numeric(16," + point + "),isnull(sum(a.ProductedCount),0)))+'&nbsp;' as ProductCount,Convert(char(20),Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0)))+'&nbsp;' as NoPassCount         ");
            sql.AppendLine(" from officedba.ManufactureTaskDetail as a                                                        ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD    and a.NotPassCount>0                                                                 ");
            if (!string.IsNullOrEmpty(ProductID) && ProductID != "0")
            {
                sql.AppendLine(" and  a.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            sql.AppendLine(" group by a.TaskNo,a.ProductID                                                                             ");
            sql.AppendLine(" ) as T1                                                                                         ");
            sql.AppendLine(" left join officedba.ManufactureTask as T2 on T1.TaskNo=T2.TaskNo                              ");
            sql.AppendLine(" left join officedba.EmployeeInfo as T3 on T3.ID=T2.Principal                                    ");
            sql.AppendLine(" left join officedba.DeptInfo as T4 on T2.DeptID=T4.ID                                   ");
            sql.AppendLine(" left join officedba.ProductInfo as T5 on T5.ID=T1.ProductID ");
            sql.AppendLine("                                                                                                 ");
            sql.AppendLine(" where T2.CompanyCD=@CompanyCD  and T2.BillStatus<>'1'                                                                 ");
            #region 条件
            if (!string.IsNullOrEmpty(DeptID) && DeptID != "0")
            {
                sql.AppendLine(" and T2.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", DeptID));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and T2.ConfirmDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and T2.ConfirmDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            #endregion
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 生产不合格走势
        public static DataTable GetManNoPassTendency(string CompanyCD, string BeginDate, string EndDate, string ProductID, string DeptID, string Type)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            string Column = "";
            sql.AppendLine(" select * from (select                           ");
            if (Type == "3")//周
            {
                Column = "(datename(yyyy,b.ConfirmDate)+'年第'+datename(week,b.ConfirmDate)+'周')";
            }
            if (Type == "2")//月
            {
                Column = "(datename(yyyy,b.ConfirmDate)+'年'+datename(mm,b.ConfirmDate)+'月')";
            }
            if (Type == "1")//年
            {
                Column = "(datename(yyyy,b.ConfirmDate)+'年')";
            }
            sql.AppendLine("(" + Column + ") as TheDate ");
            sql.AppendLine(" ,('生产数量：'+Convert(varchar,Convert(numeric(16," + point + "),isnull(Sum(a.ProductedCount),0))))+',不合格数量：'+convert(varchar,Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0))) as TotalStr,Convert(numeric(16," + point + "),isnull(Sum(a.ProductedCount),0))as ProductCount,Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0))as NotPassNum                         ");
            sql.AppendLine(" from officedba.ManufactureTaskDetail as a left join                                  ");
            sql.AppendLine(" officedba.ManufactureTask as b on a.TaskNo=b.TaskNo and a.CompanyCD=b.CompanyCD  ");
            sql.AppendLine(" left join officedba.DeptInfo as c on b.DeptID=c.ID                          ");
            sql.AppendLine(" left join officedba.ProductInfo as d on a.ProductID=d.ID                            ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD  and a.NotPassCount>0 and b.BillStatus<>'1' ");
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and b.ConfirmDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and b.ConfirmDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (ProductID != "0" && ProductID != "")
            {
                sql.AppendLine(" and a.ProductID=@ProductID ");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            if (DeptID != "" && DeptID != "0")
            {
                sql.AppendLine(" and b.DeptID=@DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", DeptID));
            }
            sql.AppendLine(" group by " + Column+" ) as TT ");
            sql.AppendLine(" order by TheDate asc ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 生产不合格走势--明细
        /// </summary>
        /// <param name="TimeType"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="DeptID"></param>
        /// <param name="XValue"></param>
        /// <returns></returns>
        public static SqlCommand GetManNoPassTendencyDetail(string TimeType, string BeginDate, string EndDate, string ProductID, string DeptID, string XValue)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            string Column = "";
            if (TimeType == "3")//周
            {
                Column = "(datename(yyyy,T2.ConfirmDate)+'年第'+datename(week,T2.ConfirmDate)+'周')";
            }
            if (TimeType == "2")//月
            {
                Column = "(datename(yyyy,T2.ConfirmDate)+'年'+datename(mm,T2.ConfirmDate)+'月')";
            }
            if (TimeType == "1")//年
            {
                Column = "(datename(yyyy,T2.ConfirmDate)+'年')";
            }

            sql.AppendLine(" select T1.*,isnull(T3.EmployeeName,'') as EmployeeName,isnull(T4.DeptName,'') as DeptName,T5.ProductName  ");
            if (Column.Trim() != "")
            {
                sql.AppendLine(",(" + Column + ") as TheDate");
            }
            sql.AppendLine(" ,isnull(T2.Subject,'') as Title,T2.DeptID,case T2.ManufactureType when '0' then '普通' when '1' then '返修' when '2' then '拆件' else '' end as ManufactureTypeName from (                                                                                ");
            sql.AppendLine(" select a.TaskNo,a.ProductID,Convert(char(20),Convert(numeric(16," + point + "),isnull(sum(a.ProductedCount),0)))+'&nbsp;' as ProductCount,Convert(char(20),Convert(numeric(16," + point + "),isnull(sum(a.NotPassCount),0)))+'&nbsp;' as NoPassCount         ");
            sql.AppendLine(" from officedba.ManufactureTaskDetail as a                                                        ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD    and a.NotPassCount>0                                                                 ");
            if (!string.IsNullOrEmpty(ProductID) && ProductID != "0")
            {
                sql.AppendLine(" and  a.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            sql.AppendLine(" group by a.TaskNo,a.ProductID                                                                             ");
            sql.AppendLine(" ) as T1                                                                                         ");
            sql.AppendLine(" left join officedba.ManufactureTask as T2 on T1.TaskNo=T2.TaskNo                              ");
            sql.AppendLine(" left join officedba.EmployeeInfo as T3 on T3.ID=T2.Principal                                    ");
            sql.AppendLine(" left join officedba.DeptInfo as T4 on T2.DeptID=T4.ID                                   ");
            sql.AppendLine(" left join officedba.ProductInfo as T5 on T5.ID=T1.ProductID ");
            sql.AppendLine("                                                                                                 ");
            sql.AppendLine(" where T2.CompanyCD=@CompanyCD  and T2.BillStatus<>'1'                                                                 ");
            #region 条件
            if (!string.IsNullOrEmpty(DeptID) && DeptID != "0")
            {
                sql.AppendLine(" and T2.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", DeptID));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and T2.ConfirmDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and T2.ConfirmDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (XValue.Trim() != "")
            {
                sql.AppendLine(" and " + Column + "=@XValue");
                comm.Parameters.Add(SqlHelper.GetParameter("@XValue", XValue));
            }
            #endregion
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 不合格产品处置分布
        public static DataTable GetNoPassNum(string CompanyCD, string BeginDate, string EndDate, string ProductID)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select                        ");
            sql.AppendLine("        case a.ProcessWay when 1 then '拒收' when 2 then '报废' when 3 then '降级' when 4 then '销毁' else '' end  as ProcessWayName,Convert(numeric(16," + point + "),sum(isnull(a.NotPassNum,0))) as NotPassNum,a.ProcessWay    ");
            sql.AppendLine(" from   officedba.CheckNotPassDetail as a                           ");
            sql.AppendLine("        left join officedba.CheckNotPass as e on a.ProcessNo=e.ProcessNo and a.CompanyCD=e.CompanyCD ");
            sql.AppendLine("        left join officedba.QualityCheckReport as f  on f.ID=e.ReportID");
            sql.AppendLine(" where  a.CompanyCD=@CompanyCD and e.BillStatus!='1'");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            if (BeginDate != "")
            {
                sql.AppendLine(" and e.ProcessDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (EndDate != "")
            {
                sql.AppendLine(" and e.ProcessDate<=@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (ProductID != "0" && ProductID!="")
            {
                sql.AppendLine(" and f.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            sql.AppendLine(" group by a.ProcessWay ");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        #region  不合格产品处置分布-导出
        public static DataTable GetOutNoPassNum(string CompanyCD, string BeginDate, string EndDate, string ProductID)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select Convert(char(20),Convert(numeric(16," + point + "),isnull(a.NotPassNum,0)))+'&nbsp;' as NotPassNum,                       ");
            sql.AppendLine("        isnull(b.ProductName,'') as ProductName,                    ");
            sql.AppendLine(" 	    isnull(c.CodeName,'') as CodeName,                           ");
            sql.AppendLine("        isnull(b.Specification,'') as Specification,                ");
            sql.AppendLine("        case a.ProcessWay when 1 then '拒收' when 2 then '报废' when 3 then '降级' when 4 then '销毁' else '' end  as ProcessWayName,a.ProcessWay    ");
            sql.AppendLine(" from   officedba.CheckNotPassDetail as a                           ");
            sql.AppendLine("        left join officedba.CheckNotPass as e on a.ProcessNo=e.ProcessNo and a.CompanyCD=e.CompanyCD ");
            sql.AppendLine("        left join officedba.QualityCheckReport as f  on f.ID=e.ReportID");
            sql.AppendLine("        left join officedba.ProductInfo as b on b.ID=f.ProductID    ");
            sql.AppendLine("        left join officedba.CodeUnitType as c on c.ID=b.UnitID      ");
            sql.AppendLine(" where  a.CompanyCD=@CompanyCD and e.BillStatus!='1'");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            if (BeginDate != "")
            {
                sql.AppendLine(" and e.ProcessDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (EndDate != "")
            {
                sql.AppendLine(" and e.ProcessDate<=@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (ProductID != "0" && ProductID != "")
            {
                sql.AppendLine(" and f.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 不合格产品处置分布-链接到详细信息
        public static DataTable GetNoPassDetail(string CompanyCD, string BeginDate, string EndDate, string ProductID, string ProcessWay, int PageIndex, int PageCount, string OrderBy, ref int TotalCount)
        {
            string point = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select Convert(numeric(16," + point + "),isnull(a.NotPassNum,0)) as NotPassNum,                       ");
            sql.AppendLine("        isnull(b.ProductName,'') as ProductName,                    ");
            sql.AppendLine(" 	    isnull(c.CodeName,'') as CodeName,                           ");
            sql.AppendLine("        isnull(b.Specification,'') as Specification,                ");
            sql.AppendLine("        case a.ProcessWay when 1 then '拒收' when 2 then '报废' when 3 then '降级' when 4 then '销毁' else '' end  as ProcessWayName,a.ProcessWay    ");
            sql.AppendLine(" from   officedba.CheckNotPassDetail as a                           ");
            sql.AppendLine("        left join officedba.CheckNotPass as e on a.ProcessNo=e.ProcessNo and a.CompanyCD=e.CompanyCD ");
            sql.AppendLine("        left join officedba.QualityCheckReport as f  on f.ID=e.ReportID");
            sql.AppendLine("        left join officedba.ProductInfo as b on b.ID=f.ProductID    ");
            sql.AppendLine("        left join officedba.CodeUnitType as c on c.ID=b.UnitID      ");
            sql.AppendLine(" where  a.CompanyCD=@CompanyCD and e.BillStatus!='1'");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            if (BeginDate != "")
            {
                sql.AppendLine(" and e.ProcessDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (EndDate != "")
            {
                sql.AppendLine(" and e.ProcessDate<=@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (ProductID != "0" && ProductID != "")
            {
                sql.AppendLine(" and f.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            if (!string.IsNullOrEmpty(ProcessWay))
            {
                sql.AppendLine(" and a.ProcessWay=@ProcessWay");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProcessWay", ProcessWay));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, OrderBy, ref TotalCount);
        }
        /// <summary>
        /// 详细信息的导出
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ProductID"></param>
        /// <param name="ProcessWay"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public static DataTable GetNoPassDetailOut(string CompanyCD, string BeginDate, string EndDate, string ProductID, string ProcessWay, string OrderBy)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select isnull(a.NotPassNum,0) as NotPassNum,                       ");
            sql.AppendLine("        isnull(b.ProductName,'') as ProductName,                    ");
            sql.AppendLine(" 	    isnull(c.CodeName,'') as CodeName,                           ");
            sql.AppendLine("        isnull(b.Specification,'') as Specification,                ");
            sql.AppendLine("        case a.ProcessWay when 1 then '拒收' when 2 then '报废' when 3 then '降级' when 4 then '销毁' else '' end  as ProcessWayName,a.ProcessWay    ");
            sql.AppendLine(" from   officedba.CheckNotPassDetail as a                           ");
            sql.AppendLine("        left join officedba.CheckNotPass as e on a.ProcessNo=e.ProcessNo and a.CompanyCD=e.CompanyCD ");
            sql.AppendLine("        left join officedba.QualityCheckReport as f  on f.ID=e.ReportID");
            sql.AppendLine("        left join officedba.ProductInfo as b on b.ID=f.ProductID    ");
            sql.AppendLine("        left join officedba.CodeUnitType as c on c.ID=b.UnitID      ");
            sql.AppendLine(" where  a.CompanyCD=@CompanyCD and e.BillStatus!='1'");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            if (BeginDate != "")
            {
                sql.AppendLine(" and e.ProcessDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (EndDate != "")
            {
                sql.AppendLine(" and e.ProcessDate<=@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (ProductID != "0" && ProductID != "")
            {
                sql.AppendLine(" and f.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            if (!string.IsNullOrEmpty(ProcessWay))
            {
                sql.AppendLine(" and a.ProcessWay=@ProcessWay");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProcessWay", ProcessWay));
            }
            sql.AppendLine(" order by " + OrderBy);
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

    }
}
