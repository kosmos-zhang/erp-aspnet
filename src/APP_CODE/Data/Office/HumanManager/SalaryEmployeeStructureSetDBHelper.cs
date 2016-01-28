/**********************************************
 * 类作用：   人员薪资结构设置
 * 建立人：   肖合明
 * 建立时间： 2009/09/07
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
    public class SalaryEmployeeStructureSetDBHelper
    {
        #region 通过人员ID获取当前的薪资结构设置
        /// <summary>
        /// 通过人员ID获取当前的薪资结构设置
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <param name="CompanyCD">防止其他公司的人员通过改参数获取信息</param>
        /// <returns></returns>
        public static DataTable GetUserInfo(string EmployeeID, string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("SELECT ID                                  ");
            strSql.AppendLine(",EmployeeID                                ");
            strSql.AppendLine(",CompanyCD                                 ");
            strSql.AppendLine(",ISNULL(IsCompanyRoyaltySet,'0') as IsCompanyRoyaltySet                  ");
            strSql.AppendLine(",ISNULL(IsDeptRoyaltySet,'0') as IsDeptRoyaltySet                        ");
            strSql.AppendLine(",ISNULL(IsProductRoyaltySet,'0') as IsProductRoyaltySet                  ");
            strSql.AppendLine(",ISNULL(IsFixSalarySet,'0') as IsFixSalarySet                            ");
            strSql.AppendLine(",ISNULL(IsPieceWorkSet,'0') as IsPieceWorkSet                            ");
            strSql.AppendLine(",ISNULL(IsInsurenceSet,'0') as IsInsurenceSet                            ");
            strSql.AppendLine(",ISNULL(IsPerIncomeTaxSet,'0') as IsPerIncomeTaxSet                      ");
            strSql.AppendLine(",ISNULL(IsQuteerSet,'0') as IsQuteerSet                                  ");
            strSql.AppendLine(",ISNULL(IsTimeWorkSet,'0') as IsTimeWorkSet                              ");
            strSql.AppendLine(",ISNULL(IsPersonalRoyaltySet,'0') as IsPersonalRoyaltySet                ");
            strSql.AppendLine(",CompanyRatePercent");
            strSql.AppendLine(",DeptRatePercent");
            strSql.AppendLine(",ModifiedUserID                            ");
            strSql.AppendLine(",ModifiedDate                              ");
            strSql.AppendLine(",ISNULL(IsPerformanceSet,'0') as IsPerformanceSet                        ");
            strSql.AppendLine("FROM officedba.SalaryEmployeeStructureSet ");
            strSql.AppendLine("where EmployeeID=@EmployeeID and CompanyCD=@CompanyCD");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", EmployeeID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        #region 执行保存操作

        /// <summary>
        /// 执行保存操作
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="ModelList"></param>
        /// <returns></returns>
        public static bool SaveInfo(string EmployeeID, SalaryEmployeeStructureSetModel Model)
        {
            ArrayList lstUpdate = new ArrayList();
            string strSqlDel = "Delete from officedba.SalaryEmployeeStructureSet where EmployeeID=@EmployeeID";
            SqlCommand commDel = new SqlCommand();
            commDel.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", EmployeeID));
            commDel.CommandText = strSqlDel;
            //先删除所有当前分公司的所有记录
            lstUpdate.Add(commDel);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SalaryEmployeeStructureSet(");
            strSql.Append("EmployeeID,CompanyCD,IsCompanyRoyaltySet,IsDeptRoyaltySet,IsProductRoyaltySet,IsFixSalarySet,IsPieceWorkSet,IsInsurenceSet,IsPerIncomeTaxSet,IsQuteerSet,IsTimeWorkSet,IsPersonalRoyaltySet,ModifiedUserID,ModifiedDate,IsPerformanceSet,CompanyRatePercent,DeptRatePercent)");
            strSql.Append(" values (");
            strSql.Append("@EmployeeID,@CompanyCD,@IsCompanyRoyaltySet,@IsDeptRoyaltySet,@IsProductRoyaltySet,@IsFixSalarySet,@IsPieceWorkSet,@IsInsurenceSet,@IsPerIncomeTaxSet,@IsQuteerSet,@IsTimeWorkSet,@IsPersonalRoyaltySet,@ModifiedUserID,getdate(),@IsPerformanceSet,@CompanyRatePercent,@DeptRatePercent)");
            strSql.Append(";select @@IDENTITY");

            SqlCommand InserComm = new SqlCommand();
            InserComm.CommandText = strSql.ToString();
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID ", Model.EmployeeID));
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", Model.CompanyCD));//分公司ID
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@IsCompanyRoyaltySet ", Model.IsCompanyRoyaltySet));//分公司ID
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@IsDeptRoyaltySet ", Model.IsDeptRoyaltySet));//分公司ID
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@IsProductRoyaltySet ", Model.IsProductRoyaltySet));//分公司ID
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@IsFixSalarySet ", Model.IsFixSalarySet));//分公司ID
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@IsPieceWorkSet ", Model.IsPieceWorkSet));//分公司ID
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@IsInsurenceSet ", Model.IsInsurenceSet));//分公司ID
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@IsPerIncomeTaxSet ", Model.IsPerIncomeTaxSet));//分公司ID
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@IsQuteerSet ", Model.IsQuteerSet));//分公司ID
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@IsTimeWorkSet ", Model.IsTimeWorkSet));//分公司ID
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@IsPersonalRoyaltySet ", Model.IsPersonalRoyaltySet));//
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", Model.ModifiedUserID));//
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@IsPerformanceSet ", Model.IsPerformanceSet));//
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyRatePercent ", Model.CompanyRatePercent));//
            InserComm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptRatePercent ", Model.DeptRatePercent));//

            //把插入Command加入集合
            lstUpdate.Add(InserComm);
            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);
        }

        #endregion

        #region 获取部门信息
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 部门信息</returns>
        public static DataTable GetDeptInfo(string companyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            //定义查询语句
            selectSql.AppendLine(" SELECT ID, CompanyCD, DeptNO ");
            selectSql.AppendLine(" , SuperDeptID, PYShort ");
            selectSql.AppendLine(" , DeptName, AccountFlag,'' as isFlag,subflag ");
            selectSql.AppendLine(" FROM officedba.DeptInfo ");
            selectSql.AppendLine(" WHERE ");
            selectSql.AppendLine(" CompanyCD = @CompanyCD ");
            selectSql.AppendLine(" AND UsedStatus = @UsedStatus ");
            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //启用状态
            param[1] = SqlHelper.GetParameter("@UsedStatus", "1");

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }
        #endregion

        #region 获取员工信息
        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 员工信息</returns>
        public static DataTable GetUserInfo(string companyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            selectSql.AppendLine("select a.ID,ISNULL(a.EmployeeName,'') AS EmployeesName");
            selectSql.AppendLine(" 	, ISNULL(C.QuarterName,'') AS QuarterName ,                      ");
            selectSql.AppendLine("isnull(cast (a.DeptID as varchar),'')  as  DeptID ,a.QuarterID   from officedba.EmployeeInfo a ");
            selectSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo B       ");
            selectSql.AppendLine(" 		ON  B.CompanyCD=A.CompanyCD  and B.ID = A.DeptID              ");
            selectSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter C    ");
            selectSql.AppendLine(" 		ON  C.CompanyCD=A.CompanyCD   and  C.ID = A.QuarterID   and c.DeptID= A.DeptID       ");
            selectSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType D ");
            selectSql.AppendLine(" 		ON  D.CompanyCD=A.CompanyCD and D.ID = A.AdminLevelID        ");
            selectSql.AppendLine("where a.CompanyCD=@CompanyCD and a.Flag='1'  and a.DeptID Is not null  ");
            selectSql.AppendLine("  ORDER BY a.DeptID, C.ID asc,a.AdminLevelID asc");
            //, a.EnterDate a.QuarterID ASC,A.AdminLevelID  asc

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }
        #endregion

        #region 查询人员薪资结构
        /// <summary>
        /// 查询人员薪资结构
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="salaryMonth">年月份</param>
        /// <returns></returns>
        public static DataTable GetSalaryStructure(string companyCD )
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT 		    *                ");
            searchSql.AppendLine(" FROM                             ");
            searchSql.AppendLine(" 	officedba.SalaryEmployeeStructureSet      ");
            searchSql.AppendLine(" WHERE                            ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD       ");
       
          
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
    }
}
