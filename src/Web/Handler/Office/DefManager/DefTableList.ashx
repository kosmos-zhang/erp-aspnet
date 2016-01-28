<%@ WebHandler Language="C#" Class="DefTableList" %>

using System;
using System.Web;
using XBase.Model.Office.DefManager;
using XBase.Business.Office.DefManager;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

using XBase.Business.DefManager;

public class DefTableList : BaseHandler
{

    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "loaddata":
                LoadData();//添加记录
                break;
            case "del":
                DelTableList();
                break;
            default:
                DefaultAction(action);
                break;
        }
    }

    private void LoadData() 
    {
        string[] fieldname = GetParam("Fields").Split(',');
        Hashtable hs = new Hashtable();
        string TableName = string.Empty;
        int TotalCount =0;
        foreach (string name in fieldname)
        {
            if (name == "")
                continue;
            if ((name.Split('#')[1] + "") == "datetime")
            {
                hs.Add(name.Split('#')[0]+"1", GetParam(name.Split('#')[0]+"1"));
                hs.Add(name.Split('#')[0]+"2", GetParam(name.Split('#')[0]+"2"));
            }
            else {
                hs.Add(name.Split('#')[0], GetParam(name.Split('#')[0]));
            }
        }
        hs.Add("Fields", fieldname);
        hs.Add("tableid", GetParam("tableid"));
        hs.Add("PageIndex", GetParam("PageIndex"));
        hs.Add("PageCount", GetParam("PageCount"));
        hs.Add("OrderBy", GetParam("OrderBy"));
        DataTable dt = DefineBus.GetDefTableList(hs,ref TableName,ref TotalCount);
         if (dt == null)
         {
             Output("{sta:0,data:'',TotalCount:'" + TotalCount + "',TableName:'"+TableName+"'}");
         }
         else if (dt.Rows.Count <= 0)
         {
             Output("{sta:0,data:'',TotalCount:'" + TotalCount + "',TableName:'"+TableName+"'}");
         }
         else {
             Output("{sta:1,data:" + JsonClass.DataTable2Json(dt) + ",TotalCount:'" + TotalCount + "',TableName:'"+TableName+"'}");
         }
    }

    private void DelTableList()
    {
        string tableid = GetParam("tableid");
        string itemlist = GetParam("str");
        int num = DefineBus.DelTableList(tableid, itemlist);
        StringBuilder sb = new StringBuilder();
        sb.Append(num.ToString());
        Output(sb.ToString());
    }

}