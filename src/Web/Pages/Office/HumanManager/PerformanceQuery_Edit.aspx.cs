using System;
using XBase.Common;
using System.Data;
using System.Collections;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Business.Common;

public partial class Pages_Office_HumanManager_PerformanceQuery_Edit : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["TaskNo"] != null && Request.QueryString["EmployeeId"] != null && Request.QueryString["TemplateNo"] != null)
            {
                hidElemID.Value = Request.QueryString["TaskNo"].ToString();
                hidEmployeId.Value = Request.QueryString["EmployeeId"].ToString();
                hidTemplateNo.Value = Request.QueryString["TemplateNo"].ToString();
                string sign = Request.QueryString["signDate"].ToString();
                if (string.IsNullOrEmpty(sign.Trim()))
                {
                    hidSign.Value = "1";
                }
                else
                {
                    hidSign.Value = "0";
                }
                GetScoreHistroy(Request.QueryString["TaskNo"].ToString(), Request.QueryString["TemplateNo"].ToString(), Request.QueryString["EmployeeId"].ToString());
            }

           

        }
    }
    protected void  GetScoreHistroy(string taskNo, string templateNo, string employeeID)
    {
        DataTable dtStepNo = PerformanceQueryBus.SearchSummaryInfo(taskNo, templateNo, employeeID);
        int totalLen = dtStepNo.Rows.Count;
       string[] temp = new String[totalLen];
       for (int i = 0; i < dtStepNo.Rows.Count; i++)
       {
           temp[i] = dtStepNo.Rows[i]["StepNo"] == null ? "" : dtStepNo.Rows[i]["StepNo"].ToString();
       }
        string[] ElemIDList = GetString(temp);
        StringBuilder sbPublishInfo = new StringBuilder();
        for (int g=0;g<ElemIDList .Length ;g++)
        {
            sbPublishInfo.AppendLine("  <tr ><td colspan='5'> &nbsp;</td></tr>");
             DataTable dtDeptInfo = GetNewDataTable(dtStepNo, "StepNo='" + ElemIDList[g] + "'");
             if (dtDeptInfo.Rows.Count > 0)
             {
                 
                 sbPublishInfo.AppendLine("<tr class='Blue' ><td>步骤名称:"
                                     + GetSafeData.ValidateDataRow_String(dtDeptInfo.Rows[0], "StepName") + "&nbsp;&nbsp;&nbsp;评分时间:"
                                     + GetSafeData.ValidateDataRow_String(dtDeptInfo.Rows[0], "ScoreDate") + "</td><td>流程权重(%):" + GetSafeData.ValidateDataRow_String(dtDeptInfo.Rows[0], "Rate") + "</td><td class='tdColInput' colspan='2'>考评人:" + GetSafeData.ValidateDataRow_String(dtDeptInfo.Rows[0], "ScoreEmployeeName") + "</td></tr>");
                 sbPublishInfo.AppendLine("<tr><td colspan='5'>评语:"
                                    + GetSafeData.ValidateDataRow_String(dtDeptInfo.Rows[0], "AdviceNote") + "</td></tr>");
                 sbPublishInfo.AppendLine("<tr><td colspan='5'>注意事项:"
                                  + GetSafeData.ValidateDataRow_String(dtDeptInfo.Rows[0], "Note") + "</td></tr>");
             }
             else
             {
                 continue;
             }
              sbPublishInfo.AppendLine("  <tr ><th   class='style2'  colspan='2'>指标</th> <th>考评分数</th><th>指标权重(%)</th></tr>");
           


             DataTable parentNullist = GetNewDataTable(dtDeptInfo, "ParentName is Null");
             for (int c = 0; c < parentNullist.Rows.Count; c++)
            {
                    sbPublishInfo.AppendLine("<tr>");
                    //选择框
                    //指标名称
                    sbPublishInfo.AppendLine("<td class='tdColInput' colspan='2'>" + GetSafeData.ValidateDataRow_String(parentNullist.Rows[c], "ElemName") + "</td>");
                    //步骤名称
                    //sbPublishInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '50' value='"
                    //          + GetSafeData.ValidateDataRow_String(parentNullist.Rows[c], "StepName") + "' class='tdinput' ></td>");
                    //评分人
                    //sbPublishInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '50' value='"
                    //          + GetSafeData.ValidateDataRow_String(parentNullist.Rows[c], "ScoreEmployeeName") + "' class='tdinput' ></td>");
                    // 分数
                    sbPublishInfo.AppendLine("<td class='tdColInput'>" + GetSafeData.ValidateDataRow_String(parentNullist.Rows[c], "Score") + "</td>");
                    // 权重
                    sbPublishInfo.AppendLine("<td class='tdColInput'>" + GetSafeData.ValidateDataRow_String(parentNullist.Rows[c], "ElemRate") + "</td>");
                    //插入行结束标识
                    sbPublishInfo.AppendLine("</tr>");
               }


                DataTable parentNOTNullIst = GetNewDataTable(dtDeptInfo, "ParentName is not Null");
                int parentNOTNullIstLen = parentNOTNullIst.Rows.Count;
                string[] tempNotNull = new String[parentNOTNullIstLen];
                for (int i = 0; i < parentNOTNullIst.Rows.Count; i++)
                {
                    tempNotNull[i] = parentNOTNullIst.Rows[i]["ParentName"] == null ? "" : parentNOTNullIst.Rows[i]["ParentName"].ToString();
                }
                string[] parentList = GetString(tempNotNull);
                for (int e = 0; e < parentList.Length; e++)
                {
                    DataTable tem = GetNewDataTable(parentNOTNullIst, "ParentName='" + parentList[e] + "'");
                    int count= tem.Rows.Count;
                    sbPublishInfo.AppendLine("<tr>");
                     //父指标名称
                    sbPublishInfo.AppendLine("<td class='tdColInput' rowspan='"+count +"'>" + GetSafeData.ValidateDataRow_String(tem.Rows[0], "ParentName") + "</td>");
                    //指标名称
                    sbPublishInfo.AppendLine("<td class='tdColInput'>"+ GetSafeData.ValidateDataRow_String(tem.Rows[0], "ElemName") + "</td>");
                    //步骤名称
                    //sbPublishInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '50' value='"
                    //          + GetSafeData.ValidateDataRow_String(tem.Rows[0], "StepName") + "' class='tdinput' ></td>");
                    //评分人
                    //sbPublishInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '50' value='"
                    //          + GetSafeData.ValidateDataRow_String(tem.Rows[0], "ScoreEmployeeName") + "' class='tdinput' ></td>");
                    // 分数
                    sbPublishInfo.AppendLine("<td class='tdColInput'>"+ GetSafeData.ValidateDataRow_String(tem.Rows[0], "Score") + "</td>");
                    // 权重
                    sbPublishInfo.AppendLine("<td class='tdColInput'>" + GetSafeData.ValidateDataRow_String(tem.Rows[0], "ElemRate") + "</td>");
                    //插入行结束标识
                    sbPublishInfo.AppendLine("</tr>");
                    for (int a = 1; a < tem.Rows.Count; a++)
                    {
                        sbPublishInfo.AppendLine("<tr>");
                        //指标名称
                        sbPublishInfo.AppendLine("<td class='tdColInput'>" + GetSafeData.ValidateDataRow_String(tem.Rows[a], "ElemName") + "</td>");
                        //步骤名称
                        //sbPublishInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '50' value='"
                        //          + GetSafeData.ValidateDataRow_String(tem.Rows[a], "StepName") + "' class='tdinput' ></td>");
                        //评分人
                        //sbPublishInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '50' value='"
                        //          + GetSafeData.ValidateDataRow_String(tem.Rows[a], "ScoreEmployeeName") + "' class='tdinput' ></td>");
                        // 分数
                        sbPublishInfo.AppendLine("<td class='tdColInput'>" + GetSafeData.ValidateDataRow_String(tem.Rows[a], "Score") + "</td>");
                        // 权重
                        sbPublishInfo.AppendLine("<td class='tdColInput'>" + GetSafeData.ValidateDataRow_String(tem.Rows[a], "ElemRate") + "</td>");
                        //插入行结束标识
                        sbPublishInfo.AppendLine("</tr>");
                    }
                }
            }
        divRectPublishDetail.InnerHtml = sbPublishInfo.ToString();
      


    }
    public static string[] GetString(string[] myData)
    {
        if (myData.Length > 0)
        {
            Array.Sort(myData); //先对数组进行排序
            int size = 1;  //定义删除重复项后的数组长度 
            for (int i = 1; i < myData.Length; i++)
                if (myData[i] != myData[i - 1])
                    size++;
            String[] myTempData = new String[size];
            int j = 0;

            myTempData[j++] = myData[0];

            for (int i = 1; i < myData.Length; i++) //遍历数组成员 
                if (myData[i] != myData[i - 1])  //如果相邻的两个不相等则放入新数组
                    myTempData[j++] = myData[i];

            return myTempData;
        }
        return myData;

    }
    private DataTable GetNewDataTable(DataTable dt, string condition)
    {
        DataTable newdt = new DataTable();
        newdt = dt.Clone();
        DataRow[] dr = dt.Select(condition);
        for (int i = 0; i < dr.Length; i++)
        {
            newdt.ImportRow((DataRow)dr[i]);
        }
        return newdt;//返回的查询结果
    }
}
