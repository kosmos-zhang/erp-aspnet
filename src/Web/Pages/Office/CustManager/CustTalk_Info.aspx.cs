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
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;
using XBase.Common;

public partial class Pages_Office_CustManager_CustTalk_Info : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnImport.Attributes["onclick"] = "return IfExp();";
            hiddUserId.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        }
    }

    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string orderString = hiddExpOrder.Value.Trim();//排序
            string order = "asc";//排序：降序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CreatedDate";//要排序的字段，如果为空，默认为"ID"

            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            string ord = " ORDER BY " + orderBy + " " + order;

            CustTalkModel CustTalkM = new CustTalkModel();
            string CustID = hiddCustID.Value;

            CustTalkM.TalkType = Convert.ToInt32(ddlTalkType.Value);//类型
            CustTalkM.Priority = selePriority.Value;//优先级
            string TalkBegin = txtTalkBegin.Value;//== "" ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(context.Request.Form["TalkBegin"].ToString());//开始时间
            string TalkEnd = txtTalkEnd.Value;//== "" ? Convert.ToDateTime("9999-12-31") : Convert.ToDateTime(context.Request.Form["TalkEnd"].ToString() + " 23:59:59.000");//结束时间            
            CustTalkM.Title = txtTitle.Value;//主题
            CustTalkM.Status = seleStatus.Value;//状态

            CustTalkM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();

            DataTable dt = TalkBus.ExportTalkInfo(CanUserID,CustID, CustTalkM, TalkBegin, TalkEnd, ord);

            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "洽谈编号", "洽谈主题", "客户名称", "客户联系人", "优先级", "洽谈方式", "完成期限", "执行人", "状态", "创建人", "创建日期" },
                new string[] { "TalkNo", "title", "custnam", "linkmanname", "PriorityName", "typename", "CompleteDate", "LinkerName", "StatusName", "EmployeeName", "CreatedDate" },
                "客户洽谈列表信息");
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Exp", "<script language=javascript>showPopup('../../../Images/Pic/Close.gif','../../../Images/Pic/note.gif','导出发生异常');</script>");
        }
    }
}
