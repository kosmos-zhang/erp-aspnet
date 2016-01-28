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
using XBase.Business.Office.CustManager;
using XBase.Common;

public partial class Pages_Office_CustManager_CustContact_Info : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {           
            btnImport.Attributes["onclick"] = "return IfExp();";
            hiddUserId.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        }
    }
    
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string orderString = hiddExpOrder.Value.Trim();//排序
            string order = "asc";//排序：降序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "LinkDate";//要排序的字段，如果为空，默认为"ID"

            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }            
            string ord = " ORDER BY " + orderBy + " " + order;

            string CustID = hiddCustID.Value;//客户ID
            string CustLinkMan = txtCustLinkMan.Value;//被联络人
            string LinkDateBegin = txtLinkDateBegin.Value;
            string LinkDateEnd = txtLinkDateEnd.Value;
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();

            DataTable dt = ContactHistoryBus.ExportContactInfo(CanUserID,CustID, CustLinkMan, CompanyCD, LinkDateBegin, LinkDateEnd, ord);

            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "联络单编号", "联络主题", "客户名称", "我方联络人", "联络时间", "客户联络人"},
                new string[] { "ContactNo", "Title", "CustNam", "Linker", "LinkDate", "LinkManName" },
                "客户联络列表");
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Exp", "<script language=javascript>showPopup('../../../Images/Pic/Close.gif','../../../Images/Pic/note.gif','导出发生异常');</script>");
        }
    }
}
