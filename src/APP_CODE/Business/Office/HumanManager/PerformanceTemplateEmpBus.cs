using System;
using XBase.Model.Office.HumanManager;
using XBase.Data.Office.HumanManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;
using System.Collections .Generic ;

namespace XBase.Business.Office.HumanManager
{
  public   class PerformanceTemplateEmpBus
    {
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns>DataTable 部门信息</returns>
        public static DataTable GetDeptInfo()
        {
            string companyCD = string.Empty;
            //获取公司代码
            try
            {
                companyCD = "AAAAAA";
                //companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                companyCD = "AAAAAA";
            }
            //查询部门信息
            DataTable dtDept = UserDeptSelectDBHelper.GetDeptInfo(companyCD);
            //部门信息不存在时，返回
            if (dtDept == null || dtDept.Rows.Count < 1) return dtDept;

            //定义返回的部门信息变量
            DataTable dtReturn = new DataTable();
            //复制部门信息表结构
            dtReturn = dtDept.Clone();

            #region 部门信息排序处理

            //获取第一级部门信息
            DataRow[] drSuperDept = dtDept.Select("SuperDeptID IS NULL");
            //遍历第一级部门
            for (int i = 0; i < drSuperDept.Length; i++)
            {
                DataRow drFirstDept = (DataRow)drSuperDept[i];
                //获取部门ID
                int deptID = (int)drFirstDept["ID"];
                //替换部门名称内容
                drFirstDept["DeptName"] =  drFirstDept["DeptName"].ToString();
                //导入第一级部门
                dtReturn.ImportRow(drFirstDept);
                //设定子部门
                dtReturn = ReorderDeptRow(dtReturn, deptID, dtDept, 1);
            }

            #endregion

            return dtReturn;
        }
        private static DataTable ReorderDeptRow(DataTable dtReturn, int deptID, DataTable dtDept, int align)
        {
            //获取部门的子部门
            DataRow[] drSubDept = dtDept.Select("SuperDeptID = " + deptID);

            //遍历所有子部门
            for (int i = 0; i < drSubDept.Length; i++)
            {
                //通过对齐位置，来控制该部门前空格数
                string alignPosition = string.Empty;
                for (int j = 0; j < align; j++)
                {
                    alignPosition += "&nbsp;&nbsp;";
                }
                //获取子部门数据
                DataRow drSubDeptTemp = (DataRow)drSubDept[i];
                //获取子部门ID
                int subDeptID = (int)drSubDeptTemp["ID"];
                drSubDeptTemp["DeptName"] = alignPosition  + drSubDeptTemp["DeptName"].ToString();
                //导入子部门
                dtReturn.ImportRow(drSubDeptTemp);
                //生成子部门的子部门信息
                dtReturn = ReorderDeptRow(dtReturn, subDeptID, dtDept, align + 1);
            }
            return dtReturn;
        }
        public static DataTable SearchRectCheckElemInfo(PerformanceTemplateModel   model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询

            return PerformanceTemplateEmpDBHelper.SearchCheckElemInfo(model);

        }
        public static bool InsertPerformenceTempElm(IList<PerformanceTemplateEmpModel> modeList, string[] templateList)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            bool isSucc = false;
            //操作日志
            LogInfoModel logModel = InitLogInfo(modeList[0].ModifiedUserID     );
         
            //更新
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(modeList [0].EditFlag ))
            {
                try
                {
                    logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                    //执行更新操作
                   isSucc = PerformanceTemplateEmpDBHelper.UpdatePerformenceTempElm(modeList, templateList);
                    logModel.ObjectID = modeList[0].ModifiedUserID;
                }
                catch (Exception ex)
                {
                    //输出系统日志
                    WriteSystemLog(userInfo, ex);
                }
            }
            //插入
            else
            {
                try
                {
                    logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                    //执行插入操作
                    isSucc = PerformanceTemplateEmpDBHelper.InsertPerformenceTempElm(modeList);
                    logModel.ObjectID = modeList[0].ModifiedUserID;
                }
                catch (Exception ex)
                {
                    //输出系统日志
                    WriteSystemLog(userInfo, ex);
                }
            }
            //更新成功时
            if (isSucc)
            {
                //设置操作成功标识
                logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            //更新不成功
            else
            {
                //设置操作成功标识 
                logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
            }

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;







        }
       
        private static LogInfoModel InitLogInfo(string no)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID
            logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_PERFORMANCECHECK;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_PERFORMANCETEMPLATEEMP;
            //操作单据编号
            logModel.ObjectID = no;

            return logModel;

        }
        #region 输出系统日志
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">异常信息</param>
        private static void WriteSystemLog(UserInfoUtil userInfo, Exception ex)
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
            //指定登陆用户信息
            logSys.UserInfo = userInfo;
            //设定模块ID
            logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_PERFORMANCECHECK;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion 
        public static DataTable SearchFlowInfo(PerformanceTemplateEmpModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询

            return PerformanceTemplateEmpDBHelper.SearchFlowInfo (model);

        }
        public static DataTable GetEmployeeInfo(PerformanceTemplateEmpModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //执行查询

            return PerformanceTemplateEmpDBHelper.GetEmployeeInfo (model);

        }
        public static bool DeletePerTemplateEmpInfo(string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行删除操作
            bool isSucc = PerformanceTemplateEmpDBHelper.DeletePerTemplateEmpInfo (ID, companyCD);
            //定义变量
            string remark;
            //成功时
            if (isSucc)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }

            //操作日志
            LogInfoModel logModel = InitLogInfo(ID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;
        }

    }
}
