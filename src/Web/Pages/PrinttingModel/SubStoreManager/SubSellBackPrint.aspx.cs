using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using XBase.Business.Office.SubStoreManager;
using XBase.Business.Office.SupplyChain;
using XBase.Common;
using XBase.Model.Common;
using XBase.Model.Office.SubStoreManager;
using XBase.Model.Office.SupplyChain;

/// <summary>
/// 销售退货单打印界面
/// </summary>
public partial class Pages_PrinttingModel_SubStoreManager_SubSellBackPrint : BasePage
{
    #region 销售退货单ID
    public int intID
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
        if (!IsPostBack)
        {
            LoadPrintInfo();
        }
    }

    #region 加载打印信息
    protected void LoadPrintInfo()
    {
        PrintParameterSettingModel pModel = new PrintParameterSettingModel();
        pModel.CompanyCD = UserInfo.CompanyCD;
        pModel.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_SUBSTORAGE);
        pModel.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_SUBSELLBACK;


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
                                { "单据编号", "BackNo"}, 
                                { "单据主题", "Title"}, 
                                { "销售分店", "DeptName" },
                                { "源单类型", "FromType" },
                                { "对应分店销售订单", "OrderID" }, 
                                { "发货模式", "SendModeName" }, 
                                { "发货时间", "OutDate"},
                                { "发货人", "OutUserIDName"},
                                { "是否增值税", "isAddTax"},
                                { "客户名称", "CustName"},
                                { "客户联系电话", "CustTel"},
                                { "客户手机号", "CustMobile"},
                                { "币种", "CurrencyTypeName"},
                                { "汇率", "Rate"},
                                { "业务状态", "BusiStatusName"},
                                { "退货时间", "BackDate"},
                                { "退货处理人", "SellerName"},
                                { "入库时间", "InDate"},
                                { "入库人", "InUserIDName"},
                                { "结算时间", "SttlDate"},
                                { "结算人", "SttlUserIDName"},
                                { "客户地址", "CustAddr"},
                                { "退货理由描述", "BackReason"},
                                { "退货数量合计", "CountTotal"},
                                { "金额合计", "TotalPrice"},
                                { "税额合计", "Tax"},
                                { "含税金额合计", "TotalFee"},
                                { "整单折扣(%)", "Discount"},
                                { "折扣金额合计", "DiscountTotal"},
                                { "折后含税金额", "RealTotal"},
                                { "应退货款", "WairPayTotal"},
                                { "已退货款", "PayedTotal"},
                                { "应退货款余额", "WairPayTotalOverage"},
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
                                { "序号", "SortNo"}, 
                                { "产品编号", "ProductNo"}, 
                                { "产品名称", "ProductName"}, 
                                { "批次", "BatchNo"}, 
                                { "规格", "standard"}, 
                                { "单位", "UnitName"}, 
                                { "发货数量", "ProductCount"}, 
                                { "退货数量", "BackCount"}, 
                                { "单价", "UnitPrice"}, 
                                { "含税价", "TaxPrice"}, 
                                { "折扣", "Discount"}, 
                                { "税率", "TaxRate"}, 
                                { "金额", "TotalPrice"}, 
                                { "含税金额", "TotalFee"}, 
                                { "仓库", "StorageName"}, 
                                { "备注", "Remark"}
                           };
        if (_isMoreUnit)
        {// 启用多计量单位
            aDetail = new string[,]
                            {
                                { "序号", "SortNo"}, 
                                { "产品编号", "ProductNo"}, 
                                { "产品名称", "ProductName"}, 
                                { "批次", "BatchNo"}, 
                                { "规格", "standard"}, 
                                { "基本单位", "UnitName" },
                                { "基本数量", "BackCount"},
                                { "单位", "UsedUnitName"}, 
                                { "发货数量", "ProductCount"}, 
                                { "退货数量", "UsedUnitCount"}, 
                                { "单价", "UnitPrice"}, 
                                { "含税价", "TaxPrice"}, 
                                { "折扣", "Discount"}, 
                                { "税率", "TaxRate"}, 
                                { "金额", "TotalPrice"}, 
                                { "含税金额", "TotalFee"}, 
                                { "仓库", "StorageName"}, 
                                { "备注", "Remark"}
                            };
        }

        #region 扩展属性
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(UserInfo.CompanyCD, "", "officedba.SubSellBack");
        int countExt = 0;
        for (int i = 0; i < dtExtTable.Rows.Count; i++)
        {
            aBase[i, 0] = dtExtTable.Rows[i]["EFDesc"].ToString();
            countExt++;
        }
        #endregion

        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(pModel);
        DataTable dt = SubSellBackBus.SubSellBack(intID);
        ConvertDataTable(dt);
        DataTable dtDetail = SubSellBackBus.Details(intID);

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
                    strBaseFields += "|ExtField" + (i + 1);
                }
            }
            strDetailFields = GetDefaultFields(aDetail);
        }

        #region 主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("销售退货单", strBaseFields, strDetailFields, aBase, aDetail, dt, dtDetail, true);
        }
        #endregion

        #region 明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("销售退货单", strBaseFields, strDetailFields, aBase, aDetail, dt, dtDetail, false);
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

    /// <summary>
    /// 转换数据
    /// </summary>
    /// <param name="dt"></param>
    private void ConvertDataTable(DataTable dt)
    {
        if (dt == null || dt.Rows.Count < 1)
        {
            return;
        }
        dt.Columns.Add("FromType1", typeof(string));
        dt.Columns.Add("isAddTax1", typeof(string));
        foreach (DataRow dr in dt.Rows)
        {
            switch (dr["FromType"].ToString())
            {
                case "1":
                    dr["FromType1"] = "销售订单";
                    break;
                default:
                    dr["FromType1"] = "无来源";
                    break;
            }
            switch (dr["isAddTax"].ToString())
            {
                case "1":
                    dr["isAddTax1"] = "是";
                    break;
                default:
                    dr["isAddTax1"] = "否";
                    break;
            }
        }
        dt.Columns.Remove("FromType");
        dt.Columns.Remove("isAddTax");
        dt.Columns["FromType1"].ColumnName = "FromType";
        dt.Columns["isAddTax1"].ColumnName = "isAddTax";
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("销售退货单") + ".xls");
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
