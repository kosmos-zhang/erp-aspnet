/**********************************************
 * 类作用：   公司提成录入
 * 建立人：   肖合明
 * 建立时间： 2009/09/08
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
    public class InputCompanyRoyaltyDBHelper
    {
        #region 查询：公司提成录入信息
        /// <summary>
        /// 查询公司提成录入信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetInfo(InputCompanyRoyaltyModel model, string timeStart, string timeEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("SELECT a.ID                                      ");
            strSql.AppendLine(",a.DeptID                                        ");
            strSql.AppendLine(",b.DeptName                                      ");
            strSql.AppendLine(",a.BusinessMoney                                 ");
            strSql.AppendLine(",Convert(Varchar(10),a.RecordDate,21) as RecordDate      ");
            strSql.AppendLine("FROM officedba.InputCompanyRoyalty a             ");
            strSql.AppendLine("inner join officedba.DeptInfo b on a.DeptID=b.ID ");
            strSql.AppendLine("where a.CompanyCD=@CompanyCD						");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.DeptID))
            {
                strSql.AppendLine(" and a.DeptID = @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));
            }
            if (!string.IsNullOrEmpty(timeStart))
            {
                strSql.AppendLine(" and a.RecordDate>=@timeStart");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeStart", timeStart));
            }

            if (!string.IsNullOrEmpty(timeEnd))
            {
                strSql.AppendLine(" and a.RecordDate<=@timeEnd");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeEnd", Convert.ToDateTime(timeEnd).AddDays(1).ToString("yyyy-MM-dd")));
            }
            comm.CommandText = strSql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);

        }
        #endregion

        #region 插入
        /// <summary>
        /// 插入一条新的数据，如果成功给Model.ID赋值主键，否则Model.ID="0"
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool InSert(InputCompanyRoyaltyModel Model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("insert into officedba.InputCompanyRoyalty(");
            strSql.AppendLine("DeptID,CompanyCD,BusinessMoney,RecordDate,ModifiedUserID,CreateTime,ModifiedDate)");
            strSql.AppendLine(" values (");
            strSql.AppendLine("@DeptID,@CompanyCD,@BusinessMoney,@RecordDate,@ModifiedUserID,getdate(),getdate())");
            strSql.AppendLine(";set @IndexID= @@IDENTITY");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();
            SqlParameter IndexID = new SqlParameter("@IndexID", SqlDbType.Int);
            IndexID.Direction = ParameterDirection.Output;
            comm.Parameters.Add(IndexID);
            SetSaveParameter(comm, Model);
            bool result = SqlHelper.ExecuteTransWithCommand(comm);
            if (result)
            {
                Model.ID = comm.Parameters["@IndexID"].Value.ToString();
            }
            else
            {
                Model.ID = "0";
            }
            return result;
        }

        #endregion

        #region 更新
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool Update(InputCompanyRoyaltyModel Model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.InputCompanyRoyalty set ");
            strSql.AppendLine("DeptID=@DeptID,");
            strSql.AppendLine("CompanyCD=@CompanyCD,");
            strSql.AppendLine("BusinessMoney=@BusinessMoney,");
            strSql.AppendLine("RecordDate=@RecordDate,");
            strSql.AppendLine("ModifiedUserID=@ModifiedUserID,");
            strSql.AppendLine("ModifiedDate=getdate()");
            strSql.AppendLine(" where ID=@ID ");
            SqlCommand comm = new SqlCommand(strSql.ToString());
            SetSaveParameter(comm, Model);
            return SqlHelper.ExecuteTransWithCommand(comm);

        }
        #endregion


        #region 保存时参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void SetSaveParameter(SqlCommand comm, InputCompanyRoyaltyModel model)
        {
            if (!string.IsNullOrEmpty(model.ID))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID ", model.ID));//自动生成
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID ", model.DeptID));//分公司ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BusinessMoney ", model.BusinessMoney));//数量
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RecordDate ", model.RecordDate));//记录时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新人

        }
        #endregion


        #region 删除
        /// <summary>
        /// 删除事件（通过ID数组删除）
        /// </summary>
        /// <param name="StrID"></param>
        /// <returns></returns>
        public static bool DoDelete(string StrID, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.InputCompanyRoyalty ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" ID In( " + StrID + ")");
            deleteSql.AppendLine(" AND CompanyCD = @CompanyCD ");
            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            //comm.Parameters.Add(SqlHelper.GetParameter("@ID", StrID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));

            return SqlHelper.ExecuteTransWithCommand(comm);
        }

        #endregion

        #region 查询月份的公司业务提成信息
        /// <summary>
        /// 查询月份的公司业务提成信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="salaryMonth">年月份</param>
        /// <returns></returns>
        public static DataTable GetMonthCompanySalary(string companyCD, string salaryMonth, string startDate, string endDate)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT 		                    ");
            searchSql.AppendLine(" 	SUM(BusinessMoney) AS TotalSalary ");
            searchSql.AppendLine(" 	,DeptID AS DeptID       ");
            searchSql.AppendLine(" FROM                             ");
            searchSql.AppendLine(" 	officedba.InputCompanyRoyalty      ");
            searchSql.AppendLine(" WHERE                            ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD  and RecordDate  between @startDate and @endDate       ");
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
        #region 查询公司业务提成设置信息
        /// <summary>
        /// 查询公司业务提成设置信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetCompanySetInfo(string companyCD)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT 		      *              ");
            searchSql.AppendLine(" FROM                             ");
            searchSql.AppendLine(" 	officedba.SalaryCompanyRoyaltySet      ");
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
