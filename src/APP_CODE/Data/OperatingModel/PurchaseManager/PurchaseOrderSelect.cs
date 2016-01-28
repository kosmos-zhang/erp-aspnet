/**********************************************
 * 类作用：   采购订单查询数据层处理
 * 建立人：   王超
 * 建立时间： 2009/06/10
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using System.Data.SqlClient;
using XBase.Model.Office.PurchaseManager;
using XBase.Common;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Collections;

namespace XBase.Data.OperatingModel.PurchaseManager
{
    public class PurchaseOrderSelect
    {
        public static DataTable SelectPurchaseOrder(string BillStatus, string StartDate, string EndDate,int pageIndex,int pageCount,string OrderBy,ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  A.OrderNo, A.Title, A.OrderDate, A.RealTotal, A.CountTotal, A.BillStatus                                                                  ");
            sql.AppendLine(",case A.BillStatus when 1 then '制单' when '2' then '执行'  when '3' then '变更'  when '4' then '手工结单'                                        ");
            sql.AppendLine(" when '5' then '自动结单' end AS BillStatusName                                                                                                   ");
            sql.AppendLine(", A.ProviderID,D.CustName AS ProviderName, A.Purchaser,E.EmployeeName AS PurchaserName                                                            ");
            sql.AppendLine(", (SELECT SUM(B.ArrivedCount) FROM officedba.PurchaseOrderDetail AS B WHERE A.CompanyCD = B.CompanyCD AND A.OrderNo = B.OrderNo ) AS ArrivedCount ");
            sql.AppendLine(",(SELECT SUM(C.InCount) FROM officedba.PurchaseArriveDetail AS C WHERE A.ID = C.FromBillID AND C.FromType = '1') AS InCount                       ");
            sql.AppendLine(",(SELECT SUM(C.BackCount) FROM officedba.PurchaseArriveDetail AS C WHERE A.ID = C.FromBillID AND C.FromType = '1') AS BackCount                   ");
            sql.AppendLine(" FROM officedba.PurchaseOrder AS A                                                                                                                 ");
            sql.AppendLine(" LEFT JOIN officedba.ProviderInfo AS D ON A.ProviderID=D.ID                                                                                        ");
            sql.AppendLine(" LEFT JOIN officedba.EmployeeInfo AS E ON A.Purchaser=E.ID                                                                                         ");

            sql.AppendLine(" WHERE 1=1");
            if (BillStatus != "")
            {
                sql.AppendLine(" AND A.BillStatus=@BillStatus ");
            }
            if (StartDate != null)
            {
                sql.AppendLine(" AND A.OrderDate > @StartDate");
            }
            if (EndDate != null)
            {
                sql.AppendLine(" AND A.OrderDate < @EndDate");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
    }
}
