/**********************************************
 * 类作用：  考核自我鉴定操作
 * 建立人：   王保军
 * 建立时间： 2009/05/10
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
   public  class PerformanceGradeDBHelper
    {

       public static DataTable SearchTaskInfo(PerformanceScoreModel model)
       {

           #region 查询语句
           //查询SQL拼写
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine(" select distinct(a.TaskNo),a.TemplateNo,isnull( f.Title,'') as TemplateName,isnull(b.Title,'') as TaskTitle,a.EmployeeID");
           searchSql.AppendLine(" 	,CASE b.TaskFlag                  ");
           searchSql.AppendLine(" 	WHEN '1' THEN '月考核'              ");
           searchSql.AppendLine(" 	WHEN '2' THEN '季考核'              ");
           searchSql.AppendLine(" 	WHEN '3' THEN '半年考核'              ");
           searchSql.AppendLine(" 	WHEN '4' THEN '年考核'              ");
           searchSql.AppendLine(" 	WHEN '5' THEN '临时考核'              ");
           searchSql.AppendLine(" 	WHEN null THEN ' '              ");
           searchSql.AppendLine(" 	ELSE ''                           ");
           searchSql.AppendLine(" 	END AS TaskFlag,a.TemplateNo             ");
           searchSql.AppendLine(" , isnull(c.EmployeeName,'') as EmployeeName,isnull(d.EmployeeName,'') as ScoreEmployeeName ,isnull(e.EmployeeName,'') as CreateEmployeeName, ISNULL( CONVERT(VARCHAR(10), b.CreateDate ,21),'') as CreateDate  from officedba.PerformanceScore a left outer join officedba.PerformanceTask b on a.CompanyCD=b.CompanyCD and a.TaskNo=b.TaskNo   left outer join officedba.EmployeeInfo c on c.CompanyCD=a.CompanyCD and a.EmployeeID=c.ID left outer join officedba.EmployeeInfo d on d.CompanyCD=a.CompanyCD and a.ScoreEmployee=d.ID left outer join officedba.EmployeeInfo e on e.CompanyCD=a.CompanyCD and b.Creator =e.ID left outer join officedba.PerformanceTemplate f on  a.CompanyCD=f.CompanyCD  and  a.TemplateNo=f.TemplateNo");
        //   searchSql.AppendLine(" WHERE	a.CompanyCD = @CompanyCD ");
           searchSql.AppendLine(" WHERE	a.CompanyCD = @CompanyCD and a.Status=@Status  and  a.ScoreEmployee=@ScoreEmployee");
           #endregion
          
           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
         comm.Parameters.Add(SqlHelper.GetParameterFromString("@ScoreEmployee", model.ScoreEmployee));
         comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", model.Status ));
           if (!string.IsNullOrEmpty(model.TaskNo))
           {
               searchSql.AppendLine(" AND a.TaskNo like @TaskNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", "%"+model.TaskNo+"%"));
           }

           if (!string.IsNullOrEmpty(model.TaskFlag))
           {

               searchSql.AppendLine(" AND b.TaskFlag = @TaskFlag ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag", model.TaskFlag));
           }
           if (!string.IsNullOrEmpty(model.TaskTitle))
           {
               searchSql.AppendLine(" AND b.Title LIKE @Title ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + model.TaskTitle + "%"));
           }
           if (!string.IsNullOrEmpty(model.EmployeeID))
           {
               searchSql.AppendLine(" AND a.EmployeeID = @EmployeeID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));
           }
           //启用状态
        
           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }


    }
}
