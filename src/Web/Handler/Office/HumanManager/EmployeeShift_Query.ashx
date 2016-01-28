<%@ WebHandler Language="C#" Class="EmployeeShift_Query" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/25
 * 描    述： 调职单列表
 * 修改日期： 2009/04/25
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

public class EmployeeShift_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            EmplApplyNotifyModel searchModel = new EmplApplyNotifyModel();
            //设置查询条件
            //调职单编号
            searchModel.NotifyNo = request.QueryString["NotifyNo"];
            //调职单主题
            searchModel.Title = request.QueryString["Title"];
            //对应申请单
            searchModel.EmplApplyNo = request.QueryString["EmplApplyNo"];
            //员工
            searchModel.EmployeeID = request.QueryString["EmployeeID"];

            //查询数据
            DataTable dtNotify = EmplApplyNotifyBus.SearchEmplApplyNotifyInfo(searchModel);
            //转化数据
            XElement dsXML = PageListCommon.ConvertDataTableToXML(dtNotify, "Data");
            //linq排序
            var dsLinq =
                (orderBy == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderByCol).Value ascending
                 select new EmplApplyNotifyModel()
                 {
                     ID = x.Element("ID").Value,//ID
                     BillStatus = x.Element("BillStatus").Value,//单据状态
                     NotifyNo = x.Element("NotifyNo").Value,//调职单编号
                     Title = x.Element("Title").Value,//调职单主题
                     EmplApplyNo = x.Element("EmplApplyNo").Value,//对应调职申请单
                     EmployeeNo = x.Element("EmployeeNo").Value,//员工编号
                     EmployeeName = x.Element("EmployeeName").Value,//员工姓名
                     NowDeptName = x.Element("NowDeptName").Value,//原部门
                     NowQuarterName = x.Element("NowQuarterName").Value,//原岗位
                     OutDate = x.Element("OutDate").Value,//调出时间
                     NewDeptName = x.Element("NewDeptName").Value,//调入部门
                     NewQuarterName = x.Element("NewQuarterName").Value,//调入岗位
                     ModifiedDate = x.Element("ModifiedDate").Value//最后更新时间
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderByCol).Value descending
                 select new EmplApplyNotifyModel()
                 {
                     ID = x.Element("ID").Value,//ID
                     BillStatus = x.Element("BillStatus").Value,//单据状态
                     NotifyNo = x.Element("NotifyNo").Value,//调职单编号
                     Title = x.Element("Title").Value,//调职单主题
                     EmplApplyNo = x.Element("EmplApplyNo").Value,//对应调职申请单
                     EmployeeNo = x.Element("EmployeeNo").Value,//员工编号
                     EmployeeName = x.Element("EmployeeName").Value,//员工姓名
                     NowDeptName = x.Element("NowDeptName").Value,//原部门
                     NowQuarterName = x.Element("NowQuarterName").Value,//原岗位
                     OutDate = x.Element("OutDate").Value,//调出时间
                     NewDeptName = x.Element("NewDeptName").Value,//调入部门
                     NewQuarterName = x.Element("NewQuarterName").Value,//调入岗位
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
            bool isSucc = EmplApplyNotifyBus.DeleteEmplApplyNotifyInfo(Nos);
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