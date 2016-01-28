/**********************************************
 * 类作用：   库存日结处理
 * 建立人：  王保军
 * 建立时间： 2009/03/30
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

namespace XBase.Data.Office.StorageManager
{
  public   class DayEndDBHelper
  {
      #region 从分仓存量表中获取改公司的所有产品信息
      /// <summary>
      /// 从分仓存量表中获取改公司的所有产品信息
      /// </summary>
      /// <param name="CompanyCD"></param>
      /// <returns></returns>
      public static DataTable GetCompanyProductList(string CompanyCD)
      {
          StringBuilder strSql = new StringBuilder();
          strSql.Append("select  StorageID,isnull(BatchNo,'') as BatchNo,ProductID  ");
          strSql.Append("from officedba.StorageProduct   ");
          strSql.Append(" where CompanyCD=@CompanyCD    "); 

         ArrayList parmList = new ArrayList();
         parmList.Add(new SqlParameter("@CompanyCD", CompanyCD));

              return SqlHelper.ExecuteSql(strSql.ToString(), parmList);

      }
      #endregion

      #region 获得前一天的某个物品的结存量
      /// <summary>
      /// 获得前一天的某个物品的结存量
      /// </summary>
      /// <param name="ProductID"></param>
      /// <param name="BatchNo"></param>
      /// <param name="StorageID"></param>
      /// <param name="DailyDate"></param>
      /// <param name="CompanyCD"></param>
      /// <returns></returns>
      public static decimal  GetFrontDayCount(string ProductID, string BatchNo, string StorageID, string DailyDate, string CompanyCD)
      {
          decimal TodayCount = 0;
              StringBuilder strSql = new StringBuilder();
              strSql.Append("SELECT isnull(TodayCount,0) as TodayCount  ");
              strSql.Append("FROM officedba.StorageDaily    ");
              if (string.IsNullOrEmpty(BatchNo))
              {
                  strSql.Append("  where CompanyCD=@CompanyCD and DailyDate=@DailyDate AND StorageID=@StorageID   and (BatchNo=@BatchNo or BatchNo is Null) and ProductID=@ProductID   ");
              }
              else
              {
                  strSql.Append(" where CompanyCD=@CompanyCD and DailyDate=@DailyDate AND StorageID=@StorageID   and BatchNo=@BatchNo and ProductID=@ProductID   ");

              }
              SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar),
					new SqlParameter("@DailyDate", SqlDbType.DateTime ),
                    new SqlParameter("@StorageID", SqlDbType.Int ),
                    new SqlParameter("@BatchNo", SqlDbType.VarChar),
                    new SqlParameter("@ProductID", SqlDbType.Int ),
                                          };
              parameters[0].Value = CompanyCD ;
              parameters[1].Value = DailyDate ;
              parameters[2].Value = StorageID;
              parameters[3].Value = BatchNo ;
              parameters[4].Value = ProductID ;
              TodayCount = Convert .ToDecimal (SqlHelper.ExecuteScalar(strSql.ToString(), parameters));
          
                return TodayCount;
      }
      #endregion
      /// <summary>
      /// 判断是否是第一次日结
      /// </summary>
      /// <param name="CompanyCD"></param>
      /// <param name="day"></param>
      /// <returns></returns>
      public static bool isHaveData(string CompanyCD, string day)
      {
          #region SQL文
          StringBuilder strSql = new StringBuilder();
          strSql.AppendLine("select count(*) from   officedba.StorageAccount   ");
          strSql.AppendLine("where CompanyCD=@CompanyCD  and  CONVERT(VARCHAR(10),HappenDate,21) =@day ");

          #endregion

          #region 参数
          SqlParameter[] param = new SqlParameter[2];
          param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          param[1] = SqlHelper.GetParameter("@day", day);
          #endregion

          int count = (int)SqlHelper.ExecuteScalar(strSql.ToString(), param);
          if (count > 0)
          {
              return true;
          }
          else
          {
              return false;
          }
      }

      #region 获得分仓存量表里的数据
      /// <summary>
      /// 获得分仓存量表里的数据
      /// </summary>
      /// <param name="ProductID"></param>
      /// <param name="BatchNo"></param>
      /// <param name="StorageID"></param>
      /// <param name="DailyDate"></param>
      /// <param name="CompanyCD"></param>
      /// <returns></returns>
      public static decimal GetFirstDayCount(string ProductID, string BatchNo, string StorageID, string CompanyCD)
      {
          decimal TodayCount = 0;
          StringBuilder strSql = new StringBuilder();
          strSql.Append("SELECT ProductCount as TodayCount  ");
          strSql.Append("FROM officedba.StorageProduct    ");
          if (string.IsNullOrEmpty(BatchNo))
          {
              strSql.Append(" where CompanyCD=@CompanyCD AND StorageID=@StorageID   and (BatchNo=@BatchNo or BatchNo is Null)   and ProductID=@ProductID    ");
          }
          else
          {
              strSql.Append(" where CompanyCD=@CompanyCD AND StorageID=@StorageID   and BatchNo=@BatchNo and ProductID=@ProductID     ");
          }
    


          SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar), 
                    new SqlParameter("@StorageID", SqlDbType.Int ),
                    new SqlParameter("@BatchNo", SqlDbType.VarChar),
                    new SqlParameter("@ProductID", SqlDbType.Int ),
                                          };
          parameters[0].Value = CompanyCD; 
          parameters[1].Value = StorageID;
          parameters[2].Value = BatchNo;
          parameters[3].Value = ProductID;
          try
          {
              TodayCount = Convert.ToDecimal(SqlHelper.ExecuteScalar(strSql.ToString(), parameters));
          }
          catch
          {
              TodayCount = -123456789;
          }



          return TodayCount;
      }
      #endregion
      #region 获得库存流水账表中取得当天的某个物品的各种单据信息
      /// <summary>
      /// 获得前一天的某个物品的数量
      /// </summary>
      /// <param name="ProductID"></param>
      /// <param name="BatchNo"></param>
      /// <param name="StorageID"></param>
      /// <param name="DailyDate"></param>
      /// <param name="CompanyCD"></param>
      /// <returns></returns>
      public static decimal GetDayItemsCount(string ProductID, string BatchNo, string StorageID, string DailyDate, string CompanyCD,int ItemsType)
      {
          decimal HappenCount = 0;
          StringBuilder strSql = new StringBuilder();
          strSql.Append("SELECT isnull(sum( isnull(HappenCount,0)),0) as HappenCount  ");
          strSql.Append("FROM officedba.StorageAccount   ");
          if (!string.IsNullOrEmpty(BatchNo))
          {
              strSql.Append(" where CompanyCD=@CompanyCD and BillType=@BillType and CONVERT(VARCHAR(10),HappenDate,21) =@DailyDate AND StorageID=@StorageID   and BatchNo=@BatchNo and ProductID=@ProductID      ");
              SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar),
                     new SqlParameter("@BillType", SqlDbType.Int ),
					new SqlParameter("@DailyDate", SqlDbType.DateTime ),
                    new SqlParameter("@StorageID", SqlDbType.Int ),
                    new SqlParameter("@BatchNo", SqlDbType.VarChar),
                    new SqlParameter("@ProductID", SqlDbType.Int ),
                                          };
              parameters[0].Value = CompanyCD;
              parameters[1].Value = ItemsType;
              parameters[2].Value = DailyDate;
              parameters[3].Value = StorageID;
              parameters[4].Value = BatchNo;
              parameters[5].Value = ProductID;
              HappenCount = Convert.ToDecimal(SqlHelper.ExecuteScalar(strSql.ToString(), parameters));
          }
          else
          {

              strSql.Append(" where CompanyCD=@CompanyCD and BillType=@BillType and CONVERT(VARCHAR(10),HappenDate,21) =@DailyDate AND StorageID=@StorageID   and (BatchNo is null or BatchNo ='') and ProductID=@ProductID      ");
              SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar),
                     new SqlParameter("@BillType", SqlDbType.Int ),
					new SqlParameter("@DailyDate", SqlDbType.DateTime ),
                    new SqlParameter("@StorageID", SqlDbType.Int ), 
                    new SqlParameter("@ProductID", SqlDbType.Int ),
                                          };
              parameters[0].Value = CompanyCD;
              parameters[1].Value = ItemsType;
              parameters[2].Value = DailyDate;
              parameters[3].Value = StorageID; 
              parameters[4].Value = ProductID;
              HappenCount = Convert.ToDecimal(SqlHelper.ExecuteScalar(strSql.ToString(), parameters));
          }


  

          return HappenCount;
      }
      #endregion

      /// <summary>
      /// 获得库存流水账表中取得当天的某个物品的各种单据数量信息
      /// </summary>
      /// <param name="ProductID"></param>
      /// <param name="BatchNo"></param>
      /// <param name="StorageID"></param>
      /// <param name="DailyDate"></param>
      /// <param name="CompanyCD"></param>
      /// <param name="ItemsType"></param>
      /// <returns></returns>
       public static decimal GetDayItemsPrice(string ProductID, string BatchNo, string StorageID, string DailyDate, string CompanyCD,int ItemsType)
      {
          decimal TotalPrice = 0;
          StringBuilder strSql = new StringBuilder();
          strSql.Append("SELECT cast( isnull(sum( isnull(HappenCount,0)*isnull(Price,0) ),0)  as Numeric(22,6) ) TotalPrice  ");
          strSql.Append("FROM officedba.StorageAccount   ");
          if (!string.IsNullOrEmpty(BatchNo))
          {
              strSql.Append(" where CompanyCD=@CompanyCD and BillType=@BillType and CONVERT(VARCHAR(10),HappenDate,21) =@DailyDate AND StorageID=@StorageID   and BatchNo=@BatchNo and ProductID=@ProductID      ");
              SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar),
                     new SqlParameter("@BillType", SqlDbType.Int ),
					new SqlParameter("@DailyDate", SqlDbType.DateTime ),
                    new SqlParameter("@StorageID", SqlDbType.Int ),
                    new SqlParameter("@BatchNo", SqlDbType.VarChar),
                    new SqlParameter("@ProductID", SqlDbType.Int ),
                                          };
              parameters[0].Value = CompanyCD;
              parameters[1].Value = ItemsType;
              parameters[2].Value = DailyDate;
              parameters[3].Value = StorageID;
              parameters[4].Value = BatchNo;
              parameters[5].Value = ProductID;
              TotalPrice = Convert.ToDecimal(SqlHelper.ExecuteScalar(strSql.ToString(), parameters));
          }
          else
          {

              strSql.Append(" where CompanyCD=@CompanyCD and BillType=@BillType and CONVERT(VARCHAR(10),HappenDate,21) =@DailyDate AND StorageID=@StorageID   and (BatchNo is null or BatchNo ='') and ProductID=@ProductID      ");
              SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar),
                     new SqlParameter("@BillType", SqlDbType.Int ),
					new SqlParameter("@DailyDate", SqlDbType.DateTime ),
                    new SqlParameter("@StorageID", SqlDbType.Int ), 
                    new SqlParameter("@ProductID", SqlDbType.Int ),
                                          };
              parameters[0].Value = CompanyCD;
              parameters[1].Value = ItemsType;
              parameters[2].Value = DailyDate;
              parameters[3].Value = StorageID; 
              parameters[4].Value = ProductID;
              TotalPrice = Convert.ToDecimal(SqlHelper.ExecuteScalar(strSql.ToString(), parameters));
          }




          return TotalPrice;
      }

      /// <summary>
      /// 从其他出库单据中分离出当天采购退货的信息
      /// </summary>
      /// <param name="ProductID"></param>
      /// <param name="BatchNo"></param>
      /// <param name="StorageID"></param>
      /// <param name="DailyDate"></param>
      /// <param name="CompanyCD"></param>
      /// <returns></returns>
       public static DataTable GetPurchaseRejectInfo(string ProductID, string BatchNo, string StorageID, string DailyDate, string CompanyCD )
       {
           ArrayList parmList = new ArrayList();
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select sum(isnull(a.HappenCount,0)) as Count, ");
           strSql.Append("cast(sum(isnull(a.HappenCount,0)*isnull(a.Price,0)) as Numeric(22,6)) TotalPrice   ");
           strSql.Append(" from officedba.StorageAccount a    ");
           strSql.Append(" left outer join officedba.PurchaseReject b     ");
           strSql.Append(" on a.CompanyCD=b.CompanyCD and a.BillNo=b.RejectNo    ");
           if (!string.IsNullOrEmpty(BatchNo))
           {
               strSql.Append(" where  a. CompanyCD=@CompanyCD and a.BillType=8 and  CONVERT(VARCHAR(10),a.HappenDate,21)=@HappenDate and  a.StorageID=@StorageID and a.BatchNo=@BatchNo and a.ProductID=@ProductID");
               parmList.Add(new SqlParameter("@BatchNo", BatchNo));
           }
           else
           {
               strSql.Append(" where  a. CompanyCD=@CompanyCD and a.BillType=8 and  CONVERT(VARCHAR(10),a.HappenDate,21)=@HappenDate and  a.StorageID=@StorageID and (a.BatchNo is null or a.BatchNo ='')  and a.ProductID=@ProductID");
           }
           parmList.Add(new SqlParameter("@CompanyCD", CompanyCD));
           parmList.Add(new SqlParameter("@HappenDate", DailyDate ));
           parmList.Add(new SqlParameter("@StorageID", StorageID));
           parmList.Add(new SqlParameter("@ProductID", ProductID)); 

           return SqlHelper.ExecuteSql(strSql.ToString(), parmList);

       }


       #region 查询日结列表所需数据
      /// <summary>
       /// 查询日结列表所需数据
      /// </summary>
      /// <param name="pageIndex"></param>
      /// <param name="pageCount"></param>
      /// <param name="orderBy"></param>
      /// <param name="TotalCount"></param>
      /// <param name="day"></param>
      /// <returns></returns>
       public static DataTable SelectDayInfo(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string day)
       {
              UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"]; 

           SqlCommand comm = new SqlCommand();
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select a.ID,CONVERT(VARCHAR(10),a.DailyDate,21)  as DailyDate,a.ProductID,a.StorageID,isnull(c.StorageName,'') as StorageName");
           sql.AppendLine(",isnull(b.ProdNo,'') as ProductNo ");
           sql.AppendLine(" ,isnull(b.ProductName,'') as ProductName ");
           sql.AppendLine("  ,isnull(b.Specification,'') as Specification                ");
           sql.AppendLine(" ,isnull(a.BatchNo,'') as BatchNo                         ");
           sql.AppendLine(" ,   Convert(numeric(22," + userInfo.SelPoint + "),a.InTotal) as InTotal     ");
           sql.AppendLine(" ,   Convert(numeric(22," + userInfo.SelPoint + "),a.OutTotal) as OutTotal                   ");
           sql.AppendLine(" , Convert(numeric(22," + userInfo.SelPoint + "),a.TodayCount) as TodayCount ");
           sql.AppendLine("from officedba.StorageDaily a ");
           sql.AppendLine("left outer join officedba.ProductInfo b on a.CompanyCD=b.CompanyCD and a.ProductID=b.id  ");
           sql.AppendLine("left outer join officedba.StorageInfo c on a.CompanyCD=c.CompanyCD and a.StorageID=c.id  ");
           sql.AppendLine("where a.CompanyCD=@CompanyCD "); 

           if (!string.IsNullOrEmpty(day))
           {
               sql.AppendLine(" AND a.DailyDate =@day");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@day", day));
           } 
        
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));

           comm.CommandText = sql.ToString();
           return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
       }
       #endregion
     /// <summary>
       /// 防止重复日结某一天，出现数据冗余，先删除当天在日结表中的数据
     /// </summary>
     /// <param name="CompanyCD"></param>
     /// <param name="day"></param>
     /// <returns></returns>
       public static bool DeleteDay(string CompanyCD,string day)
       {
           #region SQL文
           StringBuilder strSql = new StringBuilder();
           strSql.AppendLine("delete from  officedba.StorageDaily  ");
           strSql.AppendLine("where CompanyCD=@CompanyCD and DailyDate=@DailyDate ");
       
           #endregion

           #region 参数
           SqlParameter[] param = new SqlParameter[2]; 
           param[0] = SqlHelper.GetParameter("@CompanyCD",  CompanyCD);
           param[1] = SqlHelper.GetParameter("@DailyDate", day);
           #endregion

           SqlHelper.ExecuteTransSql(strSql.ToString(), param);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }


      

                 /// <summary>
       /// 判断是否是第一次日结
     /// </summary>
     /// <param name="CompanyCD"></param>
     /// <param name="day"></param>
     /// <returns></returns>
       public static bool CheckFirstOperate(string CompanyCD)
       {
           #region SQL文
           StringBuilder strSql = new StringBuilder();
           strSql.AppendLine("select count(*) from   officedba.StorageDaily  ");
           strSql.AppendLine("where CompanyCD=@CompanyCD  ");
       
           #endregion

           #region 参数
           SqlParameter[] param = new SqlParameter[1]; 
           param[0] = SqlHelper.GetParameter("@CompanyCD",  CompanyCD); 
           #endregion

        int count= (int)  SqlHelper.ExecuteScalar (strSql.ToString(), param);
        if (count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
       }


        /// <summary>
       /// 防止重复日结某一天，出现数据冗余，先删除当天在日结表中的数据
     /// </summary>
     /// <param name="CompanyCD"></param>
     /// <param name="day"></param>
     /// <returns></returns>
       public static bool CheckDay(string CompanyCD, string day)
       {
           #region SQL文
           StringBuilder strSql = new StringBuilder();
           strSql.AppendLine("select count(*) from   officedba.StorageDaily  ");
           strSql.AppendLine("where CompanyCD=@CompanyCD  and   CONVERT(varchar(100), DailyDate, 23)=@DailyDate");
       
           #endregion

           #region 参数
           SqlParameter[] param = new SqlParameter[2]; 
           param[0] = SqlHelper.GetParameter("@CompanyCD",  CompanyCD);
           param[1] = SqlHelper.GetParameter("@DailyDate", day);
           #endregion

        int count= (int)  SqlHelper.ExecuteScalar (strSql.ToString(), param);
        if (count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
       }


       /// <summary>
       /// 返回第一次日结的日期和最后一次做日结的日期
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <param name="day"></param>
       /// <returns></returns>
       public static DataTable   GetOperateDateInfo(string CompanyCD)
       {
           #region SQL文
           StringBuilder strSql = new StringBuilder();
           strSql.AppendLine("select min(CONVERT(varchar(100), DailyDate, 23)) as FirstDailyDate,max(CONVERT(varchar(100), DailyDate, 23)) as LastDailyDate  from officedba.StorageDaily ");
           strSql.AppendLine("where CompanyCD=@CompanyCD ");

           #endregion

           #region 参数
           SqlParameter[] param = new SqlParameter[1];
           param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD); 
           #endregion

         return   SqlHelper.ExecuteSql (strSql.ToString(), param);
           
       }

      

       public static DataTable GetStorageDailyInfo(string ProductID, string BatchNo, string StorageID, string DailyDate, string CompanyCD)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            

           ArrayList parmList = new ArrayList();
           StringBuilder strSql = new StringBuilder();
           strSql.Append("SELECT  ");
           strSql.Append("  CONVERT(VARCHAR(10),a.DailyDate,21) as DailyDate ,a.StorageID, isnull(c.StorageName,'') as StorageName ");
           strSql.Append(", a.ProductID   ");
           strSql.Append(", isnull(a.BatchNo,'') as  BatchNo ");
           strSql.Append(",      Convert(numeric(22," + userInfo.SelPoint + "),a.InitInCount) as InitInCount   ");
           strSql.Append(",            Convert(numeric(22," + userInfo.SelPoint + "),a.InitBatchCount) as InitBatchCount     ");
           strSql.Append(",    Convert(numeric(22," + userInfo.SelPoint + "),a.PhurInCount) as PhurInCount   ");
           strSql.Append(",       Convert(numeric(22," + userInfo.SelPoint + "),a.MakeInCount) as MakeInCount   ");
           strSql.Append(",        Convert(numeric(22," + userInfo.SelPoint + "),a.SaleBackInCount) as SaleBackInCount    ");
           strSql.Append(", Convert(numeric(22," + userInfo.SelPoint + "),a.SubSaleBackInCount) as SubSaleBackInCount   ");
           strSql.Append(",       Convert(numeric(22," + userInfo.SelPoint + "),a.RedInCount) as RedInCount   ");
           strSql.Append(",    Convert(numeric(22," + userInfo.SelPoint + "),a.OtherInCount) as OtherInCount    ");
           strSql.Append(",  Convert(numeric(22," + userInfo.SelPoint + "),a.BackInCount) as BackInCount   ");
           strSql.Append(",     Convert(numeric(22," + userInfo.SelPoint + "),a.TakeInCount) as TakeInCount     ");
           strSql.Append(",   Convert(numeric(22," + userInfo.SelPoint + "),a.DispInCount) as DispInCount    ");
           strSql.Append(",     Convert(numeric(22," + userInfo.SelPoint + "),a.SendInCount) as SendInCount   ");
           strSql.Append(",  Convert(numeric(22," + userInfo.SelPoint + "),a.SaleOutCount) as SaleOutCount     ");
           strSql.Append(",     Convert(numeric(22," + userInfo.SelPoint + "),a.SubSaleOutCount) as SubSaleOutCount   ");
           strSql.Append(",   Convert(numeric(22," + userInfo.SelPoint + "),a.PhurBackOutCount) as PhurBackOutCount   ");
           strSql.Append(",      Convert(numeric(22," + userInfo.SelPoint + "),a.RedOutCount) as RedOutCount    ");
           strSql.Append(", Convert(numeric(22," + userInfo.SelPoint + "),a.OtherOutCount) as OtherOutCount     ");
           strSql.Append(",  Convert(numeric(22," + userInfo.SelPoint + "),a.DispOutCount) as DispOutCount     ");
           strSql.Append(",    Convert(numeric(22," + userInfo.SelPoint + "),a.LendCount) as LendCount     ");
           strSql.Append(",     Convert(numeric(22," + userInfo.SelPoint + "),a.AdjustCount) as AdjustCount   ");
           strSql.Append(",    Convert(numeric(22," + userInfo.SelPoint + "),a.BadCount) as BadCount   ");
           strSql.Append(",      Convert(numeric(22," + userInfo.SelPoint + "),a.CheckCount) as CheckCount   ");
           strSql.Append(", Convert(numeric(22," + userInfo.SelPoint + "),a.TakeOutCount) as TakeOutCount    ");
           strSql.Append(",  Convert(numeric(22," + userInfo.SelPoint + "),a.SendOutCount) as SendOutCount    ");
           strSql.Append(",  Convert(numeric(22," + userInfo.SelPoint + "),a.InTotal) as InTotal     ");
           strSql.Append(", Convert(numeric(22," + userInfo.SelPoint + "),a.OutTotal) as OutTotal    "); 
           strSql.Append(",Convert(numeric(22," + userInfo.SelPoint + "),a.TodayCount) as TodayCount    ");
           strSql.Append(", Convert(numeric(22," + userInfo.SelPoint + "),a.SaleFee) as SaleFee       ");
           strSql.Append(", Convert(numeric(22," + userInfo.SelPoint + "),a.SaleBackFee) as SaleBackFee    ");
           strSql.Append(",  Convert(numeric(22," + userInfo.SelPoint + "),a.PhurFee) as PhurFee        ");
           strSql.Append(", Convert(numeric(22," + userInfo.SelPoint + "),a.PhurBackFee) as PhurBackFee   ");
           strSql.Append(", a.CreateDate   ");
           strSql.Append(", isnull(b.EmployeeName,'') as CreatorName  ");
           strSql.Append(", isnull(d.ProductName,'') as ProductName   ");
           strSql.Append(", isnull(d.ProdNo,'') as ProductNo   ");
           strSql.Append("FROM  officedba . StorageDaily a   ");
           strSql.Append("left outer join officedba.EmployeeInfo b   ");
           strSql.Append("on a.CompanyCD=b.CompanyCD and a.Creator=b.id   ");
           strSql.Append("left outer join officedba.StorageInfo c  ");
           strSql.Append("on a.CompanyCD=c.CompanyCD and a.StorageID=c.id   ");
           strSql.Append("left outer join officedba.ProductInfo d  ");
           strSql.Append("on a.CompanyCD=d.CompanyCD  and a.ProductID=d.id   ");



           if (!string.IsNullOrEmpty(BatchNo.Trim ()))
           {
               strSql.Append(" where a.CompanyCD=@CompanyCD and a.StorageID=@StorageID    and CONVERT(VARCHAR(10),a.DailyDate,21) =@DailyDate   and  a.BatchNo=@BatchNo and a.ProductID=@ProductID");
               parmList.Add(new SqlParameter("@BatchNo", BatchNo));
           }
           else
           {
               strSql.Append(" where a.CompanyCD=@CompanyCD and a.StorageID=@StorageID    and CONVERT(VARCHAR(10),a.DailyDate,21) =@DailyDate   and  (a.BatchNo is null or a.BatchNo ='')   and  a.ProductID=@ProductID  ");
           }
           parmList.Add(new SqlParameter("@CompanyCD", CompanyCD));
           parmList.Add(new SqlParameter("@StorageID", StorageID ));
           parmList.Add(new SqlParameter("@ProductID", ProductID));
           parmList.Add(new SqlParameter("@DailyDate", DailyDate));
           return SqlHelper.ExecuteSql(strSql.ToString(), parmList);

       }
  }
}
