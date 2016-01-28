/**************************
 * 描述：销售发货明细
 * 创建人：hexw
 * 创建时间：2010-6-21
 * *************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using XBase.Model.Office.SellManager;
using XBase.Data.DBHelper;

namespace XBase.Data.Office.SellManager
{
    public class SellSendDetailsListDBHelper
    {
        #region 根据条件获取 销售发货明细列表
        /// <summary>
        /// 根据条件获取 销售发货明细列表
        /// </summary>
        /// <param name="model">检索条件实体</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetSellSendDetailListData(SellSendDetailsListModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select p.ProdNo,p.ProductName,s2.CustID,c.CustName,");
            strSql.AppendLine(" case s2.isOpenbill when 0 then '未开票' when 1 then '已开票' end as IsOpenBillText,");
            strSql.AppendLine(" convert(decimal(22,"+model.SelPointLen+"),s.TotalFee) as TotalTax,");
            strSql.AppendLine(" convert(decimal(22," + model.SelPointLen + "),isnull(s.TaxPrice,0)) as TaxPrice,");
            //多计量单位 取实际数量
            if (model.IsMoreUnit)
            {
                strSql.AppendLine(" convert(decimal(22," + model.SelPointLen + "),isnull(s.UsedUnitCount,0)) as ProductCount,");
            }
            else
            {
                strSql.AppendLine(" convert(decimal(22," + model.SelPointLen + "),isnull(s.ProductCount,0)) as ProductCount,");
            }
            strSql.AppendLine(" convert(decimal(12," + model.SelPointLen + "),isnull(s.Discount,100)) as Discount,");
            strSql.AppendLine(" convert(varchar(10),s.SendDate,23) as SendDate,s.SendNo,");
            strSql.AppendLine(" p.Specification,p.ColorID,c2.TypeName as ColorName, ");
            strSql.AppendLine(" case b.InvoiceType when 1 then '增值税发票' when 2 then '普通地税' when 3 then '普通国税' when 4 then '收据' end as InvoiceTypeText,");
            strSql.AppendLine(" b.BillingNum,e.EmployeeName as BillExecutorName,convert(varchar(10),b.CreateDate,23) as BillCreateDate ");
            strSql.AppendLine(" ,s2.ID as SendID,s2.CurrencyType,s2.Rate,s2.RealTotal as TaxTotalPrice,ct.CurrencyName,s2.BillStatus ");
            /*销售发货单是否被引用Start*/
            strSql.AppendLine(",isnull(CASE ((SELECT count(1) ");
            strSql.AppendLine("FROM officedba.SellBack AS sb WHERE sb.FromType = '1' AND sb.FromBillID = s2.ID) + ");
            strSql.AppendLine("(SELECT count(1) FROM officedba.StorageOutSell AS soo ");
            strSql.AppendLine("WHERE soo.FromType = '1' AND soo.FromBillID = s2.ID) + ");
            strSql.AppendLine("(SELECT count(1) FROM officedba.SellChannelSttl AS scs ");
            strSql.AppendLine("WHERE scs.FromType = '1' AND scs.FromBillID = s2.ID) + ");
            strSql.AppendLine("(SELECT count(1) FROM officedba.SellChannelSttlDetail AS scsd ");
            strSql.AppendLine("WHERE scsd.FromType = '1' AND scsd.FromBillID = s2.ID) + ");
            strSql.AppendLine("(SELECT count(1)  FROM officedba.SellBackDetail AS sbd WHERE sbd.FromType = '1' AND sbd.FromBillID = s2.ID)) ");
            strSql.AppendLine("WHEN 0 THEN '无引用' END, '被引用') AS RefText ");
            /*销售发货单是否被引用END*/
            strSql.AppendLine(" from officedba.sellsenddetail s ");
            strSql.AppendLine(" left join officedba.productInfo p on p.ID=s.ProductID");
            strSql.AppendLine(" left join officedba.CodePublicType c2 on c2.ID=p.ColorID ");
            strSql.AppendLine(" left join officedba.sellsend s2 on s2.SendNo=s.SendNo and s.CompanyCD=s2.CompanyCD");
            strSql.AppendLine(" left join officedba.custInfo c on c.ID=s2.CustID");
            strSql.AppendLine(" left join officedba.CurrencyTypeSetting ct on ct.ID=s2.CurrencyType ");
            strSql.AppendLine(" left join officedba.Billing b on b.SourceID=s2.ID and b.BillingType=7 ");
            strSql.AppendLine(" left join officedba.EmployeeInfo e on e.ID=b.Executor ");
            strSql.AppendLine(" where s.CompanyCD=@CompanyCD");
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", model.CompanyCD));
            if (model.ProductID != null)
            {
                strSql.AppendLine(" and s.ProductID=@ProductID");
                arr.Add(new SqlParameter("@ProductID", model.ProductID));
            }
            if (model.CustID != null)
            {
                strSql.AppendLine(" and s2.CustID=@CustID");
                arr.Add(new SqlParameter("@CustID", model.CustID));
            }
            if (model.BeginDate != null)
            {
                strSql.AppendLine(" and s.SendDate >=@BeginDate");
                arr.Add(new SqlParameter("@BeginDate", model.BeginDate));
            }
            if (model.EndDate != null)
            {
                strSql.AppendLine(" and s.SendDate<dateadd(day,1,@EndDate)");
                arr.Add(new SqlParameter("@EndDate", model.EndDate));
            }
            if (model.IsOpenBill != null)
            {
                strSql.AppendLine(" and (s2.isOpenBill=@isOpenBill");
                if (model.IsOpenBill == "0")
                {
                    strSql.AppendLine(" or s2.isOpenBill is null ");
                }
                strSql.AppendLine(")");
                arr.Add(new SqlParameter("@isOpenBill", model.IsOpenBill));
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref totalCount);
        }
        #endregion
    }
}
