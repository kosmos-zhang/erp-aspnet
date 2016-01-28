/**********************************************
 * 类作用：   文件上传
 * 建立人：   吴志强
 * 建立时间： 2009/03/16
 ***********************************************/
using System;
using System.Web.UI;
using XBase.Common;
using System.IO;
using System.Web;
using System.Data;
using XBase.Business.Common;

public partial class Pages_Common_UploadFile : System.Web.UI.Page
{
    /// <summary>
    /// 类名：Pages_Common_UploadFile
    /// 描述：文件上传
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/16
    /// 最后修改时间：2009/03/16
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 文件上传操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpload_Click(object sender, ImageClickEventArgs e)
    {
        string companyCD = string.Empty;
        //获取公司代码
        try
        {
            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        }
        catch
        {
            companyCD = "AAAAAA";
        }
        //获取公司文件相关信息
        DataTable dtFileInfo = UploadFileBus.GetCompanyUploadFileInfo();
        //
        if (dtFileInfo == null || dtFileInfo.Rows.Count < 1)
        {
        }
        //文件个数
        int docNum = GetSafeData.ValidateDataRow_Int(dtFileInfo.Rows[0], "MaxDocNum");
        //文件总大小
        long totalSize = GetSafeData.ValidateDataRow_Long(dtFileInfo.Rows[0], "MaxDocSize");
        //单个文件大小
        long singleSize = GetSafeData.ValidateDataRow_Long(dtFileInfo.Rows[0], "SingleDocSize");
        //文件保存路径
        string savePath = GetSafeData.ValidateDataRow_String(dtFileInfo.Rows[0], "DocSavePath");
        //获取控件上的文件对象
        HttpPostedFile hpFile = flLocalFile.PostedFile;

        //FileInfo1 file = new FileInfo1();
        //file.FileBytes = flLocalFile.PostedFile;
        //Session["File"] = file;

        string docName = hpFile.FileName.Substring(hpFile.FileName.LastIndexOf("\\") + 1);

        //校验文件大小
        string checkResult = CheckCompanyFile(hpFile, savePath, totalSize, singleSize, docNum);
        //大小超过允许范围时
        if (!string.IsNullOrEmpty(checkResult))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "UploadFaild", "<script language=javascript>popMsgObj.ShowMsg('"+ checkResult + "');</script>");
            return;
        }
        string IsExistFilePath = savePath + "\\" + docName;

        if (File.Exists(IsExistFilePath))//文件已经存在提示
        {
            ClientScript.RegisterStartupScript(this.GetType(), "UploadFaild", "<script language=javascript>popMsgObj.ShowMsg('文件名有重复，请重命名文件在上传！');</script>");
        }
        else 
        {
            //上传文件并获取文件相对路径
            string fileName = HtmlInputFileControl.SaveUploadFile(hpFile, savePath);
            //上传未成功
            if (string.IsNullOrEmpty(fileName))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "UploadFaild", "<script language=javascript>popMsgObj.ShowMsg('"
                                    + ConstUtil.UPLOAD_FILE_ERROR_TYPE + "');</script>");
                return;
            }
            //隐藏域中写入文件相对路径
            uploadFileUrl.Value = fileName;
            //上传文件名
            uploadDocName.Value = docName;
            //执行返回函数
            ClientScript.RegisterStartupScript(this.GetType(), "UploadSucc", "<script language=javascript>DoConfirm();</script>");
        }
    }
   


    #region 判断上传该文件是否超过公司允许的文件大小
    /// <summary>
    /// 判断上传该文件是否超过公司允许的文件大小
    /// </summary>
    /// <param name="hpFile">上传文件对象</param>
    /// <param name="savePath">保存路径</param>
    /// <param name="totalSize">文件总大小</param>
    /// <param name="singleSize">单个文件大小</param>
    /// <param name="docNum">文件个数</param>
    /// 
    private string CheckCompanyFile(HttpPostedFile hpFile, string savePath, long totalSize, long singleSize, int docNum)
    {
        //判断文件个数是否达到上限
        if (FileUtil.GetFolderFileCount(savePath) >= docNum)
        {
            return ConstUtil.UPLOAD_FILE_ERROR_MAX_NUM + "(" + docNum.ToString() + ")";
        }
        //获取上传文件大小 UPLOAD_FILE_ERROR_MAX_NUM
        long fileSize = long.Parse(hpFile.ContentLength.ToString()) / (1024 * 1024);
        //判断单个文件大小是否达到上限
        if (singleSize < fileSize)
        {
            return ConstUtil.UPLOAD_FILE_ERROR_SINGLE_SIZE + "(" + singleSize.ToString() + ")";
        }
        //获取文件夹大小
        long nowTotalSize = FileUtil.GetFolderSize(savePath) / (1024 * 1024);
        //判断总大小是否达到上限
        if (totalSize < (nowTotalSize + fileSize))
        {
            return ConstUtil.UPLOAD_FILE_ERROR_MAX_SIZE + "(" + totalSize.ToString() + ")";
        }
        //返回校验结果
        return string.Empty;
    }
    #endregion
}
