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
using XBase.Business.Office.SellManager;

public partial class Pages_Office_SellManager_SellSendDetailsList :BasePage
{
    //小数精度
    private int _selPoint = 2;
    public int SelPoint
    {
        get
        {
            return _selPoint;
        }
        set
        {
            _selPoint = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnImport.Attributes["onclick"] = "return fnIsSearch();";
            // 小数位数
            _selPoint = int.Parse(UserInfo.SelPoint);
            hiddSendAddModuleid.Value = XBase.Common.ConstUtil.MODULE_ID_SELLSEND_ADD;//销售发货编辑页面ModuleID
            hiddBillingAddModuleid.Value = XBase.Common.ConstUtil.MODULE_ID_BILLING_ADD;//开票编辑页面MoudleID
            hiddModuleID.Value = XBase.Common.ConstUtil.MODULE_ID_SELLSENDDETAIL_LIST;//销售发货明细列表ModuleID
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        //设置行为参数
        string orderString = hiddExpOrder.Value.Trim();//排序
        string order = "desc";//排序：降序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "SendDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：升序
        }
        int pageCount = int.Parse(hiddExpTotal.Value);//每页显示记录数
        int pageIndex = 1;//当前页     
        int totalCount = 0;//总记录数
        string ord = orderBy + " " + order;//排序字段
        DataTable dt = new DataTable();

        string productID = hiddExcelProductID.Value.Trim().Length == 0 ? null : hiddExcelProductID.Value.Trim();
        string custID = hiddExcelCustID.Value.Trim().Length == 0 ? null : hiddExcelCustID.Value.Trim();
        string beginDate = hiddBeginDate.Value.Trim().Length == 0 ? null : hiddBeginDate.Value.Trim();
        string endDate = hiddEndDate.Value.Trim().Length == 0 ? null : hiddEndDate.Value.Trim();
        string isOpenBill = hiddIsOpenbill.Value.Trim().Length == 0 ? null : hiddIsOpenbill.Value.Trim();

        XBase.Model.Office.SellManager.SellSendDetailsListModel model = new XBase.Model.Office.SellManager.SellSendDetailsListModel();
        model.ProductID = productID;
        model.CustID = custID;
        model.BeginDate = beginDate;
        model.EndDate = endDate;
        model.IsOpenBill = isOpenBill;
        model.CompanyCD = UserInfo.CompanyCD;
        model.SelPointLen = UserInfo.SelPoint;
        model.IsMoreUnit = UserInfo.IsMoreUnit;
        dt = SellSendDetailsListBus.GetSellSendDetailListData(model, pageIndex, pageCount, ord, ref totalCount);

        //导出标题
        string headerTitle = "日期|客户名称|发货单编号|物品编号|品名|规格|颜色|数量|开票状态|票据类型|发票号|开票人|开票日期";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "SendDate|CustName|SendNo|ProdNo|ProductName|Specification|ColorName|ProductCount|IsOpenBillText|InvoiceTypeText|BillingNum|BillExecutorName|BillCreateDate";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "销售发货明细列表");
    }
}
