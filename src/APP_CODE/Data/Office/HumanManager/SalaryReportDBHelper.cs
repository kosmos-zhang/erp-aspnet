/**********************************************
 * 类作用：   工资报表
 * 建立人：   吴志强
 * 建立时间： 2009/05/20
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
    /// 类名：SalaryReportDBHelper
    /// 描述：工资报表
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/20
    /// 最后修改时间：2009/05/20
    /// </summary>
    ///
    public class SalaryReportDBHelper
    { 
        
        #region 校验所属月份的报表是否已经生成
        /// <summary>
        /// 校验所属月份的报表是否已经生成
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="belongMonth">所属月份</param>
        /// <returns></returns>
        public static bool IsExsistReport(string companyCD, string belongMonth)
        {
            #region 校验SQL语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                          ");
            searchSql.AppendLine(" 	 ID                            ");
            searchSql.AppendLine(" 	,CompanyCD                     ");
            searchSql.AppendLine(" 	,ReprotNo                      ");
            searchSql.AppendLine(" 	,ReportName                    ");
            searchSql.AppendLine(" 	,ReportMonth                   ");
            searchSql.AppendLine(" 	,StartDate                     ");
            searchSql.AppendLine(" 	,EndDate                       ");
            searchSql.AppendLine(" 	,Creator                       ");
            searchSql.AppendLine(" 	,CreateDate                    ");
            searchSql.AppendLine(" 	,Status                        ");
            searchSql.AppendLine(" FROM                            ");
            searchSql.AppendLine(" 	officedba.SalaryReport         ");
            searchSql.AppendLine(" WHERE                           ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD         ");
            searchSql.AppendLine(" 	AND ReportMonth = @ReportMonth ");
            #endregion
            
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //所属月份
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReportMonth", belongMonth));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            DataTable dtMonth = SqlHelper.ExecuteSearch(comm);
            //数据不存在时，返回false
            if (dtMonth == null || dtMonth.Rows.Count < 1) return false;
            //数据存在时，返回true
            else return true;
        }
        #endregion

        #region 创建报表操作

        #region 登陆报表
        /// <summary>
        /// 创建报表操作
        /// </summary>
        /// <param name="model">工资报表信息</param>
        /// <param name="lstSummary">工资合计信息</param>
        /// <param name="dtFixedSalary">固定工资信息</param>
        public static bool CreateSalaryReport(SalaryReportModel model, ArrayList lstSummary, DataTable dtFixedSalary)
        {
            //变量定义
            ArrayList lstCommand = new ArrayList();

            //工资报表信息
            lstCommand.Add(InsertSalaryReport(model));
            //工资合计信息
            InsertSalarySummary(lstSummary, lstCommand);
            //工资明细
            InsertSalaryDetail(dtFixedSalary, model.CompanyCD, model.ReprotNo, model.ModifiedUserID, lstCommand);

            //执行保存操作
            bool isSucc = SqlHelper.ExecuteTransWithArrayList(lstCommand);
            if (isSucc) model.ID = ((SqlCommand)lstCommand[0]).Parameters["@ReportID"].Value.ToString();
            return isSucc;
        }
        #endregion

        #region 登陆工资报表信息
        /// <summary>
        /// 登陆工资报表信息
        /// </summary>
        /// <param name="model">工资报表信息</param>
        /// <returns></returns>
        private static SqlCommand InsertSalaryReport(SalaryReportModel model)
        {
            #region SQL语句
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO            ");
            insertSql.AppendLine(" officedba.SalaryReport ");
            insertSql.AppendLine(" (CompanyCD             ");
            insertSql.AppendLine(" ,ReprotNo              ");
            insertSql.AppendLine(" ,ReportName            ");
            insertSql.AppendLine(" ,ReportMonth           ");
            insertSql.AppendLine(" ,StartDate             ");
            insertSql.AppendLine(" ,EndDate               ");
            insertSql.AppendLine(" ,Creator               ");
            insertSql.AppendLine(" ,CreateDate            ");
            insertSql.AppendLine(" ,Status                ");
            insertSql.AppendLine(" ,ModifiedDate          ");
            insertSql.AppendLine(" ,ModifiedUserID)       ");
            insertSql.AppendLine(" VALUES                 ");
            insertSql.AppendLine(" (@CompanyCD            ");
            insertSql.AppendLine(" ,@ReprotNo             ");
            insertSql.AppendLine(" ,@ReportName           ");
            insertSql.AppendLine(" ,@ReportMonth          ");
            insertSql.AppendLine(" ,@StartDate            ");
            insertSql.AppendLine(" ,@EndDate              ");
            insertSql.AppendLine(" ,@Creator              ");
            insertSql.AppendLine(" ,@CreateDate           ");
            insertSql.AppendLine(" ,@Status               ");
            insertSql.AppendLine(" ,getdate()             ");
            insertSql.AppendLine(" ,@ModifiedUserID)      ");
            insertSql.AppendLine(" SET @ReportID= @@IDENTITY  ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = insertSql.ToString();
            //状态
            model.Status = "0";
            //设置保存的参数
            SetReportParameter(comm, model);
            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ReportID", SqlDbType.Int));

            //执行插入并返回插入结果
            return comm;
        }
        #endregion

        #region 登陆工资合计信息
        /// <summary>
        /// 登陆工资合计信息
        /// </summary>
        /// <param name="lstSummary">工资合计信息</param>
        /// <param name="lstCommand">命令</param>
        /// <returns></returns>
        private static void InsertSalarySummary(ArrayList lstSummary, ArrayList lstCommand)
        {
            //数据不存在时，返回不做处理
            if (lstSummary == null || lstSummary.Count < 1) return;

            #region SQL语句
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO                   ");
            insertSql.AppendLine(" officedba.SalaryReportSummary ");
            insertSql.AppendLine(" 	(CompanyCD                   ");
            insertSql.AppendLine(" 	,ReprotNo                    ");
            insertSql.AppendLine(" 	,EmployeeID                  ");
            insertSql.AppendLine(" 	,FixedMoney                  ");
            insertSql.AppendLine(" 	,WorkMoney                   ");
            insertSql.AppendLine(" 	,TimeMoney                   ");
            insertSql.AppendLine(" 	,CommissionMoney             ");
            insertSql.AppendLine(" 	,OtherGetMoney               ");
            insertSql.AppendLine(" 	,AllGetMoney                 ");
            insertSql.AppendLine(" 	,IncomeTax                   ");
            insertSql.AppendLine(" 	,Insurance                   ");
            insertSql.AppendLine(" 	,OtherKillMoney              ");
            insertSql.AppendLine(" 	,AllKillMoney                ");
            insertSql.AppendLine(" 	,AdminLevelName                 ");
            insertSql.AppendLine(" 	,DeptName                   ");
            insertSql.AppendLine(" 	,EmployeeName                   ");
            insertSql.AppendLine(" 	,EmployeeNo              ");
            insertSql.AppendLine(" 	,QuarterName                ");
            insertSql.AppendLine(" 	,PerformanceMoney                ");
            insertSql.AppendLine(" 	,CompanyComMoney                    ");
            insertSql.AppendLine(" 	,DeptComMoney                         ");
            insertSql.AppendLine(" 	,PersonComMoney                      ");
            insertSql.AppendLine(" 	,SalaryMoney)                ");
            insertSql.AppendLine(" VALUES                        ");
            insertSql.AppendLine(" 	(@CompanyCD                  ");
            insertSql.AppendLine(" 	,@ReprotNo                   ");
            insertSql.AppendLine(" 	,@EmployeeID                 ");
            insertSql.AppendLine(" 	,@FixedMoney                 ");
            insertSql.AppendLine(" 	,@WorkMoney                  ");
            insertSql.AppendLine(" 	,@TimeMoney                  ");
            insertSql.AppendLine(" 	,@CommissionMoney            ");
            insertSql.AppendLine(" 	,@OtherGetMoney              ");
            insertSql.AppendLine(" 	,@AllGetMoney                ");
            insertSql.AppendLine(" 	,@IncomeTax                  ");
            insertSql.AppendLine(" 	,@Insurance                  ");
            insertSql.AppendLine(" 	,@OtherKillMoney             ");
            insertSql.AppendLine(" 	,@AllKillMoney               ");
            insertSql.AppendLine(" 	,@AdminLevelName                 ");
            insertSql.AppendLine(" 	,@DeptName                   ");
            insertSql.AppendLine(" 	,@EmployeeName                   ");
            insertSql.AppendLine(" 	,@EmployeeNo              ");
            insertSql.AppendLine(" 	,@QuarterName                ");
            insertSql.AppendLine(" 	,@PerformanceMoney                ");
            insertSql.AppendLine(" 	,@CompanyComMoney                    ");
            insertSql.AppendLine(" 	,@DeptComMoney                         ");
            insertSql.AppendLine(" 	,@PersonComMoney                      ");
            insertSql.AppendLine(" 	,@SalaryMoney)               ");
            #endregion

            //遍历所有工资合计信息
            for (int i = 0; i < lstSummary.Count; i++)
            {
                //定义变量
                SalaryReportSummaryModel model = (SalaryReportSummaryModel)lstSummary[i];
                //定义更新基本信息的命令
                SqlCommand comm = new SqlCommand();
                //设置SQL语句
                comm.CommandText = insertSql.ToString();
                //设置保存的参数
                SetSummaryParameter(comm, model);

                lstCommand.Add(comm);
            }
        }
        #endregion

        #region 登陆工资明细
        /// <summary>
        /// 登陆工资明细
        /// </summary>
        /// <param name="dtFixedSalary">工资明细</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="reportNo">工资报表编号</param>
        /// <param name="lstCommand">命令</param>
        /// <returns></returns>
        private static void InsertSalaryDetail(DataTable dtFixedSalary, string companyCD
                                                                , string reportNo, string modifyUser, ArrayList lstCommand)
        {
            //数据不存在时，返回不做处理
            if (dtFixedSalary == null || dtFixedSalary.Rows.Count < 1) return;

            #region SQL语句
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO                  ");
            insertSql.AppendLine(" officedba.SalaryReportDetail ");
            insertSql.AppendLine(" 	(CompanyCD                  ");
            insertSql.AppendLine(" 	,ReprotNo                   ");
            insertSql.AppendLine(" 	,EmployeeID                 ");
            insertSql.AppendLine(" 	,ItemNo                     ");
            insertSql.AppendLine(" 	,ItemName                   ");
            insertSql.AppendLine(" 	,ItemOrder                  ");
            insertSql.AppendLine(" 	,PayFlag                    ");   
            insertSql.AppendLine(" 	,SalaryMoney                ");
            insertSql.AppendLine(" 	,ChangeFlag                ");
            insertSql.AppendLine(" 	,ModifiedDate               ");
            insertSql.AppendLine(" 	,ModifiedUserID)            ");
            insertSql.AppendLine(" VALUES                       ");
            insertSql.AppendLine(" 	(@CompanyCD                 ");
            insertSql.AppendLine(" 	,@ReprotNo                  ");
            insertSql.AppendLine(" 	,@EmployeeID                ");
            insertSql.AppendLine(" 	,@ItemNo                    ");
            insertSql.AppendLine(" 	,@ItemName                  ");
            insertSql.AppendLine(" 	,@ItemOrder                 ");
            insertSql.AppendLine(" 	,@PayFlag                   ");
            insertSql.AppendLine(" 	,@SalaryMoney               ");
            insertSql.AppendLine(" 	,@ChangeFlag                ");
            insertSql.AppendLine(" 	,getdate()                  ");
            insertSql.AppendLine(" 	,@ModifiedUserID)           ");
            #endregion

            //遍历所有工资明细信息
            for (int i = 0; i < dtFixedSalary.Rows.Count; i++)
            {
                //定义更新基本信息的命令
                SqlCommand comm = new SqlCommand();
                //设置SQL语句
                comm.CommandText = insertSql.ToString();
                //设置保存的参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));//公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReprotNo", reportNo));//工资报表编号
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", GetSafeData.GetStringFromInt(dtFixedSalary.Rows[i], "EmployeeID")));//员工ID
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemNo", GetSafeData.GetStringFromInt(dtFixedSalary.Rows[i], "ItemNo")));//工资项编号
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemName", GetSafeData.ValidateDataRow_String(dtFixedSalary.Rows[i], "ItemName")));//工资项名称
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemOrder", GetSafeData.GetStringFromInt(dtFixedSalary.Rows[i], "ItemOrder")));//排列先后顺序
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PayFlag", GetSafeData.ValidateDataRow_String(dtFixedSalary.Rows[i], "PayFlag")));//是否扣款
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SalaryMoney", GetSafeData.GetStringFromDecimal(dtFixedSalary.Rows[i], "SalaryMoney")));//工资额
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ChangeFlag", GetSafeData.GetStringFromDecimal(dtFixedSalary.Rows[i], "ChangeFlag")));//工资额
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", modifyUser));//更新用户ID

                lstCommand.Add(comm);
            }
        }
        #endregion

        #endregion

        #region 设置参数

        #region 报表参数设置
        /// <summary>
        /// 报表参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">报表</param>
        private static void SetReportParameter(SqlCommand comm, SalaryReportModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//企业代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReprotNo", model.ReprotNo));//工资报表编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReportName", model.ReportName));//工资报表主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReportMonth", model.ReportMonth));//工资月份
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));//开始时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));//结束时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", model.Creator));//创建人ID(对应员工表ID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", model.CreateDate));//编制日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", model.Status));//状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//最后更新用户ID
        }
        #endregion

        #region 工资合计参数设置
        /// <summary>
        /// 工资合计参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">工资合计信息</param>
        private static void SetSummaryParameter(SqlCommand comm, SalaryReportSummaryModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReprotNo", model.ReprotNo));//工资报表编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.EmployeeID));//员工ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FixedMoney", model.FixedMoney));//固定
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@WorkMoney", model.WorkMoney));//计件工资
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TimeMoney", model.TimeMoney));//计时工资
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CommissionMoney", model.CommissionMoney));//提成工资
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OtherGetMoney", model.OtherGetMoney));//其他应付工资额
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AllGetMoney", model.AllGetMoney));//应付工资额合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@IncomeTax", model.IncomeTax));//个人所得税（扣款）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Insurance", model.Insurance));//社保（扣款）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OtherKillMoney", model.OtherKillMoney));//其他应扣款额
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AllKillMoney", model.AllKillMoney));//应扣款额合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SalaryMoney", model.SalaryMoney));//实发工资额
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdminLevelName", model.AdminLevelName));//应付工资额合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptName", model.DeptName));//个人所得税（扣款）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeName", model.EmployeeName));//社保（扣款）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeNo", model.EmployeeNo));//其他应扣款额
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuarterName", model.QuarterName));//应扣款额合计 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PerformanceMoney", model.PerformanceMoney ));//绩效金额  
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyComMoney", model.CompanyComMoney));//公司业务提成
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptComMoney", model.DeptComMoney));//部门业务提成
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PersonComMoney", model.PersonComMoney));//个人业务提成

      
        }
        #endregion

        #endregion

        #region 保存操作
        /// <summary>
        /// 保存工资报表信息
        /// </summary>
        /// <param name="lstDetail">明细信息</param>
        /// <param name="lstSummary">合计信息</param>
        /// <param name="model">基本信息</param>
        /// <returns></returns>
        public static bool SaveSalaryInfo(ArrayList lstDetail, ArrayList lstSummary, SalaryReportModel model)
        {
            //变量定义
            ArrayList lstCommand = new ArrayList();

            //工资报表信息
            lstCommand.Add(UpdateSalaryReport(model));
            //工资合计信息
            UpdateSalaryReportSummary(model.CompanyCD, lstSummary, lstCommand);
            //工资明细
            UpdateSalaryReportDetail(model, lstDetail, lstCommand);

            //执行保存操作
            bool isSucc = SqlHelper.ExecuteTransWithArrayList(lstCommand);

            return isSucc;
        }

        #region 更新报表基本信息
        /// <summary>
        /// 更新报表基本信息
        /// </summary>
        /// <param name="model">报表基本信息</param>
        /// <returns></returns>
        private static SqlCommand UpdateSalaryReport(SalaryReportModel model)
        {

            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE                             ");
            updateSql.AppendLine(" officedba.SalaryReport             ");
            updateSql.AppendLine(" SET  ReportName = @ReportName      ");
            updateSql.AppendLine(" 	,ReportMonth = @ReportMonth       ");
            updateSql.AppendLine(" 	,StartDate = @StartDate           ");
            updateSql.AppendLine(" 	,EndDate = @EndDate               ");
            updateSql.AppendLine(" 	,Status = @Status                 ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            updateSql.AppendLine(" 	AND ReprotNo = @ReprotNo          ");
            #endregion

            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //其他参数
            model.Status = "1";
            SetReportParameter(comm, model);
            //执行更新
            return comm;
        }
        #endregion

        #region 更新工资合计信息
        /// <summary>
        /// 更新工资合计信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="lstSummary">工资合计信息</param>
        /// <param name="lstCommand">命令</param>
        /// <returns></returns>
        private static void UpdateSalaryReportSummary(string companyCD, ArrayList lstSummary, ArrayList lstCommand)
        {
            //数据不存在时，返回不做处理
            if (lstSummary == null || lstSummary.Count < 1) return;

            #region SQL语句
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.SalaryReportSummary ");
            updateSql.AppendLine(" SET  FixedMoney = @FixedMoney        ");
            updateSql.AppendLine(" 	,WorkMoney = @WorkMoney             ");
            updateSql.AppendLine(" 	,TimeMoney = @TimeMoney             ");
            updateSql.AppendLine(" 	,CommissionMoney = @CommissionMoney ");
            updateSql.AppendLine(" 	,OtherGetMoney = @OtherGetMoney     ");
            updateSql.AppendLine(" 	,AllGetMoney = @AllGetMoney         ");
            updateSql.AppendLine(" 	,IncomeTax = @IncomeTax             ");
            updateSql.AppendLine(" 	,Insurance = @Insurance             ");
            updateSql.AppendLine(" 	,OtherKillMoney = @OtherKillMoney   ");
            updateSql.AppendLine(" 	,AllKillMoney = @AllKillMoney       ");
            updateSql.AppendLine(" 	,SalaryMoney = @SalaryMoney         ");
            updateSql.AppendLine(" 	,CompanyComMoney = @CompanyComMoney             ");
            updateSql.AppendLine(" 	,DeptComMoney = @DeptComMoney   ");
            updateSql.AppendLine(" 	,PersonComMoney = @PersonComMoney       ");
            updateSql.AppendLine(" 	,PerformanceMoney = @PerformanceMoney         ");
            updateSql.AppendLine("  WHERE                               ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD              ");
            updateSql.AppendLine(" 	AND ReprotNo = @ReprotNo            ");
            updateSql.AppendLine(" 	AND EmployeeID = @EmployeeID        ");
            #endregion

            //遍历所有工资合计信息
            for (int i = 0; i < lstSummary.Count; i++)
            {
                //定义变量
                SalaryReportSummaryModel summaryModel = (SalaryReportSummaryModel)lstSummary[i];
                summaryModel.CompanyCD = companyCD;
                //定义更新基本信息的命令
                SqlCommand comm = new SqlCommand();
                //设置SQL语句
                comm.CommandText = updateSql.ToString();
                //设置保存的参数
                SetSummaryParameter(comm, summaryModel);

                lstCommand.Add(comm);
            }
        }
        #endregion

        #region 更新工资合计信息
        /// <summary>
        /// 更新工资合计信息
        /// </summary>
        /// <param name="model">基本信息</param>
        /// <param name="lstDetail">工资详细信息</param>
        /// <param name="lstCommand">命令</param>
        /// <returns></returns>
        private static void UpdateSalaryReportDetail(SalaryReportModel model, ArrayList lstDetail, ArrayList lstCommand)
        {
            //数据不存在时，返回不做处理
            if (lstDetail == null || lstDetail.Count < 1) return;
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO                  ");
            insertSql.AppendLine(" officedba.SalaryReportDetail ");
            insertSql.AppendLine(" 	(CompanyCD                  ");
            insertSql.AppendLine(" 	,ReprotNo                   ");
            insertSql.AppendLine(" 	,EmployeeID                 ");
            insertSql.AppendLine(" 	,ItemNo                     ");
            insertSql.AppendLine(" 	,ItemName                   ");
            insertSql.AppendLine(" 	,ItemOrder                  ");
            insertSql.AppendLine(" 	,PayFlag                    ");
            insertSql.AppendLine(" 	,SalaryMoney                ");
            insertSql.AppendLine(" 	,ChangeFlag                ");
            insertSql.AppendLine(" 	,ModifiedDate               ");
            insertSql.AppendLine(" 	,ModifiedUserID)            ");
            insertSql.AppendLine(" VALUES                       ");
            insertSql.AppendLine(" 	(@CompanyCD                 ");
            insertSql.AppendLine(" 	,@ReprotNo                  ");
            insertSql.AppendLine(" 	,@EmployeeID                ");
            insertSql.AppendLine(" 	,@ItemNo                    ");
            insertSql.AppendLine(" 	,@ItemName                  ");
            insertSql.AppendLine(" 	,@ItemOrder                 ");
            insertSql.AppendLine(" 	,@PayFlag                   ");
            insertSql.AppendLine(" 	,@SalaryMoney               ");
            insertSql.AppendLine(" 	,@ChangeFlag                ");
            insertSql.AppendLine(" 	,getdate()                  ");
            insertSql.AppendLine(" 	,@ModifiedUserID)           ");
            #region SQL语句
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.SalaryReportDetail ");
            updateSql.AppendLine(" SET  SalaryMoney = @SalaryMoney     ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()          ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID  ");
            updateSql.AppendLine(" WHERE                               ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD             ");
            updateSql.AppendLine(" 	AND ReprotNo = @ReprotNo           ");      
            updateSql.AppendLine(" 	AND ItemNo = @ItemNo               ");
            updateSql.AppendLine(" 	AND EmployeeID = @EmployeeID       ");
   
            #endregion

            //
            string companyCD = model.CompanyCD;
            string modifyUser = model.ModifiedUserID;

            //遍历所有工资合计信息
            for (int i = 0; i < lstDetail.Count; i++)
            {
                //定义变量
                SalaryReportDetailModel detailModel = (SalaryReportDetailModel)lstDetail[i];
                //定义更新基本信息的命令
                SqlCommand comm = new SqlCommand();
                if (IsExsistSummaryDetails(companyCD, detailModel.ReprotNo, detailModel.EmployeeID, detailModel.ItemNo))
                {
                    //设置SQL语句
                    comm.CommandText = updateSql.ToString();
                    //设置保存的参数
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));//公司代码
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReprotNo", detailModel.ReprotNo));//工资报表编号
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", detailModel.EmployeeID));//员工ID
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemNo", detailModel.ItemNo));//工资项编号
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@SalaryMoney", detailModel.SalaryMoney));//工资额
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", modifyUser));//更新用户ID
                }
                else
                {
                    comm.CommandText = insertSql.ToString();
                    //设置保存的参数
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));//公司代码
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReprotNo", detailModel.ReprotNo));//工资报表编号
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID",  detailModel.EmployeeID));//员工ID
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemNo", detailModel.ItemNo));//工资项编号
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemName", detailModel.ItemName ));//工资项名称
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemOrder", detailModel.ItemOrder ));//排列先后顺序
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@PayFlag", detailModel.PayFlag ));//是否扣款
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@SalaryMoney", detailModel.SalaryMoney));//工资额
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ChangeFlag", detailModel.ChangeFlag ));//工资额
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", modifyUser));//更新用户ID
                }

                lstCommand.Add(comm);
            }
        }
        #endregion


        public static bool IsExsistSummaryDetails(string companyCD, string ReprotNo, string EmployeeID, string ItemNo)
        {
            #region 校验SQL语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                          ");
            searchSql.AppendLine(" 	 *                            "); 
            searchSql.AppendLine(" FROM                            ");
            searchSql.AppendLine(" 	officedba.SalaryReportDetail          ");
            searchSql.AppendLine(" WHERE                           ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD             ");
            searchSql.AppendLine(" 	AND ReprotNo = @ReprotNo           "); 
            searchSql.AppendLine(" 	AND ItemNo = @ItemNo               ");
            searchSql.AppendLine(" 	AND EmployeeID = @EmployeeID       ");
          
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //所属月份
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReprotNo", ReprotNo ));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", EmployeeID));
            //所属月份
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemNo", ItemNo));
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            DataTable dtMonth = SqlHelper.ExecuteSearch(comm);
            //数据不存在时，返回false
            if (dtMonth == null || dtMonth.Rows.Count < 1) return false;
            //数据存在时，返回true
            else return true;
        }
        #endregion

        #region 查询工资报表信息
        /// <summary>
        /// 查询工资报表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable SearchReportInfo(SalaryReportModel model)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                               ");
            searchSql.AppendLine(" 	 A.ID                                               ");
            searchSql.AppendLine(" 	,A.ReprotNo                                         ");
            searchSql.AppendLine(" 	,A.ReportName                                       ");
            searchSql.AppendLine(" 	,Substring(A.ReportMonth, 1, 4) + '年'              ");
            searchSql.AppendLine(" 		+ Substring(A.ReportMonth, 5, 2) + '月'         ");
            searchSql.AppendLine(" 		AS ReportMonth                                  ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.StartDate,21) AS StartDate   ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.EndDate,21) AS EndDate       ");
            searchSql.AppendLine(" 	,ISNULL(B.EmployeeName, '') AS Creator              ");
            searchSql.AppendLine(" 	,CONVERT(VARCHAR(10),A.CreateDate,21) AS CreateDate ");
            searchSql.AppendLine(" 	,A.Status                                           ");
            searchSql.AppendLine(" 	,CASE A.Status                                      ");
            searchSql.AppendLine(" 		WHEN '0' THEN '待提交'                          ");
            searchSql.AppendLine(" 		WHEN '1' THEN '已生成'                          ");
            searchSql.AppendLine(" 		WHEN '2' THEN '已提交'                          ");
            searchSql.AppendLine(" 		WHEN '3' THEN '已确认'                          ");
            searchSql.AppendLine(" 		ELSE ''                                         ");
            searchSql.AppendLine(" 	END AS StatusName                                   ");
            searchSql.AppendLine(" 	,CASE h.FlowStatus                                  ");
            searchSql.AppendLine(" 		WHEN '1' THEN '待审批'                          ");
            searchSql.AppendLine(" 		WHEN '2' THEN '审批中'                          ");
            searchSql.AppendLine(" 		WHEN '3' THEN '审批通过'                        ");
            searchSql.AppendLine(" 		WHEN '4' THEN '审批不通过'                      ");
            searchSql.AppendLine(" 		WHEN '5' THEN '撤销审批'                      ");
            searchSql.AppendLine(" 		ELSE ' '                                   ");
            searchSql.AppendLine(" 	END AS FlowStatus ,isnull( Convert(varchar(100),A.ModifiedDate,23),'') AS ModifiedDate                              ");
            searchSql.AppendLine(" FROM                                                 ");
            searchSql.AppendLine(" 	officedba.SalaryReport A                            ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.EmployeeInfo B                  ");
            searchSql.AppendLine(" 		ON B.companyCD=A.companyCD AND B.ID = A.Creator                             ");
            //searchSql.AppendLine(" 	LEFT JOIN officedba.FlowInstance C                  ");
            //searchSql.AppendLine(" 		ON  C.CompanyCD = A.CompanyCD                   ");
            //searchSql.AppendLine(" 			AND C.BillID = A.ID                         ");
            //searchSql.AppendLine(" 			AND C.BillTypeFlag = @BillTypeFlag          ");
            //searchSql.AppendLine(" 			AND C.BillTypeCode = @BillTypeCode          ");
            //searchSql.AppendLine(" 			AND C.ModifiedDate =(                       ");
            //searchSql.AppendLine(" 				SELECT                                  ");
            //searchSql.AppendLine(" 					MAX(D.ModifiedDate)                 ");
            //searchSql.AppendLine(" 				FROM                                    ");
            //searchSql.AppendLine(" 					officedba.FlowInstance D            ");
            searchSql.AppendLine(" 	LEFT JOIN (                                ");
            searchSql.AppendLine(" 			    SELECT                                           ");
            searchSql.AppendLine(" 			        MAX(E.id) ID,E.BillID,E.BillNo                            ");
            searchSql.AppendLine(" 			    FROM                                             ");
            searchSql.AppendLine(" 			        officedba.FlowInstance E,officedba.SalaryReport  n                     ");
            searchSql.AppendLine(" 			    WHERE                                            ");
            searchSql.AppendLine(" 			        E.CompanyCD = n.CompanyCD                    ");
            searchSql.AppendLine(" 			        AND E.BillID = n.ID                     ");
            searchSql.AppendLine(" 			        AND E.BillTypeFlag = @BillTypeFlag           ");
            searchSql.AppendLine(" 			        AND E.BillTypeCode = @BillTypeCode  group by E.BillID,E.BillNo      ) g  ");
            searchSql.AppendLine(" 			        on A.ID=g.BillID ");
            searchSql.AppendLine(" 	LEFT OUTER JOIN officedba.FlowInstance h ON g.ID=h.ID  ");


            //searchSql.AppendLine(" 				WHERE                                   ");
            //searchSql.AppendLine(" 					D.CompanyCD = A.CompanyCD           ");
            //searchSql.AppendLine(" 					AND D.BillID = A.ID                 ");
            //searchSql.AppendLine(" 					AND D.BillTypeFlag = @BillTypeFlag  ");
            //searchSql.AppendLine(" 					AND D.BillTypeCode = @BillTypeCode) ");
            searchSql.AppendLine(" WHERE                                                ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD                            ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //单据类别标识
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", ConstUtil.BILL_TYPEFLAG_HUMAN));
            //单据类别编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", ConstUtil.BILL_TYPECODE_HUMAN_SALARY_REPORT));

            #region 页面输入条件
            //报表编号
            if (!string.IsNullOrEmpty(model.ReprotNo))
            {
                searchSql.AppendLine("	AND A.ReprotNo LIKE '%' + @ReprotNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReprotNo", model.ReprotNo));
            }
            //报表主题
            if (!string.IsNullOrEmpty(model.ReportName))
            {
                searchSql.AppendLine("	AND A.ReportName LIKE '%' + @ReportName + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReportName", model.ReportName));
            }
            //所属月份
            if (!string.IsNullOrEmpty(model.ReportMonth))
            {
                if (model.ReportMonth.Length == 4)
                {
                    searchSql.AppendLine("	AND A.ReportMonth LIKE @ReportMonth + '%' ");
                }
                else
                {
                    searchSql.AppendLine("	AND A.ReportMonth LIKE + '%' + @ReportMonth ");
                }
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReportMonth", model.ReportMonth));
            }
            //编制状态
            if (!string.IsNullOrEmpty(model.Status))
            {
                searchSql.AppendLine("	AND A.Status = @Status ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", model.Status));
            }
            //审批状态
            if (!string.IsNullOrEmpty(model.FlowStatus))
            {
                //待提交时
                if ("0".Equals(model.FlowStatus))
                {
                    searchSql.AppendLine("	AND H.FlowStatus IS NULL ");
                }
                else
                {
                    searchSql.AppendLine("	AND H.FlowStatus = @FlowStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", model.FlowStatus));
                }
            }
            #endregion

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        #region 通过ID获取基本信息
        /// <summary>
        /// 通过ID获取基本信息
        /// </summary>
        /// <param name="ID">报表ID</param>
        /// <returns></returns>
        public static DataTable GetReportInfoByID(string ID,string companyCD)
        {
            #region 校验SQL语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                   ");            
 	searchSql.AppendLine(" A.ID                               ");
 	searchSql.AppendLine(",A.CompanyCD                   ");     
 	searchSql.AppendLine(",A.ReprotNo                       ");  
 	searchSql.AppendLine(",A.ReportName                       ");
 	searchSql.AppendLine(",Substring(A.ReportMonth, 1, 4) AS ReportYear    ");
 	searchSql.AppendLine(",Substring(A.ReportMonth, 5, 2) AS ReportMonth    ");
 	searchSql.AppendLine(",CONVERT(VARCHAR(10),A.StartDate,21) AS StartDate    ");
 	searchSql.AppendLine(",CONVERT(VARCHAR(10),A.EndDate,21) AS EndDate    ");
 	searchSql.AppendLine(",B.EmployeeName AS Creator          ");
 	searchSql.AppendLine(",CONVERT(VARCHAR(10),A.CreateDate,21) AS CreateDate    ");
 	searchSql.AppendLine(",CASE A.Status                      ");
 	 searchSql.AppendLine("   WHEN '0' THEN '待提交 '          ");
 	 searchSql.AppendLine("   WHEN '1' THEN '已生成'          ");
 	 searchSql.AppendLine("   WHEN '2' THEN '已提交'          ");
 	 searchSql.AppendLine("   WHEN '3' THEN '已确认'          ");
 	 searchSql.AppendLine("END AS Status ,                     ");
 	 searchSql.AppendLine("  h.FlowStatus                      ");               
 	searchSql.AppendLine(",CASE h.FlowStatus                 ");                    
 	searchSql.AppendLine("	WHEN '1' THEN '待审批'             ");                
 	searchSql.AppendLine("	WHEN '2' THEN '审批中'             ");                
 	searchSql.AppendLine("	WHEN '3' THEN '审批通过'        ");                   
 searchSql.AppendLine("		WHEN '4' THEN '审批不通过'        ");                 
 searchSql.AppendLine("		WHEN '5' THEN '撤销审批'         ");                
 	searchSql.AppendLine("	ELSE '待提交'                          ");            
 	searchSql.AppendLine("END AS FlowStatus                   ");                   
 searchSql.AppendLine("FROM                                              ");      
 searchSql.AppendLine("	officedba.SalaryReport A               ");                
 searchSql.AppendLine("	LEFT JOIN officedba.EmployeeInfo B      ");               
 	searchSql.AppendLine("	ON B.ID = A.Creator                    ");            
 searchSql.AppendLine("	LEFT JOIN (                                   ");
 	searchSql.AppendLine("		    SELECT                                        ");      
 	searchSql.AppendLine("		        MAX(E.id) ID,E.BillID,E.BillNo            ");                   
 	searchSql.AppendLine("		    FROM                                                ");
 		searchSql.AppendLine("	        officedba.FlowInstance E,officedba.SalaryReport  n     ");                   
 		searchSql.AppendLine("	    WHERE                                               ");
 		searchSql.AppendLine("	        E.CompanyCD = n.CompanyCD                  ");     
 		searchSql.AppendLine("	        AND E.BillID = n.ID                        ");
        searchSql.AppendLine("	        AND E.BillTypeFlag =@BillTypeFlag             ");
        searchSql.AppendLine("	        AND E.BillTypeCode =@BillTypeCode  group by E.BillID,E.BillNo      ) g     ");
 			  searchSql.AppendLine("      on A.ID=g.BillID    ");
 	searchSql.AppendLine("LEFT OUTER JOIN officedba.FlowInstance h ON g.ID=h.ID                         ");
          

            searchSql.AppendLine(" WHERE                             ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD and                            ");
            searchSql.AppendLine(" 	A.ID = @ReportID                     ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //报表名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReportID", ID));
            //单据类别标识
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", ConstUtil.BILL_TYPEFLAG_HUMAN));
            //单据类别编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", ConstUtil.BILL_TYPECODE_HUMAN_SALARY_REPORT));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        public static DataTable GetReportInfoByNo(string ID, string companyCD)
        {
            #region 校验SQL语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                   ");
            searchSql.AppendLine(" A.ID                               ");
            searchSql.AppendLine(",A.CompanyCD                   ");
            searchSql.AppendLine(",A.ReprotNo                       ");
            searchSql.AppendLine(",A.ReportName                       ");
            searchSql.AppendLine(",Substring(A.ReportMonth, 1, 4) AS ReportYear    ");
            searchSql.AppendLine(",Substring(A.ReportMonth, 5, 2) AS ReportMonth    ");
            searchSql.AppendLine(",CONVERT(VARCHAR(10),A.StartDate,21) AS StartDate    ");
            searchSql.AppendLine(",CONVERT(VARCHAR(10),A.EndDate,21) AS EndDate    ");
            searchSql.AppendLine(",B.EmployeeName AS Creator          ");
            searchSql.AppendLine(",CONVERT(VARCHAR(10),A.CreateDate,21) AS CreateDate    ");
            searchSql.AppendLine(",CASE A.Status                      ");
            searchSql.AppendLine("   WHEN '0' THEN '待提交 '          ");
            searchSql.AppendLine("   WHEN '1' THEN '已生成'          ");
            searchSql.AppendLine("   WHEN '2' THEN '已提交'          ");
            searchSql.AppendLine("   WHEN '3' THEN '已确认'          ");
            searchSql.AppendLine("END AS Status ,                     ");
            searchSql.AppendLine("  h.FlowStatus                      ");
            searchSql.AppendLine(",CASE h.FlowStatus                 ");
            searchSql.AppendLine("	WHEN '1' THEN '待审批'             ");
            searchSql.AppendLine("	WHEN '2' THEN '审批中'             ");
            searchSql.AppendLine("	WHEN '3' THEN '审批通过'        ");
            searchSql.AppendLine("		WHEN '4' THEN '审批不通过'        ");
            searchSql.AppendLine("		WHEN '5' THEN '撤销审批'         ");
            searchSql.AppendLine("	ELSE '待提交'                          ");
            searchSql.AppendLine("END AS FlowStatus                   ");
            searchSql.AppendLine("FROM                                              ");
            searchSql.AppendLine("	officedba.SalaryReport A               ");
            searchSql.AppendLine("	LEFT JOIN officedba.EmployeeInfo B      ");
            searchSql.AppendLine("	ON B.companyCD=A.companyCD AND B.ID = A.Creator                    ");
            searchSql.AppendLine("	LEFT JOIN (                                   ");
            searchSql.AppendLine("		    SELECT                                        ");
            searchSql.AppendLine("		        MAX(E.id) ID,E.BillID,E.BillNo            ");
            searchSql.AppendLine("		    FROM                                                ");
            searchSql.AppendLine("	        officedba.FlowInstance E,officedba.SalaryReport  n     ");
            searchSql.AppendLine("	    WHERE                                               ");
            searchSql.AppendLine("	        E.CompanyCD = n.CompanyCD                  ");
            searchSql.AppendLine("	        AND E.BillID = n.ID                        ");
            searchSql.AppendLine("	        AND E.BillTypeFlag =@BillTypeFlag             ");
            searchSql.AppendLine("	        AND E.BillTypeCode =@BillTypeCode  group by E.BillID,E.BillNo      ) g     ");
            searchSql.AppendLine("      on A.ID=g.BillID    ");
            searchSql.AppendLine("LEFT OUTER JOIN officedba.FlowInstance h ON g.ID=h.ID                         ");


            searchSql.AppendLine(" WHERE                             ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD and                            ");
            searchSql.AppendLine(" 	A.ReprotNo = @ReportID                     ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //报表名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReportID", ID));
            //单据类别标识
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", ConstUtil.BILL_TYPEFLAG_HUMAN));
            //单据类别编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", ConstUtil.BILL_TYPECODE_HUMAN_SALARY_REPORT));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #region 获取工资合计信息
        /// <summary>
        /// 获取工资合计信息
        /// </summary>
        /// <param name="reportNo">报表编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetReportSummaryInfoByNo(string reportNo, string companyCD)
        {
            #region 查询SQL语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT DISTINCT                       ");
            searchSql.AppendLine(" 	 A.ID                                ");
            searchSql.AppendLine(" 	,A.EmployeeID                        ");
            searchSql.AppendLine("   ,A.FixedMoney                       ");
            searchSql.AppendLine("   ,A.WorkMoney                        ");
            searchSql.AppendLine("   ,A.TimeMoney                        ");
            searchSql.AppendLine("   ,A.CommissionMoney                  ");
            searchSql.AppendLine("   ,A.OtherGetMoney                    ");
            searchSql.AppendLine("   ,A.AllGetMoney                      ");
            searchSql.AppendLine("   ,A.IncomeTax                        ");
            searchSql.AppendLine("   ,A.Insurance                        ");
            searchSql.AppendLine("   ,A.OtherKillMoney                   ");
            searchSql.AppendLine("   ,A.AllKillMoney                     ");
            searchSql.AppendLine("   ,A.SalaryMoney                      ");
            searchSql.AppendLine(" 	,A.EmployeeNo                        ");
            searchSql.AppendLine(" 	,A.EmployeeName                      ");
            searchSql.AppendLine(" 	,A.DeptName                          ");
            searchSql.AppendLine(" 	,A.QuarterName                       ");
            searchSql.AppendLine(" 	,A. AdminLevelName        ");
            searchSql.AppendLine(" 	,A. PerformanceMoney        ");
            searchSql.AppendLine(" 	,A. CompanyComMoney        ");
            searchSql.AppendLine(" 	,A. DeptComMoney        ");
            searchSql.AppendLine(" 	,A. PersonComMoney        ");
            searchSql.AppendLine(" FROM                                  ");
            searchSql.AppendLine(" 	officedba.SalaryReportSummary A      ");
            searchSql.AppendLine(" WHERE                                 ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD             ");
            searchSql.AppendLine(" 	AND A.ReprotNo = @ReprotNo           ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //报表编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReprotNo", reportNo));
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportNo"></param>
        /// <param name="companyCD"></param>
        /// <returns></returns>
        public static DataTable GetReportSalaryDetailByNo(string reportNo, string companyCD)
        {
            #region 查询SQL语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                        ");
            searchSql.AppendLine(" 	distinct ItemName,PayFlag,ItemOrder ,ItemNo,ChangeFlag                        ");
            searchSql.AppendLine(" FROM                          ");
            searchSql.AppendLine(" 	officedba.SalaryReportDetail ");
            searchSql.AppendLine(" WHERE                         ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD       ");
            searchSql.AppendLine(" 	AND ReprotNo = @ReprotNo and ItemOrder is not null order by ItemOrder     ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //报表编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReprotNo", reportNo));
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        #region 获取工资详细信息
        /// <summary>
        /// 获取工资详细信息
        /// </summary>
        /// <param name="reportNo">报表编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetReportDetailInfoByNo(string reportNo, string companyCD)
        {
            #region 查询SQL语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                        ");
            searchSql.AppendLine(" 	 ID                          ");
            searchSql.AppendLine(" 	,EmployeeID                  ");
            searchSql.AppendLine(" 	,ItemNo                      ");
            searchSql.AppendLine(" 	,ItemName                    ");
            searchSql.AppendLine(" 	,ItemOrder                   ");
            searchSql.AppendLine(" 	,PayFlag                     ");
            searchSql.AppendLine(" 	,SalaryMoney                 ");
            searchSql.AppendLine(" FROM                          ");
            searchSql.AppendLine(" 	officedba.SalaryReportDetail ");
            searchSql.AppendLine(" WHERE                         ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD       ");
            searchSql.AppendLine(" 	AND ReprotNo = @ReprotNo     ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //报表编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReprotNo", reportNo));
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 删除工资报表信息
        /// <summary>
        /// 删除工资报表信息
        /// </summary>
        /// <param name="no">报表编号</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteReport(string no, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.SalaryReport ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" CompanyCD = @CompanyCD AND  ");
            deleteSql.AppendLine(" ReprotNo In( " + no + ")");
           

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));

            //定义更新列表
            ArrayList lstDelete = new ArrayList();
            //添加基本信息更新命令
            lstDelete.Add(comm);
            //删除工资合计信息
            DeleteSummaryInfo(lstDelete, companyCD, no);
            //删除工资详细信息
            DeleteDetailInfo(lstDelete, companyCD, no);
            //执行删除并返回
            return SqlHelper.ExecuteTransWithArrayList(lstDelete);
        }

        #region 删除工资合计信息
        public static bool DeleteOneReport(string no, string companyCD)
        {
            ArrayList lstDelete = new ArrayList();
            string ID = GetReportID(companyCD, no);
            DataTable dt = GetFlowID(companyCD, ID);
            if (dt.Rows.Count > 0)
            {
                for (int di = 0; di < dt.Rows.Count; di++)
                {
                    string FlowID = dt.Rows[di]["id"] == null ? "" : dt.Rows[di]["id"].ToString();
                    StringBuilder deleteSql2 = new StringBuilder();
                    deleteSql2.AppendLine(" DELETE FROM officedba.FlowTaskHistory ");
                    deleteSql2.AppendLine(" WHERE ");
                    deleteSql2.AppendLine(" FlowInstanceID =@FlowInstanceID");
                    deleteSql2.AppendLine(" AND CompanyCD = @CompanyCD ");
                    deleteSql2.AppendLine("  and BillID =@BillID");

                    //定义更新基本信息的命令
                    SqlCommand comm2 = new SqlCommand();
                    comm2.CommandText = deleteSql2.ToString();

                    //设置参数
                    comm2.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
                    comm2.Parameters.Add(SqlHelper.GetParameter("@FlowInstanceID", FlowID));
                    comm2.Parameters.Add(SqlHelper.GetParameter("@BillID", ID));
                    //定义更新列表
                    //添加基本信息更新命令
                    lstDelete.Add(comm2);
                }
                for (int da = 0; da < dt.Rows.Count; da++)
                {
                    string FlowID = dt.Rows[da]["id"] == null ? "" : dt.Rows[da]["id"].ToString();
                    StringBuilder deleteSql3 = new StringBuilder();
                    deleteSql3.AppendLine(" DELETE FROM officedba.FlowTaskList ");
                    deleteSql3.AppendLine(" WHERE ");  
                    deleteSql3.AppendLine(" CompanyCD = @CompanyCD    AND ");
                    deleteSql3.AppendLine(" FlowInstanceID =@FlowInstanceID");
                  

                    //定义更新基本信息的命令
                    SqlCommand comm3= new SqlCommand();
                    comm3.CommandText = deleteSql3.ToString();

                    //设置参数
                    comm3.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
                    comm3.Parameters.Add(SqlHelper.GetParameter("@FlowInstanceID", FlowID));
                    //定义更新列表
                    //添加基本信息更新命令
                    lstDelete.Add(comm3);
                }
            }




            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.SalaryReport ");
            deleteSql.AppendLine(" WHERE ");  
            deleteSql.AppendLine("CompanyCD = @CompanyCD AND  ");
            deleteSql.AppendLine(" ReprotNo =@ReprotNo");
       

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReprotNo", no));
            //定义更新列表
       
            //添加基本信息更新命令
            lstDelete.Add(comm);
            DeleteFlowInstanceInfo(lstDelete, companyCD, ID );
            //删除工资合计信息
            DeleteOneSummaryInfo(lstDelete, companyCD, no);
            //删除工资详细信息
            DeleteOneDetailInfo (lstDelete, companyCD, no);
            //执行删除并返回
            return SqlHelper.ExecuteTransWithArrayList(lstDelete);
        }
        private static void DeleteFlowInstanceInfo(ArrayList lstCommand, string companyCD, string BillID)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.FlowInstance ");
            deleteSql.AppendLine(" WHERE ");     
            deleteSql.AppendLine("  CompanyCD = @CompanyCD AND");
            deleteSql.AppendLine(" BillID =@BillID");
            deleteSql.AppendLine(" AND BillTypeFlag = @BillTypeFlag ");
            deleteSql.AppendLine(" AND BillTypeCode = @BillTypeCode ");
            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillID", BillID));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", XBase.Common.ConstUtil.BILL_TYPEFLAG_HUMAN));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillTypeCode", XBase.Common.ConstUtil.BILL_TYPECODE_HUMAN_SALARY_REPORT));
            //添加命令
            lstCommand.Add(comm);
        }
        public static DataTable GetFlowID(string companyCD, string BillID)
        {
            #region 查询SQL语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select distinct id from officedba.FlowInstance where CompanyCD=@CompanyCD and BillID=@BillID and BillTypeFlag=@BillTypeFlag  and BillTypeCode=@BillTypeCode");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //报表编号
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillID", BillID));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", XBase.Common.ConstUtil.BILL_TYPEFLAG_HUMAN));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillTypeCode", XBase.Common.ConstUtil.BILL_TYPECODE_HUMAN_SALARY_REPORT));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }


        private static string  GetReportID( string companyCD, string reprotNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" select ID  FROM officedba.SalaryReport ");
            deleteSql.AppendLine(" WHERE ");     
            deleteSql.AppendLine("CompanyCD = @CompanyCD AND  ");
            deleteSql.AppendLine(" ReprotNo =@ReprotNo");


            //定义更新基本信息的命令

            SqlParameter[] p = new SqlParameter[2];
            p[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            p[1] = SqlHelper.GetParameter("@ReprotNo", reprotNo);

          string i=Convert .ToString (   SqlHelper.ExecuteScalar(deleteSql.ToString(), p));
          return i;
        }

        private static void DeleteOneSummaryInfo(ArrayList lstCommand, string companyCD, string reprotNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.SalaryReportSummary ");
            deleteSql.AppendLine(" WHERE ");   
            deleteSql.AppendLine("CompanyCD = @CompanyCD AND  ");
            deleteSql.AppendLine(" ReprotNo =@ReprotNo");
    

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReprotNo", reprotNo ));
            //添加命令
            lstCommand.Add(comm);
        }
        /// <summary>
        /// 删除人员履历信息
        /// </summary>
        /// <param name="lstCommand">命令列表</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="reprotNo">报表编号</param>
        /// <returns></returns>
        private static void DeleteSummaryInfo(ArrayList lstCommand, string companyCD, string reprotNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.SalaryReportSummary ");
            deleteSql.AppendLine(" WHERE ");   
            deleteSql.AppendLine("CompanyCD = @CompanyCD AND  ");
            deleteSql.AppendLine(" ReprotNo In( " + reprotNo + " ) ");
       

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
            //添加命令
            lstCommand.Add(comm);
        }
        #endregion
        private static void DeleteOneDetailInfo(ArrayList lstCommand, string companyCD, string reprotNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.SalaryReportDetail ");
            deleteSql.AppendLine(" WHERE ");     
            deleteSql.AppendLine("CompanyCD = @CompanyCD AND  ");
            deleteSql.AppendLine(" ReprotNo = @ReprotNo");
 

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReprotNo", reprotNo ));
            //添加命令
            lstCommand.Add(comm);
        }
        #region 删除工资详细信息
        /// <summary>
        /// 删除工资详细信息
        /// </summary>
        /// <param name="lstCommand">命令列表</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="reprotNo">报表编号</param>
        /// <returns></returns>
        private static void DeleteDetailInfo(ArrayList lstCommand, string companyCD, string reprotNo)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.SalaryReportDetail ");
            deleteSql.AppendLine(" WHERE ");   
            deleteSql.AppendLine("CompanyCD = @CompanyCD AND  ");
            deleteSql.AppendLine(" ReprotNo in ( " + reprotNo + " ) ");
       

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
            //添加命令
            lstCommand.Add(comm);
        }
        #endregion

        #endregion

        #region 更新报表状态
        /// <summary>
        /// 更新报表状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="reprotNo">报表编号</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public static bool UpdateReportStatus(string status, string companyCD, string reprotNo, string userID)
        {
            //删除SQL拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE                             ");
            updateSql.AppendLine(" officedba.SalaryReport             ");
            updateSql.AppendLine(" SET  Status = @Status              ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            updateSql.AppendLine(" 	AND ReprotNo = @ReprotNo          ");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();

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
        #endregion
        public static bool UpdateMoveApplyCancelConfirm(string BillStatus, string ID, string userID, string CompanyID, string ReprotNo)
        {
            try
            {
              
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.SalaryReport");
                sql.AppendLine("		SET Status=@Status        ");
                sql.AppendLine("		,ModifiedDate=getdate()      ");
                sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
                sql.AppendLine("WHERE                  ");
                sql.AppendLine(" 	CompanyCD = @CompanyCD            ");
                sql.AppendLine(" 	AND ReprotNo = @ReprotNo          ");

      
             
                SqlParameter[] param;
                param = new SqlParameter[4];
                param[0] = SqlHelper.GetParameter("@Status", BillStatus);
                param[1] = SqlHelper.GetParameter("@ModifiedUserID", userID);
                param[2] = SqlHelper.GetParameter("@CompanyCD", CompanyID );
                param[3] = SqlHelper.GetParameter("@ReprotNo", ReprotNo );
             
                //SqlHelper.ExecuteTransSql(sql.ToString(), param);
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {

                     
                    FlowDBHelper.OperateCancelConfirm(CompanyID, Convert .ToInt32 (XBase.Common.ConstUtil.BILL_TYPEFLAG_HUMAN), Convert .ToInt32 (XBase.Common.ConstUtil.BILL_TYPECODE_HUMAN_SALARY_REPORT  ), Convert.ToInt32(ID), userID, tran);//取消确认
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

    }
}
