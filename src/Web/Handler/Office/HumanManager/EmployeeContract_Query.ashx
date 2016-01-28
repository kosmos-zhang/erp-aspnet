<%@ WebHandler Language="C#" Class="EmployeeContract_Query" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/29
 * 描    述： 合同列表
 * 修改日期： 2009/04/29
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

public class EmployeeContract_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
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
            //要排序的字段，如果为空，默认为"ModifiedDate"
            string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";
            //降序时如果设置为降序
            if (string.IsNullOrEmpty(orderString) || orderString.EndsWith("_d"))
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
            EmployeeContractModel searchModel = new EmployeeContractModel();
            //设置查询条件
            //编号
            searchModel.ContractNo = request.QueryString["ContractNo"];
            //主题
            searchModel.Title = request.QueryString["Title"];
            //员工
            searchModel.EmployeeID = request.QueryString["EmployeeID"];

            //查询数据
            DataTable dtContract = EmployeeContractBus.SearchEmployeeContractInfo(searchModel);
            //转化数据
            XElement dsXML = PageListCommon.ConvertDataTableToXML(dtContract, "Data");
            //linq排序
            var dsLinq =
                (orderBy == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderByCol).Value ascending
                 select new EmployeeContractModel()
                 {
                     ID = x.Element("ID").Value,//ID
                     ContractNo = x.Element("ContractNo").Value,//合同编号
                     Title = x.Element("Title").Value,//主题
                     EmployeeNo = x.Element("EmployeeNo").Value,//员工编号
                     EmployeeName = x.Element("EmployeeName").Value,//员工姓名
                     SigningDate = x.Element("SigningDate").Value,//签约时间
                     StartDate = x.Element("StartDate").Value,//生效时间
                     EndDate = x.Element("EndDate").Value,//失效时间
                     ModifiedDate = x.Element("ModifiedDate").Value//最后更新时间
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderByCol).Value descending
                 select new EmployeeContractModel()
                 {
                     ID = x.Element("ID").Value,//ID
                     ContractNo = x.Element("ContractNo").Value,//合同编号
                     Title = x.Element("Title").Value,//主题
                     EmployeeNo = x.Element("EmployeeNo").Value,//员工编号
                     EmployeeName = x.Element("EmployeeName").Value,//员工姓名
                     SigningDate = x.Element("SigningDate").Value,//签约时间
                     StartDate = x.Element("StartDate").Value,//生效时间
                     EndDate = x.Element("EndDate").Value,//失效时间
                     ModifiedDate = x.Element("ModifiedDate").Value//最后更新时间
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
            bool isSucc = EmployeeContractBus.DeleteEmployeeContractInfo(Nos);
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