<%@ WebHandler Language="C#" Class="SubStorageInitEdit" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Office.SubStoreManager;
using XBase.Model.Office.SubStoreManager;
using System.Web.SessionState;

public class SubStorageInitEdit : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //获取该界面有没有被引用标识
            //string RejectNo = context.Request.Params["RejectNo"].ToString().Trim();
            int ID = Convert.ToInt32(context.Request.Params["ID"].ToString().Trim());

            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string DeptID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID.ToString();
            DataRow dr = SubStorageDBHelper.GetSubDeptFromDeptID(DeptID);
            if (dr != null)
            {
                DeptID = dr["ID"].ToString();
            }

            DataTable dtp = SubStorageBus.SubStorageIn(ID);
            DataTable dt2 = SubStorageBus.Details(ID, DeptID);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (dt2.Rows.Count == 0)
            {
                sb.Append("{");
                sb.Append("data:");
                sb.Append(JsonClass.DataTable2Json(dtp));
                sb.Append(",data2:[{");
                sb.Append(" ID:null}]}");

            }
            else
            {
                sb.Append("{");
                sb.Append("data:");
                sb.Append(JsonClass.DataTable2Json(dtp));
                sb.Append(",data2:");
                sb.Append(JsonClass.DataTable2Json(dt2));
                sb.Append("}");
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();


        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}