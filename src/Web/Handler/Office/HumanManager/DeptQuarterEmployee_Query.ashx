<%@ WebHandler Language="C#" Class="DeptQuarterEmployee_Query" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/15
 * 描    述： 组织机构图
 * 修改日期： 2009/04/15
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Business.Office.HumanManager;

public class DeptQuarterEmployee_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //返回生成的组织机构图
        context.Response.Write(DeptQuarterEmployeeBus.GetDeptQuarterEmployeeInfo());
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}