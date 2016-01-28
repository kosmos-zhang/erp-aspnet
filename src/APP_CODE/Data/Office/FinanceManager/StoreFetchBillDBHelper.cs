/**********************************************
 * 类作用：   存取款单数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/04/28
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
using XBase.Common;

namespace XBase.Data.Office.FinanceManager
{
  public class StoreFetchBillDBHelper
  {

      #region 判断收付款单号是否已存在
      public static bool SFNoisExist(string CompanyCD, string SFNo)
      {
          bool result = false;

          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select count(*)  from officedba.StoreFetchBill");
          sql.AppendLine("where CompanyCD=@CompanyCD ");
          sql.AppendLine("and SFNo=@SFNo ");

          SqlParameter[] parms =
            {
                new SqlParameter("@CompanyCD",CompanyCD),
                new SqlParameter("@SFNo",SFNo)
            };

          object objs = SqlHelper.ExecuteScalar(sql.ToString(), parms);
          if (Convert.ToInt32(objs) > 0)
          {
              result = true;
          }

          return result;
      }
      #endregion

      #region 更新收款单登记凭证状态
      /// <summary>
      /// 根据主键更新存取款单登记凭证状态
      /// </summary>
      /// <param name="ID">主键</param>
      /// <returns>true 成功,false 失败</returns>
      public static bool UpdateAccountStatus(string ID, int Accountor)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Update officedba.StoreFetchBill");
          sql.AppendLine("set IsAccount='1',AccountDate=getdate(),");
          sql.AppendLine("Accountor=@Accountor where ID in ("+ID+")");
          SqlParameter[] parms = new SqlParameter[1];
          parms[0] = SqlHelper.GetParameter("@Accountor", Accountor);


          SqlHelper.ExecuteTransSql(sql.ToString(), parms);

          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }
      #endregion


      #region  确认存取款单
      /// <summary>
      /// 确认存取款单
      /// </summary>
      /// <param name="ID">主键ID</param>
      /// <param name="Confirmor">确认人</param>
      /// <returns>true成功,false失败</returns>
      public static bool ConfirmStoreFetchBill(string ID, string Confirmor)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Update officedba.StoreFetchBill");
          sql.AppendLine("set ConfirmStatus='1' ,Confirmor=@Confirmor,");
          sql.AppendLine("ConfirmDate=getdate() where ID  In( " + ID.Trim() + ")");
          SqlParameter[] parms = new SqlParameter[1];
          parms[0] = SqlHelper.GetParameter("@Confirmor", Confirmor);

          SqlHelper.ExecuteTransSql(sql.ToString(), parms);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }
      #endregion

      #region 反确存取款单
      /// <summary>
      /// 反确存取款单
      /// </summary>
      /// <param name="ID">主键ID</param>
      /// <param name="Confirmor">确认人</param>
      /// <returns>true成功,false失败</returns>
      public static bool ReConfirmStoreFetchBill(string ID)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Update officedba.StoreFetchBill");
          sql.AppendLine("set ConfirmStatus='0' ,Confirmor=NULL,");
          sql.AppendLine("ConfirmDate=NULL where ID  In( " + ID.Trim() + ") ");


          SqlHelper.ExecuteTransSql(sql.ToString());
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }
      #endregion


      #region 添加存取款单信息
      /// <summary>
      /// 添加存取款单信息
      /// </summary>
      /// <param name="model">存取款单实体</param>
      /// <param name="IntID">返回添加后的主键ID</param>
      /// <returns>true 成功,false失败</returns>
      public static bool InsertStoreFetchBill(StoreFetchBillModel model, out int IntID)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Insert into officedba.StoreFetchBill");
          sql.AppendLine("(CompanyCD,SFNo,SFBillNum,SFDate,");
          sql.AppendLine("Executor,BankName,AccountNo,");
          sql.AppendLine("Type,TotalPrice,ModifiedDate,ModifiedUserID,");
          sql.AppendLine("Summary,CurrencyType,CurrencyRate)values(@CompanyCD,@SFNo,@SFBillNum,");
          sql.AppendLine("getdate(),@Executor,@BankName,@AccountNo,");
          sql.AppendLine("@Type,@TotalPrice,getdate(),@ModifiedUserID,@Summary,@CurrencyType,@CurrencyRate)");
          sql.AppendLine("set @IntID= @@IDENTITY");

          SqlParameter[] parms = new SqlParameter[13];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
          parms[1] = SqlHelper.GetParameter("@SFNo", model.SFNo);
          parms[2] = SqlHelper.GetParameter("@SFBillNum", model.SFBillNum);
          parms[3] = SqlHelper.GetParameter("@Executor", model.Executor);

          parms[4] = SqlHelper.GetParameter("@BankName", model.BankName);

          parms[5] = SqlHelper.GetParameter("@AccountNo", model.AccountNo);
          parms[6] = SqlHelper.GetParameter("@Type", model.Type);
          parms[7] = SqlHelper.GetParameter("@TotalPrice", model.TotalPrice);
          parms[8] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
          parms[9] = SqlHelper.GetParameter("@Summary", model.Summary);
          parms[10] = SqlHelper.GetParameter("@CurrencyType", model.CurrencyType);
          parms[11] = SqlHelper.GetParameter("@CurrencyRate", model.CurrencyRate);
          parms[12] = SqlHelper.GetOutputParameter("@IntID", SqlDbType.Int);

          SqlHelper.ExecuteTransSql(sql.ToString(), parms);
          IntID = Convert.ToInt32(parms[12].Value);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }
      #endregion

      #region 修改存取款单
      /// <summary>
      /// 修改存取款单信息
      /// </summary>
      /// <param name="model">存取款单实体</param>
      /// <returns>true 成功,false失败</returns>
      public static bool UpdateStoreFetchBill(StoreFetchBillModel model)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Update  officedba.StoreFetchBill ");
          sql.AppendLine("set SFBillNum=@SFBillNum,SFDate=@SFDate,");
          sql.AppendLine("Executor=@Executor,BankName=@BankName,");
          sql.AppendLine("AccountNo=@AccountNo,Type=@Type,");
          sql.AppendLine("TotalPrice=@TotalPrice,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID,");
          sql.AppendLine("Summary=@Summary,CurrencyType=@CurrencyType,CurrencyRate=@CurrencyRate where ID=@ID");

          SqlParameter[] parms = new SqlParameter[12];
          parms[0] = SqlHelper.GetParameter("@SFBillNum", model.SFBillNum);
          parms[1] = SqlHelper.GetParameter("@SFDate", model.SFDate);
          parms[2] = SqlHelper.GetParameter("@Executor", model.Executor);
          parms[3] = SqlHelper.GetParameter("@BankName", model.BankName);
          parms[4] = SqlHelper.GetParameter("@AccountNo", model.AccountNo);
          parms[5] = SqlHelper.GetParameter("@Type", model.Type);
          parms[6] = SqlHelper.GetParameter("@TotalPrice", model.TotalPrice);
          parms[7] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
          parms[8] = SqlHelper.GetParameter("@Summary", model.Summary);
          parms[9] = SqlHelper.GetParameter("@CurrencyType", model.CurrencyType);
          parms[10] = SqlHelper.GetParameter("@CurrencyRate", model.CurrencyRate);
          parms[11] = SqlHelper.GetParameter("@ID", model.ID);


          SqlHelper.ExecuteTransSql(sql.ToString(), parms);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }
      #endregion

      #region  根据检索条件检错存取款单信息
      /// <summary>
      /// 根据检索条件检错存取款单信息
      /// </summary>
      /// <param name="SFNo">收付款单号</param>
      /// <param name="BillNum">票号</param>
      /// <param name="Type">类别</param>
      /// <param name="Price">金额</param>
      /// <param name="ConfirmStatus">确认状态</param>
      /// <param name="IsAccount">是否登记凭证</param>
      /// <param name="StartDate">开始日期</param>
      /// <param name="EndDate">结束日期</param>
      /// <returns>DataTable</returns>
      public static DataTable SearchStoreFetchBill(string CompanyCD, string SFNo, string SFBillNum, string Type,
          string Price, string ConfirmStatus, string IsAccount, string StartDate, string EndDate, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select a.ID, a.SFNo,a.SFBillNum,");
          sql.AppendLine("convert(varchar(10),a.SFDate,120) as SFDate ,a.[Type],");
          sql.AppendLine("b.EmployeeName as ExecutorName,");
          sql.AppendLine(" a.BankName ,a.AccountNo,a.IsAccount,");
          sql.AppendLine("case when Type='0' then '存款'");
          sql.AppendLine("when [Type]='1' then '取款' end as TypeName,");
          sql.AppendLine("convert(varchar,convert(money,a.TotalPrice),1) as TotalPrice,case when a.ConfirmStatus='0' then '未确认'  when a.ConfirmStatus='1' then '已确认' end as ConfirmStatusName ,");
          sql.AppendLine("case when a.ConfirmDate is not null then  convert(varchar(10), a.ConfirmDate,120)");
          sql.AppendLine("when a.ConfirmDate is null then '' end  as ConfirmDate ,");
          sql.AppendLine("case when a.IsAccount='0' then '未登记'");
          sql.AppendLine("when a.IsAccount='1' then '已登记' end as IsAccountName,");
          sql.AppendLine("case when c.EmployeeName is not null then c.EmployeeName");
          sql.AppendLine("when   c.EmployeeName is  null then  '' end as AccountorName,");
          sql.AppendLine("case when e.EmployeeName is not null then e.EmployeeName ");
          sql.AppendLine("when  e.EmployeeName is null then '' end  as ConfirmorName,a.CurrencyType,isnull(a.CurrencyRate,1) as CurrencyRate ,h.CurrencyName ");
          sql.AppendLine(" from  officedba.StoreFetchBill as a");
          sql.AppendLine("left join  officedba.EmployeeInfo as b");
          sql.AppendLine("on a.Executor=b.ID");
          sql.AppendLine("left join  officedba.EmployeeInfo as c on a.Accountor=c.ID");
          sql.AppendLine("left join  officedba.EmployeeInfo as e  on a.Confirmor=e.ID");
          sql.AppendLine(" left join officedba.CurrencyTypeSetting as h on a.CurrencyType=h.ID ");

          sql.AppendLine("where a.CompanyCD=@CompanyCD");

          //定义查询的命令
          SqlCommand comm = new SqlCommand();
          //添加公司代码参数
          comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

          //收付款单号
          if (!string.IsNullOrEmpty(SFNo))
          {
              sql.AppendLine(" AND a.SFNo LIKE @SFNo ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@SFNo", "%" + SFNo + "%"));
          }
          //票号
          if (!string.IsNullOrEmpty(SFBillNum))
          {
              sql.AppendLine(" AND a.SFBillNum = @SFBillNum ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@SFBillNum", SFBillNum));
          }
          //类型
          if (!string.IsNullOrEmpty(Type))
          {
              sql.AppendLine(" AND a.Type =@Type ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@Type",  Type));
          }
          //金额
          if (!string.IsNullOrEmpty(Price))
          {
              sql.AppendLine(" AND a.TotalPrice like @TotalPrice ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice", "%" + Price + "%"));
          }
          //确认状态
          if (!string.IsNullOrEmpty(ConfirmStatus))
          {
              sql.AppendLine(" AND a.ConfirmStatus=@ConfirmStatus ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmStatus", ConfirmStatus));
          }
          //是否登记凭证
          if (!string.IsNullOrEmpty(IsAccount))
          {
              sql.AppendLine(" AND a.IsAccount=@IsAccount ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsAccount", IsAccount));
          }
          //开始日期
          if (!string.IsNullOrEmpty(StartDate) && string.IsNullOrEmpty(EndDate))
          {
              sql.AppendLine(" AND  convert (varchar(10),a.SFDate,120)   BetWeen @StartDate and  @StartDate ");
              comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
          }
          ////结束日期
          //if (!string.IsNullOrEmpty(EndDate) && string.IsNullOrEmpty(StartDate))
          //{
          //    sql.AppendLine(" AND a.SFDate=@EndDate ");
          //    comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
          //}
          //存取款单开始日期
          if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
          {
              sql.AppendLine(" AND convert (varchar(10),a.SFDate,120)   BetWeen  @StartDate and  @EndDate ");
              comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
              comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
          }
          //指定命令的SQL文
          comm.CommandText = sql.ToString();
          //执行查询
          return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);

      }
      #endregion

      #region 删除存取款单
      /// <summary>
      /// 删除存取款单
      /// </summary>
      /// <param name="ID">主键ID</param>
      /// <returns></returns>
      public static bool DeleteStoreFetchBill(string ID)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Delete from officedba.StoreFetchBill ");
          sql.AppendLine("where ID in ("+ID.Trim()+")");

          SqlHelper.ExecuteTransSql(sql.ToString());
          return SqlHelper.Result.OprateCount > 0 ? true : false;
 
      }
      #endregion

      #region  根据主键ID获取存取款单信息
      /// <summary>
      /// 根据主键ID获取存取款单信息
      /// </summary>
      /// <param name="ID">主键ID</param>
      /// <returns>DataTable</returns>
      public static DataTable GetStoreFetchInfoByID(string ID)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select a.ID, a.SFNo,a.SFBillNum,");
          sql.AppendLine("convert(varchar(10),a.SFDate,120) as SFDate ,a.[Type],");
          sql.AppendLine("b.EmployeeName as ExecutorName,");
          sql.AppendLine(" a.BankName ,a.AccountNo,a.IsAccount,a.Executor,a.Summary,");
          sql.AppendLine(" case when a.[Type]='0' then '存款'  when a.[Type]='1' then '取款' end as TypeName ,");
          sql.AppendLine("a.TotalPrice,case when a.ConfirmStatus='0' then '未确认'  when a.ConfirmStatus='1' then '已确认' end as ConfirmStatusName ,");
          sql.AppendLine("case when a.ConfirmDate is not null then  convert(varchar(10), a.ConfirmDate,120)");
          sql.AppendLine("when a.ConfirmDate is null then '' end  as ConfirmDate ,");
          sql.AppendLine("case when a.IsAccount='0' then '未登记'");
          sql.AppendLine("when a.IsAccount='1' then '已登记' end as IsAccountName,");
          sql.AppendLine("case when c.EmployeeName is not null then c.EmployeeName");
          sql.AppendLine("when   c.EmployeeName is  null then  '' end as AccountorName,a.CurrencyType,isnull(a.CurrencyRate,1) as CurrencyRate ,h.CurrencyName, ");
          sql.AppendLine("case when e.EmployeeName is not null then e.EmployeeName ");
          sql.AppendLine("when  e.EmployeeName is null then '' end  as ConfirmorName,convert(varchar(10),a.AccountDate,120)as AccountDate");
          sql.AppendLine(" from  officedba.StoreFetchBill as a");
          sql.AppendLine("left join  officedba.EmployeeInfo as b");
          sql.AppendLine("on a.Executor=b.ID");
          sql.AppendLine("left join  officedba.EmployeeInfo as c on a.Accountor=c.ID");
          sql.AppendLine("left join  officedba.EmployeeInfo as e  on a.Confirmor=e.ID");
          sql.AppendLine(" left join officedba.CurrencyTypeSetting as h on a.CurrencyType=h.ID ");

          sql.AppendLine("where a.ID=@ID");

          //定义查询的命令
          SqlCommand comm = new SqlCommand();
          // 主键ID参数
          comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));


             //指定命令的SQL文
          comm.CommandText = sql.ToString();
          //执行查询
          return SqlHelper.ExecuteSearch(comm);

 
      }
      #endregion

  }
}
