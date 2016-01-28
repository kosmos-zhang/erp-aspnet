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
using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;
using XBase.Common;

public partial class Pages_Office_StorageManager_StorageOutRedList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //新建模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_STORAGE_STORAGEOUTRED_ADD;
            GetBillExAttrControl1.TableName = "officedba.StorageOutRed";
            HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsDisplayPrice)
                IsDisplayPrice.Value = "true";
            else
                IsDisplayPrice.Value = "false";

            //返回处理

            string requestParam = Request.QueryString.ToString();
            //从列表过来时
            int firstIndex = requestParam.IndexOf("&");
            //返回回来时
            if (firstIndex > 0)
            {
                //获取是否查询的标识
                string flag = Request.QueryString["Flag"];
                //点击查询时，设置查询的条件，并执行查询
                if ("1".Equals(flag))
                {
                    txtOutNo.Value = Request.QueryString["OutNo"];
                    txtTitle.Value = Request.QueryString["Title"];
                    sltFromType.Value = Request.QueryString["FromType"];
                    txtFromBillID.Attributes.Add("title", Request.QueryString["FromOutID"]);
                    hidFromBillID.Value = Request.QueryString["FromOutID"];
                    sltBillStatus.Value = Request.QueryString["BillStatus"];
                    txtOuterID.Value = Request.QueryString["Transactor"];
                    txtOutDateStart.Value = Request.QueryString["OutDateStart"];
                    txtOutDateEnd.Value = Request.QueryString["OutDateEnd"];
                    txtFromBillID.Value = Request.QueryString["FromOutNo"];
                    UserOuter.Value = Request.QueryString["UserOuter"];
                    txtBatchNo.Value = Request.QueryString["BatchNo"];
                    //获取当前页
                    string pageIndex = Request.QueryString["pageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["pageCount"];

                    string EFIndex = Request.QueryString["EFIndex"];
                    string EFDesc = Request.QueryString["EFDesc"];

                    GetBillExAttrControl1.ExtIndex = EFIndex;
                    GetBillExAttrControl1.ExtValue = EFDesc;
                    GetBillExAttrControl1.SetExtControlValue();
                    //执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");DoSearch('" + pageIndex + "');</script>");
                }
            }
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {

        StorageOutRedModel model = new StorageOutRedModel();
        string OutDateStart = string.Empty;
        string OutDateEnd = string.Empty;
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.OutNo = txtOutNo.Value;
        model.Title = txtTitle.Value;
        model.BillStatus = sltBillStatus.Value;
        model.FromType = sltFromType.Value;
        model.Executor = txtOuterID.Value;
        OutDateStart = txtOutDateStart.Value;
        OutDateEnd = txtOutDateEnd.Value;
        model.FromBillID = hidFromBillID.Value;
        string orderBy = txtorderBy.Value;
        if (!string.IsNullOrEmpty(orderBy))
        {
            if (orderBy.Split('_')[1] == "a")
            {
                orderBy = orderBy.Split('_')[0] + " asc";
            }
            else
            {
                orderBy = orderBy.Split('_')[0] + " desc";
            }
        }
        string IndexValue = GetBillExAttrControl1.GetExtIndexValue;
        string TxtValue = GetBillExAttrControl1.GetExtTxtValue;
        string BatchNo = this.txtBatchNo.Value.ToString();

        DataTable dt = StorageOutRedBus.GetStorageOutRedTableBycondition(model, IndexValue, TxtValue, OutDateStart, OutDateEnd,BatchNo, orderBy);

        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsDisplayPrice)
            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "出库单编号", "出库单主题", "源单类型", "原始出库单", "出库人", "出库时间", "红冲数量", "红冲金额", "摘要", "单据状态" },
                new string[] { "OutNo", "Title", "FromTypeName", "FromOutNo", "Transactor", "OutDate", "CountTotal", "TotalPrice", "Summary", "BillStatusName" },
                "红冲出库列表");
        else
            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "出库单编号", "出库单主题", "源单类型", "原始出库单", "出库人", "出库时间", "红冲数量",  "摘要", "单据状态" },
                new string[] { "OutNo", "Title", "FromTypeName", "FromOutNo", "Transactor", "OutDate", "CountTotal",  "Summary", "BillStatusName" },
                "红冲出库列表");

    }
}
