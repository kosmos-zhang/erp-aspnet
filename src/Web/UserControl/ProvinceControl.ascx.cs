//创建人 江贻明
//创建时间 2009/01/21
//描述   获取省份表信息填充DropDownList控件显示 
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XBase.Business.SystemManager;
public partial class UserControl_ProvinceControl : System.Web.UI.UserControl
{
    #region
    /// <summary>
    /// 省编码
    /// </summary>
    private string _provincecd;
    public string ProvinceCD
    {
        get { return _provincecd; }
        set { _provincecd = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDrpList();
        }
    }

    #region BingingProvince
    private void BindDrpList()
    {
        DataTable dt = ProvinceBus.GetProvinceInfo();
        if (dt != null && dt.Rows.Count > 0)
        {
            this.DrpPrv.Items.Clear();
            foreach (DataRow Row in dt.Rows)
            {
                ListItem Province = new ListItem();
                Province.Text = Row["ProvName"].ToString();
                Province.Value = Row["ProvCD"].ToString();
                this.DrpPrv.Items.Add(Province);
            }
            ListItem select = new ListItem();
            select.Text = "请选择";
            select.Value = "0";
            this.DrpPrv.Items.Insert(0, select);
        }
    }
    #endregion
}
