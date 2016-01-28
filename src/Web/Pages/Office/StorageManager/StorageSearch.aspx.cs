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

public partial class Pages_Office_StorageManager_StorageSearch : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCom();
            BindTree();
            HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
            //扩展属性
            GetBillExAttrControl1.TableName = "officedba.ProductInfo";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
          
            GetBillExAttrControl1.ExtIndex = EFIndex;
            GetBillExAttrControl1.ExtValue = EFDesc;
            GetBillExAttrControl1.SetExtControlValue();
            //获取仓库列表
            StorageModel model = new StorageModel();
            model.CompanyCD = UserInfo.CompanyCD;
            model.UsedStatus = "1";
            DataTable dt1 = StorageBus.GetStorageListBycondition2(model);
            if (dt1.Rows.Count > 0)
            {
                ddlStorage.DataSource = dt1;
                ddlStorage.DataTextField = "StorageName";
                ddlStorage.DataValueField = "ID";
                ddlStorage.DataBind();
                ddlStorage.Items.Insert(0, new ListItem("--请选择--", ""));
            }
            //获取材质列表
            string typeflag = "5";
            string typecode = "5";
            DataTable dt2 = XBase.Business.Office.SystemManager.CodePublicTypeBus.GetCodePublicByCode(typeflag, typecode);
            if (dt2.Rows.Count > 0)
            {
                ddlMaterial.DataSource = dt2;
                ddlMaterial.DataTextField = "TypeName";
                ddlMaterial.DataValueField = "ID";
                ddlMaterial.DataBind();
                ddlMaterial.Items.Insert(0, new ListItem("--请选择--", ""));
            }
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        StorageProductModel model = new StorageProductModel();
        model.CompanyCD = UserInfo.CompanyCD;
        string ProductNo = string.Empty;
        string ProductName = string.Empty;
        string BarCode = string.Empty;
        model.StorageID = ddlStorage.SelectedValue;
        XBase.Model.Office.SupplyChain.ProductInfoModel pdtModel = new XBase.Model.Office.SupplyChain.ProductInfoModel();
        pdtModel.ProdNo = txtProductNo.Value;
        pdtModel.ProductName = txtProductName.Value;
        pdtModel.BarCode = HiddenBarCode.Value.Trim();
        pdtModel.Specification = txtSpecification.Value;
        pdtModel.Manufacturer = txtManufacturer.Value;
        pdtModel.Material = ddlMaterial.SelectedValue;
        pdtModel.FromAddr = txtFromAddr.Value;
        pdtModel.ColorID = sel_ColorID.SelectedValue;
        pdtModel.TypeID = this.hidTypeID.Value;
        string StorageCount = txtStorageCount.Value;
        string StorageCount1 = txtStorageCount1.Value;
        model.ProductCount = StorageCount;

        string BatchNo = this.ddlBatchNo.SelectedValue;
        string EFIndex = hiddenEFIndex.Value .Trim ();
        string EFDesc = hiddenEFDesc.Value .Trim ();
        string EFName = hiddenEFIndexName.Value.Trim();
        string     sidex= "ExtField"+EFIndex;
        //ProductNo = txtProductNo.Value;
        //ProductName = txtProductName.Value;
       // BarCode = HiddenBarCode.Value.Trim();

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
        //DataTable dt = StorageSearchBus.GetProductStorageTableBycondition(model, ProductNo, ProductName, orderBy, BarCode);
        DataTable dt = StorageSearchBus.GetProductStorageTableBycondition(model, pdtModel, StorageCount1, EFIndex, EFDesc, orderBy,BatchNo);
        if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
        {
            OutputToExecl.ExportToTableFormat(this, dt,
            new string[] { "仓库编号", "仓库名称","批次", "所属部门", "物品编号", "物品名称", "规格","颜色", "基本单位", "基本数量", "单位","数量",EFName  },
            new string[] { "StorageNo", "StorageName", "BatchNo", "DeptName", "ProductNo", "ProductName", "Specification", "ColorName", "UnitID", "ProductCount", "CodeName", "StoreCount", sidex },
            "现有库存查询列表");
        }
        else
        {
            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "仓库编号", "仓库名称", "批次", "所属部门", "物品编号", "物品名称", "规格","颜色", "基本单位", "基本数量", "单位", "数量" },
                new string[] { "StorageNo", "StorageName", "BatchNo", "DeptName", "ProductNo", "ProductName", "Specification", "ColorName", "UnitID", "ProductCount", "CodeName", "StoreCount" },
                "现有库存查询列表");
        }
    }

    #region 绑定分类树
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
    #endregion

    #region 绑定子类
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
    #endregion

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
        sel_ColorID.Items.Insert(0, new ListItem("--请选择--", ""));
       

    }
}
