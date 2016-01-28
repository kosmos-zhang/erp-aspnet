/***********************************
 * 描述：销售状况分析
 * 创建人：何小武
 * 创建时间：2010-6-1
 * ********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using XBase.Data.DBHelper;

namespace XBase.Data.OperatingModel.DecisionData
{
    public class SalesFiguresAnalysisDBHelper
    {
        #region 获取销售状况分析结果列表
        /// <summary>
        /// 获取销售状况分析结果列表
        /// </summary>
        /// <param name="model">检索条件实体</param>
        /// <param name="userinfo">session用户信息</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetSalesFiguresList(XBase.Model.Office.OperatingModel.SalesFiguresAnalysisModel model, XBase.Common.UserInfoUtil userinfo,int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            
            //销售订单
            strSql.AppendLine("select c.ProdNo,c.ProductName,c.Specification,");
            strSql.AppendLine("isnull(d.CodeName,'') as UnitName,");
            strSql.AppendLine("e.EmployeeName as DealerName,isnull(f.CustName,'') as CustName,a.seller,");
            strSql.AppendLine("convert(decimal(22," + userinfo.SelPoint + "),isnull(sum(a.sProductCount),0)) as SellProductCount,");
            strSql.AppendLine("convert(decimal(22," + userinfo.SelPoint + "),isnull(sum(a.sTotalFee*a.newRate),0)) as SellTotal,");
            strSql.AppendLine("convert(decimal(22," + userinfo.SelPoint + "),isnull(sum(a.backCount),0)) as SellBackCount,");
            strSql.AppendLine("convert(decimal(22," + userinfo.SelPoint + "),isnull(sum(a.backTotal*a.newRate),0)) as SellBackTotal");
            strSql.AppendLine("from ");
            strSql.AppendLine("(select s.ProductID,isnull(s.TotalFee,0) as sTotalFee ,0 as backTotal,0 as backCount,");
            if (userinfo.IsMoreUnit)
            {
                strSql.AppendLine(" s.UsedUnitID as UnitID,isnull(s.UsedUnitCount,0) as sProductCount,");
            }
            else
            {
                strSql.AppendLine(" s.UnitID,isnull(s.ProductCount,0) as sProductCount,");
            }
            strSql.AppendLine("	s2.Seller,s2.CustID,s2.OrderDate as DealDate,isnull(s2.Rate,0) as newRate");
            strSql.AppendLine("from officedba.sellorderdetail s ");
            strSql.AppendLine("left join officedba.sellorder s2 on s2.OrderNo=s.OrderNo and s2.CompanyCD=s.CompanyCD");
            strSql.AppendLine("where s2.BillStatus='2' and s.CompanyCD=@CompanyCD");
            strSql.AppendLine("union all ");
            //销售退货
            strSql.AppendLine("select s3.ProductID,0 as sTotalFee,isnull(s3.TotalFee,0) as backTotal,");
            if (userinfo.IsMoreUnit)
            {
                strSql.AppendLine(" isnull(s3.UsedUnitCount,0) as backCount,s3.UsedUnitID as UnitID,");
            }
            else
            {
                strSql.AppendLine(" isnull(s3.ProductCount,0) as backCount,s3.UnitID,");
            }
            strSql.AppendLine(" 0 as sProductCount,");
            strSql.AppendLine("	s4.Seller,s4.CustID,s4.BackDate as DealDate,isnull(s4.Rate,0) as newRate");
            strSql.AppendLine("from officedba.sellbackdetail s3 ");
            strSql.AppendLine("left join officedba.sellback s4 on s4.BackNo=s3.BackNo and s4.CompanyCD=s3.CompanyCD");
            strSql.AppendLine("where s4.BillStatus='2' and s3.CompanyCD=@CompanyCD");
            strSql.AppendLine(") as a ");
            strSql.AppendLine("left join officedba.productInfo c on c.ID=a.ProductID ");
            strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UnitID");
            strSql.AppendLine("left join officedba.EmployeeInfo e on e.ID=a.Seller");
            strSql.AppendLine("left join officedba.CustInfo f on f.ID=a.CustID");
            strSql.AppendLine(" where 1=1 ");
            SqlCommand comm = new SqlCommand();

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",model.CompanyCD));
            //物品编号
            if (model.ProductNo != null)
            {
                strSql.AppendLine(" and c.ProdNo=@ProductNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", model.ProductNo));
            }
            //物品名称
            if (model.ProductName != null)
            {
                strSql.AppendLine(" and c.ProductName like @ProductName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%"+model.ProductName+"%"));
            }
            //客户
            if (model.CustID != null)
            {
                strSql.AppendLine(" and a.CustID=@CustID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", model.CustID));
            }
            //业务员
            if (model.DealerID != null)
            {
                strSql.AppendLine(" and a.Seller=@Seller");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Seller", model.DealerID));
            }
            //开始时间
            if (model.BeginDate != null)
            {
                strSql.AppendLine(" and a.DealDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BeginDate", model.BeginDate));
            }
            //结束时间
            if (model.EndDate != null)
            {
                strSql.AppendLine(" and a.DealDate<dateadd(day,-1,@EndDate)");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));
            }
            strSql.AppendLine("group by seller,EmployeeName,CustName,c.prodNo,CodeName,ProductName,Specification");
            comm.CommandText=strSql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion
    }
}
