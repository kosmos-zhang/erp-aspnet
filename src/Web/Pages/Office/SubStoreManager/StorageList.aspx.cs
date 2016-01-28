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
using XBase.Model.Office.SubStoreManager;
using XBase.Business.Office.SubStoreManager;

public partial class Pages_Office_StoreManager_StorageList : BasePage
{
    /// <summary>
    /// 用户信息
    /// </summary>
    protected UserInfoUtil userInfo = null;
    /// <summary>
    /// 小数位数
    /// </summary>
    private int _selPoint = 2;

    /// <summary>
    /// 小数位数
    /// </summary>
    public int SelPoint
    {
        get
        {
            return _selPoint;
        }
        set
        {
            _selPoint = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        userInfo = SessionUtil.Session["UserInfo"] as UserInfoUtil;
        // 小数位数
        _selPoint = int.Parse(userInfo.SelPoint);
        if (!Page.IsPostBack)
        {
            BinddrpStorageName();//绑定仓库名称
        }
    }

    #region 导出Excel
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        string StorageID = this.drpStorageName.Value;
        if (StorageID == "0")
        {
            StorageID = "";
        }
        string ProductID = this.HidProductID.Value;
        string ProductName = this.txtProductName.Text;

        int TotalCount = 0;
        DataTable dt = SubStorageBus.SelectStorageProduct(1, 1000000, "ID", ref TotalCount, StorageID, ProductID, ProductName, this.txtBatchNo.Value);

        //导出标题
        string headerTitle = "仓库编号|仓库名称|物品编号|物品名称|物品分类|规格|单位|现有存量|批次";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "StorageNo|StorageName|ProductNo|ProductName|TypeIDName|Specification|UnitName|ProductCount|BatchNo";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "总部库存查询");
    }
    #endregion

    #region 绑定仓库名称
    private void BinddrpStorageName()
    {
        DataTable dt = SubStorageBus.GetdrpStorageName();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpStorageName.DataSource = dt;
            drpStorageName.DataTextField = "StorageName";
            drpStorageName.DataValueField = "ID";
            drpStorageName.DataBind();
            ListItem Item = new ListItem("--请选择--", "");
            drpStorageName.Items.Insert(0, Item);
        }
    }
    #endregion
}
