using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using XBase.Common;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using System.Web.UI.WebControls;

public partial class Pages_Office_PurchaseManager_PurchaseApplyEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserInfoUtil userInfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
        ddlTypeID.TypeFlag = "7";
        ddlTypeID.TypeCode = "5";
    }
    //#region 绑定采购类别的方法
    //private void BindPurchaseType()
    //{
    //    //DataTable dt = PurchaseApplyDBHelper.GetPurchaseType();
    //    //if (dt.Rows.Count > 0)
    //    //{
    //    //    ddlTypeID.DataTextField = "TypeName";
    //    //    ddlTypeID.DataValueField = "ID";
    //    //    ddlTypeID.DataSource = dt;
    //    //    ddlTypeID.DataBind();
    //    //}
    //}
    //#endregion
}
