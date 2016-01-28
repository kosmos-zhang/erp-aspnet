using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using XBase.Business.Office.DefManager;
using XBase.Business.DefManager;

public partial class Pages_Office_DefManager_CreateTableViewModel : BasePage
{
    #region TableModelID
    public int intTableModelID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["intTableModelID"], out tempID);
            return tempID;
        }
    }
    #endregion

    #region TableID
    public int tableID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["intTableID"], out tempID);
            return tempID;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //获取图片地址
        string urlname = Request.Url.AbsoluteUri;
        urlname = urlname.Substring(urlname.LastIndexOf(':') + 1);
        string port = urlname.Substring(0, urlname.IndexOf('/'));
        string imagepath = Request.Url.Host;
        if (port.Length > 0)
        {
            imagepath = imagepath + ":" + port;
        }
        
        if (!IsPostBack)
        {
            BindData();
            HidSubTable.Value = imagepath;
            //HidSubTable.Value = "<table width='100%' border='0' align='center' cellpadding='2' cellspacing='1' bgcolor='#999999'>" +
            //    "<tr>" +
            //        "<td valign='top' style='background-color:#FFFFFF'>" +
            //            "<img alt='' runat=\"server\" src='http://" + imagepath + "/images/Button/Show_add.jpg' style='cursor: hand' id='imgAdd' onclick=\"AddDetailRow('###tablename###')\" />" +
            //            "<img alt='' runat=\"server\" src='http://" + imagepath + "/images/Button/Show_del.jpg' style='cursor: hand' id='imgDel' onclick=\"DeleteDetailRow('###tablename###');\" />" +
            //        "</td>" +
            //    "</tr>" +
            //"</table>" +
            //"<table width='100%' id='###tablename###' border='0' align='center' cellpadding='2' cellspacing='1' bgcolor='#999999'>" +
            //"<tr>" +
            //    "<td valign='top' style='background-color:#FFFFFF; width:40px' align='center'>" +
            //        "<input type='checkbox' name='checkall' id=\"checkall_###tablename###\" onclick=\"fnSelectAll('###tablename###')\" title='全选' style='cursor: hand' />" +
            //    "</td>" +
            //    "###tablehead###" +
            //"</tr>" +
            //"</table>";
        }
    }

    private void BindData()
    {
        //获取表名称列表
        DataTable tbName = CreateTableViewModelBus.GetDefTableNameList(UserInfo.CompanyCD);

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
        this.txtTableName.Attributes.Add("onchange", "load(this.options[this.selectedIndex].value)");

        if (this.intTableModelID != 0)
        {
            this.hiddTBModuleID.Value = this.intTableModelID.ToString();
            DataTable dt = CreateTableViewModelBus.GetTBModuleInfo(this.intTableModelID.ToString());
            if (dt.Rows.Count >0)
            {
                //表名
                txtTableName.SelectedValue = dt.Rows[0]["TableID"].ToString();//XBase.Common.GetSafeData.ValidateDataRow_String(dt.Rows[0], "TableID");
                txtTableName.Enabled = false;
                //填充编辑区
                this.content1.Text = dt.Rows[0]["ModuleContent"].ToString();
                string tbID = txtTableName.SelectedValue;//已选择的表ID
                //获取表字段列表
                DataTable tbField = CreateTableViewModelBus.GetTableFieldsList(tbID);
                if (tbField.Rows.Count > 0)
                {
                    txtTableFieldName.DataTextField = "cnameCn";
                    txtTableFieldName.DataValueField = "ccode";
                    txtTableFieldName.DataSource = tbField;
                    txtTableFieldName.DataBind();
                    ListItem Item = new ListItem();
                    Item.Value = "0";
                    Item.Text = "--请选择--";
                    txtTableFieldName.Items.Insert(0, Item);
                }
                //根据表ID获取对应模板的查看人员ID
                string proptype = "Pages/Office/DefManager/DefTableList.aspx?tableid=" + tbID;
                DataTable tbcanViewUser = CreateTableViewModelBus.GetCanViewMenuUser(proptype);
                if (tbcanViewUser.Rows.Count > 0)
                {
                    UserJoinListName.Text = tbcanViewUser.Rows[0]["CanViewUserNames"].ToString();//可查看菜单人员名称
                    lblJoinListID.Value = tbcanViewUser.Rows[0]["CanViewUserIDs"].ToString();//可查看菜单人员ID
                }
                //启用状态
                //this.UseStatus.Text = XBase.Common.GetSafeData.ValidateDataRow_String(dt.Rows[0], "UseStatus");
                //子表名称列表
                DataTable subTB = CreateTableViewModelBus.GetSubTableNameList(tbID, UserInfo.CompanyCD);
                if (subTB.Rows.Count > 0)
                {
                    ddl_subtable.DataTextField = "AliasTableName";
                    ddl_subtable.DataValueField = "CustomTableName";
                    ddl_subtable.DataSource = subTB;
                    ddl_subtable.DataBind();
                    ListItem Item = new ListItem();
                    Item.Value = "0";
                    Item.Text = "--请选择--";
                    ddl_subtable.Items.Insert(0, Item);
                }
            }
        }
        else if (this.tableID != 0)
        {
            //表名
            txtTableName.SelectedValue = this.tableID.ToString();// XBase.Common.GetSafeData.ValidateDataRow_String(this.tableID, "TableID");
            txtTableName.Enabled = false;
            
           // string tbID = txtTableName.SelectedValue;//已选择的表ID
            //获取表字段列表
            DataTable tbField = CreateTableViewModelBus.GetTableFieldsList(tableID.ToString());
            if (tbField.Rows.Count > 0)
            {
                txtTableFieldName.DataTextField = "cnameCn";
                txtTableFieldName.DataValueField = "ccode";
                txtTableFieldName.DataSource = tbField;
                txtTableFieldName.DataBind();
                ListItem Item = new ListItem();
                Item.Value = "0";
                Item.Text = "--请选择--";
                txtTableFieldName.Items.Insert(0, Item);
            }
            //子表名称列表
            DataTable subTB = CreateTableViewModelBus.GetSubTableNameList(tableID.ToString(), UserInfo.CompanyCD);
            if (subTB.Rows.Count > 0)
            {
                ddl_subtable.DataTextField = "AliasTableName";
                ddl_subtable.DataValueField = "CustomTableName";
                ddl_subtable.DataSource = subTB;
                ddl_subtable.DataBind();
                ListItem Item = new ListItem();
                Item.Value = "0";
                Item.Text = "--请选择--";
                ddl_subtable.Items.Insert(0, Item);
            }
        }

    }

}
