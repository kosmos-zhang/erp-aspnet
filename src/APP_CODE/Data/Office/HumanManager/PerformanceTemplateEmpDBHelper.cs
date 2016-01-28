/*************王保军
 * 建立时间： 2009/05/07
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
   public  class PerformanceTemplateEmpDBHelper
    {
       /// <summary>
       /// 获取模板信息
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static DataTable SearchCheckElemInfo(PerformanceTemplateModel model)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine(" SELECT                             ");
           searchSql.AppendLine(" 	 TemplateNo                               ");
           searchSql.AppendLine(" 	,ISNULL(Title, '') AS Title ");
           searchSql.AppendLine(" FROM                               ");
           searchSql.AppendLine(" 	officedba.PerformanceTemplate         ");
           searchSql.AppendLine(" WHERE                              ");
           searchSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
           searchSql.AppendLine(" and 	UsedStatus = @UsedStatus            ");
           #endregion

           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD ));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", "1"));
           //l
           //启用状态
        

           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       /// <summary>
       /// 更新模板信息
       /// </summary>
       /// <param name="modeList"></param>
       /// <param name="templateList"></param>
       /// <returns></returns>
       public static bool UpdatePerformenceTempElm(IList<PerformanceTemplateEmpModel> modeList,string [] templateList)
       {
           if (DeleteByTemplateNo(modeList))
           {
               if (UpdatePerformenceTempElm(modeList))
               {
                   return true;
               }
               else
               {
                   return false;
               }

           }
           else
           {
               return false;
           }
           
           
          


       }
       /// <summary>
       /// 删除模板
       /// </summary>
       /// <param name="modeList"></param>
       /// <returns></returns>
       public static bool DeleteByTemplateNo(IList<PerformanceTemplateEmpModel> modeList)
       {
          
           #region 插入SQL拼写
           StringBuilder insertSql = new StringBuilder();
           insertSql.AppendLine("delete from officedba.PerformanceTemplateEmp where CompanyCD=@CompanyCD and TemplateNo=@TemplateNo  and EmployeeID=@EmployeeID ");
         //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
           #endregion
           //定义插入基本信息的命令
           SqlCommand comm = new SqlCommand();
           comm.CommandText = insertSql.ToString();
           //设置保存的参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", modeList[0].CompanyCD));	//公司代码
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", modeList[0].TemplateNo ));	//类型名称
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", modeList[0].EmployeeID));	//类型名称
           //添加返回参数
        //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

           //执行插入操作
           bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
        
           return isSucc;
          


       }
       public static bool IsExist(string templateNo, string CompanyCD, string EmployeeID)
       {
           //校验SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine(" SELECT                       ");
           searchSql.AppendLine(" 	COUNT(*) AS UsedCount       ");
           searchSql.AppendLine(" FROM                         ");
           searchSql.AppendLine(" 	officedba.PerformanceTemplateEmp ");
           searchSql.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "'");
           searchSql.AppendLine("and TemplateNo='" + templateNo + "'");
           searchSql.AppendLine("and EmployeeID='" + EmployeeID + "'");
           //searchSql.AppendLine("and StepNo='" + StepNo + "'");

           //执行查询
           DataTable dtCount = SqlHelper.ExecuteSql(searchSql.ToString());
           //获取记录数
           int count = GetSafeData.ValidateDataRow_Int(dtCount.Rows[0], "UsedCount");

           //返回结果
           return count > 0 ? true : false;
       }
       public static bool UpdatePerformenceTempElm(IList<PerformanceTemplateEmpModel> modeList)
       {
           bool isSucc;
           foreach (PerformanceTemplateEmpModel model in modeList)
           {

             
               #region 插入SQL拼写
               StringBuilder insertSql = new StringBuilder();
               insertSql.AppendLine("INSERT INTO officedba.PerformanceTemplateEmp ");
               insertSql.AppendLine("           (CompanyCD             ");
               insertSql.AppendLine("           ,TemplateNo                ");
               insertSql.AppendLine("           ,EmployeeID              ");
               insertSql.AppendLine("           ,StepNo                 ");
               insertSql.AppendLine("           ,StepName           ");
               insertSql.AppendLine("           ,Rate               ");
               insertSql.AppendLine("           ,ScoreEmployee               ");
               insertSql.AppendLine("           ,remark               ");
               insertSql.AppendLine("           ,ModifiedDate               ");
               insertSql.AppendLine("           ,ModifiedUserID)                 ");

               insertSql.AppendLine("     VALUES                        ");
               insertSql.AppendLine("           (@CompanyCD            ");
               insertSql.AppendLine("           ,@TemplateNo               ");
               insertSql.AppendLine("           ,@EmployeeID             ");
               insertSql.AppendLine("           ,@StepNo               ");
               insertSql.AppendLine("           ,@StepName          ");
               insertSql.AppendLine("           ,@Rate             ");
               insertSql.AppendLine("           ,@ScoreEmployee               ");
               insertSql.AppendLine("           ,@remark               ");
               insertSql.AppendLine("           ,getdate()              ");
               insertSql.AppendLine("           ,@ModifiedUserID)                ");
               //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
               #endregion
               //定义插入基本信息的命令
               SqlCommand comm = new SqlCommand();
               comm.CommandText = insertSql.ToString();
               //设置保存的参数
               SetSaveParameter(comm, model);

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
           return true;



       }
       /// <summary>
       /// 批插入模板信息
       /// </summary>
       /// <param name="modeList"></param>
       /// <returns></returns>
       public static bool InsertPerformenceTempElm(IList<PerformanceTemplateEmpModel> modeList)
       {  bool isSucc ;
     
       DataTable temp = new DataTable();
       DataColumn newDC;
       newDC = new DataColumn("EmployeeID", System.Type.GetType("System.String"));
       temp.Columns.Add(newDC);
       newDC = new DataColumn("TemplateNo", System.Type.GetType("System.String"));
       temp.Columns.Add(newDC);
       newDC = new DataColumn("CompanyCD", System.Type.GetType("System.String"));
       temp.Columns.Add(newDC);

       DataTable tempHave = new DataTable();
       DataColumn newDCHave;
       newDCHave = new DataColumn("EmployeeID", System.Type.GetType("System.String"));
       tempHave.Columns.Add(newDCHave);
       newDCHave = new DataColumn("TemplateNo", System.Type.GetType("System.String"));
       tempHave.Columns.Add(newDCHave);
       newDCHave = new DataColumn("CompanyCD", System.Type.GetType("System.String"));
       tempHave.Columns.Add(newDCHave);

           foreach (PerformanceTemplateEmpModel model in modeList)
           {

               if (IsExist(model.TemplateNo, model.CompanyCD, model.EmployeeID))
               {
                   bool sign = false;
                   if (tempHave.Rows.Count > 0)
                   {
                       for (int f = 0; f < tempHave.Rows.Count; f++)
                       {
                           if (tempHave.Rows[f][0].ToString() == model.EmployeeID && tempHave.Rows[f][1].ToString() == model.TemplateNo && tempHave.Rows[f][2].ToString() == model.CompanyCD)
                           {
                               sign = true;
                               break;
                           }
                       }
                   }

                   if (!sign)
                   {

                       if (temp.Rows.Count == 0)
                       {
                           DataRow newRow = temp.NewRow();
                           newRow[0] = model.EmployeeID;
                           newRow[1] = model.TemplateNo;
                           newRow[2] = model.CompanyCD;
                           temp.Rows.Add(newRow);
                           if (!DeletePerTemplateEmp(model.TemplateNo, model.CompanyCD, model.EmployeeID))
                           {
                               isSucc = false;
                               break;
                           }

                       }
                       else
                       {
                           for (int d = 0; d < temp.Rows.Count; d++)
                           {
                               if (temp.Rows[d][0].ToString() == model.EmployeeID && temp.Rows[d][1].ToString() == model.TemplateNo && temp.Rows[d][2].ToString() == model.CompanyCD)
                               {
                                   break;
                               }
                               else
                               {
                                   DataRow newRow = temp.NewRow();
                                   newRow[0] = model.EmployeeID;
                                   newRow[1] = model.TemplateNo;
                                   newRow[2] = model.CompanyCD;
                                   temp.Rows.Add(newRow);
                                   if (!DeletePerTemplateEmp(model.TemplateNo, model.CompanyCD, model.EmployeeID))
                                   {
                                       isSucc = false;
                                       break;
                                   }
                               }
                           }

                       }
                   }

               }
               else
               {
                   DataRow newRow2 = tempHave.NewRow();
                   newRow2[0] = model.EmployeeID;
                   newRow2[1] = model.TemplateNo;
                   newRow2[2] = model.CompanyCD;
                   tempHave.Rows.Add(newRow2);
               }
               #region 插入SQL拼写
               StringBuilder insertSql = new StringBuilder();
               insertSql.AppendLine("INSERT INTO officedba.PerformanceTemplateEmp ");
               insertSql.AppendLine("           (CompanyCD             ");
               insertSql.AppendLine("           ,TemplateNo                ");
               insertSql.AppendLine("           ,EmployeeID              ");
               insertSql.AppendLine("           ,StepNo                 ");
               insertSql.AppendLine("           ,StepName           ");
               insertSql.AppendLine("           ,Rate               ");
               insertSql.AppendLine("           ,ScoreEmployee               ");
               insertSql.AppendLine("           ,remark               ");
               insertSql.AppendLine("           ,ModifiedDate               ");
               insertSql.AppendLine("           ,ModifiedUserID)                 ");

               insertSql.AppendLine("     VALUES                        ");
               insertSql.AppendLine("           (@CompanyCD            ");
               insertSql.AppendLine("           ,@TemplateNo               ");
               insertSql.AppendLine("           ,@EmployeeID             ");
               insertSql.AppendLine("           ,@StepNo               ");
               insertSql.AppendLine("           ,@StepName          ");
               insertSql.AppendLine("           ,@Rate             ");
               insertSql.AppendLine("           ,@ScoreEmployee               ");
               insertSql.AppendLine("           ,@remark               ");
               insertSql.AppendLine("           ,getdate()              ");
               insertSql.AppendLine("           ,@ModifiedUserID)                ");
               //  insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
               #endregion
               //定义插入基本信息的命令
               SqlCommand comm = new SqlCommand();
               comm.CommandText = insertSql.ToString();
               //设置保存的参数
               SetSaveParameter(comm, model);

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
           return true;
          


       }
      /// <summary>
      /// 保存基本参数
      /// </summary>
      /// <param name="comm"></param>
      /// <param name="model"></param>
       private static void SetSaveParameter(SqlCommand comm, PerformanceTemplateEmpModel  model)
       {
           //设置参数

           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo ));	//类型名称
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID ));	//创建人
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@StepNo", model.StepNo ));	//启用状态
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@StepName", model.StepName ));	//更新用户ID
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate", model.Rate ));	//更新用户ID
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@ScoreEmployee", model.ScoreEmployee ));	//更新用户ID
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@remark", model.remark ));	//更新用户ID
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID ));	//更新用户ID
           if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag ))
           {
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID ));	//考核类型编号
           }
           


       }
       /// <summary>
       /// 查询模板库和模板流程库获取模板和模板相关的指标、指标顺序、权重信息
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static DataTable SearchFlowInfo(PerformanceTemplateEmpModel model)
       {

       
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();

           searchSql.AppendLine("select a.TemplateNo,a.EmployeeID,a.ID,ISNULL(a.StepName,'') as StepName, ISNULL(b.EmployeeName,'') as ScoreEmployee,ISNULL(c.EmployeeName,'') as EmployeeName,ISNULL(d.Title,'') as  TemplateName,isnull(Convert(varchar(100),a.ModifiedDate,23),'') AS ModifiedDate from  officedba.PerformanceTemplateEmp a left outer join  officedba.EmployeeInfo b on b.CompanyCD=a.CompanyCD and a.ScoreEmployee=b.ID left outer join officedba.EmployeeInfo c on c.CompanyCD=a.CompanyCD and a.EmployeeID=c.ID left outer join officedba.PerformanceTemplate d on a.CompanyCD=d.CompanyCD and a.TemplateNo=d.TemplateNo   where a.CompanyCD = @CompanyCD   ");
           if ( !string .IsNullOrEmpty  (model.EmployeeID))
           {
               searchSql.AppendLine("and a.EmployeeID=@EmployeeID");
           }
           if (!string .IsNullOrEmpty  (model.ScoreEmployee))
           {

               searchSql.AppendLine("and a.ScoreEmployee=@ScoreEmployee");
           }



           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           if (model.EmployeeID != "")
           {
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID ));
           }
           if (model.ScoreEmployee != "")
           {
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@ScoreEmployee", model.ScoreEmployee));
           }
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD ));
           //l
           //启用状态


           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       /// <summary>
       /// 根据员工编号和公司代码获取该员工信息
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static DataTable GetEmployeeInfo(PerformanceTemplateEmpModel model)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("select a.StepNo,a.Rate,a.ScoreEmployee,a.TemplateNo, a.EmployeeID,a.ID,ISNULL(a.StepName,'') as StepName, ISNULL(b.EmployeeName,'') as ScoreEmployeeName,ISNULL(c.EmployeeName,'') as EmployeeName,ISNULL(d.Title,'') as  TemplateName,ISNULL(a.remark,'') as remark from officedba.PerformanceTemplateEmp a left outer join  officedba.EmployeeInfo b on b.CompanyCD=a.CompanyCD and a.ScoreEmployee=b.ID left outer join officedba.EmployeeInfo c on c.CompanyCD=a.CompanyCD and a.EmployeeID=c.ID left outer join officedba.PerformanceTemplate d on  a.CompanyCD=d.CompanyCD  and a.TemplateNo=d.TemplateNo where a.CompanyCD = @CompanyCD and a.TemplateNo=@TemplateNo  and a.EmployeeID=@EmployeeID  ");
           #endregion


           
           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID ));
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo ));
           //l
           //启用状态


           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       public static bool DeletePerTemplateEmpInfo(string id, string CompanyCD)
       {
           //删除SQL拼写
           StringBuilder deleteSql = new StringBuilder();
           deleteSql.AppendLine(" DELETE FROM officedba.PerformanceTemplateEmp ");
           deleteSql.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "'");
           deleteSql.AppendLine("and  ID IN (" + id + ")");

           //定义更新基本信息的命令
           SqlCommand comm = new SqlCommand();
           comm.CommandText = deleteSql.ToString();

           //执行插入操作并返回更新结果
           return   SqlHelper.ExecuteTransWithCommand(comm);
          
       }
       public static bool DeletePerTemplateEmp(string templateNo, string CompanyCD, string EmployeeID)
       {
           //删除SQL拼写
           StringBuilder deleteSql = new StringBuilder();
           deleteSql.AppendLine(" DELETE FROM officedba.PerformanceTemplateEmp ");
           deleteSql.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "'");
           deleteSql.AppendLine("and  TemplateNo  ='" + templateNo + "'");
           deleteSql.AppendLine("and  EmployeeID  ='" + EmployeeID + "'");
           //deleteSql.AppendLine("and  StepNo  ='" + StepNo + "'");
           //定义更新基本信息的命令
           SqlCommand comm = new SqlCommand();
           comm.CommandText = deleteSql.ToString();

           //执行插入操作并返回更新结果
           return SqlHelper.ExecuteTransWithCommand(comm);

       }

    }
}