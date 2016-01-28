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

public partial class Pages_Office_CustManager_CustLove_Info : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindLoveType();//关怀类型
            btnImport.Attributes["onclick"] = "return IfExp();";
            hiddUserId.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        }
    }

    #region 绑定关怀类型的方法
    private void BindLoveType()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_INFO_LOVE);
        if (dt.Rows.Count > 0)
        {
            ddlLoveType.DataTextField = "TypeName";
            ddlLoveType.DataValueField = "ID";
            ddlLoveType.DataSource = dt;
            ddlLoveType.DataBind();
           
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlLoveType.Items.Insert(0, Item);
    }
    #endregion

    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string orderString = hiddExpOrder.Value.Trim();//排序
            string order = "asc";//排序：降序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "LoveDate";//要排序的字段，如果为空，默认为"ID"

            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            string ord = " ORDER BY " + orderBy + " " + order;

            CustLoveModel CustLoveM = new CustLoveModel();
            string CustID = hiddCustID.Value;//客户ID 
            CustLoveM.LoveType = Convert.ToInt32(ddlLoveType.SelectedItem.Value);//类型
            string LoveBegin = txtLoveBegin.Value.Trim();//开始时间
            string LoveEnd = txtLoveEnd.Value.Trim();//结束时间            
            string CustLinkMan = txtCustLinkMan.Value.Trim();//客户联系人
            CustLoveM.Title = txtTitle.Value.Trim();//主题
            CustLoveM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();

            DataTable dt = LoveBus.ExportLoveInfo(CanUserID,CustID, CustLoveM, LoveBegin, LoveEnd, CustLinkMan, ord);

            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "关怀编号", "关怀主题", "客户名称", "客户联系人", "关怀时间", "关怀类型", "执行人" },
                new string[] { "LoveNo", "Title", "CustNam", "LinkManName", "LoveDate", "LoveType", "EmployeeName" },
                "客户关怀列表");
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Exp", "<script language=javascript>showPopup('../../../Images/Pic/Close.gif','../../../Images/Pic/note.gif','导出发生异常');</script>");
        }
    }
}
