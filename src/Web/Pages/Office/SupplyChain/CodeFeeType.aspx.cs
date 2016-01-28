using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using XBase.Business.Office.SystemManager;
public partial class Pages_Office_SupplyChain_CodeFeeType : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFlag();
        }
    }
    private void BindFlag()
    {
        string feeflag = "5";
        string feecode = "6";
        DataTable dt = CodePublicTypeBus.GetCodePublicByCode(feeflag,feecode);
        if (dt.Rows.Count > 0)
        {
            seltype.DataTextField = "TypeName";
            seltype.DataValueField = "ID";
            seltype.DataSource = dt;
            seltype.DataBind();
         

            sel_type.DataTextField = "TypeName";
            sel_type.DataValueField = "ID";
            sel_type.DataSource = dt;
            sel_type.DataBind();
            sel_type.SelectedIndex = 0;
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        seltype.Items.Insert(0, Item);
    }
}
