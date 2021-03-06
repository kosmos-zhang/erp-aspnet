﻿using System;
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

public partial class UserControl_SelectSellOrderUC : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = XBase.Business.Office.SellManager.SellModuleSelectCurrencyBus.GetCurrencyTypeSetting();
            OrdCurrencyType.DataSource = dt;
            OrdCurrencyType.DataTextField = "CurrencyName";
            OrdCurrencyType.DataValueField = "ID";
            OrdCurrencyType.DataBind();
        }
    }
}
