<%@ WebHandler Language="C#" Class="AddGoalFromApply" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/28
 * 描    述： 招聘申请列表
 * 修改日期： 2009/03/28
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

public class AddGoalFromApply : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取上下文的请求
        HttpRequest request = context.Request;
        //从请求中获取排序列
        string orderString = request.QueryString["OrderBy"];

        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        int pageIndex = int.Parse(request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;

        //查询数据
        DataTable dtRectPlan = RectApplyBus.GetGoalInfoFromRectApply();
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtRectPlan, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new RectGoalFromApplyModel()
             {
                 ID = x.Element("ID") == null ? "" : x.Element("ID").Value,//ID
                 DeptID = x.Element("DeptID") == null ? "" : x.Element("DeptID").Value,//部门ID
                 DeptName = x.Element("DeptName") == null ? "" : x.Element("DeptName").Value,//部门名称
                 JobName = x.Element("JobName") == null ? "" : x.Element("JobName").Value,//职务名称
                 RectCount = x.Element("RectCount") == null ? "" : x.Element("RectCount").Value,//人员数量
                 SexID = x.Element("SexID") == null ? "" : x.Element("SexID").Value,//性别ID
                 SexName = x.Element("SexName") == null ? "" : x.Element("SexName").Value,//性别名称
                 CultureLevelID = x.Element("CultureLevelID") == null ? "" : x.Element("CultureLevelID").Value,//学历ID
                 CultureLevelName = x.Element("CultureLevelName") == null ? "" : x.Element("CultureLevelName").Value,//学历名称 
                 ProfessionalID = x.Element("ProfessionalID") == null ? "" : x.Element("ProfessionalID").Value,//专业ID
                 ProfessionalName = x.Element("ProfessionalName") == null ? "" : x.Element("ProfessionalName").Value,//专业名称
                 Age = x.Element("Age") == null ? "" : x.Element("Age").Value,//年龄
                 WorkNeeds = x.Element("WorkNeeds") == null ? "" : x.Element("WorkNeeds").Value,//工作要求
                 WorkAgeName = x.Element("WorkAgeName") == null ? "" : x.Element("WorkAgeName").Value,//工作年限
                 PositionID = x.Element("JobID") == null ? "" : x.Element("JobID").Value,//岗位ID
                 CompleteDate = x.Element("CompleteDate") == null ? "" : x.Element("CompleteDate").Value//完成时间 
             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new RectGoalFromApplyModel()
             {
                 ID = x.Element("ID") == null ? "" : x.Element("ID").Value,//ID
                 DeptID = x.Element("DeptID") == null ? "" : x.Element("DeptID").Value,//部门ID
                 DeptName = x.Element("DeptName") == null ? "" : x.Element("DeptName").Value,//部门名称
                 JobName = x.Element("JobName") == null ? "" : x.Element("JobName").Value,//职务名称
                 RectCount = x.Element("RectCount") == null ? "" : x.Element("RectCount").Value,//人员数量
                 SexID = x.Element("SexID") == null ? "" : x.Element("SexID").Value,//性别ID
                 SexName = x.Element("SexName") == null ? "" : x.Element("SexName").Value,//性别名称
                 CultureLevelID = x.Element("CultureLevelID") == null ? "" : x.Element("CultureLevelID").Value,//学历ID
                 CultureLevelName = x.Element("CultureLevelName") == null ? "" : x.Element("CultureLevelName").Value,//学历名称 
                 ProfessionalID = x.Element("ProfessionalID") == null ? "" : x.Element("ProfessionalID").Value,//专业ID
                 ProfessionalName = x.Element("ProfessionalName") == null ? "" : x.Element("ProfessionalName").Value,//专业名称
                 Age = x.Element("Age") == null ? "" : x.Element("Age").Value,//年龄
                 WorkNeeds = x.Element("WorkNeeds") == null ? "" : x.Element("WorkNeeds").Value,//工作要求
                 WorkAgeName = x.Element("WorkAgeName") == null ? "" : x.Element("WorkAgeName").Value,//工作年限
                 PositionID = x.Element("JobID") == null ? "" : x.Element("JobID").Value,//岗位ID
                 CompleteDate = x.Element("CompleteDate") == null ? "" : x.Element("CompleteDate").Value//完成时间 
             });
        //获取记录总数
        int totalCount = dsLinq.Count();
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //设置记录总数
        sbReturn.Append("{");
        sbReturn.Append("totalCount:");
        sbReturn.Append(totalCount.ToString());
        //设置数据
        sbReturn.Append(",data:");
        sbReturn.Append(PageListCommon.ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
         
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}