/**********************************************
 * 类作用：   文件上传
 * 建立人：   吴志强
 * 建立时间： 2009/04/11
 ***********************************************/
using System;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;

namespace XBase.Data.Common
{
    /// <summary>
    /// 类名：UploadFileDBHelper
    /// 描述：文件上传
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/11
    /// 最后修改时间：2009/04/11
    /// </summary>
    ///
    public class UploadFileDBHelper
    {
        /// <summary>
        /// 获取公司文件上传相关信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetCompanyUploadFileInfo(string companyCD)
        {
            #region 查询公司文件相关信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                  ");
            searchSql.AppendLine(" 	 MaxDocSize            ");
            searchSql.AppendLine(" 	,SingleDocSize         ");
            searchSql.AppendLine(" 	,MaxDocNum             ");
            searchSql.AppendLine(" 	,DocSavePath           ");
            searchSql.AppendLine(" FROM                    ");
            searchSql.AppendLine(" 	pubdba.companyOpenServ ");
            searchSql.AppendLine(" WHERE                   ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
    }
}
