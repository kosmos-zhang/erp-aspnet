using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
using System.Collections;
using XBase.Common;


namespace XBase.Data.Office.ProjectBudget
{
   public class ProjectTotalDBHelper
    {


       /// <summary>
       /// 获取某项目的各个收支类别的金额总和
       /// </summary>
       /// <param name="InitType">1.采购订单2.采购退货单3.付款单4.生产领料单5.退料单6.费用票据7.费用报销单8.其他入库单9.其他出库单10.销售订单11销售退货单12.收款单</param>
       /// <param name="ProjectID">项目主键</param>
        /// <param name="StartDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <param name="CompanyCD">企业编码</param>
        /// <returns>获取某项目的各个收支类别的金额总和</returns>
       public static decimal GetSumTypeAmount(string InitType,int ProjectID,string StartDate,string EndDate,string CompanyCD)
       {
           decimal rev = 0;

           StringBuilder sql = new StringBuilder();
           sql.AppendLine(" select isnull(sum(isnull(RealTotal,0)),0) as RealTotal from officedba.ProjectTotal ");
           sql.AppendLine(" where InitType in ( " + InitType + " ) and ProjectID=@ProjectID and CompanyCD=@CompanyCD ");

           if (!string.IsNullOrEmpty(StartDate))
           {
               sql.AppendLine("  and EnterDate>='"+StartDate+"' ");
           }

           if (!string.IsNullOrEmpty(EndDate))
           {
               sql.AppendLine("  and EnterDate<='" + EndDate + "' ");
           }

           SqlParameter[] parms = 
                           {
                               new SqlParameter("@ProjectID",ProjectID),
                               new SqlParameter("@CompanyCD",CompanyCD)
                           };

           object obj = SqlHelper.ExecuteScalar(sql.ToString(), parms);
           if (obj != null)
               rev = Convert.ToDecimal(obj);

           return rev;
       }



       /// <summary>
       /// 获取某项目的各个收支类别的金额总和
       /// </summary>
       /// <param name="InitType">1.采购订单2.采购退货单3.付款单4.生产领料单5.退料单6.费用票据7.费用报销单8.其他入库单9.其他出库单10.销售订单11销售退货单12.收款单</param>
       /// <param name="ProjectID">项目主键</param>
       /// <param name="StartDate">开始时间</param>
       /// <param name="EndDate">结束时间</param>
       /// <param name="CompanyCD">企业编码</param>
       /// <param name="CustID">往来单位主键</param>
       /// <param name="CustType">往来单位类别</param>
       /// <returns>获取某项目的各个收支类别的金额总和</returns>
       public static decimal GetSumTypeAmount(string InitType, int ProjectID, string StartDate, string EndDate, string CompanyCD,string CustID,string CustType)
       {
           decimal rev = 0;

           StringBuilder sql = new StringBuilder();
           sql.AppendLine(" select isnull(sum(isnull(RealTotal,0)),0) as RealTotal from officedba.ProjectTotal ");
           sql.AppendLine(" where InitType in ( " + InitType + " ) and ProjectID=@ProjectID and CompanyCD=@CompanyCD and CustID=@CustID and CustType=@CustType");

           if (!string.IsNullOrEmpty(StartDate))
           {
               sql.AppendLine("  and EnterDate>='" + StartDate + "' ");
           }

           if (!string.IsNullOrEmpty(EndDate))
           {
               sql.AppendLine("  and EnterDate<='" + EndDate + "' ");
           }

           SqlParameter[] parms = 
                           {
                               new SqlParameter("@ProjectID",ProjectID),
                               new SqlParameter("@CompanyCD",CompanyCD),
                               new SqlParameter("@CustID",CustID),
                               new SqlParameter("@CustType",CustType)
                           };

           object obj = SqlHelper.ExecuteScalar(sql.ToString(), parms);
           if (obj != null)
               rev = Convert.ToDecimal(obj);

           return rev;
       }



       /// <summary>
       /// 获取所属项目中涉及到的所有往来单位
       /// </summary>
       /// <param name="InitType">1.采购订单2.采购退货单3.付款单4.生产领料单5.退料单6.费用票据7.费用报销单8.其他入库单9.其他出库单10.销售订单11销售退货单12.收款单</param>
       /// <param name="ProjectID">项目主键</param>
       /// <param name="StartDate">开始时间</param>
       /// <param name="EndDate">结束时间</param>
       /// <param name="CompanyCD">企业编码</param>
       /// <returns>获取所属项目中涉及到的所有往来单位</returns>
       public static DataTable GetDistinctCustInfo(string InitType, int ProjectID, string StartDate, string EndDate, string CompanyCD)
       {
           DataTable dt = new DataTable();


           StringBuilder sql = new StringBuilder();
           sql.AppendLine(" select distinct CustID,CustName,CustType from officedba.ProjectTotal ");
           sql.AppendLine(" where CustType!='0'   and ProjectID=@ProjectID and CompanyCD=@CompanyCD ");

           if (!string.IsNullOrEmpty(StartDate))
           {
               sql.AppendLine("  and EnterDate>='" + StartDate + "' ");
           }

           if (!string.IsNullOrEmpty(EndDate))
           {
               sql.AppendLine("  and EnterDate<='" + EndDate + "' ");
           }

           if (!string.IsNullOrEmpty(InitType))
           {
               sql.AppendLine("  and InitType in ( " + InitType + " )  ");
           }
           SqlParameter[] parms = 
                           {
                               new SqlParameter("@ProjectID",ProjectID),
                               new SqlParameter("@CompanyCD",CompanyCD)
                           };


           dt = SqlHelper.ExecuteSql(sql.ToString(), parms);


           return dt;
       }

    }
}
