/***********************************************************************
 * 
 * Module Name:XBase.Data.Office.SystemManager.CompanyModuleDBHelper.cs
 * Current Version: 1.0 
 * Creator: taochun
 * Auditor:2009-03-03
 * End Date:
 * Description: 企业信息管理数据库层处理
 * Version History:
 ***********************************************************************/
using System;
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using XBase.Model.Office.SystemManager;
using System.Text;
namespace XBase.Data.Office.SystemManager
{
    public class CompanyBaseInfoDBHelper
    {
        /// <summary>
        /// 根据公司代码查询公司信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetCompanyModuleInfo(string CompanyCD)
        {
            string sql = "select ID, CompanyCD,CompanyNo,SuperCompanyID,CompanyName,UsedStatus,Description,ModifiedDate,ModifiedUserID ";
            sql += "from officedba.CompanyBaseInfo";
            sql += " where CompanyCD=@CompanyCD";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            return SqlHelper.ExecuteSql(sql, parms);
        }
        public static CompanyBaseInfoModel GetCompanyUntiInfo(string CompanyCD, string CompanyNo)
        {
            string sql = "select ID, CompanyCD,CompanyNo,SuperCompanyID,CompanyName,case when UsedStatus='1'then '是'else '否'end as UsedStatus ,Description ";
            sql += "from officedba.CompanyBaseInfo";
            sql += " where CompanyCD=@CompanyCD and CompanyNo=@CompanyNo";
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            parms[1] = SqlHelper.GetParameter("@CompanyNo", CompanyNo);
            DataTable dt = SqlHelper.ExecuteSql(sql, parms);
            if (dt != null && dt.Rows.Count > 0)
            {
                CompanyBaseInfoModel CompanyBaseInfoModel = new CompanyBaseInfoModel();
                CompanyBaseInfoModel.UsedStatus = dt.Rows[0]["UsedStatus"].ToString();
                CompanyBaseInfoModel.CompanyName = dt.Rows[0]["CompanyName"].ToString();
                CompanyBaseInfoModel.Description = dt.Rows[0]["Description"].ToString();
                CompanyBaseInfoModel.SuperCompanyID = Convert.ToInt32(dt.Rows[0]["SuperCompanyID"]);
                return CompanyBaseInfoModel;
            }
            return null;
        }
        /// <summary>
        /// 添加企业信息
        /// </summary>
        /// <returns>bool</returns>
        public static bool InsertCompanyBaseInfo(CompanyBaseInfoModel model)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO officedba.CompanyBaseInfo");
                sql.AppendLine("		(  CompanyCD   ");
                sql.AppendLine("		,CompanyNo         ");
                sql.AppendLine("		,SuperCompanyID         ");
                sql.AppendLine("		,CompanyName         ");
                sql.AppendLine("		,UsedStatus         ");
                sql.AppendLine("		,Description         ");
                sql.AppendLine("		,ModifiedUserID )        ");
                sql.AppendLine("VALUES                  ");
                sql.AppendLine("		(@CompanyCD     ");
                sql.AppendLine("		,@CompanyNo        ");
                sql.AppendLine("		,@SuperCompanyID        ");
                sql.AppendLine("		,@CompanyName        ");
                sql.AppendLine("		,@UsedStatus        ");
                sql.AppendLine("		,@Description        ");
                sql.AppendLine("     ,@ModifiedUserID)");
                //设置参数
                SqlParameter[] param = new SqlParameter[7];
                param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
                param[1] = SqlHelper.GetParameter("@CompanyNo", model.CompanyNo);
                param[2] = SqlHelper.GetParameter("@SuperCompanyID", model.SuperCompanyID);
                param[3] = SqlHelper.GetParameter("@CompanyName", model.CompanyName);
                param[4] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
                param[5] = SqlHelper.GetParameter("@Description", model.Description);
                param[6] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                bool flag = SqlHelper.Result.OprateCount > 0 ? true : false;
                return flag;
            }
            //SQL拼写

            catch
            {
                return false;
            }


        }

    }
}
