/**********************************************
 * 类作用：   决策支持分析
 * 建立人：   王玉贞
 * 建立时间： 2010/05/28
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
    public class ReceivableAccountDBHelper
    {
        #region 单个客户应收账款余额查询
        /// <summary>
        /// 单个客户应收账款余额查询
        /// </summary>
        /// <param name="CustID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetReceivableAccount(string CustID, string CompanyCD)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select");
            searchSql.AppendLine("--客户名称");
            searchSql.AppendLine("isnull((select CustName from officedba.CustInfo where ID=@CustID),'') as CustName,");
            searchSql.AppendLine("--信用额度");
            searchSql.AppendLine("Convert(numeric(22,"+userInfo.SelPoint+"),isnull((select MaxCredit from officedba.CustInfo where ID=@CustID) ,0))as MaxCredit,");
            searchSql.AppendLine("--超信用额度");
            searchSql.AppendLine("OverMaxCredit =0,");
            searchSql.AppendLine("--应收账款");
            searchSql.AppendLine("ReciverFee=0,");
            searchSql.AppendLine("--销售订单金额");
            searchSql.AppendLine("(select Convert(numeric(22,"+userInfo.SelPoint+"),isnull(sum(isnull(RealTotal,0)*isnull(Rate,1)),0)) as RealTotal ");
            searchSql.AppendLine("from officedba.SellOrder");
            searchSql.AppendLine("where BillStatus='2' and CompanyCD=@CompanyCD and CustID=@CustID) as OrderFee,");
            searchSql.AppendLine("--已付金额");
            searchSql.AppendLine("(");
            searchSql.AppendLine("select Convert(numeric(22,"+userInfo.SelPoint+"),isnull(sum(isnull(BlendingAmount,0)*isnull(b.CurrencyRate,1)),0)) as Ytotal from Officedba. StepsDetails a ");
            searchSql.AppendLine("left outer join Officedba. BlendingDetails b on a.BlendingID=b.ID where a.SourceID in (");
            searchSql.AppendLine("select a.ID from Officedba.IncomeBill a left outer join Officedba.Billing b on a.BillingID=b.ID");
            searchSql.AppendLine("where a.FromTBName='officedba.CustInfo' and a.CustID=@CustID and a.CompanyCD=@CompanyCD and a.ConfirmStatus='1'");
            searchSql.AppendLine("and b.CompanyCD=@CompanyCD and b.BillingType='1' ) and a.PayOrInComeType='2' ) as PayFee,");
            searchSql.AppendLine("--销售退货金额");
            searchSql.AppendLine("(");
            searchSql.AppendLine("select Convert(numeric(22,"+userInfo.SelPoint+"),isnull(sum(isnull(BlendingAmount,0)*isnull(b.CurrencyRate,1)),0))  as Ytotal from Officedba. StepsDetails a ");
            searchSql.AppendLine("left outer join Officedba. BlendingDetails b on a.BlendingID=b.ID where a.SourceID in (");
            searchSql.AppendLine("select a.ID from Officedba.PayBill a left outer join Officedba.Billing b on a.BillingID=b.ID");
            searchSql.AppendLine("where a.FromTBName='officedba.CustInfo' and a.CustID=@CustID and a.CompanyCD=@CompanyCD and a.ConfirmStatus='1'");
            searchSql.AppendLine("and b.CompanyCD=@CompanyCD and b.BillingType='3' ) and a.PayOrInComeType='1'");
            searchSql.AppendLine(") as UnPayFee,");
            searchSql.AppendLine("--预付款");
            searchSql.AppendLine("(");
            searchSql.AppendLine("	select Convert(numeric(22,6),isnull(sum(isnull(TotalPrice,0)*isnull(CurrencyRate,1)),0))  as AdvanceChargeFee from officedba.IncomeBill where ConfirmStatus=1 ");
            searchSql.AppendLine("	and companyCD=@CompanyCD and FromTBName='officedba.CUstInfo' and CustID=@CustID and BillingID=0");
            searchSql.AppendLine(")as AdvanceChargeFee");


            #endregion

            #region 定义查询命令

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustID));
            #endregion

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 所有客户
        /// <summary>
        /// 所有客户
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetCustInfo(string CompanyCD)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ID,CustName,Convert(numeric(22," + userInfo.SelPoint + "),isnull(MaxCredit,0)) as MaxCredit from officedba.CustInfo ");
            sql.AppendLine("where CompanyCD=@CompanyCD and UsedStatus=1 order by ID desc");
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            return SqlHelper.ExecuteSql(sql.ToString(), parms);
        }
        #endregion
    }
}
