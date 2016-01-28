using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;
using System.Collections;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.StorageManager;

namespace XBase.Business.Office.StorageManager
{
    public class StorageMonthlyBus
    {
        /// <summary>
        /// 为所有有效期内的公司做月结
        /// </summary>
        public static string StorageMonthEnd()
        {
            string Info = string.Empty;
            int Succ = 0;
            int defeat = 0;
            DataTable dt = new DataTable();
            dt = StorageMonthly.GetCompanyInfo();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        if (StorageMonthly.StorageMonthEndForCompany(dt.Rows[i]["CompanyCD"].ToString()))
                        {
                            Succ += 1;
                        }
                        else
                        {
                            defeat += 1;
                        }
                    }
                    catch (Exception ex)
                    {

                        WriteSystemLog(ex);
                    }
                }
                Info = "成功月结" + Succ + "家公司，失败" + defeat + "家公司";

            }
            else
            {
                Info = "系统没有公司";
            }
            return Info;
        }

        #region 输出系统日志
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">异常信息</param>
        private static void WriteSystemLog(Exception ex)
        {
            /* 
             * 出现异常时，输出系统日志到文本文件 
             * 考虑出现异常情况比较少，尽管一个方法可能多次异常，
             *      但还是考虑将异常日志的变量定义放在catch里面
             */
            //定义变量
            LogInfo logSys = new LogInfo();
            //设置日志类型 需要指定为系统日志
            logSys.Type = LogInfo.LogType.SYSTEM;
            //指定系统日志类型 出错信息
            logSys.SystemKind = LogInfo.SystemLogKind.SYSTEM_ERROR;
            ////指定登陆用户信息
            //logSys.UserInfo = userInfo;
            //设定模块ID
            logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_SHIFT_EDIT;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion

    }
}
