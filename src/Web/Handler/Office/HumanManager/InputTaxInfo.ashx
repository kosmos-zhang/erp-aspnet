<%@ WebHandler Language="C#" Class="InputTaxInfo" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/05/18
 * 描    述： 查询人才代理列表
 * 修改日期： 2009/05/18
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

public class InputTaxInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取操作类型
        //string action = context.Request.Params["Action"].ToString();
        ////查询操作
        //if ("GetInfo".Equals(action))
        //{
        //    //查询信息
        //    SearchTaxInfo(context);
        //}
    }

    ///// <summary>
    ///// 查询计算个人所得税
    ///// </summary>
    ///// <param name="context">请求上下文</param>
    //private void SearchTaxInfo(HttpContext context)
    //{
    //    HttpRequest request = context.Request;
    //    //从请求中获取排序列
    //    string orderString = request.Params["OrderBy"].ToString();

    //    //排序：默认为升序
    //    string orderBy = "ascending";
    //    //要排序的字段，如果为空，默认为"EmployeeNo"
    //    string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "EmployeeNo";
    //    //降序时如果设置为降序
    //    if (orderString.EndsWith("_d"))
    //    {
    //        //排序：降序
    //        orderBy = "descending";
    //    }
    //    //从请求中获取当前页
    //    int pageIndex = int.Parse(request.Params["PageIndex"]);
    //    //从请求中获取每页显示记录数
    //    int pageCount = int.Parse(request.Params["PageCount"]);
    //    //跳过记录数
    //    int skipRecord = (pageIndex - 1) * pageCount;

    //    //获取数据
    //    EmployeeSearchModel model = new EmployeeSearchModel();
    //    //设置查询条件
    //    //员工编号
    //    model.EmployeeNo = request.Params["EmployeeNo"].ToString();
    //    //工号
    //    model.EmployeeNum = request.Params["EmployeeNum"].ToString();
    //    //姓名
    //    model.EmployeeName = request.Params["EmployeeName"].ToString();
    //    //所在岗位
    //    model.QuarterID = request.Params["QuarterID"].ToString();
    //    //岗位职等
    //    model.AdminLevelID = request.Params["AdminLevelID"].ToString();
    //    //年月
    //    string salaryMonth = request.Params["SalaryMonth"].ToString();

    //    //查询数据
    //    DataTable dtSalary = TaxCalculateBus.CalculateEmployeeTax(null, salaryMonth.Replace("-", string.Empty), model);
    //    //转化数据
    //    XElement dsXML = PageListCommon.ConvertDataTableToXML(dtSalary, "Data");
    //    //linq排序
    //    var dsLinq =
    //        (orderBy == "ascending") ?
    //        (from x in dsXML.Descendants("Data")
    //         orderby x.Element(orderByCol).Value ascending
    //         select new SalaryTaxInfo()
    //         {
    //             EmployeeNo = x.Element("EmployeeNo").Value,//人员编号
    //             EmployeeName = x.Element("EmployeeName").Value,//姓名 
    //             QuarterName = x.Element("QuarterName").Value,//岗位
    //             DeptName = x.Element("DeptName").Value,//部门
    //             AdminLevelName = x.Element("AdminLevelName").Value,//岗位职等
    //             TotalSalary = x.Element("TotalSalary").Value,//工资合计
    //             TaxRate = x.Element("TaxRate").Value,//税率
    //             TotalTax = x.Element("TotalTax").Value//税额
    //         })
    //                  :
    //        (from x in dsXML.Descendants("Data")
    //         orderby x.Element(orderByCol).Value descending
    //         select new SalaryTaxInfo()
    //         {
    //             EmployeeNo = x.Element("EmployeeNo").Value,//人员编号
    //             EmployeeName = x.Element("EmployeeName").Value,//姓名 
    //             QuarterName = x.Element("QuarterName").Value,//岗位
    //             DeptName = x.Element("DeptName").Value,//部门
    //             AdminLevelName = x.Element("AdminLevelName").Value,//岗位职等
    //             TotalSalary = x.Element("TotalSalary").Value,//工资合计
    //             TaxRate = x.Element("TaxRate").Value,//税率
    //             TotalTax = x.Element("TotalTax").Value//税额
    //         });
    //    //获取记录总数
    //    int totalCount = dsLinq.Count();
    //    //定义返回字符串变量
    //    StringBuilder sbReturn = new StringBuilder();
    //    //设置记录总数
    //    sbReturn.Append("{");
    //    sbReturn.Append("totalCount:");
    //    sbReturn.Append(totalCount.ToString());
    //    //设置数据
    //    sbReturn.Append(",data:");
    //    sbReturn.Append(PageListCommon.ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
    //    sbReturn.Append("}");
    //    //设置输出流的 HTTP MIME 类型
    //    context.Response.ContentType = "text/plain";
    //    //向响应中输出数据
    //    context.Response.Write(sbReturn.ToString());
    //    context.Response.End();
    //}

    /// <summary>
    /// 设备汇总数据模型
    /// </summary>
    protected class SalaryTaxInfo
    {
        public string EmployeeNo { get; set; }//人员编号
        public string EmployeeName { get; set; }//姓名
        public string QuarterName { get; set; }//岗位
        public string DeptName { get; set; }//部门
        public string AdminLevelName { get; set; }//岗位职等
        public string TotalSalary { get; set; }//工资合计
        public string TaxRate { get; set; }//税率
        public string TotalTax { get; set; }//税额

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}