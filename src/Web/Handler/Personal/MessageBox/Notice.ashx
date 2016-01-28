<%@ WebHandler Language="C#" Class="Notice" %>

using System;
using System.Web;
using System.Data;
using System.Text;

public class Notice :  BaseHandler
{
    private static XBase.Business.Personal.MessageBox.PublicNotice bll = new XBase.Business.Personal.MessageBox.PublicNotice();
    protected override void ActionHandler(string action)
    {        
        switch (action.ToLower())
        {
            case "confirmbat":
                ConfirmBat();//批量确认
                break;
            case "getitem":
                GetItem();//读取记录
                break;
            case "delitem":
                DelItem();//删除记录
                break;
            case "additem":
                AddItem();//添加记录
                break;
            case "edititem":
                EditItem();//修改记录
                break; 
            case "loaddata":
               LoadData();
                break; 
            case "desktopdataload":
                LoadDeskTopNotice();
                break;
            default:
                DefaultAction(action);
                break;
        }
    }

    private void ConfirmBat()
    {
        string idList = GetParam("ID");
        if (idList == string.Empty)
        {
            OutputResult(false, "未指定ID");
            return;
        }

        XBase.Business.Personal.MessageBox.PublicNotice bll = new XBase.Business.Personal.MessageBox.PublicNotice();
        XBase.Model.Personal.MessageBox.PublicNotice entity = new XBase.Model.Personal.MessageBox.PublicNotice();
        string[] ids = idList.Split(',');
        for (int i = 0; i < ids.Length; i++)
        {
            entity = bll.GetModel(int.Parse(ids[i]));
            if (entity == null)
                continue;
            entity.IsShow = "1";
            entity.Status = "1";
            entity.ComfirmDate = DateTime.Now;
            entity.Comfirmor = UserInfo.EmployeeID;

            bll.Update(entity);
        }

        OutputResult(true, "操作成功");
        
        
    }


    private void EditItem()
    {
        string ID = GetParam("ID");
        if (ID == string.Empty)
        {
            OutputResult(false, "未指定ID");
            return;
        }

        string IsShow = GetParam("IsShow");
        if (IsShow == string.Empty)
        {
            OutputResult(false, "未指定IsShow");
            return;
        }


        string Status = GetParam("Status");
        if (Status == string.Empty)
        {
            OutputResult(false, "未指定Status");
            return;
        }

        string title = GetParam("title");
        if (title == string.Empty)
        {
            OutputResult(false, "未指定title");
            return;
        }


        string content = GetParam("content");
        if (content == string.Empty)
        {
            OutputResult(false, "未指定content");
            return;
        }        

        XBase.Model.Personal.MessageBox.PublicNotice entity = bll.GetModel(int.Parse(ID));
        entity.IsShow = IsShow;
        entity.Status = Status;
        entity.NewsTitle = title;
        entity.NewsContent = content;

        if (entity.Status == "1")
        {
            entity.Comfirmor = UserInfo.EmployeeID;
            entity.ComfirmDate = DateTime.Now;
        }

        bll.Update(entity);

        OutputResult(true, "修改成功");
    }
    

    private void GetItem()
    {
        string id = GetParam("id");
        if (id == string.Empty)
        {
            OutputResult(false, "id未指定");
            return;
        }

        XBase.Business.Personal.MessageBox.PublicNotice bll = new XBase.Business.Personal.MessageBox.PublicNotice();
        XBase.Model.Personal.MessageBox.PublicNotice entity = bll.GetModel(int.Parse(id));

        //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

        string uname = XBase.Business.Office.HumanManager.EmployeeInfoBus.GetEmployeeInfoWithID(entity.Creator).EmployeeName;

        string uname2 = "";
        if (entity.Comfirmor != 0)
        {
            uname2 = XBase.Business.Office.HumanManager.EmployeeInfoBus.GetEmployeeInfoWithID(entity.Comfirmor).EmployeeName;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("{");

        sb.Append("ID:" + entity.ID.ToString());
        sb.Append(",NewsTitle:\"" + GetSafeJSONString(entity.NewsTitle) + "\"");
        sb.Append(",NewsContent:\"" + GetSafeJSONString(entity.NewsContent) + "\"");
        sb.Append(",Creator:\"" + uname + "\"");
        sb.Append(",CreateDate:\"" + entity.CreateDate.ToString() + "\"");
        sb.Append(",Confirmor:\"" + uname2 + "\"");
        sb.Append(",ComfirmDate:\"" + entity.ComfirmDate.ToString() + "\"");
        sb.Append(",Status:\"" + entity.Status + "\"");
        sb.Append(",IsShow:\"" + entity.IsShow + "\"");
        
        sb.Append("}");

        OutputData(sb.ToString());
    }
    

    private void DelItem()
    {
        string idList = GetParam("idList");
        if (idList == string.Empty)
        {
            OutputResult(false, "idList未指定");
            return;
        }

        XBase.Business.Personal.MessageBox.PublicNotice bll = new XBase.Business.Personal.MessageBox.PublicNotice();

        string[] ids = idList.Split(',');
        for (int i = 0; i < ids.Length; i++)
        {
            bll.Delete(int.Parse(ids[i]));
        }

        OutputResult(true, "操作成功");
    }

    private void AddItem()
    {
        string title = GetParam("title");
        if (title == string.Empty)
        {
            OutputResult(false, "未指定title");
            return;
        }

        string content = GetParam("content");
        if (content == string.Empty)
        {
            OutputResult(false, "未指定content");
            return;
        }

        string status = GetParam("status");
        string isshow = GetParam("isshow");

        XBase.Model.Personal.MessageBox.PublicNotice entity = new XBase.Model.Personal.MessageBox.PublicNotice();

        entity.CompanyCD = UserInfo.CompanyCD;
        entity.CreateDate = DateTime.Now;
        entity.Creator = UserInfo.EmployeeID;
        entity.NewsTitle = title;
        entity.NewsContent = content;
        entity.Status = status;
        entity.ComfirmDate = DateTime.Now;
        entity.Comfirmor = 0;
        entity.IsShow = isshow;        
        
        bll.Add(entity);
        
        OutputResult(true, "添加成功");
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
            orderExp = "ID DESC";

        string fields = GetParam("Fields");
        if (fields == string.Empty)
        {
            fields = "[ID]";
        }

        condition += " AND a.[CompanyCD]='" + UserInfo.CompanyCD + "'";

        DataTable dataList = new DataTable();

        int recCount = new XBase.Business.Personal.MessageBox.PublicNotice().GetPageData(out dataList, condition, fields, orderExp, int.Parse(pageIndex), int.Parse(pageSize));

        foreach (DataRow row in dataList.Rows)
        {
            string tt = row["NewsContent"].ToString();
            if (tt.Length > 16)
            {
                tt = tt.Substring(0, 16);
                row["NewsContent"] = tt;
            }
        }

        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + recCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        //OutputResult(true, sb.ToString());
        Output(sb.ToString());
    }



    private void LoadDeskTopNotice()
    {

        DataTable dataList = new DataTable();

        int recCount = new XBase.Business.Personal.MessageBox.PublicNotice().GetDeskTopPageData(out dataList, UserInfo.CompanyCD);

        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + recCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        //OutputResult(true, sb.ToString());
        Output(sb.ToString());
    }
    
    
 }