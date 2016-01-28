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
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;
public partial class Pages_Office_SupplyChain_ProductPriceList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.hidModuleID.Value = ConstUtil.Menu_AddProductPrice;
            
            string requestParam = Request.QueryString.ToString();
            //从列表过来时
            int firstIndex = requestParam.IndexOf("&");
            //返回回来时
            if (firstIndex > 0)
            {
                //获取是否查询的标识
                string flag = Request.QueryString["Flag"];
                //点击查询时，设置查询的条件，并执行查询
                if ("1".Equals(flag))
                {

                    this.txt_ChangeNo.Value = Request.QueryString["ChangeNo"];
                    txt_Title.Value = Request.QueryString["Title"];
                    txt_ProductName.Value = Request.QueryString["ProductName"];
                    UserReporter.Text = Request.QueryString["UserReporter"];
                    txtOpenDate.Value = Request.QueryString["OpenDate"];
                    this.txtCloseDate.Value = Request.QueryString["CloseDate"];
                    //获取当前页
                    string pageIndex = Request.QueryString["PageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["PageCount"];
                    //执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");Fun_Search_ProductPriceInfo('" + pageIndex + "');</script>");
                }
            }
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        ProductPriceChangeModel model = new ProductPriceChangeModel();
        model.ChangeNo = txt_ChangeNo.Value;
        model.Title = txt_Title.Value;
        model.ProductID = hf_ProductID.Value;
        model.Chenger = txtChenger.Value;
        string OpenDate = txtOpenDate.Value;
        string CloseDate = txtCloseDate.Value;
        int totalCount = 0;
        model.CompanyCD  = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        DataTable dt = ProductPriceChangeBus.GetProductPriceInfo(model, OpenDate, CloseDate, 1, 1000000, "ID desc",ref totalCount);
        foreach (DataRow row in dt.Rows)
        {
            if (row["BillStatus"].ToString() == "1")
                row["BillStatus"] = "制单";
            if (row["BillStatus"].ToString() == "5")
                row["BillStatus"] = "结单";
        }
           //导出标题
        string headerTitle = "变更单编号|变更单主题|物品编号|物品名称|调整后销项率|调整后去税价|调整后含税价|调整后折扣|申请日期|确认日期|单据状态";
        //string headerTitle = "建档日期|启用状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "ChangeNo|Title|ProductID|ProductName|TaxRateNew|StandardSellNew|SellTaxNew|DiscountNew|ChangeDate|ConfirmDate|BillStatus";
        //string columnFiled = "CreateDate|strUsedStatus";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, " 物品售价变更单");
    }
}
