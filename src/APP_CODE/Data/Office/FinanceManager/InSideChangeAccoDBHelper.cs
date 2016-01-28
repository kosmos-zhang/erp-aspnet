/**********************************************
 * 类作用：   内部转账单单数据库层处理
 * 建立人：   莫申林
 * 建立时间： 2009/04/28
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
using XBase.Common;
namespace XBase.Data.Office.FinanceManager
{
   public class InSideChangeAccoDBHelper
   {

       #region 添加内部转账信息
       /// <summary>
       /// 添加内部转账信息
       /// </summary>
       /// <param name="model"></param>
       /// <param name="ID"></param>
       /// <returns></returns>
       public static bool InSertInsideChangeAccoInfo(InSideChangeAccoModel model, out int ID)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.AppendLine("insert into officedba.InSideChangeAcco(");
           strSql.AppendLine("CompanyCD,ChangeNo,ChangeBillNum,ChangeDate,InAccountNo,InBankName,OutAccountNo,OutBankName,TotalPrice,Executor,Summary,ConfirmStatus,IsAccount,CurrencyType,CurrencyRate)");
           strSql.AppendLine(" values (");
           strSql.AppendLine("@CompanyCD,@ChangeNo,@ChangeBillNum,@ChangeDate,@InAccountNo,@InBankName,@OutAccountNo,@OutBankName,@TotalPrice,@Executor,@Summary,@ConfirmStatus,@IsAccount,@CurrencyType,@CurrencyRate)");
           strSql.AppendLine("set @IntID= @@IDENTITY");

           SqlParameter[] parms = new SqlParameter[16];
           parms[0] = SqlHelper.GetParameter("@CompanyCD",model.CompanyCD);
           parms[1] = SqlHelper.GetParameter("@ChangeNo",model.ChangeNo);
           parms[2] = SqlHelper.GetParameter("@ChangeBillNum",model.ChangeBillNum);
           parms[3] = SqlHelper.GetParameter("@ChangeDate",model.ChangeDate);
           parms[4] = SqlHelper.GetParameter("@InAccountNo", model.InAccountNo);
           parms[5] = SqlHelper.GetParameter("@InBankName", model.InBankName);
           parms[6] = SqlHelper.GetParameter("@OutAccountNo", model.OutAccountNo);
           parms[7] = SqlHelper.GetParameter("@OutBankName", model.OutBankName);
           parms[8] = SqlHelper.GetParameter("@TotalPrice",model.TotalPrice);
           parms[9] = SqlHelper.GetParameter("@Executor",model.Executor);
           parms[10] = SqlHelper.GetParameter("@Summary",model.Summary);
           parms[11] = SqlHelper.GetParameter("@ConfirmStatus",model.ConfirmStatus);
           parms[12] = SqlHelper.GetParameter("@IsAccount",model.IsAccount);
           parms[13] = SqlHelper.GetParameter("@CurrencyType", model.CurrencyType);
           parms[14] = SqlHelper.GetParameter("@CurrencyRate", model.CurrencyRate);
           parms[15] = SqlHelper.GetOutputParameter("@IntID", SqlDbType.Int);

           SqlHelper.ExecuteTransSql(strSql.ToString(), parms);
           ID = Convert.ToInt32(parms[15].Value);
           return SqlHelper.Result.OprateCount > 0 ? true : false;

       }
       #endregion

       #region 修改内部转账信息
       /// <summary>
       /// 修改内部转账信息
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static bool UpdateInsideChangeAccoInfo(InSideChangeAccoModel model)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.AppendLine("update officedba.InSideChangeAcco set ");
           strSql.AppendLine("CompanyCD=@CompanyCD,");
           strSql.AppendLine("ChangeNo=@ChangeNo,");
           strSql.AppendLine("ChangeBillNum=@ChangeBillNum,");
           strSql.AppendLine("ChangeDate=@ChangeDate,");
           strSql.AppendLine("InAccountNo=@InAccountNo,");
           strSql.AppendLine("InBankName=@InBankName,");
           strSql.AppendLine("OutAccountNo=@OutAccountNo,");
           strSql.AppendLine("OutBankName=@OutBankName,");
           strSql.AppendLine("TotalPrice=@TotalPrice,");
           strSql.AppendLine("Executor=@Executor,");
           strSql.AppendLine("Summary=@Summary,");
           strSql.AppendLine("ModifiedDate=@ModifiedDate,");
           strSql.AppendLine("ModifiedUserID=@ModifiedUserID,CurrencyType=@CurrencyType,CurrencyRate=@CurrencyRate");
           strSql.AppendLine(" where ID=@ID");

           SqlParameter[] parms = new SqlParameter[16];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
           parms[1] = SqlHelper.GetParameter("@ChangeNo", model.ChangeNo);
           parms[2] = SqlHelper.GetParameter("@ChangeBillNum", model.ChangeBillNum);
           parms[3] = SqlHelper.GetParameter("@ChangeDate", model.ChangeDate);
           parms[4] = SqlHelper.GetParameter("@InAccountNo", model.InAccountNo);
           parms[5] = SqlHelper.GetParameter("@InBankName", model.InBankName);
           parms[6] = SqlHelper.GetParameter("@OutAccountNo", model.OutAccountNo);
           parms[7] = SqlHelper.GetParameter("@OutBankName", model.OutBankName);
           parms[8] = SqlHelper.GetParameter("@TotalPrice", model.TotalPrice);
           parms[9] = SqlHelper.GetParameter("@Executor", model.Executor);
           parms[10] = SqlHelper.GetParameter("@Summary", model.Summary);
           parms[11] = SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate);
           parms[12] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
           parms[13] = SqlHelper.GetParameter("@CurrencyType", model.CurrencyType);
           parms[14] = SqlHelper.GetParameter("@CurrencyRate", model.CurrencyRate);
           parms[15] = SqlHelper.GetParameter("@ID", model.ID);

           SqlHelper.ExecuteTransSql(strSql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;

       }
       #endregion

       #region 查询内部转账单信息
       /// <summary>
       /// 根据查询条件查询部转账单信息
       /// </summary>
       /// <param name="queryStr"></param>
       /// <returns></returns>
       public static DataTable GetInsideChangeAccoInfo(string queryStr)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.ChangeNo,convert(varchar(10),a.ChangeDate,120) as ChangeDate,");
           sql.AppendLine("a.ChangeBillNum,cast( round(TotalPrice,2) as decimal(20,2))  as TotalPrice,");
           sql.AppendLine("case when a.ConfirmStatus='0'then '未确认' ");
           sql.AppendLine(" when a.ConfirmStatus='1' then '已确认'");
           sql.AppendLine("end as  ConfirmStatus ,case when e.EmployeeName is null then ");
           sql.AppendLine("'' when e.EmployeeName is not null then e.EmployeeName ");
           sql.AppendLine("end as Confirmor ,a.Executor as ExecutorID,a.Summary as Summary,a.InAccountNo,");
           sql.AppendLine("a.InBankName,a.OutAccountNo,a.OutBankName, ");
           sql.AppendLine("case when a.ConfirmDate is null then ''");
           sql.AppendLine("when  a.ConfirmDate is not null then convert(varchar(10),a.ConfirmDate,120) end  as ConfirmDate");
           sql.AppendLine(" ,case when a.IsAccount='0' then '未登记' when");
           sql.AppendLine("a.IsAccount='1' then '已登记' end as IsAccount,");

           sql.AppendLine("case when a.IsAccount='0' then '' when (a.IsAccount='1' and a.AccountDate is not null ) then convert(varchar(10),a.AccountDate,120) end  as AccountDate ,  ");

           sql.AppendLine(" case when a.IsAccount='0' then '' when (a.IsAccount='1' and f.EmployeeName is not null ) then  f.EmployeeName end as  Accountor, ");

           sql.AppendLine("case when g.EmployeeName is not null then  g.EmployeeName ");
           sql.AppendLine("when   g.EmployeeName is null then '' end as  Executor , ");
           sql.AppendLine(" isnull(a.AttestBillID,0) as AttestBillID,   ");
           sql.AppendLine(" case when a.AttestBillID is null then '' when a.AttestBillID is not null then c.AttestNo end as AttestNo, ");
           sql.AppendLine(" a.CurrencyType,isnull(a.CurrencyRate,1) as CurrencyRate ,h.CurrencyName");



           sql.AppendLine("from officedba.InSideChangeAcco as a  ");
           sql.AppendLine(" left join officedba.AttestBill c on a.AttestBillID=c.ID");
           sql.AppendLine("left join  officedba.EmployeeInfo as e  on a.Confirmor=e.ID ");
           sql.AppendLine("left join  officedba.EmployeeInfo as f  on a.Accountor=f.ID ");
           sql.AppendLine("left join  officedba.EmployeeInfo as g  on a.Executor=g.ID ");
           sql.AppendLine(" left join officedba.CurrencyTypeSetting as h on a.CurrencyType=h.ID ");
           sql.AppendLine(" {0} ");
           string selectSQL = string.Format(sql.ToString(), queryStr.Trim().Length > 0 ? " where " + queryStr : "");

           return SqlHelper.ExecuteSql(selectSQL);

       }
       #endregion



       #region 查询内部转账单信息SQL排序
       /// <summary>
       /// 根据查询条件查询部转账单信息
       /// </summary>
       /// <param name="queryStr"></param>
       /// <returns></returns>
       public static DataTable GetInsideChangeAccoInfo(string queryStr, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.ChangeNo,convert(varchar(10),a.ChangeDate,120) as ChangeDate,");
           sql.AppendLine("a.ChangeBillNum,convert(varchar,convert(money,a.TotalPrice),1)  as TotalPrice,");
           sql.AppendLine("case when a.ConfirmStatus='0'then '未确认' ");
           sql.AppendLine(" when a.ConfirmStatus='1' then '已确认'");
           sql.AppendLine("end as  ConfirmStatus ,case when a.ConfirmStatus='0' then '' ");
           sql.AppendLine(" when (a.ConfirmStatus='1' and  e.EmployeeName is not null ) then e.EmployeeName ");
           sql.AppendLine("end as Confirmor ,a.Executor as ExecutorID,a.Summary as Summary,a.InAccountNo,");
           sql.AppendLine("a.InBankName,a.OutAccountNo,a.OutBankName, ");
           sql.AppendLine("case when a.ConfirmStatus='0' then '' ");
           sql.AppendLine("when (a.ConfirmStatus='1' and  a.ConfirmDate is not null ) then convert(varchar(10),a.ConfirmDate,120) end  as ConfirmDate");
           sql.AppendLine(" ,case when a.IsAccount='0' then '未登记' when");
           sql.AppendLine("a.IsAccount='1' then '已登记' end as IsAccount,");

           sql.AppendLine("case when a.IsAccount='0' then '' when (a.IsAccount='1' and a.AccountDate is not null ) then convert(varchar(10),a.AccountDate,120) end  as AccountDate ,  ");

           sql.AppendLine(" case when a.IsAccount='0' then '' when (a.IsAccount='1' and f.EmployeeName is not null ) then  f.EmployeeName end as  Accountor, ");

           sql.AppendLine("case when g.EmployeeName is not null then  g.EmployeeName ");
           sql.AppendLine("when   g.EmployeeName is null then '' end as  Executor , ");
           sql.AppendLine(" isnull(a.AttestBillID,0) as AttestBillID,   ");
           sql.AppendLine(" case when a.AttestBillID is null then '' when a.AttestBillID is not null then c.AttestNo end as AttestNo, ");
           sql.AppendLine(" a.CurrencyType,isnull(a.CurrencyRate,1) as CurrencyRate ,h.CurrencyName");



           sql.AppendLine("from officedba.InSideChangeAcco as a  ");
           sql.AppendLine(" left join officedba.AttestBill c on a.AttestBillID=c.ID");
           sql.AppendLine("left join  officedba.EmployeeInfo as e  on a.Confirmor=e.ID ");
           sql.AppendLine("left join  officedba.EmployeeInfo as f  on a.Accountor=f.ID ");
           sql.AppendLine("left join  officedba.EmployeeInfo as g  on a.Executor=g.ID ");
           sql.AppendLine(" left join officedba.CurrencyTypeSetting as h on a.CurrencyType=h.ID ");
           sql.AppendLine(" {0} ");
           string selectSQL = string.Format(sql.ToString(), queryStr.Trim().Length > 0 ? " where " + queryStr : "");

           SqlCommand comm = new SqlCommand();
           comm.CommandText = selectSQL;

           return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);

       }
       #endregion




       /// <summary>
       /// 判断内部转账单是否能删除，返回提示
       /// </summary>
       /// <param name="ids">内部转账单ID集</param>
       /// <returns></returns>
       public static string IsCanDel(string ids)
       {
           string nev = string.Empty;
           string sql = string.Format(@"select ChangeNo,ConfirmStatus,IsAccount from Officedba.InSideChangeAcco where ID in ( {0} )", ids);
           DataTable dt = SqlHelper.ExecuteSql(sql);
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               if (dt.Rows[i]["ConfirmStatus"].ToString() == "1")
               {
                   nev += "'" + dt.Rows[i]["ChangeNo"].ToString() + "'  已确认|";
               }
               if (dt.Rows[i]["IsAccount"].ToString() == "1")
               {
                   nev += "'" + dt.Rows[i]["ChangeNo"].ToString() + "'  已登记凭证|";
               }

           }
           return nev;
       }


       /// <summary>
       /// 内部转账单确认
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool Autidt(string ids)
       {
           string sql = string.Format(@"update officedba.InSideChangeAcco set ConfirmStatus='1',Confirmor={0},ConfirmDate='{1}' where ID in ( {2} )", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID, DateTime.Now.ToString("yyyy-MM-dd"), ids);
           SqlParameter[] parms = new SqlParameter[0];
           SqlHelper.ExecuteTransSql(sql, parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }

       /// <summary>
       /// 内部转账单反确认
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool AntiAutidt(string ids)
       {
           string sql = string.Format(@"update officedba.InSideChangeAcco set ConfirmStatus='0' where ID in ( {0} )", ids);
           SqlParameter[] parms = new SqlParameter[0];
           SqlHelper.ExecuteTransSql(sql, parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }

       /// <summary>
       /// 内部转账单登记凭证状态
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool AccountInSideChangeAcco(string ids, int AttestBillID)
       {
           string sql = string.Format(@"update officedba.InSideChangeAcco set IsAccount='1',Accountor={0},AccountDate='{1}',AttestBillID={2} where ID in ( {3} )", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID, DateTime.Now.ToString("yyyy-MM-dd"),AttestBillID,ids);
           SqlParameter[] parms = new SqlParameter[0];
           SqlHelper.ExecuteTransSql(sql, parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }

       /// <summary>
       /// 删除内部转账单信息
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool DeleteInSideChangeAcco(string ids)
       {
           string sql = string.Format(@"delete from officedba.InSideChangeAcco where ID in ( {0} )",ids);
           SqlParameter[] parms = new SqlParameter[0];
           SqlHelper.ExecuteTransSql(sql, parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;

       }



       /// <summary>
       /// 是否允许确认
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static string IsCanAudit(string ids)
       {
           string nev = string.Empty;
           string sql = string.Format(@"select ChangeNo,ConfirmStatus,IsAccount from Officedba.InSideChangeAcco where ID in ( {0} )", ids);
           DataTable dt = SqlHelper.ExecuteSql(sql);
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               if (dt.Rows[i]["ConfirmStatus"].ToString() == "1")
               {
                   nev += "'" + dt.Rows[i]["ChangeNo"].ToString() + "'  已确认|";
               }
               if (dt.Rows[i]["IsAccount"].ToString() == "1")
               {
                   nev += "'" + dt.Rows[i]["ChangeNo"].ToString() + "'  已登记凭证|";
               }

           }
           return nev;
       }
       /// <summary>
       /// 是否反允许确认
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static string IsCanAntiAudit(string ids)
       {
           string nev = string.Empty;
           string sql = string.Format(@"select ChangeNo,ConfirmStatus,IsAccount from Officedba.InSideChangeAcco where ID in ( {0} )", ids);
           DataTable dt = SqlHelper.ExecuteSql(sql);
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               if (dt.Rows[i]["ConfirmStatus"].ToString() == "0")
               {
                   nev += "'" + dt.Rows[i]["ChangeNo"].ToString() + "'  未确认|";
               }
               if (dt.Rows[i]["IsAccount"].ToString() == "1")
               {
                   nev += "'" + dt.Rows[i]["ChangeNo"].ToString() + "'  已登记凭证|";
               }

           }
           return nev;
       }

       #region 判断内部转帐单单据编码是否重复

       /// <summary>
       /// 判断内部转帐单单据编码是否重复
       /// </summary>
       /// <param name="ChangeNo">转账单编码</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <param name="ID">单据主键</param>
       /// <returns>bool true OR false</returns>
       public static bool IsDiffInsideNo(string ChangeNo, string CompanyCD, string ID)
       {
           int nev = 0;
           string sql = "select count(ID) from officedba.InSideChangeAcco where ID not in ( " + ID + " ) and ChangeNo=@ChangeNo and CompanyCD=@CompanyCD";
           SqlParameter[] parms = new SqlParameter[2];
           parms[0] = SqlHelper.GetParameter("@ChangeNo", ChangeNo);
           parms[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           object obj = SqlHelper.ExecuteScalar(sql, parms);
           if (obj != null)
           {
               nev = Convert.ToInt32(obj);
           }
           return nev > 0 ? true : false;
       }
       #endregion





   }
}
