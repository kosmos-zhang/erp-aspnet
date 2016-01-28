/**********************************************
 * 类作用：   解析账号密码字符串、并对密码解密
 * 建立人：   吴成好
 * 建立时间： 2009-01-07 
 * Copyright (C) 2007-2009 吴成好
 * All rights reserved
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Common
{
    public class PasswdUtil
    {

        //账号、密码
        public String[][] ResultArray = new String[2][];

        //解析输入的账号密码字符串，并对密码进行解密，赋值到ResultArray中。
        public void SetPassword(String str)
        {
            String[] TempArray;
            TempArray = str.Split(';');
            this.ResultArray[0] = new String[TempArray.Length];
            this.ResultArray[1] = new String[TempArray.Length];
            for (int i = 0; i < TempArray.Length; i++)
            {
                if (TempArray[i].Length > 0 && TempArray[i].IndexOf(":") > 0&&TempArray[i].IndexOf(":")<TempArray[i].Length)
                {
                    this.ResultArray[0][i] = TempArray[i].Substring(0, TempArray[i].IndexOf(":"));
                    this.ResultArray[1][i] = SecurityUtil.DecryptDES(TempArray[i].Substring(TempArray[i].IndexOf(":") + 1));
                }                
            }
        }

        //根据给定的账号，返回相应密码
        public String GetPassword(String UserName)
        {
            String Password = "";
            for (int i = 0; i < ResultArray.Length;i++ )
            {
                if (this.ResultArray[0][i] == UserName)
                {
                    Password = this.ResultArray[1][i];
                    return Password;
                }
            }
            return Password;
        }

    }
}
