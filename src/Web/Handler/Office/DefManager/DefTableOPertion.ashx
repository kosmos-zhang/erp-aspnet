<%@ WebHandler Language="C#" Class="DefTableOPertion" %>

using System;
using System.Web;
using XBase.Model.Office.DefManager;
using XBase.Business.Office.DefManager;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using XBase.Common;

using XBase.Business.DefManager;

public class DefTableOPertion : BaseHandler, System.Web.SessionState.IRequiresSessionState
{

    protected override void ActionHandler(string action)
    {
        switch (action.ToLower()) 
        {
            case "addtable":
                AddTable();//添加记录
                break;
            case "updatetable":
                UpdateTable();//添加记录
                break;
            case "getdetailstablestruct":
                GetDetailsTableStruct();//添加记录
                break;
            case "fillpagedata":
                FillPageData();//填充时获取数据
                break;
            default:
                DefaultAction(action);
                break;
        }
    }

    private void AddTable()
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        Hashtable hs = new Hashtable();
        string[] fieldname = GetParam("Fields").Split(',');
        hs.Add("Fieds", GetParam("Fields"));
        hs.Add("TableID",GetParam("TableID"));
        hs.Add("CompanyCD", userInfo.CompanyCD);
        foreach (string col in fieldname) {
            hs.Add(col, GetParam(col));  
        } 
        hs.Add("isHasDetails", GetParam("isHasDetails"));
        if (GetParam("isHasDetails") == "1") {
            hs.Add("TableDetailsInfo", GetParam("TableDetailsInfo"));
        }
        int result = DefineBus.AddDefTable(hs);
        Output("{sta:" + result + ",info:" + result + "}");
        //if (result>0)
        //{
        //    Output("{sta:"+result+",info:" + result + "}");
        //}
        //else {
        //    Output("{sta:"+result+",info:" + result + "}");
        //}   
    }

    private void UpdateTable()
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        Hashtable hs = new Hashtable();
        string[] fieldname = GetParam("Fields").Split(',');
        hs.Add("Fieds", GetParam("Fields"));
        hs.Add("TableID", GetParam("TableID"));
        hs.Add("ID", GetParam("ID"));
        hs.Add("CompanyCD", userInfo.CompanyCD);
        foreach (string col in fieldname)
        {
            hs.Add(col, GetParam(col));
        }
        hs.Add("isHasDetails", GetParam("isHasDetails"));
        if (GetParam("isHasDetails") == "1"  )
        {
            hs.Add("TableDetailsInfo", GetParam("TableDetailsInfo"));
        }
        int result = DefineBus.UpdateDefTable(hs);

        if (result > 0)
        {
            Output("{sta:1,info:" + result + "}");
        }
        else
        {
            Output("{sta:0,info:" + result + "}");
        }   
    }


    private void GetDetailsTableStruct() {
        DataTable dt = new DataTable();
        string info = string.Empty;
        dt = DefineBus.GetDetailsTableStruct(GetParam("TableID"), ref info);
        if (dt == null)
        { 
            Output("{sta:0,data:'',info:'" + info + "'}");    
        }
        else if (dt.Rows.Count<=0)
        {
             Output("{sta:2,data:[],info:'" + info + "'}");
        }
        else {
           Output("{sta:1,data:" + JsonClass.DataTable2Json(dt) + ",info:'" + info + "'}");
        }
    }

    private void FillPageData() {
        DataSet ds = new DataSet();
        string info = string.Empty;
        ds = DefineBus.FillPageData(GetParam("TableID"), GetParam("ID"), ref info);
        if (ds != null)
        {
            string jsondata = string.Empty;
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count > 0)
                {
                    jsondata += dt.Rows[0]["TableName"] + ":" + JsonClass.DataTable2Json(dt) + ",";
                }
            }
            jsondata = jsondata.Substring(0, jsondata.Length - 1);
            Output("{sta:1,data:{" + jsondata + "},info:" + ds.Tables.Count + " } ");
        }
        else {
            Output("{sta:0,data:'',info:0 } ");
        }
    }
}