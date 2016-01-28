<%@ WebHandler Language="C#" Class="DefForm" %>

using System;
using System.Web;
using XBase.Model.Office.DefManager;
using XBase.Business.Office.DefManager;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

public class DefForm : BaseHandler
{

    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "addtable":
                AddTable();//添加记录
                break;
            case "edittable":
                EditTable();//修改记录
                break;
            case "checkdeltable":
                CheckDelTable();// 删除记录前的判断
                break;
            case "deltable":
                DelTable();//删除记录
                break;
            case "tablelist":
                TableList();//记录列表
                break;
            case "tabledetail":
                TableDetail();
                break;
            case "updatestruct":// 初始化数据
                UpdataStruct();
                break;
            case "getdic":// 获得字典表
                GetDictionary();
                break;
            case "getbinddata":// 获得绑定数据
                GetBindData();
                break;
            case "createtable":
                CreateTable();
                break;
            case "addreguler":
                AddReguler();
                break;
            case "modreguler":
                ModReguler();
                break;
            case "menucreate":
                MenuCreate();
                break;
            default:
                DefaultAction(action);
                break;
        }
    }
    private void MenuCreate()
    {
        string tableid = GetParam("tableid");
        string userlist = GetParam("userlist");
        int num = DefFormBus.CreateMenu(tableid,userlist);
        if (num == 1)
        {
            Output("菜单生成成功!");
        }
        else
        {
            Output("菜单生成失败!");
        }
    }

    private void ModReguler()
    {
        string ColumnID = GetParam("columnid");
        string Relation = GetParam("relation");
        string totalType = GetParam("totaltype");
        string TableID = GetParam("tableid");
        string[] arr = Relation.Split('=');
        if (arr.Length == 2)
        {
            if (!string.IsNullOrEmpty(arr[0]))
            {
                int i = DefFormBus.ModReguler(Relation, Convert.ToInt32(ColumnID), 0);
                if (i == 0)
                {
                    DefFormBus.AddReguler(Relation, Convert.ToInt32(ColumnID), Convert.ToInt32(TableID), 0);
                }
            }
            if (!string.IsNullOrEmpty(totalType))
            {
                int n = DefFormBus.ModReguler(totalType + arr[1], Convert.ToInt32(ColumnID), 1);
                if (n == 0)
                {
                    DefFormBus.AddReguler(totalType + arr[1], Convert.ToInt32(ColumnID), Convert.ToInt32(TableID), 1);
                }
            }
        }
        Output("yes");
    }

    private void AddReguler()
    {
        string ColumnID = GetParam("columnid");
        string TableID = GetParam("tableid");
        string Relation = GetParam("relation");
        string totalType = GetParam("totaltype");
        string[] arr = Relation.Split('=');
        if (arr.Length == 2)
        {
            if (!string.IsNullOrEmpty(arr[0]))
            {
                DefFormBus.AddReguler(Relation, Convert.ToInt32(ColumnID), Convert.ToInt32(TableID), 0);
            }
            if (!string.IsNullOrEmpty(totalType))
            {
                DefFormBus.AddReguler(totalType + arr[1], Convert.ToInt32(ColumnID), Convert.ToInt32(TableID), 1);
            }
        }
        Output("yes");
    }

    //初始化表
    private void CreateTable()
    {
        string TableID = GetParam("TableID");
        DefFormBus.InitTable(TableID, UserInfo.CompanyCD);
        Output("yes");
    }

    //添加记录
    private void AddTable()
    {
        CustomTableModel model = GetCustomTableModel();
        List<StructTable> sonModel = GetStructTableDMList();

        string strMsg = "";
        string TableID = DefFormBus.AddTable(model, sonModel, out strMsg).ToString();
        Output(strMsg + "|" + TableID.ToString());
    }

    //修改记录
    private void EditTable()
    {
        CustomTableModel model = GetCustomTableModel();
        List<StructTable> sonModel = GetStructTableDMList();
        string strMsg = "";
        DefFormBus.EditTable(model, sonModel, out strMsg).ToString();
        Output(strMsg);

    }

    /// <summary>
    /// 删除记录前的判断
    /// </summary>
    private void CheckDelTable()
    {
        string code = "0";
        string tableName = "";
        string id = GetParam("id");
        DataSet ds = DefFormBus.GetTableById(id);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["Status"].ToString() == "2")
            {// 已经创建物理表
                code = "1";
                tableName = "define." + UserInfo.CompanyCD + "_" + ds.Tables[0].Rows[0]["CustomTableName"].ToString();
                if (DefFormBus.ExistHasData(tableName))
                {// 物理表已经存在数据
                    code = "2";
                }
            }
        }
        Output(code + "," + tableName);
    }

    //删除记录
    private void DelTable()
    {
        string id = GetParam("id");
        string tableName = GetParam("tableName");
        DefFormBus.DelTable(id, tableName, UserInfo.CompanyCD);
        Output("yes");
    }

    private void TableDetail()
    {
        string TableID = GetParam("TableId");
        DataSet ds = DefFormBus.GetTableById(TableID);
        if (ds != null && ds.Tables.Count > 0)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{mainData:");
            if (ds.Tables[0].Rows.Count == 0)
                sb.Append("[{\"ID\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(ds.Tables[0]));
            sb.Append(",dataDetail:");
            if (ds.Tables[1].Rows.Count == 0)
            {
                sb.Append("[{\"ID\":\"\"}]");
            }
            else
            {
                sb.Append(JsonClass.DataTable2Json(ds.Tables[1]));
            }

            sb.Append("}");
            Output(sb.ToString());
        }
    }

    //记录列表
    private void TableList()
    {
        string orderString = GetParam("orderby");//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(GetParam("pageCount"));//每页显示记录数
        int pageIndex = int.Parse(GetParam("pageIndex"));//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        int totalCount = 0;
        string ord = orderBy + " " + order;

        string CustomTableName = GetParam("AliasTableName");

        DataTable dt = DefFormBus.GetTableList(CustomTableName, UserInfo.CompanyCD, pageIndex, pageCount, ord, ref totalCount);

        if (dt != null)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
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

    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    private void UpdataStruct()
    {
        string[] id = GetParam("id").Split(',');
        string[] isBind = GetParam("isBind").Split(',');
        string[] values = GetParam("value").Split('|');
        IList<StructTable> list = new List<StructTable>();
        StructTable info = null;
        int bind;
        for (int i = 0; i < id.Length; i++)
        {
            info = new StructTable();
            list.Add(info);
            info.id = int.Parse(id[i]);
            if (i < isBind.Length && int.TryParse(isBind[i], out bind))
            {
                info.IsBind = bind;
            }
            else
            {
                info.IsBind = null;
            }
            if (i < values.Length)
            {
                info.DropdownlistValue = values[i];
            }
            else
            {
                info.DropdownlistValue = "";
            }
        }

        if (DefFormBus.UpdateStruct(list))
        {
            Output("yes");
        }
    }

    /// <summary>
    /// 获得字典表
    /// </summary>
    private void GetDictionary()
    {
        DataTable dt = DefFormBus.GetDictionary(UserInfo.CompanyCD);
        StringBuilder sb = new StringBuilder();
        sb.Append("{data:");
        if (dt.Rows.Count == 0)
        {
            sb.Append("[{\"ID\":\"\"}]");
        }
        else
        {
            sb.Append(JsonClass.DataTable2Json(dt));
        }
        sb.Append("}");
        Output(sb.ToString());
    }

    /// <summary>
    /// 获得绑定数据
    /// </summary>
    private void GetBindData()
    {
        string[] para = GetParam("para").Split(',');
        DataTable dt = new DataTable();
        if (para.Length == 3 && para[0].Length > 0
            && (!string.IsNullOrEmpty(para[1]) || !string.IsNullOrEmpty(para[2])))
        {
            StringBuilder sqlField = new StringBuilder();
            if (!string.IsNullOrEmpty(para[1]))
            {
                sqlField.Append(para[1]).Append(",");
            }
            if (!string.IsNullOrEmpty(para[2]))
            {
                sqlField.Append(para[2]).Append(",");
            }

            dt = DefFormBus.GetBindData(String.Format("SELECT {0} FROM {1}", sqlField.ToString().TrimEnd(','), para[0]));
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(dt.Rows.Count);
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
        {
            sb.Append("[{\"ID\":\"\"}]");
        }
        else
        {
            sb.Append(JsonClass.DataTable2Json(dt));
        }
        sb.Append("}");
        Output(sb.ToString());
    }

    //获取CustomTable实体
    private CustomTableModel GetCustomTableModel()
    {
        string AliasTableName = GetParam("AliasTableName");
        string CustomTableName = GetParam("CustomTableName");
        string ParentId = GetParam("ParentId");
        string ColumnNumber = GetParam("ColumnNumber");
        string IsDic = GetParam("IsDic");
        string totalFlag = GetParam("totalFlag");
        string ID = GetParam("Id");
        CustomTableModel model = new CustomTableModel();
        if (!string.IsNullOrEmpty(ID))
        {
            model.ID = Convert.ToInt32(ID);
        }
        model.CompanyCD = UserInfo.CompanyCD;
        model.CustomTableName = CustomTableName;
        model.AliasTableName = AliasTableName;
        model.ParentId = Convert.ToInt32(ParentId);
        model.ColumnNumber = Convert.ToInt32(ColumnNumber);
        model.totalFlag = Convert.ToInt32(totalFlag);
        model.IsDic = Convert.ToInt32(IsDic);
        return model;
    }


    //获取StructTable实体
    private List<StructTable> GetStructTableDMList()
    {
        string[] strarray = null;
        string recorditems = "";
        string[] inseritems = null;
        string strfitinfo = GetParam("strfitinfo");

        StructTable DModel;
        List<StructTable> DMList = new List<StructTable>();

        strarray = strfitinfo.Split('|');
        string[] sqlarray = new string[strarray.Length];

        for (int i = 0; i < strarray.Length; i++)
        {
            StringBuilder fitsql = new StringBuilder();
            recorditems = strarray[i];
            inseritems = recorditems.Split(',');
            if (recorditems.Length != 0)
            {
                DModel = new StructTable();
                DModel.ccode = inseritems[0].ToString();
                DModel.cname = inseritems[1].ToString();
                DModel.type = inseritems[2].ToString();
                if (!string.IsNullOrEmpty(inseritems[3]))
                {
                    DModel.length = Convert.ToInt32(inseritems[3]);
                }
                else
                {
                    DModel.length = 0;
                }
                DModel.isshow = Convert.ToInt32(inseritems[4]);
                DModel.seq = Convert.ToInt32(inseritems[5]);
                DModel.isempty = Convert.ToInt32(inseritems[6]);
                DModel.ismultiline = Convert.ToInt32(inseritems[7]);
                if (inseritems[2].ToString() == "datetime")
                {
                    DModel.typeflag = 2; 
                }
                else
                {
                    DModel.typeflag = Convert.ToInt32(inseritems[8]);
                }
                DModel.IsKeyword = Convert.ToInt32(inseritems[9]);
                DModel.isSearch = Convert.ToInt32(inseritems[10]);
                DModel.IsTotal = Convert.ToInt32(inseritems[11]);
                DModel.ControlLength = inseritems[12];
                DModel.isheadshow = Convert.ToInt32(inseritems[13]);
                DMList.Add(DModel);
            }
        }
        return DMList;
    }
    
}