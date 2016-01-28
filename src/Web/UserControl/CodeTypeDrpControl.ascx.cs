/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/10
 * 描    述： 系统分类选择列表
 * 修改日期： 2009/03/10
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Data;
using XBase.Common;
using XBase.Business.Common;
using System.Web.UI.WebControls;

public partial class UserControl_CodeTypeDrpControl : System.Web.UI.UserControl
{
    /// <summary>
    /// 类名：UserControl_CodeTypeDrpControl
    /// 描述：系统分类选择列表
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/10
    /// 最后修改时间：2009/03/10
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCodeType();
        }
    }

    #region 属性
    private string _typeFlag;//分类标识
    private string _typeCode;//分类编码
    private bool _isInsertSelect = false;//是否添加请选择选项
    private string _selectedValue;
    private bool _enabled = true;//是否可编辑

    /// <summary>
    /// 是否可编辑
    /// </summary>
    public bool Enabled
    {
        get
        {
            return _enabled;
        }
        set
        {
            _enabled = value;
        }
    }

    /// <summary>
    /// 选择值
    /// </summary>
    public string SelectedValue
    {
        get
        {
            return ddlCodeType.SelectedValue;
        }
        set
        {
            _selectedValue = value;
        }
    }

    /// <summary>
    /// 分类标识
    /// </summary>
    public string TypeFlag
    {
        get
        {
            return _typeFlag;
        }
        set
        {
            _typeFlag = value;
        }
    }
    
    /// <summary>
    /// 分类标识
    /// </summary>
    public string TypeCode
    {
        get
        {
            return _typeCode;
        }
        set
        {
            _typeCode = value;
        }
    }

    /// <summary>
    /// 是否添加请选择选项
    /// </summary>
    public bool IsInsertSelect
    {
        get
        {
            return _isInsertSelect;
        }
        set
        {
            _isInsertSelect = value;
        }
    }
    #endregion

    #region 根据企业编码以及分类标识获取分类内容
    private void BindCodeType()
    {
        //分类标识未设置时，不进行绑定
        if (string.IsNullOrEmpty(this.TypeFlag))
        {
            return;
        }
        //查询分类标识信息
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(this.TypeFlag, this.TypeCode);
        //分类标识存在时绑定数据
        if (dt != null && dt.Rows.Count > 0)
        {
            //设置列表项的文本内容的数据源字段
            ddlCodeType.DataTextField = "TypeName";
            //设置列表项的提供值的数据源字段。
            ddlCodeType.DataValueField = "ID";
            //设置列表项的数据源
            ddlCodeType.DataSource = dt;
            //绑定列表的数据源
            ddlCodeType.DataBind();
            
            //设置选中项
            if (!string.IsNullOrEmpty(_selectedValue))
            {
                ddlCodeType.SelectedValue = _selectedValue;
            }
            //
            ddlCodeType.Enabled = this.Enabled;
        }
        //如果需要添加一空选项时
        if (this.IsInsertSelect)
        {
            //添加一请选择选项
            ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
            ddlCodeType.Items.Insert(0, Item);
        }
    }
    #endregion

}
