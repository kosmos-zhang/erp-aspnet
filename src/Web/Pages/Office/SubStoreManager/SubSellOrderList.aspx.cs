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
//using System.Web.SessionState;
using XBase.Common;
using XBase.Business.Office.SubStoreManager;
using XBase.Model.Office.SubStoreManager;

public partial class Pages_Office_SubStoreManager_SubSellOrderList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GetBillExAttrControl1.TableName = "officedba.SubSellOrder";
        if (!IsPostBack)
        {
            DataRow dt = SubStorageDBHelper.GetSubDeptFromDeptID(((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID.ToString());

            if (dt != null)
            {
                hidDeptID.Value = dt["ID"].ToString();
                DeptName.Value = dt["DeptName"].ToString();
            }
            else
            {
                hidDeptID.Value = "0";
                DeptName.Value = "";
            }
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        SubSellOrderModel SubSellOrderM = new SubSellOrderModel();
        SubSellOrderM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        SubSellOrderM.OrderNo = this.txtOrderNo.Value.Trim();
        SubSellOrderM.Title = this.txtOrderTitle.Value.Trim();
        SubSellOrderM.SendMode = this.ddlSendMode.Value;
        SubSellOrderM.CustName = this.txtCustName.Value.Trim();
        SubSellOrderM.CustTel = this.txtCustTel.Value.Trim();
        SubSellOrderM.CustMobile = this.txtCustMobile.Value.Trim();
        SubSellOrderM.CustAddr = this.txtCustAddr.Value.Trim();
        SubSellOrderM.DeptID = this.hidDeptID.Value;
        SubSellOrderM.Seller = this.hidUserID.Value;
        SubSellOrderM.BusiStatus = this.ddlBusiStatus.Value;
        SubSellOrderM.BillStatus = this.ddlBillStatus.Value; ;

        string EFIndex = GetBillExAttrControl1.GetExtIndexValue;
        string EFDesc = GetBillExAttrControl1.GetExtTxtValue;

        int totalCount = 0;
        DataTable dt = SubSellOrderBus.SelectSubSellOrder(SubSellOrderM, 1, 100000000, "ID desc", EFIndex, EFDesc, ref totalCount);



        //DataTable dt = WorkCenterBus.GetWorkCenterListBycondition(model, 1, 1000000, "ID desc", ref totalCount);

        //导出标题
        string headerTitle = "订单编号|订单主题|发货模式|客户名称|客户联系电话|客户手机号|送货地址|销售分店|业务员|业务状态|单据状态";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "OrderNo|Title|SendModeName|CustName|CustTel|CustMobile|CustAddr|DeptName|SellerName|BusiStatusName|BillStatusName";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "销售订单");
    }
}
