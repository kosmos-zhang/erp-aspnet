<%@ WebHandler Language="C#" Class="RectCheckElem_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/15
 * 描    述： 面试评测要素设置
 * 修改日期： 2009/04/15
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

public class RectCheckElem_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取请求动作
        string action = context.Request.Params["Action"];
        //保存评测要素        
        if ("EditInfo".Equals(action))
        {
            RectCheckElemModel model = new RectCheckElemModel();
            //编辑模式
            model.EditFlag = context.Request.Params["EditFlag"].ToString();
            //启用状态
            model.UsedStatus = context.Request.Params["UsedStatus"].ToString();
            //要素ID 
            model.ID = context.Request.Params["ElemID"].ToString();
            //更新处理时
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {
                //获取修改前的启用状态
                string reUsedStatus = context.Request.Params["BeforeUpdateUsedStatus"].ToString();
                //当将启用状态从启用修改为停用时，校验该要素是否被使用
                if (reUsedStatus == "1" && model.UsedStatus == "0")
                {
                    //校验要素是否被使用
                    //执行保存
                    bool isExist = RectCheckElemBus.IsRectCheckElemUsed(model.ID);
                    //如果已经被使用
                    if (isExist)
                    {
                        context.Response.Write(new JsonClass("", "", 2));
                        return;
                    }
                }
            }
            //要素名称
            model.ElemName = context.Request.Params["ElemName"].ToString();
            //评分标准 
            model.Standard = context.Request.Params["Standard"].ToString();
            //备注
            model.Remark = context.Request.Params["Remark"].ToString();
            //执行保存
            bool isSucc = RectCheckElemBus.SaveRectCheckElemInfo(model);
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
            //获取组织机构信息
            DataTable dtDeptInfo = RectCheckElemBus.GetRectCheckElemInfoWithID(elemID);
            //定义放回变量
            StringBuilder sbReturn = new StringBuilder();
            sbReturn.Append("{");
            sbReturn.Append("data:");
            sbReturn.Append(JsonClass.DataTable2Json(dtDeptInfo));
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
            bool isUsed = RectCheckElemBus.IsRectCheckElemUsed(elemID);
            //已经被使用
            if (isUsed)
            {
                //输出响应 返回不执行删除
                context.Response.Write(new JsonClass("", "", 2));
            }
            else
            {
                //删除要素
                bool isSucc = RectCheckElemBus.DeleteRectCheckElemInfo(elemID);
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

    /// <summary>
    /// 查询数据
    /// </summary>
    /// <param name="context"></param>
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
        RectCheckElemModel searchModel = new RectCheckElemModel();
        //设置查询条件
        //要素名称
        searchModel.ElemName = context.Request.QueryString["ElemName"];
        //启用状态
        searchModel.UsedStatus = context.Request.QueryString["UsedStatus"];
 

        if (searchModel.ElemName != null)
        {
            int bbb = searchModel.ElemName.IndexOf('%');///过滤字符串
            if (bbb != -1)
            {
                searchModel.ElemName = searchModel.ElemName.Replace('%', ' ');
            }
        }
        
        
        //查询数据
        DataTable dtData = RectCheckElemBus.SearchRectCheckElemInfo(searchModel);
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new RectCheckElemModel()
             {
                 ID = x.Element("ID").Value,//ID
                 ElemName = x.Element("ElemName").Value,//要素名称
                 UsedStatusName = x.Element("UsedStatusName").Value//启用状态
             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new RectCheckElemModel()
             {
                 ID = x.Element("ID").Value,//ID
                 ElemName = x.Element("ElemName").Value,//要素名称
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