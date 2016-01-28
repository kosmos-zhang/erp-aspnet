/**********************************************
 * 类作用：   新建离职
 * 建立人：   吴志强
 * 建立时间： 2009/04/24
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
    /// 类名：MoveNotifyDBHelper
    /// 描述：新建离职
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/24
    /// 最后修改时间：2009/04/24
    /// </summary>
    ///
    public class MoveNotifyDBHelper
    {

        #region 通过ID查询离职信息_打印
        /// <summary>
        /// 查询离职信息
        /// </summary>
        /// <param name="moveNotifyID">离职ID</param>
        /// <returns></returns>
        public static DataTable GetMoveNotifyInfoByID(string moveNotifyID)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT  *  from officedba.View_MoveApplyNotifyNO ");
            searchSql.AppendLine(" WHERE                                   ");
            searchSql.AppendLine(" ID = @MoveNotifyID                   ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //离职ID
            param[0] = SqlHelper.GetParameter("@MoveNotifyID", moveNotifyID);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 通过ID查询离职信息
        /// <summary>
        /// 查询离职信息
        /// </summary>
        /// <param name="moveNotifyID">离职ID</param>
        /// <returns></returns>
        public static DataTable GetMoveNotifyInfoWithID(string moveNotifyID)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                  ");
            searchSql.AppendLine(" 	 A.ID                                  ");
            searchSql.AppendLine(" 	,A.CompanyCD                           ");
            searchSql.AppendLine(" 	,A.NotifyNo                            ");
            searchSql.AppendLine(" 	,A.Title                               ");
            searchSql.AppendLine(" 	,A.MoveApplyNo                         ");
            searchSql.AppendLine(" 	,A.EmployeeID                          ");
            searchSql.AppendLine(" 	,B.EmployeeNo                          ");
            searchSql.AppendLine(" 	,B.EmployeeName AS EmployeeName        ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),B.EnterDate,21)   ");
            searchSql.AppendLine(" 		AS EnterDate                       ");
            searchSql.AppendLine(" 	,C.DeptName                            ");
            searchSql.AppendLine(" 	,D.QuarterName                         ");
            searchSql.AppendLine(" 	,E.TypeName                            ");
            searchSql.AppendLine(" 	,A.Reason                              ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.OutDate,21)     ");
            searchSql.AppendLine(" 		AS OutDate                         ");
            searchSql.AppendLine(" 	,A.JobNote                             ");
            searchSql.AppendLine(" 	,A.Remark                              ");
            searchSql.AppendLine(" 	,A.BillStatus                          ");
            searchSql.AppendLine(" 	,A.Creator                             ");
            searchSql.AppendLine(" 	,F.EmployeeName AS CreatorName         ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.CreateDate,21)  ");
            searchSql.AppendLine(" 		AS CreateDate                      ");
            searchSql.AppendLine(" 	,A.Confirmor                           ");
            searchSql.AppendLine(" 	,H.EmployeeName AS  ConfirmorName      ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.ConfirmDate,21) ");
            searchSql.AppendLine(" 		AS ConfirmDate                     ");
            searchSql.AppendLine(" FROM                                    ");
            searchSql.AppendLine(" 	officedba.MoveNotify A                 ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B     ");
            searchSql.AppendLine(" 		ON A.EmployeeID = B.ID             ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo C         ");
            searchSql.AppendLine(" 		ON B.DeptID = C.ID                 ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter D      ");
            searchSql.AppendLine(" 		ON B.QuarterID = D.ID              ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType E   ");
            searchSql.AppendLine(" 		ON B.AdminLevelID = E.ID           ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo F     ");
            searchSql.AppendLine(" 		ON A.Creator = F.ID                ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo H     ");
            searchSql.AppendLine(" 		ON A.Confirmor = H.ID              ");
            searchSql.AppendLine(" WHERE                                   ");
            searchSql.AppendLine(" 	A.ID = @MoveNotifyID                   ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //离职ID
            param[0] = SqlHelper.GetParameter("@MoveNotifyID", moveNotifyID);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 获取申请单信息
        /// <summary>
        /// 获取申请单信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetApplyInfo(string companyCD, bool isSearch)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                 ");
            searchSql.AppendLine(" 	 A.ID                                 ");
            searchSql.AppendLine(" 	,A.MoveApplyNo                        ");
            searchSql.AppendLine(" 	,A.Title AS Title                     ");
            searchSql.AppendLine(" FROM                                   ");
            searchSql.AppendLine(" 	officedba.MoveApply A                 ");
            searchSql.AppendLine(" 	,officedba.FlowInstance B             ");
            searchSql.AppendLine(" WHERE                                  ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD              ");
            searchSql.AppendLine(" 	AND A.ID = B.BillID                   ");
            searchSql.AppendLine(" 	AND B.BillTypeFlag = @BillTypeFlag    ");
            searchSql.AppendLine(" 	AND B.BillTypeCode = @BillTypeCD      ");
            searchSql.AppendLine(" 	AND B.FlowStatus = @FlowStatus        ");
            searchSql.AppendLine(" 	AND B.ModifiedDate = (                ");
            searchSql.AppendLine(" 	SELECT                                ");
            searchSql.AppendLine(" 		MAX(ModifiedDate)                 ");
            searchSql.AppendLine(" 	FROM                                  ");
            searchSql.AppendLine(" 		officedba.FlowInstance C          ");
            searchSql.AppendLine(" 	WHERE                                 ");
            searchSql.AppendLine(" 		C.BillID = A.ID                   ");
            searchSql.AppendLine(" 		AND C.BillTypeFlag = @BillTypeFlag");
            searchSql.AppendLine(" 		AND C.BillTypeCode = @BillTypeCD) ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //单据类别标识
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", ConstUtil.BILL_TYPEFLAG_HUMAN));
            //单据类别编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCD", ConstUtil.BILL_TYPECODE_HUMAN_MOVE_APPLY));
            //审批状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", "3"));
            //非列表查询时，添加条件
            if (!isSearch)
            {
                searchSql.AppendLine(" 	AND A.Status = @Status                ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", "0"));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        #region 获取申请单信息
        /// <summary>
        /// 获取申请单信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetApplyInfo(string companyCD)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                 ");
            searchSql.AppendLine(" 	 A.ID                                 ");
            searchSql.AppendLine(" 	,A.MoveApplyNo                        ");
            searchSql.AppendLine(" 	,A.Title AS Title                     ");
            searchSql.AppendLine(" FROM                                   ");
            searchSql.AppendLine(" 	officedba.MoveApply A                 ");
            searchSql.AppendLine(" WHERE                                  ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD  AND A.Status='1'            ");
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
        #region 通过检索条件查询离职信息
        /// <summary>
        /// 查询离职信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchMoveNotifyInfo(MoveNotifyModel model)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                         ");
            searchSql.AppendLine(" 	 A.ID                                         ");
            searchSql.AppendLine(" 	,A.NotifyNo                                   ");
            searchSql.AppendLine(" 	,ISNULL(A.BillStatus, '') AS BillStatus       ");
            searchSql.AppendLine(" 	,A.Title                                      ");
            searchSql.AppendLine(" 	,ISNULL(A.MoveApplyNo,'') AS MoveApplyNo      ");
            searchSql.AppendLine(" 	,ISNULL(B.EmployeeNo, '') AS EmployeeNo       ");
            searchSql.AppendLine(" 	,ISNULL(B.EmployeeName, '') AS EmployeeName   ");
            searchSql.AppendLine(" 	,ISNULL(C.DeptName, '') AS DeptName           ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.OutDate,21),'') ");
            searchSql.AppendLine(" 		AS OutDate                                ");
            searchSql.AppendLine(" 	,A.ModifiedDate                               ");
            searchSql.AppendLine(" FROM                                           ");
            searchSql.AppendLine(" 	officedba.MoveNotify A                        ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B            ");
            searchSql.AppendLine(" 		ON A.EmployeeID = B.ID                    ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo C                ");
            searchSql.AppendLine(" 		ON B.DeptID = C.ID                        ");
            searchSql.AppendLine(" WHERE                                          ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                      ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            #region 页面查询条件
            //编号
            if (!string.IsNullOrEmpty(model.NotifyNo))
            {
                searchSql.AppendLine("	AND A.NotifyNo LIKE '%' + @NotifyNo + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@NotifyNo", model.NotifyNo));
            }
            //主题
            if (!string.IsNullOrEmpty(model.Title))
            {
                searchSql.AppendLine("	AND A.Title LIKE '%' + @Title + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            //对应申请单
            if (!string.IsNullOrEmpty(model.MoveApplyNo))
            {
                searchSql.AppendLine("	AND A.MoveApplyNo = @MoveApplyNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@MoveApplyNo", model.MoveApplyNo));
            }
            //员工
            if (!string.IsNullOrEmpty(model.EmployeeID))
            {
                searchSql.AppendLine("	AND A.EmployeeID = @EmployeeID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));
            }
            //离职日期
            if (!string.IsNullOrEmpty(model.OutDate))
            {
                searchSql.AppendLine("	AND A.OutDate >= @OutDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutDate", model.OutDate));
            }
            if (!string.IsNullOrEmpty(model.OutToDate))
            {
                searchSql.AppendLine("	AND A.OutDate <= @OutToDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutToDate", model.OutToDate));
            }
            #endregion

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 确认离职信息
        /// <summary>
        /// 确认离职信息
        /// </summary>
        /// <param name="model">离职信息</param>
        /// <returns></returns>
        public static bool ConfirmMoveNotifyInfo(MoveNotifyModel model)
        {
            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.MoveNotify        ");
            updateSql.AppendLine(" SET                                ");
            updateSql.AppendLine(" 	 BillStatus = @BillStatus         ");
            updateSql.AppendLine(" 	,Confirmor = @Confirmor           ");
            updateSql.AppendLine(" 	,ConfirmDate = @ConfirmDate       ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            updateSql.AppendLine("  AND NotifyNo = @NotifyNo          ");
            #endregion

            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //单据状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
            //编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NotifyNo", model.NotifyNo));
            //确认人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", model.Confirmor));
            //确认日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate", model.ConfirmDate));
            //最后更新人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));

            //定义变量
            ArrayList lstUpdate = new ArrayList();
            //更新离职表
            lstUpdate.Add(comm);
            //结单时，更新人员信息表
            //更新人员信息
            SqlCommand updateEmpl = new SqlCommand();
            //设置SQL语句
            updateEmpl.CommandText = "UPDATE officedba.EmployeeInfo SET Flag = @Flag WHERE ID = @EmplID";
            //标识参数
            updateEmpl.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", ConstUtil.JOB_FLAG_LEAVE));
            //ID
            updateEmpl.Parameters.Add(SqlHelper.GetParameterFromString("@EmplID", model.EmployeeID));
            //添加更新命令
            lstUpdate.Add(updateEmpl);
            //对应申请输入时，更新对应申请的状态
            if (!string.IsNullOrEmpty(model.MoveApplyNo))
            {
                //定义变量
                SqlCommand updateApply = new SqlCommand();
                //设置SQL语句
                updateApply.CommandText = "UPDATE officedba.MoveApply SET Status = @Status WHERE MoveApplyNo = @MoveApplyNo AND CompanyCD = @CompanyCD";
                //状态标识
                updateApply.Parameters.Add(SqlHelper.GetParameterFromString("@Status", "1"));
                //申请编号
                updateApply.Parameters.Add(SqlHelper.GetParameterFromString("@MoveApplyNo", model.MoveApplyNo));
                //公司代码
                updateApply.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

                lstUpdate.Add(updateApply);
            }

            //执行更新并设置更新结果
            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);
        }
        #endregion

        #region 新建离职信息
        /// <summary>
        /// 新建离职信息 
        /// </summary>
        /// <param name="model">离职信息</param>
        /// <returns></returns>
        public static bool InsertMoveNotifyInfo(MoveNotifyModel model)
        {
            #region 登陆SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO          ");
            insertSql.AppendLine(" officedba.MoveNotify ");
            insertSql.AppendLine(" 	(CompanyCD          ");
            insertSql.AppendLine(" 	,NotifyNo           ");
            insertSql.AppendLine(" 	,Title              ");
            insertSql.AppendLine(" 	,MoveApplyNo        ");
            insertSql.AppendLine(" 	,EmployeeID         ");
            insertSql.AppendLine(" 	,Reason             ");
            insertSql.AppendLine(" 	,OutDate            ");
            insertSql.AppendLine(" 	,JobNote            ");
            insertSql.AppendLine(" 	,Remark             ");
            insertSql.AppendLine(" 	,BillStatus         ");
            insertSql.AppendLine(" 	,Creator            ");
            insertSql.AppendLine(" 	,CreateDate         ");
            insertSql.AppendLine(" 	,ModifiedDate       ");
            insertSql.AppendLine(" 	,ModifiedUserID)    ");
            insertSql.AppendLine(" VALUES               ");
            insertSql.AppendLine(" 	(@CompanyCD         ");
            insertSql.AppendLine(" 	,@NotifyNo          ");
            insertSql.AppendLine(" 	,@Title             ");
            insertSql.AppendLine(" 	,@MoveApplyNo       ");
            insertSql.AppendLine(" 	,@EmployeeID        ");
            insertSql.AppendLine(" 	,@Reason            ");
            insertSql.AppendLine(" 	,@OutDate           ");
            insertSql.AppendLine(" 	,@JobNote           ");
            insertSql.AppendLine(" 	,@Remark            ");
            insertSql.AppendLine(" 	,'1'                ");
            insertSql.AppendLine(" 	,@Creator           ");
            insertSql.AppendLine(" 	,@CreateDate        ");
            insertSql.AppendLine(" 	,getdate()          ");
            insertSql.AppendLine(" 	,@ModifiedUserID)   ");
            insertSql.AppendLine("   SET @MoveNotifyID= @@IDENTITY  ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@MoveNotifyID", SqlDbType.Int));

            //执行登陆操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
            //设置ID
            model.ID = comm.Parameters["@MoveNotifyID"].Value.ToString();

            //执行插入并返回插入结果
            return isSucc;
        }
        #endregion

        #region 更新离职信息
        /// <summary>
        /// 更新离职信息
        /// </summary>
        /// <param name="model">离职信息</param>
        /// <returns></returns>
        public static bool UpdateMoveNotifyInfo(MoveNotifyModel model)
        {

            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.MoveNotify        ");
            updateSql.AppendLine(" SET  Title = @Title                ");
            updateSql.AppendLine(" 	,MoveApplyNo = @MoveApplyNo       ");
            updateSql.AppendLine(" 	,EmployeeID = @EmployeeID         ");
            updateSql.AppendLine(" 	,Reason = @Reason                 ");
            updateSql.AppendLine(" 	,OutDate = @OutDate               ");
            updateSql.AppendLine(" 	,JobNote = @JobNote               ");
            updateSql.AppendLine(" 	,Remark = @Remark                 ");
            updateSql.AppendLine(" 	,Creator = @Creator               ");
            updateSql.AppendLine(" 	,CreateDate = @CreateDate         ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            updateSql.AppendLine("  AND NotifyNo = @NotifyNo          ");
            #endregion

            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //其他参数
            SetSaveParameter(comm, model);

            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        #region 保存时参数设置
        /// <summary>
        /// 保存时参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">人才代理信息</param>
        private static void SetSaveParameter(SqlCommand comm, MoveNotifyModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//企业代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NotifyNo", model.NotifyNo));//离职单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));//离职单主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MoveApplyNo", model.MoveApplyNo));//离职申请编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));//离职人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Reason", model.Reason));//离职事由
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutDate", model.OutDate));//离职时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@JobNote", model.JobNote));//离职交接说明
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", model.Creator));//制单人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", model.CreateDate));//制单日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//更新用户ID
        }
        #endregion

        #region 删除离职信息
        /// <summary>
        /// 删除离职信息
        /// </summary>
        /// <param name="no">离职编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteMoveNotifyInfo(string no, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.MoveNotify ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" NotifyNo In( " + no + ")");
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

        #region 能否新建离职单信息
        /// <summary>
        /// 能否新建离职单信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IsExistInfo(string EmployeeID)
        {

            string sql = "select * from officedba.MoveNotify WHERE EmployeeID=" + EmployeeID + "  AND BillStatus='1'";
            return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
        }
        #endregion

    }
}
