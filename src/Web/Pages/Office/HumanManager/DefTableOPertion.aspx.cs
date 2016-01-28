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

using XBase.Business.DefManager;
public partial class Pages_DefManager_DefTableOPertion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //获取表头
            lbl_title.Text = DefineBus.GetDefineTableByCode(Request.QueryString["tableid"].ToString());

            #region 绑定表结构
            DataSet defineControl = DefineBus.GetTableStruct(Request.QueryString["tableid"].ToString());
            this.StructDL.DataSource = defineControl;
            this.StructDL.DataBind();
            #endregion

            #region 读取控件名
            for (int i = 0; i < defineControl.Tables[0].Rows.Count; i++)
            {
                HidControlName.Value = HidControlName.Value + "," + defineControl.Tables[0].Rows[i]["ccode"].ToString();//获取控件ID集合
                HidControlList.Value = HidControlList.Value + ",db_" + defineControl.Tables[0].Rows[i]["ccode"].ToString() + "#" + defineControl.Tables[0].Rows[i]["type"].ToString() + "#" + defineControl.Tables[0].Rows[i]["length"].ToString() + "#" + defineControl.Tables[0].Rows[i]["isempty"].ToString() +"#"+ defineControl.Tables[0].Rows[i]["cname"].ToString();
            }
            HidControlName.Value = HidControlName.Value.ToString().TrimStart(',');
            HidControlList.Value = HidControlList.Value.ToString().TrimStart(',');
            #endregion
        }

    }
    protected void StructDL_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        #region 绑定控件框
        Literal ltl_input = (Literal)(e.Item.FindControl("ltl_input"));
        Literal ltl_tag = (Literal)(e.Item.FindControl("ltl_tag"));
        if (DataBinder.Eval(e.Item.DataItem, "isempty").ToString()=="0")
        {
            ltl_tag.Text = "<span class=\"redbold\">*</span>";
        }

        if (DataBinder.Eval(e.Item.DataItem, "typeflag").ToString() == "0")
        {
            #region 判断是多选还是单选
            if (DataBinder.Eval(e.Item.DataItem, "ismultiline").ToString() == "1")
            {
                ltl_input.Text = "<textarea rows='4' cols='20' style='height:60px;width:98%;' class='TextAreaNormal' "
                    + " name='db_" + DataBinder.Eval(e.Item.DataItem, "ccode").ToString() + "' id='db_" + DataBinder.Eval(e.Item.DataItem, "ccode").ToString() + "' "
                    + " maxname='" + DataBinder.Eval(e.Item.DataItem, "cname").ToString() + "' maxlength='" + DataBinder.Eval(e.Item.DataItem, "length").ToString() + "'"
                    + " >" + "</textarea>";
            }
            else
            {
                ltl_input.Text = "<input type='text' class='InputNormal' autotab='true' style='width:98%;' "
                    + " name='db_" + DataBinder.Eval(e.Item.DataItem, "ccode").ToString() + "' id='db_" + DataBinder.Eval(e.Item.DataItem, "ccode").ToString() + "' "
                    + " maxlength=" + DataBinder.Eval(e.Item.DataItem, "length").ToString() + " value='" + "'/>";
            }
            #endregion
        }
        else if (DataBinder.Eval(e.Item.DataItem, "typeflag").ToString() == "1")
        {
            ltl_input.Text = "<select id='db_" + DataBinder.Eval(e.Item.DataItem, "ccode").ToString() + "' style='width:98%'>"+DataBinder.Eval(e.Item.DataItem,"dropdownlistValue").ToString()+"</select>";
        }
        else
        {
            ltl_input.Text = "<input id='db_" + DataBinder.Eval(e.Item.DataItem, "ccode").ToString() + "' maxlength='30' type='text' style='width:98%' readonly onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('db_" + DataBinder.Eval(e.Item.DataItem, "ccode").ToString() + "')})\" /> ";
        }
        #endregion
    }
}
