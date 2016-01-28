/**********************************************
 * 类作用：   新建离职申请
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
    /// 类名：MoveApplyDBHelper
    /// 描述：新建离职申请
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/22
    /// 最后修改时间：2009/04/22
    /// </summary>
    ///
    public class MoveApplyDBHelper
    {
        #region 通过ID查询离职申请信息_打印
        /// <summary>
        /// 查询离职申请信息
        /// </summary>
        /// <param name="moveApplyID">离职申请ID</param>
        /// <returns></returns>
        public static DataTable GetMoveApplyInfoByID(string moveApplyID)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT  *  from  officedba.View_MoveApplyNO        ");
            searchSql.AppendLine(" WHERE                                              ");
            searchSql.AppendLine(" 	ID = @MoveApplyID                               ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //离职申请ID
            param[0] = SqlHelper.GetParameter("@MoveApplyID", moveApplyID);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

       
        #region 通过ID查询离职申请信息
        /// <summary>
        /// 查询离职申请信息
        /// </summary>
        /// <param name="moveApplyID">离职申请ID</param>
        /// <returns></returns>
        public static DataTable GetMoveApplyInfoWithID(string moveApplyID)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                             ");
            searchSql.AppendLine(" 	 A.MoveApplyNo                                    ");
            searchSql.AppendLine(" 	,A.Title                                          ");
            searchSql.AppendLine(" 	,A.EmployeeID                                     ");
            searchSql.AppendLine(" 	,B.EmployeeName AS EmployeeName                   ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.EnterDate,21) AS EnterDate ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.ApplyDate,21) AS ApplyDate ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.HopeDate,21) AS HopeDate   ");
            searchSql.AppendLine(" 	,A.DeptID                                         ");
            searchSql.AppendLine(" 	,C.DeptName AS DeptName                           ");
            searchSql.AppendLine(" 	,A.QuarterID                                      ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.ContractValid,21) AS ContractValid ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.MoveDate,21) AS MoveDate   ");
            searchSql.AppendLine(" 	,A.MoveType                                       ");
            searchSql.AppendLine(" 	,A.Interview                                      ");
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
            searchSql.AppendLine(" 	officedba.MoveApply A                             ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B                ");
            searchSql.AppendLine(" 		ON A.EmployeeID = B.ID                        ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo C                    ");
            searchSql.AppendLine(" 		ON A.DeptID = C.ID                            ");
            searchSql.AppendLine(" LEFT OUTER JOIN  ");
            searchSql.AppendLine(" (select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,officedba.MoveApply n  ");
            searchSql.AppendLine(" where m.BillTypeFlag='" + ConstUtil.CODING_TYPE_HUMAN + "' AND ");
            searchSql.AppendLine("m.BillTypeCode='" + ConstUtil.CODING_HUMAN_ITEM_MOVEAPPLY + "' and  m.BillID=n.ID and Billid=" + moveApplyID + " group by m.BillID,m.BillNo,m.CompanyCD) g ");
            searchSql.AppendLine("on A.MoveApplyNo=g.BillNo and A.CompanyCD=g.CompanyCD ");
            searchSql.AppendLine("LEFT OUTER JOIN officedba.FlowInstance h ");
            searchSql.AppendLine("ON g.ID=h.ID and g.CompanyCD=h.CompanyCD ");
            searchSql.AppendLine(" WHERE                                              ");
            searchSql.AppendLine(" 	A.ID = @MoveApplyID                               ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //离职申请ID
            param[0] = SqlHelper.GetParameter("@MoveApplyID", moveApplyID);
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
            searchSql.AppendLine("  	,ISNULL(A.QuarterID, '') AS QuarterID           ");
            searchSql.AppendLine("  	,ISNULL(C.QuarterName, '') AS QuarterName       ");
            searchSql.AppendLine("  	,ISNULL(A.DeptID, '') AS DeptID                 ");
            searchSql.AppendLine("  	,ISNULL(B.DeptName, '') AS DeptName             ");
            searchSql.AppendLine("  	,ISNULL(A.AdminLevelID, '') AS AdminLevelID     ");
            searchSql.AppendLine("  	,ISNULL(D.TypeName, '') AS AdminLevelName       ");
            searchSql.AppendLine("  	,ISNULL(CONVERT(VARCHAR(10),A.EnterDate,21),'') ");
            searchSql.AppendLine("  		AS EnterDate                                ");
            searchSql.AppendLine("  FROM                                                ");
            searchSql.AppendLine(" 	officedba.MoveApply E                               ");
            searchSql.AppendLine("  	LEFT JOIN officedba.EmployeeInfo A              ");
            searchSql.AppendLine(" 		ON E.EmployeeID = A.ID                          ");
            searchSql.AppendLine("  	LEFT JOIN officedba.DeptInfo B                  ");
            searchSql.AppendLine("  		ON A.DeptID = B.ID                          ");
            searchSql.AppendLine("  	LEFT JOIN officedba.DeptQuarter C               ");
            searchSql.AppendLine("  		ON A.QuarterID = C.ID                       ");
            searchSql.AppendLine("  	LEFT JOIN officedba.CodePublicType D            ");
            searchSql.AppendLine("  		ON A.AdminLevelID = D.ID                    ");
            searchSql.AppendLine("  WHERE                                               ");
            searchSql.AppendLine("  	E.CompanyCD = @CompanyCD                        ");
            searchSql.AppendLine(" 	AND E.MoveApplyNo = @MoveApplyNo                    ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //申请No
            param[1] = SqlHelper.GetParameter("@MoveApplyNo", applyNo);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 通过检索条件查询离职申请信息
        /// <summary>
        /// 查询离职申请信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchMoveApplyInfo(MoveApplyModel model,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                                        ");
            searchSql.AppendLine(" 	 A.ID                                                        ");
            searchSql.AppendLine(" 	,A.MoveApplyNo                                               ");
            searchSql.AppendLine(" 	,A.ModifiedDate                                              ");
            searchSql.AppendLine(" 	,ISNULL(B.EmployeeName, '') AS EmployeeName                  ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.ApplyDate,21),'') AS ApplyDate ");
            searchSql.AppendLine(" 	,ISNULL(C.DeptName, '') AS DeptName                          ");
            searchSql.AppendLine(" 	,ISNULL(D.QuarterName, '') AS QuarterName                    ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.HopeDate,21),'') AS HopeDate   ");
            searchSql.AppendLine(" 	,CASE h.FlowStatus                                           ");
            searchSql.AppendLine(" 		WHEN '0' THEN ''                                   ");
            searchSql.AppendLine(" 		WHEN '1' THEN '待审批'                                   ");
            searchSql.AppendLine(" 		WHEN '2' THEN '审批中'                                   ");
            searchSql.AppendLine(" 		WHEN '3' THEN '审批通过'                                 ");
            searchSql.AppendLine(" 		WHEN '4' THEN '审批不通过'                               ");
            searchSql.AppendLine(" 		WHEN '5' THEN '撤销审批'                               ");
            searchSql.AppendLine(" 		ELSE ''                               ");
            searchSql.AppendLine(" 	END AS FlowStatusName                                        ");
            searchSql.AppendLine(" FROM                                                          ");
            searchSql.AppendLine(" 	officedba.MoveApply A                                        ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B                           ");
            searchSql.AppendLine(" 		ON A.EmployeeID = B.ID                                   ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo C                               ");
            searchSql.AppendLine(" 		ON A.DeptID = C.ID                                       ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter D                            ");
            searchSql.AppendLine(" 		ON A.QuarterID = D.ID                                    ");
            searchSql.AppendLine(" 	LEFT JOIN (                                ");
            searchSql.AppendLine(" 			    SELECT                                           ");
            searchSql.AppendLine(" 			        MAX(E.id) ID,E.BillID,E.BillNo                            ");
            searchSql.AppendLine(" 			    FROM                                             ");
            searchSql.AppendLine(" 			        officedba.FlowInstance E,officedba.MoveApply n                     ");
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
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", ConstUtil.BILL_TYPECODE_HUMAN_MOVE_APPLY));

            #region 页面查询条件
            //申请编号
            if (!string.IsNullOrEmpty(model.MoveApplyNo))
            {
                searchSql.AppendLine("	AND A.MoveApplyNo LIKE '%' + @MoveApplyNo + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@MoveApplyNo", model.MoveApplyNo));
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

        #region 新建离职申请信息
        /// <summary>
        /// 新建离职申请信息 
        /// </summary>
        /// <param name="model">离职申请信息</param>
        /// <returns></returns>
        public static bool InsertMoveApplyInfo(MoveApplyModel model)
        {
            #region 登陆SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO         ");
            insertSql.AppendLine(" officedba.MoveApply ");
            insertSql.AppendLine(" 	(CompanyCD         ");
            insertSql.AppendLine(" 	,MoveApplyNo       ");
            insertSql.AppendLine(" 	,Title             ");
            insertSql.AppendLine(" 	,EmployeeID        ");
            insertSql.AppendLine(" 	,EnterDate         ");
            insertSql.AppendLine(" 	,ApplyDate         ");
            insertSql.AppendLine(" 	,HopeDate          ");
            insertSql.AppendLine(" 	,DeptID            ");
            insertSql.AppendLine(" 	,QuarterID         ");
            insertSql.AppendLine(" 	,ContractValid     ");
            insertSql.AppendLine(" 	,MoveDate          ");
            insertSql.AppendLine(" 	,MoveType          ");
            insertSql.AppendLine(" 	,Interview         ");
            insertSql.AppendLine(" 	,Reason            ");
            insertSql.AppendLine(" 	,Remark            ");
            insertSql.AppendLine(" 	,Status            ");
            insertSql.AppendLine(" 	,ModifiedDate      ");
            insertSql.AppendLine(" 	,ModifiedUserID)   ");
            insertSql.AppendLine(" VALUES              ");
            insertSql.AppendLine(" 	(@CompanyCD        ");
            insertSql.AppendLine(" 	,@MoveApplyNo      ");
            insertSql.AppendLine(" 	,@Title            ");
            insertSql.AppendLine(" 	,@EmployeeID       ");
            insertSql.AppendLine(" 	,@EnterDate        ");
            insertSql.AppendLine(" 	,@ApplyDate        ");
            insertSql.AppendLine(" 	,@HopeDate         ");
            insertSql.AppendLine(" 	,@DeptID           ");
            insertSql.AppendLine(" 	,@QuarterID        ");
            insertSql.AppendLine(" 	,@ContractValid    ");
            insertSql.AppendLine(" 	,@MoveDate         ");
            insertSql.AppendLine(" 	,@MoveType         ");
            insertSql.AppendLine(" 	,@Interview        ");
            insertSql.AppendLine(" 	,@Reason           ");
            insertSql.AppendLine(" 	,@Remark           ");
            insertSql.AppendLine(" 	,'0'               ");
            insertSql.AppendLine(" 	,getdate()         ");
            insertSql.AppendLine(" 	,@ModifiedUserID)  ");
            insertSql.AppendLine("   SET @MoveApplyID= @@IDENTITY  ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@MoveApplyID", SqlDbType.Int));

            //执行登陆操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
            //设置ID
            model.ID = comm.Parameters["@MoveApplyID"].Value.ToString();

            //执行插入并返回插入结果
            return isSucc;
        }
        #endregion

        #region 更新离职申请信息
        /// <summary>
        /// 更新离职申请信息
        /// </summary>
        /// <param name="model">离职申请信息</param>
        /// <returns></returns>
        public static bool UpdateMoveApplyInfo(MoveApplyModel model)
        {

            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.MoveApply         ");
            updateSql.AppendLine(" SET                                ");
            updateSql.AppendLine(" 	EmployeeID = @EmployeeID          ");
            updateSql.AppendLine(" 	,Title = @Title                   ");
            updateSql.AppendLine(" 	,EnterDate = @EnterDate           ");
            updateSql.AppendLine(" 	,ApplyDate = @ApplyDate           ");
            updateSql.AppendLine(" 	,HopeDate = @HopeDate             ");
            updateSql.AppendLine(" 	,DeptID = @DeptID                 ");
            updateSql.AppendLine(" 	,QuarterID = @QuarterID           ");
            updateSql.AppendLine(" 	,ContractValid = @ContractValid   ");
            updateSql.AppendLine(" 	,MoveDate = @MoveDate             ");
            updateSql.AppendLine(" 	,MoveType = @MoveType             ");
            updateSql.AppendLine(" 	,Interview = @Interview           ");
            updateSql.AppendLine(" 	,Reason = @Reason                 ");
            updateSql.AppendLine(" 	,Remark = @Remark                 ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            updateSql.AppendLine(" 	AND MoveApplyNo = @MoveApplyNo    ");
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
        private static void SetSaveParameter(SqlCommand comm, MoveApplyModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//企业代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MoveApplyNo", model.MoveApplyNo));//离职申请编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));//申请人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EnterDate", model.EnterDate));//入职时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyDate", model.ApplyDate));//申请日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@HopeDate", model.HopeDate));//希望离职日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));//部门(对应部门表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.QuarterID));//岗位(对应部门岗位表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractValid", model.ContractValid));//合同有效期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MoveDate", model.MoveDate));//通知离职日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MoveType", model.MoveType));//离职类型(1主动辞职,2辞退,3合同期满离职)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Interview", model.Interview));//访谈记录
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Reason", model.Reason));//事由
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//更新用户ID
        }
        #endregion
        #region 能否删除离职申请信息
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
        #region 能否删除离职申请信息
        /// <summary>
        /// 能否删除离职申请信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IsExistInfo(string BillNo,string CompanyID,string BillTypeFlag, string BillTypeCode)
        {

            string sql = "SELECT * FROM officedba.FlowInstance WHERE BillNo=" + BillNo + " AND CompanyCD='" + CompanyID + "'  AND BillTypeFlag='" + BillTypeFlag + "' AND BillTypeCode='" + BillTypeCode + "'";
            return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
        }
        #endregion
        #region 删除离职申请信息
        /// <summary>
        /// 删除离职申请信息
        /// </summary>
        /// <param name="applyNo">离职申请编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteMoveApplyInfo(string applyNo, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.MoveApply ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" MoveApplyNo In( " + applyNo + ")");
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

        #region 确认离职申请信息
        /// <summary>
        /// 确认离职申请信息
        /// </summary>
        /// <param name="model">离职申请信息</param>
        /// <returns></returns>
        public static bool ConfirmMoveApplyInfo(string ID, string UserName)
        {
            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.MoveApply         ");
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
            SetConfirmParameter(comm, Status, UserName, ID);//其他参数

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
        #region 取消离职确认信息
        /// <summary>
        /// 取消离职确认信息
        /// </summary>
        /// <param name="CarApplyM"></param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool UpdateMoveApplyCancelConfirm(string BillStatus, string ID, string userID, string CompanyID)
        {
            try
            {
                #region 车辆申请信息SQL拼写
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.MoveApply");
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
                    FlowDBHelper.OperateCancelConfirm(CompanyID, 2, 13, Convert.ToInt32(ID), userID, tran);//取消确认
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

        #region 能否取消确认离职信息
        /// <summary>
        /// 能否取消确认离职信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IfCancelMoveApplyInf(string EmpNo, string CompanyCD)
        {

            string sql = "select * from officedba.MoveNotify WHERE MoveApplyNo='" + EmpNo + "' AND CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
        }
        #endregion
    }
}
