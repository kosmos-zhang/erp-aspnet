<%@ WebHandler Language="C#" Class="CreateTableReport" %>

using System;
using System.Web;
using XBase.Business.Office.DefManager;
using XBase.Model.Office.DefManager;
using System.Data;
using System.IO;
using System.Text;

public class CreateTableReport : BaseHandler
{

    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "reportlist":
                GetReportList();//获取表字段列表
                break;
            case "del":
                DelReportList();
                break;
            default:
                DefaultAction(action);
                break;
        }
    }

    private void GetReportList()
    {
        int pageindex = Convert.ToInt32(GetParam("pageIndex"));
        int pageCount = Convert.ToInt32(GetParam("pageCount"));
        string orderBy = GetParam("orderBy");
        string menuname = GetParam("menuname");
        int totalCount = 0;
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "ID";
        }
        DataTable dt = TableReportBus.GetReportList(UserInfo, menuname, pageindex, pageCount, orderBy, ref totalCount);
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");
        Output(sb.ToString());
    }

    private void DelReportList()
    {
        string idlist = GetParam("str");
        int num = TableReportBus.DelReportList(idlist);
        StringBuilder sb = new StringBuilder();
        sb.Append(num.ToString());
        Output(sb.ToString());
    }
}