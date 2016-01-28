/**********************************************
 * 类作用：   社会保险设置
 * 建立人：   吴志强
 * 建立时间： 2009/05/06
 ***********************************************/
using System;
using XBase.Common;
using System.Text;
using System.Data;
using XBase.Business.Office.HumanManager;

public partial class Pages_Office_HumanManager_SalaryInsuSocial : BasePage
{
    /// <summary>
    /// 类名：SalaryInsuSocial
    /// 描述：社会保险设置
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/06
    /// 最后修改时间：2009/05/06
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            //显示工资项列表
            InitSalaryInfo();
        }
    }



    /// <summary>
    /// 设置工资表格
    /// </summary>
    private void InitSalaryInfo()
    {
        //设置工资表格内容
        divSalaryList.InnerHtml = CreateSalaryListTable() + InitSalaryDetailInfo() + EndTable();
    }

    #region 设置工资内容
    /// <summary>
    /// 设置工资内容
    /// </summary>
    private string InitSalaryDetailInfo()
    {
        //定义变量
        StringBuilder sbSalaryInfo = new StringBuilder();
        //获取数据
        DataTable dtSalaryInfo = InsuSocialBus.GetInsuSocialInfo(false);
        //数据存在时
        if (dtSalaryInfo != null && dtSalaryInfo.Rows.Count > 0)
        {
            //遍历显示所有数据
            for (int i = 0; i < dtSalaryInfo.Rows.Count; i++)
            {
                //插入行开始标识
                sbSalaryInfo.AppendLine("<tr>");
                //选择框
                sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                            + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
                            + GetSafeData.GetStringFromInt(dtSalaryInfo.Rows[i], "ID")
                            + "' /><input type='hidden' id='txtEditFlag_" + (i + 1).ToString() + "' value='1' />"
                            + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + (i + 1).ToString() + "' /></td>");
                //保险名称
                sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                            + "<input type='text' maxlength = '50' style='width:98%;' value='"
                            + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "InsuranceName")
                            + "' class='tdinput' id='txtInsuranceName_" + (i + 1).ToString() + "' /></td>");
                //单位比例
                sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                            + "<input type='text' maxlength = '6' style='width:98%;' value='"
                            + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "CompanyPayRate")
                            + "' class='tdinput' id='txtCompanyPayRate_" + (i + 1).ToString() + "' onchange='Number_round(this,\"2\");'    /></td>");
                //个人比例
                sbSalaryInfo.AppendLine("<td class='tdColInput'>"
                            + "<input type='text' maxlength = '6' style='width:98%;' value='"
                            + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "PersonPayRate")
                            + "' class='tdinput' id='txtPersonPayRate_" + (i + 1).ToString() + "' onchange='Number_round(this,\"2\");'    /></td>");
                //变量定义
                string selectOne = string.Empty;
                string selectTwo = string.Empty;
                //投保方式
                //按月
                if ("1".Equals(GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "InsuranceWay")))
                {
                    selectOne = "selected";
                    selectTwo = "";
                }
                //按年
                else
                {
                    selectOne = "";
                    selectTwo = "selected";
                }
                sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                            + "<select id='ddlInsuranceWay_" + (i + 1).ToString() + "'><option value='1' " + selectOne
                            + ">按月</option><option value='2'" + selectTwo + ">按年</option></select></td>");
                //启用状态
                string usedStatus = GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "UsedStatus");
                //停用
                if ("0".Equals(usedStatus))
                {
                    selectOne = "selected";
                    selectTwo = "";
                }
                //启用
                else
                {
                    selectOne = "";
                    selectTwo = "selected";
                }
                sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                            + "<input type='hidden' id='txtUsedStatusModify_" + (i + 1).ToString() + "' value='" + usedStatus + "' / >"
                            + "<select id='ddlUsedStatus_" + (i + 1).ToString() + "'><option value='0' " + selectOne
                            + ">停用</option><option value='1'" + selectTwo + ">启用</option></select></td>");
                //插入行结束标识
                sbSalaryInfo.AppendLine("</tr>");
            }
        }

        //返回信息
        return sbSalaryInfo.ToString();
    }
    #endregion

    #region 生成表格的标题
    /// <summary>
    /// 生成表格的标题
    /// </summary>
    /// <returns></returns>
    private string CreateSalaryListTable()
    {
        //定义表格变量
        StringBuilder table = new StringBuilder();
        //生成表格标题
        table.AppendLine("<table  width='99%' border='0' id='tblSalary'  align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        table.AppendLine("	<tr>                                                                  ");
        table.AppendLine("		<td class='ListTitle' width='50'>                                 ");
        table.AppendLine("		   选择<input type='checkbox' id='chkAll' onclick='SelectAll();'/>");
        table.AppendLine("		</td>                                                             ");
        table.AppendLine("		<td class='ListTitle' style='width:30%'><span style=\"color:Red\">*</span>保险名称</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:20%'><span style=\"color:Red\">*</span>单位比例(%)</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:20%'><span style=\"color:Red\">*</span>个人比例(%)</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:13%'><span style=\"color:Red\">*</span>投保方式</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:13%'><span style=\"color:Red\">*</span>启用状态</td>             ");
        table.AppendLine("	</tr>                                                                 ");
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
