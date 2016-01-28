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
  public class SalaryDepatmentRoyaltySetDBHelper
    {
        public static DataTable SearchInsuPersonalTaxInfo(string companyCD,string DeptID)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                   ");
            searchSql.AppendLine(" 	  a.ID                                   ");
            searchSql.AppendLine(" 	 ,a.DeptID                               ");
            searchSql.AppendLine(",case a.DeptID when 0 then '默认' else b.DeptName end as DeptName ");
            searchSql.AppendLine(" 	,a.MiniMoney                            ");
            searchSql.AppendLine(" 	,a.MaxMoney                             ");
            searchSql.AppendLine(" 	,a.TaxPercent                           ");
            searchSql.AppendLine(" FROM                                      ");
            searchSql.AppendLine(" 	officedba.SalaryDepatmentRoyaltySet a    ");
            searchSql.AppendLine(" left join	officedba.DeptInfo b on a.DeptID=b.ID  ");
            searchSql.AppendLine(" WHERE                   ");
            searchSql.AppendLine(" 	a.CompanyCD = @CompanyCD and a.DeptID=@DeptID");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));


            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        public static bool UpdateIsuPersonalTaxInfo(string CompanyCD,string DeptID, IList<SalaryDepatmentRoyaltySetModel> modeList)
        {
            if (!DeletePersonalTaxInfo(CompanyCD,DeptID))
            {
                return false;
            }
            bool isSucc = false;
            foreach (SalaryDepatmentRoyaltySetModel model in modeList)
            {
                #region 插入SQL拼写
                StringBuilder insertSql = new StringBuilder();
                insertSql.AppendLine("insert into  officedba.SalaryDepatmentRoyaltySet(CompanyCD,DeptID,MiniMoney,MaxMoney,TaxPercent,ModifiedUserID,ModifiedDate) ");
                insertSql.AppendLine("          values(@CompanyCD,@DeptID,@MiniMoney,@MaxMoney,@TaxPercent,@ModifiedUserID,@ModifiedDate) ");
                #endregion
                //定义插入基本信息的命令
                SqlCommand comm = new SqlCommand();
                comm.CommandText = insertSql.ToString();
                //设置保存的参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));	//创建人
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@MiniMoney ", model.MiniMoney));	//启用状态
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaxMoney", model.MaxMoney));	//启用状态
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaxPercent", model.TaxPercent));	//启用状态
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", System.DateTime.Now.ToString()));
                //添加返回参数
                //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

                //执行插入操作
                isSucc = SqlHelper.ExecuteTransWithCommand(comm);
                if (!isSucc)
                {
                    isSucc = false;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return true;

        }

        public static bool DeletePersonalTaxInfo(string CompanyCD,string DeptID)
        {
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine("Delete from officedba.SalaryDepatmentRoyaltySet where CompanyCD=@CompanyCD and DeptID=@DeptID");

            //定义插入基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));	//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));	//公司代码
            //添加返回参数
            //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

            //执行插入操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
            return isSucc;


        }
    }
}
