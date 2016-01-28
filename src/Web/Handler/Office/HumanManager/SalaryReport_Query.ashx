<%@ WebHandler Language="C#" Class="SalaryReport_Query" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/05/23
 * 描    述： 工资报表列表
 * 修改日期： 2009/05/23
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using System.Data;
using System.Xml.Linq;
using System.Linq;
using System.Web.Script.Serialization;
using System.Text;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using XBase.Business.Common;

public class SalaryReport_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取上下文的请求
        HttpRequest request = context.Request;
        //从请求中获取当前操作
        string action = request.Params["Action"].ToString();

        //分页控件查询数据
        if ("GetInfo".Equals(action))
        {
            //从请求中获取排序列
            string orderString = request.Params["OrderBy"].ToString();

            //排序：默认为升序
            string orderBy = "ascending";
            //要排序的字段，如果为空，默认为"ReprotNo"
            string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ReprotNo";
            //降序时如果设置为降序
            if (string.IsNullOrEmpty(orderString) || orderString.EndsWith("_d"))
            {
                //排序：降序
                orderBy = "descending";
            }
            //从请求中获取当前页
            int pageIndex = int.Parse(request.Params["PageIndex"].ToString());
            //从请求中获取每页显示记录数
            int pageCount = int.Parse(request.Params["PageCount"].ToString());
            //跳过记录数
            int skipRecord = (pageIndex - 1) * pageCount;

            //获取数据
            SalaryReportModel searchModel = new SalaryReportModel();
            //设置查询条件
            //报表编号
            searchModel.ReprotNo = request.Params["ReportNo"].ToString();
            //报表主题
            searchModel.ReportName = request.Params["ReportName"].ToString();
            //所属年月
            searchModel.ReportMonth = request.Params["ReportMonth"].ToString();
            //编制状态
            searchModel.Status = request.Params["Status"].ToString();
            //审批状态
            searchModel.FlowStatus = request.Params["FlowStatus"].ToString();

            //查询数据
            DataTable dtReport = SalaryReportBus.SearchReportInfo(searchModel);
            //转化数据
            XElement dsXML = PageListCommon.ConvertDataTableToXML(dtReport, "Data");
            //linq排序
            var dsLinq =
                (orderBy == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderByCol).Value ascending
                 select new SalaryReportModel()
                 {
                     ID = x.Element("ID").Value,//ID
                     ReprotNo = x.Element("ReprotNo").Value,//工资报表编号
                     ReportName = x.Element("ReportName").Value,//工资报表主题
                     ReportMonth = x.Element("ReportMonth").Value,//工资月份
                     StartDate = x.Element("StartDate").Value,//开始时间
                     EndDate = x.Element("EndDate").Value,//结束时间
                     Creator = x.Element("Creator").Value,//创建人
                     CreateDate = x.Element("CreateDate").Value,//编制日期
                     Status = x.Element("Status").Value,//状态
                     StatusName = x.Element("StatusName").Value,//状态名称
                     FlowStatus = x.Element("FlowStatus").Value//审批状态
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderByCol).Value descending
                 select new SalaryReportModel()
                 {
                     ID = x.Element("ID").Value,//ID
                     ReprotNo = x.Element("ReprotNo").Value,//工资报表编号
                     ReportName = x.Element("ReportName").Value,//工资报表主题
                     ReportMonth = x.Element("ReportMonth").Value,//工资月份
                     StartDate = x.Element("StartDate").Value,//开始时间
                     EndDate = x.Element("EndDate").Value,//结束时间
                     Creator = x.Element("Creator").Value,//创建人
                     CreateDate = x.Element("CreateDate").Value,//编制日期
                     Status = x.Element("Status").Value,//状态
                     StatusName = x.Element("StatusName").Value,//状态名称
                     FlowStatus = x.Element("FlowStatus").Value//审批状态
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
        //删除数据
        else
        {
            //获取删除的ID
            string Nos = request.QueryString["DeleteNOs"];
            JsonClass jc;
            //执行删除
            bool isSucc = SalaryReportBus.DeleteReport(Nos);
            //定义Json返回变量
            //删除成功时
            if (isSucc)
            {
                jc = new JsonClass("", "", 1);
            }
            //删除未成功时
            else
            {
                jc = new JsonClass("", "", 0);
            }
            //输出响应
            context.Response.Write(jc);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}