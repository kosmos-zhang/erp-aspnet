/**********************************************
 * 类作用：   公司提成设置
 * 建立人：   肖合明
 * 建立时间： 2009/09/02
 ***********************************************/
using System;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using XBase.Common;
using System.Collections;
using System.Collections.Generic;
namespace XBase.Data.Office.HumanManager
{
    /// <summary>
    /// 类名：SalaryCompanyRoyaltySetDBHelper
    /// 描述：公司提成设置
    /// 
    /// 作者：肖合明
    /// 创建时间：2009/09/02
    /// 最后修改时间：2009/09/02
    /// </summary>
    ///
    public class SalaryCompanyRoyaltySetDBHelper
    {
        #region 查询公司提成信息
        /// <summary>
        /// 查询公司提成基本信息
        /// </summary>
        /// <param name="compnayCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetInfoTable(string compnayCD, string DeptID)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT a.ID							  ");
            searchSql.AppendLine(",a.DeptID                               ");
            searchSql.AppendLine(",case a.DeptID when 0 then '默认' else b.DeptName end as DeptName ");
            //searchSql.AppendLine(",b.DeptName                             ");
            searchSql.AppendLine(",a.CompanyCD                            ");
            searchSql.AppendLine(",a.MiniMoney                            ");
            searchSql.AppendLine(",a.MaxMoney                             ");
            searchSql.AppendLine(",a.TaxPercent                           ");
            searchSql.AppendLine("FROM officedba.SalaryCompanyRoyaltySet a");
            searchSql.AppendLine(" left join officedba.DeptInfo b on a.DeptID=b.ID");
            searchSql.AppendLine(" where a.CompanyCD = @CompanyCD and a.DeptID=@DeptID ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", compnayCD));
            //分公司ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            //工资项编号

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        /// <summary>
        /// 获取分公司信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSubCompany(string CompanyCD)
        {
            string strSql = "select * from officedba.DeptInfo where CompanyCD=@CompanyCD and SaleFlag='1' and UsedStatus='1' ";
            SqlCommand comm = new SqlCommand();

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.CommandText = strSql;
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 执行保存
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool SaveInfo(string CompanyCD, string DeptID, List<SalaryCompanyRoyaltySetModel> ModelList)
        {
            ArrayList lstUpdate = new ArrayList();

            string strSqlDel = "Delete from officedba.SalaryCompanyRoyaltySet where CompanyCD=@CompanyCD and DeptID=@DeptID";
            SqlCommand commDel = new SqlCommand();
            commDel.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            commDel.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            commDel.CommandText = strSqlDel;
            //先删除所有当前分公司的所有记录
            lstUpdate.Add(commDel);

            if (ModelList != null && ModelList.Count > 0)
            {
                StringBuilder strSqlInSert = new StringBuilder();
                strSqlInSert.Append("insert into officedba.SalaryCompanyRoyaltySet(");
                strSqlInSert.Append("DeptID,CompanyCD,MiniMoney,MaxMoney,ModifiedUserID,ModifiedDate,TaxPercent)");
                strSqlInSert.Append(" values (");
                strSqlInSert.Append("@DeptID,@CompanyCD,@MiniMoney,@MaxMoney,@ModifiedUserID,getdate(),@TaxPercent)");
                strSqlInSert.Append(";select @@IDENTITY");
                foreach (SalaryCompanyRoyaltySetModel model in ModelList)
                {
                    SqlCommand commInSert = new SqlCommand();
                    commInSert.CommandText = strSqlInSert.ToString();
                    EditInsertParam(commInSert, model);
                    lstUpdate.Add(commInSert);//循环加入数组（重新获取页面上明细数据）
                }
            }
            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);
        }

        #region 明细参数设置
        /// <summary>
        /// 明细参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void EditInsertParam(SqlCommand comm, SalaryCompanyRoyaltySetModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID ", model.DeptID));//分公司ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MiniMoney ", model.MiniMoney));//业绩上限
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaxMoney ", model.MaxMoney));//业绩下限
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新人
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate ", model.ModifiedDate));//最后更新时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaxPercent ", model.TaxPercent));//提成率

        }
        #endregion
    }
}
