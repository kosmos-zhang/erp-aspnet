<%@ WebHandler Language="C#" Class="DeleteFile" %>

using System;
using System.Web;
using System.IO;
using System.Net;
using System.Text;

public class DeleteFile : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        string _FileName = context.Request.Form["FileName"];//文件名
        string _FilePath = context.Request.Form["FilePath"].Replace(_FileName,"");//文件路径
        JsonClass JC;


        try
        {
            if (_FilePath.LastIndexOf("\\") == _FilePath.Length)
            {
                if (File.Exists(_FilePath + _FileName))
                {
                    File.Delete(_FilePath + _FileName);
                    JC = new JsonClass("", "", 1);
                }
                else 
                {
                    JC = new JsonClass("", "", 2);
                }
            }
            else
            {
                if (File.Exists(_FilePath + "\\" + _FileName))
                {
                    File.Delete(_FilePath + "\\" + _FileName);
                    JC = new JsonClass("", "", 1);
                }
                else 
                {
                    JC = new JsonClass("", "", 2);
                }
            }
        }
        catch
        {
            JC = new JsonClass("", "", 0);
        }
        context.Response.Write(JC);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}