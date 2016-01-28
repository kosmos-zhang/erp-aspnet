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
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using XBase.Model.Common;
using XBase.Common;
using System.Text;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;

/// <summary>
/// 质检报告单打印界面
/// </summary>
public partial class Pages_PrinttingModel_StorageManager_StorageCheckReportPrint : BasePage
{
    #region 质检报告单ID
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

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadPrintInfo();
        }
    }

    #region 加载打印信息
    protected void LoadPrintInfo()
    {
        PrintParameterSettingModel pModel = new PrintParameterSettingModel();
        pModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        pModel.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_QUALITYCHECK);
        pModel.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_CHECKREPORT;


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
                                { "单据编号", "ReportNo"}, 
                                { "主题", "Title"}, 
                                { "源单类型", "FromType" },
                                { "源单编号", "FromReportNo" },
                                { "往来单位", "OtherCorpName" }, 
                                { "往来单位大类", "CorpBigType" }, 
                                { "生产负责人", "PrincipalName" },
                                { "生产部门", "DeptName" },
                                { "质检类别", "CheckType" },
                                { "检验日期", "CheckDate" },
                                { "报检人员", "ApplyUserName" },
                                { "报检部门", "ApplyDeptName" },
                                { "检验人员", "CheckerName" },
                                { "检验部门", "CheckDeptName" },
                                { "检验方案", "CheckContent" },
                                { "物品编号", "ProdNo"},
                                { "物品名称", "ProductName"},
                                { "单位", "CodeName"},
                                { "规格", "Specification"},
                                { "报检数量", "CheckNum"},
                                { "抽样数量", "SampleNum"},
                                { "检验方式", "CheckMode"},
                                { "合格数量", "PassNum"},
                                { "不合格数量", "NoPass"},
                                { "合格率(%)", "PassPercent"},
                                { "检验标准", "CheckStandard"},
                                { "检验结果描述 ", "CheckResult"},
                                { "检验结果 ", "IsPass"},
                                { "是否需要复检", "isRecheck"},
                                { "单据状态", "BillStatus"},
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
                                { "检验项目", "CheckItem"}, 
                                { "检验指标", "CheckStandard"}, 
                                { "检验值", "CheckValue"}, 
                                { "检验结论", "CheckResult"}, 
                                { "检验结果", "isPass"}, 
                                { "检验数量", "CheckNum"}, 
                                { "合格数量", "PassNum"}, 
                                { "不合格数", "NotPassNum" },
                                { "检验人员", "Checker"}, 
                                { "检验部门", "CheckDeptID" },
                                { "标准值", "StandardValue"},
                                { "指标上限", "NormUpLimit"},
                                { "指标下限", "LowerLimit"},
                                { "备注", "Remark"},
                           };

        #region 扩展属性
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba.QualityCheckReport");
        int countExt = 0;

        for (int i = 0; i < dtExtTable.Rows.Count; i++)
        {
            aBase[i, 0] = dtExtTable.Rows[i]["EFDesc"].ToString();
            countExt++;
        }
        #endregion

        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(pModel);
        DataTable dt = CheckReportBus.GetReportInfo(this.intID);
        DataTable dtDetail = CheckReportBus.GetDetailInfo(this.intID);
        dtDetail.Columns.Add("SortNo", typeof(string));
        for (int i = 0; i < dtDetail.Rows.Count; i++)
        {
            dtDetail.Rows[i]["SortNo"] = i + 1;
        }
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
            tableBase.InnerHtml = WritePrintPageTable("质检报告单", strBaseFields, strDetailFields, aBase, aDetail, dt, dtDetail, true);
        }
        #endregion

        #region 明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("质检报告单", strBaseFields, strDetailFields, aBase, aDetail, dt, dtDetail, false);
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("质检报告单") + ".xls");
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
