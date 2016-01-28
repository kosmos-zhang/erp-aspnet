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
using XBase.Business.Office.CustManager;
using XBase.Common;
using XBase.Business.Common;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.SupplyChain;

public partial class Pages_PrinttingModel_CustManager_CustLinkPrint : BasePage
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
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_CUSTINFOLINK.ToString();
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
        model.PrintTypeFlag = ConstUtil.PRINTBILL_CUSTINFOLINK;

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
                                { "客户大类", "BigType" },

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
                              
                                { "业务类型", "BusiType"},
                                { "分管业务员 ", "ManagerName"},
                                                               
                                { "联络期限(天)", "LinkCycleNm"},
                                { "收货地址", "ReceiveAddress"},
                                
                                { "允许延期付款", "CreditManage"},
                               
                                { "帐期天数(天)", "MaxCreditDate"},
                                { "结算方式", "PayTypeNm"},
                               
                                { "关系等级", "RelaGrade"},
                                { "启用状态", "UsedStatus"},
                                
                                { "最后更新用户", "ModifiedUserID"},
                                { "最后更新日期", "ModifiedDate"},
                                
                                { "可查看该客户档案的人员", "CanViewUserName"},
                                { "卡号", "CustNum"},
                                { "性别", "Sex"},
                                { "联系人类型", "LinkTypeName"},
                                { "身份证号", "PaperNum"},
                                { "生日", "Birthday"},
                                { "电话", "WorkTel"},
                                { "手机", "Handset"},
                                { "传真", "Fax"},
                                { "职务", "Position"},
                                { "年龄", "Age"},
                                { "邮编", "Post"},
                                { "Eamil", "MailAddress"},
                                { "籍贯", "HomeTown"},
                                { "民族", "NationalName"},
                                { "所受教育", "CultureLevelName"},
                                { "所学专业", "ProfessionalName"},
                                { "年收入情况", "IncomeYear"},
                                { "饮食偏好", "FuoodDrink"},
                                { "喜欢的音乐", "LoveMusic"},
                                { "喜欢的颜色", "LoveColor"},
                                { "喜欢的香烟", "LoveSmoke"},
                                { "爱喝的酒", "LoveDrink"},
                                { "爱喝的茶", "LoveTea"},
                                { "喜欢的书籍", "LoveBook"},
                                { "喜欢的运动", "LoveSport"},
                                { "喜欢的品牌服饰", "LoveClothes"},
                                { "喜欢的品牌化妆品", "Cosmetic"},
                                { "性格描述", "Nature"},
                                { "外表描述", "Appearance"},
                                { "健康状况", "AdoutBody"},
                                { "家人情况", "AboutFamily"},
                                { "开什么车", "Car"},
                              
                          };

        string[,] aDetail = { { "", "" } };

        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = XBase.Business.Office.SupplyChain.TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba.CustInfo");
        if (dtExtTable.Rows.Count > 0)
        {
            for (int i = 0; i < dtExtTable.Rows.Count; i++)
            {
                for (int x = 0; x < (aBase.Length / 2) - 59; x++)
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
            strBaseFields = "CustNo|CustName|BigType|CustShort|CustTypeManage|CustTypeSell|CreditGradeNm|CustTypeTime|CustClassName|CustTypaNm|CreatorName|" +
                "CreatedDate|CustNote|CountryName|AreaName|BusiType|ManagerName|LinkCycleNm|ReceiveAddress|CreditManage|MaxCreditDate|PayTypeNm|RelaGrade|" +
                "UsedStatus|ModifiedUserID|ModifiedDate|CanViewUserName|CustNum|Sex|LinkTypeName|PaperNum|Birthday|WorkTel|Handset|Fax|Position|Age|Post|" +
                "MailAddress|HomeTown|NationalName|CultureLevelName|ProfessionalName|IncomeYear|FuoodDrink|LoveMusic|LoveColor|LoveSmoke|LoveDrink|LoveTea|" +
                "LoveBook|LoveSport|LoveClothes|Cosmetic|Nature|Appearance|AdoutBody|AboutFamily|Car";

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
