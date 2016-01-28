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
public partial class Pages_PrinttingModel_PurchaseManager_PurchaseContractPrint: BasePage
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

    protected void Page_Init(object sender, EventArgs e)
    {
        hidBillTypeFlag.Value = ConstUtil.BILL_TYPEFLAG_PURCHASE;
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_TYPEFLAG_PurchaseContract.ToString();

        if (!IsPostBack)
        {
            LoadPrintInfo();
        }
    }

    #region 加载打印信息
    protected void LoadPrintInfo()
    {
        PrintParameterSettingModel model = new PrintParameterSettingModel();
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_PURCHASE);
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_PurchaseContract;


        //PurchaseApplyModel modelMRP = new PurchaseApplyModel();
        //PurchaseApplyModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //PurchaseApplyModel.ID = this.intMrpID;

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
                                { "单据编号", "ContractNo"}, 
                                { "主题", "Title"}, 
                                   { "源单类型", "FromTypeName" },
                                { "采购类别", "TypeIDName" },
                                   { "采购员", "SellerName" },
          { "部门", "DeptName" },
            { "是否为增值税", "isAddTaxName" },
                { "支付方式", "MoneyTypeName" },
                            { "签约日期", "SignDate" },
                               { "供应商签约人", "TheyDelegate" },
                                 { "供应商", "ProviderName"},
                      
                               
                        
                                   
                                     
                                      { "我方签约人", "OurUserName" },
                                    
                                        
                                             { "交货方式", "TakeTypeName" },
                                              { "运送方式", "CarryTypeName" },
                                                   { "结算方式", "PayTypeName" },
                                                { "币种", "CurrencyTypeName" },
                                                     { "汇率", "Rate" },
                                                            { "签约地点", "SignAddr" },

                                { "数量总计", "CountTotal"},
                                { "金额总计", "TotalPrice"}, 
                                   { "税额合计", "TotalTax"},
                                     { "含税总额总计", "TotalFee"},
                                         { "整单折扣", "Discount"},
                                          { "折扣金额", "DiscountTotal"},
                                            { "折后含税额", "RealTotal"},
                                   
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
          
          string[,] aDetail ;
                if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
        {
       aDetail =new  string[,]  { 
                           { "序号", "SortNo" },
                             { "物品编号", "ProductNo" },
                                { "物品名称", "ProductName" },
                                  { "颜色", "ColorName" },
                                 { "基本数量", "ProductCount"},
                                { "交货日期", "RequireDate" }, 

                                   { "采购数量", "UsedUnitCount"},
                                { "单位", "UsedUnitName" }, 
                                    { "基本单位", "UnitName" },
                                { "金额", "TotalPrice"},
                                 { "单价", "UsedPrice" },
                                      { "含税价", "TaxPrice" }, 
                                { "税率", "TaxRate"},
                                { "含税金额", "TotalFee"},
                                    { "税额", "TotalTax"},

                                      { "规格", "standard"},
                                    
                           };
                }
                else
                {       aDetail =new  string[,]  { 
                           { "序号", "SortNo" },
                             { "物品编号", "ProductNo" },
                                { "物品名称", "ProductName" },
                                     { "颜色", "ColorName" },
                                 { "采购数量", "ProductCount"},
                                { "交货日期", "RequireDate" }, 
                                { "金额", "TotalPrice"},
                                 { "单价", "UnitPrice" },
                                   { "单位", "UnitName" },
                                      { "含税价", "TaxPrice" }, 
                                { "税率", "TaxRate"},
                                { "含税金额", "TotalFee"},
                                    { "税额", "TotalTax"},
                                      { "规格", "standard"},
                           };

                }

        /*第二明细*/
        //string[,] aSecondDetail = { 
        //                   };

        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba." + ConstUtil.CODING_RULE_TABLE_PURCHASECONTRACT);
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
        DataTable dtMRP = PurchaseContractBus.SelectContract(this.intMrpID);
        DataTable dtDetail = PurchaseContractBus.Details(this.intMrpID);

        //DataTable dtp = PurchaseContractBus.SelectContract(ID);
        //DataTable dt2 = PurchaseContractBus.Details(ID);

        //DataTable PurchaseApplyPrimary = PurchaseApplyBus.GetPurchaseApply(ID);
        //DataTable PurchaseApplySource = PurchaseApplyBus.GetPurchaseApplySource(ID);
        //   DataTable PurchaseApplyDetail2 = PurchasePlanBus.GetPurchasePlanDetail(this.intMrpID.ToString());





        string strBaseFields = "";
        string strDetailFields = "";
        /*第二明细*/
        string strDetailSecondFields = "";

        if (dbPrint.Rows.Count > 0)
        {
            isSeted.Value = "1";
            strBaseFields = dbPrint.Rows[0]["BaseFields"].ToString();
            strDetailFields = dbPrint.Rows[0]["DetailFields"].ToString();
            /*第二明细*/
            strDetailSecondFields = dbPrint.Rows[0]["DetailSecondFields"].ToString();
        }
        else
        {
            isSeted.Value = "0";
            strBaseFields = "ContractNo|Title|TypeIDName|ProviderName|isAddTaxName|MoneyTypeName|OurUserName|TakeTypeName|CarryTypeName|Rate|CurrencyTypeName|PayTypeName|TheyDelegate|TotalTax|DeptName|SellerName|SignDate|FromTypeName|CountTotal|TotalPrice|CreatorName|CreateDate|BillStatusName|SignAddr|ConfirmorName|ConfirmDate|CloserName|CloseDate|ModifiedUserID|ModifiedDate|Remark";
            /*基本信息字段+扩展信息字段*/
            if (countExt > 0)
            {
                for (int i = 0; i < countExt; i++)
                {
                    strBaseFields = strBaseFields + "|" + "ExtField" + (i + 1);
                }
            }

                  

                  if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                  {
                      strDetailFields = "SortNo|ProductNo|ProductName|ColorName|ProductCount|RequireDate|UsedPrice|UnitName|UsedUnitCount|UsedUnitName|TotalPrice|TaxPrice|TotalTax|TaxRate|TotalFee|standard";
                  }
                  else{
                      strDetailFields = "SortNo|ProductNo|ProductName|ColorName|ProductCount|RequireDate|UnitPrice|UnitName|TotalPrice|TaxPrice|TotalTax|TaxRate|TotalFee|standard";
        }
            /*第二明细*/
        //    strDetailSecondFields = "SortNo|ProdNo|ProductName|ProductCount|RequireDate|TotalTax|ProductCount|RequireDate|UnitPrice|TotalPrice|OrderCount";
        }

        #region 2.主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("采购合同", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, true);
        }
        #endregion

        #region 3.明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("采购合同", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, false);
        }
        #endregion

        /*第二明细*/
        #region 4.明细信息2
        //if (!string.IsNullOrEmpty(strDetailSecondFields))
        //{
        //    tableDetail2.InnerHtml = WritePrintPageTable("采购合同", strBaseFields, strDetailSecondFields, aBase, aSecondDetail, dtMRP, PurchaseApplyDetail2, false);
        //}
        #endregion

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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("采购合同") + ".xls");
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
