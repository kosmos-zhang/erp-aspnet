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
    public class MeetingRoomDBHelper
    {
        #region 添加会议室信息的方法
        /// <summary>
        /// 添加会议室信息的方法
        /// </summary>
        /// <param name="MeetingRoomM">会议室信息</param>
        /// <returns>被添加会议室ID</returns>
        public static int MeetingRoomAdd(MeetingRoomModel MeetingRoomM)
        {
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[10];
                param[0] = SqlHelper.GetParameter("@CompanyCD     ",MeetingRoomM.CompanyCD     );
                param[1] = SqlHelper.GetParameter("@RoomName      ",MeetingRoomM.RoomName      );
                param[2] = SqlHelper.GetParameter("@Place         ",MeetingRoomM.Place         );
                param[3] = SqlHelper.GetParameter("@PersonCount   ",MeetingRoomM.PersonCount   );
                param[4] = SqlHelper.GetParameter("@MultimediaFlag",MeetingRoomM.MultimediaFlag);
                param[5] = SqlHelper.GetParameter("@Remark        ",MeetingRoomM.Remark        );
                param[6] = SqlHelper.GetParameter("@UsedStatus    ",MeetingRoomM.UsedStatus    );
                param[7] = SqlHelper.GetParameter("@ModifiedDate  ",MeetingRoomM.ModifiedDate  );
                param[8] = SqlHelper.GetParameter("@ModifiedUserID",MeetingRoomM.ModifiedUserID);

                SqlParameter paramid = new SqlParameter("@id", SqlDbType.Int);
                paramid.Direction = ParameterDirection.Output;
                param[9] = paramid;
                #endregion

                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.insertMeetingRoom", comm, param);
                int MeetingRoomID = Convert.ToInt32(comm.Parameters["@id"].Value);

                return MeetingRoomID;
            }
            catch 
            {
                return 0;
            }
        }
        #endregion

        #region 根据会议室ID修改会议室信息
        /// <summary>
        /// 根据会议室ID修改会议室信息
        /// </summary>
        /// <param name="MeetingRoomM">会议室信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateMeetingRoom(MeetingRoomModel MeetingRoomM)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.MeetingRoom set ");
                sql.AppendLine("CompanyCD     =@CompanyCD     ,");
                sql.AppendLine("RoomName      =@RoomName      ,");
                sql.AppendLine("Place         =@Place         ,");
                sql.AppendLine("PersonCount   =@PersonCount   ,");
                sql.AppendLine("MultimediaFlag=@MultimediaFlag,");
                sql.AppendLine("Remark        =@Remark        ,");
                sql.AppendLine("UsedStatus    =@UsedStatus    ,");
                sql.AppendLine("ModifiedDate  =@ModifiedDate  ,");
                sql.AppendLine("ModifiedUserID=@ModifiedUserID");
                sql.AppendLine(" WHERE ");
                sql.AppendLine("ID = @ID ");

                SqlParameter[] param = new SqlParameter[10];
                param[0] = SqlHelper.GetParameter("@ID      ", MeetingRoomM.ID);
                param[1] = SqlHelper.GetParameter("@CompanyCD     ",MeetingRoomM.CompanyCD     );
                param[2] = SqlHelper.GetParameter("@RoomName      ",MeetingRoomM.RoomName      );
                param[3] = SqlHelper.GetParameter("@Place         ",MeetingRoomM.Place         );
                param[4] = SqlHelper.GetParameter("@PersonCount   ",MeetingRoomM.PersonCount   );
                param[5] = SqlHelper.GetParameter("@MultimediaFlag",MeetingRoomM.MultimediaFlag);
                param[6] = SqlHelper.GetParameter("@Remark        ",MeetingRoomM.Remark        );
                param[7] = SqlHelper.GetParameter("@UsedStatus    ",MeetingRoomM.UsedStatus    );
                param[8] = SqlHelper.GetParameter("@ModifiedDate  ",MeetingRoomM.ModifiedDate  );
                param[9] = SqlHelper.GetParameter("@ModifiedUserID",MeetingRoomM.ModifiedUserID);

                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion

        #region 检索会议室信息的方法
        /// <summary>
        /// 检索会议室信息的方法
        /// </summary>
        /// <param name="MeetingRoomM">会议室信息</param> 
        /// <returns>会议室列表信息</returns>
        public static DataTable GetMeetingRoom(string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "select " +
                                   " ID,RoomName,Place,PersonCount,MultimediaFlag,UsedStatus" +
                               " from " +
                                   " officedba.MeetingRoom" +
                               " where" +
                               " CompanyCD = '" + CompanyCD + "'";
                               

                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            }
            catch 
            {
                return null;
            }
        }
        #endregion

        //public static DataTable GetMeetingRm(string CompanyCD)
        //{
        //    try
        //    {
        //        string sql = "select " +
        //                           " ID,RoomName,Place,PersonCount,MultimediaFlag,UsedStatus" +
        //                       " from " +
        //                           " officedba.MeetingRoom" +
        //                       " where" +
        //                       " CompanyCD = '" + CompanyCD + "'";


        //        return SqlHelper.ExecuteSql(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        string smeg = ex.Message;
        //        return null;
        //    }
        //}

        #region 根据ID获得会议室详细信息
        /// <summary>
        /// 根据ID获得会议室详细信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="MeetingRoomID">会议室ID</param>
        /// <returns>会议室信息</returns>
        public static DataTable GetMeetingRoomByID(string CompanyCD, int MeetingRoomID)
        {
            try
            {
                string sql = "select " +
                                   " ID,CompanyCD,RoomName,Place,PersonCount,MultimediaFlag,Remark,	UsedStatus," +
                                   " CONVERT(varchar(100),ModifiedDate, 23) ModifiedDate,ModifiedUserID" +
                               " from" +
                                   " officedba.MeetingRoom" +
                               " where ID = @ID " +
                                   " and CompanyCD = @CompanyCD";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@ID", MeetingRoomID);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 根据会议室ID修改会议室功能/状态
        /// <summary>
        /// 根据会议室ID修改会议室功能/状态
        /// </summary>
        /// <param name="id">会议室ID</param>
        /// <param name="Filed">字段名</param>
        /// <param name="FiledValue">字段值</param>
        /// <returns>Bool值</returns>
        public static bool UpdateMeetingFlag(int id,string Filed,string FiledValue)
        {
            try
            {
                string sql = "UPDATE officedba.MeetingRoom set " + Filed + "=@"+Filed+" where ID = @ID";

                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@ID", id);
                param[1] = SqlHelper.GetParameter("@"+Filed, FiledValue);                

                SqlHelper.ExecuteTransSql(sql, param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion

        #region 检索会议室名的方法
        /// <summary>
        /// 检索会议室名的方法
        /// </summary>
        /// <param name="MeetingRoomM">会议室名</param> 
        /// <returns>会议室名</returns>
        public static DataTable GetMeetingRoomName(string CompanyCD)
        {
            try
            {
                string sql = "select " +
                                   " ID,RoomName" +
                               " from " +
                                   " officedba.MeetingRoom" +
                               " where" +
                               " CompanyCD = '" + CompanyCD + "'"+
                               " and UsedStatus = 1";

                return SqlHelper.ExecuteSql(sql);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 导出会议室列表
        /// <summary>
        /// 导出会议室列表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable ExportMeetingRoom(string CompanyCD)
        {
            try
            {
                string sql = "select " +
                                   " ID,RoomName,Place,PersonCount,(case MultimediaFlag when '1' then '否' when '2' then '是' end) MultimediaFlag,"+
                                   " (case UsedStatus when '1' then '启用' when '2' then '停用' end) UsedStatus " +
                               " from " +
                                   " officedba.MeetingRoom" +
                               " where" +
                               " CompanyCD = '" + CompanyCD + "'";


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
