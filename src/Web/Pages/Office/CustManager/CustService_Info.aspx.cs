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
using XBase.Business.Common;
using XBase.Common;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;

public partial class Pages_Office_CustManager_CustService_Info : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindServeType();//绑定服务类型
            btnImport.Attributes["onclick"] = "return IfExp();";
            hiddUserId.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        }
    }

    #region 绑定服务类型的方法
    private void BindServeType()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_SERVICES_SERVETYPE);
        if (dt.Rows.Count > 0)
        {
            ddlServeType.DataTextField = "TypeName";
            ddlServeType.DataValueField = "ID";
            ddlServeType.DataSource = dt;
            ddlServeType.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlServeType.Items.Insert(0, Item);
    }
    #endregion   

    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string orderString = hiddExpOrder.Value.Trim();//排序
            string order = "asc";//排序：降序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "BeginDate";//要排序的字段，如果为空，默认为"ID"

            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            string ord = " ORDER BY " + orderBy + " " + order;
                       
            //获取检索条件
            string CustID = hiddCustID.Value;//客户ID 
            CustServiceModel CustServiceM = new CustServiceModel();
            CustServiceM.ServeType = Convert.ToInt32(ddlServeType.SelectedItem.Value);//服务类型        
            CustServiceM.Fashion = Convert.ToInt32(ddlFashion.Value);//服务方式        
            string ServiceDateBegin = txtServiceDateBegin.Value;
            string ServiceDateEnd = txtServiceDateEnd.Value;//服务结束时间
            CustServiceM.Title = txtTitle.Value.Trim();//客户服务主题
            string Executant = txtExecutant.Value.Trim();//执行人
            string CustLinkMan = txtCustLinkMan.Value.Trim();//客户联系人        
            CustServiceM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();

            DataTable dt = ServiceBus.ExportServiceInfo(CanUserID,CustID, CustServiceM, ServiceDateBegin, ServiceDateEnd, Executant, CustLinkMan, ord);

            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "服务单编号", "服务主题", "服务时间", "客户名称", "服务类型", "服务方式", "执行人", "客户联络人" },
                new string[] { "ServeNo", "title", "BeginDate", "custnam", "ServeType", "Fashion", "EmployeeName", "LinkManName" },
                "客户服务列表");
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Exp", "<script language=javascript>showPopup('../../../Images/Pic/Close.gif','../../../Images/Pic/note.gif','导出发生异常');</script>");
        }
    }
}
