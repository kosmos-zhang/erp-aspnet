/**********************************************
 * 类作用：   上传类
 * 建立人：   吴成好
 * 建立时间： 2008-09-30 
 * Copyright (C) 2007-2008 吴成好
 * All rights reserved
 ***********************************************/

using System;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace XBase.Common
{
    /// <summary>
    /// HtmlInputFileControl 的摘要说明。
    /// </summary>
    public class HtmlInputFileControl
    {

        //文件类型
        public String fileContentType = "";
        //文件大小
        public int fileContentLength = 0;

        #region HtmlInputFileControl
        public HtmlInputFileControl()
        {
        }
        #endregion

        #region IsAllowedExtension是否允许该扩展名上传
        public bool IsAllowedExtension(HttpPostedFile hifile)
        {
            string strOldFilePath = "", strExtension = "";

            //允许上传的扩展名，可以改成从配置文件中读出
            string[] arrExtension = { ".gif", ".GIF", ".JPG", ".jpg", ".JPEG", ".BMP", ".PNG", ".jpeg", ".bmp", ".png", ".rar", ".zip", ".doc", ".xls" };

            if (hifile.FileName != string.Empty)
            {
                strOldFilePath = hifile.FileName;
                //取得上传文件的扩展名
                strExtension = strOldFilePath.Substring(strOldFilePath.LastIndexOf("."));
                //判断该扩展名是否合法
                for (int i = 0; i < arrExtension.Length; i++)
                {
                    if (strExtension.Equals(arrExtension[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region IsAllowedLength判断上传文件大小是否超过最大值
        public bool IsAllowedLength(HttpPostedFile hifile)
        {
            //允许上传文件大小的最大值,可以保存在xml文件中,单位为KB
            int i = 512;
            //如果上传文件的大小超过最大值,返回flase,否则返回true.
            if (hifile.ContentLength > i * 512)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region SaveFile上传文件并返回文件名
        public string SaveFile(HttpPostedFile hifile, string strAbsolutePath)
        {
            string strOldFilePath = "", strExtension = "", strNewFileName = "";

            if (hifile.FileName != string.Empty)
            {
                strOldFilePath = hifile.FileName;
                //判断是否符合大小限制
                /*if (!this.IsAllowedLength(strOldFilePath))
                {
                    return "notAllowedLength";
                }*/
                //取得上传文件的扩展名
                strExtension = strOldFilePath.Substring(strOldFilePath.LastIndexOf("."));
                //文件上传后的命名
                strNewFileName = GetUniqueString() + strExtension;
                //判断文件类型，并指定相应路径
                this.fileContentType = strOldFilePath.Substring(strOldFilePath.LastIndexOf("."));
                if (this.fileContentType.Equals(".jpg") || this.fileContentType.Equals(".gif") || this.fileContentType.Equals(".gif") || this.fileContentType.Equals(".GIF") || this.fileContentType.Equals(".JPG") || this.fileContentType.Equals(".jpg") || this.fileContentType.Equals(".JPEG") || this.fileContentType.Equals(".BMP") || this.fileContentType.Equals(".PNG") || this.fileContentType.Equals(".jpeg") || this.fileContentType.Equals(".bmp") || this.fileContentType.Equals(".png"))
                {
                    strAbsolutePath += "\\" + "picture";
                }
                else if (this.fileContentType.Equals(".doc") || this.fileContentType.Equals(".ppt") || this.fileContentType.Equals(".xls") || this.fileContentType.Equals(".pdf"))
                {
                    strAbsolutePath += "\\" + "office";
                }
                else if (this.fileContentType.Equals(".zip") || this.fileContentType.Equals(".rar"))
                {
                    strAbsolutePath += "\\" + "winrar";
                }
                else
                {
                    return "notAllowedExtension";
                }

                if (strAbsolutePath.LastIndexOf("\\") == strAbsolutePath.Length)
                {
                    hifile.SaveAs(strAbsolutePath + strNewFileName);
                }
                else
                {
                    hifile.SaveAs(strAbsolutePath + "\\" + strNewFileName);
                }
                //文件参数赋值                
                this.fileContentLength = hifile.ContentLength; //文件大小
            }
            return strNewFileName;
        }
        #endregion

        #region CoverFile重新上传文件，删除原有文件
        public void CoverFile(HttpPostedFile ffFile, string strAbsolutePath, string strOldFileName)
        {
            //获得新文件名
            string strNewFileName = GetUniqueString();

            if (ffFile.FileName != string.Empty)
            {
                //旧图片不为空时先删除旧图片
                if (strOldFileName != string.Empty)
                {
                    DeleteFile(strAbsolutePath, strOldFileName);
                }
                SaveFile(ffFile, strAbsolutePath);
            }
        }
        #endregion

        #region DeleteFile删除指定文件
        public void DeleteFile(string strAbsolutePath, string strFileName)
        {
            if (strAbsolutePath.LastIndexOf("\\") == strAbsolutePath.Length)
            {
                if (File.Exists(strAbsolutePath + strFileName))
                {
                    File.Delete(strAbsolutePath + strFileName);
                }
            }
            else
            {
                if (File.Exists(strAbsolutePath + "\\" + strFileName))
                {
                    File.Delete(strAbsolutePath + "\\" + strFileName);
                }
            }
        }
        #endregion

        #region GetUniqueString获取一个不重复的文件名
        public static string GetUniqueString()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffffff");
        }
        #endregion

        #region 上传文件并返回文件的相对路径
        /// <summary>
        /// 上传文件并返回文件的相对路径
        /// </summary>
        /// <param name="hpFile">上传文件</param>
        /// <param name="companySavePath">公司保存路径</param>
        /// <returns></returns>
        public static string SaveUploadFile(HttpPostedFile hpFile, string companySavePath)
        {
            //上传后的文件名
            string newFileName = "";

            //获取文件名
            string oldFilePath = hpFile.FileName;
            string docName = oldFilePath.Substring(hpFile.FileName.LastIndexOf("\\") + 1);
            //上传路径输入时
            if (!string.IsNullOrEmpty(oldFilePath))
            {
                //取得上传文件的扩展名
                string extension = oldFilePath.Substring(oldFilePath.LastIndexOf(".")).ToLower();
                //不允许上传的文件格式时
                if (StringUtil.GetInArrayID(extension, ConstUtil.SAVE_PILE_TYPE) < 0)
                {
                    return null;
                }
                //文件上传后的命名
                //newFileName = GetUniqueString() + extension;
                newFileName = docName;
                //文件路径是否存在
                DirectoryInfo folder = new DirectoryInfo(companySavePath);
                //目录不存在，则创建新的目录
                if (!folder.Exists)
                {
                    //创建目录
                    folder.Create();
                }
                //保存文件
                hpFile.SaveAs(companySavePath + "\\" + newFileName);
            }
            else
                return null;
            //返回新的文件路径
            return companySavePath + "\\" + newFileName;
        }
        #endregion

    }
}
