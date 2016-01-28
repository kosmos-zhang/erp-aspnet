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

using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;
using XBase.Common;

public partial class Pages_Office_StorageManager_StorageJournal : BasePage
{
    private string companyCD = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (!IsPostBack)
        {
            HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
            GetBillExAttrControl1.TableName = "officedba.Productinfo";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            GetBillExAttrControl1.ExtIndex = EFIndex;
            GetBillExAttrControl1.ExtValue = EFDesc;
            GetBillExAttrControl1.SetExtControlValue();

            StorageModel model = new StorageModel();
            model.CompanyCD = companyCD;
            model.UsedStatus = "1";
            DataTable dt1 = StorageBus.GetStorageListBycondition(model);
            if (dt1.Rows.Count > 0)
            {
                ddlStorage.DataSource = dt1;
                ddlStorage.DataTextField = "StorageName";
                ddlStorage.DataValueField = "ID";
                ddlStorage.DataBind();
                ddlStorage.Items.Insert(0, new ListItem("--请选择--", ""));
            }
            HidStartDate.Value = System.DateTime.Now.AddDays(1 - System.DateTime.Now.Day).ToString("yyyy-MM-dd");
            HidEndDate.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
            this.hidModuleID.Value = ConstUtil.MODULE_ID_STORAGE_STORAGEJOURNAL;



            //点击查询时，设置查询的条件，并执行查询
            if (Request.QueryString["Flag"] != null)
            {
                this.ddlStorage.SelectedValue = Request.QueryString["ddlStorage"].ToString();
                this.txtProductNo.Value = Request.QueryString["txtProductNo"].ToString();
                // this.txtProviderName.Text = Request.QueryString["txtProductName"].ToString();
                this.txtStartDate.Text = Request.QueryString["StartDate"].ToString();
                this.txtEndDate.Text = Request.QueryString["EndDate"].ToString();
                this.hiddenProductID.Value = Request.QueryString["ProductID"].ToString();
                this.txtProviderID.Value = Request.QueryString["ProviderID"].ToString();
                this.txtProviderName.Text = Request.QueryString["ProviderName"].ToString();
                this.UserCreator.Text = Request.QueryString["CreatorName"].ToString();
                this.txtCreatorID.Value = Request.QueryString["CreatorID"].ToString();
                this.ddlSourceType.SelectedValue = Request.QueryString["SourceType"].ToString();
                this.txtSourceNo.Text = Request.QueryString["SourceNo"].ToString();
                if (Request.QueryString["ckbIsM"].ToString() == "1")
                {
                    this.ckbIsM.Checked = true;
                }
                //获取当前页
                string pageIndex = Request.QueryString["pageIndex"];
                //获取每页显示记录数 
                string pageCount = Request.QueryString["pageCount"];
                //执行查询
                ClientScript.RegisterStartupScript(this.GetType(), "Fun_Search_StorageInfo"
                        , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");Fun_Search_StorageInfo('" + pageIndex + "');</script>");
            }

        }
        this.txtStartDate.Text = HidStartDate.Value;
        this.txtEndDate.Text = HidEndDate.Value;
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {


        int pageIndex = 1;
        int pageCount = 10000;
        int totalCount = 0;



        string orderBy = txtorderBy.Value;
        if (!string.IsNullOrEmpty(orderBy))
        {
            if (orderBy.Split('_')[1] == "a")
            {
                orderBy = orderBy.Split('_')[0] + " asc";
            }
            else
            {
                orderBy = orderBy.Split('_')[0] + " desc";
            }
        }
        string EFindex = hiddenEFIndex.Value.Trim();
        string EFDesc = hiddenEFDesc.Value.Trim();
        string EFName = hiddenEFIndexName.Value;
        string extQueryStr = string.Empty;
        if (!string.IsNullOrEmpty(EFindex) && !string.IsNullOrEmpty(EFDesc))
        {
            extQueryStr = "   b.ExtField" + EFindex + " LIKE '%" + EFDesc + "%' " + "@b.ExtField" + EFindex;
        }
        string sidex = "ExtField" + EFindex;
        //设置查询条件
        string ddlStorage, txtProductNo, ProductID, StartDate, EndDate, ProviderID, ckbIsM, SourceNo = "";
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        ddlStorage = this.ddlStorage.SelectedValue;
        //txtProductName = this.txtProductName.Value;
        txtProductNo = this.txtProductNo.Value;
        ProductID = this.hiddenProductID.Value;
        StartDate = this.txtStartDate.Text;
        EndDate = this.txtEndDate.Text;
        ProviderID = this.txtProviderID.Value;
        SourceNo = this.txtSourceNo.Text;
        if (this.ckbIsM.Checked)
        {
            ckbIsM = "1";
        }
        else
        {
            ckbIsM = "0";
        }
        string BatchNo = this.ddlBatchNo.SelectedValue;
        string ListID = StorageBus.GetStorageIDStr(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID, ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
        DataTable dt = new DataTable();

        if (string.IsNullOrEmpty(ProviderID))
        {
            string QueryStr = " and  CompanyCD='" + CompanyCD + "' ";
            if (ddlStorage.Trim().Length > 0 && ddlStorage.Trim().Length > 0)
            {
                QueryStr += " and StorageID= '";
                QueryStr += ddlStorage;
                QueryStr += "' ";
            }
            if (ProductID.Trim().Length > 0 && ProductID.Trim().Length > 0)
            {
                QueryStr += " and ProductID= '";
                QueryStr += ProductID;
                QueryStr += "' ";
            }

            if (StartDate.Trim().Length > 0)
            {
                QueryStr += " and HappenDate >='";
                QueryStr += StartDate + " 00:00:00";
                QueryStr += "' ";
            }

            if (EndDate.Trim().Length > 0)
            {
                QueryStr += " and HappenDate <='";
                QueryStr += EndDate + " 23:59:59";
                QueryStr += "' ";
            }


            if (BatchNo != "0")
            {
                if (BatchNo == "未设置批次")
                {
                    QueryStr += " and ( BatchNo is null or BatchNo='') ";
                }
                else
                {
                    QueryStr += " and BatchNo='" + BatchNo + "' ";
                }
            }



            if (SourceNo.Trim().Length > 0 && SourceNo != "0")
            {
                if (ckbIsM == "0")
                {
                    QueryStr += " and BillNo= '";
                    QueryStr += SourceNo;
                    QueryStr += "' ";
                }
                else
                {
                    QueryStr += " and BillNo like '%";
                    QueryStr += SourceNo;
                    QueryStr += "%' ";
                }
            }



            dt = StrongeJournalBus.GetSumStrongJournal(QueryStr, extQueryStr, pageIndex, pageCount, " ProductID asc ", ref totalCount);
            if (!string.IsNullOrEmpty(EFindex) && !string.IsNullOrEmpty(EFDesc))
            {
                OutputToExecl.ExportToTableFormat(this, dt,
        new string[] { "仓库编号", "仓库名称", "批次", "物品编号", "物品名称", "规格型号", "尺寸", "产地", "入库总量", "出库总量", "现有存量", EFName },
        new string[] { "StorageNo", "StorageName", "BatchNo", "ProdNo", "ProductName", "Specification", "ProductSize", "FromAddr", "InProductCount", "OutProductCount", "NowProductCount", sidex },
        "库存流水账列表");
            }
            else
            {
                OutputToExecl.ExportToTableFormat(this, dt,
                    new string[] { "仓库编号", "仓库名称", "批次", "物品编号", "物品名称", "规格型号", "尺寸", "产地", "入库总量", "出库总量", "现有存量" },
                    new string[] { "StorageNo", "StorageName", "BatchNo", "ProdNo", "ProductName", "Specification", "ProductSize", "FromAddr", "InProductCount", "OutProductCount", "NowProductCount" },
                    "库存流水账列表");

            }





        }
        else
        {
            string QueryStr = " and  a.CompanyCD='" + CompanyCD + "' and c.ProviderID='" + ProviderID + "'  ";
            if (ddlStorage.Trim().Length > 0 && ddlStorage.Trim().Length > 0)
            {
                QueryStr += " and b.StorageID= '";
                QueryStr += ddlStorage;
                QueryStr += "' ";
            }
            if (ProductID.Trim().Length > 0 && ProductID.Trim().Length > 0)
            {
                QueryStr += " and b.ProductID= '";
                QueryStr += ProductID;
                QueryStr += "' ";
            }

            if (StartDate.Trim().Length > 0)
            {
                QueryStr += " and a.EnterDate >='";
                QueryStr += StartDate + " 00:00:00";
                QueryStr += "' ";
            }

            if (EndDate.Trim().Length > 0)
            {
                QueryStr += " and a.EnterDate <='";
                QueryStr += EndDate + " 23:59:59";
                QueryStr += "' ";
            }

            if (BatchNo != "0")
            {
                if (BatchNo == "未设置批次")
                {
                    QueryStr += " and ( b.BatchNo is null or b.BatchNo='') ";
                }
                else
                {
                    QueryStr += " and b.BatchNo='" + BatchNo + "' ";
                }
            }


            //查询数据
            //DataTable dt = AcountBookBus.GetAcountBookInfo(QueryStr);
            dt = StrongeJournalBus.GetStrongJournalByPro(QueryStr, extQueryStr, pageIndex, pageCount, " ProductID asc ", ref totalCount);

            if (!string.IsNullOrEmpty(EFindex) && !string.IsNullOrEmpty(EFDesc))
            {
                OutputToExecl.ExportToTableFormat(this, dt,
               new string[] { "仓库编号", "仓库名称", "批次", "物品编号", "物品名称", "规格型号", "尺寸", "产地", "入库总量", EFName },
               new string[] { "StorageNo", "StorageName", "BatchNo", "ProdNo", "ProductName", "Specification", "ProductSize", "FromAddr", "ProductCount", sidex },
               "库存流水账列表");
            }
            else
            {
                OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "仓库编号", "仓库名称", "批次", "物品编号", "物品名称", "规格型号", "尺寸", "产地", "入库总量" },
                new string[] { "StorageNo", "StorageName", "BatchNo", "ProdNo", "ProductName", "Specification", "ProductSize", "FromAddr", "ProductCount" },
                "库存流水账列表");
            }
        }






    }
}
