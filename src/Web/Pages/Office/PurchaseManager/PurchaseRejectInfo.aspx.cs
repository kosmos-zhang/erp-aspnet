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

public partial class Pages_Office_PurchaseManager_PurchaseRejectInfo : BasePage
{
    /// <summary>
    /// 小数位数
    /// </summary>
    private int _selPoint = 2;

    /// <summary>
    /// 小数位数
    /// </summary>
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

        UserInfo = SessionUtil.Session["UserInfo"] as UserInfoUtil;
        // 小数位数
        _selPoint = int.Parse(UserInfo.SelPoint);

        if (!Page.IsPostBack)
        {
            //新建修改采购合同模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_PurchaseReject_Add;
            BinddrpTypeID();
            GetBillExAttrControl1.TableName = "officedba.PurchaseReject";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            GetBillExAttrControl1.ExtIndex = EFIndex;
            GetBillExAttrControl1.ExtValue = EFDesc;
            GetBillExAttrControl1.SetExtControlValue();
        }
    }

    #region 导出Excel
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {


        string companyCD = UserInfo.CompanyCD;
        PurchaseContractModel model = new PurchaseContractModel();
        string RejectNo = this.txtArriveNo.Text;
        string Title = this.txtTitle.Text;
        string TypeID = this.drpTypeID.Value;
        if (TypeID == "0")
        {
            TypeID = "";
        }
        string Purchaser = this.HidPurchaser.Value;
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
        string DeptID = this.HidDeptID.Value;

        model.CompanyCD = companyCD;
        int TotalCount = 0;

        string EFIndex = GetBillExAttrControl1.GetExtIndexValue;
        string EFDesc = GetBillExAttrControl1.GetExtTxtValue;

        DataTable dt = PurchaseRejectBus.SelectPurchaseReject(1, 1000000, "ID", ref TotalCount, RejectNo, Title, TypeID, Purchaser, FromType, ProviderID, BillStatus, UsedStatus, DeptID, this.hidProjectID.Value, EFIndex, EFDesc);




        //导出标题
        string headerTitle = "单据编号|单据主题|采购分类|采购员|供应商|源单类型|应退货款合计|单据状态|审批状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "RejectNo|Title|TypeName|PurchaserName|ProviderName|FromTypeName|TotalYthkhj|BillStatusName|UsedStatus";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "采购退货单列表");
    }
    #endregion

    #region 绑定列表采购类别
    private void BinddrpTypeID()
    {
        DataTable dt = PurchaseRejectBus.GetddlTypeID();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpTypeID.DataSource = dt;
            drpTypeID.DataTextField = "TypeName";
            drpTypeID.DataValueField = "ID";
            drpTypeID.DataBind();
            ListItem Item = new ListItem("--请选择--", "");
            drpTypeID.Items.Insert(0, Item);
        }
    }
    #endregion
}
