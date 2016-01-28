]<%@ WebHandler Language="C#" Class="CodeReasonFeeList" %>

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
public class CodeReasonFeeList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    CodeReasonFeeModel model = new CodeReasonFeeModel();
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
                    DelItem(context, id);//删除记录
                    break;
                case "Edit":
                    EditItem(context);//修改记录
                    break;
            }

        }
    }

    private void LoadData(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "ascending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CodeName";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "descending";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        //获取数据
        //UserInfoModel model = new UserInfoModel();
        string TypeFlag = context.Request.Form["TypeFlag"].ToString();
        string TypeName = context.Request.Form["TypeName"].ToString();
        string UsedStatus = context.Request.Form["UsedStatus"].ToString();
        string seltype = context.Request.Form["seltype"].ToString();
        string SubNo = "";
        try { SubNo = context.Request.Form["SubNo"].ToString(); }
        catch{}
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //string CompanyCD = "AAAAAA";
        XElement dsXML = ConvertDataTableToXML(CodeReasonFeeBus.GetThreeCodeType(CompanyCD, SubNo,TypeFlag, UsedStatus, TypeName, seltype));
        System.Collections.Generic.IEnumerable<DataSourceModel> dsLinq = new System.Collections.Generic.List<DataSourceModel>();
        //linq排序
        if (TypeFlag == "officedba.CodeReasonType" )
            dsLinq =
          (order == "ascending") ?
          (from x in dsXML.Descendants("Data")
           orderby x.Element(orderBy).Value ascending
           select new DataSourceModel()
           {
               CodeName = x.Element("CodeName").Value,
               Flag = x.Element("Flag").Value,
               Description = x.Element("Description").Value,
               ModifiedDate = x.Element("ModifiedDate").Value,
               ModifiedUserID = x.Element("ModifiedUserID").Value,
               UsedStatus = x.Element("UsedStatus").Value,
               ID = x.Element("ID").Value,
               Publicflag = x.Element("Publicflag").Value

           })
                    :
          (from x in dsXML.Descendants("Data")
           orderby x.Element(orderBy).Value descending
           select new DataSourceModel()
           {
               CodeName = x.Element("CodeName").Value,
               Flag = x.Element("Flag").Value,
               Description = x.Element("Description").Value,
               ModifiedDate = x.Element("ModifiedDate").Value,
               ModifiedUserID = x.Element("ModifiedUserID").Value,
               UsedStatus = x.Element("UsedStatus").Value,
               ID = x.Element("ID").Value,
               Publicflag = x.Element("Publicflag").Value
           });
        if (TypeFlag == "officedba.CodeFeeType")
            dsLinq =
          (order == "ascending") ?
          (from x in dsXML.Descendants("Data")
           orderby x.Element(orderBy).Value ascending
           select new DataSourceModel()
           {
               CodeName = x.Element("CodeName").Value,
               FeeSubjectsNo = x.Element("FeeSubjectsNo").Value,
               SubjectsName = x.Element("SubjectsName").Value,
               Flag = x.Element("Flag").Value,
               Description = x.Element("Description").Value,
               ModifiedDate = x.Element("ModifiedDate").Value,
               ModifiedUserID = x.Element("ModifiedUserID").Value,
               UsedStatus = x.Element("UsedStatus").Value,
               ID = x.Element("ID").Value,
               Publicflag = x.Element("Publicflag").Value

           })
                    :
          (from x in dsXML.Descendants("Data")
           orderby x.Element(orderBy).Value descending
           select new DataSourceModel()
           {
               CodeName = x.Element("CodeName").Value,
               FeeSubjectsNo = x.Element("FeeSubjectsNo").Value,
               SubjectsName = x.Element("SubjectsName").Value,
               Flag = x.Element("Flag").Value,
               Description = x.Element("Description").Value,
               ModifiedDate = x.Element("ModifiedDate").Value,
               ModifiedUserID = x.Element("ModifiedUserID").Value,
               UsedStatus = x.Element("UsedStatus").Value,
               ID = x.Element("ID").Value,
               Publicflag = x.Element("Publicflag").Value
           });
            if (TypeFlag == "officedba.CodeUnitType")
            {
                dsLinq =
           (order == "ascending") ?
           (from x in dsXML.Descendants("Data")
            orderby x.Element(orderBy).Value ascending
            select new DataSourceModel()
            {
                CodeName = x.Element("CodeName").Value,
                Flag = x.Element("Flag").Value,
                Description = x.Element("Description").Value,
                ModifiedDate = x.Element("ModifiedDate").Value,
                ModifiedUserID = x.Element("ModifiedUserID").Value,
                UsedStatus = x.Element("UsedStatus").Value,
                ID = x.Element("ID").Value,
                Publicflag = x.Element("Publicflag").Value,
                CodeSymbol = x.Element("CodeSymbol").Value

            })
                     :
           (from x in dsXML.Descendants("Data")
            orderby x.Element(orderBy).Value descending
            select new DataSourceModel()
            {
                CodeName = x.Element("CodeName").Value,
                Flag = x.Element("Flag").Value,
                Description = x.Element("Description").Value,
                ModifiedDate = x.Element("ModifiedDate").Value,
                ModifiedUserID = x.Element("ModifiedUserID").Value,
                UsedStatus = x.Element("UsedStatus").Value,
                ID = x.Element("ID").Value,
                Publicflag = x.Element("Publicflag").Value,
                CodeSymbol = x.Element("CodeSymbol").Value
            });
            }
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




    private void AddCodePublicType(HttpContext context)
    {
        string TableName = context.Request.Form["TableName"].ToString();
        string CodeName = context.Request.Form["CodeName"].ToString();
        string ReasonFlag = context.Request.Form["ReasonFlag"].ToString();
        string Description = context.Request.Form["Description"].ToString();
        string UseStatus = context.Request.Form["UseStatus"].ToString();
        string FeeSubjectsNo = "";
        try
        {
            FeeSubjectsNo = context.Request.Form["FeeSubjectsNo"].ToString();
        }catch{}//对应会计科目
        model.Flag = int.Parse(ReasonFlag);
        model.UsedStatus = UseStatus;
        model.Description = Description;
        model.CodeName = CodeName;
        model.FeeSubjectsNo = FeeSubjectsNo;
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        //model.CompanyCD = "AAAAAA";
        //model.ModifiedUserID = "Admin";
        model.ModifiedDate = System.DateTime.Now;
        //判断是否存在
           bool isAlready = CodeReasonFeeBus.CheckCodeUniq(TableName,CodeName,ReasonFlag);
            //存在的场合
            if (!isAlready)
            {
                jc = new JsonClass("属于此类的名称已存在，请重新输入", "", 0);
                context.Response.Write(jc);
                return;
            }
            else
            {
                if (TableName == "officedba.CodeUnitType")
                {
                    string CodeSymbol = context.Request.Form["CodeSymbol"].ToString();
                    model.CodeSymbol = CodeSymbol;
                }
                bool result_unit = CodeReasonFeeBus.InsertThreeCodeInfo(model, TableName);
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


    private void EditItem(HttpContext context)
    {
         string TableName = context.Request.Form["TableName"].ToString();
        string CodeName = context.Request.Form["CodeName"].ToString();
        string ReasonFlag = context.Request.Form["ReasonFlag"].ToString();
        string Name = context.Request.Form["Name"].ToString();
        string Description = context.Request.Form["Description"].ToString();
        string UseStatus = context.Request.Form["UseStatus"].ToString();
        string FeeSubjectsNo = "";
        try
        {
            FeeSubjectsNo = context.Request.Form["FeeSubjectsNo"].ToString();
        }catch{}//对应会计科目
        model.Flag = int.Parse(ReasonFlag);
        model.UsedStatus = UseStatus;
        model.Description = Description;
        model.CodeName = CodeName;
        model.FeeSubjectsNo = FeeSubjectsNo;
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string cy = "";
        if (TableName == "officedba.CodeUnitType")
        {
            cy = context.Request.Form["cy"].ToString();
        }
        model.ModifiedDate = System.DateTime.Now;
        model.ID = int.Parse(context.Request.Form["ID"].ToString());
        bool isAlready = false;
        if (Name != CodeName)
        {
            isAlready = CodeReasonFeeBus.CheckCodeUniq(TableName, CodeName, ReasonFlag);
            if (!isAlready)
            {
                jc = new JsonClass("属于此类的名称已存在，请重新输入", "", 0);
                context.Response.Write(jc);
            }
            else
            {
                if (TableName == "officedba.CodeUnitType")
                {
                    string CodeSymbol = context.Request.Form["CodeSymbol"].ToString();
                    model.CodeSymbol = CodeSymbol;
                }
                bool result = CodeReasonFeeBus.UpdateThreeCodeInfo(model, TableName);
                {
                    if (result)
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
        else
        {
            if (TableName == "officedba.CodeUnitType")
            {
                string CodeSymbol = context.Request.Form["CodeSymbol"].ToString();
                model.CodeSymbol = CodeSymbol;
            }
            bool result = CodeReasonFeeBus.UpdateThreeCodeInfo(model, TableName);
            {
                if (result)
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
    /// <summary>
    /// 删除公共分类
    /// </summary>
    private void DelItem(HttpContext context, string id)
    {
        //string companyCD = "AAAAAA";
        string TableName = context.Request.Form["TableName"].ToString();
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        JsonClass jc;
        bool isDelete = CodeReasonFeeBus.DeleteThreeCodeType(id, TableName);
        //删除成功时
        if (isDelete)
        {
            jc = new JsonClass("删除成功", "", 1);
        }
        else
        {
            jc = new JsonClass("删除失败,请检查数据是否正在使用！", "", 0);
        }
        context.Response.Write(jc);
        return;
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


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    //数据源结构
    public class DataSourceModel
    {
        public string CodeName { get; set; }
        public string FeeSubjectsNo { get; set; }
        public string SubjectsName { get; set; }
        public string Flag { get; set; }
        public string Description { get; set; }
        public string UsedStatus { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedUserID { get; set; }
        public string ID { get; set; }
        public string Publicflag { get;set; }
        public string CodeSymbol { get; set; }
    }

}