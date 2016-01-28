/***********************************************************************
 * 
 * Module Name:APP_CODE.Common.XBase.Data.SystemManager.SystemDBHelper
 * Current Version: 1.0 
 * Creator: jiangym
 * Auditor:2009-01-07
 * End Date:
 * Description:
 * Version History:
 ***********************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Caching;
using XBase.Data.DBHelper;
namespace XBase.Data.SystemManager
{
    public class ParmsDBHelper
    {

        /// <summary>
        /// 获取系统参数信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetPubParms()
        {
            string sql = "select ID,IndexType,IndexNum,IndexCode" +
                       ",IndexValue,remark from pubdba.SysParam";
            return SqlHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 根据企业编码获取指定企业信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCompanyParms()
        {
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.AppendLine("select [CompanyCD],[NameCn],[NameEn],[NameShort],");
            sql.AppendLine("[PYShort],[ArtiPerson],[SetupAddr],");
            sql.AppendLine("[SetupDate],[SetupMoney],[CapitalScale],");
            sql.AppendLine("[SaleroomY],[ProfitY],[TaxCD],[BusiNumber],");
            sql.AppendLine("[IsTax],[Country],ProfessionType,[Province],[City],");
            sql.AppendLine("[ContactName],[Tel],[Mobile],[email],[Fax],[QQ],");
            sql.AppendLine("[MSN],[IM],[Addr],[Post],[WebSite],[SalesMan],");
            sql.AppendLine("[Remark],[ModifiedDate],[ModifiedUserID] from pubdba.company");
            //sql.AppendLine(" where CompanyCD=@CompanyCD");
            //SqlParameter[] parms = new SqlParameter[1];
            //parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        /// <summary>
        /// 获取企业开通服务信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCompanyOpenSevParms()
        {
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.AppendLine("select [CompanyCD],[MaxRoles],[MaxUers],[MaxDocSize],[SingleDocSize],");
            sql.AppendLine("[MaxDocNum],[DocSavePath],[MaxKeywords],[MaxUserKeywords],[OpenDate],");
            sql.AppendLine("[CloseDate],[ModifiedDate],[ModifiedUserID],[remark],[LogoImg] ");
            sql.AppendLine("from pubdba.companyOpenServ ");
            //SqlParameter[] parms = new SqlParameter[1];
            //parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            return SqlHelper.ExecuteSql(sql.ToString());
        }

    }
}
