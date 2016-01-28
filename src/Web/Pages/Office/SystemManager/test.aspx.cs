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

public partial class Pages_Office_SystemManager_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //DataTable dt = new DataTable();
        //DataColumn cu = new DataColumn();
        //cu.DataType = Type.GetType("System.String");
        //cu.ColumnName = "test";
        //dt.Columns.Add(cu);
        //for (int i = 0; i < 10; i++)
        //{
        //    DataRow row = dt.NewRow();
        //    row[0] = i;
        //    dt.Rows.Add(row);
        //}
        //this.DataList1.DataSource = dt;
        //this.DataList1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("RoleLicense_Edit.aspx?RoleID=2");

    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {

    }
    protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DataList1_ItemCreated(object sender, DataListItemEventArgs e)
    {

    }
}
