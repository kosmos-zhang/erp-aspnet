<%@ WebHandler Language="C#" Class="HRProxy_Query" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/23
 * 描    述： 查询人才代理列表
 * 修改日期： 2009/03/23
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.IO;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;

public class HRProxy_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取上下文的请求
        HttpRequest request = context.Request;
        //从请求中获取当前操作
        string action = request.QueryString["Action"];

        //分页控件查询数据
        if ("GetInfo".Equals(action))
        {
            //从请求中获取排序列
            string orderString = request.QueryString["OrderBy"];

            //排序：默认为降序
            string orderBy = "descending";
            //要排序的字段，如果为空，默认为"ID"
            string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";
            //降序时如果设置为升序
            if (orderString.EndsWith("_d"))
            {
                //排序：升序
                orderBy = "ascending";
            }
            //从请求中获取当前页
            int pageIndex = int.Parse(request.QueryString["PageIndex"]);
            //从请求中获取每页显示记录数
            int pageCount = int.Parse(request.QueryString["PageCount"]);
            //跳过记录数
            int skipRecord = (pageIndex - 1) * pageCount;

            //获取数据
            HRProxyModel searchModel = new HRProxyModel();
            //设置查询条件
            //企业编号
            searchModel.ProxyCompanyCD = request.QueryString["ProxyCompanyCD"];
            //企业名称
            searchModel.ProxyCompanyName = request.QueryString["ProxyCompanyName"].Trim();
            //重要程度
            searchModel.Important = request.QueryString["Important"].Trim();
            //合作关系
            searchModel.Cooperation = request.QueryString["Cooperation"].Trim();
            //启用状态
            searchModel.UsedStatus = request.QueryString["UsedStatus"].Trim();

            //查询数据
            DataTable dtProxy = HRProxyBus.SearchProxyInfo(searchModel);
            //转化数据
            XElement dsXML = PageListCommon.ConvertDataTableToXML(dtProxy, "Data");
            //linq排序
            var dsLinq =
                (orderBy == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderByCol).Value ascending
                 select new HRProxyModel()
                 {
                     ID = x.Element("ID").Value,//ID
                     ProxyCompanyCD = x.Element("ProxyCompanyCD").Value,//企业编号
                     ProxyCompanyName = x.Element("ProxyCompanyName").Value,//企业名称
                     ContactName = x.Element("ContactName").Value,//联系人
                     ContactTel = x.Element("ContactTel").Value,//固定电话
                     ContactMobile = x.Element("ContactMobile").Value,//移动电话
                     ContactWeb = x.Element("ContactWeb").Value,//网络通讯
                     Important = x.Element("Important").Value,//重要程度
                     Cooperation = x.Element("Cooperation").Value,//合作关系 
                     UsedStatus = x.Element("UsedStatus").Value//启用状态 
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderByCol).Value descending
                 select new HRProxyModel()
                 {
                     ID = x.Element("ID").Value,//ID
                     ProxyCompanyCD = x.Element("ProxyCompanyCD").Value,//企业编号
                     ProxyCompanyName = x.Element("ProxyCompanyName").Value,//企业名称
                     ContactName = x.Element("ContactName").Value,//联系人
                     ContactTel = x.Element("ContactTel").Value,//固定电话
                     ContactMobile = x.Element("ContactMobile").Value,//移动电话
                     ContactWeb = x.Element("ContactWeb").Value,//网络通讯
                     Important = x.Element("Important").Value,//重要程度
                     Cooperation = x.Element("Cooperation").Value,//合作关系 
                     UsedStatus = x.Element("UsedStatus").Value//启用状态 
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
        //删除数据
        else
        {
            //获取删除的ID
            string proxyNos = request.QueryString["DeleteNOs"].Trim();
            JsonClass jc;
            //执行删除
            bool isSucc = HRProxyBus.DeleteProxyInfo(proxyNos);
            //定义Json返回变量
            //删除成功时
            if (isSucc)
            {
                jc = new JsonClass("", "", 1);
            }
            //删除未成功时
            else
            {
                jc = new JsonClass("", "", 0);
            }
            //输出响应
            context.Response.Write(jc);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}