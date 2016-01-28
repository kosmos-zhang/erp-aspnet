<%@ WebHandler Language="C#" Class="Main" %>

using System;
using System.Web;
using System.Data;
using XBase.Common;
using System.Data.SqlClient;

public class Main : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

        if (context.Request.Params["sign"] != null)
        {
            if (context.Request.Params["sign"].ToString() == "GetContractCount")
            {
                context.Response.Write("{sta:1,info:'',totalCount:" + GetContractCount() + "}");
            }
            else if (context.Request.Params["sign"].ToString() == "SearchCanHuiTongZhi")
            {
                context.Response.Write("{sta:1,info:'',totalCount:" + SearchCanHuiTongZhi() + "}");
            }
            else if (context.Request.Params["sign"].ToString() == "GetBeiWangLuCount")
            {
                context.Response.Write("{sta:1,info:'',totalCount:" + GetBeiWangLuCount() + "}");
            }
            else if (context.Request.Params["sign"].ToString() == "SearchFeiyongShezhi")
            {
                context.Response.Write("{sta:1,info:'',totalCount:" + SearchFeiyongShezhi() + "}");
            }
            
                
        }
        else
        {
            DataTable dtContract = GetContractList();
            if (dtContract == null)
            {
                context.Response.Write("{sta:0,info:'',totalCount:0}");
            }
            else
            {
                context.Response.Write("{sta:1,info:'',totalCount:" + dtContract.Rows.Count + "}");
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private int GetBeiWangLuCount()
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string sql = " select count(*) from officedba.PersonalMemo where CompanyCD=@CompanyCD and  Memoer=@Memoer and Status=0";
        SqlParameter[] paras = new SqlParameter[2];
        paras[0] = new SqlParameter("@CompanyCD", userInfo.CompanyCD);
        paras[1] = new SqlParameter("@Memoer",  userInfo.EmployeeID  ); 

        return Convert.ToInt32(XBase.Data.DBHelper.SqlHelper.ExecuteScalar(sql, paras));
    }

    private int SearchFeiyongShezhi()
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string sql = " select count(*) from officedba.FeeApply where CompanyCD=@CompanyCD and CONVERT(varchar(100),EndReimbTime, 23)<=CONVERT(varchar(100),getdate(), 23) and Status='2' and IsReimburse='0' and Applyor=@Applyor";
        SqlParameter[] paras = new SqlParameter[2];
        paras[0] = new SqlParameter("@CompanyCD", userInfo.CompanyCD);
        paras[1] = new SqlParameter("@Applyor", userInfo.EmployeeID); 

        return Convert.ToInt32(XBase.Data.DBHelper.SqlHelper.ExecuteScalar(sql, paras));
    }
    
    

    private int SearchCanHuiTongZhi()
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string sql = " select count(*) from officedba.meetinginfo where CompanyCD=@CompanyCD and CONVERT(varchar(100),StartDate, 23)>=CONVERT(varchar(100),getdate(), 23)and (MeetingStatus=2 or MeetingStatus=4) and JoinUser like @JoinUser";
        SqlParameter[] paras = new SqlParameter[2];
        paras[0] = new SqlParameter("@CompanyCD", userInfo.CompanyCD);
        paras[1] = new SqlParameter("@JoinUser", ",%" + userInfo.EmployeeID + "%,");

        return Convert.ToInt32(XBase.Data.DBHelper.SqlHelper.ExecuteScalar(sql, paras));
    }

    private int GetContractCount()
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string sql = " select  count(*) from officedba.EmployeeContract where  CompanyCD=@CompanyCD and  Reminder= @Reminder  and    DATEADD(day, -AheadDay, EndDate) <@day ";
        SqlParameter[] paras = new SqlParameter[3];
        paras[0] = new SqlParameter("@CompanyCD", userInfo.CompanyCD );
        paras[1] = new SqlParameter("@Reminder", userInfo.EmployeeID);
        paras[2] = new SqlParameter("@day", DateTime.Now.ToString("yyyy-MM-dd"));

        return Convert.ToInt32(XBase.Data.DBHelper.SqlHelper.ExecuteScalar(sql, paras));
    }
    
    
    private DataTable GetContractList() {
         UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
         string sql = " select * from officedba.EmployeeContract where  Reminder= " + userInfo.EmployeeID + " and    DATEADD(day, -AheadDay, EndDate) <'"+DateTime.Now.ToString("yyyy-MM-dd")+"' ";
        return  XBase.Data.DBHelper.SqlHelper.ExecuteSql(sql);
    }

}