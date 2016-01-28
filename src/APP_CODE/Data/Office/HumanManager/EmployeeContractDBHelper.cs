/**********************************************
 * 类作用：   新建合同
 * 建立人：   吴志强
 * 建立时间： 2009/04/28
 ***********************************************/
using System;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using XBase.Common;

namespace XBase.Data.Office.HumanManager
{
    /// <summary>
    /// 类名：EmployeeContractDBHelper
    /// 描述：新建合同
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/28
    /// 最后修改时间：2009/04/28
    /// </summary>
    ///
    public class EmployeeContractDBHelper
    {
        #region 通过ID查询合同信息
        /// <summary>
        /// 查询合同信息
        /// </summary>
        /// <param name="contractID">合同ID</param>
        /// <returns></returns>
        public static DataTable GetEmployeeContractInfoWithID(string contractID)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                              ");
            searchSql.AppendLine(" 	 A.ContractNo                      ");
            searchSql.AppendLine(" 	,A.EmployeeID                      ");
            searchSql.AppendLine(" 	,B.EmployeeName                    ");
            searchSql.AppendLine(" 	,A.Title                           ");
            searchSql.AppendLine(" 	,A.ContractName                    ");
            searchSql.AppendLine(" 	,A.ContractKind                    ");
            searchSql.AppendLine(" 	,A.ContractType                    ");
            searchSql.AppendLine(" 	,A.ContractProperty                ");
            searchSql.AppendLine(" 	,A.ContractStatus                  ");
            searchSql.AppendLine(" 	,A.ContractPeriod                  ");
            searchSql.AppendLine(" 	,A.TestWage                        ");
            searchSql.AppendLine(" 	,A.Wage                            ");
            searchSql.AppendLine(" 	,A.SigningDate                     ");
            searchSql.AppendLine(" 	,A.StartDate                       ");
            searchSql.AppendLine(" 	,A.EndDate                         ");
            searchSql.AppendLine(" 	,A.TrialMonthCount                 ");
            searchSql.AppendLine(" 	,A.Flag                            ");
            searchSql.AppendLine(" 	,A.Attachment                      ");
            searchSql.AppendLine(" 	,A.AttachmentName                  ");
            searchSql.AppendLine(" 	,A.Reminder,C.EmployeeName ReminderName ");
            searchSql.AppendLine(" 	,A.AheadDay                        ");
            searchSql.AppendLine(" FROM                                ");
            searchSql.AppendLine(" 	officedba.EmployeeContract A       ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B ");
            searchSql.AppendLine(" 		ON A.EmployeeID = B.ID         ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo C ");
            searchSql.AppendLine(" 		ON A.Reminder = C.ID         ");
            searchSql.AppendLine(" WHERE                               ");
            searchSql.AppendLine(" 	A.ID = @ContractID                 ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //合同ID
            param[0] = SqlHelper.GetParameter("@ContractID", contractID);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 通过检索条件查询合同信息
        /// <summary>
        /// 查询合同信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchEmployeeContractInfo(EmployeeContractModel model)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                             ");
            searchSql.AppendLine(" 	 A.ID                                             ");
            searchSql.AppendLine(" 	,A.ContractNo                                     ");
            searchSql.AppendLine(" 	,A.EmployeeID                                     ");
            searchSql.AppendLine(" 	,ISNULL(B.EmployeeNo, '') AS EmployeeNo           ");
            searchSql.AppendLine(" 	,ISNULL(B.EmployeeName, '') AS EmployeeName       ");
            searchSql.AppendLine(" 	,ISNULL(A.Title, '') AS Title                     ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.SigningDate,21),'') ");
            searchSql.AppendLine(" 		AS SigningDate                                ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.StartDate,21),'')   ");
            searchSql.AppendLine(" 		AS StartDate                                  ");
            searchSql.AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),A.EndDate,21),'')     ");
            searchSql.AppendLine(" 		AS EndDate                                    ");
            searchSql.AppendLine(" 	,A.ModifiedDate                                   ");
            searchSql.AppendLine(" FROM                                               ");
            searchSql.AppendLine(" 	officedba.EmployeeContract A                      ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B                ");
            searchSql.AppendLine(" 		ON B.ID = A.EmployeeID                        ");
            searchSql.AppendLine(" WHERE                                              ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                          ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            #region 页面查询条件
            //申请编号
            if (!string.IsNullOrEmpty(model.ContractNo))
            {
                searchSql.AppendLine("	AND A.ContractNo LIKE '%' + @ContractNo + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractNo", model.ContractNo));
            }
            //主题
            if (!string.IsNullOrEmpty(model.Title))
            {
                searchSql.AppendLine("	AND A.Title LIKE '%' + @Title + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
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

        #region 新建合同信息
        /// <summary>
        /// 新建合同信息 
        /// </summary>
        /// <param name="model">合同信息</param>
        /// <returns></returns>
        public static bool InsertEmployeeContractInfo(EmployeeContractModel model)
        {
            #region 登陆SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO                ");
            insertSql.AppendLine(" officedba.EmployeeContract ");
            insertSql.AppendLine(" 	(CompanyCD                ");
            insertSql.AppendLine(" 	,ContractNo               ");
            insertSql.AppendLine(" 	,EmployeeID               ");
            insertSql.AppendLine(" 	,Title                    ");
            insertSql.AppendLine(" 	,ContractName             ");
            insertSql.AppendLine(" 	,ContractKind             ");
            insertSql.AppendLine(" 	,ContractType             ");
            insertSql.AppendLine(" 	,ContractProperty         ");
            insertSql.AppendLine(" 	,ContractStatus           ");
            insertSql.AppendLine(" 	,ContractPeriod           ");
            insertSql.AppendLine(" 	,TestWage                 ");
            insertSql.AppendLine(" 	,Wage                     ");
            insertSql.AppendLine(" 	,SigningDate              ");
            insertSql.AppendLine(" 	,StartDate                ");
            insertSql.AppendLine(" 	,EndDate                  ");
            insertSql.AppendLine(" 	,TrialMonthCount          ");
            insertSql.AppendLine(" 	,Flag                     ");
            insertSql.AppendLine(" 	,Attachment               ");
            insertSql.AppendLine(" 	,ModifiedDate             ");
            insertSql.AppendLine(" 	,ModifiedUserID          ");
            insertSql.AppendLine(" 	,Reminder             ");
            insertSql.AppendLine(" 	,AheadDay          ");
            insertSql.AppendLine(" 	,AttachmentName)          ");
            insertSql.AppendLine(" VALUES                     ");
            insertSql.AppendLine(" 	(@CompanyCD               ");
            insertSql.AppendLine(" 	,@ContractNo              ");
            insertSql.AppendLine(" 	,@EmployeeID              ");
            insertSql.AppendLine(" 	,@Title                   ");
            insertSql.AppendLine(" 	,@ContractName            ");
            insertSql.AppendLine(" 	,@ContractKind            ");
            insertSql.AppendLine(" 	,@ContractType            ");
            insertSql.AppendLine(" 	,@ContractProperty        ");
            insertSql.AppendLine(" 	,@ContractStatus          ");
            insertSql.AppendLine(" 	,@ContractPeriod          ");
            insertSql.AppendLine(" 	,@TestWage                ");
            insertSql.AppendLine(" 	,@Wage                    ");
            insertSql.AppendLine(" 	,@SigningDate             ");
            insertSql.AppendLine(" 	,@StartDate               ");
            insertSql.AppendLine(" 	,@EndDate                 ");
            insertSql.AppendLine(" 	,@TrialMonthCount         ");
            insertSql.AppendLine(" 	,@Flag                    ");
            insertSql.AppendLine(" 	,@Attachment              ");
            insertSql.AppendLine(" 	,getdate()                ");
            insertSql.AppendLine(" 	,@ModifiedUserID         ");
            insertSql.AppendLine(" 	,@Reminder             ");
            insertSql.AppendLine(" 	,@AheadDay          ");
            insertSql.AppendLine(" 	,@AttachmentName)         ");
            insertSql.AppendLine("   SET @ContractID= @@IDENTITY  ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ContractID", SqlDbType.Int));

            //执行登陆操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
            //设置ID
            model.ID = comm.Parameters["@ContractID"].Value.ToString();

            //执行插入并返回插入结果
            return isSucc;
        }
        #endregion

        #region 更新合同信息
        /// <summary>
        /// 更新合同信息
        /// </summary>
        /// <param name="model">合同信息</param>
        /// <returns></returns>
        public static bool UpdateEmployeeContractInfo(EmployeeContractModel model)
        {

            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.EmployeeContract      ");
            updateSql.AppendLine(" SET  EmployeeID = @EmployeeID          ");
            updateSql.AppendLine(" 	,Title = @Title                       ");
            updateSql.AppendLine(" 	,ContractName = @ContractName         ");
            updateSql.AppendLine(" 	,ContractKind = @ContractKind         ");
            updateSql.AppendLine(" 	,ContractType = @ContractType         ");
            updateSql.AppendLine(" 	,ContractProperty = @ContractProperty ");
            updateSql.AppendLine(" 	,ContractStatus = @ContractStatus     ");
            updateSql.AppendLine(" 	,ContractPeriod = @ContractPeriod     ");
            updateSql.AppendLine(" 	,TestWage = @TestWage                 ");
            updateSql.AppendLine(" 	,Wage = @Wage                         ");
            updateSql.AppendLine(" 	,SigningDate = @SigningDate           ");
            updateSql.AppendLine(" 	,StartDate = @StartDate               ");
            updateSql.AppendLine(" 	,EndDate = @EndDate                   ");
            updateSql.AppendLine(" 	,TrialMonthCount = @TrialMonthCount   ");
            updateSql.AppendLine(" 	,Flag = @Flag                         ");
            updateSql.AppendLine(" 	,Attachment = @Attachment             ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()             ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID     ");
            updateSql.AppendLine(" 	,AttachmentName = @AttachmentName     ");

            updateSql.AppendLine(" 	,Reminder = @Reminder     ");
            updateSql.AppendLine(" 	,AheadDay = @AheadDay     ");

            updateSql.AppendLine(" WHERE                                  ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD                ");
            updateSql.AppendLine("     AND ContractNo = @ContractNo       ");
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
        private static void SetSaveParameter(SqlCommand comm, EmployeeContractModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//企业代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractNo", model.ContractNo));//合同编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));//员工ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractName", model.ContractName));//合同名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractKind", model.ContractKind));//工种
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractType", model.ContractType));//合同类型
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractProperty", model.ContractProperty));//合同属性
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractStatus", model.ContractStatus));//合同状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContractPeriod", model.ContractPeriod));//合同期限
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TestWage", model.TestWage));//试用工资(元)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Wage", model.Wage));//转正工资(元)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SigningDate", model.SigningDate));//签约时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));//生效时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));//失效时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TrialMonthCount", model.TrialMonthCount));//试用月数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", model.Flag));//转正标识
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Attachment", model.PageAttachment));//附件
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//更新用户ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AttachmentName", model.AttachmentName));//附件名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Reminder", model.Reminder));//提醒人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AheadDay", model.AheadDay));//提前时间
        }
        #endregion

        #region 删除合同信息
        /// <summary>
        /// 删除合同信息
        /// </summary>
        /// <param name="contactNo">合同编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteEmployeeContractInfo(string contractNo, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.EmployeeContract ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" ContractNo In( " + contractNo + ")");
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

        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            try
            {
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                strCompanyCD = "AAAAAA";
            }
            string strSql = "select * from  officedba.HumanManager_report_EmployeeContract WHERE (ContractNo = @ContractNo) AND (CompanyCD = @CompanyCD)";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@ContractNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }

        #region 合同打印
        /// <summary>
        /// 合同打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static DataTable PrintContract(string CompanyCD, string EmployeeID)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                              ");
            searchSql.AppendLine(" 	 A.ContractNo                      ");
            searchSql.AppendLine(" 	,A.EmployeeID                      ");
            searchSql.AppendLine(" 	,B.EmployeeName                    ");
            searchSql.AppendLine(" 	,A.Title                    ");
            searchSql.AppendLine(" 	,(case A.ContractType when '1' then '新签合同' when '2' then '续签合同' when '3' then '变更合同' end) ContractType ");
            searchSql.AppendLine(" ,(case A.ContractProperty when '1' then '试用合同' when '2' then '正式合同' when '3' then '临时用工合同' end) ContractProperty ");
            searchSql.AppendLine(" 	,convert(varchar(20),A.SigningDate,23) SigningDate ");
            searchSql.AppendLine(" 	,convert(varchar(20),A.StartDate,23) StartDate ");
            searchSql.AppendLine(" 	,(case A.ContractStatus when '1' then '失效' when '2' then '有效' end) ContractStatus ");          
            searchSql.AppendLine(" FROM                                ");
            searchSql.AppendLine(" 	officedba.EmployeeContract A       ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B ");
            searchSql.AppendLine(" 		ON A.EmployeeID = B.ID         ");
            searchSql.AppendLine(" WHERE                               ");
            searchSql.AppendLine(" 	A.EmployeeID = @EmployeeID and A.CompanyCD = @CompanyCD ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //
            param[0] = SqlHelper.GetParameter("@EmployeeID", EmployeeID);
            param[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion
    }
}
