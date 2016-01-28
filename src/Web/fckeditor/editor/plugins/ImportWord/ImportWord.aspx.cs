using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace Fck.CustomButton.ImportWord
{
    public partial class ImportWord : System.Web.UI.Page
    {
        //Html文件名
        private string _htmlFileName;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 上传Word文档
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="filePath"></param>
        private string UpLoadFile(HtmlInputFile inputFile)
        {
            string fileName, fileExtension;
  
            //建立上传对象
            HttpPostedFile postedFile = inputFile.PostedFile;

            fileName = System.IO.Path.GetFileName(postedFile.FileName);
            fileExtension = System.IO.Path.GetExtension(fileName);

            //上传图片保存Base路径 如果修改此路径需要同时修改图片地址替换路径(在212行附件)
            string phyPath = Server.MapPath("~/") + "FckUpload\\ImportWord\\0\\";

            //判断路径是否存在,若不存在则创建路径
            DirectoryInfo upDir = new DirectoryInfo(phyPath);
            if (!upDir.Exists)
            {
                upDir.Create();
            }

            //保存文件
            try
            {
                postedFile.SaveAs(phyPath + fileName);
            }
            catch
            {
                
            }
            return phyPath + fileName;
        }

        /// <summary>
        /// word转成html
        /// </summary>
        /// <param name="wordFileName"></param>
        private string WordToHtml(object wordFileName)
        {
            //在此处放置用户代码以初始化页面
            ApplicationClass word = new ApplicationClass();
            Type wordType = word.GetType();
            Documents docs = word.Documents;

            //打开文件
            Type docsType = docs.GetType();
            Document doc = (Document)docsType.InvokeMember("Open",
            System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[] { wordFileName, true, true });

            //转换格式，另存为
            Type docType = doc.GetType();

            string wordSaveFileName = wordFileName.ToString();
            string strSaveFileName = wordSaveFileName.Substring(0, wordSaveFileName.Length - 3) + "html";
            object saveFileName = (object)strSaveFileName;
            //下面是Microsoft Word 9 Object Library的写法，如果是10，可能写成如下，不明之处欢迎到Programbbs.com讨论
            /*
            docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod,
             null, doc, new object[]{saveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatFilteredHTML});
            */
            ///其它格式：
            ///wdFormatHTML
            ///wdFormatDocument
            ///wdFormatDOSText
            ///wdFormatDOSTextLineBreaks
            ///wdFormatEncodedText
            ///wdFormatRTF
            ///wdFormatTemplate
            ///wdFormatText
            ///wdFormatTextLineBreaks
            ///wdFormatUnicodeText
            docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod,
             null, doc, new object[] { saveFileName, WdSaveFormat.wdFormatHTML });

            docType.InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod,
             null, doc, null);

            //退出 Word
            wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod,
             null, word, null);

            return saveFileName.ToString();
        }

        /// <summary>
        /// 上传并处理word文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string strFilename = UpLoadFile(this.fileWord);
            _htmlFileName = WordToHtml((object)strFilename);

            findUsedFromHtml(getHtml(_htmlFileName),strFilename);

            string strTempFilename = strFilename.Substring(0, strFilename.Length - 3) + "files";

            //删除文件
            System.IO.File.Delete(_htmlFileName);
            DeleteFolder(strTempFilename);
            System.IO.File.Delete(strFilename);
        }
/// <summary>   
/// 用递归方法删除文件夹目录及文件   
/// </summary>   
/// <param name="dir">带文件夹名的路径</param>    
public void DeleteFolder(string dir)   
{   
    if (Directory.Exists(dir)) //如果存在这个文件夹删除之    
    {   
       foreach (string d in Directory.GetFileSystemEntries(dir))   
      {   
         if (File.Exists(d))   
               File.Delete(d); //直接删除其中的文件                           
           else  
                DeleteFolder(d); //递归删除子文件夹    
       }   
       Directory.Delete(dir, true); //删除已空文件夹                    
    }   
}  
        /// <summary>
        /// 读取html文件，返回字符串
        /// </summary>
        /// <param name="strHtmlFileName"></param>
        /// <returns></returns>
        private string getHtml(string strHtmlFileName)
        {
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("gb2312");

            StreamReader sr = new StreamReader(strHtmlFileName, encoding);

            string str = sr.ReadToEnd();

            sr.Close();

            return str;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        private void findUsedFromHtml(string strHtml, string strFileName)
        {
            string strStyle;
            string strBody;

            // stytle 部分
            int index = 0;
            int intStyleStart = 0;
            int intStyleEnd = 0;

            while (index < strHtml.Length)
            {
                int intStyleStartTmp = strHtml.IndexOf("<style>", index);
                if (intStyleStartTmp == -1)
                {
                    break;
                }
                int intContentStart = strHtml.IndexOf("<!--", intStyleStartTmp);
                if (intContentStart - intStyleStartTmp == 9)
                {
                    intStyleStart = intStyleStartTmp;
                    break;
                }
                else
                {
                    index = intStyleStartTmp + 7;
                }
            }

            index = 0;
            while (index < strHtml.Length)
            {
                int intContentEndTmp = strHtml.IndexOf("-->", index);
                if (intContentEndTmp == -1)
                {
                    break;
                }
                int intStyleEndTmp = strHtml.IndexOf("</style>", intContentEndTmp);
                if (intStyleEndTmp - intContentEndTmp == 5)
                {
                    intStyleEnd = intStyleEndTmp;
                    break;
                }
                else
                {
                    index = intContentEndTmp + 4;
                }
            }

            strStyle = strHtml.Substring(intStyleStart, intStyleEnd - intStyleStart + 8);

            // Body部分
            int bodyStart = strHtml.IndexOf("<body");
            int bodyEnd = strHtml.IndexOf("</body>");

            strBody = strHtml.Substring(bodyStart, bodyEnd - bodyStart + 7);

            //替换图片地址
            string fullName = strFileName.Substring(strFileName.LastIndexOf("\\")+1);
            string strOld = fullName.Replace("doc", "files").Replace(" ", "%20");
            string strNew = Page.Request.ApplicationPath + "/FckUpload/ImportWord/0/" + strOld;

            strBody = strBody.Replace(strOld, strNew);
            strBody = strBody.Replace("v:imagedata", "img");
            strBody = strBody.Replace("</v:imagedata>", "");

            //Sgxcn临时调试用-但不可去掉
            //this.TextArea1.InnerText = strStyle;
            //this.Textarea2.InnerText = strBody;
            this.TextArea1.Value = strBody;

            //更改信息
            pnlOk.Visible = true;
            pnlUpload.Visible = false;
        }
    }
}