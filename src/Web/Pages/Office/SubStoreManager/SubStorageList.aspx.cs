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

public partial class Pages_Office_StoreManager_SubStorageList : BasePage
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
            DataRow dt = SubStorageDBHelper.GetSubDeptFromDeptID(((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID.ToString());

            if (dt != null)
            {
                HidDeptID.Value = dt["ID"].ToString();

            }
        }
    }

    #region 导出Excel
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        string DeptID = this.HidDeptID.Value;
        string ProductID = this.HidProductID.Value;
        string ProductName = this.txtProductName.Text;

        int TotalCount = 0;
        DataTable dt = SubStorageBus.SelectSubStorageProduct(1, 1000000, "ProductID", ref TotalCount, DeptID, ProductID, ProductName, this.txtBatchNo.Value);

        //导出标题
        string headerTitle = "分店名称|物品编号|物品名称|规格|单位|现有存量|批次";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "DeptName|ProductNo|ProductName|Specification|UnitName|ProductCount2|BatchNo";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "分店库存查询");
    }
    #endregion
}
