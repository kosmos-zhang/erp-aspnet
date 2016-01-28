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
using XBase.Common;
using XBase.Business.Office.PurchaseManager;

public partial class Pages_Office_PurchaseManager_PurchaseOrderInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//
        if (!IsPostBack)
        {
            GetBillExAttrControl1.TableName = "officedba.PurchaseOrder";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            GetBillExAttrControl1.ExtIndex = EFIndex;
            GetBillExAttrControl1.ExtValue = EFDesc;
            GetBillExAttrControl1.SetExtControlValue();


            #region 采购类别
            ddlTypeID.TypeCode = "5";
            ddlTypeID.TypeFlag = "7";
            ddlTypeID.IsInsertSelect = true;
            #endregion
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        PurchaseOrderModel PurchaseOrderM = new PurchaseOrderModel();
        PurchaseOrderM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        PurchaseOrderM.OrderNo = txtOrderNo.Value;
        PurchaseOrderM.Title = txtOrderTitle.Value;
        PurchaseOrderM.TypeID = ddlTypeID.SelectedValue;
        PurchaseOrderM.DeptID = hidDeptID.Value;
        PurchaseOrderM.Purchaser = hidPurchaseID.Value;
        PurchaseOrderM.FromType = ddlFromType.Value;
        PurchaseOrderM.ProviderID = hidProviderID.Value;
        PurchaseOrderM.BillStatus = ddlBillStatus.Value;
        PurchaseOrderM.FlowStatus = ddlFlowStatus.Value;
        PurchaseOrderM.ProjectID  = this.hidProjectID .Value;
      PurchaseOrderM.EFIndex =  GetBillExAttrControl1.GetExtIndexValue ;
           PurchaseOrderM.EFDesc =    GetBillExAttrControl1.GetExtTxtValue;
        string OrderBy = hidOrderBy.Value;

        DataTable dt = PurchaseOrderBus.GetPurchaseOrder(PurchaseOrderM, OrderBy);

        OutputToExecl.ExportToTableFormat(this, dt,
            new string[] { "订单编号", "订单主题", "采购类别", "采购员", "供应商", "金额合计", "税额合计", "含税金额合计", "单据状态", "审批状态", "是否已建单" },
            new string[] { "OrderNo", "OrderTitle", "TypeName", "PurchaserName", "ProviderName", "TotalPrice", "TotalTax", "TotalFee", "BillStatusName", "FlowStatusName", "isOpenBillName" },
            "采购订单列表");
    }
}
