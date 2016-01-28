<%@ WebHandler Language="C#" Class="CustTree" %>

using System;
using System.Web;
using System.Text;
using XBase.Model.Office.CustManager;
using XBase.Common;
using System.Data;
using XBase.Business.Office.CustManager;

public class CustTree : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    private const string TYPE_ALL = "0";//所有类别信息
    //样式名称
    private string ClassName = "";
    //子结点数目
    private int PrantCount;
    //子结点代码
    private string BaseCode = "";
    //子结点名称
    private string BaseName = "";
    //子结点编号
    private string BaseNo = "";
    //Javascript事件名
    private string JsAction = "";
    //样式表名称
    private string IfShowClass = "";
    //前面图片显示
    private string PrefixImg = "";
    //子符串变量
    StringBuilder Sb;
    private string ID;
    private string Type;
    private string TreeType;
    
    public void ProcessRequest (HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string action = (context.Request.Params["action"].ToString()).Trim();//操作  
            switch (action)
            {
                case "TreeShow":
                    TreeShow(context);
                    break;
                default:
                    break; 
            }
        }
    }

    //客户名称树形控件
    private void TreeShow(HttpContext context)
    {
        DataTable dt = new DataTable();
        Sb = new StringBuilder();
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();

        ID = context.Request.Params["ID"].ToString().Trim();
        Type = context.Request.Params["Type"].ToString().Trim();
        TreeType = context.Request.Params["TreeType"].ToString().Trim();

        if (!string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(Type))
        {
            if (Type == TYPE_ALL)
            {
                CustInfoModel model = new CustInfoModel();
                model.CompanyCD = companyCD;                
                //model.CustNo = "";
                //model.ID = 0;

                dt = CustCallBus.GetCustTree(model, CanUserID);

                if (dt != null && dt.Rows.Count > 0)
                {
                    PrantCount = dt.Rows.Count;

                    for (int i = 0; i < PrantCount; i++)
                    {
                        //子结点代码
                        BaseCode = dt.Rows[i]["ID"].ToString().Trim();
                        BaseNo = dt.Rows[i]["CustNo"].ToString().Trim();
                        //子结点名称
                        BaseName = dt.Rows[i]["CustName"].ToString().Trim();

                        string IsHasSub = string.Empty;

                        //最后一个结点
                        if (i == PrantCount - 1)
                        {
                            ClassName = "file";
                            IfShowClass = "";
                            PrefixImg = "<img src=\"../../../Images/L.gif\" />";
                        }
                        else
                        {
                            ClassName = "file";
                            IfShowClass = "class='list0'";
                            PrefixImg = "<img src=\"../../../Images/T.gif\" />";
                        }
                        JsAction = "onclick=\"LoadBomInfo(" + BaseCode + ",'" + BaseNo + "','" + BaseName + "')\"";

                        //生成子树代码
                        Sb.Append("<table  border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                        Sb.Append("<tr><td ><div id=\"P_" + BaseCode + "\" style=\"float: left\">" + PrefixImg + "</div><div id='M1_" + BaseCode + "'  class='" + ClassName + "' title='" + BaseName + "'  " + IsHasSub + " ><a  href='#this' " + JsAction + "  title=\"" + BaseName + "\"> " + (BaseName.Substring(0, BaseName.Length > 18 ? 18 : BaseName.Length)) + (BaseName.Length > 18 ? "..." : string.Empty) + "</a></div>");
                        Sb.Append("<div id='M2_" + BaseCode + "' style='display:none;' " + IfShowClass + "></div></td></tr>");
                        Sb.Append("</table>");
                    }
                }
            }
        }

        context.Response.Write(Sb.ToString());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}