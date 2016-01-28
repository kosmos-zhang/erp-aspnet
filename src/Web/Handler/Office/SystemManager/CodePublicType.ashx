<%@ WebHandler Language="C#" Class="CodePublicType" %>

using System;
using System.Web;
using System.Data;
using System.Xml.Linq;
using XBase.Model.Office.SystemManager;
using XBase.Business.Office.SystemManager;
using System.IO;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Serialization;
using XBase.Common;
using System.Linq;
public class CodePublicType : IHttpHandler, System.Web.SessionState.IRequiresSessionState {
    CodePublicTypeModel model = new CodePublicTypeModel();
    JsonClass jc;
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string ActionFlag = context.Request.Form["ActionFlag"].ToString();
            string id = context.Request.QueryString["str"].ToString();

            switch (ActionFlag)
            {
                case "Search":
                LoadData(context);//加载数据
                break;
                case "Add":
                AddCodePublicType(context);//添加记录
                break;
                case "Del":
                DelItem(context,id);//删除记录
                break;
                case "Edit":
                EditItem(context);//修改记录
                break;
            }
              
            }
    }

    private void LoadData(HttpContext context)
    {
        try
        {
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "asc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "TypeFlag";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            int totalCount = 0;
            string ord = orderBy + " " + order;
            //获取数据
            //UserInfoModel model = new UserInfoModel();
            string TypeFlag = context.Request.Form["TypeFlag"].ToString();
            string TypeName = context.Request.Form["TypeName"].ToString();
            string UsedStatus = context.Request.Form["UsedStatus"].ToString();
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            DataTable dt = CodePublicTypeBus.GetCodePublicType(TypeFlag, TypeName, UsedStatus, CompanyCD, pageIndex, pageCount, ord, ref totalCount);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(totalCount.ToString());
            sb.Append(",data:");
            if (dt.Rows.Count == 0)
                sb.Append("[{\"TypeFlag\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append("}");
            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            //context.Response.End();
        }
        catch (Exception)
        {
        }
    
    }




    private void AddCodePublicType(HttpContext context)
    {
       
        
        
       string TypeFlag = context.Request.Form["TypeFlag"].ToString();
       string UseStatus=context.Request.Form["UseStatus"].ToString();
       string TypeCode=context.Request.Form["TypeCode"].ToString();
       string TypeName=context.Request.Form["TypeName"].ToString();
       string Description = context.Request.Form["Description"].ToString().Replace("\r\n", "");
        model .TypeFlag=int.Parse(TypeFlag);
        model.TypeName=TypeName;
        model.TypeCode=int.Parse(TypeCode);
        model.UsedStatus=UseStatus;
        model.Description=Description;
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.ModifiedDate = System.DateTime.Now;
        model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        bool result = CodePublicTypeBus.InsertCodePublicType(model);
        {
            if (result)
            {
                jc = new JsonClass("添加成功！", "", 1);
            }
            else
            {
                jc = new JsonClass("添加失败！", "", 0);
            }
            context.Response.Write(jc);
            return;
        }
    }


    private void EditItem(HttpContext context)
    {
        string TypeFlag = context.Request.Form["TypeFlag"].ToString();
        string UseStatus = context.Request.Form["UseStatus"].ToString();
        string TypeCode = context.Request.Form["TypeCode"].ToString();
        string TypeName = context.Request.Form["TypeName"].ToString();
        string Description = context.Request.Form["Description"].ToString();
        model.TypeName = TypeName;
        model.TypeCode = int.Parse(TypeCode);
        model.UsedStatus = UseStatus;
        model.Description = Description;
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.ModifiedDate = System.DateTime.Now;
        model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        model.TypeFlag =int.Parse( TypeFlag);
        model.ID = int.Parse(context.Request.Form["ID"].ToString());
        bool result = CodePublicTypeBus.UpdateCodePublicType(model);
        {
            if (result)
            {
                jc = new JsonClass("修改成功！", "", 1);
            }
            else
            {
                jc = new JsonClass("修改失败！", "", 0);
            }
            context.Response.Write(jc);
            return;
        }
    }
 
    /// <summary>
    /// 删除公共分类
    /// </summary>
    private void DelItem(HttpContext context,string id)
    {
         string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
         JsonClass jc;
         bool CanDel = XBase.Business.Office.SupplyChain.CodeReasonFeeBus.ChargeFee(id);
            //删除成功时
         if (CanDel)
         {
             jc = new JsonClass("对不起，您选择的数据有正在使用中的数据，请重新选择！", "", 1);
         }
        else
        {
            bool isDelete = CodePublicTypeBus.DeleteCodePublicType(id, companyCD);
            if (isDelete)
            {
                jc = new JsonClass("删除成功", "", 1);
            }
            else
            {
                jc = new JsonClass("删除失败", "", 0);
            }
        }
            context.Response.Write(jc);
            return;
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    ////数据源结构
    //public class DataSourceModel
    //{
    //    public string TypeFlag { get; set; }
    //    public string TypeCode { get; set; }
    //    public string TypeName { get; set; }
    //    public string UsedStatus { get; set; }
    //    public string Description { get; set; }
    //    public string ModifiedDate { get; set; }
    //    public string ModifiedUserID { get; set; }
    //    public string ID { get; set; }
    //    public string typecodeflag { get; set; }
    //}

}