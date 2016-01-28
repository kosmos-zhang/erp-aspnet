<%@ WebHandler Language="C#" Class="TalkInfo" %>

using System;
using System.Web;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;
using System.Data;
using System.Xml.Linq;
using XBase.Common;
using System.IO;
using System.Web.Script.Serialization;
using System.Linq;

public class TalkInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        
                //设置行为参数
                string orderString = (context.Request.Form["orderby"].ToString());//排序
                string order = "desc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CreatedDate";//要排序的字段，如果为空，默认为"linkmanname"
                if (orderString.EndsWith("_d"))
                {
                    order = "asc";//排序：降序
                }
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

                //获取数据
                CustTalkModel CustTalkM = new CustTalkModel();
                string CustName = context.Request.Form["CustName"].ToString().Trim();//客户名称
                CustTalkM.TalkType = Convert.ToInt32(context.Request.Form["TalkType"].ToString().Trim());//类型
                CustTalkM.Priority = context.Request.Form["Priority"].ToString().Trim();//优先级
                string TalkBegin = context.Request.Form["TalkBegin"].ToString().Trim() ;//== "" ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(context.Request.Form["TalkBegin"].ToString());//开始时间
                string TalkEnd = context.Request.Form["TalkEnd"].ToString().Trim() ;//== "" ? Convert.ToDateTime("9999-12-31") : Convert.ToDateTime(context.Request.Form["TalkEnd"].ToString() + " 23:59:59.000");//结束时间            
                CustTalkM.Title = context.Request.Form["Title"].ToString().Trim();//主题
                CustTalkM.Status = context.Request.Form["Status"].ToString().Trim();//状态

                CustTalkM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                string Manager = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID

                string ord = orderBy + " " + order;
                int totalCount = 0;

                DataTable dt = TalkBus.GetTalkInfoBycondition(Manager,CustName, CustTalkM, TalkBegin, TalkEnd, pageIndex, pageCount, ord, ref totalCount);
                
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt == null)
                    sb.Append("[{\"id\":\"\"}]");
                else
                    sb.Append(JsonClass.DataTable2Json(dt)); 
                sb.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}