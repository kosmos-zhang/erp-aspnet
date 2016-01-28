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
using System.Xml.Linq;
using XBase.Business.Office.SystemManager;
using XBase.Business.Office.SupplyChain;
using XBase.Common;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using XBase.Model.Office.SupplyChain;
public partial class Pages_Office_SupplyChain_ProductInfoAdd : BasePage
{

    #region System Init ModuleID
    public int SysModuleID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["SysModuleID"], out tempID);
            return tempID;
        }
    }
    #endregion

    #region 变量
    /// <summary>
    /// 是否启用多计量单位(true：启用；false：不启用)
    /// </summary>
    private bool _isMoreUnit = false;

    #endregion


    #region 属性
    /// <summary>
    /// 是否启用多计量单位(ture：启用；false：不启用)
    /// </summary>
    public bool IsMoreUnit
    {
        get
        {
            return _isMoreUnit;
        }
        set
        {
            _isMoreUnit = value;
        }
    }
    #endregion


    private string TypeFlag = "";
    public int intOtherCorpInfoID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["intOtherCorpInfoID"], out tempID);
            return tempID;
        }
    }
    private string Code = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            // 多计量单位控制
            _isMoreUnit = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit;
            //模板列表模块ID
            this.txtModifiedDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
            this.txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            hidModuleID.Value = ConstUtil.Menu_SerchProduct;
            //获取请求参数
            string requestParam = Request.QueryString.ToString();
            //通过参数个数来判断是否从菜单过来
            int firstIndex = requestParam.IndexOf("&");
            //从列表过来时
            if (firstIndex > 0)
            {
                //返回按钮可见
                product_btnback.Visible = true;
                //获取列表的查询条件
                string searchCondition = requestParam.Substring(firstIndex);
                //去除参数
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.Menu_AddProduct, string.Empty);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
                //迁移页面
                hidFromPage.Value = Request.QueryString["FromPage"];
            }
            else
            {
                //返回按钮不可见
                product_btnback.Visible = false;
            }
            CodingRuleControl1.CodingType = ConstUtil.CODING_RULE_TYPE_ZERO;
            CodingRuleControl1.ItemTypeID = ConstUtil.CODINGA_BASE_ITEM_PRODUCT;
            //CodingRuleControl1.TableName = "ProductInfo";
            //CodingRuleControl1.ColumnName = "ProdNo";
            this.txt_CheckUser.Value = Convert.ToString(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
            this.txtPrincipal.Value = Convert.ToString(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
            this.UserPrincipal.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            this.txt_CheckUserName.Text = Convert.ToString(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName);
            this.txt_CheckDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.txt_CreateDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
            BindTree();
            BindCom();//绑定下拉框
            if (Request["intOtherCorpInfoID"] != "" && Request["intOtherCorpInfoID"] != null)
            {
                if (ProductInfoBus.IsConfirmProduct(Request["intOtherCorpInfoID"]))
                {
                    this.txt_IsConfirmProduct.Value = "1";
                }
                ProductInfoModel model = new ProductInfoModel();
                DataTable dt = ProductInfoBus.GetProductInfoByID(int.Parse(Request["intOtherCorpInfoID"]));
                if (dt.Rows.Count > 0)
                {
                    this.txtModifiedUserID.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ModifiedUserID");
                    this.txtModifiedDate.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ModifiedDate");
                    this.divNo.InnerHtml = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ProdNo");
                    txt_PYShort.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "PYShort");
                    this.txt_Manufacturer.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Manufacturer");
                    txt_ProductName.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ProductName");
                    txt_ShortNam.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ShortNam");
                    txt_BarCode.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "BarCode");
                    txt_BigType.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "BigType");
                    sel_GradeID.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "GradeID");
                    sel_UnitID.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "UnitID");
                    sel_Brand.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Brand");
                    sel_ColorID.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ColorID");
                    txt_Specification.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Specification");
                    txt_Size.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Size");
                    sel_Source.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Source");
                    txt_FromAddr.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "FromAddr");
                    txt_DrawingNum.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "DrawingNum");
                    //txt_ImgUrl.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ImgUrl");
                    txt_FileNo.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "FileNo");
                    txt_PricePolicy.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "PricePolicy");
                    txt_Params.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Params");
                    txt_Questions.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Questions");
                    txt_ReplaceName.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ReplaceName");
                    txt_Description.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Description");
                    if (dt.Rows[0]["IsBatchNo"].ToString() == "1")
                    {
                        RdUseBatch.Checked = true;
                        RdNotUseBatch.Checked = false;
                    }
                    else
                    {
                        RdUseBatch.Checked = false;
                        RdNotUseBatch.Checked = true;
                    }


                    this.txt_CheckUserName.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "CheckUserName");

                    this.txtPrincipal.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Creator");
                    this.UserPrincipal.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "CreatorName");
                    var photoURL = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ImgUrl"); ;
                    string StockIs = GetSafeData.ValidateDataRow_String(dt.Rows[0], "StockIs");
                    string MinusIs = GetSafeData.ValidateDataRow_String(dt.Rows[0], "MinusIs");
                    if (StockIs == "1") { rd_StockIs.Checked = true; rd_notStockIs.Checked = false; }
                    if (StockIs == "0") { rd_notStockIs.Checked = true; rd_StockIs.Checked = false; }
                    if (MinusIs == "1") { this.rd_MinusIs.Checked = true; this.rd_notMinusIs.Checked = false; }
                    if (MinusIs == "0") { this.rd_notMinusIs.Checked = true; this.rd_MinusIs.Checked = false; }
                    //chk_MinusIs.Checked=StockIs=="0"?false:true;
                    //chk_StockIs.Checked = StockIs == "0" ?  false: true;
                    this.HdGroupNo.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "GroupUnitNo");
                    if (!String.IsNullOrEmpty(dt.Rows[0]["GroupUnitNo"].ToString()))
                    {
                        this.txtUnitGroup.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "GroupUnitNo") + "_" + GetSafeData.ValidateDataRow_String(dt.Rows[0], "GroupUnitName");
                    }
                    
                    if (!string.IsNullOrEmpty(HdGroupNo.Value))
                    {
                        string GroupUnitNo = HdGroupNo.Value;
                        DataTable dt_GroupUnit = ProductInfoBus.GetUnitGroupList(GroupUnitNo);
                        if (dt_GroupUnit.Rows.Count > 0)
                        {
                            //库存单位
                            selStorageUnit.DataSource = dt_GroupUnit;
                            selStorageUnit.DataTextField = "CodeName";
                            selStorageUnit.DataValueField = "UnitID";
                            selStorageUnit.DataBind();
                            //采购单位
                            selPurchseUnit.DataSource = dt_GroupUnit;
                            selPurchseUnit.DataTextField = "CodeName";
                            selPurchseUnit.DataValueField = "UnitID";
                            selPurchseUnit.DataBind();
                            //销售
                            selSellUnit.DataSource = dt_GroupUnit;
                            selSellUnit.DataTextField = "CodeName";
                            selSellUnit.DataValueField = "UnitID";
                            selSellUnit.DataBind();
                            //生产
                            selProductUnit.DataSource = dt_GroupUnit;
                            selProductUnit.DataTextField = "CodeName";
                            selProductUnit.DataValueField = "UnitID";
                            selProductUnit.DataBind();
                        }
                        //selSellUnit.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "SaleUnitID");
                        this.selStorageUnit.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "StockUnitID");
                        this.selPurchseUnit.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "InUnitID");
                        this.selSellUnit.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "SaleUnitID");
                        this.selProductUnit.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "MakeUnitID");
                    }
                    else
                    {
                        //往各业务单位组里添加默认的单位
                        selStorageUnit.Items.Insert(0, new ListItem(sel_UnitID.SelectedItem.Text, sel_UnitID.SelectedValue));
                        selPurchseUnit.Items.Insert(0, new ListItem(sel_UnitID.SelectedItem.Text, sel_UnitID.SelectedValue));
                        selSellUnit.Items.Insert(0, new ListItem(sel_UnitID.SelectedItem.Text, sel_UnitID.SelectedValue));
                        selProductUnit.Items.Insert(0, new ListItem(sel_UnitID.SelectedItem.Text, sel_UnitID.SelectedValue));
                    }

                    if (photoURL == "")
                    {
                        imgPhoto.Src = "../../../Images/Pic/Pic_Nopic.jpg";
                        //document.getElementById("imgPhoto").src = "../../../Images/Pic/Pic_Nopic.jpg";
                    }
                    else
                    {
                        //document.getElementById("imgPhoto").src = "../../../Images/Photo/" + photoURL;
                        imgPhoto.Src = "../../../Images/Photo/" + photoURL;
                        hfPagePhotoUrl.Value = photoURL;
                        hfPagePhotoUrl.Value = photoURL;
                    }

                    sel_StorageID.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "StorageID");
                    sel_Material.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Material");
                    txt_SafeStockNum.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "SafeStockNum");
                    txt_MinStockNum.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "MinStockNum");
                    txt_MaxStockNum.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "MaxStockNum");
                    sel_ABCType.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ABCType");
                    sel_CalcPriceWays.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "CalcPriceWays");

                    txt_StandardCost.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "StandardCost");
                    txt_PlanCost.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "PlanCost");
                    txt_StandardSell.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "StandardSell");
                    txt_SellMin.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "SellMin");
                    txt_SellMax.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "SellMax");
                    txt_TaxRate.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "TaxRate");
                    txt_InTaxRate.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "InTaxRate");
                    txt_SellTax.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "SellTax");
                    txt_SellPrice.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "SellPrice");
                    txt_TransfrePrice.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "TransferPrice");
                    txt_Discount.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Discount");
                    txt_StandardBuy.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "StandardBuy");
                    txt_TaxBuy.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "TaxBuy");
                    txt_BuyMax.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "BuyMax");

  
                    txt_Remark.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Remark");
                    txt_CreateDate.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "CreateDate");
                    sel_CheckStatus.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "CheckStatus");
                    if (sel_CheckStatus.Value == "1")
                    {
                        txt_CheckDate.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "CheckDate");
                        divConfirmor.Attributes.Add("style", "display:block;");
                        this.txt_CheckUser.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "CheckUser");
                        this.txt_CheckUserName.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "CheckUserName");
                    }
                    sel_UsedStatus.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "UsedStatus");
                    txt_Code.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "TypeID");//隐藏
                    string Flag = GetSafeData.ValidateDataRow_String(dt.Rows[0], "TypeFlag");
                    txt_BigType.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "BigType");//隐藏
                    txt_TypeID.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "CodeName");//隐藏
                    storage.Attributes.Add("style", "display:block;");
                    divInputNo.Attributes.Add("style", "display:none;float:left");
                    divNo.Attributes.Add("style", "display:block;float:left");
                    DataTable dt_stor = ProductInfoBus.GetStorageCount(int.Parse(Request["intOtherCorpInfoID"]));
                    if (dt_stor.Rows[0]["ProductCount"].ToString().IndexOf('.') > -1)
                        this.txt_Storage.Value = dt_stor.Rows[0]["ProductCount"].ToString();
                    //txt_TypeID.Disabled = true;
                    switch (Flag)
                    {
                        case "1":
                            this.txt_BigTypeName.Value = "成品";
                            break;
                        case "2":
                            this.txt_BigTypeName.Value = "原材料";
                            break;
                        case "3":
                            this.txt_BigTypeName.Value = "固定资产";
                            break;
                        case "4":
                            this.txt_BigTypeName.Value = "低值易耗";
                            break;
                        case "5":
                            this.txt_BigTypeName.Value = "包装物";
                            break;
                        case "6":
                            this.txt_BigTypeName.Value = "服务产品";
                            break;
                        case "7":
                            this.txt_BigTypeName.Value = "半成品";
                            break;
                    }
                    //this.txt_CheckUserName.Text = Convert.ToString(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName);
                    //this.txt_CheckDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());

                }
            }



        }
    }
    /// <summary>
    /// 绑定树
    /// </summary>
    private void BindTree()
    {
        DataTable dt_product = CategorySetBus.GetProductType();
        DataView dataView = dt_product.DefaultView;
        string BigtypeName = "";
        for (int i = 1; i < 8; i++)
        {
            dataView.RowFilter = "TypeFlag='" + i + "'";
            DataTable dtnew = new DataTable();
            dtnew = dataView.ToTable();
            TreeNode node = new TreeNode();
            switch (i)
            {
                case 1:
                    BigtypeName = "成品";
                    break;
                case 2:
                    BigtypeName = "原材料";
                    break;
                case 3:
                    BigtypeName = "固定资产";
                    break;
                case 4:
                    BigtypeName = "低值易耗";
                    break;
                case 5:
                    BigtypeName = "包装物";
                    break;
                case 6:
                    BigtypeName = "服务产品";
                    break;
                case 7:
                    BigtypeName = "半成品";
                    break;
            }
            try
            {
                node.Value = dtnew.Rows[0]["TypeFlag"].ToString();
                node.Text = BigtypeName;
                node.NavigateUrl = string.Format("javascript:javascript:void(0)");
                BindTreeChildNodes(node, dtnew);
                this.TreeView1.Nodes.Add(node);
                //TreeView1.Attributes.Add("onclick", "OnTreeNodeClick()");
                node.Expanded = false;
            }
            catch
            {

            }

        }

    }


    private void BindTreeChildNodes(TreeNode node, DataTable dt)
    {
        foreach (DataRow row in dt.Select("SupperID=0"))
        {
            TreeNode nodes = new TreeNode();
            nodes.Text = row["CodeName"].ToString();
            nodes.Value = row["ID"].ToString();
            TypeFlag = row["TypeFlag"].ToString();
            nodes.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}','{2}');", nodes.Text, nodes.Value, TypeFlag);

            LoadSubData(row["ID"].ToString(), nodes, dt);
            node.ChildNodes.Add(nodes);
            node.Expanded = false;
        }
    }
    private void LoadSubData(string pid, TreeNode nodes, DataTable dt)
    {
        foreach (DataRow row in dt.Select("SupperID=" + pid))
        {
            TreeNode nodess = new TreeNode();
            nodess.Text = row["CodeName"].ToString();
            nodess.Value = row["ID"].ToString();
            TypeFlag = row["TypeFlag"].ToString();
            nodess.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}','{2}');", nodess.Text, nodess.Value, TypeFlag);
            LoadSubData(row["ID"].ToString(), nodess, dt);
            nodes.ChildNodes.Add(nodess);
            nodes.Expanded = false;
        }
    }

    /// <summary>
    /// 绑定所有下拉框
    /// </summary>
    private void BindCom()
    {
        TypeFlag = "5";
        Code = "2";
        DataTable dt = null;
        dt = CodePublicTypeBus.GetCodePublicByCode(TypeFlag, Code);//绑定等级
        if (dt.Rows.Count > 0)
        {
            this.sel_GradeID.DataTextField = "TypeName";
            sel_GradeID.DataValueField = "ID";
            sel_GradeID.DataSource = dt;
            sel_GradeID.DataBind();
        }
        sel_GradeID.Items.Insert(0, new ListItem("--请选择--", "0"));
        Code = "1";
        dt = CodePublicTypeBus.GetCodePublicByCode(TypeFlag, Code);//绑定品牌
        if (dt.Rows.Count > 0)
        {
            this.sel_Brand.DataTextField = "TypeName";
            sel_Brand.DataValueField = "ID";
            sel_Brand.DataSource = dt;
            sel_Brand.DataBind();
        }
        sel_Brand.Items.Insert(0, new ListItem("--请选择--", "0"));
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
        dt = CodeReasonFeeBus.GetCodeUnitType();//绑定单位
        if (dt.Rows.Count > 0)
        {
            this.sel_UnitID.DataTextField = "CodeName";
            sel_UnitID.DataValueField = "ID";
            sel_UnitID.DataSource = dt;
            sel_UnitID.DataBind();
        }
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
        StorageModel model = new StorageModel();
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        model.CompanyCD = userInfo.CompanyCD;
        DataTable dt_Stor = StorageBus.GetStorageListBycondition(model);//绑定仓库
        if (dt_Stor.Rows.Count > 0)
        {
            sel_StorageID.DataSource = dt_Stor;
            sel_StorageID.DataTextField = "StorageName";
            sel_StorageID.DataValueField = "ID";
            sel_StorageID.DataBind();
        }
        //DataTable dt_BindUnit =new DataTable();
        //dt_BindUnit = ProductInfoBus.GetUnitGroupList(this.txtUnitGroup.Value);
    }
}
