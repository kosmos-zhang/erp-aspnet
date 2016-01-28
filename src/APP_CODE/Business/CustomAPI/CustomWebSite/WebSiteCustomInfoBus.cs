/**********************************************
 * 类作用   站点用户数据处理层
 * 创建人   xz
 * 创建时间 2010-3-15 16:11:04 
 ***********************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using XBase.Business.Common;
using XBase.Common;
using XBase.Data.Common;
using XBase.Data.DBHelper;
using XBase.Model.Common;

using XBase.Model.CustomAPI.CustomWebSite;
using XBase.Data.CustomAPI.CustomWebSite;


namespace XBase.Business.CustomAPI.CustomWebSite
{
    /// <summary>
    /// 站点用户业务类
    /// </summary>
    public class WebSiteCustomInfoBus
    {
        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD"></param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int iD)
        {
            return WebSiteCustomInfoDBHelper.SelectDataTable(iD);
        }

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="customID">往来单位ID</param>
        /// <param name="loginUserName">站点登陆用户名</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int customID, string loginUserName)
        {
            return WebSiteCustomInfoDBHelper.SelectDataTable(customID, loginUserName);
        }

        /// <summary>
        /// 列表界面查询方法
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页记录数</param>
        /// <param name="orderBy">排序方法</param>
        /// <param name="TotalCount">总记录数</param>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public static DataTable SelectListData(int pageIndex, int pageCount, string orderBy, ref int TotalCount
            , WebSiteCustomInfoModel model, string CompanyCD)
        {
            return WebSiteCustomInfoDBHelper.SelectListData(pageIndex, pageCount, orderBy, ref TotalCount, model, CompanyCD);
        }

        /// <summary>
        /// 插入数据记录
        /// </summary>
        ///<param name="model">实体类</param>
        /// <returns>插入数据是否成功:true成功,false不成功</returns>
        public static bool Insert(WebSiteCustomInfoModel model)
        {
            ArrayList sqlList = new ArrayList();
            SqlCommand cmd = WebSiteCustomInfoDBHelper.InsertCommand(model);

            sqlList.Add(cmd);

            if (SqlHelper.ExecuteTransWithArrayList(sqlList))
            {
                int i = 0;
                if (int.TryParse(cmd.Parameters["@IndexID"].Value.ToString(), out i))
                {
                    model.ID = i;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改数据记录
        /// </summary>
        ///<param name="model">实体类</param>
        /// <returns>修改数据是否成功:true成功,false不成功</returns>
        public static bool Update(WebSiteCustomInfoModel model)
        {
            return WebSiteCustomInfoDBHelper.Update(model);
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">ID集合</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(string iD)
        {
            return WebSiteCustomInfoDBHelper.Delete(iD);
        }

        /// <summary>
        /// 根据用户名和密码查询用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public static DataTable Login(string userName, string passWord)
        {
            return WebSiteCustomInfoDBHelper.Login(userName, passWord);
        }

        /// <summary>
        /// 读取用户信息
        /// </summary>
        /// <param name="userName">登陆用户名</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetUserInfo(string userName, string CompanyCD)
        {
            return WebSiteCustomInfoDBHelper.GetUserInfo(userName, CompanyCD);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userID">登陆用户ID</param>
        /// <param name="OldPwd">旧登陆密码</param>
        /// <param name="NewPwd">新登陆密码</param>
        /// <returns></returns>
        public static Hashtable SetUserInfo(int userID, string OldPwd, string NewPwd)
        {
            Hashtable ht = new Hashtable();
            DataTable dt = WebSiteCustomInfoDBHelper.SelectDataTable(userID);
            if (dt.Rows.Count < 1)
            {
                ht.Add("0", "用户不存在!");
            }
            else
            {
                if (dt.Rows[0]["LoginPassword"].ToString() != OldPwd)
                {
                    ht.Add("0", "原密码错误!");
                }
                else
                {
                    WebSiteCustomInfoModel model = new WebSiteCustomInfoModel();
                    model.ID = userID;
                    model.LoginPassword = NewPwd;
                    if (WebSiteCustomInfoDBHelper.UpdatePassWord(model))
                    {
                        ht.Add("1", "修改成功!");
                    }
                    else
                    {
                        ht.Add("0", "修改失败!");
                    }
                }
            }
            return ht;
        }


        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool SetPassword(WebSiteCustomInfoModel item)
        {
            return Data.CustomAPI.CustomWebSite.WebSiteCustomInfoDBHelper.UpdatePassWord(item);
        }


        /// <summary>
        /// 判断用户名是否已经存在
        /// </summary>
        /// <param name="Name">用户名</param>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        public static bool ExisitName(string Name, int? ID)
        {
            return WebSiteCustomInfoDBHelper.ExisitName(Name, ID);
        }
    }
}