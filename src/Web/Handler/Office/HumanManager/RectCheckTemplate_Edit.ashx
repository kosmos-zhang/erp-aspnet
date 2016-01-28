<%@ WebHandler Language="C#" Class="RectCheckTemplate_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/16
 * 描    述： 面试模板
 * 修改日期： 2009/04/16
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using XBase.Business.Common;
using System.Data;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Text;
using System.Linq;

public class RectCheckTemplate_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState {

    public void ProcessRequest(HttpContext context)
    {
        //从请求中获取当前操作
        string action = context.Request.Params["Action"].ToString();
        //分页控件查询数据
        if ("GetElem".Equals(action))
        {
            //查询要素信息
            GetElemInfo(context);
        }
        //保存信息
        else if ("SaveTemplate".Equals(action))
        {
            //保存面试模板信息
            SaveTemplateInfo(context);
        }
    }

    /// <summary>
    /// 保存面试模板信息
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void SaveTemplateInfo(HttpContext context)
    {
        //定义储存培训考核信息的Model变量
        RectCheckTemplateModel model = EditRequstData(context.Request);
        //定义Json返回变量
        JsonClass jc;
        //编码能用时
        if (model != null)
        {
            //执行保存操作
            bool isSucce = RectCheckTemplateBus.SaveRectCheckTemplateInfo(model);
            //保存成功时
            if (isSucce)
            {
                jc = new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, model.TemplateNo, 1);
            }
            //保存未成功时
            else
            {
                jc = new JsonClass(model.EditFlag, "", 0);
            }
        }
        else
        {
            jc = new JsonClass("", "", 2);
        }
        //输出响应
        context.Response.Write(jc);
    }

    /// <summary>
    /// 从请求中获取培训信息并转换为Model模式
    /// </summary>
    /// <param name="request">客户端请求</param>
    /// <returns></returns>
    private RectCheckTemplateModel EditRequstData(HttpRequest request)
    {
        //定义Model变量
        RectCheckTemplateModel model = new RectCheckTemplateModel();
        //编辑标识
        model.EditFlag = request.Params["EditFlag"].ToString();
        //获取编号
        string templateNo = request.Params["TemplateNo"].ToString();

        //新建时
        if (ConstUtil.EDIT_FLAG_INSERT.Equals(model.EditFlag))
        {
            //编号为空时，通过编码规则编号获取编号
            if (string.IsNullOrEmpty(templateNo))
            {
                //获取编码规则编号
                string codeRuleID = request.Params["CodeRuleID"].ToString();
                //通过编码规则代码获取编号
                templateNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_RECTCHECKTEMPLATE
                                , ConstUtil.CODING_RULE_COLUMN_RECTTEMPLATE_NO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_RECTCHECKTEMPLATE
                                , ConstUtil.CODING_RULE_COLUMN_RECTTEMPLATE_NO, templateNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        //设置编号
        model.TemplateNo = templateNo;

        //主题
        model.Title = request.Params["Title"].ToString();
        //岗位
        model.QuarterID = request.Params["QuarterID"].ToString();
        //启用状态
        model.UsedStatus = request.Params["UsedStatus"].ToString();
        //备注                                                  
        model.Remark = request.Params["Remark"].ToString();

        //设置模板要素
        int elemCount = int.Parse(request.Params["ElemCount"].ToString());
        if (elemCount > 0)
        {
            //遍历招聘目标
            for (int i = 1; i <= elemCount; i++)
            {
                //定义Model变量
                RectCheckTemplateElemModel elemModel = new RectCheckTemplateElemModel();
                //要素ID
                elemModel.CheckElemID = request.Params["ElemID_" + i].ToString();
                //满分
                elemModel.MaxScore = request.Params["MaxScore_" + i].ToString();
                //权重
                elemModel.Rate = request.Params["Rate_" + i].ToString();
                //备注
                elemModel.Remark = request.Params["ElemRemark_" + i].ToString();

                //添加到列表中
                model.ElemList.Add(elemModel);
            }
        }

        return model;
    }

    /// <summary>
    /// 查询要素信息
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void GetElemInfo(HttpContext context)
    {
        HttpRequest request = context.Request;
        //从请求中获取排序列
        string orderString = request.Params["OrderBy"].ToString();
        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        int pageIndex = int.Parse(request.Params["PageIndex"].ToString());
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(request.Params["PageCount"].ToString());
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;

        //获取数据
        RectCheckElemModel searchModel = new RectCheckElemModel();
        //设置查询条件
        searchModel.UsedStatus = ConstUtil.USED_STATUS_ON;
        //查询数据
        DataTable dtElem = RectCheckElemBus.SearchRectCheckElemInfo(searchModel);
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtElem, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new RectCheckElemModel()
             {
                 ID = x.Element("ID").Value,//ID
                 ElemName = x.Element("ElemName").Value,//要素名称
                 Standard = x.Element("Standard").Value//评分标准
             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new RectCheckElemModel()
             {
                 ID = x.Element("ID").Value,//ID
                 ElemName = x.Element("ElemName").Value,//要素名称
                 Standard = x.Element("Standard").Value//评分标准
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
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}