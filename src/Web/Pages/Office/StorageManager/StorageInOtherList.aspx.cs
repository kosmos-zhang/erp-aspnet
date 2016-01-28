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


public partial class Pages_Office_StorageManager_StorageInOtherList : BasePage
{
    private string companyCD = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (!IsPostBack)
        {
            //启用小数位数,默认2位
            hidSelPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;

            StorageModel model = new StorageModel();
            model.CompanyCD = companyCD;
            model.UsedStatus = "1";
            DataTable dt = StorageBus.GetStorageListBycondition(model);
            if (dt.Rows.Count > 0)
            {
                sltStorageID.DataSource = dt;
                sltStorageID.DataTextField = "StorageName";
                sltStorageID.DataValueField = "ID";
                sltStorageID.DataBind();
                sltStorageID.Items.Insert(0, new ListItem("--请选择--", ""));
            }

            //新建模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_STORAGE_STORAGEINOTHER_ADD;
            GetBillExAttrControl1.TableName = "officedba.StorageInOther";
            //返回处理
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
                    txtTakerID.Value = Request.QueryString["Taker"];
                    txtCheckerID.Value = Request.QueryString["Checker"];
                    txtDeptID.Value = Request.QueryString["InPutDept"];
                    sltBillStatus.Value = Request.QueryString["sltBillStatus"];
                    txtExecutorID.Value = Request.QueryString["Executor"];
                    txtEnterDateStart.Value = Request.QueryString["EnterDateStart"];
                    txtEnterDateEnd.Value = Request.QueryString["EnterDateEnd"];
                    sltStorageID.SelectedValue = Request.QueryString["StorageID"];
                    UserTaker.Value = Request.QueryString["UserTaker"];
                    UserChecker.Value = Request.QueryString["UserChecker"];
                    DeptName.Value = Request.QueryString["DeptName"];
                    UserExecutor.Value = Request.QueryString["UserExecutor"];
                    txtBatchNo.Value = Request.QueryString["BatchNo"];
                    txtSelProject.Value = Request.QueryString["SelProjectName"];
                    HidProjectID.Value = Request.QueryString["ProjectID"];
                    //txtInNo.Attributes.Add("title", Request.QueryString["InNO"]);

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
        StorageInOtherModel model = new StorageInOtherModel();
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string EnterDateStart = string.Empty;
        string EnterDateEnd = string.Empty;
        model.InNo = txtInNo.Value;
        model.Title = txtTitle.Value;
        model.DeptID = txtDeptID.Value;
        model.BillStatus = sltBillStatus.Value;
        model.Executor = txtExecutorID.Value;
        model.Taker = txtTakerID.Value;
        model.Checker = txtCheckerID.Value;
        EnterDateStart = txtEnterDateStart.Value;
        EnterDateEnd = txtEnterDateEnd.Value;
        string StorageID = sltStorageID.SelectedValue;
        string orderBy = txtorderBy.Value;
        string BatchNo = txtBatchNo.Value;
        model.ProjectID = HidProjectID.Value;
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

        DataTable dt = StorageInOtherBus.GetStorageInOtherTableBycondition(BatchNo,model, StorageID, EnterDateStart, EnterDateEnd, orderBy);
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsDisplayPrice)
        {
            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "单据编号", "单据主题", "交货人", "验收人", "入库部门", "入库人", "入库日期", "入库原因", "入库数量", "入库金额", "摘要", "单据状态" },
                new string[] { "InNo", "Title", "TakerName", "CheckerName", "InPutDept", "ExecutorName", "EnterDate", "ReasonTypeName", "CountTotal", "TotalPrice", "Summary", "BillStatusName" },
                "其他入库列表");
        }
        else
        {
            OutputToExecl.ExportToTableFormat(this, dt,
                        new string[] { "单据编号", "单据主题", "交货人", "验收人", "入库部门", "入库人", "入库日期", "入库原因", "入库数量", "摘要", "单据状态" },
                        new string[] { "InNo", "Title", "TakerName", "CheckerName", "InPutDept", "ExecutorName", "EnterDate", "ReasonTypeName", "CountTotal", "Summary", "BillStatusName" },
                        "其他入库列表");
        }

    }
}
