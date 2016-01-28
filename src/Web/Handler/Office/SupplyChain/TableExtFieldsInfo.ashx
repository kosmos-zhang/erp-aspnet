<%@ WebHandler Language="C#" Class="TableExtFieldsInfo" %>

using System;
using System.Web;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;

public class TableExtFieldsInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        bool isSucc = false;//是否更新成功       

        string strMsg = string.Empty;//操作返回的信息  
      
        string action = context.Request.Params["action"].ToString().Trim();	//动作
        JsonClass jc;

        //状态为insert时才计算编号
        if (action == "AddExtAttr")
        {
            string CustomValues = context.Request.Params["CustomValues"].ToString().Trim();	//各项自定义的值
            string InsertOrUpdate = context.Request.Params["InsertOrUpdate"].ToString().Trim();	//新增OR更新
            TableExtFields tableExtFields = new TableExtFields();
            tableExtFields=GetModel(context);
           
            if (InsertOrUpdate == "Insert")
            {
                DataTable dt1 = TableExtFieldsBus.IsHaveRepeatNo(tableExtFields.CompanyCD, tableExtFields.BranchID, tableExtFields.ModelNo);
                if (dt1.Rows.Count > 0)
                {
                    jc = new JsonClass(0, "", "", "已经存在此编号的记录！", 0);
                    context.Response.Write(jc.ToJosnString());
                    return;
                }
                DataTable dt2 = TableExtFieldsBus.IsHaveAttr(tableExtFields.CompanyCD, tableExtFields.BranchID, tableExtFields.TabName);
                if (dt2.Rows.Count > 0)
                {
                    jc = new JsonClass(0, "", "", "此表的扩展属性已经存在！", 0);
                    context.Response.Write(jc.ToJosnString());
                    return;
                }             
                isSucc = TableExtFieldsBus.Add(tableExtFields, out strMsg, CustomValues);
            }
            else
                isSucc = TableExtFieldsBus.Update(tableExtFields, out strMsg, CustomValues);
        }
        else if (action == "update")//保存后修改
        {
            string CustomValues = context.Request.Params["CustomValues"].ToString().Trim();	//各项自定义的值
            isSucc = TableExtFieldsBus.Update(GetModel(context), out strMsg, CustomValues);
        }
        else if (action == "del")//删除
        {
            DelOrder(context);
            return;
        }
        else if (action == "list")//获取列表
        {
            GetLsit(context);
            return;
        }
        else if (action == "GetDataByNo")//根据NO获取数据
        {
            string ModelNo = context.Request.Params["ModelNo"].ToString().Trim();	//模板编号
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码 
            string BranchID = "";//公司代码 
            DataTable dt = TableExtFieldsBus.GetDataByNo(CompanyCD,BranchID, ModelNo);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(dt.Rows.Count.ToString());
            sb.Append(",data:");
            if (dt.Rows.Count == 0)
                sb.Append("[{\"ID\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
            return;
        }
        else if (action == "all")//获取列表
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码 
            string TableName = context.Request.Params["TableName"].ToString().Trim();	//表名
            //try
            //{
            //    string BranchID = context.Request.Params["BranchID"].ToString().Trim();
            //}
            //catch (Exception ex) {  }	//分店ID
           // if(BranchID=="0")
                   string BranchID = "";//branchid
            
            DataTable dt = TableExtFieldsBus.GetAllList(CompanyCD,BranchID, TableName);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(dt.Rows.Count.ToString());
            sb.Append(",data:");
            if (dt.Rows.Count == 0)
                sb.Append("[{\"ID\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
            return;
        }
        
        if (isSucc)
        {
           
            jc = new JsonClass(0, "", "", strMsg, 1);
        }
        else
        {
            jc = new JsonClass(0, "", "", strMsg, 0);
        }
        context.Response.Write(jc.ToJosnString());
    }

    /// <summary>
    /// 删除单据
    /// </summary>
    private void DelOrder(HttpContext context)
    {
        string orderNos = context.Request.Params["orderNos"].ToString().Trim();
        orderNos = orderNos.Remove(orderNos.Length - 1, 1);
        string strMsg = string.Empty;
        string strFieldText = string.Empty;
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码 
        string BranchID = "";
        JsonClass JC;
        if (TableExtFieldsBus.Delete(CompanyCD,orderNos,BranchID,out strMsg, out strFieldText))
            JC = new JsonClass(0, strFieldText, "", strMsg, 1);
        else
            JC = new JsonClass(0, strFieldText, "", strMsg, 0);

        context.Response.Write(JC.ToJosnString());
    }
    
    /// <summary>
    /// 获取数据列表
    /// </summary>
    private void GetLsit(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        string EFDesc = context.Request.Params["EFDesc"].ToString().Trim();
        string ModelNo = context.Request.Params["ModelNo"].ToString().Trim();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码 

        string FunctionModule = context.Request.Params["FunctionModule"].ToString().Trim();//功能模块
        string FormType = context.Request.Params["FormType"].ToString().Trim();//单据类型
        
        string BranchID = "";//公司代码 
        int totalCount = 0;
        string ord = orderBy + " " + order;

        DataTable dt = TableExtFieldsBus.Getlist(FunctionModule,FormType,CompanyCD, BranchID, EFDesc, ModelNo, pageIndex, pageCount, ord, ref totalCount);

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

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    
    /// <summary>
    /// 获取实体
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private TableExtFields GetModel(HttpContext context)
    {
        TableExtFields tableExtFields = new TableExtFields();
        tableExtFields.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码  
        tableExtFields.BranchID = "" ;//公司代码  
        tableExtFields.ModelNo = context.Request.Params["ModelNo"].ToString().Trim();//模板编号
        tableExtFields.FunctionModule = context.Request.Params["FunctionModule"].ToString().Trim();//功能模块
        tableExtFields.EFType = "1";// context.Request.Params["EFType"].ToString().Trim();//字段类型
        tableExtFields.TabName = context.Request.Params["FormType"].ToString().Trim();// "officedba.TableExtFields";//表名
        return tableExtFields;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
