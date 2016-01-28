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

public partial class Pages_Office_SubStoreManager_SubStorageInitList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //分店期初库存录入模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_SUBSTOREMANAGER_SUBSTORAGEINIT;
            UserInfoUtil userInfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
            //HidDeptID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID.ToString();
            DataRow dt = SubStorageDBHelper.GetSubDeptFromDeptID(((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID.ToString());

            if (dt != null)
            {
                HidDeptID.Value = dt["ID"].ToString();

            }
        }
        this.GetBillExAttrControl1.TableName = "officedba.SubStorageIn";
        //小数点位数
        hidSelPoint.Value = UserInfo.SelPoint;
    }

    #region 导出Excel
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {

        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        string DeptID = this.HidDeptID.Value;
        string ProductID = this.HidProductID.Value;
        string ProductName = this.txtProductName.Text;
        string EFIndex = GetBillExAttrControl1.GetExtIndexValue;
        string EFDesc = GetBillExAttrControl1.GetExtTxtValue;

        int TotalCount = 0;
        DataTable dt = SubStorageBus.SelectSubStorageInitList(1, 1000000, "ID", ref TotalCount, DeptID, ProductID, ProductName, EFIndex, EFDesc, this.txtBatchNo.Value);


        //导出标题
        string headerTitle = "分店名称|入库单编号|入库单主题|入库数量|确认人|确认时间|单据状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "DeptName|InNo|Title|CountTotal|ConfirmorName|ConfirmDate|BillStatusName";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "分店期初库存列表");
    }
    #endregion
}
