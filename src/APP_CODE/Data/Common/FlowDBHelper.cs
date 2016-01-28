/**********************************************
 * 类作用：   通用审批流程数据层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/07
 ***********************************************/

using System;
using XBase.Model.Office.ProductionManager;
using XBase.Data.Office.ProductionManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Business.Common;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;

namespace XBase.Data.Common
{
    public class FlowDBHelper
    {
        /// <summary>
        /// 获取功能模块的流程步骤
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="FlowNo"></param>
        /// <returns></returns>
        public static DataSet GetFlow(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID)
        {
            string[] sql = new string[3];
            sql[0] = "select ID,FlowNo,FlowName,isFlowApply=0,isnull(IsMobileNotice,1) as IsMobileNotice from officedba.Flow where CompanyCD='" + CompanyCD + "' and BillTypeFlag=" + BillTypeFlag + " and BillTypeCode=" + BillTypeCode + " and UsedStatus=2";
            sql[1] = "select top 1 a.ID,a.FlowNo,a.FlowName,isFlowApply=1,isnull(a.IsMobileNotice,1) as IsMobileNotice from officedba.Flow a inner join officedba.FlowInstance b on b.FlowNo=a.FlowNo where a.CompanyCD='" + CompanyCD + "' and b.CompanyCD='" + CompanyCD + "' and b.BillTypeFlag=" + BillTypeFlag + " and b.BillTypeCode=" + BillTypeCode + " and b.BillID=" + BillID + " and b.FlowStatus<>4 order by b.id desc";

            sql[2] = "select top 1 TypeName from pubdba.BillType  where TypeFlag=" + BillTypeFlag + " and TypeCode=" + BillTypeCode + " and TypeLabel=0";
            return SqlHelper.ExecuteForListWithSQL(sql);
        }

        #region 提交审批
        /// <summary>
        /// 提交审批
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="FlowNo"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="PageUrl"></param>
        /// <param name="ApplyUserId"></param>
        /// <param name="ModifiedUserID"></param>
        /// <returns></returns>
        public static string FlowApplyAdd(string CompanyCD, string @FlowNo, int BillTypeFlag, int BillTypeCode, int BillID, string BillNo, string PageUrl, string ApplyUserId, string ModifiedUserID)
        {
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[10];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@FlowNo", FlowNo);
                param[2] = SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag);
                param[3] = SqlHelper.GetParameter("@BillTypeCode", BillTypeCode);
                param[4] = SqlHelper.GetParameter("@BillID", BillID);
                param[5] = SqlHelper.GetParameter("@BillNo", BillNo);
                param[6] = SqlHelper.GetParameter("@PageUrl", PageUrl);
                param[7] = SqlHelper.GetParameter("@ApplyUserId", ApplyUserId);
                param[8] = SqlHelper.GetParameter("@ModifiedUserID", ModifiedUserID);
                SqlParameter paramRetVal = new SqlParameter("@RetVal", SqlDbType.VarChar, 100);
                paramRetVal.Direction = ParameterDirection.Output;
                param[9] = paramRetVal;
                #endregion

                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.FlowApply_Insert", comm, param);
                string Retval = comm.Parameters["@RetVal"].Value.ToString();

                return Retval;

            }
            catch
            {
                return "0|失败";
            }
        }
        #endregion

        #region 审批
        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="FlowNo"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="State"></param>
        /// <param name="Note"></param>
        /// <param name="ModifiedUserID"></param>
        /// <returns></returns>
        public static string FlowApprovalAdd(string CompanyCD, string @FlowNo, int BillTypeFlag, int BillTypeCode, int BillID, string State, string Note, string ModifiedUserID, int ModifiedEmployeeID)
        {
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[10];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@FlowNo", FlowNo);
                param[2] = SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag);
                param[3] = SqlHelper.GetParameter("@BillTypeCode", BillTypeCode);
                param[4] = SqlHelper.GetParameter("@BillID", BillID);
                param[5] = SqlHelper.GetParameter("@State", State);
                param[6] = SqlHelper.GetParameter("@Note", Note);
                param[7] = SqlHelper.GetParameter("@ModifiedUserID", ModifiedUserID);
                param[8] = SqlHelper.GetParameter("@ModifiedEmployeeID", ModifiedEmployeeID);
                SqlParameter paramRetVal = new SqlParameter("@RetVal", SqlDbType.VarChar, 100);
                paramRetVal.Direction = ParameterDirection.Output;
                param[9] = paramRetVal;
                #endregion

                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.FlowApproval_Insert", comm, param);
                string Retval = comm.Parameters["@RetVal"].Value.ToString();

                return Retval;
            }
            catch
            {
                return "0|失败";
            }
        }
        #endregion

        #region 撤消审批
        /// <summary>
        /// 撤消审批
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="FlowNo"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="State"></param>
        /// <param name="ModifiedUserID"></param>
        /// <returns></returns>
        public static string FlowApprovalUpdate(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID, string ModifiedUserID, int ModifiedEmployeeID)
        {
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[7];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag);
                param[2] = SqlHelper.GetParameter("@BillTypeCode", BillTypeCode);
                param[3] = SqlHelper.GetParameter("@BillID", BillID);
                param[4] = SqlHelper.GetParameter("@ModifiedUserID", ModifiedUserID);
                param[5] = SqlHelper.GetParameter("@ModifiedEmployeeID", ModifiedEmployeeID);
                SqlParameter paramRetVal = new SqlParameter("@RetVal", SqlDbType.VarChar, 100);
                paramRetVal.Direction = ParameterDirection.Output;
                param[6] = paramRetVal;
                #endregion

                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.FlowApproval_Update", comm, param);
                string Retval = comm.Parameters["@RetVal"].Value.ToString();

                return Retval;
            }
            catch
            {
                return "0|失败";
            }
        }
        #endregion


        #region 单据状态
        /// <summary>
        /// 单据处理状态
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="FlowNo"></param>
        /// <returns></returns>
        public static int GetBillUsedStatus(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID)
        {
            int result = 0;
            try
            {
                string sql = "select top 1 FlowStatus from officedba.FlowInstance where CompanyCD=@CompanyCD and BillTypeFlag=@BillTypeFlag and BillTypeCode=@BillTypeCode and BillID=@BillID order by ID desc";
                SqlParameter[] param = new SqlParameter[4];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag);
                param[2] = SqlHelper.GetParameter("@BillTypeCode", BillTypeCode);
                param[3] = SqlHelper.GetParameter("@BillID", BillID);

                DataTable dtFlow = SqlHelper.ExecuteSql(sql, param);
                if (Convert.ToInt32(dtFlow.Rows[0]["FlowStatus"]) > 0)
                {
                    result = Convert.ToInt32(dtFlow.Rows[0]["FlowStatus"]);
                }
            }
            catch 
            {
                result = 0;
            }
            return result;
        }
        #endregion

        #region 查询：流程步骤
        /// <summary>
        /// 查询工作中心
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetFlowStep(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID)
        {
            //string sql = "select a.StepNo,a.StepName,b.EmployeeName as ActorReal,CurrentStep=0,CountStep=0 from officedba.FlowStepActor a left join officedba.EmployeeInfo b on a.Actor=b.ID where a.FlowNo=(select top 1 FlowNo from officedba.FlowInstance where CompanyCD='" + CompanyCD + "' and BillTypeFlag=" + BillTypeFlag + " and BillTypeCode=" + BillTypeCode + " and BillID=" + BillID + " and FlowStatus not in(3,4) order by ID desc)";
            //return SqlHelper.ExecuteSql(sql);

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select a.StepNo,a.StepName,b.EmployeeName as ActorReal,CurrentStep=0,CountStep=0,a.Actor as EmployeeID  ");
            infoSql.AppendLine("from officedba.FlowStepActor a ");
            infoSql.AppendLine("left join officedba.EmployeeInfo b on a.Actor=b.ID");
            infoSql.AppendLine("where a.CompanyCD=@CompanyCD and a.FlowNo=(");
            infoSql.AppendLine("					select top 1 FlowNo from officedba.FlowInstance ");
            infoSql.AppendLine("					where CompanyCD=@CompanyCD");
            infoSql.AppendLine("					and BillTypeFlag=@BillTypeFlag");
            infoSql.AppendLine("					and BillTypeCode=@BillTypeCode");
            infoSql.AppendLine("					and BillID=@BillID");
            infoSql.AppendLine("					order by ID desc");
            infoSql.AppendLine("				)");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", BillTypeFlag.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", BillTypeCode.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillID", BillID.ToString()));

            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 当前审批的步骤
        /// <summary>
        /// 已经审批到第几步+1
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <returns></returns>
        public static int CurrentStep(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID)
        {
            string sql = "select StepNo from officedba.FlowTaskList where companycd=@CompanyCD and  FlowInStanceID=(select top 1 ID from officedba.FlowInstance where BillTypeFlag=@BillTypeFlag and BillTypeCode=@BillTypeCode and BillID=@BillID order by id desc)";
            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            parms[1] = SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag);
            parms[2] = SqlHelper.GetParameter("@BillTypeCode", BillTypeCode);
            parms[3] = SqlHelper.GetParameter("@BillID", BillID);
            object obj = SqlHelper.ExecuteScalar(sql, parms);
            return Convert.ToInt32(obj);
        }
        #endregion

        #region 审批操作记录
        /// <summary>
        /// 审批操作记录
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="FlowNo"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <returns></returns>
        public static DataTable GetOperateRecordList(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from ");
            searchSql.AppendLine("(");
            searchSql.AppendLine("	select  StepNo=null,State=null,a.ApplyUserId,a.ApplyDate as operateDate,Note='',c.EmployeeName as Operator,b.EmployeeID as EmployeeID");
            searchSql.AppendLine("	from officedba.FlowInstance  a");
            searchSql.AppendLine("	left join officedba.UserInfo b on a.ApplyUserID=b.UserID");
            searchSql.AppendLine("	left join officedba.EmployeeInfo c on c.ID=b.EmployeeID");
            searchSql.AppendLine("	where a.CompanyCD=@CompanyCD ");
            searchSql.AppendLine("		and a.BillTypeFlag=@BillTypeFlag");
            searchSql.AppendLine("		and a.BillTypeCode=@BillTypeCode");
            searchSql.AppendLine("		and a.BillID=@BillID");
            searchSql.AppendLine(") as infoA");
            searchSql.AppendLine("union all");
            searchSql.AppendLine("(");
            searchSql.AppendLine("	select a.StepNo,a.State,a.operateUserId,a.operateDate,a.Note,e.EmployeeName as Operator,d.EmployeeID as EmployeeID");
            searchSql.AppendLine("	from officedba.FlowTaskHistory a");
            searchSql.AppendLine("	left join officedba.UserInfo d on a.operateUserId=d.UserId");
            searchSql.AppendLine("	left join officedba.EmployeeInfo e on d.EmployeeID=e.ID");
            searchSql.AppendLine("	where a.CompanyCD=@CompanyCD");
            searchSql.AppendLine("		and FlowInstanceID in(");
            searchSql.AppendLine("								select ID from officedba.FlowInstance where CompanyCD=@CompanyCD");
            searchSql.AppendLine("								and BillTypeFlag=@BillTypeFlag and BillTypeCode=@BillTypeCode and BillID=@BillID");
            searchSql.AppendLine("							 )");
            searchSql.AppendLine(")");
            searchSql.AppendLine("order by operateDate desc");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", BillTypeFlag.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", BillTypeCode.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillID", BillID.ToString()));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion


        #region 取消确认撤消审批

        #region 获取实例ID,流程状态,流程编号
        /// <summary>
        /// 获取流程实例表信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <returns></returns>
        public static DataTable GetFlowInstanceInfo(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID)
        {
            //--获取实例ID和步骤
            //Select top 1 a.FlowInstanceID,b.FlowStatus,b.FlowNo
            //From officedba.FlowTaskList a inner join officedba.FlowInstance b 
            //     on b.ID=a.FlowInstanceID 
            //Where b.CompanyCD='AAAAAA' and b.BillTypeFlag=7 and b.BillTypeCode=1 and b.BillID=12
            //     Order by b.ID desc

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("Select top 1 a.FlowInstanceID,b.FlowStatus,b.FlowNo");
            infoSql.AppendLine("From officedba.FlowTaskList a inner join officedba.FlowInstance b ");
            infoSql.AppendLine("     on b.ID=a.FlowInstanceID ");
            infoSql.AppendLine("Where b.CompanyCD=@CompanyCD and b.BillTypeFlag=@BillTypeFlag and b.BillTypeCode=@BillTypeCode and b.BillID=@BillID");
            infoSql.AppendLine("     Order by b.ID desc");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", BillTypeFlag.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", BillTypeCode.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillID", BillID.ToString()));

            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 执行状态的单据取消确认，撤消审批
        public static void OperateCancelConfirm(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID, string loginUserID, TransactionManager tran)
        {
            //可参见撤消审批的存储过程[FlowApproval_Update]

            //--1.往流程任务历史记录表（officedba.FlowTaskHistory）插1条处理记录，
            //--记录的步骤序号为0（表示返回到流程提交人环节)，审批状态为撤销审批   
            //Insert into officedba.FlowTaskHistory(CompanyCD,FlowInstanceID,FlowNo,BillTypeID,BillID,StepNo,State,operateUserId,operateDate)
            //Values(@CompanyCD,@tempFlowInstanceID,@tempFlowNo,@BillTypeFlag,@BillID,0,2,@ModifiedUserID,getdate())

            //--2.更新流程任务处理表（officedba.FlowTaskList）中的流程步骤序号为0（表示返回到流程提交人环节）
            //Update officedba.FlowTaskList Set StepNo=0,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID
            //Where CompanyCD=@CompanyCD and FlowInstanceID=@tempFlowInstanceID

            //--3更新流程实例表（officedba.FlowInstance）中的流程状态为“撤销审批”
            //Update officedba.FlowInstance Set FlowStatus=5,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID 
            //Where CompanyCD=@CompanyCD 
            //and FlowNo=@tempFlowNo 
            //and BillTypeFlag=@BillTypeFlag 
            //and BillTypeCode=@BillTypeCode 
            //and BillID=@BillID

            DataTable dt = GetFlowInstanceInfo(CompanyCD, BillTypeFlag, BillTypeCode, BillID);
            if (dt.Rows.Count > 0)
            {
                string FlowInstanceID = dt.Rows[0]["FlowInstanceID"].ToString();
                string FlowStatus = dt.Rows[0]["FlowStatus"].ToString();
                string FlowNo = dt.Rows[0]["FlowNo"].ToString();

                OperateCancelConfirm1(tran, CompanyCD, FlowInstanceID, FlowNo, BillTypeFlag, BillID, loginUserID);
                OperateCancelConfirm2(tran, CompanyCD, FlowInstanceID, loginUserID);
                OperateCancelConfirm3(tran, CompanyCD, FlowNo, BillTypeFlag, BillTypeCode, BillID, loginUserID);
            }


        }
        #endregion

        #region 直接获得所有 取消确认所需要的 SqlCommond 泛型 pdd
        public static IList<SqlCommand> GetCancelConfirmSqlCommond(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID, string loginUserID)
        {
            DataTable dt = GetFlowInstanceInfo(CompanyCD, BillTypeFlag, BillTypeCode, BillID);
            IList<SqlCommand> SqlCmdList = new List<SqlCommand>();
            if (dt.Rows.Count > 0)
            {
                string FlowInstanceID = dt.Rows[0]["FlowInstanceID"].ToString();
                string FlowStatus = dt.Rows[0]["FlowStatus"].ToString();
                string FlowNo = dt.Rows[0]["FlowNo"].ToString();

                SqlCmdList.Add(CancelConfirmHis(CompanyCD, FlowInstanceID, FlowNo, BillTypeFlag.ToString(), BillID.ToString(), loginUserID));
                SqlCmdList.Add(CancelConfirmTsk(CompanyCD, FlowInstanceID, loginUserID));
                SqlCmdList.Add(CancelConfirmIns(CompanyCD, FlowNo, BillTypeFlag.ToString(), BillTypeCode.ToString(), BillID.ToString(), loginUserID));
            }
            return SqlCmdList;
        }
        #endregion

        #region 往流程任务历史记录表
        /// <summary>
        /// 往流程任务历史记录表
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="FlowInstanceID"></param>
        /// <param name="FlowNo"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillID"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static void OperateCancelConfirm1(TransactionManager tran, string CompanyCD, string FlowInstanceID, string FlowNo, int BillTypeFlag, int BillID, string loginUserID)
        {
            //--1.往流程任务历史记录表（officedba.FlowTaskHistory）插1条处理记录，
            //--记录的步骤序号为0（表示返回到流程提交人环节)，审批状态为撤销审批   
            //Insert into officedba.FlowTaskHistory(CompanyCD,FlowInstanceID,FlowNo,BillTypeID,BillID,StepNo,State,operateUserId,operateDate)
            //Values(@CompanyCD,@tempFlowInstanceID,@tempFlowNo,@BillTypeFlag,@BillID,0,2,@ModifiedUserID,getdate())
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Insert into officedba.FlowTaskHistory(CompanyCD,FlowInstanceID,FlowNo,BillTypeID,BillID,StepNo,State,operateUserId,operateDate) ");
            strSql.Append("Values(@CompanyCD,@tempFlowInstanceID,@tempFlowNo,@BillTypeFlag,@BillID,0,2,@ModifiedUserID,getdate())");

            #region 参数
            //设置参数
            SqlParameter[] param = new SqlParameter[6];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            param[1] = SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID);
            param[2] = SqlHelper.GetParameter("@tempFlowNo", FlowNo);
            param[3] = SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag);
            param[4] = SqlHelper.GetParameter("@BillID", BillID);
            param[5] = SqlHelper.GetParameter("@ModifiedUserID", loginUserID);

            #endregion
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
        }

        //Add by WangChao
        public static SqlCommand CancelConfirmHis(string CompanyCD, string FlowInstanceID, string FlowNo, string BillTypeFlag, string BillID, string loginUserID)
        {
            SqlCommand comm = new SqlCommand();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Insert into officedba.FlowTaskHistory(CompanyCD,FlowInstanceID,FlowNo,BillTypeID,BillID,StepNo,State,operateUserId,operateDate) ");
            strSql.Append("Values(@CompanyCD,@tempFlowInstanceID,@tempFlowNo,@BillTypeFlag,@BillID,0,2,@ModifiedUserID,getdate())");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@tempFlowInstanceID", FlowInstanceID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@tempFlowNo", FlowNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", BillTypeFlag));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillID", BillID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", loginUserID));

            comm.CommandText = strSql.ToString();
            return comm;
        }
        #endregion

        #region 更新流程任务处理表
        /// <summary>
        /// 更新流程任务处理表
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="FlowInstanceID"></param>
        /// <param name="FlowNo"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillID"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static void OperateCancelConfirm2(TransactionManager tran, string CompanyCD, string FlowInstanceID, string loginUserID)
        {
            //--2.更新流程任务处理表（officedba.FlowTaskList）中的流程步骤序号为0（表示返回到流程提交人环节）
            //Update officedba.FlowTaskList Set StepNo=0,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID
            //Where CompanyCD=@CompanyCD and FlowInstanceID=@tempFlowInstanceID
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update officedba.FlowTaskList Set StepNo=0,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID ");
            strSql.Append("Where CompanyCD=@CompanyCD and FlowInstanceID=@tempFlowInstanceID");

            #region 参数
            //设置参数
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            param[1] = SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID);
            param[2] = SqlHelper.GetParameter("@ModifiedUserID", loginUserID);

            #endregion
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
        }

        //Add by WangChao
        public static SqlCommand CancelConfirmTsk(string CompanyCD, string FlowInstanceID, string loginUserID)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update officedba.FlowTaskList Set StepNo=0,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID ");
            strSql.Append("Where CompanyCD=@CompanyCD and FlowInstanceID=@tempFlowInstanceID");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@tempFlowInstanceID", FlowInstanceID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", loginUserID));
            comm.CommandText = strSql.ToString();
            return comm;
        }
        #endregion

        #region 更新流程实例表
        /// <summary>
        /// 更新流程实例表
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="FlowNo"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <returns></returns>
        public static void OperateCancelConfirm3(TransactionManager tran, string CompanyCD, string FlowNo, int BillTypeFlag, int BillTypeCode, int BillID, string loginUserID)
        {
            //--3更新流程实例表（officedba.FlowInstance）中的流程状态为“撤销审批”
            //Update officedba.FlowInstance Set FlowStatus=5,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID 
            //Where CompanyCD=@CompanyCD 
            //and FlowNo=@tempFlowNo 
            //and BillTypeFlag=@BillTypeFlag 
            //and BillTypeCode=@BillTypeCode 
            //and BillID=@BillID
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update officedba.FlowInstance Set FlowStatus=5,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID ");
            strSql.Append("Where CompanyCD=@CompanyCD ");
            strSql.Append("and FlowNo=@tempFlowNo ");
            strSql.Append("and BillTypeFlag=@BillTypeFlag ");
            strSql.Append("and BillTypeCode=@BillTypeCode ");
            strSql.Append("and BillID=@BillID");

            #region 参数
            //设置参数
            SqlParameter[] param = new SqlParameter[6];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            param[1] = SqlHelper.GetParameter("@tempFlowNo", FlowNo);
            param[2] = SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag);
            param[3] = SqlHelper.GetParameter("@BillTypeCode", BillTypeCode);
            param[4] = SqlHelper.GetParameter("@BillID", BillID);
            param[5] = SqlHelper.GetParameter("@ModifiedUserID", loginUserID);
            #endregion
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
        }

        public static SqlCommand CancelConfirmIns(string CompanyCD, string FlowNo, string BillTypeFlag, string BillTypeCode, string BillID, string loginUserID)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update officedba.FlowInstance Set FlowStatus=5,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID ");
            strSql.Append("Where CompanyCD=@CompanyCD ");
            strSql.Append("and FlowNo=@tempFlowNo ");
            strSql.Append("and BillTypeFlag=@BillTypeFlag ");
            strSql.Append("and BillTypeCode=@BillTypeCode ");
            strSql.Append("and BillID=@BillID");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@tempFlowNo", FlowNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", BillTypeFlag));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", BillTypeCode));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillID", BillID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", loginUserID));

            comm.CommandText = strSql.ToString();
            return comm;
        }
        #endregion

        #endregion

        #region 待审批的步骤
        /// <summary>
        /// 获取下一步即将审批的步骤
        /// </summary>
        /// <returns></returns>
        public static DataTable GetNextOperateList(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID)
        {
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select a.StepNo,b.EmployeeName ,a.Actor as EmployeeID from officedba.FlowStepActor a");
            searchSql.AppendLine("left join officedba.EmployeeInfo b on a.Actor=b.ID");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD and FlowNo=(");
            searchSql.AppendLine("				select top 1 FlowNo from officedba.FlowInstance");
            searchSql.AppendLine("				where CompanyCD=@CompanyCD and BillTypeFlag=@BillTypeFlag and BillTypeCode=@BillTypeCode and BillID=@BillID order by id desc");
            searchSql.AppendLine("			  )");
            searchSql.AppendLine("and StepNo=(");
            searchSql.AppendLine("				select StepNo from officedba.FlowTaskList");
            searchSql.AppendLine("				where CompanyCD=@CompanyCD and FlowInstanceId=");
            searchSql.AppendLine("								(");
            searchSql.AppendLine("									select max(ID) from officedba.FlowInstance where CompanyCD=@CompanyCD");
            searchSql.AppendLine("												and BillTypeFlag=@BillTypeFlag and BillTypeCode=@BillTypeCode and BillID=@BillID");
            searchSql.AppendLine("								)");
            searchSql.AppendLine("			)");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", BillTypeFlag.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", BillTypeCode.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillID", BillID.ToString()));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 审批流程手机短信提醒
        /// <summary>
        /// 审批流程手机短信提醒
        /// </summary>
        /// <param name="CompanyCD">公司CD</param>
        /// <param name="FlowNo">流程编号</param>
        /// <param name="BillTypeFlag">Flag</param>
        /// <param name="BillTypeCode">Code</param>
        /// <param name="BillID">单据ID</param>
        /// <param name="State">审批状态(1:通过 0:不通过)</param>
        /// <param name="OperateType">操作类型(0:提交审批时 1:审批时)</param>
        /// <returns></returns>
        public static DataTable GetMsgRemindList(string CompanyCD, string FlowNo, int BillTypeFlag, int BillTypeCode, int BillID, int State,string OperateUserID, int OperateType)
        {

            SqlParameter[] param = new SqlParameter[8];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            param[1] = SqlHelper.GetParameter("@FlowNo", FlowNo);
            param[2] = SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag);
            param[3] = SqlHelper.GetParameter("@BillTypeCode", BillTypeCode);
            param[4] = SqlHelper.GetParameter("@BillID", BillID);
            param[5] = SqlHelper.GetParameter("@State", State);
            param[6] = SqlHelper.GetParameter("@OperateUserID", OperateUserID);
            param[7] = SqlHelper.GetParameter("@OperateType", OperateType);
            return SqlHelper.ExecuteStoredProcedure("officedba.FlowApproval_MsgRemindContactor_Select", param);
        }
        #endregion


        #region 判断是否有权限审批(创建日期：2009-07-15 16:40)
        /// <summary>
        /// 判断当前审批的用户是否有权限
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="FlowNo"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="ModifiedEmployeeID"></param>
        /// <returns></returns>
        public static string GetFlowApprovalAuthority(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID, int ModifiedEmployeeID)
        {
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[6];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag);
                param[2] = SqlHelper.GetParameter("@BillTypeCode", BillTypeCode);
                param[3] = SqlHelper.GetParameter("@BillID", BillID);
                param[4] = SqlHelper.GetParameter("@ModifiedEmployeeID", ModifiedEmployeeID);
                SqlParameter paramRetVal = new SqlParameter("@RetVal", SqlDbType.VarChar, 100);
                paramRetVal.Direction = ParameterDirection.Output;
                param[5] = paramRetVal;
                #endregion

                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.FlowApproval_HaveAuthority", comm, param);
                string Retval = comm.Parameters["@RetVal"].Value.ToString();

                return Retval;
            }
            catch 
            {
                return "0|没有权限";
            }
        }
        #endregion

        #region 删除审批流程相关信息（删除撤销审批单据时同时需删除的审批流程相关的信息）
        /// <summary>
        /// 删除审批流程相关信息
        /// 2010-8-17 add by hexw 
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="billIDStr">单据ID串，以逗号隔开</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="typeflag"></param>
        /// <param name="typecode"></param>
        public static void DelFlowInfo(TransactionManager tran, string billIDStr, string strCompanyCD, string typeflag, string typecode)
        {
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from officedba.FlowTaskList where FlowInstanceID in (select ID from officedba.FlowInstance where billid in (" + billIDStr + ") and billtypeflag='" + typeflag + "' and billtypecode='" + typecode + "' and companycd='" + strCompanyCD + "')", null);
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from officedba.FlowTaskHistory where FlowInstanceID in (select ID from officedba.FlowInstance where billid in (" + billIDStr + ") and billtypeflag='" + typeflag + "' and billtypecode='" + typecode + "' and companycd='" + strCompanyCD + "')", null);
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from officedba.FlowInstance where billid in (" + billIDStr + ") and billtypeflag='" + typeflag + "' and billtypecode='" + typecode + "'  and CompanyCD='" + strCompanyCD + "'", null);

        }
        #endregion
    }
}
