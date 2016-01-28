/**********************************************
 * 类作用：   新建面试评测模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/16
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Collections;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Business.Common;

public partial class Pages_Office_HumanManager_RectCheckTemplate_Edit : BasePage
{

    /// <summary>
    /// 类名：RectCheckTemplate_Edit
    /// 描述：新建面试评测模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/16
    /// 最后修改时间：2009/04/16
    /// </summary>
    ///
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {

            #region 新建、修改共通处理
            //岗位设置
            DataTable dtQuarter = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
            ddlQuarter.DataSource = dtQuarter;
            ddlQuarter.DataValueField = "ID";
            ddlQuarter.DataTextField = "QuarterName";
            ddlQuarter.DataBind();
            //模板列表模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_RECTCHECKTEMPLATE_INFO;
            //获取请求参数
            string requestParam = Request.QueryString.ToString();
            //通过参数个数来判断是否从菜单过来
            int firstIndex = requestParam.IndexOf("&");
            //从列表过来时
            if (firstIndex > 0)
            {
                //返回按钮可见
                btnBack.Visible = true;
                //获取列表的查询条件
                string searchCondition = requestParam.Substring(firstIndex);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
            }
            else
            {
                //返回按钮不可见
                btnBack.Visible = false;
            }
            #endregion

            //获取模板ID
            string templateID = Request.QueryString["TemplateID"];
            //模板ID为空时，为新建模式
            if (string.IsNullOrEmpty(templateID))
            {
                #region 新建时初期处理
                //编号初期处理
                codeRule.CodingType = ConstUtil.CODING_TYPE_HUMAN;
                codeRule.ItemTypeID = ConstUtil.CODING_HUMAN_ITEM_CHECKTEMPLATE;
                //设置编号不可见
                divCodeNo.Attributes.Add("style", "display:none;");
                //自动生成编号的控件设置为可见
                divCodeRule.Attributes.Add("style", "display:block;");
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_INSERT;
                //要素信息结果
                divElemDetail.InnerHtml = CreateElemTable() + EndTable();
                #endregion
            }
            else
            {
                #region 修改时初期处理
                //设置标题
                divTitle.InnerText = "面试评测模板";
                //获取并设置模板信息
                InitTemplateInfo(templateID);
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_UPDATE;
                #endregion
            }
        }
    }
    #endregion

    #region 根据面试模板ID，获取模板信息，并设置到页面显示
    /// <summary>
    /// 根据面试模板ID，获取模板信息，并设置到页面显示
    /// </summary>
    /// <param name="templateID">模板ID</param>
    private void InitTemplateInfo(string templateID)
    {
        //设置编号可见
        divCodeNo.Attributes.Add("style", "display:block;");
        //自动生成编号的控件设置为不可见
        divCodeRule.Attributes.Add("style", "display:none;");

        //查询考试信息
        DataSet dsTemplateInfo = RectCheckTemplateBus.GetRectCheckTemplateInfoWithID(templateID);
        //获取考试基本信息
        DataTable dtBaseInfo = dsTemplateInfo.Tables[0];
        //基本信息存在时
        if (dtBaseInfo != null && dtBaseInfo.Rows.Count > 0)
        {
            #region 设置基本信息
            //模板编号
            divCodeNo.InnerHtml = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "TemplateNo");
            //主题
            txtTitle.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Title");
            //岗位
            ddlQuarter.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "QuarterID");
            //考试状态
            txtRemark.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Remark");
            #endregion

            //设置要素信息
            InitElemInfo(dsTemplateInfo.Tables[1]);
        }
    }
    #endregion

    #region 设置要素结果
    /// <summary>
    /// 设置要素结果
    /// </summary>
    /// <param name="dtElem">要素信息</param>
    private void InitElemInfo(DataTable dtElem)
    {
        //定义保存进度安排的变量
        StringBuilder sbElemInfo = new StringBuilder();
        //进度安排存在时，设置进度安排
        if (dtElem != null && dtElem.Rows.Count > 0)
        {
            //遍历所有要素添加到表格中
            for (int i = 0; i < dtElem.Rows.Count; i++)
            {
                //插入行开始标识
                sbElemInfo.AppendLine("<tr>");
                //选择框
                sbElemInfo.AppendLine("<td class='tdColInputCenter'><input type='checkbox' id='chkSelect_" + (i + 1).ToString() + "' style='width:95%'></td>");
                //要素名称
                sbElemInfo.AppendLine("<td class='tdColInput'  disabled =\"true\"><input type='hidden' value='"
                            + GetSafeData.ValidateDataRow_String(dtElem.Rows[i], "CheckElemID") + "' id='hidElemID_" + (i + 1).ToString() + "' style='width:95%'>"
                            + GetSafeData.ValidateDataRow_String(dtElem.Rows[i], "ElemName") + "</td>");
                //满分
                sbElemInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '6' value='"
                            + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtElem.Rows[i], "MaxScore"))
                            + "' class='tdinput' id='txtMaxScore_" + (i + 1).ToString() + "'  style='width:95%'></td>");
                //权重
                sbElemInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '6' value='"
                            + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtElem.Rows[i], "Rate"))
                            + "' class='tdinput' id='txtRate_" + (i + 1).ToString() + "' style='width:95%'></td>");
                //备注
                sbElemInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '100' value='"
                            + GetSafeData.ValidateDataRow_String(dtElem.Rows[i], "Remark") + "' class='tdinput'  id='txtElemRemark_" + (i + 1).ToString() + "' style='width:95%'></td>");
                
                //插入行结束标识
                sbElemInfo.AppendLine("</tr>");
            }
        }
        //要素细腻设置到DIV中表示
        divElemDetail.InnerHtml = CreateElemTable() + sbElemInfo.ToString() + EndTable();
    }
    #endregion

    #region 生成要素表格的标题部分
    /// <summary>
    /// 生成要素表格的标题部分
    /// </summary>
    /// <returns></returns>
    private string CreateElemTable()
    {
        //定义表格变量
        StringBuilder table = new StringBuilder();
        //生成表格标题
        table.AppendLine("<table id='tblElemDetail'  width='100%' border='0'align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        table.AppendLine("<tr>");
        table.AppendLine("<td class='ListTitle' style='width:20%'>");
        table.AppendLine("选择<input type='checkbox' name='chkAll' id='chkAll' onclick='SelectAll();'/>");
        table.AppendLine("</td>");
        table.AppendLine("<td class='ListTitle' style='width:20%'>要素名称</td>");
        table.AppendLine("<td class='ListTitle' style='width:20%'>满分</td>");
        table.AppendLine("<td class='ListTitle' style='width:20%'>权重(%)</td>");
        table.AppendLine("<td class='ListTitle' style='width:20%'>备注</td>");
        table.AppendLine("</tr>");

        //返回表格语句
        return table.ToString();
    }
    #endregion

    #region 返回表格的结束符
    /// <summary>
    /// 返回表格的结束符
    /// </summary>
    /// <returns></returns>
    private string EndTable()
    {
        return "</table>";
    }
    #endregion

}
