/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/11
 * 描    述： 编码规则
 * 修改日期： 2009/03/11
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
    /// 类名：ItemCodingRuleDBHelper
    /// 描述：编码规则
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/11
    /// 最后修改时间：2009/03/11
    /// </summary>
    ///
    public class ItemCodingRuleDBHelper
    {

        #region 编码规则下拉列表查询数据

        /// <summary>
        /// 编码规则下拉列表查询数据
        /// </summary>
        /// <param name="companyCD">公司编码</param>
        /// <param name="codingType">编码类型</param>
        /// <param name="itemTypeID">单据代码或基础数据代码</param>
        /// <returns></returns>
        public static DataTable GetCodingRuleInfoForDrp(string companyCD, string codingType, string itemTypeID)
        {
            //定义查询SQL变量
            StringBuilder searchSql = new StringBuilder();
            //定义查询语句
            searchSql.AppendLine("SELECT ID                     ");
            searchSql.AppendLine("		,CompanyCD              ");
            searchSql.AppendLine("		,CodingType             ");
            searchSql.AppendLine("		,ItemTypeID             ");
            searchSql.AppendLine("		,RuleName               ");
            searchSql.AppendLine("      ,IsDefault              ");
            searchSql.AppendLine("FROM                          ");
            searchSql.AppendLine("	  officedba.ItemCodingRule  ");
            searchSql.AppendLine("WHERE                         ");
            searchSql.AppendLine("	CompanyCD = @CompanyCD      ");
            searchSql.AppendLine("	AND CodingType = @CodingType");
            searchSql.AppendLine("	AND ItemTypeID = @ItemTypeID");
            searchSql.AppendLine("	AND UsedStatus = @UsedStatus");
            //设置参数
            SqlParameter[] param = new SqlParameter[4];
            int i = 0;
            //公司代码
            param[i++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //编码类型
            param[i++] = SqlHelper.GetParameter("@CodingType", codingType);
            //单据代码或基础数据代码
            param[i++] = SqlHelper.GetParameter("@ItemTypeID", itemTypeID);
            //启用状态
            param[i++] = SqlHelper.GetParameter("@UsedStatus", ConstUtil.USED_STATUS_ON);

            //执行查询并返回的查询到的分类信息
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 根据编码规则ID获取编码规则数据

        /// <summary>
        /// 根据编码规则ID获取编码规则数据
        /// </summary>
        /// <param name="codeID">编码规则ID</param>
        /// <returns></returns>
        public static DataTable GetCodingRuleInfoWithID(string codeID)
        {
            //定义查询SQL变量
            StringBuilder searchSql = new StringBuilder();
            //定义查询语句
            searchSql.AppendLine(" SELECT ID           ");
            searchSql.AppendLine("      ,CompanyCD     ");
            searchSql.AppendLine("      ,CodingType    ");
            searchSql.AppendLine("      ,ItemTypeID    ");
            searchSql.AppendLine("      ,RuleName      ");
            searchSql.AppendLine("      ,RulePrefix    ");
            searchSql.AppendLine("      ,RuleDateType  ");
            searchSql.AppendLine("      ,RuleNoLen     ");
            searchSql.AppendLine("      ,LastNo        ");
            searchSql.AppendLine("      ,RuleExample   ");
            searchSql.AppendLine("      ,IsDefault     ");
            searchSql.AppendLine("      ,Remark        ");
            searchSql.AppendLine("      ,UsedStatus    ");
            searchSql.AppendLine("      ,ModifiedDate  ");
            searchSql.AppendLine("      ,ModifiedUserID");
            searchSql.AppendLine(" FROM                ");
            searchSql.AppendLine(" officedba.ItemCodingRule  ");
            searchSql.AppendLine("WHERE                ");
            searchSql.AppendLine("	ID = @CodeID       ");
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //编码规则ID
            param[0] = SqlHelper.GetParameter("@CodeID", codeID);

            //执行查询并返回的查询到的分类信息
            DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), param);
            //数据存在时，更新当前流水号最大值
            bool updateFlag = false; //更新成功标识
            if (data.Rows.Count > 0)
            {
                //编码流水号长度
                int ruleNoLen = GetSafeData.ValidateDataRow_Int(data.Rows[0], "RuleNoLen");
                //定义最大流水号
                string maxNo = string.Empty;
                //最大流水号
                for (int i = 0; i < ruleNoLen; i++)
                {
                    maxNo += "9";
                }
                //获取当前流水号最大值
                int lastNo = GetSafeData.ValidateDataRow_Int(data.Rows[0], "LastNo");
                //当前最大流水号+1
                int updateNo = lastNo + 1;
                //当前最大流水号+1 大于 最大流水号时，从1开始记数
                if (updateNo > int.Parse(maxNo))
                {
                  
                    return null;
                    //updateNo = 1;
                }
                //重新设置最大流水号，生成编号不用重复计算
                data.Rows[0]["LastNo"] = updateNo;
                //更新操作
                StringBuilder updateSql = new StringBuilder();
                updateSql.AppendLine(" UPDATE officedba.ItemCodingRule ");
                updateSql.AppendLine("    SET                          ");
                updateSql.AppendLine("       LastNo = @LastNo          ");
                updateSql.AppendLine("  WHERE ID = @CodeID             ");
                //设置参数
                SqlParameter[] updatePram = new SqlParameter[2];
                //编码规则ID
                updatePram[0] = SqlHelper.GetParameter("@CodeID", codeID);
                //最大流水号
                updatePram[1] = SqlHelper.GetParameter("@LastNo", updateNo);
                //执行更新
                updateFlag = SqlHelper.ExecuteTransSql(updateSql.ToString(), updatePram) > 0 ? true:false;
            }

            //返回获取的数据
            if (updateFlag)
            {
                return data;
            }
            return null;
        }
        #endregion

    }
}
