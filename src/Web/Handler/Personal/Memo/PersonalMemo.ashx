<%@ WebHandler Language="C#" Class="PersonalMemo" %>

using System;
using System.Web;
using System.Data;
using System.Text;

public class PersonalMemo : BaseHandler
{
    private XBase.Business.Personal.Memo.PersonalMemo bll = new XBase.Business.Personal.Memo.PersonalMemo();
    
    protected override void ActionHandler(string action)
    {       
        switch (action.ToLower())
        {
            case "delitem":
                DelItem();//删除记录
                break;        
            case "getnote":
               // GetNote();
                break;
            case "clearnotes":
                //ClearNotes();
                break;
            case "loaddata":
                LoadData();
                break;
            case "loaddesktop":
                LoadDeskTop();
                break;
            default:
                DefaultAction(action);
                break;
        }
    }

    private void DelItem()
    {
        string idList = GetParam("idList");
        if (idList == string.Empty)
        {
            OutputResult(false, "idList未指定");
            return;
        }

        XBase.Business.Personal.Memo.PersonalMemo bll = new XBase.Business.Personal.Memo.PersonalMemo();
        string[] ids = idList.Split(',');
        for (int i = 0; i < ids.Length; i++)
        {
            bll.Delete(int.Parse(ids[i]));
        }

        OutputResult(true, "操作成功");
    }
    
    private void LoadData()
    {
        string condition = GetParam("Condition");
        if (condition == string.Empty)
            condition = "1=1";
        string pageIndex = GetParam("PageIndex");
        if (pageIndex == string.Empty)
            pageIndex = "1";
        string pageSize = GetParam("PageSize");
        if (pageSize == string.Empty)
            pageSize = "10";
        string orderExp = GetParam("OrderExp");
        if (orderExp == string.Empty)
            orderExp = "ModifiedDate DESC";

        string fields = GetParam("Fields");
        if (fields == string.Empty)
        {
            fields = "[ID]";
        }

        string ueid = UserInfo.EmployeeID.ToString();
        condition += " AND a.CompanyCD='" + UserInfo.CompanyCD + "' AND ( [Creator]=" + ueid + " OR [Memoer]=" + ueid;
        condition += " OR CHARINDEX('," + ueid + ",',','+CanViewUser+',')>0 OR CanViewUser='')";

        DataTable dataList = new DataTable();

        int recCount = new XBase.Business.Personal.Memo.PersonalMemo().GetPageData(out dataList, condition, fields, orderExp, int.Parse(pageIndex), int.Parse(pageSize));
       

        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{EmployeeID:" + ueid + ",");
        sb.Append("count:" + recCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        //OutputResult(true, sb.ToString());
        Output(sb.ToString());

    }

    private void LoadDeskTop()
    {
        string condition = "";
        if (condition == string.Empty)
        {
            condition = " a.MemoDate >='2000-1-1'    and  a.MemoDate <'2099-1-1'";
        }
           
        string pageIndex = GetParam("PageIndex");
        if (pageIndex == string.Empty)
            pageIndex = "1";
        string pageSize = GetParam("PageSize");
        if (pageSize == string.Empty)
            pageSize = "5";
        string orderExp = GetParam("OrderExp");

        orderExp = "MemoDate DESC";

        string fields = GetParam("Fields");
        if (fields == string.Empty)
        {
            fields = "[ID]";
        }

        string ueid = UserInfo.EmployeeID.ToString();
        condition += " AND a.CompanyCD='" + UserInfo.CompanyCD + "' AND ( [Memoer]=" + ueid;
        condition += " )";

        DataTable dataList = new DataTable();

        int recCount = new XBase.Business.Personal.Memo.PersonalMemo().GetPageData(out dataList, condition, fields, orderExp, int.Parse(pageIndex), int.Parse(pageSize));


        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{EmployeeID:" + ueid + ",");
        sb.Append("count:" + recCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        //OutputResult(true, sb.ToString());
        Output(sb.ToString());

    }
    
    
}