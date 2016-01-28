<%@ WebHandler Language="C#" Class="MenuList" %>

using System;
using System.Web;
using XBase.Common;
using System.Data;
using System.Configuration;

public class MenuList : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        String httpRequestResult = "";
        String ParentID = context.Request.QueryString["ParentID"];
        if (ParentID == "" || ParentID == null)
        {
            ParentID = "0";
        }
        UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        DataTable AllMenuInfo = UserInfo.MenuInfo;
        DataTable MenuInfo = new DataTable();
        MenuInfo = AllMenuInfo.Clone();
        DataRow[] rows = AllMenuInfo.Select("ParentID = '" + ParentID + "'");
        for (int i = 0; i < rows.Length; i++)
        {
            if ((string)rows[i]["ModuleType"] == "M")
            {
                httpRequestResult += "<Li id=\"LI1\"><a href=\"" + rows[i]["PropertyValue"].ToString() + "?ModuleID=" + rows[i]["ModuleID"].ToString() + "\" target=\"_mainFrame\">" + rows[i]["ModuleName"].ToString() + "</a></Li>";
            }
            else
            {
                httpRequestResult += "<LI class=\"menu2off\" id=\"LI" + rows[i]["ModuleID"].ToString() + "\"><P><SPAN><A onclick=\"MenuList('" + rows[i]["ModuleID"].ToString() + "');\"><img src='Images/Menu/Main_left_file.jpg'/>" + rows[i]["ModuleName"].ToString() + "</A></SPAN></P><UL id=\"UL" + rows[i]["ModuleID"].ToString() + "\"></UL></LI>";
            }
        }
        context.Response.Write(httpRequestResult);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}