/**********************************************
 * 类作用：   固定资产折旧明细数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/05/07
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
  public class FixAssetDeprDetailDBHelper
  {
      #region 添加固定资产计提明细
      /// <summary>
      /// 添加固定资产计提明细
      /// </summary>
      /// <param name="model">实体MODEL</param>
      /// <returns>true 成功,false 失败</returns>
      public static bool InsertFixAssetDeprDetailInfo( FixAssetDeprDetailModel model)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Insert into officedba.FixAssetDeprDetail");
          sql.AppendLine("(CompanyCD,FixNo,FixName,FixType,");
          sql.AppendLine("UsedDate,DeprDate,Number,UsedYears,");
          sql.AppendLine("OriginalValue,MDeprPrice,TotalDeprPrice,");
          sql.AppendLine("EndNetValue,TotalImpairment,Creator)");
          sql.AppendLine("values(@CompanyCD,@FixNo,@FixName,");
          sql.AppendLine("@FixType,@UsedDate,getdate(),@Number,@UsedYears,");
          sql.AppendLine("@OriginalValue,@MDeprPrice,@TotalDeprPrice,");
          sql.AppendLine("@EndNetValue,@TotalImpairment,@Creator)");

          SqlParameter[] parms = 
          {
              new SqlParameter("@CompanyCD",model.CompanyCD),
              new SqlParameter("@FixNo",model.FixNo),
              new SqlParameter("@FixName",model.FixName),
              new SqlParameter("@FixType",model.FixType),
              new SqlParameter("@UsedDate",model.UsedDate),
              new SqlParameter("@Number",model.Number),
              new SqlParameter("@UsedYears",model.UsedYears),
              new SqlParameter("@OriginalValue",model.OriginalValue),
              new SqlParameter("@MDeprPrice",model.MDeprPrice),
              new SqlParameter("@TotalDeprPrice",model.TotalDeprPrice),
              new SqlParameter("@EndNetValue",model.EndNetValue),
              new SqlParameter("@TotalImpairment",model.TotalImpairment),
              new SqlParameter("@Creator",model.Creator),
          };

          SqlHelper.ExecuteTransSql(sql.ToString(), parms);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }
      #endregion
      /// <summary>
      /// 获取固定资产折旧明细信息
      /// </summary>
      /// <param name="ID">主键</param>
      /// <param name="FixName">资产名称</param>
      /// <param name="FixType">资产类别</param>
      /// <param name="StartDate">开始日期</param>
      /// <param name="EndDate">结束日期</param>
      /// <returns></returns>
      public static DataTable GetFixAssetDeprDetailInfo(string ID, string FixName, string FixType, string StartDate, string EndDate)
      {
          try
          {
              StringBuilder sql = new StringBuilder();
              sql.AppendLine("select cast(a.ID as varchar(50)) as ID,a.CompanyCD,a.FixNo,a.FixName,case when b.TypeName is null then '' when b.TypeName is not null then b.TypeName end as FixType,");
              sql.AppendLine("CONVERT(VARCHAR(10),a.UsedDate,21) as UsedDate,");
              sql.AppendLine("CONVERT(VARCHAR(10),a.DeprDate,21) as DeprDate,");
              sql.AppendLine(" cast(a.Number as varchar(50)) as Number,cast(a.UsedYears as varchar(50)) as UsedYears ,a.OriginalValue,a.MDeprPrice,a.TotalDeprPrice,");
              sql.AppendLine("a.EndNetValue,a.TotalImpairment,isnull(cast(a.EstimateUse as varchar(50)),0) as EstimateUse ");
              sql.AppendLine(" from officedba.FixAssetDeprDetail a ");
              sql.AppendLine(" left outer join officedba.AssetTypeSetting b ");
              sql.AppendLine(" on a.FixType=b.ID  ");
              sql.AppendLine(" where a.CompanyCD=@CompanyCD ");

              string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
              //定义查询的命令
              SqlCommand comm = new SqlCommand();
              //添加公司代码参数
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));

              //主键
              if (!string.IsNullOrEmpty(ID))
              {
                  sql.AppendLine(" AND a.ID=@ID ");
                  comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
              }

              //资产名称
              if (!string.IsNullOrEmpty(FixName))
              {
                  sql.AppendLine(" AND FixName LIKE @FixName ");
                  comm.Parameters.Add(SqlHelper.GetParameterFromString("@FixName", "%" + FixName + "%"));
              }

              //资产类别
              if (!string.IsNullOrEmpty(FixType) && FixType != "0")
              {
                  sql.AppendLine(" AND a.FixType=@FixType ");
                  comm.Parameters.Add(SqlHelper.GetParameterFromString("@FixType", FixType));
              }

              //开始日期
              if (!string.IsNullOrEmpty(StartDate))
              {
                  sql.AppendLine(" AND a.DeprDate>=@StartDate ");
                  comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate + " 00:00:00"));
              }

              //结束日期
              if (!string.IsNullOrEmpty(EndDate))
              {
                  sql.AppendLine(" AND a.DeprDate<=@EndDate ");
                  comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate + " 23:59:59"));
              }

              //指定命令的SQL文
              comm.CommandText = sql.ToString();
              //执行查询
              DataTable dt= SqlHelper.ExecuteSearch(comm);


              DataTable totaldt =GetFixAssetDeprTotal(GetAssetDeprTotalIDS(FixName, FixType, StartDate, EndDate));

              decimal OriginalValue = 0;
              decimal MDeprPrice = 0;
              decimal TotalDeprPrice = 0;
              decimal EndNetValue = 0;
              decimal TotalImpairment = 0;

              foreach (DataRow dr in totaldt.Rows)
              {
                  if (dr["ID"].ToString().Trim().Length > 0)
                  {
                      OriginalValue += Convert.ToDecimal(dr["OriginalValue"].ToString());
                      MDeprPrice += Convert.ToDecimal(dr["MDeprPrice"].ToString());
                      TotalDeprPrice += Convert.ToDecimal(dr["TotalDeprPrice"].ToString());
                      EndNetValue += Convert.ToDecimal(dr["EndNetValue"].ToString());
                      TotalImpairment += Convert.ToDecimal(dr["TotalImpairment"].ToString());
                  }
              }

              if (dt.Rows.Count > 0)
              {
                  DataRow row = dt.NewRow();
                  row["ID"] = "";
                  row["CompanyCD"] = companyCD;
                  row["FixNo"] = "合计";
                  row["FixName"] = "";
                  row["FixType"] = "";
                  row["UsedDate"] = "";
                  row["DeprDate"] = "";
                  row["Number"] = "";
                  row["UsedYears"] = "";
                  row["OriginalValue"] = OriginalValue;
                  row["MDeprPrice"] = MDeprPrice;
                  row["TotalDeprPrice"] = TotalDeprPrice;
                  row["EndNetValue"] = EndNetValue;
                  row["TotalImpairment"] = TotalImpairment;
                  row["EstimateUse"] = "";

                  dt.Rows.Add(row);
              }
              return dt;

          }
          catch (Exception ex)
          {
              throw ex;
          }

      }
      /// <summary>
      /// 获取折旧汇总记录
      /// </summary>
      /// <param name="FixName"></param>
      /// <param name="FixType"></param>
      /// <param name="StartDate"></param>
      /// <param name="EndDate"></param>
      /// <returns></returns>
      public static string GetAssetDeprTotalIDS(string FixName, string FixType, string StartDate, string EndDate)
      {
          try
          {
              StringBuilder sql = new StringBuilder();
              sql.AppendLine("select max(ID) as ID from officedba.FixAssetDeprDetail where CompanyCD=@CompanyCD ");
              string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
              //定义查询的命令
              SqlCommand comm = new SqlCommand();
              //添加公司代码参数
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));

              //资产名称
              if (!string.IsNullOrEmpty(FixName))
              {
                  sql.AppendLine(" AND FixName LIKE @FixName ");
                  comm.Parameters.Add(SqlHelper.GetParameterFromString("@FixName", "%" + FixName + "%"));
              }

              //资产类别
              if (!string.IsNullOrEmpty(FixType) && FixType != "0")
              {
                  sql.AppendLine(" AND FixType=@FixType ");
                  comm.Parameters.Add(SqlHelper.GetParameterFromString("@FixType", FixType));
              }

              //开始日期
              if (!string.IsNullOrEmpty(StartDate))
              {
                  sql.AppendLine(" AND DeprDate>=@StartDate ");
                  comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate + " 00:00:00"));
              }

              //结束日期
              if (!string.IsNullOrEmpty(EndDate))
              {
                  sql.AppendLine(" AND DeprDate<=@EndDate ");
                  comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate + " 23:59:59"));
              }

              sql.AppendLine(" group by FixName ");

              //指定命令的SQL文
              comm.CommandText = sql.ToString();
              //执行查询
              DataTable dt=SqlHelper.ExecuteSearch(comm);
              string nev = string.Empty;
              for (int i = 0; i < dt.Rows.Count; i++)
              {
                  nev += dt.Rows[i]["ID"].ToString() + ",";
              }

              return nev.TrimEnd(new char[] {','});

              


          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      /// <summary>
      /// 获取固定资产折旧汇总信息
      /// </summary>
      /// <param name="ids"></param>
      /// <returns></returns>
      public static DataTable GetFixAssetDeprTotal(string ids)
      {
          try
          {
              string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
              DataTable DT = new DataTable();
              if (ids.Trim().Length > 0)
              {
                  StringBuilder sql = new StringBuilder();
                  sql.AppendLine("select cast(a.ID as varchar(50)) as ID,a.CompanyCD,a.FixNo,a.FixName,case when b.TypeName is null then '' when b.TypeName is not null then b.TypeName end as FixType ,");
                  sql.AppendLine("CONVERT(VARCHAR(10),a.UsedDate,21) as UsedDate,");
                  sql.AppendLine("CONVERT(VARCHAR(10),a.DeprDate,21) as DeprDate,");
                  sql.AppendLine(" cast(a.Number as varchar(50)) as Number,cast(a.UsedYears as varchar(50)) as UsedYears ,a.OriginalValue,a.MDeprPrice,a.TotalDeprPrice,");
                  sql.AppendLine("a.EndNetValue,a.TotalImpairment, isnull(cast(a.EstimateUse as varchar(50)),0) as EstimateUse");
                  sql.AppendLine(" from officedba.FixAssetDeprDetail a ");
                  sql.AppendLine(" left outer join officedba.AssetTypeSetting b ");
                  sql.AppendLine(" on a.FixType=b.ID {0} ");
                  string queryStr = string.Empty;
                  if (ids.Trim().Length > 0)
                  {
                      queryStr = " a.ID in ( " + ids + " ) ";
                  }

                  string selectsql = string.Format(sql.ToString(), queryStr.Trim().Length > 0 ? " where " + queryStr : "");

                  DT=SqlHelper.ExecuteSql(selectsql);

                  decimal OriginalValue = 0;
                  decimal MDeprPrice = 0;
                  decimal TotalDeprPrice = 0;
                  decimal EndNetValue = 0;
                  decimal TotalImpairment = 0;

                  foreach (DataRow dr in DT.Rows)
                  {
                      OriginalValue += Convert.ToDecimal(dr["OriginalValue"].ToString());
                      MDeprPrice += Convert.ToDecimal(dr["MDeprPrice"].ToString());
                      TotalDeprPrice += Convert.ToDecimal(dr["TotalDeprPrice"].ToString());
                      EndNetValue += Convert.ToDecimal(dr["EndNetValue"].ToString());
                      TotalImpairment += Convert.ToDecimal(dr["TotalImpairment"].ToString());
                  }

                  DataRow row = DT.NewRow();
                  row["ID"] = "";
                  row["CompanyCD"] = companyCD;
                  row["FixNo"] = "合计";
                  row["FixName"] = "";
                  row["FixType"] = "";
                  row["UsedDate"] = "";
                  row["DeprDate"] = "";
                  row["Number"] = "";
                  row["UsedYears"] = "";
                  row["OriginalValue"] = OriginalValue;
                  row["MDeprPrice"] = MDeprPrice;
                  row["TotalDeprPrice"] = TotalDeprPrice;
                  row["EndNetValue"] = EndNetValue;
                  row["TotalImpairment"] = TotalImpairment;
                  row["EstimateUse"] = "";
                  DT.Rows.Add(row);
              }

             

              return DT;
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

  }
}
