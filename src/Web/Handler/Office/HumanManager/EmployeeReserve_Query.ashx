<%@ WebHandler Language="C#" Class="EmployeeReserve_Query" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/23
 * 描    述： 查询人才储备列表
 * 修改日期： 2009/03/23
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

public class EmployeeReserve_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            EmployeeSearchModel searchModel = new EmployeeSearchModel();
            //设置查询条件
            //编号
            searchModel.EmployeeNo = request.QueryString["EmployeeNo"].Trim();
            //拼音缩写
            searchModel.PYShort = request.QueryString["PYShort"].Trim();
            //姓名
            searchModel.EmployeeName = request.QueryString["EmployeeName"].Trim();
            //性别
            string sex = request.QueryString["Sex"].Trim();
            searchModel.SexID = sex;
            //文化程度 
            searchModel.CultureLevel = request.QueryString["CultureID"].Trim();
            searchModel.ProfessionalID = request.QueryString["ProfessionalID"];
            //毕业院校
            searchModel.SchoolName = request.QueryString["SchoolName"].Trim();
            //应聘岗位
            searchModel.PositionTitle = request.QueryString["PositionTitle"].Trim();
            //工龄
            searchModel.TotalSeniority = request.QueryString["TotalSeniority"].Trim();
            //手机号码
            searchModel.Mobile = request.QueryString["Mobile"].Trim();

            //查询数据
            DataTable dtEmployee = EmployeeInfoBus.SearchEmployeeReserveInfo(searchModel);
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
                     PYShort = x.Element("PYShort").Value,//拼音缩写
                     EmployeeName = x.Element("EmployeeName").Value,//姓名
                     SexName = x.Element("SexName").Value,//性别
                     Birth = x.Element("Birth").Value,//出身年月
                     Age = x.Element("Age").Value,//年龄
                     Origin = x.Element("Origin").Value,//籍贯 
                     ProfessionalName = x.Element("ProfessionalName").Value,//专业
                    // TotalSeniority = x.Element("TotalSeniority").Value,//工龄
                     PositionTitle = x.Element("PositionTitle").Value//应聘岗位 
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderByCol).Value descending
                 select new EmployeeSearchModel()
                 {
                     ID = x.Element("ID").Value,//ID
                     EmployeeNo = x.Element("EmployeeNo").Value,//编号
                     PYShort = x.Element("PYShort").Value,//拼音缩写
                     EmployeeName = x.Element("EmployeeName").Value,//姓名
                     SexName = x.Element("SexName").Value,//性别
                     Birth = x.Element("Birth").Value,//出身年月
                     Age = x.Element("Age").Value,//年龄
                     Origin = x.Element("Origin").Value,//籍贯 
                     ProfessionalName = x.Element("ProfessionalName").Value,//专业
                    // TotalSeniority = x.Element("TotalSeniority").Value,//工龄
                     PositionTitle = x.Element("PositionTitle").Value//应聘岗位 
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
            JsonClass jc;
            //获取删除的ID
            string employNos = request.QueryString["DeleteNOs"].Trim();
            bool isSucc = false;
            
            switch (action)
            {
                case "CallBackList":
                    EmpCallBackList(context);
                    break;
                case "callback":
                    isSucc = EmployeeInfoBus.CallBack(employNos);
                    //成功时
                    if (isSucc)
                    {
                        jc = new JsonClass("", "", 1);
                    }
                    //未成功时
                    else
                    {
                        jc = new JsonClass("", "", 0);
                    }
                    //输出响应
                    context.Response.Write(jc);
                    break;
                    
                case "delete":
                     isSucc = EmployeeInfoBus.DeleteEmp(employNos);
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
                    break;
                    
                default:
                                      
                    //执行删除
                     isSucc = EmployeeInfoBus.DeleteEmployeeInfo(employNos);
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
                    break;
            }
            
        }
    }

private void EmpCallBackList(HttpContext context)
{
    //从请求中获取排序列
    string orderString = context.Request.QueryString["OrderBy"];

            //排序：默认为升序
    string orderBy = "desc";
            //要排序的字段，如果为空，默认为"ID"
            string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";
            //降序时如果设置为降序
            if (orderString.EndsWith("_d"))
            {
                //排序：降序
                orderBy = "asc";
            }
            //从请求中获取当前页
            int pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);
            //从请求中获取每页显示记录数
            int pageCount = int.Parse(context.Request.QueryString["PageCount"]);
            //跳过记录数
            int skipRecord = (pageIndex - 1) * pageCount;

            //获取数据
            EmployeeSearchModel searchModel = new EmployeeSearchModel();
            //设置查询条件
            //编号
            searchModel.EmployeeNo = context.Request.QueryString["EmployeeNo"].Trim();
            //拼音缩写
            searchModel.PYShort = context.Request.QueryString["PYShort"].Trim();
            //姓名
            searchModel.EmployeeName = context.Request.QueryString["EmployeeName"].Trim();
            //性别
            string sex = context.Request.QueryString["Sex"].Trim();
            searchModel.SexID = sex;
            //文化程度 
            searchModel.CultureLevel = context.Request.QueryString["CultureID"].Trim();
            searchModel.ProfessionalID = context.Request.QueryString["ProfessionalID"];
            //毕业院校
            searchModel.SchoolName = context.Request.QueryString["SchoolName"].Trim();
            //应聘岗位
            searchModel.PositionTitle = context.Request.QueryString["PositionTitle"].Trim();
            //工龄
            searchModel.TotalSeniority = context.Request.QueryString["TotalSeniority"].Trim();
            //手机号码
            searchModel.Mobile = context.Request.QueryString["Mobile"].Trim();

            string ord = orderByCol + " " + orderBy;
    int totalCount = 0;
            //查询数据
    DataTable dtEmployee = EmployeeInfoBus.SearchEmployeeCallBack(searchModel, pageIndex, pageCount, ord, ref totalCount);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(totalCount.ToString());
            sb.Append(",data:");
            if (dtEmployee.Rows.Count == 0)
                sb.Append("[{\"ID\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dtEmployee));
            sb.Append("}");

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