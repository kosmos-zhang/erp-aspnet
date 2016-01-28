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
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;

public partial class Pages_Office_PurchaseManager_PurchaseContractInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string CompanyCD = "";
        CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//
        if (!IsPostBack)
        {
            GetBillExAttrControl1.TableName = "officedba.PurchaseContract";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            GetBillExAttrControl1.ExtIndex = EFIndex;
            GetBillExAttrControl1.ExtValue = EFDesc;
            GetBillExAttrControl1.SetExtControlValue();
       
            //新建修改采购合同模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_PurchaseContract_Add;
            BindDrpTypeID();
        }
    }

    #region 导出Excel
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        PurchaseContractModel model = new PurchaseContractModel();
        string ContractNo = this.txtContractNo.Text;
        string Title = this.txtTitle.Text;
        string TypeID = this.DrpTypeID.Value;
        if (TypeID == "0")
        {
            TypeID = "";
        }
        string DeptID = this.txtHidOurDept.Value;
        string Seller = this.HidSeller.Value;
        //if (Seller == undefined)
        //{
        //    Seller = "";
        //}
        string FromType = this.ddlFromType.Value;
        if (FromType == "-1")
        {
            FromType = "";
        }
        string ProviderID = this.txtHidProviderID.Value;
        string BillStatus = this.ddlBillStatus.Value;
        if (BillStatus == "0")
        {
            BillStatus = "";
        }
        string UsedStatus = this.ddlUsedStatus.Value;
        if (UsedStatus == "0")
        {
            UsedStatus = "";
        }

        string tIndex=GetBillExAttrControl1.GetExtIndexValue ;
        string xtValue = GetBillExAttrControl1.GetExtTxtValue;

        model.CompanyCD = companyCD;
        int TotalCount = 0;
        DataTable dt = PurchaseContractBus.SelectPurchaseContract(1, 1000000, "ID", ref TotalCount, ContractNo, Title, TypeID, DeptID, Seller, FromType, ProviderID, BillStatus, UsedStatus,tIndex ,xtValue );

        

        //DataTable dt = WorkCenterBus.GetWorkCenterListBycondition(model, 1, 1000000, "ID desc", ref totalCount);

        //导出标题
        string headerTitle = "合同编号|合同主题|采购分类|采购员|供应商|金额合计|税额合计|含税金额合计|单据状态|审批状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "ContractNo|Title|TypeName|EmployeeName|CustName|TotalPrice|TotalTax|TotalFee|BillStatusName|UsedStatus";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "采购合同列表");
    }
    #endregion

    #region 绑定列表采购类别
    private void BindDrpTypeID()
    {
        DataTable dt = PurchaseContractBus.GetddlTypeID();
        if (dt != null && dt.Rows.Count > 0)
        {
            DrpTypeID.DataSource = dt;
            DrpTypeID.DataTextField = "TypeName";
            DrpTypeID.DataValueField = "ID";
            DrpTypeID.DataBind();
            ListItem Item = new ListItem("--请选择--", "");
            DrpTypeID.Items.Insert(0, Item);
        }
    }
    #endregion
}
