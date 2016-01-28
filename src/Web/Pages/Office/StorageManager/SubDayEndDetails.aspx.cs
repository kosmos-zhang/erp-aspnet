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
using XBase.Business.Office.StorageManager;
using XBase.Common;
public partial class Pages_Office_StorageManager_SubDayEndDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["ProductID"] != null)
            {
                string ProductID = Request.QueryString["ProductID"].ToString();
                string BatchNo = Request.QueryString["BatchNo"].ToString();
                string DailyDate = Request.QueryString["DailyDate"].ToString();
                 string DeptID = Request.QueryString["DeptID"].ToString();
                UserInfoUtil user = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
                string _companycd = user.CompanyCD;
                hiddenEquipCode.Value = "List";
                hidBatchNo.Value = BatchNo;
                hidDailyDate.Value = DailyDate;
                hidProductID.Value = ProductID;
                this.hidDeptID.Value = DeptID;
                GetValue(ProductID, BatchNo, DeptID,DailyDate, _companycd);
            }
        }
    }

    protected void GetValue(string ProductID, string BatchNo, string DeptID, string DailyDate, string CompanyCD)
    {
       DataTable dt= SubDayEndBus.GetSubStorageDailyInfo(ProductID, BatchNo, DeptID, DailyDate, CompanyCD);
       if (dt.Rows.Count > 0)
       {
           txtBatchNo.Text = dt.Rows[0]["BatchNo"].ToString();
           txtCreateDate.Text = dt.Rows[0]["CreateDate"].ToString();
           this.txtCreator.Text = dt.Rows[0]["CreatorName"].ToString();
           this.txtDailyDate.Text = dt.Rows[0]["DailyDate"].ToString();
           this.txtDeptID.Text = dt.Rows[0]["DeptName"].ToString();
           this.txtDispInCont.Text = dt.Rows[0]["DispInCont"].ToString();
           this.txtDispOutCount.Text = dt.Rows[0]["DispOutCount"].ToString();
           this.txtInitInCount.Text = dt.Rows[0]["InitInCount"].ToString();
           this.txtInTotal.Text = dt.Rows[0]["InTotal"].ToString();
           this.txtOutTotal.Text = dt.Rows[0]["OutTotal"].ToString();
           this.txtProductID.Text = dt.Rows[0]["ProductName"].ToString();
           this.txtSaleBackFee.Text = dt.Rows[0]["SaleBackFee"].ToString();
           this.txtSaleBackInCount.Text = dt.Rows[0]["SaleBackInCount"].ToString();
           this.txtSaleFee.Text = dt.Rows[0]["SaleFee"].ToString();
           this.txtSaleOutCount.Text = dt.Rows[0]["SaleOutCount"].ToString();
           this.txtSendInCount.Text = dt.Rows[0]["SendInCount"].ToString();
           this.txtSendOutCount.Text = dt.Rows[0]["SendOutCount"].ToString();
           this.txtSubSaleBackInCount.Text = dt.Rows[0]["SubSaleBackInCount"].ToString();
           this.txtSubSaleOutCount.Text = dt.Rows[0]["SubSaleOutCount"].ToString();
           this.txtTodayCount.Text = dt.Rows[0]["TodayCount"].ToString();
           this.txtProductNo.Text = dt.Rows[0]["ProductNo"].ToString();
           this.txtDAORU.Text = dt.Rows[0]["InitBatchCount"].ToString();
       
       }
    }
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["ProductID"] != null)
        {
            if (Request.QueryString["subRpt"] != null)
            {
               
                string query = Request.QueryString.ToString();
                Response.Redirect("../../OperatingModel/IntegratedData/SubStoreInvoicingDateReport.aspx?" + query);
            }
            else
            {

                string DailyDate = Request.QueryString["DailyDate"].ToString();
                string pageIndex = Request.QueryString["pageIndex"].ToString();
                string pageCount = Request.QueryString["pageCount"].ToString();
                string ModuleID = Request.QueryString["ModuleID"].ToString();
                string orderby = Request.QueryString["orderby"].ToString();
                Response.Redirect("SubDayEnd.aspx?day=" + DailyDate + "&pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&orderby=" + orderby + "&ModuleID=" + ModuleID);
            }
        }
    }
}
