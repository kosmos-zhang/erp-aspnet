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
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;

public partial class Pages_Office_PurchaseManager_PurchaseApplyInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        #region 采购类别
        ddlTypeID.TypeCode = "5";
        ddlTypeID.TypeFlag = "7";
        ddlTypeID.IsInsertSelect = true;
        GetBillExAttrControl1.TableName = "officedba.PurchaseApply";
        #endregion  
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        PurchaseApplyModel PurchaseApplyM = new PurchaseApplyModel();
        PurchaseApplyM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        PurchaseApplyM.ApplyNo = txtapplyno.Value;
        PurchaseApplyM.Title = txttitle.Value;
        PurchaseApplyM.ApplyUserID = hidApplyID.Value;
        PurchaseApplyM.ApplyDeptID = hidDeptID.Value;
        PurchaseApplyM.TypeID = ddlTypeID.SelectedValue;
        PurchaseApplyM.FromType = ddlfromtype.Value;
        PurchaseApplyM.StartApplyDate = txtstartapplydate.Value;
        PurchaseApplyM.EndApplyDate = txtendapplydate.Value;
        PurchaseApplyM.BillStatus = ddlBillStatus.Value;
        PurchaseApplyM.FlowStatus = ddlFlowStatus.Value;
        PurchaseApplyM.EFIndex = GetBillExAttrControl1.GetExtIndexValue;
        PurchaseApplyM.EFDesc = GetBillExAttrControl1.GetExtTxtValue;

        string OrderBy = hidOrderBy.Value;
        DataTable dt = PurchaseApplyBus.SelectPurchaseApply(PurchaseApplyM, OrderBy);


        OutputToExecl.ExportToTableFormat(this, dt,
            new string[] { "申请单编号", "申请单主题", "采购类别", "源单类型", "申请人", "申请部门", "申请时间", "单据状态", "审批状态" },
            new string[] { "ApplyNo", "Title", "TypeName", "FromTypeName", "ApplyUserName", "ApplyDeptName", "ApplyDate", "BillStatusName", "FlowStatusName" },
            "采购申请列表");
    }
}
