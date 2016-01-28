<%@ WebHandler Language="C#" Class="Training_Query" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/28
 * 描    述： 招聘申请列表
 * 修改日期： 2009/03/28
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

public class Training_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            int pageIndex = int.Parse(request.QueryString["PageIndex"]);
            //从请求中获取每页显示记录数
            int pageCount = int.Parse(request.QueryString["PageCount"]);
            //跳过记录数
            int skipRecord = (pageIndex - 1) * pageCount;

            //获取数据
            TrainingSearchModel searchModel = new TrainingSearchModel();
            //设置查询条件
            //培训编号
            searchModel.TrainingNo = request.QueryString["TrainingNo"].Trim();
            //培训名称
            searchModel.TrainingName = request.QueryString["TrainingName"].Trim();
            //培训方式
            searchModel.TrainingWayID = request.QueryString["TrainingWay"].Trim();
            //开始时间
            searchModel.StartDate = request.QueryString["StartDate"].Trim();
            searchModel.StartToDate = request.QueryString["StartToDate"].Trim();

            //查询数据
            DataTable dtTraining = TrainingBus.SearchTrainingInfo(searchModel);
            //转化数据
            XElement dsXML = PageListCommon.ConvertDataTableToXML(dtTraining, "Data");
            //linq排序
            var dsLinq =
                (orderBy == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderByCol).Value ascending
                 select new TrainingSearchModel()
                 {
                     ID = x.Element("ID").Value,//ID
                     TrainingNo = x.Element("TrainingNo").Value,//培训编号
                     TrainingName = x.Element("TrainingName").Value,//培训名称
                     TrainingWayName = x.Element("TrainingWayName").Value,//培训方式
                     TrainingTeacher = x.Element("TrainingTeacher").Value,//培训老师
                     StartDate = x.Element("StartDate").Value,//开始时间
                     EndDate = x.Element("EndDate").Value,//结束时间
                     TrainingPlace = x.Element("TrainingPlace").Value//培训地点
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderByCol).Value descending
                 select new TrainingSearchModel()
                 {
                     ID = x.Element("ID").Value,//ID
                     TrainingNo = x.Element("TrainingNo").Value,//培训编号
                     TrainingName = x.Element("TrainingName").Value,//培训名称
                     TrainingWayName = x.Element("TrainingWayName").Value,//培训方式
                     TrainingTeacher = x.Element("TrainingTeacher").Value,//培训老师
                     StartDate = x.Element("StartDate").Value,//开始时间
                     EndDate = x.Element("EndDate").Value,//结束时间
                     TrainingPlace = x.Element("TrainingPlace").Value//培训地点
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
            string Nos = request.QueryString["DeleteNOs"].Trim();
            JsonClass jc;

            bool IsHave = TrainingBus.GetAsseByTraNo(Nos);
            if (IsHave == true)
            {
                jc = new JsonClass("success", "", 2);
            }
            else
            {
                 //执行删除
                bool isSucc = TrainingBus.DeleteTrainingInfo(Nos);
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