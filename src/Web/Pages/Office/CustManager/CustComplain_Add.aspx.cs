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

public partial class Pages_Office_CustManager_CustComplain_Add : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtModifiedDate.Value = DateTime.Now.ToShortDateString();
            txtModifiedUserID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改用户为当前用户

            ddlComplainNo.CodingType = ConstUtil.CUST_CODINGTYPE_BILL;
            ddlComplainNo.ItemTypeID = ConstUtil.CUST_BILL_Complain;
            //ddlComplainNo.TableName = ConstUtil.CUST_TABLENAME_SERVICE;//客户投诉表名
            //ddlComplainNo.ColumnName = ConstUtil.CUST_NO_COMPLAIN;//客户投诉单编号字段名

            //CustNameSel1.CustIDContrl = "hfCustID";
            //CustNameSel1.CustNoContrl = "hfCustNo";

            BindComplainType();
        }
    }

    #region 绑定投诉类型的方法
    private void BindComplainType()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_COMPLAIN_TYPE);
        if (dt.Rows.Count > 0)
        {
            ddlComplainType.DataTextField = "TypeName";
            ddlComplainType.DataValueField = "ID";
            ddlComplainType.DataSource = dt;
            ddlComplainType.DataBind();
        }
        else
        {
            ListItem Item = new ListItem();
            Item.Value = "0";
            Item.Text = "--请选择--";
            ddlComplainType.Items.Insert(0, Item);
        }
    }
    #endregion
}
