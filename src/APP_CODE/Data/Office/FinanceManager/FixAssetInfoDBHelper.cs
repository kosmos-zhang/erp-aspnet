/**********************************************
 * 类作用：   固定资产数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/04/03
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Collections;
using System.Text;
using XBase.Model.Office.FinanceManager;
namespace XBase.Data.Office.FinanceManager
{
  public  class FixAssetInfoDBHelper
  {

      #region 判断固定资产编号是否存在
      public static bool FixNoIsExist(string CompanyCD, string FixNo)
      {
          bool result = false;
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select count(FixNo)from officedba.FixAssetInfo");
          sql.AppendLine("where CompanyCD=@CompanyCD and FixNo=@FixNo ");

          SqlParameter[] parms = 
          {
              new SqlParameter("@CompanyCD",CompanyCD),
              new SqlParameter("@FixNo",FixNo)
          };

          object obj = SqlHelper.ExecuteScalar(sql.ToString(),parms);
          if (Convert.ToInt32(obj) > 0)
          {
              result = true;
          }

          return result;
      }
      #endregion


      #region 查询车辆列表
      /// <summary>
      /// 查询车辆列表
      /// </summary>
      /// <returns>DataTable</returns>
      public static DataTable GetCarInfoList(string CarNo, string CarName, string CarMark, string CarType, string CompanyID)
      {
          string sql = "SELECT a.ID, a.CarNo, a.CarName, a.CarMark,"
                        + "CASE WHEN CAST(a.BuyMoney AS varchar)  IS NULL THEN '' WHEN a.BuyMoney IS NOT NULL THEN CAST(a.BuyMoney AS varchar) END AS BuyMoney,"
                        + "CASE a.CarType WHEN 1 THEN '小客'"
                        + "WHEN 2 THEN '大客'"
                        + "WHEN 3 THEN '小货'"
                        + "WHEN 4 THEN '大货'"
                        + "WHEN 5 THEN '其他' End CarType, a.Factory, a.Displacement,"
                        + "convert(varchar(10),a.CreateDate,120)CreateDate,isnull(b.EmployeeName,'') AS Motorman,"
                        + "isnull(b.DeptName,'')DeptName,isnull(c.EmployeeName,'') AS Creator "
                        + "FROM  officedba.CarInfo a "
                        + "LEFT OUTER JOIN "
                        + "(select x.ID,x.EmployeeName,x.EmployeeNo,z.DeptName "
                        + "from officedba.EmployeeInfo x "
                        + "left outer join officedba.DeptInfo z "
                        + "on x.DeptID=z.ID) b "
                        + "ON a.Motorman=b.ID "
                        + "LEFT OUTER JOIN   "
                        + "(select x.ID,x.EmployeeName,x.EmployeeNo,z.DeptName "
                        + "from officedba.EmployeeInfo x "
                        + "LEFT OUTER JOIN officedba.DeptInfo z "
                        + "on x.DeptID=z.ID) c "
                        + "ON a.Creator=c.ID "
                        + " WHERE CompanyCD='" + CompanyID + "'  ";
          if (CarNo != "")
              sql += " and a.CarNo LIKE '%" + CarNo + "%'";
          if (CarName != "")
              sql += " and a.CarName LIKE '%" + CarName + "%'";
          if (CarMark != "")
              sql += " and a.CarMark LIKE '%" + CarMark + "%'";
          if (CarType != "")
              sql += " and a.CarType LIKE '%" + CarType + "%'";

          return SqlHelper.ExecuteSql(sql);
      }
      #endregion


      #region 获取设备信息
      public static DataTable GetEquipmentInfo(string CompanyCD,
          string EquipmentCD, string EquipmentName,
            string EquipmentType, string UsedStatus, string StartDate, string EndDate)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select a.ID,a.EquipmentNo,a.EquipmentName,a.Norm,");
          sql.AppendLine("CONVERT(varchar(10),a.BuyDate,120) as BuyDate,");
          sql.AppendLine("CASE WHEN CAST([Money] AS varchar) ");
          sql.AppendLine("IS NULL THEN '' WHEN [Money] IS NOT NULL THEN ");
          sql.AppendLine("CAST([Money] AS varchar) END AS BuyPrice,");
          sql.AppendLine("case when a.Status='0' then '空闲'");
          sql.AppendLine("when a.status='1' then '使用中'");
          sql.AppendLine("when a.status='2' then '申请维修'");
          sql.AppendLine("when a.status='3' then '维修中'");
          sql.AppendLine("when a.status='4' then '申请报废'");
          sql.AppendLine("end as UsedStatus,");
          sql.AppendLine("b.CodeName");
          sql.AppendLine(" from officedba.EquipmentInfo as a");
          sql.AppendLine("left join officedba.CodeEquipmentType as b");
          sql.AppendLine("on a.[Type]=b.ID where a.CompanyCD=@CompanyCD and");
          sql.AppendLine("a.Status!='5'");

          //定义查询的命令
          SqlCommand comm = new SqlCommand();
          //添加公司代码参数
          comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

          //设备编号
          if (!string.IsNullOrEmpty(EquipmentCD))
          {
              sql.AppendLine(" AND a.EquipmentNo=@EquipmentNo ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentNo", EquipmentCD));
          }
          //设备名称
          if (!string.IsNullOrEmpty(EquipmentName))
          {
              sql.AppendLine(" AND a.EquipmentName = @EquipmentName ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentName", EquipmentName));
          }
          //设备类别
          if (!string.IsNullOrEmpty(EquipmentType))
          {
              sql.AppendLine(" AND a.Type = @EquipmentType ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentType", EquipmentType));
          }
          //使用状态
          if (!string.IsNullOrEmpty(UsedStatus))
          {
              sql.AppendLine(" AND a.status=@status ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@status", UsedStatus));
          }

          //购买时间
          if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
          {
              sql.AppendLine(" AND CONVERT(varchar(10),a.BuyDate,120) BetWeen  @StartDate and @EndDate ");
              comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartDate));
              comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
          }

          //指定命令的SQL文
          comm.CommandText = sql.ToString();
          //执行查询
          return SqlHelper.ExecuteSearch(comm);
      }
      #endregion


      #region 获取生成折旧凭证的信息
      public static DataTable GetBuildAttestInfo(string CompanyCD)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select AccuDeprSubjeCD, DeprCostSubjeCD");
          sql.AppendLine(",sum(AmorDeprM)  as  CountSum ");
          sql.AppendLine("from Officedba.FixWithInfo");
          sql.AppendLine("where isnull(EndNetValue,0) >0");
          sql.AppendLine("and CompanyCD=@CompanyCD");
          sql.AppendLine("group by AccuDeprSubjeCD, DeprCostSubjeCD");

          SqlParameter[] parms = 
          {
              new SqlParameter("@CompanyCD",CompanyCD)
          };


          return SqlHelper.ExecuteSql(sql.ToString(),parms);
      }

      #endregion


      #region 添加固定资产计提明细
      /// <summary>
      /// 添加固定资产计提明细
      /// </summary>
      /// <param name="model">实体MODEL</param>
      /// <returns>true 成功,false 失败</returns>
      private static void InsertFixAssetDeprDetailInfo(ArrayList modelList, ArrayList lstCommand)
      {
          if (modelList.Count > 0)
          {
              StringBuilder sql = null;

              SqlCommand CmdFixAssetDetail = null;

              for (int i = 0; i < modelList.Count; i++)
              {
                  CmdFixAssetDetail=new SqlCommand();

                  sql = new StringBuilder();
                  sql.AppendLine("Insert into officedba.FixAssetDeprDetail");
                  sql.AppendLine("(CompanyCD,FixNo,FixName,FixType,");
                  sql.AppendLine("UsedDate,DeprDate,Number,UsedYears,EstimateUse,");
                  sql.AppendLine("OriginalValue,MDeprPrice,TotalDeprPrice,");
                  sql.AppendLine("EndNetValue,TotalImpairment,Creator)");
                  sql.AppendLine("values(@CompanyCD,@FixNo,@FixName,");
                  sql.AppendLine("@FixType,@UsedDate,getdate(),@Number,@UsedYears,@EstimateUse,");
                  sql.AppendLine("@OriginalValue,@MDeprPrice,@TotalDeprPrice,");
                  sql.AppendLine("@EndNetValue,@TotalImpairment,@Creator)");

                  #region 参数赋值   
                  CmdFixAssetDetail.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar).Value =(modelList[i] as FixAssetDeprDetailModel).CompanyCD;
                  CmdFixAssetDetail.Parameters.AddWithValue("@FixNo", SqlDbType.VarChar).Value = (modelList[i] as FixAssetDeprDetailModel).FixNo;
                  CmdFixAssetDetail.Parameters.AddWithValue("@FixName", SqlDbType.VarChar).Value = (modelList[i] as FixAssetDeprDetailModel).FixName;
                  CmdFixAssetDetail.Parameters.AddWithValue("@FixType", SqlDbType.Int).Value = (modelList[i] as FixAssetDeprDetailModel).FixType;
                  CmdFixAssetDetail.Parameters.AddWithValue("@UsedDate", SqlDbType.DateTime).Value = (modelList[i] as FixAssetDeprDetailModel).UsedDate;
                  CmdFixAssetDetail.Parameters.AddWithValue("@Number", SqlDbType.Int).Value = (modelList[i] as FixAssetDeprDetailModel).Number;
                  CmdFixAssetDetail.Parameters.AddWithValue("@UsedYears", SqlDbType.Decimal).Value = (modelList[i] as FixAssetDeprDetailModel).UsedYears;
                  CmdFixAssetDetail.Parameters.AddWithValue("@EstimateUse", SqlDbType.Decimal).Value = (modelList[i] as FixAssetDeprDetailModel).EstimateUse;
                  CmdFixAssetDetail.Parameters.AddWithValue("@OriginalValue", SqlDbType.Decimal).Value = (modelList[i] as FixAssetDeprDetailModel).OriginalValue;
                  CmdFixAssetDetail.Parameters.AddWithValue("@MDeprPrice", SqlDbType.Decimal).Value = (modelList[i] as FixAssetDeprDetailModel).MDeprPrice;
                  CmdFixAssetDetail.Parameters.AddWithValue("@TotalDeprPrice", SqlDbType.Decimal).Value = (modelList[i] as FixAssetDeprDetailModel).TotalDeprPrice;
                  CmdFixAssetDetail.Parameters.AddWithValue("@EndNetValue", SqlDbType.Decimal).Value = (modelList[i] as FixAssetDeprDetailModel).EndNetValue;
                  CmdFixAssetDetail.Parameters.AddWithValue("@TotalImpairment", SqlDbType.Decimal).Value = (modelList[i] as FixAssetDeprDetailModel).TotalImpairment;
                  CmdFixAssetDetail.Parameters.AddWithValue("@Creator", SqlDbType.Int).Value = (modelList[i] as FixAssetDeprDetailModel).Creator;
                  #endregion


                  CmdFixAssetDetail.CommandText = sql.ToString();
                  lstCommand.Add(CmdFixAssetDetail);
              }  
          }
      }
      #endregion


      #region 添加已期末处理的项目信息
      private static void InsertPeriodProcedReturnParms(
        string CompanyCD, string ItemID, string PeriodNum, ArrayList lstCommand, ref SqlParameter parms)
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
          CmdPeriodProced.Parameters.AddWithValue("@IsAccount", SqlDbType.VarChar).Value = "1";
          //定义返回值参数
          SqlParameter Ret = new SqlParameter("@IntID", SqlDbType.Int);
          Ret.Direction = ParameterDirection.Output;
          CmdPeriodProced.Parameters.Add(Ret);

          parms = Ret;


          #endregion

          CmdPeriodProced.CommandText = sql.ToString();
          lstCommand.Add(CmdPeriodProced);
      }
      #endregion


      #region 添加已期末处理的项目信息
      private static void InsertPeriodProced(
        string CompanyCD , string ItemID, string PeriodNum, ArrayList lstCommand)
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
          CmdPeriodProced.Parameters.AddWithValue("@IsAccount", SqlDbType.VarChar).Value = "1";
          //定义返回值参数
          SqlParameter Ret = new SqlParameter("@IntID", SqlDbType.Int); 
          Ret.Direction = ParameterDirection.Output;
          CmdPeriodProced.Parameters.Add(Ret);



          #endregion

          CmdPeriodProced.CommandText = sql.ToString();
          lstCommand.Add(CmdPeriodProced);
      }
      #endregion


      #region  生成固定资产折旧明细
      public static bool BuildDepreDetailInfo(ArrayList modelList)
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


      #region 生成固定资产折旧凭证
      public static bool BuildDepreAttestBill(AttestBillModel Attestmodel,out int ID,string ItemID, string PeriodNum)
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
          CmdAttest.Parameters.AddWithValue("@VoucherDate", SqlDbType.DateTime).Value = Attestmodel.VoucherDate;
          CmdAttest.Parameters.AddWithValue("@AttestNo", SqlDbType.VarChar).Value = Attestmodel.AttestNo;
          CmdAttest.Parameters.AddWithValue("@AttestName", SqlDbType.VarChar).Value = Attestmodel.AttestName;
          CmdAttest.Parameters.AddWithValue("@Creator", SqlDbType.Int).Value = Attestmodel.Creator;
          CmdAttest.Parameters.AddWithValue("@status", SqlDbType.Int).Value = Attestmodel.status;
          CmdAttest.Parameters.AddWithValue("@FromTbale", SqlDbType.VarChar).Value = Attestmodel.FromTbale;
          CmdAttest.Parameters.AddWithValue("@FromValue", SqlDbType.VarChar).Value = Attestmodel.FromValue;


          SqlParameter Ret = new SqlParameter("@IntID", SqlDbType.Int); //定义返回值参数 
          Ret.Direction = ParameterDirection.Output;
          CmdAttest.Parameters.Add(Ret);

          CmdAttest.CommandText = Attestsql.ToString();

          ArrayList cmdList = new ArrayList();
          cmdList.Add(CmdAttest);


         // InsertPeriodProced(Attestmodel.CompanyCD, ItemID, PeriodNum, cmdList);//添加已期末处理的项目信息
       //   UpdateEndItemIsAccount(ItemID, PeriodNum,cmdList);//更新


           result = SqlHelper.ExecuteTransWithArrayList(cmdList);
           ID = Convert.ToInt32(Ret.Value);

           return result;
      }
      #endregion


      #region 更新期末项目登证状态
      public static void UpdateEndItemIsAccount(string ItemID,string PeriodNum, ArrayList lstCommand)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Update officedba.EndItemProcSetting ");
          sql.AppendLine(" set PeriodNum=@PeriodNum,IsAccount=@IsAccount  where ID=@ID");

          //SqlParameter[] parms = 
          //{
          //    new SqlParameter("@ID",ItemID),
          //    new SqlParameter("@PeriodNum",PeriodNum),
          //    new SqlParameter("@IsAccount",'1')
          //};

          SqlCommand cmdOprt = new SqlCommand();
          cmdOprt.CommandText = sql.ToString();

          cmdOprt.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = ItemID;
          cmdOprt.Parameters.AddWithValue("@PeriodNum", SqlDbType.VarChar).Value = PeriodNum;
          cmdOprt.Parameters.AddWithValue("@IsAccount", SqlDbType.VarChar).Value = '1';

          lstCommand.Add(cmdOprt);
      }
      #endregion


      #region 更新期末净值
      /// <summary>
      /// 固定资产折旧更新期末净值
      /// </summary>
      /// <param name="EndNetValue">期末净值</param>
      /// <param name="CompanyCD">企业编码</param>
      /// <param name="FixNo">固定资产编号</param>
      /// <param name="AmorDeprRate">月折旧率</param>
      /// <param name="AmorDeprM">月折旧额</param>
      /// <param name="DeprStatus">折旧状态</param>
      /// <returns>true 成功,false 失败</returns>
      public static bool UpdateEndNetValue(decimal EndNetValue, string CompanyCD,
          string FixNo, decimal AmorDeprM,decimal AmorDeprRate, decimal TotalDeprPrice,
          FixAssetDeprDetailModel model)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Update officedba.FixWithInfo set EndNetValue=@EndNetValue, ");
          sql.AppendLine("AmorDeprRate=@AmorDeprRate,AmorDeprM=@AmorDeprM,");
          sql.AppendLine("TotalDeprPrice=@TotalDeprPrice ");
          sql.AppendLine("where CompanyCD=@CompanyCD and FixNo=@FixNo");

          SqlCommand cmdUpdateNetValue = new SqlCommand();

          #region
          cmdUpdateNetValue.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar).Value = CompanyCD;
          cmdUpdateNetValue.Parameters.AddWithValue("@EndNetValue", SqlDbType.Decimal).Value = EndNetValue;
          cmdUpdateNetValue.Parameters.AddWithValue("@FixNo", SqlDbType.VarChar).Value = FixNo;
          cmdUpdateNetValue.Parameters.AddWithValue("@AmorDeprRate", SqlDbType.Decimal).Value = AmorDeprRate;
          cmdUpdateNetValue.Parameters.AddWithValue("@AmorDeprM", SqlDbType.Decimal).Value = AmorDeprM;
          cmdUpdateNetValue.Parameters.AddWithValue("@TotalDeprPrice", SqlDbType.Decimal).Value = TotalDeprPrice;
          #endregion
          cmdUpdateNetValue.CommandText = sql.ToString();
          ArrayList listCmd = new ArrayList();
          listCmd.Add(cmdUpdateNetValue);
        //  InsertFixAssetDeprDetailInfo(model, listCmd);//添加固定资产计提明细
          return SqlHelper.ExecuteTransWithArrayList(listCmd);
      }
      #endregion


      #region 更新固定资产折旧
      public static bool UpdateEndFixAssetInfo(ArrayList DeprDetailList,
          ArrayList DeprPeriodList, string CompanyCD, string PeriodNum, string ItemID, ref int PeriodID, ArrayList FixDeprAfterList)
      {
          bool result = false;
          SqlCommand cmdDeprDetail = null;
          StringBuilder sql = null;
          ArrayList cmdlist = new ArrayList();
          if (DeprPeriodList.Count > 0)
          {
              for (int i = 0; i < DeprPeriodList.Count; i++)
              {
                  cmdDeprDetail = new SqlCommand();
                  sql = new StringBuilder();

                  sql.AppendLine("Update officedba.FixWithInfo set EndNetValue=@EndNetValue, ");
                  sql.AppendLine("AmorDeprRate=@AmorDeprRate,AmorDeprM=@AmorDeprM,");
                  sql.AppendLine("TotalDeprPrice=TotalDeprPrice+@TotalDeprPrice ");
                  sql.AppendLine("where CompanyCD=@CompanyCD and FixNo=@FixNo");

                  cmdDeprDetail.Parameters.AddWithValue("@EndNetValue", SqlDbType.Decimal).Value = (DeprPeriodList[i] as FixAssetPeriodDeprModel).EndNetValue;
                  cmdDeprDetail.Parameters.AddWithValue("@AmorDeprRate", SqlDbType.Decimal).Value = (DeprPeriodList[i] as FixAssetPeriodDeprModel).AmorDeprRate;
                  cmdDeprDetail.Parameters.AddWithValue("@AmorDeprM", SqlDbType.Decimal).Value = (DeprPeriodList[i] as FixAssetPeriodDeprModel).AmorDeprM;
                  cmdDeprDetail.Parameters.AddWithValue("@TotalDeprPrice", SqlDbType.Decimal).Value = (DeprPeriodList[i] as FixAssetPeriodDeprModel).TotalDeprPrice;
                  cmdDeprDetail.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar).Value = (DeprPeriodList[i] as FixAssetPeriodDeprModel).CompanyCD;
                  cmdDeprDetail.Parameters.AddWithValue("@FixNo", SqlDbType.VarChar).Value = (DeprPeriodList[i] as FixAssetPeriodDeprModel).FixNo;

                  cmdDeprDetail.CommandText = sql.ToString();
                  cmdlist.Add(cmdDeprDetail);
              }
          }
          //添加固定资产计提明细
          InsertFixAssetDeprDetailInfo(DeprDetailList, cmdlist);
          SqlParameter Ret = null;

          //添加固定资产处理期间信息
          InsertPeriodProcedReturnParms(CompanyCD, ItemID, PeriodNum, cmdlist,ref Ret);

          //添加固定资产计提前资产信息
          InsertFixDeprAfterInfo(FixDeprAfterList, cmdlist);

          result = SqlHelper.ExecuteTransWithArrayList(cmdlist);

          
          PeriodID = Convert.ToInt32(Ret.Value); 
          return result;
      }
      #endregion


      #region  检索企业是否有固定资产
      public static bool FixAssestIsexist(string CompanyCD)
      {
          bool result = false;

          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select count(*)  from officedba.FixAssetInfo");
          sql.AppendLine("where CompanyCD=@CompanyCD ");

          SqlParameter[] parms =
            {
                new SqlParameter("@CompanyCD",CompanyCD)
            };

          object objs = SqlHelper.ExecuteScalar(sql.ToString(), parms);
          if (Convert.ToInt32(objs) > 0)
          {
              result = true;
          }

          return result;
      }
      #endregion


      #region  添加固定资产计提前资产信息
      private static void InsertFixDeprAfterInfo(ArrayList DeprPeriodList, 
           ArrayList cmdlist)        
      {
          SqlCommand cmdDeprDetailAfter = null;
          StringBuilder sql = null;
          if (DeprPeriodList.Count > 0)
          {
              for (int i = 0; i < DeprPeriodList.Count; i++)
              {
                  cmdDeprDetailAfter = new SqlCommand();
                  sql = new StringBuilder();
                  sql.AppendLine("Insert into officedba.FixPeriodDeprDetails");
                  sql.AppendLine("(CompanyCD,FixNo,PeriodNum,EndNetValue,TotalDeprPrice,AmorDeprRate,AmorDeprM)");
                  sql.AppendLine("values(@CompanyCD,@FixNo,@PeriodNum,@EndNetValue,@TotalDeprPrice,@AmorDeprRate,@AmorDeprM)");

                  cmdDeprDetailAfter.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar).Value = (DeprPeriodList[i] as FixAssetDeprAfterModel).CompanyCD;
                  cmdDeprDetailAfter.Parameters.AddWithValue("@FixNo", SqlDbType.VarChar).Value = (DeprPeriodList[i] as FixAssetDeprAfterModel).FixNo;
                  cmdDeprDetailAfter.Parameters.AddWithValue("@PeriodNum", SqlDbType.Int).Value = (DeprPeriodList[i] as FixAssetDeprAfterModel).PeriodNum;
                  cmdDeprDetailAfter.Parameters.AddWithValue("@EndNetValue", SqlDbType.Decimal).Value = (DeprPeriodList[i] as FixAssetDeprAfterModel).EndNetValue;
                  cmdDeprDetailAfter.Parameters.AddWithValue("@TotalDeprPrice", SqlDbType.Decimal).Value = (DeprPeriodList[i] as FixAssetDeprAfterModel).TotalDeprPrice;
                  cmdDeprDetailAfter.Parameters.AddWithValue("@AmorDeprRate", SqlDbType.Decimal).Value = (DeprPeriodList[i] as FixAssetDeprAfterModel).AmorDeprRate;
                  cmdDeprDetailAfter.Parameters.AddWithValue("@AmorDeprM", SqlDbType.Decimal).Value = (DeprPeriodList[i] as FixAssetDeprAfterModel).AmorDeprM;
                  cmdDeprDetailAfter.CommandText = sql.ToString();
                  cmdlist.Add(cmdDeprDetailAfter);
              }
          }
      }
      #endregion

      #region 获取当前企业的固定资产计算信息
      public static DataTable GetAssetInfoByCompanyCD(string CompanyCD,string PeriodNum)
      {
          PeriodNum = PeriodNum.Substring(0, 4) + "-" + PeriodNum.Substring(4, PeriodNum.Length - 4);
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select isnull(a.ReduValueRe,0) as ReduValueRe ,isnull(b.TotalDeprPrice,0) as TotalDeprPrice ,a.FixName,a.FixNo,a.FixType,a.FixNumber,a.OriginalValue,");
          sql.AppendLine("CONVERT(varchar(10), b.UseDate, 120) as UseDate,isnull(b.EstimateUse,0) as EstimateUse,isnull(b.AmorDeprM,0) as  AmorDeprM,");
          sql.AppendLine("isnull(b.CurrValueRe,0) as CurrValueRe ,a.CompanyCD,a.FixNo,b.EstiResiValue,a.OriginalValue,");
          sql.AppendLine("b.EstiWorkLoad,b.CountMethod,b.EndNetValue,b.AmorDeprRate,isnull(b.UsedYear,0) as UsedYear,AccuDeprSubjeCD,DeprCostSubjeCD from officedba.FixAssetInfo as a");
          sql.AppendLine("left join officedba.FixWithInfo as b on a.FixNo=b.FixNo");
          sql.AppendLine("where a.CompanyCD=@CompanyCD ");
          sql.AppendLine(" and  cast(year( b.UseDate) as varchar) +'-' + cast(month( b.UseDate) as varchar)<@PeriodNum");
          //sql.AppendLine(" and  MONTH(b.UseDate)< MONTH(getdate())");

          SqlParameter[] parms = 
          {
              new SqlParameter("@CompanyCD",CompanyCD),
              new SqlParameter("@PeriodNum",PeriodNum)
          };

          return SqlHelper.ExecuteSql(sql.ToString(), parms);
      }
      #endregion

      #region 添加固定资产信息及计提信息
      /// <summary>
      /// 添加固定资产信息及计提信息
      /// </summary>
      /// <param name="FixInfoModel">固定资产实体</param>
      /// <param name="FixWithModel">固定资产计提实体</param>
      /// <returns>true 成功，false 失败</returns>
      public static bool InsertFixAssetInfo(FixAssetInfoModel FixInfoModel,FixWithInfoModel FixWithModel)
      {
          string []sqlArray=new string[2];
          //固定资产
          StringBuilder sql_FixInfo = new StringBuilder();
          FixWithModel.CompanyCD = FixInfoModel.CompanyCD;
          sql_FixInfo.AppendLine("Insert into officedba.FixAssetInfo");
          sql_FixInfo.AppendLine("(CompanyCD,FixNo,FixName,FixType,FixSpec,");
          sql_FixInfo.AppendLine("FixModel,FixNumber,Unit,OriginalValue,");
          sql_FixInfo.AppendLine("UseDept,StorePlace,RespPerson,RegisterDate,BegiDisCount,");
          sql_FixInfo.AppendLine("ReduValueRe,NetValue,UsedStatus,ModifiedDate,ModifiedUserID,Remark)");
          sql_FixInfo.AppendLine("values('" + FixInfoModel.CompanyCD + "','" + FixInfoModel.FixNo + "','" + FixInfoModel.FixName+ "','" + FixInfoModel.FixType + "',");
          sql_FixInfo.AppendLine("'" + FixInfoModel.FixSpec + "','" + FixInfoModel.FixModel + "','" + FixInfoModel.FixNumber + "', '" + FixInfoModel .Unit+ "',");
          sql_FixInfo.AppendLine(" '" + FixInfoModel.OriginalValue + "','" + FixInfoModel.UseDept + "','" + FixInfoModel.StorePlace + "','" +FixInfoModel. RespPerson + "', ");
          sql_FixInfo.AppendLine(" '" + FixInfoModel.RegisterDate + "','" + FixInfoModel.BegiDisCount + "','" + FixInfoModel.ReduValueRe + "','" + FixInfoModel.NetValue + "' ,'" + FixInfoModel.UsedStatus + "',getdate(),'" + FixInfoModel.ModifiedUserID + "','" + FixInfoModel .Remark+ "' )");

          //固定资产计提
          StringBuilder sql_FixWith = new StringBuilder();
          sql_FixWith.AppendLine("Insert into officedba.FixWithInfo (CompanyCD,FixNo,UseDate,CountMethod,EstimateUse,");
          sql_FixWith.AppendLine("UsedYear,EstiResiValue,AccuDeprSubjeCD,DeprCostSubjeCD,EstiWorkLoad,AmorDeprRate,AmorDeprM");
          sql_FixWith.AppendLine(",CurrValueRe,DeprStatus,EndNetValue,MonthWorkLoad,Remark)");
          sql_FixWith.AppendLine("values('" + FixWithModel.CompanyCD + "','" + FixWithModel.FixNo + "','" + FixWithModel.UseDate + "','" + FixWithModel.CountMethod + "'");
          sql_FixWith.AppendLine(", '" + FixWithModel.EstimateUse + "','" + FixWithModel.UsedYear + "','" + FixWithModel.EstiResiValue + "','" + FixWithModel.AccuDeprSubjeCD + "','" + FixWithModel.DeprCostSubjeCD + "', ");
          sql_FixWith.AppendLine("'" + FixWithModel.EstiWorkLoad + "','" + FixWithModel.AmorDeprRate + "','" + FixWithModel.AmorDeprM + "','" + FixWithModel.CurrValueRe + "','" + FixWithModel.DeprStatus + "','" + FixWithModel.EndNetValue + "' ,'" + FixWithModel.MonthWorkLoad + "','" + FixWithModel.Remark + "' )");
          if (sql_FixInfo != null && sql_FixWith != null)
          {
              sqlArray[0] = sql_FixInfo.ToString();
              sqlArray[1] = sql_FixWith.ToString();
          }
          return SqlHelper.ExecuteTransForListWithSQL(sqlArray);
      }
      #endregion

      #region  修改固定资产信息及计提信息
      /// <summary>
      /// 修改固定资产信息及计提信息
      /// </summary>
      /// <param name="FixInfoModel">固定资产实体</param>
      /// <param name="FixWithModel">固定资产计提实体</param>
      /// <returns>true 成功，false 失败</returns>
      public static bool UpdateFixAssetInfo(FixAssetInfoModel FixInfoModel, FixWithInfoModel FixWithModel)
      {
          //固定资产
          string[] sqlArray = new string[2];
          StringBuilder sql_FixInfo = new StringBuilder();
          sql_FixInfo.AppendLine("update officedba.FixAssetInfo set ");
          sql_FixInfo.AppendLine("[FixName] = '" + FixInfoModel .FixName+ "',");
          sql_FixInfo.AppendLine("[FixType] ='" + FixInfoModel .FixType+ "',");
          sql_FixInfo.AppendLine("[FixSpec] = '"+FixInfoModel.FixSpec+"',");
          sql_FixInfo.AppendLine("[FixModel] ='"+FixInfoModel.FixModel+"',");
          sql_FixInfo.AppendLine("[FixNumber] = '" + FixInfoModel .FixNumber+ "',");
          sql_FixInfo.AppendLine("[Unit] = '" + FixInfoModel .Unit+ "',");
          sql_FixInfo.AppendLine("[OriginalValue] = '" + FixInfoModel.OriginalValue + "',");
          sql_FixInfo.AppendLine("[UseDept] = '"+FixInfoModel.UseDept+"',");
          sql_FixInfo.AppendLine("[StorePlace] = '"+FixInfoModel.StorePlace+"',");
          sql_FixInfo.AppendLine("[RespPerson] = '" + FixInfoModel.RespPerson + "',");
          sql_FixInfo.AppendLine("[RegisterDate] = '" + FixInfoModel.RegisterDate + "',");
          sql_FixInfo.AppendLine("[BegiDisCount] = '" + FixInfoModel.BegiDisCount + "',");
          sql_FixInfo.AppendLine("[ReduValueRe] = '" + FixInfoModel.ReduValueRe + "',");
          sql_FixInfo.AppendLine("[NetValue] ='"+FixInfoModel.NetValue+"',");
          sql_FixInfo.AppendLine("[UsedStatus] = '"+FixInfoModel.UsedStatus+"',");
          sql_FixInfo.AppendLine("[ModifiedDate] = getdate(),");
          sql_FixInfo.AppendLine("[ModifiedUserID] ='"+FixInfoModel.ModifiedUserID+"',");
          sql_FixInfo.AppendLine("[Remark] ='" + FixInfoModel.Remark + "' where CompanyCD='" + FixInfoModel.CompanyCD + "' and FixNo='" + FixInfoModel.FixNo + "'");

          //固定资产计提
          StringBuilder sql_FixWith = new StringBuilder();
          sql_FixWith.AppendLine("update officedba.FixWithInfo set ");
          sql_FixWith.AppendLine("[UseDate] = '" + FixWithModel.UseDate + "',");
          sql_FixWith.AppendLine("[CountMethod] = '" + FixWithModel.CountMethod + "',");
          sql_FixWith.AppendLine("[EstimateUse] = '" + FixWithModel.EstimateUse + "',");
          sql_FixWith.AppendLine("[UsedYear] = '" + FixWithModel.UsedYear + "',");
          sql_FixWith.AppendLine("[EstiResiValue] = '" + FixWithModel.EstiResiValue + "',");
          sql_FixWith.AppendLine("[AccuDeprSubjeCD] = '" + FixWithModel.AccuDeprSubjeCD + "',");
          sql_FixWith.AppendLine("[EstiWorkLoad] = '" + FixWithModel.EstiWorkLoad + "',");
          sql_FixWith.AppendLine("[AmorDeprRate] = '" + FixWithModel.AmorDeprRate + "',");
          sql_FixWith.AppendLine("[AmorDeprM] = '" + FixWithModel.AmorDeprM + "',");
          sql_FixWith.AppendLine("[CurrValueRe] = '" + FixWithModel.CurrValueRe + "',");
          sql_FixWith.AppendLine("[DeprStatus] = '" + FixWithModel.DeprStatus + "',");
          sql_FixWith.AppendLine("[EndNetValue] = '" + FixWithModel.EndNetValue + "',");
          sql_FixWith.AppendLine("[MonthWorkLoad] = '" + FixWithModel.MonthWorkLoad + "',");
          sql_FixWith.AppendLine("[Remark] = '" + FixWithModel .Remark+ "'");
          sql_FixWith.AppendLine("Where CompanyCD='" + FixInfoModel.CompanyCD + "' and FixNo='" + FixWithModel.FixNo + "' ");
          if (sql_FixInfo != null && sql_FixWith != null)
          {
              sqlArray[0] = sql_FixInfo.ToString();
              sqlArray[1] = sql_FixWith.ToString();
          }
          return SqlHelper.ExecuteTransForListWithSQL(sqlArray);

        
      }
      #endregion

      #region 删除资产计提信息
      /// <summary>
      /// 删除资产计提信息
      /// </summary>
      /// <param name="lstCommand">命令列表</param>
      /// <param name="companyCD">公司代码</param>
      /// <param name="FixNo">资产编号</param>
      /// <returns></returns>
      private static void DeleteFixWithInfo(ArrayList lstCommand, string companyCD, string FixNo)
      {
          //删除SQL拼写
          StringBuilder deleteSql = new StringBuilder();
          deleteSql.AppendLine(" DELETE FROM officedba.FixWithInfo ");
          deleteSql.AppendLine(" WHERE ");
          deleteSql.AppendLine(" FixNo In( " + FixNo + " ) ");
          deleteSql.AppendLine(" AND CompanyCD = @CompanyCD ");

          //定义更新基本信息的命令
          SqlCommand comm = new SqlCommand();
          comm.CommandText = deleteSql.ToString();

          //设置参数
          comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
          //添加命令
          lstCommand.Add(comm);
      }
      #endregion

      #region 删除固定资产信息
      /// <summary>
      /// 删除固定资产信息
      /// </summary>
      /// <param name="MID">资产信息主键</param>
      /// <param name="DID">计提信息主键</param>
      /// <returns>true 成功，false 失败</returns>
      public static bool DeleteFixAssetInfo(string CompanyCD, string FixNo)
      {

          //删除SQL拼写
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("delete from officedba.FixAssetInfo ");
          sql.AppendLine(" WHERE ");
          sql.AppendLine(" FixNo In( " + FixNo + ")");
          sql.AppendLine(" AND CompanyCD = @CompanyCD ");

          //定义更新基本信息的命令
          SqlCommand comm = new SqlCommand();
          comm.CommandText = sql.ToString();

          //设置参数
          comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
          ArrayList listDelete = new ArrayList();
          listDelete.Add(comm);
          //删除计提信息
          DeleteFixWithInfo(listDelete,CompanyCD,FixNo);
          return SqlHelper.ExecuteTransWithArrayList(listDelete);
      }
      #endregion


      #region 获取固定资产信息及计提信息_打印
      /// <summary>
      /// 获取固定资产信息及计提信息
      /// </summary>
      /// <param name="ID">固定资产编号</param>
      /// <returns>DataSet</returns>
      public static DataTable GetFixiInfoByNO(string CompanyCD,string FixNo)
      {
          DataSet FixSet = new DataSet();
          StringBuilder sql = new StringBuilder();
          sql.AppendLine(" SELECT  TOP (1) a_1.CompanyCD, a_1.ID, a_1.FixNo, a_1.FixName, (c_1.TypeName)FixType, a_1.FixSpec,  ");
          sql.AppendLine("  a_1.FixModel, a_1.FixNumber, a_1.Unit, a_1.OriginalValue, ");
          sql.AppendLine(" a_1.UseDept, a_1.StorePlace, a_1.RespPerson, a_1.RegisterDate, ");
          sql.AppendLine("a_1.BegiDisCount, a_1.ReduValueRe, a_1.NetValue, a_1.UsedStatusName,  ");
          sql.AppendLine(" a_1.UsedStatus, a_1.Remark, a_1.DeptName, a_1.EmployeeName, ");
          sql.AppendLine(" b_1.UseDate, b_1.CompanyCD1, b_1.FixNo1, ");
          sql.AppendLine(" (case b_1.CountMethod when '0' then '年限平均法' when '1' then '工作量法' when '2' then '年数总和法' when '3' then '双倍余额递减' else '' end) CountMethod ");
          sql.AppendLine(" , b_1.EstimateUse, b_1.UsedYear, b_1.EstiResiValue, b_1.AccuDeprSubjeCD, ");
          sql.AppendLine("  b_1.DeprCostSubjeCD, b_1.EstiWorkLoad, b_1.AmorDeprRate, b_1.AmorDeprM, ");
          sql.AppendLine("  b_1.CurrValueRe, b_1.EndNetValue, b_1.MonthWorkLoad, b_1.Remark AS Expr1, ");
          sql.AppendLine(" b_1.AccuDeprSubjeName, b_1.DeprCostName ");
          sql.AppendLine(" FROM  ");
          sql.AppendLine(" (SELECT     a.CompanyCD, a.ID, a.FixNo, a.FixName, a.FixType, a.FixSpec, ");
          sql.AppendLine(" a.FixModel, a.FixNumber, a.Unit, a.OriginalValue, a.UseDept, a.StorePlace, ");
          sql.AppendLine(" a.RespPerson, CONVERT(VARCHAR(10), a.RegisterDate, 21) AS RegisterDate, a.BegiDisCount, a.ReduValueRe, a.NetValue, ");
          sql.AppendLine(" CASE WHEN a.UsedStatus = '0' THEN '未使用' WHEN a.UsedStatus = '1' THEN '使用' END AS UsedStatusName, a.UsedStatus, a.Remark,  ");
          sql.AppendLine(" d.DeptName, e.EmployeeName ");
          sql.AppendLine(" FROM  officedba.FixAssetInfo AS a LEFT OUTER JOIN ");
          sql.AppendLine(" officedba.DeptInfo AS d ON a.UseDept = d.ID LEFT OUTER JOIN ");
          sql.AppendLine(" officedba.EmployeeInfo AS e ON a.RespPerson = e.ID) AS a_1 INNER JOIN ");
          sql.AppendLine(" (SELECT   CONVERT(VARCHAR(10), f.UseDate, 21) AS UseDate, f.CompanyCD AS CompanyCD1, f.FixNo AS FixNo1, f.CountMethod, f.EstimateUse, ");
          sql.AppendLine(" f.UsedYear, f.EstiResiValue, f.AccuDeprSubjeCD, f.DeprCostSubjeCD, f.EstiWorkLoad, f.AmorDeprRate, f.AmorDeprM, f.CurrValueRe,  ");
          sql.AppendLine(" f.EndNetValue, f.MonthWorkLoad, f.Remark, b.SubjectsName AS AccuDeprSubjeName, c.SubjectsName AS DeprCostName ");
          sql.AppendLine(" FROM  officedba.FixWithInfo AS f LEFT OUTER JOIN ");
          sql.AppendLine(" officedba.AccountSubjects AS b ON f.AccuDeprSubjeCD = b.SubjectsCD LEFT OUTER JOIN ");
          sql.AppendLine(" officedba.AccountSubjects AS c ON f.DeprCostSubjeCD = c.SubjectsCD) AS b_1 ON a_1.CompanyCD = b_1.CompanyCD1 AND  ");
          sql.AppendLine(" a_1.FixNo = b_1.FixNo1 Left Join officedba.AssetTypeSetting c_1 on a_1.FixType=c_1.Id and a_1.CompanyCD=c_1.CompanyCD ");
          sql.AppendLine(" where a_1.CompanyCD=@CompanyCD and a_1.FixNo=@FixNo ");
          
          SqlParameter[] parms = new SqlParameter[2];
          parms[0] = SqlHelper.GetParameter("@CompanyCD",CompanyCD);
          parms[1] = SqlHelper.GetParameter("@FixNo",FixNo);
          DataTable dt = SqlHelper.ExecuteSql(sql.ToString(), parms);
        
          return dt;
      }
      #endregion

      #region 获取固定资产信息及计提信息
      /// <summary>
      /// 获取固定资产信息及计提信息
      /// </summary>
      /// <param name="ID">固定资产主键</param>
      /// <returns>DataSet</returns>
      public static DataSet GetFixiInfo(string ID)
      {
          DataSet FixSet = new DataSet();
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select a.CompanyCD,a.ID,a.FixNo,a.FixName,a.FixType, ");
          sql.AppendLine("a.FixSpec,a.FixModel,a.FixNumber,a.Unit,");
          sql.AppendLine("a.OriginalValue,a.UseDept,");
          sql.AppendLine("a.StorePlace,a.RespPerson,CONVERT(VARCHAR(10),a.RegisterDate,21) as RegisterDate ,");
          sql.AppendLine("a.BegiDisCount,a.ReduValueRe,a.NetValue,case when a.UsedStatus='0' then");
          sql.AppendLine("'未使用' when a.UsedStatus='1' then '使用'  end as UsedStatusName,a.UsedStatus ,a.Remark,d.DeptName,e.EmployeeName");
          sql.AppendLine("from officedba.FixAssetInfo as a left join officedba.DeptInfo as d on a.UseDept=d.ID ");
          sql.AppendLine("left join officedba.EmployeeInfo as e on a.RespPerson=e.ID where a.ID=@ID");
          SqlParameter[] parms = new SqlParameter[1];
          parms[0] = SqlHelper.GetParameter("@ID",ID);
          DataTable dt = SqlHelper.ExecuteSql(sql.ToString(), parms);
          FixSet.Tables.Add(dt);
          if (dt.Rows.Count > 0)
          {
               //获取公司代码
              string companyCD = dt.Rows[0]["CompanyCD"].ToString();
                //获取固定资产编码
              string FixNo = dt.Rows[0]["FixNo"].ToString();
 
              FixSet.Tables.Add(GetFixWithInfo(companyCD, FixNo));
          }
          return FixSet;
      }
      #endregion

      #region 根据公司编码和资产编号获取计提信息
      /// <summary>
      /// 根据公司编码和资产编号获取计提信息
      /// </summary>
      /// <param name="CompanyCD">公司编码</param>
      /// <param name="FixNo">计提信息</param>
      /// <returns>DataTable</returns>
      private static DataTable GetFixWithInfo(string CompanyCD, string FixNo)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select CONVERT(VARCHAR(10),a.UseDate,21) as UseDate,");
          sql.AppendLine("a.CountMethod,a.EstimateUse,a.UsedYear,a.EstiResiValue,a.AccuDeprSubjeCD,");
          sql.AppendLine("a.DeprCostSubjeCD,a.EstiWorkLoad,a.AmorDeprRate,a.AmorDeprM,a.CurrValueRe,a.EndNetValue,");
          //sql.AppendLine(" case when a.DeprStatus is null then '未计提' when a.DeprStatus is not null then a.DeprStatus end  as  DeprStatus,");
          sql.AppendLine("a.MonthWorkLoad,a.Remark,b.SubjectsName as AccuDeprSubjeName,c.SubjectsName as DeprCostName");
          sql.AppendLine("from officedba.FixWithInfo as a left join  ");
          sql.AppendLine("officedba.AccountSubjects as b on a.AccuDeprSubjeCD=b.SubjectsCD ");
          sql.AppendLine("left join   officedba.AccountSubjects as c on a.DeprCostSubjeCD=c.SubjectsCD");
          sql.AppendLine("where a.CompanyCD=@CompanyCD and a.FixNo=@FixNo");
          SqlParameter[] parms = new SqlParameter[2];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          parms[1] = SqlHelper.GetParameter("@FixNo", FixNo);
           return SqlHelper.ExecuteSql(sql.ToString(), parms);
      }
      #endregion

      #region  汇总固定资产累计折旧额
      public static DataTable CountFixTotalZJE(string CompanyCD)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select  a.FixNo,sum(isnull (a.TotalDeprPrice,0)) as TotalDeprPrice");
          sql.AppendLine("from  Officedba.FixAssetDeprDetail as a");
          sql.AppendLine("where a.CompanyCD=@CompanyCD");
          sql.AppendLine("group by a.CompanyCD,a.FixNo");

          SqlParameter[] parms = {
                                     new SqlParameter("@CompanyCD",CompanyCD)
                                 };

          return SqlHelper.ExecuteSql(sql.ToString(), parms);
      }
      #endregion

      #region 根据企业编码获取企业固定资产信息
      /// <summary>
      /// 根据企业编码获取企业固定资产信息
      /// </summary>
      /// <param name="CompanyCD">企业编码</param>
      /// <returns>DataTable</returns>
      public static DataTable SearchFixAssetInfo(string CompanyCD, string FixNo,
          string FixName, string Type, string FixStatus, string ZJSubjectsCD, string DeptID,
          string StartPeriod, string EndPeriod)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select a.CompanyCD,a.ID,a.FixNo,a.FixName,a.FixType,isnull(a.FixSpec,'') as FixSpec,isnull(a.FixModel,'')as FixModel");
          sql.AppendLine(",a.FixNumber,a.Unit,convert(varchar,convert(money,a.OriginalValue),1) as OriginalValue ,isnull(a.UseDept,'')as UseDept,isnull(a.StorePlace,'')as StorePlace,");
          sql.AppendLine("isnull(a.RespPerson,'')as RespPerson,CONVERT(VARCHAR(10),a.RegisterDate,21) as RegisterDate,convert(varchar,convert(money,a.BegiDisCount),1) as BegiDisCount ,convert(varchar,convert(money,a.ReduValueRe),1) as ReduValueRe  ,convert(varchar,convert(money,a.NetValue),1) as NetValue  ,");
          sql.AppendLine(" case when a.UsedStatus='0' then '停用'when a.UsedStatus='1' then '启用' end as UsedStatusName ,a.UsedStatus,case when b.TypeName is not null then b.TypeName  when b.TypeName is null then '' end as  TypeName ");
          sql.AppendLine(" ,isnull(convert(varchar,convert(money,c.AmorDeprM),1),'') as AmorDeprM   ,isnull(convert(varchar,convert(money,c.EndNetValue),1),'') as EndNetValue ,isnull (d.DeptName,'') as DeptName");
          sql.AppendLine("from officedba.FixAssetInfo as a ");
          sql.AppendLine("left join officedba.FixWithInfo as c on a.FixNo=c.FixNo  and a.CompanyCD=c.CompanyCD ");
          sql.AppendLine("left join officedba.DeptInfo as d on a.UseDept=d.ID");
          sql.AppendLine("left join officedba.AssetTypeSetting as b on a.FixType=b.ID where a.CompanyCD=@CompanyCD ");


          //定义查询的命令
          SqlCommand comm = new SqlCommand();
          //添加公司代码参数
          comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
          //资产编号
          if (!string.IsNullOrEmpty(FixNo))
          {
              sql.AppendLine(" AND a.FixNo LIKE @FixNo ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@FixNo", "%" + FixNo + "%"));
          }
          //资产名称
          if (!string.IsNullOrEmpty(FixName))
          {
              sql.AppendLine(" AND a.FixName LIKE @FixName ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@FixName", "%" + FixName + "%"));
          }
          //资产类型
          if (!string.IsNullOrEmpty(Type))
          {
              sql.AppendLine(" AND a.FixType = @FixType ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@FixType", Type));
          }
          //使用状态
          if (!string.IsNullOrEmpty(FixStatus))
          {
              sql.AppendLine(" AND a.UsedStatus=@UsedStatus ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", FixStatus));
          }
          //折旧费用科目编号
          if (!string.IsNullOrEmpty(ZJSubjectsCD))
          {
              sql.AppendLine(" AND c.DeprCostSubjeCD=@DeprCostSubjeCD ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeprCostSubjeCD", ZJSubjectsCD));
          }
          //部门ID
          if (!string.IsNullOrEmpty(DeptID))
          {
              sql.AppendLine(" AND a.UseDept=@UseDept ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@UseDept", DeptID));
          }
          //开始期间结束期间
          if (!string.IsNullOrEmpty(StartPeriod) && !string.IsNullOrEmpty(EndPeriod))
          {
              sql.AppendLine(" AND  a.RegisterDate  BetWeen  @StartDate and @EndDate ");
              comm.Parameters.Add(SqlHelper.GetParameter("@StartDate", StartPeriod));
              comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndPeriod));
          }
          //指定命令的SQL文
          comm.CommandText = sql.ToString();
          //执行查询
          return SqlHelper.ExecuteSearch(comm);
      }
      #endregion

      /// <summary>
      ///给定企业编号，资产类别查找在资产类别表中是否存在
      ///zxb 2009-08-27
      /// </summary>
      /// <param name="codename"></param>
      /// <param name="compid"></param>
      /// <returns></returns>
      public static bool ChargeAssetTypeInfo(string codename, string compid)
      {
          SqlParameter[] parameters = { new SqlParameter("@codename", SqlDbType.VarChar, 50),
                                          new SqlParameter("@companyid",SqlDbType.VarChar,50)};
          parameters[0].Value = codename;
          parameters[1].Value = compid;
          object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.AssetTypeSetting where TypeName=@codename and CompanyCD=@companyid", parameters);
          return Convert.ToInt32(obj) > 0 ? true : false;
      }

      /// <summary>
      /// 给定企业编号，资产名称查找在资产名称在表中是否存在
      /// zxb 2009-08-27
      /// </summary>
      /// <param name="codename"></param>
      /// <param name="compid"></param>
      /// <returns></returns>
      public static bool ChargeFixAssetInfo(string codename, string compid)
      {
          SqlParameter[] parameters = { new SqlParameter("@codename", SqlDbType.VarChar, 50),
                                          new SqlParameter("@companyid",SqlDbType.VarChar,50)};
          parameters[0].Value = codename;
          parameters[1].Value = compid;
          object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.FixAssetInfo where FixName=@codename and CompanyCD=@companyid", parameters);
          return Convert.ToInt32(obj) > 0 ? true : false;
      }

      /// <summary>
      /// 从excel导入固定资产信息表
      /// zxb 2009-08-28
      /// </summary>
      /// <param name="companycd"></param>
      /// <param name="fname"></param>
      /// <param name="tbname"></param>
      /// <returns></returns>
      public static int GetExcelToFixAssetInfo(string companycd, string usercode)
      {
          SqlParameter[] param = 
            {
                new SqlParameter("@compcode",companycd),
                new SqlParameter("@createperson",usercode)
            };
          DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[excelIntoSql_FixAsset]", param);
          DataSet ds = new DataSet();
          ds.Tables.Add(dt);
          return ds.Tables[0].Rows.Count; //暂保留返回记录数，备日志使用
      }

      public static DataSet ReadEexcel(string FilePath, string companycd)
      {
          string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;data source=" + FilePath;
          string sql = "SELECT distinct * FROM [Sheet1$]";
          DataSet ds = new DataSet();
          System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(sql, connStr);
          da.Fill(ds);
          //删除历史记录
          SqlParameter[] paramter = { new SqlParameter("@companycd", companycd) };
          sql = "delete from officedba.FixAssetInfo_temp where [企业编号]=@companycd";
          SqlHelper.ExecuteTransSql(sql, paramter);
          //传到临时表中
          for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
          {
              SqlParameter[] param = 
                {
                    new SqlParameter("@companycd",companycd),
                    new SqlParameter("@id",ds.Tables[0].Rows[i][0].ToString()),
                    new SqlParameter("@FixName",ds.Tables[0].Rows[i][1].ToString()),
                    new SqlParameter("@FixType",ds.Tables[0].Rows[i][2].ToString()),
                    new SqlParameter("@FixNumber",ds.Tables[0].Rows[i][3].ToString()),
                    new SqlParameter("@Unit",ds.Tables[0].Rows[i][4].ToString()),
                    new SqlParameter("@OriginalValue",ds.Tables[0].Rows[i][5].ToString()),
                    new SqlParameter("@BegiDisCount",ds.Tables[0].Rows[i][6].ToString().Length<1?"0":ds.Tables[0].Rows[i][6].ToString()),
                    new SqlParameter("@ReduValueRe",ds.Tables[0].Rows[i][7].ToString().Length<1?"0":ds.Tables[0].Rows[i][7].ToString()),
                    new SqlParameter("@useDate",ds.Tables[0].Rows[i][8].ToString()),
                    new SqlParameter("@EstimateUse",ds.Tables[0].Rows[i][9].ToString()),
                    new SqlParameter("@UsedYear",ds.Tables[0].Rows[i][10].ToString()),
                    new SqlParameter("@EstiResiValue",ds.Tables[0].Rows[i][11].ToString())
                };
              
              string lenstr = string.Empty;
              for (int j = 0; j < 12; j++)
              {
                  if (ds.Tables[0].Rows[i][j].ToString().Trim().Length < 1)
                  {
                      lenstr += "#";
                  }
              }
              if (lenstr.Length == 12)
              {
                  continue;
              }
              sql = "insert into officedba.FixAssetInfo_temp values(@id,@companycd,@FixName,@FixType,@FixNumber,@Unit,@OriginalValue,@BegiDisCount,@ReduValueRe,@useDate,@EstimateUse,@UsedYear,@EstiResiValue)";
              SqlHelper.ExecuteTransSql(sql, param);
          }
          sql = "select * from officedba.FixAssetInfo_temp where [企业编号]=@companycd order by [流水号]";
          ds = new DataSet();
          SqlParameter[] paramter1 = { new SqlParameter("@companycd", companycd) };
          DataTable dt = SqlHelper.ExecuteSql(sql, paramter1);
          ds.Tables.Add(dt);
          return ds;
      }

      /// <summary>
      /// 返回从excel向固定资产信息库中设置的特殊资产编号的数据集
      /// zxb 2009-08-28 ,特殊格式为"##@@$$@@##"
      /// </summary>
      /// <param name="companycd"></param>
      /// <returns></returns>
      public static DataSet GetNullFixAssetList(string companycd)
      {
          SqlParameter[] param = 
            {
                new SqlParameter("@compcode",companycd)
            };
          DataTable dt = SqlHelper.ExecuteSql("select * from officedba.FixAssetInfo where CompanyCD=@compcode and FixNo='##@@$$@@##'", param);
          DataSet ds = new DataSet();
          ds.Tables.Add(dt);
          return ds;
      }

      /// <summary>
      /// 获取固定资产编号设置规则
      /// zxb 2009-08-28
      /// </summary>
      /// <param name="companycd"></param>
      /// <returns></returns>
      public static int GetCodeRuleID(string companycd)
      {
          SqlParameter[] param = 
            {
                new SqlParameter("@compcode",companycd)
            };
          DataTable dt = SqlHelper.ExecuteSql("select top 1 * from officedba.ItemCodingRule where CompanyCD=@compcode and CodingType=9 and ItemTypeID=5 and UsedStatus=1", param);
          DataSet ds = new DataSet();
          ds.Tables.Add(dt);
          try
          {
              return Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
          }
          catch
          {
              return 0;//如果返回0值，那么表示该企业没有设置员工编号规则
          }
      }

      /// <summary>
      /// 更新固定资产信息表的资产类别编号
      /// zxb 2009-08-27
      /// </summary>
      /// <param name="companycd"></param>
      /// <param name="employeeNo"></param>
      /// <param name="ID"></param>
      public static void UpdateFixAssetInfo(string companycd, string FixType, string ID)
      {
          SqlParameter[] param = 
            {
                new SqlParameter("@compcode",companycd),
                new SqlParameter("@FixNo",FixType),
                new SqlParameter("@ID",ID)
            };

          string sqlstr = "update officedba.FixAssetInfo set FixNo=@FixNo where ID=@ID and CompanyCD=@compcode";
          SqlHelper.ExecuteSql(sqlstr, param);

      }

      /// <summary>
      /// 更新到fixwithinfo表
      /// zxb 2009-09-07
      /// </summary>
      public static void InsertFixWithInfo()
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("insert into officedba.FixWithInfo(CountMethod,AccuDeprSubjeCD,CompanyCD,FixNo,UseDate,EstimateUse,UsedYear,EstiResiValue)");
          sql.AppendLine("select 0,0,B.CompanyCD,B.FixNo,A.使用日期,A.预计使用年限,A.已使用年限,A.预计净残值率 from officedba.FixAssetInfo_temp A left join officedba.FixAssetInfo B ");
          sql.AppendLine("on A.企业编号=B.CompanyCD and A.资产名称=B.FixName");
          SqlHelper.ExecuteSql(sql.ToString());
      }

  }
}
