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

public partial class Pages_Office_SubStoreManager_SubSellBackList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GetBillExAttrControl1.TableName = "officedba.SubSellBack";
        if (!Page.IsPostBack)
        {
            //新建修改门店销售退货单模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_SUBSTOREMANAGER_SUBSELLBACKADD;
            //HidDeptID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID.ToString();
            //DeptDeptID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptName;

            DataRow dt = SubStorageDBHelper.GetSubDeptFromDeptID(((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID.ToString());

            if (dt != null)
            {
                if (dt["ID"] != null && dt["ID"].ToString() != "")
                {
                    HidDeptID.Value = dt["ID"].ToString();
                }
                else
                {
                    HidDeptID.Value = "0";
                }
                DeptDeptID.Text = dt["DeptName"].ToString();

            }
            else
            {
                HidDeptID.Value = "0";
            }

        }
    }

    #region 导出Excel
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {

        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        string BackNo = this.txtBackNo.Text;
        string Title = this.txtTitle.Text;
        string OrderID = this.HidOrderID.Value;
        string CustName = this.txtCustName.Text;
        string CustTel = this.txtCustTel.Text;
        string DeptID = this.HidDeptID.Value;
        string Seller = this.HidSeller.Value;
        string BusiStatus = this.drpBusiStatus.Value;
        if (BusiStatus == "0")
        {
            BusiStatus = "";
        }
        string BillStatus = this.ddlBillStatus.Value;
        if (BillStatus == "0")
        {
            BillStatus = "";
        }
        string CustAddr = this.txtCustAddr.Text;
        string EFIndex = GetBillExAttrControl1.GetExtIndexValue;
        string EFDesc = GetBillExAttrControl1.GetExtTxtValue;
        int TotalCount = 0;
        DataTable dt = SubSellBackBus.SelectSubSellBack(1, 1000000, "ID", ref TotalCount, BackNo, Title, OrderID, CustName, CustTel, DeptID, Seller, BusiStatus, BillStatus, CustAddr, EFIndex, EFDesc);



        //DataTable dt = WorkCenterBus.GetWorkCenterListBycondition(model, 1, 1000000, "ID desc", ref totalCount);

        //导出标题
        string headerTitle = "单据编号|单据主题|对应销售订单|客户名称|客户联系电话|客户地址|销售分店|退货处理人|单据状态|业务状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "BackNo|Title|OrderNo|CustName|CustTel|CustAddr|DeptName|SellerName|BillStatusName|BusiStatusName";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "销售退货单");
    }
    #endregion
}
