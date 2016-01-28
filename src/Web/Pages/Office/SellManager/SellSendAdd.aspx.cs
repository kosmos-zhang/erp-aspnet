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

public partial class Pages_Office_SellManager_SellSendAdd : BasePage
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
           
                UserSeller.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
                hiddSeller.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
                ////临时注释
                Creator.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
                ////临时注释
                ModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            
            CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            FlowApply1.BillTypeFlag = ConstUtil.CODING_RULE_SELL;
            FlowApply1.BillTypeCode = ConstUtil.CODING_RULE_SELLSEND_NO;
            sellTypeUC.TypeFlag = ConstUtil.SELL_TYPE_SELL;//销售类别
            sellTypeUC.TypeCode = ConstUtil.SELL_TYPE_SELLTYPE;//销售类别
            sellTypeUC.IsInsertSelect = true;
            SendOrderUC.CodingType = ConstUtil.CODING_RULE_SELL;//销售订单编号
            SendOrderUC.ItemTypeID = ConstUtil.CODING_RULE_SELLSEND_NO;//销售订单编号
            PayTypeUC.TypeFlag = ConstUtil.CUST_TYPE_CUST;//结算方式
            PayTypeUC.TypeCode = ConstUtil.CUST_INFO_PAYTYPE;//结算方式
            PayTypeUC.IsInsertSelect = true;
            PackageUC.TypeFlag = ConstUtil.SELL_TYPE_SELL;//包装
            PackageUC.IsInsertSelect = true;
            PackageUC.TypeCode = ConstUtil.SELL_TYPE_PACKAGE;//包装
            MoneyTypeUC.IsInsertSelect = true;
            MoneyTypeUC.TypeFlag = ConstUtil.CUST_TYPE_CUST;
            MoneyTypeUC.TypeCode = ConstUtil.CUST_INFO_MONEYTYPE;

        
            CarryType.TypeFlag = ConstUtil.SELL_TYPE_SELL;//运送方式
            CarryType.TypeCode = ConstUtil.SELL_TYPE_CARRYTYPE;//运送方式
            CarryType.IsInsertSelect = true;

            TakeType.TypeFlag = ConstUtil.SELL_TYPE_SELL;//交货方式
            TakeType.TypeCode = ConstUtil.SELL_TYPE_TAKETYPE;//交
            TakeType.IsInsertSelect = true;

            TransPayType.TypeFlag = ConstUtil.CUST_TYPE_CUST;//结算方式
            TransPayType.TypeCode = ConstUtil.CUST_INFO_PAYTYPE;//结算方式
            TransPayType.IsInsertSelect = true;

            hiddBillingAddModuleid.Value = XBase.Common.ConstUtil.MODULE_ID_BILLING_ADD;//开票编辑页面MoudleID
            hiddModuleID.Value = XBase.Common.ConstUtil.MODULE_ID_SELLSEND_ADD;//销售发货ModuleID
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
            
            //是否启用超订单发货
            if (UserInfo.IsOverOrder)
            {
                txtIsOverOrder.Value = "1";
            }
            else
            {
                txtIsOverOrder.Value = "0";
            }

            SetSellDept();//初始化部门信息
        }
    }
    //初始化部门信息
    protected void SetSellDept()
    {
        DeptId.Value = UserInfo.DeptName;
        hiddDeptID.Value = UserInfo.DeptID.ToString().Trim();
    }
}
