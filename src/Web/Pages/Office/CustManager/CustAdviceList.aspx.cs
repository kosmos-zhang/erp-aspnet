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
using XBase.Common;
using XBase.Business.Office.CustManager;
using XBase.Model.Office.CustManager;
public partial class Pages_Office_CustManager_CustAdviceList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            CustAdviceModel model = new CustAdviceModel();
            model.CompanyCD = companyCD;
            model.Title = this.txtTitle.Text.Trim();
            model.AdviceNo = this.txtAdvicetNo.Text.Trim();
            model.CustID = this.hiddenCustIDL.Value;
            model.DestClerk = this.hiddenExecutor.Value;
            model.AdviceType = this.txtAdviceType.Value;
            model.State = this.txtState.Value; 
            string theBeginTime = this.BeginTime.Value;
            string theEndTime = this.EndTime.Value;
            string myOrderBy = " AdviceDate Desc";
            if (this.hiddenOrder.Value != "0")
            {
                string[] myOrder = this.hiddenOrder.Value.Split('_');
                if (myOrder[1] == "a")
                {
                    myOrderBy= myOrder[0] + " asc ";
                }
                else
                {
                    myOrderBy = myOrder[0] + " desc ";
                }
            }
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            DataTable dt = CustAdviceBus.GetAllCustAdvice(CanUserID,model, myOrderBy, theBeginTime, theEndTime);

            //导出标题
            string headerTitle = "单据编号|单据主题|提出建议客户|客户联系人|接待人|采纳程度|建议时间|建议类型|状态";
            string[] header = headerTitle.Split('|');

            //导出标题所对应的列字段名称
            string columnFiled = "AdviceNo|Title|CustName|LinkManName|EmployeeName|Accept|AdviceDate|AdviceType|State";
            string[] field = columnFiled.Split('|');

            XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "客户建议列表");
        }
        catch
        { }
    }
}
