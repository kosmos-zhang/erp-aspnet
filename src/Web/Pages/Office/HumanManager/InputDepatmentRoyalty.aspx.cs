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
using XBase.Business.Office.HumanManager;
using System.Text;
using XBase.Common;
public partial class Pages_Office_HumanManager_InputDepatmentRoyalty : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //DataTable dtTaxInfo = InputDepatmentRoyaltyBus.SearchInsuPersonalTaxInfo(this.DeptID.Value,this.txtStartDate.Text.ToString(),this.txtEndDate.Text.ToString());
            //StringBuilder message = new StringBuilder();
            //if (dtTaxInfo != null || dtTaxInfo.Rows.Count > 1)
            //{
            //    //遍历个人所得税的所有信息
            //    for (int i = 0; i < dtTaxInfo.Rows.Count; i++)
            //    {
            //        string MinMoney = GetSafeData.GetStringFromDecimal(dtTaxInfo.Rows[i], "MiniMoney");
            //        string MaxMoney = GetSafeData.GetStringFromDecimal(dtTaxInfo.Rows[i], "MaxMoney");
            //        string TaxPercent = GetSafeData.GetStringFromDecimal(dtTaxInfo.Rows[i], "TaxPercent");
            //        string DeptID = GetSafeData.ValidateDataRow_String(dtTaxInfo.Rows[i], "DeptID");
            //        message.AppendLine(MinMoney + "," + MaxMoney + "," + TaxPercent + "," + DeptID + "@");
            //    }
            //}
            //hidTaxInfo.Value = message.ToString();
        }
    }
}
