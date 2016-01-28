<%@ WebHandler Language="C#" Class="SelectReserve_Query" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/21
 * 描    述： 人才储备选择列表
 * 修改日期： 2009/04/21
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

public class SelectReserve_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
        if (string.IsNullOrEmpty(orderBy) || orderString.EndsWith("_d"))
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

        //获取数据
        EmployeeSearchModel searchModel = new EmployeeSearchModel();
        //设置查询条件
        //编号
        searchModel.EmployeeNo = request.QueryString["EmployeeNo"];
        //姓名
        searchModel.EmployeeName = request.QueryString["EmployeeName"];
        //应聘岗位
        searchModel.QuarterID = request.QueryString["QuarterID"];
        //工作年限
        searchModel.TotalSeniority = request.QueryString["TotalSeniority"];
        //学历
        searchModel.CultureLevel = request.QueryString["CultureID"];
        //专业
        searchModel.ProfessionalID = request.QueryString["ProfessionalID"];

        //查询数据
        DataTable dtEmployee = RectInterviewBus.GetReserveInfo(searchModel);
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtEmployee, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new EmployeeSearchModel()
             {
                 ID = x.Element("ID").Value,//ID
                 EmployeeNo = x.Element("EmployeeNo").Value,//编号
                 //ModifiedDate = x.Element("ModifiedDate").Value,//最后修改日期
                 EmployeeName = x.Element("EmployeeName").Value,//姓名
                 SexName = x.Element("SexName").Value,//性别
                 QuarterName = x.Element("QuarterName").Value,//应聘岗位
                 SchoolName = x.Element("SchoolName").Value,//毕业院校
                 ProfessionalName = x.Element("ProfessionalName").Value,//专业
                 CultureLevelName = x.Element("CultureLevelName").Value,//学历
                 TotalSeniority = x.Element("TotalSeniority").Value//工龄 
             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new EmployeeSearchModel()
             {
                 ID = x.Element("ID").Value,//ID
                 EmployeeNo = x.Element("EmployeeNo").Value,//编号
                 //ModifiedDate = x.Element("ModifiedDate").Value,//最后修改日期
                 EmployeeName = x.Element("EmployeeName").Value,//姓名
                 SexName = x.Element("SexName").Value,//性别
                 QuarterName = x.Element("QuarterName").Value,//应聘岗位
                 SchoolName = x.Element("SchoolName").Value,//毕业院校
                 ProfessionalName = x.Element("ProfessionalName").Value,//专业
                 CultureLevelName = x.Element("CultureLevelName").Value,//学历
                 TotalSeniority = x.Element("TotalSeniority").Value//工龄 
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