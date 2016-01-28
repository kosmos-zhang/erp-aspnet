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
using XBase.Business.Common;
using XBase.Common;

public partial class Pages_Office_CustManager_LinkMan_Add : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //公司编号
            txtCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;//建档人
            txtCreatedDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            //学历
            CodeTypeDrpControl2.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            CodeTypeDrpControl2.TypeCode = ConstUtil.CODE_TYPE_CULTURE;
            CodeTypeDrpControl2.IsInsertSelect = true;
            //专业
            CodeTypeDrpControl3.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            CodeTypeDrpControl3.TypeCode = ConstUtil.CODE_TYPE_PROFESSIONAL;
            CodeTypeDrpControl3.IsInsertSelect = true;
            BindLinkManLinkType();
            //民族
            CodeTypeDrpControl1.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            CodeTypeDrpControl1.TypeCode = ConstUtil.CODE_TYPE_NATIONAL;
            CodeTypeDrpControl1.IsInsertSelect = true;

            //CustNameSel1.CustNoContrl = "hfCustNo";
            //CustNameSel1.CustNamContrl = "txtCustNam";
        }
    }

    #region 绑定联系人类型
    private void BindLinkManLinkType()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_LINK_LINKTYPE);
        if (dt.Rows.Count > 0)
        {
            ddlLinkType.DataTextField = "TypeName";
            ddlLinkType.DataValueField = "ID";
            ddlLinkType.DataSource = dt;
            ddlLinkType.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlLinkType.Items.Insert(0, Item);
    }
    #endregion
}
