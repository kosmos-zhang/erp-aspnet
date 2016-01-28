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

public partial class Pages_Office_PurchaseManager_PurchasePlanInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
        if (!IsPostBack)
        {
            GetBillExAttrControl1.TableName = "officedba.PurchasePlan";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            GetBillExAttrControl1.ExtIndex = EFIndex;
            GetBillExAttrControl1.ExtValue = EFDesc;
            GetBillExAttrControl1.SetExtControlValue();
        }
        #region 绑定用于审批
        #endregion
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        PurchasePlanModel PurchasePlanM = new PurchasePlanModel();
        PurchasePlanM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        PurchasePlanM.PlanNo = txtPlanNo.Value;
        PurchasePlanM.Title = txtPlanTitle.Value;

        PurchasePlanM.PlanUserID = PlanUserID.Value;
        PurchasePlanM.PlanMoney = txtTotalMoneyMin.Value;
        PurchasePlanM.TotalMoneyMax = txtTotalMoneyMax.Value;
        PurchasePlanM.PlanDeptID = txtDeptID.Value;
        PurchasePlanM.PlanDate = txtStartPlanDate.Value;
        PurchasePlanM.EndPlanDate = txtEndPlanDate.Value;
        PurchasePlanM.BillStatus = ddlFlowStatus.Value;
        PurchasePlanM.FlowStatus = ddlFlowStatus.Value;
        PurchasePlanM.EFIndex = GetBillExAttrControl1.GetExtIndexValue;
        PurchasePlanM.EFDesc = GetBillExAttrControl1.GetExtTxtValue;
        string OrderBy = hidOrderBy.Value;
        //XElement dsXML = ConvertDataTableToXML(PurchasePlanBus.SelectPurchasePlan(PurchasePlanM));
        DataTable dt = PurchasePlanBus.SelectPurchasePlan(PurchasePlanM,OrderBy);

        OutputToExecl.ExportToTableFormat(this, dt,
            new string[] { "计划单编号", "计划单主题", "计划员", "预控金额", "计划时间", "单据状态", "审批状态"},
            new string[] { "PlanNo", "PlanTitle", "PlanUserName", "PlanMoney", "PlanDate", "BillStatusName", "FlowStatusName"},
            "采购计划列表");
    }
}
