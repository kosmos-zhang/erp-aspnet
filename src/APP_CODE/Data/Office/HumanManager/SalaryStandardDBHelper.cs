/**********************************************
 * 类作用：   工资标准设置
 * 建立人：   吴志强
 * 建立时间： 2009/05/07
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

namespace XBase.Data.Office.HumanManager
{
    /// <summary>
    /// 类名：SalaryStandardDBHelper
    /// 描述：工资标准设置
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/07
    /// 最后修改时间：2009/05/07
    /// </summary>
    ///
    public class SalaryStandardDBHelper
    {
        #region 通过检索条件查询工资标准信息
        /// <summary>
        /// 查询工资标准信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchSalaryStandardInfo(SalaryStandardModel model)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                     ");
            searchSql.AppendLine(" 	 A.ID                                     ");
            searchSql.AppendLine(" 	,A.CompanyCD                              ");
            searchSql.AppendLine(" 	,ISNULL(A.QuarterID, '') AS QuarterID     ");
            searchSql.AppendLine(" 	,ISNULL(B.QuarterName, '') AS QuarterName ");
            searchSql.AppendLine(" 	,ISNULL(A.AdminLevel, '') AS AdminLevel   ");
            searchSql.AppendLine(" 	,ISNULL(C.TypeName, '') AS AdminLevelName ");
            searchSql.AppendLine(" 	,ISNULL(A.ItemNo, '') AS ItemNo           ");
            searchSql.AppendLine(" 	,ISNULL(D.ItemName, '') AS ItemName       ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR,A.UnitPrice), '') ");
            searchSql.AppendLine(" 		AS UnitPrice                          ");
            searchSql.AppendLine(" 	,ISNULL(A.Remark, '') AS Remark           ");
            searchSql.AppendLine(" 	,ISNULL(A.UsedStatus, '') AS UsedStatus   ");
            searchSql.AppendLine(" 	,CASE A.UsedStatus                        ");
            searchSql.AppendLine(" 		WHEN '0' THEN '停用'                  ");
            searchSql.AppendLine(" 		WHEN '1' THEN '启用'                  ");
            searchSql.AppendLine(" 		ELSE ''                               ");
            searchSql.AppendLine(" 	END AS UsedStatusName                     ");
            searchSql.AppendLine(" FROM                                       ");
            searchSql.AppendLine(" 	officedba.SalaryStandard A                ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter B         ");
            searchSql.AppendLine(" 		ON B.companyCD=A.companyCD AND B.ID = A.QuarterID                 ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType C      ");
            searchSql.AppendLine(" 		ON C.companyCD=A.companyCD AND C.ID = A.AdminLevel                ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.SalaryItem D          ");
            searchSql.AppendLine(" 		ON D.companyCD=A.companyCD AND D.ItemNo = A.ItemNo                ");
            searchSql.AppendLine(" WHERE                                      ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                  ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();

            #region 设置参数
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //岗位ID
            if (!string.IsNullOrEmpty(model.QuarterID))
            {
                searchSql.AppendLine("	AND A.QuarterID = @QuarterID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));
            }
            //岗位职等
            if (!string.IsNullOrEmpty(model.AdminLevel))
            {
                searchSql.AppendLine("	AND A.AdminLevel = @AdminLevel ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdminLevel", model.AdminLevel));
            }
            //启用状态
            if (!string.IsNullOrEmpty(model.UsedStatus))
            {
                searchSql.AppendLine("	AND A.UsedStatus = @UsedStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));
            }
            #endregion

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        
        #region 通过检索条件查询工资标准信息
        /// <summary>
        /// 查询工资标准信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static bool CheckSalaryStandardInfo(SalaryStandardModel model)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                        ");
            searchSql.AppendLine(" 	 ID                          ");
            searchSql.AppendLine(" FROM                          ");
            searchSql.AppendLine(" 	officedba.SalaryStandard     ");
            searchSql.AppendLine(" WHERE                         ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD       ");   
            searchSql.AppendLine(" 	AND ItemNo = @ItemNo         ");
            searchSql.AppendLine(" 	AND QuarterID = @QuarterID   ");
            searchSql.AppendLine(" 	AND AdminLevel = @AdminLevel ");
       
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();

            #region 设置参数
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //岗位ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));
            //岗位职等
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdminLevel", model.AdminLevel));
            //工资编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemNo", model.ItemNo));
            #endregion

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
          
            DataTable dtStandard = SqlHelper.ExecuteSearch(comm);
            if (dtStandard == null || dtStandard.Rows.Count < 1) return false;
            else return true;
        }
        #endregion
        #region 根据员工ID获取所在部门名称
        /// <summary>
        /// 根据员工ID获取所在部门名称
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static string GetNameByDeptID(string deptID, string companyCD)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select DeptName from officedba.DeptInfo where  CompanyCD=@CompanyCD and ID=@DeptID ");
            #endregion
            
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = SqlHelper.GetParameter("@DeptID", deptID);
            parms[1] = SqlHelper.GetParameter("@CompanyCD", companyCD );
            object obj = SqlHelper.ExecuteScalar(searchSql .ToString (), parms);
            return Convert.ToString (obj);

        }
        #endregion

        #region 删除工资标准信息
        /// <summary>
        /// 删除工资标准信息
        /// </summary>
        /// <param name="standardID">工资标准ID</param>
        /// <returns></returns>
        public static bool DeleteSalaryStandard(string standardID)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.SalaryStandard ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" ID = @StandardID");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@StandardID", standardID));

            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion
        public static bool DeleteAllSalaryInfo(string standardID, string CompanyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.SalaryStandard ");
            deleteSql.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "'");
            deleteSql.AppendLine("and  ID IN (" + standardID + ")");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #region 新建工资标准信息
        /// <summary>
        /// 新建工资标准信息 
        /// </summary>
        /// <param name="model">工资标准信息</param>
        /// <returns></returns>
        public static bool InsertSalaryStandard(SalaryStandardModel model)
        {
            #region 登陆SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO             ");
            insertSql.AppendLine(" officedba.SalaryStandard");
            insertSql.AppendLine(" 	(CompanyCD             ");
            insertSql.AppendLine(" 	,QuarterID             ");
            insertSql.AppendLine(" 	,AdminLevel            ");
            insertSql.AppendLine(" 	,ItemNo                ");
            insertSql.AppendLine(" 	,UnitPrice             ");
            insertSql.AppendLine(" 	,Remark                ");
            insertSql.AppendLine(" 	,UsedStatus)           ");
            insertSql.AppendLine(" VALUES                  ");
            insertSql.AppendLine(" 	(@CompanyCD            ");
            insertSql.AppendLine(" 	,@QuarterID            ");
            insertSql.AppendLine(" 	,@AdminLevel           ");
            insertSql.AppendLine(" 	,@ItemNo               ");
            insertSql.AppendLine(" 	,@UnitPrice            ");
            insertSql.AppendLine(" 	,@Remark               ");
            insertSql.AppendLine(" 	,@UsedStatus)          ");
            insertSql.AppendLine("   SET @StandardID= @@IDENTITY  ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@StandardID", SqlDbType.Int));

            //执行登陆操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
            //设置ID
            model.ID = comm.Parameters["@StandardID"].Value.ToString();

            //执行插入并返回插入结果
            return isSucc;
        }
        #endregion

        #region 更新工资标准信息
        /// <summary>
        /// 更新工资标准信息
        /// </summary>
        /// <param name="model">保存信息</param>
        /// <returns></returns>
        public static bool UpdateSalaryStandard(SalaryStandardModel model)
        {

            #region 更新SQL拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.SalaryStandard ");
            updateSql.AppendLine(" SET CompanyCD = @CompanyCD      ");
            updateSql.AppendLine("   ,QuarterID = @QuarterID       ");
            updateSql.AppendLine("   ,AdminLevel = @AdminLevel     ");
            updateSql.AppendLine("   ,ItemNo = @ItemNo             ");
            updateSql.AppendLine("   ,UnitPrice = @UnitPrice       ");
            updateSql.AppendLine("   ,Remark = @Remark             ");
            updateSql.AppendLine("   ,UsedStatus = @UsedStatus     ");
            updateSql.AppendLine(" WHERE                           ");
            updateSql.AppendLine(" 	ID = @StandardID               ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);
            //ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StandardID", model.ID));

            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">保存信息</param>
        private static void SetSaveParameter(SqlCommand comm, SalaryStandardModel model)
        {
            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));//岗位ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdminLevel", model.AdminLevel));//岗位职等
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemNo", model.ItemNo));//工资项编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitPrice", model.UnitPrice));//金额
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));//启用状态
        }
        #endregion
        #region 通过检索条件查询工资标准以显示报表
        /// <summary>
        /// 查询工资标准信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchSalaryStandardReport(SalaryStandardModel model)
        {
           
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                     ");
            searchSql.AppendLine(" 	 ISNULL(A.QuarterID, '') AS QuarterID     ,ISNULL(A.AdminLevel, '') AS AdminLevel   ,ISNULL(D.ItemName, '') AS CompanyCD   ,ISNULL(C.TypeName, '') AS ItemNo  ,sum(A.UnitPrice)AS UnitPrice , ISNULL( b.QuarterName, '') AS Remark ");
            searchSql.AppendLine(" FROM                                       ");
            searchSql.AppendLine(" 	officedba.SalaryStandard A                ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter B         ");
            searchSql.AppendLine(" 		ON B.companyCD=A.companyCD AND B.ID = A.QuarterID                 ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType C      ");
            searchSql.AppendLine(" 		ON C.companyCD=A.companyCD AND C.ID = A.AdminLevel                ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.SalaryItem D          ");
            searchSql.AppendLine(" 		ON D.companyCD=A.companyCD AND D.ItemNo = A.ItemNo                ");
            searchSql.AppendLine(" WHERE                                      ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                  ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();

            #region 设置参数
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //岗位ID
            if (!string.IsNullOrEmpty(model.QuarterID))
            {
                searchSql.AppendLine("	AND A.QuarterID = @QuarterID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));
            }
            //岗位职等
            if (!string.IsNullOrEmpty(model.AdminLevel))
            {
                searchSql.AppendLine("	AND A.AdminLevel = @AdminLevel ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdminLevel", model.AdminLevel));
            }
            //启用状态
            if (!string.IsNullOrEmpty(model.UsedStatus))
            {
                searchSql.AppendLine("	AND A.UsedStatus = @UsedStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));
            }
            #endregion
            searchSql.AppendLine("	group by A.QuarterID,A.AdminLevel,A.ItemNo,D.ItemName ,C.TypeName,b.QuarterName");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        #region 通过检索条件查询生成工资明细报表
        /// <summary>
        /// 查询工资标准信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchSalaryDetailsReport(SalaryStandardModel model)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("  select  a.ReprotNo,c.DeptID ,Substring(a.ReportMonth, 1, 4) + '年'+ Substring(a.ReportMonth, 5, 2) + '月' as ReportMonth ,b.EmployeeID from officedba.SalaryReport a left outer join officedba. SalaryReportSummary  b on  b.companyCD=a.companyCD and a.ReprotNo =b.ReprotNo   left outer join officedba.EmployeeInfo c   on c.companyCD=a.companyCD and   b.EmployeeID=C.ID left outer join officedba.DeptInfo d on  d.companyCD=a.companyCD and c.DeptID=d.ID  where a. CompanyCD=@CompanyCD  and    a.Status='3'                        ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();

            #region 设置参数
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //岗位ID
            if (!string.IsNullOrEmpty(model.QuarterID))
            {
                searchSql.AppendLine("	AND c.DeptID = @DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.QuarterID));
            }
            if (!string.IsNullOrEmpty(model.AdminLevel))//开始月份
            {
                searchSql.AppendLine("	AND Substring(a.ReportMonth, 5, 2)>=@StartMonth ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartMonth", model.AdminLevel));
            }
            if (!string.IsNullOrEmpty(model.AdminLevelName))//结束月份
            {
                searchSql.AppendLine("	AND Substring(a.ReportMonth, 5, 2)<=@EndMonth");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndMonth", model.AdminLevelName));
            }

            if (!string.IsNullOrEmpty(model.UnitPrice))//年度
            {
                searchSql.AppendLine("	AND Substring(a.ReportMonth, 1, 4)=@year");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@year", model.UnitPrice));
            }


            if (!string.IsNullOrEmpty(model.UsedStatus))//人员
            {
                searchSql.AppendLine("	AND b.EmployeeID=@EmployeeID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.UsedStatus));
            }
            

            //岗位职等
            //if (!string.IsNullOrEmpty(model.AdminLevel))
            //{
            //    searchSql.AppendLine("	AND A.AdminLevel = @AdminLevel ");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdminLevel", model.AdminLevel));
            //}
         
            #endregion
            searchSql.AppendLine(" 	 group by c.DeptID,a.ReportMonth,b.EmployeeID ,a.ReprotNo      ");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        #region 查询产生工资汇总报表信息
        /// <summary>
        /// 查询工资标准信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchSalarySummaryReport(SalaryStandardModel model)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select c.DeptID as Remark,Substring(a.ReportMonth, 1, 4) + '年'+ Substring(a.ReportMonth, 5, 2) + '月' as itemNo,count( distinct(b.EmployeeID)) as CompanyCD,sum(b.SalaryMoney) as UnitPrice                                    ");
            searchSql.AppendLine(" from officedba.SalaryReport a left outer join officedba. SalaryReportSummary  b ");
            searchSql.AppendLine(" on b.companyCD=a.companyCD and a.ReprotNo =b.ReprotNo                                   ");
            searchSql.AppendLine(" 	left outer join officedba.EmployeeInfo c             ");
            searchSql.AppendLine(" 	on  c.companyCD=a.companyCD and b.EmployeeID=C.ID         ");
            searchSql.AppendLine(" 	left outer join officedba.DeptInfo d                ");
            searchSql.AppendLine(" on d.companyCD=a.companyCD and c.DeptID=d.ID    ");
            searchSql.AppendLine(" 	where   a. CompanyCD=@CompanyCD   and     a.Status='3'      ");
          
            #endregion

         

            //定义查询的命令
            SqlCommand comm = new SqlCommand();

            #region 设置参数
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //岗位ID
            if (!string.IsNullOrEmpty(model.QuarterID))
            {
                searchSql.AppendLine("	AND c.DeptID = @DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.QuarterID));
            }
            if (!string.IsNullOrEmpty(model.AdminLevel))//开始月份
            {
                searchSql.AppendLine("	AND Substring(a.ReportMonth, 5, 2)>=@StartMonth ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartMonth", model.AdminLevel));
            }
            if (!string.IsNullOrEmpty(model.AdminLevelName))//结束月份
            {
                searchSql.AppendLine("	AND Substring(a.ReportMonth, 5, 2)<=@EndMonth");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndMonth", model.AdminLevelName));
            }
            if (!string.IsNullOrEmpty(model.UnitPrice))//年度
            {
                searchSql.AppendLine("	AND Substring(a.ReportMonth, 1, 4)=@year");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@year", model.UnitPrice));
            }
            //岗位职等
            //if (!string.IsNullOrEmpty(model.AdminLevel))
            //{
            //    searchSql.AppendLine("	AND A.AdminLevel = @AdminLevel ");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdminLevel", model.AdminLevel));
            //}
         
            #endregion
            searchSql.AppendLine(" 	group by c.DeptID,a.ReportMonth        ");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        #region 通过员工ID产生返回员工计时金额
        /// <summary>
        /// 通过员工ID产生返回工资信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable GetEmployeeDetailsOutTime(string employeeID, string DeptID, string comanyCD, string ReprotNo)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select b.DeptID, c.DeptName,b.EmployeeName, a.TimeMoney ");
            searchSql.AppendLine(" from officedba.SalaryReport e left outer join");
            searchSql.AppendLine("  officedba. SalaryReportSummary a                                 ");
            searchSql.AppendLine(" on e.ReprotNo =a.ReprotNo            ");
            searchSql.AppendLine(" 	left outer join officedba.EmployeeInfo b         ");
            searchSql.AppendLine(" 	on b.companyCD=a.companyCD and a.EmployeeID=b.ID               ");
            searchSql.AppendLine(" left outer join officedba.DeptInfo c    ");
            searchSql.AppendLine(" 	on c.companyCD=a.companyCD and b.DeptID=c.ID            ");
            searchSql.AppendLine(" 	where e. CompanyCD=@CompanyCD  and a. ReprotNo=@ReprotNo and e.Status='3'   and  a.EmployeeID=@EmployeeID       ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();

            #region 设置参数
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",comanyCD ));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", employeeID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReprotNo", ReprotNo ));
            //部门ID
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine("	AND b.DeptID = @DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID  ));
            }

            #endregion
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        #region 通过员工ID产生返回员工计件金额
        /// <summary>
        /// 通过员工ID产生返回工资信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable GetEmployeeDetailsOutPiece(string employeeID, string DeptID, string comanyCD, string ReprotNo)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select b.DeptID, c.DeptName,b.EmployeeName, a.WorkMoney ");
            searchSql.AppendLine(" from officedba.SalaryReport e left outer join");
            searchSql.AppendLine("  officedba. SalaryReportSummary a                                 ");
            searchSql.AppendLine(" on a.companyCD=e.companyCD and e.ReprotNo =a.ReprotNo            ");
            searchSql.AppendLine(" 	left outer join officedba.EmployeeInfo b         ");
            searchSql.AppendLine(" 	on b.companyCD=e.companyCD and a.EmployeeID=b.ID               ");
            searchSql.AppendLine(" left outer join officedba.DeptInfo c    ");
            searchSql.AppendLine(" 	on c.companyCD=e.companyCD and  b.DeptID=c.ID            ");
            searchSql.AppendLine(" 	where e. CompanyCD=@CompanyCD and e.ReprotNo=@ReprotNo   and e.Status='3'  and a.EmployeeID=@EmployeeID      ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();

            #region 设置参数
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",comanyCD ));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", employeeID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReprotNo", ReprotNo ));
            //部门ID
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine("	AND b.DeptID = @DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID  ));
            }

            #endregion
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 返回已确认的报表的部门ID序列
        /// <summary>
        /// 返回已确认的报表的部门ID序列
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable GetDeptInfo(string comanyCD)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select distinct(c.DeptID)   from officedba.SalaryReport a left outer join officedba. SalaryReportSummary  b  ");
            searchSql.AppendLine(" on b.companyCD=a.companyCD and a.ReprotNo =b.ReprotNo");
            searchSql.AppendLine("  left outer join officedba.EmployeeInfo c                                ");
            searchSql.AppendLine(" on c.companyCD=a.companyCD and   b.EmployeeID=C.ID            ");
            searchSql.AppendLine(" 	where a. CompanyCD=@CompanyCD and a.Status='3'      ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();

            #region 设置参数
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", comanyCD));
           

            #endregion
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
           #region 通过月份和年度和部门ID产生返回实发工资人数、实发工资合计
        /// <summary>
        /// 通过月份和年度和部门ID产生返回实发工资人数、实发工资合计
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable GetMonthlyInfo(string year, string deptID, string month,string comanyCD)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("  select c.DeptID as Remark,Substring(a.ReportMonth, 1, 4) + '年'+ Substring(a.ReportMonth, 5, 2) + '月' as itemNo,count( distinct(b.EmployeeID)) as CompanyCD,sum(b.SalaryMoney) as UnitPrice  ");
            searchSql.AppendLine(" from officedba.SalaryReport a left outer join officedba. SalaryReportSummary  b ");
            searchSql.AppendLine("  on  b.companyCD=a.companyCD and a.ReprotNo =b.ReprotNo                                 ");
            searchSql.AppendLine(" left outer join officedba.EmployeeInfo c            ");
            searchSql.AppendLine(" 	on c.companyCD=a.companyCD and  b.EmployeeID=C.ID         ");
            searchSql.AppendLine(" left outer join officedba.DeptInfo d                 ");
            searchSql.AppendLine("on d.companyCD=a.companyCD and c.DeptID=d.ID     ");
            searchSql.AppendLine(" 	where  a. CompanyCD=@CompanyCD and  a.Status='3'  and Substring(a.ReportMonth, 1, 4)=@Year and c.DeptID=@DeptID and Substring(a.ReportMonth, 5, 2)=@Month           ");
            searchSql.AppendLine("group by c.DeptID,a.ReportMonth        ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();

            #region 设置参数
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", comanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Year", year ));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", deptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Month", month ));

            #endregion
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        #region 通过员工ID产生返回工资信息
        /// <summary>
        /// 通过员工ID产生返回工资信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable GetEmployeeDetails(string employeeID, string DeptID, string comanyCD, string ReprotNo)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select b.DeptID, a.DeptName,a.EmployeeID,a.EmployeeName, a. AdminLevelName ,a.QuarterName , a.AllGetMoney,a.AllKillMoney,a.SalaryMoney  ");
            searchSql.AppendLine(" from officedba.SalaryReport e left outer join");
            searchSql.AppendLine("  officedba. SalaryReportSummary a                                 ");
            searchSql.AppendLine(" on  a.companyCD=e.companyCD and e.ReprotNo =a.ReprotNo            ");
            searchSql.AppendLine(" 	left outer join officedba.EmployeeInfo b         ");
            searchSql.AppendLine(" 	on b.companyCD=e.companyCD and a.EmployeeID=b.ID               ");
            searchSql.AppendLine(" left outer join officedba.DeptInfo c    ");
            searchSql.AppendLine(" 	on c.companyCD=e.companyCD and b.DeptID=c.ID            ");
            searchSql.AppendLine("left outer join officedba.DeptQuarter d        ");
            searchSql.AppendLine(" on d.companyCD=e.companyCD and  b.QuarterID=d.ID              ");
            searchSql.AppendLine(" LEFT JOIN officedba.CodePublicType f   ");
            searchSql.AppendLine(" 	ON f.companyCD=e.companyCD and  f.ID = b.AdminLevelID           ");
            searchSql.AppendLine(" 	where e. CompanyCD=@CompanyCD and a.ReprotNo=@ReprotNo  and   a.EmployeeID=@EmployeeID  and e.Status='3'        ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();

            #region 设置参数
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",comanyCD ));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", employeeID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReprotNo", ReprotNo));
            //部门ID
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine("	AND b.DeptID = @DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID  ));
            }

            #endregion
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
    }
}
