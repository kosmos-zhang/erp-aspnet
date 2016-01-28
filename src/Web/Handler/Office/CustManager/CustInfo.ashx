<%@ WebHandler Language="C#" Class="CustInfo" %>

using System;
using System.Web;
using XBase.Model.Office.CustManager;
using System.Xml.Linq;
using System.IO;
using System.Data;
using XBase.Business.Office.CustManager;
using System.Web.Script.Serialization;
using System.Collections;
using XBase.Common;

public class CustInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{    
    public void ProcessRequest (HttpContext context) 
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "desc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CreatedDate";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "asc";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

            //获取数据
            CustInfoModel CustInfoM = new CustInfoModel();
            CustInfoM.CustNo = context.Request.Form["CustNo"].ToString().Trim();
            CustInfoM.CustNam = context.Request.Form["CustNam"].ToString().Trim();
            CustInfoM.CustClass = (context.Request.Form["CustClass"].ToString().Trim() == "") ? 0 : Convert.ToInt32(context.Request.Form["CustClass"].ToString());

            CustInfoM.CustName = context.Request.Form["CustName"].ToString().Trim();
            CustInfoM.CustShort = context.Request.Form["CustShort"].ToString().Trim();
            CustInfoM.AreaID = context.Request.Form["Area"].ToString().Trim() == "0" ? 0 : Convert.ToInt32(context.Request.Form["Area"].ToString());
            CustInfoM.CreditGrade = context.Request.Form["CreditGrade"].ToString().Trim() == "0" ? 0 : Convert.ToInt32(context.Request.Form["CreditGrade"].ToString()); ;
            CustInfoM.RelaGrade = context.Request.Form["RelaGrade"].ToString().Trim();
            CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            CustInfoM.UsedStatus = context.Request.Form["UsedStatus"].ToString().Trim();
            CustInfoM.Tel = context.Request.Form["Tel"].ToString().Trim();
            CustInfoM.CustBig = context.Request.Form["CustBig"].ToString().Trim();

            string Manager = context.Request.Form["Manager"].ToString().Trim();
            string CreatedBegin = context.Request.Form["CreatedBegin"].ToString().Trim();
            string CreatedEnd = context.Request.Form["CreatedEnd"].ToString().Trim();
            
            string ord=orderBy+" "+order;
            int totalCount = 0;

            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID
            //XElement dsXML = ConvertDataTableToXML(CustInfoBus.GetCustInfoBycondition(CustInfoM, Manager, CreatedBegin, CreatedEnd));
            DataTable dt = CustInfoBus.GetCustInfoBycondition(CustInfoM, Manager,CanUserID, CreatedBegin, CreatedEnd, pageIndex, pageCount, ord, ref totalCount);

            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(totalCount.ToString());
            sb.Append(",data:");
            //if (dt.Rows.Count == 0)
            if (dt == null)
                sb.Append("[{\"id\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dt)); 
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
}