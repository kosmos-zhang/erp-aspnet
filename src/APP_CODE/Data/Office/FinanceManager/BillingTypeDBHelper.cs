/**********************************************
 * 类作用：   开票类型数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/04/14
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
namespace XBase.Data.Office.FinanceManager
{
   public class BillingTypeDBHelper
    {
       /// <summary>
       /// 添加开票类型
       /// </summary>
       /// <param name="CompanyCD">公司编码</param>
        /// <param name="CompanyCD">名称</param>
       /// <param name="UsedStatus">使用状态</param>
       /// <param name="Remark">备注</param>
       /// <returns>true 成功，false失败</returns>
       public static bool InsertBillingType(string CompanyCD,string Name, string UsedStatus, string Remark,out int ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Insert into officedba.BillingType");
           sql.AppendLine("(CompanyCD,Name,UsedStatus,Remark)");
           sql.AppendLine("values(@CompanyCD,@Name,@UsedStatus,@Remark)");
           sql.AppendLine("set @ID= @@IDENTITY");
           SqlParameter []parms=new SqlParameter[5];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           parms[1] = SqlHelper.GetParameter("@Name",Name);
           parms[2] = SqlHelper.GetParameter("@UsedStatus", UsedStatus);
           parms[3] = SqlHelper.GetParameter("@Remark", Remark);
           parms[4] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);
           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           ID =Convert.ToInt32(parms[4].Value);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }

       /// <summary>
       /// 修改开票类型
       /// </summary>
       /// <param name="ID">主键</param>
       /// <param name="Name">名称</param>
       /// <param name="UsedStatus">使用状态</param>
       /// <param name="Remark">备注</param>
       /// <returns>true 成功，false失败</returns>
       public static bool UpdateBillingType(string ID, string Name, string UsedStatus, string Remark)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update  officedba.BillingType");
           sql.AppendLine("set Name=@Name,");
           sql.AppendLine("UsedStatus=@UsedStatus,");
           sql.AppendLine("Remark=@Remark ");
           sql.AppendLine("where ID=@ID");
           SqlParameter []parms=new SqlParameter[4];
           parms[0] = SqlHelper.GetParameter("@Name", Name);
           parms[1] = SqlHelper.GetParameter("@UsedStatus", UsedStatus);
           parms[2] = SqlHelper.GetParameter("@Remark", Remark);
           parms[3] = SqlHelper.GetParameter("@ID", ID);
           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }

       /// <summary>
       /// 删除开票类别
       /// </summary>
       /// <param name="ID">主键</param>
       /// <returns>true 成功，false失败</returns>
       public static bool DelBillingType(string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Delete from officedba.BillingType");
           sql.AppendLine("where ID=@ID");
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@ID", ID);
           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }


       /// <summary>
       /// 获取开票类型所有信息
       /// </summary>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns>DataTable</returns>
       public static DataTable GetBillingTypeInfo(string CompanyCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select ID,Name,case when UsedStatus='1' then '启用' when UsedStatus='0' then '停用' end as  UsedStatus ,Remark");
           sql.AppendLine("from officedba.BillingType");
           sql.AppendLine("where CompanyCD=@CompanyCD");
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           return SqlHelper.ExecuteSql(sql.ToString(),parms);
       }

       /// <summary>
       /// 获取开票类型状态为启用状态的信息
       /// </summary>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns>DataTable</returns>
       public static DataTable GetBillingTypeIsUsed(string CompanyCD)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select ID,Name,UsedStatus,Remark");
           sql.AppendLine("from officedba.BillingType");
           sql.AppendLine("where CompanyCD=@CompanyCD");
           sql.AppendLine("and UsedStatus=1");
           SqlParameter[] parms = new SqlParameter[1];
           parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
           return SqlHelper.ExecuteSql(sql.ToString(), parms);
       }

    }
}
