<%@ WebHandler Language="C#" Class="UserLifeHandler" %>

using System;
using System.Web;
using System.Web.SessionState;

using System.Data;

public class UserLifeHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    ///// <summary>
    ///// DataTable转换为json字符串
    ///// </summary>
    ///// <param name="dt"></param>
    ///// <returns></returns>
    //protected string DataTable2Json(DataTable dt)
    //{
    //    System.Text.StringBuilder jsonBuilder = new System.Text.StringBuilder();
    //    jsonBuilder.Append("[");
    //    if (dt.Rows.Count == 0)
    //    {
    //        jsonBuilder.Append("]");
    //        return jsonBuilder.ToString();
    //    }

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        jsonBuilder.Append("{");
    //        for (int j = 0; j < dt.Columns.Count; j++)
    //        {
    //            //jsonBuilder.Append("\"");
    //            jsonBuilder.Append(dt.Columns[j].ColumnName);
    //            jsonBuilder.Append(":\"");
    //            try
    //            {
    //                jsonBuilder.Append(GetSafeJSONString(dt.Rows[i][j].ToString()));//.Replace("\"","\\\""));
    //            }
    //            catch
    //            {
    //                jsonBuilder.Append("");
    //            }
    //            jsonBuilder.Append("\",");
    //        }
    //        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
    //        jsonBuilder.Append("},");
    //    }
    //    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
    //    jsonBuilder.Append("]");
    //    return jsonBuilder.ToString();
    //}

    //private static System.Text.RegularExpressions.Regex safeJSON = new System.Text.RegularExpressions.Regex("[\\n\\r]");
    //protected string GetSafeJSONString(string input)
    //{
    //    string output = input.Replace("\"", "\\\"");
    //    output = safeJSON.Replace(output, "<br>");

    //    return output;

    //}

    
    

    //private static XBase.Business.SystemManager.SysNotice NoticeBll = new XBase.Business.SystemManager.SysNotice();
        
    public void ProcessRequest (HttpContext context) {
        
        if (context.Request["act"] != null)
        {
            //刷新的时候不结束当前会话
            string IsFresh = context.Request["IsFresh"];
            if (!string.IsNullOrEmpty(IsFresh) && IsFresh=="true" )
                return;
            
            XBase.Common.UserSessionManager.Remove(context.Session.SessionID);

            return;
        }
        
        XBase.Common.UserSessionManager.Life(context.Session.SessionID);
        
        
        
        
        /*
         获取最新公告并返回
         */

        //DataTable sysNotice = NoticeBll.GetList("OverDate>GETDATE()", 1);
        //if (sysNotice.Rows.Count == 0)
        //{
        //    context.Response.Write("{result:false,data:[]}");
        //}

        //context.Response.Write("{result:true,data:" + DataTable2Json(sysNotice) + "}"); 
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}