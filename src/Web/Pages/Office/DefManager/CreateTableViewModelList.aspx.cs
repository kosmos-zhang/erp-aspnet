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

public partial class Pages_Office_DefManager_CreateTableViewModelList :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //获取表名称列表
            DataTable tbName = XBase.Business.Office.DefManager.CreateTableViewModelBus.GetDefTableNameList(UserInfo.CompanyCD);

            if (tbName.Rows.Count > 0)
            {
                txtTableName.DataTextField = "AliasTableName";
                txtTableName.DataValueField = "ID";
                txtTableName.DataSource = tbName;
                txtTableName.DataBind();
                ListItem Item = new ListItem();
                Item.Value = "0";
                Item.Text = "--请选择--";
                txtTableName.Items.Insert(0, Item);
            }
        }
    }
}
