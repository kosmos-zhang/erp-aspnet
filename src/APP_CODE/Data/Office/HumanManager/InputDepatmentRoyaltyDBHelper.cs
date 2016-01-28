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
  public class InputDepatmentRoyaltyDBHelper
    {
      public static DataTable SearchPersonTaxInfo(string companyCD, string DeptID, string StartDate, string EndDate, int pageIndex, int pageCount, string ord, ref int totalCount)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                          ");
            searchSql.AppendLine(" 	 a.ID                                          ");
            searchSql.AppendLine(" 	,a.CompanyCD                                   ");
            searchSql.AppendLine(" 	,a.DeptID                                      ");
            searchSql.AppendLine(" 	, b.DeptName                                   ");
            searchSql.AppendLine(" 	,isnull(a.BusinessMoney,0)  BusinessMoney                            ");
            searchSql.AppendLine("   ,isnull( CONVERT(CHAR(10), a.CreateTime, 23),'') as CreateTime                        ");
            searchSql.AppendLine(" FROM                                            ");
            searchSql.AppendLine(" 	officedba.InputDepatmentRoyalty    a            ");
            searchSql.AppendLine(" inner join	officedba.DeptInfo b on a.DeptID=b.ID                 ");
            searchSql.AppendLine(" WHERE                                           ");
            searchSql.AppendLine(" 	a.CompanyCD = @CompanyCD                         ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));

            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine("	AND  a.DeptID=@DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            //时间
            if (!string.IsNullOrEmpty(StartDate))
            {
                searchSql.AppendLine("	AND a.CreateTime >= @StartDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                searchSql.AppendLine("	AND a.CreateTime<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
        }

        /// <summary>
        /// 根据时间查询
        /// </summary>
        /// <param name="companyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static DataTable SearchInsuPersonalTaxInfo(string companyCD, string DeptID, string StartDate, string EndDate)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                                                                          ");
            searchSql.AppendLine(" 	  a.ID                                                                                         ");
            searchSql.AppendLine(" 	 ,a.DeptID                                                                                     ");
            searchSql.AppendLine(" 	, b.DeptName                                                                                   ");
            searchSql.AppendLine(" 	,a.MiniMoney                                                                                   ");
            searchSql.AppendLine(" 	,a.MaxMoney                                                                                    ");
            searchSql.AppendLine(" 	,a.TaxPercent                                                                                  ");
            searchSql.AppendLine(" FROM                                                                                            ");
            searchSql.AppendLine(" 	officedba.SalaryDepatmentRoyaltySet a                                                          ");
            searchSql.AppendLine(" inner join	officedba.DeptInfo b on a.DeptID=b.ID                                              ");
            searchSql.AppendLine(" left  join	officedba.InputDepatmentRoyalty c on a.DeptID=c.DeptID and a.CompanyCD=c.CompanyCD ");
            searchSql.AppendLine(" and a.TaxPercent=c.RatePercent                                                                   ");
            searchSql.AppendLine(" WHERE                                                                                           ");
            searchSql.AppendLine(" 	a.CompanyCD = @CompanyCD                                                    ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine("	AND  a.DeptID=@DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            //时间
            if (!string.IsNullOrEmpty(StartDate))
            {
                searchSql.AppendLine("	AND c.CreateTime >= @StartDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                searchSql.AppendLine("	AND c.CreateTime<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        public static DataTable SearchPersonTax(string companyCD, string reportMonth)
        {
            int year = Convert.ToInt32(reportMonth.Substring(0, 4));
            int month = Convert.ToInt32(reportMonth.Substring(4, 2));
            int day = 0;
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                day = 31;
            }
            if (month == 2)
            {
                day = 28;
            }
            else
            {
                day = 30;
            }

            DateTime reportM = new DateTime(year, month, day);
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                  ");
            searchSql.AppendLine(" 	 ID                    ");
            searchSql.AppendLine(" 	,CompanyCD         ");
            searchSql.AppendLine(" 	,EmployeeID          ");
            searchSql.AppendLine(" 	,StartDate        ");
            searchSql.AppendLine(" 	,SalaryCount         ");
            searchSql.AppendLine(" 	,TaxPercent            ");
            searchSql.AppendLine(" 	,TaxCount            ");
            searchSql.AppendLine(" FROM                    ");
            searchSql.AppendLine(" 	officedba.IncomeTax   ");
            searchSql.AppendLine(" WHERE                   ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD ");
            searchSql.AppendLine("  and 	@StartDate >= StartDate ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", reportM.ToShortDateString()));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        public static DataTable PersonTaxInfo(string companyCD)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                  ");
            searchSql.AppendLine(" 	MinMoney                    ");
            searchSql.AppendLine(" 	,MaxMoney         ");
            searchSql.AppendLine(" 	,TaxPercent          ");
            searchSql.AppendLine(" 	,MinusMoney        ");
            searchSql.AppendLine(" FROM                    ");
            searchSql.AppendLine(" 	officedba.IncomeTaxPercent   ");
            searchSql.AppendLine(" WHERE                   ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD ");
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


      /// <summary>
      /// 修改
      /// </summary>
      /// <param name="modeList"></param>
      /// <returns></returns>
        public static bool UpdateIsuPersonalTaxInfo(IList<InputDepatmentRoyaltyModel> modeList)
        {
            if (!DeletePersonalTaxInfo(modeList[0].CompanyCD,modeList[0].DeptID))
            {
                return false;
            }
            bool isSucc = false;
            foreach (InputDepatmentRoyaltyModel model in modeList)
            {
                #region 插入SQL拼写
                StringBuilder insertSql = new StringBuilder();
                insertSql.AppendLine("insert into  officedba.InputDepatmentRoyalty(CompanyCD,DeptID,CreateTime,BusinessMoney,RatePercent,ModifiedUserID,ModifiedDate) ");
                insertSql.AppendLine("          values(@CompanyCD,@DeptID,@CreateTime,@BusinessMoney,@RatePercent,@ModifiedUserID,getdate()) ");
                #endregion
                //定义插入基本信息的命令
                SqlCommand comm = new SqlCommand();
                comm.CommandText = insertSql.ToString();
                //设置保存的参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));	//部门ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateTime ", model.CreateTime));	//生效日期
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BusinessMoney", model.BusinessMoney));	//业务量
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RatePercent", model.RatePercent));	//提成率
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));
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
            return isSucc;

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeletePersonalTaxInfo(string CompanyCD,string DeptID)
        {
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine("Delete from officedba.InputDepatmentRoyalty where CompanyCD=@CompanyCD and DeptID=@DeptID");

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


        /// <summary>
        /// 插入部门提成
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertInputDepatmentRoyalty(InputDepatmentRoyaltyModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.InputDepatmentRoyalty");
            sql.AppendLine("           (CompanyCD                     ");
            sql.AppendLine("           ,DeptID                      ");
            sql.AppendLine("           ,BusinessMoney                      ");
            sql.AppendLine("           ,CreateTime                      ");
            sql.AppendLine("           ,ModifiedDate                  ");
            sql.AppendLine("           ,ModifiedUserID)               ");
            sql.AppendLine("     VALUES                               ");
            sql.AppendLine("           (@CompanyCD                    ");
            sql.AppendLine("           ,@DeptID                     ");
            sql.AppendLine("           ,@BusinessMoney                     ");
            sql.AppendLine("           ,@CreateTime                     ");
            sql.AppendLine("           ,@ModifiedDate                 ");
            sql.AppendLine("           ,@ModifiedUserID)               ");
            //设置参数
            SqlParameter[] param = new SqlParameter[6];
            int i = 0;
            param[i++] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[i++] = SqlHelper.GetParameter("@DeptID", model.DeptID);
            param[i++] = SqlHelper.GetParameter("@BusinessMoney", model.BusinessMoney);
            param[i++] = SqlHelper.GetParameter("@CreateTime", model.CreateTime);
            param[i++] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);
            param[i++] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }
        /// <summary>
        /// 修改部门提成
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateInputDepatmentRoyalty(InputDepatmentRoyaltyModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.InputDepatmentRoyalty   ");
            sql.AppendLine("   SET                                   ");
            sql.AppendLine("       BusinessMoney = @BusinessMoney              ");
            sql.AppendLine("      ,CreateTime = @CreateTime          ");
            sql.AppendLine("      ,ModifiedDate = @ModifiedDate      ");
            sql.AppendLine("      ,ModifiedUserID = @ModifiedUserID  ");
            sql.AppendLine(" WHERE ID=@ID ");

            //设置参数
            SqlParameter[] param = new SqlParameter[5];
            int i = 0;
            param[i++] = SqlHelper.GetParameter("@BusinessMoney", model.BusinessMoney);
            param[i++] = SqlHelper.GetParameter("@ID", model.ID);
            param[i++] = SqlHelper.GetParameter("@CreateTime", model.CreateTime);
            param[i++] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);
            param[i++] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }
        #region 查询月份的部门业务提成信息
        /// <summary>
        /// 查询月份的部门业务提成信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="salaryMonth">年月份</param>
        /// <returns></returns>
        public static DataTable GetMonthDeptSalary(string companyCD, string salaryMonth, string startDate, string endDate)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT 		                    ");
            searchSql.AppendLine(" 	SUM(BusinessMoney) AS TotalSalary ");
            searchSql.AppendLine(" 	,DeptID AS DeptID       ");
            searchSql.AppendLine(" FROM                             ");
            searchSql.AppendLine(" 	officedba.InputDepatmentRoyalty      ");
            searchSql.AppendLine(" WHERE                            ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD  and CreateTime  between @startDate and @endDate       ");
            //searchSql.AppendLine(" 	CompanyCD = @CompanyCD          ");
            //searchSql.AppendLine(" 	AND SUBSTRING(                  ");
            //searchSql.AppendLine(" 	  CONVERT(VARCHAR, CommDate     ");
            //searchSql.AppendLine(" 	  , 112), 1, 6) = @SalaryMonth  ");
            searchSql.AppendLine(" GROUP BY                         ");
            searchSql.AppendLine(" 	DeptID                      ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //员工ID
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@SalaryMonth", salaryMonth));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@startDate", startDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@endDate", endDate));
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        #region 查询部门业务提成设置信息
        /// <summary>
        /// 查询部门业务提成设置信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetDeptSetInfo(string companyCD)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT 		      *              ");
            searchSql.AppendLine(" FROM                             ");
            searchSql.AppendLine(" 	officedba.SalaryDepatmentRoyaltySet      ");
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
        /// <summary>
        /// 删除部门提成
        /// </summary>
        /// <param name="TypeFlag"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteInputDepatmentRoyalty(string ID)
        {
            string allID = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] Delsql = new string[1];
            try
            {
                string[] IdS = null;
                ID = ID.Substring(0, ID.Length);
                IdS = ID.Split(',');

                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                //allUserID = sb.ToString();
                allID = sb.ToString().Replace("''", "','");
                Delsql[0] = "delete from  officedba.InputDepatmentRoyalty where ID IN (" + allID + ") ";
                SqlHelper.ExecuteTransForListWithSQL(Delsql);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
