/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/10
 * 描    述： 公共分类列表
 * 修改日期： 2009/03/10
 * 版    本： 0.5.0
 ***********************************************/
using System.Data;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using XBase.Common;

namespace XBase.Data.Common
{
    /// <summary>
    /// 类名：CodePublicTypeDBHelper
    /// 描述：公共分类列表选择的数据处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/10
    /// 最后修改时间：2009/03/10
    /// </summary>
    ///
    public class CodePublicTypeDBHelper
    {

        #region 公共下拉列表选择查询数据

        /// <summary>
        /// 公共下拉列表选择查询数据
        /// </summary>
        /// <param name="companyCD">公司编码</param>
        /// <param name="codeFlag">分类标识</param>
        /// <param name="typeCode">分类编码</param>
        /// <returns></returns>
        public static DataTable GetCodeTypeInfoForDrp(string companyCD, string codeFlag, string typeCode)
        {
            //定义查询SQL变量
            StringBuilder searchSql = new StringBuilder();
            //定义查询语句
            searchSql.AppendLine("SELECT ID                     ");
            searchSql.AppendLine("		,CompanyCD              ");
            searchSql.AppendLine("		,TypeFlag               ");
            searchSql.AppendLine("		,TypeCode               ");
            searchSql.AppendLine("		,TypeName               ");
            searchSql.AppendLine("FROM                          ");
            searchSql.AppendLine("	  officedba.CodePublicType  ");
            searchSql.AppendLine("WHERE                         ");
            searchSql.AppendLine("	CompanyCD = @CompanyCD      ");
            searchSql.AppendLine("	AND TypeFlag = @TypeFlag    ");
            searchSql.AppendLine("	AND TypeCode = @TypeCode    ");
            searchSql.AppendLine("	AND UsedStatus = @UsedStatus");
            //设置参数
            SqlParameter[] param = new SqlParameter[4];
            int i = 0;
            //公司代码
            param[i++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //分类标识
            param[i++] = SqlHelper.GetParameter("@TypeFlag", codeFlag);
            //分类编码
            param[i++] = SqlHelper.GetParameter("@TypeCode", typeCode);
            //启用状态
            param[i++] = SqlHelper.GetParameter("@UsedStatus", ConstUtil.USED_STATUS_ON);

            //执行查询并返回的查询到的分类信息
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion
        
        #region 根据分类标识分类编码获取分类名称

        /// <summary>
        /// 根据分类标识分类编码获取分类名称
        /// </summary>
        /// <param name="companyCD">公司编码</param>
        /// <param name="codeFlag">分类标识</param>
        /// <param name="typeCode">分类编码</param>
        /// <returns></returns>
        public static string GetNameFromFlagCode(string companyCD, string codeFlag, string typeCode)
        {
            //定义查询SQL变量
            StringBuilder searchSql = new StringBuilder();
            //定义查询语句
            searchSql.AppendLine("SELECT ID                     ");
            searchSql.AppendLine("		,CompanyCD              ");
            searchSql.AppendLine("		,TypeFlag               ");
            searchSql.AppendLine("		,TypeCode               ");
            searchSql.AppendLine("		,TypeName               ");
            searchSql.AppendLine("FROM                          ");
            searchSql.AppendLine("	  officedba.CodePublicType  ");
            searchSql.AppendLine("WHERE                         ");
            searchSql.AppendLine("	CompanyCD = @CompanyCD      ");
            searchSql.AppendLine("	AND TypeFlag = @TypeFlag    ");
            searchSql.AppendLine("	AND TypeCode = @TypeCode    ");
            //设置参数
            SqlParameter[] param = new SqlParameter[3];
            int i = 0;
            //公司代码
            param[i++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //分类标识
            param[i++] = SqlHelper.GetParameter("@TypeFlag", codeFlag);
            //分类编码
            param[i++] = SqlHelper.GetParameter("@TypeCode", typeCode);
            
            //执行查询并返回的查询到的分类信息
            DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), param);
            //如果数据不存在，则返回null
            if (data == null || data.Rows.Count < 1)
            {
                return null;
            }
            //如果数据存在，则返回对应的值
            else
            {
                return data.Rows[0]["TypeName"].ToString();
            }
        }
        #endregion

        #region 根据ID获取分类名称

        /// <summary>
        /// 获取分类名称
        /// 主键ID
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public static string GetNameFromID(string ID)
        {
            //定义查询SQL变量
            StringBuilder searchSql = new StringBuilder();
            //定义查询语句
            searchSql.AppendLine("SELECT ID                     ");
            searchSql.AppendLine("		,CompanyCD              ");
            searchSql.AppendLine("		,TypeFlag               ");
            searchSql.AppendLine("		,TypeCode               ");
            searchSql.AppendLine("		,TypeName               ");
            searchSql.AppendLine("FROM                          ");
            searchSql.AppendLine("	  officedba.CodePublicType  ");
            searchSql.AppendLine("WHERE                         ");
            searchSql.AppendLine("	CompanyCD = @ID      ");
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //主键ID
            param[0] = SqlHelper.GetParameter("@ID", ID);

            //执行查询并返回的查询到的分类信息
            DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), param);
            //如果数据不存在，则返回null
            if (data == null || data.Rows.Count < 1)
            {
                return null;
            }
            //如果数据存在，则返回对应的值
            else
            {
                return data.Rows[0]["TypeName"].ToString();
            }
        }
        #endregion

    }
}
