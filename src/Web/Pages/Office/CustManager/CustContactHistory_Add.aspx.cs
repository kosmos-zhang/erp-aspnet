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
using XBase.Business.Common;

public partial class Pages_Office_CustManager_CustContactHistory_Add : BasePage
{
    string CompanyCD = "";
    //string CompanyCD = "AAAAAA";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //编号初期处理
            //UserInfoUtil userInfo = new UserInfoUtil();
            //userInfo.CompanyCD = CompanyCD;
            //SessionUtil.Session.Add("UserInfo", userInfo);

            ddlContactNo.CodingType = ConstUtil.CUST_CODINGTYPE_BILL;
            ddlContactNo.ItemTypeID = ConstUtil.CUST_BILL_CONTACT;
            //ddlContactNo.TableName = ConstUtil.CUST_TABLENAME_CONTACT;//客户表名
            //ddlContactNo.ColumnName = ConstUtil.CUST_NO_CONTACTNO;//客户联络编号字段名

            //CustNameSel1.CustIDContrl = "hfCustID";
            //CustNameSel1.CustNoContrl = "hfCustNo";
            //EmployeeSel1.EmployeeIDContrl = "hfEmployeeID";//把用于存储员工ID的隐藏域控件名传出到控件属性

            BindContactHistoryLinkReason();
        }
    }

    #region 绑定客户联络事由的方法
    private void BindContactHistoryLinkReason()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_CONTACT_LINKREASONID);
        if (dt.Rows.Count > 0)
        {
            ddlLinkReasonID.DataTextField = "TypeName";
            ddlLinkReasonID.DataValueField = "ID";
            ddlLinkReasonID.DataSource = dt;
            ddlLinkReasonID.DataBind();
        }
        else
        {
            ListItem Item = new ListItem();
            Item.Value = "";
            Item.Text = "--请选择--";
            ddlLinkReasonID.Items.Insert(0, Item);
        }
    }
    #endregion
}
