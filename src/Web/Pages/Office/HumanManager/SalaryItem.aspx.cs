/**********************************************
 * 类作用：   工资项设置
 * 建立人：   吴志强
 * 建立时间： 2009/05/04
 ***********************************************/
using System;
using XBase.Common;
using System.Text;
using System.Data;
using XBase.Business.Office.HumanManager;

public partial class Pages_Office_HumanManager_SalaryItem : BasePage
{
    /// <summary>
    /// 类名：SalaryItem
    /// 描述：工资项设置
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/04
    /// 最后修改时间：2009/05/04
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
        DataTable dtSalaryInfo = SalaryItemBus.GetSalaryItemInfo(false);
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
                            + "<input type='hidden' id='txtItemNo_" + (i + 1).ToString() + "' value='"
                            + GetSafeData.GetStringFromInt(dtSalaryInfo.Rows[i], "ItemNo")
                            + "' /><input type='hidden' id='txtEditFlag_" + (i + 1).ToString() + "' value='1' />"
                            + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + (i + 1).ToString() + "' /></td>");
                //固定工资项编号
                sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                            + "<input type='text' readonly='readonly'  id='txtItem2No_" + (i + 1).ToString() + "' class='tdinput'  value='"
                            + GetSafeData.GetStringFromInt(dtSalaryInfo.Rows[i], "ItemNo")
                            + "' /> "
                            + " </td>");
                
                //排列顺序
                sbSalaryInfo.AppendLine("<td class='tdColInputCenter' id='tdRowNo_" + (i + 1).ToString() + "'>"
                            + GetSafeData.GetStringFromInt(dtSalaryInfo.Rows[i], "ItemOrder") + "</td>");
                //名称
                sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                            + "<input type='text' maxlength = '50' style='width:98%;' value='"
                            + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "ItemName")
                            + "' class='tdinput' id='txtSalaryName_" + (i + 1).ToString() + "' /></td>");
                //计算公式
                string t=GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "Calculate");
                while (t.IndexOf('A') != -1)
                {
                    t = t.Replace('A', '+');
                }
               

                sbSalaryInfo.AppendLine("<td class='tdColInputCenter'  >"
                            + "<input type='hidden'  value='"
                            +t+ "' id='txtCalculate_" + (i + 1).ToString() + "' />"
                             + "<input type='hidden'  value='"
                            + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "ParamsList") + "' id='txtCalculateParam_" + (i + 1).ToString() + "' />"
                            + "<a href='#' onclick='DoEditCalculate(  +\"" + GetSafeData.GetStringFromInt(dtSalaryInfo.Rows[i], "ItemNo") + "\" ,\"" + (i + 1).ToString() + "\");'>编辑计算公式</a></td>");
                //备注
                sbSalaryInfo.AppendLine("<td class='tdColInput'>"
                            + "<input type='text' maxlength = '100' style='width:98%;' value='"
                            + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "Remark")
                            + "' class='tdinput' id='txtRemark_" + (i + 1).ToString() + "' /></td>");
                //变量定义
                string selectZero = string.Empty;
                string selectOne = string.Empty;
                //是否扣款
                //否
                if ("0".Equals(GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "PayFlag")))
                {
                    selectZero = "selected";
                }
                //是
                else
                {
                    selectOne = "selected";
                }
                sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                            + "<select id='ddlPayFlag_" + (i + 1).ToString() + "'><option value='0' " + selectZero
                            + ">否</option><option value='1'" + selectOne + ">是</option></select></td>");
                //是否固定
                //否
                if ("0".Equals(GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "ChangeFlag")))
                {
                    selectZero = "selected";
                    selectOne = "";
                }
                //是
                else
                {
                    selectZero = "";
                    selectOne = "selected";
                }
                sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                            + "<select id='ddlChangeFlag_" + (i + 1).ToString() + "'><option value='0' " + selectZero
                            + ">否</option><option value='1'" + selectOne + ">是</option></select></td>");
                //启用状态
                string usedStatus = GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "UsedStatus");
                //停用
                if ("0".Equals(usedStatus))
                {
                    selectZero = "selected";
                    selectOne = "";
                }
                //启用
                else
                {
                    selectZero = "";
                    selectOne = "selected";
                }
                sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                            + "<input type='hidden' id='txtUsedStatusModify_" + (i + 1).ToString() + "' value='" + usedStatus + "' / >"
                            + "<select id='ddlUsedStatus_" + (i + 1).ToString() + "'><option value='0' " + selectZero
                            + ">停用</option><option value='1'" + selectOne + ">启用</option></select></td>");
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
        table.AppendLine("<table  width='100%' border='0' id='tblSalary'  align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        table.AppendLine("	<tr>                                                                  ");
        table.AppendLine("		<td class='ListTitle' width='50'>                                 ");
        table.AppendLine("		   选择<input type='checkbox' id='chkAll' onclick='SelectAll();'/>");
        table.AppendLine("		</td>                                                             ");


        table.AppendLine("		<td class='ListTitle' style='width:10%'>固定工资项编号</td>              ");
        table.AppendLine("		<td class='ListTitle' style='width:10%'>排列顺序</td>              ");
        table.AppendLine("		<td class='ListTitle' style='width:15%'><span style=\"color:Red\">*</span>名称</td>                 ");
        table.AppendLine("		<td class='ListTitle' style='width:10%'  >计算公式</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:15%'>备注</td>                 ");
        table.AppendLine("		<td class='ListTitle' style='width:10%'><span style=\"color:Red\">*</span>是否扣款</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:10%'><span style=\"color:Red\">*</span>是否固定</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:10%'><span style=\"color:Red\">*</span>启用状态</td>             ");
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
