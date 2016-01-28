<%@ WebHandler Language="C#" Class="PerformanceType" %>
/**********************************************
 * 作    者： 王保军
 * 创建日期： 2009/04/21
 * 描    述： 考核类型设置
 * 修改日期： 2009/04/21
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

public class PerformanceType : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
      public void ProcessRequest(HttpContext context)
    {
        //获取请求动作
        string action = context.Request.Params["Action"];
        //保存考核类型     
        if ("EditInfo".Equals(action))
        {
            PerformanceTypeModel model = new PerformanceTypeModel();
            //编辑模式
            model.EditFlag = context.Request.QueryString["EditFlag"];
            //类型ID 
            model.ID = context.Request.QueryString["ElemID"];
            //类型名称
            model.TypeName  = context.Request.QueryString["ElemName"];
            //启用状态
            model.UsedStatus = context.Request.QueryString["UsedStatus"];
            //执行保存
            bool isSucc = PerformanceTypeBus.SaveRectCheckElemInfo(model);
            //成功
            if (isSucc)
            {
                context.Response.Write(new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, model.ID, 1));
            }
            //失败
            else
            {
                context.Response.Write(new JsonClass("", "", 0));
            }
        }
        //修改时获取数据操作 
        else if ("GetInfo".Equals(action))
        {
            //获取要素ID
            string elemID = context.Request.QueryString["ElemID"];
            //获取考核类型信息

            DataTable dtDeptInfo = PerformanceTypeBus.GetRectCheckElemInfoWithID(elemID);
            
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
        else if ("DeleteInfo".Equals(action))
        {
            //获取要素ID
            string elemID = context.Request.QueryString["DeleteNO"];
            //替换引号
            elemID = elemID.Replace("'", "");
            //判断要素是否被使用
            bool isUsed = PerformanceTypeBus .IsTemplateUsed (elemID);
            //已经被使用
            if (isUsed)
            {
                //输出响应 返回不执行删除
                context.Response.Write(new JsonClass("", "", 2));
            }
            else
            {
                //删除要素
                bool isSucc =PerformanceTypeBus .DeletePerTypeInfo (elemID);
                //删除成功
                if (isSucc)
                {
                    //输出响应
                    context.Response.Write(new JsonClass("", "", 1));
                }
                //删除失败
                else
                {
                    //输出响应
                    context.Response.Write(new JsonClass("", "", 0));
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
          PerformanceTypeModel searchModel = new PerformanceTypeModel();
          //设置查询条件
          //要素名称
          searchModel.TypeName  = context.Request.QueryString["ElemName"];
          //启用状态
          searchModel.UsedStatus = context.Request.QueryString["UsedStatus"];
 
          //查询数据
          DataTable dtData = PerformanceTypeBus.SearchRectCheckElemInfo(searchModel);
          //转化数据
          XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
          //linq排序
          var dsLinq =
              (orderBy == "ascending") ?
              (from x in dsXML.Descendants("Data")
               orderby x.Element(orderByCol).Value ascending
               select new PerformanceTypeModel()
               {
                   ID = x.Element("ID").Value,//ID
                   TypeName = x.Element("TypeName").Value,//类型名称
                   UsedStatusName = x.Element("UsedStatusName").Value//启用状态
               })
                        :
              (from x in dsXML.Descendants("Data")
               orderby x.Element(orderByCol).Value descending
               select new PerformanceTypeModel()
               {
                   ID = x.Element("ID").Value,//ID
                   TypeName = x.Element("TypeName").Value,//类型名称
                   UsedStatusName = x.Element("UsedStatusName").Value//启用状态
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