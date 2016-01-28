<%@ WebHandler Language="C#" Class="ProductExtList" %>

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
using XBase.Business.Common;

public class ProductExtList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
 public void ProcessRequest(HttpContext context)
    {
        bool isSucc = false;//是否更新成功       

        string strMsg = string.Empty;//操作返回的信息  
      
        string action = context.Request.Params["action"].ToString().Trim();	//动作

        //状态为insert时才计算编号
        if (action == "insert")
        {
            JsonClass JC;
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("TableExtFields"
                         , "EFDesc", context.Request.Params["EFDesc"].ToString().Trim());
            //存在的场合
            if (!isAlready)
            {
                //JC = new JsonClass("描述已经存在，不允许重复！", "", 0);
                JC = new JsonClass(0, "", "", "描述已经存在，不允许重复！", 1);
                context.Response.Write(JC);
                return;
            }
            isSucc = ProductExtListBus.Add(GetModel(context), out strMsg);
        }
        else if (action == "update")//保存后修改
        {
            JsonClass JC;
            string HdEFDesc = context.Request.Params["HdEFDesc"].ToString().Trim();
            if (HdEFDesc != context.Request.Params["EFDesc"].ToString().Trim())
            {
                bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("TableExtFields"
                      , "EFDesc", context.Request.Params["EFDesc"].ToString().Trim());
                //存在的场合
                if (!isAlready)
                {
                    //JC = new JsonClass("描述已经存在，不允许重复！", "", 0);
                    JC = new JsonClass(0, "", "", "描述已经存在，不允许重复！", 1);
                    context.Response.Write(JC);
                    return;
                }  
            }
            isSucc = ProductExtListBus.Update(GetModel(context), out strMsg);
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
        else if (action == "all")//获取列表
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码 
            DataTable dt = ProductExtListBus.GetAllList(CompanyCD);

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
        JsonClass jc;
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
        JsonClass JC;
        if (ProductExtListBus.Delete(CompanyCD,orderNos, out strMsg, out strFieldText))
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
        string EFType = context.Request.Params["EFType"].ToString().Trim();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码 
        int totalCount = 0;
        string ord = orderBy + " " + order;

        DataTable dt = ProductExtListBus.Getlist(CompanyCD, EFDesc, EFType, pageIndex, pageCount, ord, ref totalCount);

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
        tableExtFields.EFDesc = context.Request.Params["EFDesc"].ToString().Trim();//字段描述
        tableExtFields.EFType = context.Request.Params["EFType"].ToString().Trim();//字段类型
        tableExtFields.EFValueList = context.Request.Params["EFValueList"].ToString().Trim();//Select控件对应 的可选值列表（逗号分隔）
        tableExtFields.TabName = "officedba.TableExtFields";//表名
        tableExtFields.ID = Convert.ToInt32( context.Request.Params["ID"].ToString().Trim());//字段描述
        tableExtFields.EFIndex = Convert.ToInt32(context.Request.Params["EFIndex"].ToString().Trim());//字段描述
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