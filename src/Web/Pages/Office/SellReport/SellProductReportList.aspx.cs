using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using XBase.Business.Office.SellReport;
using XBase.Model.Office.SellReport;

public partial class Pages_Office_SellReport_SellProductReportList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    btnImport.Attributes["onclick"] = "return fnIsSearch();";
        //}
    }
    ////导出
    //protected void btnImport_Click(object sender, ImageClickEventArgs e)
    //{
    //    //设置行为参数
    //    string orderString = "";// GetParam("orderby").Trim();//排序
    //    string order = "desc";//排序：降序
    //    string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "createdate";//要排序的字段，如果为空，默认为"createdate"
    //    if (orderString.EndsWith("_a"))
    //    {
    //        order = "asc";//排序：升序
    //    }
    //    int pageCount = int.Parse(hiddPageCount.Value);//每页显示记录数
    //    int pageIndex = 1;//当前页
    //    //int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
    //    int totalCount = 0;
    //    string ord = orderBy + " " + order;

    //    DataTable hdt = new DataTable();
    //    SellReportModel sellRpt = new SellReportModel();
    //    sellRpt.CompanyCD = UserInfo.CompanyCD;
    //    sellRpt.SelPointLen = UserInfo.SelPoint;
    //    sellRpt.CreateDate =hiddCreateDate.Value.Trim().Length==0?null:(DateTime?)Convert.ToDateTime(hiddCreateDate.Value.Trim()); //
    //    sellRpt.SellDept = hiddDeptID.Value.Trim().Length == 0 ? null : (int?)Convert.ToInt32(hiddDeptID.Value.Trim()); //

    //    DateTime? CreateDate1 = hiddCreateDate1.Value.Trim().Length==0?null:(DateTime?)Convert.ToDateTime(hiddCreateDate1.Value.Trim()); //
    //    hdt = SellProductReportBus.GetSellRptList(sellRpt, CreateDate1, pageIndex, pageCount, ord, ref totalCount);

    //    //导出标题
    //    string headerTitle = "日期|销售部门|产品名称|销售数量|销售金额|业务员";
    //    string[] header = headerTitle.Split('|');

    //    //导出标题所对应的列字段名称
    //    string columnFiled = "createdate|SellDeptName|productName|sellNum|sellPrice|SellerRate";
    //    string[] field = columnFiled.Split('|');

    //    XBase.Common.OutputToExecl.ExportToTable(this.Page, hdt, header, field, "销售汇报列表");
    //}
}
