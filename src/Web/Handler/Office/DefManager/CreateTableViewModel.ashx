<%@ WebHandler Language="C#" Class="CreateTableViewModel" %>

using System;
using System.Web;
using XBase.Business.Office.DefManager;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using XBase.Model.Office.DefManager;

public class CreateTableViewModel : BaseHandler {
    
    protected override void ActionHandler(string action)
    {       
        switch (action.ToLower())
        {
            case "gettbfnamelist":
                GetTableFieldNameList();//获取表字段列表
                break;
            case "savetbmodel":
                SaveTableModel();//保存模板
                break;
            case "updatetbmodel":
                UpdateTableModel();//
                break;
            case "tablemodellist":
                GetTableViewModelList();//获取模板列表
                break;
            case "gettbmoduleinfo":
                GetTBModuleInfo();//获取模板信息
                break;
            case "getsubtbfnamelist":
                GetSubTBNameList();//获取子表名称列表
                break;
            case "gettotalflag":
                GetTotalFlag();
                break;
            case "delmodule":
                DelTableModule();//删除模板
                break;
            default:
                DefaultAction(action);
                break;
        }
    }
    
    //记录列表
    private void GetTableFieldNameList()
    {
        string tbID = GetParam("state");

        DataTable dt = CreateTableViewModelBus.GetTableFieldsList(tbID);

        if (dt != null)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("data:");
            if (dt.Rows.Count == 0)
                sb.Append("[{\"ID\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append("}");
            Output(sb.ToString());
        }  
        
    }

    //保存模板
    private void SaveTableModel()
    {
        ModuleTableModel tbModel = GetTableModel();

        string strMsg = "";
        int modelID = 0;
        modelID = CreateTableViewModelBus.SaveTableModel(tbModel, out strMsg);

        Output(strMsg + "|" + modelID.ToString());
    }

    //修改模板
    private void UpdateTableModel()
    {
        ModuleTableModel tbModel = GetTableModel();

        string strMsg = "";
        bool isSucc = false;
        int modelID = Convert.ToInt32(GetParam("moduleID").Trim());
        tbModel.ID = modelID;
        isSucc = CreateTableViewModelBus.UpdateTableModel(tbModel, out strMsg);
        if (isSucc)
        {
            strMsg = "修改成功";
        }
        else 
        {
            strMsg = "修改失败，请联系管理员！";
        }

        Output(strMsg + "|" + modelID.ToString());
    }
    
    private ModuleTableModel GetTableModel()
    {
        string ModuleContent = GetParam("ModuleContent").Length == 0 ? null : GetParam("ModuleContent");
        string TableID = GetParam("TableID").Length == 0 ? null : GetParam("TableID");
        string Moduletype = GetParam("Moduletype").Length == 0 ? null : GetParam("Moduletype");
        string UseStatus = GetParam("UseStatus").Length == 0 ? null : GetParam("UseStatus");

        ModuleTableModel model = new ModuleTableModel();
        model.ModuleContent = ModuleContent;
        model.TableID = TableID;
        model.ModuleType = Moduletype;
        model.UseStatus = UseStatus;
        model.CompanyCD = UserInfo.CompanyCD;
        
        return model;
    }

    //获取模板列表
    private void GetTableViewModelList()
    {
        //设置行为参数
        string orderString = GetParam("orderBy").Trim();//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(GetParam("pageCount").Trim());//每页显示记录数
        int pageIndex = int.Parse(GetParam("pageIndex").Trim());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        int totalCount = 0;
        string ord = orderBy + " " + order;

        string TableID = GetParam("TableID").Length == 0 ? null : GetParam("TableID");//表ID
        string ModuleType = GetParam("ModuleType").Length == 0 ? null : GetParam("ModuleType");//模板类型
        string UseStatus = GetParam("UseStatus").Length == 0 ? null : GetParam("UseStatus");//启用状态

        ModuleTableModel model = new ModuleTableModel();
        model.TableID = TableID;
        model.ModuleType = ModuleType;
        model.UseStatus = UseStatus;
        model.CompanyCD = UserInfo.CompanyCD;

        DataTable dt = CreateTableViewModelBus.GetTableViewModelList(model, pageIndex, pageCount, ord, ref totalCount);
        
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

    //获取模板信息
    private void GetTBModuleInfo()
    {
        string tbModuleID = GetParam("tbModuleID").Trim();
        DataTable dt = CreateTableViewModelBus.GetTBModuleInfo(tbModuleID);
        
        string strJson = string.Empty;
        strJson = "{";
        if (dt.Rows.Count > 0)
        {
            strJson += "\"data\":" + JsonClass.DataTable2Json(dt);
        }
        strJson += "}";
        Output(strJson);
    }

    //获取子表名称列表
    private void GetSubTBNameList()
    {
        string tbID = GetParam("tableID");

        DataTable dt = CreateTableViewModelBus.GetSubTableNameList(tbID, UserInfo.CompanyCD);

        if (dt != null)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("data:");
            if (dt.Rows.Count == 0)
                sb.Append("[{\"ID\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append("}");
            Output(sb.ToString());
        }

    }

    private void GetTotalFlag()
    {
        StringBuilder sb = new StringBuilder();
        string tablename = GetParam("tablename");
        string num = CreateTableViewModelBus.GetTableTotalFlag(UserInfo.CompanyCD, tablename);
        sb.Append(num.ToString());
        Output(sb.ToString());
    }

    //删除模板
    public void DelTableModule()
    {
        string tbIDStr = GetParam("delIDS");//要删除的模板表ID串
        tbIDStr = tbIDStr.Remove(tbIDStr.Length - 1, 1);

        string strMsg = string.Empty;
        string strFieldText = string.Empty;
        JsonClass JC;
        if (CreateTableViewModelBus.DelTableModule(tbIDStr, out strMsg, out strFieldText))
            JC = new JsonClass(0, strFieldText, "", strMsg, 1);
        else
            JC = new JsonClass(0, strFieldText, "", strMsg, 0);

        Output(JC.ToJosnString());
    }
}