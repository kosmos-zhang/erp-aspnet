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
using XBase.Business.Office.SellManager;

public partial class Pages_Office_SellManager_SellOrderMod : BasePage
{
    //小数精度
    private int _selPoint = 2;
    public int SelPoint
    {
        get
        {
            return _selPoint;
        }
        set
        {
            _selPoint = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
           
            CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            sellTypeUC.TypeFlag = ConstUtil.SELL_TYPE_SELL;//销售类别
            sellTypeUC.TypeCode = ConstUtil.SELL_TYPE_SELLTYPE;//销售类别
            sellTypeUC.IsInsertSelect = true;
           
            PayTypeUC.TypeFlag = ConstUtil.CUST_TYPE_CUST;//结算方式
            PayTypeUC.TypeCode = ConstUtil.CUST_INFO_PAYTYPE;//结算方式
            PayTypeUC.IsInsertSelect = true;
            PackageUC.TypeFlag = ConstUtil.SELL_TYPE_SELL;//包装
            PackageUC.IsInsertSelect = true;
            PackageUC.TypeCode = ConstUtil.SELL_TYPE_PACKAGE;//包装
            MoneyTypeUC.IsInsertSelect = true;
            MoneyTypeUC.TypeFlag = ConstUtil.CUST_TYPE_CUST;
            MoneyTypeUC.TypeCode = ConstUtil.CUST_INFO_MONEYTYPE;

            OrderMethod.TypeCode = ConstUtil.SELL_TYPE_ORDERMETHOD;//订货方式
            OrderMethod.TypeFlag = ConstUtil.SELL_TYPE_SELL;//订货方式
            OrderMethod.IsInsertSelect = true;

            CarryType.TypeFlag = ConstUtil.SELL_TYPE_SELL;//运送方式
            CarryType.TypeCode = ConstUtil.SELL_TYPE_CARRYTYPE;//运送方式
            CarryType.IsInsertSelect = true;

            TakeType.TypeFlag = ConstUtil.SELL_TYPE_SELL;//交货方式
            TakeType.TypeCode = ConstUtil.SELL_TYPE_TAKETYPE;//交
            TakeType.IsInsertSelect = true;

            FlowApply1.BillTypeFlag = ConstUtil.CODING_RULE_SELL;
            FlowApply1.BillTypeCode = ConstUtil.CODING_RULE_SELLORDER_NO;

            //设置隐藏域条码的值
            if (UserInfo.IsBarCode)
            {
                hidBarCode.Value = "1";
            }
            else
            {
                hidBarCode.Value = "0";
            }
            //是否启用多单位
            if (UserInfo.IsMoreUnit)
            {
                txtIsMoreUnit.Value = "1";
                BaseUnitD.Visible = true;
                BaseCountD.Visible = true;
            }
            else
            {
                txtIsMoreUnit.Value = "0";
                BaseUnitD.Visible = false;
                BaseCountD.Visible = false;
            }
            // 小数位数
            _selPoint = int.Parse(UserInfo.SelPoint);

            SetMasterCurrency();//绑定本位币
            SetSellDept();//初始化部门信息
        }
    }
    //绑定本位币
    protected void SetMasterCurrency()
    {
        DataTable dt = XBase.Business.Office.FinanceManager.CurrTypeSettingBus.GetMasterCurrency();
        if (dt.Rows.Count > 0)
        { 
            CurrencyType.Value = dt.Rows[0]["CurrencyName"].ToString().Trim();
            CurrencyType.Attributes.Add("title", dt.Rows[0]["ID"].ToString().Trim());
            Rate.Value = dt.Rows[0]["ExchangeRate"].ToString().Trim();
        }
    }

    //初始化部门信息
    protected void SetSellDept()
    {
        DeptId.Value = UserInfo.DeptName;
        SellDeptId.Value = UserInfo.DeptID.ToString().Trim();
    }
}
