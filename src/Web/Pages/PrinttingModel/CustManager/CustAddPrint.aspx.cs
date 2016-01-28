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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using XBase.Model.Common;
using XBase.Business.Office.CustManager;
using XBase.Common;
using XBase.Business.Common;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.SupplyChain;

public partial class Pages_PrinttingModel_CustManager_CustAddPrint : BasePage
{
    #region CustNo
    public string CustNo
    {
        get
        {
            return Request.QueryString["id"].ToString();
        }
    }
    #endregion

    #region CustBig
    public string CustBig
    {
        get
        {
            return Request.QueryString["CustBig"].ToString();
        }
    }
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        hidBillTypeFlag.Value = ConstUtil.BILL_TYPEFLAG_CUSTOMER;
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_CUSTINFO.ToString();
        hidPlanNo.Value = CustNo;
        hidCustBig.Value = CustBig;

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
        model.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_CUSTOMER);
        model.PrintTypeFlag = ConstUtil.PRINTBILL_CUSTINFO;

        CustInfoModel modelMRP = new CustInfoModel();
        modelMRP.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        modelMRP.CustNo = this.CustNo;

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
                                { "{ExtField11}", "ExtField11"},
                                { "{ExtField12}", "ExtField12"},
                                { "{ExtField13}", "ExtField13"},
                                { "{ExtField14}", "ExtField14"},
                                { "{ExtField15}", "ExtField15"},
                                { "{ExtField16}", "ExtField16"},
                                { "{ExtField17}", "ExtField17"},
                                { "{ExtField18}", "ExtField18"},
                                { "{ExtField19}", "ExtField19"},
                                { "{ExtField20}", "ExtField20"},
                                { "{ExtField21}", "ExtField21"},
                                { "{ExtField22}", "ExtField22"},
                                { "{ExtField23}", "ExtField23"},
                                { "{ExtField24}", "ExtField24"},
                                { "{ExtField25}", "ExtField25"},
                                { "{ExtField26}", "ExtField26"},
                                { "{ExtField27}", "ExtField27"},
                                { "{ExtField28}", "ExtField28"},
                                { "{ExtField29}", "ExtField29"},
                                { "{ExtField30}", "ExtField30"},

                                { "客户编号", "CustNo"}, 
                                { "客户名称", "CustName"}, 
                                { "客户大类", "CustBig" },

                                { "客户简称", "CustNam" },
                                { "拼音缩写", "CustShort"},
                               

                                { "客户管理分类", "CustTypeManage"},
                                { "客户营销分类", "CustTypeSell"},
                                { "客户优质级别", "CreditGradeNm"},

                                { "客户时间分类", "CustTypeTime"},
                                { "客户细分", "CustClassName"},
                                { "客户类别", "CustTypaNm"},

                                { "建档人", "CreatorName"},
                                { "建档日期", "CreatedDate"},
                                { "客户简介", "CustNote"},

                                { "国家地区", "CountryName"},
                                { "区域", "AreaName"},
                                { "省", "Province"},

                                { "市(县)", "City"},
                                { "业务类型", "BusiType"},
                                { "分管业务员 ", "ManagerName"},

                                { "联系人", "ContactName"},
                                { "电话", "Tel"},
                                { "手机", "Mobile"},

                                { "传真", "Fax"},
                                { "在线咨询", "OnLine"},
                                { "公司网址 ", "WebSite"},

                                { "邮编", "Post"},
                                { "电子邮件", "email"},
                                { "首次交易日期", "FirstBuyDate"},

                                { "运送方式", "CarryTypeNm"},
                                { "交货方式", "TakeTypeNm"},
                                { "联络期限(天)", "LinkCycleNm"},
                                { "收货地址", "ReceiveAddress"},
                                { "经营范围", "SellArea"},


                                { "允许延期付款", "CreditManage"},
                                { "信用额度(元)", "MaxCredit"},
                                { "帐期天数(天)", "MaxCreditDate"},
                                { "结算方式", "PayTypeNm"},
                                { "结算币种", "CurrencyaNm"},
                                { "发票类型", "BillTypeNm"},

                                { "支付方式", "MoneyTypeNm"},
                                { "开户行", "OpenBank"},
                                { "户名", "AccountMan"},
                                { "账号", "AccountNum"},

                                { "单位性质", "CompanyType"},
                                { "资产规模(万元)", "CapitalScale"},
                                { "成立时间", "SetupDate"},
                                { "注册资本(万元)", "SetupMoney"},
                                { "员工总数(个)", "StaffCount"},
                                { "法人代表", "ArtiPerson"},
                                { "行业", "Trade"},
                                { "营业执照号", "BusiNumber"},
                                { "注册地址", "SetupAddress"},
                                { "税务登记号", "TaxCD"},
                                { "是否为一般纳税人", "IsTax"},
                                { "客户来源", "Source"},
                                { "年销售额(万元)", "SaleroomY"},
                                { "年利润额(万元)", "ProfitY"},
                                { "销售模式", "SellMode"},
                                { "上级客户", "CustSupe"},
                                { "价值评估", "MeritGrade"},
                                { "阶段", "Phase"},
                                { "热点客户", "HotIs"},
                                { "热度", "HotHow"},
                                { "关系等级", "RelaGrade"},
                                { "启用状态", "UsedStatus"},
                                { "关系描述", "Relation"},
                                { "备注", "Remark"},
                                { "最后更新用户", "ModifiedUserID"},
                                { "最后更新日期", "ModifiedDate"},
                                { "经营理念", "CompanyValues"},
                                { "企业口号", "CatchWord"},
                                { "企业文化概述", "ManageValues"},
                                { "发展潜力", "Potential"},
                                { "存在问题", "Problem"},
                                { "市场优劣势", "Advantages"},
                                { "行业地位", "TradePosition"},
                                { "竞争对手", "Competition"},
                                { "合作伙伴", "Collaborator"},
                                { "发展计划", "ManagePlan"},
                                { "合作方法", "Collaborate"},
                                { "可查看该客户档案的人员", "CanViewUserName"},                                
                          };

        string[,] aDetail = { { "", "" } };

        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = XBase.Business.Office.SupplyChain.TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba.CustInfo");
        if (dtExtTable.Rows.Count > 0)
        {
            for (int i = 0; i < dtExtTable.Rows.Count; i++)
            {
                for (int x = 0; x < (aBase.Length / 2) - 82; x++)
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
        //DataTable dtMRP = SellContractBus.GetRepOrder(OfferNo);
        DataTable dtMRP = CustInfoBus.GetCustInfoByNo(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, CustBig, CustNo);
                
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
            strBaseFields = "CustNo|CustName|CustBig|CustNam|CustShort|CustTypeManage|CustTypeSell|CreditGradeNm|CustTypeTime|CustClassName|CustTypaNm|CreatorName|" +
                "CreatedDate|CustNote|CountryName|AreaName|Province|City|BusiType|ManagerName|ContactName|Tel|Mobile|Fax|OnLine|WebSite|Post|email|FirstBuyDate|" +
                "CarryTypeNm|TakeTypeNm|LinkCycleNm|ReceiveAddress|SellArea|CreditManage|MaxCredit|MaxCreditDate|PayTypeNm|CurrencyaNm|BillTypeNm|MoneyTypeNm|" +
                "OpenBank|AccountMan|AccountNum|CompanyType|CapitalScale|SetupDate|SetupMoney|StaffCount|ArtiPerson|Trade|BusiNumber|SetupAddress|TaxCD|IsTax|" +
                "Source|SaleroomY|ProfitY|SellMode|CustSupe|MeritGrade|Phase|HotIs|HotHow|RelaGrade|UsedStatus|Relation|Remark|ModifiedUserID|ModifiedDate|" +
                "CompanyValues|CatchWord|ManageValues|Potential|Problem|Advantages|TradePosition|Competition|Collaborator|ManagePlan|Collaborate|CanViewUserName";

            /*基本信息字段+扩展信息字段*/
            if (countExt > 0)
            {
                for (int i = 0; i < countExt; i++)
                {
                    strBaseFields = strBaseFields + "|" + "ExtField" + (i + 1);
                }
            }

            strDetailFields = "";
        }

        #region 主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("客户档案", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, null, true);
        }
        #endregion
    }
    #endregion


    #region
    //void LoadDataBind()
    //{
    //    string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;    

    //    string strKey = "";
    //    if (!string.IsNullOrEmpty(Request.QueryString["keyList"].ToString().Trim()))
    //    {
    //        strKey = Request.QueryString["keyList"].ToString().Trim();

    //        strKey = strKey.Substring(1, strKey.Length - 1);
    //        strKey = strKey.Replace('|', ',');
    //    }
    //    DataTable dtColumn = TableExtFieldsBus.GetAllListReport(CompanyCD);//获取扩展属性列
        
    //    DataTable myDt = new DataTable();
    //    if (dtColumn.Rows.Count > 0)
    //    {
    //        strKey = strKey.Substring(1, strKey.Length - 1);
    //        string[] myColumn = strKey.Split(',');

    //        DataTable dtValue = CustInfoBus.GetExtAttrValueReport(strKey, CustNo, CompanyCD);

    //        myDt.Columns.Add(new DataColumn("ExtField1", typeof(string)));
    //        myDt.Columns.Add(new DataColumn("ExtField2", typeof(string)));

    //        for (int i = 0; i < dtColumn.Rows.Count; i++)
    //        {
    //            DataRow myDr = myDt.NewRow();
    //            myDr["ExtField1"] = dtColumn.Rows[i]["ExtField1"];
    //            myDr["ExtField2"] = dtValue.Rows[0][myColumn[i]];
    //            myDt.Rows.Add(myDr);
    //        }
    //    }

    //    //    ReportDocument rdDetail = new ReportDocument();
    //    //    rdDetail = CrystalReportSource1.ReportDocument.Subreports["CustAddReport.rpt"];
    //    //    rdDetail.SetDataSource(myDt);

    //    //    CrystalReportSource1.ReportDocument.SetParameterValue("Today", "制表人：" + userInfo.EmployeeName);
    //    //    this.CrystalReportViewer1.ReportSource = CrystalReportSource1;

    //}
    #endregion

    //导出
    protected void btnImport_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        Response.Clear();
        Response.Charset = "gb2312";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("客户档案") + ".xls");
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
}
