<%@ WebHandler Language="C#" Class="EmployeeWork_Query" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/19
 * 描    述： 查询在职人员列表
 * 修改日期： 2009/03/19
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

public class EmployeeWork_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        
        HttpRequest request = context.Request;//获取上下文的请求

        string action = request.QueryString["Action"];//从请求中获取当前操作

        //分页控件查询数据
        if ("GetInfo".Equals(action))
        {
            
            string orderString = request.QueryString["OrderBy"];//从请求中获取排序列
            string orderBy = "asc";//排序：默认为升序
            string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))//降序时如果设置为降序
            {
                orderBy = "desc";//排序：降序
            }
            
            int pageIndex = int.Parse(request.QueryString["PageIndex"]);//从请求中获取当前页
            int pageCount = int.Parse(request.QueryString["PageCount"]); //从请求中获取每页显示记录数
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            EmployeeSearchModel model = new EmployeeSearchModel();//获取数据
            model.EmployeeNo = request.QueryString["EmployeeNo"].Trim();//编号
            model.EmployeeNum = request.QueryString["EmployeeNum"].Trim();//工号
            model.PYShort = request.QueryString["PYShort"].Trim();//工号;//拼音缩写
            model.EmployeeName = request.QueryString["EmployeeName"].Trim();//姓名
            model.ContractKind = "";//request.QueryString["ContractKind"].Trim();//工种
            model.AdminLevelID = "";// request.QueryString["AdminLevel"].Trim(); //行政等级
            model.QuarterID = request.QueryString["QuarterID"].Trim(); //岗位
            model.PositionID = request.QueryString["PositionID"].Trim(); //职称
            model.EnterDate = request.QueryString["StartDate"].Trim();//入职时间
            model.EnterEndDate = request.QueryString["EnterEndDate"].Trim();
            model.Mobile = request.QueryString["Mobile"].Trim();//手机号码

            string ord = orderByCol + " " + orderBy;
            int TotalCount = 0;

            DataTable dtEmployee = EmployeeInfoBus.SearchEmployeeWorkInfo(model, pageIndex, pageCount, ord, ref TotalCount);//查询数据

            //定义返回字符串变量
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