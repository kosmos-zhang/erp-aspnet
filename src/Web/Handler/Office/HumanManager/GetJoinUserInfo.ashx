<%@ WebHandler Language="C#" Class="GetJoinUserInfo" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/03
 * 描    述： 获取培训的参与人员信息
 * 修改日期： 2009/04/03
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using System.Data;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;

public class GetJoinUserInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //从请求中获取培训编号
        string trainingNo = context.Request.QueryString["TrainingNo"];
        //查询培训的参与人员
        DataTable dtJionUser = TrainingBus.GetJionUserInfo(trainingNo);

        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtJionUser, "Data");
        //linq排序
        var dsLinq =
                from x in dsXML.Descendants("Data")
                select new EmployeeInfo()
                 {
                     UserID = x.Element("UserID").Value,//ID
                     UserName = x.Element("UserName").Value//姓名
                 };
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
        sbReturn.Append(PageListCommon.ToJSON(dsLinq.ToList()));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }

    /// <summary>
    /// 人员信息
    /// </summary>
    public class EmployeeInfo
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}