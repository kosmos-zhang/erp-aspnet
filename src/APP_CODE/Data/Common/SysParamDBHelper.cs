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

namespace XBase.Data.Common
{
    /// <summary>
    /// 类名：SysParamDBHelper
    /// 描述：公共分类列表选择的数据处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/10
    /// 最后修改时间：2009/03/10
    /// </summary>
    ///
    public class SysParamDBHelper
    {

        #region 公共下拉列表选择查询数据

        /// <summary>
        /// 公共下拉列表选择查询数据
        /// </summary>
        /// <param name="type">参数类型</param>
        /// <param name="number">参数编号</param>
        /// <returns></returns>
        public static DataTable GetSysParamInfoForDrp(string type, string number)
        {
            //定义查询SQL变量
            StringBuilder searchSql = new StringBuilder();
            //定义查询语句
            searchSql.AppendLine("SELECT ID                     ");
            searchSql.AppendLine("		,IndexType              ");
            searchSql.AppendLine("		,IndexNum               ");
            searchSql.AppendLine("		,IndexCode              ");
            searchSql.AppendLine("		,IndexValue             ");
            searchSql.AppendLine("		,remark                 ");
            searchSql.AppendLine("FROM                          ");
            searchSql.AppendLine("	  pubdba.SysParam           ");
            searchSql.AppendLine("WHERE                         ");
            searchSql.AppendLine("	IndexType = @IndexType      ");
            searchSql.AppendLine("	AND IndexNum = @IndexNum    ");
            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            int i = 0;
            //参数类型
            param[i++] = SqlHelper.GetParameter("@IndexType", type);
            //参数编号
            param[i++] = SqlHelper.GetParameter("@IndexNum", number);

            //执行查询并返回的查询到的分类信息
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 通过参数类型、参数编号以及参数编码获取参数值
        /// <summary>
        /// 通过参数类型、参数编号以及参数编码获取参数值
        /// </summary>
        /// <param name="type">参数类型</param>
        /// <param name="number">参数编号</param>
        /// <param name="code">参数编码</param>
        /// <returns></returns>
        public static string GetNameFromTypeNumCode(string type, string number, string code)
        {
            //定义查询SQL变量
            StringBuilder searchSql = new StringBuilder();
            //定义查询语句
            searchSql.AppendLine("SELECT ID                     ");
            searchSql.AppendLine("		,IndexType              ");
            searchSql.AppendLine("		,IndexNum               ");
            searchSql.AppendLine("		,IndexCode              ");
            searchSql.AppendLine("		,IndexValue             ");
            searchSql.AppendLine("		,remark                 ");
            searchSql.AppendLine("FROM                          ");
            searchSql.AppendLine("	  pubdba.SysParam           ");
            searchSql.AppendLine("WHERE                         ");
            searchSql.AppendLine("	IndexType = @IndexType      ");
            searchSql.AppendLine("	AND IndexNum = @IndexNum    ");
            searchSql.AppendLine("	AND IndexCode = @IndexCode  ");
            //设置参数
            SqlParameter[] param = new SqlParameter[3];
            int i = 0;
            //参数类型
            param[i++] = SqlHelper.GetParameter("@IndexType", type);
            //参数编号
            param[i++] = SqlHelper.GetParameter("@IndexNum", number);
            //参数编码
            param[i++] = SqlHelper.GetParameter("@IndexCode", code);

            //执行查询并返回的查询到的分类信息
            DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), param);
            //如果数据不存在则返回null
            if (data == null || data.Rows.Count < 1)
            {
                return null;
            }
            //如果数据存在,则返回对应的值
            else
            {
                return data.Rows[0]["IndexValue"].ToString();
            }
        }
        #endregion

    }
}
