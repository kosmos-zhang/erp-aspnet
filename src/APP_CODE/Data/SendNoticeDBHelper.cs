using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;

namespace XBase.Data
{
    public  class SendNoticeDBHelper
    {
        public static DataTable GetSendTable() {
            string Sqlstr = "select * from  officedba.NoticeHistory where  PlanNoticeDate>@NowTime  and RealNoticeDate is  NULL ";

            SqlCommand comm = new SqlCommand();
            comm.CommandText = Sqlstr;
            comm.Parameters.AddWithValue("@NowTime", SqlDbType.DateTime);
            comm.Parameters["@NowTime"].Value = DateTime.Now.AddHours(-1.5);
            return SqlHelper.ExecuteSearch(comm);
        }

        public static void GetAgendString(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        {
            string Sqlstr = "select *,ei.ID as ReceiveUserID  ,ei.EmployeeName as ReceiveUserName from officedba.PersonalDateArrange as  pda  inner join officedba.EmployeeInfo as ei on pda.Creator = ei.ID  where pda.ID = " + SourceID;

            SqlCommand comm = new SqlCommand();
            comm.CommandText = Sqlstr;
            DataTable dt =  SqlHelper.ExecuteSearch(comm);
            if (dt != null && dt.Rows.Count > 0)
            {
                CompanyCD = dt.Rows[0]["CompanyCD"] + "";
                MobileNum = dt.Rows[0]["Mobile"] + "";
                ReceiveUserName = dt.Rows[0]["ReceiveUserName"] + "";
                ReceiveUserID = dt.Rows[0]["ReceiveUserID"] + "";
                SendText = "您有新的日程安排，" + dt.Rows[0]["Content"] + ",请登陆系统查看";
            }
            else {
                CompanyCD = "";
                MobileNum = "";
                ReceiveUserName = "";
                ReceiveUserID = "";
                SendText = "";
            }
        }
        public static void GetAimString(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        {
            string Sqlstr = "select * ,ei.ID as ReceiveUserID  ,ei.EmployeeName as ReceiveUserName ,bt.TypeName    from officedba.PlanAim pa  inner join officedba.EmployeeInfo as ei on pa.PrincipalID = ei.ID   left join  officedba.CodePublicType  bt on  pa.AimTypeID=bt.id  where pa.ID = " + SourceID;

            SqlCommand comm = new SqlCommand();
            comm.CommandText = Sqlstr;
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            if (dt != null && dt.Rows.Count > 0)
            {
                CompanyCD = dt.Rows[0]["CompanyCD"] + "";
                MobileNum = dt.Rows[0]["Mobile"] + "";
                ReceiveUserName = dt.Rows[0]["ReceiveUserName"] + "";
                ReceiveUserID = dt.Rows[0]["ReceiveUserID"] + "";
                switch (dt.Rows[0]["AimFlag"] + "")
                {
                    case "1": SendText = "请尽快登录系统查看" +dt.Rows[0]["AimDate"]  + "日" + dt.Rows[0]["ReceiveUserName"] + "的"+dt.Rows[0]["TypeName"]+"。"; break;
                    case "2": SendText = "请尽快登录系统查看" +dt.Rows[0]["AimDate"]  + "年第"+dt.Rows[0]["AimNum"]+"周" + dt.Rows[0]["ReceiveUserName"] + "的"+dt.Rows[0]["TypeName"]+"。"; break;
                    case "3": SendText = "请尽快登录系统查看" +dt.Rows[0]["AimDate"]  + "年"+dt.Rows[0]["AimNum"]+"月" + dt.Rows[0]["ReceiveUserName"] + "的"+dt.Rows[0]["TypeName"]+"。"; break;
                    case "4": SendText = "请尽快登录系统查看" +dt.Rows[0]["AimDate"]  + "年第"+dt.Rows[0]["AimNum"]+"季度" + dt.Rows[0]["ReceiveUserName"] + "的"+dt.Rows[0]["TypeName"]+"。"; break;
                    case "5": SendText = "请尽快登录系统查看" + dt.Rows[0]["AimDate"] + "年" + dt.Rows[0]["ReceiveUserName"] + "的" + dt.Rows[0]["TypeName"] + "。"; break;
                    default: SendText = ""; break;
                }
            }
            else
            {
                CompanyCD = "";
                MobileNum = "";
                SendText = "";
                ReceiveUserName =  "";
                ReceiveUserID =  "";
            }
        }


        public static void GetTaskString(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        {
            string Sqlstr = "select *,ei.ID as ReceiveUserID  ,ei.EmployeeName as ReceiveUserName,bt.TypeName from officedba.Task  ta  inner join officedba.EmployeeInfo as ei on ta.Principal = ei.ID    left join  officedba.CodePublicType  bt on  ta.TaskTypeID=bt.id   where ta.ID = " + SourceID;
            SqlCommand comm = new SqlCommand();
            comm.CommandText = Sqlstr;
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            if (dt != null && dt.Rows.Count > 0)
            {
                CompanyCD = dt.Rows[0]["CompanyCD"] + "";
                MobileNum = dt.Rows[0]["Mobile"] + "";
                ReceiveUserName = dt.Rows[0]["ReceiveUserName"] + "";
                ReceiveUserID = dt.Rows[0]["ReceiveUserID"] + "";
                SendText = "您有" + dt.Rows[0]["TypeName"] + "(" + dt.Rows[0]["Title"] + ")需要完成,请尽快登录系统查看详情";
            }
            else
            {
                CompanyCD = "";
                MobileNum = "";
                SendText = "";
                ReceiveUserName = "";
                ReceiveUserID =  "";
            }
        }
        #region 获取销售机会发送信息串
        /// <summary>
        /// 获取销售机会发送信息串
        /// </summary>
        /// <param name="SourceID"></param>
        /// <param name="MobileNum"></param>
        /// <param name="SendText"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="ReceiveUserName"></param>
        /// <param name="ReceiveUserID"></param>
        public static void GetSellChanceString(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        {
            string Sqlstr = "select sc.*,e.EmployeeName as ReceiveUserName ";
            Sqlstr += " from officedba.SellChance  sc  ";
            Sqlstr += "left join officedba.EmployeeInfo as e on sc.ReceiverID = e.ID  ";
            Sqlstr += "where sc.ID = " + SourceID;
            SqlCommand comm = new SqlCommand();
            comm.CommandText = Sqlstr;
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            if (dt != null && dt.Rows.Count > 0)
            {
                CompanyCD = dt.Rows[0]["CompanyCD"] + "";
                MobileNum = dt.Rows[0]["RemindMTel"] + "";
                ReceiveUserName = dt.Rows[0]["ReceiveUserName"] + "";
                ReceiveUserID = dt.Rows[0]["ReceiverID"] + "";
                //SendText = "您有" + dt.Rows[0]["ChanceNo"] + "(" + dt.Rows[0]["Title"] + ")需要完成,请尽快登录系统查看详情";
                SendText = dt.Rows[0]["RemindContent"].ToString()+" 来自编号为"+dt.Rows[0]["ChanceNo"]+"的销售机会！";
            }
            else
            {
                CompanyCD = "";
                MobileNum = "";
                SendText = "";
                ReceiveUserName = "";
                ReceiveUserID = "";
            }
        }
        #endregion

        public static int GetAutoMessageNum(string CompanyCD) {
            string sql = " select AutoMsgNum from pubdba.companyOpenServ  where CompanyCD='" + CompanyCD + "' ";
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            int num = 0;
            try
            {
               num =  Convert.ToInt32(dt.Rows[0][0]);
            }
            catch { 
            }

            return num;
        }


        public static bool UpdataAutoMessageNum(string CompanyCD, int num, string ReceiveUserName, string ReceiveUserID, string MobileNum, string SendText)
        {
            string sql = " update pubdba.companyOpenServ set  AutoMsgNum= " + num + "  where   CompanyCD='" + CompanyCD + "' ";
            sql += @"INSERT INTO [officedba].[MobileMsgMonitor]
           ([CompanyCD]
           ,[MsgType]
           ,[SendUserID]
           ,[SendUserName]
           ,[ReceiveUserID]
           ,[ReceiveUserName]
           ,[ReceiveMobile]
           ,[Content]
           ,[Status]
           ,[CreateDate]
           ,[SendDate])"+
         "    VALUES  ( '" + CompanyCD + "' ,'0',0,'系统短信'," + ReceiveUserID + ",'" + ReceiveUserName + "','" + MobileNum + "','" + SendText + "','1','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' )";
            
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            try
            {
              return   SqlHelper.ExecuteTransWithCommand(comm);
            }
            catch {
                return false;
            }
        }

        #region 获取进度模块发送信息串
        /// <summary>
        /// 获取进度模块发送信息串
        /// </summary>
        /// <param name="SourceID"></param>
        /// <param name="MobileNum"></param>
        /// <param name="SendText"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="ReceiveUserName"></param>
        /// <param name="ReceiveUserID"></param>
        public static void GetMsgProess(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        {
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.*,B.Mobile,B.EmployeeName from officedba.MsgSendList A left join officedba.Employeeinfo B ");
            sb.AppendLine("on A.empid=B.ID where A.ID=@id");
            SqlParameter[] param = {
                                       new SqlParameter("@id",SqlDbType.VarChar,50)
                                   };
            param[0].Value = SourceID;
            DataTable dt = SqlHelper.ExecuteSql(sb.ToString(), param);
            if (dt != null && dt.Rows.Count > 0)
            {
                CompanyCD = dt.Rows[0]["CompanyCD"] + "";
                MobileNum = dt.Rows[0]["Mobile"] + "";
                ReceiveUserName = dt.Rows[0]["EmployeeName"] + "";
                ReceiveUserID = dt.Rows[0]["Empid"] + "";
                SendText = dt.Rows[0]["msgContent"].ToString();
            }
            else
            {
                CompanyCD = "";
                MobileNum = "";
                SendText = "";
                ReceiveUserName = "";
                ReceiveUserID = "";
            }
        }
        #endregion

    }
}
