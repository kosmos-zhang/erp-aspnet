/**********************************************
 * 类作用：   人才代理列表
 * 建立人：   吴志强
 * 建立时间： 2009/03/25
 ***********************************************/
using System;
using XBase.Common;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Data;

public partial class Pages_Office_HumanManager_HRProxy_Info : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnImport.Attributes["onclick"] = "return IfExp();";

        //设置模块ID
        hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_HRPROXY_ADD;
        //获取请求参数
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
                //编号
                txtProxyNo.Value = Request.QueryString["ProxyCompanyCD"];
                //企业名称
                txtProxyName.Value = Request.QueryString["ProxyCompanyName"];
                //重要程度
                ddlImportant.SelectedValue = Request.QueryString["Important"];
                //合作关系
                ddlCooperation.SelectedValue = Request.QueryString["Cooperation"];
                //启用状态
                ddlUsedStatus.SelectedValue = Request.QueryString["UsedStatus"];

                //获取当前页
                string pageIndex = Request.QueryString["PageIndex"];
                //获取每页显示记录数 
                string pageCount = Request.QueryString["PageCount"];
                //排序 
                string orderBy = Request.QueryString["OrderBy"];
                //执行查询
                ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                        , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");"
                                + "this.orderBy = \"" + orderBy + "\";SearchProxyInfo('" + pageIndex + "');</script>");
            }
        }
    }

    protected void btnImport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {
            string orderString = hiddExpOrder.Value.Trim();//排序
            string order = "asc";//排序：降序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"

            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            string ord = " ORDER BY " + orderBy + " " + order;

            //获取数据
            HRProxyModel searchModel = new HRProxyModel();
            //设置查询条件
            //企业编号
            searchModel.ProxyCompanyCD = txtProxyNo.Value.Trim();
            //企业名称
            searchModel.ProxyCompanyName = txtProxyName.Value.Trim();
            //重要程度
            searchModel.Important = ddlImportant.SelectedItem.Value;
            //合作关系
            searchModel.Cooperation = ddlCooperation.SelectedItem.Value;
            //启用状态
            searchModel.UsedStatus = ddlUsedStatus.SelectedItem.Value;

            DataTable dt = HRProxyBus.SearchProxyInfo(searchModel);

            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "企业编号", "企业名称", "联系人", "固定电话", "移动电话", "网络通讯", "重要程度", "合作关系", "启用状态" },
                new string[] { "ProxyCompanyCD", "ProxyCompanyName", "ContactName", "ContactTel", "ContactMobile", "ContactWeb", "Important", "Cooperation", "UsedStatus" },
                "人才代理列表");
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Exp", "<script language=javascript>showPopup('../../../Images/Pic/Close.gif','../../../Images/Pic/note.gif','导出发生异常');</script>");
        }
    }
}
