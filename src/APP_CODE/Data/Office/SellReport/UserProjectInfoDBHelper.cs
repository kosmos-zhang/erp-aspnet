using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
namespace XBase.Data.Office.SellReport
{
    public class UserProjectInfoDBHelper
    {
        public static int Add(XBase.Model.Office.SellReport.UserProductInfo model)
        {
            int num = 0;
            string sqlstr = "insert into officedba.UserProductInfo(CompanyCD,productNum,productName,price,bref,memo) values(@CompanyCD,@productNum,@productName,@price,@bref,@memo)";
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@productNum",SqlDbType.VarChar,50),
                                       new SqlParameter("@productName",SqlDbType.VarChar,200),
                                       new SqlParameter("@price",SqlDbType.Decimal),
                                       new SqlParameter("@bref",SqlDbType.VarChar,500),
                                       new SqlParameter("@memo",SqlDbType.VarChar,1000)
                                   };
            param[0].Value = model.CompanyCD;
            param[1].Value = model.productNum;
            param[2].Value = model.productName;
            param[3].Value = model.price;
            param[4].Value = model.bref;
            param[5].Value = model.memo;
            try
            {
                num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr, param);
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            return num;
        }

        public static DataTable DataList(int pageindex, int pagecount, string OrderBy, XBase.Common.UserInfoUtil userinfo, ref int totalCount)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("select id,CompanyCD,productNum,productName,convert(decimal(22,"+userinfo.SelPoint+"),price) price,bref,memo from officedba.UserProductInfo where CompanyCD=@CompanyCD");
            
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", userinfo.CompanyCD));
            
            comm.CommandText = sqlstr.ToString();
            return SqlHelper.PagerWithCommand(comm, pageindex, pagecount, OrderBy, ref totalCount);
        }

        public static DataTable GetDataDetailByID(string id,XBase.Common.UserInfoUtil userinfo)
        {
            SqlParameter[] param = { new SqlParameter("@ID",SqlDbType.VarChar,50)};
            param[0].Value = id;
            return SqlHelper.ExecuteSql("select id,CompanyCD,productNum,productName,convert(decimal(22," + userinfo.SelPoint + "),price) price,bref,memo from officedba.UserProductInfo where ID=@ID", param);
        }

        public static int Update(XBase.Model.Office.SellReport.UserProductInfo model)
        {
            int num = 0;
            string sqlstr = "update officedba.UserProductInfo set productNum=@productNum,productName=@productName,price=@price,bref=@bref,memo=@memo where id=@id";
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@productNum",SqlDbType.VarChar,50),
                                       new SqlParameter("@productName",SqlDbType.VarChar,200),
                                       new SqlParameter("@price",SqlDbType.Decimal),
                                       new SqlParameter("@bref",SqlDbType.VarChar,500),
                                       new SqlParameter("@memo",SqlDbType.VarChar,1000),
                                       new SqlParameter("@id",SqlDbType.Int,4)
                                   };
            param[0].Value = model.CompanyCD;
            param[1].Value = model.productNum;
            param[2].Value = model.productName;
            param[3].Value = model.price;
            param[4].Value = model.bref;
            param[5].Value = model.memo;
            param[6].Value = model.id;
            try
            {
                num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr, param);
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            return num;
        }

        public static int Delete(string idlist)
        {
            int num = 0;
            string sqlstr = "delete from officedba.UserProductInfo where id in (" + idlist + ")";
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr);
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            return num;
        }

        public static DataSet GetSummaryData(string begintime, string endtime, XBase.Common.UserInfoUtil userinfo,string orderflag,int summarytype,string point)
        {
            StringBuilder sb = new StringBuilder();
            switch(summarytype)
            {
                case 1:    //按部门统计
                    sb.AppendLine("select A.DeptName,B.PriceTotal,B.NumTotal from officedba.deptinfo A");
                    sb.AppendLine("right join");
                    sb.AppendLine("(");
                    sb.AppendLine(" select SellDept,convert(decimal(22,"+point+"),sum(sellPrice)) priceTotal,convert(decimal(22,"+point+"),sum(sellNum)) NumTotal from officedba.SellReport");
                    sb.AppendLine(" where CompanyCD=@companyCD and convert(char(10),createdate,120) between @begintime and @endtime");
                    sb.AppendLine(" group by SellDept");
                    sb.AppendLine(")B on A.ID=B.SellDept");
                    if (orderflag == "0")
                    {
                        sb.AppendLine("order by B.PriceTotal desc,B.NumTotal DESC");
                    }
                    else
                    {
                        sb.AppendLine("order by B.NumTotal desc,B.PriceTotal DESC");
                    }
                break;

                case 2:   //按业务员业绩统计
                    sb.AppendLine("select C.EmployeeName DeptName,D.priceTotal,D.NumTotal from officedba.EmployeeInfo C right join");
                    sb.AppendLine("(");
	                sb.AppendLine("    select sellerID,convert(decimal(22,"+point+"),sum(B.sellPrice*A.rate)) priceTotal,convert(decimal(22,"+point+"),sum(B.sellNum)) NumTotal from officedba.sellerRate A left join officedba.Sellreport B on A.sellreportID=B.ID");
                    sb.AppendLine(" where A.CompanyCD=@companyCD and convert(char(10),createdate,120) between @begintime and @endtime");
	                sb.AppendLine(" group by sellerID");
                    sb.AppendLine(")D on C.ID=D.sellerID");
                    if (orderflag == "0")
                    {
                        sb.AppendLine("order by D.PriceTotal desc,D.NumTotal DESC");
                    }
                    else
                    {
                        sb.AppendLine("order by D.NumTotal desc,D.PriceTotal DESC");
                    }
                    break;

                case 3:    //按产品统计
                    sb.AppendLine("select productName DeptName,convert(decimal(22,"+point+"),sum(sellPrice)) priceTotal ,convert(decimal(22,"+point+"),sum(sellNum)) NumTotal  from officedba.SellReport");
                    sb.AppendLine("where CompanyCD=@companyCD and convert(char(10),createdate,120) between @begintime and @endtime");
                    sb.AppendLine("group by ProductName ");
                    if (orderflag == "0")
                    {
                        sb.AppendLine("order by PriceTotal desc,NumTotal DESC");
                    }
                    else
                    {
                        sb.AppendLine("order by NumTotal desc,PriceTotal DESC");
                    }
                break;
            }
        
            SqlParameter[] param = { new SqlParameter("@companyCD",SqlDbType.VarChar,50),
                                     new SqlParameter("@begintime",SqlDbType.VarChar,30),
                                     new SqlParameter("@endtime",SqlDbType.VarChar,30)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = begintime;
            param[2].Value = endtime;
            return SqlHelper.ExecuteSqlX(sb.ToString(), param);
        }
    }
}
