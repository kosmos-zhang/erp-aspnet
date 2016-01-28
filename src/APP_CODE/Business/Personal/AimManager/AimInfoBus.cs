/**********************************************
 * 类作用：   目标信息事务层处理
 * 建立人：   王乾睿
 * 建立时间： 2009.04.08
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

using XBase.Common;
using XBase.Model.Personal.AimManager;
using XBase.Data.Personal.AimManager;

namespace XBase.Business.Personal.AimManager
{
    /// <summary>
    /// 类名：AimInfoBus
    /// 描述：目标信息事务层处理
    /// 作者：王乾睿
    /// 创建时间：2009.4.8
    /// 最后修改时间：2009.4.8
    /// </summary>
    ///
    public class AimInfoBus
    {
        #region 编辑目标信息
        /// <summary>
        /// 编辑目标信息
        /// </summary>
        /// <param name="model">目标信息</param>
        /// <returns></returns>
        public static int SaveAimInfo(AimInfoModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //定义返回变量
            int isSucc = -1;

            //ID存在时，更新
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {
                try
                {
                    //执行更新操作
                    isSucc = AimInfoDBHelper.UpdateAimInfo(model);
                }
                catch 
                {
                    //输出日志

                }
            }
            //插入
            else
            {
                try
                {
                    //执行插入操作
                    isSucc = AimInfoDBHelper.InsertAimInfo(model);
                }
                catch 
                {
                    //输出日志

                }
            }
            return isSucc;
        }

        #endregion

        #region 查询目标列表
        public static DataTable GetAimList(int pageindex, int pagesize, Hashtable parm,out int count)
        {
            int pagecount = 0;
            DataTable aimlisttable = new DataTable();
            try
            {
                //执行更新操作
                aimlisttable = AimInfoDBHelper.SelectAimList(pageindex, pagesize, parm, out pagecount);
             
            }
            catch 
            {
                //输出日志 

            }
            count = pagecount;
            return aimlisttable;
        }

        public static DataTable GetAimList1(int pageindex, int pagesize, Hashtable parm, out int count)
        {
            int pagecount = 0;
            DataTable aimlisttable = new DataTable();
            try
            {
                //执行更新操作
                aimlisttable = AimInfoDBHelper.SelectAimList1(pageindex, pagesize, parm, out pagecount);

            }
            catch 
            {
                //输出日志 

            }
            count = pagecount;
            return aimlisttable;
        }


        public static DataTable GetAimListReportDept(Hashtable parm)
        {
            DataTable aimlisttable = new DataTable();
            try
            {
                //执行更新操作
                aimlisttable = AimInfoDBHelper.SelectAimListReportDept(parm);

            }
            catch 
            {
                //输出日志 

            }
            return aimlisttable;
        }
        public static DataTable GetAimListReportDeptPrint(Hashtable parm,int pageindex,int pagecount,ref int totalcount)
        {
            DataTable aimlisttable = new DataTable();
            try
            {
                //执行更新操作
                aimlisttable = AimInfoDBHelper.SelectAimListReportDeptPrint(parm,pageindex,pagecount,ref totalcount);

            }
            catch 
            {
                //输出日志 

            }
            return aimlisttable;
        }

        public static DataTable GetAimListReportPrincipal(Hashtable parm)
        {

            DataTable aimlisttable = new DataTable();
            try
            {
                //执行更新操作
                aimlisttable = AimInfoDBHelper.GetAimListReportPrincipal(parm);

            }
            catch 
            {
                //输出日志 

            }
            return aimlisttable;
        }

        public static DataTable GetAimListReportPrincipalPrint(Hashtable parm, int pagesize, int pagecount, ref int totalcount)
        {

            DataTable aimlisttable = new DataTable();
            try
            {
                //执行更新操作
                aimlisttable = AimInfoDBHelper.GetAimListReportPrincipalPrint(parm,pagesize,pagecount,ref  totalcount);

            }
            catch 
            {
                //输出日志 

            }
            return aimlisttable;
        }
        
        public static DataTable GetAimListReportStatus(Hashtable parm)
        {

            DataTable aimlisttable = new DataTable();
            try
            {
                //执行更新操作
                aimlisttable = AimInfoDBHelper.GetAimListReportStatus(parm);

            }
            catch 
            {
                //输出日志 

            }
            return aimlisttable;
        }

        public static DataTable GetAimListReportStatus1(Hashtable parm)
        {

            DataTable aimlisttable = new DataTable();
            try
            {
                //执行更新操作
                aimlisttable = AimInfoDBHelper.GetAimListReportStatus1(parm);

            }
            catch 
            {
                //输出日志 

            }
            return aimlisttable;
        }

        public static DataTable GetAimInfoByID(int id)
        {
            DataTable aimlisttable = new DataTable();
            try
            {
                //执行更新操作
                aimlisttable = AimInfoDBHelper.SelectAimInfoById(id);
            }
            catch 
            {
                //输出日志
            }
            return aimlisttable;
        }


        #endregion

        #region  删除目标信息
        public static int DelAimInfo(string[] IdStr)
        {
            int delCount = IdStr.Length;
            try
            {
                //执行更新操作
                if (AimInfoDBHelper.DelAimInfoByIdArray(IdStr) != true)
                    return 0;
            }
            catch 
            {
                return 0;
                //输出日志
            }
            return delCount;
        }
        #endregion

    }
}