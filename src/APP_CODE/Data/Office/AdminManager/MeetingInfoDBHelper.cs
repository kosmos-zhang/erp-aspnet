using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.AdminManager;
using System.Data.SqlTypes;

namespace XBase.Data.Office.AdminManager
{
    public class MeetingInfoDBHelper
    {
        #region 添加会议通知的方法
        /// <summary>
        /// 添加会议通知的方法
        /// </summary>
        /// <param name="MeetingInfoM">会议通知信息</param>
        /// <returns>被添加会议通知ID</returns>
        public static int MeetingInfoAdd(MeetingInfoModel MeetingInfoM)
        {
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[25];
                param[0] = SqlHelper.GetParameter("@CompanyCD     ",MeetingInfoM.CompanyCD     );
                param[1] = SqlHelper.GetParameter("@MeetingNo     ",MeetingInfoM.MeetingNo     );
                param[2] = SqlHelper.GetParameter("@Title         ",MeetingInfoM.Title         );
                param[3] = SqlHelper.GetParameter("@TypeID        ",MeetingInfoM.TypeID        );
                param[4] = SqlHelper.GetParameter("@Caller        ",MeetingInfoM.Caller        );
                param[5] = SqlHelper.GetParameter("@DeptID        ",MeetingInfoM.DeptID        );
                param[6] = SqlHelper.GetParameter("@Chairman      ",MeetingInfoM.Chairman      );
                param[7] = SqlHelper.GetParameter("@StartDate", MeetingInfoM.StartDate == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(MeetingInfoM.StartDate.ToString()));
                param[8] = SqlHelper.GetParameter("@StartTime     ",MeetingInfoM.StartTime     );
                param[9] = SqlHelper.GetParameter("@TimeLong      ",MeetingInfoM.TimeLong      );
                param[10] = SqlHelper.GetParameter("@Place         ",MeetingInfoM.Place         );
                param[11] = SqlHelper.GetParameter("@Topic         ",MeetingInfoM.Topic         );
                param[12] = SqlHelper.GetParameter("@Contents      ",MeetingInfoM.Contents      );
                param[13] = SqlHelper.GetParameter("@ReadyInfo     ",MeetingInfoM.ReadyInfo     );
                param[14] = SqlHelper.GetParameter("@Attachment    ",MeetingInfoM.Attachment    );
                param[15] = SqlHelper.GetParameter("@Remark        ",MeetingInfoM.Remark        );
                param[16] = SqlHelper.GetParameter("@MeetingStatus ",MeetingInfoM.MeetingStatus );
                param[17] = SqlHelper.GetParameter("@IsMobileCall  ",MeetingInfoM.IsMobileCall  );
                param[18] = SqlHelper.GetParameter("@CallContent   ",MeetingInfoM.CallContent   );
                param[19] = SqlHelper.GetParameter("@JoinUser      ",MeetingInfoM.JoinUser      );
                param[20] = SqlHelper.GetParameter("@Sender        ",MeetingInfoM.Sender        );                
                param[21] = SqlHelper.GetParameter("@SendDate", MeetingInfoM.SendDate == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(MeetingInfoM.SendDate.ToString()));
                param[22] = SqlHelper.GetParameter("@ModifiedDate  ",MeetingInfoM.ModifiedDate  );
                param[23] = SqlHelper.GetParameter("@ModifiedUserID",MeetingInfoM.ModifiedUserID);

                SqlParameter paramid = new SqlParameter("@id", SqlDbType.Int);
                paramid.Direction = ParameterDirection.Output;
                param[24] = paramid;
                #endregion

                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.insertMeetingInfo", comm, param);
                int MeetingInfoID = Convert.ToInt32(comm.Parameters["@id"].Value);

                return MeetingInfoID;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 根据会议通知ID修改会议通知信息
        /// <summary>
        /// 根据会议通知ID修改会议通知信息
        /// </summary>
        /// <param name="MeetingInfoM">会议通知信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateMeetingInfo(MeetingInfoModel MeetingInfoM)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.MeetingInfo set ");
                sql.AppendLine("CompanyCD     =@CompanyCD     ,");
                //sql.AppendLine("MeetingNo     =@MeetingNo     ,");
                sql.AppendLine("Title         =@Title         ,");
                sql.AppendLine("TypeID        =@TypeID        ,");
                sql.AppendLine("Caller        =@Caller        ,");
                sql.AppendLine("DeptID        =@DeptID        ,");
                sql.AppendLine("Chairman      =@Chairman      ,");
                sql.AppendLine("StartDate     =@StartDate     ,");
                sql.AppendLine("StartTime     =@StartTime     ,");
                sql.AppendLine("TimeLong      =@TimeLong      ,");
                sql.AppendLine("Place         =@Place         ,");
                sql.AppendLine("Topic         =@Topic         ,");
                sql.AppendLine("Contents      =@Contents      ,");
                sql.AppendLine("ReadyInfo     =@ReadyInfo     ,");
                sql.AppendLine("Attachment    =@Attachment    ,");
                sql.AppendLine("Remark        =@Remark        ,");
               
                sql.AppendLine("IsMobileCall  =@IsMobileCall  ,");
                sql.AppendLine("CallContent   =@CallContent   ,");
                sql.AppendLine("JoinUser      =@JoinUser      ,");
                sql.AppendLine("Sender        =@Sender        ,");
                sql.AppendLine("SendDate      =@SendDate      ,");
                sql.AppendLine("ModifiedDate  =@ModifiedDate  ,");
                sql.AppendLine("ModifiedUserID=@ModifiedUserID");
                sql.AppendLine(" WHERE ");
                sql.AppendLine("ID = @ID ");

                SqlParameter[] param = new SqlParameter[23];
                param[0] = SqlHelper.GetParameter("@ID      ", MeetingInfoM.ID);
                param[1] = SqlHelper.GetParameter("@CompanyCD     ",MeetingInfoM.CompanyCD     );                
                param[2] = SqlHelper.GetParameter("@Title         ",MeetingInfoM.Title         );
                param[3] = SqlHelper.GetParameter("@TypeID        ",MeetingInfoM.TypeID        );
                param[4] = SqlHelper.GetParameter("@Caller        ",MeetingInfoM.Caller        );
                param[5] = SqlHelper.GetParameter("@DeptID        ",MeetingInfoM.DeptID        );
                param[6] = SqlHelper.GetParameter("@Chairman      ",MeetingInfoM.Chairman      );
                //param[7] = SqlHelper.GetParameter("@StartDate     ",MeetingInfoM.StartDate     );
                param[7] = SqlHelper.GetParameter("@StartDate", MeetingInfoM.StartDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(MeetingInfoM.StartDate.ToString()));
                param[8] = SqlHelper.GetParameter("@StartTime     ",MeetingInfoM.StartTime     );
                param[9] = SqlHelper.GetParameter("@TimeLong      ",MeetingInfoM.TimeLong      );
                param[10] = SqlHelper.GetParameter("@Place         ",MeetingInfoM.Place         );
                param[11] = SqlHelper.GetParameter("@Topic         ",MeetingInfoM.Topic         );
                param[12] = SqlHelper.GetParameter("@Contents      ",MeetingInfoM.Contents      );
                param[13] = SqlHelper.GetParameter("@ReadyInfo     ",MeetingInfoM.ReadyInfo     );
                param[14] = SqlHelper.GetParameter("@Attachment    ",MeetingInfoM.Attachment    );
                param[15] = SqlHelper.GetParameter("@Remark        ",MeetingInfoM.Remark        );
              
                param[16] = SqlHelper.GetParameter("@IsMobileCall  ",MeetingInfoM.IsMobileCall  );
                param[17] = SqlHelper.GetParameter("@CallContent   ",MeetingInfoM.CallContent   );
                param[18] = SqlHelper.GetParameter("@JoinUser      ",MeetingInfoM.JoinUser      );
                param[19] = SqlHelper.GetParameter("@Sender        ",MeetingInfoM.Sender        );
                //param[21] = SqlHelper.GetParameter("@SendDate      ",MeetingInfoM.SendDate      );
                param[20] = SqlHelper.GetParameter("@SendDate", MeetingInfoM.SendDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(MeetingInfoM.SendDate.ToString()));
                param[21] = SqlHelper.GetParameter("@ModifiedDate  ",MeetingInfoM.ModifiedDate  );
                param[22] = SqlHelper.GetParameter("@ModifiedUserID",MeetingInfoM.ModifiedUserID);

                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion

        #region 根据条件检索会议信息
        /// <summary>
        /// 根据条件检索会议信息
        /// </summary>
        /// <param name="MeetingInfoM">会议信息</param>
        /// <param name="FileDateBegin">开始时间</param>
        /// <param name="FileDateEnd">结束时间</param>        
        /// <returns>会议列表信息</returns>
        public static DataTable GetMeetingInfoBycondition(MeetingInfoModel MeetingInfoM, string FileDateBegin, string FileDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select " +
                                   " mi.ID,mi.MeetingNo,mi.Title," +
                                   " mi.DeptID,isnull(di.DeptName,'') DeptName," +
                                   " mi.Caller,isnull(ei.EmployeeName,'') EmployeeName," +
                                   " CONVERT(varchar(100), mi.StartDate, 23)+' '+mi.StartTime StartDate," +
                                   " mi.Place,mr.RoomName," +
                                   " mi.MeetingStatus," +
                                   " mi.Sender,isnull(eis.EmployeeName,'') SenderName " +
                                   " ,isnull(mrc.MeetingNo,'') ishave" +
                               " from " +
                                   " officedba.MeetingInfo mi" +
                               " left join officedba.DeptInfo di on di.id = mi.DeptID" +
                               " left join officedba.EmployeeInfo ei on ei.id = mi.Caller" +
                               " left join officedba.MeetingRoom mr on mr.id = mi.Place" +
                               " left join officedba.EmployeeInfo eis on eis.id = mi.Sender" +
                               " left join officedba.MeetingRecord mrc on mrc.MeetingNo = mi.MeetingNo" +
                               " where" +
                                   " mi.CompanyCD = '" + MeetingInfoM.CompanyCD + "'";

                if (MeetingInfoM.MeetingNo != "")
                    sql += " and mi.MeetingNo like '%" + MeetingInfoM.MeetingNo + "%'";
                if (MeetingInfoM.MeetingStatus != "0")
                    sql += " and mi.MeetingStatus = '" + MeetingInfoM.MeetingStatus + "'";
                if (MeetingInfoM.Place != 0)
                    sql += " and mi.Place = " + MeetingInfoM.Place + "";
                if (MeetingInfoM.DeptID != 0)
                    sql += " and mi.DeptID = " + MeetingInfoM.DeptID + "";
                if (MeetingInfoM.Title != "")
                    sql += " and mi.Title like '%" + MeetingInfoM.Title + "%'";
                if (MeetingInfoM.Caller != 0)
                    sql += " and mi.Caller = " + MeetingInfoM.Caller + "";
                if (MeetingInfoM.Sender != 0)
                    sql += " and mi.Sender = " + MeetingInfoM.Sender + "";
                if (FileDateBegin != "")
                    sql += " and mi.StartDate >= '" + FileDateBegin.ToString() + "'";
                if (FileDateEnd != "")
                    sql += " and mi.StartDate <= '" + FileDateEnd.ToString() + "'";

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

        #region 根据ID获得会议详细信息
        /// <summary>
        /// 根据ID获得会议详细信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="MeetingInfoID">会议ID</param>
        /// <returns>会议信息</returns>
        public static DataTable GetMeetingInfoByID(string CompanyCD, int MeetingInfoID)
        {
            try
            {
                string sql = "select " +
	                               " mi.ID,mi.CompanyCD,mi.MeetingNo,mi.Title,mi.TypeID,mi.Caller,ei.EmployeeName CallerName,"+
	                               " mi.DeptID,di.DeptName,mi.Chairman,eic.EmployeeName ChairName,"+
	                               " CONVERT(varchar(100),mi.StartDate, 23) StartDate,mi.StartTime,mi.TimeLong,"+
	                               " mi.Place,mi.Topic,mi.Contents,mi.ReadyInfo,mi.Attachment,"+
	                               " mi.Remark,mi.MeetingStatus,mi.IsMobileCall,mi.CallContent,"+
	                               " mi.JoinUser,mi.Sender,eis.EmployeeName SenderName, "+
	                               " CONVERT(varchar(100),mi.SendDate, 23) SendDate,"+
	                               " CONVERT(varchar(100),mi.ModifiedDate, 23) ModifiedDate,"+
	                               " mi.ModifiedUserID"+
                               " from "+
	                               " officedba.MeetingInfo mi"+
                               " left join officedba.EmployeeInfo ei on ei.id = mi.Caller" +
                               " left join officedba.DeptInfo di on di.id = mi.DeptID" +
                               " left join officedba.EmployeeInfo eic on eic.id = mi.Chairman"+
                               " left join officedba.EmployeeInfo eis on eis.id = mi.Sender" +
                               " where mi.ID = @ID " +
                               " and mi.CompanyCD = @CompanyCD";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@ID", MeetingInfoID);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 获取会议通知单列表的方法
        /// <summary>
        /// 获取会议通知单列表的方法
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetMeetingInfo(string CompanyCD ,int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "select " +
                                   " mi.ID,mi.MeetingNo,mi.Title" +
                               " from " +
                                   " officedba.MeetingInfo mi" +
                               " where mi.CompanyCD = '" + CompanyCD + "' and (MeetingStatus = '2' or MeetingStatus = '4')";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);                
                //return SqlHelper.ExecuteSql(sql, param);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 取消会议通知的方法
        /// <summary>
        /// 取消会议通知的方法
        /// </summary>
        /// <param name="id">会议通知ID</param>
        /// <returns>BOOL值</returns>
        public static bool UpdateMeetingCancel(int id)
        {
            try
            {
                string sql = "UPDATE officedba.MeetingInfo set MeetingStatus = '3' where ID = @ID";

                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@ID", id);

                SqlHelper.ExecuteTransSql(sql, param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion

        #region 延期会议通知的方法
        /// <summary>
        /// 延期会议通知的方法
        /// </summary>
        /// <param name="id">会议通知ID</param>
        /// <param name="StartDate">会议通知日期</param>
        /// <param name="StartTime">会议通知时间</param>
        /// <returns>BOOL值</returns>
        public static bool DeferMeeting(int id,string StartDate,string StartTime)
        {
            try
            {
                string sql = "UPDATE officedba.MeetingInfo set StartDate =@StartDate,StartTime=@StartTime,MeetingStatus='2' where ID = @ID";

                SqlParameter[] param = new SqlParameter[3];
                param[0] = SqlHelper.GetParameter("@ID", id);
                param[1] = SqlHelper.GetParameter("@StartDate", StartDate);
                param[2] = SqlHelper.GetParameter("@StartTime", StartTime);

                SqlHelper.ExecuteTransSql(sql, param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion

        #region 获得会议通知单的方法
        /// <summary>
        /// 获得会议通知单的方法
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetMeetingInfoNo(string CompanyCD)
        {
            try
            {
                string sql = "select " +
                                   " ID,MeetingNo" +
                               " from " +
                                   " officedba.MeetingInfo" +
                               " where" +
                               " (MeetingStatus = '2' or MeetingStatus = '4')" +
                               " and CompanyCD = '" + CompanyCD + "'";                              

                return SqlHelper.ExecuteSql(sql);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据会议通知ID修改会议通知状态
        /// <summary>
        /// 根据会议通知ID修改会议通知状态
        /// </summary>
        /// <param name="MeetingInfoM">会议通知ID</param>
        /// <returns>bool值</returns>
        public static bool UpdateMeetingStatusByID(string ID,string MeetingStatus, string ModifiedUserID, string ModifiedDate)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.MeetingInfo set ");
                sql.AppendLine("MeetingStatus     =@MeetingStatus     ,");                
                sql.AppendLine("ModifiedDate  =@ModifiedDate  ,");
                sql.AppendLine("ModifiedUserID=@ModifiedUserID");
                sql.AppendLine(" WHERE ");
                sql.AppendLine("ID = @ID ");

                SqlParameter[] param = new SqlParameter[4];
                param[0] = SqlHelper.GetParameter("@ID", ID);
                param[1] = SqlHelper.GetParameter("@MeetingStatus", MeetingStatus);               
                param[2] = SqlHelper.GetParameter("@ModifiedDate  ", ModifiedDate);
                param[3] = SqlHelper.GetParameter("@ModifiedUserID", ModifiedUserID);

                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion

        #region 导出会议通知列表
        /// <summary>
        /// 导出会议通知列表
        /// </summary>
        /// <param name="MeetingInfoM"></param>
        /// <param name="FileDateBegin"></param>
        /// <param name="FileDateEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportMeetingInfo(MeetingInfoModel MeetingInfoM, string FileDateBegin, string FileDateEnd,string ord)
        {
            try
            {
                #region sql语句
                string sql = "select " +
                                   " mi.ID,mi.MeetingNo,mi.Title," +
                                   " mi.DeptID,isnull(di.DeptName,'') DeptName," +
                                   " mi.Caller,isnull(ei.EmployeeName,'') EmployeeName," +
                                   " CONVERT(varchar(100), mi.StartDate, 23)+' '+mi.StartTime StartDate," +
                                   " mi.Place,mr.RoomName," +
                                   " (case mi.MeetingStatus when '1' then '草稿' when '2' then '延期' when '3' then '取消' when '4' then '已通知' end)MeetingStatus," +
                                   " mi.Sender,isnull(eis.EmployeeName,'') SenderName " +
                                   " ,isnull(mrc.MeetingNo,'') ishave" +
                               " from " +
                                   " officedba.MeetingInfo mi" +
                               " left join officedba.DeptInfo di on di.id = mi.DeptID" +
                               " left join officedba.EmployeeInfo ei on ei.id = mi.Caller" +
                               " left join officedba.MeetingRoom mr on mr.id = mi.Place" +
                               " left join officedba.EmployeeInfo eis on eis.id = mi.Sender" +
                               " left join officedba.MeetingRecord mrc on mrc.MeetingNo = mi.MeetingNo" +
                               " where" +
                                   " mi.CompanyCD = '" + MeetingInfoM.CompanyCD + "'";

                if (MeetingInfoM.MeetingNo != "")
                    sql += " and mi.MeetingNo like '%" + MeetingInfoM.MeetingNo + "%'";
                if (MeetingInfoM.MeetingStatus != "0")
                    sql += " and mi.MeetingStatus = '" + MeetingInfoM.MeetingStatus + "'";
                if (MeetingInfoM.Place != 0)
                    sql += " and mi.Place = " + MeetingInfoM.Place + "";
                if (MeetingInfoM.DeptID != 0)
                    sql += " and mi.DeptID = " + MeetingInfoM.DeptID + "";
                if (MeetingInfoM.Title != "")
                    sql += " and mi.Title like '%" + MeetingInfoM.Title + "%'";
                if (MeetingInfoM.Caller != 0)
                    sql += " and mi.Caller = " + MeetingInfoM.Caller + "";
                if (MeetingInfoM.Sender != 0)
                    sql += " and mi.Sender = " + MeetingInfoM.Sender + "";
                if (FileDateBegin != "")
                    sql += " and mi.StartDate >= '" + FileDateBegin.ToString() + "'";
                if (FileDateEnd != "")
                    sql += " and mi.StartDate <= '" + FileDateEnd.ToString() + "'";

                #endregion

                return SqlHelper.ExecuteSql(sql);
            }
            catch 
            {
                return null;
            }
        }
        #endregion
    }
}
