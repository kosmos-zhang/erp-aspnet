<%@ WebHandler Language="C#" Class="FlowNetscape" %>
/*
针对审批流程firefox下
*/
using System;
using System.Web;
using XBase.Common;
using XBase.Business.Common;
using System.Data;
using System.Text;
using XBase.Business.Office.SystemManager;


public class FlowNetscape : IHttpHandler, System.Web.SessionState.IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        string companyCD = string.Empty;
        string loginUserID = string.Empty;
        int loginEmployeeID = 0;
        try
        {
            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            loginEmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
        }
        catch
        {
            companyCD = "AAAAAA";//[待修改]
            loginUserID = "admin";//[待修改]
            loginEmployeeID = 7;//[待修改]
        }
        string Action = context.Request.Params["Action"].ToString().Trim();



        int BillTypeFlag = int.Parse(context.Request.QueryString["BillTypeFlag"].ToString().Trim());
        int BillTypeCode = int.Parse(context.Request.QueryString["BillTypeCode"].ToString().Trim());
        int BillID = int.Parse(context.Request.QueryString["BillID"].ToString().Trim());
        string ModifiedUserID = loginUserID;

        JsonClass jc;
        if (Action == "GetFlow")//获取流程
        {
            int intSure = int.Parse(context.Request.QueryString["intSure"].ToString().Trim());
            string sbFlow = "";
            string tmpReason = "";
            DataSet ds = XBase.Business.Common.FlowBus.GetFlow(companyCD, BillTypeFlag, BillTypeCode, BillID);
            if (ds.Tables.Count > 0)
            {
                string strTypeName = "";
                if (ds.Tables[intSure].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[intSure].Rows.Count; i++)
                    {
                        sbFlow = sbFlow + "<option value=\"" + ds.Tables[intSure].Rows[i]["FlowNo"].ToString() + "\">" + ds.Tables[intSure].Rows[i]["FlowName"].ToString() + "</option>";
                    }
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    strTypeName = ds.Tables[2].Rows[0]["TypeName"].ToString();
                }
                tmpReason = sbFlow + "|" + ds.Tables[intSure].Rows.Count + "|" + strTypeName;

            }
            else
            {
                //jc = new JsonClass("没有找到流程", "", 0);
                tmpReason = "没有找到流程|0| ";
            }
            context.Response.Write(tmpReason);
        }
        else
        {
            jc = new JsonClass("设置错误", "", 0);
            context.Response.Write(jc);
        }


        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}