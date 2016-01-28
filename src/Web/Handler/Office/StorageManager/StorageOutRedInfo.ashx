<%@ WebHandler Language="C#" Class="StorageOutRedInfo" %>

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
public class StorageOutRedInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string companyCD = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string action = context.Request.QueryString["Act"];
        if ("getinfo".Equals(action.Trim()))//获取弹出层信息的时候
        {
            GetOutTypeList(context);
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
                StorageOutRedModel model = new StorageOutRedModel();
                model.CompanyCD = companyCD;
                model.ID = ID;
                DataTable DT = StorageOutRedBus.GetStorageOutRedDetailInfo(model);

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
        string OutType = context.Request.QueryString["OutType"].ToString();
        string OutNo = context.Request.QueryString["OutNo"].ToString();
        string strJson = string.Empty;
        string strListJson = string.Empty;//出库单明细信息
        strJson = "{";
        if (StorageOutRedBus.GetDetailInfo(companyCD, OutType, OutNo).Rows.Count > 0)
        {
            strListJson += "\"OTList\":" + JsonClass.DataTable2Json(StorageOutRedBus.GetDetailInfo(companyCD, OutType, OutNo));
            strJson += strListJson;
        }
        strJson += "}";
        context.Response.Write(strJson);
    }

    public void GetOutTypeList(HttpContext context)
    {
        string orderString = (context.Request.QueryString["orderByOff"].ToString());//排序
        string order = "descending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "OutType";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ascending";//排序：降序
        }
        int pageCount = int.Parse(context.Request.QueryString["pageOffCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        string OutType = context.Request.QueryString["OutType"].ToString();

        string txtNo = context.Request.QueryString["txtNo"].ToString();
        string txtTitle = context.Request.QueryString["txtTitle"].ToString();
        
        DataTable dt = StorageOutRedBus.GetOutTypeInfo(companyCD, OutType,txtNo,txtTitle);
        XElement dsXML = ConvertDataTableToXML(dt);
        //linq排序
        var dsLinq =
            (order == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderBy).Value ascending
             select new DataSourceModel()
             {

                 ID = x.Element("ID").Value,
                 OutType = x.Element("OutType").Value,
                 OutNo = x.Element("OutNo").Value,
                 Title = x.Element("Title").Value,
                 CountTotal = x.Element("CountTotal").Value,
             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderBy).Value descending
             select new DataSourceModel()
             {
                 ID = x.Element("ID").Value,
                 OutType = x.Element("OutType").Value,
                 OutNo = x.Element("OutNo").Value,
                 Title = x.Element("Title").Value,
                 CountTotal = x.Element("CountTotal").Value,
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
        public string OutType { get; set; }
        public string OutNo { get; set; }
        public string Title { get; set; }
        public string CountTotal { get; set; }

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
                StorageOutRedModel model = new StorageOutRedModel();
                model.ID = ID.ToString();
                model.CompanyCD = companyCD;
                model.Closer = loginUser_id.ToString();
                model.ModifiedUserID = LoginUserID;
                if (StorageOutRedBus.CloseBill(model))
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
            StorageOutRedModel model = new StorageOutRedModel();
            model.ID = ID.ToString();
            model.CompanyCD = companyCD;
            model.Closer = loginUser_id.ToString();
            model.ModifiedUserID = LoginUserID;
            if (StorageOutRedBus.CancelCloseBill(model))
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