/**********************************************
 * 类作用：   报表项目设置数据库层处理
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
  public class ReportItemSettingDBHelper
    {

      /// <summary>
      /// 新增报表项目信息
      /// </summary>
      /// <param name="Model">实体</param>
     /// <returns>true 成功，false失败</returns>
      public static bool InsertReportItem(ReportItemSettingModel Model)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Insert into officedba.ReportItemSetting (CompanyCD,");
          sql.AppendLine("ItemType,ItemRow,ItemName,UsedStatus)");
          sql.AppendLine("values(@CompanyCD,@ItemType,@ItemRow,@ItemName,@UsedStatus)");
          SqlParameter []parms=new  SqlParameter[5];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
          parms[1] = SqlHelper.GetParameter("@ItemType", Model.ItemType);
          parms[2] = SqlHelper.GetParameter("@ItemRow", Model.ItemRow);
          parms[3] = SqlHelper.GetParameter("@ItemName", Model.ItemName);
          parms[4] = SqlHelper.GetParameter("@UsedStatus", Model.UsedStatus);
          SqlHelper.ExecuteTransSql(sql.ToString(), parms);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }

      /// <summary>
      /// 修改报表项目信息
      /// </summary>
      /// <param name="Model">实体</param>
      /// <returns>true 成功，false失败</returns>
      public static bool UpdateReportItem(ReportItemSettingModel Model)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Update officedba.ReportItemSetting set ItemType=@ItemType ,");
          sql.AppendLine("ItemRow=@ItemRow,ItemName=@ItemName,UsedStatus=@UsedStatus");
          sql.AppendLine("where CompanyCD=@CompanyCD and ID=@ID");
          SqlParameter[] parms = new SqlParameter[6];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
          parms[1] = SqlHelper.GetParameter("@ItemType", Model.ItemType);
          parms[2] = SqlHelper.GetParameter("@ItemRow", Model.ItemRow);
          parms[3] = SqlHelper.GetParameter("@ItemName", Model.ItemName);
          parms[4] = SqlHelper.GetParameter("@UsedStatus", Model.UsedStatus);
          parms[5] = SqlHelper.GetParameter("@ID", Model.ID);
          SqlHelper.ExecuteTransSql(sql.ToString(), parms);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }

      /// <summary>
      /// 删除报表项目信息
      /// </summary>
      /// <param name="CompanyCD">公司编码</param>
      /// <param name="ID">主键</param>
      /// <returns>true 成功，false失败</returns>
      public static bool DelReportItem(string CompanyCD, string ID)
      {
          string sql = "delete from officedba.ReportItemSetting where  CompanyCD=@CompanyCD and ID=@ID";
          SqlParameter[] parms = new SqlParameter[2];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          parms[1] = SqlHelper.GetParameter("@ID", ID);
          SqlHelper.ExecuteTransSql(sql.ToString(), parms);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }

      /// <summary>
      /// 获取报表项目信息
      /// </summary>
      /// <param name="CompanyCD">公司编码</param>
      /// <returns>DataTable</returns>
      public static DataTable GetReportItemInfo(string CompanyCD)
      {
          string sql = "select ID,ItemType,ItemRow,ItemName,UsedStatus  from  officedba.ReportItemSetting where  CompanyCD=@CompanyCD";
          SqlParameter[] parms = new SqlParameter[1];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          return  SqlHelper.ExecuteSql(sql,parms);
      }
    }
}
