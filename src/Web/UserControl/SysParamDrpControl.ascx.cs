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

public partial class UserControl_SysParamDrpControl : System.Web.UI.UserControl
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
    private string _type;//参数类型
    private string _number;//参数编号
    private bool _isInsertSelect = false;//是否添加请选择选项

    /// <summary>
    /// 参数类型
    /// </summary>
    public string Type
    {
        get
        {
            return _type;
        }
        set
        {
            _type = value;
        }
    }

    /// <summary>
    /// 参数编号
    /// </summary>
    public string Number
    {
        get
        {
            return _number;
        }
        set
        {
            _number = value;
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

    #region 根据参数类型以及参数编号获取分类内容
    private void BindCodeType()
    {
        //分类标识未设置时，不进行绑定
        if (string.IsNullOrEmpty(this.Type) || string.IsNullOrEmpty(this.Number))
        {
            return;
        }
        //查询分类标识信息
        DataTable dt = SysParamBus.GetSysParamInfoForDrp(this.Type, this.Number);
        //分类标识存在时绑定数据
        if (dt != null && dt.Rows.Count > 0)
        {
            //设置列表项的文本内容的数据源字段
            ddlSysParam.DataTextField = "IndexValue";
            //设置列表项的提供值的数据源字段。
            ddlSysParam.DataValueField = "IndexCode";
            //设置列表项的数据源
            ddlSysParam.DataSource = dt;
            //绑定列表的数据源
            ddlSysParam.DataBind();
            //如果需要添加一空选项时
            if (this.IsInsertSelect)
            {
                //添加一请选择选项
                ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_VALUE, ConstUtil.CODE_TYPE_INSERT_TEXT);
                ddlSysParam.Items.Insert(0, Item);
            }
        }
    }
    #endregion
}
