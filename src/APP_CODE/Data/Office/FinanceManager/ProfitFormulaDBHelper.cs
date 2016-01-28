/**********************************************
 * 类作用：   利润表计算公式数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/05/25
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
namespace XBase.Data.Office.FinanceManager
{
    public class ProfitFormulaDBHelper
    {
        #region singleton
        private static ProfitFormulaDBHelper _Instance=null;
        public static ProfitFormulaDBHelper GetInstance()
        {
            if (_Instance == null)
            {
                lock (typeof(ProfitFormulaDBHelper))
                {
                    if (_Instance == null)
                    {
                        _Instance = new ProfitFormulaDBHelper();
                    }
                }
            }
            return _Instance;
        }
        #endregion


       #region 获取利润表项目信息
       public  DataTable GetProfitFormulaInfo()
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select ID,Name,Line from officedba.ProfitFormula ");
           return SqlHelper.ExecuteSql(sql.ToString());
       }
       #endregion

       #region 删除利润表明细信息
       public bool DeleteProfitFormulaInfo(string ID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Delete from officedba.ProfitFormulaDetails where ID in("+ID+") ");

           SqlCommand cmd = new SqlCommand();
           cmd.CommandText = sql.ToString();


         return   SqlHelper.ExecuteTransWithCommand(cmd);
       }
       #endregion

       #region 添加利润公式明细信息
       public  bool InsertProfitFormulaDetails(string CompanyCD,
           int FormulaID, string SubjectsCD, string SubjectsName, string Direction, string Operator)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Insert into officedba.ProfitFormulaDetails");
           sql.AppendLine("(CompanyCD,FormulaID,SubjectsCD,SubjectsName,");
           sql.AppendLine("Direction,Operator)");
           sql.AppendLine("values(@CompanyCD,@FormulaID,@SubjectsCD,@SubjectsName,");
           sql.AppendLine("@Direction,@Operator)");

           SqlParameter[] parms = 
           {
                new SqlParameter("@CompanyCD",CompanyCD),
                new SqlParameter("@FormulaID",FormulaID),
                new SqlParameter("@SubjectsCD",SubjectsCD),
                new SqlParameter("@SubjectsName",SubjectsName),
                new SqlParameter("@Direction",Direction),
                new SqlParameter("@Operator",Operator)
           };

           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 修改公式明细信息
       public bool UpdateProfitFormulaDetails(string ID,
           string SubjectsCD, string SubjectsName, string Direction, string Operator)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Update officedba.ProfitFormulaDetails");
           sql.AppendLine("set SubjectsCD=@SubjectsCD,");
           sql.AppendLine("SubjectsName=@SubjectsName,");
           sql.AppendLine("Direction=@Direction,");
           sql.AppendLine("Operator=@Operator");
           sql.AppendLine("where ID=@ID");

           SqlParameter[] parms = 
           {
               new SqlParameter("@ID",ID),
               new SqlParameter("@SubjectsCD",SubjectsCD),
               new SqlParameter("@SubjectsName",SubjectsName),
               new SqlParameter("@Direction",Direction),
               new SqlParameter("@Operator",Operator)
           };

           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion

       #region 获取利润公式明细信息
       public DataTable GetProfitFormulaDetails(string CompanyCD, int FormulaID)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select ID,SubjectsCD,SubjectsName,Operator,Direction from ");
           sql.AppendLine("officedba.ProfitFormulaDetails");
           sql.AppendLine("where FormulaID=@FormulaID and ");
           sql.AppendLine("(CompanyCD=@CompanyCD) ");
           sql.AppendLine("union");
           sql.AppendLine("select ID,SubjectsCD,SubjectsName,Operator,Direction from ");
           sql.AppendLine("officedba.ProfitFormulaDetails");
           sql.AppendLine("where FormulaID=@FormulaID  and   companyCD is null  ");
           SqlParameter[] parms = 
           {
               new SqlParameter("@CompanyCD",CompanyCD),
               new SqlParameter("@FormulaID",FormulaID)
           };
           return SqlHelper.ExecuteSql(sql.ToString(), parms);
       }
       #endregion
    }
}
