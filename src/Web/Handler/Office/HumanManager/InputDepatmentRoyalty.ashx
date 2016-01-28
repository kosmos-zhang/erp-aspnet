<%@ WebHandler Language="C#" Class="InputDepatmentRoyalty" %>

using System;
using System.Data;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using System.Text;
using XBase.Business.Common;
using System.Collections.Generic;
public class InputDepatmentRoyalty: IHttpHandler, System.Web.SessionState.IRequiresSessionState {
    InputDepatmentRoyaltyModel model = new InputDepatmentRoyaltyModel();
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
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "DeptID";//要排序的字段，如果为空，默认为"ID"
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
            string DeptID = context.Request.Form["DeptID"].ToString();
            string OpenDate = context.Request.Form["OpenDate"].ToString();
            string CloseDate = context.Request.Form["CloseDate"].ToString();
            DataTable dt = InputDepatmentRoyaltyBus.SearchPersonTaxInfo(DeptID, OpenDate, CloseDate, pageIndex, pageCount, ord, ref totalCount);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(totalCount.ToString());
            sb.Append(",data:");
            if (dt.Rows.Count == 0)
                sb.Append("[{\"DeptID\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append("}");
            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
        }
        catch (Exception)
        {
        }
    
    }




    private void AddCodePublicType(HttpContext context)
    {



        string DeptID = context.Request.Form["DeptID"].ToString();
        string BusinessMoney = context.Request.Form["BusinessMoney"].ToString();
        string CreateTime = context.Request.Form["CreateTime"].ToString();
        model.DeptID = DeptID;
        model.BusinessMoney = BusinessMoney;
        model.CreateTime = CreateTime;
       
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.ModifiedDate = System.DateTime.Now.ToString();
        model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        bool result = InputDepatmentRoyaltyBus.InsertInputDepatmentRoyalty(model);
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

        string DeptID = context.Request.Form["DeptID"].ToString();
        string BusinessMoney = context.Request.Form["BusinessMoney"].ToString();
        string CreateTime = context.Request.Form["CreateTime"].ToString();
        model.DeptID = DeptID;
        model.BusinessMoney = BusinessMoney;
        model.CreateTime = CreateTime;
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.ModifiedDate = System.DateTime.Now.ToString();
        model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        model.ID =context.Request.Form["ID"].ToString();
        bool result = InputDepatmentRoyaltyBus.UpdateInputDepatmentRoyalty(model);
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
        JsonClass jc;
        bool isDelete = InputDepatmentRoyaltyBus.DeleteInputDepatmentRoyalty(id);
        //删除成功时
        if (isDelete)
        {
            jc = new JsonClass("删除成功", "", 1);
        }
        else
        {
            jc = new JsonClass("删除失败", "", 0);
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