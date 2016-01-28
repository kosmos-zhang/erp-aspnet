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

public partial class Pages_Office_StorageManager_StorageJournalDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
            this.PID.Value = Request.QueryString["PID"].ToString();
            this.SID.Value = Request.QueryString["SID"].ToString();
            this.SD.Value = Request.QueryString["SD"].ToString();
            this.ED.Value = Request.QueryString["ED"].ToString();
            this.PRD.Value = Request.QueryString["ProviderID"].ToString();
            this.BatchNo.Value = Request.QueryString["BNo"].ToString();
            //获取请求参数
            string requestParam = Request.QueryString.ToString();
            //通过参数个数来判断是否从菜单过来
            this.hiddenSearch.Value = requestParam;

        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {


        int pageIndex = 1;
        int pageCount = 10000;
        int totalCount = 0;

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //设置查询条件
        string ddlStorage, ProductID, StartDate, EndDate, ProviderID, BatchNo = "";

        ddlStorage = Request.QueryString["SID"].Trim().ToString();
        ProductID = Request.QueryString["PID"].Trim().ToString();
        StartDate = Request.QueryString["SD"].Trim().ToString();
        EndDate = Request.QueryString["ED"].Trim().ToString();
        ProviderID = Request.QueryString["ProviderID"].Trim().ToString();
        BatchNo = Request.QueryString["BNo"].ToString();


        DataTable dt = new DataTable();
        if (string.IsNullOrEmpty(ProviderID))
        {
            string QueryStr = "  a.CompanyCD='" + CompanyCD + "' ";
            if (ddlStorage.Trim().Length > 0 && ddlStorage.Trim().Length > 0)
            {
                QueryStr += " and a.StorageID= '";
                QueryStr += ddlStorage;
                QueryStr += "' ";
            }

            if (ProductID.Trim().Length > 0 && ProductID.Trim().Length > 0)
            {
                QueryStr += " and a.ProductID= '";
                QueryStr += ProductID;
                QueryStr += "' ";
            }

            if (StartDate.Trim().Length > 0)
            {
                QueryStr += " and a.HappenDate >='";
                QueryStr += StartDate + " 00:00:00";
                QueryStr += "' ";
            }

            if (EndDate.Trim().Length > 0)
            {
                QueryStr += " and a.HappenDate <='";
                QueryStr += EndDate + " 23:59:59";
                QueryStr += "' ";
            }


            if (BatchNo != "0")
            {
                if (BatchNo == "未设置批次" || string.IsNullOrEmpty(BatchNo))
                {
                    QueryStr += " and ( a.BatchNo is null or a.BatchNo='') ";
                }
                else
                {
                    QueryStr += " and a.BatchNo='" + BatchNo + "' ";
                }
            }



            dt = StrongeJournalBus.GetDetailStrongJournal(QueryStr, pageIndex, pageCount, " ProductID desc ", ref totalCount);
        }
        else
        {
            string QueryStr = " and  a.CompanyCD='" + CompanyCD + "' and c.ProviderID='" + ProviderID + "' ";
            if (ddlStorage.Trim().Length > 0 && ddlStorage.Trim().Length > 0)
            {
                QueryStr += " and e.StorageID= '";
                QueryStr += ddlStorage;
                QueryStr += "' ";
            }

            if (ProductID.Trim().Length > 0 && ProductID.Trim().Length > 0)
            {
                QueryStr += " and e.ProductID= '";
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
                    QueryStr += " and ( e.BatchNo is null or e.BatchNo='') ";
                }
                else
                {
                    QueryStr += " and e.BatchNo='" + BatchNo + "' ";
                }
            }
            dt = StrongeJournalBus.GetDetailStrongJournalByPro(QueryStr, pageIndex, pageCount, " ProductID desc ", ref totalCount);
        }

        OutputToExecl.ExportToTableFormat(this, dt,
              new string[] { "仓库编号", "仓库名称", "批次","物品编号", "物品名称", "规格型号","颜色", "尺寸", "产地", "出入库时间", "出入库数量" },
              new string[] { "StorageNo", "StorageName", "BatchNo", "ProdNo", "ProductName", "Specification", "ColorName", "ProductSize", "FromAddr", "EnterDate", "ProductCount" },
              "库存流水账明细");
    }
}
