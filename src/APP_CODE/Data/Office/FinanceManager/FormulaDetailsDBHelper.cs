/**********************************************
 * 类作用：   资产负债表计算公式明细数据库层处理
 * 建立人：   莫申林
 * 建立时间： 2009/05/22
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;

namespace XBase.Data.Office.FinanceManager
{
    public class FormulaDetailsDBHelper
    {
        /// <summary>
        /// 添加资产负债表公式明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="IntID"></param>
        /// <returns></returns>
        public static bool InsertFormulaDetails(FormulaDetailsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("insert into officedba.FormulaDetails(");
            strSql.AppendLine("FormulaID,SubjectsCD,SubjectsName,Operator,Direction,CompanyCD)");
            strSql.AppendLine(" values (");
            strSql.AppendLine("@FormulaID,@SubjectsCD,@SubjectsName,@Operator,@Direction,@CompanyCD)");
            SqlParameter[] parms = new SqlParameter[6];
            parms[0] = SqlHelper.GetParameter("@FormulaID", model.FormulaID);
            parms[1] = SqlHelper.GetParameter("@SubjectsCD", model.SubjectsCD);
            parms[2] = SqlHelper.GetParameter("@SubjectsName", model.SubjectsName);
            parms[3] = SqlHelper.GetParameter("@Operator", model.Operator);
            parms[4] = SqlHelper.GetParameter("@Direction", model.Direction);
            parms[5] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);

            SqlHelper.ExecuteTransSql(strSql.ToString(), parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        /// <summary>
        ///  修改资产负债表计算公式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateFormulaDetails(FormulaDetailsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.FormulaDetails set ");
            strSql.AppendLine("FormulaID=@FormulaID,");
            strSql.AppendLine("SubjectsCD=@SubjectsCD,");
            strSql.AppendLine("SubjectsName=@SubjectsName,");
            strSql.AppendLine("Operator=@Operator,");
            strSql.AppendLine("Direction=@Direction,");
            strSql.AppendLine(" where ID=@ID");

            SqlParameter[] parms = new SqlParameter[6];
            parms[0] = SqlHelper.GetParameter("@FormulaID", model.FormulaID);
            parms[1] = SqlHelper.GetParameter("@SubjectsCD", model.SubjectsCD);
            parms[2] = SqlHelper.GetParameter("@SubjectsName", model.SubjectsName);
            parms[3] = SqlHelper.GetParameter("@Operator", model.Operator);
            parms[4] = SqlHelper.GetParameter("@Direction", model.Direction);
            parms[5] = SqlHelper.GetParameter("@ID", model.ID);

            SqlHelper.ExecuteTransSql(strSql.ToString(), parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        /// <summary>
        /// 根据查询条件查询资产负债表计算公式信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="KeyID">主键</param>
        /// <param name="companyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetBalanceFormulaDetails(string KeyID, string companyCD, string FormulaID)
        {
            string sql="select ID,FormulaID,SubjectsCD,SubjectsName,Operator,Direction  from officedba.FormulaDetails {0} ";
            string QueryStr = " where CompanyCD='"+companyCD+"' ";

            //主键
            if (!string.IsNullOrEmpty(KeyID))
            {
                QueryStr+=" AND ID="+KeyID+" ";
                
            }
            //外键
            if (!string.IsNullOrEmpty(FormulaID))
            {

                QueryStr+=" AND FormulaID="+FormulaID+" ";
            }
           string QSql=string.Format(sql,QueryStr);
           return SqlHelper.ExecuteSql(QSql);

            
        }

       

        /// <summary>
        /// 删除资产负债表计算公式信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteFormulaDetailsInfo(string ids)
        {
            string sql = string.Format(@"delete from officedba.FormulaDetails where ID in ( {0} )", ids);
            SqlParameter[] parms = new SqlParameter[0];
            SqlHelper.ExecuteTransSql(sql, parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }
    }
}
