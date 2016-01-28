/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/11
 * 描    述： 编码规则用户控件
 * 修改日期： 2009/03/11
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Data;
using XBase.Common;
using XBase.Business.Common;
using System.Web.UI.WebControls;
using System.Text;

public partial class UserControl_CodingRuleControl : System.Web.UI.UserControl
{
    /// <summary>
    /// 类名：UserControl_CodingRuleControl
    /// 描述：编码规则用户控件
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/11
    /// 最后修改时间：2009/03/11
    /// </summary>
    ///

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //根据编码类型以及单据代码或基础数据代码获取编码规则信息
            BindCodingRule();
            //给下拉框添加事件
            StringBuilder script = new StringBuilder();
            //获取下拉列表的客户端ID
            string ddlClientID = ddlCodeRule.ClientID;
            //获取输入框的客户端ID
            string txtClientID = txtCode.ClientID;
            script.AppendLine("	selectCoding = document.getElementById('" + ddlClientID + "').value; ");
            script.AppendLine("	code = document.getElementById('" + txtClientID + "'); ");
            script.AppendLine("	if(selectCoding == '') {                               ");
            script.AppendLine("		code.disabled = false;                             ");
            script.AppendLine("		code.value = '';                                   ");
            script.AppendLine("	}                                                      ");
            script.AppendLine("	else {                                                 ");
            script.AppendLine("		code.disabled = true;                              ");
            script.AppendLine("		code.value = '" + ConstUtil.CODING_RULE_DISPLAY_TEXT + "';");
            script.AppendLine("	}                                                      ");
            ddlCodeRule.Attributes.Add("onchange", script.ToString());
        }
    }
    #endregion

    #region 属性
    private string _CodingType;//编码类型
    private string _ItemTypeID;//单据代码或基础数据代码

    /// <summary>
    /// 单据编号
    /// </summary>
    public string CodeValue
    {
        get
        {
            return GetCodeValue();
        }
    }

    /// <summary>
    /// 编码类型
    /// </summary>
    public string CodingType
    {
        get
        {
            return _CodingType;
        }
        set
        {
            _CodingType = value;
        }
    }

    /// <summary>
    /// 单据代码或基础数据代码
    /// </summary>
    public string ItemTypeID
    {
        get
        {
            return _ItemTypeID;
        }
        set
        {
            _ItemTypeID = value;
        }
    }
    #endregion

    #region 根据编码类型以及单据代码或基础数据代码获取编码规则信息
    /// <summary>
    /// 根据编码类型以及单据代码或基础数据代码获取编码规则信息
    /// </summary>
    public  void BindCodingRule()
    {
        //分类标识未设置时，不进行绑定
        if (string.IsNullOrEmpty(this.CodingType) || string.IsNullOrEmpty(this.ItemTypeID))
        {
            return;
        }
        //查询分类标识信息
        DataTable dt = ItemCodingRuleBus.GetCodingRuleInfoForDrp(this.CodingType, this.ItemTypeID);
        //分类标识存在时绑定数据
        if (dt != null && dt.Rows.Count > 0)
        {
            //设置列表项的文本内容的数据源字段
            ddlCodeRule.DataTextField = "RuleName";
            //设置列表项的提供值的数据源字段。
            ddlCodeRule.DataValueField = "ID";
            //设置列表项的数据源
            ddlCodeRule.DataSource = dt;
            //绑定列表的数据源
            ddlCodeRule.DataBind();
            //获取默认编码规则
            DataRow[] drDefault = dt.Select("IsDefault = '" + ConstUtil.CODING_RULE_DEFAULT_TRUE + "'");
            //存在默认编码规则时，设定默认编码规则为选中项
            if (drDefault.Length > 0)
            {
                //获取默认编码规则的ID
                string selectID = GetSafeData.ValidateDataRow_Int(drDefault[0], "ID").ToString();
                //设置为选中项
                ddlCodeRule.SelectedValue = selectID;
                //设置提示信息
                txtCode.Text = ConstUtil.CODING_RULE_DISPLAY_TEXT;
                //设置为不可编辑
                txtCode.Enabled = false;
            }
        }
        //添加一请选择选项
        ListItem Item = new ListItem(ConstUtil.CODING_RULE_INSERT_TEXT, ConstUtil.CODING_RULE_INSERT_VALUE);
        ddlCodeRule.Items.Insert(0, Item);
    }
    #endregion

    #region 获取编码
    /// <summary>
    /// 获取编码
    /// </summary>
    private string GetCodeValue()
    {
        //获取选择的编码规则
        string selectCodeRule = ddlCodeRule.SelectedValue;
        //如果是手工输入的时候，返回输入的内容
        if (string.IsNullOrEmpty(selectCodeRule))
        {
            return txtCode.Text;
        }
        //根据编码规则生成编码
        else
        {
            return ItemCodingRuleBus.GetCodeValue(selectCodeRule);
        }
    }

    #endregion


    #region 获取编码规则ID
    /// <summary>
    /// 获取编码规则
    /// </summary>
    public  string GetCodeRuleID()
    {
  
        string selectCodeRule = ddlCodeRule.SelectedValue;
        return selectCodeRule;
    }

    #endregion


      #region 获取手动输入的编码 
    /// <summary>
    /// 获取手动输入的编码
    /// </summary>
    public string GetDisplaycode()
    {

        string selectCodeRule = txtCode.Text .Trim ();
        return selectCodeRule;
    }

    #endregion


    
}
