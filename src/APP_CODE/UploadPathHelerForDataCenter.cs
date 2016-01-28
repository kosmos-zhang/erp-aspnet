using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
///UploadPathHelerForDataCenter 的摘要说明
/// </summary>
public class UploadPathHelerForDataCenter
{
    public UploadPathHelerForDataCenter()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }


    public static string KnowledgeCenterDownloadPath
    {
        get
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["KnowledgeCenterDownloadPath"];
            return path.Replace("~", AppDomain.CurrentDomain.BaseDirectory);

        }
    }

    public static string InfomationCenterDownloadPath
    {
        get
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["InfomationCenterDownloadPath"];
            return path.Replace("~", AppDomain.CurrentDomain.BaseDirectory);

        }
    }
}
