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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;
using XBase.Model.Common;
using XBase.Common;
using System.Text;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;

public partial class Pages_PrinttingModel_PurchaseManager_PurchaseRejectPrint : BasePage
{
    #region MRP ID
    public int intMrpID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ID"], out tempID);
            return tempID;
        }
    }
    #endregion
    /// <summary>
    /// 是否启用多计量单位(true：启用；false：不启用)
    /// </summary>
    private bool _isMoreUnit = false;


    /// <summary>
    /// 是否启用多计量单位(ture：启用；false：不启用)
    /// </summary>
    public bool IsMoreUnit
    {
        get
        {
            return _isMoreUnit;
        }
        set
        {
            _isMoreUnit = value;
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        UserInfo = SessionUtil.Session["UserInfo"] as UserInfoUtil;
        _isMoreUnit = UserInfo.IsMoreUnit;
        hidBillTypeFlag.Value = ConstUtil.BILL_TYPEFLAG_PURCHASE;
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_TYPEFLAG_PurchaseReject.ToString();

        if (!IsPostBack)
        {
            LoadPrintInfo();
        }
    }

    #region 加载打印信息
    protected void LoadPrintInfo()
    {
        PrintParameterSettingModel model = new PrintParameterSettingModel();
        model.CompanyCD = UserInfo.CompanyCD;
        model.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_PURCHASE);
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_PurchaseReject;

        /*此处需注意在模板设置表里的字段和取基本信息的字段是否一致*/
        string[,] aBase = { 
                                { "{ExtField1}", "ExtField1"},
                                { "{ExtField2}", "ExtField2"},
                                { "{ExtField3}", "ExtField3"},
                                { "{ExtField4}", "ExtField4"},
                                { "{ExtField5}", "ExtField5"},
                                { "{ExtField6}", "ExtField6"},
                                { "{ExtField7}", "ExtField7"},
                                { "{ExtField8}", "ExtField8"},
                                { "{ExtField9}", "ExtField9"},
                                { "{ExtField10}", "ExtField10"},
                                { "单据编号", "RejectNo"}, 
                                { "主题", "Title"}, 
                                { "采购类别", "TypeName" },
                                { "供应商", "ProviderName"},
                                { "部门", "DeptName" },
                                { "采购员", "PurchaserName" },
                                { "是否建单", "isOpenbillName" }, 
                                { "收货人", "ReceiveMan" },
                                { "收货人联系电话", "ReceiveTel" },
                                { "源单类型", "FromTypeName" },
                                { "支付方式", "MoneyTypeName" },
                                { "发货地址", "SendAddress" },
                                { "收货地址", "ReceiveOverAddress" },
                                { "是否为增值税", "isAddTaxName" },
                                { "交货方式", "TakeTypeName" },
                                { "运送方式", "CarryTypeName" },
                                { "结算方式", "PayTypeName" },
                                { "币种", "CurrencyTypeName" },
                                { "汇率", "Rate" }, 
                                { "退货时间", "RejectDate" }, 
                                { "所属项目", "ProjectName" }, 
                                { "数量总计", "CountTotal"},
                                { "金额总计", "TotalPrice"}, 
                                { "税额合计", "TotalTax"},
                                { "含税总额总计", "TotalFee"},
                                { "整单折扣", "Discount"},
                                { "折扣金额", "DiscountTotal"},
                                { "折后含税额", "RealTotal"},
                                { "抵应付帐款", "TotalDyfzk"},
                                { "应退货款合计", "TotalYthkhj"},

                                { "单据状态", "BillStatusName"},
                                { "制单人", "CreatorName"},
                                { "制单日期", "CreateDate"},
                                { "确认人", "ConfirmorName"},
                                { "确认日期", "ConfirmDate"},
                                { "结单人", "CloserName"},
                                { "结单日期", "CloseDate"},
                                { "最后更新人", "ModifiedUserID"},
                                { "最后更新日期", "ModifiedDate"},
                                { "备注", "Remark"},
                          };

        string[,] aDetail = { 
                                { "序号", "SortNo" },
                                { "物品编号", "ProductNo" },
                                { "物品名称", "ProductName" },
                                { "规格", "Specification" },
                                { "颜色", "ColorName" },
                                { "单位", "UnitName"}, 
                                { "退货数量", "BackCount"},
                                { "金额", "TotalPrice"},
                                { "单价", "UnitPrice" },
                                { "含税价", "TaxPrice" }, 
                                { "税率", "TaxRate"},
                                { "含税金额", "TotalFee"},
                                { "税额", "TotalTax"},
                           };
        if (_isMoreUnit)
        {// 启用多计量单位
            aDetail = new string[,]
                            { 
                                { "序号", "SortNo" },
                                { "物品编号", "ProductNo" },
                                { "物品名称", "ProductName" },
                                { "规格", "Specification" },
                                { "颜色", "ColorName" },
                                { "基本单位", "UnitName" },
                                { "基本数量", "BackCount"},
                                { "单位", "UsedUnitName"}, 
                                { "退货数量", "UsedUnitCount"},
                                { "金额", "TotalPrice"},
                                { "单价", "UnitPrice" },
                                { "含税价", "TaxPrice" }, 
                                { "税率", "TaxRate"},
                                { "含税金额", "TotalFee"},
                                { "税额", "TotalTax"},
                           };
        }
        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(UserInfo.CompanyCD, "", "officedba." + ConstUtil.CODING_RULE_TABLE_PURCHASEREJECT);
        if (dtExtTable.Rows.Count > 0)
        {
            for (int i = 0; i < dtExtTable.Rows.Count; i++)
            {
                for (int x = 0; x < (aBase.Length / 2) - 15; x++)
                {
                    if (x == i)
                    {
                        aBase[x, 0] = dtExtTable.Rows[i]["EFDesc"].ToString();
                        countExt++;
                    }
                }
            }
        }
        #endregion

        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(model);

        DataTable dtMRP = PurchaseRejectBus.SelectReject(this.intMrpID);
        string formType = string.Empty;
        if (dtMRP != null)
        {
            if (dtMRP.Rows.Count > 0)
            {
                formType = dtMRP.Rows[0]["FromType"] == null ? "" : dtMRP.Rows[0]["FromType"].ToString();
            }
        }
        DataTable dtDetail = PurchaseRejectBus.Details(this.intMrpID, formType);



        string strBaseFields = "";
        string strDetailFields = "";

        if (dbPrint.Rows.Count > 0)
        {
            isSeted.Value = "1";
            strBaseFields = dbPrint.Rows[0]["BaseFields"].ToString();
            strDetailFields = dbPrint.Rows[0]["DetailFields"].ToString();
        }
        else
        {
            isSeted.Value = "0";

            strBaseFields = GetDefaultFields(aBase);
            /*基本信息字段+扩展信息字段*/
            if (countExt > 0)
            {
                for (int i = 0; i < countExt; i++)
                {
                    strBaseFields = strBaseFields + "|" + "ExtField" + (i + 1);
                }
            }
            strDetailFields = GetDefaultFields(aDetail);
        }

        #region 2.主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("采购退货", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, true);
        }
        #endregion

        #region 3.明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("采购退货", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, false);
        }
        #endregion
    }

    /// <summary>
    /// 获得默认字段
    /// </summary>
    /// <param name="a">二维字符串数组</param>
    /// <returns></returns>
    private string GetDefaultFields(string[,] aBase)
    {
        StringBuilder sb = new StringBuilder();
        int len = aBase.Length / 2;
        for (int i = 0; i < len; i++)
        {
            if (aBase[i, 1].StartsWith("ExtField"))
            {
                continue;
            }
            sb.AppendFormat("{0}|", aBase[i, 1]);
        }
        return sb.ToString().TrimEnd('|');
    }

    #endregion

    #region 导出
    protected void btnImport_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        Response.Clear();
        Response.Charset = "gb2312";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("采购退货") + ".xls");
        Response.Write("<html><head><META http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\"></head><body>");
        Response.Write(hiddExcel.Value);
        Response.Write(tw.ToString());
        Response.Write("</body></html>");
        Response.End();
        hw.Close();
        hw.Flush();
        tw.Close();
        tw.Flush();
    }
    #endregion
}
