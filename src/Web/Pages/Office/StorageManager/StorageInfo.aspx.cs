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

public partial class Pages_Office_StorageManager_StorageInfo : BasePage
{
    private string companyCD = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (!IsPostBack)
        {
            //新建模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_STORAGE_STORAGEINFO;

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
                txtStorageNo.Value = Request.QueryString["StorageNo"];
                txtStorageName.Value = Request.QueryString["StorageName"];
                sltType.Value = Request.QueryString["StorageType"];
                sltUsedStatus.Value = Request.QueryString["UsedStatus"];
                //获取当前页
                string pageIndex = Request.QueryString["pageIndex"];
                //获取每页显示记录数 
                string pageCount = Request.QueryString["pageCount"];
                //执行查询
                ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                        , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");DoSearch('" + pageIndex + "');</script>");
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "DoSearch", "DoSearch()", true);
                //ClientScript.RegisterStartupScript(this.GetType(), "DoSearch", "DoSearch()");
                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "DoSearch", "DoSearch()");
            }
        }
    }

    #region 导出到Excel

    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        StorageModel model = new StorageModel();
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.StorageNo = txtStorageNo.Value.Trim();
        model.StorageName = txtStorageName.Value.Trim();
        model.StorageType = sltType.Value;
        model.UsedStatus = sltUsedStatus.Value;
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
        DataTable dt = StorageBus.GetStorageListBycondition(model, orderBy);

        foreach (DataRow dr in dt.Rows)
        {
            if (dr["StorageType"].ToString() == "0")
            {
                dr["StorageType"] = "一般库";
            }
            else if (dr["StorageType"].ToString() == "1")
            {
                dr["StorageType"] = "委托代销库";
            }
            else if (dr["StorageType"].ToString() == "2")
            {
                dr["StorageType"] = "贵重物品库";
            }

            if (dr["UsedStatus"].ToString() == "1")
            {
                dr["UsedStatus"] = "已启用";
            }
            else
            {
                dr["UsedStatus"] = "未启用";
            }
        }
        OutputToExecl.ExportToTableFormat(this, dt,
            new string[] { "仓库编号", "仓库名称", "仓库类型", "仓库状态", "仓管员","仓库说明" },
            new string[] { "StorageNo", "StorageName", "StorageType", "UsedStatus", "CanViewUserName", "Remark" },
            "仓库列表");

    }
    #endregion
}
