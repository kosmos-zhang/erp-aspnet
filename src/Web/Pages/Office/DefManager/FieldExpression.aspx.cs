using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Office_DefManager_FieldExpression : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["pageIndex"] != null && Request.QueryString["pageCount"] != null)
        {
            Hidpagestate.Value = "&pageIndex=" + Request.QueryString["pageIndex"].ToString() + "&pageCount=" + Request.QueryString["pageCount"].ToString();
        }
        else
        {
            Hidpagestate.Value = "&pageIndex=1&pageCount=10";
        }

        if (Request.QueryString["TableID"] != null && Request.QueryString["TableID"] != "")
        {
            hidID.Value = Request.QueryString["TableID"];
            hidColumnID.Value = Request.QueryString["ColumnID"];
            DataSet ds=XBase.Business.Office.DefManager.DefFormBus.GetTableById(Request.QueryString["TableID"]);
            if (ds!=null&&ds.Tables.Count>0)
            {
                Repeater1.DataSource = ds.Tables[1];
                Repeater1.DataBind();
                ExpressionValue.Text = Request.QueryString["FieldName"];
                DataTable dt = XBase.Business.Office.DefManager.DefFormBus.GetRegular(Convert.ToInt32(Request.QueryString["ColumnID"]));
                if (dt != null && dt.Rows.Count > 0)
                {
                    hidAction.Value = "1";
                    DataRow[] dr = dt.Select("summaryflag=0");
                    if (dr.Length>0)
                    {
                        ExpressionText.Text = dr[0]["relation"].ToString().Replace("=#" + Request.QueryString["FieldName"] + "#", "");
                    }
                     DataRow[] dr1 = dt.Select("summaryflag=1");
                     if (dr1.Length > 0)
                     {
                         ddlTotalType.SelectedValue = dr1[0]["relation"].ToString().Replace("#" + Request.QueryString["FieldName"] + "#", "");
                     }
                }
            }
        }
    }
}