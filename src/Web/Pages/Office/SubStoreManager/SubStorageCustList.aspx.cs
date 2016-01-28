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
using XBase.Model.Office.SubStoreManager;
using XBase.Business.Office.SubStoreManager;
using XBase.Common;
public partial class Pages_Office_SubStoreManager_SubStorageCustList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        SubSellCustInfoModel model = new SubSellCustInfoModel();
        model.CompanyCD = companyCD;
        model.DeptID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID.ToString();
        DataRow dt1 = SubStorageDBHelper.GetSubDeptFromDeptID(((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID.ToString());
        if (dt1 != null)
        {
            model.DeptID = dt1["ID"].ToString();
        }   
        model.CustName = hiddenOrderCustName.Value;
        model.CustTel = hiddenOrderCustTel.Value;
        model.CustMobile = hiddenOrderCustMobile.Value;
        model.CustAddr = hiddenOrderCustAddr.Value;
        int TotalCount = 0;

        string myOrder = " ID asc";
        if (this.hiddenOrder.Value != "0")
        {
            string[] myOrder1 = this.hiddenOrder.Value.Split('_');
            if (myOrder1[1] == "a")
            {
                myOrder = myOrder1[0] + " asc ";
            }
            else
            {
                myOrder = myOrder1[0] + " desc ";
            }
        }
        DataTable dt = SubSellOrderBus.GetAllCustInfo(model,"out",1,1,myOrder, ref TotalCount);

        //导出标题
        string headerTitle = "客户名称|客户电话|客户手机号|客户地址";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "CustName|CustTel|CustMobile|CustAddr";
        string[] field = columnFiled.Split('|');

        XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "分店客户列表");
    }
}
