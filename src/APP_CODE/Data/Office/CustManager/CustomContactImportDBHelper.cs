using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Collections;

namespace XBase.Data.Office.CustManager
{
    public class CustomContactImportDBHelper
    {
        #region 验证对应客户是否存在
        public static string CustomExists(string companyCD, string custName)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT CustNo FROM officedba.CustInfo ");
            sbSql.AppendLine("WHERE LTRIM(RTRIM(CustName))=LTRIM(RTRIM(@CustName)) AND CompanyCD=@CompanyCD ");
            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CustName", custName);
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0]["CustNo"].ToString();
            else
                return string.Empty;
        }
        #endregion

        #region 验证联系人类型是否存在
        public static string ContactExists(string companyCD, string typeName)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("  ");
            sbSql.AppendLine("   SELECT ID FROM officedba.CodePublicType ");
            sbSql.AppendLine("  WHERE TypeFlag='4' AND TypeCode='4' ");
            sbSql.AppendLine("  AND CompanyCD=@CompanyCD AND LTRIM(RTRIM(TypeName))=LTRIM(RTRIM(@TypeName)) ");
            int index = 0;
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@TypeName", typeName);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0]["ID"].ToString();
            else
                return string.Empty;

        }
        #endregion

        #region 导入数据
        public static bool ImportContact(List<XBase.Model.Office.CustManager.LinkManModel> modeList)
        {
            if (modeList.Count < 0)
                return false;
            else
            {
                List<SqlCommand> cmdList = new List<SqlCommand>();
                foreach (XBase.Model.Office.CustManager.LinkManModel model in modeList)
                {

                    #region 构造SQL
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.AppendLine(" INSERT INTO officedba.CustLinkMan ");
                    sbSql.AppendLine(" (CompanyCD,CustNo,LinkType,LinkManName,Sex,Important,Age,Operation,Company,Position,Department,WorkTel,Fax,Handset,HomeTel,MailAddress,CreatedDate,Creator,CanViewUser) VALUES ");
                    sbSql.AppendLine(" (@CompanyCD,@CustNo,@LinkType,@LinkManName,@Sex,@Important,@Age,@Operation,@Company,@Position,@Department,@WorkTel,@Fax,@Handset,@HomeTel,@MailAddress,@CreatedDate,@Creator,@CanViewUser)");
                    SqlParameter[] sqlParams = new SqlParameter[19];
                    int index = 0;
                    sqlParams[index++]=SqlHelper.GetParameter("@CompanyCD",model.CompanyCD);
                    sqlParams[index++] = SqlHelper.GetParameter("@CustNo", model.CustNo);
                    sqlParams[index++] = SqlHelper.GetParameter("@LinkType", model.LinkType);
                    sqlParams[index++]=SqlHelper.GetParameter("@LinkManName",model.LinkManName);
                    sqlParams[index++] = SqlHelper.GetParameter("@Sex", model.Sex);
                    sqlParams[index++] = SqlHelper.GetParameter("@Important", model.Important);
                    sqlParams[index++] = SqlHelper.GetParameter("@Age", model.Age);
                    sqlParams[index++]=SqlHelper.GetParameter("@Operation",model.Operation);
                    sqlParams[index++]=SqlHelper.GetParameter("@Company",model.Company);
                    sqlParams[index++] = SqlHelper.GetParameter("@Position", model.Position);
                    sqlParams[index++] = SqlHelper.GetParameter("@Department", model.Department);
                    sqlParams[index++] = SqlHelper.GetParameter("@WorkTel", model.WorkTel);
                    sqlParams[index++] = SqlHelper.GetParameter("@Fax", model.Fax);
                    sqlParams[index++] = SqlHelper.GetParameter("@Handset", model.Handset);
                    sqlParams[index++] = SqlHelper.GetParameter("@HomeTel", model.HomeTel);
                    sqlParams[index++] = SqlHelper.GetParameter("@MailAddress", model.MailAddress);
                    sqlParams[index++] = SqlHelper.GetParameter("@CreatedDate", model.CreatedDate);
                    sqlParams[index++] = SqlHelper.GetParameter("@Creator", model.Creator);
                    sqlParams[index++] = SqlHelper.GetParameter("@CanViewUser", model.CanViewUser);
                    #endregion

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sbSql.ToString();
                    cmd.Parameters.AddRange(sqlParams);
                    cmdList.Add(cmd);
                }

                 return  SqlHelper.ExecuteTransWithCollections(cmdList);
            }
 
        }
        #endregion
    }
}
