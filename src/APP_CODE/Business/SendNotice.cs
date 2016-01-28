using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using XBase.Data.DBHelper;
using System.Data.SqlClient;

namespace XBase.Business
{
    public  class SendNotice
    {
        
        private static System.Threading.Timer timer = null;
        private static string  PostUrl
         {
            get
            {
                string val = System.Web.Configuration.WebConfigurationManager.AppSettings["SendMessageURL"];
                if (val + "" == "")
                {
                    throw new Exception("短信发送页面地址未配置");
                }
                return val;
            }
        }

        private static long ScanTime
        {
            get
            {
                string val = System.Web.Configuration.WebConfigurationManager.AppSettings["SendMessageScanTime"];
                if (val + "" == "")
                {
                    throw new Exception("短信发送后台扫描时间没有配置 未配置");
                }
                return Convert.ToInt64(val);
            }
        }

        public static void Start()
        {
            SendNotice Action = new SendNotice();
            timer = new System.Threading.Timer(new System.Threading.TimerCallback(Action.DoSend));
            timer.Change(10, ScanTime);
        }

        public void DoSend(object state)
        {
            DataTable dt = new DataTable();
            dt = XBase.Data.SendNoticeDBHelper.GetSendTable();
            foreach (DataRow dr in dt.Rows) { 
                 DateTime date = DateTime.Now;
                 try{
                   date = DateTime.Parse(dr["PlanNoticeDate"].ToString()); 
                 } catch{
                    continue; 
                 }
                 if (date < DateTime.Now) {

                     string MobileNum = "";
                     string SendText="";
                     string CompanyCD = "";
                     string ReceiveUserName = "";
                     string ReceiveUserID = "";

                     GetSendString(dr["SourceFlag"].ToString(), dr["SourceID"].ToString(), out  MobileNum, out  SendText, out CompanyCD, out ReceiveUserName, out ReceiveUserID);

                     int messgeanum = XBase.Data.SendNoticeDBHelper.GetAutoMessageNum(CompanyCD);
                     if (messgeanum <= 0)
                         return ;
                     if (!string.IsNullOrEmpty(MobileNum))
                     {
                         if (XBase.Common.SMSender.InternalSendbackgroud(MobileNum, SendText))
                         {

                             string Sqlstr = "update  officedba.NoticeHistory  set RealNoticeDate ='" + DateTime.Now + "'  where SourceFlag= '" + dr["SourceFlag"] + "'   and  SourceID='" + dr["SourceID"] + "'";
                             SqlHelper.ExecuteSql(Sqlstr);
                             XBase.Data.SendNoticeDBHelper.UpdataAutoMessageNum(CompanyCD, messgeanum - 1, ReceiveUserName, ReceiveUserID, MobileNum, SendText);
                         }
                     }
                 }
            }
        }

        private void GetSendString(string SourceFlag, string SourceID, out string MobileNum,out  string SendText ,out string CompanyCD,out string ReceiveUserName, out string ReceiveUserID)
        {
            string RNum ="";
            string RText="";
            string RCompanyCD = "";
            string RReceiveUserName="";
            string RReceiveUserID = "";
            switch (SourceFlag) {
                case "1": XBase.Data.SendNoticeDBHelper.GetAgendString(SourceID, out  RNum, out   RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break;
                case "2": XBase.Data.SendNoticeDBHelper.GetAimString(SourceID, out  RNum, out   RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break;
                case "3": XBase.Data.SendNoticeDBHelper.GetTaskString(SourceID, out  RNum, out   RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break;
                case "4": XBase.Data.SendNoticeDBHelper.GetSellChanceString(SourceID, out RNum, out   RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break;
                case "5": XBase.Data.SendNoticeDBHelper.GetMsgProess(SourceID, out RNum, out RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break; //进度控制 
            }
            MobileNum  = RNum;
            SendText  =RText;
            CompanyCD = RCompanyCD;
            ReceiveUserName =RReceiveUserName;
            ReceiveUserID = RReceiveUserID;
        }

    }
}
