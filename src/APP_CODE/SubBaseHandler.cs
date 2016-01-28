using System;
using System.Web;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using System.Collections.Generic;

using System.Web.SessionState;
using XBase.Common;
using System.Text;
using System.Data;

/// <summary>
///SubBaseHandler 的摘要说明
/// </summary>
public abstract class SubBaseHandler : IHttpHandler, IRequiresSessionState
{
    protected HttpContext _context = null;
    protected HttpRequest _request = null;
    protected HttpResponse _response = null;
    private string _action = string.Empty;
    public string Action
    {
        get { return _action; }
    }
    public void ProcessRequest(HttpContext context)
    {
        _context = context;
        _request = context.Request;
        _response = context.Response;
        _action = GetRequestForm("action", false);
        ActionHandler(_action);
    }

    public abstract void ActionHandler(string action);

    public UserInfoUtil UserInfo
    {
        get 
        {
            if (XBase.Common.SessionUtil.Session["UserInfo"] != null)
                return (UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            else
                return null;
        }
    }

    public string GetRequestForm(string key,bool IsNum)
    {
        return FormatRequest(_context, key, IsNum);
    }

    
    public  string FormatRequest(HttpContext context, string key, bool IsNum)
    {
        if (context.Request.Form[key] == null)
        {
            if (IsNum)
                return "-1";
            else
                return string.Empty;
        }
        else
        {
            if (string.IsNullOrEmpty(context.Request.Form[key].ToString()))
                if (IsNum)
                    return "-1";
                else
                    return string.Empty;
            else
                return context.Request.Form[key].ToString();
        }
    }

    public  string GetElementValue(XElement x, string Key, bool IsDate)
    {
        if (x.Element(Key) != null)
        {
            string tempValue = x.Element(Key).Value;
            if (IsDate)
            {
                if (string.IsNullOrEmpty(tempValue))
                {
                    return string.Empty;
                }
                else
                    return Convert.ToDateTime(tempValue).ToString("yyyy-MM-dd");
            }
            else
                return x.Element(Key).Value;
        }
        else
            return string.Empty;
    }

    protected void OutputResult(bool result, string content)
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
    protected void OutputDataTable(DataTable dt, int TotalCount)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("{totalCount:'" + TotalCount.ToString() + "',data:");
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
                    /*根据datatable列的数据格式 来格式化字符*/
                    string tmp=string.Empty;
                    if (dt.Columns[j].DataType.ToString() == "System.Int16"|| dt.Columns[j].DataType.ToString() == "System.Int32" || dt.Columns[j].DataType.ToString() == "System.Int64")
                        tmp = dt.Rows[i][j].ToString();
                    else if (dt.Columns[j].DataType.ToString() == "System.Decimal")
                        tmp = dt.Rows[i][j].ToString();
                    else if (dt.Columns[j].DataType.ToString() == "System.DateTime")
                    {
                        if (dt.Rows[i][j] != null && dt.Rows[i][j].ToString() != "")
                        {
                            tmp = (Convert.ToDateTime(dt.Rows[i][j].ToString())).ToString("yyyy-MM-dd");
                        }
                    }
                    else
                        tmp = dt.Rows[i][j].ToString();
                        jsonBuilder.Append(GetSafeJSONString(tmp));//.Replace("\"","\\\""));
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
        string output = input.Replace("\"", "\\\"");
        output = safeJSON.Replace(output, "<br>");

        return output;

    }


    protected void Output(string content)
    {
        _context.Response.Write(content);
    }

}
