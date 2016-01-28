using System;
using System.Linq;
using System.Text;
using XBase.Model.Office.SystemManager;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
namespace XBase.Data.Office.SystemManager
{
  public  class ApprovalFlowSetDBHelper
    {



      public static bool InsertFlowInfo(FlowModel model, string StepNo, string StepName, string StepActor)
      {
          ArrayList listADD = new ArrayList();
          bool result = false;
          try
          {
              #region  增加SQL语句
              StringBuilder sqlflow = new StringBuilder();
              sqlflow.AppendLine("INSERT INTO officedba.Flow");
              sqlflow.AppendLine("		(CompanyCD      ");
              sqlflow.AppendLine("		,DeptID         ");
              sqlflow.AppendLine("		,FlowNo         ");
              sqlflow.AppendLine("		,FlowName         ");
              sqlflow.AppendLine("		,BillTypeFlag         ");
              sqlflow.AppendLine("		,BillTypeCode         ");
              sqlflow.AppendLine("		,UsedStatus         ");
              sqlflow.AppendLine("		,IsMobileNotice     ");
              sqlflow.AppendLine("		,ModifiedDate         ");
              sqlflow.AppendLine("		,ModifiedUserID)        ");
              sqlflow.AppendLine("VALUES                  ");
              sqlflow.AppendLine("		(@CompanyCD,     ");
              sqlflow.AppendLine("		@DeptID ,     ");
              sqlflow.AppendLine("		@FlowNo ,     ");
              sqlflow.AppendLine("		@FlowName ,     ");
              sqlflow.AppendLine("		@BillTypeFlag,     ");
              sqlflow.AppendLine("		@BillTypeCode,     ");
              sqlflow.AppendLine("		@UsedStatus,     ");
              sqlflow.AppendLine("		@IsMobileNotice     ");
              sqlflow.AppendLine("		,@ModifiedDate     ");
              sqlflow.AppendLine("		,@ModifiedUserID)       ");
              SqlCommand comm = new SqlCommand();
              comm.CommandText = sqlflow.ToString();
              comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
              comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
              comm.Parameters.Add(SqlHelper.GetParameter("@FlowNo", model.FlowNo));
              comm.Parameters.Add(SqlHelper.GetParameter("@FlowName", model.FlowName));
              comm.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", model.BillTypeFlag));
              comm.Parameters.Add(SqlHelper.GetParameter("@BillTypeCode", model.BillTypeCode));
              comm.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", model.UsedStatus));
              comm.Parameters.Add(SqlHelper.GetParameter("@IsMobileNotice", model.IsMobileNotice));
              comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
              comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
              listADD.Add(comm);
              #endregion

              #region 流程步骤添加SQL语句
              if (!String.IsNullOrEmpty(StepNo) && !String.IsNullOrEmpty(StepName) && !String.IsNullOrEmpty(StepActor))
              {
                  string[] dStepNo = StepNo.Split(',');
                  string[] dStepName = StepName.Split(',');
                  string[] dStepActor = StepActor.Split(',');
                  //页面上这些字段都是必填，数组的长度必须是相同的
                  if (dStepNo.Length >= 1)
                  {
                      for (int i = 0; i < dStepNo.Length; i++)
                      {
                          System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                          cmdsql.AppendLine("INSERT INTO officedba.FlowStepActor");
                          cmdsql.AppendLine("           (CompanyCD");
                          cmdsql.AppendLine("           ,FlowNo");
                          cmdsql.AppendLine("           ,StepNo");
                          cmdsql.AppendLine("           ,StepName");
                          cmdsql.AppendLine("           ,Actor");
                          cmdsql.AppendLine("           ,ModifiedDate");
                          cmdsql.AppendLine("           ,ModifiedUserID)");
                          cmdsql.AppendLine("     VALUES");
                          cmdsql.AppendLine("           (@CompanyCD");
                          cmdsql.AppendLine("           ,@FlowNo");
                          cmdsql.AppendLine("           ,@StepNo");
                          cmdsql.AppendLine("           ,@StepName");
                          cmdsql.AppendLine("           ,@Actor");
                          cmdsql.AppendLine("           ,@ModifiedDate");
                          cmdsql.AppendLine("           ,@ModifiedUserID)");
                          SqlCommand comms = new SqlCommand();
                          comms.CommandText = cmdsql.ToString();
                          comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                          comms.Parameters.Add(SqlHelper.GetParameter("@FlowNo", model.FlowNo));
                          if (dStepNo[i].ToString().Length > 0)
                          {
                              if (!string.IsNullOrEmpty(dStepNo[i].ToString().Trim()))
                              {
                                  comms.Parameters.Add(SqlHelper.GetParameter("@StepNo", dStepNo[i].ToString()));
                              }
                          }
                          if (dStepName[i].ToString().Length > 0)
                          {
                              if (!string.IsNullOrEmpty(dStepName[i].ToString().Trim()))
                              {
                                  comms.Parameters.Add(SqlHelper.GetParameter("@StepName", dStepName[i].ToString()));
                              }
                          }
                          if (dStepActor[i].ToString().Length > 0)
                          {
                              if (!string.IsNullOrEmpty(dStepActor[i].ToString().Trim()))
                              {
                                  comms.Parameters.Add(SqlHelper.GetParameter("@Actor", dStepActor[i].ToString()));
                              }
                          }
                          comms.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
                          comms.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));

                          listADD.Add(comms);
                      }
                  }
              }
              #endregion
              if (SqlHelper.ExecuteTransWithArrayList(listADD))
              {
                  result = true;
              }
              return result;
          }

          catch (Exception ex)
          {
              throw ex;
          }


      }

        /// <summary>
        /// 添加流程
        /// </summary>
        /// <returns></returns>
      public static bool InsertFlow(string[] sql)
      {
          return SqlHelper.ExecuteTransForListWithSQL(sql);
      }
      /// <summary>
      /// 事物删除
      /// </summary>
      /// <param name="sql"></param>
      /// <returns></returns>
      public static bool DeleteFlow(string[] sql)
      {
          return SqlHelper.ExecuteTransForListWithSQL(sql);
      }
      /// <summary>
      /// 修改流程
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      public static bool UpdateFlowInfo(FlowModel model)
      {
          //SQL拼写
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("UPDATE officedba.Flow			     ");
          sql.AppendLine("SET                                      ");
          sql.AppendLine("		DeptID = @DeptID,             ");
          sql.AppendLine("		FlowName = @FlowName ,            ");
          sql.AppendLine("		BillTypeFlag = @BillTypeFlag ,            ");
          sql.AppendLine("		BillTypeCode = @BillTypeCode ,            ");
          sql.AppendLine("		ModifiedDate = @ModifiedDate ,            ");
          sql.AppendLine("		ModifiedUserID = @ModifiedUserID                ");
          sql.AppendLine("WHERE                                    ");
          sql.AppendLine("		FlowNo =@FlowNo  AND CompanyCD = @CompanyCD and UsedStatus='0'   ");


          //设置参数
          SqlParameter[] param = new SqlParameter[8];
          param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
          param[1] = SqlHelper.GetParameter("@DeptID", model.DeptID);
          param[2] = SqlHelper.GetParameter("@FlowNo", model.FlowNo);
          param[3] = SqlHelper.GetParameter("@FlowName", model.FlowName);
          param[4] = SqlHelper.GetParameter("@BillTypeFlag", model.BillTypeFlag);
          param[5] = SqlHelper.GetParameter("@BillTypeCode", model.BillTypeCode);
          param[6] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);
          param[7] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
          SqlHelper.ExecuteTransSql(sql.ToString(), param);
          return SqlHelper.Result.OprateCount > 0 ? true : false;

      }
      /// <summary>
      /// 删除流程信息
      /// </summary>
      /// <param name="userID">用户ID</param>
      /// <param name="companyCD">公司代码</param>
      /// <returns>用户信息</returns>
      public static bool DeleteFlowStepInfo(string ID)
      {
          string allID = "";
          System.Text.StringBuilder sb = new System.Text.StringBuilder();
          string[] Delsql = new string[1];
          try
          {
              string[] IdS = null;
              ID = ID.Substring(0, ID.Length);
              IdS = ID.Split(',');

              for (int i = 0; i < IdS.Length; i++)
              {
                  IdS[i] = "'" + IdS[i] + "'";
                  sb.Append(IdS[i]);
              }
              allID = sb.ToString().Replace("''", "','");
              Delsql[0] = "delete from  officedba.FlowStepActor where ID IN (" + allID + ") and UsedStatus='0'";
              SqlHelper.ExecuteTransForListWithSQL(Delsql);
              return SqlHelper.Result.OprateCount > 0 ? true : false;
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }


      public static bool DelFlow(string ComPanyCD, string FlowNo)
      {
          string sqlflow = "delete from officedba.Flow where CompanyCD=@CompanyCD and FlowNo=@FlowNo";
          SqlParameter[] param = new SqlParameter[2];
          param[0] = SqlHelper.GetParameter("@CompanyCD", ComPanyCD);
          param[1] = SqlHelper.GetParameter("@FlowNo", FlowNo);
          SqlHelper.ExecuteTransSql(sqlflow, param);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }


      public static bool DelFlowstep(string ComPanyCD, string FlowNo)
      {
          string sqlflowstep = "delete from officedba.FlowStepActor where  CompanyCD=@CompanyCD and FlowNo=@FlowNo";
          SqlParameter[] param = new SqlParameter[2];
          param[0] = SqlHelper.GetParameter("@CompanyCD", ComPanyCD);
          param[1] = SqlHelper.GetParameter("@FlowNo", FlowNo);
          SqlHelper.ExecuteTransSql(sqlflowstep, param);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
        
      }

      /// <summary>
      /// 根据树返回结果集
      /// </summary>
      /// <param name="BillTypeFlag"></param>
      /// <param name="BillTypeCode"></param>
      /// <param name="CompanyCD"></param>
      /// <returns></returns>
      public static DataTable GetFlowInfo(string BillTypeFlag, string BillTypeCode, string CompanyCD, string UseStatus, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
      {
          string sql = "select isnull(c.DeptName,'') as DeptID,a.FlowNo,a.FlowName,Case WHEN a.UsedStatus = '0' ";
          sql += " THEN '草稿' WHEN a.UsedStatus = '1'then '停止' WHEN a.UsedStatus = '2'then '发布' END ";
          sql += " AS UsedStatus from officedba.Flow as a  ";
          sql += "  left join officedba.DeptInfo as c on c.ID=a.DeptID";
          sql += "  where a.CompanyCD=@CompanyCD and  a.BillTypeFlag=@BillTypeFlag and a.BillTypeCode=@BillTypeCode";
          if (!string.IsNullOrEmpty(UseStatus))
          {
              sql += " and a.UsedStatus=@UsedStatus";
          }
          SqlParameter[] param = new SqlParameter[4];
          param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          param[1] = SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag);
          param[2] = SqlHelper.GetParameter("@BillTypeCode", BillTypeCode);
          param[3] = SqlHelper.GetParameter("@UsedStatus", UseStatus);
          DataTable dt = SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount,OrderBy, param, ref totalCount);
          return dt;
      }


  
     /// <summary>
     /// 获取流程步骤信息
     /// </summary>
     /// <param name="FlowNo"></param>
     /// <param name="CompanyCD"></param>
     /// <returns></returns>
      public static DataTable GetFlowStepInfoByFlowId(string FlowNo, string CompanyCD)
      {
          string sql = "select StepNo,StepName,Actor from officedba.FlowStepActor where CompanyCD=@CompanyCD and FlowNo=@FlowNo";
          SqlParameter[] param = new SqlParameter[2];
          param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          param[1] = SqlHelper.GetParameter("@FlowNo", FlowNo);
          DataTable dt = SqlHelper.ExecuteSql(sql, param);
          return dt;
      }

      /// <summary>
      /// 获取流程信息
      /// </summary>
      /// <param name="FlowNo"></param>
      /// <param name="CompanyCD"></param>
      /// <returns></returns>
      public static DataTable GetFlowInfoByFlowId(string FlowNo, string CompanyCD)
      {
          string sql = "select isnull(c.DeptName,'') as DeptID,a.FlowNo,a.FlowName,a.DeptID as DeptmentID,a.BillTypeCode as typecode, a.UsedStatus,isnull(a.IsMobileNotice,1) as IsMobileNotice,a.BillTypeFlag,d.TypeName as BillTypeCode , b.StepNo,isnull(e.EmployeeName ,'')as actorname,b.StepName,b.Actor,d.ModuleName as billflag from officedba.Flow as a  left join officedba.DeptInfo as c on c.ID=a.DeptID right join officedba.FlowStepActor as b on  a.FlowNo=b.FlowNo and b.CompanyCD=a.CompanyCD left join  pubdba.BillType as d on d.TypeCode=a.BillTypeCode left join officedba.EmployeeInfo as e on e.ID=b.Actor where a.CompanyCD=@CompanyCD and a.FlowNo=@FlowNo and d.TypeFlag=a.BillTypeFlag and TypeLabel='0'and AuditFlag='1'";
          SqlParameter[] param = new SqlParameter[2];
          param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          param[1] = SqlHelper.GetParameter("@FlowNo", FlowNo);
          DataTable dt = SqlHelper.ExecuteSql(sql, param);
          return dt;
      }





      /// <summary>
      /// 获取流程使用状态
      /// </summary>
      /// <param name="CompanyCD"></param>
      /// <param name="BillTypeFlag"></param>
      /// <param name="BillTypeCode"></param>
      /// <returns></returns>
      public static int GetFlowUsedStatus(string CompanyCD, int BillTypeFlag, int BillTypeCode)
      {
          int result = 0;
          try
          {
              string sql = "Select UsedStatus from officedba.Flow where CompanyCD=@CompanyCD and BillTypeFlag=@BillTypeFlag and BillTypeCode=@BillTypeCode and UsedStatus=2";
              SqlParameter[] param = new SqlParameter[3];
              param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
              param[1] = SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag);
              param[2] = SqlHelper.GetParameter("@BillTypeCode", BillTypeCode);
              DataTable dtFlow = SqlHelper.ExecuteSql(sql, param);
              if (dtFlow.Rows.Count > 0)
              {
                  if (Convert.ToInt32(dtFlow.Rows[0]["UsedStatus"]) > 0)
                  {
                      result = Convert.ToInt32(dtFlow.Rows[0]["UsedStatus"]);
                  }
              }
          }
          catch (Exception ex)
          {
              throw ex;
          }
          return result;
      }

      /// <summary>
      /// 发布流程|停止流程
      /// </summary>
      /// <returns></returns>
      public static bool PublishFlow(string UsedStatus, string FlowNo, string ComPanyCD)
      {
          string sqlflowstep = "update officedba.Flow set UsedStatus=@UsedStatus where  CompanyCD=@CompanyCD and FlowNo=@FlowNo";
          SqlParameter[] param = new SqlParameter[3];
          param[0] = SqlHelper.GetParameter("@CompanyCD", ComPanyCD);
          param[1] = SqlHelper.GetParameter("@FlowNo", FlowNo);
          param[2] = SqlHelper.GetParameter("@UsedStatus", UsedStatus);
          SqlHelper.ExecuteTransSql(sqlflowstep, param);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }
      /// <summary>
      /// 获取流程实例表里的审批状态
      /// </summary>
      /// <param name="FlowNo"></param>
      /// <param name="CompanyCD"></param>
      /// <returns></returns>
      public static DataTable GetFlowStatusbyFlowNo(string FlowNo, string CompanyCD)
      {
          string sql = "select count(*) as status from officedba.FlowInstance a where a.CompanyCD=@CompanyCD and a.FlowNo=@FlowNo and a.FlowStatus in(1,2) and a.BillID not in(select BillID from officedba.FlowInstance b where b.CompanyCD=@CompanyCD and b.FlowNo=@FlowNo and b.FlowStatus=3 and a.BillID=b.BillID)";
          SqlParameter[] param = new SqlParameter[2];
          param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          param[1] = SqlHelper.GetParameter("@FlowNo", FlowNo);
          DataTable dt = SqlHelper.ExecuteSql(sql, param);
          return dt;
      }

    }
}
