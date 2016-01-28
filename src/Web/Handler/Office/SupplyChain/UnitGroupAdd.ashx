
<%@ WebHandler Language="C#" Class="UnitGroupAdd" %>

using System;
using System.Web;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Common;

public class UnitGroupAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    /// <summary>
    /// 输出信息
    /// </summary>
    private JsonClass jc = new JsonClass();

    /// <summary>
    /// 加载时
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string ActionFlag = context.Request.Form["action"].ToString();

            switch (ActionFlag.ToLower())
            {
                case "searchedit":
                    SearchEditData(context);
                    break;
                case "searchdetail":
                    SearchDetail(context);
                    break;
                case "search":
                    SearchData(context);
                    break;
                case "save":
                    SaveData(context);//添加记录
                    break;
                case "del":
                    DeleteData(context);//删除记录
                    break;
            }
        }
    }

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="context"></param>
    private void DeleteData(HttpContext context)
    {
        IList<int> listID = new List<int>();
        IList<string> listNo = new List<string>();
        int id;
        foreach (string item in context.Request.Form["ID"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            if (int.TryParse(item, out id))
            {
                listID.Add(id);
            }
        }
        foreach (string item in context.Request.Form["GroupUnitNo"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            listNo.Add(item);
        }
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (UnitGroupBus.DeleteData(listID.ToArray(), CompanyCD, listNo.ToArray()))
        {
            jc = new JsonClass("删除成功！", "", 1);
        }
        else
        {
            jc = new JsonClass("删除失败！", "", 0);
        }
        context.Response.Write(jc);
        return;
    }

    /// <summary>
    /// 查询摘要数据
    /// </summary>
    /// <param name="context"></param>
    private void SearchEditData(HttpContext context)
    {
        int id;
        int.TryParse(context.Request.Form["ID"].ToString(), out id);
        DataTable dt = UnitGroupBus.SelectDataTable(id);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("data:");
        sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

    /// <summary>
    /// 查询查询明细数据
    /// </summary>
    /// <param name="context"></param>
    private void SearchDetail(HttpContext context)
    {
        string GroupUnitNo = context.Request.Form["GroupUnitNo"].ToString();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        DataTable dt = UnitGroupDetailBus.SelectDataTable(CompanyCD, GroupUnitNo);
        decimal dec = 0m;
        foreach (DataRow dr in dt.Rows)
        {
            if (decimal.TryParse(dr["ExRate"].ToString(), out dec))
            {
                dr["ExRate"] = dec.ToString("#0.######");
            }
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("data:");
        sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

    /// <summary>
    /// 查询数据
    /// </summary>
    /// <param name="context"></param>
    private void SearchData(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        int TotalCount;
        //获取数据
        string GroupUnitNo = context.Request.Form["GroupUnitNo"].ToString();
        string GroupUnitName = context.Request.Form["GroupUnitName"].ToString();
        int? BaseUnitID = null;
        if (int.TryParse(context.Request.Form["BaseUnitID"].ToString(), out TotalCount))
        {
            if (TotalCount > 0)
            {
                BaseUnitID = TotalCount;
            }
        }
        TotalCount = 0;
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        DataTable dt = UnitGroupBus.SelectListData(pageIndex, pageCount, orderBy + " " + order, ref TotalCount
            , CompanyCD, GroupUnitNo, GroupUnitName, BaseUnitID, "");

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(TotalCount.ToString());
        sb.Append(",data:");
        sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }



    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="context"></param>
    private void SaveData(HttpContext context)
    {
        UnitGroupModel model = new UnitGroupModel();
        int id;
        if (int.TryParse(context.Request.Form["ID"].ToString(), out id))
        {
            model.ID = id;
        }
        model.GroupUnitNo = context.Request.Form["GroupUnitNo"].ToString();
        model.GroupUnitName = context.Request.Form["GroupUnitName"].ToString();
        model.Remark = context.Request.Form["Remark"].ToString();
        model.BaseUnitID = int.Parse(context.Request.Form["BaseUnitID"].ToString());
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        //判断是否存在
        bool isAlready = UnitGroupBus.CheckUnitNo(model.CompanyCD, model.GroupUnitNo, model.ID);
        //存在的场合
        if (!isAlready)
        {
            jc = new JsonClass("计量单位组编号已存在，请重新输入", "", 0);
            context.Response.Write(jc);
            return;
        }
        else
        {
            IList<UnitGroupDetailModel> list = new List<UnitGroupDetailModel>();
            UnitGroupDetailModel uModel;
            string[] UnitID = context.Request.Form["UnitID"].ToString().Split(',');
            string[] ExRate = context.Request.Form["ExRate"].ToString().Split(',');
            string[] Remark = context.Request.Form["RemarkDetail"].ToString().Split(',');
            for (int i = 0; i < UnitID.Length; i++)
            {
                if (String.IsNullOrEmpty(UnitID[i]))
                {
                    continue;
                }
                uModel = new UnitGroupDetailModel();
                list.Add(uModel);
                uModel.CompanyCD = model.CompanyCD;
                uModel.GroupUnitNo = model.GroupUnitNo;
                uModel.UnitID = int.Parse(UnitID[i]);
                if (i < ExRate.Length)
                {
                    uModel.ExRate = decimal.Parse(ExRate[i]);
                }
                if (i < Remark.Length)
                {
                    uModel.Remark = Remark[i];
                }
            }

            bool result_unit = UnitGroupBus.SaveData(model, list);
            {
                if (result_unit)
                {
                    jc = new JsonClass("保存成功！", "", 1);
                }
                else
                {
                    jc = new JsonClass("保存失败！", "", 0);
                }
                context.Response.Write(jc);
                return;
            }
        }
    }


    //private void EditItem(HttpContext context)
    //{
    //    string TableName = context.Request.Form["TableName"].ToString();
    //    string CodeName = context.Request.Form["CodeName"].ToString();
    //    string ReasonFlag = context.Request.Form["ReasonFlag"].ToString();
    //    string Name = context.Request.Form["Name"].ToString();
    //    string Description = context.Request.Form["Description"].ToString();
    //    string UseStatus = context.Request.Form["UseStatus"].ToString();
    //    model.Flag = int.Parse(ReasonFlag);
    //    model.UsedStatus = UseStatus;
    //    model.Description = Description;
    //    model.CodeName = CodeName;
    //    model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
    //    model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
    //    string cy = "";
    //    if (TableName == "officedba.CodeUnitType")
    //    {
    //        cy = context.Request.Form["cy"].ToString();
    //    }
    //    model.ModifiedDate = System.DateTime.Now;
    //    model.ID = int.Parse(context.Request.Form["ID"].ToString());
    //    bool isAlready = false;
    //    if (Name != CodeName)
    //    {
    //        isAlready = CodeReasonFeeBus.CheckCodeUniq(TableName, CodeName, ReasonFlag);
    //        if (!isAlready)
    //        {
    //            jc = new JsonClass("属于此类的名称已存在，请重新输入", "", 0);
    //            context.Response.Write(jc);
    //        }
    //        else
    //        {
    //            if (TableName == "officedba.CodeUnitType")
    //            {
    //                string CodeSymbol = context.Request.Form["CodeSymbol"].ToString();
    //                model.CodeSymbol = CodeSymbol;
    //            }
    //            bool result = CodeReasonFeeBus.UpdateThreeCodeInfo(model, TableName);
    //            {
    //                if (result)
    //                {
    //                    jc = new JsonClass("保存成功！", "", 1);
    //                }
    //                else
    //                {
    //                    jc = new JsonClass("保存失败！", "", 0);
    //                }
    //                context.Response.Write(jc);
    //                return;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (TableName == "officedba.CodeUnitType")
    //        {
    //            string CodeSymbol = context.Request.Form["CodeSymbol"].ToString();
    //            model.CodeSymbol = CodeSymbol;
    //        }
    //        bool result = CodeReasonFeeBus.UpdateThreeCodeInfo(model, TableName);
    //        {
    //            if (result)
    //            {
    //                jc = new JsonClass("保存成功！", "", 1);
    //            }
    //            else
    //            {
    //                jc = new JsonClass("保存失败！", "", 0);
    //            }
    //            context.Response.Write(jc);
    //            return;
    //        }
    //    }

    //}
    ///// <summary>
    ///// 删除公共分类
    ///// </summary>
    //private void DelItem(HttpContext context, string id)
    //{
    //    //string companyCD = "AAAAAA";
    //    string TableName = context.Request.Form["TableName"].ToString();
    //    string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
    //    JsonClass jc;
    //    bool isDelete = CodeReasonFeeBus.DeleteThreeCodeType(id, TableName);
    //    //删除成功时
    //    if (isDelete)
    //    {
    //        jc = new JsonClass("删除成功", "", 1);
    //    }
    //    else
    //    {
    //        jc = new JsonClass("删除失败", "", 0);
    //    }
    //    context.Response.Write(jc);
    //    return;
    //}
    ///// <summary>
    ///// datatabletoxml
    ///// </summary>
    ///// <param name="xmlDS"></param>
    ///// <returns></returns>
    //private XElement ConvertDataTableToXML(DataTable xmlDS)
    //{
    //    StringWriter sr = new StringWriter();
    //    xmlDS.TableName = "Data";
    //    xmlDS.WriteXml(sr, System.Data.XmlWriteMode.IgnoreSchema, true);
    //    string contents = sr.ToString();
    //    return XElement.Parse(contents);
    //}

    //public static string ToJSON(object obj)
    //{
    //    JavaScriptSerializer serializer = new JavaScriptSerializer();
    //    return serializer.Serialize(obj);
    //}


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
    //    public string CodeName { get; set; }
    //    public string Flag { get; set; }
    //    public string Description { get; set; }
    //    public string UsedStatus { get; set; }
    //    public string ModifiedDate { get; set; }
    //    public string ModifiedUserID { get; set; }
    //    public string ID { get; set; }
    //    public string Publicflag { get; set; }
    //    public string CodeSymbol { get; set; }
    //}

}