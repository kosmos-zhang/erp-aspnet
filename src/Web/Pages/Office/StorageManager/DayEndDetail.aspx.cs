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
public partial class Pages_Office_StorageManager_DayEndDetail : System.Web.UI.Page
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
                string StorageID = Request.QueryString["StorageID"].ToString();
                UserInfoUtil user = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
                string _companycd = user.CompanyCD;
                hiddenEquipCode.Value = "List";
                hidBatchNo.Value = BatchNo;
                hidDailyDate.Value = DailyDate;
                hidProductID.Value = ProductID;
                hidStorageID.Value = StorageID;
                GetValue(ProductID, BatchNo, StorageID, DailyDate, _companycd);
            }
        }
    }

    protected void GetValue(string ProductID, string BatchNo, string StorageID, string DailyDate, string CompanyCD)
    {
        DataTable dt = DayEndBus.GetStorageDailyInfo(ProductID, BatchNo, StorageID, DailyDate, CompanyCD);
        if (dt.Rows.Count > 0)
        {
            txtBatchNo.Text = dt.Rows[0]["BatchNo"].ToString();
            txtCreateDate.Text = dt.Rows[0]["CreateDate"].ToString();
            this.txtCreator.Text = dt.Rows[0]["CreatorName"].ToString();
            this.txtDailyDate.Text = dt.Rows[0]["DailyDate"].ToString();
            this.txtAdjustCount.Text = dt.Rows[0]["AdjustCount"].ToString();
            this.txtBackInCount.Text = dt.Rows[0]["BackInCount"].ToString();
            this.txtBadCount.Text = dt.Rows[0]["BadCount"].ToString();
            this.txtCheckCount.Text = dt.Rows[0]["CheckCount"].ToString();
            this.txtDispInCount.Text = dt.Rows[0]["DispInCount"].ToString();
            this.txtDispOutCount.Text = dt.Rows[0]["DispOutCount"].ToString();
            this.txtInitBatchCount.Text = dt.Rows[0]["InitBatchCount"].ToString();
            this.txtInitInCount.Text = dt.Rows[0]["InitInCount"].ToString();  
            this.txtInTotal.Text = dt.Rows[0]["InTotal"].ToString();

              this.txtLendCount.Text = dt.Rows[0]["LendCount"].ToString();
            this.txtMakeInCount.Text = dt.Rows[0]["MakeInCount"].ToString();
            this.txtOtherInCount.Text = dt.Rows[0]["OtherInCount"].ToString();
            this.txtOtherOutCount.Text = dt.Rows[0]["OtherOutCount"].ToString();  
             


            this.txtOutTotal.Text = dt.Rows[0]["OutTotal"].ToString();

            this.txtPhurBackFee.Text = dt.Rows[0]["PhurBackFee"].ToString();
            this.txtPhurBackOutCount.Text = dt.Rows[0]["PhurBackOutCount"].ToString();
            this.txtPhurFee.Text = dt.Rows[0]["PhurFee"].ToString();

            this.txtPhurInCount.Text = dt.Rows[0]["PhurInCount"].ToString();

            this.txtPhurBackFee.Text = dt.Rows[0]["PhurBackFee"].ToString();

            this.txtProductID.Text = dt.Rows[0]["ProductName"].ToString();
            this.txtProductNo.Text = dt.Rows[0]["ProductNo"].ToString();


            this.txtRedInCount.Text = dt.Rows[0]["RedInCount"].ToString();
            this.txtRedOutCount.Text = dt.Rows[0]["RedOutCount"].ToString();

         

            this.txtSaleBackFee.Text = dt.Rows[0]["SaleBackFee"].ToString();
            this.txtSaleBackInCount.Text = dt.Rows[0]["SaleBackInCount"].ToString();
            this.txtSaleFee.Text = dt.Rows[0]["SaleFee"].ToString();
            this.txtSaleOutCount.Text = dt.Rows[0]["SaleOutCount"].ToString();
            this.txtSendInCount.Text = dt.Rows[0]["SendInCount"].ToString();
            this.txtSendOutCount.Text = dt.Rows[0]["SendOutCount"].ToString();
            this.txtStorageID.Text = dt.Rows[0]["StorageName"].ToString();
            this.txtSubSaleBackInCount.Text = dt.Rows[0]["SubSaleBackInCount"].ToString();
            this.txtSubSaleOutCount.Text = dt.Rows[0]["SubSaleOutCount"].ToString();
            this.txtTakeInCount.Text = dt.Rows[0]["TakeInCount"].ToString();

            this.txtTakeOutCount.Text = dt.Rows[0]["TakeOutCount"].ToString(); 
            this.txtTodayCount.Text = dt.Rows[0]["TodayCount"].ToString();
          


        }
    }
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["ProductID"] != null)
        {
            string DailyDate = Request.QueryString["DailyDate"].ToString();
            string pageIndex = Request.QueryString["pageIndex"].ToString();
            string pageCount = Request.QueryString["pageCount"].ToString();
            string ModuleID = Request.QueryString["ModuleID"].ToString();
            string orderby = Request.QueryString["orderby"].ToString();
            Response.Redirect("DayEnd.aspx?day=" + DailyDate + "&pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&orderby=" + orderby + "&ModuleID=" + ModuleID);
        }
    }
 
}
