using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
namespace XBase.Common
{
    public class ReportUtil
    {
        private static string _LoginName = string.Empty;
        private static string _LoginPwd = string.Empty;
        private static string _DataBase = string.Empty;
        private static string _Server = string.Empty;
        //登陆用户名
        public static string LoginName
        {
    
            get {
                if (_LoginName == string.Empty)
                {
                    _LoginName = ConfigurationManager.ConnectionStrings["CrystalLoginName"].ToString();
                    if (ConfigurationManager.AppSettings["enableEncryptConnectionString"] == "1")
                    {
                        _LoginName = XBase.Common.SecurityUtil.DecryptDES(_LoginName);
                    }
                }
                return _LoginName;
            }
        }
        //登陆密码
        public static string LoginPwd
        {
            get
            {
                if (_LoginPwd == string.Empty)
                {
                    _LoginPwd = ConfigurationManager.ConnectionStrings["CrystalLoginPwd"].ToString();
                    if (ConfigurationManager.AppSettings["CrystalLoginPwd"] == "1")
                    {
                        _LoginPwd = XBase.Common.SecurityUtil.DecryptDES(_LoginPwd);
                    }
                }
                return _LoginPwd;
            }
        }
        //连接数据库
        public static string DataBase
        {
            get
            {
                if (_DataBase == string.Empty)
                {
                    _DataBase = ConfigurationManager.ConnectionStrings["CrystalDataBase"].ToString();
                    if (ConfigurationManager.AppSettings["CrystalDataBase"] == "1")
                    {
                        _DataBase = XBase.Common.SecurityUtil.DecryptDES(_DataBase);
                    }
                }
                return _DataBase;
            }
        }
       //连接数据库服务器
        public static string Server
        {
            get
            {
                if (_Server == string.Empty)
                {
                    _Server = ConfigurationManager.ConnectionStrings["CrystalServer"].ToString();
                    if (ConfigurationManager.AppSettings["CrystalServer"] == "1")
                    {
                        _Server = XBase.Common.SecurityUtil.DecryptDES(_Server);
                    }
                }
                return _Server;
            }
        }

        //#region 返回TableLogOnInfo对象
        //public static TableLogOnInfo GetTableLogOnInfo(string TableName)
        //{
        //    TableLogOnInfo tmp;
        //    tmp = new TableLogOnInfo();
        //    tmp.ConnectionInfo.ServerName = ConfigurationManager.ConnectionStrings["CrystalServer"].ToString();//数据库服务器的名称或IP   
        //    tmp.ConnectionInfo.DatabaseName = ConfigurationManager.ConnectionStrings["CrystalDataBase"].ToString();//数据库名称   
        //    tmp.ConnectionInfo.UserID = ConfigurationManager.ConnectionStrings["CrystalLoginName"].ToString();//登入数据库的用户名   
        //    tmp.ConnectionInfo.Password = ConfigurationManager.ConnectionStrings["CrystalLoginPwd"].ToString();//密码   
        //    tmp.TableName = TableName;//表名 
        //    return tmp;
        //}
        //#endregion
    }
}
