/**********************************************
 * 类作用：   客户公司开通业务
 * 建立人：   吴志强
 * 建立时间： 2009/01/21
 ***********************************************/
using System.Data;
using XBase.Model.SystemManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System;

namespace XBase.Data.SystemManager
{
    /// <summary>
    /// 类名：CompanyOpenServDBHelper
    /// 描述：公司业务开通数据层处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/01/21
    /// 最后修改时间：2009/01/21
    /// </summary>
    ///
    public class CompanyOpenServDBHelper
    {
        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>客户公司信息</returns>
        public static CompanyOpenServModel GetCompanyOpenServInfo(string companyCD)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("	SELECT CompanyCD	");
            sql.AppendLine("	      ,MaxRoles	");
            sql.AppendLine("	      ,MaxUers	");
            sql.AppendLine("	      ,MaxDocSize	");
            sql.AppendLine("	      ,SingleDocSize	");
            sql.AppendLine("	      ,MaxDocNum	");
            sql.AppendLine("	      ,OpenDate	");
            sql.AppendLine("	      ,CloseDate	");
            sql.AppendLine("	      ,ModifiedDate	");
            sql.AppendLine("	      ,ModifiedUserID	");
            sql.AppendLine("	      ,remark,LogoImg	");
            sql.AppendLine("	FROM pubdba.companyOpenServ	");
            sql.AppendLine("	WHERE	");
            sql.AppendLine("	      CompanyCD = @CompanyCD	");
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            DataTable companyData = SqlHelper.ExecuteSql(sql.ToString(), param);
            if (companyData != null && companyData.Rows.Count > 0)
            {
                CompanyOpenServModel model = new CompanyOpenServModel();
                //公司代码
                model.CompanyCD = (string)companyData.Rows[0]["CompanyCD"];
                //最大角色数
                model.MaxRoles = companyData.Rows[0]["MaxRoles"].ToString();
                //最大用户数
                model.MaxUers = companyData.Rows[0]["MaxUers"].ToString();
                //文件总大小
                model.MaxDocSize = companyData.Rows[0]["MaxDocSize"].ToString();
                //单个文件最大大小
                model.SingleDocSize = companyData.Rows[0]["SingleDocSize"].ToString();
                //最大文件个数
                model.MaxDocNum = companyData.Rows[0]["MaxDocNum"].ToString();
                model.LogoImg = companyData.Rows[0]["LogoImg"].ToString();
                //开始日期
                string openDate = companyData.Rows[0]["OpenDate"].ToString();
                if (!string.IsNullOrEmpty(openDate) && openDate.Length == 8)
                {
                    openDate = openDate.Substring(0, 4) + "-" + openDate.Substring(4, 2) + "-" + openDate.Substring(6);
                }
                model.OpenDate = openDate;
                //结束日期
                string closeDate = (string)companyData.Rows[0]["CloseDate"];
                if (!string.IsNullOrEmpty(closeDate) && closeDate.Length == 8)
                {
                    closeDate = closeDate.Substring(0, 4) + "-" + closeDate.Substring(4, 2) + "-" + closeDate.Substring(6);
                }
                model.CloseDate = closeDate;
                //备注
                model.Remark = (string)companyData.Rows[0]["remark"];

                return model;
            }
            return null;
        }

        /// <summary>
        /// 客户公司信息更新或者插入
        /// </summary>
        /// <param name="model">公司信息</param>
        /// <returns>更新成功与否</returns>
        public static bool ModifyCompanyOpenServInfo(CompanyOpenServModel model)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            //追加的场合
            if (model.IsInsert)
            {
                sql.AppendLine("	INSERT INTO pubdba.companyOpenServ	");
                sql.AppendLine("	       (CompanyCD	");
                sql.AppendLine("	       ,MaxRoles	");
                sql.AppendLine("	       ,MaxUers	");
                sql.AppendLine("	       ,MaxDocSize	");
                sql.AppendLine("	       ,SingleDocSize	");
                sql.AppendLine("	       ,MaxDocNum	");
                sql.AppendLine("	       ,OpenDate	");
                sql.AppendLine("	       ,CloseDate	");
                sql.AppendLine("	       ,ModifiedDate	");
                sql.AppendLine("	       ,ModifiedUserID	");
                sql.AppendLine("	       ,remark)	");
                sql.AppendLine("	VALUES	");
                sql.AppendLine("	       (@CompanyCD	");
                sql.AppendLine("	       ,@MaxRoles	");
                sql.AppendLine("	       ,@MaxUers	");
                sql.AppendLine("	       ,@MaxDocSize	");
                sql.AppendLine("	       ,@SingleDocSize	");
                sql.AppendLine("	       ,@MaxDocNum	");
                sql.AppendLine("	       ,@OpenDate	");
                sql.AppendLine("	       ,@CloseDate	");
                sql.AppendLine("	       ,@ModifiedDate	");
                sql.AppendLine("	       ,@ModifiedUserID	");
                sql.AppendLine("	       ,@remark)	");
            }
            //更新的场合
            else
            {
                sql.AppendLine("	UPDATE pubdba.companyOpenServ SET 	");
                sql.AppendLine("	MaxRoles = @MaxRoles	");
                sql.AppendLine("	,MaxUers = @MaxUers	");
                sql.AppendLine("	,MaxDocSize = @MaxDocSize	");
                sql.AppendLine("	,SingleDocSize = @SingleDocSize	");
                sql.AppendLine("	,MaxDocNum = @MaxDocNum	");
                sql.AppendLine("	,OpenDate = @OpenDate	");
                sql.AppendLine("	,CloseDate = @CloseDate	");
                sql.AppendLine("	,ModifiedDate = @ModifiedDate	");
                sql.AppendLine("	,ModifiedUserID = @ModifiedUserID	");
                sql.AppendLine("	,remark = @remark	");
                sql.AppendLine("	WHERE 	");
                sql.AppendLine("	CompanyCD = @CompanyCD	");
            }
            //设置参数
            SqlParameter[] param = new SqlParameter[11];
            //客户代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            //最大角色数，如果未输入，则登陆DBNull而不登陆0
            if (string.IsNullOrEmpty(model.MaxRoles))
            {
                param[1] = new SqlParameter("@MaxRoles", DBNull.Value);
            }
            else
            {
                param[1] = new SqlParameter("@MaxRoles", int.Parse(model.MaxRoles));
            }
            //最大用户数，如果未输入，则登陆DBNull而不登陆0
            if (string.IsNullOrEmpty(model.MaxUers))
            {
                param[2] = new SqlParameter("@MaxUers", DBNull.Value);
            }
            else
            {
                param[2] = new SqlParameter("@MaxUers", int.Parse(model.MaxUers));
            }
            //文档大小上限数，如果未输入，则登陆DBNull而不登陆0
            if (string.IsNullOrEmpty(model.MaxDocSize))
            {
                param[3] = new SqlParameter("@MaxDocSize", DBNull.Value);
            }
            else
            {
                param[3] = new SqlParameter("@MaxDocSize", int.Parse(model.MaxDocSize));
            }
            //单个文档大小上限，如果未输入，则登陆DBNull而不登陆0          
            if (string.IsNullOrEmpty(model.SingleDocSize))
            {
                param[4] = new SqlParameter("@SingleDocSize", DBNull.Value);
            }
            else
            {
                param[4] = new SqlParameter("@SingleDocSize", int.Parse(model.SingleDocSize));
            }
            //文件个数上限，如果未输入，则登陆DBNull而不登陆0
            if (string.IsNullOrEmpty(model.MaxDocNum))
            {
                param[5] = new SqlParameter("@MaxDocNum", DBNull.Value);
            }
            else
            {
                param[5] = new SqlParameter("@MaxDocNum", int.Parse(model.MaxDocNum));
            }
            //客户公司业务开通日期
            param[6] = SqlHelper.GetParameter("@OpenDate", model.OpenDate);
            //客户公司业务结束日期
            param[7] = SqlHelper.GetParameter("@CloseDate", model.CloseDate);
            //最后修改日期
            param[8] = SqlHelper.GetParameter("@ModifiedDate", DateTime.Now);
            //最后修改者
            param[9] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
            //备注
            param[10] = SqlHelper.GetParameter("@remark", model.Remark);

            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }

        /// <summary>
        /// 删除客户公司信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteCompanyOpenServInfo(string companyCD)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE FROM pubdba.companyOpenServ ");
            sql.AppendLine("WHERE ");
            sql.AppendLine(" CompanyCD IN (" + companyCD + ")");

            SqlHelper.ExecuteTransSql(sql.ToString());
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }


        public static void UpdateCompanyManMsgNum(string companyCD, int count)
        {
            SqlParameter[] parameters = new SqlParameter[]{
               new SqlParameter("@companyCD", SqlDbType.Char,6),
                new SqlParameter("@ManMsgNum", SqlDbType.Int,4)
			};
            parameters[0].Value = companyCD;
            parameters[1].Value = count;

            string sql = "UPDATE [pubdba].[companyOpenServ] set ManMsgNum=@ManMsgNum  WHERE [CompanyCD]=@CompanyCD";

            SqlHelper.ExecuteSql(sql, parameters);
        }

        public static void UpdateCompanyAutoMsgNum(string companyCD, int count)
        {
            SqlParameter[] parameters = new SqlParameter[]{
               new SqlParameter("@companyCD", SqlDbType.Char,6),
                new SqlParameter("@AutoMsgNum", SqlDbType.Int,4)
			};
            parameters[0].Value = companyCD;
            parameters[1].Value = count;

            string sql = "UPDATE [pubdba].[companyOpenServ] set AutoMsgNum=@AutoMsgNum  WHERE [CompanyCD]=@CompanyCD";

            SqlHelper.ExecuteSql(sql, parameters);
        }
    }
}
