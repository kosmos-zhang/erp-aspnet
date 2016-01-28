using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;

using XBase.Model.Common;
using XBase.Common;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;
using XBase.Business.Common;
using XBase.Data.Common;
using XBase.Data.DBHelper;

namespace XBase.Business.Office.AdminManager
{
    public class MeetingRecordBus
    {
        #region 添加会议记录、对应发言明细、对应决议明细的方法
        /// <summary>
        /// 添加会议记录、对应发言明细、对应决议明细的方法
        /// </summary>
        /// <param name="MeetingRecordM">会议记录</param>
        /// <param name="LinkTalk">会议发言信息</param>
        /// <param name="LinkDecision">会议决议信息</param>
        /// <returns></returns>
        public static string AddMeetingRecord(MeetingRecordModel MeetingRecordM, string LinkTalk, string LinkDecision)
        {
            #region 拼写添加会议记录SQL语句
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.MeetingRecord");
            sql.AppendLine("(CompanyCD     ");
            sql.AppendLine(",MeetingNo     ");
            sql.AppendLine(",RecordNo      ");
            sql.AppendLine(",Title         ");
            sql.AppendLine(",TypeID        ");
            sql.AppendLine(",Caller        ");
            sql.AppendLine(",DeptID        ");
            sql.AppendLine(",Chairman      ");
            sql.AppendLine(",StartDate     ");
            sql.AppendLine(",StartTime     ");
            sql.AppendLine(",TimeLong      ");
            sql.AppendLine(",Place         ");
            sql.AppendLine(",Topic         ");
            sql.AppendLine(",Contents      ");
            sql.AppendLine(",CanViewUserName      ");
            sql.AppendLine(",CanViewUser      ");
            sql.AppendLine(",Attachment    ");
            sql.AppendLine(",Remark        ");
            //sql.AppendLine(",MeetingStatus ");
            sql.AppendLine(",JoinUser      ");
            sql.AppendLine(",Recorder      ");
            sql.AppendLine(",RecordDate    ");
            sql.AppendLine(",Creator       ");
            sql.AppendLine(",CreateDate    ");
            sql.AppendLine(",Sender        ");
            sql.AppendLine(",SendDate      ");
            sql.AppendLine(",ModifiedDate  ");
            sql.AppendLine(",ModifiedUserID)");
            sql.AppendLine(" values ");
            sql.AppendLine("(@CompanyCD     ");
            sql.AppendLine(",@MeetingNo     ");
            sql.AppendLine(",@RecordNo      ");
            sql.AppendLine(",@Title         ");
            sql.AppendLine(",@TypeID        ");
            sql.AppendLine(",@Caller        ");
            sql.AppendLine(",@DeptID        ");
            sql.AppendLine(",@Chairman      ");
            sql.AppendLine(",@StartDate     ");
            sql.AppendLine(",@StartTime     ");
            sql.AppendLine(",@TimeLong      ");
            sql.AppendLine(",@Place         ");
            sql.AppendLine(",@Topic         ");
            sql.AppendLine(",@Contents      ");
            sql.AppendLine(",@CanViewUserName      ");
            sql.AppendLine(",@CanViewUser      ");
            sql.AppendLine(",@Attachment    ");
            sql.AppendLine(",@Remark        ");
            //sql.AppendLine(",@MeetingStatus ");
            sql.AppendLine(",@JoinUser      ");
            sql.AppendLine(",@Recorder      ");
            sql.AppendLine(",@RecordDate    ");
            sql.AppendLine(",@Creator       ");
            sql.AppendLine(",@CreateDate    ");
            sql.AppendLine(",@Sender        ");
            sql.AppendLine(",@SendDate      ");
            sql.AppendLine(",@ModifiedDate  ");
            sql.AppendLine(",@ModifiedUserID)");
            #endregion

            #region 设置添加会议记录参数
            SqlParameter[] param = new SqlParameter[27];
            param[0] = SqlHelper.GetParameter("@CompanyCD", MeetingRecordM.CompanyCD);
            param[1] = SqlHelper.GetParameter("@MeetingNo", MeetingRecordM.MeetingNo);
            param[2] = SqlHelper.GetParameter("@RecordNo", MeetingRecordM.RecordNo);
            param[3] = SqlHelper.GetParameter("@Title", MeetingRecordM.Title);
            param[4] = SqlHelper.GetParameter("@TypeID", MeetingRecordM.TypeID);
            param[5] = SqlHelper.GetParameter("@Caller", MeetingRecordM.Caller);
            param[6] = SqlHelper.GetParameter("@DeptID", MeetingRecordM.DeptID);
            param[7] = SqlHelper.GetParameter("@Chairman", MeetingRecordM.Chairman);
            param[8] = SqlHelper.GetParameter("@StartDate", MeetingRecordM.StartDate == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(MeetingRecordM.StartDate.ToString()));
            param[9] = SqlHelper.GetParameter("@StartTime", MeetingRecordM.StartTime);
            param[10] = SqlHelper.GetParameter("@TimeLong", MeetingRecordM.TimeLong);
            param[11] = SqlHelper.GetParameter("@Place", MeetingRecordM.Place);
            param[12] = SqlHelper.GetParameter("@Topic", MeetingRecordM.Topic);
            param[13] = SqlHelper.GetParameter("@Contents", MeetingRecordM.Contents);
            param[14] = SqlHelper.GetParameter("@Attachment", MeetingRecordM.Attachment);
            param[15] = SqlHelper.GetParameter("@Remark", MeetingRecordM.Remark);
            //param[16] = SqlHelper.GetParameter("@MeetingStatus ", MeetingRecordM.MeetingStatus);
            param[16] = SqlHelper.GetParameter("@JoinUser", MeetingRecordM.JoinUser);
            param[17] = SqlHelper.GetParameter("@Recorder", MeetingRecordM.Recorder);
            param[18] = SqlHelper.GetParameter("@RecordDate", MeetingRecordM.RecordDate == null
                                      ? SqlDateTime.Null
                                      : SqlDateTime.Parse(MeetingRecordM.RecordDate.ToString()));
            param[19] = SqlHelper.GetParameter("@Creator", MeetingRecordM.Creator);
            param[20] = SqlHelper.GetParameter("@CreateDate", MeetingRecordM.CreateDate == null
                                     ? SqlDateTime.Null
                                     : SqlDateTime.Parse(MeetingRecordM.CreateDate.ToString()));
            param[21] = SqlHelper.GetParameter("@Sender", MeetingRecordM.Sender);
            param[22] = SqlHelper.GetParameter("@SendDate", MeetingRecordM.SendDate == null
                                     ? SqlDateTime.Null
                                     : SqlDateTime.Parse(MeetingRecordM.SendDate.ToString()));
            param[23] = SqlHelper.GetParameter("@ModifiedDate", MeetingRecordM.ModifiedDate);
            param[24] = SqlHelper.GetParameter("@ModifiedUserID", MeetingRecordM.ModifiedUserID);
            param[25] = SqlHelper.GetParameter("@CanViewUser", MeetingRecordM.CanViewUser);
            param[26] = SqlHelper.GetParameter("@CanViewUserName", MeetingRecordM.CanViewUserName);

            #endregion

            string[] strTalk = LinkTalk.Split('|'); //把发言列表流分隔成数组
            string[] strDecision = LinkDecision.Split('|'); //把决议列表流分隔成数组
            SqlCommand[] comms = new SqlCommand[Convert.ToInt32(strTalk.Length) + Convert.ToInt32(strDecision.Length) - 1]; //申明cmd数组  

            SqlCommand CmdRecord = new SqlCommand(sql.ToString());  //未执行的会议记录信息添加命令
            CmdRecord.Parameters.AddRange(param);
            comms[0] = CmdRecord; //把未执行的会议记录添加命令给cmd数组第一项

            #region 循环会议发言记录
            MeetingTalkModel MeetingTalkM = new MeetingTalkModel();

            string recorditems = "";
            string[] linkmanfield = null;

            for (int i = 1; i < strTalk.Length; i++) //循环数组
            {
                recorditems = strTalk[i].ToString();//取到每一条记录:[序号,发言人,主旨,发言要点,重要程度,备注]
                linkmanfield = recorditems.Split(','); //把每条记录分隔到字段

                string fieldxh = linkmanfield[0].ToString();//序号
                string fieldTalker = linkmanfield[1].ToString();//发言人
                string fieldTopic = linkmanfield[2].ToString();//主旨
                string fieldContents = linkmanfield[3].ToString();//发言要点
                string fieldImportant = linkmanfield[4].ToString();//重要程度
                string fieldRemark = linkmanfield[5].ToString();//备注

                MeetingTalkM.CompanyCD = MeetingRecordM.CompanyCD;
                MeetingTalkM.RecordNo = MeetingRecordM.RecordNo;
                MeetingTalkM.Talker = fieldTalker == "" ? 0 : Convert.ToInt32(fieldTalker);
                MeetingTalkM.Topic = fieldTopic;
                MeetingTalkM.Contents = fieldContents;
                MeetingTalkM.Important = fieldImportant;
                MeetingTalkM.Remark = fieldRemark;

                #region 拼写添加联系人信息sql语句
                StringBuilder sqllinkman = new StringBuilder();
                sqllinkman.AppendLine("INSERT INTO officedba.MeetingTalk");
                sqllinkman.AppendLine("(CompanyCD");
                sqllinkman.AppendLine(",RecordNo     ");
                sqllinkman.AppendLine(",Talker");
                sqllinkman.AppendLine(",Topic   ");
                sqllinkman.AppendLine(",Contents  ");
                sqllinkman.AppendLine(",Important    ");
                sqllinkman.AppendLine(",Remark)    ");
                sqllinkman.AppendLine(" values ");
                sqllinkman.AppendLine("(@CompanyCD");
                sqllinkman.AppendLine(",@RecordNo     ");
                sqllinkman.AppendLine(",@Talker");
                sqllinkman.AppendLine(",@Topic   ");
                sqllinkman.AppendLine(",@Contents  ");
                sqllinkman.AppendLine(",@Important    ");
                sqllinkman.AppendLine(",@Remark)   ");
                #endregion

                #region 设置参数
                SqlParameter[] paramlinkman = new SqlParameter[7];
                paramlinkman[0] = SqlHelper.GetParameter("@CompanyCD", MeetingTalkM.CompanyCD);
                paramlinkman[1] = SqlHelper.GetParameter("@RecordNo", MeetingTalkM.RecordNo);
                paramlinkman[2] = SqlHelper.GetParameter("@Talker", MeetingTalkM.Talker);
                paramlinkman[3] = SqlHelper.GetParameter("@Topic", MeetingTalkM.Topic);
                paramlinkman[4] = SqlHelper.GetParameter("@Contents", MeetingTalkM.Contents);
                paramlinkman[5] = SqlHelper.GetParameter("@Important", MeetingTalkM.Important);
                paramlinkman[6] = SqlHelper.GetParameter("@Remark", MeetingTalkM.Remark);
                #endregion

                SqlCommand cmdlinkman = new SqlCommand(sqllinkman.ToString());  //未执行的会议发言信息添加命令
                cmdlinkman.Parameters.AddRange(paramlinkman);
                comms[i] = cmdlinkman; //把未执行的会议发言信息添加命令给cmd数组
            }
            #endregion

            #region 循环会议决议记录
            MeetingDecisionModel MeetingDecisionM = new MeetingDecisionModel();

            string DecisionItems = "";
            string[] DecisionField = null;

            string DecisionList = "";

            int k = 1;
            for (int i = strTalk.Length; i < (strTalk.Length + strDecision.Length - 1); i++) //循环数组
            {
                DecisionItems = strDecision[k].ToString();//取到每一条记录:[序号,会议决议编号,决议事项,执行负责人,实施目标,完成期限,完成状态,检查人,核查时间,核查结果,备注,最后更新日期,最后更新用户ID]
                k++;
                DecisionField = DecisionItems.Split(','); //把每条记录分隔到字段

                string fieldxh = DecisionField[0].ToString();//序号

                string fieldDecisionNoType = DecisionField[1].ToString();//会议决议编号类型
                string fieldDecisionNo = DecisionField[2].ToString();//会议决议编号

                if (fieldDecisionNo != "")
                {
                    string tableName = "MeetingDecision";//会议决议记录表
                    string columnName = "DecisionNo";//会议决议编号
                    string codeValue = fieldDecisionNo;
                    bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq(tableName, columnName, codeValue);
                    if (!ishave)
                    { 
                        return "-1";
                    }
                }

                if (fieldDecisionNo == "" || fieldDecisionNo == string.Empty)
                {
                    fieldDecisionNo = ItemCodingRuleBus.GetCodeValue(fieldDecisionNoType);
                }
                DecisionList = DecisionList + fieldDecisionNo + ",";
                
                string fieldContents = DecisionField[3].ToString();//决议事项
                string fieldPrincipal = DecisionField[4].ToString();//执行负责人
                string fieldAim = DecisionField[5].ToString();//实施目标
                string fieldCompleteDate = DecisionField[6].ToString();//完成期限
                string fieldStatus = DecisionField[7].ToString();//完成状态
                string fieldCheker = DecisionField[8].ToString();//检查人
                string fieldCheckDate = DecisionField[9].ToString();//核查时间
                string fieldCheckResult = DecisionField[10].ToString();//核查结果
                string fieldRemark = DecisionField[11].ToString();//备注
                //string fieldModifiedDate = DecisionField[11].ToString();//最后更新日期
                //string fieldModifiedUserID = DecisionField[12].ToString();//最后更新用户ID                

                MeetingDecisionM.CompanyCD = MeetingRecordM.CompanyCD;
                MeetingDecisionM.RecordNo = MeetingRecordM.RecordNo;
                MeetingDecisionM.DecisionNo = fieldDecisionNo;
                MeetingDecisionM.Contents = fieldContents;
                MeetingDecisionM.Principal = fieldPrincipal == "" ? 0 : Convert.ToInt32(fieldPrincipal);
                MeetingDecisionM.Aim = fieldAim;
                if (fieldCompleteDate != "")
                    MeetingDecisionM.CompleteDate = Convert.ToDateTime(fieldCompleteDate);
                MeetingDecisionM.Status = fieldStatus;
                MeetingDecisionM.Cheker = fieldCheker == "" ? 0 : Convert.ToInt32(fieldCheker);
                if (fieldCheckDate != "")
                    MeetingDecisionM.CheckDate = Convert.ToDateTime(fieldCheckDate);
                MeetingDecisionM.CheckResult = fieldCheckResult;
                MeetingDecisionM.Remark = fieldRemark;

                MeetingDecisionM.ModifiedDate = MeetingRecordM.ModifiedDate;
                MeetingDecisionM.ModifiedUserID = MeetingRecordM.ModifiedUserID;

                #region 拼写添加会议决议信息sql语句
                StringBuilder sqlDecision = new StringBuilder();
                sqlDecision.AppendLine("INSERT INTO officedba.MeetingDecision");
                sqlDecision.AppendLine("(CompanyCD");
                sqlDecision.AppendLine(",RecordNo     ");
                sqlDecision.AppendLine(",DecisionNo");
                sqlDecision.AppendLine(",Contents   ");
                sqlDecision.AppendLine(",Principal  ");
                sqlDecision.AppendLine(",Aim    ");
                sqlDecision.AppendLine(",CompleteDate");
                sqlDecision.AppendLine(",Status     ");
                sqlDecision.AppendLine(",Cheker");
                sqlDecision.AppendLine(",CheckDate   ");
                sqlDecision.AppendLine(",CheckResult  ");
                sqlDecision.AppendLine(",Remark    ");
                sqlDecision.AppendLine(",ModifiedDate    ");
                sqlDecision.AppendLine(",ModifiedUserID)    ");
                sqlDecision.AppendLine(" values ");
                sqlDecision.AppendLine("(@CompanyCD");
                sqlDecision.AppendLine(",@RecordNo     ");
                sqlDecision.AppendLine(",@DecisionNo");
                sqlDecision.AppendLine(",@Contents   ");
                sqlDecision.AppendLine(",@Principal  ");
                sqlDecision.AppendLine(",@Aim    ");
                sqlDecision.AppendLine(",@CompleteDate");
                sqlDecision.AppendLine(",@Status     ");
                sqlDecision.AppendLine(",@Cheker");
                sqlDecision.AppendLine(",@CheckDate   ");
                sqlDecision.AppendLine(",@CheckResult  ");
                sqlDecision.AppendLine(",@Remark    ");
                sqlDecision.AppendLine(",@ModifiedDate    ");
                sqlDecision.AppendLine(",@ModifiedUserID)    ");
                #endregion

                #region 设置参数
                SqlParameter[] paramdecision = new SqlParameter[14];
                paramdecision[0] = SqlHelper.GetParameter("@CompanyCD", MeetingDecisionM.CompanyCD);
                paramdecision[1] = SqlHelper.GetParameter("@RecordNo", MeetingDecisionM.RecordNo);
                paramdecision[2] = SqlHelper.GetParameter("@DecisionNo", MeetingDecisionM.DecisionNo);
                paramdecision[3] = SqlHelper.GetParameter("@Contents", MeetingDecisionM.Contents);
                paramdecision[4] = SqlHelper.GetParameter("@Principal", MeetingDecisionM.Principal);
                paramdecision[5] = SqlHelper.GetParameter("@Aim", MeetingDecisionM.Aim);
                paramdecision[6] = SqlHelper.GetParameter("@CompleteDate", MeetingDecisionM.CompleteDate == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(MeetingDecisionM.CompleteDate.ToString()));
                paramdecision[7] = SqlHelper.GetParameter("@Status", MeetingDecisionM.Status);
                paramdecision[8] = SqlHelper.GetParameter("@Cheker", MeetingDecisionM.Cheker);
                paramdecision[9] = SqlHelper.GetParameter("@CheckDate", MeetingDecisionM.CheckDate == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(MeetingDecisionM.CheckDate.ToString()));
                paramdecision[10] = SqlHelper.GetParameter("@CheckResult", MeetingDecisionM.CheckResult);
                paramdecision[11] = SqlHelper.GetParameter("@Remark", MeetingDecisionM.Remark);
                paramdecision[12] = SqlHelper.GetParameter("@ModifiedDate", MeetingDecisionM.ModifiedDate == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(MeetingDecisionM.ModifiedDate.ToString()));
                paramdecision[13] = SqlHelper.GetParameter("@ModifiedUserID", MeetingDecisionM.ModifiedUserID);
                #endregion

                SqlCommand cmdDecision = new SqlCommand(sqlDecision.ToString());  //未执行的会议发言信息添加命令
                cmdDecision.Parameters.AddRange(paramdecision);
                comms[i] = cmdDecision; //把未执行的决议记录信息添加命令给cmd数组
            }
            #endregion

            if (MeetingRecordDBHelper.AddMeetingRecord(comms))
                return DecisionList;
            else
                return "faile";
        }
        #endregion

        #region 修改会议记录、对应发言明细、对应决议明细的方法
        /// <summary>
        /// 修改会议记录、对应发言明细、对应决议明细的方法
        /// </summary>
        /// <param name="MeetingRecordM"></param>
        /// <param name="LinkTalk"></param>
        /// <param name="LinkDecision"></param>
        /// <returns></returns>
        public static string UpdateMeetingRecord(MeetingRecordModel MeetingRecordM, string LinkTalk, string LinkDecision)
        {
            #region 拼写修改会议记录信息SQL语句
            StringBuilder SqlRecord = new StringBuilder();
            SqlRecord.AppendLine("UPDATE officedba.MeetingRecord set ");
            SqlRecord.AppendLine("CompanyCD     = @CompanyCD     ,");
            SqlRecord.AppendLine("MeetingNo     = @MeetingNo     ,");
            //SqlRecord.AppendLine("RecordNo      = @RecordNo      ,");
            SqlRecord.AppendLine("Title         = @Title         ,");
            SqlRecord.AppendLine("TypeID        = @TypeID        ,");
            SqlRecord.AppendLine("Caller        = @Caller        ,");
            SqlRecord.AppendLine("DeptID        = @DeptID        ,");
            SqlRecord.AppendLine("Chairman      = @Chairman      ,");
            SqlRecord.AppendLine("StartDate     = @StartDate     ,");
            SqlRecord.AppendLine("StartTime     = @StartTime     ,");
            SqlRecord.AppendLine("TimeLong      = @TimeLong      ,");
            SqlRecord.AppendLine("Place         = @Place         ,");
            SqlRecord.AppendLine("Topic         = @Topic         ,");
            SqlRecord.AppendLine("Contents      = @Contents      ,");
            SqlRecord.AppendLine("CanViewUser      = @CanViewUser      ,");
            SqlRecord.AppendLine("CanViewUserName      = @CanViewUserName      ,");
            SqlRecord.AppendLine("Attachment    = @Attachment    ,");
            SqlRecord.AppendLine("Remark        = @Remark        ,");
            //SqlRecord.AppendLine("MeetingStatus = @MeetingStatus ,");
            SqlRecord.AppendLine("JoinUser      = @JoinUser      ,");
            SqlRecord.AppendLine("Recorder      = @Recorder      ,");
            SqlRecord.AppendLine("RecordDate    = @RecordDate    ,");
            SqlRecord.AppendLine("Creator       = @Creator       ,");
            SqlRecord.AppendLine("CreateDate    = @CreateDate    ,");
            SqlRecord.AppendLine("Sender        = @Sender        ,");
            SqlRecord.AppendLine("SendDate      = @SendDate      ,");
            SqlRecord.AppendLine("ModifiedDate  = @ModifiedDate  ,");
            SqlRecord.AppendLine("ModifiedUserID= @ModifiedUserID");
            SqlRecord.AppendLine(" WHERE ");
            SqlRecord.AppendLine("RecordNo = @RecordNo ");
            #endregion

            #region 设置修改会议记录信息参数
            SqlParameter[] param = new SqlParameter[27];
            param[0] = SqlHelper.GetParameter("@CompanyCD     ",MeetingRecordM.CompanyCD     );
            param[1] = SqlHelper.GetParameter("@MeetingNo     ",MeetingRecordM.MeetingNo     );
            param[2] = SqlHelper.GetParameter("@RecordNo      ",MeetingRecordM.RecordNo      );
            param[3] = SqlHelper.GetParameter("@Title         ",MeetingRecordM.Title         );
            param[4] = SqlHelper.GetParameter("@TypeID        ",MeetingRecordM.TypeID        );
            param[5] = SqlHelper.GetParameter("@Caller        ",MeetingRecordM.Caller        );
            param[6] = SqlHelper.GetParameter("@DeptID        ",MeetingRecordM.DeptID        );
            param[7] = SqlHelper.GetParameter("@Chairman      ",MeetingRecordM.Chairman      );
            param[8] = SqlHelper.GetParameter("@StartDate", MeetingRecordM.StartDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(MeetingRecordM.StartDate.ToString()));
            param[9] = SqlHelper.GetParameter("@StartTime     ",MeetingRecordM.StartTime     );
            param[10] = SqlHelper.GetParameter("@TimeLong      ",MeetingRecordM.TimeLong      );
            param[11] = SqlHelper.GetParameter("@Place         ",MeetingRecordM.Place         );
            param[12] = SqlHelper.GetParameter("@Topic         ",MeetingRecordM.Topic         );
            param[13] = SqlHelper.GetParameter("@Contents      ",MeetingRecordM.Contents      );
            param[14] = SqlHelper.GetParameter("@Attachment    ",MeetingRecordM.Attachment    );
            param[15] = SqlHelper.GetParameter("@Remark        ",MeetingRecordM.Remark        );
            //param[16] = SqlHelper.GetParameter("@MeetingStatus ",MeetingRecordM.MeetingStatus );
            param[16] = SqlHelper.GetParameter("@JoinUser      ",MeetingRecordM.JoinUser      );
            param[17] = SqlHelper.GetParameter("@Recorder      ",MeetingRecordM.Recorder      );
            param[18] = SqlHelper.GetParameter("@RecordDate", MeetingRecordM.RecordDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(MeetingRecordM.RecordDate.ToString()));
            param[19] = SqlHelper.GetParameter("@Creator       ",MeetingRecordM.Creator       );
            param[20] = SqlHelper.GetParameter("@CreateDate", MeetingRecordM.CreateDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(MeetingRecordM.CreateDate.ToString()));
            param[21] = SqlHelper.GetParameter("@Sender        ",MeetingRecordM.Sender        );           
            param[22] = SqlHelper.GetParameter("@SendDate", MeetingRecordM.SendDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(MeetingRecordM.SendDate.ToString()));
            param[23] = SqlHelper.GetParameter("@ModifiedDate", MeetingRecordM.ModifiedDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(MeetingRecordM.ModifiedDate.ToString()));
            param[24] = SqlHelper.GetParameter("@ModifiedUserID",MeetingRecordM.ModifiedUserID);
            param[25] = SqlHelper.GetParameter("@CanViewUser", MeetingRecordM.CanViewUser);
            param[26] = SqlHelper.GetParameter("@CanViewUserName", MeetingRecordM.CanViewUserName);
            #endregion

            SqlCommand CmdRecord = new SqlCommand(SqlRecord.ToString());  //未执行的会议记录信息添加命令
            CmdRecord.Parameters.AddRange(param);
            //SqlCommand[] comms = new SqlCommand[Convert.ToInt32(strTalk.Length) + Convert.ToInt32(strDecision.Length) + 1]; //申明cmd数组  
            ArrayList comms = new ArrayList();
            
            comms.Add(CmdRecord);

            string[] strTalk = LinkTalk.Split('|'); //把发言列表流分隔成数组
            string[] strDecision = LinkDecision.Split('|'); //把决议列表流分隔成数组

            #region 循环会议发言记录

            if (LinkTalk != "")
            {
                SqlCommand CmdTalkDelete = new SqlCommand("delete officedba.MeetingTalk where RecordNo = '" + MeetingRecordM.RecordNo + "'");
                comms.Add(CmdTalkDelete);

                MeetingTalkModel MeetingTalkM = new MeetingTalkModel();
                string recorditems = "";
                string[] linkmanfield = null;

                for (int i = 1; i < strTalk.Length; i++) //循环数组
                {
                    recorditems = strTalk[i].ToString();//取到每一条记录:[序号,发言人,主旨,发言要点,重要程度,备注]
                    linkmanfield = recorditems.Split(','); //把每条记录分隔到字段

                    string fieldxh = linkmanfield[0].ToString();//序号
                    string fieldTalker = linkmanfield[1].ToString();//发言人
                    string fieldTopic = linkmanfield[2].ToString();//主旨
                    string fieldContents = linkmanfield[3].ToString();//发言要点
                    string fieldImportant = linkmanfield[4].ToString();//重要程度
                    string fieldRemark = linkmanfield[5].ToString();//备注

                    MeetingTalkM.CompanyCD = MeetingRecordM.CompanyCD;
                    MeetingTalkM.RecordNo = MeetingRecordM.RecordNo;
                    MeetingTalkM.Talker = fieldTalker == "" ? 0 : Convert.ToInt32(fieldTalker);
                    MeetingTalkM.Topic = fieldTopic;
                    MeetingTalkM.Contents = fieldContents;
                    MeetingTalkM.Important = fieldImportant;
                    MeetingTalkM.Remark = fieldRemark;

                    #region 拼写添加联系人信息sql语句
                    StringBuilder sqllinkman = new StringBuilder();
                    sqllinkman.AppendLine("INSERT INTO officedba.MeetingTalk");
                    sqllinkman.AppendLine("(CompanyCD");
                    sqllinkman.AppendLine(",RecordNo     ");
                    sqllinkman.AppendLine(",Talker");
                    sqllinkman.AppendLine(",Topic   ");
                    sqllinkman.AppendLine(",Contents  ");
                    sqllinkman.AppendLine(",Important    ");
                    sqllinkman.AppendLine(",Remark)    ");
                    sqllinkman.AppendLine(" values ");
                    sqllinkman.AppendLine("(@CompanyCD");
                    sqllinkman.AppendLine(",@RecordNo     ");
                    sqllinkman.AppendLine(",@Talker");
                    sqllinkman.AppendLine(",@Topic   ");
                    sqllinkman.AppendLine(",@Contents  ");
                    sqllinkman.AppendLine(",@Important    ");
                    sqllinkman.AppendLine(",@Remark)   ");
                    #endregion

                    #region 设置参数
                    SqlParameter[] paramlinkman = new SqlParameter[7];
                    paramlinkman[0] = SqlHelper.GetParameter("@CompanyCD", MeetingTalkM.CompanyCD);
                    paramlinkman[1] = SqlHelper.GetParameter("@RecordNo", MeetingTalkM.RecordNo);
                    paramlinkman[2] = SqlHelper.GetParameter("@Talker", MeetingTalkM.Talker);
                    paramlinkman[3] = SqlHelper.GetParameter("@Topic", MeetingTalkM.Topic);
                    paramlinkman[4] = SqlHelper.GetParameter("@Contents", MeetingTalkM.Contents);
                    paramlinkman[5] = SqlHelper.GetParameter("@Important", MeetingTalkM.Important);
                    paramlinkman[6] = SqlHelper.GetParameter("@Remark", MeetingTalkM.Remark);
                    #endregion

                    SqlCommand cmdlinkman = new SqlCommand(sqllinkman.ToString());  //未执行的会议发言信息添加命令
                    cmdlinkman.Parameters.AddRange(paramlinkman);
                    comms.Add(cmdlinkman);//把未执行的会议发言信息添加命令给cmd数组
                }
            }
            #endregion

            #region 循环会议决议记录

            SqlCommand CmdDecisionDelete = new SqlCommand("delete officedba.MeetingDecision where RecordNo = '" + MeetingRecordM.RecordNo + "'");
            comms.Add(CmdDecisionDelete);

            string DecisionList = "";

            if (LinkDecision != "")
            {                            
                MeetingDecisionModel MeetingDecisionM = new MeetingDecisionModel();

                string DecisionItems = "";
                string[] DecisionField = null;                

                int k = 1;
                for (int i = strTalk.Length; i < (strTalk.Length + strDecision.Length - 1); i++) //循环数组
                {
                    DecisionItems = strDecision[k].ToString();//取到每一条记录:[序号,会议决议编号,决议事项,执行负责人,实施目标,完成期限,完成状态,检查人,核查时间,核查结果,备注,最后更新日期,最后更新用户ID]
                    k++;
                    DecisionField = DecisionItems.Split(','); //把每条记录分隔到字段

                    string fieldxh = DecisionField[0].ToString();//序号
                    string fieldDecisionNoType = DecisionField[1].ToString();//会议决议编号类型
                    string fieldDecisionNo = DecisionField[2].ToString();//会议决议编号


                    if (fieldDecisionNo == "" || fieldDecisionNo == string.Empty)
                    {
                        fieldDecisionNo = ItemCodingRuleBus.GetCodeValue(fieldDecisionNoType);

                        for (int z = 1; z < strDecision.Length;z++ )
                        {
                            string OldNo = strDecision[z].Split(',')[2].ToString();
                            if (fieldDecisionNo == OldNo)
                            {                                
                                fieldDecisionNo = GetMeetDescNo(fieldDecisionNoType, OldNo);
                            }
                        }

                    }
                    

                    DecisionList = DecisionList + fieldDecisionNo + ",";
                    
                    
                    string fieldContents = DecisionField[3].ToString();//决议事项
                    string fieldPrincipal = DecisionField[4].ToString();//执行负责人
                    string fieldAim = DecisionField[5].ToString();//实施目标
                    string fieldCompleteDate = DecisionField[6].ToString();//完成期限
                    string fieldStatus = DecisionField[7].ToString();//完成状态
                    string fieldCheker = DecisionField[8].ToString();//检查人
                    string fieldCheckDate = DecisionField[9].ToString();//核查时间
                    string fieldCheckResult = DecisionField[10].ToString();//核查结果
                    string fieldRemark = DecisionField[11].ToString();//备注
                    //string fieldModifiedDate = DecisionField[11].ToString();//最后更新日期
                    //string fieldModifiedUserID = DecisionField[12].ToString();//最后更新用户ID                

                    MeetingDecisionM.CompanyCD = MeetingRecordM.CompanyCD;
                    MeetingDecisionM.RecordNo = MeetingRecordM.RecordNo;
                    MeetingDecisionM.DecisionNo = fieldDecisionNo;
                    MeetingDecisionM.Contents = fieldContents;
                    MeetingDecisionM.Principal = fieldPrincipal == "" ? 0 : Convert.ToInt32(fieldPrincipal);
                    MeetingDecisionM.Aim = fieldAim;
                    if (fieldCompleteDate != "")
                        MeetingDecisionM.CompleteDate = Convert.ToDateTime(fieldCompleteDate);
                    MeetingDecisionM.Status = fieldStatus;
                    MeetingDecisionM.Cheker = fieldCheker == "" ? 0 : Convert.ToInt32(fieldCheker);
                    if (fieldCheckDate != "")
                        MeetingDecisionM.CheckDate = Convert.ToDateTime(fieldCheckDate);
                    MeetingDecisionM.CheckResult = fieldCheckResult;
                    MeetingDecisionM.Remark = fieldRemark;

                    MeetingDecisionM.ModifiedDate = MeetingRecordM.ModifiedDate;
                    MeetingDecisionM.ModifiedUserID = MeetingRecordM.ModifiedUserID;

                    #region 拼写添加会议决议信息sql语句
                    StringBuilder sqlDecision = new StringBuilder();
                    sqlDecision.AppendLine("INSERT INTO officedba.MeetingDecision");
                    sqlDecision.AppendLine("(CompanyCD");
                    sqlDecision.AppendLine(",RecordNo     ");
                    sqlDecision.AppendLine(",DecisionNo");
                    sqlDecision.AppendLine(",Contents   ");
                    sqlDecision.AppendLine(",Principal  ");
                    sqlDecision.AppendLine(",Aim    ");
                    sqlDecision.AppendLine(",CompleteDate");
                    sqlDecision.AppendLine(",Status     ");
                    sqlDecision.AppendLine(",Cheker");
                    sqlDecision.AppendLine(",CheckDate   ");
                    sqlDecision.AppendLine(",CheckResult  ");
                    sqlDecision.AppendLine(",Remark    ");
                    sqlDecision.AppendLine(",ModifiedDate    ");
                    sqlDecision.AppendLine(",ModifiedUserID)    ");
                    sqlDecision.AppendLine(" values ");
                    sqlDecision.AppendLine("(@CompanyCD");
                    sqlDecision.AppendLine(",@RecordNo     ");
                    sqlDecision.AppendLine(",@DecisionNo");
                    sqlDecision.AppendLine(",@Contents   ");
                    sqlDecision.AppendLine(",@Principal  ");
                    sqlDecision.AppendLine(",@Aim    ");
                    sqlDecision.AppendLine(",@CompleteDate");
                    sqlDecision.AppendLine(",@Status     ");
                    sqlDecision.AppendLine(",@Cheker");
                    sqlDecision.AppendLine(",@CheckDate   ");
                    sqlDecision.AppendLine(",@CheckResult  ");
                    sqlDecision.AppendLine(",@Remark    ");
                    sqlDecision.AppendLine(",@ModifiedDate    ");
                    sqlDecision.AppendLine(",@ModifiedUserID)    ");
                    #endregion

                    #region 设置参数
                    SqlParameter[] paramdecision = new SqlParameter[14];
                    paramdecision[0] = SqlHelper.GetParameter("@CompanyCD", MeetingDecisionM.CompanyCD);
                    paramdecision[1] = SqlHelper.GetParameter("@RecordNo", MeetingDecisionM.RecordNo);
                    paramdecision[2] = SqlHelper.GetParameter("@DecisionNo", MeetingDecisionM.DecisionNo);
                    paramdecision[3] = SqlHelper.GetParameter("@Contents", MeetingDecisionM.Contents);
                    paramdecision[4] = SqlHelper.GetParameter("@Principal", MeetingDecisionM.Principal);
                    paramdecision[5] = SqlHelper.GetParameter("@Aim", MeetingDecisionM.Aim);
                    paramdecision[6] = SqlHelper.GetParameter("@CompleteDate", MeetingDecisionM.CompleteDate == null
                                           ? SqlDateTime.Null
                                           : SqlDateTime.Parse(MeetingDecisionM.CompleteDate.ToString()));
                    paramdecision[7] = SqlHelper.GetParameter("@Status", MeetingDecisionM.Status);
                    paramdecision[8] = SqlHelper.GetParameter("@Cheker", MeetingDecisionM.Cheker);
                    paramdecision[9] = SqlHelper.GetParameter("@CheckDate", MeetingDecisionM.CheckDate == null
                                           ? SqlDateTime.Null
                                           : SqlDateTime.Parse(MeetingDecisionM.CheckDate.ToString()));
                    paramdecision[10] = SqlHelper.GetParameter("@CheckResult", MeetingDecisionM.CheckResult);
                    paramdecision[11] = SqlHelper.GetParameter("@Remark", MeetingDecisionM.Remark);
                    paramdecision[12] = SqlHelper.GetParameter("@ModifiedDate", MeetingDecisionM.ModifiedDate == null
                                           ? SqlDateTime.Null
                                           : SqlDateTime.Parse(MeetingDecisionM.ModifiedDate.ToString()));
                    paramdecision[13] = SqlHelper.GetParameter("@ModifiedUserID", MeetingDecisionM.ModifiedUserID);
                    #endregion

                    SqlCommand cmdDecision = new SqlCommand(sqlDecision.ToString());  //未执行的会议发言信息添加命令
                    cmdDecision.Parameters.AddRange(paramdecision);
                    comms.Add(cmdDecision); //把未执行的决议记录信息添加命令给cmd数组
                }
            }
            #endregion

            if (MeetingRecordDBHelper.UpdateMeetingRecord(comms))
                return DecisionList;
            else
                return "faile";
        }
        #endregion

        #region 根据条件检索会议记录的方法
        /// <summary>
        /// 根据条件检索会议记录
        /// </summary>
        /// <param name="MeetingInfoM">会议记录信息</param>
        /// <param name="FileDateBegin">开始时间</param>
        /// <param name="FileDateEnd">结束时间</param>        
        /// <returns>会议列表</returns>
        public static DataTable GetMeetingRecordBycondition(string CanUserID, MeetingRecordModel MeetingRecordM, string FileDateBegin, string FileDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return MeetingRecordDBHelper.GetMeetingRecordBycondition(CanUserID,MeetingRecordM, FileDateBegin, FileDateEnd, pageIndex, pageCount, ord, ref TotalCount);
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
           return MeetingRecordDBHelper.DelMeetingRecord(RecordNO);
        }
        #endregion

        #region 根据ID获得会议记录详细信息
        /// <summary>
        /// 根据ID获得会议记录详细信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="MeetingRecordID">会议记录ID</param>
        /// <returns>会议记录信息</returns>
        public static DataTable GetMeetingRecordByNO(string CompanyCD, string MeetingRecordNO)
        {
           // return MeetingRecordDBHelper.GetMeetingRecordByNO(CompanyCD, MeetingRecordNO);
            string MainIds = "";//每条记录多个ID
            string EmployeeNames = "";//多个员工姓名

            DataTable dt = MeetingRecordDBHelper.GetMeetingRecordByNO(CompanyCD, MeetingRecordNO);

            DataColumn JoinName = new DataColumn();
            dt.Columns.Add("JoinUserName");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MainIds = dt.Rows[i]["JoinUser"].ToString();

                string[] MainList = MainIds.Split(',');

                for (int j = 0; j < MainList.Length; j++)
                {
                    if (MainList[j] != "")
                    {
                        //获取参与人ID
                        int inputID = Convert.ToInt32(MainList[j]);
                        //调用方法取name
                        EmployeeNames = EmployeeNames + "," + EmployeeDBHelper.GetEmployeeNameByID(inputID, CompanyCD);
                    }                   
                }

                //插入EmployeeNames到一条记录
                dt.Rows[i]["JoinUserName"] = EmployeeNames.Substring(1);
                EmployeeNames = "";
            }

            return dt;
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
            return MeetingRecordDBHelper.GetMeetingTalkByRecordNo(CompanyCD, MeetingRecordNO);
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
            return MeetingRecordDBHelper.GetMeetingDecisionByRecordNo(CompanyCD, MeetingRecordNO);
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
            return MeetingRecordDBHelper.GetMeetingDecisionBycondition(MeetingDecisionM, FileDateBegin, FileDateEnd, pageIndex, pageCount, ord, ref TotalCount);
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
            return MeetingRecordDBHelper.GetMeetingDecisionByID(CompanyCD, DecisionID);
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
            return MeetingRecordDBHelper.UpdateMeetingDecisionByID(MeetingDecisionM);
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
            return MeetingRecordDBHelper.GetMeetingRecordList(CompanyCD, DeptID,TypeID,BeginDate, EndDate, pageIndex, pageCount, ord, ref TotalCount);
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
        public static DataTable GetMeetingRecordListPrint(string CompanyCD, string DeptID, string TypeID,string BeginDate, string EndDate, string ord)
        {
            return MeetingRecordDBHelper.GetMeetingRecordListPrint(CompanyCD, DeptID, TypeID, BeginDate, EndDate, ord);
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
            return MeetingRecordDBHelper.GetMeetingCount(CompanyCD, DeptID, TypeID, BeginDate, EndDate, pageIndex, pageCount, ord, ref TotalCount);
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
        public static DataTable GetMeetingCountPrint(string CompanyCD, string DeptID, string TypeID, string BeginDate, string EndDate, string ord)
        {
            return MeetingRecordDBHelper.GetMeetingCountPrint(CompanyCD, DeptID, TypeID, BeginDate, EndDate, ord);
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
            DataTable dt = MeetingRecordDBHelper.GetMeetingJoinUser(CompanyCD, JoinUser, TypeID, BeginDate, EndDate, pageIndex, pageCount, ord, ref TotalCount);

            string MainIds = "";//每条记录多个ID
            string EmployeeNames = "";//多个员工姓名

            DataColumn JoinUserName = new DataColumn();
            dt.Columns.Add("JoinUserName");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MainIds = dt.Rows[i]["JoinUser"].ToString();

                string[] MainList = MainIds.Split(',');

                for (int j = 0; j < MainList.Length; j++)
                {
                    //获取参与人ID
                    int inputID = Convert.ToInt32(MainList[j]);
                    //调用方法取name
                    EmployeeNames = EmployeeNames + "," + EmployeeDBHelper.GetEmployeeNameByID(inputID, CompanyCD);
                }

                //插入EmployeeNames到一条记录
                dt.Rows[i]["JoinUserName"] = EmployeeNames.Substring(1);
                EmployeeNames = "";
            }

            return dt;
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
        public static DataTable GetMeetingJoinUserPrint(string CompanyCD, string JoinUser, string TypeID, string BeginDate, string EndDate, string ord)
        {
            DataTable dt = MeetingRecordDBHelper.GetMeetingJoinUserPrint(CompanyCD, JoinUser, TypeID, BeginDate, EndDate,ord);

            string MainIds = "";//每条记录多个ID
            string EmployeeNames = "";//多个员工姓名

            DataColumn JoinUserName = new DataColumn();
            dt.Columns.Add("JoinUserName");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MainIds = dt.Rows[i]["JoinUser"].ToString();

                string[] MainList = MainIds.Split(',');

                for (int j = 0; j < MainList.Length; j++)
                {
                    //获取参与人ID
                    int inputID = Convert.ToInt32(MainList[j]);
                    //调用方法取name
                    EmployeeNames = EmployeeNames + "," + EmployeeDBHelper.GetEmployeeNameByID(inputID, CompanyCD);
                }

                //插入EmployeeNames到一条记录
                dt.Rows[i]["JoinUserName"] = EmployeeNames.Substring(1);
                EmployeeNames = "";
            }

            return dt;
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
            return MeetingRecordDBHelper.GetMeetingDecisionList(CompanyCD, TypeID, BeginDate, EndDate, pageIndex, pageCount, ord, ref TotalCount);
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
        public static DataTable GetMeetingDecisionPrint(string CompanyCD, string TypeID, string BeginDate, string EndDate, string ord)
        {
            return MeetingRecordDBHelper.GetMeetingDecisionPrint(CompanyCD, TypeID, BeginDate, EndDate, ord);
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
            return MeetingRecordDBHelper.ExportMeetingRecord(MeetingRecordM, FileDateBegin, FileDateEnd, ord);
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
            return MeetingRecordDBHelper.ExportMeetingDecision(MeetingDecisionM, FileDateBegin, FileDateEnd, ord);
        }
        #endregion

        #region 递归生成会议决议编号
        public static string GetMeetDescNo(string NoType, string Nox)
        {
            string No = "";
            No = ItemCodingRuleBus.GetCodeValue(NoType);
            if(No == Nox)
            {
                No = GetMeetDescNo(NoType, Nox);
            }
            return No;
        }
        #endregion

        #region 根据ID获得会议记录详细信息_打印
        /// <summary>
        /// 根据ID获得会议记录详细信息_打印
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="MeetingRecordID">会议记录ID</param>
        /// <returns>会议记录信息</returns>
        public static DataTable GetMeetingRecByNO_Print(string CompanyCD, string MeetingRecordNO)
        {
            string MainIds = "";//每条记录多个ID
            string EmployeeNames = "";//多个员工姓名

            DataTable dt = MeetingRecordDBHelper.GetMeetingRecByNO_Print(CompanyCD, MeetingRecordNO);

            DataColumn JoinName = new DataColumn();
            dt.Columns.Add("JoinUserName");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MainIds = dt.Rows[i]["JoinUser"].ToString();

                string[] MainList = MainIds.Split(',');

                for (int j = 0; j < MainList.Length; j++)
                {
                    if (MainList[j] != "")
                    {
                        //获取参与人ID
                        int inputID = Convert.ToInt32(MainList[j]);
                        //调用方法取name
                        EmployeeNames = EmployeeNames + "," + EmployeeDBHelper.GetEmployeeNameByID(inputID, CompanyCD);
                    }
                }

                //插入EmployeeNames到一条记录
                dt.Rows[i]["JoinUserName"] = EmployeeNames.Substring(1);
                EmployeeNames = "";
            }

            return dt;
        }
        #endregion

        #region 根据编号获得会议记录明细（发言记录详细信息）_打印
        /// <summary>
        /// 根据编号获得会议记录详细信息（发言记录详细信息）_打印
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="MeetingRecordID">会议记录编号</param>
        /// <returns>会议记录信息</returns>
        public static DataTable GetMeetingRecTalkByNO_Print(string CompanyCD, string MeetingRecordNO)
        {
            return MeetingRecordDBHelper.GetMeetingRecTalkByNO_Print(CompanyCD, MeetingRecordNO);
        }
        #endregion

        #region 根据编号获得会议记录明细（会议决议记录）_打印
        /// <summary>
        /// 根据编号获得会议记录详细信息（发言记录详细信息）_打印
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="MeetingRecordID">会议记录编号</param>
        /// <returns>会议记录信息</returns>
        public static DataTable GetMeetingRecDecisionByNO_Print(string CompanyCD, string MeetingRecordNO)
        {
            return MeetingRecordDBHelper.GetMeetingRecDecisionByNO_Print(CompanyCD, MeetingRecordNO);
        }
        #endregion
    }
}
