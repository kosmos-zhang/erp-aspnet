using System;
using System.Web;

using System.Web.SessionState;
using XBase.Common;
using System.Text;
using System.Data;

/// <summary>
///BaseHandler 的摘要说明
/// </summary>
public abstract class BaseHandler : IHttpHandler,IRequiresSessionState
{   

    /// <summary>
    /// 当前的ACTION
    /// </summary>
    private string _action = string.Empty;
    protected string Action
    {
        get { return _action; }
        set { _action = value; }
    }


    protected HttpContext _context = null;
    protected HttpRequest _request = null;
    protected HttpResponse _response = null;
    protected UserInfoUtil UserInfo = null;

    //protected bool Check_page_security_validate_code(string fromUrl)
    //{
    //   string _page_security_validate_code = GetParam("_page_security_validate_code");
    //   bool isok = CRCer.CheckString(_page_security_validate_code);
    //    if(!isok)
    //    {
    //        return false;
    //    }

    //    string _fromUrl = _page_security_validate_code.Substring(0,_page_security_validate_code.Length - 6);

    //    return (_fromUrl == fromUrl);

    //}

    public void ProcessRequest(HttpContext context)
    {
        
        _context = context;
        _request = context.Request;
        _response = context.Response;
        _context.Response.ContentType = "text/plain";

        UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        if (UserInfo == null)
        {
            OutputResult(false,"Session过期,请重新登陆");
            return;
        }

        if (_request.UrlReferrer == null)
        {
            OutputResult(false, "未授权的访问.");
            return;
        }

        if (_request.UrlReferrer.Host != _request.Url.Host)
        {
            OutputResult(false, "未授权的访问.");
            return;
        }

        //string fromUrl = _request.UrlReferrer.PathAndQuery;
        //if (fromUrl.IndexOf("))/") != -1)
        //{ 
        //    fromUrl = fromUrl.Substring(  fromUrl.IndexOf("))/")+3);
        //}
      
        //if (!Check_page_security_validate_code(fromUrl))
        //{
        //    OutputResult(false, "未授权的访问.");
        //    return;
        //}



     

        _action = GetParam("action");//获取当前的ACTION

        if (_action == string.Empty)
        {
            Output("未指定action");
        }
        else
        {
            ActionHandler(_action);
        }
    }


    protected string GetProfessionType()
    {
        string ProfessionType = string.Empty;
        if (_context.Session["ProfessionType"] == null)
        {
            DataTable dt = XBase.Business.SystemManager.CompanyBus.GetCompanyByCD("CompanyCD='" + UserInfo.CompanyCD + "'");
            if (dt.Rows.Count > 0)
            {
                _context.Session["ProfessionType"] = dt.Rows[0]["ProfessionType"].ToString();
            }
        }
        if (_context.Session["ProfessionType"] != null)
        {
            ProfessionType = _context.Session["ProfessionType"].ToString();
        }

        return ProfessionType;
    }

    /// <summary>
    /// 默认的ACTION处理
    /// </summary>
    /// <param name="action"></param>
    protected void DefaultAction(string action)
    {
        OutputResult(false, "未定义的action:" + action);
    }

    /// <summary>
    /// ActionHandler
    /// </summary>
    /// <param name="action"></param>
    protected abstract void ActionHandler(string action);
    

    /// <summary>
    /// 获取REQUEST的参数值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected string GetParam(string key)
    {
        if (_context.Request[key] == null)
        {
            return string.Empty;
        }
        else
        {
            if (_context.Request[key].ToString().Trim() + "" == "")
            {
                return string.Empty;
            }
            else
            {
                return _context.Request[key].ToString().Trim();
            }
        }
    }

    /// <summary>
    /// 向客户端输出信息
    /// </summary>
    /// <param name="content"></param>
    protected void Output(string content)
    {
        _context.Response.Write(content);
    }


    /// <summary>
    /// 向客户端输出信息(JSON)
    /// </summary>
    /// <param name="content"></param>
    protected void OutputResult(bool result,string content)
    {
        if (result)
            content = "{result:true,data:'" + content + "'}";
        else
            content = "{result:false,data:'" + content + "'}";
        _context.Response.Write(content);
    }

    /// <summary>
    /// 向客户端输出DataTable信息(JSON格式)
    /// </summary>
    /// <param name="dt"></param>
    protected void OutputDataTable(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append(DataTable2Json(dt));
        sb.Append("}");
        
        Output(sb.ToString());
    }

    /// <summary>
    /// 向客户端输出Data(JSON格式)
    /// </summary>
    /// <param name="dt"></param>
    protected void OutputData(string data)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append(data);
        sb.Append("}");

        Output(sb.ToString());
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


    /// <summary>
    /// DataTable转换为json字符串
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    protected string DataTable2Json(DataTable dt)
    {
        System.Text.StringBuilder jsonBuilder = new System.Text.StringBuilder();
        jsonBuilder.Append("[");
        if (dt.Rows.Count == 0)
        {
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                //jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
                jsonBuilder.Append(":\"");
                try
                {
                    jsonBuilder.Append( GetSafeJSONString(dt.Rows[i][j].ToString()) );//.Replace("\"","\\\""));
                }
                catch
                {
                    jsonBuilder.Append("");
                }
                jsonBuilder.Append("\",");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        return jsonBuilder.ToString();
    }

    private static System.Text.RegularExpressions.Regex safeJSON = new System.Text.RegularExpressions.Regex("[\\n\\r]");
    protected string GetSafeJSONString(string input)
    { 
        string output =  input.Replace("\"","\\\"");
        output = safeJSON.Replace(output, "<br>");

        return output;

    }


}
