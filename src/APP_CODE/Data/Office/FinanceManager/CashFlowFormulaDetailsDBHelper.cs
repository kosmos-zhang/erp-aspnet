/**********************************************
 * 类作用：   现金流量表计算公式明细数据库层处理
 * 建立人：   莫申林
 * 建立时间： 2009/05/26
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;

namespace XBase.Data.Office.FinanceManager
{
    public class CashFlowFormulaDetailsDBHelper
    {
        /// <summary> 
        /// 添加现金流量表公式明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="IntID"></param>
        /// <returns></returns>
        public static bool InsertCashFlowFormulaDetails(CashFlowFormulaDetailsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("insert into officedba.CashFlowFormulaDetails(");
            strSql.AppendLine("FormulaID,SubjectsCD,SubjectsName,Operator,CompanyCD,Direction,OperatorType)");
            strSql.AppendLine(" values (");
            strSql.AppendLine("@FormulaID,@SubjectsCD,@SubjectsName,@Operator,@CompanyCD,@Direction,@OperatorType)");
            SqlParameter[] parms = new SqlParameter[7];
            parms[0] = SqlHelper.GetParameter("@FormulaID", model.FormulaID);
            parms[1] = SqlHelper.GetParameter("@SubjectsCD", model.SubjectsCD);
            parms[2] = SqlHelper.GetParameter("@SubjectsName", model.SubjectsName);
            parms[3] = SqlHelper.GetParameter("@Operator", model.Operator);
            parms[4] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            parms[5] = SqlHelper.GetParameter("@Direction", model.Direction);
            parms[6] = SqlHelper.GetParameter("@OperatorType", model.OperatorType);

            SqlHelper.ExecuteTransSql(strSql.ToString(), parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        /// <summary>
        ///  修改现金流量表计算公式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateCashFlowFormulaDetails(CashFlowFormulaDetailsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.CashFlowFormulaDetails set ");
            strSql.AppendLine("FormulaID=@FormulaID,");
            strSql.AppendLine("SubjectsCD=@SubjectsCD,");
            strSql.AppendLine("SubjectsName=@SubjectsName,");
            strSql.AppendLine("Operator=@Operator,");
            strSql.AppendLine("Direction=@Direction,");
            strSql.AppendLine("OperatorType=@OperatorType,");
            strSql.AppendLine("CompanyCD=@CompanyCD");
            strSql.AppendLine(" where ID=@ID");

            SqlParameter[] parms = new SqlParameter[8];
            parms[0] = SqlHelper.GetParameter("@FormulaID", model.FormulaID);
            parms[1] = SqlHelper.GetParameter("@SubjectsCD", model.SubjectsCD);
            parms[2] = SqlHelper.GetParameter("@SubjectsName", model.SubjectsName);
            parms[3] = SqlHelper.GetParameter("@Operator", model.Operator);
            parms[4] = SqlHelper.GetParameter("@Direction", model.Direction);
            parms[5] = SqlHelper.GetParameter("@OperatorType", model.OperatorType);
            parms[6] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            parms[7] = SqlHelper.GetParameter("@ID", model.ID);

            SqlHelper.ExecuteTransSql(strSql.ToString(), parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        /// <summary>
        /// 根据查询条件查询现金流量表计算公式信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="KeyID">主键</param>
        /// <param name="companyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetCashFlowFormulaDetails(string KeyID, string companyCD, string FormulaID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select ID,FormulaID,SubjectsCD,SubjectsName,Operator,CompanyCD,Direction,OperatorType  from officedba.CashFlowFormulaDetails ");
            strSql.AppendLine(" where  CompanyCD=@CompanyCD ");

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));



            //主键
            if (!string.IsNullOrEmpty(KeyID))
            {
                strSql.AppendLine(" AND ID=@ID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", KeyID));
            }

            //外键
            if (!string.IsNullOrEmpty(FormulaID))
            {
                strSql.AppendLine(" AND FormulaID=@FormulaID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FormulaID", FormulaID));
            }


            //指定命令的SQL文
            comm.CommandText = strSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);


        }



        /// <summary>
        /// 删除现金流量表计算公式信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteCashFlowFormulaDetailsInfo(string ids)
        {
            string sql = string.Format(@"delete from officedba.CashFlowFormulaDetails where ID in ( {0} )", ids);
            SqlParameter[] parms = new SqlParameter[0];
            SqlHelper.ExecuteTransSql(sql, parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }
    }
}
