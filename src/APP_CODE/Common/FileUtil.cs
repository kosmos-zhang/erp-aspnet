/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2008.12.30
 * 描    述： 文件操作类
 * 修改日期： 2009.01.03
 * 版    本： 0.5.0
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace XBase.Common
{
    /// <summary>
    /// 类名：FileUtil
    /// 描述：提供文件操作一些公用方法
    /// 
    /// 作者：吴志强
    /// 创建时间：2008/12/30
    /// 最后修改时间：2008/12/30
    /// </summary>
    ///
    public class FileUtil
    {
        /// <summary>
        /// 构造方法，设置为private属性，不能实例化
        /// </summary>
        private FileUtil()
        {
        }

        #region 创建目录
        /// <summary>
        /// 创建目录
        /// 文件夹中不带【.】字符，如果带这个字符，创建的目录将和预想的不一样
        /// </summary>
        /// <param name="FileOrPath">文件或目录</param>
        public static void CreateDirectory(string FileOrPath)
        {
            if (FileOrPath != null && string.Empty.Equals(FileOrPath))
            {
                //目录
                string path;
                //参数为一个文件形式时
                if (FileOrPath.Contains("."))
                    //获得目录
                    path = Path.GetDirectoryName(FileOrPath);
                else
                    path = FileOrPath;
                //如果目录不存在，创建新的目录
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }     
        #endregion

        #region 读取文件
        /// <summary>
        /// 读取文件
        /// 当输入的文件名为空时，抛出异常
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="encoding">文件编码</param>
        /// <param name="isCache">是否先从缓存中先读取，true 先从缓存读取，缓存中读取不到再从文件夹中读取</param>
        /// <returns>文件内容</returns>
        public static string ReadFile(string fileName, Encoding encoding, bool isCache)
        {
            //源文件名或目标文件名未输入时，返回不操作
            if (fileName == null || string.Empty.Equals(fileName))
            {
                throw new FileNotFoundException(fileName + "文件名不存在！");
            }
            //文件内容
            string body;
            //读取缓存时
            if (isCache)
            {
                //从缓存中读取文件内容
                body = (string)HttpContext.Current.Cache[fileName];
                //如果尚未读取到缓存时，从文件读取
                if (body == null)
                {
                    //缓存中读取失败，从文件夹中读取
                    body = ReadFile(fileName, encoding, false);
                    //将读取到的内容保存到缓存中去
                    HttpContext.Current.Cache.Add(fileName, body, new System.Web.Caching.CacheDependency(fileName), DateTime.MaxValue, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                }
            }
            else
            {
                //文件不存在
                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException(fileName + "文件不存在！");
                }
                //从文件夹中读取文件
                using (StreamReader sr = new StreamReader(fileName, encoding))
                {
                    body = sr.ReadToEnd();
                }
            }

            return body;
        }      
        #endregion

        #region 保存文件 以新文件的形式保存
        /// <summary>
        /// 保存文件
        /// 以新文件的形式保存，如果保存的文件已经存在，则删除原来文件，生成新的文件
        /// </summary>
        /// <param name="fileText">文件内容</param>
        /// <param name="fileName">文件名</param>
        /// <param name="encoding">文件编码</param>
        /// <returns>是否保存成功 true 成功 false 失败</returns>
        public static bool SaveTextAsNewFile(string fileText, string fileName, Encoding encoding)
        {
            //源文件名或目标文件名未输入时，返回不操作
            if (fileName == null || string.Empty.Equals(fileName))
            {
                throw new FileNotFoundException(fileName + "文件名不存在！");
            }
            //创建目录
            FileUtil.CreateDirectory(fileName);

            //文件存在时
            if (File.Exists(fileName))
            {
                //先删除原文件，再创建新文件
                File.Delete(fileName);
            }

            //保存文件
            File.WriteAllText(fileName, fileText, encoding);

            return true;
        }       
        #endregion

        #region 保存文件 将要保存的内容保存到给定文件的末尾
        /// <summary>
        /// 保存文件
        /// 将要保存的内容保存到给定文件的末尾，如果该文件不存在，生成新的文件。
        /// 文件名未输入，或者输入空时，抛出异常
        /// </summary>
        /// <param name="fileText">文件内容</param>
        /// <param name="fileName">文件名</param>
        /// <returns>是否保存成功 true 成功 false 失败</returns>
        public static bool SaveTextToFileEnd(string fileText, string fileName)
        {
            //源文件名或目标文件名未输入时，返回不操作
            if (fileName == null || string.Empty.Equals(fileName))
            {
                throw new FileNotFoundException(fileName + "文件名不存在！");
            }
            //创建目录
            FileUtil.CreateDirectory(fileName);
            
            StreamWriter sw = null;
            FileInfo fileInfo = new FileInfo(fileName);
            //文件不存在
            if (!fileInfo.Exists)
            {
                //创建新文件
                sw = File.CreateText(fileName);
            }
            else
            {
                //增加到文件最后
                sw = new StreamWriter(fileInfo.OpenWrite());
            }

            //保存文件
            sw.BaseStream.Seek(0, SeekOrigin.End);

            sw.Write(fileText);
            sw.Flush();
            sw.Close();

            return true;
        }       
        #endregion

        #region 保存文件到缓存中
        /// <summary>
        /// 保存文件到缓存中
        /// </summary>
        /// <param name="fileText">文件内容</param>
        /// <param name="fileName">文件名</param>
        /// <returns>是否保存成功 true 成功 false 失败</returns>
        public static bool SaveFileToCache(string fileText, string fileName)
        {
            //将读取到的内容保存到缓存中去
            HttpContext.Current.Cache.Add(fileName, fileText, new System.Web.Caching.CacheDependency(fileName), DateTime.MaxValue, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            return true;
        }        
        #endregion

        #region 备份文件
        /// <summary>
        /// 备份文件
        /// 当源文件或者目标文件未输入时，抛出异常
        /// 当源文件和目标文件同名时，返回true
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="isOverWrite">当目标文件存在时是否覆盖 true 覆盖 false 不覆盖</param>
        /// <param name="isDelete">删除源文件标志 true 删除源文件 false 不删除</param>
        /// <returns>操作是否成功 true 成功 false 失败</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool isOverWrite, bool isDelete)
        {
            //源文件名或目标文件名未输入时，返回不操作
            if (sourceFileName == null || string.Empty.Equals(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件名不存在！");
            }
            //源文件名或目标文件名未输入时，返回不操作
            if (destFileName == null || string.Empty.Equals(destFileName))
            {
                throw new FileNotFoundException(destFileName + "文件名不存在！");
            }
            //当源文件和目标文件，路径和文件名一样时，返回
            if (sourceFileName.Equals(destFileName))
            {
                return true;
            }

            //备份源文件是否存在
            if (!File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            //目标文件已经存在，并且不允许覆盖时，返回
            if (!isOverWrite && File.Exists(destFileName))
            {
                return false;
            }
            //备份文件
            try
            {
                //备份文件
                System.IO.File.Copy(sourceFileName, destFileName, true);
                //如果需要删除源文件，删除源文件
                if (isDelete)
                {
                    File.Delete(sourceFileName);
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }        
        #endregion

        #region 从备份文件中恢复文件
        /// <summary>
        /// 从备份文件中恢复文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        /// <param name="backupTargetFileName">备份要被恢复的文件的名称,如果为null,则不再备份恢复文件</param>
        /// <returns>操作是否成功</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                //备份文件是否存在
                if (!System.IO.File.Exists(backupFileName))
                {
                    throw new FileNotFoundException(backupFileName + "文件不存在！");
                }
                //备份源文件是否存在
                if (backupTargetFileName != null)
                {
                    //要恢复文件是否存在
                    if (!System.IO.File.Exists(targetFileName))
                    {
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    }
                    else
                    {
                        File.Copy(targetFileName, backupTargetFileName, true);
                    }
                }
                File.Delete(targetFileName);
                File.Copy(backupFileName, targetFileName);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }
        #endregion

        #region 拷贝文件
        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="isOverWrite">当目标文件存在时是否覆盖 true 覆盖 false 不覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool CopyFile(string sourceFileName, string destFileName, bool isOverWrite)
        {
            return BackupFile(sourceFileName, destFileName, isOverWrite, false);
        }
        #endregion

        #region 删除文件
        /// <summary>
        /// 删除文件
        /// 如果该文件不存在，或者 文件名未输入，输入空时，抛出异常
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool DeleteFile(string fileName)
        {
            //源文件名或目标文件名未输入时，返回不操作
            if (fileName == null || string.Empty.Equals(fileName) || !File.Exists(fileName))
            {
                throw new FileNotFoundException(fileName + "文件名不存在！");
            }
            File.Delete(fileName);
            return true;
        }
        #endregion

        #region 删除文件夹
        /// <summary>
        /// 删除文件夹
        /// 如果该文件夹不存在，或者 文件夹名未输入，输入空时，抛出异常
        /// </summary>
        /// <param name="folderNames">文件夹名</param>
        /// <returns>操作是否成功 true 成功 false 失败</returns>
        public static bool DeleteFolder(string folderName)
        {
            //源文件名或目标文件名未输入时，返回不操作
            if (folderName == null || string.Empty.Equals(folderName) || !Directory.Exists(folderName))
            {
                throw new FileNotFoundException(folderName + "文件夹不存在！");
            }
            //获得文件夹下的文件
            string[] files = Directory.GetFiles(folderName);
            //将文件夹下的文件删除
            for (int i = 0; i < files.Length; i++)
            {
                //文件删除
                if (File.Exists(files[i]))
                {
                    File.Delete(folderName + "\\" + files[i]);
                }
                //文件夹删除
                if (Directory.Exists(files[i]))
                {
                    DeleteFolder(files[i]);
                }                
            }
            //删除文件夹
            Directory.Delete(folderName);

            return true;
        }
        #endregion

        #region 获取文件夹大小
        /// <summary>
        /// 获取文件夹大小
        /// </summary>
        /// <param name="dirPath">文件夹名</param>
        /// <returns></returns>
        public static long GetFolderSize(string dirPath)
        {
            //判断给定的路径是否存在,如果不存在则退出
            if (!Directory.Exists(dirPath))
                return 0;
            //定义返回变量
            long size = 0;
            //定义一个DirectoryInfo对象
            DirectoryInfo direInfo = new DirectoryInfo(dirPath);

            //通过GetFiles方法,获取direInfo目录中的所有文件的大小
            foreach (FileInfo file in direInfo.GetFiles())
            {
                //增加大小
                size += file.Length;
            }

            //获取direInfo中所有的文件夹,并存到一个新的对象数组中,以进行递归
            DirectoryInfo[] folder = direInfo.GetDirectories();
            //子文件夹存在时，获取子文件夹大小
            if (folder.Length > 0)
            {
                //遍历所有子文件夹获取大小
                for (int i = 0; i < folder.Length; i++)
                {
                    //获取子文件夹大小
                    size += GetFolderSize(folder[i].FullName);
                }
            }
            //返回文件夹大小
            return size;
        }
        #endregion

        #region 获取文件的大小
        /// <summary>
        /// 获取文件的大小
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static long FileSize(string filePath)
        {
            //判断当前路径所指向的是否为文件
            if (!File.Exists(filePath))
            {
                //不是文件时，判断文件夹大小
                return GetFolderSize(filePath);
            }
            else
            {
                //定义一个FileInfo对象,使之与filePath所指向的文件向关联,以获取其大小
                FileInfo fileInfo = new FileInfo(filePath);
                //返回文件大小
                return fileInfo.Length;
            }
        }
        #endregion

        #region 获取文件夹文件个数
        /// <summary>
        /// 获取文件夹文件个数
        /// </summary>
        /// <param name="dirPath">文件夹名</param>
        /// <returns></returns>
        public static int GetFolderFileCount(string dirPath)
        {
            //判断给定的路径是否存在,如果不存在则退出
            if (!Directory.Exists(dirPath))
                return 0;
            //定义返回变量
            int count = 0;
            //定义一个DirectoryInfo对象
            DirectoryInfo direInfo = new DirectoryInfo(dirPath);
            //获取文件个数
            count += direInfo.GetFiles().Length;

            //获取direInfo中所有的文件夹,并存到一个新的对象数组中,以进行递归
            DirectoryInfo[] folder = direInfo.GetDirectories();
            //子文件夹存在时，获取子文件夹文件个数
            if (folder.Length > 0)
            {
                //遍历所有子文件夹获取文件个数
                for (int i = 0; i < folder.Length; i++)
                {
                    //获取子文件夹文件个数
                    count += GetFolderFileCount(folder[i].FullName);
                }
            }
            //返回文件夹大小
            return count;
        }
        #endregion
    }
}
