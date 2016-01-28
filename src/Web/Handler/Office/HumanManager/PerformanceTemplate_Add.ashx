<%@ WebHandler Language="C#" Class="PerformanceTemplate_Add" %>

/**********************************************
 * 作    者： 王保军
 * 创建日期： 2009/04/21
 * 描    述： 考核类型设置
 * 修改日期： 2009/04/23
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using XBase.Common;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;

public class PerformanceTemplate_Add : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取请求动作
        string action = context.Request.Params["action"];
        //保存评分项目信息  
        //初期化评分项目树时
        if ("GetPerformanceTypeInfo".Equals(action))
        {
            GetPerformanceTypeInfo(context);
        }
        else if ("saveInfro".Equals(action))
        {
            string  editFlag=context.Request.QueryString["editFlag"];
            if (editFlag.Equals("UPDATE"))
            {
                UpdatePerformanceTemplate(context);
            }
            if (editFlag.Equals("INSERT"))
            {
              savePerformanceTemplate(context);
            }
           
        }
        else if ("SearchFlowInfo".Equals(action))
        {
            SearchFlowData(context); 
        }
        else if ("GetPerformanceElemInf".Equals(action))
        {
            string templateNo = context.Request.QueryString["templateNo"];
            //获取考核类型信息

            PerformanceTemplateModel model = new PerformanceTemplateModel();
            model.TemplateNo = templateNo;
            DataTable dtDeptInfo = PerformanceTemplateBus.GetPerformanceElemInfo(model);

            //定义放回变量
            StringBuilder sbReturn = new StringBuilder();
            sbReturn.Append("{");
            sbReturn.Append("data:");
            if (dtDeptInfo.Rows.Count > 0)
            {
                sbReturn.Append(JsonClass.DataTable2Json(dtDeptInfo));
            }
            else
            {
                sbReturn.Append("[]");
            }
            sbReturn.Append("}");
            context.Response.ContentType = "text/plain";
            //返回数据
            context.Response.Write(sbReturn.ToString());
            context.Response.End();

        }
        else if ("DeleteInfo".Equals(action))
        {
            //获取要素ID
            string elemID = context.Request.QueryString["DeleteNO"];
            //替换引号
            elemID = elemID.Replace("'", "");
            string[] elemIDlist = elemID.Split(',');
            bool isSucc = false;
            bool signIsUsed = false;
            for (int i = 0; i < elemIDlist.Length; i++)
            {
                bool isUsed = false;
                //判断要素是否被使用
                if (context.Request.QueryString["sign"] == "0")
                {
                      isUsed = PerformanceTemplateBus.IsTemplateUsed(elemIDlist[i]);
                }
                //已经被使用
                if (isUsed)
                {
                    //输出响应 返回不执行删除
                 
                    signIsUsed = true;
                    break;
                }
                else
                {
                    //删除要素
                      isSucc = PerformanceTemplateBus.DeletePerTemplateInfo(elemIDlist[i]);
                    //删除成功
                    if (isSucc)
                    {
                        isSucc = true;
                        continue;
                        //输出响应
                   
                    }
                    //删除失败
                    else
                    {
                        isSucc = false;
                        break;
                        //输出响应
                        
                    }
                }
            }
            if (signIsUsed)
            {
                context.Response.Write(new JsonClass("", "", 2));
                return;
            }
            else
            {
                if (isSucc)
                {
                    context.Response.Write(new JsonClass("", "", 1));
                }
                else
                {
                    context.Response.Write(new JsonClass("", "", 0));
                }
            } 
         }
    }
    private void SearchFlowData(HttpContext context)
    {
        //从请求中获取排序列
        string orderString = context.Request.QueryString["OrderBy"];

        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "TemplateNo";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        int pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(context.Request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;

        string TypeID = context.Request.QueryString["TypeID"];

        string Title = context.Request.QueryString["Title"];


        //获取数据
        PerformanceTemplateModel searchModel = new PerformanceTemplateModel();
        searchModel.TypeID  = TypeID;
        searchModel.Title  = Title;
        //设置查询条件
        //要素名称
        /// searchModel.ElemName = context.Request.QueryString["ElemName"];
        //启用状态
        /// searchModel.UsedStatus = context.Request.QueryString["UsedStatus"];
 
        //查询数据
        DataTable dtData = PerformanceTemplateBus.SearchFlowInfo (searchModel);

        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new PerformanceTemplateModel ()
             {
                 ID = x.Element("ID") == null ? "" : x.Element("ID").Value,//ID
                 TemplateNo = x.Element("TemplateNo") == null ? "" : x.Element("TemplateNo").Value,//类型名称
                 Title = x.Element("Title") == null ? "" : x.Element("Title").Value,//类型名称 
                 TypeName = x.Element("TypeName") == null ? "" : x.Element("TypeName").Value,//类型名称
                 CreateDate = x.Element("CreateDate") == null ? "" : x.Element("CreateDate").Value,//类型名称
                 CreaterName = x.Element("CreaterName") == null ? "" : x.Element("CreaterName").Value,//类型名称,
                 ModifiedDate = x.Element("ModifiedDate") == null ? "" : x.Element("ModifiedDate").Value//类型名称
                 
                  

             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PerformanceTemplateModel()
             {
                 ID = x.Element("ID") == null ? "" : x.Element("ID").Value,//ID
                 TemplateNo = x.Element("TemplateNo") == null ? "" : x.Element("TemplateNo").Value,//类型名称
                 Title = x.Element("Title") == null ? "" : x.Element("Title").Value,//类型名称 
                 TypeName = x.Element("TypeName") == null ? "" : x.Element("TypeName").Value,//类型名称
                 CreateDate = x.Element("CreateDate") == null ? "" : x.Element("CreateDate").Value,//类型名称
                 CreaterName = x.Element("CreaterName") == null ? "" : x.Element("CreaterName").Value,//类型名称,
                 ModifiedDate = x.Element("ModifiedDate") == null ? "" : x.Element("ModifiedDate").Value//类型名称
             });
        //获取记录总数
        int totalCount = dsLinq.Count();
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //设置记录总数
        sbReturn.Append("{");
        sbReturn.Append("totalCount:");
        sbReturn.Append(totalCount.ToString());
        //设置数据
        sbReturn.Append(",data:");
        sbReturn.Append(PageListCommon.ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }
    private void savePerformanceTemplate(HttpContext context)
    {
        string messageList = context.Request.Params["message"];
        string templateNo = context.Request.Params["templateNo"];
        string usedStatus = context.Request.Params["usedStatus"];
        string remark=context.Request.Params["remark"];
        string title = context.Request.Params["title"];
        string performanceType = context.Request.Params["performanceType"];
        if (string.IsNullOrEmpty(templateNo.Trim().Replace("undefined","")))
        {
            //获取编码规则编号
            string codeRuleID = context.Request.QueryString["CodeRuleID"];
            //通过编码规则代码获取人员编码
            templateNo = ItemCodingRuleBus.GetCodeValue(codeRuleID);
        }
        
        if (PerformanceTemplateBus.IsExist(templateNo))
        {
            context.Response.Write(new JsonClass("", "", 2));
            return;
        }
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
       IList <PerformanceTemplateElemModel  > modelList=new List <PerformanceTemplateElemModel >();
        string[] message = messageList.Split(',');
        int count=message .Length ;
        for (int i = 0; i < count; i++)
        {
            string [] performance=message [i].Split ('_');
            PerformanceTemplateElemModel model = new PerformanceTemplateElemModel();
            model.CompanyCD = userInfo.CompanyCD;
            model.EditFlag = "INSERT";
            model.ElemID = performance[0].ToString();
            model.ElemOrder = (i+1).ToString ();
            model.Rate = Convert.ToDecimal(performance[1].ToString());
            model.TemplateNo = templateNo;
            model.ModifiedUserID = userInfo.EmployeeID.ToString();
            modelList.Add(model );
            
        }
        PerformanceTemplateModel templateModel = new PerformanceTemplateModel();
        templateModel.CompanyCD = userInfo.CompanyCD;
        templateModel.Creator = userInfo.EmployeeID.ToString();
        templateModel.Description = remark;
        templateModel.EditFlag = "INSERT";
        templateModel.TemplateNo = templateNo;
        templateModel.Title = title;
        templateModel.UsedStatus = usedStatus;
        templateModel.TypeID = performanceType;
        
                
        
        if (PerformanceTemplateBus .InsertPerformenceTemplate (modelList ,templateModel ))
        {

            context.Response.Write(new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, templateNo , 1));
            
        }else
       {

           context.Response.Write(new JsonClass("", "", 0));
            
        }  
        
    }
    private void UpdatePerformanceTemplate(HttpContext context)
    {
        string messageList = context.Request.Params["message"];
        string templateNo = context.Request.Params["templateNo"];
        string usedStatus = context.Request.Params["usedStatus"];
        string remark=context.Request.Params["remark"];
        string title = context.Request.Params["title"];
        string performanceType = context.Request.Params["performanceType"];
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
       IList <PerformanceTemplateElemModel  > modelList=new List <PerformanceTemplateElemModel >();
        string[] message = messageList.Split(',');
        int count=message .Length ;
        for (int i = 0; i < count; i++)
        {
            string [] performance=message [i].Split ('_');
            PerformanceTemplateElemModel model = new PerformanceTemplateElemModel();
            model.CompanyCD = userInfo.CompanyCD;
            model.EditFlag = "UPDATE";
            model.ElemID = performance[0].ToString();
            model.ElemOrder = (i+1).ToString ();
            model.Rate = Convert.ToDecimal(performance[1].ToString());
            model.TemplateNo = templateNo;
            model.ModifiedUserID = userInfo.EmployeeID.ToString();
            modelList.Add(model );
            
        }
        PerformanceTemplateModel templateModel = new PerformanceTemplateModel();
        templateModel.CompanyCD = userInfo.CompanyCD;
        templateModel.Creator = userInfo.EmployeeID.ToString();
        templateModel.Description = remark;
        templateModel.EditFlag = "UPDATE";
        templateModel.TemplateNo = templateNo;
        templateModel.Title = title;
        templateModel.UsedStatus = usedStatus;
        templateModel.TypeID = performanceType;
        templateModel.ModifiedUserID = userInfo.EmployeeID.ToString();
        
                
        
        if (PerformanceTemplateBus .InsertPerformenceTemplate (modelList ,templateModel ))
        {

            context.Response.Write(new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, templateNo, 1));

        }
        else
        {

            context.Response.Write(new JsonClass("", "", 0));

        }  
        
    }
    
    
    
    
    
    
    private void GetPerformanceTypeInfo(HttpContext context)
    {
        //获取要素ID

        //获取考核类型信息
        DataTable dtDeptInfo = PerformanceTemplateBus.GetPerformanceType();

        //定义放回变量
        StringBuilder sbReturn = new StringBuilder();
        sbReturn.Append("{");
        sbReturn.Append("data:");
        if (dtDeptInfo.Rows.Count > 0)
        {
            sbReturn.Append(JsonClass.DataTable2Json(dtDeptInfo));
        }
        else
        {
            sbReturn.Append("[]");
        }
        sbReturn.Append("}");
        context.Response.ContentType = "text/plain";
        //返回数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();



    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}