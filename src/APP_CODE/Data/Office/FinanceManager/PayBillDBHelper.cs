/**********************************************
 * 类作用：   付款单数据库层处理
 * 建立人：   莫申林
 * 建立时间： 2009/04/25
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
   public class PayBillDBHelper
    {
       /// <summary>
       /// 添加付款单信息
       /// </summary>
       /// <param name="Model"></param>
       /// <param name="ID"></param>
       /// <returns></returns>
       public static bool InSertIntoPayBillInfo(PayBillModel model, out int IntID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("insert into Officedba.PayBill ( ");
           sql.AppendLine("  BillingID,CompanyCD,CreatDate,PayNo, ");
           sql.AppendLine("CustName,PayAmount,PayDate,AcceWay,Executor,  ");
           sql.AppendLine(" BankName,BankNo,ConfirmStatus,IsAccount,Remark,BlendingType,CurrencyType,CurrencyRate,CustID,FromTBName,FileName,ProjectID ) ");
           sql.AppendLine(" values (  @BillingID,@CompanyCD,@CreatDate,@PayNo,");
           sql.AppendLine("@CustName,@PayAmount,@PayDate,@AcceWay,@Executor,  ");
           sql.AppendLine(" @BankName,@BankNo,@ConfirmStatus,@IsAccout,@Remark,@BlendingType,@CurrencyType,@CurrencyRate,@CustID,@FromTBName,@FileName,@ProjectID ) ");
           sql.AppendLine("set @IntID= @@IDENTITY");


           SqlCommand comm = new SqlCommand();

           comm.CommandText = sql.ToString();

           comm.Parameters.AddWithValue("@BillingID", SqlDbType.Int).Value = model.BillingID;
           comm.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar).Value = model.CompanyCD;
           comm.Parameters.AddWithValue("@CreatDate", SqlDbType.DateTime).Value = model.CreatDate;
           comm.Parameters.AddWithValue("@PayNo", SqlDbType.VarChar).Value = model.PayNo;
           comm.Parameters.AddWithValue("@CustName", SqlDbType.VarChar).Value = model.CustName;
           comm.Parameters.AddWithValue("@PayAmount", SqlDbType.Decimal).Value = model.PayAmount;
           comm.Parameters.AddWithValue("@PayDate", SqlDbType.DateTime).Value = model.PayDate;
           comm.Parameters.AddWithValue("@AcceWay", SqlDbType.Char).Value = model.AcceWay;
           comm.Parameters.AddWithValue("@Executor", SqlDbType.Int).Value = model.Executor;
           comm.Parameters.AddWithValue("@BankName", SqlDbType.VarChar).Value = model.BankName;
           comm.Parameters.AddWithValue("@BankNo", SqlDbType.VarChar).Value = model.BankNo;
           comm.Parameters.AddWithValue("@ConfirmStatus", SqlDbType.Char).Value = model.ConfirmStatus;
           comm.Parameters.AddWithValue("@IsAccout", SqlDbType.Char).Value = model.IsAccout;
           comm.Parameters.AddWithValue("@Remark", SqlDbType.VarChar).Value = model.Remark;
           comm.Parameters.AddWithValue("@BlendingType", SqlDbType.Char).Value = model.BlendingType;
           comm.Parameters.AddWithValue("@CurrencyType", SqlDbType.Int).Value = model.CurrencyType;
           comm.Parameters.AddWithValue("@CurrencyRate", SqlDbType.Decimal).Value = model.CurrencyRate;
           comm.Parameters.AddWithValue("@CustID", SqlDbType.Int).Value = model.CustID;
           comm.Parameters.AddWithValue("@FromTBName", SqlDbType.VarChar).Value = model.FromTBName;
           comm.Parameters.AddWithValue("@FileName", SqlDbType.VarChar).Value = model.FileName;
           comm.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = model.ProjectID;

           SqlParameter Ret = new SqlParameter("@IntID", SqlDbType.Int); //定义返回值参数 
           Ret.Direction = ParameterDirection.Output;
           comm.Parameters.Add(Ret);

           ArrayList listCmd = new ArrayList();
           listCmd.Add(comm);
           if (model.BillingID > 0)
           {
               UpdateInsertBillingPrice(listCmd, model.BillingID, model.PayAmount);
           }
           bool result = SqlHelper.ExecuteTransWithArrayList(listCmd);
           IntID = Convert.ToInt32(Ret.Value);
           //bool updateResult=false;
           //if (result)
           //{
           //    updateResult = UpdateStauts(model.BillingID);
           //}
           //return (result&&updateResult);

           return result;
       }

       /// <summary>
       /// 更新业务单关联的金额
       /// </summary>
       /// <param name="lstCommand">数组</param>
       /// <param name="ID">主键</param>
       private static void UpdateInsertBillingPrice(ArrayList lstCommand, int ID, decimal Price)
       {
           decimal NAccount = BillingDBHelper.GetBillingNAccounts(ID.ToString());

           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update  officedba.Billing set YAccounts=isnull(YAccounts,0)+@YAccounts,NAccounts=isnull(NAccounts,0)-@YAccounts  ");
           sql.AppendLine(" where ID=@ID");

           SqlCommand cmd = new SqlCommand();
           cmd.CommandText = sql.ToString();
           //定义参数
           cmd.Parameters.AddWithValue("@YAccounts", SqlDbType.Decimal).Value = Price;
           cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = ID;
           lstCommand.Add(cmd);


           decimal StatusAmount = NAccount - Price;
           string updateStautsSQL = "Update  officedba.Billing set AccountsStatus=@AccountsStatus where ID=@ID1";
           SqlCommand comdd = new SqlCommand();
           comdd.CommandText = updateStautsSQL;
           if (StatusAmount > 0)
           {
               comdd.Parameters.AddWithValue("@AccountsStatus", SqlDbType.Decimal).Value = ConstUtil.ACCOUNTS_STATUS_YJSZ;
               comdd.Parameters.AddWithValue("@ID1", SqlDbType.Int).Value = ID;
           }
           else
           {
               comdd.Parameters.AddWithValue("@AccountsStatus", SqlDbType.Decimal).Value = ConstUtil.ACCOUNTS_STATUS_YJS;
               comdd.Parameters.AddWithValue("@ID1", SqlDbType.Int).Value = ID;
           }
           lstCommand.Add(comdd);

       }

       //public static bool UpdateStauts(int ID)
       //{
       //    ArrayList lstCommand = new ArrayList();
       //    string sqlStr = string.Format("select isnull(NAccounts,0) as NAccounts,isnull(TotalPrice,0) as TotalPrice ,isnull(YAccounts,0) as YAccounts  from officedba.Billing where ID=" + ID + "");
       //    DataTable dt = SqlHelper.ExecuteSql(sqlStr);
       //    if (Convert.ToDecimal(dt.Rows[0]["NAccounts"].ToString()) <= 0)//全部结算完成
       //    {
       //        string updateStautsSQL="Update  officedba.Billing set AccountsStatus=@AccountsStatus where ID=@ID1";
       //        SqlCommand com = new SqlCommand();
       //        com.CommandText = updateStautsSQL;
       //        com.Parameters.AddWithValue("@AccountsStatus", SqlDbType.Char).Value = "1";
       //        com.Parameters.AddWithValue("@ID1", SqlDbType.Int).Value = ID;
       //        lstCommand.Add(com);
       //    }
       //    else if ((Convert.ToDecimal(dt.Rows[0]["YAccounts"].ToString()) < Convert.ToDecimal(dt.Rows[0]["TotalPrice"].ToString())) && Convert.ToDecimal(dt.Rows[0]["YAccounts"].ToString()) > 0)//结算中
       //    {
       //        string updateStautsSQL2 = "Update  officedba.Billing set AccountsStatus=@AccountsStatus2 where ID=@ID2";
       //        SqlCommand comd = new SqlCommand();
       //        comd.CommandText = updateStautsSQL2;
       //        comd.Parameters.AddWithValue("@AccountsStatus2", SqlDbType.Char).Value = "2";
       //        comd.Parameters.AddWithValue("@ID2", SqlDbType.Int).Value = ID;
       //        lstCommand.Add(comd);
       //    }
       //    else//未结算
       //    {
       //        string updateStautsSQL3= "Update  officedba.Billing set AccountsStatus=@AccountsStatus3 where ID=@ID3";
       //        SqlCommand comd3 = new SqlCommand();
       //        comd3.CommandText = updateStautsSQL3;
       //        comd3.Parameters.AddWithValue("@AccountsStatus3", SqlDbType.Char).Value = "0";
       //        comd3.Parameters.AddWithValue("@ID3", SqlDbType.Int).Value = ID;
       //        lstCommand.Add(comd3);
       //    }

       //    return SqlHelper.ExecuteTransWithArrayList(lstCommand);
       //}
       /// <summary>
       /// 根据查询条件获取付款单表信息
       /// </summary>
       /// <param name="queryStr"></param>
       /// <returns></returns>
       public static DataTable GetPayBillInfo(string queryStr,int pageIndex,int pageSize,string OrderBy,ref int totalCount)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.PayNo,convert(varchar(10),a.PayDate,120) as PayDate,a.BlendingType,");
           sql.AppendLine("a.CustName,convert(varchar,convert(money,a.PayAmount),1) as PayAmount,case when a.AcceWay='0' then '现金' when");
           sql.AppendLine("a.AcceWay='1' then '银行转账' end as AcceWay ,a.AcceWay as AcceWayID,isnull(a.CustID,0) as CustID ,isnull(a.FromTBName,'') as FromTBName ,isnull(a.FileName,'') as FileName, ");
           sql.AppendLine("case when a.ConfirmStatus='0'then '未确认' ");
           sql.AppendLine(" when a.ConfirmStatus='1' then '已确认'");
           sql.AppendLine("end as  ConfirmStatus ,case when a.ConfirmStatus='0' then '' ");
           sql.AppendLine(" when (a.ConfirmStatus='1' and e.EmployeeName is not null) then e.EmployeeName ");
           sql.AppendLine("end as Confirmor ,a.Executor as ExecutorID,a.Remark as Remark,a.BankName,a.BankNo,a.BillingID, ");

           sql.AppendLine("case when a.ConfirmStatus='0' then ''  ");
           sql.AppendLine(" when (a.ConfirmStatus='1' and a.ConfirmDate is not null ) then convert(varchar(10),a.ConfirmDate,120) end  as ConfirmDate");
           sql.AppendLine(" ,case when a.IsAccount='0' then '未登记' when");
           sql.AppendLine("a.IsAccount='1' then '已登记' end as IsAccount, ");

           sql.AppendLine("case when a.IsAccount='0' then '' when (a.IsAccount='1' and a.AccountDate is not null ) then convert(varchar(10),a.AccountDate,120) end  as AccountDate ,  ");

           sql.AppendLine(" case when a.IsAccount='0' then '' when (a.IsAccount='1' and f.EmployeeName is not null ) then  f.EmployeeName end as  Accountor, ");

           sql.AppendLine("case when b.BillingNum is not null then b.BillingNum");
           sql.AppendLine(" when b.BillingNum is null then '' end  as BillingNum , ");

           sql.AppendLine("case when g.EmployeeName is not null then  g.EmployeeName ");
           sql.AppendLine("when   g.EmployeeName is null then '' end as  Executor , ");
           sql.AppendLine(" isnull(a.AttestBillID,0) as AttestBillID,   ");
           sql.AppendLine(" case when a.AttestBillID is null then '' when a.AttestBillID is not null then c.AttestNo end as AttestNo, ");
           sql.AppendLine("a.CurrencyType,isnull(a.CurrencyRate,1) as CurrencyRate,h.CurrencyName,a.ProjectID,t.ProjectName ");


           sql.AppendLine("from officedba.PayBill as a left join ");
           sql.AppendLine("officedba.Billing  as b on a.BillingID=b.ID ");
           sql.AppendLine(" left join officedba.AttestBill c on a.AttestBillID=c.ID");
           sql.AppendLine("left join  officedba.EmployeeInfo as e  on a.Confirmor=e.ID");
           sql.AppendLine("left join  officedba.EmployeeInfo as f  on a.Accountor=f.ID");
           sql.AppendLine("left join  officedba.EmployeeInfo as g  on a.Executor=g.ID");
           sql.AppendLine(" left join officedba.CurrencyTypeSetting as h on a.CurrencyType=h.ID   left outer join officedba.ProjectInfo t on a.ProjectID=t.ID  ");
           sql.AppendLine(" {0} ");

           //SqlParameter[] parms = new SqlParameter[1];
           //parms[0] = SqlHelper.GetParameter("@QueryStr", queryStr.Trim().Length>0?" where "+queryStr:"");
           string selectSQL = string.Format(sql.ToString(), queryStr.Trim().Length > 0 ? " where " + queryStr : "");

           SqlCommand comm = new SqlCommand();
           comm.CommandText = selectSQL;

           return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);


       }


       /// <summary>
       /// 根据查询条件获取付款单表信息
       /// </summary>
       /// <param name="queryStr"></param>
       /// <returns></returns>
       public static DataTable GetPayBillInfo(string queryStr)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,a.PayNo,convert(varchar(10),a.PayDate,120) as PayDate,a.BlendingType,isnull(a.CustID,0) as CustID ,isnull(a.FromTBName,'') as FromTBName ,isnull(a.FileName,'') as FileName,");
           sql.AppendLine("a.CustName,cast( round(a.PayAmount,2) as decimal(20,2))  as PayAmount,case when a.AcceWay='0' then '现金' when");
           sql.AppendLine("a.AcceWay='1' then '银行转账' end as AcceWay ,a.AcceWay as AcceWayID, ");
           sql.AppendLine("case when a.ConfirmStatus='0'then '未确认' ");
           sql.AppendLine(" when a.ConfirmStatus='1' then '已确认'");
           sql.AppendLine("end as  ConfirmStatus ,case when e.EmployeeName is null then ");
           sql.AppendLine("'' when e.EmployeeName is not null then e.EmployeeName ");
           sql.AppendLine("end as Confirmor ,a.Executor as ExecutorID,a.Remark as Remark,a.BankName,a.BankNo,a.BillingID, ");
           sql.AppendLine("case when a.ConfirmDate is null then ''");
           sql.AppendLine("when  a.ConfirmDate is not null then convert(varchar(10),a.ConfirmDate,120) end  as ConfirmDate");
           sql.AppendLine(" ,case when a.IsAccount='0' then '未登记' when");
           sql.AppendLine("a.IsAccount='1' then '已登记' end as IsAccount, ");

           sql.AppendLine("case when a.IsAccount='0' then '' when (a.IsAccount='1' and a.AccountDate is not null ) then convert(varchar(10),a.AccountDate,120) end  as AccountDate ,  ");

           sql.AppendLine(" case when a.IsAccount='0' then '' when (a.IsAccount='1' and f.EmployeeName is not null ) then  f.EmployeeName end as  Accountor, ");

           sql.AppendLine("case when b.BillingNum is not null then b.BillingNum");
           sql.AppendLine(" when b.BillingNum is null then '' end  as BillingNum , ");

           sql.AppendLine("case when g.EmployeeName is not null then  g.EmployeeName ");
           sql.AppendLine("when   g.EmployeeName is null then '' end as  Executor , ");
           sql.AppendLine(" isnull(a.AttestBillID,0) as AttestBillID,   ");
           sql.AppendLine(" case when a.AttestBillID is null then '' when a.AttestBillID is not null then c.AttestNo end as AttestNo ,");
           sql.AppendLine(" a.CurrencyType,isnull(a.CurrencyRate,1) as CurrencyRate ,h.CurrencyName,a.ProjectID,t.ProjectName ");


           sql.AppendLine("from officedba.PayBill as a left join ");
           sql.AppendLine("officedba.Billing  as b on a.BillingID=b.ID ");
           sql.AppendLine(" left join officedba.AttestBill c on a.AttestBillID=c.ID");
           sql.AppendLine("left join  officedba.EmployeeInfo as e  on a.Confirmor=e.ID");
           sql.AppendLine("left join  officedba.EmployeeInfo as f  on a.Accountor=f.ID");
           sql.AppendLine("left join  officedba.EmployeeInfo as g  on a.Executor=g.ID");
           sql.AppendLine(" left join officedba.CurrencyTypeSetting as h on a.CurrencyType=h.ID     left outer join officedba.ProjectInfo t on a.ProjectID=t.ID   ");
           sql.AppendLine(" {0} ");

           //SqlParameter[] parms = new SqlParameter[1];
           //parms[0] = SqlHelper.GetParameter("@QueryStr", queryStr.Trim().Length>0?" where "+queryStr:"");
           string selectSQL = string.Format(sql.ToString(), queryStr.Trim().Length > 0 ? " where " + queryStr : "");

           return SqlHelper.ExecuteSql(selectSQL);

       }

       /// <summary>
       /// 判断付款单是否确认
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
       public static bool GetStatus(int ID)
       {
           string sql = "select ConfirmStatus from officedba.PayBill where id=@ID";
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@ID", ID);
           return Convert.ToInt32(SqlHelper.ExecuteScalar(sql,parms))>0?false:true;
       }
       /// <summary>
       /// 更新付款单信息
       /// </summary>
       /// <param name="model">实体</param>
       /// <param name="DiffAmount">本次输入的付款金额与修改前的付款金额之差</param>
       /// <returns></returns>
       public static bool UpdatePayBill(PayBillModel model, decimal DiffAmount)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("update officedba.PayBill set  ");
           sql.AppendLine("PayNo=@PayNo,CustName=@CustName,PayAmount=@PayAmount,PayDate=@PayDate,AcceWay=@AcceWay, ");
           sql.AppendLine("Executor=@Executor,BankName=@BankName,BankNo=@BankNo,Remark=@Remark,ModifiedDate=@ModifiedDate,ModifiedUserID=@ModifiedUserID,BlendingType=@BlendingType,CurrencyType=@CurrencyType,CurrencyRate=@CurrencyRate,CustID=@CustID,FromTBName=@FromTBName,FileName=@FileName,ProjectID=@ProjectID ");
           sql.AppendLine(" where ID=@ID");


           SqlCommand comm = new SqlCommand();

           comm.CommandText = sql.ToString();

           comm.Parameters.AddWithValue("@PayNo", SqlDbType.VarChar).Value = model.PayNo;
           comm.Parameters.AddWithValue("@CustName", SqlDbType.VarChar).Value = model.CustName;
           comm.Parameters.AddWithValue("@PayAmount", SqlDbType.Decimal).Value = model.PayAmount;
           comm.Parameters.AddWithValue("@PayDate", SqlDbType.DateTime).Value = model.PayDate;
           comm.Parameters.AddWithValue("@AcceWay", SqlDbType.Char).Value = model.AcceWay;
           comm.Parameters.AddWithValue("@Executor", SqlDbType.Int).Value = model.Executor;
           comm.Parameters.AddWithValue("@BankName", SqlDbType.VarChar).Value = model.BankName;
           comm.Parameters.AddWithValue("@BankNo", SqlDbType.VarChar).Value = model.BankNo;
           comm.Parameters.AddWithValue("@Remark", SqlDbType.VarChar).Value = model.Remark;
           comm.Parameters.AddWithValue("@ModifiedDate", SqlDbType.DateTime).Value = model.ModifiedDate;
           comm.Parameters.AddWithValue("@ModifiedUserID", SqlDbType.Int).Value = model.ModifiedUserID;
           comm.Parameters.AddWithValue("@BlendingType", SqlDbType.Char).Value = model.BlendingType;

           comm.Parameters.AddWithValue("@CurrencyType", SqlDbType.Int).Value = model.CurrencyType;
           comm.Parameters.AddWithValue("@CurrencyRate", SqlDbType.Decimal).Value = model.CurrencyRate;

           comm.Parameters.AddWithValue("@CustID", SqlDbType.Int).Value = model.CustID;
           comm.Parameters.AddWithValue("@FromTBName", SqlDbType.VarChar).Value = model.FromTBName;
           comm.Parameters.AddWithValue("@FileName", SqlDbType.VarChar).Value = model.FileName;
           comm.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = model.ProjectID;

           comm.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = model.ID;

           ArrayList listCmd = new ArrayList();
           listCmd.Add(comm);
           if (model.BillingID > 0)
           {
               updateUpBillingStatus(listCmd, model.BillingID.ToString(), DiffAmount);
           }
           bool result = SqlHelper.ExecuteTransWithArrayList(listCmd);
           return result;

       }
       /// <summary>
       /// 修改时更新业务单表状态信息
       /// </summary>
       /// <param name="lstCommand"></param>
       /// <param name="BillingID"></param>
       /// <param name="DiffAmount"></param>
       public static void updateUpBillingStatus(ArrayList lstCommand, string BillingID, decimal DiffAmount)
       {
           decimal NAccount = BillingDBHelper.GetBillingNAccounts(BillingID);

           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update  officedba.Billing set YAccounts=isnull(YAccounts,0)+(@DiffAmount),NAccounts=isnull(NAccounts,0)-(@DiffAmount)  ");
           sql.AppendLine(" where ID=@ID");

           SqlCommand cmd = new SqlCommand();
           cmd.CommandText = sql.ToString();
           //定义参数
           cmd.Parameters.AddWithValue("@DiffAmount", SqlDbType.Decimal).Value = DiffAmount;
           cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = BillingID;
           lstCommand.Add(cmd);


           decimal StatusAmount = NAccount - (DiffAmount);
           string updateStautsSQL = "Update  officedba.Billing set AccountsStatus=@AccountsStatus where ID=@ID1";
           SqlCommand comdd = new SqlCommand();
           comdd.CommandText = updateStautsSQL;
           if (StatusAmount > 0)
           {
               comdd.Parameters.AddWithValue("@AccountsStatus", SqlDbType.Decimal).Value = ConstUtil.ACCOUNTS_STATUS_YJSZ;
               comdd.Parameters.AddWithValue("@ID1", SqlDbType.Int).Value = BillingID;
           }
           else
           {
               comdd.Parameters.AddWithValue("@AccountsStatus", SqlDbType.Decimal).Value = ConstUtil.ACCOUNTS_STATUS_YJS;
               comdd.Parameters.AddWithValue("@ID1", SqlDbType.Int).Value = BillingID;
           }
           lstCommand.Add(comdd); 
       }
       /// <summary>
       /// 删除付款单信息级联更新对应的业务单的已付金额和未付金额
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool DeletePayBill(string ids)
       {
           SqlConnection conn = new SqlConnection(SqlHelper._connectionStringStr);
           conn.Open();
           SqlTransaction mytran = conn.BeginTransaction();
           try
           {
                
               string[] Str = ids.Split(',');
               for (int i = 0; i < Str.Length; i++)
               {
                   string deletePayBillingSQL = "delete from officedba.PayBill where ID=@IDStr"+i+" ";//删除付款单sql 
                   string updateBillingAmountSQL = "Update  officedba.Billing set YAccounts=isnull(YAccounts,0)-(@PayAmount" + i + "),NAccounts=isnull(NAccounts,0)+(@PayAmount" + i + ") where ID=@ID"+i+"";
                   DataTable dt = GetBillingInfo(int.Parse(Str[i].ToString()));
                   string billingID = dt.Rows[0]["BillingID"].ToString();
                   decimal PayAmount = Convert.ToDecimal(dt.Rows[0]["PayAmount"].ToString());
                   decimal YAccount = BillingDBHelper.GetBillingYAccounts(billingID);//获取业务单已付金额
                   decimal diffYAccount = YAccount - PayAmount;

                   SqlParameter[] parms = new SqlParameter[2];
                   parms[0] = SqlHelper.GetParameter("@PayAmount" + i + "", PayAmount);
                   parms[1] = SqlHelper.GetParameter("@ID" + i + "", billingID);

                   SqlHelper.ExecuteNonQuery(mytran,CommandType.Text,updateBillingAmountSQL,parms);
                   string updateStautsSQL = "Update  officedba.Billing set AccountsStatus=@AccountsStatus"+i+" where ID=@IDS"+i+"";
                   SqlParameter[] parmss = new SqlParameter[2];
                   if (diffYAccount > 0)//结算中
                   {
                       parmss[0] = SqlHelper.GetParameter("@AccountsStatus" + i + "", ConstUtil.ACCOUNTS_STATUS_YJSZ);
                       parmss[1] = SqlHelper.GetParameter("@IDS"+i+"", billingID);
                   }
                   else//未结算
                   {
                       parmss[0] = SqlHelper.GetParameter("@AccountsStatus" + i + "", ConstUtil.ACCOUNTS_STATUS_WJS);
                       parmss[1] = SqlHelper.GetParameter("@IDS" + i + "", billingID);
                   }
                   SqlHelper.ExecuteNonQuery(mytran,CommandType.Text,updateStautsSQL,parmss);

                   SqlParameter[] parmsss = new SqlParameter[1];
                   parmsss[0] = SqlHelper.GetParameter("@IDStr" + i + "", Str[i].ToString());
                   SqlHelper.ExecuteNonQuery(mytran,CommandType.Text,deletePayBillingSQL,parmsss);
               }
               mytran.Commit();
               return true;
           }
           catch
           {
               mytran.Rollback();
               return false;
           }
           finally
           {
               conn.Close();
           }

       }

       /// <summary>
       /// 判断付款单是否能删除，返回提示
       /// </summary>
       /// <param name="ids">付款单ID集</param>
       /// <returns></returns>
       public static string IsCanDel(string ids)
       {
           string nev = string.Empty;
           string sql = string.Format(@"select PayNo,ConfirmStatus,IsAccount from Officedba.PayBill where ID in ( {0} )", ids);
           DataTable dt = SqlHelper.ExecuteSql(sql);
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               if (dt.Rows[i]["ConfirmStatus"].ToString() == "1")
               {
                   nev += "'" + dt.Rows[i]["PayNo"].ToString() + "'  已确认|";
               }
               if (dt.Rows[i]["IsAccount"].ToString() == "1")
               {
                   nev += "'" + dt.Rows[i]["PayNo"].ToString() + "'  已登记凭证|";
               }
              
           }
           return nev;
       }


       /// <summary>
       /// 获取付款单对应的业务单ID及付款金额
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
       public static DataTable GetBillingInfo(int ID)
       {
           string sql = "select BillingID,PayAmount from officedba.PayBill where id=@ID";
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@ID", ID);

           return SqlHelper.ExecuteSql(sql,parms); ;
       }
       /// <summary>
       /// 付款单确认
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool Autidt(string ids)
       {
           string sql = string.Format(@"update officedba.PayBill set ConfirmStatus='1',Confirmor={0},ConfirmDate='{1}' where ID in ( {2} )", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID,DateTime.Now.ToString("yyyy-MM-dd"),ids);
           SqlParameter[] parms = new SqlParameter[0];
           SqlHelper.ExecuteTransSql(sql, parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }

       /// <summary>
       /// 付款单反确认
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool AntiAutidt(string ids)
       {
           string sql = string.Format(@"update officedba.PayBill set ConfirmStatus='0' where ID in ( {0} )",ids);
           SqlParameter[] parms = new SqlParameter[0];
           SqlHelper.ExecuteTransSql(sql, parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }

       /// <summary>
       /// 付款单登记凭证状态
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool AccountPayBill(string ids, int AttestBillID)
       {
           string sql = string.Format(@"update officedba.PayBill set IsAccount='1',Accountor={0},AccountDate='{1}',AttestBillID={2} where ID in ( {3} )", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID, DateTime.Now.ToString("yyyy-MM-dd"),AttestBillID,ids);
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
           string sql = string.Format(@"select PayNo,ConfirmStatus,IsAccount from Officedba.PayBill where ID in ( {0} )", ids);
           DataTable dt = SqlHelper.ExecuteSql(sql);
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               if (dt.Rows[i]["ConfirmStatus"].ToString() == "1")
               {
                   nev += "'" + dt.Rows[i]["PayNo"].ToString() + "'  已确认|";
               }
               if (dt.Rows[i]["IsAccount"].ToString() == "1")
               {
                   nev += "'" + dt.Rows[i]["PayNo"].ToString() + "'  已登记凭证|";
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
           string sql = string.Format(@"select PayNo,ConfirmStatus,IsAccount from Officedba.PayBill where ID in ( {0} )", ids);
           DataTable dt = SqlHelper.ExecuteSql(sql);
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               if (dt.Rows[i]["ConfirmStatus"].ToString() == "0")
               {
                   nev += "'" + dt.Rows[i]["PayNo"].ToString() + "'  未确认|";
               }
               if (dt.Rows[i]["IsAccount"].ToString() == "1")
               {
                   nev += "'" + dt.Rows[i]["PayNo"].ToString() + "'  已登记凭证|";
               }

           }
           return nev;
       }



       #region 判断付款单单据编码是否重复

       /// <summary>
       /// 判断付款单单据编码是否重复
       /// </summary>
       /// <param name="ChangeNo">付款单编码</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <param name="ID">单据主键</param>
       /// <returns>bool true OR false</returns>
       public static bool IsDiffInsideNo(string PayNo, string CompanyCD, string ID)
       {
           int nev = 0;
           string sql = "select count(ID) from officedba.PayBill where ID not in ( " + ID + " ) and PayNo=@PayNo and CompanyCD=@CompanyCD";
           SqlParameter[] parms = new SqlParameter[2];
           parms[0] = SqlHelper.GetParameter("@PayNo", PayNo);
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
