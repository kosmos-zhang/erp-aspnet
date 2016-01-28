<%@ WebHandler Language="C#" Class="MoveApply_Query" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/22
 * 描    述： 离职申请列表
 * 修改日期： 2009/04/22
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

public class MoveApply_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            
            string orderString = request.QueryString["OrderBy"];//从请求中获取排序列
            string orderBy = "asc";//排序：默认为升序
            string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ModifiedDate"
            if (string.IsNullOrEmpty(orderString) || orderString.EndsWith("_d"))//降序时如果设置为降序
            {
                orderBy = "desc";//排序：降序
            }
            int pageIndex = int.Parse(request.QueryString["PageIndex"]);//从请求中获取当前页
            int pageCount = int.Parse(request.QueryString["PageCount"]);//从请求中获取每页显示记录数
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            
            MoveApplyModel searchModel = new MoveApplyModel();//获取数据
            //设置查询条件
            searchModel.MoveApplyNo = request.QueryString["MoveApplyNo"];//申请编号
            searchModel.EmployeeID = request.QueryString["ApplyID"];//申请人
            searchModel.ApplyDate = request.QueryString["ApplyDate"];//申请日期
            searchModel.ApplyToDate = request.QueryString["ApplyToDate"];
            searchModel.FlowStatusID = request.QueryString["FlowStatus"];//审批状态
            
            string ord = orderByCol + " " + orderBy;
            int TotalCount = 0;


            DataTable dtApply = MoveApplyBus.SearchMoveApplyInfo(searchModel, pageIndex, pageCount, ord, ref TotalCount);//查询数据
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(TotalCount.ToString());
            if (dtApply.Rows.Count != 0)
            {
                sb.Append(",data:");
                sb.Append(JsonClass.DataTable2Json(dtApply));
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
            string BillTypeFlag=ConstUtil.CODING_TYPE_HUMAN;
            string BillTypeCode =ConstUtil.CODING_HUMAN_ITEM_MOVEAPPLY;
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            JsonClass jc;
            if (!MoveApplyDBHelper.IfDeleteMoveApplyInfo(Nos,companyCD,BillTypeFlag,BillTypeCode)) 
            {
                jc = new JsonClass("", "", 2);
                context.Response.Write(jc);
                context.Response.End();
            }
            //执行删除
            bool isSucc = MoveApplyBus.DeleteMoveApplyInfo(Nos);
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