/**********************************************
 * 类作用：   新建面试
 * 建立人：   吴志强
 * 建立时间： 2009/04/02
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
    /// 类名：RectInterviewDBHelper
    /// 描述：新建招聘活动
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/02
    /// 最后修改时间：2009/04/02
    /// </summary>
    ///
    public class RectInterviewDBHelper
    {

        #region 获取公司的招聘活动
        /// <summary>
        /// 获取公司的招聘活动
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetRectPlanInfo(string companyCD)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                      ");
            searchSql.AppendLine(" 	PlanNo                      ");
            searchSql.AppendLine("     ,Title                  ");
            searchSql.AppendLine(" FROM                        ");
            searchSql.AppendLine(" 	officedba.RectPlan         ");
            searchSql.AppendLine(" WHERE                       ");
            searchSql.AppendLine(" 	 CompanyCD = @CompanyCD ");

            //searchSql.AppendLine(" 	Status = @Status           ");
            //searchSql.AppendLine(" 	AND CompanyCD = @CompanyCD ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //状态
            //param[1] = SqlHelper.GetParameter("@Status", "1");
            //执行查询并返回值
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 获取人才储备信息
        /// <summary>
        /// 获取人才储备信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetReserveInfo(EmployeeSearchModel model)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT                                        ");
            searchSql.AppendLine("	 A.ID                                       ");
            searchSql.AppendLine("	,A.EmployeeNo                               ");
            searchSql.AppendLine("	,A.EmployeeName                             ");
            searchSql.AppendLine("	,A.ModifiedDate                             ");
            searchSql.AppendLine("	,ISNULl(B.QuarterName, '') AS QuarterName   ");
            searchSql.AppendLine("	,ISNULl(C.TypeName, '') AS ProfessionalName ");
            searchSql.AppendLine("	,ISNULl(D.TypeName, '') AS CultureLevelName ");
            searchSql.AppendLine("	,ISNULl(A.GraduateSchool, '') AS SchoolName ");
            searchSql.AppendLine("	,CASE                                       ");
            searchSql.AppendLine("	 WHEN A.TotalSeniority IS NULL              ");
            searchSql.AppendLine("	 THEN ''                                    ");
            searchSql.AppendLine("	 ELSE                                       ");
            searchSql.AppendLine("	  CONVERT(VARCHAR,A.TotalSeniority) + ' 年' ");
            searchSql.AppendLine("	 END                                        ");
            searchSql.AppendLine("	AS TotalSeniority                           ");
            searchSql.AppendLine("  ,Case A.Sex                                 ");
            searchSql.AppendLine("	 WHEN '1' then '男'                         ");
            searchSql.AppendLine("	 WHEN '2' then '女'                         ");
            searchSql.AppendLine("	 ELSE ''                                    ");
            searchSql.AppendLine("	End AS SexName                              ");
            searchSql.AppendLine("FROM                                          ");
            searchSql.AppendLine("	officedba.EmployeeInfo A                    ");
            searchSql.AppendLine("	LEFT JOIN officedba.DeptQuarter B           ");
            searchSql.AppendLine("		ON B.companyCD=A.companyCD AND A.QuarterID = B.ID                   ");
            searchSql.AppendLine("	LEFT JOIN officedba.CodePublicType C        ");
            searchSql.AppendLine("		ON C.companyCD=A.companyCD AND A.Professional = C.ID                ");
            searchSql.AppendLine("	LEFT JOIN officedba.CodePublicType D        ");
            searchSql.AppendLine("		ON D.companyCD=A.companyCD AND A.CultureLevel = D.ID                ");
            searchSql.AppendLine("WHERE                                         ");
            searchSql.AppendLine("	A.CompanyCD = @CompanyCD                    ");
            searchSql.AppendLine("	AND A.Flag = @Flag                          ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //人才储备
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", ConstUtil.EMPLOYEE_FLAG_TALENT));

            //编号
            if (!string.IsNullOrEmpty(model.EmployeeNo))
            {
                searchSql.AppendLine("	AND A.EmployeeNo LIKE '%' + @EmployeeNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", model.EmployeeNo));
            }
            //姓名 
            if (!string.IsNullOrEmpty(model.EmployeeName))
            {
                searchSql.AppendLine("	AND A.EmployeeName LIKE '%' + @EmployeeName + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeName", model.EmployeeName));
            }
            //应聘岗位
            if (!string.IsNullOrEmpty(model.QuarterID))
            {
                searchSql.AppendLine("	AND A.QuarterID = @QuarterID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));
            }
            //工作年限
            if (!string.IsNullOrEmpty(model.TotalSeniority))
            {
                searchSql.AppendLine("	AND A.TotalSeniority >= @TotalSeniority ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalSeniority", model.TotalSeniority));
            }
            //学历
            if (!string.IsNullOrEmpty(model.CultureLevel))
            {
                searchSql.AppendLine("	AND A.CultureLevel = @CultureLevel ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CultureLevel", model.CultureLevel));
            }
            //专业
            if (!string.IsNullOrEmpty(model.ProfessionalID))
            {
                searchSql.AppendLine("	AND A.Professional = @ProfessionalID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProfessionalID", model.ProfessionalID));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 获取模板的要素
        /// <summary>
        /// 获取模板的要素
        /// </summary>
        /// <param name="templateNo">模板编号</param>
        /// <returns></returns>
        public static DataTable GetCheckElemInfo(string templateNo, string companyCD)
        {  
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                               ");
            searchSql.AppendLine(" 	 A.CheckElemID AS ElemID            ");
            searchSql.AppendLine(" 	,A.MaxScore AS MaxScore             ");
            searchSql.AppendLine(" 	,A.Rate AS Rate                     ");
            searchSql.AppendLine(" 	,ISNULL(B.ElemName, '') AS ElemName ");
            searchSql.AppendLine(" FROM                                 ");
            searchSql.AppendLine(" 	officedba.RectCheckTemplateElem A   ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.RectCheckElem B ");
            searchSql.AppendLine(" 		ON  B. CompanyCD=A.CompanyCD and  A.CheckElemID = B.ID       ");
            searchSql.AppendLine(" WHERE                                ");     
            searchSql.AppendLine("  A.CompanyCD = @CompanyCD 	AND       ");
            searchSql.AppendLine(" 	A.TemplateNo = @TemplateNo          ");
      
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //模板编号
            param[0] = SqlHelper.GetParameter("@TemplateNo", templateNo);
            //公司代码
            param[1] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            //执行查询并返回值
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 获取岗位的面试模板
        /// <summary>
        /// 获取岗位的面试模板
        /// </summary>
        /// <param name="quarterID">岗位ID</param>
        /// <returns></returns>
        public static DataTable GetTemplateInfo(string quarterID, string companyCD)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" 		SELECT                          ");
            searchSql.AppendLine(" 			TemplateNo                  ");
            searchSql.AppendLine(" 			,Title                      ");
            searchSql.AppendLine(" 		FROM                            ");
            searchSql.AppendLine(" 			officedba.RectCheckTemplate ");
            searchSql.AppendLine(" 		WHERE                           ");  
            searchSql.AppendLine(" 	 CompanyCD = @CompanyCD		AND  ");
            searchSql.AppendLine(" 			QuarterID = @QuarterID      ");
        
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //模板编号
            param[0] = SqlHelper.GetParameter("@QuarterID", quarterID);
            //公司代码
            param[1] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            //执行查询并返回值
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion
        public static DataTable GetQuterInfo(string planNo, string companyCD)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" 		SELECT                          ");
            searchSql.AppendLine(" 			PositionID                  ");
            searchSql.AppendLine(" 			,PositionTitle                      ");
            searchSql.AppendLine(" 		FROM                            ");
            searchSql.AppendLine(" 			officedba. RectGoal ");
            searchSql.AppendLine(" 		WHERE                           ");    
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD		AND   ");
            searchSql.AppendLine(" 			planNo = @planNo      ");
        
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //模板编号
            param[0] = SqlHelper.GetParameter("@planNo", planNo );
            //公司代码
            param[1] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            //执行查询并返回值
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #region 获取公司的人才代理信息
        /// <summary>
        /// 获取公司的人才代理信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetProxyInfo(string companyCD)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                    ");
            searchSql.AppendLine(" 	ID                       ");
            searchSql.AppendLine(" 	,ProxyCompanyCD          ");
            searchSql.AppendLine("     ,ProxyCompanyName     ");
            searchSql.AppendLine(" FROM                      ");
            searchSql.AppendLine(" 	officedba.HRProxy        ");
            searchSql.AppendLine(" WHERE                     "); 
            searchSql.AppendLine(" CompanyCD = @CompanyCD	AND  ");
            searchSql.AppendLine(" 	UsedStatus = @UsedStatus ");
          
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //启用状态
            param[1] = SqlHelper.GetParameter("@UsedStatus", ConstUtil.USED_STATUS_ON);
            //执行查询并返回值
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 通过ID查询面试记录信息
        /// <summary>
        /// 查询面试记录信息
        /// </summary>
        /// <param name="interviewID">面试记录ID</param>
        /// <returns></returns>
        public static DataSet GetInterviewInfoWithID(string interviewID, string companyCD)
        {
            //定义返回的数据变量
            DataSet dsReturnInfo = new DataSet();

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                               ");
            searchSql.AppendLine(" 	 A.ID                               ");
            searchSql.AppendLine(" 	,A.CompanyCD                        ");
            searchSql.AppendLine(" 	,A.PlanID                           ");
            searchSql.AppendLine(" 	,A.InterviewNo                      ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.InterviewDate,21),'') ");
            searchSql.AppendLine(" 		AS InterviewDate                                ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.CheckDate,21),'') ");
            searchSql.AppendLine(" 		AS CheckDate                                ");
            searchSql.AppendLine(" 	,  isnull(A.StaffName,'') as   StaffName                      ");
            searchSql.AppendLine(" 	,B.EmployeeName AS StaffNameDisplay ");
            searchSql.AppendLine(" 	,A.QuarterID                        ");
            searchSql.AppendLine(" 	,A.RectType                         ");
            searchSql.AppendLine(" 	,A.InterviewType                        ");
            searchSql.AppendLine(" 	, isnull(A.InterviewPlace,'') as   InterviewPlace                      ");
            searchSql.AppendLine(" 	,isnull(A.InterviewNote,'') as  InterviewNote                      ");
            searchSql.AppendLine(" 	,A.TestScore                        ");
            searchSql.AppendLine(" 	,A.InterviewResult                   ");
            searchSql.AppendLine(" 	,A.CheckType                     ");
            searchSql.AppendLine(" 	,isnull(A.CheckPlace  ,'') as  CheckPlace                     ");
            searchSql.AppendLine(" 	,isnull(A.CheckNote,'') as  CheckNote                    ");
            searchSql.AppendLine(" 	,isnull(A.ManNote,'') as  ManNote                           ");
            searchSql.AppendLine(" 	,isnull(A.KnowNote  ,'') as KnowNote                      ");
            searchSql.AppendLine(" 	,isnull(A.WorkNote ,'') as   WorkNote                     ");
            searchSql.AppendLine(" 	,isnull(A.SalaryNote  ,'') as  SalaryNote                     ");
            searchSql.AppendLine(" 	,isnull(A.OurSalary  ,'') as OurSalary                      ");
            searchSql.AppendLine(" 	,isnull(A.FinalSalary ,'') as FinalSalary                     ");
            searchSql.AppendLine(" 	,isnull(A.OtherNote  ,'') as OtherNote                     ");
            searchSql.AppendLine(" 	,A.FinalResult                         ");
            searchSql.AppendLine(" 	,isnull(A.Remark,'') as   Remark                            ");
            searchSql.AppendLine(" 	,isnull(A.Attachment   ,'') as   Attachment                     ");
            searchSql.AppendLine(" 	,A.TemplateNo                   ");
            searchSql.AppendLine(" 	,isnull(A.AttachmentName ,'') as  AttachmentName                  ");
            searchSql.AppendLine(" FROM                                 ");
            searchSql.AppendLine(" 	officedba.RectInterview A           ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B  ");
            searchSql.AppendLine(" 		ON B.companyCD=A.companyCD AND  A.StaffName = B.ID           ");
            searchSql.AppendLine(" WHERE                                ");
            searchSql.AppendLine(" 	 A.CompanyCD=@CompanyCD  and  A.ID = @InterviewID             ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //面试记录ID
            param[0] = SqlHelper.GetParameter("@InterviewID", interviewID);
            param[1] = SqlHelper.GetParameter("@CompanyCD", companyCD );
            //执行查询
            DataTable dtBaseInfo = SqlHelper.ExecuteSql(searchSql.ToString(), param);
            //添加面试记录基本信息
            dsReturnInfo.Tables.Add(dtBaseInfo);
            //基本信息存在时
            if (dtBaseInfo != null && dtBaseInfo.Rows.Count > 0)
            {
                //公司代码
                string companyCD2 = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "CompanyCD");
                //面试记录编号
                string interviewNo = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "InterviewNo");
                //岗位ID
                string quarterID = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "QuarterID");

                //添加要素得分信息
                dsReturnInfo.Tables.Add(GetElemScoreInfo(companyCD2, interviewNo, quarterID));
            }

            return dsReturnInfo;
        }
        #endregion

        #region 查询要素得分信息
        /// <summary>
        /// 查询要素得分信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="interviewNo">面试记录编号</param>
        /// <param name="quarterID">岗位ID</param>
        /// <returns></returns>
        public static DataTable GetElemScoreInfo(string companyCD, string interviewNo, string quarterID)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                       ");
            searchSql.AppendLine(" 	 A.CheckElemID AS ElemID                    ");
            searchSql.AppendLine(" 	,A.RealScore AS RealScore                   ");
            searchSql.AppendLine(" 	,A.Remark AS Remark                         ");
            searchSql.AppendLine(" 	,B.MaxScore AS MaxScore                     ");
            searchSql.AppendLine(" 	,B.Rate AS Rate                             ");
            searchSql.AppendLine(" 	,C.ElemName AS ElemName                     ");
            searchSql.AppendLine(" FROM                                         ");
            searchSql.AppendLine(" 	officedba.RectInterviewDetail A             ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.RectCheckTemplateElem B ");
            searchSql.AppendLine(" 	ON  A. CompanyCD=B.CompanyCD  and   A.CheckElemID = B.CheckElemID         ");
            searchSql.AppendLine(" 		AND B.TemplateNo = (                    ");
            searchSql.AppendLine(" 			SELECT                              ");
            searchSql.AppendLine(" 				TOP 1 TemplateNo                ");
            searchSql.AppendLine(" 			FROM                                ");
            searchSql.AppendLine(" 				officedba.RectInterview     ");
            searchSql.AppendLine(" 			WHERE                               ");
            searchSql.AppendLine(" 				   CompanyCD = @CompanyCD      ");
            searchSql.AppendLine(" 				AND QuarterID = @QuarterID AND InterviewNo = @InterviewNo )     ");
            searchSql.AppendLine(" LEFT JOIN officedba.RectCheckElem C          ");
            searchSql.AppendLine(" 	ON  C.CompanyCD=A.CompanyCD    and     A.CheckElemID = C.ID             ");
            searchSql.AppendLine(" WHERE                                        ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                    ");
            searchSql.AppendLine(" 	AND A.InterviewNo = @InterviewNo            ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[3];
            //岗位ID
            param[0] = SqlHelper.GetParameter("@QuarterID", quarterID);
            //公司代码
            param[1] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //面试记录编号
            param[2] = SqlHelper.GetParameter("@InterviewNo", interviewNo);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        public static DataTable SearchInterviewCSInfo(RectInterviewModel model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                               ");
            searchSql.AppendLine(" 	 A.ID                                               ");
            searchSql.AppendLine(" 	,A.InterviewNo                                      ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.InterviewDate,21),'') ");
            searchSql.AppendLine(" 		AS InterviewDate                                ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.CheckDate,21),'') ");
            searchSql.AppendLine(" 		AS CheckDate                                ");
            searchSql.AppendLine(" 	,ISNULL(C.EmployeeName, '') AS StaffName            ");
            searchSql.AppendLine(" 	,ISNULL(B.QuarterName, '') AS QuarterName           ");
            searchSql.AppendLine(" 	,isnull(d.Title,'') as PlanName           ");
            searchSql.AppendLine(" 	,CASE A.InterviewResult   WHEN '1' THEN '列入考虑'  WHEN '2' THEN '不予考虑'  ELSE '' END	AS InterviewResult     ");
            searchSql.AppendLine(" 	,CASE A.FinalResult  WHEN '0' THEN '不予考虑'  WHEN '1' THEN '拟予试用'  ELSE ''  	END	 AS FinalResult                               ");
            searchSql.AppendLine(" 	,CASE A.RectType  WHEN '1' THEN '公开招聘'  WHEN '2' THEN '推荐'      WHEN '3' THEN '内部竞聘'  ELSE ''	END 	AS RectType ,isnull( Convert(varchar(100),A.ModifiedDate,23),'') AS ModifiedDate                    "); 
            searchSql.AppendLine(" FROM                                                 ");
            searchSql.AppendLine(" 	officedba.RectInterview A                           ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter B                   ");
            searchSql.AppendLine(" 		ON B.companyCD=A.companyCD AND A.QuarterID = B.ID                           ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo C                  ");
            searchSql.AppendLine(" 		ON C.companyCD=A.companyCD AND A.StaffName = C.ID                           ");
            searchSql.AppendLine(" 	left outer join  officedba.RectPlan D                 ");
            searchSql.AppendLine(" 	 on    D.CompanyCD=A.CompanyCD   and    a.planID=D.PlanNo               ");
            searchSql.AppendLine(" WHERE                                                ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                            ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //编号
            if (!string.IsNullOrEmpty(model.InterviewNo))
            {
                
                searchSql.AppendLine("	AND A.InterviewNo LIKE '%' + @InterviewNo + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewNo", model.InterviewNo));
            }
            //初试日期
            if (!string.IsNullOrEmpty(model.InterviewDate))
            {
                searchSql.AppendLine("	AND A.InterviewDate >= @InterviewDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewDate", model.InterviewDate));
            }
            if (!string.IsNullOrEmpty(model.Attachment))
            {
                searchSql.AppendLine("	AND A.InterviewDate <= @InterviewToDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewToDate", model.Attachment));
            }

       

            if (!string.IsNullOrEmpty(model.PlanID))
            {
                searchSql.AppendLine("	AND A.PlanID = @PlanID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanID", model.PlanID));
            }
            if (!string.IsNullOrEmpty(model.RectType))
            {
                searchSql.AppendLine("	AND A.RectType= @RectType ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RectType", model.RectType));
            }







            //复试日期
            if (!string.IsNullOrEmpty(model.CheckDate))
            {
                searchSql.AppendLine("	AND A.CheckDate >= @CheckDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckDate", model.CheckDate));
            }
            if (!string.IsNullOrEmpty(model.CheckNote))
            {
                searchSql.AppendLine("	AND A.CheckDate <= @CheckNote ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckNote", model.CheckNote));
            }


            //应聘岗位
            if (!string.IsNullOrEmpty(model.QuarterID))
            {
                searchSql.AppendLine("	AND A.QuarterID = @QuarterID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));
            }
            //姓名 
            if (!string.IsNullOrEmpty(model.StaffName))
            {
                searchSql.AppendLine("	AND C.EmployeeName   LIKE  '%' + @StaffName + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StaffName",model.StaffName));
            }
            //////人力资源
            if (!string.IsNullOrEmpty(model.InterviewResult))
            {
                searchSql.AppendLine("	AND A.InterviewResult = @InterviewResult ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewResult", model.InterviewResult));
            }
            //部门主管
            //if (!string.IsNullOrEmpty(model.DepartmentResult))
            //{
            //    searchSql.AppendLine("	AND A.DepartmentResult = @DepartmentResult ");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DepartmentResult", model.DepartmentResult));
            //}
            //最终结果
            if (!string.IsNullOrEmpty(model.FinalResult))
            {
                searchSql.AppendLine("	AND A.FinalResult = @FinalResult ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FinalResult", model.FinalResult));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount); //执行查询
        }
        #region 通过检索条件查询面试记录信息
        /// <summary>
        /// 查询面试记录信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        /// 
        public static DataTable SearchInterviewInfo(RectInterviewModel model)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                               ");
            searchSql.AppendLine(" 	 A.ID                                               ");
            searchSql.AppendLine(" 	,A.InterviewNo                                      ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.InterviewDate,21),'') ");
            searchSql.AppendLine(" 		AS InterviewDate                                ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.CheckDate,21),'') ");
            searchSql.AppendLine(" 		AS CheckDate                                ");
            searchSql.AppendLine(" 	,ISNULL(C.EmployeeName, '') AS StaffName            ");
            searchSql.AppendLine(" 	,ISNULL(B.QuarterName, '') AS QuarterName           ");
            searchSql.AppendLine(" 	,isnull(d.Title,'') as PlanName           ");
            searchSql.AppendLine(" 	,CASE A.InterviewResult   WHEN '1' THEN '列入考虑'  WHEN '2' THEN '不予考虑'  ELSE '' END	AS InterviewResult     ");
            searchSql.AppendLine(" 	,CASE A.FinalResult  WHEN '0' THEN '不予考虑'  WHEN '1' THEN '拟予试用'  ELSE ''  	END	 AS FinalResult                               ");
            searchSql.AppendLine(" 	,CASE A.RectType  WHEN '1' THEN '公开招聘'  WHEN '2' THEN '推荐'      WHEN '3' THEN '内部竞聘'  ELSE ''	END 	AS RectType ,isnull( Convert(varchar(100),A.ModifiedDate,23),'') AS ModifiedDate                    "); 
            searchSql.AppendLine(" FROM                                                 ");
            searchSql.AppendLine(" 	officedba.RectInterview A                           ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter B                   ");
            searchSql.AppendLine(" 		ON B.companyCD=A.companyCD AND A.QuarterID = B.ID                           ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo C                  ");
            searchSql.AppendLine(" 		ON C.companyCD=A.companyCD AND A.StaffName = C.ID                           ");
            searchSql.AppendLine(" 	left outer join  officedba.RectPlan D                 ");
            searchSql.AppendLine(" 	 on    D.CompanyCD=A.CompanyCD   and    a.planID=D.PlanNo               ");
            searchSql.AppendLine(" WHERE                                                ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                            ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //编号
            if (!string.IsNullOrEmpty(model.InterviewNo))
            {
                
                searchSql.AppendLine("	AND A.InterviewNo LIKE '%' + @InterviewNo + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewNo", model.InterviewNo));
            }
            //初试日期
            if (!string.IsNullOrEmpty(model.InterviewDate))
            {
                searchSql.AppendLine("	AND A.InterviewDate >= @InterviewDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewDate", model.InterviewDate));
            }
            if (!string.IsNullOrEmpty(model.Attachment))
            {
                searchSql.AppendLine("	AND A.InterviewDate <= @InterviewToDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewToDate", model.Attachment));
            }

       

            if (!string.IsNullOrEmpty(model.PlanID))
            {
                searchSql.AppendLine("	AND A.PlanID = @PlanID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanID", model.PlanID));
            }
            if (!string.IsNullOrEmpty(model.RectType))
            {
                searchSql.AppendLine("	AND A.RectType= @RectType ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RectType", model.RectType));
            }







            //复试日期
            if (!string.IsNullOrEmpty(model.CheckDate))
            {
                searchSql.AppendLine("	AND A.CheckDate >= @CheckDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckDate", model.CheckDate));
            }
            if (!string.IsNullOrEmpty(model.CheckNote))
            {
                searchSql.AppendLine("	AND A.CheckDate <= @CheckNote ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckNote", model.CheckNote));
            }


            //应聘岗位
            if (!string.IsNullOrEmpty(model.QuarterID))
            {
                searchSql.AppendLine("	AND A.QuarterID = @QuarterID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));
            }
            //姓名 
            if (!string.IsNullOrEmpty(model.StaffName))
            {
                searchSql.AppendLine("	AND C.EmployeeName   LIKE  '%' + @StaffName + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StaffName",model.StaffName));
            }
            //////人力资源
            if (!string.IsNullOrEmpty(model.InterviewResult))
            {
                searchSql.AppendLine("	AND A.InterviewResult = @InterviewResult ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewResult", model.InterviewResult));
            }
            //部门主管
            //if (!string.IsNullOrEmpty(model.DepartmentResult))
            //{
            //    searchSql.AppendLine("	AND A.DepartmentResult = @DepartmentResult ");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DepartmentResult", model.DepartmentResult));
            //}
            //最终结果
            if (!string.IsNullOrEmpty(model.FinalResult))
            {
                searchSql.AppendLine("	AND A.FinalResult = @FinalResult ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FinalResult", model.FinalResult));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 新建面试记录信息
        /// <summary>
        /// 新建面试记录信息 
        /// </summary>
        /// <param name="model">面试记录信息</param>
        /// <returns></returns>
        public static bool InsertInterviewInfo(RectInterviewModel model)
        {
            #region 登陆SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO officedba.RectInterview ");
            insertSql.AppendLine("            (CompanyCD               ");
            insertSql.AppendLine("            ,PlanID                  ");
            insertSql.AppendLine("            ,InterviewNo             ");
            insertSql.AppendLine("            ,InterviewDate           ");
            insertSql.AppendLine("            ,StaffName               ");
            insertSql.AppendLine("            ,QuarterID               ");
            insertSql.AppendLine("            ,RectType               ");
            insertSql.AppendLine("            ,TemplateNo              ");
            insertSql.AppendLine("            ,InterviewType                ");
            insertSql.AppendLine("            ,InterviewPlace               ");
            insertSql.AppendLine("            ,InterviewNote               ");
            insertSql.AppendLine("            ,TestScore           ");
            insertSql.AppendLine("            ,InterviewResult               ");
            insertSql.AppendLine("            ,CheckDate          ");
            insertSql.AppendLine("            ,CheckType            ");
            insertSql.AppendLine("            ,CheckPlace            ");
            insertSql.AppendLine("            ,CheckNote            ");
            insertSql.AppendLine("            ,ManNote                ");
            insertSql.AppendLine("            ,KnowNote                  ");
            insertSql.AppendLine("            ,WorkNote                 ");
            insertSql.AppendLine("            ,SalaryNote                    ");
            insertSql.AppendLine("            ,FinalResult         ");
            insertSql.AppendLine("            ,Remark             ");
            insertSql.AppendLine("            ,Attachment        ");
            insertSql.AppendLine("            ,ModifiedDate             ");
            insertSql.AppendLine("            ,ModifiedUserID                  ");
            insertSql.AppendLine("            ,OurSalary        ");
            insertSql.AppendLine("            ,FinalSalary             ");
            insertSql.AppendLine("            ,OtherNote                  ");
            insertSql.AppendLine("            ,AttachmentName  )            ");

            insertSql.AppendLine("      VALUES                         ");
            insertSql.AppendLine("            (@CompanyCD               ");
            insertSql.AppendLine("            ,@PlanID                  ");
            insertSql.AppendLine("            ,@InterviewNo             ");
            insertSql.AppendLine("            ,@InterviewDate           ");
            insertSql.AppendLine("            ,@StaffName               ");
            insertSql.AppendLine("            ,@QuarterID               ");
            insertSql.AppendLine("            ,@RectType               ");
            insertSql.AppendLine("            ,@TemplateNo              ");
            insertSql.AppendLine("            ,@InterviewType                ");
            insertSql.AppendLine("            ,@InterviewPlace               ");
            insertSql.AppendLine("            ,@InterviewNote               ");
            insertSql.AppendLine("            ,@TestScore           ");
            insertSql.AppendLine("            ,@InterviewResult               ");
            insertSql.AppendLine("            ,@CheckDate          ");
            insertSql.AppendLine("            ,@CheckType            ");
            insertSql.AppendLine("            ,@CheckPlace            ");
            insertSql.AppendLine("            ,@CheckNote            ");
            insertSql.AppendLine("            ,@ManNote                ");
            insertSql.AppendLine("            ,@KnowNote                  ");
            insertSql.AppendLine("            ,@WorkNote                 ");
            insertSql.AppendLine("            ,@SalaryNote                    ");
            insertSql.AppendLine("            ,@FinalResult         ");
            insertSql.AppendLine("            ,@Remark             ");
            insertSql.AppendLine("            ,@Attachment        ");
            insertSql.AppendLine("            ,getdate()             ");
            insertSql.AppendLine("            ,@ModifiedUserID                  ");
            insertSql.AppendLine("            ,@OurSalary        ");
            insertSql.AppendLine("            ,@FinalSalary             ");
            insertSql.AppendLine("            ,@OtherNote                  ");
            insertSql.AppendLine("            ,@AttachmentName  )            ");
            insertSql.AppendLine("   SET @InterviewID= @@IDENTITY      ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@InterviewID", SqlDbType.Int));
            ArrayList lstInsert = new ArrayList();
            lstInsert.Add(comm);
            //添加要素得分
            EditElemScoreInfo(lstInsert, model.ElemScoreList, model.InterviewNo, model.CompanyCD);
            //执行登陆操作
            bool isSuccess = SqlHelper.ExecuteTransWithArrayList(lstInsert);
            //获取插入后的命令
            SqlCommand commID = (SqlCommand)lstInsert[0];
            //设置ID
            model.ID = commID.Parameters["@InterviewID"].Value.ToString();

            //执行插入并返回插入结果
            return isSuccess;
        }
        #endregion

        #region 更新面试记录信息
        /// <summary>
        /// 更新面试记录信息
        /// </summary>
        /// <param name="model">面试记录信息</param>
        /// <returns></returns>
        public static bool UpdateInterviewInfo(RectInterviewModel model)
        {
            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.RectInterview         ");
            updateSql.AppendLine(" SET                                    ");
            updateSql.AppendLine(" 	 PlanID = @PlanID                     ");
            updateSql.AppendLine("            ,InterviewDate=@InterviewDate           ");
            updateSql.AppendLine("            ,StaffName=@StaffName               ");
            updateSql.AppendLine("            ,QuarterID=@QuarterID               ");
            updateSql.AppendLine("            ,RectType=@RectType               ");
            updateSql.AppendLine("            ,TemplateNo=@TemplateNo              ");
            updateSql.AppendLine("            ,InterviewType=@InterviewType                ");
            updateSql.AppendLine("            ,InterviewPlace=@InterviewPlace               ");
            updateSql.AppendLine("            ,InterviewNote=@InterviewNote               ");
            updateSql.AppendLine("            ,TestScore=@TestScore           ");
            updateSql.AppendLine("            ,InterviewResult=@InterviewResult               ");
            updateSql.AppendLine("            ,CheckDate=@CheckDate          ");
            updateSql.AppendLine("            ,CheckType=@CheckType            ");
            updateSql.AppendLine("            ,CheckPlace=@CheckPlace            ");
            updateSql.AppendLine("            ,CheckNote=@CheckNote            ");
            updateSql.AppendLine("            ,OurSalary =@OurSalary       ");
            updateSql.AppendLine("            ,FinalSalary=@FinalSalary             ");
            updateSql.AppendLine("            ,OtherNote=@OtherNote                  ");
            updateSql.AppendLine("            ,ManNote=@ManNote                ");
            updateSql.AppendLine("            ,KnowNote=@KnowNote                  ");
            updateSql.AppendLine("            ,WorkNote=@WorkNote                 ");
            updateSql.AppendLine("            ,SalaryNote=@SalaryNote                    ");
            updateSql.AppendLine("            ,FinalResult=@FinalResult         ");
            updateSql.AppendLine("            ,Remark=@Remark             ");
            updateSql.AppendLine("            ,Attachment=@Attachment        ");
            updateSql.AppendLine("            ,ModifiedDate=getdate()             ");
            updateSql.AppendLine("            ,ModifiedUserID=@ModifiedUserID                  ");
            updateSql.AppendLine("            ,AttachmentName=@AttachmentName             ");
            updateSql.AppendLine(" WHERE                                  ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD                ");
            updateSql.AppendLine(" 	AND InterviewNo = @InterviewNo        ");
            #endregion

            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //添加参数
            SetSaveParameter(comm, model);
            //定义变量
            ArrayList lstUpdate = new ArrayList();
            lstUpdate.Add(comm);
            //添加要素得分
            EditElemScoreInfo(lstUpdate, model.ElemScoreList, model.InterviewNo, model.CompanyCD);

            //执行更新并设置更新结果
            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);
        }
        #endregion

        #region 保存时参数设置
        /// <summary>
        /// 保存时参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">人才代理信息</param>
        private static void SetSaveParameter(SqlCommand comm, RectInterviewModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//企业代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanID", model.PlanID));//招聘计划ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewNo", model.InterviewNo));//面试记录编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewDate", model.InterviewDate));//面试日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StaffName", model.StaffName));//面试者
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));//应聘岗位ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemplateNo", model.TemplateNo));//消息渠道
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RectType", model.RectType ));//消息渠道
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewType", model.InterviewType ));//期望最高月薪
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewPlace", model.InterviewPlace ));//面试方式（对应系统分类代码表ID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewNote", model.InterviewNote ));//面试总成绩
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TestScore", model.TestScore ));//面试地点
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewResult", model.InterviewResult ));//面试状况(0未面试，1已面试)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckDate", model.CheckDate ));//录用状况(0未录用，1已录用，2不录用)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckType", model.CheckType ));//通知单状况(0未通知，1已通知，2不通知)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckPlace", model.CheckPlace ));//试用工资
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckNote", model.CheckNote ));//登记日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ManNote", model.ManNote ));//截止日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@KnowNote", model.KnowNote ));//面试记录
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@WorkNote", model.WorkNote ));//面试人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SalaryNote", model.SalaryNote ));//人力资源结果（0拒绝、1同意）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FinalResult", model.FinalResult ));//部门主管结果（0拒绝、1同意）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark ));//最终结果（0拒绝、1同意）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Attachment", model.PageAttachment  ));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID ));//附件
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AttachmentName", model.AttachmentName));//附件
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OurSalary", model.OurSalary));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FinalSalary", model.FinalSalary));//附件
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OtherNote", model.OtherNote));//附件
        }
        #endregion

        #region 登陆或更新要素得分信息
        /// <summary>
        /// 登陆或更新要素得分信息
        /// </summary>
        /// <param name="lstCommand">数据库操作命令列表</param>
        /// <param name="lstElemScore">要素得分信息</param>
        /// <param name="interviewNo">面试编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        private static void EditElemScoreInfo(ArrayList lstCommand, ArrayList lstElemScore, string interviewNo, string companyCD)
        {

            //全删全插方式插入招聘目标信息

            #region 删除操作
            //删除SQL
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.RectInterviewDetail ");
            deleteSql.AppendLine(" WHERE                          "); 
            deleteSql.AppendLine("CompanyCD = @CompanyCD     AND  ");
            deleteSql.AppendLine("      InterviewNo = @InterviewNo          ");
           
            //定义Command
            SqlCommand comm = new SqlCommand();
            //设置执行 Transact-SQL 语句
            comm.CommandText = deleteSql.ToString();
            //员工编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewNo", interviewNo));
            //公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //添加删除命令
            lstCommand.Add(comm);
            #endregion

            #region 插入操作
            //未填写招聘目标时，返回true
            if (lstElemScore != null || lstElemScore.Count > 0)
            {
                /* 插入操作 */
                #region 插入SQL文
                StringBuilder insertSql = new StringBuilder();
                insertSql.AppendLine(" INSERT INTO                   ");
                insertSql.AppendLine(" officedba.RectInterviewDetail ");
                insertSql.AppendLine(" 	(CompanyCD                   ");
                insertSql.AppendLine(" 	,InterviewNo                 ");
                insertSql.AppendLine(" 	,CheckElemID                 ");
                insertSql.AppendLine(" 	,RealScore                   ");
                insertSql.AppendLine(" 	,Remark)                     ");
                insertSql.AppendLine(" VALUES                        ");
                insertSql.AppendLine(" 	(@CompanyCD                  ");
                insertSql.AppendLine(" 	,@InterviewNo                ");
                insertSql.AppendLine(" 	,@CheckElemID                ");
                insertSql.AppendLine(" 	,@RealScore                  ");
                insertSql.AppendLine(" 	,@Remark)                    ");
                #endregion

                //遍历所有的要素信息
                for (int i = 0; i < lstElemScore.Count; i++)
                {
                    //获取单条目标记录
                    RectInterviewDetailModel model = (RectInterviewDetailModel)lstElemScore[i];
                    //定义Command
                    comm = new SqlCommand();
                    //设置执行 Transact-SQL 语句
                    comm.CommandText = insertSql.ToString();

                    #region 设置参数
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));//企业代码
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@InterviewNo", interviewNo));//面试记录编号
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckElemID", model.CheckElemID));//评测要素ID
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@RealScore", model.RealScore));//得分
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//备注
                    #endregion

                    //添加插入命令
                    lstCommand.Add(comm);

                }
            }
            #endregion

        }
        #endregion

        #region 删除面试记录信息
        /// <summary>
        /// 删除面试记录信息
        /// </summary>
        /// <param name="interviewNo">面试记录编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteInterviewInfo(string interviewNo, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.RectInterview ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" CompanyCD = @CompanyCD  AND  ");
            deleteSql.AppendLine(" InterviewNo In( " + interviewNo + ")");
            

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));

            //定义更新列表
            ArrayList lstDelete = new ArrayList();
            //添加基本信息更新命令
            lstDelete.Add(comm);
            //删除得分要素
            DeleteDetailInfo(lstDelete, companyCD, interviewNo);
            //执行删除并返回
            return SqlHelper.ExecuteTransWithArrayList(lstDelete);
        }
        #endregion

        #region 删除要素得分信息
        /// <summary>
        /// 删除要素得分信息
        /// </summary>
        /// <param name="lstCommand">命令列表</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="interviewNo">面试记录编号</param>
        /// <returns></returns>
        private static void DeleteDetailInfo(ArrayList lstCommand, string companyCD, string interviewNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.RectInterviewDetail ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" CompanyCD = @CompanyCD  AND  ");
            deleteSql.AppendLine(" InterviewNo In( " + interviewNo + " ) ");
            

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
            //添加命令
            lstCommand.Add(comm);
        }
        #endregion
    }
}
