/**********************************************
 * 类作用：   执行DOS命令
 * 建立人：   吴成好
 * 建立时间： 2009-01-07 
 * Copyright (C) 2007-2009 吴成好
 * All rights reserved
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace XBase.Common
{
    public class CmdUtil
    {
        
        /// 
        /// 执行cmd.exe命令
        /// 
        ///命令文本
        /// 命令输出文本
        public static string ExeCommand(string commandText)
        {
            return ExeCommand(new string[] { commandText });
        }

        /// 
        /// 执行多条cmd.exe命令
        /// 
        ///命令文本数组
        /// 命令输出文本        
        public static string ExeCommand(string[] commandTexts)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            string strOutput = null;
            try
            {
                p.Start();
                foreach (string item in commandTexts)
                {
                    p.StandardInput.WriteLine(item);
                }
                p.StandardInput.WriteLine("exit");
                strOutput = p.StandardOutput.ReadToEnd();
                //strOutput = Encoding.UTF8.GetString(Encoding.Default.GetBytes(strOutput));
                p.WaitForExit();
                p.Close();
            }
            catch (Exception e)
            {
                strOutput = e.Message;
            }
            return strOutput;

        }
    }
}
