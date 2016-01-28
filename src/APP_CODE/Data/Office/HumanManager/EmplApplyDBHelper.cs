/**********************************************
 * 类作用：   新建调职申请
 * 建立人：   吴志强
 * 建立时间： 2009/04/22
 ***********************************************/
using System;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using XBase.Common;
using XBase.Data.Common;

namespace XBase.Data.Office.HumanManager
{
    /// <summary>
    /// 类名：EmplApplyDBHelper
    /// 描述：新建调职申请
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/22
    /// 最后修改时间：2009/04/22
    /// </summary>
    ///
    public class EmplApplyDBHelper
    {
        #region 通过ID查询调职申请信息
        /// <summary>
        /// 查询调职申请信息
        /// </summary>
        /// <param name="emplApplyID">调职申请ID</param>
        /// <returns></returns>
        public static DataTable GetEmplApplyInfoWithID(string emplApplyID)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                             ");
            searchSql.AppendLine(" 	A.EmplApplyNo                                     ");
            searchSql.AppendLine(" 	,A.Title                                          ");
            searchSql.AppendLine(" 	,A.EmployeeID                                     ");
            searchSql.AppendLine(" 	,B.EmployeeName AS EmployeeName                   ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.EnterDate,21) AS EnterDate ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.ApplyDate,21) AS ApplyDate ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.HopeDate,21) AS HopeDate   ");
            searchSql.AppendLine(" 	,A.NowDeptID                                      ");
            searchSql.AppendLine(" 	,C.DeptName AS NowDeptName                        ");
            searchSql.AppendLine(" 	,A.NowQuarterID                                   ");
            searchSql.AppendLine(" 	,A.NowAdminLevelID                                ");
            searchSql.AppendLine(" 	,A.NowWage                                        ");
            searchSql.AppendLine(" 	,A.NewDeptID                                      ");
            searchSql.AppendLine(" 	,D.DeptName AS NewDeptName                        ");
            searchSql.AppendLine(" 	,A.NewQuarterID                                   ");
            searchSql.AppendLine(" 	,A.NewAdminLevelID                                ");
            searchSql.AppendLine(" 	,A.NewWage                                        ");
            searchSql.AppendLine(" 	,A.ApplyType                                      ");
            searchSql.AppendLine(" 	,A.Reason                                         ");
            searchSql.AppendLine(" 	,A.Remark                                         ");
            searchSql.AppendLine(" 	,A.Status                                         ");
            searchSql.AppendLine(",CASE isnull(h.FlowStatus,0) WHEN 0 THEN '' ");
            searchSql.AppendLine("WHEN 1 THEN '待审批' ");
            searchSql.AppendLine(" WHEN 2 THEN '审批中' ");
            searchSql.AppendLine("WHEN 3 THEN '审批通过'");
            searchSql.AppendLine("WHEN 4 THEN '审批不通过' ");
            searchSql.AppendLine(" WHEN 5 THEN '撤销审批' ");
            searchSql.AppendLine("END FlowStatus ");
            searchSql.AppendLine(" FROM                                               ");
            searchSql.AppendLine(" 	officedba.EmplApply A                             ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B                ");
            searchSql.AppendLine(" 		ON A.EmployeeID = B.ID                        ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo C                    ");
            searchSql.AppendLine(" 		ON A.NowDeptID = C.ID                         ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo D                    ");
            searchSql.AppendLine(" 		ON A.NewDeptID = D.ID                         ");
            searchSql.AppendLine(" LEFT OUTER JOIN  ");
            searchSql.AppendLine(" (select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,officedba.EmplApply n  ");
            searchSql.AppendLine(" where m.BillTypeFlag='" + ConstUtil.CODING_TYPE_HUMAN + "' AND ");
            searchSql.AppendLine("m.BillTypeCode='" + ConstUtil.CODING_HUMAN_ITEM_EMPLAPPLY + "' and  m.BillID=n.ID and Billid=" + emplApplyID + " group by m.BillID,m.BillNo,m.CompanyCD) g ");
            searchSql.AppendLine("on A.EmplApplyNo=g.BillNo and A.CompanyCD=g.CompanyCD ");
            searchSql.AppendLine( "LEFT OUTER JOIN officedba.FlowInstance h ");
            searchSql.AppendLine("ON g.ID=h.ID and g.CompanyCD=h.CompanyCD ");
            searchSql.AppendLine(" WHERE                                              ");
            searchSql.AppendLine(" 	A.ID = @EmplApplyID                               ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //调职申请ID
            param[0] = SqlHelper.GetParameter("@EmplApplyID", emplApplyID);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 通过ID查询调职申请信息_打印
        /// <summary>
        /// 查询调职申请信息
        /// </summary>
        /// <param name="emplApplyID">调职申请ID</param>
        /// <returns></returns>
        public static DataTable GetEmplApplyInfoByID(string emplApplyID)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT * ");
            searchSql.AppendLine(" FROM officedba.View_EmplApplyInfo where ");
            searchSql.AppendLine(" ID = @EmplApplyID ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //调职申请ID
            param[0] = SqlHelper.GetParameter("@EmplApplyID", emplApplyID);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }

        #endregion
        #region 通过申请编号获取对应员工信息
        /// <summary>
        /// 通过申请编号获取对应员工信息
        /// </summary>
        /// <param name="applyNo">申请No</param>
        /// <returns></returns>
        public static DataTable GetEmployeeInfoWithNo(string companyCD, string applyNo)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                               ");
            searchSql.AppendLine("  	 E.EmployeeID                                   ");
            searchSql.AppendLine("  	,ISNULL(A.EmployeeNo, '') AS EmployeeNo         ");
            searchSql.AppendLine("  	,ISNULL(A.EmployeeNum, '') AS EmployeeNum       ");
            searchSql.AppendLine("  	,ISNULL(A.EmployeeName, '') AS EmployeeName     ");
            searchSql.AppendLine("  	,ISNULL(E.NowQuarterID, '') AS NowQuarterID     ");
            searchSql.AppendLine("  	,ISNULL(C.QuarterName, '') AS NowQuarterName    ");
            searchSql.AppendLine("  	,ISNULL(E.NowDeptID, '') AS NowDeptID           ");
            searchSql.AppendLine("  	,ISNULL(B.DeptName, '') AS NowDeptName          ");
            searchSql.AppendLine("    ,ISNULL(E.NowAdminLevelID, '') AS NowAdminLevelID ");
            searchSql.AppendLine("  	,ISNULL(D.TypeName, '') AS NowAdminLevelName    ");
            searchSql.AppendLine("  	,ISNULL(CONVERT(VARCHAR(10),A.EnterDate,21),'') ");
            searchSql.AppendLine("  		AS EnterDate                                ");
            searchSql.AppendLine("  	,ISNULL(E.NewDeptID, '') AS NewDeptID           ");
            searchSql.AppendLine("  	,ISNULL(F.DeptName, '') AS NewDeptName          ");
            searchSql.AppendLine("  	,ISNULL(E.NewQuarterID, '') AS NewQuarterID     ");
            searchSql.AppendLine("    ,ISNULL(E.NewAdminLevelID, '') AS NewAdminLevelID ");
            searchSql.AppendLine("  FROM                                                ");
            searchSql.AppendLine(" 	    officedba.EmplApply E                           ");
            searchSql.AppendLine("  	LEFT JOIN officedba.EmployeeInfo A              ");
            searchSql.AppendLine(" 		ON E.EmployeeID = A.ID                          ");
            searchSql.AppendLine("  	LEFT JOIN officedba.DeptInfo B                  ");
            searchSql.AppendLine("  		ON E.NowDeptID = B.ID                       ");
            searchSql.AppendLine("  	LEFT JOIN officedba.DeptQuarter C               ");
            searchSql.AppendLine("  		ON E.NowQuarterID = C.ID                    ");
            searchSql.AppendLine("  	LEFT JOIN officedba.CodePublicType D            ");
            searchSql.AppendLine("  		ON E.NowAdminLevelID = D.ID                 ");
            searchSql.AppendLine("  	LEFT JOIN officedba.DeptInfo F                  ");
            searchSql.AppendLine("  		ON E.NewDeptID = F.ID                       ");
            searchSql.AppendLine("  WHERE                                               ");
            searchSql.AppendLine("  	E.CompanyCD = @CompanyCD                        ");
            searchSql.AppendLine(" 	AND E.EmplApplyNo = @EmplApplyNo                    ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //申请No
            param[1] = SqlHelper.GetParameter("@EmplApplyNo", applyNo);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 通过检索条件查询调职申请信息
        /// <summary>
        /// 查询调职申请信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchEmplApplyInfo(EmplApplyModel model,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                                        ");
            searchSql.AppendLine(" 	 A.ID                                                        ");
            searchSql.AppendLine(" 	,A.EmplApplyNo                                               ");
            searchSql.AppendLine(" 	,A.ModifiedDate                                              ");
            searchSql.AppendLine(" 	,ISNULL(X.TypeName, '') AS NowAdminLevelName                 ");
            searchSql.AppendLine(" 	,ISNULL(Y.TypeName, '') AS NewAdminLevelName                 ");
            searchSql.AppendLine(" 	,ISNULL(B.EmployeeName, '') AS EmployeeName                  ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.ApplyDate,21),'') AS ApplyDate ");
            searchSql.AppendLine(" 	,ISNULL(C.DeptName, '') AS NowDeptName                       ");
            searchSql.AppendLine(" 	,ISNULL(D.QuarterName, '') AS NowQuarterName                 ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.HopeDate,21),'') AS HopeDate   ");
            searchSql.AppendLine(" 	,ISNULL(E.DeptName, '') AS NewDeptName                       ");
            searchSql.AppendLine(" 	,ISNULL(F.QuarterName, '') AS NewQuarterName                 ");
            searchSql.AppendLine(" 	,CASE h.FlowStatus                                           ");
            searchSql.AppendLine(" 		WHEN '0' THEN ''                                   ");
            searchSql.AppendLine(" 		WHEN '1' THEN '待审批'                                   ");
            searchSql.AppendLine(" 		WHEN '2' THEN '审批中'                                   ");
            searchSql.AppendLine(" 		WHEN '3' THEN '审批通过'                                 ");
            searchSql.AppendLine(" 		WHEN '4' THEN '审批不通过'                               ");
            searchSql.AppendLine(" 		WHEN '5' THEN '撤销审批'                                   ");
            searchSql.AppendLine(" 		ELSE ''                                   ");
            searchSql.AppendLine(" 	END AS FlowStatusName                                        ");
            searchSql.AppendLine(" FROM                                                          ");
            searchSql.AppendLine(" 	officedba.EmplApply A                                        ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType X                         ");
            searchSql.AppendLine(" 		ON A.NowAdminLevelID = X.ID                              ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType Y                         ");
            searchSql.AppendLine(" 		ON A.NewAdminLevelID = Y.ID                              ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B                           ");
            searchSql.AppendLine(" 		ON A.EmployeeID = B.ID                                   ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo C                               ");
            searchSql.AppendLine(" 		ON A.NowDeptID = C.ID                                    ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter D                            ");
            searchSql.AppendLine(" 		ON A.NowQuarterID = D.ID                                 ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo E                               ");
            searchSql.AppendLine(" 		ON A.NewDeptID = E.ID                                    ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter F                            ");
            searchSql.AppendLine(" 		ON A.NewQuarterID = F.ID                                 ");
            searchSql.AppendLine(" 	LEFT JOIN (                                ");
            searchSql.AppendLine(" 			    SELECT                                           ");
            searchSql.AppendLine(" 			        MAX(E.id) ID,E.BillID,E.BillNo                            ");
            searchSql.AppendLine(" 			    FROM                                             ");
            searchSql.AppendLine(" 			        officedba.FlowInstance E,officedba.EmplApply n                     ");
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
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", ConstUtil.BILL_TYPECODE_HUMAN_EMPL_APPLY));

            #region 页面查询条件
            //申请编号
            if (!string.IsNullOrEmpty(model.EmplApplyNo))
            {
                searchSql.AppendLine("	AND A.EmplApplyNo LIKE '%' + @EmplApplyNo + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmplApplyNo", model.EmplApplyNo));
            }
            //申请人
            if (!string.IsNullOrEmpty(model.EmployeeID))
            {
                searchSql.AppendLine("	AND A.EmployeeID = @EmployeeID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));
            }
            //申请日期
            if (!string.IsNullOrEmpty(model.ApplyDate))
            {
                searchSql.AppendLine("	AND A.ApplyDate >= @ApplyDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyDate", model.ApplyDate));
            }
            if (!string.IsNullOrEmpty(model.ApplyToDate))
            {
                searchSql.AppendLine("	AND A.ApplyDate <= @ApplyToDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyToDate", model.ApplyToDate));
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
            #endregion

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);

            //return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 新建调职申请信息
        /// <summary>
        /// 新建调职申请信息 
        /// </summary>
        /// <param name="model">调职申请信息</param>
        /// <returns></returns>
        public static bool InsertEmplApplyInfo(EmplApplyModel model)
        {
            #region 登陆SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO         ");
            insertSql.AppendLine(" officedba.EmplApply ");
            insertSql.AppendLine(" 	(CompanyCD         ");
            insertSql.AppendLine(" 	,EmplApplyNo       ");
            insertSql.AppendLine(" 	,Title             ");
            insertSql.AppendLine(" 	,EmployeeID        ");
            insertSql.AppendLine(" 	,EnterDate         ");
            insertSql.AppendLine(" 	,ApplyDate         ");
            insertSql.AppendLine(" 	,HopeDate          ");
            insertSql.AppendLine(" 	,NowDeptID         ");
            insertSql.AppendLine(" 	,NowQuarterID      ");
            insertSql.AppendLine(" 	,NowAdminLevelID   ");
            insertSql.AppendLine(" 	,NowWage           ");
            insertSql.AppendLine(" 	,NewDeptID         ");
            insertSql.AppendLine(" 	,NewQuarterID      ");
            insertSql.AppendLine(" 	,NewAdminLevelID   ");
            insertSql.AppendLine(" 	,NewWage           ");
            insertSql.AppendLine(" 	,ApplyType         ");
            insertSql.AppendLine(" 	,Reason            ");
            insertSql.AppendLine(" 	,Remark            ");
            insertSql.AppendLine(" 	,Status            ");
            insertSql.AppendLine(" 	,ModifiedDate      ");
            insertSql.AppendLine(" 	,ModifiedUserID)   ");
            insertSql.AppendLine(" VALUES              ");
            insertSql.AppendLine(" 	(@CompanyCD        ");
            insertSql.AppendLine(" 	,@EmplApplyNo      ");
            insertSql.AppendLine(" 	,@Title            ");
            insertSql.AppendLine(" 	,@EmployeeID       ");
            insertSql.AppendLine(" 	,@EnterDate        ");
            insertSql.AppendLine(" 	,@ApplyDate        ");
            insertSql.AppendLine(" 	,@HopeDate         ");
            insertSql.AppendLine(" 	,@NowDeptID        ");
            insertSql.AppendLine(" 	,@NowQuarterID     ");
            insertSql.AppendLine(" 	,@NowAdminLevelID  ");
            insertSql.AppendLine(" 	,@NowWage          ");
            insertSql.AppendLine(" 	,@NewDeptID        ");
            insertSql.AppendLine(" 	,@NewQuarterID     ");
            insertSql.AppendLine(" 	,@NewAdminLevelID  ");
            insertSql.AppendLine(" 	,@NewWage          ");
            insertSql.AppendLine(" 	,@ApplyType        ");
            insertSql.AppendLine(" 	,@Reason           ");
            insertSql.AppendLine(" 	,@Remark           ");
            insertSql.AppendLine(" 	,'0'               ");
            insertSql.AppendLine(" 	,getdate()         ");
            insertSql.AppendLine(" 	,@ModifiedUserID)  ");
            insertSql.AppendLine("   SET @EmplApplyID= @@IDENTITY  ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@EmplApplyID", SqlDbType.Int));

            //执行登陆操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
            //设置ID
            model.ID = comm.Parameters["@EmplApplyID"].Value.ToString();

            //执行插入并返回插入结果
            return isSucc;
        }
        #endregion

        #region 更新调职申请信息
        /// <summary>
        /// 更新调职申请信息
        /// </summary>
        /// <param name="model">调职申请信息</param>
        /// <returns></returns>
        public static bool UpdateEmplApplyInfo(EmplApplyModel model)
        {

            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.EmplApply         ");
            updateSql.AppendLine(" SET                                ");
            updateSql.AppendLine(" 	 EmployeeID = @EmployeeID         ");
            updateSql.AppendLine(" 	,Title = @Title                   ");
            updateSql.AppendLine(" 	,EnterDate = @EnterDate           ");
            updateSql.AppendLine(" 	,ApplyDate = @ApplyDate           ");
            updateSql.AppendLine(" 	,HopeDate = @HopeDate             ");
            updateSql.AppendLine(" 	,NowDeptID = @NowDeptID           ");
            updateSql.AppendLine(" 	,NowQuarterID = @NowQuarterID     ");
            updateSql.AppendLine(",NowAdminLevelID = @NowAdminLevelID ");
            updateSql.AppendLine(" 	,NowWage = @NowWage               ");
            updateSql.AppendLine(" 	,NewDeptID = @NewDeptID           ");
            updateSql.AppendLine(" 	,NewQuarterID = @NewQuarterID     ");
            updateSql.AppendLine(",NewAdminLevelID = @NewAdminLevelID ");
            updateSql.AppendLine(" 	,NewWage = @NewWage               ");
            updateSql.AppendLine(" 	,ApplyType = @ApplyType           ");
            updateSql.AppendLine(" 	,Reason = @Reason                 ");
            updateSql.AppendLine(" 	,Remark = @Remark                 ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            updateSql.AppendLine(" 	AND EmplApplyNo = @EmplApplyNo    ");
            #endregion

            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            SetSaveParameter(comm, model);//其他参数

            //执行更新并设置更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        #region 保存时参数设置
        /// <summary>
        /// 保存时参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">人才代理信息</param>
        private static void SetSaveParameter(SqlCommand comm, EmplApplyModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//企业代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmplApplyNo", model.EmplApplyNo));//调职申请编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));//申请人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EnterDate", model.EnterDate));//入职时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyDate", model.ApplyDate));//申请日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@HopeDate", model.HopeDate));//希望日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NowDeptID", model.NowDeptID));//部门(对应部门表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NowQuarterID", model.NowQuarterID));//岗位(对应岗位表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NowAdminLevelID", model.NowAdminLevelID));//岗位职等ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NowWage", model.NowWage));//调职前工资
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NewDeptID", model.NewDeptID));//调至部门ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NewQuarterID", model.NewQuarterID));//调至岗位ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NewAdminLevelID", model.NewAdminLevelID));//调至岗位职等ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NewWage", model.NewWage));//调职后工资
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyType", model.ApplyType));//申报类别
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Reason", model.Reason));//事由
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//更新用户ID
        }
        #endregion

        #region 能否删除调职申请信息
        /// <summary>
        /// 能否删除离职申请信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IfDeleteMoveApplyInfo(string MoveApplyNos, string CompanyID, string BillTypeFlag, string BillTypeCode)
        {
            string[] NOS = null;
            NOS = MoveApplyNos.Split(',');
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
        #region 能否删除调职申请信息
        /// <summary>
        /// 能否删除离职申请信息
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
        #region 删除调职申请信息
        /// <summary>
        /// 删除调职申请信息
        /// </summary>
        /// <param name="applyNo">调职申请编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteEmplApplyInfo(string applyNo, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.EmplApply ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" EmplApplyNo In( " + applyNo + ")");
            deleteSql.AppendLine(" AND CompanyCD = @CompanyCD ");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //设置SQL语句
            comm.CommandText = deleteSql.ToString();

            //执行删除并返回
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion


        #region 确认调职申请信息
        /// <summary>
        /// 确认调职申请信息
        /// </summary>
        /// <param name="model">调职申请信息</param>
        /// <returns></returns>
        public static bool ConfirmEmplApplyInfo(string ID,string UserName)
        {

            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.EmplApply         ");
            updateSql.AppendLine(" SET                                ");
            updateSql.AppendLine(" 	 Status = @Status         ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	ID = @ID            ");
            #endregion

            //定义更新基本信息的命令  
            string Status = "1";
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            SetConfirmParameter(comm,Status, UserName, ID);//其他参数

            //执行更新并设置更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        #region 保存时参数设置
        /// <summary>
        /// 保存时参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">人才代理信息</param>
        private static void SetConfirmParameter(SqlCommand comm, string Status, string UserName, string ID)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", Status));//单据状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", UserName));//更新用户ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));//备注
        }
        #endregion

        #region 取消调职确认信息
        /// <summary>
        /// 取消调职确认信息
        /// </summary>
        /// <param name="CarApplyM">车辆申请信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool UpdateMoveApplyCancelConfirm(string BillStatus,string ID, string userID, string CompanyID)
        {
            try
            {
                #region 车辆申请信息SQL拼写
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.EmplApply");
                sql.AppendLine("		SET Status=@Status        ");
                sql.AppendLine("		,ModifiedDate=getdate()      ");
                sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
                sql.AppendLine("WHERE                  ");
                sql.AppendLine("		ID=@ID   ");

                #endregion
                #region 车辆申请信息参数设置
                SqlParameter[] param;
                param = new SqlParameter[3];
                param[0] = SqlHelper.GetParameter("@Status", BillStatus);
                param[1] = SqlHelper.GetParameter("@ModifiedUserID", userID);
                param[2] = SqlHelper.GetParameter("@ID", ID);
                #endregion
                //SqlHelper.ExecuteTransSql(sql.ToString(), param);
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    FlowDBHelper.OperateCancelConfirm(CompanyID, 2, 12, Convert.ToInt32(ID), userID, tran);//取消确认
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
        #endregion

        #region 能否取消确认调职信息
        /// <summary>
        /// 能否取消确认调职信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IfCancelEmplApplyInf(string EmpNo,string CompanyCD)
        {

            string sql = "select * from officedba.EmplApplyNotify WHERE EmplApplyNo='" + EmpNo + "' AND CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
        }
        #endregion
    }
}
