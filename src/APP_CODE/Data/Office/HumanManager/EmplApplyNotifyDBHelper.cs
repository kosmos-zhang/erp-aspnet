/**********************************************
 * 类作用：   新建调职
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
    /// 类名：EmplApplyNotifyDBHelper
    /// 描述：新建调职
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/24
    /// 最后修改时间：2009/04/24
    /// </summary>
    ///
    public class EmplApplyNotifyDBHelper
    {
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
            searchSql.AppendLine(" 	,A.EmplApplyNo                        ");
            searchSql.AppendLine(" 	,A.Title AS Title                     ");
            searchSql.AppendLine(" FROM                                   ");
            searchSql.AppendLine(" 	officedba.EmplApply A                 ");
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
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCD", ConstUtil.BILL_TYPECODE_HUMAN_EMPL_APPLY));
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
            searchSql.AppendLine(" 	,A.EmplApplyNo                        ");
            searchSql.AppendLine(" 	,A.Title AS Title                     ");
            searchSql.AppendLine(" FROM                                   ");
            searchSql.AppendLine(" 	officedba.EmplApply A                 ");
            searchSql.AppendLine(" WHERE                                  ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD AND A.Status='1'             ");
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

        #region 通过ID查询调职信息_打印
        /// <summary>
        /// 查询调职信息
        /// </summary>
        /// <param name="notifyID">调职ID</param>
        /// <returns></returns>
        public static DataTable GetEmplApplyNotifyInfoByID(string notifyID)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT * from officedba.View_EmplApplyNotifyInfo       ");
            searchSql.AppendLine(" WHERE                                                  ");
            searchSql.AppendLine(" ID = @NotifyID                                      ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //调职ID
            param[0] = SqlHelper.GetParameter("@NotifyID", notifyID);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion


        #region 通过ID查询调职信息
        /// <summary>
        /// 查询调职信息
        /// </summary>
        /// <param name="notifyID">调职ID</param>
        /// <returns></returns>
        public static DataTable GetEmplApplyNotifyInfoWithID(string notifyID)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                                 ");
            searchSql.AppendLine(" 	 A.NotifyNo                                           ");
            searchSql.AppendLine(" 	,A.Title                                              ");
            searchSql.AppendLine(" 	,A.EmplApplyNo                                        ");
            searchSql.AppendLine(" 	,A.EmployeeID                                         ");
            searchSql.AppendLine(" 	,B.EmployeeNo                                         ");
            searchSql.AppendLine(" 	,B.EmployeeName AS EmployeeName                       ");
            searchSql.AppendLine(" 	,A.NowDeptID                                          ");
            searchSql.AppendLine(" 	,C.DeptName AS NowDeptName                            ");
            searchSql.AppendLine(" 	,A.NowQuarterID                                       ");
            searchSql.AppendLine(" 	,E.QuarterName AS NowQuarterName                      ");
            searchSql.AppendLine(" 	,A.NowAdminLevel                                      ");
            searchSql.AppendLine(" 	,F.TypeName AS NowAdminLevelName                      ");
            searchSql.AppendLine(" 	,A.NewDeptID                                          ");
            searchSql.AppendLine(" 	,D.DeptName AS NewDeptName                            ");
            searchSql.AppendLine(" 	,A.NewQuarterID                                       ");
            searchSql.AppendLine(" 	,A.NewAdminLevel                                      ");
            searchSql.AppendLine(" 	,A.Reason                                             ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.OutDate,21) AS OutDate         ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.IntDate,21) AS IntDate         ");
            searchSql.AppendLine(" 	,A.Remark                                             ");
            searchSql.AppendLine(" 	,A.BillStatus                                         ");
            searchSql.AppendLine(" 	,A.Creator                                            ");
            searchSql.AppendLine(" 	,J.EmployeeName AS CreatorName                        ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.CreateDate,21) AS CreateDate   ");
            searchSql.AppendLine(" 	,A.Confirmor                                          ");
            searchSql.AppendLine(" 	,H.EmployeeName AS  ConfirmorName                     ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.ConfirmDate,21) AS ConfirmDate ");
            searchSql.AppendLine(" FROM                                                   ");
            searchSql.AppendLine(" 	officedba.EmplApplyNotify A                           ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B                    ");
            searchSql.AppendLine(" 		ON A.EmployeeID = B.ID                            ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo C                        ");
            searchSql.AppendLine(" 		ON A.NowDeptID = C.ID                             ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo D                        ");
            searchSql.AppendLine(" 		ON A.NewDeptID = D.ID                             ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter E                     ");
            searchSql.AppendLine(" 		ON A.NowQuarterID = E.ID                          ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType F                  ");
            searchSql.AppendLine(" 		ON A.NowAdminLevel = F.ID                         ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo H                    ");
            searchSql.AppendLine(" 		ON A.Confirmor = H.ID                             ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo J                    ");
            searchSql.AppendLine(" 		ON A.Creator = J.ID                               ");
            searchSql.AppendLine(" WHERE                                                  ");
            searchSql.AppendLine(" 	A.ID = @NotifyID                                      ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //调职ID
            param[0] = SqlHelper.GetParameter("@NotifyID", notifyID);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 通过检索条件查询调职信息
        /// <summary>
        /// 查询调职申请信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchEmplApplyNotifyInfo(EmplApplyNotifyModel model)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                         ");
            searchSql.AppendLine(" 	 A.ID                                         ");
            searchSql.AppendLine(" 	,ISNULL(A.BillStatus, '') AS BillStatus       ");
            searchSql.AppendLine(" 	,A.NotifyNo                                   ");
            searchSql.AppendLine(" 	,A.Title                                      ");
            searchSql.AppendLine(" 	,ISNULL(A.EmplApplyNo, '') AS EmplApplyNo     ");
            searchSql.AppendLine(" 	,ISNULL(B.EmployeeNo, '') AS EmployeeNo       ");
            searchSql.AppendLine(" 	,ISNULL(B.EmployeeName, '') AS EmployeeName   ");
            searchSql.AppendLine(" 	,ISNULL(C.DeptName, '') AS NowDeptName        ");
            searchSql.AppendLine(" 	,ISNULL(D.QuarterName, '') AS NowQuarterName  ");
            searchSql.AppendLine(" 	,ISNULL(E.DeptName, '') AS NewDeptName        ");
            searchSql.AppendLine(" 	,ISNULL(F.QuarterName, '') AS NewQuarterName  ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.OutDate,21),'') ");
            searchSql.AppendLine(" 		AS OutDate                                ");
            searchSql.AppendLine(" 	,A.ModifiedDate                               ");
            searchSql.AppendLine(" FROM                                           ");
            searchSql.AppendLine(" 	officedba.EmplApplyNotify A                   ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B            ");
            searchSql.AppendLine(" 		ON A.EmployeeID = B.ID                    ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo C                ");
            searchSql.AppendLine(" 		ON A.NowDeptID = C.ID                     ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter D             ");
            searchSql.AppendLine(" 		ON A.NowQuarterID = D.ID                  ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo E                ");
            searchSql.AppendLine(" 		ON A.NewDeptID = E.ID                     ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter F             ");
            searchSql.AppendLine(" 		ON A.NewQuarterID = F.ID                  ");
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
            //调职单主题
            if (!string.IsNullOrEmpty(model.Title))
            {
                searchSql.AppendLine("	AND A.Title LIKE '%' + @Title + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            //对应申请单
            if (!string.IsNullOrEmpty(model.EmplApplyNo))
            {
                searchSql.AppendLine("	AND A.EmplApplyNo = @EmplApplyNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmplApplyNo", model.EmplApplyNo));
            }
            //员工
            if (!string.IsNullOrEmpty(model.EmployeeID))
            {
                searchSql.AppendLine("	AND A.EmployeeID = @EmployeeID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));
            }
            #endregion

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 新建调职信息
        /// <summary>
        /// 新建调职申请信息 
        /// </summary>
        /// <param name="model">调职申请信息</param>
        /// <returns></returns>
        public static bool InsertEmplApplyNotifyInfo(EmplApplyNotifyModel model)
        {
            #region 登陆SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO               ");
            insertSql.AppendLine(" officedba.EmplApplyNotify ");
            insertSql.AppendLine(" 	(CompanyCD               ");
            insertSql.AppendLine(" 	,NotifyNo                ");
            insertSql.AppendLine(" 	,Title                   ");
            insertSql.AppendLine(" 	,EmplApplyNo             ");
            insertSql.AppendLine(" 	,EmployeeID              ");
            insertSql.AppendLine(" 	,NowDeptID               ");
            insertSql.AppendLine(" 	,NowQuarterID            ");
            insertSql.AppendLine(" 	,NowAdminLevel           ");
            insertSql.AppendLine(" 	,NewDeptID               ");
            insertSql.AppendLine(" 	,NewQuarterID            ");
            insertSql.AppendLine(" 	,NewAdminLevel           ");
            insertSql.AppendLine(" 	,Reason                  ");
            insertSql.AppendLine(" 	,OutDate                 ");
            insertSql.AppendLine(" 	,IntDate                 ");
            insertSql.AppendLine(" 	,Remark                  ");
            insertSql.AppendLine(" 	,BillStatus              ");
            insertSql.AppendLine(" 	,Creator                 ");
            insertSql.AppendLine(" 	,CreateDate              ");
            insertSql.AppendLine(" 	,ModifiedDate            ");
            insertSql.AppendLine(" 	,ModifiedUserID)         ");
            insertSql.AppendLine(" VALUES                    ");
            insertSql.AppendLine(" 	(@CompanyCD              ");
            insertSql.AppendLine(" 	,@NotifyNo               ");
            insertSql.AppendLine(" 	,@Title                  ");
            insertSql.AppendLine(" 	,@EmplApplyNo            ");
            insertSql.AppendLine(" 	,@EmployeeID             ");
            insertSql.AppendLine(" 	,@NowDeptID              ");
            insertSql.AppendLine(" 	,@NowQuarterID           ");
            insertSql.AppendLine(" 	,@NowAdminLevel          ");
            insertSql.AppendLine(" 	,@NewDeptID              ");
            insertSql.AppendLine(" 	,@NewQuarterID           ");
            insertSql.AppendLine(" 	,@NewAdminLevel          ");
            insertSql.AppendLine(" 	,@Reason                 ");
            insertSql.AppendLine(" 	,@OutDate                ");
            insertSql.AppendLine(" 	,@IntDate                ");
            insertSql.AppendLine(" 	,@Remark                 ");
            insertSql.AppendLine(" 	,'1'                     ");
            insertSql.AppendLine(" 	,@Creator                ");
            insertSql.AppendLine(" 	,@CreateDate             ");
            insertSql.AppendLine(" 	,getdate()               ");
            insertSql.AppendLine(" 	,@ModifiedUserID)        ");
            insertSql.AppendLine("   SET @EmplApplyNotify= @@IDENTITY  ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@EmplApplyNotify", SqlDbType.Int));

            //执行登陆操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
            //设置ID
            model.ID = comm.Parameters["@EmplApplyNotify"].Value.ToString();

            //执行插入并返回插入结果
            return isSucc;
        }
        #endregion

        #region 确认调职信息
        /// <summary>
        /// 确认调职信息
        /// </summary>
        /// <param name="model">调职信息</param>
        /// <returns></returns>
        public static bool ConfirmEmplApplyNotifyInfo(EmplApplyNotifyModel model)
        {
            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.EmplApplyNotify   ");
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
            //更新调职表
            lstUpdate.Add(comm);

            //更新人员信息
            SqlCommand updateEmpl = new SqlCommand();
            //设置SQL语句
            updateEmpl.CommandText = "UPDATE officedba. EmployeeInfo SET QuarterID = @QuarterID,DeptID = @DeptID,AdminLevelID = @AdminLevelID  WHERE ID = @EmplID";
            //岗位
            updateEmpl.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterID", model.NewQuarterID));
            //部门
            updateEmpl.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.NewDeptID));
            //岗位职等
            updateEmpl.Parameters.Add(SqlHelper.GetParameterFromString("@AdminLevelID", model.NewAdminLevel));
            //ID
            updateEmpl.Parameters.Add(SqlHelper.GetParameterFromString("@EmplID", model.EmployeeID));
            //添加更新命令
            lstUpdate.Add(updateEmpl);

            //对应申请输入时，更新对应申请的状态
            if (!string.IsNullOrEmpty(model.EmplApplyNo))
            {
                //定义变量
                SqlCommand updateApply = new SqlCommand();
                //设置SQL语句
                updateApply.CommandText = "UPDATE officedba.EmplApply SET Status = @Status WHERE EmplApplyNo = @EmplApplyNo AND CompanyCD = @CompanyCD";
                //状态标识
                updateApply.Parameters.Add(SqlHelper.GetParameterFromString("@Status", "1"));
                //申请编号
                updateApply.Parameters.Add(SqlHelper.GetParameterFromString("@EmplApplyNo", model.EmplApplyNo));
                //公司代码
                updateApply.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

                lstUpdate.Add(updateApply);
            }

            //执行更新并设置更新结果
            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);
        }
        #endregion

        #region 更新调职申请信息
        /// <summary>
        /// 更新调职申请信息
        /// </summary>
        /// <param name="model">调职申请信息</param>
        /// <returns></returns>
        public static bool UpdateEmplApplyNotifyInfo(EmplApplyNotifyModel model)
        {

            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.EmplApplyNotify   ");
            updateSql.AppendLine(" SET	 Title = @Title               ");
            updateSql.AppendLine(" 	,EmplApplyNo = @EmplApplyNo       ");
            updateSql.AppendLine(" 	,EmployeeID = @EmployeeID         ");
            updateSql.AppendLine(" 	,NowDeptID = @NowDeptID           ");
            updateSql.AppendLine(" 	,NowQuarterID = @NowQuarterID     ");
            updateSql.AppendLine(" 	,NowAdminLevel = @NowAdminLevel   ");
            updateSql.AppendLine(" 	,NewDeptID = @NewDeptID           ");
            updateSql.AppendLine(" 	,NewQuarterID = @NewQuarterID     ");
            updateSql.AppendLine(" 	,NewAdminLevel = @NewAdminLevel   ");
            updateSql.AppendLine(" 	,Reason = @Reason                 ");
            updateSql.AppendLine(" 	,OutDate = @OutDate               ");
            updateSql.AppendLine(" 	,IntDate = @IntDate               ");
            updateSql.AppendLine(" 	,Remark = @Remark                 ");
            updateSql.AppendLine(" 	,Creator = @Creator               ");
            updateSql.AppendLine(" 	,CreateDate = @CreateDate         ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            updateSql.AppendLine(" 	AND NotifyNo = @NotifyNo          ");
            #endregion

            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            SetSaveParameter(comm, model);//其他参数
            //执行更新
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        #region 保存时参数设置
        /// <summary>
        /// 保存时参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">人才代理信息</param>
        private static void SetSaveParameter(SqlCommand comm, EmplApplyNotifyModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//企业代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NotifyNo", model.NotifyNo));//调职单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));//调职单主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmplApplyNo", model.EmplApplyNo));//对应调职申请编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));//被调职人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NowDeptID", model.NowDeptID));//原部门(对应部门表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NowQuarterID", model.NowQuarterID));//原岗位(对应岗位表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NowAdminLevel", model.NowAdminLevel));//原岗位职等（分类代码表设置）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NewDeptID", model.NewDeptID));//调至部门ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NewQuarterID", model.NewQuarterID));//调至岗位ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@NewAdminLevel", model.NewAdminLevel));//调至岗位职等（分类代码表设置）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Reason", model.Reason));//调职事由
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutDate", model.OutDate));//调出时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@IntDate", model.IntDate));//调入时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", model.Creator));//制单人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", model.CreateDate));//制单日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//更新用户ID
        }
        #endregion

        #region 删除调职信息
        /// <summary>
        /// 删除调职信息
        /// </summary>
        /// <param name="no">调职编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteEmplApplyNotifyInfo(string no, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.EmplApplyNotify ");
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

        #region 能否新建调职单信息
        /// <summary>
        /// 能否新建调职单信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IsExistInfo(string EmployeeID)
        {

            string sql = "select * from officedba.EmplApplyNotify WHERE EmployeeID=" + EmployeeID + "  AND BillStatus='1'";
            return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
        }
        #endregion
    }
}
