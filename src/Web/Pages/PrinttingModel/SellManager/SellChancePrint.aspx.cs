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

using XBase.Model.Common;
using XBase.Common;
using System.Text;
using XBase.Model.Office.SellManager;
using XBase.Business.Office.SellManager;
using XBase.Business.Office.SupplyChain;

public partial class Pages_PrinttingModel_SellManager_SellChancePrint : BasePage
{
    #region ChanceNo
    public string ChanceNo
    {
        get
        {
            return Request["no"].ToString();
        }
    }
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        hidBillTypeFlag.Value = ConstUtil.BILL_TYPEFLAG_SALE;
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_SELLCHANCE.ToString();
        hidPlanNo.Value = ChanceNo;

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
        model.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_SALE);
        model.PrintTypeFlag = ConstUtil.PRINTBILL_SELLCHANCE;

        SellChanceModel modelMRP = new SellChanceModel();
        modelMRP.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        modelMRP.ChanceNo = this.ChanceNo;

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

                                { "机会编号", "ChanceNo"}, 
                                { "机会主题", "Title"}, 
                                { "机会类型", "ChanceTypeName" },

                                { "客户名称", "CustName" },
                                { "客户电话", "CustTel"},
                                { "客户类型", "CustTypeName"},

                                { "机会来源", "HapSourceName"},
                                { "发现日期", "FindDate"},
                                { "业务员", "SellerName"},

                                { "部门", "DeptName"},
                                { "提供人", "ProvideMan"},

                                { "需求描述", "Requires"},

                                { "可查看该机会人员", "CanViewUserName"},
                                { "提醒时间", "RemindTime"},
                                { "提醒手机号", "RemindMTel"},
                                { "接收人", "ReceiverName"},
                                { "提醒内容", "RemindContent"},

                                { "预期金额", "IntendMoney"},
                                { "预期签单日", "IntendDate"},

                                { "制单人", "CreatorName"},
                                { "制单日期", "CreateDate"},
                                { "最后更新人", "ModifiedUserID"},

                                { "最后更新日期", "ModifiedDate"},
                                { "是否被报价", "IsQuotedName"},

                                { "备注", "Remark"},
                          };

        string[,] aDetail = {
                                { "阶段", "PhaseName"}, 
                                { "日期", "PushDate"}, 
                                { "业务员", "EmployeeName"}, 
                                { "状态", "StateName" },
                                { "可能性", "TypeName" },
                                { "阶段备注", "Remark"},
                           };
        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba.SellChance");
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
        DataTable dtMRP = SellChanceBus.GetRepOrder(ChanceNo);// SellPlanBus.GetRepOrder(PlanNo);
        DataTable dtDetail = SellChanceBus.GetRepOrderDetail(ChanceNo); //SellPlanBus.GetOrderDetail(PlanNo);
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
            strBaseFields = "ChanceNo|Title|ChanceTypeName|CustName|CustTel|CustTypeName|HapSourceName|FindDate|SellerName|DeptName|ProvideMan|Requires|CanViewUserName|RemindTime|RemindMTel|ReceiverName|RemindContent|IntendMoney|IntendDate|CreatorName|CreateDate|ModifiedUserID|ModifiedDate|IsQuotedName|Remark";
            /*基本信息字段+扩展信息字段*/
            if (countExt > 0)
            {
                for (int i = 0; i < countExt; i++)
                {
                    strBaseFields = strBaseFields + "|" + "ExtField" + (i + 1);
                }
            }

            strDetailFields = "PhaseName|PushDate|EmployeeName|StateName|TypeName|Remark";
        }

        #region 主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("销售机会", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, true);
        }
        #endregion

        #region 明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("销售机会", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, false);
        }
        #endregion

    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnImport_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        Response.Clear();
        Response.Charset = "gb2312";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("销售机会") + ".xls");
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
