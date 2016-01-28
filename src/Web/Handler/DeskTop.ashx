<%@ WebHandler Language="C#" Class="DeskTop" %>

using System;
using System.Web;
using System.Data;
using System.Text;
using XBase.Common;

public class DeskTop : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

        if (context.Request.QueryString["GetServerTime"] != null) {
            context.Response.Write("{result:true,data:'" + GetServerTime()+"'}");
            return;
        }
        
        DataTable dataList = new DataTable();
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        int modfiyid = userInfo.EmployeeID;
        int recCount = XBase.Business.Personal.WorkFlow.WorkFlowBus.GetDeskTopVFlowWaitProcessList(out dataList,modfiyid);
        
        StringBuilder sb = new StringBuilder();
        sb.Append("{result:true,data:");
        sb.Append("{count:" + recCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        //OutputResult(true, sb.ToString());
       context.Response.Write(sb.ToString());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }


    private string GetServerTime() {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
    
       protected string DataTable2Json(DataTable dt)
    {
        System.Text.StringBuilder jsonBuilder = new System.Text.StringBuilder();
        jsonBuilder.Append("[");
        if (dt.Rows.Count == 0)
        {
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                //jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
                jsonBuilder.Append(":\"");
                try
                {
                    jsonBuilder.Append( GetSafeJSONString(dt.Rows[i][j].ToString()) );//.Replace("\"","\\\""));
                }
                catch
                {
                    jsonBuilder.Append("");
                }
                jsonBuilder.Append("\",");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        return jsonBuilder.ToString();
    }


       private static System.Text.RegularExpressions.Regex safeJSON = new System.Text.RegularExpressions.Regex("[\\n\\r]");
       protected string GetSafeJSONString(string input)
       {
           string output = input.Replace("\"", "\\\"");
           output = safeJSON.Replace(output, "<br>");

           return output;

       }
}