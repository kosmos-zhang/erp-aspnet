/**********************************************
 * 类作用：   摘要设置数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/03/10
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
namespace XBase.Data.Office.FinanceManager
{
   public class SummarySettingDBHelper
   {
       #region 新增摘要信息
       /// <summary>
       /// 新增摘要信息
       /// </summary>
       /// <param name="Model">实体</param>
       /// <returns>true 成功，false失败</returns>
       public static bool InsertSummary(SummarySettingModel Model)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Insert into officedba.SummarySetting(CompanyCD,");
           sql.AppendLine("SummTypeID,Name,UsedStatus)");
           sql.AppendLine("values(@CompanyCD,@SummTypeID,");
           sql.AppendLine("@Name,@UsedStatus)");
          SqlParameter []parms=new SqlParameter[4];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
          parms[1] = SqlHelper.GetParameter("@SummTypeID", Model.SummTypeID);
          parms[2] = SqlHelper.GetParameter("@Name", Model.Name);
          parms[3] = SqlHelper.GetParameter("@UsedStatus", Model.UsedStatus);
          SqlHelper.ExecuteTransSql(sql.ToString(), parms);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 修改摘要信息
       /// <summary>
       /// 修改摘要信息
       /// </summary>
       /// <param name="Model">实体</param>
       /// <returns>true 成功，false失败</returns>
       public static bool UpdateSummary(SummarySettingModel Model)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update officedba.SummarySetting set SummTypeID=@SummTypeID,Name=@Name,");
           sql.AppendLine("UsedStatus=@UsedStatus  where CompanyCD=@CompanyCD and ID=@SummaryCD");
           SqlParameter[] parms = new SqlParameter[5];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
           parms[1] = SqlHelper.GetParameter("@SummaryCD", Model.SummaryCD);
           parms[2] = SqlHelper.GetParameter("@SummTypeID", Model.SummTypeID);
           parms[3] = SqlHelper.GetParameter("@Name", Model.Name);
           parms[4] = SqlHelper.GetParameter("@UsedStatus", Model.UsedStatus);
           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 删除摘要信息
       /// <summary>
       /// 删除摘要信息
       /// </summary>
       /// <param name="SummaryCD">摘要编码</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns>true 成功，false失败</returns>
       public static bool DelSummary(string SummaryCD, string CompanyCD)
       {
           string sql = "Delete from officedba.SummarySetting where ID in ("+SummaryCD+") and CompanyCD=@CompanyCD";

           SqlParameter[] parms = 
           {
               new SqlParameter("@CompanyCD",CompanyCD)
           }; 

           SqlHelper.ExecuteTransSql(sql, parms);

           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 获取摘要信息
       /// <summary>
       /// 获取摘要信息
       /// </summary>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns>DataTable</returns>
       public static DataTable GetSummaryByCompanyCD(string CompanyCD,string TypeID,string isUsed)
       {
           StringBuilder sql = new StringBuilder();

           sql.AppendLine("select a.ID,a.SummTypeID,a.[Name],");
           sql.AppendLine("a.UsedStatus,b.TypeName from");
           sql.AppendLine("officedba.summarySetting as a");
           sql.AppendLine("inner join officedba.SummaryType as");
           sql.AppendLine("b on a.SummTypeID=b.ID where 1=1 ");

           SqlCommand cmd = new SqlCommand();
           if (!string.IsNullOrEmpty(CompanyCD))
           {
               sql.AppendLine(" and a.CompanyCD=@CompanyCD");


               cmd.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
           }
           if (!string.IsNullOrEmpty(TypeID))
           {
               sql.AppendLine(" and  a.SummTypeID=@TypeID");

               cmd.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", TypeID));
           }
           if (!string.IsNullOrEmpty(isUsed))
           {
               sql.AppendLine(" and  a.UsedStatus=@UsedStatus");

               cmd.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", isUsed));
           }
      
           //指定命令的SQL文
           cmd.CommandText = sql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(cmd);
  
       }
       #endregion

       #region 根据摘要名称获取ID
       public static  string  GetSummaryIDByName(string SummaryName)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select ID from officedba.SummarySetting ");
           sql.AppendLine(" where [Name]=@Name and UsedStatus='1'");

           SqlParameter[] parms = 
           {
               new SqlParameter("@Name",SummaryName)
           };

          return SqlHelper.ExecuteSql(sql.ToString(),parms).Rows[0]["ID"].ToString();
       
       }
       #endregion

   }

}
