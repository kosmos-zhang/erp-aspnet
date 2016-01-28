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
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using XBase.Common;

public partial class Pages_Office_PurchaseManager_PurchaseAskPriceInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//
        if (!IsPostBack)
        {
            GetBillExAttrControl1.TableName = "officedba.PurchaseAskPrice";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            GetBillExAttrControl1.ExtIndex = EFIndex;
            GetBillExAttrControl1.ExtValue = EFDesc;
            GetBillExAttrControl1.SetExtControlValue();
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        PurchaseAskPriceModel PurchaseAskPriceM = new PurchaseAskPriceModel();
        PurchaseAskPriceM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        PurchaseAskPriceM.AskNo = txtAskNo.Value;
        PurchaseAskPriceM.AskTitle = txtAskTitle.Value;
        PurchaseAskPriceM.FromType = ddlFromType.Value;
        PurchaseAskPriceM.DeptID = hidDeptID.Value;
        PurchaseAskPriceM.AskUserID = hidUserID.Value;
        PurchaseAskPriceM.BillStatus = ddlBillStatus.Value;
        PurchaseAskPriceM.ProviderID = hidProviderID.Value;
        PurchaseAskPriceM.FlowStatus = ddlFlowStatus.Value;
        PurchaseAskPriceM.AskDate = StartAskDate.Value;
        PurchaseAskPriceM.EndAskDate = EndAskDate.Value;
        PurchaseAskPriceM.EFDesc = GetBillExAttrControl1.GetExtTxtValue;
        PurchaseAskPriceM.EFIndex = GetBillExAttrControl1.GetExtIndexValue;

        string OrderBy = hidOrderBy.Value;
        DataTable dt = PurchaseAskPriceBus.GetPurchaseAskPrice(PurchaseAskPriceM,OrderBy);


        OutputToExecl.ExportToTableFormat(this, dt,
            new string[] { "询价单编号", "询价单主题", "供应商", "询价日期", "询价员", "当前询价次数", "金额合计", "税额合计", "含税金额合计", "单据状态", "审批状态" },
            new string[] { "AskNo", "AskTitle", "ProviderName", "AskDate", "AskUserName", "AskOrder", "TotalPrice", "TotalTax", "TotalFee", "BillStatusName", "FlowStatusName" },
            "采购询价单列表");
    }
}
