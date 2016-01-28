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
using XBase.Business.Common;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;

public partial class Pages_Office_CustManager_CustComplain_Info : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindComplainType();
            btnImport.Attributes["onclick"] = "return IfExp();";
            hiddUserId.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        }
    }

    #region 绑定投诉类型的方法
    private void BindComplainType()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_COMPLAIN_TYPE);
        if (dt.Rows.Count > 0)
        {
            ddlComplainType.DataTextField = "TypeName";
            ddlComplainType.DataValueField = "ID";
            ddlComplainType.DataSource = dt;
            ddlComplainType.DataBind();            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlComplainType.Items.Insert(0, Item);
    }
    #endregion

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

           

            CustComplainModel CustComplainM = new CustComplainModel();
            string CustID = hiddCustID.Value;//客户ID   
            CustComplainM.ComplainType = Convert.ToInt32(ddlComplainType.SelectedItem.Value);//投诉类型
            CustComplainM.Critical = seleCritical.Value;//紧急程度
            string ComplainBegin = txtComplainBegin.Value.Trim();//投诉开始时间
            string ComplainEnd = txtComplainEnd.Value.Trim();//结束时间            
            CustComplainM.Title = txtTitle.Value.Trim();//客户投诉主题
            string CustLinkMan = txtCustLinkMan.Value.Trim();//客户联系人        
            string DestClerk = txtEmplNameL.Value.Trim();//接待人
            CustComplainM.State = seleState.Value;//状态
            CustComplainM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();

            DataTable dt = ComplainBus.ExportComplainInfo(CanUserID,CustID, CustComplainM, ComplainBegin, ComplainEnd, CustLinkMan, DestClerk, ord);

            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "投诉单编号", "投诉主题", "客户名称", "投诉时间", "投诉分类", "紧急程度", "接待人", "处理状态" },
                new string[] { "ComplainNo", "title", "custNam", "ComplainDate", "typename", "Critical", "EmployeeName", "state" },
                "客户投诉列表");
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Exp", "<script language=javascript>showPopup('../../../Images/Pic/Close.gif','../../../Images/Pic/note.gif','导出发生异常');</script>");
        }
    }
}
