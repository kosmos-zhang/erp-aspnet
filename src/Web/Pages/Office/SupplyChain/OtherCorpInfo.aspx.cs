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
using XBase.Business.Office.SystemManager;
public partial class Pages_Office_SupplyChain_OtherCorpInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSel();
            this.hidModuleID.Value = ConstUtil.Menu_OtherCorpInfoAdd;
            string requestParam = Request.QueryString.ToString();
            //从列表过来时
            int firstIndex = requestParam.IndexOf("&");
            //返回回来时
            if (firstIndex > 0)
            {
                //获取是否查询的标识
                string flag = Request.QueryString["Flag"];
                //点击查询时，设置查询的条件，并执行查询
                if ("1".Equals(flag))
                {

                    this.sel_BigType.Value = Request.QueryString["BigType"];
                    txt_CustNo.Value = Request.QueryString["CustNo"];
                    txt_CustName.Text = Request.QueryString["CustName"];
                    UsedStatus.Value = Request.QueryString["UsedStatus"];
                    this.sel_BillType.SelectedValue = Request.QueryString["BillType"];
                    chk_isTax.Value = Request.QueryString["isTax"];
                    //获取当前页
                    string pageIndex = Request.QueryString["PageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["PageCount"];
                    //  执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");Fun_Search_OtherCorpInfo('" + pageIndex + "');</script>");
                }
            }
            else
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                //                        , "<script language=javascript>Fun_Search_OtherCorpInfo();</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                          , "<script language=javascript>Fun_Search_OtherCorpInfo();</script>");
            }
        }

    }
    /// <summary>
    /// 所在区域
    /// </summary>
    private void BindSel()
    {
       string TypeFlag = "4";//客户
       string TypeCode = "12";//区域
        DataTable dt = CodePublicTypeBus.GetCodePublicByCode(TypeFlag, TypeCode);
        if (dt.Rows.Count > 0)
        {
            this.sel_BillType.DataTextField = "TypeName";
            sel_BillType.DataValueField = "ID";
            sel_BillType.DataSource = dt;
            sel_BillType.DataBind();
            ListItem Item = new ListItem();
            Item.Value = "";
            Item.Text = "--请选择--";
            sel_BillType.Items.Insert(0, Item);
        }

    }
}
