using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using XBase.Common;
using XBase.Business.Office.SellManager;
using XBase.Model.Office.SellManager;

public partial class Pages_Office_SellManager_SellOrderList : BasePage
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
            //扩展属性
            GetBillExAttrControl1.TableName = "officedba.SellOrder";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            GetBillExAttrControl1.ExtIndex = EFIndex;
            GetBillExAttrControl1.ExtValue = EFDesc;
            GetBillExAttrControl1.SetExtControlValue();

            btnImport.Attributes["onclick"] = "return fnIsSearch();";
            // 小数位数
            _selPoint = int.Parse(UserInfo.SelPoint);
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        //设置行为参数
        string orderString = hiddExpOrder.Value.Trim();//排序
        string order = "desc";//排序：降序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "OrderDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：升序
        }
        int pageCount = int.Parse(hiddExpTotal.Value);//每页显示记录数
        int pageIndex = 1;//当前页     
        int TotalCount = 0;//总记录数
        string ord = orderBy + " " + order;//排序字段
        DataTable dt = new DataTable();

        string strorderNo = hiddExpOrderNo.Value.Trim();
        string strTitle = hiddExpTitle.Value.Trim();
        string strTotalPrice = hiddExpTotalPrice.Value.Trim();
        string strTotalPrice1 = hiddExpTotalPrice1.Value.Trim();
        string strFromType = hiddExpFromType.Value.Trim();
        string strBillStatus = hiddExpBillStatus.Value.Trim();
        string strCustID = hiddExpCustID.Value.Trim();
        string strSeller = hiddExpSeller.Value.Trim();
        string strisOpenbill = hiddExpIsOpenbill.Value.Trim();
        string strFlowStatus = hiddExpFlowStatus.Value.Trim();
        string strFromBillID = hiddExpFromBillID.Value.Trim();
        string strSendPro = hiddExpSendPro.Value.Trim();
        string strProjectID = hiddProjectID.Value.Trim();

        string orderNo = strorderNo.Length == 0 ? null : strorderNo;
        string Title = strTitle.Length == 0 ? null : strTitle;
        decimal? TotalPrice = strTotalPrice.Length == 0 ? null : (decimal?)Convert.ToDecimal(strTotalPrice);
        decimal? TotalPrice1 = strTotalPrice1.Length == 0 ? null : (decimal?)Convert.ToDecimal(strTotalPrice1);
        string FromType = strFromType.Length == 0 ? null : strFromType;
        string BillStatus = strBillStatus.Length == 0 ? null : strBillStatus;
        int? CustID = strCustID.Length == 0 ? null : (int?)Convert.ToInt32(strCustID);
        int? Seller = strSeller.Length == 0 ? null : (int?)Convert.ToInt32(strSeller);
        string isOpenbill = strisOpenbill.Length == 0 ? null : strisOpenbill;
        int? FlowStatus = strFlowStatus.Length == 0 ? null : (int?)Convert.ToInt32(strFlowStatus);
        int? FromBillID = strFromBillID.Length == 0 ? null : (int?)Convert.ToInt32(strFromBillID);
        string SendPro = strSendPro.Length == 0 ? null : strSendPro;
        int? ProjectID = strProjectID.Length == 0 ? null : (int?)Convert.ToInt32(strProjectID);
        //扩展属性
        string EFIndex = Request.QueryString["EFIndex"];
        string EFDesc = Request.QueryString["EFDesc"];
        GetBillExAttrControl1.ExtIndex = EFIndex;
        GetBillExAttrControl1.ExtValue = EFDesc;
        GetBillExAttrControl1.SetExtControlValue();

        SellOrderModel model = new SellOrderModel();
        model.OrderNo = orderNo;
        model.Title = Title;
        model.TotalPrice = TotalPrice;
        model.FromType = FromType;
        model.FromBillID = FromBillID;
        model.BillStatus = BillStatus;
        model.CustID = CustID;
        model.Seller = Seller;
        model.isOpenbill = isOpenbill;
        model.ProjectID = ProjectID;

        dt = SellOrderBus.GetOrderList(model, TotalPrice1, SendPro, FlowStatus,EFIndex,EFIndex, pageIndex, pageCount, ord, ref TotalCount);
        //导出标题
        string headerTitle = "订单编号|订单主题|客户|源单类型|源单编号|订单日期|总金额|是否已建单|发货情况|回款金额|单据状态|审批状态";
        //string headerTitle = "建档日期|启用状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "OrderNo|Title|CustName|FromTypeText|FromBillNo|OrderDate|RealTotal|isOpenbillText|isSendText|YAccounts|BillStatusText|FlowInstanceText";
        //string columnFiled = "CreateDate|strUsedStatus";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "销售订单列表");
    }
}
