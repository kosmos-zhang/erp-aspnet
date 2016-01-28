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
using XBase.Business.Office.SupplyChain;
using XBase.Common;
using System.Text;

public partial class UserControl_GetBillExAttrControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetExtList();
            StringBuilder script = new StringBuilder();
            string SelClientID = SelExtValue.ClientID;
            string txtClientID = TxtExtValue.ClientID;
            script.AppendLine("document.getElementById('" + txtClientID + "').value=''; ");
            script.AppendLine("if(document.getElementById('" + SelClientID + "').value != '') {                               ");
            script.AppendLine(" document.getElementById('" + txtClientID + "').disabled = false; }");
            script.AppendLine("else document.getElementById('" + txtClientID + "').disabled = true; ");

            SelExtValue.Attributes.Add("onchange", script.ToString());
        }
    }
    #region 获取列表
    /// <summary>
    /// 获取列表
    /// </summary>
    private void GetExtList()
    {

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码 
        DataTable dt = TableExtFieldsBus.GetAllList(CompanyCD, "", this.TableName);
        if (dt != null && dt.Rows.Count > 0)
        {
            SelExtValue.DataTextField = "EFDesc";
            SelExtValue.DataValueField = "EFIndex";
            SelExtValue.DataSource = dt;
            
            SelExtValue.DataBind();

        }
        else divBillAttr.Style.Add("display","none");
    }
    #endregion
    #region 属性
    /// <summary>
    /// 扩展属性下拉列表值
    /// </summary>
    private string _ExtIndex;
    /// <summary>
    /// 扩展属性下拉列表文本
    /// </summary>
    private string _ExtText;
    /// <summary>
    /// 扩展属性文本框值
    /// </summary>
    private string _ExtValue;
    /// <summary>
    /// 单据表名
    /// </summary>
    private string _TableName;

    /// <summary>
    /// 单据表名
    /// </summary>
    public string TableName
    {
        get
        {
            return _TableName;
        }
        set
        {
            _TableName = value;
        }
    }
    /// <summary>
    /// 扩展属性下拉列表值
    /// </summary>
    public string ExtIndex
    {
        get
        {
            return _ExtIndex;
        }
        set
        {
            _ExtIndex = value;
        }
    }
    /// <summary>
    /// 扩展属性下拉列表文本
    /// </summary>
    public string ExtText
    {
        get
        {
            return _ExtText;
        }
        set
        {
            _ExtText = value;
        }
    }

    /// <summary>
    /// 扩展属性文本框值
    /// </summary>
    public string ExtValue
    {
        get
        {
            return _ExtValue;
        }
        set
        {
            _ExtValue = value;
        }
    }
    /// <summary>
    /// 获取下拉列表选择值
    /// </summary>
    public string GetExtIndexValue
    {
        get
        {
            return this.SelExtValue.SelectedValue;
        }
        set
        {
            this.SelExtValue.SelectedValue = value;
        }
    }
    /// <summary>
    /// 获取下拉列表选择项文本
    /// </summary>
    public string GetExtText
    {
        get
        {
            return this.SelExtValue.SelectedItem.Text;
        }
        set
        {
            this.SelExtValue.SelectedItem.Text = value;
        }
    }
    /// <summary>
    /// 获取文本框值
    /// </summary>
    public string GetExtTxtValue
    {
        get
        {
            return this.TxtExtValue.Text;
        }
        set
        {
            this.TxtExtValue.Text = value;
        }
    }

    #endregion
    #region 设置控件值
    /// <summary>
    /// 设置控件值
    /// </summary>
    public string SetExtControlValue()
    {
        SelExtValue.SelectedValue = _ExtIndex;
        TxtExtValue.Text = _ExtValue;
        return _ExtIndex + _ExtValue;
    }
    #endregion
}
