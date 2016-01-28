using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using XBase.Business.Office.SellReport;

public partial class Pages_Office_SellReport_SellProductReportAdd : BasePage
{
    //小数精度
    private int _selPoint = 2;
    public int SelPoint
    {
        get
        {
            return _selPoint;
        }
        set
        {
            _selPoint = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.productName.Attributes.Add("onchange", "fnProdNameChange()");
        if (!IsPostBack)
        {
            this.createdate.Value = System.DateTime.Now.ToString("yyyy-MM-dd");

            DataTable dt = SellProductReportBus.GetProductList(UserInfo.CompanyCD);
            if (dt.Rows.Count > 0)
            {
                this.productName.DataTextField = "productName";
                this.productName.DataValueField = "id";
                this.productName.DataSource = dt;
                this.productName.DataBind();

                ListItem Item = new ListItem();
                Item.Value = "0";
                Item.Text = "--请选择--";
                this.productName.Items.Insert(0, Item);
            }
            else
            { 
                ListItem Item = new ListItem();
                Item.Value = "0";
                Item.Text = "--请选择--";
                this.productName.Items.Insert(0, Item);
            }

            // 小数位数
            _selPoint = int.Parse(UserInfo.SelPoint);
        }
    }

    public int isList//销售汇报id
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["id"], out tempID);
            return tempID;
        }
    }
}
