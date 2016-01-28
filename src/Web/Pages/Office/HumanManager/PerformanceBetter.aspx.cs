/**********************************************
 * 类作用：      新建绩效改进计划      
 * 建立人：    王保军
 * 建立时间： 2009.5.17
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Collections;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Business.Common;

public partial class Pages_Office_HumanManager_PerformanceBetter : BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 新建自我鉴定编号初期处理
            AimNum.CodingType = ConstUtil.CODING_RULE_TYPE_PERFORMANCEBETTER;
            AimNum.ItemTypeID = ConstUtil.CODING_RULE_HUMEN_PERFORMANCEBETTER;
            // AimNum.TableName = ConstUtil.CODING_RULE_TABLE_PERFORMANCEBETTER;//目标表名
            // AimNum.ColumnName = ConstUtil.CODING_RULE_COLUMN_PERFORMANCEBETTER_PLANO;//目标编号字段名
            //获取人员编号
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_PERFORMANCEBETTER;
            string requestParam = Request.QueryString.ToString();
            //通过参数个数来判断是否从菜单过来
            int firstIndex = requestParam.IndexOf("&");
            //从列表过来时
            if (firstIndex > 0)
            {
                //返回按钮可见
       
                //获取列表的查询条件
                string searchCondition = requestParam.Substring(firstIndex);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
            }
            else
            {
                //返回按钮不可见
             
            }
            if (Request.QueryString["hidIsliebiao"] != null)
            {
                hidIsliebiao.Value = "1";
            }

            string planID = string.Empty;
            if (Request.QueryString["PlanNo"] != null)
            {
                planID = Request.QueryString["PlanNo"].ToString();
            }
            else
            {
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                divCreater.InnerHtml = userInfo.EmployeeName;
                dvCreateDate.InnerHtml = DateTime.Now.ToShortDateString();
            }
           

            //planID = "38";
            //招聘活动编号为空，为新建模式
            if (string.IsNullOrEmpty(planID))
            {
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_INSERT;
                divTitle.InnerText = "新建绩效改进计划";
            }
            else
            {
                #region 修改时初期处理
                //设置标题
                divTitle.InnerText = "绩效改进计划";
                //获取并设置人员信息
                InitRectPlanInfo(planID);
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_UPDATE;
                #endregion
            }
        }
    }
    private void InitRectPlanInfo(string planID)
    {

        //设置申请计划编号可见
        divRectPalnNo.Attributes.Add("style", "display:block;");
        //自动生成编号的控件设置为不可见
        txtPerformTmNo.Attributes.Add("style", "display:none;");
         

        //查询招聘计划信息
        DataSet dsPlanInfos = PerformanceBetterBus.GetRectPlanInfoWithID(planID);
        //获取招聘计划基本信息
        DataTable dtBaseInfo = dsPlanInfos.Tables[0];
        StringBuilder sbPublishInfo = new StringBuilder();
        //基本信息存在时
        if (dtBaseInfo != null && dtBaseInfo.Rows.Count > 0)
        {
            //设置申请计划编号
            divRectPalnNo.InnerHtml = dtBaseInfo.Rows[0]["PlanNo"] == null ? "" : dtBaseInfo.Rows[0]["PlanNo"].ToString();
            //主题
            
            txtTitle.Value  = dtBaseInfo.Rows[0]["Title"]==null ?"":dtBaseInfo.Rows[0]["Title"].ToString ();
            //创建时间
            dvCreateDate.InnerHtml = dtBaseInfo.Rows[0]["CreateDate"] == null ? "" : dtBaseInfo.Rows[0]["CreateDate"].ToString ();
            divCreater.InnerHtml = dtBaseInfo.Rows[0]["Creator"] == null ? "" : dtBaseInfo.Rows[0]["Creator"].ToString();
            txtPlanRemark.Value = dtBaseInfo.Rows[0]["Remark"] == null ? "" : dtBaseInfo.Rows[0]["Remark"].ToString();
            for (int i = 0; i < dtBaseInfo.Rows.Count; i++)
            {
                //插入行开始标识
                sbPublishInfo.AppendLine("<tr style='display:block;'>");
                //选择框
                sbPublishInfo.AppendLine("<td class='tdColInputCenter'><input type='checkbox' id='tbDetail_chkSelect_" + (i + 1).ToString() + "' ></td>");
                //员工  
                sbPublishInfo.AppendLine("<td class='tdColInput'> <input  id='UsertxtEmployeeID1" + (i + 1).ToString() + "' size='8'  maxlength='50'  readonly  type='text' value='"
                            + GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[i], "EmployeeName") + "'   class='tdinput' onclick=alertdiv('UsertxtEmployeeID1" + (i + 1).ToString() + ",txtEmployeeID_" + (i + 1).ToString() + "') />" + "<input  id='txtEmployeeID_" + (i + 1).ToString() + "'     type='hidden'  value='"
                            + GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[i], "EmployeeID") + "'  /></td>");
               //有待改进计划
                  sbPublishInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '50' value='"
                            + GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[i], "Content") + "' class='tdinput' id='txtContent_" + (i + 1).ToString() + "'></td>");
                  // 完成目标 
                  sbPublishInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '50' value='"
                          + GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[i], "CompleteAim") + "' class='tdinput' id='txtCompleteAim_" + (i + 1).ToString() + "'></td>");
                  //完成期限
                sbPublishInfo.AppendLine("<td class='tdColInputCenter'><input type='text' maxlength = '10'  value='"
                            + GetSafeData.GetStringFromDateTime(dtBaseInfo.Rows[i], "CompleteDate", "yyyy-MM-dd")
                            + "' class='tdinput' id='txtCompleteDate_" + (i + 1).ToString() + "' readonly='readonly' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCompleteDate_" + (i + 1).ToString() + "')})\"></td>");
                //核查人
                sbPublishInfo.AppendLine("<td class='tdColInput'><input  id='UsertxtChecker1" + (i + 1).ToString() + "'   maxlength='50'  type='text'  value='"
                    + GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[i], "CheckerName") + "'  class='tdinput' onclick=alertdiv('UsertxtChecker1" + (i + 1).ToString() + ",txtChecker_" + (i + 1).ToString() + "') />" + "<input  id='txtChecker_" + (i + 1).ToString() + "'     type='hidden'  value='"
                            + GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[i], "Checker") + "'  /></td>");
                //核查结果
                sbPublishInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '50' value='"
                        + GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[i], "CheckResult") + "' class='tdinput' id='txtCheckResult_" + (i + 1).ToString() + "'></td>");
                //核查时间
                sbPublishInfo.AppendLine("<td class='tdColInputCenter'><input type='text' maxlength = '10'  value='"
                     + GetSafeData.GetStringFromDateTime(dtBaseInfo.Rows[i], "CheckDate", "yyyy-MM-dd")
                     + "' class='tdinput' id='txtCheckDate_" + (i + 1).ToString() + "' readonly='readonly' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCheckDate_" + (i + 1).ToString() + "')})\"></td>");
                //备注
                sbPublishInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '50' value='"
                          + GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[i], "Content") + "' class='tdinput' id='txtRemark_" + (i + 1).ToString() + "'></td>");
                //插入行结束标识
                sbPublishInfo.AppendLine("</tr>");
            }
        }
  
        //信息发布设置到DIV中表示
        //divRectPublishDetail.InnerHtml = CreatePublishTable() + sbPublishInfo.ToString() + EndTable();
        divRectPublishDetail.InnerHtml =  sbPublishInfo.ToString() ;
    }
    private string CreatePublishTable()
    {
        //定义表格变量
        StringBuilder table = new StringBuilder();
        //生成表格标题
        table.AppendLine("<table  width='100%' border='0' id='tblRectPublishDetailInfo'  align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        table.AppendLine("<tr>");
        table.AppendLine("<td class='ListTitle' width='50'>");
        table.AppendLine("选择<input type='checkbox' name='tblRectPublishDetailInfo_chkAll' id='tblRectPublishDetailInfo_chkAll' onclick='SelectAll(\"tblRectPublishDetailInfo\");'/>");
        table.AppendLine("</td>");
        table.AppendLine("<td class='ListTitle'>发布媒体和渠道</td>");
        table.AppendLine("<td class='ListTitle'>发布时间</td>");
        table.AppendLine("<td class='ListTitle'>有效时间(天)</td>");
        table.AppendLine("<td class='ListTitle'>截止时间</td>");
        table.AppendLine("<td class='ListTitle'>费用</td>");
        table.AppendLine("<td class='ListTitle'>效果</td>");
        table.AppendLine("<td class='ListTitle'>发布状态</td>");
        table.AppendLine("</tr>");

        //返回表格语句
        return table.ToString();
    }
    private string EndTable()
    {
        return "</table>";
    }
}
