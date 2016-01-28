/**********************************************
 * 类作用：   计时工资设置
 * 建立人：   吴志强
 * 建立时间： 2009/05/06
 ***********************************************/
using System;
using XBase.Common;
using System.Text;
using System.Data;
using XBase.Business.Office.HumanManager;

public partial class Pages_Office_HumanManager_SalaryTime : BasePage
{
    /// <summary>
    /// 类名：SalaryTime
    /// 描述：计时工资设置
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
        DataTable dtSalaryInfo = TimeItemBus.GetTimeItemInfo(false);
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
                //项目编号
                sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                            + "<input type='text' maxlength = '50' style='width:98%;' value='"
                            + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "TimeNo")
                            + "' disabled class='tdinput' id='txtTimeNo_" + (i + 1).ToString() + "' /></td>");
                //项目名称
                sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                            + "<input type='text' maxlength = '50' style='width:98%;' value='"
                            + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "TimeName")
                            + "' class='tdinput' id='txtTimeName_" + (i + 1).ToString() + "' /></td>");
                //单价
                sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                            + "<input type='text' maxlength = '12' style='width:98%;' value='"
                            + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "UnitPrice")
                            + "' class='tdinput' id='txtUnitPrice_" + (i + 1).ToString() + "' style='text-align:center' onchange='Number_round(this,\"2\");'    /></td>");
                //启用状态
                //变量定义
                string selectZero = string.Empty;
                string selectOne = string.Empty;
                //停用
                if ("0".Equals(GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "UsedStatus")))
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
        table.AppendLine("<table  width='99%' border='0' id='tblSalary'  align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        table.AppendLine("	<tr>                                                                  ");
        table.AppendLine("		<td class='ListTitle' width='50'>                                 ");
        table.AppendLine("		   选择<input type='checkbox' id='chkAll' onclick='SelectAll();'/>");
        table.AppendLine("		</td>                                                             ");
        table.AppendLine("		<td class='ListTitle' style='width:30%'><span style=\"color:Red\">*</span>项目编号</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:30%'><span style=\"color:Red\">*</span>项目名称</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:20%'><span style=\"color:Red\">*</span>单价</td>                 ");
        table.AppendLine("		<td class='ListTitle' style='width:15%'><span style=\"color:Red\">*</span>启用状态</td>             ");
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
