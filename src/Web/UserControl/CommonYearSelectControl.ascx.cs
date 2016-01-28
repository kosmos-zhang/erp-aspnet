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

public partial class UserControl_CommonYearSelectControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            InintDDL();
        }
    }
    /// <summary>
    /// 初始化年份
    /// </summary>
    private void InintDDL()
    {
        int StartYear = 2007;
        int Year = System.DateTime.Now.Year;
        int EndYear = Year + 10 + Year - StartYear;

        int j = 0;

        for (int i = StartYear; i < EndYear; i++)
        {
            ListItem NoYearItem = new ListItem(i.ToString(), i.ToString());
            CommonYearSelectControl.Items.Insert(j, NoYearItem);
            if (Year == i) CommonYearSelectControl.SelectedIndex = j;
            j++;
        }
    }
}
