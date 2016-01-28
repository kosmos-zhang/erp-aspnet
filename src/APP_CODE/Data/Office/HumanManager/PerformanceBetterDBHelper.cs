/**********************************************
 * 类作用：  新建绩效改进计划
 * 建立人：   王保军
 * 建立时间： 2009/05/17
 ***********************************************/
using System;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using XBase.Common;
using System.Collections;
using System.Collections.Generic;

namespace XBase.Data.Office.HumanManager
{
  public    class PerformanceBetterDBHelper
  {
      public static DataSet GetRectPlanInfoWithID(string companyCD, string planNo)
      {
          //定义返回的数据变量
          DataSet dsRectPlanInfo = new DataSet();

          #region 查询招聘活动信息
          StringBuilder planSql = new StringBuilder();
          planSql.AppendLine(" SELECT A.ID               ");
          planSql.AppendLine("       ,A.CompanyCD        ");
          planSql.AppendLine("       ,A.PlanNo           ");
          planSql.AppendLine("       , isnull(A.Title,'') as Title ,isnull(A.Remark ,'') as Remark          ");
          planSql.AppendLine("       ,CONVERT(VARCHAR(10),A.CreateDate,21) AS CreateDate ");
          planSql.AppendLine("       ,isnull(B.EmployeeName,'') AS Creator,c.EmployeeID,c.Checker,  isnull(d.EmployeeName,'') AS EmployeeName,isnull(c.Content,'') as Content,isnull(c.CompleteAim,'') as CompleteAim,CONVERT(VARCHAR(10),c.CompleteDate,21) AS CompleteDate,isnull(f.EmployeeName,'') AS CheckerName,CONVERT(VARCHAR(10),c.CheckDate,21) AS CheckDate,isnull(c.CheckResult,'') as CheckResult,isnull(c.Remark,'') as employeeRemark");
          planSql.AppendLine(" FROM officedba.PerformanceBetter A ");
          planSql.AppendLine(" left join officedba.EmployeeInfo B  ");
          planSql.AppendLine(" on B.CompanyCD=A.CompanyCD AND  A.Creator = B.ID left outer join officedba.PerformanceBetterDetail c on  a.CompanyCD=c.CompanyCD and A.PlanNo = c.PlanNo");
          planSql.AppendLine("  left outer join officedba.EmployeeInfo d on d.CompanyCD=A.CompanyCD AND  c.EmployeeID = d.ID");
          planSql.AppendLine("  left outer join officedba.EmployeeInfo f on F.CompanyCD=A.CompanyCD AND   c.Checker = f.ID");
          planSql.AppendLine(" WHERE A.CompanyCD=@CompanyCD and A.PlanNo = @PlanNo   ");
          //设置参数
          SqlParameter[] param = new SqlParameter[2];
          //招聘活动ID
          param[0] = SqlHelper.GetParameter("@PlanNo", planNo);
          param[1] = SqlHelper.GetParameter("@CompanyCD", companyCD);
          //执行查询
          DataTable dtBaseInfo = new DataTable("BaseInfo");
          dtBaseInfo = SqlHelper.ExecuteSql(planSql.ToString(), param);
          //设置招聘活动基本信息
          dsRectPlanInfo.Tables.Add(dtBaseInfo);
      

          #endregion

          return dsRectPlanInfo;
      }
      public static DataTable SearchBetterInfo(PerformanceBetterModel  model)
      {
          
          #region 查询语句
          //查询SQL拼写
          StringBuilder searchSql = new StringBuilder();
          searchSql.AppendLine(" SELECT   distinct( a.PlanNo),a.ID ,isnull(a.Title,'') as Title,CONVERT(VARCHAR(10),a.CreateDate,21) AS CreateDate ,isnull(b.EmployeeName,'') as Creator ,isnull(Convert(varchar(100),a.ModifiedDate,23),'') AS ModifiedDate                      ");
          searchSql.AppendLine(" FROM    officedba.PerformanceBetter  a left outer join  officedba.EmployeeInfo b on b.CompanyCD=a.CompanyCD and  a.Creator=b.ID  left outer join officedba.PerformanceBetterDetail c on  a.CompanyCD=c.CompanyCD and a.PlanNo=c.PlanNo");
          searchSql.AppendLine(" WHERE	a.CompanyCD = @CompanyCD            ");
          #endregion

          //定义查询的命令
          SqlCommand comm = new SqlCommand();
          //添加公司代码参数
          comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

          //l
         

          if (!string.IsNullOrEmpty(model.EmployeeId))
          {

              searchSql.AppendLine(" AND c.EmployeeID = @EmployeeID ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeId ));
          }


          if (!string.IsNullOrEmpty(model.CreateDate) && !string.IsNullOrEmpty(model.EndDate))
              {
                  searchSql.AppendLine(" AND a.CreateDate between @CreateDate and @EndDate ");
                  comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", model.CreateDate));
                  comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));
              }
          if (!string.IsNullOrEmpty(model.EndDate) && string.IsNullOrEmpty(model.CreateDate))
              {
                  searchSql.AppendLine(" AND a.CreateDate <@EndDate ");
                  comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));
              }
          if (string.IsNullOrEmpty(model.EndDate) && !string.IsNullOrEmpty(model.CreateDate))
          {
              searchSql.AppendLine(" AND a.CreateDate between @CreateDate and @EndDate ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", model.CreateDate));
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", DateTime .Now .ToString ()));
          }

          if (!string.IsNullOrEmpty(model.PlanNo))
          {
              searchSql.AppendLine(" AND a.PlanNo like @PlanNo ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", "%" + model.PlanNo + "%"));
          }

          if (!string.IsNullOrEmpty(model.Title))
          {
              searchSql.AppendLine(" AND a.Title LIKE @Title ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + model.Title + "%"));
          }
          //启用状态

          //指定命令的SQL文
          comm.CommandText = searchSql.ToString();
          //执行查询
          return SqlHelper.ExecuteSearch(comm);
      }
      public static bool UpdateBetterInfobyPlanNo(PerformanceBetterModel model)
      {
          bool isSucc = false;

          #region 插入SQL拼写
          StringBuilder insertSql = new StringBuilder();
          insertSql.AppendLine("update officedba.PerformanceBetter ");
          insertSql.AppendLine("           set Title=@Title ");
          insertSql.AppendLine("            ,Remark=@Remark ");
          insertSql.AppendLine("            , ModifiedUserID=@ModifiedUserID ");
          insertSql.AppendLine("            , ModifiedDate=getdate() ");
          insertSql.AppendLine("where CompanyCD=@CompanyCD and PlanNo=@PlanNo  ");
          #endregion
          //定义插入基本信息的命令
          SqlCommand comm = new SqlCommand();
          comm.CommandText = insertSql.ToString();
          //设置保存的参数
          comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
          comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", model.PlanNo ));	//类型名称
          comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark ));	//创建人
          comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title ));	//启用状态
          comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));
          //执行插入操作
          isSucc = SqlHelper.ExecuteTransWithCommand(comm);
          return isSucc;
      }
      public static bool DeleteBetterDetatilInfobyPlanNo(string planNo,string companyCD )
      {
          bool isSucc = false;

          #region 插入SQL拼写
          StringBuilder insertSql = new StringBuilder();
          insertSql.AppendLine("delete from  officedba.PerformanceBetterDetail ");
          insertSql.AppendLine("where CompanyCD=@CompanyCD and PlanNo=@PlanNo  ");
          #endregion
          //定义插入基本信息的命令
          SqlCommand comm = new SqlCommand();
          comm.CommandText = insertSql.ToString();
          //设置保存的参数
          comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));	//公司代码
          comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", planNo));	//类型名称
          //执行插入操作
          isSucc = SqlHelper.ExecuteTransWithCommand(comm);
          return isSucc;
      }
      public static bool InsertBetterDetaiInfo(IList<PerformanceBetterDetailModel> modeList)
      {
          bool isSucc = false;
          foreach (PerformanceBetterDetailModel model in modeList)
          {
              #region 插入SQL拼写
              StringBuilder insertSql = new StringBuilder();
              insertSql.AppendLine("INSERT INTO officedba.PerformanceBetterDetail ");
              insertSql.AppendLine("           (CompanyCD             ");
              insertSql.AppendLine("           ,PlanNo                ");
              insertSql.AppendLine("           ,EmployeeID              ");
              insertSql.AppendLine("           ,Content                 ");
              insertSql.AppendLine("           ,CompleteAim           ");
              insertSql.AppendLine("           ,CompleteDate               ");
              insertSql.AppendLine("           ,Checker               ");
              insertSql.AppendLine("           ,CheckDate               ");
              insertSql.AppendLine("           ,CheckResult               ");
              insertSql.AppendLine("           ,Remark )              ");

              insertSql.AppendLine("     VALUES                        ");
              insertSql.AppendLine("           (@CompanyCD            ");
              insertSql.AppendLine("           ,@PlanNo               ");
              insertSql.AppendLine("           ,@EmployeeID             ");
              insertSql.AppendLine("           ,@Content               ");
              insertSql.AppendLine("           ,@CompleteAim          ");
              insertSql.AppendLine("           ,@CompleteDate             ");
              insertSql.AppendLine("           ,@Checker               ");
              insertSql.AppendLine("           ,@CheckDate               ");
              insertSql.AppendLine("           ,@CheckResult            ");
              insertSql.AppendLine("           ,@Remark)               ");
              #endregion
              //定义插入基本信息的命令
              SqlCommand comm = new SqlCommand();
              comm.CommandText = insertSql.ToString();
              //设置保存的参数
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", model.PlanNo ));	//类型名称
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID ));	//创建人
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@Content", model.Content ));	//启用状态
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompleteAim", model.CompleteAim ));	//启用状态
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompleteDate", model.CompleteDate ));
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@Checker", model.Checker ));	//创建人
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckDate", model.CheckDate ));	//启用状态
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckResult", model.CheckResult ));	//启用状态
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark ));
              //添加返回参数
              //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

              //执行插入操作
              isSucc = SqlHelper.ExecuteTransWithCommand(comm);
              if (!isSucc)
              {
                  isSucc = false;
                  break;
              }
              else
              {
                  continue;
              }
          }
          return isSucc;

      }
      public static bool InsertBetterInfo(PerformanceBetterModel model)
      {
          bool isSucc = false;
              #region 插入SQL拼写
              StringBuilder insertSql = new StringBuilder();
              insertSql.AppendLine("INSERT INTO officedba.PerformanceBetter ");
              insertSql.AppendLine("           (CompanyCD             ");
              insertSql.AppendLine("           ,PlanNo                ");
              insertSql.AppendLine("           ,Title              ");
              insertSql.AppendLine("           ,Remark                 ");
              insertSql.AppendLine("           ,Creator           ");
              insertSql.AppendLine("           ,CreateDate)               ");

              insertSql.AppendLine("     VALUES                        ");
              insertSql.AppendLine("           (@CompanyCD            ");
              insertSql.AppendLine("           ,@PlanNo               ");
              insertSql.AppendLine("           ,@Title             ");
              insertSql.AppendLine("           ,@Remark               ");
              insertSql.AppendLine("           ,@Creator          ");
              insertSql.AppendLine("           ,getdate() )           ");
              #endregion
              //定义插入基本信息的命令
              SqlCommand comm = new SqlCommand();
              comm.CommandText = insertSql.ToString();
              //设置保存的参数
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", model.PlanNo));	//类型名称
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title ));	//创建人
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark ));	//启用状态
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", model.Creator ));	//启用状态
              //添加返回参数
              //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

              //执行插入操作
              isSucc = SqlHelper.ExecuteTransWithCommand(comm);
              return isSucc;

      }
      public static bool DeleteBetterInfo(IList<PerformanceBetterModel > modeList)
      {
          bool isSucc = false;
          foreach (PerformanceBetterModel model in modeList)
          {
              #region 插入SQL拼写
              StringBuilder insertSql = new StringBuilder();
              insertSql.AppendLine("DELETE FROM  officedba.PerformanceBetter ");
              insertSql.AppendLine("where CompanyCD=@CompanyCD and PlanNo=@PlanNo ");
              #endregion
              //定义插入基本信息的命令
              SqlCommand comm = new SqlCommand();
              comm.CommandText = insertSql.ToString();
              //设置保存的参数
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", model.PlanNo));	//类型名称
              //执行插入操作
              isSucc = SqlHelper.ExecuteTransWithCommand(comm);
              if (isSucc)
              {
                  if (DeleteBetterDetatilInfobyPlanNo(model.PlanNo, model.CompanyCD))
                  {
                      continue;
                  }
                  else
                  {
                      isSucc = false;
                      break;
                  }
              }
              else
              {
                  isSucc = false;
                  break;
              }

          }
          return isSucc;

      }
    }
}
