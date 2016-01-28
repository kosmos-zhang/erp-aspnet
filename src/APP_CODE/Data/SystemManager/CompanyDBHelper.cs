/**********************************************
 * 描述：     公司数据层处理
 * 建立人：   江贻明
 * 建立时间： 2009/01/21
 * 类名：CompanyDBHelper
 ***********************************************/
using System.Data;
using XBase.Model.SystemManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
namespace XBase.Data.SystemManager
{
    public class CompanyDBHelper
    {
        /// <summary>
        /// 增加公司信息
        /// </summary>
        /// <returns></returns>
        public static bool AddCompany(CompanyModel Model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Insert into pubdba.company(CompanyCD,CompanyNameCn,CompanyNameEn,");
            sql.AppendLine("ProvCD,CityCD,Addr,Contact,Tel,Fax,Post,HomePage,Email,QQ,");
            sql.AppendLine("MSN,IM,TradeCD,Staff,Size,Production,Sale,Credit,Remark,ModifiedDate,ModifiedUserID)");
            sql.AppendLine("Values(@CompanyCD,@CompanyNameCn,@CompanyNameEn,@ProvCD,@CityCD,@Addr,");
            sql.AppendLine("@Contact,@Tel,@Fax,@Post,@HomePage,@Email,@QQ,@MSN,");
            sql.AppendLine("@IM,@TradeCD,@Staff,@Size,@Production,@Sale,@Credit,@Remark,getdate(),@ModifiedUserID)");
            SqlParameter[] parms = new SqlParameter[23];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
            parms[1] = SqlHelper.GetParameter("@CompanyNameCn", Model.CompanyNameCn);
            parms[2] = SqlHelper.GetParameter("@CompanyNameEn", Model.CompanyNameEn);
            parms[3] = SqlHelper.GetParameter("@ProvCD", Model.ProvCD);
            parms[4] = SqlHelper.GetParameter("@CityCD", Model.CityCD);
            parms[5] = SqlHelper.GetParameter("@Addr", Model.Addr);
            parms[6] = SqlHelper.GetParameter("@Contact", Model.Contact);
            parms[7] = SqlHelper.GetParameter("@Tel", Model.Tel);
            parms[8] = SqlHelper.GetParameter("@Fax", Model.Fax);
            parms[9] = SqlHelper.GetParameter("@Post", Model.Post);
            parms[10] = SqlHelper.GetParameter("@HomePage", Model.HomePage);
            parms[11] = SqlHelper.GetParameter("@Email", Model.Email);
            parms[12] = SqlHelper.GetParameter("@QQ", Model.QQ);
            parms[13] = SqlHelper.GetParameter("@MSN", Model.MSN);
            parms[14] = SqlHelper.GetParameter("@IM", Model.IM);
            parms[15] = SqlHelper.GetParameter("@TradeCD", Model.TradeCD);
            parms[16] = SqlHelper.GetParameter("@Staff", Model.Staff);
            parms[17] = SqlHelper.GetParameter("@Size", Model.Size);
            parms[18] = SqlHelper.GetParameter("@Production", Model.Production);
            parms[19] = SqlHelper.GetParameter("@Sale", Model.Sale);
            parms[20] = SqlHelper.GetParameter("@Credit", Model.Credit);
            parms[21] = SqlHelper.GetParameter("@Remark", Model.Remark);
            parms[22] = SqlHelper.GetParameter("@ModifiedUserID", Model.ModifiedUserID);
            SqlHelper.ExecuteTransSql(sql.ToString(),parms);

            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }

        /// <summary>
        /// 修改公司信息
        /// </summary>
        /// <param name="Model">Company实体</param>
        /// <returns></returns>
        public static bool ModifyCompany(CompanyModel Model)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("update pubdba.company set");
            sql.AppendLine("[CompanyNameCn] = @CompanyNameCn,");
            sql.AppendLine("[CompanyNameEn] = @CompanyNameEn,");
            sql.AppendLine("[ProvCD] = @ProvCD,[CityCD] = @CityCD,");
            sql.AppendLine("[Addr] = @Addr,[Contact] = @Contact,");
            sql.AppendLine("[Tel] = @Tel,[Fax] = @Fax,");
            sql.AppendLine("[Post] = @Post,[HomePage] = @HomePage,");
            sql.AppendLine("[Email] = @Email,[QQ] = @QQ,");
            sql.AppendLine("[MSN] = @MSN,[IM] = @IM,");
            sql.AppendLine("[TradeCD] = @TradeCD,[Staff] = @Staff,");
            sql.AppendLine("[Size] = @Size,[Production] = @Production,");
            sql.AppendLine("[Sale] = @Sale,[Credit] = @Credit,[Remark] = @Remark,");
            sql.AppendLine("[ModifiedDate] = getdate(), [ModifiedUserID] =ModifiedUserID  where CompanyCD=@CompanyCD");
            SqlParameter[] parms = new SqlParameter[23];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD);
            parms[1] = SqlHelper.GetParameter("@CompanyNameCn", Model.CompanyNameCn);
            parms[2] = SqlHelper.GetParameter("@CompanyNameEn", Model.CompanyNameEn);
            parms[3] = SqlHelper.GetParameter("@ProvCD", Model.ProvCD);
            parms[4] = SqlHelper.GetParameter("@CityCD", Model.CityCD);
            parms[5] = SqlHelper.GetParameter("@Addr", Model.Addr);
            parms[6] = SqlHelper.GetParameter("@Contact", Model.Contact);
            parms[7] = SqlHelper.GetParameter("@Tel", Model.Tel);
            parms[8] = SqlHelper.GetParameter("@Fax", Model.Fax);
            parms[9] = SqlHelper.GetParameter("@Post", Model.Post);
            parms[10] = SqlHelper.GetParameter("@HomePage", Model.HomePage);
            parms[11] = SqlHelper.GetParameter("@Email", Model.Email);
            parms[12] = SqlHelper.GetParameter("@QQ", Model.QQ);
            parms[13] = SqlHelper.GetParameter("@MSN", Model.MSN);
            parms[14] = SqlHelper.GetParameter("@IM", Model.IM);
            parms[15] = SqlHelper.GetParameter("@TradeCD", Model.TradeCD);
            parms[16] = SqlHelper.GetParameter("@Staff", Model.Staff);
            parms[17] = SqlHelper.GetParameter("@Size", Model.Size);
            parms[18] = SqlHelper.GetParameter("@Production", Model.Production);
            parms[19] = SqlHelper.GetParameter("@Sale", Model.Sale);
            parms[20] = SqlHelper.GetParameter("@Credit", Model.Credit);
            parms[21] = SqlHelper.GetParameter("@Remark", Model.Remark);
            parms[22] = SqlHelper.GetParameter("@ModifiedUserID", Model.ModifiedUserID);
            SqlHelper.ExecuteTransSql(sql.ToString(), parms);

            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }

        /// <summary>
        /// 判断公司编码是否存在
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns>是否存在</returns>
        public static bool IsExist(string CompanyCD)
        {
            string sql = "select CompanyCD from pubdba.company where CompanyCD=@CompanyCD ";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            int result=SqlHelper.ExecuteSql(sql, parms).Rows.Count;
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <returns></returns>
        public static bool DelCompany(string CompanyCD)
        {
            //string sql = "select ID,ModuleID,FunctionID,FunctionCD,FunctionName " +
            //            " from ModuleFunction where ModuleID  in('" + ModuleID.Replace(",", "','") + "') ";
            string sql = "Delete from pubdba.company where CompanyCD in('" + CompanyCD.Replace(",", "','") + "') ";
           // string sql = "Delete from company where CompanyCD=@CompanyCD";
            //SqlParameter[] parms = new SqlParameter[1];
            //parms[0] = SqlHelper.GetParameter("@Company", CompanyCD);
            SqlHelper.ExecuteSql(sql);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }

        /// <summary>
        /// 根据公司编码查询公司信息
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetCompanyByCD(string StrWhere)
        {
            string sql = "select * from  pubdba.company where  " + StrWhere + " ";
            return SqlHelper.ExecuteSql(sql); 
        }
        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCompanyInfo()
        {
            string sql = "select * from  pubdba.company";
            return  SqlHelper.ExecuteSql(sql);
        }
    }
}
