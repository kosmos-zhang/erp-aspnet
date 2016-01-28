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

public partial class Pages_Office_CustManager_CustLove_Add : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtModifiedDate.Value = DateTime.Now.ToShortDateString();
            txtModifiedUserID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改用户为当前用户

            ddlLoveNo.CodingType = ConstUtil.CUST_CODINGTYPE_BILL;
            ddlLoveNo.ItemTypeID = ConstUtil.CUST_BILL_Love;

            //CustNameSel1.CustIDContrl = "hfCustID";
            //CustNameSel1.CustNoContrl = "hfCustNo";

            BindLoveType();//关怀类型
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
        else
        {
            ListItem Item = new ListItem();
            Item.Value = "0";
            Item.Text = "--请选择--";
            ddlLoveType.Items.Insert(0, Item);
        }
    }
    #endregion
}
