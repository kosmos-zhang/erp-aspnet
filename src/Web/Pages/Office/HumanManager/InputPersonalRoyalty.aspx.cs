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
using System.Text;
public partial class Pages_Office_HumanManager_InputPersonalRoyalty : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            //显示工资项列表
            InitSalaryInfo();
            this.txtOpenDate.Value = System.DateTime.Now.AddDays(1 - System.DateTime.Now.Day).ToString("yyyy-MM-dd");
            this.txtCloseDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    /// <summary>
    /// 设置工资表格
    /// </summary>
    private void InitSalaryInfo()
    {
        //设置工资表格内容
        divInsuDetail.InnerHtml = CreateSalaryListTable() + EndTable();
    }

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
        table.AppendLine("		<td class='ListTitle' style='width:5%'>                                 ");
        table.AppendLine("		   选择<input type='checkbox' id='chkAll' onclick='SelectAll();'/>");
        table.AppendLine("		</td>                                                             ");
        table.AppendLine("		<td class='ListTitle' style='width:12%'>员工姓名</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:12%'>订单编号</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:10%'>币种</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:10%'>汇率</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:12%'>含税金额合计</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:13%'>客户名称</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:8%'>提成率</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:8%'>订单日期</td>             ");
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
