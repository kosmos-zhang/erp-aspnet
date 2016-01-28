<%@ WebHandler Language="C#" Class="EmployeeLeave_Query" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/25
 * 描    述： 查询离职人员列表
 * 修改日期： 2009/03/25
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

public class EmployeeLeave_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        //获取上下文的请求
        HttpRequest request = context.Request;
        //从请求中获取当前操作
        string action = request.QueryString["Action"];

        //分页控件查询数据
        if ("GetInfo".Equals(action))
        {
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

            //获取数据
            EmployeeSearchModel model = new EmployeeSearchModel();
            //编号
            model.EmployeeNo = request.QueryString["EmployeeNo"].Trim();
            //工号
            model.EmployeeNum = request.QueryString["EmployeeNum"].Trim();
            //拼音缩写
            model.PYShort = request.QueryString["PYShort"].Trim();
            //姓名
            model.EmployeeName = request.QueryString["EmployeeName"].Trim();
            //工种
            //model.ContractKind = request.QueryString["ContractKind"].Trim();
            //行政等级
            //model.AdminLevelID = request.QueryString["AdminLevel"].Trim();
            //岗位
            model.QuarterID = request.QueryString["QuarterID"].Trim();
            //职称
            model.PositionID = request.QueryString["PositionID"].Trim();
            //入职时间
            model.EnterDate = request.QueryString["StartDate"].Trim();
            model.EnterEndDate = request.QueryString["EnteryEndDate"].Trim();
            //离职时间
            model.LeaveDate = request.QueryString["LeaveDate"].Trim();
            model.LeaveEndDate = request.QueryString["LeaveEndDate"].Trim();
            //手机号码
            model.Mobile = request.QueryString["Mobile"].Trim();

            //查询数据
            DataTable dtEmployee = EmployeeInfoBus.SearchEmployeeLeaveInfo(model);
            //转化数据
            XElement dsXML = PageListCommon.ConvertDataTableToXML(dtEmployee, "Data");
            //linq排序
            var dsLinq =
                (orderBy == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderByCol).Value ascending
                 select new EmployeeSearchModel()
                 {
                     ID = x.Element("ID").Value,
                     LeaveDate = x.Element("LeaveDate").Value,
                     EmployeeNo = x.Element("EmployeeNo").Value,
                     EmployeeNum = x.Element("EmployeeNum").Value,
                     PYShort = x.Element("PYShort").Value,
                     EmployeeName = x.Element("EmployeeName").Value,
                     SexName = x.Element("SexName").Value,
                     //ContractKind = x.Element("ContractKind").Value,
                     DeptName = x.Element("DeptName").Value,
                     //AdminLevelName = x.Element("AdminLevelName").Value,
                     QuarterName = x.Element("QuarterName").Value,
                     PositionName = x.Element("PositionName").Value,
                     EnterDate = x.Element("EntryDate").Value,
                     //TotalTime = x.Element("TotalYear").Value
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderByCol).Value descending
                 select new EmployeeSearchModel()
                 {
                     ID = x.Element("ID").Value,
                     LeaveDate = x.Element("LeaveDate").Value,
                     EmployeeNo = x.Element("EmployeeNo").Value,
                     EmployeeNum = x.Element("EmployeeNum").Value,
                     PYShort = x.Element("PYShort").Value,
                     EmployeeName = x.Element("EmployeeName").Value,
                     SexName = x.Element("SexName").Value,
                     //ContractKind = x.Element("ContractKind").Value,
                     DeptName = x.Element("DeptName").Value,
                     //AdminLevelName = x.Element("AdminLevelName").Value,
                     QuarterName = x.Element("QuarterName").Value,
                     PositionName = x.Element("PositionName").Value,
                     EnterDate = x.Element("EntryDate").Value,
                     //TotalTime = x.Element("TotalYear").Value
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
            string employNos = request.QueryString["DeleteNOs"].Trim();
            JsonClass jc;
            //执行删除
            bool isSucc = EmployeeInfoBus.DeleteEmployeeInfo(employNos);
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