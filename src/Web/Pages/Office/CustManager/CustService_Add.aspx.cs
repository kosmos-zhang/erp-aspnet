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
using XBase.Business.Common;

public partial class Pages_Office_CustManager_CustService_Add : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindServeType();//绑定服务类型           

            txtModifiedDate.Value = DateTime.Now.ToShortDateString();
            txtModifiedUserID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改用户为当前用户

            ddlServeNO.CodingType = ConstUtil.CUST_CODINGTYPE_BILL;
            ddlServeNO.ItemTypeID = ConstUtil.CUST_BILL_Service;
            //ddlServeNO.TableName = ConstUtil.CUST_TABLENAME_SERVICE;//客户服务表名
            //ddlServeNO.ColumnName = ConstUtil.CUST_NO_SERVENO;//客户服务单编号字段名

            //CustNameSel1.CustIDContrl = "hfCustID";
            //CustNameSel1.CustNoContrl = "hfCustNo";
        }
    }

    #region 绑定服务类型的方法
    private void BindServeType()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_SERVICES_SERVETYPE);
        if (dt.Rows.Count > 0)
        {
            ddlServeType.DataTextField = "TypeName";
            ddlServeType.DataValueField = "ID";
            ddlServeType.DataSource = dt;
            ddlServeType.DataBind();
        }
        else
        {
            ListItem Item = new ListItem();
            Item.Value = "0";
            Item.Text = "--请选择--";
            ddlServeType.Items.Insert(0, Item);
        }
    }
    #endregion
   
}
