using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.AdminManager;
using System.Data.SqlTypes;
using System.Collections;

namespace XBase.Data.Office.AdminManager
{
    public class MeetingRecordDBHelper
    {
        #region 添加会议记录、对应发言明细、对应决议明细的方法
        /// <summary>
        /// 添加会议记录、对应发言明细、对应决议明细的方法
        /// </summary>
        /// <param name="comms">命令集</param>
        /// <returns></returns>
        public static bool AddMeetingRecord(SqlCommand[] comms)
        {
            SqlHelper.ExecuteTransForList(comms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        #region 修改会议记录、对应发言明细、对应决议明细的方法
        /// <summary>
        /// 修改会议记录、对应发言明细、对应决议明细的方法
        /// </summary>
        /// <param name="lstComm"></param>
        /// <returns></returns>
        public static bool UpdateMeetingRecord(ArrayList lstComm)
        {
            SqlHelper.ExecuteTransWithArrayList(lstComm);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        #region 根据条件检索会议记录的方法
        /// <summary>
        /// 根据条件检索会议记录
        /// </summary>
        /// <param name="MeetingRecordM">会议记录信息</param>
        /// <param name="FileDateBegin">开始时间</param>
        /// <param name="FileDateEnd">结束时间</param>        
        /// <returns>会议列表</returns>
        public static DataTable GetMeetingRecordBycondition(string CanUserID,MeetingRecordModel MeetingRecordM, string FileDateBegin, string FileDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select mr.ID,mr.RecordNo,(CASE mr.MeetingNo WHEN '0' THEN '' ELSE mr.MeetingNo END) MeetingNo,mr.Title," +
                               " mr.DeptID,isnull(di.DeptName,'') DeptName," +
                               " mr.Chairman,isnull(eic.EmployeeName,'') ChairmanName," +
                               " CONVERT(varchar(100), mr.StartDate, 23)+' '+mr.StartTime StartDate,	" +
                               " mr.Place,mro.RoomName," +
                               " mr.Recorder,isnull(eir.EmployeeName,'') EmployeeName" +
                           " from officedba.MeetingRecord mr" +
                           " left join officedba.DeptInfo di on di.id = mr.DeptID" +
                           " left join officedba.EmployeeInfo eic on eic.id = mr.Chairman	" +
                           " left join officedba.MeetingRoom mro on mro.id = mr.Place" +
                           " left join officedba.EmployeeInfo eir on eir.id = mr.Recorder" +
                           " where" +
                                " mr.CompanyCD = '" + MeetingRecordM.CompanyCD + "'" +

                                " and (charindex('," + CanUserID + ",' , ','+mr.CanViewUser+',')>0 or mr.CanViewUser like '%," + CanUserID + ",%' or '" + CanUserID + "' = mr.Recorder or mr.CanViewUser = ',,' or mr.CanViewUser is null) ";

                if (MeetingRecordM.RecordNo != "")
                    sql += " and mr.RecordNo like '%" + MeetingRecordM.RecordNo + "%'";
                if (MeetingRecordM.MeetingNo != "0")
                    sql += " and mr.MeetingNo = '" + MeetingRecordM.MeetingNo + "'";
                if (MeetingRecordM.DeptID != 0)
                    sql += " and mr.DeptID = " + MeetingRecordM.DeptID + "";
                if (MeetingRecordM.Title != "")
                    sql += " and mr.Title like '%" + MeetingRecordM.Title + "%'";
                if (MeetingRecordM.Caller != 0)
                    sql += " and mr.Caller = " + MeetingRecordM.Caller + "";
                if (MeetingRecordM.Chairman != 0)
                    sql += " and mr.Chairman = " + MeetingRecordM.Chairman + "";
                if (MeetingRecordM.TypeID != 0)
                    sql += " and mr.TypeID = " + MeetingRecordM.TypeID + "";
                if (FileDateBegin != "")
                    sql += " and mr.StartDate >= '" + FileDateBegin.ToString() + "'";
                if (FileDateEnd != "")
                    sql += " and mr.StartDate <= '" + FileDateEnd.ToString() + "'";

                #endregion

                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 批量删除某会议记录
        /// <summary>
        /// 批量删除某会议记录
        /// </summary>
        /// <param name="RecordNO"></param>
        /// <param name="TabelName">表名</param>
        /// <returns>操作记录数</returns>
        public static int DelMeetingRecord(string[] RecordNO)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string allRecordNO = "";
            string[] Delsql = new string[3];

            if (RecordNO.Length == 0)
            {
                return 0;
            }

            try
            {
                for (int i = 0; i < RecordNO.Length; i++)
                {
                    RecordNO[i] = "'" + RecordNO[i] + "'";
                    sb.Append(RecordNO[i]);
                }

                allRecordNO = sb.ToString().Replace("''", "','");
                Delsql[0] = "delete from officedba.MeetingRecord where RecordNo in (" + allRecordNO + ")";
                Delsql[1] = "delete from officedba.MeetingTalk where RecordNo in (" + allRecordNO + ")";
                Delsql[2] = "delete from officedba.MeetingDecision where RecordNo in (" + allRecordNO + ")";

                SqlHelper.ExecuteTransForListWithSQL(Delsql);
                return SqlHelper.Result.OprateCount > 0 ? SqlHelper.Result.OprateCount : 0;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 根据编号获得会议记录详细信息
        /// <summary>
        /// 根据编号获得会议记录详细信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="MeetingRecordID">会议记录编号</param>
        /// <returns>会议记录信息</returns>
        public static DataTable GetMeetingRecordByNO(string CompanyCD, string MeetingRecordNO)
        {
            try
            {
                string sql = "select " +
                                    " mr.ID,mr.MeetingNo,isnull(mi.ID,'')  miID,mr.RecordNo,mr.Title,mr.TypeID" +
                                    ",mr.Caller,eic.EmployeeName CallerName" +
                                    ",mr.DeptID,di.DeptName" +
                                    ",mr.Chairman,eih.EmployeeName ChairmanName" +
                                    ",CONVERT(varchar(100),mr.StartDate, 23) StartDate,mr.StartTime" +
                                    ",mr.TimeLong,mr.Place,mr.Topic,mr.Contents,mr.CanViewUser,mr.CanViewUserName,mr.Attachment" +
                                    //",mr.Remark,mr.JoinUser,eij.EmployeeName JoinUserName" +
                                    ",mr.Remark,mr.JoinUser" +
                                    ",mr.Recorder,eir.EmployeeName RecordName" +
                                    ",CONVERT(varchar(100),mr.RecordDate, 23) RecordDate" +
                                    ",mr.Creator,eict.EmployeeName CreatorName" +
                                    ",CONVERT(varchar(100),mr.CreateDate, 23) CreateDate" +
                                    ",mr.Sender,eis.EmployeeName SenderName" +
                                    ",CONVERT(varchar(100),mr.SendDate, 23) SendDate" +
                                    ",CONVERT(varchar(100),mr.ModifiedDate, 23) ModifiedDate" +
                                    ",mr.ModifiedUserID" +
                               " from " +
                                   " officedba.MeetingRecord mr " +
                               " left join officedba.DeptInfo di on di.id = mr.DeptID" +
                               " left join officedba.MeetingInfo mi on mi.MeetingNo = mr.MeetingNo" +
                               " left join officedba.EmployeeInfo eic on eic.id = mr.Caller" +
                               " left join officedba.EmployeeInfo eih on eih.id = mr.Chairman" +
                              // " left join officedba.EmployeeInfo eij on eij.id = mr.JoinUser" +
                               " left join officedba.EmployeeInfo eir on eir.id = mr.Recorder" +
                               " left join officedba.EmployeeInfo eict on eict.id = mr.Creator" +
                               " left join officedba.EmployeeInfo eis on eis.id = mr.Sender" +
                               " where mr.RecordNo = @RecordNo " +
                               " and mr.CompanyCD = @CompanyCD";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@RecordNo", MeetingRecordNO);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据编号获得会议记录发言信息
        /// <summary>
        /// 根据编号获得会议记录发言信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="MeetingRecordNO">会议记录编号</param>
        /// <returns></returns>
        public static DataTable GetMeetingTalkByRecordNo(string CompanyCD, string MeetingRecordNO)
        {
            try
            {
                string sql = "select " +
                                   " mt.ID,mt.RecordNo,mt.Talker,eit.EmployeeName TalkerName," +
                                   " mt.Topic,mt.Contents,mt.Important,mt.Remark" +
                               " from " +
                                   " officedba.MeetingTalk mt" +
                               " left join officedba.EmployeeInfo eit on eit.id = mt.Talker" +
                               " where mt.RecordNo = @RecordNo " +
                               " and mt.CompanyCD = @CompanyCD";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@RecordNo", MeetingRecordNO);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据编号获得会议记录决议信息
        /// <summary>
        /// 根据编号获得会议记录决议信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="MeetingRecordNO">会议记录编号</param>
        /// <returns></returns>
        public static DataTable GetMeetingDecisionByRecordNo(string CompanyCD, string MeetingRecordNO)
        {
            try
            {
                string sql = "select md.ID,md.RecordNo,md.DecisionNo,	md.Contents," +
                               " md.Principal,emp.EmployeeName PrincipalName," +
                               " md.Aim," +
                               " CONVERT(varchar(100),md.CompleteDate, 23) CompleteDate," +
                               " md.Status," +
                               " md.Cheker,emc.EmployeeName ChekerName," +
                               " CONVERT(varchar(100),md.CheckDate, 23) CheckDate," +
                               " md.CheckResult," +
                               " md.Remark," +
                               " CONVERT(varchar(100),md.ModifiedDate, 23) ModifiedDate," +
                               " md.ModifiedUserID" +
                           " from " +
                               " officedba.MeetingDecision md" +
                           " left join officedba.EmployeeInfo emp on emp.id = md.Principal" +
                           " left join officedba.EmployeeInfo emc on emc.id = md.Cheker" +
                               " where md.RecordNo = @RecordNo " +
                               " and md.CompanyCD = @CompanyCD";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@RecordNo", MeetingRecordNO);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据条件检索决议的方法
        /// <summary>
        /// 根据条件检索决议
        /// </summary>
        /// <param name="DecisionNo">决议编号</param>
        /// <param name="MeetingRecordM">决议信息</param>
        /// <param name="FileDateBegin">开始时间</param>
        /// <param name="FileDateEnd">结束时间</param>        
        /// <returns>决议列表</returns>
        public static DataTable GetMeetingDecisionBycondition(MeetingRecordModel MeetingDecisionM, string FileDateBegin, string FileDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select md.ID,md.DecisionNo,md.Principal,isnull(mi.EmployeeName,'') PrincipalName," +
                                   " isnull(CONVERT(varchar(100), md.CompleteDate, 23),'') CompleteDate,md.Status," +
                                   " md.Cheker,isnull(mic.EmployeeName,'') ChekerName," +
                                   " isnull(CONVERT(varchar(100), md.CheckDate, 23),'') CheckDate,md.RecordNo" +
                           " from " +
                               " officedba.MeetingDecision md" +
                           " left join officedba.EmployeeInfo mi on mi.id = md.Principal" +
                           " left join officedba.EmployeeInfo mic on mic.id = md.Cheker" +
                           " left join officedba.MeetingRecord mr on mr.RecordNo = md.RecordNo" +
                           " where" +
                                " md.CompanyCD = '" + MeetingDecisionM.CompanyCD + "'";
                if (MeetingDecisionM.MeetingNo != "0")
                    sql += " and mr.MeetingNo = '" + MeetingDecisionM.MeetingNo + "'";
                if (MeetingDecisionM.RecordNo != "")
                    sql += " and md.RecordNo like '%" + MeetingDecisionM.RecordNo + "%'";
                if (MeetingDecisionM.DeptID != 0)
                    sql += " and mr.DeptID = " + MeetingDecisionM.DeptID + "";
                if (MeetingDecisionM.Title != "")
                    sql += " and mr.Title like '%" + MeetingDecisionM.Title + "%'";
                if (MeetingDecisionM.Caller != 0)
                    sql += " and mr.Caller = " + MeetingDecisionM.Caller + "";
                if (MeetingDecisionM.Chairman != 0)
                    sql += " and mr.Chairman = " + MeetingDecisionM.Chairman + "";
                if (MeetingDecisionM.TypeID != 0)
                    sql += " and mr.TypeID = " + MeetingDecisionM.TypeID + "";
                if (FileDateBegin != "")
                    sql += " and mr.StartDate >= '" + FileDateBegin.ToString() + "'";
                if (FileDateEnd != "")
                    sql += " and mr.StartDate <= '" + FileDateEnd.ToString() + "'";

                #endregion

                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据ID获得会议决议详细信息
        /// <summary>
        /// 根据ID获得会议决议详细信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="MeetingInfoID">会议决议ID</param>
        /// <returns>会议决议信息</returns>
        public static DataTable GetMeetingDecisionByID(string CompanyCD, int DecisionID)
        {
            try
            {
                string sql = "select md.ID " +
                                   " ,md.RecordNo,md.DecisionNo,md.Contents" +
                                   " ,md.Principal,ei.EmployeeName PrincipalName" +
                                    ",md.Aim" +
                                    ",CONVERT(varchar(100),md.CompleteDate, 23) CompleteDate" +
                                    ",md.Status,md.Cheker,eic.EmployeeName ChekerName" +
                                    ",CONVERT(varchar(100),md.CheckDate, 23) CheckDate" +
                                    ",md.CheckResult" +
                                    ",md.Remark" +
                                    ",CONVERT(varchar(100),md.ModifiedDate, 23) ModifiedDate" +
                                    ",md.ModifiedUserID" +
                                " from " +
                                   " officedba.MeetingDecision md" +
                               " left join officedba.EmployeeInfo ei on ei.ID = md.Principal" +
                               " left join officedba.EmployeeInfo eic on eic.ID = md.Cheker" +
                               " where md.ID = @ID " +
                               " and md.CompanyCD = @CompanyCD";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@ID", DecisionID);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据会议决议ID修改会核查信息
        /// <summary>
        /// 根据会议决议ID修改会议决议核查信息
        /// </summary>
        /// <param name="MeetingInfoM">会议决议ID</param>
        /// <returns>bool值</returns>
        public static bool UpdateMeetingDecisionByID(MeetingDecisionModel MeetingDecisionM)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.MeetingDecision set ");
                sql.AppendLine("Status     =@Status     ,");
                sql.AppendLine("CheckDate  =@CheckDate  ,");
                sql.AppendLine("CheckResult  =@CheckResult  ,");
                sql.AppendLine("Remark  =@Remark  ,");
                sql.AppendLine("ModifiedDate  =@ModifiedDate  ,");
                sql.AppendLine("ModifiedUserID=@ModifiedUserID");
                sql.AppendLine(" WHERE ");
                sql.AppendLine("ID = @ID ");

                SqlParameter[] param = new SqlParameter[7];
                param[0] = SqlHelper.GetParameter("@ID", MeetingDecisionM.ID);
                param[1] = SqlHelper.GetParameter("@Status", MeetingDecisionM.Status);               
                param[2] = SqlHelper.GetParameter("@CheckDate", MeetingDecisionM.CheckDate == null
                                      ? SqlDateTime.Null
                                      : SqlDateTime.Parse(MeetingDecisionM.CheckDate.ToString()));
                param[3] = SqlHelper.GetParameter("@CheckResult",MeetingDecisionM.CheckResult);
                param[4] = SqlHelper.GetParameter("@Remark",MeetingDecisionM.Remark);
                param[5] = SqlHelper.GetParameter("@ModifiedDate  ", MeetingDecisionM.ModifiedDate);
                param[6] = SqlHelper.GetParameter("@ModifiedUserID", MeetingDecisionM.ModifiedUserID);

                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion

        #region 会议一览表
        /// <summary>
        /// 会议一览表
        /// </summary>
        /// <param name="MeetingInfoM"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetMeetingRecordList(string CompanyCD,string DeptID, string TypeID, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                //string sql = " select mi.id,mi.DeptID,di.DeptName,mi.Caller,ei.EmployeeName," +
                //                   " convert(varchar(20),Convert(datetime,(mi.StartDate + ' ' + mi.StartTime)),20) MeetingDate," +
                //                       " mi.TimeLong,mi.Title Topic,mi.TypeID,cp.TypeName," +
                //                   " (case mi.MeetingStatus when '1' then '草稿'" +
                //                                         " when '2' then '延期'" +
                //                                         " when '3' then '取消'" +
                //                                         " when '4' then '已通知' end)MeetingStatus" +
                //               " from officedba.MeetingInfo mi" +
                //               " left join officedba.DeptInfo di on di.id = mi.DeptID" +
                //               " left join officedba.EmployeeInfo ei on ei.id = mi.Caller" +
                //               " left join officedba.CodePublictype cp on cp.id = mi.TypeID" +
                //               " where mi.CompanyCD = '" + CompanyCD + "'";
                //if (DeptID != "")
                //    sql += "and mi.DeptID = '" + DeptID + "'";
                //if (TypeID != "0")
                //    sql += "and mi.TypeID = '" + TypeID + "'";
                //if (MeetingStatus != "0")
                //    sql += "and mi.MeetingStatus = '" + MeetingStatus + "'";
                //if (BeginDate != "")
                //    sql += "and convert(varchar(20),Convert(datetime,(mi.StartDate + ' ' + mi.StartTime)),20) >= '" + BeginDate + "'";
                //if (EndDate != "")
                //    sql += "and convert(varchar(20),Convert(datetime,(mi.StartDate + ' ' + mi.StartTime)),20) <= '" + EndDate + "'";


                //sql += " union" +
                string sql = " select mr.id,mr.DeptID,dir.DeptName,mr.Caller,eir.EmployeeName," +
                                   " convert(varchar(20),Convert(datetime,(mr.StartDate + ' ' + mr.StartTime)),20) MeetingDate," +
                                   " mr.TimeLong,mr.Title Topic,mr.TypeID,cpr.TypeName," +
                                   " '' MeetingStatus" +
                               " from officedba.MeetingRecord mr" +
                               " left join officedba.DeptInfo dir on dir.id = mr.DeptID" +
                               " left join officedba.EmployeeInfo eir on eir.id = mr.Caller" +
                               " left join officedba.CodePublictype cpr on cpr.id = mr.TypeID" +
                               " where mr.CompanyCD = '" + CompanyCD + "'";
                               //" and mr.MeetingNo = ''";

                if (DeptID != "")
                    sql += " and mr.DeptID = '" + DeptID + "'";
                if (TypeID != "0")
                    sql += " and mr.TypeID = '" + TypeID + "'";               
                if (BeginDate != "")
                    sql += " and convert(varchar(20),Convert(datetime,(mr.StartDate + ' ' + mr.StartTime)),20) >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += "and convert(varchar(20),Convert(datetime,(mr.StartDate + ' ' + mr.StartTime)),20) <= '" + EndDate + "'";

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);                
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 会议一览表打印
        /// <summary>
        /// 会议一览表打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="TypeID"></param>
        /// <param name="MeetingStatus"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetMeetingRecordListPrint(string CompanyCD,string DeptID, string TypeID,  string BeginDate, string EndDate, string ord)
        {
            try
            {
                //string sql = " select mi.id,mi.DeptID,di.DeptName,mi.Caller,ei.EmployeeName," +
                //                   " convert(varchar(20),Convert(datetime,(mi.StartDate + ' ' + mi.StartTime)),20) MeetingDate," +
                //                       " mi.TimeLong,mi.Topic,mi.TypeID,cp.TypeName," +
                //                   " (case mi.MeetingStatus when '1' then '草稿'" +
                //                                         " when '2' then '延期'" +
                //                                         " when '3' then '取消'" +
                //                                         " when '4' then '已通知' end)MeetingStatus" +
                //               " from officedba.MeetingInfo mi" +
                //               " left join officedba.DeptInfo di on di.id = mi.DeptID" +
                //               " left join officedba.EmployeeInfo ei on ei.id = mi.Caller" +
                //               " left join officedba.CodePublictype cp on cp.id = mi.TypeID" +
                //               " where mi.CompanyCD = '" + CompanyCD + "'";
                //if (DeptID != "")
                //    sql += "and mi.DeptID = '" + DeptID + "'";
                //if (TypeID != "0")
                //    sql += "and mi.TypeID = '" + TypeID + "'";
                ////if (MeetingStatus != "0")
                ////    sql += "and mi.MeetingStatus = '" + MeetingStatus + "'";
                //if (BeginDate != "")
                //    sql += "and convert(varchar(20),Convert(datetime,(mi.StartDate + ' ' + mi.StartTime)),20) >= '" + BeginDate + "'";
                //if (EndDate != "")
                //    sql += "and convert(varchar(20),Convert(datetime,(mi.StartDate + ' ' + mi.StartTime)),20) <= '" + EndDate + "'";


                //sql += " union" +
                string sql = " select mr.id,mr.DeptID,dir.DeptName,mr.Caller,eir.EmployeeName," +
                                   " convert(varchar(20),Convert(datetime,(mr.StartDate + ' ' + mr.StartTime)),20) MeetingDate," +
                                   " mr.TimeLong,mr.Title Topic,mr.TypeID,cpr.TypeName," +
                                   " '' MeetingStatus" +
                               " from officedba.MeetingRecord mr" +
                               " left join officedba.DeptInfo dir on dir.id = mr.DeptID" +
                               " left join officedba.EmployeeInfo eir on eir.id = mr.Caller" +
                               " left join officedba.CodePublictype cpr on cpr.id = mr.TypeID" +
                               " where mr.CompanyCD = '" + CompanyCD + "'";
                               //" and mr.MeetingNo = ''";

                if (DeptID != "")
                    sql += "and mr.DeptID = '" + DeptID + "'";
                if (TypeID != "0")
                    sql += "and mr.TypeID = '" + TypeID + "'";
                //if (MeetingStatus != "0")
                //    sql += "and 1<>1 ";
                if (BeginDate != "")
                    sql += "and convert(varchar(20),Convert(datetime,(mr.StartDate + ' ' + mr.StartTime)),20) >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += "and convert(varchar(20),Convert(datetime,(mr.StartDate + ' ' + mr.StartTime)),20) <= '" + EndDate + "'";

                sql = sql + ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 会议数量统计
        /// <summary>
        /// 会议数量统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="TypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetMeetingCount(string CompanyCD, string DeptID, string TypeID, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                //string sql = "select isnull(di.DeptName,'') DeptName,isnull(cp.TypeName,'') TypeName," +
                //                   " a.num,a.timelong" +
                //               " from " +
                //                   " (select " +
                //                       " count(*) num,sum(mi.TimeLong) timelong" +
                //                       " ,mi.DeptID,mi.TypeID,mi.CompanyCD	" +
                //                   " from " +
                //                       " officedba.MeetingInfo mi " +
                //                   " where mi.CompanyCD='" + CompanyCD + "'";
                //if (BeginDate != "")
                //    sql += " and mi.StartDate >= '" + BeginDate + "'";
                //if (EndDate != "")
                //    sql += " and mi.StartDate <= '" + EndDate + "'";

                //sql += " group by mi.DeptID,mi.TypeID,mi.CompanyCD) a" +
                //       " left join officedba.DeptInfo di on di.id = a.DeptID" +
                //       " left join officedba.CodePublicType cp on cp.id = a.TypeID where 1=1";
                //if (DeptID != "")
                //    sql += " and a.DeptID = '" + DeptID + "'";
                //if (TypeID != "0")
                //    sql += " and a.TypeID = '" + TypeID + "'";

                //sql += " union " +
                string sql =   "select isnull(dir.DeptName,'') DeptName,isnull(cpr.TypeName,'') TypeName," +
                                   " b.num,b.timelong" +
                               " from " +
                                   " (select " +
                                       " count(*) num,sum(mir.TimeLong) timelong" +
                                       " ,mir.DeptID,mir.TypeID,mir.CompanyCD	" +
                                   " from " +
                                       " officedba.MeetingRecord mir " +
                                   " where mir.CompanyCD='" + CompanyCD + "'";
                if (BeginDate != "")
                    sql += " and mir.StartDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and mir.StartDate <= '" + EndDate + "'";

                sql += " group by mir.DeptID,mir.TypeID,mir.CompanyCD) b" +
                       " left join officedba.DeptInfo dir on dir.id = b.DeptID" +
                       " left join officedba.CodePublicType cpr on cpr.id = b.TypeID where 1=1 ";
                if (DeptID != "")
                    sql += " and b.DeptID = '" + DeptID + "'";
                if (TypeID != "0")
                    sql += " and b.TypeID = '" + TypeID + "'";
                

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 会议数量统计打印
        /// <summary>
        /// 会议数量统计打印
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <param name="DeptID"></param>
       /// <param name="TypeID"></param>
       /// <param name="BeginDate"></param>
       /// <param name="EndDate"></param>
       /// <param name="ord"></param>
       /// <returns></returns>
        public static DataTable GetMeetingCountPrint(string CompanyCD, string DeptID, string TypeID, string BeginDate, string EndDate,string ord)
        {
            try
            {
                //string sql = "select isnull(di.DeptName,'') DeptName,isnull(cp.TypeName,'') TypeName," +
                //                   " a.num,a.timelong" +
                //               " from " +
                //                   " (select " +
                //                       " count(*) num,sum(mi.TimeLong) timelong" +
                //                       " ,mi.DeptID,mi.TypeID,mi.CompanyCD	" +
                //                   " from " +
                //                       " officedba.MeetingInfo mi " +
                //                   " where mi.CompanyCD='" + CompanyCD + "'";
                //if (BeginDate != "")
                //    sql += "and mi.StartDate >= '" + BeginDate + "'";
                //if (EndDate != "")
                //    sql += "and mi.StartDate <= '" + EndDate + "'";

                //sql += " group by mi.DeptID,mi.TypeID,mi.CompanyCD) a" +
                //       " left join officedba.DeptInfo di on di.id = a.DeptID" +
                //       " left join officedba.CodePublicType cp on cp.id = a.TypeID";
                //if (DeptID != "")
                //    sql += "and a.DeptID = '" + DeptID + "'";
                //if (TypeID != "0")
                //    sql += "and a.TypeID = '" + TypeID + "'";

                //sql += " union " +
                string sql = "select isnull(dir.DeptName,'') DeptName,isnull(cpr.TypeName,'') TypeName," +
                                   " b.num,b.timelong" +
                               " from " +
                                   " (select " +
                                       " count(*) num,sum(mir.TimeLong) timelong" +
                                       " ,mir.DeptID,mir.TypeID,mir.CompanyCD	" +
                                   " from " +
                                       " officedba.MeetingRecord mir " +
                                   " where  mir.CompanyCD='" + CompanyCD + "'";
                if (BeginDate != "")
                    sql += "and mir.StartDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += "and mir.StartDate <= '" + EndDate + "'";

                sql += " group by mir.DeptID,mir.TypeID,mir.CompanyCD) b" +
                       " left join officedba.DeptInfo dir on dir.id = b.DeptID" +
                       " left join officedba.CodePublicType cpr on cpr.id = b.TypeID";
                if (DeptID != "")
                    sql += "and b.DeptID = '" + DeptID + "'";
                if (TypeID != "0")
                    sql += "and b.TypeID = '" + TypeID + "'";

                sql = sql + ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch 
            {
                return null;
            }
        }
        #endregion 

        #region 人员参会状况统计
        /// <summary>
        /// 人员参会状况统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="JoinUser"></param>
        /// <param name="TypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetMeetingJoinUser(string CompanyCD, string JoinUser, string TypeID, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "select a.JoinUser,isnull(cp.TypeName,'') TypeName," +
                                   " a.num,convert(varchar(100),a.timelong )timelong" +
                               " from " +
                                   " (select " +
                                       " count(*) num,sum(mi.TimeLong) timelong" +
                                       " ,mi.JoinUser,mi.TypeID " +
                                   " from " +
                                       " officedba.MeetingInfo mi " +
                                   " where mi.CompanyCD='" + CompanyCD + "'";
                if (BeginDate != "")
                    sql += " and mi.StartDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and mi.StartDate <= '" + EndDate + "'";

                sql += " group by mi.JoinUser,mi.TypeID) a" +                      
                       " left join officedba.CodePublicType cp on cp.id = a.TypeID where 1=1 ";
                if (JoinUser != "")
                {
                    string[] UserList = JoinUser.Split(',');

                    for (int j = 0; j < UserList.Length; j++)
                    {
                        //获取参与人ID
                        int UserID = Convert.ToInt32(UserList[j]);
                        sql += " and a.joinuser+',' like '%" + UserID + ",%'";
                    }
                }               
                if (TypeID != "0")
                    sql += " and a.TypeID = '" + TypeID + "'";

                sql += " union " +
                        "select b.JoinUser,isnull(cpr.TypeName,'') TypeName," +
                                   " b.num,convert(varchar(100),b.timelong )timelong" +
                               " from " +
                                   " (select " +
                                       " count(*) num,sum(mir.TimeLong) timelong" +
                                       " ,mir.JoinUser,mir.TypeID	" +
                                   " from " +
                                       " officedba.MeetingRecord mir " +
                                   " where mir.MeetingNo = '' and mir.CompanyCD='" + CompanyCD + "'";
                if (BeginDate != "")
                    sql += " and mir.StartDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and mir.StartDate <= '" + EndDate + "'";

                sql += " group by mir.JoinUser,mir.TypeID) b" +                       
                       " left join officedba.CodePublicType cpr on cpr.id = b.TypeID where 1=1 ";
                if (JoinUser != "")
                {
                    string[] UserList2 = JoinUser.Split(',');

                    for (int j = 0; j < UserList2.Length; j++)
                    {
                        //获取参与人ID
                        int UserID2 = Convert.ToInt32(UserList2[j]);
                        sql += " and b.joinuser+',' like '%" + UserID2 + ",%'";
                    }
                } 
                if (TypeID != "0")
                    sql += " and b.TypeID = '" + TypeID + "'";


                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 人员参会状况统计打印
        /// <summary>
        /// 人员参会状况统计打印
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <param name="JoinUser"></param>
       /// <param name="TypeID"></param>
       /// <param name="BeginDate"></param>
       /// <param name="EndDate"></param>
       /// <param name="ord"></param>
       /// <returns></returns>
        public static DataTable GetMeetingJoinUserPrint(string CompanyCD, string JoinUser, string TypeID, string BeginDate, string EndDate,string ord)
        {
            try
            {
                string sql = "select a.JoinUser,isnull(cp.TypeName,'') TypeName," +
                                   " a.num,convert(varchar(100),a.timelong )timelong" +
                               " from " +
                                   " (select " +
                                       " count(*) num,sum(mi.TimeLong) timelong" +
                                       " ,mi.JoinUser,mi.TypeID " +
                                   " from " +
                                       " officedba.MeetingInfo mi " +
                                   " where mi.CompanyCD='" + CompanyCD + "'";
                if (BeginDate != "")
                    sql += "and mi.StartDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += "and mi.StartDate <= '" + EndDate + "'";

                sql += " group by mi.JoinUser,mi.TypeID) a" +
                       " left join officedba.CodePublicType cp on cp.id = a.TypeID";
                if (JoinUser != "")
                {
                    string[] UserList = JoinUser.Split(',');

                    for (int j = 0; j < UserList.Length; j++)
                    {
                        //获取参与人ID
                        int UserID = Convert.ToInt32(UserList[j]);
                        sql += "and b.joinuser+',' like '%" + UserID + ",%'";
                    }
                }
                if (TypeID != "0")
                    sql += "and a.TypeID = '" + TypeID + "'";

                sql += " union " +
                        "select b.JoinUser,isnull(cpr.TypeName,'') TypeName," +
                                   " b.num,convert(varchar(100),b.timelong )timelong" +
                               " from " +
                                   " (select " +
                                       " count(*) num,sum(mir.TimeLong) timelong" +
                                       " ,mir.JoinUser,mir.TypeID	" +
                                   " from " +
                                       " officedba.MeetingRecord mir " +
                                   " where mir.MeetingNo = '' and mir.CompanyCD='" + CompanyCD + "'";
                if (BeginDate != "")
                    sql += "and mir.StartDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += "and mir.StartDate <= '" + EndDate + "'";

                sql += " group by mir.JoinUser,mir.TypeID) b" +
                       " left join officedba.CodePublicType cpr on cpr.id = b.TypeID";
                if (JoinUser != "")
                {
                    string[] UserList2 = JoinUser.Split(',');

                    for (int j = 0; j < UserList2.Length; j++)
                    {
                        //获取参与人ID
                        int UserID2 = Convert.ToInt32(UserList2[j]);
                        sql += "and b.joinuser+',' like '%" + UserID2 + ",%'";
                    }
                }
                if (TypeID != "0")
                    sql += "and b.TypeID = '" + TypeID + "'";

                sql = sql + ord;

                return SqlHelper.ExecuteSql(sql);
                
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 会议决议一览
        /// <summary>
        /// 会议决议一览
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="TypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetMeetingDecisionList(string CompanyCD, string TypeID, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "select md.id, md.Contents " +
                                ",eip.EmployeeName PriName " +
                                ",md.Aim" +
                                ",Convert(varchar(20),md.CompleteDate,23) CompleteDate" +
                                ",(case md.Status when '1' then '未完成' when '2' then '已完成' end) Status	" +
                                ",isnull(ei.EmployeeName,'') EmployeeName" +
                                ",Convert(varchar(20),md.CheckDate,23) CheckDate" +
                                ",md.CheckResult" +
                           " from " +
                               " officedba.MeetingDecision md" +
                           " left join officedba.EmployeeInfo eip on eip.id = md.Principal" +
                           " left join officedba.EmployeeInfo ei on ei.id = md.Cheker" +
                           " left join officedba.MeetingRecord mr on mr.RecordNo = md.RecordNo" +
                           " where md.CompanyCD = '" + CompanyCD + "'";
               
                if (TypeID != "0")
                    sql += " and mr.TypeID = '" + TypeID + "'";               
                if (BeginDate != "")
                    sql += " and mr.StartDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and mr.StartDate <= '" + EndDate + "'";            

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);                
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 会议决议一览打印
        /// <summary>
        /// 会议决议一览打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="TypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetMeetingDecisionPrint(string CompanyCD, string TypeID, string BeginDate, string EndDate,string ord)
        {
            try
            {
                string sql = "select md.id, md.Contents " +
                                ",eip.EmployeeName PriName " +
                                ",md.Aim" +
                                ",Convert(varchar(20),md.CompleteDate,23) CompleteDate" +
                                ",(case md.Status when '1' then '未完成' when '2' then '已完成' end) Status	" +
                                ",isnull(ei.EmployeeName,'') EmployeeName" +
                                ",Convert(varchar(20),md.CheckDate,23) CheckDate" +
                                ",md.CheckResult" +
                           " from " +
                               " officedba.MeetingDecision md" +
                           " left join officedba.EmployeeInfo eip on eip.id = md.Principal" +
                           " left join officedba.EmployeeInfo ei on ei.id = md.Cheker" +
                           " left join officedba.MeetingRecord mr on mr.RecordNo = md.RecordNo" +
                           " where md.CompanyCD = '" + CompanyCD + "'";

                if (TypeID != "0")
                    sql += " and mr.TypeID = '" + TypeID + "'";
                if (BeginDate != "")
                    sql += " and mr.StartDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and mr.StartDate <= '" + EndDate + "'";

                sql = sql + ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 导出会议记录列表
        /// <summary>
        /// 导出会议记录列表
        /// </summary>
        /// <param name="MeetingRecordM"></param>
        /// <param name="FileDateBegin"></param>
        /// <param name="FileDateEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportMeetingRecord(MeetingRecordModel MeetingRecordM, string FileDateBegin, string FileDateEnd, string ord)
        {
            try
            {
                #region sql语句
                string sql = "select mr.ID,mr.RecordNo,(CASE mr.MeetingNo WHEN '0' THEN '' ELSE mr.MeetingNo END) MeetingNo,mr.Title," +
                               " mr.DeptID,isnull(di.DeptName,'') DeptName," +
                               " mr.Chairman,isnull(eic.EmployeeName,'') ChairmanName," +
                               " CONVERT(varchar(100), mr.StartDate, 23)+' '+mr.StartTime StartDate,	" +
                               " mr.Place,mro.RoomName," +
                               " mr.Recorder,isnull(eir.EmployeeName,'') EmployeeName" +
                           " from officedba.MeetingRecord mr" +
                           " left join officedba.DeptInfo di on di.id = mr.DeptID" +
                           " left join officedba.EmployeeInfo eic on eic.id = mr.Chairman	" +
                           " left join officedba.MeetingRoom mro on mro.id = mr.Place" +
                           " left join officedba.EmployeeInfo eir on eir.id = mr.Recorder" +
                           " where" +
                                " mr.CompanyCD = '" + MeetingRecordM.CompanyCD + "'";

                if (MeetingRecordM.RecordNo != "")
                    sql += " and mr.RecordNo like '%" + MeetingRecordM.RecordNo + "%'";
                if (MeetingRecordM.MeetingNo != "0")
                    sql += " and mr.MeetingNo = '" + MeetingRecordM.MeetingNo + "'";
                if (MeetingRecordM.DeptID != 0)
                    sql += " and mr.DeptID = " + MeetingRecordM.DeptID + "";
                if (MeetingRecordM.Title != "")
                    sql += " and mr.Title like '%" + MeetingRecordM.Title + "%'";
                if (MeetingRecordM.Caller != 0)
                    sql += " and mr.Caller = " + MeetingRecordM.Caller + "";
                if (MeetingRecordM.Chairman != 0)
                    sql += " and mr.Chairman = " + MeetingRecordM.Chairman + "";
                if (MeetingRecordM.TypeID != 0)
                    sql += " and mr.TypeID = " + MeetingRecordM.TypeID + "";
                if (FileDateBegin != "")
                    sql += " and mr.StartDate >= '" + FileDateBegin.ToString() + "'";
                if (FileDateEnd != "")
                    sql += " and mr.StartDate <= '" + FileDateEnd.ToString() + "'";

                #endregion

                return SqlHelper.ExecuteSql(sql);               
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 导出会议决议列表
        /// <summary>
        /// 导出会议决议列表
        /// </summary>
        /// <param name="MeetingDecisionM"></param>
        /// <param name="FileDateBegin"></param>
        /// <param name="FileDateEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportMeetingDecision(MeetingRecordModel MeetingDecisionM, string FileDateBegin, string FileDateEnd, string ord)
        {
            try
            {
                #region sql语句
                string sql = "select md.ID,md.DecisionNo,md.Principal,isnull(mi.EmployeeName,'') PrincipalName," +
                                   " isnull(CONVERT(varchar(100), md.CompleteDate, 23),'') CompleteDate,(case md.Status when '1' then '未完成' when '2' then '已完成' end)Status," +
                                   " md.Cheker,isnull(mic.EmployeeName,'') ChekerName," +
                                   " isnull(CONVERT(varchar(100), md.CheckDate, 23),'') CheckDate,md.RecordNo" +
                           " from " +
                               " officedba.MeetingDecision md" +
                           " left join officedba.EmployeeInfo mi on mi.id = md.Principal" +
                           " left join officedba.EmployeeInfo mic on mic.id = md.Cheker" +
                           " left join officedba.MeetingRecord mr on mr.RecordNo = md.RecordNo" +
                           " where" +
                                " md.CompanyCD = '" + MeetingDecisionM.CompanyCD + "'";
                if (MeetingDecisionM.MeetingNo != "0")
                    sql += " and mr.MeetingNo = '" + MeetingDecisionM.MeetingNo + "'";
                if (MeetingDecisionM.RecordNo != "")
                    sql += " and md.RecordNo like '%" + MeetingDecisionM.RecordNo + "%'";
                if (MeetingDecisionM.DeptID != 0)
                    sql += " and mr.DeptID = " + MeetingDecisionM.DeptID + "";
                if (MeetingDecisionM.Title != "")
                    sql += " and mr.Title like '%" + MeetingDecisionM.Title + "%'";
                if (MeetingDecisionM.Caller != 0)
                    sql += " and mr.Caller = " + MeetingDecisionM.Caller + "";
                if (MeetingDecisionM.Chairman != 0)
                    sql += " and mr.Chairman = " + MeetingDecisionM.Chairman + "";
                if (MeetingDecisionM.TypeID != 0)
                    sql += " and mr.TypeID = " + MeetingDecisionM.TypeID + "";
                if (FileDateBegin != "")
                    sql += " and mr.StartDate >= '" + FileDateBegin.ToString() + "'";
                if (FileDateEnd != "")
                    sql += " and mr.StartDate <= '" + FileDateEnd.ToString() + "'";

                #endregion

                return SqlHelper.ExecuteSql(sql);                
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据编号获得会议记录详细信息_打印
        /// <summary>
        /// 根据编号获得会议记录详细信息_打印
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="MeetingRecordID">会议记录编号</param>
        /// <returns>会议记录信息</returns>
        public static DataTable GetMeetingRecByNO_Print(string CompanyCD, string MeetingRecordNO)
        {
            try
            {
                string sql = "select " +
                                    " mr.ID,mr.MeetingNo,isnull(mi.ID,'')  miID,mr.RecordNo,mr.Title,b.TypeName TypeID" +
                                    ",eic.EmployeeName Caller,di.DeptName DeptID" +
                                    ",eih.EmployeeName Chairman" +
                                    ",CONVERT(varchar(100),mr.StartDate, 23) StartDate,mr.StartTime" +
                                    ",mr.TimeLong,a.RoomName Place,mr.Topic,mr.Contents,mr.CanViewUser,mr.CanViewUserName,mr.Attachment" +
                                    ",mr.Remark,mr.JoinUser" +
                                    ",eir.EmployeeName Recorder" +
                                    ",CONVERT(varchar(100),mr.RecordDate, 23) RecordDate" +
                                    ",eict.EmployeeName Creator" +
                                    ",CONVERT(varchar(100),mr.CreateDate, 23) CreateDate" +
                                    ",eis.EmployeeName Sender" +
                                    ",CONVERT(varchar(100),mr.SendDate, 23) SendDate" +
                                    ",CONVERT(varchar(100),mr.ModifiedDate, 23) ModifiedDate" +
                                    ",mr.ModifiedUserID,mr.CanViewUserName" +
                               " from " +
                                   " officedba.MeetingRecord mr " +
                               " left join officedba.DeptInfo di on di.id = mr.DeptID" +
                               " left join officedba.MeetingInfo mi on mi.MeetingNo = mr.MeetingNo" +
                               " left join officedba.EmployeeInfo eic on eic.id = mr.Caller" +
                               " left join officedba.EmployeeInfo eih on eih.id = mr.Chairman" +                   
                               " left join officedba.EmployeeInfo eir on eir.id = mr.Recorder" +
                               " left join officedba.EmployeeInfo eict on eict.id = mr.Creator" +
                               " left join officedba.EmployeeInfo eis on eis.id = mr.Sender" +
                               " left join officedba.MeetingRoom a on a.id = mr.Place" +
                               " left join officedba.CodePublicType b on b.id = mr.TypeID" +
                               " where mr.RecordNo = @RecordNo " +
                               " and mr.CompanyCD = @CompanyCD";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@RecordNo", MeetingRecordNO);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据编号获得会议记录明细（发言记录详细信息）_打印
        /// <summary>
        /// 根据编号获得会议记录详细信息（发言记录详细信息）_打印
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="MeetingRecordID">会议记录编号</param>
        /// <returns>发言记录详细信息</returns>
        public static DataTable GetMeetingRecTalkByNO_Print(string CompanyCD, string MeetingRecordNO)
        {
            try
            {
                string sql = "select " +
                                    " b.EmployeeName Talker,a.Topic,a.Contents," +
    " (case a.Important when '1' then '不重要' when '2' then '普通' when '3' then '重要' else '关键' end) Important " +
                               " from " +
                                   " officedba.MeetingTalk a " +
                               " left join officedba.EmployeeInfo b on b.id = a.Talker" +                               
                               " where a.RecordNo = @RecordNo " +
                               " and a.CompanyCD = @CompanyCD";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@RecordNo", MeetingRecordNO);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据编号获得会议记录明细（会议决议记录）_打印
        /// <summary>
        /// 根据编号获得会议记录详细信息（会议决议记录）_打印
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="MeetingRecordID">会议记录编号</param>
        /// <returns>会议决议记录</returns>
        public static DataTable GetMeetingRecDecisionByNO_Print(string CompanyCD, string MeetingRecordNO)
        {
            try
            {
                string sql = "select " +
                                    " a.DecisionNo,a.Contents,b.EmployeeName Principal,a.Aim," +
                                    " CONVERT(varchar(100),a.CompleteDate, 23) CompleteDate,b.EmployeeName Cheker " +
                               " from " +
                                   " officedba.MeetingDecision a " +
                               " left join officedba.EmployeeInfo b on b.id = a.Principal" +
                                " left join officedba.EmployeeInfo c on c.id = a.Cheker" +
                               " where a.RecordNo = @RecordNo " +
                               " and a.CompanyCD = @CompanyCD";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@RecordNo", MeetingRecordNO);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion
    }
}
