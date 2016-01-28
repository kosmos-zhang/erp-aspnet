/**********************************************
 * 类作用：   员工信息数据库层处理
 * 建立人：   吴志强
 * 建立时间： 2009/03/09
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
    /// 类名：EmployeeInfoDBHelper
    /// 描述：员工信息数据库层处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/09
    /// 最后修改时间：2009/03/09
    /// </summary>
    ///
    public class EmployeeInfoDBHelper
    {

        #region 查询人员以及相关的履历技能信息

        #region 通过人员ID查询人员信息
        /// <summary>
        /// 查询人员信息
        /// </summary>
        /// <param name="employeeID">人员ID</param>
        /// <returns></returns>
        public static EmployeeInfoModel GetEmployeeInfoWithID(int employeeID)
        {

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT A.ID                    ");
            searchSql.AppendLine("      ,A.EmployeeNum           ");
            searchSql.AppendLine("      ,A.EmployeeNo            ");
            searchSql.AppendLine("      ,A.PYShort               ");
            searchSql.AppendLine("      ,A.CompanyCD             ");
            searchSql.AppendLine("      ,A.CardID                ");
            searchSql.AppendLine("      ,A.SafeguardCard         ");
            searchSql.AppendLine("      ,A.EmployeeName          ");
            searchSql.AppendLine("      ,A.UsedName              ");
            searchSql.AppendLine("      ,A.NameEn                ");
            searchSql.AppendLine("      ,A.Sex                   ");
            searchSql.AppendLine("      ,A.Birth                 ");
            searchSql.AppendLine("      ,A.Account               ");
            searchSql.AppendLine("      ,A.AccountNature         ");
            searchSql.AppendLine("      ,A.CountryID             ");
            searchSql.AppendLine("      ,A.Height                ");
            searchSql.AppendLine("      ,A.Weight                ");
            searchSql.AppendLine("      ,A.Sight                 ");
            searchSql.AppendLine("      ,A.Degree                ");
            searchSql.AppendLine("      ,A.PositionID            ");
            searchSql.AppendLine("      ,A.DocuType              ");
            //searchSql.AppendLine("      ,A.CreateUserID          ");
            //searchSql.AppendLine("      ,A.CreateDate            ");
            searchSql.AppendLine("      ,A.NationalID            ");
            searchSql.AppendLine("      ,A.MarriageStatus        ");
            searchSql.AppendLine("      ,A.Origin                ");
            searchSql.AppendLine("      ,A.Landscape             ");
            searchSql.AppendLine("      ,A.Religion              ");
            searchSql.AppendLine("      ,A.Telephone             ");
            searchSql.AppendLine("      ,A.Mobile                ");
            searchSql.AppendLine("      ,A.EMail                 ");
            searchSql.AppendLine("      ,A.OtherContact          ");
            searchSql.AppendLine("      ,A.HomeAddress           ");
            searchSql.AppendLine("      ,A.HealthStatus          ");
            searchSql.AppendLine("      ,A.CultureLevel          ");
            searchSql.AppendLine("      ,A.GraduateSchool        ");
            searchSql.AppendLine("      ,A.Professional          ");
            searchSql.AppendLine("      ,A.Features              ");
            searchSql.AppendLine("      ,A.ComputerLevel         ");
            searchSql.AppendLine("      ,A.ForeignLanguage1      ");
            searchSql.AppendLine("      ,A.ForeignLanguageLevel1 ");
            searchSql.AppendLine("      ,A.ForeignLanguage2      ");
            searchSql.AppendLine("      ,A.ForeignLanguageLevel2 ");
            searchSql.AppendLine("      ,A.ForeignLanguage3      ");
            searchSql.AppendLine("      ,A.ForeignLanguageLevel3 ");
            searchSql.AppendLine("      ,A.WorkTime              ");
            searchSql.AppendLine("      ,A.TotalSeniority        ");
            searchSql.AppendLine("      ,A.PhotoURL              ");
            searchSql.AppendLine("      ,A.PositionTitle         ");
            searchSql.AppendLine("      ,A.Flag                  ");
            searchSql.AppendLine("      ,A.ModifiedDate        ");
            searchSql.AppendLine("      ,A.ModifiedUserID      ");
            searchSql.AppendLine("      ,A.QuarterID             ");
            searchSql.AppendLine("      ,A.AdminLevelID             ");
            searchSql.AppendLine("      ,A.DeptID             ");
            searchSql.AppendLine("      ,C.DeptName             ");
            searchSql.AppendLine("      ,CONVERT(varchar(100), A.EnterDate, 23) EnterDate             ");
            searchSql.AppendLine("      ,A.Resume,A.ProfessionalDes                ");
            //searchSql.AppendLine("      ,B.EmployeeName AS CreateUserName      ");
            searchSql.AppendLine("  FROM officedba.EmployeeInfo A ");
            //searchSql.AppendLine("  LEFT OUTER JOIN officedba.EmployeeInfo B on ");
            //searchSql.AppendLine("  B.id = A.CreateUserID AND B.CompanyCD = A.CompanyCD ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.DeptInfo C on ");
            searchSql.AppendLine(" C.id = A.DeptID ");
            searchSql.AppendLine(" WHERE                       ");
            searchSql.AppendLine("	A.ID = @EmployeeID           ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //人员ID
            param[0] = SqlHelper.GetParameter("@EmployeeID", employeeID);
            //执行查询
            DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), param);
            //返回查询的值
            if (data == null || data.Rows.Count < 1)
            {
                //数据不存在时，返回空值
                return null;
            }
            else
            {
                //数据存在时，返回转化后的EmployeeInfoModel
                EmployeeInfoModel model = ChangeEmplDataToModel(data.Rows[0]);
                //设置履历信息
                model.HistoryInfo = GetHistoryInfo(model.CompanyCD, model.EmployeeNo);
                //设置技能信息
                model.SkillInfo = GetSkillInfo(model.CompanyCD, model.EmployeeNo);
                //设置合同信息
                model.ContractInfo = GetContractInfo(employeeID);
                //返回人员信息
                return model;
            }
        }
        #endregion

        #region 查询在职人员列表
        /// <summary>
        /// 查询在职人员列表
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchEmployeeWorkInfo(EmployeeSearchModel searchModel,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                                            ");
            searchSql.AppendLine(" 	 A.ID                                                            ");
            searchSql.AppendLine(" 	,A.EmployeeNo                                                    ");
            searchSql.AppendLine(" 	,ISNULL(A.EmployeeNum, '') AS EmployeeNum                        ");
            searchSql.AppendLine(" 	,ISNULL(A.PYShort, '') AS PYShort                                ");
            searchSql.AppendLine(" 	,A.EmployeeName                                                  ");
            //searchSql.AppendLine(" 	,CASE F.ContractKind                                             ");
            //searchSql.AppendLine(" 		WHEN '1' THEN '合同工'                                       ");
            //searchSql.AppendLine(" 		WHEN '2' THEN '临时工'                                       ");
            //searchSql.AppendLine(" 		WHEN '3' THEN '兼职'                                         ");
            //searchSql.AppendLine(" 		ELSE ''                                                      ");
            //searchSql.AppendLine(" 	END AS ContractKind                                              ");
            searchSql.AppendLine(" 	,ISNULL(B.DeptName, '') AS DeptName                              ");
            searchSql.AppendLine(" 	,ISNULL(C.QuarterName, '') AS QuarterName                        ");
            searchSql.AppendLine(" 	,ISNULL(D.TypeName, '') AS AdminLevelName                        ");
            searchSql.AppendLine(" 	,ISNULL(G.TypeName, '') AS PositionName                          ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.EnterDate,21),'') AS EntryDate     ");
            //searchSql.AppendLine(" 	,CASE                                                            ");
            //searchSql.AppendLine(" 		WHEN A.EnterDate is null THEN ''                             ");
            //searchSql.AppendLine(" 		ELSE                                                         ");
            //searchSql.AppendLine(" 	CONVERT(VARCHAR(2),DATEDIFF(YEAR,A.EnterDate,getdate())+1) + ' 年' ");           
            //searchSql.AppendLine(" 	END AS TotalYear                                                 ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(100),A.EnterDate,23) TotalYear ");
            searchSql.AppendLine(" FROM                                                              ");
            searchSql.AppendLine(" 	officedba.EmployeeInfo A                                         ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo B                                   ");
            searchSql.AppendLine(" 		ON A.DeptID = B.ID                                           ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter C                                ");
            searchSql.AppendLine(" 		ON A.QuarterID = C.ID                                        ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType D                             ");
            searchSql.AppendLine(" 		ON A.AdminLevelID = D.ID                                     ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType G                             ");
            searchSql.AppendLine(" 		ON A.PositionID = G.ID                                       ");
            //searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeContract F                           ");
            //searchSql.AppendLine(" 		ON A.ID = F.EmployeeID                                       ");
            //searchSql.AppendLine("  		AND F.ContractStatus = @ContractStatus                   ");
            searchSql.AppendLine(" WHERE                                                             ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                                         ");
            searchSql.AppendLine(" 	AND A.Flag = @Flag and (A.delFlag <> @delFlag or A.delFlag is null) ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加非离职参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", "1"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@delFlag", "1"));
            ////添加合同状态参数
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractStatus", ConstUtil.CONTRACT_FLAG_ONE));
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", searchModel.CompanyCD));

            #region 页面条件
            //编号
            if (!string.IsNullOrEmpty(searchModel.EmployeeNo))
            {
                searchSql.AppendLine("	AND A.EmployeeNo LIKE '%' + @EmployeeNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", searchModel.EmployeeNo));
            }
            //工号
            if (!string.IsNullOrEmpty(searchModel.EmployeeNum))
            {
                searchSql.AppendLine("	AND A.EmployeeNum LIKE '%' + @EmployeeNum + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNum", searchModel.EmployeeNum));
            }
            //拼音缩写
            if (!string.IsNullOrEmpty(searchModel.PYShort))
            {
                searchSql.AppendLine("	AND A.PYShort LIKE '%' + @PYShort + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", searchModel.PYShort));
            }
            //姓名
            if (!string.IsNullOrEmpty(searchModel.EmployeeName))
            {
                searchSql.AppendLine("	AND A.EmployeeName LIKE '%' + @EmployeeName + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeName", searchModel.EmployeeName));
            }
            //工种
            //if (!string.IsNullOrEmpty(searchModel.ContractKind))
            //{
            //    searchSql.AppendLine("	AND F.ContractKind = @ContractKind ");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractKind", searchModel.ContractKind));
            //}
            //行政等级
            //if (!string.IsNullOrEmpty(searchModel.AdminLevelID))
            //{
            //    searchSql.AppendLine("	AND A.AdminLevelID = @AdminLevelID ");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdminLevelID", searchModel.AdminLevelID));
            //}
            //岗位
            if (!string.IsNullOrEmpty(searchModel.QuarterID))
            {
                searchSql.AppendLine("	AND A.QuarterID = @QuarterID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", searchModel.QuarterID));
            }
            //职称
            if (!string.IsNullOrEmpty(searchModel.PositionID))
            {
                searchSql.AppendLine("	AND A.PositionID = @PositionID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PositionID", searchModel.PositionID));
            }
            //入职时间
            if (!string.IsNullOrEmpty(searchModel.EnterDate))
            {
                searchSql.AppendLine("	AND A.EnterDate >= @EnterDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EnterDate", searchModel.EnterDate));
            }
            //入职时间
            if (!string.IsNullOrEmpty(searchModel.EnterEndDate))
            {
                searchSql.AppendLine("	AND A.EnterDate <= @EnterEndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EnterEndDate", searchModel.EnterEndDate));
            }
            //手机号码
            if (!string.IsNullOrEmpty(searchModel.Mobile))
            {
                searchSql.AppendLine("	AND A.Mobile like '%'+ @Mobile + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Mobile", searchModel.Mobile));
            }
            #endregion

            //设定comm的SQL文
            comm.CommandText = searchSql.ToString();

            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            //执行查询
            //return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 查询离职人员列表
        /// <summary>
        /// 查询离职人员列表
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchEmployeeLeaveInfo(EmployeeSearchModel searchModel)
        {

            #region 查询SQL拼写
            //查询SQL拼写
            //StringBuilder searchSql = new StringBuilder();
            //searchSql.AppendLine(" SELECT                                                              ");
            //searchSql.AppendLine("  		A.ID                                                       ");
            //searchSql.AppendLine("  		,A.EmployeeNo                                              ");
            //searchSql.AppendLine("  		,ISNULL(A.EmployeeNum, '') AS EmployeeNum                  ");
            //searchSql.AppendLine("  		,ISNULL(A.PYShort, '') AS PYShort                          ");
            //searchSql.AppendLine("  		,A.EmployeeName                                            ");
            //searchSql.AppendLine("  		,CASE G.ContractKind                                       ");
            //searchSql.AppendLine("  		  WHEN '1' THEN '合同工'                                   ");
            //searchSql.AppendLine("  		  WHEN '2' THEN '临时工'                                   ");
            //searchSql.AppendLine("  		  WHEN '3' THEN '兼职'                                     ");
            //searchSql.AppendLine("  		  ELSE ''                                                  ");
            //searchSql.AppendLine("  		END AS ContractKind                                        ");
            //searchSql.AppendLine("       ,Case A.Sex                                                   ");
            //searchSql.AppendLine(" 		When '1' then '男'                                             ");
            //searchSql.AppendLine(" 		When '2' then '女'                                             ");
            //searchSql.AppendLine(" 		else ''                                                        ");
            //searchSql.AppendLine(" 		End as SexName                                                 ");
            //searchSql.AppendLine("  		,ISNULL(E.DeptName, '') AS DeptName                        ");
            ////searchSql.AppendLine("  	    ,ISNULL(H.TypeName, '') AS AdminLevelName                  ");
            //searchSql.AppendLine("  	    ,ISNULL(I.QuarterName, '') AS QuarterName                  ");
            //searchSql.AppendLine("  	    ,ISNULL(F.TypeName, '') AS PositionName                    ");
            //searchSql.AppendLine("  	  ,ISNULL(CONVERT(VARCHAR(10),A.EnterDate,21),'') AS EntryDate ");
            //searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),B.OutDate,21),'') AS LeaveDate         ");
            ////searchSql.AppendLine("       ,CASE WHEN A.EnterDate is null THEN '' ELSE                   ");
            ////searchSql.AppendLine("  CONVERT(VARCHAR(2),DATEDIFF(YEAR,A.EnterDate,getdate())+1) + ' 年'   ");
            ////searchSql.AppendLine("  	    END AS TotalYear                                           ");
            //searchSql.AppendLine("  FROM                                                               ");
            //searchSql.AppendLine(" 	officedba.EmployeeInfo A                                           ");

            //searchSql.AppendLine(" 	LEFT JOIN (select companycd,EmployeeID,max(outdate) outdate from officedba.MoveNotify where BillStatus = '2' group by EmployeeID,companycd ) B  ");
            //searchSql.AppendLine(" 		ON B.EmployeeID = A.ID                                         ");
            //searchSql.AppendLine(" 		AND B.CompanyCD = A.CompanyCD                                  ");

            //searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo E                                     ");
            //searchSql.AppendLine(" 		ON E.ID = A.DeptID                                             ");
            //searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType F                               ");
            //searchSql.AppendLine(" 		ON F.ID = A.PositionID                                         ");
            ////searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType H                               ");
            ////searchSql.AppendLine(" 		ON H.ID = A.AdminID                                            ");
            //searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter I                                  ");
            //searchSql.AppendLine(" 		ON I.ID = A.QuarterID                                          ");
            //searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeContract G                             ");
            //searchSql.AppendLine(" 		ON G.EmployeeID = A.ID                                         ");
            //searchSql.AppendLine("  		AND G.ContractStatus = @ContractStatus                     ");
            //searchSql.AppendLine("  WHERE                                                              ");
            //searchSql.AppendLine("  	A.CompanyCD = @CompanyCD                                       ");
            //searchSql.AppendLine(" 	AND A.Flag = @JobFlag											   ");
            #endregion

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT A.ID,ISNULL(CONVERT(VARCHAR(10),r.OutDate,21),'') AS LeaveDate ");
            searchSql.AppendLine("      ,ISNULL(A.EmployeeNum, '') AS EmployeeNum,ISNULL(CONVERT(VARCHAR(10),A.EnterDate,21),'') AS EntryDate");
            searchSql.AppendLine("      ,A.EmployeeNo            ");
            searchSql.AppendLine("      ,ISNULL(A.PYShort, '') AS PYShort               ");
            searchSql.AppendLine("      ,A.CompanyCD             ");
            searchSql.AppendLine("      ,A.CardID                ");
            searchSql.AppendLine("      ,A.SafeguardCard         ");
            searchSql.AppendLine("      ,isnull(A.EmployeeName,'') EmployeeName ");
            searchSql.AppendLine("      ,A.UsedName              ");
            searchSql.AppendLine("      ,A.NameEn                ");
            searchSql.AppendLine("      ,(case A.Sex when '1' then '男' else '女' end)SexName ");
            searchSql.AppendLine("      ,A.Birth                 ");
            searchSql.AppendLine("      ,A.Account               ");
            searchSql.AppendLine("      ,(case A.AccountNature when '1' then '非农业' when '2' then '农业' else '' end) AccountNature ");
            searchSql.AppendLine("      ,A.CountryID             ");
            searchSql.AppendLine("      ,A.Height                ");
            searchSql.AppendLine("      ,CONVERT(varchar(8),A.Weight) Weight                ");
            searchSql.AppendLine("      ,CONVERT(varchar(8),A.Sight) Sight                 ");
            searchSql.AppendLine("      ,A.Degree                ");
            searchSql.AppendLine("      ,A.DocuType              ");
            searchSql.AppendLine("      ,A.Origin                ");
            searchSql.AppendLine("      ,A.Telephone             ");
            searchSql.AppendLine("      ,A.Mobile                ");
            searchSql.AppendLine("      ,A.EMail                 ");
            searchSql.AppendLine("      ,A.OtherContact          ");
            searchSql.AppendLine("      ,A.HomeAddress           ");
            searchSql.AppendLine("      ,(case A.HealthStatus when '0' then '一般' when '1' then '良好' when '2' then '很好' else '' end) HealthStatus");
            searchSql.AppendLine("      ,A.GraduateSchool        ");
            searchSql.AppendLine("      ,A.Features              ");
            searchSql.AppendLine("      ,A.ComputerLevel         ");
            searchSql.AppendLine("      ,(case A.ForeignLanguageLevel1 when '1' then '一般' when '2' then '熟练' when '3' then '精通' end)ForeignLanguageLevel1 ");
            searchSql.AppendLine("      ,(case A.ForeignLanguageLevel2 when '1' then '一般' when '2' then '熟练' when '3' then '精通' end)ForeignLanguageLevel2 ");
            searchSql.AppendLine("      ,(case A.ForeignLanguageLevel3 when '1' then '一般' when '2' then '熟练' when '3' then '精通' end)ForeignLanguageLevel3 ");
            searchSql.AppendLine("      ,A.WorkTime ");
            searchSql.AppendLine("      ,A.TotalSeniority,A.PhotoURL,A.PositionTitle,'离职人员' Flag ");
            searchSql.AppendLine("      ,A.ModifiedDate ,A.ModifiedUserID,A.QuarterID ");
            searchSql.AppendLine("      ,A.AdminLevelID ,A.DeptID ,ISNULL(C.DeptName, '') AS DeptName ");
            searchSql.AppendLine("      ,CONVERT(varchar(100), A.EnterDate, 23) EnterDate             ");
            searchSql.AppendLine("      ,A.Resume,A.ProfessionalDes,q.TypeName NationalName ");
            searchSql.AppendLine("      ,ISNULL(d.TypeName, '') AS PositionName,ISNULL(e.QuarterName, '') AS QuarterName,f.TypeName MarriageStatus ");
            searchSql.AppendLine("      ,g.TypeName CultureLevel,g.TypeName Professional,i.TypeName Landscape ");
            searchSql.AppendLine("      ,j.TypeName Religion,k.TypeName CountryName,l.TypeName ForeignLanguage11 ");
            searchSql.AppendLine("      ,m.TypeName ForeignLanguage12,o.TypeName ForeignLanguage13,p.TypeName AdminLevelName ");
            searchSql.AppendLine("  FROM officedba.EmployeeInfo A ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.DeptInfo C on C.id = A.DeptID ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType d on d.id = A.PositionID ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.DeptQuarter e on e.id = A.QuarterID ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType f on f.id = A.MarriageStatus ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType g on g.id = A.CultureLevel ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType h on h.id = A.Professional ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType i on i.id = A.Landscape ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType j on j.id = A.Religion ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType k on k.id = A.CountryID ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType l on l.id = A.ForeignLanguage1 ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType m on m.id = A.ForeignLanguage2 ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType o on o.id = A.ForeignLanguage3 ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType p on p.id = A.AdminLevelID ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType q on q.id = A.NationalID ");

            searchSql.AppendLine(" 	LEFT JOIN (select companycd,EmployeeID,max(outdate) outdate from officedba.MoveNotify where BillStatus = '2' group by EmployeeID,companycd ) r  ");
            searchSql.AppendLine(" 		ON r.EmployeeID = A.ID                                         ");
            searchSql.AppendLine(" 		AND r.CompanyCD = A.CompanyCD                                  ");

            searchSql.AppendLine(" WHERE                                                             ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                                         ");
            searchSql.AppendLine(" 	AND A.Flag = @JobFlag and (A.delFlag <> @delFlag  or A.delFlag is null) ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加离职参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@JobFlag", ConstUtil.JOB_FLAG_LEAVE));
            //添加合同状态参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractStatus", ConstUtil.CONTRACT_FLAG_ONE));
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", searchModel.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@delFlag", "1"));

            #region 页面输入条件
            //编号
            if (!string.IsNullOrEmpty(searchModel.EmployeeNo))
            {
                searchSql.AppendLine("	AND A.EmployeeNo  LIKE '%' + @EmployeeNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", searchModel.EmployeeNo));
            }
            //工号
            if (!string.IsNullOrEmpty(searchModel.EmployeeNum))
            {
                searchSql.AppendLine("	AND A.EmployeeNum LIKE '%' + @EmployeeNum + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNum", searchModel.EmployeeNum));
            }
            //拼音缩写
            if (!string.IsNullOrEmpty(searchModel.PYShort))
            {
                searchSql.AppendLine("	AND A.PYShort LIKE '%' + @PYShort + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", searchModel.PYShort));
            }
            //姓名
            if (!string.IsNullOrEmpty(searchModel.EmployeeName))
            {
                searchSql.AppendLine("	AND A.EmployeeName LIKE '%' + @EmployeeName + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeName", searchModel.EmployeeName));
            }
            //工种
            if (!string.IsNullOrEmpty(searchModel.ContractKind))
            {
                searchSql.AppendLine("	AND G.ContractKind = @ContractKind ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractKind", searchModel.ContractKind));
            }
            //行政等级
            if (!string.IsNullOrEmpty(searchModel.AdminLevelID))
            {
                searchSql.AppendLine("	AND A.AdminID = @NowAdminLevel ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@NowAdminLevel", searchModel.AdminLevelID));
            }
            //岗位
            if (!string.IsNullOrEmpty(searchModel.QuarterID))
            {
                searchSql.AppendLine("	AND A.QuarterID = @QuarterID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", searchModel.QuarterID));
            }
            //职称
            if (!string.IsNullOrEmpty(searchModel.PositionID))
            {
                searchSql.AppendLine("	AND A.PositionID = @NowPositionID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@NowPositionID", searchModel.PositionID));
            }
            //入职时间
            if (!string.IsNullOrEmpty(searchModel.EnterDate))
            {
                searchSql.AppendLine("	AND A.EnterDate >= @EntryDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EntryDate", searchModel.EnterDate));
            }
            if (!string.IsNullOrEmpty(searchModel.EnterEndDate))
            {
                searchSql.AppendLine("	AND A.EnterDate <= @EnterEndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EnterEndDate", searchModel.EnterEndDate));
            }
            //离职时间
            if (!string.IsNullOrEmpty(searchModel.LeaveDate))
            {
                searchSql.AppendLine("	AND B.OutDate >= @RegisterDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RegisterDate", searchModel.LeaveDate));
            }
            if (!string.IsNullOrEmpty(searchModel.LeaveEndDate))
            {
                searchSql.AppendLine("	AND B.OutDate <= @LeaveEndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@LeaveEndDate", searchModel.LeaveEndDate));
            }
            if (!string.IsNullOrEmpty(searchModel.Mobile))
            {
                searchSql.AppendLine("	AND A.Mobile LIKE '%' + @Mobile + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Mobile", searchModel.Mobile));
            }
            #endregion

            //设定comm的SQL文
            comm.CommandText = searchSql.ToString();

            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 查询待入职人员列表
        /// <summary>
        /// 查询待入职人员列表
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchWaitEnterInfo(EmployeeSearchModel searchModel, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                                             ");
            searchSql.AppendLine(" 	 A.ID                                                             ");
            searchSql.AppendLine(" 	,A.EmployeeNo                                                     ");
            searchSql.AppendLine(" 	,ISNULL(A.CardID, '') AS CardID                                   ");
            searchSql.AppendLine(" 	,A.EmployeeName                                                   ");
            searchSql.AppendLine(" 	,CASE A.Sex                                                       ");
            searchSql.AppendLine(" 		WHEN '1' THEN '男'                                            ");
            searchSql.AppendLine(" 		WHEN '2' THEN '女'                                            ");
            searchSql.AppendLine(" 		ELSE ''                                                       ");
            searchSql.AppendLine(" 	 END AS SexName                                                   ");
            searchSql.AppendLine(" 	 ,CASE A.Flag   WHEN '2' THEN '人才储备'   WHEN '3' THEN '离职' END  Flag  ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR,A.QuarterID), '') AS QuarterID            ");
            searchSql.AppendLine(" 	,ISNULL(C.QuarterName, '') AS QuarterName                         ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.Birth, 21), '') AS Birth            ");
            searchSql.AppendLine(" 	,CASE WHEN A.Birth IS NULL THEN ''                                ");
            searchSql.AppendLine(" 	ELSE CONVERT(VARCHAR(3),DATEDIFF(YEAR,A.Birth,getdate())) + ' 岁' ");
            searchSql.AppendLine(" 	END AS Age                                                        ");
            searchSql.AppendLine(" 	,ISNULL(A.HomeAddress, '') AS HomeAddress                         ");
            searchSql.AppendLine(" 	,ISNULL(A.Telephone, '') + ' ' + ISNULL(A.Mobile, '') AS Contact  ");
            searchSql.AppendLine(" 	,ISNULL(D.TypeName, '') AS CultureLevelName                       ");
            searchSql.AppendLine(" 	,ISNULL(A.GraduateSchool, '') AS SchoolName                       ");
            searchSql.AppendLine(" 	,ISNULL(E.TypeName, '') AS ProfessionalName                       ");
            searchSql.AppendLine(" 	,CASE B.FinalResult                                              ");
            searchSql.AppendLine(" 		WHEN '0' THEN '不予考虑'  WHEN '1' THEN '拟予试用'                                    ");
            searchSql.AppendLine(" 		ELSE ''                                                 ");
            searchSql.AppendLine(" 	 END AS FinalResult                                              ");
            searchSql.AppendLine(" 	,A.ModifiedDate                                                   ");
            searchSql.AppendLine(" FROM                                                               ");
            searchSql.AppendLine(" 	officedba.EmployeeInfo A                                          ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.RectInterview B                               ");
            searchSql.AppendLine(" 		ON B.StaffName = A.ID                                         ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter C                                 ");
            searchSql.AppendLine(" 		ON A.QuarterID = C.ID                                         ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType D                              ");
            searchSql.AppendLine(" 		ON A.CultureLevel = D.ID                                      ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType E                              ");
            searchSql.AppendLine(" 		ON A.Professional = E.ID                                      ");
            searchSql.AppendLine(" WHERE                                                              ");
            searchSql.AppendLine(" 	(A.Flag = '2'  OR A.Flag='3')                                       ");
            searchSql.AppendLine(" 	AND A.CompanyCD = @CompanyCD                                      ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", searchModel.CompanyCD));

            //编号
            if (!string.IsNullOrEmpty(searchModel.EmployeeNo))
            {
                searchSql.AppendLine("	AND A.EmployeeNo LIKE '%' + @EmployeeNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", searchModel.EmployeeNo));
            }
            //姓名
            if (!string.IsNullOrEmpty(searchModel.EmployeeName))
            {
                searchSql.AppendLine("	AND A.EmployeeName LIKE '%' + @EmployeeName + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeName", searchModel.EmployeeName));
            }
            //性别
            if (!string.IsNullOrEmpty(searchModel.SexID))
            {
                searchSql.AppendLine("	AND A.Sex = @Sex ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Sex", searchModel.SexID));
            }
            //学历 
            if (!string.IsNullOrEmpty(searchModel.CultureLevel))
            {
                searchSql.AppendLine("	AND A.CultureLevel = @CultureLevel ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CultureLevel", searchModel.CultureLevel));
            }
            //毕业院校
            if (!string.IsNullOrEmpty(searchModel.SchoolName))
            {
                searchSql.AppendLine("	AND A.GraduateSchool LIKE '%' + @GraduateSchool + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@GraduateSchool", searchModel.SchoolName));
            }
            //应聘岗位
            if (!string.IsNullOrEmpty(searchModel.QuarterID))
            {
                searchSql.AppendLine("	AND A.QuarterID = @QuarterID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", searchModel.QuarterID));
            }
            //人员类型
            if (!string.IsNullOrEmpty(searchModel.Flag))
            {
                searchSql.AppendLine("	AND A.Flag = @Flag ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", searchModel.Flag));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            //return SqlHelper.ExecuteSearch(comm);
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);

        }
        #endregion

        #region 查询人才储备信息
        /// <summary>
        /// 通过条件查询人才储备信息
        /// </summary>
        /// <param name="searchModel">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchEmployeeReserveInfo(EmployeeSearchModel searchModel)
        {

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT A.ID                                                             ");
            searchSql.AppendLine("      ,A.EmployeeNo                                                     ");
            searchSql.AppendLine("	  ,ISNULL(A.PYShort, '') AS PYShort                                   ");
            searchSql.AppendLine("      ,A.EmployeeName                                                   ");
            searchSql.AppendLine("      ,Case A.Sex                                                       ");
            searchSql.AppendLine("		When '1' then '男'                                                ");
            searchSql.AppendLine("		When '2' then '女'                                                ");
            searchSql.AppendLine("		else ''                                                           ");
            searchSql.AppendLine("		End as SexName                                                    ");
            searchSql.AppendLine("      ,ISNULL(CONVERT(VARCHAR(10),A.Birth, 21), '') AS Birth            ");
            searchSql.AppendLine("      ,CASE WHEN A.Birth is null THEN '' ELSE                           ");
            searchSql.AppendLine("CONVERT(VARCHAR(3),DATEDIFF(YEAR,A.Birth,getdate())) + ' 岁' END AS Age ");
            searchSql.AppendLine("	  ,ISNULL(A.Origin, '') AS Origin                                     ");
            //searchSql.AppendLine("	  ,isnull(datediff(year,WorkTime,GETDATE())+1,'') TotalSeniority ");
            //searchSql.AppendLine("    ,CASE WHEN A.TotalSeniority is null then '' else                    ");
            //searchSql.AppendLine("    CONVERT(VARCHAR,A.TotalSeniority) + ' 年' END AS TotalSeniority     ");           
            searchSql.AppendLine("      ,ISNULL(c.QuarterName, '') AS PositionTitle                     ");
            searchSql.AppendLine("	  ,ISNULL(B.TypeName, '') AS ProfessionalName                         ");
            searchSql.AppendLine(" FROM officedba.EmployeeInfo A LEFT outer join officedba.CodePublicType B on");
            searchSql.AppendLine("		A.CompanyCD = B.CompanyCD AND A.Professional = B.ID");
            searchSql.AppendLine(" LEFT join officedba.DeptQuarter c on c.id = A.QuarterID");

            searchSql.AppendLine("WHERE                                                                   ");
            searchSql.AppendLine("	A.CompanyCD = @CompanyCD                                              ");
            searchSql.AppendLine("	AND A.Flag = @Flag and (A.delFlag <> @delFlag or A.delFlag is null) ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", searchModel.CompanyCD));
            //添加人才储备参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", ConstUtil.EMPLOYEE_FLAG_TALENT));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@delFlag", "1"));

            #region 页面输入条件
            //编号
            if (!string.IsNullOrEmpty(searchModel.EmployeeNo))
            {
                searchSql.AppendLine("	AND A.EmployeeNo LIKE '%' + @EmployeeNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", searchModel.EmployeeNo));
            }
            //拼音缩写
            if (!string.IsNullOrEmpty(searchModel.PYShort))
            {
                searchSql.AppendLine("	AND A.PYShort LIKE '%' + @PYShort + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", searchModel.PYShort));
            }
            //姓名
            if (!string.IsNullOrEmpty(searchModel.EmployeeName))
            {
                searchSql.AppendLine("	AND A.EmployeeName LIKE '%' + @EmployeeName + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeName", searchModel.EmployeeName));
            }
            //性别
            if (!string.IsNullOrEmpty(searchModel.SexID))
            {
                searchSql.AppendLine("	AND A.Sex = @Sex ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Sex", searchModel.SexID));
            }
            //文化程度 
            if (!string.IsNullOrEmpty(searchModel.CultureLevel))
            {
                searchSql.AppendLine("	AND A.CultureLevel = @CultureLevel ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CultureLevel", searchModel.CultureLevel));
            }
            //专业 
            if (!string.IsNullOrEmpty(searchModel.ProfessionalID))
            {
                searchSql.AppendLine("	AND A.Professional = @ProfessionalID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProfessionalID", searchModel.ProfessionalID));
            }
            //毕业院校
            if (!string.IsNullOrEmpty(searchModel.SchoolName))
            {
                searchSql.AppendLine("	AND A.GraduateSchool LIKE '%' + @GraduateSchool + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@GraduateSchool", searchModel.SchoolName));
            }
            //应聘岗位
            if (!string.IsNullOrEmpty(searchModel.PositionTitle))
            {
                searchSql.AppendLine("	AND c.QuarterName LIKE '%' + @PositionTitle + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PositionTitle", searchModel.PositionTitle));
            }
            //工龄
            if (!string.IsNullOrEmpty(searchModel.TotalSeniority))
            {
                searchSql.AppendLine("	AND isnull(datediff(year,WorkTime,GETDATE())+1,'') = @TotalSeniority ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalSeniority", searchModel.TotalSeniority));
            }
            //手机号码
            if (!string.IsNullOrEmpty(searchModel.Mobile))
            {
                searchSql.AppendLine("	AND A.Mobile LIKE '%' + @Mobile + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Mobile", searchModel.Mobile));
            }
            #endregion

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 查询人力档案回收站
        /// <summary>
        /// 查询人力档案回收站
        /// </summary>
        /// <param name="searchModel">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchEmployeeCallBack(EmployeeSearchModel searchModel,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT A.ID                                                             ");
            searchSql.AppendLine("      ,A.EmployeeNo                                                     ");
            searchSql.AppendLine("	  ,ISNULL(A.PYShort, '') AS PYShort                                   ");
            searchSql.AppendLine("      ,A.EmployeeName                                                   ");
            searchSql.AppendLine("      ,Case A.Sex                                                       ");
            searchSql.AppendLine("		When '1' then '男'                                                ");
            searchSql.AppendLine("		When '2' then '女'                                                ");
            searchSql.AppendLine("		else ''                                                           ");
            searchSql.AppendLine("		End as SexName                                                    ");
            searchSql.AppendLine("      ,ISNULL(CONVERT(VARCHAR(10),A.Birth, 21), '') AS Birth            ");
            searchSql.AppendLine("      ,CASE WHEN A.Birth is null THEN '' ELSE                           ");
            searchSql.AppendLine("CONVERT(VARCHAR(3),DATEDIFF(YEAR,A.Birth,getdate())) + ' 岁' END AS Age ");
            searchSql.AppendLine("	  ,ISNULL(A.Origin, '') AS Origin                                     ");
            searchSql.AppendLine("    ,(case A.Flag when '1' then '在职人员' when '2' then '人才储备'     ");
            searchSql.AppendLine(" when '3' then '离职人员' end ) Flag ");
            searchSql.AppendLine("      ,ISNULL(c.QuarterName, '') AS PositionTitle                     ");
            searchSql.AppendLine("	  ,ISNULL(B.TypeName, '') AS ProfessionalName                         ");
            searchSql.AppendLine(" FROM officedba.EmployeeInfo A LEFT outer join officedba.CodePublicType B on");
            searchSql.AppendLine("		A.CompanyCD = B.CompanyCD AND A.Professional = B.ID");
            searchSql.AppendLine(" LEFT join officedba.DeptQuarter c on c.id = A.QuarterID");
            searchSql.AppendLine("WHERE                                                                   ");
            searchSql.AppendLine("	A.CompanyCD = @CompanyCD                                              ");
            searchSql.AppendLine("	AND delFlag = @delFlag ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", searchModel.CompanyCD));
            ////添加人才储备参数
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", ConstUtil.EMPLOYEE_FLAG_TALENT));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@delFlag", "1"));

            #region 页面输入条件
            //编号
            if (!string.IsNullOrEmpty(searchModel.EmployeeNo))
            {
                searchSql.AppendLine("	AND A.EmployeeNo LIKE '%' + @EmployeeNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", searchModel.EmployeeNo));
            }
            //拼音缩写
            if (!string.IsNullOrEmpty(searchModel.PYShort))
            {
                searchSql.AppendLine("	AND A.PYShort LIKE '%' + @PYShort + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", searchModel.PYShort));
            }
            //姓名
            if (!string.IsNullOrEmpty(searchModel.EmployeeName))
            {
                searchSql.AppendLine("	AND A.EmployeeName LIKE '%' + @EmployeeName + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeName", searchModel.EmployeeName));
            }
            //性别
            if (!string.IsNullOrEmpty(searchModel.SexID))
            {
                searchSql.AppendLine("	AND A.Sex = @Sex ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Sex", searchModel.SexID));
            }
            //文化程度 
            if (!string.IsNullOrEmpty(searchModel.CultureLevel))
            {
                searchSql.AppendLine("	AND A.CultureLevel = @CultureLevel ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CultureLevel", searchModel.CultureLevel));
            }
            //专业 
            if (!string.IsNullOrEmpty(searchModel.ProfessionalID))
            {
                searchSql.AppendLine("	AND A.Professional = @ProfessionalID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProfessionalID", searchModel.ProfessionalID));
            }
            //毕业院校
            if (!string.IsNullOrEmpty(searchModel.SchoolName))
            {
                searchSql.AppendLine("	AND A.GraduateSchool LIKE '%' + @GraduateSchool + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@GraduateSchool", searchModel.SchoolName));
            }
            //应聘岗位
            if (!string.IsNullOrEmpty(searchModel.PositionTitle))
            {
                searchSql.AppendLine("	AND c.QuarterName LIKE '%' + @PositionTitle + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PositionTitle", searchModel.PositionTitle));
            }
            //工龄
            if (!string.IsNullOrEmpty(searchModel.TotalSeniority))
            {
                searchSql.AppendLine("	AND isnull(datediff(year,WorkTime,GETDATE())+1,'') = @TotalSeniority ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalSeniority", searchModel.TotalSeniority));
            }
            //手机号码
            if (!string.IsNullOrEmpty(searchModel.Mobile))
            {
                searchSql.AppendLine("	AND A.Mobile LIKE '%' + @Mobile + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Mobile", searchModel.Mobile));
            }
            #endregion

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            //return SqlHelper.ExecuteSearch(comm);
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 将人员信息从 DataTable 转化为 EmployeeInfoModel

        /// <summary>
        /// 将人员信息从 DataTable 转化为 EmployeeInfoModel
        /// </summary>
        /// <param name="data">人员信息</param>
        /// <returns>EmployeeInfoModel形式的人员信息</returns>
        private static EmployeeInfoModel ChangeEmplDataToModel(DataRow data)
        {
            //定义返回的 EmployeeInfoModel
            EmployeeInfoModel model = new EmployeeInfoModel();

            //人员信息存在时，转化为model形式的数据
            if (data != null)
            {
                model.ID = GetSafeData.ValidateDataRow_Int(data, "ID");	//内部id，自动生成
                model.EmployeeNum = GetSafeData.ValidateDataRow_String(data, "EmployeeNum");	//员工工号
                model.EmployeeNo = GetSafeData.ValidateDataRow_String(data, "EmployeeNo");	//员工编号
                model.PYShort = GetSafeData.ValidateDataRow_String(data, "PYShort");	//拼音缩写
                model.CompanyCD = GetSafeData.ValidateDataRow_String(data, "CompanyCD");	//公司代码
                model.CardID = GetSafeData.ValidateDataRow_String(data, "CardID");	//身份证
                model.SafeguardCard = GetSafeData.ValidateDataRow_String(data, "SafeguardCard");	//社保卡号
                model.EmployeeName = GetSafeData.ValidateDataRow_String(data, "EmployeeName");	//姓名
                model.UsedName = GetSafeData.ValidateDataRow_String(data, "UsedName");	//曾用名
                model.NameEn = GetSafeData.ValidateDataRow_String(data, "NameEn");	//英文名
                model.Sex = GetSafeData.ValidateDataRow_String(data, "Sex");	//性别
                model.Birth = GetSafeData.GetStringFromDateTime(data, "Birth", "yyyy-MM-dd");	//出生年月
                model.Account = GetSafeData.ValidateDataRow_String(data, "Account");	//户口
                model.AccountNature = GetSafeData.ValidateDataRow_String(data, "AccountNature");	//户口性质(1城市，2农村)
                model.CountryID = GetSafeData.GetStringFromInt(data, "CountryID");	//国籍(对应分类代码表ID，国家地区)
                model.Height = GetSafeData.GetStringFromInt(data, "Height");	//身高(单位厘米)
                model.Weight = GetSafeData.GetStringFromDecimal(data, "Weight");	//体重(单位千克)
                model.Sight = GetSafeData.GetStringFromDecimal(data, "Sight");	//视力
                model.Degree = GetSafeData.ValidateDataRow_String(data, "Degree");	//最高学位
                model.PositionID = GetSafeData.GetStringFromInt(data, "PositionID");	//职称ID(对应分类代码表ID，职称)
                model.DocuType = GetSafeData.ValidateDataRow_String(data, "DocuType");	//证件类型
                //model.CreateUserID = GetSafeData.ValidateDataRow_String(data, "CreateUserID");	//创建人
                //model.CreateUserName = GetSafeData.ValidateDataRow_String(data, "CreateUserName");	//创建人
                //model.CreateDate = GetSafeData.ValidateDataRow_DateTime(data, "CreateDate");	//创建时间
                model.National = GetSafeData.GetStringFromInt(data, "NationalID");	//民族ID(对应分类代码表ID)
                model.MarriageStatus = GetSafeData.GetStringFromInt(data, "MarriageStatus");	//婚姻状况ID(对应分类代码表ID)
                model.Origin = GetSafeData.ValidateDataRow_String(data, "Origin");	//籍贯
                model.Landscape = GetSafeData.GetStringFromInt(data, "Landscape");	//政治面貌ID(对应分类代码表ID)
                model.Religion = GetSafeData.GetStringFromInt(data, "Religion");	//宗教信仰ID(对应分类代码表ID)
                model.Telephone = GetSafeData.ValidateDataRow_String(data, "Telephone");	//联系电话
                model.Mobile = GetSafeData.ValidateDataRow_String(data, "Mobile");	//手机号码
                model.EMail = GetSafeData.ValidateDataRow_String(data, "EMail");	//电子邮件
                model.OtherContact = GetSafeData.ValidateDataRow_String(data, "OtherContact");	//其他联系方式
                model.HomeAddress = GetSafeData.ValidateDataRow_String(data, "HomeAddress");	//家庭住址
                model.HealthStatus = GetSafeData.ValidateDataRow_String(data, "HealthStatus");	//健康状况
                model.CultureLevel = GetSafeData.GetStringFromInt(data, "CultureLevel");	//学历ID(对应分类代码表ID)
                model.GraduateSchool = GetSafeData.ValidateDataRow_String(data, "GraduateSchool");	//毕业院校
                model.Professional = GetSafeData.GetStringFromInt(data, "Professional");	//专业ID(对应分类代码表ID)
                model.Features = GetSafeData.ValidateDataRow_String(data, "Features");	//特长
                model.ComputerLevel = GetSafeData.ValidateDataRow_String(data, "ComputerLevel");	//计算机水平
                model.ForeignLanguage1 = GetSafeData.GetStringFromInt(data, "ForeignLanguage1");	//外语语种1 ID(对应分类代码表ID)
                model.ForeignLanguageLevel1 = GetSafeData.ValidateDataRow_String(data, "ForeignLanguageLevel1");	//外语水平1(1一般,2熟练,3精通)
                model.ForeignLanguage2 = GetSafeData.GetStringFromInt(data, "ForeignLanguage2");	//外语语种2
                model.ForeignLanguageLevel2 = GetSafeData.ValidateDataRow_String(data, "ForeignLanguageLevel2");	//外语水平2(1一般,2熟练,3精通)
                model.ForeignLanguage3 = GetSafeData.GetStringFromInt(data, "ForeignLanguage3");	//外语语种3
                model.ForeignLanguageLevel3 = GetSafeData.ValidateDataRow_String(data, "ForeignLanguageLevel3");	//外语水平3(1一般,2熟练,3精通)
                model.WorkTime = GetSafeData.GetStringFromDateTime(data, "WorkTime", "yyyy-MM-dd");	//参加工作时间
                model.TotalSeniority = GetSafeData.GetStringFromDecimal(data, "TotalSeniority");	//总工龄
                model.PhotoURL = GetSafeData.ValidateDataRow_String(data, "PhotoURL");	//员工相片
                model.PositionTitle = GetSafeData.ValidateDataRow_String(data, "PositionTitle");	//应聘职务
                model.Flag = GetSafeData.ValidateDataRow_String(data, "Flag");	//分类标识
                model.ModifiedDate = GetSafeData.ValidateDataRow_DateTime(data, "ModifiedDate");	//更新日期
                model.ModifiedUserID = GetSafeData.ValidateDataRow_String(data, "ModifiedUserID");	//更新用户ID
                model.QuarterID = GetSafeData.GetStringFromInt(data, "QuarterID");	//目前所在岗位ID（对应岗位表ID）
                model.DeptID = Convert.ToInt32(data["DeptID"].ToString());
                model.DeptName = data["DeptName"].ToString();
                model.AdminLevelID = Convert.ToInt32(data["AdminLevelID"].ToString());
                //model.EnterDate = data["EnterDate"].ToString() == "" ? DateTime.dat Convert.ToDateTime(data["EnterDate"].ToString());
                model.EnterDate = GetSafeData.ValidateDataRow_DateTime(data, "EnterDate");	//入职日期
                model.Resume = GetSafeData.ValidateDataRow_String(data, "Resume");	//简历
                model.ProfessionalDes = GetSafeData.ValidateDataRow_String(data, "ProfessionalDes");	//专业描述
            }

            return model;
        }

        #endregion

        #region 查询人员的履历信息
        /// <summary>
        /// 查询人员的履历信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="employeeNo">人员编号</param>
        /// <returns></returns>
        private static DataTable GetHistoryInfo(string companyCD, string employeeNo)
        {

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT ID                       ");
            searchSql.AppendLine("      ,CompanyCD                ");
            searchSql.AppendLine("      ,EmployeeNo               ");
            searchSql.AppendLine("      ,StartDate                ");
            searchSql.AppendLine("      ,EndDate                  ");
            searchSql.AppendLine("      ,Flag                     ");
            searchSql.AppendLine("      ,Company                  ");
            searchSql.AppendLine("      ,Department               ");
            searchSql.AppendLine("      ,WorkContent              ");
            searchSql.AppendLine("      ,LeaveReason              ");
            searchSql.AppendLine("      ,SchoolName               ");
            searchSql.AppendLine("      ,Professional             ");
            searchSql.AppendLine("      ,CultureLevel             ");
            searchSql.AppendLine("      ,ModifiedDate             ");
            searchSql.AppendLine("      ,ModifiedUserID           ");
            searchSql.AppendLine("  FROM officedba.EmployeeHistory");
            searchSql.AppendLine(" WHERE CompanyCD = @CompanyCD   ");
            searchSql.AppendLine("   AND EmployeeNo = @EmployeeNo ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameterFromString("@CompanyCD", companyCD);
            //员工编号
            param[1] = SqlHelper.GetParameterFromString("@EmployeeNo", employeeNo);
            //执行查询
            DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), param);

            return data;
        }
        #endregion

        #region 查询人员的技能信息
        /// <summary>
        /// 查询人员的技能信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="employeeNo">人员编号</param>
        /// <returns></returns>
        private static DataTable GetSkillInfo(string companyCD, string employeeNo)
        {

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT ID                     ");
            searchSql.AppendLine("      ,CompanyCD              ");
            searchSql.AppendLine("      ,EmployeeNo             ");
            searchSql.AppendLine("      ,SkillName              ");
            searchSql.AppendLine("      ,CertificateName        ");
            searchSql.AppendLine("      ,CertificateNo          ");
            searchSql.AppendLine("      ,CertificateLevel       ");
            searchSql.AppendLine("      ,IssueCompany           ");
            searchSql.AppendLine("      ,IssueDate              ");
            searchSql.AppendLine("      ,Validity               ");
            searchSql.AppendLine("      ,DeadDate               ");
            searchSql.AppendLine("      ,Remark                 ");
            searchSql.AppendLine("      ,ModifiedDate           ");
            searchSql.AppendLine("      ,ModifiedUserID         ");
            searchSql.AppendLine("  FROM officedba.EmployeeSkill");
            searchSql.AppendLine(" WHERE CompanyCD = @CompanyCD ");
            searchSql.AppendLine("  AND EmployeeNo = @EmployeeNo");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameterFromString("@CompanyCD", companyCD);
            //员工编号
            param[1] = SqlHelper.GetParameterFromString("@EmployeeNo", employeeNo);
            //执行查询
            DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), param);

            return data;
        }
        #endregion

        #region 查询人员的合同信息
        /// <summary>
        /// 查询人员的合同信息
        /// </summary>
        /// <param name="employeeID">人员ID</param>
        /// <returns></returns>
        private static DataTable GetContractInfo(int employeeID)
        {

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT ID                         ");
            searchSql.AppendLine("       ,CompanyCD                  ");
            searchSql.AppendLine("       ,ContractNo                 ");
            searchSql.AppendLine("       ,EmployeeID                 ");
            searchSql.AppendLine("       ,Title ContractName               ");
            searchSql.AppendLine("       ,ContractKind               ");

            searchSql.AppendLine("       ,CASE ContractType          ");
            searchSql.AppendLine("          WHEN '1' THEN '新签合同' ");
            searchSql.AppendLine("          WHEN '2' THEN '续签合同' ");
            searchSql.AppendLine("          WHEN '3' THEN '变更合同' ");
            searchSql.AppendLine("        END AS ContractType    ");

            searchSql.AppendLine("       ,CASE ContractProperty      ");
            searchSql.AppendLine("          WHEN '1' THEN '试用合同'   ");
            searchSql.AppendLine("          WHEN '2' THEN '正式合同' ");
            searchSql.AppendLine("          WHEN '3' THEN '临时用工合同' ");
            searchSql.AppendLine("        END AS ContractProperty    ");

            searchSql.AppendLine("       ,CASE ContractStatus        ");
            searchSql.AppendLine("          WHEN '0' THEN '失效'     ");
            searchSql.AppendLine("          WHEN '1' THEN '有效'     ");
            searchSql.AppendLine("        END AS ContractStatus      ");

            searchSql.AppendLine("       ,ContractPeriod             ");
            searchSql.AppendLine("       ,TestWage                   ");
            searchSql.AppendLine("       ,Wage                       ");
            searchSql.AppendLine("       ,CONVERT(VARCHAR(10),SigningDate, 21) AS SigningDate ");
            searchSql.AppendLine("       ,CONVERT(VARCHAR(10),StartDate, 21) AS StartDate ");
            searchSql.AppendLine("       ,CONVERT(VARCHAR(10),EndDate, 21) AS EndDate ");
            searchSql.AppendLine("       ,TrialMonthCount            ");
            searchSql.AppendLine("       ,Flag                       ");
            searchSql.AppendLine("       ,Attachment                 ");
            searchSql.AppendLine("       ,ModifiedDate               ");
            searchSql.AppendLine("       ,ModifiedUserID             ");
            searchSql.AppendLine("   FROM officedba.EmployeeContract ");
            searchSql.AppendLine("   WHERE                           ");
            searchSql.AppendLine("       EmployeeID = @EmployeeID    ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //人员ID
            param[0] = SqlHelper.GetParameter("@EmployeeID", employeeID);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 通过人员ID查询人员信息
        /// <summary>
        /// 查询人员信息
        /// </summary>
        /// <param name="employeeID">人员ID</param>
        /// <returns></returns>
        public static DataTable GetEmplDeptInfoWithID(string employeeID)
        {

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                ");
            searchSql.AppendLine(" 	A.ID                                 ");
            searchSql.AppendLine(" 	,A.EmployeeNo                        ");
            searchSql.AppendLine(" 	,A.EmployeeNum                       ");
            searchSql.AppendLine(" 	,A.EmployeeName                      ");
            searchSql.AppendLine(" 	,A.QuarterID                         ");
            searchSql.AppendLine(" 	,C.QuarterName                       ");
            searchSql.AppendLine(" 	,A.DeptID                            ");
            searchSql.AppendLine(" 	,B.DeptName                          ");
            searchSql.AppendLine(" 	,A.AdminLevelID                      ");
            searchSql.AppendLine(" 	,D.TypeName AS AdminLevelName        ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.EnterDate,21) ");
            searchSql.AppendLine(" 		AS EnterDate                     ");
            searchSql.AppendLine(" FROM                                  ");
            searchSql.AppendLine(" 	officedba.EmployeeInfo A             ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo B       ");
            searchSql.AppendLine(" 		ON A.DeptID = B.ID               ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter C    ");
            searchSql.AppendLine(" 		ON A.QuarterID = C.ID            ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType D ");
            searchSql.AppendLine(" 		ON A.AdminLevelID = D.ID         ");
            searchSql.AppendLine(" WHERE                                 ");
            searchSql.AppendLine(" 	A.ID = @EmployeeID                   ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //人员ID
            param[0] = SqlHelper.GetParameter("@EmployeeID", employeeID);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 查询在职部门岗位等相关信息
        /// <summary>
        /// 查询在职部门岗位等相关信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable GetWorkEmplInfo(EmployeeSearchModel model)
        {

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                ");
            searchSql.AppendLine(" 	 A.ID                                ");
            searchSql.AppendLine(" 	,A.EmployeeNo                        ");
            searchSql.AppendLine(" 	,A.EmployeeNum                       ");
            searchSql.AppendLine(" 	,A.CompanyCD                         ");
            searchSql.AppendLine(" 	,A.EmployeeName                      ");
            searchSql.AppendLine(" 	,A.QuarterID                         ");
            searchSql.AppendLine(" 	,C.QuarterName                       ");
            searchSql.AppendLine(" 	,A.DeptID                            ");
            searchSql.AppendLine(" 	,B.DeptName                          ");
            searchSql.AppendLine(" 	,A.AdminLevelID                      ");
            searchSql.AppendLine(" 	,D.TypeName AS AdminLevelName        ");
            searchSql.AppendLine(" FROM                                  ");
            searchSql.AppendLine(" 	officedba.EmployeeInfo A             ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo B       ");
            searchSql.AppendLine(" 		ON B.ID = A.DeptID               ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter C    ");
            searchSql.AppendLine(" 		ON C.ID = A.QuarterID            ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType D ");
            searchSql.AppendLine(" 		ON D.ID = A.AdminLevelID         ");
            searchSql.AppendLine(" WHERE                                 ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD             ");
            searchSql.AppendLine(" 	AND A.Flag = @Flag                   ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //在职标识
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", "1"));

            #region 页面查询条件
            //编号
            if (!string.IsNullOrEmpty(model.EmployeeNo))
            {
                searchSql.AppendLine("	AND A.EmployeeNo LIKE '%' + @EmployeeNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo",  model.EmployeeNo ));
            }
            //工号
            if (!string.IsNullOrEmpty(model.EmployeeNum))
            {
                searchSql.AppendLine("	AND A.EmployeeNum LIKE '%' + @EmployeeNum + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNum", model.EmployeeNum));
            }
            //姓名
            if (!string.IsNullOrEmpty(model.EmployeeName))
            {
                searchSql.AppendLine("	AND A.EmployeeName LIKE '%' + @EmployeeName + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeName",  model.EmployeeName ));
            }
            //所在岗位
            if (!string.IsNullOrEmpty(model.QuarterID))
            {
                searchSql.AppendLine("	AND A.QuarterID = @QuarterID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));
            }
            //岗位职等
            if (!string.IsNullOrEmpty(model.AdminLevelID))
            {
                searchSql.AppendLine("	AND A.AdminLevelID = @AdminLevelID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdminLevelID", model.AdminLevelID));
            }
            #endregion

            //设置排序
            searchSql.AppendLine(" ORDER BY A.DeptID, A.EnterDate ");
            //设定comm的SQL文
            comm.CommandText = searchSql.ToString();

            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 通过检索条件查询在职人员相关信息
        /// <summary>
        /// 通过检索条件查询在职人员相关信息
        /// </summary>
        /// <param name="model">检索条件</param>
        /// <returns></returns>
        public static DataTable SearchEmplInfo(EmployeeSearchModel model)
        {
            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                           ");
            searchSql.AppendLine(" 	 A.ID                                           ");
            searchSql.AppendLine(" 	,ISNULL(A.EmployeeNo, '') AS EmployeeNo         ");
            searchSql.AppendLine(" 	,ISNULL(A.EmployeeNum, '') AS EmployeeNum       ");
            searchSql.AppendLine(" 	,ISNULL(A.EmployeeName, '') AS EmployeeName     ");
            searchSql.AppendLine(" 	,ISNULL(C.QuarterName, '') AS QuarterName       ");
            searchSql.AppendLine(" 	,ISNULL(A.QuarterID, '') AS QuarterID           ");
            searchSql.AppendLine(" 	,ISNULL(B.DeptName, '') AS DeptName             ");
            searchSql.AppendLine(" 	,ISNULL(A.DeptID, '') AS Dept                   ");
            searchSql.AppendLine(" 	,ISNULL(D.TypeName, '') AS AdminLevelName       ");
            searchSql.AppendLine(" 	,ISNULL(A.AdminLevelID, '') AS AdminLevelID     ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.EnterDate,21),'') ");
            searchSql.AppendLine(" 		AS EnterDate                                ");
            searchSql.AppendLine(" FROM                                             ");
            searchSql.AppendLine(" 	officedba.EmployeeInfo A                        ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo B                  ");
            searchSql.AppendLine(" 		ON A.DeptID = B.ID                          ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter C               ");
            searchSql.AppendLine(" 		ON A.QuarterID = C.ID                       ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType D            ");
            searchSql.AppendLine(" 		ON A.AdminLevelID = D.ID                    ");
            searchSql.AppendLine(" WHERE                                            ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                        ");
            searchSql.AppendLine(" 	AND A.Flag = @Flag                              ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //非离职
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", "1"));

            //编号
            if (!string.IsNullOrEmpty(model.EmployeeNo))
            {
                searchSql.AppendLine("	AND A.EmployeeNo LIKE  '%' + @EmployeeNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", model.EmployeeNo));
            }
            //姓名
            if (!string.IsNullOrEmpty(model.EmployeeName))
            {
                searchSql.AppendLine("	AND A.EmployeeName LIKE  '%' + @EmployeeName + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeName", model.EmployeeName));
            }
            //姓名
            if (!string.IsNullOrEmpty(model.Dept))
            {
                searchSql.AppendLine("	AND B.DeptName  LIKE  '%' + @DeptName + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptName", model.Dept));
            }
            //岗位
            if (!string.IsNullOrEmpty(model.QuarterID))
            {
                searchSql.AppendLine("	AND A.QuarterID = @QuarterID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));
            }
            //岗位职等
            if (!string.IsNullOrEmpty(model.AdminLevelID))
            {
                searchSql.AppendLine("	AND A.AdminLevelID = @AdminLevelID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdminLevelID", model.AdminLevelID));
            }

            //设定comm的SQL文
            comm.CommandText = searchSql.ToString();

            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #endregion

        #region 添加人员以及相关的履历技能信息

        /// <summary>
        /// 添加人员信息
        /// </summary>
        /// <param name="model">人员信息</param>
        /// <returns></returns>
        public static bool InsertEmployeeInfo(EmployeeInfoModel model)
        {

            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine("INSERT INTO officedba.EmployeeInfo ");
            insertSql.AppendLine("           (EmployeeNum            ");
            insertSql.AppendLine("           ,EmployeeNo             ");
            insertSql.AppendLine("           ,PYShort                ");
            insertSql.AppendLine("           ,CompanyCD              ");
            insertSql.AppendLine("           ,CardID                 ");
            insertSql.AppendLine("           ,SafeguardCard          ");
            insertSql.AppendLine("           ,EmployeeName           ");
            insertSql.AppendLine("           ,UsedName               ");
            insertSql.AppendLine("           ,NameEn                 ");
            insertSql.AppendLine("           ,Sex                    ");
            insertSql.AppendLine("           ,Birth                  ");
            insertSql.AppendLine("           ,Account                ");
            insertSql.AppendLine("           ,AccountNature          ");
            insertSql.AppendLine("           ,CountryID              ");
            insertSql.AppendLine("           ,Height                 ");
            insertSql.AppendLine("           ,Weight                 ");
            insertSql.AppendLine("           ,Sight                  ");
            insertSql.AppendLine("           ,Degree                 ");
            insertSql.AppendLine("           ,PositionID             ");
            insertSql.AppendLine("           ,DocuType               ");
            //insertSql.AppendLine("           ,CreateUserID           ");
            //insertSql.AppendLine("           ,CreateDate             ");
            insertSql.AppendLine("           ,NationalID             ");
            insertSql.AppendLine("           ,MarriageStatus         ");
            insertSql.AppendLine("           ,Origin                 ");
            insertSql.AppendLine("           ,Landscape              ");
            insertSql.AppendLine("           ,Religion               ");
            insertSql.AppendLine("           ,Telephone              ");
            insertSql.AppendLine("           ,Mobile                 ");
            insertSql.AppendLine("           ,EMail                  ");
            insertSql.AppendLine("           ,OtherContact           ");
            insertSql.AppendLine("           ,HomeAddress            ");
            insertSql.AppendLine("           ,HealthStatus           ");
            insertSql.AppendLine("           ,CultureLevel           ");
            insertSql.AppendLine("           ,GraduateSchool         ");
            insertSql.AppendLine("           ,Professional           ");
            insertSql.AppendLine("           ,Features               ");
            insertSql.AppendLine("           ,ComputerLevel          ");
            insertSql.AppendLine("           ,ForeignLanguage1       ");
            insertSql.AppendLine("           ,ForeignLanguageLevel1  ");
            insertSql.AppendLine("           ,ForeignLanguage2       ");
            insertSql.AppendLine("           ,ForeignLanguageLevel2  ");
            insertSql.AppendLine("           ,ForeignLanguage3       ");
            insertSql.AppendLine("           ,ForeignLanguageLevel3  ");
            insertSql.AppendLine("           ,WorkTime               ");
            insertSql.AppendLine("           ,TotalSeniority         ");
            insertSql.AppendLine("           ,PhotoURL               ");
            insertSql.AppendLine("           ,PositionTitle          ");
            insertSql.AppendLine("           ,Flag                   ");
            insertSql.AppendLine("           ,ModifiedDate           ");
            insertSql.AppendLine("           ,ModifiedUserID         ");
            insertSql.AppendLine("           ,QuarterID              ");
            insertSql.AppendLine("           ,DeptID              ");
            insertSql.AppendLine("           ,AdminLevelID              ");
            insertSql.AppendLine("           ,EnterDate              ");
            insertSql.AppendLine("           ,ProfessionalDes              ");
            insertSql.AppendLine("           ,Resume)                ");
            insertSql.AppendLine("     VALUES                        ");
            insertSql.AppendLine("           (@EmployeeNum           ");
            insertSql.AppendLine("           ,@EmployeeNo            ");
            insertSql.AppendLine("           ,@PYShort               ");
            insertSql.AppendLine("           ,@CompanyCD             ");
            insertSql.AppendLine("           ,@CardID                ");
            insertSql.AppendLine("           ,@SafeguardCard         ");
            insertSql.AppendLine("           ,@EmployeeName          ");
            insertSql.AppendLine("           ,@UsedName              ");
            insertSql.AppendLine("           ,@NameEn                ");
            insertSql.AppendLine("           ,@Sex                   ");
            insertSql.AppendLine("           ,@Birth                 ");
            insertSql.AppendLine("           ,@Account               ");
            insertSql.AppendLine("           ,@AccountNature         ");
            insertSql.AppendLine("           ,@CountryID             ");
            insertSql.AppendLine("           ,@Height                ");
            insertSql.AppendLine("           ,@Weight                ");
            insertSql.AppendLine("           ,@Sight                 ");
            insertSql.AppendLine("           ,@Degree                ");
            insertSql.AppendLine("           ,@PositionID            ");
            insertSql.AppendLine("           ,@DocuType              ");
            //insertSql.AppendLine("           ,@CreateUserID          ");
            //insertSql.AppendLine("           ,@CreateDate            ");
            insertSql.AppendLine("           ,@National              ");
            insertSql.AppendLine("           ,@MarriageStatus        ");
            insertSql.AppendLine("           ,@Origin                ");
            insertSql.AppendLine("           ,@Landscape             ");
            insertSql.AppendLine("           ,@Religion              ");
            insertSql.AppendLine("           ,@Telephone             ");
            insertSql.AppendLine("           ,@Mobile                ");
            insertSql.AppendLine("           ,@EMail                 ");
            insertSql.AppendLine("           ,@OtherContact          ");
            insertSql.AppendLine("           ,@HomeAddress           ");
            insertSql.AppendLine("           ,@HealthStatus          ");
            insertSql.AppendLine("           ,@CultureLevel          ");
            insertSql.AppendLine("           ,@GraduateSchool        ");
            insertSql.AppendLine("           ,@Professional          ");
            insertSql.AppendLine("           ,@Features              ");
            insertSql.AppendLine("           ,@ComputerLevel         ");
            insertSql.AppendLine("           ,@ForeignLanguage1      ");
            insertSql.AppendLine("           ,@ForeignLanguageLevel1 ");
            insertSql.AppendLine("           ,@ForeignLanguage2      ");
            insertSql.AppendLine("           ,@ForeignLanguageLevel2 ");
            insertSql.AppendLine("           ,@ForeignLanguage3      ");
            insertSql.AppendLine("           ,@ForeignLanguageLevel3 ");
            insertSql.AppendLine("           ,@WorkTime              ");
            insertSql.AppendLine("           ,@TotalSeniority        ");
            insertSql.AppendLine("           ,@PhotoURL              ");
            insertSql.AppendLine("           ,@PositionTitle         ");
            insertSql.AppendLine("           ,@Flag                  ");
            insertSql.AppendLine("           ,getdate()              ");
            insertSql.AppendLine("           ,@ModifiedUserID        ");
            insertSql.AppendLine("           ,@QuarterID             ");
            insertSql.AppendLine("           ,@DeptID             ");
            insertSql.AppendLine("           ,@AdminLevelID             ");
            insertSql.AppendLine("           ,@EnterDate             ");
            insertSql.AppendLine("           ,@ProfessionalDes              ");
            insertSql.AppendLine("           ,@Resume)               ");

            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);
            //定义更新列表
            ArrayList lstInsert = new ArrayList();
            //添加基本信息更新命令
            lstInsert.Add(comm);
            //登陆或者更新履历信息
            EditEmployeeHistoryInfo(lstInsert, model.HistoryList, model.EmployeeNo, model.CompanyCD);
            //登陆或者更新技能信息
            EditEmployeeSkillInfo(lstInsert, model.SkillList, model.EmployeeNo, model.CompanyCD);

            //执行更新操作并返回更新结果
            return SqlHelper.ExecuteTransWithArrayList(lstInsert);

        }

        #endregion

        #region 更新人员以及相关的履历技能信息

        /// <summary>
        /// 更新人员信息
        /// </summary>
        /// <param name="model">人员信息</param>
        /// <returns></returns>
        public static bool UpdateEmployeeInfo(EmployeeInfoModel model)
        {

            #region 更新SQL拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine("UPDATE officedba.EmployeeInfo                        ");
            updateSql.AppendLine("   SET                                               ");
            updateSql.AppendLine("	    EmployeeNum = @EmployeeNum                     ");
            updateSql.AppendLine("      ,PYShort = @PYShort                            ");
            updateSql.AppendLine("      ,CardID = @CardID                              ");
            updateSql.AppendLine("      ,SafeguardCard = @SafeguardCard                ");
            updateSql.AppendLine("      ,EmployeeName = @EmployeeName                  ");
            updateSql.AppendLine("      ,UsedName = @UsedName                          ");
            updateSql.AppendLine("      ,NameEn = @NameEn                              ");
            updateSql.AppendLine("      ,Sex = @Sex                                    ");
            updateSql.AppendLine("      ,Birth = @Birth                                ");
            updateSql.AppendLine("      ,Account = @Account                            ");
            updateSql.AppendLine("      ,AccountNature = @AccountNature                ");
            updateSql.AppendLine("      ,CountryID = @CountryID                        ");
            updateSql.AppendLine("      ,Height = @Height                              ");
            updateSql.AppendLine("      ,Weight = @Weight                              ");
            updateSql.AppendLine("      ,Sight = @Sight                                ");
            updateSql.AppendLine("      ,Degree = @Degree                              ");
            updateSql.AppendLine("      ,PositionID = @PositionID                      ");
            updateSql.AppendLine("      ,DocuType = @DocuType                          ");
            updateSql.AppendLine("      ,NationalID = @National                        ");
            updateSql.AppendLine("      ,MarriageStatus = @MarriageStatus              ");
            updateSql.AppendLine("      ,Origin = @Origin                              ");
            updateSql.AppendLine("      ,Landscape = @Landscape                        ");
            updateSql.AppendLine("      ,Religion = @Religion                          ");
            updateSql.AppendLine("      ,Telephone = @Telephone                        ");
            updateSql.AppendLine("      ,Mobile = @Mobile                              ");
            updateSql.AppendLine("      ,EMail = @EMail                                ");
            updateSql.AppendLine("      ,OtherContact = @OtherContact                  ");
            updateSql.AppendLine("      ,HomeAddress = @HomeAddress                    ");
            updateSql.AppendLine("      ,HealthStatus = @HealthStatus                  ");
            updateSql.AppendLine("      ,CultureLevel = @CultureLevel                  ");
            updateSql.AppendLine("      ,GraduateSchool = @GraduateSchool              ");
            updateSql.AppendLine("      ,Professional = @Professional                  ");
            updateSql.AppendLine("      ,Features = @Features                          ");
            updateSql.AppendLine("      ,ComputerLevel = @ComputerLevel                ");
            updateSql.AppendLine("      ,ForeignLanguage1 = @ForeignLanguage1          ");
            updateSql.AppendLine("      ,ForeignLanguageLevel1 = @ForeignLanguageLevel1");
            updateSql.AppendLine("      ,ForeignLanguage2 = @ForeignLanguage2          ");
            updateSql.AppendLine("      ,ForeignLanguageLevel2 = @ForeignLanguageLevel2");
            updateSql.AppendLine("      ,ForeignLanguage3 = @ForeignLanguage3          ");
            updateSql.AppendLine("      ,ForeignLanguageLevel3 = @ForeignLanguageLevel3");
            updateSql.AppendLine("      ,WorkTime = @WorkTime                          ");
            updateSql.AppendLine("      ,TotalSeniority = @TotalSeniority              ");
            updateSql.AppendLine("      ,PhotoURL = @PhotoURL                          ");
            updateSql.AppendLine("      ,PositionTitle = @PositionTitle                ");
            updateSql.AppendLine("      ,Flag = @Flag                                  ");
            updateSql.AppendLine("      ,ModifiedDate = getdate()                      ");
            updateSql.AppendLine("      ,ModifiedUserID = @ModifiedUserID              ");
            updateSql.AppendLine("      ,QuarterID = @QuarterID                        ");
            updateSql.AppendLine("      ,DeptID = @DeptID                        ");
            updateSql.AppendLine("      ,AdminLevelID = @AdminLevelID                        ");
            updateSql.AppendLine("      ,EnterDate = @EnterDate                        ");
            updateSql.AppendLine("      ,Resume = @Resume                              ");
            updateSql.AppendLine("      ,ProfessionalDes = @ProfessionalDes             ");
            updateSql.AppendLine(" WHERE CompanyCD = @CompanyCD                        ");
            updateSql.AppendLine("      AND EmployeeNo = @EmployeeNo                   ");

            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);
            //定义更新列表
            ArrayList lstUpdate = new ArrayList();
            //添加基本信息更新命令
            lstUpdate.Add(comm);
            //登陆或者更新履历信息
            EditEmployeeHistoryInfo(lstUpdate, model.HistoryList, model.EmployeeNo, model.CompanyCD);
            //登陆或者更新技能信息
            EditEmployeeSkillInfo(lstUpdate, model.SkillList, model.EmployeeNo, model.CompanyCD);

            //执行更新操作并返回更新结果
            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);
        }

        #endregion

        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">人员信息</param>
        private static void SetSaveParameter(SqlCommand comm, EmployeeInfoModel model)
        {
            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNum", model.EmployeeNum));	//员工编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", model.EmployeeNo));	//员工工号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", model.PYShort));	//拼音缩写
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CardID", model.CardID));	//身份证
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SafeguardCard", model.SafeguardCard));	//社保卡号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeName", model.EmployeeName));	//姓名
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedName", model.UsedName));	//曾用名
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NameEn", model.NameEn));	//英文名
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Sex", model.Sex));	//性别
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Birth", model.Birth));	//出生年月
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Account", model.Account));	//户口
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AccountNature", model.AccountNature));	//户口性质(1城市，2农村)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountryID", model.CountryID));	//国籍(对应分类代码表ID，国家地区)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Height", model.Height));	//身高(单位厘米)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Weight", model.Weight));	//体重(单位千克)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Sight", model.Sight));	//视力
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Degree", model.Degree));	//最高学位
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PositionID", model.PositionID));	//职称ID(对应分类代码表ID，职称)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DocuType", model.DocuType));	//证件类型
            ////登陆时，设置下面两参数
            //if (ConstUtil.EDIT_FLAG_INSERT.Equals(model.EditFlag))
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateUserID", model.CreateUserID));	//创建人
            //    comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));	//创建时间
            //}
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@National", model.National));	//民族ID(对应分类代码表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MarriageStatus", model.MarriageStatus));	//婚姻状况ID(对应分类代码表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Origin", model.Origin));	//籍贯
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Landscape", model.Landscape));	//政治面貌ID(对应分类代码表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Religion", model.Religion));	//宗教信仰ID(对应分类代码表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Telephone", model.Telephone));	//联系电话
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Mobile", model.Mobile));	//手机号码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EMail", model.EMail));	//电子邮件
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OtherContact", model.OtherContact));	//其他联系方式
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@HomeAddress", model.HomeAddress));	//家庭住址
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@HealthStatus", model.HealthStatus));	//健康状况
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CultureLevel", model.CultureLevel));	//学历ID(对应分类代码表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@GraduateSchool", model.GraduateSchool));	//毕业院校
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Professional", model.Professional));	//专业ID(对应分类代码表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Features", model.Features));	//特长
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ComputerLevel", model.ComputerLevel));	//计算机水平
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ForeignLanguage1", model.ForeignLanguage1));	//外语语种1 ID(对应分类代码表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ForeignLanguageLevel1", model.ForeignLanguageLevel1));	//外语水平1(1一般,2熟练,3精通)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ForeignLanguage2", model.ForeignLanguage2));	//外语语种2
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ForeignLanguageLevel2", model.ForeignLanguageLevel2));	//外语水平2(1一般,2熟练,3精通)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ForeignLanguage3", model.ForeignLanguage3));	//外语语种3
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ForeignLanguageLevel3", model.ForeignLanguageLevel3));	//外语水平3(1一般,2熟练,3精通)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@WorkTime", model.WorkTime));	//参加工作时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalSeniority", model.TotalSeniority));	//总工龄
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PhotoURL", model.PagePhotoURL));	//员工相片
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PositionTitle", model.PositionTitle));	//应聘职务
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", model.Flag));	//分类标识
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));	//更新用户ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));	//目前所在岗位ID（对应岗位表ID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Resume", model.PageResume));	//简历
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID.ToString()));	//部门ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdminLevelID", model.AdminLevelID.ToString()));	//岗位职等ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EnterDate", model.EnterDate.ToString()));	//入职时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProfessionalDes", model.ProfessionalDes));	//专业描述
        }
        #endregion

        #region 删除人员以及相关的履历技能信息

        #region 删除人员信息
        /// <summary>
        /// 删除人员信息
        /// </summary>
        /// <param name="employeeNo">人员编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteEmployeeInfo(string employeeNo, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" update officedba.EmployeeInfo set delFlag = '1' ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" EmployeeNo In( " + employeeNo + ")");
            deleteSql.AppendLine(" AND CompanyCD = @CompanyCD ");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            //comm.Parameters.Add(SqlHelper.GetParameter("@EmployeeNo", employeeNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));

            //定义更新列表
            ArrayList lstDelete = new ArrayList();
            //添加基本信息更新命令
            lstDelete.Add(comm);
            //删除履历信息
            //DeleteHistoryInfo(lstDelete, companyCD, employeeNo);
            //删除技能信息
            //DeleteSkillInfo(lstDelete, companyCD, employeeNo);
            //执行删除并返回
            return SqlHelper.ExecuteTransWithArrayList(lstDelete);
        }
        #endregion

        #region 还原人员信息
        /// <summary>
        /// 还原人员信息
        /// </summary>
        /// <param name="employeeNo">人员编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool CallBack(string employeeNo, string companyCD)
        {
            //还原SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" update officedba.EmployeeInfo set delFlag = '0' ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" EmployeeNo In( " + employeeNo + ")");
            deleteSql.AppendLine(" AND CompanyCD = @CompanyCD ");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            //comm.Parameters.Add(SqlHelper.GetParameter("@EmployeeNo", employeeNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));

            //定义更新列表
            ArrayList lstDelete = new ArrayList();
            //添加基本信息更新命令
            lstDelete.Add(comm);
            //删除履历信息
            //DeleteHistoryInfo(lstDelete, companyCD, employeeNo);
            //删除技能信息
            //DeleteSkillInfo(lstDelete, companyCD, employeeNo);
            //执行删除并返回
            return SqlHelper.ExecuteTransWithArrayList(lstDelete);
        }
        #endregion

        #region 彻底删除人员信息
        /// <summary>
        /// 彻底删除人员信息
        /// </summary>
        /// <param name="employeeNo">人员编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteEmp(string employeeNo, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" delete from officedba.EmployeeInfo ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" EmployeeNo In( " + employeeNo + ")");
            deleteSql.AppendLine(" AND CompanyCD = @CompanyCD ");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            //comm.Parameters.Add(SqlHelper.GetParameter("@EmployeeNo", employeeNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));

            //定义更新列表
            ArrayList lstDelete = new ArrayList();
            //添加基本信息更新命令
            lstDelete.Add(comm);
            //删除履历信息
            DeleteHistoryInfo(lstDelete, companyCD, employeeNo);
            //删除技能信息
            DeleteSkillInfo(lstDelete, companyCD, employeeNo);
            //执行删除并返回
            return SqlHelper.ExecuteTransWithArrayList(lstDelete);
        }
        #endregion

        #region 删除人员履历信息
        /// <summary>
        /// 删除人员履历信息
        /// </summary>
        /// <param name="lstCommand">命令列表</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="employeeNo">人员编号</param>
        /// <returns></returns>
        private static void DeleteHistoryInfo(ArrayList lstCommand, string companyCD, string employeeNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.EmployeeHistory ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" EmployeeNo In( " + employeeNo + " ) ");
            deleteSql.AppendLine(" AND CompanyCD = @CompanyCD ");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            //comm.Parameters.Add(SqlHelper.GetParameter("@EmployeeNo", employeeNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
            //添加命令
            lstCommand.Add(comm);
        }
        #endregion

        #region 删除人员技能信息
        /// <summary>
        /// 删除人员技能信息
        /// </summary>
        /// <param name="lstCommand">命令列表</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="employeeNo">人员编号</param>
        /// <returns></returns>
        private static void DeleteSkillInfo(ArrayList lstCommand, string companyCD, string employeeNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.EmployeeSkill ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" EmployeeNo in ( " + employeeNo + " ) ");
            deleteSql.AppendLine(" AND CompanyCD = @CompanyCD ");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            //comm.Parameters.Add(SqlHelper.GetParameter("@EmployeeNo", employeeNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
            //添加命令
            lstCommand.Add(comm);
        }
        #endregion

        #endregion

        #region 登陆或更新人员的履历信息

        /// <summary>
        /// 登陆或更新人员的履历信息
        /// 包括工作履历和学习履历
        /// </summary>
        /// <param name="lstCommand">数据库操作命令列表</param>
        /// <param name="history">履历信息</param>
        /// <param name="employeeNo">员工编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        private static void EditEmployeeHistoryInfo(ArrayList lstCommand, ArrayList history, string employeeNo, string companyCD)
        {
            //全删全插方式插入履历信息

            /* 删除操作 */

            //删除SQL
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.EmployeeHistory ");
            deleteSql.AppendLine("   WHERE                               ");
            deleteSql.AppendLine("      EmployeeNo = @EmployeeNo         ");
            deleteSql.AppendLine("    AND CompanyCD = @CompanyCD         ");
            //定义Command
            SqlCommand comm = new SqlCommand();
            //设置执行 Transact-SQL 语句
            comm.CommandText = deleteSql.ToString();
            //员工编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", employeeNo));
            //公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //添加删除命令
            lstCommand.Add(comm);

            //未填写履历时，返回true
            if (history == null || history.Count < 1)
            {
                return;
            }

            /* 插入操作 */

            #region 插入SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine("INSERT INTO officedba.EmployeeHistory");
            insertSql.AppendLine("           (CompanyCD                ");
            insertSql.AppendLine("           ,EmployeeNo               ");
            insertSql.AppendLine("           ,StartDate                ");
            insertSql.AppendLine("           ,EndDate                  ");
            insertSql.AppendLine("           ,Flag                     ");
            insertSql.AppendLine("           ,Company                  ");
            insertSql.AppendLine("           ,Department               ");
            insertSql.AppendLine("           ,WorkContent              ");
            insertSql.AppendLine("           ,LeaveReason              ");
            insertSql.AppendLine("           ,SchoolName               ");
            insertSql.AppendLine("           ,Professional             ");
            insertSql.AppendLine("           ,CultureLevel             ");
            insertSql.AppendLine("           ,ModifiedDate             ");
            insertSql.AppendLine("           ,ModifiedUserID)          ");
            insertSql.AppendLine("     VALUES                          ");
            insertSql.AppendLine("           (@CompanyCD               ");
            insertSql.AppendLine("           ,@EmployeeNo              ");
            insertSql.AppendLine("           ,@StartDate               ");
            insertSql.AppendLine("           ,@EndDate                 ");
            insertSql.AppendLine("           ,@Flag                    ");
            insertSql.AppendLine("           ,@Company                 ");
            insertSql.AppendLine("           ,@Department              ");
            insertSql.AppendLine("           ,@WorkContent             ");
            insertSql.AppendLine("           ,@LeaveReason             ");
            insertSql.AppendLine("           ,@SchoolName              ");
            insertSql.AppendLine("           ,@Professional            ");
            insertSql.AppendLine("           ,@CultureLevel            ");
            insertSql.AppendLine("           ,getdate()                ");
            insertSql.AppendLine("           ,@ModifiedUserID)         ");
            #endregion

            //遍历所有的履历信息
            for (int i = 0; i < history.Count; i++)
            {
                //获取单条履历记录
                EmployeeHistoryModel model = (EmployeeHistoryModel)history[i];
                //定义Command
                comm = new SqlCommand();
                //设置执行 Transact-SQL 语句
                comm.CommandText = insertSql.ToString();

                #region 设置参数
                /* 更新时ID的设置在拼写SQL文时已添加，这里不再做判断 */
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));	//公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", employeeNo));	//员工编号（对应员工信息表中的员工编号）
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));	//开始时间
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));	//结束时间
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", model.Flag));	//区分(1工作，2 学习)
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Company", model.Company));	//工作单位
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Department", model.Department));	//所在部门
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@WorkContent", model.WorkContent));	//工作内容
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@LeaveReason", model.LeaveReason));	//离职原因
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SchoolName", model.SchoolName));	//学校名称
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Professional", model.Professional));	//专业ID(对应分类代码表ID)
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CultureLevel", model.CultureLevel));	//学历ID(对应分类代码表ID)
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));	//更新用户ID
                #endregion

                //添加插入命令
                lstCommand.Add(comm);

            }
        }

        #endregion

        #region 登陆或更新人员的技能信息

        /// <summary>
        /// 登陆或更新人员的技能信息
        /// </summary>
        /// <param name="lstCommand">数据库操作命令列表</param>
        /// <param name="skill">技能信息</param>
        /// <param name="employeeNo">员工编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        private static void EditEmployeeSkillInfo(ArrayList lstCommand, ArrayList skill, string employeeNo, string companyCD)
        {
            //全删全插方式插入履历信息

            /* 删除操作 */

            //删除SQL
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.EmployeeSkill   ");
            deleteSql.AppendLine("   WHERE                               ");
            deleteSql.AppendLine("      EmployeeNo = @EmployeeNo         ");
            deleteSql.AppendLine("    AND CompanyCD = @CompanyCD         ");
            //定义Command
            SqlCommand comm = new SqlCommand();
            //设置执行 Transact-SQL 语句
            comm.CommandText = deleteSql.ToString();
            //员工编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", employeeNo));
            //公司编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));

            lstCommand.Add(comm);

            //未填写履历时，返回true
            if (skill == null || skill.Count < 1)
            {
                return;
            }

            /* 插入操作 */

            #region 插入SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine("INSERT INTO officedba.EmployeeSkill");
            insertSql.AppendLine("           (CompanyCD              ");
            insertSql.AppendLine("           ,EmployeeNo            ");
            insertSql.AppendLine("           ,SkillName              ");
            insertSql.AppendLine("           ,CertificateName        ");
            insertSql.AppendLine("           ,CertificateNo          ");
            insertSql.AppendLine("           ,CertificateLevel       ");
            insertSql.AppendLine("           ,IssueCompany           ");
            insertSql.AppendLine("           ,IssueDate              ");
            insertSql.AppendLine("           ,Validity               ");
            insertSql.AppendLine("           ,DeadDate               ");
            insertSql.AppendLine("           ,Remark                 ");
            insertSql.AppendLine("           ,ModifiedDate           ");
            insertSql.AppendLine("           ,ModifiedUserID)        ");
            insertSql.AppendLine("     VALUES                        ");
            insertSql.AppendLine("           (@CompanyCD             ");
            insertSql.AppendLine("           ,@EmployeeNo            ");
            insertSql.AppendLine("           ,@SkillName             ");
            insertSql.AppendLine("           ,@CertificateName       ");
            insertSql.AppendLine("           ,@CertificateNo         ");
            insertSql.AppendLine("           ,@CertificateLevel      ");
            insertSql.AppendLine("           ,@IssueCompany          ");
            insertSql.AppendLine("           ,@IssueDate             ");
            insertSql.AppendLine("           ,@Validity              ");
            insertSql.AppendLine("           ,@DeadDate              ");
            insertSql.AppendLine("           ,@Remark                ");
            insertSql.AppendLine("           ,getdate()              ");
            insertSql.AppendLine("           ,@ModifiedUserID)       ");
            #endregion

            //遍历所有的技能信息
            for (int i = 0; i < skill.Count; i++)
            {
                //获取单条技能记录
                EmployeeSkillModel model = (EmployeeSkillModel)skill[i];
                //定义Command
                comm = new SqlCommand();
                //设置执行 Transact-SQL 语句
                comm.CommandText = insertSql.ToString();

                #region 设置参数
                /* 更新时ID的设置在拼写SQL文时已添加，这里不再做判断 */
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));//公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", employeeNo));//员工编号（对应员工信息表中的员工编号）
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SkillName", model.SkillName));//技能名称
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CertificateName", model.CertificateName));//证件名称
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CertificateNo", model.CertificateNo));//证件编号
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CertificateLevel", model.CertificateLevel));//证件等级
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@IssueCompany", model.IssueCompany));//发证单位
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@IssueDate", model.IssueDate));//发证时间
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Validity", model.Validity));//有效期
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeadDate", model.DeadDate));//失效时间
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//备注
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//更新用户ID
                #endregion

                lstCommand.Add(comm);

            }
        }

        #endregion

        #region 入职处理更新数据库
        /// <summary>
        /// 入职处理更新数据库
        /// </summary>
        /// <param name="model">人员信息</param>
        /// <returns></returns>
        public static bool UpdateEnterInfo(EmployeeSearchModel model)
        {

            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.EmployeeInfo      ");
            updateSql.AppendLine(" SET                                ");
            updateSql.AppendLine(" 	 DeptID = @DeptID                 ");
            updateSql.AppendLine(" 	,QuarterID = @QuarterID           ");
            //updateSql.AppendLine(" 	,AdminID = @AdminID               ");
            updateSql.AppendLine(" 	,AdminLevelID = @AdminLevelID     ");
            updateSql.AppendLine(" 	,PositionID = @PositionID         ");
            updateSql.AppendLine(" 	,PositionTitle = @PositionTitle   ");
            updateSql.AppendLine(" 	,EnterDate = @EnterDate           ");
            updateSql.AppendLine(" 	,Flag = @Flag                     ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	ID = @EmplID                      ");
            #endregion

            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //人员ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmplID", model.ID));
            //部门
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.Dept));
            //岗位
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));
            //行政等级
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdminID", model.AdminID));
            //岗位职等
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdminLevelID", model.AdminLevelID));
            //职称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PositionID", model.PositionID));
            //职务
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PositionTitle", model.PositionTitle));
            //入职时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EnterDate", model.EnterDate));
            //状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", "1"));
            //最后修改者
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));

            //执行更新并设置更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion


        /// <summary>
        /// 获取联系方式和一些基本信息
        /// </summary>
        /// <param name="idList">ID列表</param>
        /// <returns></returns>
        public static DataTable GetContact(string idList)
        {
            if (idList + "" == "")
            {
                idList = "-1";
            }

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT ID                    ");
            searchSql.AppendLine("      ,CardID                ");
            searchSql.AppendLine("      ,EmployeeName          ");
            searchSql.AppendLine("      ,Sex                   ");
            searchSql.AppendLine("      ,Birth                 ");
            searchSql.AppendLine("      ,Mobile                ");
            searchSql.AppendLine("      ,EMail                 ");
            searchSql.AppendLine("      ,PhotoURL              ");
            searchSql.AppendLine("  FROM officedba.EmployeeInfo ");
            searchSql.AppendLine(" WHERE                       ");
            searchSql.AppendLine("	ID in (" + idList + ") ");
            #endregion

            //设置参数


            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString());
        }

        #region 根据ID获取员工手机号码的方法
        /// <summary>
        /// 根据ID获取员工手机号码的方法
        /// </summary>
        /// <param name="idList">ID列表</param>
        /// <returns>手机号码列表</returns>
        public static DataTable GetModileByID(string idList)
        {
            if (idList + "" == "")
            {
                idList = "-1";
            }

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT ID,EmployeeName       ");
            searchSql.AppendLine("      ,Mobile                ");
            searchSql.AppendLine("  FROM officedba.EmployeeInfo ");
            searchSql.AppendLine(" WHERE                       ");
            searchSql.AppendLine("	ID in (" + idList + ") ");
            searchSql.AppendLine("	AND (Mobile <> null or Mobile <> '')");
            #endregion

            return SqlHelper.ExecuteSql(searchSql.ToString());
        }
        #endregion

        #region 在职员工性别统计
        /// <summary>
        /// 在职员工性别统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetEmployeeSex(string CompanyCD, string DeptID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = " select " +
                                   " isnull(di.DeptName,'')DeptName,c.DeptID,c.ManCount,c.WoManCount,c.num" +
                               " from " +
                                   " (select isnull(a.DeptID,b.DeptID)DeptID," +
                                           " isnull(a.ManCount,0)ManCount," +
                                           " isnull(b.WoManCount,0)WoManCount," +
                                           " isnull(b.WoManCount,0)+isnull(a.ManCount,0) num" +
                                   " from " +
                                   " (select  isnull(ei.DeptID,'')DeptID ,Count(id) ManCount from  officedba.EmployeeInfo ei " +
                                       " where ei.sex='1'";
                if (DeptID != "")
                    sql += " and  ei.DeptID = '" + DeptID + "'";

                sql += "and ei.Flag = 1  and ei.CompanyCD = '" + CompanyCD + "' group by  ei.DeptID) a" +
                                   " full join " +
                                   " (select  isnull(ei.DeptID,'')DeptID ,Count(id) WoManCount from  officedba.EmployeeInfo ei " +
                                       " where ei.sex='2'";
                if (DeptID != "")
                    sql += " and  ei.DeptID = '" + DeptID + "'";

                sql += "and ei.Flag = 1 and ei.CompanyCD = '" + CompanyCD + "' group by  ei.DeptID) b" +
                                   " on a.DeptID=b.DeptID)c" +
                               " left join officedba.DeptInfo di on di.id = c.DeptID ";

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 在职员工性别统计打印
        /// <summary>
        /// 在职员工性别统计打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetEmployeeSexPrint(string CompanyCD, string DeptID, string ord)
        {
            try
            {
                string sql = " select A.DeptID, di.DeptName,A.ManCount,B.WoManCount,isnull(A.ManCount,0)+isnull(B.WoManCount,'') num FROM " +
                               " (select " +
                                       " isnull(ei.DeptID,'')DeptID" +
                               " ,Count(id) ManCount" +
                                   " from " +
                                       " officedba.EmployeeInfo ei" +
                                   " where ei.sex='1' and ei.Flag = 1 ";
                if (DeptID != "")
                    sql += " and  ei.DeptID = '" + DeptID + "'";

                sql += " and ei.CompanyCD = '" + CompanyCD + "'" +
                               " group by  ei.DeptID) A" +
                               " left outer join" +
                               " (select " +
                                       " isnull(ei.DeptID,'')DeptID" +
                               " ,Count(id) WoManCount" +
                                   " from " +
                                       " officedba.EmployeeInfo ei" +
                                   " where ei.sex='2' and ei.Flag = 1 " +
                               " group by  ei.DeptID) B" +
                               " ON A.Deptid=B.DeptID" +
                               " left join officedba.DeptInfo di on di.id = A.Deptid " +
                               " where di.DeptName is not null";
                sql += ord;
                               
                return SqlHelper.ExecuteSql(sql);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 人员信息打印
        /// <summary>
        /// 人员信息打印
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static DataTable PrintEmployee(string CompanyCD,string EmployeeNo)
        {
            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT A.ID                    ");
            searchSql.AppendLine("      ,A.EmployeeNum           ");
            searchSql.AppendLine("      ,A.EmployeeNo            ");
            searchSql.AppendLine("      ,A.PYShort               ");
            searchSql.AppendLine("      ,A.CompanyCD           ");
            searchSql.AppendLine("      ,A.CardID                ");
            searchSql.AppendLine("      ,A.SafeguardCard         ");
            searchSql.AppendLine("      ,A.EmployeeName          ");
            searchSql.AppendLine("      ,A.UsedName              ");
            searchSql.AppendLine("      ,A.NameEn                ");
            searchSql.AppendLine("      ,(case A.Sex when '1' then '男' when '2' then '女' end) Sex");
            searchSql.AppendLine("      ,convert(varchar(20),A.Birth,23) Birth");
            searchSql.AppendLine("      ,A.Account               ");
            searchSql.AppendLine("      ,(case A.AccountNature when '1' then '非农业' when '2' then '农村' end) AccountNature");
            searchSql.AppendLine("      ,cp.TypeName  CountryName");
            searchSql.AppendLine("      ,A.Height                ");
            searchSql.AppendLine("      ,A.Weight                ");
            searchSql.AppendLine("      ,A.Sight                 ");
            searchSql.AppendLine("      ,A.Degree                ");
            searchSql.AppendLine("      ,cpp.TypeName PositionNm ");
            searchSql.AppendLine("      ,A.DocuType              ");
            searchSql.AppendLine("      ,cpn.TypeName NationalNm ");
            searchSql.AppendLine("      ,cpm.typename marriageStaNm ");
            searchSql.AppendLine("      ,A.Origin                ");
            searchSql.AppendLine("      ,cpl.typename LandscapeNm ");
            searchSql.AppendLine("      ,cpr.typename ReligionNm ");
            searchSql.AppendLine("      ,A.Telephone             ");
            searchSql.AppendLine("      ,A.Mobile                ");
            searchSql.AppendLine("      ,A.EMail                 ");
            searchSql.AppendLine("      ,A.OtherContact          ");
            searchSql.AppendLine("      ,A.HomeAddress           ");
            searchSql.AppendLine("      ,(case A.HealthStatus when '0' then '一般' when '1' then '好' when '2' then '很好' end) HealthStatus ");
            searchSql.AppendLine("      ,cpc.TypeName CultureLevelNm ");
            searchSql.AppendLine("      ,A.GraduateSchool        ");
            searchSql.AppendLine("      ,cppf.TypeName ProfessionalNm ");
            searchSql.AppendLine("      ,A.Features              ");
            searchSql.AppendLine("      ,A.ComputerLevel         ");
            searchSql.AppendLine("      ,cpfl1.typename ForeignLanguage1Nm ");
            searchSql.AppendLine("      ,(case A.ForeignLanguageLevel1 when '1' then '一般' when '2' then '熟练' when '3' then '精通' end) ForeignLanguageLevel1 ");
            searchSql.AppendLine("      ,cpfl2.typename ForeignLanguage2Nm ");
            searchSql.AppendLine("      ,(case A.ForeignLanguageLevel2 when '1' then '一般' when '2' then '熟练' when '3' then '精通' end) ForeignLanguageLevel2 ");
            searchSql.AppendLine("      ,cpfl3.typename ForeignLanguage3Nm ");
            searchSql.AppendLine("      ,(case A.ForeignLanguageLevel3 when '1' then '一般' when '2' then '熟练' when '3' then '精通' end) ForeignLanguageLevel3 ");
            searchSql.AppendLine("      ,convert(varchar(20),A.WorkTime,23) WorkTime ");
            searchSql.AppendLine("      ,A.TotalSeniority        ");
            searchSql.AppendLine("      ,A.PhotoURL              ");
            searchSql.AppendLine("      ,A.PositionTitle         ");
            searchSql.AppendLine("      ,(case A.Flag when '1' then '在职人员' when '2' then '人才储备' when '3' then '离职人员' end) Flag ");
            searchSql.AppendLine("      ,A.ModifiedDate        ");
            searchSql.AppendLine("      ,A.ModifiedUserID      ");
            searchSql.AppendLine("      ,dq.QuarterName        ");
            searchSql.AppendLine("      ,cpa.typename AdminLevelNm ");
            searchSql.AppendLine("      ,A.DeptID             ");
            searchSql.AppendLine("      ,C.DeptName             ");
            searchSql.AppendLine("      ,CONVERT(varchar(100), A.EnterDate, 23) EnterDate             ");
            searchSql.AppendLine("      ,A.Resume,A.ProfessionalDes                ");
            searchSql.AppendLine("  FROM officedba.EmployeeInfo A ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.DeptInfo C on ");
            searchSql.AppendLine(" C.id = A.DeptID ");
            searchSql.AppendLine(" left join officedba.CodePublicType cp on cp.id = A.CountryID ");
            searchSql.AppendLine(" left join officedba.codepublictype cpp on cpp.id = A.PositionID ");
            searchSql.AppendLine(" left join officedba.DeptQuarter dq on dq.id = A.QuarterID ");
            searchSql.AppendLine(" left join officedba.codepublictype cpa on cpa.id = A.AdminLevelID ");
            searchSql.AppendLine(" left join officedba.codepublictype cpn on cpn.id = A.NationalID ");
            searchSql.AppendLine(" left join officedba.codepublictype cpm on cpm.id = A.MarriageStatus ");
            searchSql.AppendLine(" left join officedba.codepublictype cpl on cpl.id = A.Landscape ");
            searchSql.AppendLine(" left join officedba.codepublictype cpr on cpr.id = A.Religion ");
            searchSql.AppendLine(" left join officedba.codepublictype cpc on cpc.id = A.CultureLevel ");
            searchSql.AppendLine(" left join officedba.codepublictype cppf on cppf.id = A.Professional ");
            searchSql.AppendLine(" left join officedba.codepublictype cpfl1 on cpfl1.id = A.ForeignLanguage1 ");
            searchSql.AppendLine(" left join officedba.codepublictype cpfl2 on cpfl2.id = A.ForeignLanguage2 ");
            searchSql.AppendLine(" left join officedba.codepublictype cpfl3 on cpfl3.id = A.ForeignLanguage3 ");
            searchSql.AppendLine(" WHERE                       ");
            searchSql.AppendLine("	A.EmployeeNo = @EmployeeNo  and A.CompanyCD = @CompanyCD ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //人员ID
            param[0] = SqlHelper.GetParameter("@EmployeeNo", EmployeeNo);
            param[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //执行查询
            DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), param);
            //返回查询的值
            if (data == null || data.Rows.Count < 1)
            {
                //数据不存在时，返回空值
                return null;
            }
            else
            {
                ////数据存在时，返回转化后的EmployeeInfoModel
                //EmployeeInfoModel model = ChangeEmplDataToModel(data.Rows[0]);
                ////设置履历信息
                //model.HistoryInfo = GetHistoryInfo(model.CompanyCD, model.EmployeeNo);
                ////设置技能信息
                //model.SkillInfo = GetSkillInfo(model.CompanyCD, model.EmployeeNo);
                ////设置合同信息
                //model.ContractInfo = GetContractInfo(employeeID);
                //返回人员信息
                return data;
            }
        }
        #endregion

        #region 工作经历/教育经历打印
        /// <summary>
        /// 工作经历/教育经历打印
        /// </summary>
        /// <param name="companyCD"></param>
        /// <param name="employeeNo"></param>
        /// <returns></returns>
        public static DataTable PrintHistory(string companyCD, string employeeNo)
        {
            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT eh.ID                       ");
            searchSql.AppendLine("      ,eh.CompanyCD                ");
            searchSql.AppendLine("      ,eh.EmployeeNo               ");
            searchSql.AppendLine("      ,convert(varchar(20),eh.StartDate,23) StartDate ");
            searchSql.AppendLine("      ,convert(varchar(20),eh.EndDate,23)EndDate ");
            searchSql.AppendLine("      ,eh.Flag                     ");
            searchSql.AppendLine("      ,eh.Company                  ");
            searchSql.AppendLine("      ,eh.Department               ");
            searchSql.AppendLine("      ,eh.WorkContent              ");
            searchSql.AppendLine("      ,eh.LeaveReason              ");
            searchSql.AppendLine("      ,eh.SchoolName               ");
            searchSql.AppendLine("      ,eh.Professional,cp.typename ProfessionalNm ");
            searchSql.AppendLine("      ,eh.CultureLevel,cpc.typename  CultureLevelNm ");
            searchSql.AppendLine("      ,eh.ModifiedDate             ");
            searchSql.AppendLine("      ,eh.ModifiedUserID           ");
            searchSql.AppendLine("  FROM officedba.EmployeeHistory eh");
            searchSql.AppendLine("  left join officedba.codepublictype cp on cp.id = eh.Professional");
            searchSql.AppendLine("  left join officedba.codepublictype cpc on cpc.id = eh.CultureLevel");
            searchSql.AppendLine(" WHERE eh.CompanyCD = @CompanyCD   ");
            searchSql.AppendLine("   AND eh.EmployeeNo = @EmployeeNo ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameterFromString("@CompanyCD", companyCD);
            //员工编号
            param[1] = SqlHelper.GetParameterFromString("@EmployeeNo", employeeNo);
            //执行查询
            DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), param);

            return data;
        }
        #endregion

        #region 技能信息打印
        /// <summary>
        /// 技能信息打印
        /// </summary>
        /// <param name="companyCD"></param>
        /// <param name="employeeNo"></param>
        /// <returns></returns>
        public static DataTable PrintSkill(string companyCD, string employeeNo)
        {
            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT ID                     ");
            searchSql.AppendLine("      ,CompanyCD              ");
            searchSql.AppendLine("      ,EmployeeNo             ");
            searchSql.AppendLine("      ,SkillName              ");
            searchSql.AppendLine("      ,CertificateName        ");
            searchSql.AppendLine("      ,CertificateNo          ");
            searchSql.AppendLine("      ,CertificateLevel       ");
            searchSql.AppendLine("      ,IssueCompany           ");
            searchSql.AppendLine("      ,convert(varchar(20),IssueDate,23)IssueDate");
            searchSql.AppendLine("      ,Validity               ");
            searchSql.AppendLine("      ,convert(varchar(20),DeadDate,23)DeadDate ");
            searchSql.AppendLine("      ,Remark                 ");
            searchSql.AppendLine("      ,ModifiedDate           ");
            searchSql.AppendLine("      ,ModifiedUserID         ");
            searchSql.AppendLine("  FROM officedba.EmployeeSkill");
            searchSql.AppendLine(" WHERE CompanyCD = @CompanyCD ");
            searchSql.AppendLine("  AND EmployeeNo = @EmployeeNo");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameterFromString("@CompanyCD", companyCD);
            //员工编号
            param[1] = SqlHelper.GetParameterFromString("@EmployeeNo", employeeNo);
            //执行查询
            DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), param);

            return data;
        }
        #endregion

        #region 导出在职人员列表
        /// <summary>
        /// 导出在职人员列表
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportEmployeeWorkInfo(EmployeeSearchModel searchModel,string BeginDate,string EndDate, string ord)
        {
            #region 查询SQL拼写
            //查询SQL拼写
            //StringBuilder searchSql = new StringBuilder();
            //searchSql.AppendLine(" SELECT                                                            ");
            //searchSql.AppendLine(" 	 A.ID                                                            ");
            //searchSql.AppendLine(" 	,A.EmployeeNo                                                    ");
            //searchSql.AppendLine(" 	,ISNULL(A.EmployeeNum, '') AS EmployeeNum                        ");
            //searchSql.AppendLine(" 	,ISNULL(A.PYShort, '') AS PYShort                                ");
            //searchSql.AppendLine(" 	,A.EmployeeName                                                  ");
            //searchSql.AppendLine(" 	,CASE F.ContractKind                                             ");
            //searchSql.AppendLine(" 		WHEN '1' THEN '合同工'                                       ");
            //searchSql.AppendLine(" 		WHEN '2' THEN '临时工'                                       ");
            //searchSql.AppendLine(" 		WHEN '3' THEN '兼职'                                         ");
            //searchSql.AppendLine(" 		ELSE ''                                                      ");
            //searchSql.AppendLine(" 	END AS ContractKind                                              ");
            //searchSql.AppendLine(" 	,ISNULL(B.DeptName, '') AS DeptName                              ");
            //searchSql.AppendLine(" 	,ISNULL(C.QuarterName, '') AS QuarterName                        ");
            //searchSql.AppendLine(" 	,ISNULL(D.TypeName, '') AS AdminLevelName                        ");
            //searchSql.AppendLine(" 	,ISNULL(G.TypeName, '') AS PositionName                          ");
            //searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.EnterDate,21),'') AS EntryDate     ");
            ////searchSql.AppendLine(" 	,CASE                                                            ");
            ////searchSql.AppendLine(" 		WHEN A.EnterDate is null THEN ''                             ");
            ////searchSql.AppendLine(" 		ELSE                                                         ");
            ////searchSql.AppendLine(" 	CONVERT(VARCHAR(2),DATEDIFF(YEAR,A.EnterDate,getdate())+1) + ' 年' ");
            ////searchSql.AppendLine(" 	END AS TotalYear                                                 ");
            ////searchSql.AppendLine(" 	,CONVERT(VARCHAR(100),A.EnterDate,23) TotalYear ");
            //searchSql.AppendLine(" FROM                                                              ");
            //searchSql.AppendLine(" 	officedba.EmployeeInfo A                                         ");
            //searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo B                                   ");
            //searchSql.AppendLine(" 		ON A.DeptID = B.ID                                           ");
            //searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter C                                ");
            //searchSql.AppendLine(" 		ON A.QuarterID = C.ID                                        ");
            //searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType D                             ");
            //searchSql.AppendLine(" 		ON A.AdminLevelID = D.ID                                     ");
            //searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType G                             ");
            //searchSql.AppendLine(" 		ON A.PositionID = G.ID                                       ");
            //searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeContract F                           ");
            //searchSql.AppendLine(" 		ON A.ID = F.EmployeeID                                       ");
            //searchSql.AppendLine("  		AND F.ContractStatus = @ContractStatus                   ");
            //searchSql.AppendLine(" WHERE                                                             ");
            //searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                                         ");
            //searchSql.AppendLine(" 	AND A.Flag = @Flag                                               ");
            #endregion

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT A.ID                    ");
            searchSql.AppendLine("      ,A.EmployeeNum           ");
            searchSql.AppendLine("      ,A.EmployeeNo            ");
            searchSql.AppendLine("      ,A.PYShort               ");
            searchSql.AppendLine("      ,A.CompanyCD             ");
            searchSql.AppendLine("      ,A.CardID                ");
            searchSql.AppendLine("      ,A.SafeguardCard         ");
            searchSql.AppendLine("      ,A.EmployeeName          ");
            searchSql.AppendLine("      ,A.UsedName              ");
            searchSql.AppendLine("      ,A.NameEn                ");
            searchSql.AppendLine("      ,(case A.Sex when '1' then '男' else '女' end)Sex ");
            searchSql.AppendLine("      ,A.Birth                 ");
            searchSql.AppendLine("      ,A.Account               ");
            searchSql.AppendLine("      ,(case A.AccountNature when '1' then '非农业' when '2' then '农业' else '' end) AccountNature ");
            searchSql.AppendLine("      ,A.CountryID             ");
            searchSql.AppendLine("      ,A.Height                ");
            searchSql.AppendLine("      ,CONVERT(varchar(8),A.Weight) Weight                ");
            searchSql.AppendLine("      ,CONVERT(varchar(8),A.Sight) Sight                 ");
            searchSql.AppendLine("      ,A.Degree                ");
            searchSql.AppendLine("      ,A.DocuType              ");
            searchSql.AppendLine("      ,A.Origin                ");
            searchSql.AppendLine("      ,A.Telephone             ");
            searchSql.AppendLine("      ,A.Mobile                ");
            searchSql.AppendLine("      ,A.EMail                 ");
            searchSql.AppendLine("      ,A.OtherContact          ");
            searchSql.AppendLine("      ,A.HomeAddress           ");
            searchSql.AppendLine("      ,(case A.HealthStatus when '0' then '一般' when '1' then '良好' when '2' then '很好' else '' end) HealthStatus");
            searchSql.AppendLine("      ,A.GraduateSchool        ");
            searchSql.AppendLine("      ,A.Features              ");
            searchSql.AppendLine("      ,A.ComputerLevel         ");
            searchSql.AppendLine("      ,(case A.ForeignLanguageLevel1 when '1' then '一般' when '2' then '熟练' when '3' then '精通' end)ForeignLanguageLevel1 ");
             searchSql.AppendLine("      ,(case A.ForeignLanguageLevel2 when '1' then '一般' when '2' then '熟练' when '3' then '精通' end)ForeignLanguageLevel2 ");
            searchSql.AppendLine("      ,(case A.ForeignLanguageLevel3 when '1' then '一般' when '2' then '熟练' when '3' then '精通' end)ForeignLanguageLevel3 ");
            searchSql.AppendLine("      ,A.WorkTime ");
            searchSql.AppendLine("      ,A.TotalSeniority,A.PhotoURL,A.PositionTitle,'在职人员' Flag ");
            searchSql.AppendLine("      ,A.ModifiedDate ,A.ModifiedUserID,A.QuarterID ");
            searchSql.AppendLine("      ,A.AdminLevelID ,A.DeptID ,C.DeptName ");
            searchSql.AppendLine("      ,CONVERT(varchar(100), A.EnterDate, 23) EnterDate             ");
            searchSql.AppendLine("      ,A.Resume,A.ProfessionalDes,q.TypeName NationalName ");
            searchSql.AppendLine("      ,d.TypeName PositionName,e.QuarterName QuarterName,f.TypeName MarriageStatus ");
            searchSql.AppendLine("      ,g.TypeName CultureLevel,g.TypeName Professional,i.TypeName Landscape ");
            searchSql.AppendLine("      ,j.TypeName Religion,k.TypeName CountryName,l.TypeName ForeignLanguage11 ");
            searchSql.AppendLine("      ,m.TypeName ForeignLanguage12,o.TypeName ForeignLanguage13,p.TypeName AdminLevelName ");
            searchSql.AppendLine("  FROM officedba.EmployeeInfo A ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.DeptInfo C on C.id = A.DeptID ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType d on d.id = A.PositionID ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.DeptQuarter e on e.id = A.QuarterID ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType f on f.id = A.MarriageStatus ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType g on g.id = A.CultureLevel ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType h on h.id = A.Professional ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType i on i.id = A.Landscape ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType j on j.id = A.Religion ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType k on k.id = A.CountryID ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType l on l.id = A.ForeignLanguage1 ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType m on m.id = A.ForeignLanguage2 ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType o on o.id = A.ForeignLanguage3 ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType p on p.id = A.AdminLevelID ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType q on q.id = A.NationalID ");
            searchSql.AppendLine(" WHERE                                                             ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                                         ");
            searchSql.AppendLine(" 	AND A.Flag = @Flag and (A.delFlag <> @delFlag  or A.delFlag is null) ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加非离职参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", "1"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@delFlag", "1"));
            //添加合同状态参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractStatus", ConstUtil.CONTRACT_FLAG_ONE));
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", searchModel.CompanyCD));

            #region 页面条件
            //编号
            if (!string.IsNullOrEmpty(searchModel.EmployeeNo))
            {
                searchSql.AppendLine("	AND A.EmployeeNo LIKE '%' + @EmployeeNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", searchModel.EmployeeNo));
            }
            //工号
            if (!string.IsNullOrEmpty(searchModel.EmployeeNum))
            {
                searchSql.AppendLine("	AND A.EmployeeNum LIKE '%' + @EmployeeNum + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNum", searchModel.EmployeeNum));
            }
            //拼音缩写
            if (!string.IsNullOrEmpty(searchModel.PYShort))
            {
                searchSql.AppendLine("	AND A.PYShort LIKE '%' + @PYShort + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", searchModel.PYShort));
            }
            //姓名
            if (!string.IsNullOrEmpty(searchModel.EmployeeName))
            {
                searchSql.AppendLine("	AND A.EmployeeName LIKE '%' + @EmployeeName + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeName", searchModel.EmployeeName));
            }           
            //岗位
            if (!string.IsNullOrEmpty(searchModel.QuarterID))
            {
                searchSql.AppendLine("	AND A.QuarterID = @QuarterID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", searchModel.QuarterID));
            }
            //职称
            if (!string.IsNullOrEmpty(searchModel.PositionID))
            {
                searchSql.AppendLine("	AND A.PositionID = @PositionID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PositionID", searchModel.PositionID));
            }
            //入职时间
            if (BeginDate != "")
            {
                searchSql.AppendLine("	AND A.EnterDate >= @BeginDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BeginDate", BeginDate));
            }
            //入职时间
            if (EndDate != "")
            {
                searchSql.AppendLine("	AND A.EnterDate <= @EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }
            #endregion

            //设定comm的SQL文
            comm.CommandText = searchSql.ToString();           
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 导出人员储备列表
        /// <summary>
        /// 导出人员储备列表
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static DataTable ExportEmployeeReserve(EmployeeSearchModel searchModel)
        {

            #region 查询SQL拼写
            //查询SQL拼写
            //StringBuilder searchSql = new StringBuilder();
            //searchSql.AppendLine("SELECT A.ID                                                             ");
            //searchSql.AppendLine("      ,A.EmployeeNo                                                     ");
            //searchSql.AppendLine("	  ,ISNULL(A.PYShort, '') AS PYShort                                   ");
            //searchSql.AppendLine("      ,A.EmployeeName                                                   ");
            //searchSql.AppendLine("      ,Case A.Sex                                                       ");
            //searchSql.AppendLine("		When '1' then '男'                                                ");
            //searchSql.AppendLine("		When '2' then '女'                                                ");
            //searchSql.AppendLine("		else ''                                                           ");
            //searchSql.AppendLine("		End as SexName                                                    ");
            //searchSql.AppendLine("      ,ISNULL(CONVERT(VARCHAR(10),A.Birth, 21), '') AS Birth            ");
            //searchSql.AppendLine("      ,CASE WHEN A.Birth is null THEN '' ELSE                           ");
            //searchSql.AppendLine("CONVERT(VARCHAR(3),DATEDIFF(YEAR,A.Birth,getdate())) + ' 岁' END AS Age ");
            //searchSql.AppendLine("	  ,ISNULL(A.Origin, '') AS Origin                                     ");
            ////searchSql.AppendLine("	  ,isnull(datediff(year,WorkTime,GETDATE())+1,'') TotalSeniority ");
           
            //searchSql.AppendLine(" 	,CASE                                                                 ");
            //searchSql.AppendLine(" 		WHEN A.WorkTime is null THEN ''                                   ");
            //searchSql.AppendLine(" 		ELSE                                                              ");
            //searchSql.AppendLine(" 	isnull(datediff(year,WorkTime,GETDATE())+1,'')                        ");           
            //searchSql.AppendLine(" 	END AS TotalYear                                                      ");

            //searchSql.AppendLine("      ,ISNULL(c.QuarterName, '') AS PositionTitle                     ");
            //searchSql.AppendLine("	  ,ISNULL(B.TypeName, '') AS ProfessionalName                         ");
            //searchSql.AppendLine(" FROM officedba.EmployeeInfo A LEFT outer join officedba.CodePublicType B on");
            //searchSql.AppendLine("		A.CompanyCD = B.CompanyCD AND A.Professional = B.ID");
            //searchSql.AppendLine(" LEFT join officedba.DeptQuarter c on c.id = A.QuarterID");
            //searchSql.AppendLine("WHERE                                                                   ");
            //searchSql.AppendLine("	A.CompanyCD = @CompanyCD                                              ");
            //searchSql.AppendLine("	AND A.Flag = @Flag                                                    ");
            #endregion

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT A.ID ");
            searchSql.AppendLine("      ,ISNULL(A.EmployeeNum, '') AS EmployeeNum,ISNULL(CONVERT(VARCHAR(10),A.EnterDate,21),'') AS EnterDate");
            searchSql.AppendLine("      ,A.EmployeeNo            ");
            searchSql.AppendLine("      ,ISNULL(A.PYShort, '') AS PYShort               ");
            searchSql.AppendLine("      ,A.CompanyCD             ");
            searchSql.AppendLine("      ,A.CardID                ");
            searchSql.AppendLine("      ,A.SafeguardCard         ");
            searchSql.AppendLine("      ,isnull(A.EmployeeName,'') EmployeeName ");
            searchSql.AppendLine("      ,A.UsedName              ");
            searchSql.AppendLine("      ,A.NameEn                ");
            searchSql.AppendLine("      ,(case A.Sex when '1' then '男' else '女' end)SexName ");
            searchSql.AppendLine("      ,isnull(convert(varchar(10),Birth,23),'') Birth ");
            searchSql.AppendLine("      ,A.Account               ");
            searchSql.AppendLine("      ,(case A.AccountNature when '1' then '非农业' when '2' then '农业' else '' end) AccountNature ");
            searchSql.AppendLine("      ,A.CountryID             ");
            searchSql.AppendLine("      ,A.Height ");
            searchSql.AppendLine("      ,CASE WHEN A.Birth is null THEN '' ELSE                           ");
            searchSql.AppendLine(" CONVERT(VARCHAR(3),DATEDIFF(YEAR,A.Birth,getdate())) + ' 岁' END AS Age ");
            searchSql.AppendLine("      ,CONVERT(varchar(8),A.Weight) Weight                ");
            searchSql.AppendLine("      ,CONVERT(varchar(8),A.Sight) Sight                 ");
            searchSql.AppendLine("      ,A.Degree                ");
            searchSql.AppendLine("      ,A.DocuType              ");
            searchSql.AppendLine("      ,ISNULL(A.Origin, '') AS Origin ");
            searchSql.AppendLine("      ,A.Telephone             ");
            searchSql.AppendLine("      ,A.Mobile                ");
            searchSql.AppendLine("      ,A.EMail                 ");
            searchSql.AppendLine("      ,A.OtherContact          ");
            searchSql.AppendLine("      ,A.HomeAddress           ");
            searchSql.AppendLine("      ,(case A.HealthStatus when '0' then '一般' when '1' then '良好' when '2' then '很好' else '' end) HealthStatus");
            searchSql.AppendLine("      ,A.GraduateSchool        ");
            searchSql.AppendLine("      ,A.Features              ");
            searchSql.AppendLine("      ,A.ComputerLevel         ");
            searchSql.AppendLine("      ,(case A.ForeignLanguageLevel1 when '1' then '一般' when '2' then '熟练' when '3' then '精通' end)ForeignLanguageLevel1 ");
            searchSql.AppendLine("      ,(case A.ForeignLanguageLevel2 when '1' then '一般' when '2' then '熟练' when '3' then '精通' end)ForeignLanguageLevel2 ");
            searchSql.AppendLine("      ,(case A.ForeignLanguageLevel3 when '1' then '一般' when '2' then '熟练' when '3' then '精通' end)ForeignLanguageLevel3 ");
            searchSql.AppendLine("      ,A.WorkTime ");
            searchSql.AppendLine("      ,A.TotalSeniority,A.PhotoURL,ISNULL(A.PositionTitle, '') AS PositionTitle,'人才储备' Flag ");
            searchSql.AppendLine("      ,A.ModifiedDate ,A.ModifiedUserID,A.QuarterID ");
            searchSql.AppendLine("      ,A.AdminLevelID ,A.DeptID ,ISNULL(C.DeptName, '') AS DeptName ");
            
            searchSql.AppendLine("      ,A.Resume,A.ProfessionalDes,q.TypeName NationalName ");
            searchSql.AppendLine("      ,ISNULL(d.TypeName, '') AS PositionName,ISNULL(e.QuarterName, '') AS QuarterName,f.TypeName MarriageStatus ");
            searchSql.AppendLine("      ,g.TypeName CultureLevel,isnull(h.TypeName,'') ProfessionalName,i.TypeName Landscape ");
            searchSql.AppendLine("      ,j.TypeName Religion,k.TypeName CountryName,l.TypeName ForeignLanguage11 ");
            searchSql.AppendLine("      ,m.TypeName ForeignLanguage12,o.TypeName ForeignLanguage13,p.TypeName AdminLevelName ");
            searchSql.AppendLine("  FROM officedba.EmployeeInfo A ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.DeptInfo C on C.id = A.DeptID ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType d on d.id = A.PositionID ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.DeptQuarter e on e.id = A.QuarterID ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType f on f.id = A.MarriageStatus ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType g on g.id = A.CultureLevel ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType h on h.id = A.Professional ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType i on i.id = A.Landscape ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType j on j.id = A.Religion ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType k on k.id = A.CountryID ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType l on l.id = A.ForeignLanguage1 ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType m on m.id = A.ForeignLanguage2 ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType o on o.id = A.ForeignLanguage3 ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType p on p.id = A.AdminLevelID ");
            searchSql.AppendLine("  LEFT OUTER JOIN officedba.CodePublicType q on q.id = A.NationalID ");


            searchSql.AppendLine(" WHERE                                                             ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                                         ");          
            searchSql.AppendLine(" 	AND A.Flag = @Flag and (A.delFlag <> @delFlag  or A.delFlag is null) ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", searchModel.CompanyCD));
            //添加人才储备参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", ConstUtil.EMPLOYEE_FLAG_TALENT));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@delFlag", "1"));

            #region 页面输入条件
            //编号
            if (!string.IsNullOrEmpty(searchModel.EmployeeNo))
            {
                searchSql.AppendLine("	AND A.EmployeeNo LIKE '%' + @EmployeeNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", searchModel.EmployeeNo));
            }
            //拼音缩写
            if (!string.IsNullOrEmpty(searchModel.PYShort))
            {
                searchSql.AppendLine("	AND A.PYShort LIKE '%' + @PYShort + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", searchModel.PYShort));
            }
            //姓名
            if (!string.IsNullOrEmpty(searchModel.EmployeeName))
            {
                searchSql.AppendLine("	AND A.EmployeeName LIKE '%' + @EmployeeName + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeName", searchModel.EmployeeName));
            }
            //性别
            if (!string.IsNullOrEmpty(searchModel.SexID))
            {
                searchSql.AppendLine("	AND A.Sex = @Sex ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Sex", searchModel.SexID));
            }
            //文化程度 
            if (!string.IsNullOrEmpty(searchModel.CultureLevel))
            {
                searchSql.AppendLine("	AND A.CultureLevel = @CultureLevel ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CultureLevel", searchModel.CultureLevel));
            }
            //专业 
            if (!string.IsNullOrEmpty(searchModel.ProfessionalID))
            {
                searchSql.AppendLine("	AND A.Professional = @ProfessionalID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProfessionalID", searchModel.ProfessionalID));
            }
            //毕业院校
            if (!string.IsNullOrEmpty(searchModel.SchoolName))
            {
                searchSql.AppendLine("	AND A.GraduateSchool LIKE '%' + @GraduateSchool + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@GraduateSchool", searchModel.SchoolName));
            }
            //应聘岗位
            if (!string.IsNullOrEmpty(searchModel.PositionTitle))
            {
                searchSql.AppendLine("	AND c.QuarterName LIKE '%' + @PositionTitle + '%'  ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PositionTitle", searchModel.PositionTitle));
            }
            //工龄
            if (!string.IsNullOrEmpty(searchModel.TotalSeniority))
            {
                searchSql.AppendLine("	AND isnull(datediff(year,WorkTime,GETDATE())+1,'') = @TotalSeniority ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalSeniority", searchModel.TotalSeniority));
            }
            #endregion

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        /// <summary>
        /// 取Excel数据并读取到officedba.ProductInfo中
        /// zxb 2009-09-01
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static DataSet ReadEexcel(string FilePath, string companycd)
        {
            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;data source=" + FilePath;
            string sql = "SELECT distinct * FROM [Sheet1$]";
            DataSet ds = new DataSet();
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(sql, connStr);
            da.Fill(ds);
            //删除历史记录
            SqlParameter[] paramter = { new SqlParameter("@companycd", companycd) };
            sql = "delete from officedba.EmployeeInfo_temp where [企业编号]=@companycd";
            SqlHelper.ExecuteTransSql(sql, paramter);
            //传到临时表中
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                SqlParameter[] param = 
                {
                    new SqlParameter("@companycd",companycd),
                    new SqlParameter("@id",ds.Tables[0].Rows[i][0].ToString()),
                    new SqlParameter("@CardID",ds.Tables[0].Rows[i][1].ToString()),
                    new SqlParameter("@EmpLoyeeName",ds.Tables[0].Rows[i][2].ToString()),
                    new SqlParameter("@Sex",ds.Tables[0].Rows[i][3].ToString()),
                    new SqlParameter("@Birth",ds.Tables[0].Rows[i][4].ToString()),
                    new SqlParameter("@QuarterName",ds.Tables[0].Rows[i][5].ToString()),
                    new SqlParameter("@QuarterNo",ds.Tables[0].Rows[i][6].ToString()),
                    new SqlParameter("@DeptName",ds.Tables[0].Rows[i][7].ToString()),
                    new SqlParameter("@DeptNo",ds.Tables[0].Rows[i][8].ToString()),
                    new SqlParameter("@EnterDate",ds.Tables[0].Rows[i][9].ToString()),
                    new SqlParameter("@MarriageStatus",ds.Tables[0].Rows[i][10].ToString()),
                    new SqlParameter("@Telephone",ds.Tables[0].Rows[i][11].ToString()),
                    new SqlParameter("@Mobile",ds.Tables[0].Rows[i][12].ToString()),
                    new SqlParameter("@HomeAddress",ds.Tables[0].Rows[i][13].ToString()),
                    new SqlParameter("@WorkTime",ds.Tables[0].Rows[i][14].ToString()),
                    new SqlParameter("@empType",ds.Tables[0].Rows[i][15].ToString())
                    
                };
                string lenstr = string.Empty;
                for (int j = 0; j < 16; j++)
                {
                    if (ds.Tables[0].Rows[i][j].ToString().Trim().Length < 1)
                    {
                        lenstr += "#";
                    }
                }
                if (lenstr.Length == 16)
                {
                    continue;
                }
                sql = "insert into officedba.EmployeeInfo_temp values(@id,@companycd,@CardID,@EmpLoyeeName,@Sex,@Birth,@QuarterName,@QuarterNo,@DeptName,@DeptNo,@EnterDate,@MarriageStatus,@Telephone,@Mobile,@HomeAddress,@WorkTime,@empType)";
                SqlHelper.ExecuteTransSql(sql, param);
            }
            sql = "select * from officedba.EmployeeInfo_temp where [企业编号]=@companycd and [流水号]!=0 order by [流水号]";
            ds = new DataSet();
            SqlParameter[] paramter1 = { new SqlParameter("@companycd", companycd) };
            DataTable dt = SqlHelper.ExecuteSql(sql, paramter1);
            ds.Tables.Add(dt);
            return ds;
        }
        /// <summary>
        /// 判断给定企业编号，员工姓名，查找库中是否存在对应的人员
        /// lysong 2009-09-04
        /// </summary>
        /// <param name="EmployeeName"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable IsHaveEmployeeNameByCompanyCD(string EmployeeName, string CompanyCD)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@EmployeeName", SqlDbType.VarChar, 50),
                                          new SqlParameter("@CompanyCD",SqlDbType.VarChar,50)};
                parameters[0].Value = EmployeeName;
                parameters[1].Value = CompanyCD;
                string searchSql = "select * from officedba.EmployeeInfo where EmployeeName=@EmployeeName and CompanyCD=@CompanyCD";
                DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), parameters);
                return data;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 判断给定企业编号，员工编号，查找库中是否存在对应的人员
        /// lysong 2009-09-04
        /// </summary>
        /// <param name="EmployeeName"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable IsHaveEmployeeNOByCompanyCD(string EmployeeNo, string CompanyCD)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@EmployeeNo", SqlDbType.VarChar, 50),
                                          new SqlParameter("@CompanyCD",SqlDbType.VarChar,50)};
                parameters[0].Value = EmployeeNo;
                parameters[1].Value = CompanyCD;
                string searchSql = "select * from officedba.EmployeeInfo where EmployeeNo=@EmployeeNo and CompanyCD=@CompanyCD";
                DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), parameters);
                return data;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 判断给定企业编号，员工姓名和编号是否匹配
        /// lysong 2009-09-04
        /// </summary>
        /// <param name="EmployeeName"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable IsHaveEmployeesByEmployeeNOAndName(string EmployeeName, string EmployeeNo, string CompanyCD)
        {
            try
            {
                SqlParameter[] parameters = { 
                                                new SqlParameter("@EmployeeName", SqlDbType.VarChar,50),
                                                new SqlParameter("@EmployeeNo",SqlDbType.VarChar,50),
                                                new SqlParameter("@CompanyCD",SqlDbType.VarChar,50)
                                            };
                parameters[0].Value = EmployeeName;
                parameters[1].Value = EmployeeNo;
                parameters[2].Value = CompanyCD;
                string searchSql = "select * from officedba.EmployeeInfo where EmployeeName=@EmployeeName and EmployeeNo=@EmployeeNo and CompanyCD=@CompanyCD";
                DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), parameters);
                return data;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 判断给定企业编号、班段名称，查询是否有此班段信息
        /// lysong 2009-09-04
        /// </summary>
        /// <param name="EmployeeName"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable IsHaveWorkShiftTimeByCompanyCD(string WorkShiftTimeName, string CompanyCD)
        {
            try
            {
                SqlParameter[] parameters = {   new SqlParameter("@WorkShiftTimeName", SqlDbType.VarChar, 50),
                                            new SqlParameter("@CompanyCD",SqlDbType.VarChar,50)};
                parameters[0].Value = WorkShiftTimeName;
                parameters[1].Value = CompanyCD;
                string searchSql = "select * from officedba.WorkShiftTime where ShiftTimeName=@WorkShiftTimeName and CompanyCD=@CompanyCD";
                DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), parameters);
                return data;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 判断给定企业编号、班组名称、时间，查询是否有此班组设置信息
        /// lysong 2009-09-04
        /// </summary>
        /// <param name="WorkGroupName"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable IsHaveWorkGroupSetByGroupName(string WorkGroupNo, string AttendanceDate, string CompanyCD, string EmployeeID)
        {
            try
            {
                SqlParameter[] parameters = {   new SqlParameter("@WorkGroupNo", SqlDbType.VarChar, 50),
                                                new SqlParameter("@AttendanceDate", SqlDbType.VarChar, 50),
                                                new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                                new SqlParameter("@EmployeeID",SqlDbType.VarChar,50)
                                            };
                parameters[0].Value = WorkGroupNo;
                parameters[1].Value = AttendanceDate;
                parameters[2].Value = CompanyCD;
                parameters[3].Value = EmployeeID;
                string searchSql = "select * from officedba.EmployeeAttendanceSet where EmployeeID=@EmployeeID "
                                    + "and WorkGroupNo=@WorkGroupNo and "
                                    + "((StartDate<=@AttendanceDate and EndDate>=@AttendanceDate) or (StartDate<=@AttendanceDate and EndDate IS NULL))";
                DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), parameters);
                return data;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 判断给定企业编号、班组名称，查询是否有此班组信息
        /// lysong 2009-09-04
        /// </summary>
        /// <param name="WorkGroupName"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable IsHaveWorkGroup(string WorkGroupName, string CompanyCD)
        {
            try
            {
                SqlParameter[] parameters = {   new SqlParameter("@WorkGroupName", SqlDbType.VarChar, 50),
                                            new SqlParameter("@CompanyCD",SqlDbType.VarChar,50)};
                parameters[0].Value = WorkGroupName;
                parameters[1].Value = CompanyCD;
                string searchSql = "select * from officedba.workgroup where WorkGroupName=@WorkGroupName and CompanyCD=@CompanyCD";
                DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), parameters);
                return data;
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// 判断给定企业编号，岗位名称，查找库中是否存在对应的岗位
        /// zxb 2009-08-26
        /// </summary>
        /// <param name="codename"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static bool ChargeQuarterInfo(string codename,string quarterno,string deptName,string deptNo, string compid)
        {
            SqlParameter[] parameters = { new SqlParameter("@codename", SqlDbType.VarChar, 50),
                                          new SqlParameter("@quarterno", SqlDbType.VarChar, 200),
                                          new SqlParameter("@deptName", SqlDbType.VarChar, 200),
                                          new SqlParameter("@deptNo", SqlDbType.VarChar, 200),
                                          new SqlParameter("@companyid",SqlDbType.VarChar,50)};
            parameters[0].Value = codename;
            parameters[1].Value = quarterno;
            parameters[2].Value = deptName;
            parameters[3].Value = deptNo;
            parameters[4].Value = compid;
            string sqlstr = string.Empty;
            sqlstr = "select count(*) from officedba.DeptQuarter where 1=1 ";
            
            //添加部门条件
            if (deptName.Length > 0 && deptNo.Length>0)
            {
                sqlstr += " and DeptID=(select distinct ID from officedba.DeptInfo where CompanyCD=@companyid and DeptNo=@deptNo and DeptName=@deptName) ";
            }
            else if (deptName.Length > 0)
            {
                sqlstr += " and DeptID=(select distinct ID from officedba.DeptInfo where CompanyCD=@companyid and DeptName=@deptName) ";
            }
            else if (deptNo.Length > 0)
            {
                sqlstr += " and DeptID=(select distinct ID from officedba.DeptInfo where CompanyCD=@companyid and DeptNo=@deptNo) ";
            }

            if (codename.Length > 0)
            {
                sqlstr += "and QuarterName = @codename";
            }

            if (quarterno.Length > 0)
            {
                sqlstr += " and QuarterNo=@quarterno";
            }
            object obj = SqlHelper.ExecuteScalar(sqlstr, parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }

        /// <summary>
        /// 判断多个职位
        /// </summary>
        /// <param name="codename"></param>
        /// <param name="deptName"></param>
        /// <param name="deptNo"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static DataTable ChargeQuarterInfoNum(string codename, string quarterno, string deptName, string deptNo, string compid)
        {
            SqlParameter[] parameters = { new SqlParameter("@codename", SqlDbType.VarChar, 50),
                                          new SqlParameter("@quarterno", SqlDbType.VarChar, 200),
                                          new SqlParameter("@deptName", SqlDbType.VarChar, 200),
                                          new SqlParameter("@deptNo", SqlDbType.VarChar, 200),
                                          new SqlParameter("@companyid",SqlDbType.VarChar,50)};
            parameters[0].Value = codename;
            parameters[1].Value = quarterno;
            parameters[2].Value = deptName;
            parameters[3].Value = deptNo;
            parameters[4].Value = compid;

            //string sqlstr = string.Empty;
            StringBuilder sqlstr = new StringBuilder();
            if (deptName.Length > 0 && deptNo.Length > 0)
            {
                sqlstr.AppendLine("select isnull(B.QuarterName,'无上级') SuperQuarterName,isnull(C.DeptName,'暂未指定部门') DeptName,A.QuarterNo,A.QuarterName from officedba.DeptQuarter A left join officedba.DeptQuarter B");
                sqlstr.AppendLine("on A.SuperQuarterID=B.ID");
                sqlstr.AppendLine("left join ");
                sqlstr.AppendLine("(");
                sqlstr.AppendLine("    select * from officedba.DeptInfo");
                sqlstr.AppendLine(")C on A.DeptID =C.ID");
                if (quarterno.Length > 0 && codename.Length > 0)
                {
                    sqlstr.AppendLine("where A.QuarterName = @codename and A.QuarterNo=@quarterno and A.CompanyCD=@companyid and A.DeptID=(select distinct ID from officedba.DeptInfo where CompanyCD=@companyid and DeptNo=@deptNo and DeptName=@deptName)");
                }
                else if (quarterno.Length > 0)
                {
                    sqlstr.AppendLine("where A.QuarterNo = @quarterno and A.CompanyCD=@companyid and A.DeptID=(select distinct ID from officedba.DeptInfo where CompanyCD=@companyid and DeptNo=@deptNo and DeptName=@deptName)");
                }
                else
                {
                    sqlstr.AppendLine("where A.QuarterName = @codename and A.CompanyCD=@companyid and A.DeptID=(select distinct ID from officedba.DeptInfo where CompanyCD=@companyid and DeptNo=@deptNo and DeptName=@deptName)");
                }
            }
            else if (deptName.Length > 0)
            {
                sqlstr.AppendLine("select isnull(B.QuarterName,'无上级') SuperQuarterName,isnull(C.DeptName,'暂未指定部门') DeptName,A.QuarterNo,A.QuarterName from officedba.DeptQuarter A left join officedba.DeptQuarter B");
                sqlstr.AppendLine("on A.SuperQuarterID=B.ID");
                sqlstr.AppendLine("left join ");
                sqlstr.AppendLine("(");
                sqlstr.AppendLine("    select * from officedba.DeptInfo");
                sqlstr.AppendLine(")C on A.DeptID =C.ID");
                if (quarterno.Length > 0 && codename.Length > 0)
                {
                    sqlstr.AppendLine(" where A.QuarterName = @codename and A.QuarterNo=@quarterno and A.CompanyCD=@companyid and A.DeptID=(select distinct ID from officedba.DeptInfo where CompanyCD=@companyid and DeptName=@deptName)");
                }
                else if (quarterno.Length > 0)
                {
                    sqlstr.AppendLine(" where A.QuarterNo = @quarterno and A.CompanyCD=@companyid and A.DeptID=(select distinct ID from officedba.DeptInfo where CompanyCD=@companyid and DeptName=@deptName)");
                }
                else
                {
                    sqlstr.AppendLine(" where A.QuarterName = @codename and A.CompanyCD=@companyid and A.DeptID=(select distinct ID from officedba.DeptInfo where CompanyCD=@companyid and DeptName=@deptName)");
                }
                //sqlstr = "select count(*) from officedba.DeptQuarter where CompanyCD=@companyid and DeptID=(select distinct ID from officedba.DeptInfo where CompanyCD=@companyid and DeptName=@deptName)";
            }
            else if(deptNo.Length>0)
            {
                sqlstr.AppendLine("select isnull(B.QuarterName,'无上级') SuperQuarterName,isnull(C.DeptName,'暂未指定部门') DeptName,A.QuarterNo,A.QuarterName from officedba.DeptQuarter A left join officedba.DeptQuarter B");
                sqlstr.AppendLine("on A.SuperQuarterID=B.ID");
                sqlstr.AppendLine("left join ");
                sqlstr.AppendLine("(");
                sqlstr.AppendLine("    select * from officedba.DeptInfo");
                sqlstr.AppendLine(")C on A.DeptID =C.ID");
                if (quarterno.Length > 0 && codename.Length > 0)
                {
                    sqlstr.AppendLine(" where A.QuarterName = @codename and A.QuarterNo=@quarterno and A.CompanyCD=@companyid and A.DeptID=(select distinct ID from officedba.DeptInfo where CompanyCD=@companyid and DeptNo=@deptNo)");
                }
                else if (quarterno.Length > 0)
                {
                    sqlstr.AppendLine(" where A.QuarterNo = @quarterno and A.CompanyCD=@companyid and A.DeptID=(select distinct ID from officedba.DeptInfo where CompanyCD=@companyid and DeptNo=@deptNo)");
                }
                else
                {
                    sqlstr.AppendLine(" where A.QuarterName = @codename and A.CompanyCD=@companyid and A.DeptID=(select distinct ID from officedba.DeptInfo where CompanyCD=@companyid and DeptNo=@deptNo)");
                } 
            }
            else
            {
                sqlstr.AppendLine("select isnull(B.QuarterName,'无上级') SuperQuarterName,isnull(C.DeptName,'暂未指定部门') DeptName,A.QuarterNo,A.QuarterName from officedba.DeptQuarter A left join officedba.DeptQuarter B");
                sqlstr.AppendLine("on A.SuperQuarterID=B.ID");
                sqlstr.AppendLine("left join ");
                sqlstr.AppendLine("(");
                sqlstr.AppendLine("    select * from officedba.DeptInfo");
                sqlstr.AppendLine(")C on A.DeptID =C.ID");
                if (quarterno.Length > 0 && codename.Length > 0)
                {
                    sqlstr.AppendLine(" where A.QuarterName = @codename and A.QuarterNo=@quarterno and A.CompanyCD=@companyid");
                }
                else if (quarterno.Length > 0)
                {
                    sqlstr.AppendLine(" where A.QuarterName = @codename and A.QuarterNo=@quarterno and A.CompanyCD=@companyid");
                }
                else
                {
                    sqlstr.AppendLine(" where A.QuarterName = @codename and A.QuarterName=@codename and A.CompanyCD=@companyid");
                }
                //sqlstr = "select count(*) from officedba.DeptQuarter where QuarterName=@codename and CompanyCD=@companyid";
            }

            return SqlHelper.ExecuteSql(sqlstr.ToString(), parameters);

        }

        /// <summary>
        /// 判断有多少个部门
        /// </summary>
        /// <param name="codename"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static int ChargeDeptInfoNum(string codename,string deptno, string compid)
        {
            SqlParameter[] parameters = { new SqlParameter("@codename", SqlDbType.VarChar, 50),
                                          new SqlParameter("@deptNo", SqlDbType.VarChar, 50),
                                          new SqlParameter("@companyid",SqlDbType.VarChar,50)};
            parameters[0].Value = codename;
            parameters[1].Value = deptno;
            parameters[2].Value = compid;
            string sqlStr = string.Empty;
            if (deptno.Length > 0)
            {
                sqlStr = "select count(*) from officedba.DeptInfo where DeptName=@codename and DeptNO=@deptNo and CompanyCD=@companyid";
            }
            else
            {
                sqlStr = "select count(*) from officedba.DeptInfo where DeptName=@codename and CompanyCD=@companyid";
            }
            object obj = SqlHelper.ExecuteScalar(sqlStr, parameters);
            return Convert.ToInt32(obj);
        }

        public static DataTable GetDeptInfo(string codename, string compid)
        {
            SqlParameter[] parameters = { new SqlParameter("@codename", SqlDbType.VarChar, 50),
                                          new SqlParameter("@companyid",SqlDbType.VarChar,50)};
            parameters[0].Value = codename;
            parameters[1].Value = compid;
            return SqlHelper.ExecuteSql("select isnull(B.DeptName,'无上级') superDeptName,A.ID,A.DeptNO,A.DeptName from officedba.DeptInfo A left join officedba.DeptInfo B on A.SuperDeptID = B.ID  where A.DeptName=@codename and A.CompanyCD=@companyid", parameters);
        }

        /// <summary>
        /// 判断给定企业编号，部门名称，查找库中是否存在对应的部门
        /// zxb 2009-08-26
        /// </summary>
        /// <param name="codename"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static bool ChargeDeptInfo(string codename,string deptno, string compid)
        {
            SqlParameter[] parameters = { new SqlParameter("@codename", SqlDbType.VarChar, 50),
                                          new SqlParameter("@deptno", SqlDbType.VarChar, 50),
                                          new SqlParameter("@companyid",SqlDbType.VarChar,50)};
            parameters[0].Value = codename;
            parameters[1].Value = deptno;
            parameters[2].Value = compid;
            string sqlstr = string.Empty;
            if (deptno.Length > 0)
            {
                sqlstr = "select count(*) from officedba.DeptInfo where DeptName=@codename and deptNo=@deptno and CompanyCD=@companyid";
            }
            else
            {
                sqlstr = "select count(*) from officedba.DeptInfo where DeptName=@codename and CompanyCD=@companyid";
            }
            object obj = SqlHelper.ExecuteScalar(sqlstr, parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }

        /// <summary>
        ///  判断给定企业编号，婚姻状态，查找库中是否存在对应的婚姻状态
        ///  zxb 2009-08-26
        /// </summary>
        /// <param name="codename"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static bool ChargeHyInfo(string codename, string compid)
        {
            SqlParameter[] parameters = { new SqlParameter("@codename", SqlDbType.VarChar, 50),
                                          new SqlParameter("@companyid",SqlDbType.VarChar,50)};
            parameters[0].Value = codename;
            parameters[1].Value = compid;
            object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.CodePublicType where TypeFlag=2 and TypeCode=6 and TypeName=@codename and CompanyCD=@companyid", parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }

        /// <summary>
        /// 判断给定企业编号，员工编号，查找库中是否存在对应员工编号
        /// zxb 2009-08-26
        /// </summary>
        /// <param name="codename"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static bool ChargeEmployeeInfo(string codename, string compid)
        {
            SqlParameter[] parameters = { new SqlParameter("@codename", SqlDbType.VarChar, 50),
                                          new SqlParameter("@companyid",SqlDbType.VarChar,50)};
            parameters[0].Value = codename;
            parameters[1].Value = compid;
            object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.EmployeeInfo where EmployeeNo=@codename and CompanyCD=@companyid", parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }

        /// <summary>
        /// 从excel导入员工信息表
        /// zxb 2009-08-26
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="fname"></param>
        /// <param name="tbname"></param>
        /// <returns></returns>
        public static int GetExcelToEmployeeInfo(string companycd)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@compcode",companycd)
            };
            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[excelIntoSql_HR]", param);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds.Tables[0].Rows.Count; //暂保留返回记录数，备日志使用
        }

        /// <summary>
        /// 返回从excel向员工信息库中设置的特殊员工编号的数据集
        /// zxb 2009-08-26 ,特殊格式为"##@@$$@@##"
        /// </summary>
        /// <param name="companycd"></param>
        /// <returns></returns>
        public static DataSet GetNullEmployeeList(string companycd)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@compcode",companycd)
            };
            DataTable dt = SqlHelper.ExecuteSql("select * from officedba.EmployeeInfo where CompanyCD=@compcode and EmployeeNo='##@@$$@@##'", param);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// 获取企业员工编号设置规则
        /// zxb 2009-08-27
        /// </summary>
        /// <param name="companycd"></param>
        /// <returns></returns>
        public static int GetCodeRuleID(string companycd)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@compcode",companycd)
            };
            DataTable dt = SqlHelper.ExecuteSql("select top 1 * from officedba.ItemCodingRule where CompanyCD=@compcode and CodingType=0 and ItemTypeID=5 and UsedStatus=1", param);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            try
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
            }
            catch
            {
                return 0;//如果返回0值，那么表示该企业没有设置员工编号规则
            }
        }

        /// <summary>
        /// 更新员工信息表的员工编号
        /// zxb 2009-08-27
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="employeeNo"></param>
        /// <param name="ID"></param>
        public static void UpdateEmployeeInfo(string companycd, string employeeNo, string ID)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@compcode",companycd),
                new SqlParameter("@employeeNo",employeeNo),
                new SqlParameter("@ID",ID)
            };

            string sqlstr = "update officedba.EmployeeInfo set EmployeeNo=@employeeNo where ID=@ID and CompanyCD=@compcode";
            SqlHelper.ExecuteSql(sqlstr, param);

        }

       
    }
}

