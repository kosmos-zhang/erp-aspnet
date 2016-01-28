/**********************************************
 * 类作用：   新建招聘申请
 * 建立人：   吴志强
 * 建立时间： 2009/03/27
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
using XBase.Data.Common;
namespace XBase.Data.Office.HumanManager
{
    /// <summary>
    /// 类名：RectApplyDBHelper
    /// 描述：新建招聘申请
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/27
    /// 最后修改时间：2009/03/27
    /// </summary>
    ///
    public class RectApplyDBHelper
    {

        #region 通过ID查询招聘申请信息
        /// <summary>
        /// 查询招聘申请信息
        /// </summary>
        /// <param name="rectApplyID">招聘申请ID</param>
        /// <returns></returns>
        public static DataTable GetRectApplyInfoWithID(string rectApplyID, string companyCD)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                              ");
            searchSql.AppendLine(" 	 A.RectApplyNo                     ");
            searchSql.AppendLine(" 	,A.CompanyCD                       ");
            searchSql.AppendLine(" 	,A.DeptID                          ");
            searchSql.AppendLine(" 	,B.DeptName                        ");
            searchSql.AppendLine(" 	,A.MaxNum                           ");
            searchSql.AppendLine(" 	,A.NowNum    ");
            searchSql.AppendLine(" 	,A.RequireNum                       ");
            //searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.UsedDate,21) AS UsedDate ");
            searchSql.AppendLine(" 	,A.Principal                       ");
            searchSql.AppendLine(" 	,A.RequstReason                    ");
            searchSql.AppendLine(" 	,A.Remark                    ");
            searchSql.AppendLine(" 	,CASE A.BillStatus                                    ");
            searchSql.AppendLine(" 		WHEN '1' THEN '制单'                         ");
            searchSql.AppendLine(" 		WHEN '2' THEN '执行'                         ");
            searchSql.AppendLine(" 		WHEN '4' THEN '手工结单'                         ");
            searchSql.AppendLine(" 	END AS BillStatus                                 ");
            searchSql.AppendLine(" 	,ISNULL(C.EmployeeName,'') AS Creator     ");
            searchSql.AppendLine(" 	,ISNULL(D.EmployeeName,'') AS PrincipalName     ");
            searchSql.AppendLine(" 	,ISNULL(E.EmployeeName,'') AS Confirmor     ");
            searchSql.AppendLine(" 	,ISNULL(F.EmployeeName,'') AS Closer       ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.CreateDate,21) AS CreateDate      ");
            searchSql.AppendLine(" 	, CONVERT(VARCHAR(10),A.ConfirmDate,21) AS ConfirmDate       ");
            searchSql.AppendLine(" 	, CONVERT(VARCHAR(10),A.CloseDate,21) AS CloseDate    ");
            searchSql.AppendLine(" 	,CASE h.FlowStatus                                           ");
            searchSql.AppendLine(" 		WHEN '1' THEN '待审批'                                   ");
            searchSql.AppendLine(" 		WHEN '2' THEN '审批中'                                   ");
            searchSql.AppendLine(" 		WHEN '3' THEN '审批通过'                                 ");
            searchSql.AppendLine(" 		WHEN '4' THEN '审批不通过'                               ");
            searchSql.AppendLine(" 		WHEN '5' THEN '撤销审批'                               ");
            searchSql.AppendLine(" 		ELSE ' '                                            ");
            searchSql.AppendLine(" 	END AS FlowStatusName                                        ");
            searchSql.AppendLine(" FROM                                ");
            searchSql.AppendLine(" 	officedba.RectApply A              ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo B     ");
            searchSql.AppendLine(" 		ON  B.companyCD=A.companyCD AND A.DeptID = B.ID             ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo C ");
            searchSql.AppendLine(" 		ON C.companyCD=A.companyCD AND A.Creator = C.ID              ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo D ");
            searchSql.AppendLine(" 		ON D.companyCD=A.companyCD AND A.Principal = D.ID          ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo E ");
            searchSql.AppendLine(" 		ON E.companyCD=A.companyCD AND A.Confirmor = E.ID          ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo F ");
            searchSql.AppendLine(" 		ON F.companyCD=A.companyCD AND A.Closer = F.ID          ");
            searchSql.AppendLine(" 	LEFT JOIN (                                ");
            searchSql.AppendLine(" 			    SELECT                                           ");
            searchSql.AppendLine(" 			        MAX(E.id) ID,E.BillID,E.BillNo                            ");
            searchSql.AppendLine(" 			    FROM                                             ");
            searchSql.AppendLine(" 			        officedba.FlowInstance E,officedba.RectApply n                     ");
            searchSql.AppendLine(" 			    WHERE                                            ");
            searchSql.AppendLine(" 			        E.CompanyCD = n.CompanyCD                    ");
            searchSql.AppendLine(" 			        AND E.BillID = n.ID                     ");
            searchSql.AppendLine(" 			        AND E.BillTypeFlag = @BillTypeFlag           ");
            searchSql.AppendLine(" 			        AND E.BillTypeCode = @BillTypeCode  group by E.BillID,E.BillNo      ) g  ");
            searchSql.AppendLine(" 			        on A.ID=g.BillID ");
            searchSql.AppendLine(" 	LEFT OUTER JOIN officedba.FlowInstance h ON g.ID=h.ID  ");
            searchSql.AppendLine(" WHERE                               ");
            searchSql.AppendLine(" A.CompanyCD = @companyCD              ");
            searchSql.AppendLine("  and 	 	A.ID = @RectApplyID                 ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[4];
            //招聘申请ID
            param[0] = SqlHelper.GetParameter("@RectApplyID", rectApplyID);
            param[1] = SqlHelper.GetParameter("@companyCD", companyCD);
            param[2] = SqlHelper.GetParameter("@BillTypeFlag", ConstUtil.BILL_TYPEFLAG_HUMAN);
            param[3] = SqlHelper.GetParameter("@BillTypeCode", ConstUtil.BILL_TYPECODE_HUMAN_RECT_APPLY);
         
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        public static DataTable GetGoalDetailsWithID(string RectApplyNo, string companyCD)
        {
          

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                              ");
            searchSql.AppendLine("            RectApplyNo         ");
            searchSql.AppendLine("            ,isnull(JobName ,'') as JobName        ");
            searchSql.AppendLine("            ,isnull(JobDescripe ,'') as JobDescripe        "); 
            searchSql.AppendLine("            , isnull(cast (JobID as varchar),'')  as  JobID         ");
            searchSql.AppendLine("            ,RectCount               ");
            searchSql.AppendLine("            ,isnull(WorkPlace,'') as WorkPlace               ");
            searchSql.AppendLine("            ,isnull(WorkNature,'') as   WorkNature           ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10), UsedDate,21) AS UsedDate ");
            searchSql.AppendLine("            , isnull(cast (MaxAge as varchar),'')  as  MaxAge         ");
            searchSql.AppendLine("            , isnull(cast (MinAge as varchar),'')  as  MinAge         ");  
            searchSql.AppendLine("            ,Sex        ");
            searchSql.AppendLine("            ,WorkAge              ");
            searchSql.AppendLine("            ,Professional           ");
            searchSql.AppendLine("            ,isnull(WorkNeeds ,'') as   WorkNeeds          ");
            searchSql.AppendLine("            ,isnull(OtherAbility ,'') as OtherAbility          ");
            searchSql.AppendLine("            ,isnull(SalaryNote,'') as SalaryNote            ");
            searchSql.AppendLine("            ,CultureLevel             ");
            searchSql.AppendLine(" FROM                                ");
            searchSql.AppendLine(" 	officedba. RectApplyDetail               ");
            searchSql.AppendLine(" WHERE                               ");
            searchSql.AppendLine(" 	 CompanyCD=@CompanyCD  and    RectApplyNo = @RectApplyNo           ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //招聘申请ID
            param[0] = SqlHelper.GetParameter("@RectApplyNo", RectApplyNo);
            param[1] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #region 通过检索条件查询招聘申请信息
        /// <summary>
        /// 查询招聘申请信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchRectApplyInfo(RectApplyModel model,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                                        ");
            searchSql.AppendLine(" 	 A.ID                                                        ");
            searchSql.AppendLine(" 	,A.RectApplyNo                                               ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.CreateDate,21) AS CreateDate      ");
            searchSql.AppendLine(" 	,ISNULL(B.DeptName, '') AS DeptName                          "); 
            searchSql.AppendLine(" 	,isnull(cast (A.MaxNum as varchar),'')  as  MaxNum                           ");
            searchSql.AppendLine(" 	,isnull(cast (A.RequireNum as varchar),'')  as  RequireNum                           ");
            searchSql.AppendLine(" 	,isnull(cast (A.NowNum as varchar),'')  as  NowNum                           "); 
            searchSql.AppendLine(" 	,ISNULL(C.EmployeeName, '') AS PrincipalName                 ");
            searchSql.AppendLine(" 	,CASE A.BillStatus                                           ");
            searchSql.AppendLine(" 		WHEN '1' THEN '制单'                                   ");
            searchSql.AppendLine(" 		WHEN '2' THEN '执行'                                   ");
            searchSql.AppendLine(" 		WHEN '4' THEN '手工结单'                               ");
            searchSql.AppendLine(" 		WHEN '5' THEN '自动结单'                               ");
            searchSql.AppendLine(" 	END BillStatus                                        ");
            searchSql.AppendLine(" 	,CASE h.FlowStatus                                           ");
            searchSql.AppendLine(" 		WHEN '1' THEN '待审批'                                   ");
            searchSql.AppendLine(" 		WHEN '2' THEN '审批中'                                   ");
            searchSql.AppendLine(" 		WHEN '3' THEN '审批通过'                                 ");
            searchSql.AppendLine(" 		WHEN '4' THEN '审批不通过'                               ");
            searchSql.AppendLine(" 		WHEN '5' THEN '撤销审批'                               ");
            searchSql.AppendLine(" 		ELSE ' '                                            ");
            searchSql.AppendLine(" 	END AS FlowStatusName  ,isnull( Convert(varchar(100),A.ModifiedDate,23),'') AS ModifiedDate                                      ");
            searchSql.AppendLine(" FROM                                                          ");
            searchSql.AppendLine(" 	officedba.RectApply A                                        ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo B                               ");
            searchSql.AppendLine(" 		ON B.companyCD=A.companyCD AND A.DeptID = B.ID                                       ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo C                           ");
            searchSql.AppendLine(" 		ON C.companyCD=A.companyCD AND A.Principal = C.ID                                    ");
            searchSql.AppendLine(" 	LEFT JOIN (                                ");
            searchSql.AppendLine(" 			    SELECT                                           ");
            searchSql.AppendLine(" 			        MAX(E.id) ID,E.BillID,E.BillNo                            ");
            searchSql.AppendLine(" 			    FROM                                             ");
            searchSql.AppendLine(" 			        officedba.FlowInstance E,officedba.RectApply n                     ");
            searchSql.AppendLine(" 			    WHERE                                            ");
            searchSql.AppendLine(" 			        E.CompanyCD = n.CompanyCD                    ");
            searchSql.AppendLine(" 			        AND E.BillID = n.ID                     ");
            searchSql.AppendLine(" 			        AND E.BillTypeFlag = @BillTypeFlag           ");
            searchSql.AppendLine(" 			        AND E.BillTypeCode = @BillTypeCode  group by E.BillID,E.BillNo      ) g  ");
            searchSql.AppendLine(" 			        on A.ID=g.BillID ");
            searchSql.AppendLine(" 	LEFT OUTER JOIN officedba.FlowInstance h ON g.ID=h.ID  ");
            searchSql.AppendLine(" WHERE                                                         ");
            searchSql.AppendLine("       A.CompanyCD = @CompanyCD                                ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //单据类别标识
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", ConstUtil.BILL_TYPEFLAG_HUMAN));
            //单据类别编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", ConstUtil.BILL_TYPECODE_HUMAN_RECT_APPLY));

            //申请编号
            if (!string.IsNullOrEmpty(model.RectApplyNo))
            {
                searchSql.AppendLine("	AND A.RectApplyNo LIKE @RectApplyNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RectApplyNo", "%" + model.RectApplyNo + "%"));
            }
            //申请部门
            if (!string.IsNullOrEmpty(model.DeptID))
            {
                searchSql.AppendLine("	AND A.DeptID = @DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));
            }
            ////职位名称
            //if (!string.IsNullOrEmpty(model.JobName))
            //{
            //    searchSql.AppendLine("	AND k.JobName LIKE @JobName ");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@JobName", "%" + model.JobName + "%"));
            //}
            //申请日期
            if (!string.IsNullOrEmpty(model.UsedDate))
            {
                searchSql.AppendLine("	AND A.CreateDate >= @CreateDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", model.UsedDate));
            }
            //申请日期
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                searchSql.AppendLine("	AND A.BillStatus= @BillStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
            }
            //审批状态
            if (!string.IsNullOrEmpty(model.FlowStatusID))
            {
                //待提交时
                if ("0".Equals(model.FlowStatusID))
                {
                    searchSql.AppendLine("	AND h.FlowStatus IS NULL ");
                }
                else
                {
                    searchSql.AppendLine("	AND h.FlowStatus = @FlowStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", model.FlowStatusID));
                }
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount); //执行查询

           
            //return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        public static bool UpdateReportStatus(string status, string companyCD, string reprotNo, string userID,string DoSatus,int  User)
        {
            //删除SQL拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE                             ");
            updateSql.AppendLine(" officedba.RectApply             ");
            updateSql.AppendLine(" SET  BillStatus = @Status              ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            if (DoSatus == "1")//确认错做
            {
                updateSql.AppendLine(" 	,ConfirmDate = getdate()         ");
                updateSql.AppendLine(" 	,Confirmor = @Confirmor ");
            }
            else if (DoSatus == "2")//结单操作
            {
                updateSql.AppendLine(" 	,CloseDate = getdate()         ");
                updateSql.AppendLine(" 	,Closer = @Closer ");
            }
            else if (DoSatus == "3")
            {
                updateSql.AppendLine(" 	,CloseDate = null         ");
                updateSql.AppendLine(" 	,Closer =null ");
            }
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            updateSql.AppendLine(" 	AND RectApplyNo = @ReprotNo          ");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            if (DoSatus == "1")
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", User));
            }
            else if (DoSatus == "2")
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Closer", User  ));
            }
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
            //报表编号
            comm.Parameters.Add(SqlHelper.GetParameter("@ReprotNo", reprotNo));
            //状态
            comm.Parameters.Add(SqlHelper.GetParameter("@Status", status));
            //最后更新人
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", userID));

            //添加命令
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #region 从招聘申请中获取招聘目标
        /// <summary>
        /// 从招聘申请中获取招聘目标
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetGoalInfoFromRectApply(string companyCD)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                          ");
            searchSql.AppendLine(" 	A.ID AS ID                                     ");
            searchSql.AppendLine(" 	,A.DeptID AS DeptID                            ");
            searchSql.AppendLine(" 	, isnull(B.DeptName,' ') AS DeptName                        "); 
            searchSql.AppendLine(" 	,isnull(E.JobName,'') AS JobName                          ");
            searchSql.AppendLine(" 	,isnull(E.JobID,'') AS JobID                          ");
            searchSql.AppendLine(" 	,isnull(E.WorkAge,'') as  WorkAge                 ");
            searchSql.AppendLine(" 	,E.RectCount AS RectCount                      ");
            searchSql.AppendLine(" 	,ISNULL(E.Sex, '3') AS SexID                   ");
            searchSql.AppendLine(" 	,CASE E.WorkAge                                    ");
            searchSql.AppendLine(" 		WHEN '1' THEN '在读学生'                         ");
            searchSql.AppendLine(" 		WHEN '2' THEN '应届毕业生'                         ");
            searchSql.AppendLine(" 		WHEN '3' THEN '一年以内'                         ");
            searchSql.AppendLine(" 		WHEN '4' THEN '一年以上'                         ");
            searchSql.AppendLine(" 		WHEN '5' THEN '三年以上'                         ");
            searchSql.AppendLine(" 		WHEN '6' THEN '五年以上'                         ");
            searchSql.AppendLine(" 		WHEN '7' THEN '十年以上'                         ");
            searchSql.AppendLine(" 		WHEN '8' THEN '二十年以上'                         ");
            searchSql.AppendLine(" 		WHEN '9' THEN '退休人员'                         ");
            searchSql.AppendLine(" 		ELSE ' '                                ");
            searchSql.AppendLine(" 	END AS WorkAgeName                                 ");
            searchSql.AppendLine(" 	,CASE E.SEX                                    ");
            searchSql.AppendLine(" 		WHEN '1' THEN '男'                         ");
            searchSql.AppendLine(" 		WHEN '2' THEN '女'                         ");
            searchSql.AppendLine(" 		ELSE '不限'                                ");
            searchSql.AppendLine(" 	END AS SexName                                 ");
            searchSql.AppendLine(" 	,ISNULL(E.CultureLevel,'') AS CultureLevelID   ");
            searchSql.AppendLine(" 	,ISNULL(C.TypeName,'') AS CultureLevelName     ");
            searchSql.AppendLine(" 	,ISNULL(E.Professional,'') AS ProfessionalID   ");
            searchSql.AppendLine(" 	,ISNULL(D.TypeName,'') AS ProfessionalName     ");
            searchSql.AppendLine(" 	,  ISNULL(CONVERT(VARCHAR(10),E.MinAge,21),'') as MinAge                       ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),E.MaxAge,21),'') as MaxAge                      ");
            searchSql.AppendLine(" 	,'' AS Age                                     ");
            searchSql.AppendLine(" 	,isnull(E.WorkNeeds,'') AS WorkNeeds                      ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),E.UsedDate,21),'') ");
            searchSql.AppendLine(" 		AS CompleteDate                            ");
            searchSql.AppendLine(" FROM                                            ");
            searchSql.AppendLine(" 	officedba.RectApply A                          ");
            searchSql.AppendLine(" 	left join officedba.DeptInfo B                 ");
            searchSql.AppendLine(" 		on B.companyCD=A.companyCD AND A.DeptID = B.ID                         ");
            searchSql.AppendLine(" 	left join officedba. RectApplyDetail E           ");
            searchSql.AppendLine(" 		on   A.CompanyCD=E.CompanyCD  AND    A.RectApplyNo = E.RectApplyNo              ");
            searchSql.AppendLine(" 	left join officedba.CodePublicType C           ");
            searchSql.AppendLine(" 		on C.companyCD=A.companyCD AND  E.CultureLevel = C.ID                   ");
            searchSql.AppendLine(" 	left join officedba.CodePublicType D           ");
            searchSql.AppendLine(" 		on E.companyCD=A.companyCD AND E.Professional = D.ID                   ");
            searchSql.AppendLine(" WHERE                                           ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                       ");
            searchSql.AppendLine(" 	AND A.BillStatus = '2'                             ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //招聘申请ID
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 新建招聘申请信息
        /// <summary>
        /// 新建招聘申请信息 
        /// </summary>
        /// <param name="model">招聘申请信息</param>
        /// <returns></returns>
        public static RectApplyModel InsertRectApplyInfo(RectApplyModel model)
        {
            #region 登陆SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO officedba.RectApply ");
            insertSql.AppendLine("            (RectApplyNo         ");
            insertSql.AppendLine("            ,CompanyCD           ");
            insertSql.AppendLine("            ,DeptID              ");
            insertSql.AppendLine("            ,MaxNum               ");
            insertSql.AppendLine("            ,NowNum             ");
            insertSql.AppendLine("            ,RequireNum           ");
            insertSql.AppendLine("            ,Principal         ");
            insertSql.AppendLine("            ,RequstReason           ");
            insertSql.AppendLine("            ,Remark           ");
            insertSql.AppendLine("            ,BillStatus        "); 
            insertSql.AppendLine("            ,Creator              ");
            insertSql.AppendLine("            ,CreateDate  )            ");
            insertSql.AppendLine("      VALUES                     ");
            insertSql.AppendLine("            (@RectApplyNo         ");
            insertSql.AppendLine("            ,@CompanyCD           ");
            insertSql.AppendLine("            ,@DeptID              ");
            insertSql.AppendLine("            ,@MaxNum               ");
            insertSql.AppendLine("            ,@NowNum             ");
            insertSql.AppendLine("            ,@RequireNum           ");
            insertSql.AppendLine("            ,@Principal         ");
            insertSql.AppendLine("            ,@RequstReason           ");
            insertSql.AppendLine("            ,@Remark           ");
            insertSql.AppendLine("            ,@BillStatus        ");
            insertSql.AppendLine("            ,@Creator              ");
            insertSql.AppendLine("            ,getdate()  )            ");
            insertSql.AppendLine("   SET @RectApplyID= @@IDENTITY  ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@RectApplyID", SqlDbType.Int));
         
            //执行登陆操作
            bool sign = SqlHelper.ExecuteTransWithCommand(comm);
            if (sign)
            {
                if (InsertRectApplyDetailsInfo(model.GoalList, model.RectApplyNo, model.CompanyCD))
                {
                    model.IsSuccess = true;
                }
                else
                {
                    model.IsSuccess = false;
                }
            }
            else
            {
                model.IsSuccess = false ;
            }
  
            //设置ID
            model.ID = comm.Parameters["@RectApplyID"].Value.ToString();

            //执行插入并返回插入结果
            return model;
        }
        #endregion

        public static bool InsertRectApplyDetailsInfo(ArrayList lstGoal, string RectApplyNo, string CompanyCD)
        {
            StringBuilder deleteS = new StringBuilder();
            deleteS.AppendLine(" DELETE FROM officedba. RectApplyDetail ");
            deleteS.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "'");
            deleteS.AppendLine("and  RectApplyNo='" + RectApplyNo + "'");

            //定义更新基本信息的命令
            SqlCommand commmm = new SqlCommand();
            commmm.CommandText = deleteS.ToString();
            bool resu = SqlHelper.ExecuteTransWithCommand(commmm);
            if (resu)
            {
                StringBuilder insertSql = new StringBuilder();
                insertSql.AppendLine(" INSERT INTO officedba. RectApplyDetail ");
                insertSql.AppendLine("            (RectApplyNo         ");
                insertSql.AppendLine("            ,CompanyCD           ");
                insertSql.AppendLine("            ,JobName              ");
                insertSql.AppendLine("            ,JobDescripe              ");
                insertSql.AppendLine("            ,JobID              ");
                insertSql.AppendLine("            ,RectCount               ");
                insertSql.AppendLine("            ,WorkPlace             ");
                insertSql.AppendLine("            ,WorkNature           ");
                insertSql.AppendLine("            ,UsedDate         ");
                insertSql.AppendLine("            ,MaxAge           ");
                insertSql.AppendLine("            ,MinAge           ");
                insertSql.AppendLine("            ,Sex        ");
                insertSql.AppendLine("            ,WorkAge              ");
                insertSql.AppendLine("            ,Professional           ");
                insertSql.AppendLine("            ,WorkNeeds           ");
                insertSql.AppendLine("            ,OtherAbility        ");
                insertSql.AppendLine("            ,SalaryNote              ");
                insertSql.AppendLine("            ,CultureLevel  )            ");
                insertSql.AppendLine("      VALUES                     ");
                insertSql.AppendLine("            (@RectApplyNo         ");
                insertSql.AppendLine("            ,@CompanyCD           ");
                insertSql.AppendLine("            ,@JobName              ");
                insertSql.AppendLine("            ,@JobDescripe              ");
                insertSql.AppendLine("            ,@JobID              "); 
                insertSql.AppendLine("            ,@RectCount               ");
                insertSql.AppendLine("            ,@WorkPlace             ");
                insertSql.AppendLine("            ,@WorkNature           ");
                insertSql.AppendLine("            ,@UsedDate         ");
                insertSql.AppendLine("            ,@MaxAge           ");
                insertSql.AppendLine("            ,@MinAge           ");
                insertSql.AppendLine("            ,@Sex        ");
                insertSql.AppendLine("            ,@WorkAge              ");
                insertSql.AppendLine("            ,@Professional           ");
                insertSql.AppendLine("            ,@WorkNeeds           ");
                insertSql.AppendLine("            ,@OtherAbility        ");
                insertSql.AppendLine("            ,@SalaryNote              ");
                insertSql.AppendLine("            ,@CultureLevel  )            ");
           
             
                for (int i = 0; i < lstGoal.Count; i++)
                {
                    //获取单条目标记录
                    RectApplyDetailModel model = (RectApplyDetailModel)lstGoal[i];
                    //定义Command
                    SqlCommand comm = new SqlCommand();
                    //设置执行 Transact-SQL 语句
                    comm.CommandText = insertSql.ToString();

                    #region 设置参数
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));//企业代码
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@RectApplyNo", RectApplyNo));//招聘计划编号 
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@JobName", model.JobName ));//申请部门(对应部门表ID) 
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@RectCount", model.RectCount ));//岗位名称
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@WorkPlace", model.WorkPlace ));//岗位ID
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@WorkNature", model.WorkNature ));//人员数量
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedDate", model.UsedDate ));//性别(1 男，2 女，3不限)
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaxAge", model.MaxAge ));//年龄要求
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@MinAge", model.MinAge ));//学历ID(对应分类代码表ID) 
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Sex", model.Sex ));//专业ID(对应分类代码表ID) 
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@WorkAge", model.WorkAge ));//要求 
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Professional", model.Professional ));//计划完成时间
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@WorkNeeds", model .WorkNeeds ));//更新用户ID
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@OtherAbility", model.OtherAbility ));//更新用户ID
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@JobID", model.JobID));//更新用户ID
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@SalaryNote", model.SalaryNote ));//计划完成时间
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CultureLevel", model .CultureLevel ));//更新用户ID 
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@JobDescripe", model.JobDescripe ));//更新用户ID 
                    
                    #endregion

                    bool result = SqlHelper.ExecuteTransWithCommand(comm);
                    if (result)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }

                }

                return true;
          


            }
            else
            {
                return false;
            }
        }
        #region 更新招聘申请信息
        /// <summary>
        /// 更新招聘申请信息
        /// </summary>
        /// <param name="model">招聘申请信息</param>
        /// <returns></returns>
        public static RectApplyModel UpdateRectApplyInfo(RectApplyModel model)
        {

            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.RectApply          ");
            updateSql.AppendLine(" SET DeptID = @DeptID                ");
            updateSql.AppendLine("            ,MaxNum=@MaxNum               ");
            updateSql.AppendLine("            ,NowNum=@NowNum             "); 
            updateSql.AppendLine("            ,RequireNum=@RequireNum           "); 
            updateSql.AppendLine("            ,Principal=@Principal         ");
            updateSql.AppendLine("            ,RequstReason=@RequstReason           ");
            updateSql.AppendLine("            ,Remark=@Remark           ");
            updateSql.AppendLine("   ,ModifiedDate = getdate()         ");
            updateSql.AppendLine("   ,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE CompanyCD = @CompanyCD        ");
            updateSql.AppendLine(" AND RectApplyNo = @RectApplyNo      ");
          
            #endregion

            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            SetSaveParameter(comm, model);//其他参数

            //执行更新并设置更新结果
            bool sign = SqlHelper.ExecuteTransWithCommand(comm);
            if (sign)
            {
                if (InsertRectApplyDetailsInfo(model.GoalList, model.RectApplyNo, model.CompanyCD))
                {
                    model.IsSuccess = true;
                }
                else
                {
                    model.IsSuccess = false;
                }
            }
            else
            {
                model.IsSuccess = false;
            }

            return model;
        }
        #endregion

        #region 保存时参数设置
        /// <summary>
        /// 保存时参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">人才代理信息</param>
        private static void SetSaveParameter(SqlCommand comm, RectApplyModel model)
        {


            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RectApplyNo", model.RectApplyNo));//招聘申请编号   
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//企业代码                   
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));//申请部门                   
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaxNum", model.MaxNum ));//直接主管(对应员工表ID)     
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NowNum", model.NowNum ));//职位名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RequireNum", model.RequireNum ));//工作地点    
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Principal", model.Principal ));//申请人(对应员工表ID)       
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RequstReason", model.RequstReason ));//申请日期   
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark ));//工作要求                   
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus","1"));//其他能力                   
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", model.Creator ));//备注     
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//备注     
          
        }
        #endregion

        #region 能否删除招聘申请信息
        /// <summary>
        /// 能否删除招聘申请信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IfDeleteRectApplyInfo(string RectApplyNos, string CompanyID, string BillTypeFlag, string BillTypeCode)
        {
            string[] NOS = null;
            NOS = RectApplyNos.Split(',');
            bool Flag = true;

            for (int i = 0; i < NOS.Length; i++)
            {
                if (IsExistInfo(NOS[i], CompanyID, BillTypeFlag, BillTypeCode))
                {
                    Flag = false;
                    break;
                }
            }
            return Flag;
        }
        #endregion
        #region 能否删除招聘申请信息
        /// <summary>
        /// 能否删除招聘申请信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IsExistInfo(string BillNo, string CompanyID, string BillTypeFlag, string BillTypeCode)
        {

            string sql = "SELECT * FROM officedba.FlowInstance WHERE BillNo=" + BillNo + " AND CompanyCD='" + CompanyID + "'  AND BillTypeFlag='" + BillTypeFlag + "' AND BillTypeCode='" + BillTypeCode + "'";
            return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
        }
        #endregion
        #region 删除招聘申请信息
        /// <summary>
        /// 删除招聘申请信息
        /// </summary>
        /// <param name="applyNo">招聘申请编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteRectApplyInfo(string applyNo, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.RectApply ");
            deleteSql.AppendLine(" WHERE  CompanyCD = @CompanyCD  AND");
            deleteSql.AppendLine(" RectApplyNo In( " + applyNo + ")");
 

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));

            //定义更新列表
            ArrayList lstDelete = new ArrayList();
            //添加基本信息更新命令
            lstDelete.Add(comm);
            //执行删除并返回
            return SqlHelper.ExecuteTransWithArrayList(lstDelete);
        }
        #endregion


        public static bool UpdateMoveApplyCancelConfirm(string BillStatus, string ID, string userID, string CompanyID, string ReprotNo)
        {
            try
            {

                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.RectApply");
                sql.AppendLine("		SET BillStatus=@Status        ");
                sql.AppendLine("		,ModifiedDate=getdate()      ");
                sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
                sql.AppendLine(" 	,ConfirmDate = null       ");
                sql.AppendLine(" 	,Confirmor = null ");
                sql.AppendLine("WHERE                  ");
                sql.AppendLine(" 	CompanyCD = @CompanyCD            ");
                sql.AppendLine(" 	AND RectApplyNo = @RectApplyNo          ");



                SqlParameter[] param;
                param = new SqlParameter[4];
                param[0] = SqlHelper.GetParameter("@Status", BillStatus);
                param[1] = SqlHelper.GetParameter("@ModifiedUserID", userID);
                param[2] = SqlHelper.GetParameter("@CompanyCD", CompanyID);
                param[3] = SqlHelper.GetParameter("@RectApplyNo", ReprotNo);

                //SqlHelper.ExecuteTransSql(sql.ToString(), param);
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {


                    FlowDBHelper.OperateCancelConfirm(CompanyID, Convert.ToInt32(XBase.Common.ConstUtil.BILL_TYPEFLAG_HUMAN), Convert.ToInt32(XBase.Common.ConstUtil.BILL_TYPECODE_HUMAN_RECT_APPLY), Convert.ToInt32(ID), userID, tran);//取消确认
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sql.ToString(), param);
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 页面打印数据源
        /// </summary>
        /// <param name="rectApplyID"></param>
        /// <returns></returns>
        public static DataTable GetRectApplyInfoWithIDByReport(string rectApplyID, string CompanyCD)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                              ");
            searchSql.AppendLine(" 	 A.RectApplyNo                     ");
            searchSql.AppendLine(" 	,A.CompanyCD                       "); 
            searchSql.AppendLine(" 	,B.DeptName   as   DeptID                    ");
            searchSql.AppendLine(" 	,A.MaxNum                           ");
            searchSql.AppendLine(" 	,A.NowNum    ");
            searchSql.AppendLine(" 	,A.RequireNum                       ");  
            searchSql.AppendLine(" 	,A.RequstReason                    "); 
            searchSql.AppendLine(" 	,A.Remark                    ");
            searchSql.AppendLine(" 	,CASE A.BillStatus                                    ");
            searchSql.AppendLine(" 		WHEN '1' THEN '制单'                         ");
            searchSql.AppendLine(" 		WHEN '2' THEN '执行'                         ");
            searchSql.AppendLine(" 		WHEN '4' THEN '手工结单'                         ");
            searchSql.AppendLine(" 	END AS BillStatus                                 ");
            searchSql.AppendLine(" 	,ISNULL(C.EmployeeName,'') AS Creator     ");
            searchSql.AppendLine(" 	,ISNULL(D.EmployeeName,'') AS Principal     ");
            searchSql.AppendLine(" 	,ISNULL(E.EmployeeName,'') AS Confirmor     ");
            searchSql.AppendLine(" 	,ISNULL(F.EmployeeName,'') AS Closer       ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.CreateDate,21) AS CreateDate      ");
            searchSql.AppendLine(" 	, CONVERT(VARCHAR(10),A.ConfirmDate,21) AS ConfirmDate       ");
            searchSql.AppendLine(" 	, CONVERT(VARCHAR(10),A.CloseDate,21) AS CloseDate    ");
            searchSql.AppendLine(" FROM                                ");
            searchSql.AppendLine(" 	officedba.RectApply A              "); 
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo B     ");
            searchSql.AppendLine(" 		ON A.DeptID = B.ID              ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo C ");
            searchSql.AppendLine(" 		ON A.Creator = C.ID              ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo D ");
            searchSql.AppendLine(" 		ON A.Principal = D.ID          ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo E ");
            searchSql.AppendLine(" 		ON A.Confirmor = E.ID          ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo F ");
            searchSql.AppendLine(" 		ON A.Closer = F.ID          ");
          
            searchSql.AppendLine(" WHERE                               ");
            searchSql.AppendLine(" 	 A.CompanyCD=@CompanyCD and A.RectApplyNo = @RectApplyID    ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //招聘申请ID
            param[0] = SqlHelper.GetParameter("@RectApplyID", rectApplyID); 
            param[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        public static DataTable GetRectApplyDetailsInfoByReport(string rectApplyID, string CompanyCD)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                              ");
            searchSql.AppendLine(" 	 A.RectApplyNo                     ");
            searchSql.AppendLine(" 	,A.CompanyCD                       ");  
            searchSql.AppendLine("     ,A.JobDescripe              ");
            searchSql.AppendLine(" 	,A.JobName                         "); 
            searchSql.AppendLine(" 	,A.RectCount                       ");
            searchSql.AppendLine(" 	,A.WorkPlace                       ");
            searchSql.AppendLine(" 	,CASE A.WorkNature                                    ");
            searchSql.AppendLine(" 		WHEN '1' THEN '不限'                         ");
            searchSql.AppendLine(" 		WHEN '2' THEN '全职'                         ");
            searchSql.AppendLine(" 		WHEN '3' THEN '兼职'                         ");
            searchSql.AppendLine(" 		WHEN '4' THEN '实习'                         ");
            searchSql.AppendLine(" 		else   ''                        ");
            searchSql.AppendLine(" 	END AS WorkNature                                 ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.UsedDate,21) AS UsedDate "); 
            searchSql.AppendLine(" 	,A.MaxAge                      ");
            searchSql.AppendLine(" 	,A.MinAge                          ");
            searchSql.AppendLine(" 	,CASE A.SEX                                    ");
            searchSql.AppendLine(" 		WHEN '1' THEN '男'                         ");
            searchSql.AppendLine(" 		WHEN '2' THEN '女'                         ");
            searchSql.AppendLine(" 		ELSE '不限'                                ");
            searchSql.AppendLine(" 	END AS SEX                                 ");
            searchSql.AppendLine(" 	,CASE A.WorkAge                                    ");
            searchSql.AppendLine(" 		WHEN '1' THEN '在读学生'                         ");
            searchSql.AppendLine(" 		WHEN '2' THEN '应届毕业生'                         ");
            searchSql.AppendLine(" 		WHEN '3' THEN '一年以内'                         ");
            searchSql.AppendLine(" 		WHEN '4' THEN '一年以上'                         ");
            searchSql.AppendLine(" 		WHEN '5' THEN '三年以上'                         ");
            searchSql.AppendLine(" 		WHEN '6' THEN '五年以上'                         ");
            searchSql.AppendLine(" 		WHEN '7' THEN '十年以上'                         ");
            searchSql.AppendLine(" 		WHEN '8' THEN '二十年以上'                         ");
            searchSql.AppendLine(" 		WHEN '9' THEN '退休人员'                         ");
            searchSql.AppendLine(" 		else   ''                        ");
            searchSql.AppendLine(" 	END AS WorkAge                                 ");
            searchSql.AppendLine(" 	,isnull(G. TypeName,'') as CultureLevel                         "); 
            searchSql.AppendLine(" 	,isnull(G. TypeName,'') as CultureLevel                         "); 
            searchSql.AppendLine(" 	,isnull(h. TypeName,'') as Professional                         ");
            searchSql.AppendLine(" 	,isnull(A.WorkNeeds,'') as   WorkNeeds                  ");
            searchSql.AppendLine(" 	,isnull(A.SalaryNote,'') as   SalaryNote                  ");
            searchSql.AppendLine(" 	,isnull(A.OtherAbility,'') as   OtherAbility                     ");
            searchSql.AppendLine(" FROM                                ");
            searchSql.AppendLine(" 	officedba. RectApplyDetail A              "); 
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType G");
            searchSql.AppendLine(" 		ON G.companyCD=A.companyCD AND A.CultureLevel = G.ID  and G.TypeFlag=@TypeFlag AND     G.TypeCode=@TypeCode           ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType H");
            searchSql.AppendLine(" 		ON H.companyCD=A.companyCD AND A.Professional = h.ID and h.TypeFlag=@TypeFlag and   h.TypeCode=@PofessTypeCode           ");
            searchSql.AppendLine(" WHERE                               ");
            searchSql.AppendLine(" A.CompanyCD=@CompanyCD  and	A.RectApplyNo = @RectApplyID      ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[5];
            //招聘申请ID
            param[0] = SqlHelper.GetParameter("@RectApplyID", rectApplyID);
            param[1] = SqlHelper.GetParameter("@TypeFlag", ConstUtil.CODE_TYPE_HUMAN);
            param[2] = SqlHelper.GetParameter("@TypeCode", ConstUtil.CODE_TYPE_CULTURE);
            param[3] = SqlHelper.GetParameter("@PofessTypeCode", ConstUtil.CODE_TYPE_PROFESSIONAL);
            param[4] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
    }
}

