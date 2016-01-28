<%@ WebHandler Language="C#" Class="PerformanceElem" %>
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

public class PerformanceElem : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取请求动作
        string action = context.Request.Params["Action"];
        //保存评分项目信息  
        string ID = context.Request.Params["DeptID"];
        //初期化评分项目树时
        if ("InitTree".Equals(action))
        {
            //定义变量
            StringBuilder sbDeptTree = new StringBuilder();
            //获取评分项目数据
            DataTable dtDeptInfo = PerformanceElemBus .SearchDeptInfo (ID);
            //评分项目数据存在时
            if (dtDeptInfo != null && dtDeptInfo.Rows.Count > 0)
            {
                //获取记录数
                int deptCount = dtDeptInfo.Rows.Count;
                //遍历所有组织机构，以显示数据
                for (int i = 0; i < dtDeptInfo.Rows.Count; i++)
                {
                    //获取评分项目ID
                    string deptID = GetSafeData.ValidateDataRow_String(dtDeptInfo.Rows[i], "ID");
                    //或评分项目名称
                    string deptName = GetSafeData.ValidateDataRow_String(dtDeptInfo.Rows[i], "ElemName");
                    //获取父评分项目
                    string superDeptID = GetSafeData.ValidateDataRow_String (dtDeptInfo.Rows[i], "ParentElemNo");
                    //获取是否有子评分项目 
                    int subCount = GetSafeData.ValidateDataRow_Int(dtDeptInfo.Rows[i], "SubCount");

                    //样式名称
                    string className = "file";
                    //Javascript事件名
                    string jsAction = "";
                    //样式表名称
                    string showClass = "list";

                    //有子结点时
                    if (subCount > 0)
                    {
                        //最后一个结点
                        if (i == deptCount - 1)
                        {
                            className = "folder_close_end";
                            showClass = "list_last";
                        }
                        else
                        {
                            className = "folder_close";
                        }
                        jsAction = "onclick=\"ShowDeptTree('"+deptID+"');\"";
                    }
                    else if (i == deptCount - 1)
                    {
                        className = "file_end";
                    }
                    string temDeptName = deptName;
                    if (temDeptName.Length > 30)
                    {
                         temDeptName = temDeptName.Substring(0,35) + "...";
                    }
                    //生成子树代码
                    sbDeptTree.Append("<table border='0' cellpadding='0' cellspacing='0'>");
                    sbDeptTree.Append("<tr><td><div id='divSuper_" + deptID + "' class='" + className + "' " + jsAction
                                    + " alt='" + deptName + "'><a  href='#' onclick=\"SetSelectValue1('"
                                    + deptID + "','" + deptName + "','" + superDeptID + "');\"><div id='divDeptName_" + deptID
                                    + "'>" + " " + temDeptName + "</div></a></div>");
                    sbDeptTree.Append("<div id='divSub_" + deptID + "' style='display:none;' class='" + showClass + "'></div></td></tr>");
                    sbDeptTree.Append("</table>");
                }
            }
            //返回生成的评分项目树 
            context.Response.Write(sbDeptTree.ToString());
        }  else  if ("EditInfo".Equals(action))
        {
            PerformanceElemModel  model = new PerformanceElemModel();
            //编辑模式
            model.EditFlag = context.Request.QueryString["EditFlag"];
            //类型ID 
            model.ID = context.Request.QueryString["ElemID"];
            //类型名称
            model.ElemName  = context.Request.QueryString["ElemName"].Replace("\r\n", "\\r\\n");
            //启用状态
            model.UsedStatus = context.Request.QueryString["UsedStatus"]; 
           //指标标准分
            model.StandardScore  = context.Request.QueryString["StandardScore"];
            //指标最小分
            model.MinScore  = context.Request.QueryString["MinScore"];
            //指标最大分
            model.MaxScore  =context.Request.QueryString["MaxScore"];
            //评分标准
            model.AsseStandard = context.Request.QueryString["AsseStandard"].Replace("\r\n", "\\r\\n");
            //评分来源
            model.AsseFrom = context.Request.QueryString["AsseFrom"].Replace("\r\n", "\\r\\n");
            //备注
            model.Remark = context.Request.QueryString["Remark"].Replace("\r\n", "\\r\\n");
            //评分细则
            model.ScoreRules = context.Request.QueryString["ScoreRules"].Replace("\r\n", "\\r\\n");
            // 指标编号
            model .ElemNo = context.Request.QueryString["ElemNo"];
            //父指标编号
           
          string   parentElemNo = context.Request.QueryString["ParentElemNo"];
          //判断节点是否为新建顶级节点，如果是，将ParentElemNo置空
          if (parentElemNo == "undefined")
          {
              model.ParentElemNo = "";
          }
          else
          {
              model.ParentElemNo = parentElemNo;
          }

          if (model.EditFlag == "INSERT")
          {
              if (string.IsNullOrEmpty(model.ElemNo))
              {
                  //获取编码规则编号
                  string codeRuleID = context.Request.QueryString["CodeRuleID"];
                  //通过编码规则代码获取人员编码
                  model.ElemNo = ItemCodingRuleBus.GetCodeValue(codeRuleID);
              }
              if (PerformanceElemBus.IsExist(model.ElemNo))
              {
                  context.Response.Write(new JsonClass("", "", 3));
                  return;
              } 
          }
          if (!(string.IsNullOrEmpty(model.ParentElemNo)) && model.EditFlag == "INSERT")
          {
              if (!PerformanceElemBus.IsHaveChild(model.ParentElemNo))
              {

                  context.Response.Write(new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, model.ID + "|" + model.ParentElemNo + "|" + model.ElemName + "|" + model.ElemNo , 2));
                  return;
              
              
              }
              }
          bool isSucc = PerformanceElemBus.SaveRectCheckElemInfo(model);
          //成功
          if (isSucc)
          {

              context.Response.Write(new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, model.ID + "|" + model.ParentElemNo + "|" + model.ElemName + "|" + model.ElemNo , 1));
          }
          //失败
          else
          {
              context.Response.Write(new JsonClass("", "", 0));


          }
            //执行保存 
        }
            
        //修改时获取数据操作 
        else if ("GetInfo".Equals(action))
        {
            //获取要素ID
            string elemID = context.Request.QueryString["ElemID"];
            //获取考核类型信息

            DataTable dtDeptInfo = PerformanceElemBus.GetRectCheckElemInfoWithID(elemID);

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
        //查询数据操作 
        else if ("SearchInfo".Equals(action))
        {
            //执行查询
            SearchData(context);
        }
        //删除操作
        else if ("DeleteDeptInfo".Equals(action))
        { 
            //判断组织是否有子评分项目
            DataTable dtSubDeptInfo = PerformanceElemBus.SearchDeptInfo(ID);
            //存在子组织机构时
            if (dtSubDeptInfo != null && dtSubDeptInfo.Rows.Count > 0)
            {
                //输出响应 返回不执行删除
                context.Response.Write(new JsonClass("", "", 2));
                return;
            }
            else if (!(PerformanceElemBus.IsHaveReferring (ID)))
            {
                context.Response.Write(new JsonClass("", "", 3));
                return;
            }
            else 
            {
                
                //删除评分项目信息
                bool isSucc = PerformanceElemBus.DeleteDeptInfo (ID);
                //删除成功
                if (isSucc)
                {
                    //输出响应
                    context.Response.Write(new JsonClass("", "", 1));
                    return;
                }
                //删除失败
                else
                {
                    //输出响应
                    context.Response.Write(new JsonClass("", "", 0));
                    return;
                }
            }
        }
    }
    private void SearchData(HttpContext context)
    {
        //从请求中获取排序列
        string orderString = context.Request.QueryString["OrderBy"];

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
        int pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(context.Request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;

        //获取数据
        PerformanceElemModel searchModel = new PerformanceElemModel();
        //设置查询条件
        //要素名称
        searchModel.ElemName  = context.Request.QueryString["ElemName"];
        //启用状态
        searchModel.UsedStatus = context.Request.QueryString["UsedStatus"];

        //查询数据
        DataTable dtData = PerformanceElemBus.SearchRectCheckElemInfo(searchModel);
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new PerformanceElemModel()
             {
                 ID = x.Element("ID").Value,//ID
                 ElemName = x.Element("ElemName").Value,//类型名称
                 UsedStatusName = x.Element("UsedStatusName").Value,//启用状态
                 StandardScore= x.Element ("StandardScore").Value,
                 ScoreArrange = x.Element("ScoreArrange").Value,
                 AsseStandard = x.Element("AsseStandard").Value,
                 AsseFrom = x.Element("AsseFrom").Value,
                 Remark = x.Element("Remark").Value
               
                    
                 
             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PerformanceElemModel()
             {
                 ID = x.Element("ID").Value,//ID
                 ElemName = x.Element("ElemName").Value,//类型名称
                 UsedStatusName = x.Element("UsedStatusName").Value,//启用状态
                  StandardScore=x.Element ("StandardScore").Value,
                 ScoreArrange = x.Element("ScoreArrange").Value,
                 AsseStandard = x.Element("AsseStandard").Value,
                 AsseFrom = x.Element("AsseFrom").Value,
                 Remark = x.Element("Remark").Value
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