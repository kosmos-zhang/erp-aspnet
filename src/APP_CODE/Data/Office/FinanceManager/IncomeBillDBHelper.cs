/**********************************************
 * 类作用：   收款单数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/04/10
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Collections;
using System.Text;
using XBase.Model.Office.FinanceManager;
using XBase.Common;
namespace XBase.Data.Office.FinanceManager
{
    public class IncomeBillDBHelper
    {
        #region 判断收款单号是否已存在
        public static bool IncomeNoisExist(string CompanyCD,string IncomeNo)
        {
            bool result = false;

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select count(*)  from officedba.IncomeBill");
            sql.AppendLine("where CompanyCD=@CompanyCD ");
            sql.AppendLine("and IncomeNo=@IncomeNo ");

            SqlParameter [] parms=
            {
                new SqlParameter("@CompanyCD",CompanyCD),
                new SqlParameter("@IncomeNo",IncomeNo)
            };

            object objs = SqlHelper.ExecuteScalar(sql.ToString(),parms);
            if (Convert.ToInt32(objs) > 0)
            {
                result = true;
            }

            return result;
        }
        #endregion

        #region 更新收款单登记凭证状态
        /// <summary>
        /// 根据主键更新收款单登记凭证状态
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>true 成功,false 失败</returns>
        public static bool UpdateAccountStatus(string ID,int Accountor)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Update officedba.IncomeBill");
            sql.AppendLine("set IsAccount='1',AccountDate=getdate(),");
            sql.AppendLine("Accountor=@Accountor where ID in ("+ID+")");
            SqlParameter[] parms = new SqlParameter[1];
           // parms[0] = SqlHelper.GetParameter("@ID", ID);
            parms[0] = SqlHelper.GetParameter("@Accountor", Accountor);
            SqlHelper.ExecuteTransSql(sql.ToString(), parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        #region  确认收款单
        /// <summary>
        /// 确认收款单
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="Confirmor">确认人</param>
        /// <returns>true成功,false失败</returns>
        public static bool ConfirmIncomeBill(string ID, string Confirmor)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Update officedba.IncomeBill");
            sql.AppendLine("set ConfirmStatus='1' ,Confirmor=@Confirmor,");
            sql.AppendLine("ConfirmDate=getdate() where ID  In( " + ID + ")");
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@Confirmor", Confirmor);

            SqlHelper.ExecuteTransSql(sql.ToString(), parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        #region 反确认收款单
        /// <summary>
        /// 反确认收款单
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="Confirmor">确认人</param>
        /// <returns>true成功,false失败</returns>
        public static bool ReConfirmIncomeBill(string ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Update officedba.IncomeBill");
            sql.AppendLine("set ConfirmStatus='0' ,Confirmor=NULL,");
            sql.AppendLine("ConfirmDate=NULL where ID  In( " + ID + ") ");
            

            SqlHelper.ExecuteTransSql(sql.ToString());
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        #region  根据主键获取收款单信息
        /// <summary>
        /// 根据主键获取收款单信息
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetIncomeBillInfoByID(string ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.IncomeNo,a.AcceDate,a.CustName,a.BlendingType,isnull(a.CustID,0) as CustID ,isnull(a.FromTBName,'') as FromTBName ,isnull(a.FileName,'') as FileName,");
            sql.AppendLine("a.TotalPrice,isnull(a.BillingID,0) as BillingID ,a.BankName,a.AcceWay,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.CurrencyRate,1) as CurrencyRate,CASE WHEN g.CurrencyName IS NOT NULL THEN g.CurrencyName WHEN g.CurrencyName IS NULL THEN '' END AS CurrencyName,");
            sql.AppendLine("a.Executor,a.AccountNo,case when a.ConfirmStatus='0' then '未确认'");
            sql.AppendLine("when a.ConfirmStatus='1' then '已确认' end as ConfirmStatus,e.EmployeeName as Confirmor,");
            sql.AppendLine("case when  a.ConfirmDate is null then '' when a.ConfirmDate is not null then convert(varchar(10),a.ConfirmDate,120) end as ConfirmDate ,a.Summary,");
            sql.AppendLine("case when a.IsAccount='0' then '未登帐' when a.IsAccount='1' then '已登帐'");
            sql.AppendLine("end as IsAccount ,case when  a.AccountDate is null then '' when a.AccountDate  is not null then convert(varchar(10),a.AccountDate,120) end as AccountDate,f.EmployeeName Accountor,b.BillingNum,c.EmployeeName ");
            sql.AppendLine("as ExecutorName,a.ProjectID,t.ProjectName ");
            sql.AppendLine("from  officedba.IncomeBill as a");
            sql.AppendLine("left join  officedba.Billing as b  on a.BillingID=b.ID");
            sql.AppendLine("left join officedba.EmployeeInfo as c on c.ID=a.Executor");
            sql.AppendLine("left join officedba.EmployeeInfo as e  on e.ID=a.Confirmor");
            sql.AppendLine("left join officedba.EmployeeInfo as f  on f.ID=a.Accountor LEFT OUTER JOIN officedba.CurrencyTypeSetting AS g ON a.CurrencyType = g.ID   left outer join officedba.ProjectInfo t on a.ProjectID=t.ID ");
            sql.AppendLine("where a.ID=@ID");
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@ID", ID);

            return SqlHelper.ExecuteSql(sql.ToString(), parms);
        }
        #endregion

        #region 添加收款单信息
        /// <summary>
        /// 添加收款单信息
        /// </summary>
        /// <param name="model">收款单实体</param>
        /// <returns>true 成功，false失败</returns>
        public static bool InsertIncomeBill(IncomeBillModel model, out int ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Insert into officedba.IncomeBill");
            sql.AppendLine("(CompanyCD,InComeNo,AcceDate,");
            sql.AppendLine("CustName,BillingID,TotalPrice,");
            sql.AppendLine("AcceWay,BankName,Executor,");
            sql.AppendLine("AccountNo,ModifiedDate,ModifiedUserID,Summary,BlendingType,CurrencyType,CurrencyRate,CustID,FromTBName,FileName,ProjectID)");
            sql.AppendLine("values(@CompanyCD,@InComeNo,@AcceDate,");
            sql.AppendLine("@CustName,@BillingID,@TotalPrice,@AcceWay,");
            sql.AppendLine("@BankName,@Executor,@AccountNo,getdate(),@ModifiedUserID,@Summary,@BlendingType,@CurrencyType,@CurrencyRate,@CustID,@FromTBName,@FileName,@ProjectID)");
            sql.AppendLine("set @IntID= @@IDENTITY");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar).Value = model.CompanyCD;
            comm.Parameters.AddWithValue("@InComeNo", SqlDbType.VarChar).Value = model.InComeNo;
            comm.Parameters.AddWithValue("@AcceDate", SqlDbType.VarChar).Value = model.AcceDate;
            comm.Parameters.AddWithValue("@CustName", SqlDbType.VarChar).Value = model.CustName;
            comm.Parameters.AddWithValue("@BillingID", SqlDbType.VarChar).Value = model.BillingID;
            comm.Parameters.AddWithValue("@TotalPrice", SqlDbType.VarChar).Value = model.TotalPrice;
            comm.Parameters.AddWithValue("@AcceWay", SqlDbType.Char).Value = model.AcceWay;
            comm.Parameters.AddWithValue("@BankName", SqlDbType.VarChar).Value = model.BankName;
            comm.Parameters.AddWithValue("@Executor", SqlDbType.VarChar).Value = model.Executor;
            comm.Parameters.AddWithValue("@AccountNo", SqlDbType.VarChar).Value = model.AccountNo;
            comm.Parameters.AddWithValue("@ModifiedUserID", SqlDbType.VarChar).Value = model.ModifiedUserID;
            comm.Parameters.AddWithValue("@Summary", SqlDbType.VarChar).Value = model.Summary;
            comm.Parameters.AddWithValue("@BlendingType", SqlDbType.Char).Value = model.BlendingType;
            comm.Parameters.AddWithValue("@CurrencyType", SqlDbType.Int).Value = model.CurrencyType;
            comm.Parameters.AddWithValue("@CurrencyRate", SqlDbType.Decimal).Value = model.CurrencyRate;

            comm.Parameters.AddWithValue("@CustID", SqlDbType.Int).Value = model.CustID;
            comm.Parameters.AddWithValue("@FromTBName", SqlDbType.VarChar).Value = model.FromTBName;
            comm.Parameters.AddWithValue("@FileName", SqlDbType.VarChar).Value = model.FileName;
            comm.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = model.ProjectID;

            SqlParameter Ret = new SqlParameter("@IntID", SqlDbType.Int);//定义返回值参数 
            Ret.Direction = ParameterDirection.Output;
            comm.Parameters.Add(Ret);
            ArrayList listCmd = new ArrayList();
            listCmd.Add(comm);
            if (model.BillingID > 0)
            {
                UpdateBillingPrice(listCmd, model.BillingID, model.TotalPrice);
            }
            bool result = SqlHelper.ExecuteTransWithArrayList(listCmd);
            ID = Convert.ToInt32(Ret.Value);

            return result;
        }
        #endregion

        #region 更新业务单关联的金额
        /// <summary>
        /// 更新业务单关联的金额
        /// </summary>
        /// <param name="lstCommand">数组</param>
        /// <param name="ID">主键</param>
        private static void UpdateBillingPrice(ArrayList lstCommand, int ID, decimal Price)
        {
            decimal NAccounts = 0;
            decimal result=BillingDBHelper.GetBillingNAccounts(ID.ToString());
            if (result>0)
            {   
                //获取业务单未付金额
                NAccounts = result;
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Update  officedba.Billing set YAccounts=isnull(YAccounts,0)+@YAccounts,");
            sql.AppendLine("NAccounts=@NAccounts,AccountsStatus=@AccountsStatus ");
            sql.AppendLine(" where ID=@ID");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql.ToString();

            //定义参数
            cmd.Parameters.AddWithValue("@YAccounts", SqlDbType.Decimal).Value = Price;
            cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = ID;
            //未结金额
            decimal ProcPrice=NAccounts - Price;

            if (ProcPrice == 0)
            {
                cmd.Parameters.AddWithValue("@AccountsStatus", SqlDbType.Char).Value = ConstUtil.ACCOUNTS_STATUS_YJS;
            }
            else
            {
                cmd.Parameters.AddWithValue("@AccountsStatus", SqlDbType.Char).Value = ConstUtil.ACCOUNTS_STATUS_YJSZ;
            }

            cmd.Parameters.AddWithValue("@NAccounts", SqlDbType.Decimal).Value = ProcPrice;
            lstCommand.Add(cmd);
        }
        #endregion

        #region 修改收款单信息
        /// <summary>
        /// 修改收款单信息
        /// </summary>
        /// <param name="model">收款单实体</param>
        /// <returns>true 成功，false失败</returns>
        public static bool UpdateIncomeBill(IncomeBillModel model,decimal OldPrice)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update officedba.IncomeBill set ");
            sql.AppendLine("[AcceDate] =@AcceDate,");
            sql.AppendLine("[CustName] =@CustName,");
            sql.AppendLine("[BillingID] =@BillingID,");
            sql.AppendLine("[TotalPrice] =@TotalPrice,");
            sql.AppendLine("[AcceWay] =@AcceWay,");
            sql.AppendLine("[BankName] =@BankName,");
            sql.AppendLine("[Executor] =@Executor,");
            sql.AppendLine("[AccountNo] =@AccountNo,");
            sql.AppendLine("[ModifiedDate] =getdate(),");
            sql.AppendLine("[ModifiedUserID] = @ModifiedUserID,");
            sql.AppendLine("[Summary] = @Summary,");
            sql.AppendLine("[BlendingType] = @BlendingType,CurrencyType=@CurrencyType,CurrencyRate=@CurrencyRate,CustID=@CustID,FromTBName=@FromTBName,FileName=@FileName,ProjectID=@ProjectID");
            sql.AppendLine("where ID=@ID");


            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();

            comm.Parameters.AddWithValue("@AcceDate", SqlDbType.DateTime).Value = model.AcceDate;
            comm.Parameters.AddWithValue("@CustName", SqlDbType.VarChar).Value = model.CustName;
            comm.Parameters.AddWithValue("@BillingID", SqlDbType.Int).Value = model.BillingID;
            comm.Parameters.AddWithValue("@TotalPrice", SqlDbType.Decimal).Value = model.TotalPrice;
            comm.Parameters.AddWithValue("@AcceWay", SqlDbType.Char).Value = model.AcceWay;
            comm.Parameters.AddWithValue("@BankName", SqlDbType.VarChar).Value = model.BankName;
            comm.Parameters.AddWithValue("@Executor", SqlDbType.Int).Value = model.Executor;
            comm.Parameters.AddWithValue("@AccountNo", SqlDbType.VarChar).Value = model.AccountNo;
            comm.Parameters.AddWithValue("@ModifiedUserID", SqlDbType.VarChar).Value = model.ModifiedUserID;
            comm.Parameters.AddWithValue("@Summary", SqlDbType.VarChar).Value = model.Summary;
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
                UpdateBillingPriceInfo(listCmd, model.BillingID, model.TotalPrice, OldPrice);
            }

            bool result = SqlHelper.ExecuteTransWithArrayList(listCmd);

            return result;
        }
        #endregion

        #region 更新业务单关联的金额
        /// <summary>
        /// 更新业务单关联的金额
        /// </summary>
        /// <param name="lstCommand">数组</param>
        /// <param name="ID">主键</param>
        private static void UpdateBillingPriceInfo(ArrayList lstCommand, int ID, decimal Price, decimal OldPrice)
        {
            decimal NAccounts = 0;

            decimal result = BillingDBHelper.GetBillingNAccounts(ID.ToString());
            if (result > 0)
            {
                //获取业务单未付金额
                NAccounts = result;
            }

            decimal diffAccounts = Price - OldPrice;//update by moshenlin 2009-08-05
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Update  officedba.Billing set YAccounts=isnull(YAccounts,0)+@YAccounts,");
            sql.AppendLine("NAccounts=isnull(NAccounts,0)-@NAccounts,AccountsStatus=@AccountsStatus ");
            sql.AppendLine(" where ID=@ID");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql.ToString();
            //定义参数
            //cmd.Parameters.AddWithValue("@YAccounts", SqlDbType.Decimal).Value = OldPrice;
            cmd.Parameters.AddWithValue("@YAccounts", SqlDbType.Decimal).Value = diffAccounts;//update by moshenlin 2009-08-05
            cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = ID;
            //cmd.Parameters.AddWithValue("@NAccounts", SqlDbType.Decimal).Value = OldPrice;
            cmd.Parameters.AddWithValue("@NAccounts", SqlDbType.Decimal).Value = diffAccounts;//update by moshenlin 2009-08-05

            if (NAccounts - diffAccounts == 0)
            {
                cmd.Parameters.AddWithValue("@AccountsStatus", SqlDbType.Char).Value = ConstUtil.ACCOUNTS_STATUS_YJS;
            }
            else
            {
                cmd.Parameters.AddWithValue("@AccountsStatus", SqlDbType.Char).Value = ConstUtil.ACCOUNTS_STATUS_YJSZ;
            }

            lstCommand.Add(cmd);
        }
        #endregion

        #region 根据检索条件检索收款单信息
        /// <summary>
        /// 根据检索条件检索收款单信息
        /// </summary>
        /// <param name="IncomeBillNo">收款单号</param>
        /// <param name="AcceWay">收款方式</param>
        /// <param name="TotalPrice">收款金额</param>
        /// <param name="ConfirmStatus">确认状态</param>
        /// <param name="IsVoucher">是否登记凭证</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns>DataTable</returns>
        public static DataTable SearchIncomeInfo(string ProjectID, string CompanyCD, string IncomeBillNo, string AcceWay, string TotalPrice
            , string ConfirmStatus, string IsVoucher, string StartDate, string EndDate,
            int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.BillingID,a.ID,a.InComeNo,convert(varchar(10),a.AcceDate,120)as AcceDate,a.BlendingType,isnull(a.CurrencyType,'') as CurrencyType,isnull(a.CurrencyRate,1) as CurrencyRate,CASE WHEN g.CurrencyName IS NOT NULL THEN g.CurrencyName WHEN g.CurrencyName IS NULL THEN '' END AS CurrencyName,");
            sql.AppendLine("a.CustName,convert(varchar,convert(money,a.TotalPrice),1) as TotalPrice,case when a.AcceWay='0' then '现金' when");
            sql.AppendLine("a.AcceWay='1' then '银行转账' end as AcceWay ,isnull(a.CustID,0) as CustID ,isnull(a.FromTBName,'') as FromTBName ,isnull(a.FileName,'') as FileName,");
            sql.AppendLine("case when a.ConfirmStatus='0'then '未确认' ");
            sql.AppendLine(" when a.ConfirmStatus='1' then '已确认'");
            sql.AppendLine(" when a.ConfirmStatus='2' then '反确认'");
            sql.AppendLine("end as  ConfirmStatus ,case when e.EmployeeName is null then ");
            sql.AppendLine("'' when e.EmployeeName is not null then e.EmployeeName ");
            sql.AppendLine("end as Confirmor ,");
            sql.AppendLine("case when a.ConfirmDate is null then ''");
            sql.AppendLine("when  a.ConfirmDate is not null then convert(varchar(10),a.ConfirmDate,120)end  as ConfirmDate");
            sql.AppendLine(" ,case when a.IsAccount='0' then '未登记' when");
            sql.AppendLine("a.IsAccount='1' then '已登记' end as IsAccount,");
            sql.AppendLine("case when b.BillingNum is not null then b.BillingNum");
            sql.AppendLine(" when b.BillingNum is null then '' end  BillingNum ");

            sql.AppendLine(", case when a.IsAccount='0' then '' when (a.IsAccount='1' and f.EmployeeName is not null ) then  f.EmployeeName end as  Accountor, ");
            sql.AppendLine(" isnull(a.AttestBillID,0) as AttestBillID,   ");
            sql.AppendLine(" case when a.AttestBillID is null then '' when a.AttestBillID is not null then c.AttestNo end as AttestNo,a.ProjectID,t.ProjectName ");

            sql.AppendLine("from officedba.IncomeBill as a left join ");
            sql.AppendLine("officedba.Billing  as b on a.BillingID=b.ID");
            sql.AppendLine(" left join officedba.AttestBill c on a.AttestBillID=c.ID");
            sql.AppendLine("left join  officedba.EmployeeInfo as e  on a.Confirmor=e.ID");
            sql.AppendLine("left join  officedba.EmployeeInfo as f  on a.Accountor=f.ID LEFT OUTER JOIN officedba.CurrencyTypeSetting AS g ON a.CurrencyType = g.ID  left outer join officedba.ProjectInfo t on a.ProjectID=t.ID ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //收款单号
            if (!string.IsNullOrEmpty(IncomeBillNo))
            {
                sql.AppendLine(" AND a.InComeNo LIKE @InComeNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InComeNo", "%" + IncomeBillNo + "%"));
            }
            //收款方式
            if (!string.IsNullOrEmpty(AcceWay))
            {
                sql.AppendLine(" AND a.AcceWay = @AcceWay ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AcceWay", AcceWay));
            }
            //收款金额
            if (!string.IsNullOrEmpty(TotalPrice))
            {
                sql.AppendLine(" AND a.TotalPrice like  @TotalPrice ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice", "%" + TotalPrice + "%"));
            }
            //确认状态
            if (!string.IsNullOrEmpty(ConfirmStatus))
            {
                sql.AppendLine(" AND a.ConfirmStatus=@ConfirmStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmStatus", ConfirmStatus));
            }
            //登记凭证
            if (!string.IsNullOrEmpty(IsVoucher))
            {
                sql.AppendLine(" AND a.IsAccount=@IsVoucher ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsVoucher", IsVoucher));
            }
            //收款时间
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" AND a.AcceDate BetWeen  @StartDate and @EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }

            //所属项目
            if (!string.IsNullOrEmpty(ProjectID))
            {
                sql.AppendLine(" AND a.ProjectID=@ProjectID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", ProjectID));
            }

            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);

        }
        #endregion

        #region 删除收款单
        /// <summary>
        /// 删除收款单
        /// </summary>
        /// <returns>true 成功，false失败</returns>
        public static bool DeleteIncomeBill(string ID,string BillingID,string Price)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Delete from officedba.IncomeBill");
            sql.AppendLine("where ID in("+ID+") ");
            SqlCommand comm = new SqlCommand();

            comm.CommandText = sql.ToString();

            ArrayList listCmd = new ArrayList();
            listCmd.Add(comm);

            UpdateBilling(listCmd,BillingID,Price);

            return SqlHelper.ExecuteTransWithArrayList(listCmd);
        }

        //删除收款单时更新业务单已付金额和未付金额信息
        private static void UpdateBilling(ArrayList lstCommand, string ID, string Price)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Update officedba.Billing  set YAccounts=isnull(YAccounts,0)-@YAccounts ");
            sql.AppendLine(",NAccounts=isnull(NAccounts,0)+@NAccounts where ID=@ID");
            SqlCommand cmd = null;
            string[] IDarray = ID.Split(',');
            string[] Pricearray = Price.Split(',');
            int index = 0;
            if (IDarray.Length > 0 && Pricearray.Length > 0)
            {
                for (int i = 0; i < IDarray.Length; i++)
                {
                    cmd = new SqlCommand();
                    //定义参数
                    decimal splitPrice = Convert.ToDecimal(Pricearray[index]);
                    int IDs = Convert.ToInt32(IDarray[i]);
                    cmd.Parameters.AddWithValue("@YAccounts", SqlDbType.Decimal).Value = splitPrice;
                    cmd.Parameters.AddWithValue("@NAccounts", SqlDbType.Decimal).Value = splitPrice;
                    cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = IDs;
                    cmd.CommandText = sql.ToString();
                    lstCommand.Add(cmd);
                    index++;
                }
            }
        }
        #endregion



        #region 删除收款单事务处理--add  by moshenlin 2010-06-21
        public static bool DeleteInComeBillInfo(string ids)
        {
            SqlConnection conn = new SqlConnection(SqlHelper._connectionStringStr);
            conn.Open();
            SqlTransaction mytran = conn.BeginTransaction();
            try
            {

                string[] Str = ids.Split(',');
                for (int i = 0; i < Str.Length; i++)
                {
                    string deletePayBillingSQL = "delete from officedba.IncomeBill where ID=@IDStr" + i + " ";//删除付款单sql 
                    string updateBillingAmountSQL = "Update  officedba.Billing set YAccounts=isnull(YAccounts,0)-(@PayAmount" + i + "),NAccounts=isnull(NAccounts,0)+(@PayAmount" + i + ") where ID=@ID" + i + "";
                    DataTable dt = GetBillingInfo(int.Parse(Str[i].ToString()));
                    string billingID = dt.Rows[0]["BillingID"].ToString();
                    decimal PayAmount = Convert.ToDecimal(dt.Rows[0]["PayAmount"].ToString());
                    decimal YAccount = BillingDBHelper.GetBillingYAccounts(billingID);//获取业务单已付金额
                    decimal diffYAccount = YAccount - PayAmount;

                    SqlParameter[] parms = new SqlParameter[2];
                    parms[0] = SqlHelper.GetParameter("@PayAmount" + i + "", PayAmount);
                    parms[1] = SqlHelper.GetParameter("@ID" + i + "", billingID);

                    SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, updateBillingAmountSQL, parms);
                    string updateStautsSQL = "Update  officedba.Billing set AccountsStatus=@AccountsStatus" + i + " where ID=@IDS" + i + "";
                    SqlParameter[] parmss = new SqlParameter[2];
                    if (diffYAccount > 0)//结算中
                    {
                        parmss[0] = SqlHelper.GetParameter("@AccountsStatus" + i + "", ConstUtil.ACCOUNTS_STATUS_YJSZ);
                        parmss[1] = SqlHelper.GetParameter("@IDS" + i + "", billingID);
                    }
                    else//未结算
                    {
                        parmss[0] = SqlHelper.GetParameter("@AccountsStatus" + i + "", ConstUtil.ACCOUNTS_STATUS_WJS);
                        parmss[1] = SqlHelper.GetParameter("@IDS" + i + "", billingID);
                    }
                    SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, updateStautsSQL, parmss);

                    SqlParameter[] parmsss = new SqlParameter[1];
                    parmsss[0] = SqlHelper.GetParameter("@IDStr" + i + "", Str[i].ToString());
                    SqlHelper.ExecuteNonQuery(mytran, CommandType.Text, deletePayBillingSQL, parmsss);
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
        #endregion


        /// <summary>
        /// 获取收款单对应的业务单ID及收款款金额
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetBillingInfo(int ID)
        {
            string sql = "select BillingID,TotalPrice as PayAmount from officedba.IncomeBill where id=@ID";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@ID", ID);

            return SqlHelper.ExecuteSql(sql, parms); ;
        }
    }
}
