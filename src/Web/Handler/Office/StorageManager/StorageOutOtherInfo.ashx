<%@ WebHandler Language="C#" Class="StorageOutOtherInfo" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;

public class StorageOutOtherInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string companyCD = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string action = context.Request.QueryString["Act"];
        if ("getinfo".Equals(action.Trim()))//获取弹出层信息的时候
        {
            GetPRList(context);
        }
        else if ("list".Equals(action.Trim()))//根据明细ID获取明细信息
        {
            GetDetailList(context);
        }

        else if ("Close".Equals(action.Trim()))
        {
            CloseBill(context);
        }
        else if ("CancelClose".Equals(action.Trim()))//取消结单
        {
            CancelCloseBill(context);
        }
        else
        {
            if (context.Request.RequestType == "POST")
            {
                string ID = context.Request.QueryString["ID"].ToString();
                StorageOutOtherModel model = new StorageOutOtherModel();
                model.CompanyCD = companyCD;
                model.ID = ID;
                DataTable DT = StorageOutOtherBus.GetStorageOutOtherDetailInfo(model);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("data:");
                sb.Append(JsonClass.DataTable2Json(DT));
                sb.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
            }
        }

    }

    private void GetDetailList(HttpContext context)
    {
        string strDetailIDList = string.Empty;
        strDetailIDList = (context.Request.QueryString["DetailIDList"].ToString());//明细信息的ID组合字符串
        string strJson = string.Empty;
        string strPRListJson = string.Empty;//采购单单明细信息
        strJson = "{";
        if (StorageOutOtherBus.GetInfoByDetalIDList(strDetailIDList, companyCD).Rows.Count > 0)
        {
            strPRListJson += "\"PRList\":" + JsonClass.DataTable2Json(StorageOutOtherBus.GetInfoByDetalIDList(strDetailIDList, companyCD));
            strJson += strPRListJson;
        }
        strJson += "}";
        context.Response.Write(strJson);
    }

    public void GetPRList(HttpContext context)
    {
        string orderString = (context.Request.QueryString["orderByOff"].ToString());//排序
        string order = "descending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "RejectNo";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ascending";//排序：降序
        }
        int pageCount = int.Parse(context.Request.QueryString["pageOffCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
        string txtNo = context.Request.QueryString["txtNo"].ToString();
        string txtTitle = context.Request.QueryString["txtTitle"].ToString();
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        DataTable dt = StorageOutOtherBus.GetPRDetailInfo(companyCD,txtNo,txtTitle);
        XElement dsXML = ConvertDataTableToXML(dt);
        //linq排序
        var dsLinq =
            (order == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderBy).Value ascending
             select new DataSourceModel()
             {
                 ID = x.Element("ID").Value,
                 RejectNo = x.Element("RejectNo").Value,
                 Title = x.Element("Title").Value,
                 RejectDate = x.Element("RejectDate").Value,
                 ProductName = x.Element("ProductName").Value,
                 BackCount = x.Element("BackCount").Value,
                 OutedTotal = x.Element("OutedTotal").Value,
                 DetailID = x.Element("DetailID").Value,
                 UnitPrice = x.Element("UnitPrice").Value,
                 CodeName = x.Element("CodeName").Value,                
                 ColorName = x.Element("ColorName").Value
             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderBy).Value descending
             select new DataSourceModel()
             {
                 ID = x.Element("ID").Value,
                 RejectNo = x.Element("RejectNo").Value,
                 Title = x.Element("Title").Value,
                 RejectDate = x.Element("RejectDate").Value,
                 ProductName = x.Element("ProductName").Value,
                 BackCount = x.Element("BackCount").Value,
                 OutedTotal = x.Element("OutedTotal").Value,
                 DetailID = x.Element("DetailID").Value,
                 UnitPrice = x.Element("UnitPrice").Value,
                 CodeName = x.Element("CodeName").Value,
                 ColorName = x.Element("ColorName").Value
             });
        int totalCount = dsLinq.Count();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    /// <summary>
    /// datatabletoxml
    /// </summary>
    /// <param name="xmlDS"></param>
    /// <returns></returns>
    private XElement ConvertDataTableToXML(DataTable xmlDS)
    {
        StringWriter sr = new StringWriter();
        xmlDS.TableName = "Data";
        xmlDS.WriteXml(sr, System.Data.XmlWriteMode.IgnoreSchema, true);
        string contents = sr.ToString();
        return XElement.Parse(contents);
    }

    public static string ToJSON(object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(obj);
    }

    //数据源结构
    public class DataSourceModel
    {
        public string ID { get; set; }
        public string RejectNo { get; set; }
        public string Title { get; set; }
        public string RejectDate { get; set; }
        public string ProductName { get; set; }
        public string BackCount { get; set; }
        public string OutedTotal { get; set; }
        public string DetailID { get; set; }
        public string UnitPrice { get; set; }
        public string CodeName { get; set; }
        public string ColorName { get; set; }
    }

    private void CloseBill(HttpContext context)
    {
        JsonClass jc = new JsonClass();

        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//[待修改] 
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string LoginUserName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;//登陆用户名
        string Date = System.DateTime.Now.ToString("yyyy-MM-dd");

        int ID = int.Parse((context.Request.QueryString["ID"].ToString()));//明细信息的ID组合字符串
        if (context.Request.QueryString["Act"].ToString().Trim() == "Close")
            if (ID > 0)
            {
                StorageOutOtherModel model = new StorageOutOtherModel();
                model.ID = ID.ToString();
                model.CompanyCD = companyCD;
                model.Closer = loginUser_id.ToString();
                model.ModifiedUserID = LoginUserID;
                if (StorageOutOtherBus.CloseBill(model))
                {
                    jc = new JsonClass("结单成功", LoginUserName + " | " + Date, 1);
                }
                else
                {
                    jc = new JsonClass("结单失败", "", 0);
                }
            }
        context.Response.Write(jc);
    }

    private void CancelCloseBill(HttpContext context)
    {
        JsonClass jc = new JsonClass();

        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//[待修改] 
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string LoginUserName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;//登陆用户名
        string Date = System.DateTime.Now.ToString("yyyy-MM-dd");

        int ID = int.Parse(context.Request.QueryString["ID"].ToString());//明细信息的ID组合字符串
        if (ID > 0)
        {
            StorageOutOtherModel model = new StorageOutOtherModel();
            model.ID = ID.ToString();
            model.CompanyCD = companyCD;
            model.Closer = loginUser_id.ToString();
            model.ModifiedUserID = LoginUserID;
            if (StorageOutOtherBus.CancelCloseBill(model))
            {
                jc = new JsonClass("取消结单成功", LoginUserName + " | " + Date, 1);
            }
            else
            {
                jc = new JsonClass("取消结单失败", "", 0);
            }
        }
        context.Response.Write(jc);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}