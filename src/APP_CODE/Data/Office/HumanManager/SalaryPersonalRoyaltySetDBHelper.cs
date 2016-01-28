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
   public class SalaryPersonalRoyaltySetDBHelper
    {
        public static DataTable SearchInsuPersonalTaxInfo(string companyCD, string EmpID,string CustID)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT a.ID                                           ");
            searchSql.AppendLine("      ,a.EmployeeID                                   ");
            searchSql.AppendLine("      ,isnull(a.CustID,0) CustID                                    ");
            searchSql.AppendLine("      ,a.MiniMoney                                    ");
            searchSql.AppendLine("      ,a.NewCustomerTax                               ");
            searchSql.AppendLine("      ,a.OldCustomerTax                               ");
            searchSql.AppendLine("      ,a.MaxMoney                                     ");
            searchSql.AppendLine("      ,a.ISCustomerRoyaltySet                         ");
            searchSql.AppendLine("      ,TaxPercent                               ");


            searchSql.AppendLine(",case a.EmployeeID when 0 then '默认' else b.EmployeeName end as EmployeeName ");


            searchSql.AppendLine(",case a.CustID when 0 then '默认' else c.CustName end as CustName ");

            searchSql.AppendLine(" ,isnull(b.EmployeeNo,'')EmployeeNo,isnull(c.CustNo,'')CustNo   ");
            searchSql.AppendLine("  FROM officedba .SalaryPersonalRoyaltySet a          ");
            searchSql.AppendLine("  left join  officedba .EmployeeInfo b on a.EmployeeID=b.ID          ");
            searchSql.AppendLine("  left join  officedba .CustInfo c on a.CustID=c.ID          "); 
            searchSql.AppendLine(" WHERE                                                ");
            searchSql.AppendLine(" 	a.CompanyCD = @CompanyCD and a.EmployeeID=@EmpID ");
            #endregion

            if (CustID == "0")
            {
                searchSql.AppendLine(" and (a.CustID=@CustID ) ");
            }
            else
            {
                searchSql.AppendLine("and (a.CustID=@CustID or a.CustID='"+"-"+CustID+"')");
            }

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            //if()
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmpID", EmpID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustID));


            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询6
            return SqlHelper.ExecuteSearch(comm);
        }
        public static bool UpdateIsuPersonalTaxInfo(string CompanyCD, string EmployeeID, string CustID, IList<SalaryPersonalRoyaltySetModel> ModelList)
        {
            if (!DeletePersonalTaxInfo(CompanyCD,EmployeeID,CustID))
            {
                return false;
            }
            bool isSucc = false;
            foreach (SalaryPersonalRoyaltySetModel model in ModelList)
            {
                #region 插入SQL拼写
                StringBuilder insertSql = new StringBuilder();
                insertSql.Append("insert into officedba.SalaryPersonalRoyaltySet(");
                insertSql.Append("EmployeeID,CompanyCD,MiniMoney,MaxMoney,ModifiedUserID,ModifiedDate,TaxPercent,CustID,NewCustomerTax,OldCustomerTax,ISCustomerRoyaltySet)");
                insertSql.Append(" values (");
                insertSql.Append("@EmployeeID,@CompanyCD,@MiniMoney,@MaxMoney,@ModifiedUserID,getdate(),@TaxPercent,@CustID,@NewCustomerTax,@OldCustomerTax,@ISCustomerRoyaltySet)");
                #endregion
                //定义插入基本信息的命令
                SqlCommand comm = new SqlCommand();
                //设置保存的参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID ", model.EmployeeID));//分公司ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@MiniMoney ", model.MiniMoney));//业绩上限
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaxMoney ", model.MaxMoney));//业绩下限
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新人
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate ", model.ModifiedDate));//最后更新时间
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaxPercent ", model.TaxPercent));//提成率

                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID ", model.CustID));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ISCustomerRoyaltySet ", model.ISCustomerRoyaltySet));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@NewCustomerTax ", model.NewCustomerTax));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OldCustomerTax ", model.OldCustomerTax));
                //添加返回参数
                //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));
                comm.CommandText = insertSql.ToString();

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
            return true ;

        }
        public static bool DeletePersonalTaxInfo(string CompanyCD,string EmpID,string CustID)
        {
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine("Delete from officedba.SalaryPersonalRoyaltySet where CompanyCD=@CompanyCD and EmployeeID=@EmployeeID and CustID=@CustID");

            //定义插入基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));	//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", EmpID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustID));
            //添加返回参数
            //   comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

            //执行插入操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
            return isSucc;


        }
        

        /// <summary>
        /// 选择执行状态的单据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCustList(string OrderNo, string Title, string model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号


            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            strSql = "SELECT ID, CustNo, CustName, ArtiPerson, CustNote, Relation" +
                     " FROM officedba.CustInfo " +
                     " WHERE  CompanyCD=@CompanyCD ";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (OrderNo != null)
            {
                strSql += " and CustNo like  '%" + OrderNo + "%'";
            }
            if (Title != null)
            {
                strSql += " and CustName like  '%" + Title + "%'";

            }
            if (model != "all")
            {
                strSql += " and  UsedStatus = '1' ";
            }

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);

        }

        /// <summary>
        /// 获取客户详细信息
        /// </summary>
        /// <param name="strID">客户编号</param>
        /// <returns></returns>
        public static DataTable GetCustInfo(string strID)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号


            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


            strSql = "SELECT officedba.CustInfo.CustType,officedba.CustInfo.CustNO, " +
                 " officedba.CustInfo.CurrencyType, officedba.CustInfo.TakeType, " +
                 " officedba.CustInfo.PayType,officedba.CustInfo.Tel,officedba.CustInfo.MoneyType, officedba.CustInfo.BusiType," +
                 " officedba.CustInfo.CarryType, officedba.CustInfo.CustName, " +
                 " officedba.CodePublicType.TypeName, officedba.CurrencyTypeSetting.ExchangeRate, officedba.CurrencyTypeSetting.CurrencyName " +
                 " FROM officedba.CustInfo LEFT OUTER JOIN " +
                 " officedba.CodePublicType ON officedba.CustInfo.CustType = officedba.CodePublicType.ID LEFT OUTER JOIN " +
                 " officedba.CurrencyTypeSetting ON officedba.CustInfo.CurrencyType = officedba.CurrencyTypeSetting.ID";
            strSql += " where officedba.CustInfo.ID='" + strID + "' and officedba.CustInfo.CompanyCD='" + strCompanyCD + "'";
            return SqlHelper.ExecuteSql(strSql);
        }
    }
}
