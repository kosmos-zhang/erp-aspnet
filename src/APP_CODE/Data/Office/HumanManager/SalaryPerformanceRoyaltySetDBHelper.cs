/**********************************************
 * 类作用：   绩效工资设置
 * 建立人：   肖合明
 * 建立时间： 2009/09/04
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
    public class SalaryPerformanceRoyaltySetDBHelper
    {
        /// <summary>
        /// 获取个人绩效工资设置信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="EmployeerID"></param>
        /// <returns></returns>
        public static DataTable GetInfoTable(string CompanyCD, string Taskflag, string EmployeeID)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT a.ID							  ");
            searchSql.AppendLine(",a.EmployeeID                               ");
            searchSql.AppendLine(",case a.EmployeeID when '0' then '默认' else b.EmployeeName end as EmployeeName    ");
            searchSql.AppendLine(",a.MiniScore                             ");
            searchSql.AppendLine(",a.MaxScore                            ");
            searchSql.AppendLine(",a.Confficent                             ");
            searchSql.AppendLine(",a.Taskflag                           ");
            searchSql.AppendLine("FROM officedba.SalaryPerformanceRoyaltySet a");
            searchSql.AppendLine(" left join officedba.EmployeeInfo b on a.EmployeeID=b.ID");
            searchSql.AppendLine(" where a.CompanyCD = @CompanyCD and a.EmployeeID=@EmployeeID and a.Taskflag=@Taskflag ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //人员ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", EmployeeID));
            //考核类型
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Taskflag", Taskflag));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        /// <summary>
        /// 执行保存
        /// </summary>
        /// <param name="p"></param>
        /// <param name="EmployeerID"></param>
        /// <param name="ModelList"></param>
        /// <returns></returns>
        public static bool SaveInfo(string CompanyCD, string EmployeeID, string Taskflag, List<SalaryPerformanceRoyaltySetModel> ModelList)
        {
            ArrayList lstUpdate = new ArrayList();

            string strSqlDel = "Delete from officedba.SalaryPerformanceRoyaltySet where CompanyCD=@CompanyCD and EmployeeID=@EmployeeID and Taskflag=@Taskflag";
            SqlCommand commDel = new SqlCommand();
            commDel.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            commDel.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", EmployeeID));
            commDel.Parameters.Add(SqlHelper.GetParameterFromString("@Taskflag", Taskflag));
            commDel.CommandText = strSqlDel;
            //先删除所有当前分公司的所有记录
            lstUpdate.Add(commDel);

            if (ModelList != null && ModelList.Count > 0)
            {
                StringBuilder strSqlInSert = new StringBuilder();

                strSqlInSert.Append("insert into officedba.SalaryPerformanceRoyaltySet(");
                strSqlInSert.Append("EmployeeID,CompanyCD,MiniScore,MaxScore,Confficent,ModifiedUserID,ModifiedDate,Taskflag)");
                strSqlInSert.Append(" values (");
                strSqlInSert.Append("@EmployeeID,@CompanyCD,@MiniScore,@MaxScore,@Confficent,@ModifiedUserID,getdate(),@Taskflag)");
                strSqlInSert.Append(";select @@IDENTITY");
                foreach (SalaryPerformanceRoyaltySetModel model in ModelList)
                {
                    SqlCommand commInSert = new SqlCommand();
                    commInSert.CommandText = strSqlInSert.ToString();
                    EditInsertParam(commInSert, model);
                    lstUpdate.Add(commInSert);//循环加入数组（重新获取页面上明细数据）
                }
            }
            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);
        }

        private static void EditInsertParam(SqlCommand comm, SalaryPerformanceRoyaltySetModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID ", model.EmployeeID));//人员ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MiniScore ", model.MiniScore));//上限
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaxScore ", model.MaxScore));//下限
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confficent ", model.Confficent));//绩效系数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Taskflag ", model.Taskflag));//考核类型
        }
    }
}
