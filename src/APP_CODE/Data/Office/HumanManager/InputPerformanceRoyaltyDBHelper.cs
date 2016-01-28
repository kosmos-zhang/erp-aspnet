/**********************************************
 * 类作用：   绩效工资录入
 * 建立人：   肖合明
 * 建立时间： 2009/09/09
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
    public class InputPerformanceRoyaltyDBHelper
    {
        #region 读取列表信息
        public static DataTable GetInfo(InputPerformanceRoyaltyModel model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select a.ID,c.EmployeeName                                                               ");
            strSql.AppendLine(",a.TaskNo                                                                                ");
            strSql.AppendLine(",b.Title                                                                                 ");
            strSql.AppendLine(",case b.TaskFlag when '1' then '月考核' when '2' then '季考核'                           ");
            strSql.AppendLine("	when '3' then '半年考核' when '4' then '年考核' end as TaskFlag                         ");
            strSql.AppendLine(",b.TaskNum                                                                               ");
            strSql.AppendLine(",Convert(varchar(10),b.StartDate,21) as StartDate                                        ");
            strSql.AppendLine(",Convert(varchar(10),b.EndDate,21) as EndDate                                            ");
            strSql.AppendLine(",a.BaseMoney                                                                             ");
            strSql.AppendLine(",a.Confficent                                                                            ");
            strSql.AppendLine(",a.PerformanceMoney                                                                      ");
            strSql.AppendLine("from officedba. InputPerformanceRoyalty a                                                ");
            strSql.AppendLine("inner join officedba. PerformanceTask b on a.CompanyCD=b.CompanyCD and a.TaskNo=b.TaskNo ");
            strSql.AppendLine("inner join officedba.EmployeeInfo c on c.ID=a.EmployeeID                                 ");
            strSql.AppendLine("where a.CompanyCD=@CompanyCD 															");

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(model.TaskFlag))
            {
                strSql.AppendLine(" and a.TaskFlag =@TaskFlag");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag", model.TaskFlag));
            }
            if (!string.IsNullOrEmpty(model.EmployeeID))
            {
                strSql.AppendLine(" and a.EmployeeID = @EmployeeID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));
            }
            comm.CommandText = strSql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        public static bool DoInsert(string CompanyCD, string ModefiedUserID)
        {
            ArrayList Commlst = new ArrayList();
            string strDele = "Delete from officedba. InputPerformanceRoyalty where CompanyCD='" + CompanyCD + "'";
            //先把所有当前公司的记录都删除
            SqlCommand commdele = new SqlCommand(strDele);
            Commlst.Add(commdele);

            DataTable dt = GetBaseMoneyByInfo(CompanyCD);
            foreach (DataRow dr in dt.Rows)
            {
                InputPerformanceRoyaltyModel model = new InputPerformanceRoyaltyModel();
                model.EmployeeID = dr["EmployeeID"].ToString();
                model.TaskNo = dr["TaskNo"].ToString();
                model.CompanyCD = CompanyCD;
                model.TaskFlag = dr["TaskFlag"].ToString();
                model.BaseMoney = dr["BaseMoney"].ToString();
                model.PerformanceMoney = dr["PerformanceMoney"].ToString();
                model.Confficent = dr["Confficent"].ToString();
                model.ModifiedUserID = ModefiedUserID;

                SqlCommand comm = Insertcomm(model);
                Commlst.Add(comm);
            }
            return SqlHelper.ExecuteTransWithArrayList(Commlst);
        }

        #region 从总评表中获取所有数据
        /// <summary>
        /// 从总评表中获取所有数据(TaskNo,EmployeeID,RealScore,TaskFlag)
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        private static DataTable GetInfoFromSum(string CompanyCD)
        {
            string strSql = "select a.TaskNo,a.EmployeeID,a.RealScore,b.TaskFlag from officedba. PerformanceSummary a"
                            + " inner join officedba. PerformanceTask b on a.CompanyCD=b.CompanyCD and a.TaskNo=b.TaskNo"
                            + " where a.CompanyCD='" + CompanyCD + "' and a.realScore is not null";
            return SqlHelper.ExecuteSql(strSql);
        }

        #endregion

        #region 通过员工的实际得分和考核类型找出他的绩效系数
        /// <summary>
        /// 通过员工的实际得分和考核类型找出他的绩效系数,增加一列"Confficent"
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        private static DataTable GetConfficentByInfo(string CompanyCD)
        {
            //从总评表中获取所有数据
            DataTable dt = GetInfoFromSum(CompanyCD);
            dt.Columns.Add("Confficent", typeof(decimal));
            foreach (DataRow dr in dt.Rows)
            {
                DataTable dt1 = GetConfficent(dr["RealScore"].ToString(), dr["EmployeeID"].ToString(), dr["TaskFlag"].ToString());
                if (dt1.Rows.Count > 0)
                {
                    dr["Confficent"] = dt1.Rows[0]["Confficent"];
                }
                else
                {
                    DataTable dt2 = GetLastConfficent(dr["EmployeeID"].ToString(), dr["TaskFlag"].ToString());
                    if (dt2.Rows.Count > 0)
                    {
                        dr["Confficent"] = dt2.Rows[0]["Confficent"];
                    }
                    else
                    {
                        DataTable dt3 = GetDefaultConfficent(dr["RealScore"].ToString(), CompanyCD, dr["TaskFlag"].ToString());
                        if (dt3.Rows.Count > 0)
                        {
                            dr["Confficent"] = dt3.Rows[0]["Confficent"];
                        }
                        else
                        {
                            DataTable dt4 = GetLastDefaultConfficent(CompanyCD, dr["TaskFlag"].ToString());
                            if (dt4.Rows.Count > 0)
                            {
                                dr["Confficent"] = dt4.Rows[0]["Confficent"];
                            }
                        }
                    }
                }
            }
            return dt;
        }

        #endregion

        #region 通过员工还有他的考核类型找出他的绩效基数,已经算出他的绩效工资(把所有的信息组合了，下一步插入)
        private static DataTable GetBaseMoneyByInfo(string CompanyCD)
        {
            //得到DataTable，包含了绩效系数
            DataTable dt = GetConfficentByInfo(CompanyCD);
            dt.Columns.Add("BaseMoney", typeof(decimal));
            dt.Columns.Add("PerformanceMoney", typeof(decimal));
            foreach (DataRow dr in dt.Rows)
            {
                DataTable dt1 = GetBaseMoney(dr["TaskFlag"].ToString(), dr["EmployeeID"].ToString());
                if (dt1.Rows.Count > 0)
                {
                    dr["BaseMoney"] = dt1.Rows[0]["BaseMoney"];
                }
                else
                {
                    DataTable dt2 = GetDefaultBaseMoney(dr["TaskFlag"].ToString(), CompanyCD);
                    if (dt2.Rows.Count > 0)
                    {
                        dr["BaseMoney"] = dt2.Rows[0]["BaseMoney"];
                    }
                }
                if (string.IsNullOrEmpty(dr["Confficent"].ToString()))
                {
                    dr["Confficent"] = "0";
                }
                dr["PerformanceMoney"] = decimal.Parse(dr["BaseMoney"].ToString()) * decimal.Parse(dr["Confficent"].ToString ()) / 100;
            }
            return dt;
        }

        #endregion


        #region 返回一个插入的SqlCommand
        /// <summary>
        /// 返回一个插入的SqlCommand
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static SqlCommand Insertcomm(InputPerformanceRoyaltyModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.InputPerformanceRoyalty(");
            strSql.Append("EmployeeID,TaskNo,CompanyCD,TaskFlag,BaseMoney,PerformanceMoney,Confficent,ModifiedUserID,CreateTime,ModifiedDate)");
            strSql.Append(" values (");
            strSql.Append("@EmployeeID,@TaskNo,@CompanyCD,@TaskFlag,@BaseMoney,@PerformanceMoney,@Confficent,@ModifiedUserID,getdate(),getdate())");
            strSql.Append(";set @IndexID = @@IDENTITY");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();
            SqlParameter IndexID = new SqlParameter("@IndexID", SqlDbType.Int);
            IndexID.Direction = ParameterDirection.Output;
            comm.Parameters.Add(IndexID);
            SetSaveParameter(comm, model);
            return comm;

        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(InputPerformanceRoyaltyModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.InputPerformanceRoyalty set ");
            strSql.Append("EmployeeID=@EmployeeID,");
            strSql.Append("TaskNo=@TaskNo,");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("taskNum=@taskNum,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("TaskFlag=@TaskFlag,");
            strSql.Append("BaseMoney=@BaseMoney,");
            strSql.Append("PerformanceMoney=@PerformanceMoney,");
            strSql.Append("Confficent=@Confficent,");
            strSql.Append("ModifiedUserID=@ModifiedUserID,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("ModifiedDate=@ModifiedDate");
            strSql.Append(" where ID=@ID ");
            SqlCommand comm = new SqlCommand(strSql.ToString());
            SetSaveParameter(comm, model);
            return SqlHelper.ExecuteTransWithCommand(comm);


        }
        #endregion

        #region 保存时参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void SetSaveParameter(SqlCommand comm, InputPerformanceRoyaltyModel model)
        {
            if (!string.IsNullOrEmpty(model.ID) && model.ID != "0")
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID ", model.ID));//自动生成
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID ", model.EmployeeID));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo ", model.TaskNo));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate ", model.StartDate));//
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate ", model.EndDate));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag ", model.TaskFlag));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BaseMoney ", model.BaseMoney));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PerformanceMoney ", model.PerformanceMoney));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confficent ", model.Confficent));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//

        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除事件（通过ID数组删除）
        /// </summary>
        /// <param name="StrID"></param>
        /// <returns></returns>
        public static bool Delete(string StrID, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.InputPerformanceRoyalty ");
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

        #region 查询绩效系数
        /// <summary>
        /// 查询绩效系数
        /// </summary>
        /// <param name="RealScore"> 实际得分</param>
        /// <returns></returns>
        private static DataTable GetConfficent(string RealScore, string EmployeeID, string TaskFlag)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                        ");
            searchSql.AppendLine(" 	 Confficent                    ");
            searchSql.AppendLine(" FROM                          ");
            searchSql.AppendLine(" 	officedba.SalaryPerformanceRoyaltySet   ");
            searchSql.AppendLine(" WHERE                         ");
            searchSql.AppendLine("EmployeeID=@EmployeeID and");
            searchSql.AppendLine(" 	MiniScore < @RealScore      ");
            searchSql.AppendLine(" 	AND MaxScore >= @RealScore ");
            searchSql.AppendLine(" 	and Taskflag=@Taskflag ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //员工ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", EmployeeID));
            //实际得分
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RealScore", RealScore));
            //考核类型
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Taskflag", TaskFlag));
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        #endregion

        #region 获取某个员工的最大系数（当超过所有区间的时候取他的最大系数）
        /// <summary>
        /// 获取某个员工的最大系数（当超过所有区间的时候取他的最大系数）
        /// </summary>
        /// <param name="EmployeeID">员工编号</param>
        /// <returns></returns>
        private static DataTable GetLastConfficent(string EmployeeID, string TaskFlag)
        {
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select Confficent 					");
            searchSql.AppendLine("from officedba.SalaryPerformanceRoyaltySet where ");
            searchSql.AppendLine("EmployeeID=@EmployeeID                            ");
            searchSql.AppendLine("and Taskflag=@Taskflag                     ");
            searchSql.AppendLine("and ID=                                          ");
            searchSql.AppendLine("	(select Max(ID) from                            ");
            searchSql.AppendLine("	  officedba.SalaryPerformanceRoyaltySet         ");
            searchSql.AppendLine("	  where EmployeeID=@EmployeeID)                ");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //员工ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", EmployeeID));
            //考核类型
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Taskflag", TaskFlag));
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        #endregion

        #region 获取某个公司的默认系数
        /// <summary>
        /// 获取某个员工的最大系数
        /// </summary>
        /// <param name="EmployeeID">员工编号</param>
        /// <returns></returns>
        private static DataTable GetDefaultConfficent(string RealScore, string CompanyCD, string TaskFlag)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                        ");
            searchSql.AppendLine(" 	 Confficent                    ");
            searchSql.AppendLine(" FROM                          ");
            searchSql.AppendLine(" 	officedba.SalaryPerformanceRoyaltySet   ");
            searchSql.AppendLine(" WHERE                         ");
            searchSql.AppendLine("EmployeeID =0 and CompanyCD=@CompanyCD and");
            searchSql.AppendLine(" 	MiniScore < @RealScore      ");
            searchSql.AppendLine(" 	AND MaxScore >= @RealScore ");
            searchSql.AppendLine(" 	and Taskflag=@Taskflag ");
            #endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //实际得分
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RealScore", RealScore));
            //公司编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //考核类型
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Taskflag", TaskFlag));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        #endregion

        #region 获取某个公司默认的最大系数（当取默认值的时候，超过所有区间的时候取他的最大系数）
        /// <summary>
        /// 获取某个公司默认的最大系数（当取默认值的时候，超过所有区间的时候取他的最大系数）
        /// </summary>
        /// <param name="CompanyCD">公司编号</param>
        /// <returns></returns>
        private static DataTable GetLastDefaultConfficent(string CompanyCD, string TaskFlag)
        {
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select Confficent 					");
            searchSql.AppendLine("from officedba.SalaryPerformanceRoyaltySet where ");
            searchSql.AppendLine("EmployeeID =0 and CompanyCD=@CompanyCD           ");
            searchSql.AppendLine(" 	and Taskflag=@Taskflag ");
            searchSql.AppendLine("and ID=                                          ");
            searchSql.AppendLine("	(select Max(ID) from                            ");
            searchSql.AppendLine("	  officedba.SalaryPerformanceRoyaltySet         ");
            searchSql.AppendLine("	  where EmployeeID is null and CompanyCD=@CompanyCD)  ");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //员工ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //考核类型
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Taskflag", TaskFlag));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        #endregion

        #region 通过员工、考核类型获取绩效基数
        private static DataTable GetBaseMoney(string TaskFlag, string EmployeeID)
        {
            string strSql = "select BaseMoney from officedba. PerformanceRoyaltyBase where EmployeeID=" + EmployeeID + " and TaskFlag='" + TaskFlag + "'";
            return SqlHelper.ExecuteSql(strSql);
        }
        #endregion

        #region 通过考核类型获取公司默认的绩效基数
        private static DataTable GetDefaultBaseMoney(string TaskFlag, string CompanyCD)
        {
            string strSql = "select BaseMoney from officedba. PerformanceRoyaltyBase where CompanyCD='" + CompanyCD + "' and TaskFlag='" + TaskFlag + "' and EmployeeID=0";
            return SqlHelper.ExecuteSql(strSql);
        }
        #endregion

        #region 判断员工(通过考核类型)绩效基数是否设置
        /// <summary>
        /// 判断员工(通过考核类型)绩效基数是否设置
        /// </summary>
        /// <param name="TaskFlag"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns>Ture:已经设置；false:没有设置（提示）</returns>
        public static bool IsBaseMoneySet(string CompanyCD)
        {
            //获取总评表中当前公司所有数据
            DataTable dt = GetInfoFromSum(CompanyCD);
            bool result = true;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string strSqlEmp = "select BaseMoney from officedba. PerformanceRoyaltyBase where CompanyCD='" + CompanyCD + "' and TaskFlag='" + dr["TaskFlag"].ToString() + "' and EmployeeID=" + dr["EmployeeID"].ToString();
                    DataTable dt1 = SqlHelper.ExecuteSql(strSqlEmp);
                    string strSqlCom = "select BaseMoney from officedba. PerformanceRoyaltyBase where CompanyCD='" + CompanyCD + "' and TaskFlag='" + dr["TaskFlag"].ToString() + "' and EmployeeID=0";
                    DataTable dt2 = SqlHelper.ExecuteSql(strSqlCom);
                    if (dt1.Rows.Count < 1 && dt2.Rows.Count < 1)
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        #endregion

        #region 判断员工绩效系数是否设置
        public static bool IsConfficentSet(string CompanyCD)
        {
            //获取总评表中当前公司所有数据
            DataTable dt = GetInfoFromSum(CompanyCD);
            bool result = true;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string strSqlEmp = "select Confficent from officedba. SalaryPerformanceRoyaltySet where CompanyCD='" + CompanyCD + "' and TaskFlag='" + dr["TaskFlag"].ToString() + "' and EmployeeID=" + dr["EmployeeID"].ToString();
                    DataTable dt1 = SqlHelper.ExecuteSql(strSqlEmp);
                    string strSqlCom = "select Confficent from officedba. SalaryPerformanceRoyaltySet where CompanyCD='" + CompanyCD + "' and TaskFlag='" + dr["TaskFlag"].ToString() + "' and EmployeeID=0";
                    DataTable dt2 = SqlHelper.ExecuteSql(strSqlCom);
                    if (dt1.Rows.Count < 1 && dt2.Rows.Count < 1)
                    {
                        result = false;
                    }

                }
            }
            return result;
        }
        #endregion

        public static DataTable GetMonthPerformanceSalary(string companyCD, string salaryMonth, string startDate, string endDate)
        {
            string year = salaryMonth.Substring(0, 4);
            string month = salaryMonth.Substring(4, 2);
            bool sign = false;
            bool isHalfYear = false;
            bool isCompleteYear = false; ;
            int i = Convert.ToInt32(month);

            int halfYearSign = 0, sessionSign = 0;

            if (i == 12)
            {
                halfYearSign = 2;//下半年
                isHalfYear = true;
            }
            else if (i == 6)
            {
                halfYearSign = 1;//上半年
                isHalfYear = true;
            }

            if (i == 3)
            {
                sessionSign = 1;//第一季度
                sign = true;
            }
            else if (i == 6)
            {
                sessionSign = 2;//第二季度
                sign = true;
            }
            else if (i == 9)
            {
                sessionSign = 3;//第三季度
                sign = true;
            }
            else if (i == 12)
            {
                sessionSign = 4;//第四季度
                sign = true;
                isCompleteYear = true;
            }





            StringBuilder searchSql = new StringBuilder();

            searchSql.AppendLine(" SELECT 		EmployeeID, sum(PerformanceMoney) as TotalPerformanceMoney  from (                  ");

            searchSql.AppendLine(" SELECT 		A.EmployeeID,A.PerformanceMoney                    ");
            searchSql.AppendLine(" FROM                             ");
            searchSql.AppendLine(" 	officedba.InputPerformanceRoyalty   A left outer join officedba.PerformanceTask B on A.CompanyCD=B.CompanyCD  and  A.taskno=B.taskno      ");
            searchSql.AppendLine(" WHERE                            ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD  and A.taskflag='1'  and B.taskdate=@taskdate and B.taskNum=@tasknum    ");
            if (sign)//季度考核
            {
                searchSql.AppendLine(" 	 Union   ");
                searchSql.AppendLine(" SELECT 		A.EmployeeID,A.PerformanceMoney                    ");
                searchSql.AppendLine(" FROM                             ");
                searchSql.AppendLine(" 	officedba.InputPerformanceRoyalty   A left outer join officedba.PerformanceTask B on A.CompanyCD=B.CompanyCD  and  A.taskno=B.taskno      ");
                searchSql.AppendLine(" WHERE                            ");
                searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD  and A.taskflag='2'  and B.taskdate=@taskdate and B.taskNum=@sessionSign    ");
            }
            if (isHalfYear)//半年考核
            {
                searchSql.AppendLine(" 	 Union   ");
                searchSql.AppendLine(" SELECT 		A.EmployeeID,A.PerformanceMoney                    ");
                searchSql.AppendLine(" FROM                             ");
                searchSql.AppendLine(" 	officedba.InputPerformanceRoyalty   A left outer join officedba.PerformanceTask B on A.CompanyCD=B.CompanyCD  and  A.taskno=B.taskno      ");
                searchSql.AppendLine(" WHERE                            ");
                searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD  and A.taskflag='3'  and B.taskdate=@taskdate and B.taskNum=@halfYearSign    ");
            }
            if (isCompleteYear)//年考核
            {
                searchSql.AppendLine(" 	 Union   ");
                searchSql.AppendLine(" SELECT 		A.EmployeeID,A.PerformanceMoney                    ");
                searchSql.AppendLine(" FROM                             ");
                searchSql.AppendLine(" 	officedba.InputPerformanceRoyalty   A left outer join officedba.PerformanceTask B on A.CompanyCD=B.CompanyCD  and  A.taskno=B.taskno      ");
                searchSql.AppendLine(" WHERE                            ");
                searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD  and A.taskflag='4'  and B.taskdate=@taskdate  ");
            }

            searchSql.AppendLine(" )as G  group by EmployeeID          ");

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@taskdate", year));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@tasknum", Convert.ToString(i)));
            if (sign)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@sessionSign", Convert.ToString(sessionSign)));
            }
            if (isHalfYear)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@halfYearSign", Convert.ToString(halfYearSign)));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
    }
}
