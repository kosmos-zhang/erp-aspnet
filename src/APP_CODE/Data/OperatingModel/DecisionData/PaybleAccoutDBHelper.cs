/**********************************************
 * 类作用：   决策支持分析
 * 建立人：   莫申林
 * 建立时间： 2010/06/01
 ***********************************************/

using System;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;

namespace XBase.Data.OperatingModel.DecisionData
{
    public class PaybleAccoutDBHelper
    {
        /// <summary>
        /// 获取对应企业的采购订单中的所有不重复的供应商
        /// </summary>
        /// <param name="CompanyCD">企业编码</param>
        /// <returns></returns>
        public static DataTable GetDistinctProviderByPucharOrder(string CompanyCD)
        {
            string sql = "select distinct ID from officedba.ProviderInfo where CompanyCD=@CompanyCD";
            SqlParameter[] parms = 
                           {
                               new SqlParameter("@CompanyCD",CompanyCD)
                           };

            return SqlHelper.ExecuteSql(sql, parms);
        }

        /// <summary>
        /// 获取某供应商对应的采购订单金额，已付款金额，采购退货单金额及供应商名称
        /// </summary>
        /// <param name="CompanyCD">企业代码</param>
        /// <param name="ProviderID">供应商ID</param>
        /// <returns></returns>
        public static DataTable GetPaybleAccountInfo(string CompanyCD, string ProviderID)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine(" select ");
            sqlStr.AppendLine(" ( select isnull(sum(isnull(RealTotal,0)*isnull(Rate,1)),0) as RealTotal ");
            sqlStr.AppendLine(" from officedba.PurchaseOrder ");
            sqlStr.AppendLine(" where BillStatus='2' and CompanyCD=@CompanyCD and ProviderID=@ProviderID ) as OrderAmount, ");
            sqlStr.AppendLine(" ( select isnull(sum(isnull(BlendingAmount,0)*isnull(b.CurrencyRate,1)),0)  as Ytotal from Officedba. StepsDetails a ");
            sqlStr.AppendLine(" left outer join Officedba. BlendingDetails b on a.BlendingID=b.ID where a.SourceID in ( ");
            sqlStr.AppendLine(" select a.ID from Officedba.PayBill a left outer join Officedba.Billing b on a.BillingID=b.ID ");
            sqlStr.AppendLine(" where a.FromTBName='officedba.ProviderInfo' and a.CustID=@ProviderID and a.CompanyCD=@CompanyCD and a.ConfirmStatus='1' ");
            sqlStr.AppendLine(" and b.CompanyCD=@CompanyCD and b.BillingType='2' and a.BillingID<>'0' ) ");
            sqlStr.AppendLine(" and a.PayOrInComeType='1' ) as PayAmount, ");
            sqlStr.AppendLine(" ( select isnull(sum(isnull(PayAmount,0)*isnull(CurrencyRate,1)),0) from Officedba.PayBill where  FromTBName='officedba.ProviderInfo' and CustID=@ProviderID and CompanyCD=@CompanyCD and ConfirmStatus='1'   and BillingID='0' ) as yufu, ");
            sqlStr.AppendLine(" ( select isnull(sum(isnull(BlendingAmount,0)*isnull(b.CurrencyRate,1)),0)  as Ytotal from Officedba. StepsDetails a ");
            sqlStr.AppendLine(" left outer join Officedba. BlendingDetails b on a.BlendingID=b.ID where a.SourceID in ( ");
            sqlStr.AppendLine(" select a.ID from Officedba.IncomeBill a left outer join Officedba.Billing b on a.BillingID=b.ID ");
            sqlStr.AppendLine(" where a.FromTBName='officedba.ProviderInfo' and a.CustID=@ProviderID and a.CompanyCD=@CompanyCD and a.ConfirmStatus='1' ");
            sqlStr.AppendLine(" and b.CompanyCD=@CompanyCD and b.BillingType='5' ) ");
            sqlStr.AppendLine(" and a.PayOrInComeType='2' ) as BackAmount,");
            sqlStr.AppendLine(" isnull(( select CustName from officedba.ProviderInfo where ID=@ProviderID ),'') as ProviderName ");

            SqlParameter[] parms = 
                           {
                               new SqlParameter("@CompanyCD",CompanyCD),
                               new SqlParameter("@ProviderID",ProviderID)
                           };
            return SqlHelper.ExecuteSql(sqlStr.ToString(), parms);
        }
    }
}
