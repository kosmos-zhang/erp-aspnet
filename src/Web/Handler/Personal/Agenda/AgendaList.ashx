<%@ WebHandler Language="C#" Class="AgendaList" %>

using System;
using System.Web;
using System.Data;

using XBase.Common;
using XBase.Business.Personal.Agenda;

public class AgendaList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        if (context.Request["isDeskTop"] != null)
        {
            GetDeskTopList(context.Request, context.Response);
            return;
        }
        DateTime searchtime = DateTime.Now.Date;
        try { searchtime = Convert.ToDateTime(context.Request.QueryString["SearchDate"]); }
        catch { }
        DateTime strtime = new DateTime(searchtime.Year, searchtime.Month, 1);
        DateTime endtime = new DateTime(searchtime.Year, searchtime.Month, DateTime.DaysInMonth(searchtime.Year, searchtime.Month));
        bool iscrru = true;
        //获取登陆用户信息
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        int searchuid = userInfo.EmployeeID;
        try { searchuid = Convert.ToInt32(context.Request.QueryString["Uid"].ToString()); }
        catch { searchuid = userInfo.EmployeeID; }

        if (userInfo.EmployeeID != searchuid)
            iscrru = false;

        if (context.Request.QueryString["SearchMode"] != null && context.Request.QueryString["SearchMode"].ToString() == "ByDay")
        {
            DataTable dt = AgendaListInfoBus.GetAgendaCountByDay(searchtime, searchuid, iscrru);
            //string jsondataStr = "";
            ////定义Json返回变量
            //JsonClass json;
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    jsondataStr = "[";
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        jsondataStr += "{";
            //        try
            //        {
            //            jsondataStr += "'ID':'" + dr["ID"] + "',";
            //            jsondataStr += "'StartDate':'" + Convert.ToDateTime(dr["StartDate"]).ToString("yyyy-MM-dd") + "',";
            //            jsondataStr += "'EndDate':'" + Convert.ToDateTime(dr["EndDate"]).ToString("yyyy-MM-dd") + "',";
            //            jsondataStr += "'ArrangeTItle':'" + dr["ArrangeTItle"] + "',";
            //            jsondataStr += "'Critical':'" + dr["Critical"] + "',";
            //            jsondataStr += "'Content':'" + dr["Content"] + "',";
            //            jsondataStr += "'StartTime':'" + dr["StartTime"] + "',";
            //            jsondataStr += "'EndTime':'" + dr["EndTime"] + "',";
            //            jsondataStr += "'CreatorName':'" + dr["CreatorName"] + "',";
            //            jsondataStr += "'IsPublic':'" + dr["IsPublic"] + "',";
            //            jsondataStr += "'Important':'" + dr["Important"] + "',";
            //            jsondataStr += "'CreateDate':'" + dr["CreateDate"] + "',";
            //            jsondataStr += "'IsMobileNotice':'" + dr["IsMobileNotice"] + "',";
            //            jsondataStr += "'AheadTimes':'" + dr["AheadTimes"] + "',";
            //            jsondataStr += "'Status':'" + dr["Status"] + "',";
            //            jsondataStr += "'ModifiedDate':'" + dr["ModifiedDate"] + "'";
            //        }
            //        catch
            //        {
            //            jsondataStr += "'datemark':'0000-00-00'";
            //            jsondataStr += "'countnumber':'0'";
            //        }
            //        jsondataStr += "},";
            //    }
            //    jsondataStr = jsondataStr.Substring(0, jsondataStr.Length - 1) + "]";
            //}
            //json = new JsonClass("", jsondataStr, 1);
            //context.Response.Write(json);

            context.Response.ContentType = "text/plain";
            context.Response.Write(JsonClass.DataTable2Json(dt));
            context.Response.End();
        }
        else
        {

            DataTable dt = AgendaListInfoBus.GetAgendaCountByDate(strtime, endtime, searchuid, iscrru);
            string jsondataStr = "";
            //定义Json返回变量
            JsonClass json;
            if (dt != null && dt.Rows.Count > 0)
            {
                bool isIn = true;
                jsondataStr = "[";
                while (isIn)
                {
                    int total = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        DateTime dt1 = DateTime.Parse(dr["StartDate"].ToString());
                        DateTime dt2 = DateTime.Parse(dr["EndDate"].ToString());
                        if (DateTime.Compare(strtime, dt1) >= 0 && DateTime.Compare(dt2, strtime) >= 0)
                        {
                            total += int.Parse(dr["countNum"].ToString());
                        }
                    }
                    jsondataStr += "{";
                    jsondataStr += "'datemark':'" + strtime.ToString("yyyy-MM-dd") + "',";
                    jsondataStr += "'countnumber':'" + total.ToString() + "'";
                    jsondataStr += "},";
                    strtime = strtime.AddDays(1);
                    isIn = DateTime.Compare(endtime, strtime) >= 0;
                }
                jsondataStr = jsondataStr.Substring(0, jsondataStr.Length - 1) + "]";
            }
            json = new JsonClass("", jsondataStr, 1);
            context.Response.Write(json);
        }


    }

    private void GetDeskTopList(HttpRequest request, HttpResponse response)
    {

        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        DataTable dt = AgendaListInfoBus.GetAgendaCountByDay(DateTime.Now.Date, userInfo.EmployeeID, true);
        string jsondataStr = "";
        //定义Json返回变量
        if (dt != null && dt.Rows.Count > 0)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = " StartTime ASC ";
            jsondataStr = JsonClass.DataTable2Json(dt);
        }
        else
        {
            jsondataStr = "{sta:0,info:0,data:''}";
        }

        response.Write(jsondataStr);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}