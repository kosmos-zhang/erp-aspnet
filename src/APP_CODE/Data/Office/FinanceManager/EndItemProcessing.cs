/**********************************************
 * 类作用：   期末项目数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/03/27
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
namespace XBase.Data.Office.FinanceManager
{
  public  class EndItemProcessing
  {
      #region 获取当前企业登记账簿信息
      public static DataTable GetAccountBookEndAmount(string CompanyCD,int IsMasterCurrency,
          int CurrencyID,string StartDate,string EndDate)
      {
          SqlParameter[] parms = 
          {
              new SqlParameter("@CompanyCD",CompanyCD),
              new SqlParameter("@CurrencyTypeID",IsMasterCurrency),
              new SqlParameter("@CurrencyID",CurrencyID),
              new SqlParameter("@StartDate",StartDate),
              new SqlParameter("@EndDate",EndDate)
          };
          return SqlHelper.ExecuteStoredProcedure("[officedba].[ProcgetAccountEndAmount]", parms);
      }
      #endregion

      #region 根据期数获取登帐凭证的会计科目
      public static DataTable GetAccountAttestSubjectsCD(int Month,string CompanyCD)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select SubjectsCD  from officedba.acountBook");
          sql.AppendLine("where  MONTH(AccountDate)=@Month ");
          sql.AppendLine("and CompanyCD=@CompanyCD ");
          sql.AppendLine(" group by SubjectsCD ");


          SqlParameter[] parms =
          {
              new SqlParameter("@Month",Month),
              new SqlParameter("@CompanyCD",CompanyCD)
          };

          return SqlHelper.ExecuteSql(sql.ToString(),parms);
      }
      #endregion

      #region 添加已期末处理的项目信息
      /// <summary>
      /// 添加已期末处理的项目信息 
      /// </summary>
      /// <param name="CompanyCD">企业编码</param>
      /// <param name="ItemID">项目编号</param>
      /// <param name="PeriodNum">期数</param>
      /// <returns>True成功,False失败</returns>
      public static bool InsertPeriodProced(
        string CompanyCD, string ItemID, string PeriodNum, string IsAccount,out int ID)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Insert into officedba.EndItemProcessedRecord");
          sql.AppendLine("(CompanyCD,ItemID,PeriodNum,CreateDate,IsAccount)");
          sql.AppendLine("Values(@CompanyCD,@ItemID,@PeriodNum,getdate(),@IsAccount)");
          sql.AppendLine("set @IntID= @@IDENTITY");


          SqlCommand CmdPeriodProced = new SqlCommand();
          #region 参数定义
          CmdPeriodProced.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar).Value = CompanyCD;
          CmdPeriodProced.Parameters.AddWithValue("@ItemID", SqlDbType.Int).Value = ItemID;
          CmdPeriodProced.Parameters.AddWithValue("@PeriodNum", SqlDbType.VarChar).Value = PeriodNum;
          CmdPeriodProced.Parameters.AddWithValue("@IsAccount", SqlDbType.VarChar).Value = IsAccount;
          #endregion

          SqlParameter Ret = new SqlParameter("@IntID", SqlDbType.Int);//定义返回值参数 
          Ret.Direction = ParameterDirection.Output;
          CmdPeriodProced.Parameters.Add(Ret);

          CmdPeriodProced.CommandText = sql.ToString();
          ArrayList listCmd = new ArrayList();
          listCmd.Add(CmdPeriodProced);

          //lstCommand.Add(CmdPeriodProced);
          bool result = SqlHelper.ExecuteTransWithArrayList(listCmd);
           ID=Convert.ToInt32(Ret.Value);

           return result;
      }
      #endregion


      #region 更新期末项目登证状态
      public static void UpdateEndItemIsAccount(string ItemID, ArrayList lstCommand)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Update officedba.EndItemProcSetting ");
          sql.AppendLine(" set IsAccount='1' where ID=@ID");

          SqlParameter[] parms = 
          {
              new SqlParameter("@ID",ItemID)
          };

          SqlCommand cmdOprt = new SqlCommand();
          cmdOprt.CommandText = sql.ToString();

          cmdOprt.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = ItemID;

          lstCommand.Add(cmdOprt);
      }
      #endregion

      #region 生成凭证单据
      public static bool BuildAttestBill(AttestBillModel Attestmodel, out int ID)
      {
          bool result = false;
          StringBuilder Attestsql = new StringBuilder();
          Attestsql.AppendLine("Insert into officedba.AttestBill");
          Attestsql.AppendLine("(CompanyCD,VoucherDate,AttestNo,");
          Attestsql.AppendLine("AttestName,Creator,CreateDate,");
          Attestsql.AppendLine("status,FromTbale,FromValue)");
          Attestsql.AppendLine("Values(@CompanyCD,@VoucherDate,");
          Attestsql.AppendLine("@AttestNo,@AttestName,@Creator,getdate(),");
          Attestsql.AppendLine("@status,@FromTbale,@FromValue)");
          Attestsql.AppendLine("set @IntID= @@IDENTITY");


          SqlCommand CmdAttest = new SqlCommand();

          CmdAttest.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar).Value = Attestmodel.CompanyCD;
          CmdAttest.Parameters.AddWithValue("@AttestNo", SqlDbType.VarChar).Value = Attestmodel.AttestNo;
          CmdAttest.Parameters.AddWithValue("@AttestName", SqlDbType.VarChar).Value = Attestmodel.AttestName;
          CmdAttest.Parameters.AddWithValue("@Creator", SqlDbType.Int).Value = Attestmodel.Creator;
          CmdAttest.Parameters.AddWithValue("@status", SqlDbType.Int).Value = Attestmodel.status;
          CmdAttest.Parameters.AddWithValue("@FromTbale", SqlDbType.VarChar).Value = Attestmodel.FromTbale;
          CmdAttest.Parameters.AddWithValue("@FromValue", SqlDbType.VarChar).Value = Attestmodel.FromValue;
          CmdAttest.Parameters.AddWithValue("@VoucherDate", SqlDbType.DateTime).Value = Attestmodel.VoucherDate;

          SqlParameter Ret = new SqlParameter("@IntID", SqlDbType.Int); //定义返回值参数 
          Ret.Direction = ParameterDirection.Output;
          CmdAttest.Parameters.Add(Ret);

          CmdAttest.CommandText = Attestsql.ToString();

          ArrayList cmdList = new ArrayList();
          cmdList.Add(CmdAttest);

          //添加已期末处理的项目信息
        //  InsertPeriodProced(Attestmodel.CompanyCD, ItemID,"1", PeriodNum, cmdList);
         // UpdateEndItemIsAccount(ItemID,cmdList);//更新登帐状态
          result = SqlHelper.ExecuteTransWithArrayList(cmdList);
          ID = Convert.ToInt32(Ret.Value);
          return result;
      }
      #endregion





      #region  生成期末调汇凭证明细
      public static bool BuildEndRatechangeDetailInfo(ArrayList modelList)
      {
          StringBuilder Detailsql = new StringBuilder();
          Detailsql.AppendLine("Insert into officedba.AttestBillDetails");
          Detailsql.AppendLine("(AttestBillID,Abstract,SubjectsCD,");
          Detailsql.AppendLine("CurrencyTypeID,ExchangeRate,OriginalAmount,");
          Detailsql.AppendLine("DebitAmount,CreditAmount)");
          Detailsql.AppendLine("Values(@AttestBillID,@Abstract,");
          Detailsql.AppendLine("@SubjectsCD,@CurrencyTypeID,@ExchangeRate,@OriginalAmount,@DebitAmount,@CreditAmount)");
          ArrayList cmdList = new ArrayList();
          SqlCommand cmd = null;
          if (modelList.Count > 0)
          {
              for (int i = 0; i < modelList.Count; i++)
              {
                  cmd = new SqlCommand();
                  cmd.Parameters.AddWithValue("@AttestBillID", SqlDbType.Int).Value = (modelList[i] as AttestBillDetailsModel).AttestBillID;
                  cmd.Parameters.AddWithValue("@Abstract", SqlDbType.VarChar).Value = (modelList[i] as AttestBillDetailsModel).Abstract;
                  cmd.Parameters.AddWithValue("@SubjectsCD", SqlDbType.VarChar).Value = (modelList[i] as AttestBillDetailsModel).SubjectsCD;
                  cmd.Parameters.AddWithValue("@CurrencyTypeID", SqlDbType.Int).Value = (modelList[i] as AttestBillDetailsModel).CurrencyTypeID;
                  cmd.Parameters.AddWithValue("@ExchangeRate", SqlDbType.Decimal).Value = (modelList[i] as AttestBillDetailsModel).ExchangeRate;

                  cmd.Parameters.AddWithValue("@OriginalAmount", SqlDbType.Decimal).Value = (modelList[i] as AttestBillDetailsModel).OriginalAmount;
                  cmd.Parameters.AddWithValue("@DebitAmount", SqlDbType.Decimal).Value = (modelList[i] as AttestBillDetailsModel).DebitAmount;
                  cmd.Parameters.AddWithValue("@CreditAmount", SqlDbType.Decimal).Value = (modelList[i] as AttestBillDetailsModel).CreditAmount;
                  cmd.CommandText = Detailsql.ToString();
                  cmdList.Add(cmd);
              }
          }
          return SqlHelper.ExecuteTransWithArrayList(cmdList);
      }
      #endregion


      #region 查询当期项目是否进行期末处理
      public static bool CheckCurrentPeriodIsProced(string PeriodNum, int ItemID)
      {
          bool result = false;

          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select count(PeriodNum) from ");
          sql.AppendLine(" officedba.EndItemProcessedRecord ");
          sql.AppendLine("where CompanyCD=@CompanyCD  and  ItemID=@ItemID  and PeriodNum=@PeriodNum");

          SqlParameter[] parms = 
           {
                new SqlParameter("@ID",ItemID),
                new SqlParameter("@PeriodNum",PeriodNum)
           };
          object objs = SqlHelper.ExecuteScalar(sql.ToString(), parms);
          if (Convert.ToInt32(objs) > 0)
          {
              result = true;
          }
          return result;
      }
      #endregion



  
      public static bool CheckPeriodIsexist(string PeriodNum,string CompanyCD)
      {
          bool result = false;

          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select count(PeriodNum) from ");
          sql.AppendLine(" officedba.EndItemProcSetting ");
          sql.AppendLine("where  CompanyCD=@CompanyCD  and PeriodNum=@PeriodNum");

          SqlParameter[] parms = 
           {
                new SqlParameter("@PeriodNum",PeriodNum),
                new SqlParameter("@CompanyCD",CompanyCD)
           };
          object objs = SqlHelper.ExecuteScalar(sql.ToString(), parms);
          if (Convert.ToInt32(objs) > 0)
          {
              result = true;
          }
          return result;
      }
     



      #region  生成结转损益凭证明细
      public static bool BuildJZDetailInfo(ArrayList modelList,string UpdateID)
      {
          StringBuilder Detailsql = new StringBuilder();
          Detailsql.AppendLine("Insert into officedba.AttestBillDetails");
          Detailsql.AppendLine("(AttestBillID,Abstract,SubjectsCD,");
          Detailsql.AppendLine("CurrencyTypeID,OriginalAmount,");
          Detailsql.AppendLine("DebitAmount,CreditAmount)");
          Detailsql.AppendLine("Values(@AttestBillID,@Abstract,");
          Detailsql.AppendLine("@SubjectsCD,@CurrencyTypeID,@OriginalAmount,@DebitAmount,@CreditAmount)");

          ArrayList cmdList = new ArrayList();

          SqlCommand cmd = null;

          if (modelList.Count > 0)
          {
              for (int i = 0; i < modelList.Count; i++)
              {
                  cmd = new SqlCommand();
                  cmd.Parameters.AddWithValue("@AttestBillID", SqlDbType.Int).Value = (modelList[i] as AttestBillDetailsModel).AttestBillID;
                  cmd.Parameters.AddWithValue("@Abstract", SqlDbType.VarChar).Value = (modelList[i] as AttestBillDetailsModel).Abstract;
                  cmd.Parameters.AddWithValue("@SubjectsCD", SqlDbType.VarChar).Value = (modelList[i] as AttestBillDetailsModel).SubjectsCD;
                  cmd.Parameters.AddWithValue("@CurrencyTypeID", SqlDbType.Int).Value = (modelList[i] as AttestBillDetailsModel).CurrencyTypeID;
                  cmd.Parameters.AddWithValue("@OriginalAmount", SqlDbType.Decimal).Value = (modelList[i] as AttestBillDetailsModel).OriginalAmount;
                  cmd.Parameters.AddWithValue("@DebitAmount", SqlDbType.Decimal).Value = (modelList[i] as AttestBillDetailsModel).DebitAmount;
                  cmd.Parameters.AddWithValue("@CreditAmount", SqlDbType.Decimal).Value = (modelList[i] as AttestBillDetailsModel).CreditAmount;
                  cmd.CommandText = Detailsql.ToString();
                  cmdList.Add(cmd);
              }
          }
         // UpdateAccountBookEndAmount(UpdateID, cmdList);//更新账簿期末余额
          return SqlHelper.ExecuteTransWithArrayList(cmdList);
      }
      #endregion

      #region   根据指定ID获取损益类登帐凭证期末余额
      public static DataTable  GetSYEndAmountByID(string ID)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select isnull(EndAmount,0) as EndAmount,SubjectsCD  from officedba.AcountBook");
          sql.AppendLine("where ID in ("+ID+")");

          //SqlParameter[] parms = 
          //{
          //    new SqlParameter("@ID",ID)
          //};

          return SqlHelper.ExecuteSql(sql.ToString());
      }
      #endregion

      #region  根据ID统计损益类科目登帐期末余额
      public static decimal GetSYCountAmountByID(string ID)
      {

          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select sum(isnull(EndAmount,0)) as EndAmount from officedba.AcountBook ");
          sql.AppendLine("where ID in (" + ID + ")");


          return Convert.ToDecimal(SqlHelper.ExecuteSql(sql.ToString()).Rows[0]["EndAmount"]);
      }
      #endregion

      #region 汇总不同币种的期末余额
      public static DataTable GetEndAmount(string ID)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine(" select  CurrencyTypeID, ");
          sql.AppendLine(" sum(ForeignEndAmount) as ForeignEndAmount ");
          sql.AppendLine(",sum (EndAmount ) as  EndAmount");
          sql.AppendLine("  from officedba.acountBook  ");
          sql.AppendLine("  where ID in ( "+ID+") ");
          sql.AppendLine(" group by CurrencyTypeID ");
      
          return SqlHelper.ExecuteSql(sql.ToString());
      }
      #endregion

      #region 结转后更新损益科目期末值
      public static void UpdateAccountBookEndAmount(string ID,ArrayList cmdList)
      {
          if (ID.Length > 0)
          {
              StringBuilder sql = new StringBuilder();
              sql.AppendLine("Update officedba.AcountBook");
              sql.AppendLine("set EndAmount=0 where ID");
              sql.AppendLine("in (" + ID + ")  ");


              SqlCommand cmdsql = new SqlCommand();
              cmdsql.CommandText = sql.ToString();
              cmdList.Add(cmdsql);
          }
      }
      #endregion

      #region 获取当前企业损益科目信息
      public static DataTable  SumSYEndAmount(string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select SubjectsCD,max (ID) as ID");
            sql.AppendLine("from officedba.AcountBook where");
            sql.AppendLine("left (SubjectsCD,1)='6'");
            sql.AppendLine("and CompanyCD=@CompanyCD  and  Direction='1' ");
            sql.AppendLine(" group by  SubjectsCD ");
           SqlParameter[] parms = 
           {
               new SqlParameter("@CompanyCD",CompanyCD)
           };
           return SqlHelper.ExecuteSql(sql.ToString(), parms);
        }
        #endregion

      #region 根据科目汇总损益类科目期末余额
        public static DataTable GetSYSubjectsEndAmount(string CompanyCD,string Direction,
            string StartDate,string EndDate)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select SubjectsCD,max (ID) as ID");
            sql.AppendLine("from officedba.AcountBook where");
            sql.AppendLine("left (SubjectsCD,1)='6'");
            sql.AppendLine("and CompanyCD=@CompanyCD  and  Direction=@Direction ");
            sql.AppendLine("and (VoucherDate>=@StartDate  and  VoucherDate<=@EndDate)");
            sql.AppendLine(" group by  SubjectsCD");

           SqlParameter[] parms = 
           {
               new SqlParameter("@CompanyCD",CompanyCD),
               new SqlParameter("@Direction",Direction),
               new SqlParameter("@StartDate",StartDate),
               new SqlParameter("@EndDate",EndDate)
           };
            return SqlHelper.ExecuteSql(sql.ToString(), parms);
        }
        #endregion

      #region 获取凭证登帐期末调汇信息
        public static DataTable GetAttestEndInfo(string CompanyCD,int Month)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("  select SubjectsCD,max (ID) as ID");
            sql.AppendLine("  from officedba.AcountBook where ");
          //  sql.AppendLine("  CompanyCD=@CompanyCD and MONTH(AccountDate)=@Month ");
            sql.AppendLine("   CompanyCD=@CompanyCD  ");
            sql.AppendLine("  group by  SubjectsCD  ");
            SqlParameter[] parms = 
            {
                new SqlParameter("@CompanyCD",CompanyCD)
               // new SqlParameter("@Month",Month)
            };
            return SqlHelper.ExecuteSql(sql.ToString(), parms);
        }
      #endregion

        #region 结转到下一年方法

        //#region  更新科目期初值新
        //public 

        //#endregion



        #region 更新科目期初值
        public static bool UpdateSubjectsYInitialValue(ArrayList modelList)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Update officedba.AccountSubjects");
            sql.AppendLine("set YInitialValue=isnull(YInitialValue,0)+@YInitialValue ");
            sql.AppendLine(" where CompanyCD=@CompanyCD and SubjectsCD=@SubjectsCD ");

            ArrayList cmdList = new ArrayList();
            SqlCommand cmd = null;
            if (modelList.Count > 0)
            {
                for (int i = 0; i < modelList.Count; i++)
                {
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@YInitialValue", SqlDbType.Decimal).Value = (modelList[i] as AccountSubjectsModel).YInitialValue;
                    cmd.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar).Value = (modelList[i] as AccountSubjectsModel).CompanyCD;
                    cmd.Parameters.AddWithValue("@SubjectsCD", SqlDbType.VarChar).Value = (modelList[i] as AccountSubjectsModel).SubjectsCD;

                    cmd.CommandText = sql.ToString();
                    cmdList.Add(cmd);
                }
            }
            return SqlHelper.ExecuteTransWithArrayList(cmdList);
        }
        #endregion

        #region  根据ID获取结转下一年的期末余额
        public static DataTable GetJZNextYearEndAmount(string ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select SubjectsCD,isnull(EndAmount,0) as EndAmount from ");
            sql.AppendLine(" officedba.AcountBook  ");
            sql.AppendLine(" where ID in("+ID+") ");

            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 获取结转下一年的期末余额的记录ID
        public static DataTable GetJZNextYearRecordId(string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("  select SubjectsCD,max (ID) as ID ");
            sql.AppendLine("  from officedba.AcountBook where  ");
            sql.AppendLine("  CompanyCD=@CompanyCD ");
            sql.AppendLine("  group by  SubjectsCD ");

            SqlParameter[] parms = 
            {
                new SqlParameter("@CompanyCD",CompanyCD)
            };

            return SqlHelper.ExecuteSql(sql.ToString(), parms);
        }
      #endregion
        #endregion

        #region 获取账簿中所有损益类科目的期末余额  --add by Moshenlin 2009-08-11
        /// <summary>
        /// 获取账簿中所有损益类科目的期末余额
        /// </summary>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns></returns>
        public static DataTable GetProfitandLossInfo(string CompanyCD,string StartDate, string EndDate)
        {
            string sql = "select case when direction='1' then SUM(isnull(ThisCredit,0))-SUM(isnull(ThisDebit,0)) when direction='0' then SUM(isnull(ThisDebit,0))-SUM(isnull(ThisCredit,0)) end as EndM,Direction,SubjectsCD from officedba.acountBook where CompanyCD=@CompanyCD and left(subjectscd,1)='6' and  Voucherdate>=@StartDate and Voucherdate<=@EndDate  group by SubjectsCD,Direction ";

            SqlParameter[] parms = 
           {
               new SqlParameter("@CompanyCD",CompanyCD),
               new SqlParameter("@StartDate",StartDate),
               new SqlParameter("@EndDate",EndDate)
           };
            return SqlHelper.ExecuteSql(sql.ToString(), parms);

        }
        #endregion

      #region 获取账簿中所有资产类及损益类科目的本位币余额与外币余额乘以期末调汇后汇率之差 --add by Moshenlin 2009-08-12
      /// <summary>
      /// 获取账簿中所有资产类及损益类科目的本位币余额与外币余额乘以期末调汇后汇率之差
      /// </summary>
      /// <param name="RateStr">期末处理调整后的汇率集</param>
      /// <param name="CurrencyTypeStr">期末处理调整后的币种主键集</param>
      /// <param name="CompanyCD">公司编码</param>
      /// <param name="StartDate">凭证开始日期</param>
      /// <param name="EndDate">凭证结束日期</param>
      /// <param name="MasterCurrencyType">本位币币种主键</param>
      /// <returns></returns>
      public static DataTable GetTermEndAdjustmentSource(string RateStr, string CurrencyTypeStr, string CompanyCD, string StartDate, string EndDate, string MasterCurrencyType)
      {
          SqlParameter[] parms = 
          {
              new SqlParameter("@RateStr",RateStr),
              new SqlParameter("@CurrencyTypeStr",CurrencyTypeStr),
              new SqlParameter("@CompanyCD",CompanyCD),
              new SqlParameter("@StartDate",StartDate),
              new SqlParameter("@EndDate",EndDate),
              new SqlParameter("@MasterCurrencyType",MasterCurrencyType)
          };
          return SqlHelper.ExecuteStoredProcedure("[officedba].[TermEndAdjustment]", parms);
      }
      #endregion
  }
}
