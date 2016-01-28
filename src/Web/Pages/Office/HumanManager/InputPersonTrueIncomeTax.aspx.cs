using System;
using XBase.Common;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
public partial class Pages_Office_HumanManager_InputPersonTrueIncomeTax : BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            //显示工资项列表
            //InitSalaryInfo();
            //岗位初期
            DataTable dtQuarter = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
            ddlQuarter.DataSource = dtQuarter;
            ddlQuarter.DataValueField = "ID";
            ddlQuarter.DataTextField = "QuarterName";
            ddlQuarter.DataBind();
            //添加一请选择选项
            ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
            ddlQuarter.Items.Insert(0, Item);
            DataTable dtTaxInfo = InputPersonTrueIncomeTaxBus.PersonTaxInfo();
            StringBuilder message = new StringBuilder();
            if (dtTaxInfo != null || dtTaxInfo.Rows.Count >1)
               {
                        //遍历个人所得税的所有信息
                        for (int i = 0; i < dtTaxInfo.Rows.Count; i++)
                        {
                            string MinMoney = GetSafeData.GetStringFromDecimal(dtTaxInfo.Rows[i], "MinMoney");
                            string MaxMoney = GetSafeData.GetStringFromDecimal(dtTaxInfo.Rows[i], "MaxMoney");
                            string TaxPercent = GetSafeData.GetStringFromDecimal(dtTaxInfo.Rows[i], "TaxPercent");
                            string MinusMoney = GetSafeData.GetStringFromDecimal(dtTaxInfo.Rows[i], "MinusMoney");
                            message.AppendLine(MinMoney + "," + MaxMoney + "," + TaxPercent + "," + MinusMoney + "@") ;
                        }
               }
            hidTaxInfo.Value = message.ToString();

        }
    }
}
