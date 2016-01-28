/**********************************************
 * 类作用：   验证码小页面
 * 建立人：   吴志强
 * 建立时间： 2008/12/31
 ***********************************************/

using System;
using System.Web;
using XBase.Common;
using System.IO;

public partial class CheckCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //获得保存了验证码图片的内存
            MemoryStream ms = ValidateImage.GenerateImage();
            //清空响应
            Response.ClearContent();
            //设置响应类型
            Response.ContentType = "image/Gif";
            //返回响应
            Response.BinaryWrite(ms.ToArray());
        }
    }
}
