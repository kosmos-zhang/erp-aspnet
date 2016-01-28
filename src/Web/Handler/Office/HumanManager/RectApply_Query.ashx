<%@ WebHandler Language="C#" Class="RectApply_Query" %>
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
using XBase.Common;

public class RectApply_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            string orderBy = "asc";
            //要排序的字段，如果为空，默认为"RectApplyNo"
            string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "RectApplyNo";
            //降序时如果设置为降序
            if (orderString.EndsWith("_d"))
            {
                //排序：降序
                orderBy = "desc";
            }
            //从请求中获取当前页
            int pageIndex = int.Parse(request.QueryString["PageIndex"]);
            //从请求中获取每页显示记录数
            int pageCount = int.Parse(request.QueryString["PageCount"]);
            //跳过记录数
            int skipRecord = (pageIndex - 1) * pageCount;

            //获取数据
            RectApplyModel searchModel = new RectApplyModel();
            //设置查询条件
            
            searchModel.RectApplyNo = request.QueryString["RectApplyNo"];//申请编号
            searchModel.DeptID = request.QueryString["ApplyDeptID"];//申请部门
            //申请日期
            //searchModel.UsedDate  = request.QueryString["ApplyDate"];
            //searchModel.JobName = request.QueryString["JobName"];//职位名称
            searchModel.FlowStatusID = request.QueryString["FlowStatus"];//审批状态
            searchModel.BillStatus = request.QueryString["BillStatus"];//单据状态

            string ord = orderByCol + " " + orderBy;
            int TotalCount = 0;


            //查询数据
            DataTable dtRectApply = RectApplyBus.SearchRectApplyInfo(searchModel, pageIndex, pageCount, ord, ref TotalCount);//查询数据
            //转化数据
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(TotalCount.ToString());
            if (dtRectApply.Rows.Count != 0)
            {
                sb.Append(",data:");
                sb.Append(JsonClass.DataTable2Json(dtRectApply));
                sb.Append("}");
            }
            else
            {
                sb.Append(",data:[{");
                sb.Append("\"ID\":\"\"}]");
                sb.Append("}");
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();


        }
        //删除数据
        else
        {
            //获取删除的ID
            string Nos = request.QueryString["DeleteNOs"];
            JsonClass jc;
          
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            if (!RectApplyDBHelper.IfDeleteRectApplyInfo(Nos, companyCD, ConstUtil.BILL_TYPEFLAG_HUMAN, ConstUtil.BILL_TYPECODE_HUMAN_RECT_APPLY)) 
            {
                jc = new JsonClass("", "", 2);
                context.Response.Write(jc);
                context.Response.End();
            }
            //执行删除
            bool isSucc = RectApplyBus.DeleteRectApplyInfo(Nos);
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