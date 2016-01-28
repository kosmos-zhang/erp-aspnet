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
using XBase.Model.Office.CustManager;
using XBase.Common;
using XBase.Business.Office.CustManager;

public partial class Pages_Office_CustManager_CustCall_Info : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnImport.Attributes["onclick"] = "return IfExp();";
        }        
    }

    //导出
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string orderString = hiddExpOrder.Value.Trim();//排序
            string order = "desc";//排序：降序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"

            if (orderString.EndsWith("_d"))
            {
                order = "asc";//排序：降序
            }
            string ord = " ORDER BY " + orderBy + " " + order;

            CustCallModel CustCallM = new CustCallModel();
            CustCallM.CustID = hiddCustID.Value == "" ? 0 : Convert.ToInt32(hiddCustID.Value);
            CustCallM.Tel = txtTel.Value;
            string DateBegin = txtDateBegin.Value.Trim();//开始时间
            string DateEnd = txtDateEnd.Value.Trim();//结束时间 
            CustCallM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            DataTable dt = CustCallBus.GetCustCallByCon(CustCallM, DateBegin, DateEnd, ord);
            
            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "客户名称", "来电号码", "来电时间", "来电标题", "来电人", "创建人" },
                new string[] { "CustName", "Tel", "CallTime", "Title", "Callor", "EmployeeName" },
                "来电记录列表");
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Exp", "<script language=javascript>showPopup('../../../Images/Pic/Close.gif','../../../Images/Pic/note.gif','导出发生异常');</script>");
        }
    }
}
