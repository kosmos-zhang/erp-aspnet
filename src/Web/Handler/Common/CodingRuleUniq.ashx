<%@ WebHandler Language="C#" Class="CodingRule" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/18
 * 描    述： 编码唯一性校验
 * 修改日期： 2009/03/18
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Business.Common;

public class CodingRule : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //判断唯一性
        //获取表名
        string tableName = context.Request.QueryString["TableName"];
        //获取表名
        string columnName = context.Request.QueryString["ColumnName"];
        //获取输入的编码
        string codeValue = context.Request.QueryString["CodeValue"];

        //校验编号的唯一性
        bool isUniq = PrimekeyVerifyBus.CheckCodeUniq(tableName, columnName, codeValue);
        //唯一性字段是否存在存在
        JsonClass jc;
        if (isUniq)
            jc = new JsonClass("success", "", 1);
        else
            jc = new JsonClass("faile", "", 0);
        
        context.Response.Write(jc);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}