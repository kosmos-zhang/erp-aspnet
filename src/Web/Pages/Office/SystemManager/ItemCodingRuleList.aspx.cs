using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XBase.Business.SystemManager;
public partial class Pages_Office_SystemManager_ItemCodingRuleList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.hf_typeflag.Value = Request["TypeFlag"].ToString();
            BindTypeCode();
            if (hf_typeflag.Value == "0")
            {
                datetypeflag.Visible = false;
            }
        }
    }
    /// <summary>
    /// 绑定到单据
    /// </summary>
    private void BindTypeCode()
    {
        DataTable dt = ApprovalFlowSetBus.GetBillTypeByTypeFlag(this.hf_typeflag.Value);
        if (dt.Rows.Count > 0)
        {
            drp_typecode.DataTextField = "TypeName";
            drp_typecode.DataValueField = "TypeCode";
            drp_typecode.DataSource = dt;
            drp_typecode.DataBind();
        }
    }
}
