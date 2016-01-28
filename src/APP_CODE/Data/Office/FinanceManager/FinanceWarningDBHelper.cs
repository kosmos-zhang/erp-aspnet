/**********************************************
 * 类作用：   预警设置数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/03/24
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
namespace XBase.Data.Office.FinanceManager
{
 public class FinanceWarningDBHelper
    {

     /// <summary>
     /// 根据企业编码获取预警信息
     /// </summary>
     /// <param name="CompanyCD">企业编码</param>
     /// <returns>DataTable</returns>
     public static DataTable GetFinenceWarningInfo(string CompanyCD)
     {
         StringBuilder sql = new StringBuilder();
         sql.AppendLine("select a.CompanyCD,a.ID,a.WarningItem,a.UpLimit,a.Lowerlimit,");
         sql.AppendLine("a.WarningPersonID,b.EmployeeName,a.Remark,a.UsedStatus,a.WarningWay  from");
         sql.AppendLine("officedba.FinanceWarning as a inner join  officedba.EmployeeInfo ");
         sql.AppendLine("as b on a.WarningPersonID=b.ID where a.CompanyCD=@CompanyCD ");
         SqlParameter[] parms = new SqlParameter[1];
         parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
         return SqlHelper.ExecuteSql(sql.ToString(), parms);
     }



     /// <summary>
     /// 添加预警信息
     /// </summary>
     /// <param name="Model">预警实体</param>
     /// <returns>true 成功,false 失败</returns>
     public static bool InsertWarningInfo(FinanceWarningModel Model)
     {
         StringBuilder sql = new StringBuilder();
         sql.AppendLine("INSERT INTO  officedba.FinanceWarning( ");
         sql.AppendLine("[CompanyCD] ,[WarningItem] ,");
         sql.AppendLine("[UpLimit] ,[Lowerlimit] ,[WarningPersonID] ,");
         sql.AppendLine("[UsedStatus] ,[WarningWay] ,[Remark] )");
         sql.AppendLine(" VALUES (@CompanyCD,@WarningItem,");
         sql.AppendLine("@UpLimit,@Lowerlimit,@WarningPersonID,@UsedStatus,");
         sql.AppendLine("@WarningWay,@Remark)");
         SqlParameter []parms=new SqlParameter[8];
         parms[0] = SqlHelper.GetParameter("@CompanyCD",Model.CompanyCD);
         parms[1] = SqlHelper.GetParameter("@WarningItem",Model.WarningItem);
         parms[2] = SqlHelper.GetParameter("@UpLimit",Model.UpLimit);
         parms[3] = SqlHelper.GetParameter("@Lowerlimit",Model.Lowerlimit);
         parms[4] = SqlHelper.GetParameter("@WarningPersonID",Model.WarningPersonID);
         parms[5] = SqlHelper.GetParameter("@UsedStatus",Model.UsedStatus);
         parms[6] = SqlHelper.GetParameter("@WarningWay",Model.WarningWay);
         parms[7] = SqlHelper.GetParameter("@Remark",Model.Remark);
         SqlHelper.ExecuteTransSql(sql.ToString(), parms);
         return SqlHelper.Result.OprateCount > 0 ? true : false;
     }

     /// <summary>
     /// 更新预警信息
     /// </summary>
     /// <param name="Model">预警实体</param>
     /// <returns>true 成功,false 失败</returns>
     public static bool UpdateWarningInfo(FinanceWarningModel Model)
     {
         StringBuilder sql = new StringBuilder();
         sql.AppendLine("update officedba.FinanceWarning set ");
         sql.AppendLine("[WarningItem] =@WarningItem,");
         sql.AppendLine("[UpLimit] =@UpLimit,");
         sql.AppendLine("[Lowerlimit] =@Lowerlimit,");
         sql.AppendLine("[WarningPersonID] =@WarningPersonID,");
         sql.AppendLine("[UsedStatus] = @UsedStatus,");
         sql.AppendLine("[WarningWay] = @WarningWay,");
         sql.AppendLine("[Remark] = @Remark ");
         sql.AppendLine("where CompanyCD=@CompanyCD");
         sql.AppendLine("and ID=@ID");
         SqlParameter[]parms=new SqlParameter[9];
         parms[0] = SqlHelper.GetParameter("@WarningItem", Model.WarningItem);
         parms[1] = SqlHelper.GetParameter("@UpLimit", Model.UpLimit);
         parms[2] = SqlHelper.GetParameter("@Lowerlimit", Model.Lowerlimit);
         parms[3] = SqlHelper.GetParameter("@WarningPersonID", Model.WarningPersonID);
         parms[4] = SqlHelper.GetParameter("@UsedStatus", Model.UsedStatus);
         parms[5] = SqlHelper.GetParameter("@WarningWay", Model.WarningWay);
         parms[6] = SqlHelper.GetParameter("@Remark", Model.Remark);
         parms[7] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
         parms[8] = SqlHelper.GetParameter("@ID", Model.ID);
         SqlHelper.ExecuteTransSql(sql.ToString(), parms);
         return SqlHelper.Result.OprateCount > 0 ? true : false;
     }

     /// <summary>
     /// 根据指定条件删除预警信息
     /// </summary>
     /// <param name="CompanyCD">企业编码</param>
     /// <param name="ID">主键ID</param>
     /// <returns>true 成功,false 失败</returns>
     public static bool DelWaringInfo(string CompanyCD, string ID)
     {
         string sql = "Delete from  officedba.FinanceWarning where CompanyCD=@CompanyCD and ID=@ID";
         SqlParameter[] parms = new SqlParameter[2];
         parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
         parms[1] = SqlHelper.GetParameter("@ID", ID);
         SqlHelper.ExecuteTransSql(sql.ToString(), parms);
         return SqlHelper.Result.OprateCount > 0 ? true : false;
     }

   }
}
