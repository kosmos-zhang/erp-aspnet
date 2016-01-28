using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using XBase.Business.SystemManager;
using XBase.Common;
public partial class Pages_Office_SystemManager_Default2 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CodingRuleControl1.CodingType = ConstUtil.CODING_RULE_TYPE_ZERO;
            CodingRuleControl1.ItemTypeID = ConstUtil.CODINGA_BASE_ITEM_FLOW;

            if (Request["FlowNo"] != null)
            {
                this.hf_flowid.Value = this.Request["FlowNo"].ToString();
            }
            else
            {
                string typeflag = Request["typeflag"].ToString();
                string typecode = Request["typecode"].ToString();
                hidfcode.Value = typecode;
                string flagtext = Request["flagtext"].ToString();
                string codetext = Request["codetext"].ToString();
                this.hf_typeflag.Value = typeflag;
                this.hf_typecode.Value = typecode;
                this.hd_typeflag.Value = Request["typeflag"].ToString();
                this.hd_usestatus.Value = Request["usestatus"].ToString();
                this.txt_flag.Value = flagtext;
                this.txt_BillTypeName.Value = codetext;
            }
          

        }
    }
    private void BindBillTypeInfo()
    {
        //DataTable dt = ApprovalFlowSetBus.GetBillTypeByTypeFlag();
        //if (dt.Rows.Count > 0)
        //{
        //    Ddp_BillType.DataTextField = "ModuleName";
        //    Ddp_BillType.DataValueField = "TypeFlag";
        //    Ddp_BillType.DataSource = dt;
        //    this.Ddp_BillType.DataBind();
        //    ListItem Item = new ListItem();
        //    Item.Value = "0";
        //    Item.Text = "-请选择单据分类-";
        //    Ddp_BillType.Items.Insert(0, Item);
        //}
    }

    protected void btn_back_Click(object sender, ImageClickEventArgs e)
    {
         string typeflag = hf_typeflag.Value;
         string typecode = hidfcode.Value;
         string UseStatus = hd_usestatus.Value;
         string MID="";
        if(Request["ModuleID"]!=null)
          MID = Request["ModuleID"].ToString();

        Response.Redirect("ApprovalFlowSet.aspx?TypeFlag=" + typeflag + "&TypeCodeflag=" + typecode + "&UseStatusflag=" + UseStatus + "&ModuleID=" + MID);
       
    }
}
