/*********************************
 * 模块：预算控制--分项预算概要
 * 创建人：何小武
 * 创建时间：2010-5-24
 * 功能描述：处理“分项预算概要”的插入，修改删除，获取列表信息，每条记录详细信息等功能
 * *******************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Model.Office.ProjectBudget;
using XBase.Data.DBHelper;

namespace XBase.Data.Office.ProjectBudget
{
    public class SubBudgetDBHelper
    {
        #region 添加、编辑 分项预算概要
        public static int AddSubBudgetInfo(SubBudgetModel subBudgetModel, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("insert into officedba.SubBudget(CompanyCD,projectid,BudgetName) values(@CompanyCD,@projectid,@BudgetName)");
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@projectid",SqlDbType.Int),
                                       new SqlParameter("@BudgetName",SqlDbType.VarChar,200)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = subBudgetModel.Projectid;
            param[2].Value = subBudgetModel.BudgetName;

            TransactionManager tran = new TransactionManager();
            int num = 0;
            tran.BeginTransaction();
            try
            {
                num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString(), param);
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            return num;
        }

        public static int EditSubBudget(SubBudgetModel subBudgetModel, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("update officedba.SubBudget set BudgetName=@BudgetName,projectid=@projectid where ID=@ID");
            SqlParameter[] param = {
                                       new SqlParameter("@BudgetName",SqlDbType.VarChar,200),
                                       new SqlParameter("@projectid",SqlDbType.Int,4),
                                       new SqlParameter("@ID",SqlDbType.Int)
                                   };
            param[0].Value = subBudgetModel.BudgetName;
            param[1].Value = subBudgetModel.Projectid;
            param[2].Value = subBudgetModel.ID;
            TransactionManager tran = new TransactionManager();
            int num = 0;
            tran.BeginTransaction();
            try
            {
                num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString(), param);
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            return num;
        }
        #endregion

        #region 获取分项预算概要列表
        /// <summary>
        /// 获取分项预算概要列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagecount"></param>
        /// <param name="projectid"></param>
        /// <param name="summaryname"></param>
        /// <param name="OrderBy"></param>
        /// <param name="userinfo"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetSubBudgetList(int pageindex, int pagecount, string projectid, string summaryname, string OrderBy, XBase.Common.UserInfoUtil userinfo, ref int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select a.ID,a.projectid,a.BudgetName, ");
            strSql.AppendLine(" c.ProjectName,d.status ");
            strSql.AppendLine(" from officedba.SubBudget a ");
            strSql.AppendLine(" left join officedba.projectInfo c on a.projectid=c.ID");
            strSql.AppendLine(" left join officedba.ProjectBudget d on a.ProjectID=d.ProjectID and a.companyCD=d.CompanyCD ");
            strSql.AppendLine(" where a.CompanyCD=@CompanyCD ");
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", userinfo.CompanyCD));
            if (projectid != "0")
            {
                strSql.AppendLine(" and a.projectid=@projectID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@projectID", projectid));
            }

            if (!string.IsNullOrEmpty(summaryname))
            {
                strSql.AppendLine(" and a.BudgetName like @BudgetName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BudgetName", "%"+summaryname+"%"));
            }
            comm.CommandText = strSql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageindex, pagecount, OrderBy, ref totalCount);
        }
        #endregion

        #region 删除分项预算概要 及其项目摘要，项目预算表中与该“分项预算概要”相关的单据
        /// <summary>
        /// 删除分项预算概要 及其项目摘要，项目预算表中与该“分项预算概要”相关的单据
        /// </summary>
        /// <param name="billID">删除的ID串</param>
        /// <returns></returns>
        public static int DeLSubBudgetInfo(string billID)
        {
            StringBuilder sqlstr = new StringBuilder();
            TransactionManager tran = new TransactionManager();
            int num = 0;
            string[] billIDArr = billID.Split(',');
            string str = "";
            string budgetIDstr = "-1";
            //获取要删除的项目摘要ID串
            for (int i = 0; i < billIDArr.Length; i++)
            { 
                str = "select budgetID from officedba.budgetsummary where subBudgetID=@ID ";
                SqlParameter[] paramstr = { 
                                            new SqlParameter("@ID",billIDArr[i].ToString().Trim())
                                          };
                int budgetID = Convert.ToInt32(SqlHelper.ExecuteScalar(str.ToString(), paramstr));
                budgetIDstr += ","+budgetID;
            }
            //删除与此分项预算概要相关的“项目摘要”及其“项目预算表信息”
            sqlstr.AppendLine("delete from officedba.budgetSummary where subBudgetID in (" + billID + ")");
            sqlstr.AppendLine("delete from officedba.ProjectBaseNum where SummaryID in (" + budgetIDstr + ")");
            sqlstr.AppendLine("delete from officedba.ProjectBudgetDetails where SummaryID in (" + budgetIDstr + ")");
            sqlstr.AppendLine("delete from officedba.SubBudget where ID in (" + billID + ")");

            tran.BeginTransaction();
            try
            {
                num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString());
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            return num;
        }
        #endregion

        #region 获取分项预算概要详细信息
        /// <summary>
        /// 获取分项预算概要详细信息
        /// </summary>
        /// <param name="billID">单据ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetSubBudgetInfo(string billID, string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select a.ID,a.projectid,a.BudgetName, ");
            strSql.AppendLine(" c.ProjectName ");
            strSql.AppendLine(" from officedba.SubBudget a ");
            strSql.AppendLine(" left join officedba.projectInfo c on A.projectid=c.ID");
            strSql.AppendLine(" where a.CompanyCD=@CompanyCD and a.ID=@BillID ");
            SqlParameter[] param = { 
                                    new SqlParameter("@BillID",billID),
                                    new SqlParameter("@CompanyCD",strCompanyCD)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion
    }
}
