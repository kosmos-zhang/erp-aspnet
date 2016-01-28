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
using XBase.Business.Office.StorageManager;
using XBase.Common;
using XBase.Model.Office.StorageManager;

public partial class Pages_Office_StorageManager_StorageInRedList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //启用小数位数,默认2位
        hidSelPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;

        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (!IsPostBack)
        {
            DataTable dtReason = CodeReasonTypeBus.GetReasonType(companyCD);
            if (dtReason.Rows.Count > 0)
            {
                ddlReasonType.DataSource = dtReason;
                ddlReasonType.DataTextField = "CodeName";
                ddlReasonType.DataValueField = "ID";
                ddlReasonType.DataBind();
            }
            ddlReasonType.Items.Insert(0, new ListItem("-请选择-", ""));

            //新建模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_STORAGE_STORAGEINRED_ADD;
            GetBillExAttrControl1.TableName = "officedba.StorageInRed";
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

                    txtInNo.Value = Request.QueryString["InNO"];
                    txtTitle.Value = Request.QueryString["Title"];
                    sltFromType.Value = Request.QueryString["FromType"];
                    txtFromBillID.Attributes.Add("title", Request.QueryString["FromBillID"]);
                    hidFromBillID.Value = Request.QueryString["FromBillID"];
                    txtDeptID.Value = Request.QueryString["InPutDept"];
                    sltBillStatus.Value = Request.QueryString["BillStatus"];
                    txtExecutorID.Value = Request.QueryString["Executor"];
                    txtEnterDateStart.Value = Request.QueryString["EnterDateStart"];
                    txtEnterDateEnd.Value = Request.QueryString["EnterDateEnd"];
                    DeptName.Value = Request.QueryString["DeptName"];
                    txtFromBillID.Value = Request.QueryString["FromBillNo"];
                    UserExecutor.Value = Request.QueryString["UserExecutor"];
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
        StorageInRedModel model = new StorageInRedModel();
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string EnterDateStart = string.Empty;
        string EnterDateEnd = string.Empty;
        model.InNo = txtInNo.Value;
        model.Title = txtTitle.Value;
        model.ReasonType = ddlReasonType.SelectedValue;
        if (hidFromBillID.Value == "undefined")
        {
            hidFromBillID.Value = "";
        }
        model.FromBillID = hidFromBillID.Value;
        model.DeptID = txtDeptID.Value;
        model.BillStatus = sltBillStatus.Value;
        model.Executor = txtExecutorID.Value;
        model.FromType = sltFromType.Value;
        EnterDateStart = txtEnterDateStart.Value;
        EnterDateEnd = txtEnterDateEnd.Value;
        string BatchNo = txtBatchNo.Value;
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
        DataTable dt = StorageInRedBus.GetStorageInRedTableBycondition(BatchNo,model, EnterDateStart, EnterDateEnd, orderBy);
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsDisplayPrice)
        {
            OutputToExecl.ExportToTableFormat(this, dt,
    new string[] { "单据编号", "单据主题", "源单类型", "源单编号", "入库部门", "入库人", "入库时间", "入库原因", "红冲数量", "红冲金额", "摘要", "单据状态" },
    new string[] { "InNo", "Title", "FromType", "FromInNo", "DeptName", "ExecutorName", "EnterDate", "CodeName", "CountTotal", "TotalPrice", "Summary", "BillStatusName" },
    "红冲入库列表");
        }
        else
        {
            OutputToExecl.ExportToTableFormat(this, dt,
               new string[] { "单据编号", "单据主题", "源单类型", "源单编号", "入库部门", "入库人", "入库时间", "入库原因", "红冲数量",  "摘要", "单据状态" },
               new string[] { "InNo", "Title", "FromType", "FromInNo", "DeptName", "ExecutorName", "EnterDate", "CodeName", "CountTotal", "Summary", "BillStatusName" },
               "红冲入库列表");
        }

    }
}
