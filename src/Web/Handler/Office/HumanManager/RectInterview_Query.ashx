<%@ WebHandler Language="C#" Class="RectInterview_Query" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/20
 * 描    述： 面试记录列表
 * 修改日期： 2009/04/20
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

public class RectInterview_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            //要排序的字段，如果为空，默认为"InterviewNo"
            string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "InterviewNo";
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
            RectInterviewModel searchModel = new RectInterviewModel();
            //设置查询条件
            //编号
            searchModel.InterviewNo = request.QueryString["InterviewNo"];
            //面试日期
            searchModel.InterviewDate = request.QueryString["InterviewDate"];
            searchModel.Attachment  = request.QueryString["InterviewToDate"];

            searchModel.InterviewResult  = request.QueryString["InterviewResult"];
            //应聘岗位
            searchModel.QuarterID = request.QueryString["QuarterID"];
            //姓名
            searchModel.StaffName = request.QueryString["StaffName"];
            //人力资源
            searchModel.CheckDate = request.QueryString["CheckStartDate"];
            ////部门主管
            searchModel.CheckNote = request.QueryString["CheckEndDate"];
            //最终结果
            searchModel.FinalResult = request.QueryString["FinalResult"];
            searchModel.PlanID = request.QueryString["PlanID"];
            searchModel.RectType = request.QueryString["RectType"];
            
            int aaa=searchModel.InterviewNo.IndexOf('%') ;///过滤字符串
              if (aaa !=-1)
            {
                searchModel .InterviewNo =searchModel .InterviewNo .Replace ('%',' '); 
            }
              if (searchModel.StaffName != null)
              {
                  int bbb = searchModel.StaffName.IndexOf('%');///过滤字符串
                  if (bbb != -1)
                  {
                      searchModel.StaffName = searchModel.StaffName.Replace('%', ' ');
                  }
              }
            //查询数据
            DataTable dtRectApply = RectInterviewBus.SearchInterviewInfo(searchModel);
            //转化数据
            XElement dsXML = PageListCommon.ConvertDataTableToXML(dtRectApply, "Data");
            //linq排序
            var dsLinq =
                (orderBy == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderByCol).Value ascending
                 select new RectInterviewModel()
                 {
                     ID = x.Element("ID").Value,//ID
                     InterviewNo = x.Element("InterviewNo").Value,//编号
                     InterviewDate = x.Element("InterviewDate").Value,//面试时间
                     StaffName = x.Element("StaffName").Value,//姓名
                     QuarterName = x.Element("QuarterName").Value,//岗位名称
                     InterviewResult  = x.Element("InterviewResult").Value,//面试人
                     RectType  = x.Element("RectType").Value,//人力资源
                     PlanName  = x.Element("PlanName").Value,//部门主管
                     CheckDate  = x.Element("CheckDate").Value,//部门主管
                     FinalResult = x.Element("FinalResult").Value//终审
                 })
                          :
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderByCol).Value descending
                 select new RectInterviewModel()
                 {
                     ID = x.Element("ID").Value,//ID
                     InterviewNo = x.Element("InterviewNo").Value,//编号
                     InterviewDate = x.Element("InterviewDate").Value,//面试时间
                     StaffName = x.Element("StaffName").Value,//姓名
                     QuarterName = x.Element("QuarterName").Value,//岗位名称
                     InterviewResult = x.Element("InterviewResult").Value,//面试人
                     RectType = x.Element("RectType").Value,//人力资源
                     PlanName = x.Element("PlanName").Value,//部门主管
                     CheckDate = x.Element("CheckDate").Value,//部门主管
                     FinalResult = x.Element("FinalResult").Value//终审
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
            string Nos = request.QueryString["DeleteNOs"];
            JsonClass jc;
            //执行删除
            bool isSucc = RectInterviewBus.DeleteInterviewInfo(Nos);
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