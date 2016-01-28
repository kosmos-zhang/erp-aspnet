<%@ WebHandler Language="C#" Class="CheckFlow" %>

using System;
using System.Web;
using XBase.Business.Common;
public class CheckFlow : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string Code = context.Request.QueryString["FlowNo"];//获取序列号
        string CommonTable = context.Request.QueryString["TableName"];
        string ColName = context.Request.QueryString["ColumName"];
        bool result = PrimekeyVerifyBus.CheckCodeUniq(CommonTable, ColName, Code);
        //唯一性字段是否存在存在
        JsonClass jc;
        if (!result)
        {
            jc = new JsonClass("faile", "流程编号已经存在，请重新输入！", 0);
        }
        else
        {
            jc = new JsonClass("succeed", "", 1);
        }
        context.Response.Write(jc);
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}