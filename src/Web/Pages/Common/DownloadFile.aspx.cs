/**********************************************
 * 类作用：   文件下载
 * 建立人：   吴志强
 * 建立时间： 2009/04/13
 ***********************************************/
using System;
using System.Web.UI;
using System.IO;
using System.Web;
using System.Data;

public partial class Pages_Common_DownloadFile : System.Web.UI.Page
{
    /// <summary>
    /// 类名：DownloadFile
    /// 描述：文件下载
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/13
    /// 最后修改时间：2009/04/13
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期处理
        if(!IsPostBack){
            //获取相对的文件路径名，和上传时对应
            string filePath = Request.QueryString["RelativeFilePath"];
            //如果路径未输入，返回不处理
            if (string.IsNullOrEmpty(filePath))
            {
                Response.Write("下载的文件未指明！");
                return;
            }
            else
            {
                //生成文件类实例
                FileInfo fileInfo = new FileInfo(filePath);
                //文件不存在时返回
                if (!fileInfo.Exists)
                {
                    Response.Write("该文件已经不存在！");
                    return;
                }
                //文件存在时，下载文件
                else
                {
                    //文件名
                    string fileName = fileInfo.Name;
                    FileStream fileStream = new FileStream(filePath, FileMode.Open);
                    long fileSize = fileStream.Length;
                    byte[] fileBuffer = new byte[fileSize];
                    fileStream.Read(fileBuffer, 0, (int)fileSize);
                    System.Web.HttpContext.Current.Response.ClearHeaders();

                    string UserAgent = Request.ServerVariables["http_user_agent"].ToLower();
                    if (UserAgent.IndexOf("firefox") == -1)//不是ff时
                        fileName = HttpUtility.UrlEncode(fileName.Trim(), System.Text.Encoding.UTF8);//utf8编码中文

                    System.Web.HttpContext.Current.Response.Charset = "utf-8";
                    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;

                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    System.Web.HttpContext.Current.Response.AppendHeader("Content-Type", "application/octet-stream");// "application/octet-stream"); 
                    //System.Web.HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
                    System.Web.HttpContext.Current.Response.BinaryWrite(fileBuffer);
                    HttpContext.Current.Response.End();


                }
            }
        }
    }
}
