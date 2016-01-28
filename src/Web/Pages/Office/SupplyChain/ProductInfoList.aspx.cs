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
public partial class Pages_Office_SupplyChain_ProductInfoList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindTree();
        if (!IsPostBack)
        {
            BindColor();
            this.hidModuleID.Value = ConstUtil.Menu_AddProduct;
            GetBillExAttrControl1.TableName = "officedba.ProductInfo";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            GetBillExAttrControl1.ExtIndex = EFIndex;
            GetBillExAttrControl1.ExtValue = EFDesc;
            GetBillExAttrControl1.SetExtControlValue();
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
                    this.txt_TypeID.Value = Request.QueryString["txt_TypeID"];
                    txt_ProdNo.Value = Request.QueryString["ProdNo"];
                    txt_PYShort.Value = Request.QueryString["PYShort"];
                    txt_ProductName.Text = Request.QueryString["ProductName"];
                    txt_BarCode.Value = Request.QueryString["BarCode"];
                    this.selCorlor.SelectedValue = Request.QueryString["Color"];
                    this.txt_Specification.Value = Request.QueryString["Specification"];
                    this.UsedStatus.Value = Request.QueryString["UsedStatus"];
                    this.CheckStatus.Value = Request.QueryString["CheckStatus"];
                    //获取当前页
                    string pageIndex = Request.QueryString["PageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["PageCount"];
                    //执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");Fun_Search_ProductInfo('" + pageIndex + "');</script>");
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
            string TypeFlag = row["TypeFlag"].ToString();
            nodes.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}','{2}');", nodes.Text, nodes.Value, TypeFlag);
            //DataTable dt_new = CategorySetBus.GetProductTypeInfo(int.Parse(row["ID"]));
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
            string TypeFlag = row["TypeFlag"].ToString();
            nodess.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}','{2}');", nodess.Text, nodess.Value, TypeFlag);
            LoadSubData(row["ID"].ToString(), nodess, dt);
            nodes.ChildNodes.Add(nodess);
            nodes.Expanded = false;
        }
    }
    /// <summary>
    /// 绑定颜色
    /// </summary>
    private void BindColor()
    {
        string TypeFlag = "5";
        string Code = "3";
        DataTable dt = CodePublicTypeBus.GetCodePublicByCode(TypeFlag, Code);
        if (dt.Rows.Count > 0)
        {
            this.selCorlor.DataTextField = "TypeName";
            selCorlor.DataValueField = "ID";
            selCorlor.DataSource = dt;
            selCorlor.DataBind();
        }
        selCorlor.Items.Insert(0, new ListItem("--请选择--", ""));
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        ProductInfoModel model = new ProductInfoModel();
        model.ProdNo = txt_ProdNo.Value;
        model.ProductName = txt_ProductName.Text;
        model.TypeID = txt_ID.Value;
        model.UsedStatus = UsedStatus.Value;
        model.PYShort = txt_PYShort.Value;
        model.Specification = txt_Specification.Value;
        model.BarCode = this.HiddenBarCode.Value;
        model.CheckStatus = CheckStatus.Value;
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string EFIndex = HdselEFIndex.Value;
        string EFDesc = HdtxtEFDesc.Value;
        //string ProductDiyAttr2=
        int totalCount = 0;
        DataTable dt = ProductInfoBus.GetProductInfo(model, EFIndex, EFDesc, 1, 1000000, "ID desc", ref totalCount);
       
        //导出标题
        string headerTitle = "物品编号|物品名称|拼音缩写|名称简称|条码|物品分类|基本单位|所属大类|"+
            "计量单位组|规格型号|采购计量单位|颜色|销售计量单位|品牌|库存计量单位|档次级别|"+
            "生产计量单位|尺寸|是否启用批次|ABC分类|成本核算计价方法|标准成本(元)|去税售价(元)|" +
            "销项税率(%)|含税售价(元)|零售价(元)|销售折扣率(%)|调拨单价(元)|去税进价(元)| 进项税率(%)|" +
            "含税进价(元)|是否计入库存|是否允许负库存|主放仓库|安全库存量|最低库存量|最高库存量|" +
            "物品来源分类|产地|图号|启用状态|批准文号|价格策略|技术参数|常见问题|替代品名称|" +
            "物品描述信息|厂家|材质|建档时间|建档人|审核状态|最后更新日期|" +
            "最后更新用户";
        //string headerTitle = "建档日期|启用状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "ProdNo|ProductName|PYShort|ShortNam|BarCode|TypeName|UnitName|BigType|"+
            "GroupUnitName|Specification|InUnitName|ColorName|SaleUnitName|BrandName|StockUnitName|GradeName|"+
            "MakeUnitName|Size|IsBatchNo|ABCType|CalcPriceWays|StandardCost|StandardSell|" +
            "TaxRate|SellTax|SellPrice|Discount|TransferPrice|TaxBuy|InTaxRate|" +
            "StandardBuy|StockIs|MinusIs|StorageName|SafeStockNum|MinStockNum|MaxStockNum|" +
            "Source|FromAddr|DrawingNum|UsedStatus|FileNo|PricePolicy|Params|Questions|ReplaceName|" +
            "Description|Manufacturer|MaterialName|CreateDate|EmployeeName|CheckStatus|ModifiedDate|" +
            "ModifiedUserID";
        //string columnFiled = "CreateDate|strUsedStatus";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "物品档案列表");
    }
}
