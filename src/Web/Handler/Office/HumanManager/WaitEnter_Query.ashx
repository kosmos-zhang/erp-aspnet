<%@ WebHandler Language="C#" Class="WaitEnter_Query" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/24
 * 描    述： 待入职
 * 修改日期： 2009/04/24
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.IO;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;

public class WaitEnter_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取操作类型
        string action = context.Request.QueryString["Action"];
        //查询
        if ("GetInfo".Equals(action))
        {
            DoSearch(context);
        }
        //保存
        else if ("Save".Equals(action))
        {
            DoSave(context);
        }
    }
    
    /// <summary>
    /// 执行保存操作
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void DoSave(HttpContext context)
    {
        //获取上下文的请求
        HttpRequest request = context.Request;
        //定义Model变量
        EmployeeSearchModel model = new EmployeeSearchModel();
        //ID
        model.ID = request.QueryString["EmployeeID"];
        //人员编号
        model.EmployeeNo = request.QueryString["EmployeeNo"];
        //部门
        model.Dept = request.QueryString["Dept"];
        //岗位ID
        model.QuarterID = request.QueryString["QuarterID"];
        //岗位职等
        model.AdminLevelID = request.QueryString["QuarterLevelID"];
        //行政等级
        model.AdminID = request.QueryString["AdminLevelID"];
        //职称
        model.PositionID = request.QueryString["PositionID"];
        //职务
        model.PositionTitle = request.QueryString["PositionTitle"];
        //入职时间
        model.EnterDate = request.QueryString["EnterDate"];

        //执行保存操作
        bool isSucce = EmployeeInfoBus.UpdateEnterInfo(model);
        //定义Json返回变量
        JsonClass jc;
        //保存成功时
        if (isSucce)
        {
            jc = new JsonClass("", "", 1);
        }
        //保存未成功时
        else
        {
            jc = new JsonClass("", "", 0);
        }
        
        //输出响应
        context.Response.Write(jc);
    }
    
    /// <summary>
    /// 执行查询操作
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void DoSearch(HttpContext context)
    {
        //获取上下文的请求
        HttpRequest request = context.Request;
        string orderString = request.QueryString["OrderBy"];        //从请求中获取排序列
        string orderBy = "asc";//排序：默认为升序
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ModifiedDate"
        if (string.IsNullOrEmpty(orderString) || orderString.EndsWith("_d"))//降序时如果设置为降序
            orderBy = "desc";//排序：降序
        int pageIndex = int.Parse(request.QueryString["PageIndex"]);//从请求中获取当前页
        int pageCount = int.Parse(request.QueryString["PageCount"]);//从请求中获取每页显示记录数
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        EmployeeSearchModel searchModel = new EmployeeSearchModel();//获取数据
        searchModel.EmployeeNo = request.QueryString["EmployeeNo"];//编号
        searchModel.EmployeeName = request.QueryString["EmployeeName"];//姓名
        searchModel.SexID = request.QueryString["Sex"];//性别
        searchModel.CultureLevel = request.QueryString["Culture"];//学历
        searchModel.SchoolName = request.QueryString["SchoolName"];//毕业院校
        searchModel.QuarterID = request.QueryString["QuarterID"];//应聘岗位
        searchModel.Flag = request.QueryString["Flag"];//应聘岗位

        string ord = orderByCol + " " + orderBy;
        int TotalCount = 0;

        DataTable dtEmployee = EmployeeInfoBus.SearchWaitEnterInfo(searchModel, pageIndex, pageCount, ord, ref TotalCount);//查询数据
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(TotalCount.ToString());
        if (dtEmployee.Rows.Count != 0)
        {
            sb.Append(",data:");
            sb.Append(JsonClass.DataTable2Json(dtEmployee));
            sb.Append("}");
        }
        else
        {
            sb.Append(",data:[{");
            sb.Append("\"ID\":\"\"}]");
            sb.Append("}");
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}