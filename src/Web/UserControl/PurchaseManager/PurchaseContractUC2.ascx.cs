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

public partial class UserControl_PurchaseManager_PurchaseContractUC2 : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //BindCurrencyType2();
    }
    //private void BindCurrencyType2()
    //{
    //    DataTable dt = PurchaseOrderDBHelper.GetCurrenyType();
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        ddlCurrencyType2.DataSource = dt;
    //        ddlCurrencyType2.DataTextField = "CurrencyName";
    //        ddlCurrencyType2.DataValueField = "hhh";
    //        ddlCurrencyType2.DataBind();

    //        string aaa = ddlCurrencyType2.Value;
    //        hidPurConCurrency.Value = aaa.Split('_')[0];
    //        //txtExchangeRate.Text = aaa.Split('_')[1].Substring(0, aaa.Split('_')[1].Length - 2);
    //        //txtExchangeRate.Text = aaa.Split('_')[1];
    //    }
    //}
}
