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
using XBase.Business.Office.SystemManager;


public partial class Pages_Office_StorageManager_StorageJournalDetails : BasePage
{
    private string companyCD = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (!IsPostBack)
        {
            BindCom();
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
            //this.txtStartDate.Text = System.DateTime.Now.AddDays(1 - System.DateTime.Now.Day).ToString("yyyy-MM-dd");
            //this.txtEndDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            this.hidModuleID.Value = ConstUtil.MODULE_ID_STORAGE_STORAGEJOURNAL;



            //点击查询时，设置查询的条件，并执行查询
            if (Request.QueryString["Flag"] != null)
            {
                this.ddlStorage.SelectedValue = Request.QueryString["ddlStorage"].ToString();
                this.txtProductNo.Value = Request.QueryString["txtProductNo"].ToString();
                //this.txtProductName.Value = Request.QueryString["txtProductName"].ToString();
                this.txtStartDate.Text = Request.QueryString["StartDate"].ToString();
                this.txtEndDate.Text = Request.QueryString["EndDate"].ToString();
                this.hiddenProductID.Value = Request.QueryString["ProductID"].ToString();
                //获取当前页
                string pageIndex = Request.QueryString["pageIndex"];
                //获取每页显示记录数 
                string pageCount = Request.QueryString["pageCount"];
                //执行查询
                //ClientScript.RegisterStartupScript(this.GetType(), "Fun_Search_StorageInfo"
                //        , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");Fun_Search_StorageInfo('" + pageIndex + "');</script>");
            }

        }
      
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        int pageIndex = 1;
        int pageCount = 10000;
        int totalCount = 0;

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //设置查询条件
        string ddlStorage, SourceType, SourceNo, CreatorID, ProductID, StartDate, EndDate, BatchNo, EFindex, EFDesc, Specification, ColorID, Material, Manufacturer, Size, FromAddr, BarCode, ckbIsM = "";

        ddlStorage = this.ddlStorage.SelectedValue;
        ProductID = this.hiddenProductID.Value;
        StartDate = this.txtStartDate.Text;
        EndDate = this.txtEndDate.Text;
        BatchNo = this.hiddenBatchNo.Value;
        SourceType = this.ddlSourceType.SelectedValue;
        SourceNo = this.txtSourceNo.Text;
        CreatorID = this.txtCreatorID.Value;


        EFindex = hiddenEFIndex.Value.Trim();
        EFDesc = hiddenEFDesc.Value.Trim();

        Specification = this.txt_Specification.Value;
        ColorID = this.sel_ColorID.SelectedValue;
        Material = this.sel_Material.SelectedValue;
        Manufacturer = this.txt_Manufacturer.Text;
        Size = this.txt_Size.Value;
        FromAddr = this.txt_FromAddr.Text;
        BarCode = this.txt_BarCode.Value;

        if (this.ckbIsM.Checked)
        {
            ckbIsM = "1";
        }
        else
        {
            ckbIsM = "0";
        }

        DataTable dt = new DataTable();
        string QueryStr = "  a.CompanyCD='" + CompanyCD + "' ";
        if (ddlStorage.Trim().Length > 0 && ddlStorage != "0")
        {
            QueryStr += " and a.StorageID= '";
            QueryStr += ddlStorage;
            QueryStr += "' ";
        }

        if (ProductID.Trim().Length > 0)
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


        if (SourceType.Trim().Length > 0 && SourceType != "0")
        {
            QueryStr += " and a.BillType= '";
            QueryStr += SourceType;
            QueryStr += "' ";
        }


        if (SourceNo.Trim().Length > 0 && SourceNo != "0")
        {
            if (ckbIsM == "0")
            {
                QueryStr += " and a.BillNo= '";
                QueryStr += SourceNo;
                QueryStr += "' ";
            }
            else
            {
                QueryStr += " and a.BillNo like '%";
                QueryStr += SourceNo;
                QueryStr += "%' ";
            }
        }


        if (CreatorID.Trim().Length > 0 && CreatorID != "0")
        {
            QueryStr += " and a.Creator= '";
            QueryStr += CreatorID;
            QueryStr += "' ";
        }


        if (!string.IsNullOrEmpty(EFindex) && !string.IsNullOrEmpty(EFDesc))
        {
            QueryStr += " and   b.ExtField" + EFindex + " LIKE '%" + EFDesc + "%' ";
        }


        if (!string.IsNullOrEmpty(Specification))
        {
            QueryStr += "  and   b.Specification LIKE '%" + Specification + "%' ";
        }

        if (!string.IsNullOrEmpty(ColorID) && ColorID != "0")
        {
            QueryStr += " and    b.ColorID= '" + ColorID + "' ";
        }

        if (!string.IsNullOrEmpty(Material) && Material != "0")
        {
            QueryStr += "  and   b.Material = '" + Material + "' ";
        }
        if (!string.IsNullOrEmpty(Manufacturer))
        {
            QueryStr += " and   b.Manufacturer LIKE '%" + Manufacturer + "%' ";
        }
        if (!string.IsNullOrEmpty(Size))
        {
            QueryStr += " and   b.Size LIKE '%" + Size + "%' ";
        }
        if (!string.IsNullOrEmpty(FromAddr))
        {
            QueryStr += "  and   b.FromAddr LIKE '%" + FromAddr + "%' ";
        }
        if (!string.IsNullOrEmpty(BarCode))
        {
            QueryStr += " and   b.BarCode LIKE '%" + BarCode + "%' ";
        }













        //查询数据
        //DataTable dt = AcountBookBus.GetAcountBookInfo(QueryStr);
        dt = StrongeJournalBus.GetDetailStrongJournal(QueryStr, pageIndex, pageCount, " ProductID desc", ref totalCount);
        

        OutputToExecl.ExportToTableFormat(this, dt,
              new string[] { "单据编号","单据类别","仓库编号", "仓库名称", "批次", "物品编号", "物品名称","单位", "规格型号","颜色", "尺寸", "产地", "出入库时间", "出入库数量","结存量","创建人","备注" },
              new string[] { "BillNo", "typeflag", "StorageNo", "StorageName", "BatchNo", "ProdNo", "ProductName", "CodeName", "Specification", "ColorName", "ProductSize", "FromAddr", "EnterDate", "ProductCount", "NowProductCount", "CreatorName", "ReMark" },
              "库存流水账明细");
    }


    private void BindCom()
    {
        string TypeFlag = "5";
        string Code = "2";
        DataTable dt = null;
       
        Code = "3";
        dt = CodePublicTypeBus.GetCodePublicByCode(TypeFlag, Code);//绑定颜色
        if (dt.Rows.Count > 0)
        {
            this.sel_ColorID.DataTextField = "TypeName";
            sel_ColorID.DataValueField = "ID";
            sel_ColorID.DataSource = dt;
            sel_ColorID.DataBind();
        }
        sel_ColorID.Items.Insert(0, new ListItem("--请选择--", "0"));
        Code = "5";
        dt = CodePublicTypeBus.GetCodePublicByCode(TypeFlag, Code);//绑定材质
        if (dt.Rows.Count > 0)
        {
            this.sel_Material.DataTextField = "TypeName";
            sel_Material.DataValueField = "ID";
            sel_Material.DataSource = dt;
            sel_Material.DataBind();
        }
        sel_Material.Items.Insert(0, new ListItem("--请选择--", "0"));

    }



   
}
