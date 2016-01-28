/**********************************************
 * 类作用：   机构岗位表格操作
 * 建立人：   吴志强
 * 建立时间： 2009/04/13
 ***********************************************/
using System;
using XBase.Model.Office.HumanManager;
using XBase.Data.Office.HumanManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;
using System.Text;
using System.Collections.Generic;
namespace XBase.Business.Office.HumanManager
{
    /// <summary>
    /// 类名：DeptQuarterBus
    /// 描述：机构岗位表格操作
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/13
    /// 最后修改时间：2009/04/13
    /// </summary>
    ///
    public class DeptQuarterBus
    {
        public static bool SaveQuarterSet(IList<QuterModuleSetModel> QuterModuleSetList   )
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           
            //定义返回变量
            bool isSucc = false;
                   
                    //执行插入操作
            isSucc = DeptQuarterDBHelper.SaveQuarterSet(QuterModuleSetList);
               
            return isSucc;
        }


        #region 编辑机构岗位信息
        /// <summary>
        /// 编辑机构岗位信息
        /// </summary>
        /// <param name="model">保存信息</param>
        /// <returns></returns>
        public static bool SaveDeptQuarterInfo(DeptQuarterModel model)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            model.ModifiedUserID = userInfo.UserID;
            //定义返回变量
            bool isSucc = false;
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.QuarterNo);

            //更新
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {
                try
                {
                    logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                    //执行更新操作
                    isSucc = DeptQuarterDBHelper.UpdateDeptQuarterInfo(model);
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
                    isSucc = DeptQuarterDBHelper.InsertDeptQuarterInfo(model);
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
        #endregion

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
            logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_DEPTQUARTER_EDIT;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion

        public static DataTable GetQuarterModelSet(string DeptID, string QuarterNo)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string CompanyCD = userInfo.CompanyCD;
            return DeptQuarterDBHelper.GetQuarterModelSet(DeptID, QuarterNo, CompanyCD);
        }



        #region 通过ID查询机构岗位信息
        /// <summary>
        /// 查询机构岗位信息
        /// </summary>
        /// <param name="quarterID">机构岗位ID</param>
        /// <returns></returns>
        public static DataTable GetDeptQuarterInfoWithID(string quarterID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string  CompanyCD = userInfo.CompanyCD;
            return DeptQuarterDBHelper.GetDeptQuarterInfoWithID(quarterID, CompanyCD);
        }
        #endregion


        public static DataTable GetQuarterDescrible( )
        {
            
            
            return DeptQuarterDBHelper.GetQuarterDescrible();
        }

        public static DataTable selectQuarterDescrible(string DescibleID)
        {


            return DeptQuarterDBHelper.selectQuarterDescrible(DescibleID);
        }


        public static DataTable selectQuarterSet(string DescibleID)
        {


            return DeptQuarterDBHelper.selectQuarterSet(DescibleID);
        }

        #region 通过检索条件查询机构岗位信息
        /// <summary>
        /// 查询机构岗位信息
        /// </summary>
        /// <param name="quarterID">机构岗位ID</param>
        /// <returns></returns>
        public static string GetQuarterInfoWithID(string quarterID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            string CompanyCD = userInfo.CompanyCD;

            //执行查询
            DataTable dtQuarterInfo = DeptQuarterDBHelper.SearchQuarterInfo(quarterID,CompanyCD );
            //返回
            return GetQuarterTreeInfo(dtQuarterInfo);
        }
        #endregion

        #region 通过检索条件查询机构岗位信息
        /// <summary>
        /// 查询机构岗位信息
        /// </summary>
        /// <param name="quarterID">机构岗位ID</param>
        /// <returns></returns>
        public static DataTable GetSubQuarterInfoWithID(string quarterID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码

            string CompanyCD = userInfo.CompanyCD;
            //执行查询
            DataTable dtQuarterInfo = DeptQuarterDBHelper.SearchQuarterInfo(quarterID,CompanyCD );
            //返回
            return dtQuarterInfo;
        }
        #endregion

        #region 删除机构岗位信息
        /// <summary>
        /// 删除机构岗位信息
        /// </summary>
        /// <param name="quarterID">组织机构ID</param>
        /// <returns></returns>
        public static bool DeleteDeptQuarterInfo(string quarterID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码

            string CompanyCD = userInfo.CompanyCD;
            //执行删除操作
            bool isSucc = DeptQuarterDBHelper.DeleteQuarterInfo(quarterID,CompanyCD );
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
            LogInfoModel logModel = InitLogInfo(quarterID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;
        }
        #endregion

        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <returns></returns>
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
            logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_DEPTQUARTER_EDIT;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_DEPTQUARTER;
            //操作单据编号
            logModel.ObjectID = no;

            return logModel;

        }
        #endregion

        #region 通过机构ID获取岗位信息
        /// <summary>
        /// 通过机构ID获取岗位信息
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public static string GetQuarterInfoWithDeptID(string deptID, string quarterID)
        {
            //变量定义
            StringBuilder sbDeptQuarterInfo = new StringBuilder();
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //获取机构岗位信息
            DataTable dtDeptQuarterInfo = DeptQuarterDBHelper.GetQuarterInfoWithDeptID(deptID,quarterID, companyCD);
            //机构岗位数据存在时
            if (dtDeptQuarterInfo != null && dtDeptQuarterInfo.Rows.Count > 0)
            {
                //生成岗位信息
                sbDeptQuarterInfo.Append(GetQuarterTreeInfo(dtDeptQuarterInfo));
            }
            //机构岗位信息不存在时 
            else
            {
                //生成子树代码
                sbDeptQuarterInfo.Append("<table border='0' cellpadding='0' cellspacing='0'><tr><td>");
                sbDeptQuarterInfo.Append("<div id='divDQuarterName_" + deptID + "' onclick=\"SetSelectValue('','','','" + deptID + "');\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>");
                sbDeptQuarterInfo.Append("</td></tr></table>");
            }

            //返回机构岗位信息
            return sbDeptQuarterInfo.ToString();
        }
        #endregion

        #region 初期表示机构信息
        /// <summary>
        /// 初期表示机构信息
        /// </summary>
        public static string InitDeptQuarterInfo()
        {
            //定义变量
            StringBuilder sbDeptQuarterInfo = new StringBuilder();
            //获取机构信息
            DataTable dtDeptInfo = GetDeptInfo();
            //机构信息存在
            if (dtDeptInfo != null && dtDeptInfo.Rows.Count > 0)
            {
                //表格开始
                sbDeptQuarterInfo.AppendLine("<table border='0' cellpadding='0' cellspacing='0'>");
                //遍历所有机构信息，获取岗位信息
                for (int i = 0; i < dtDeptInfo.Rows.Count; i++)
                {
                    //行开始
                    sbDeptQuarterInfo.AppendLine("<tr>");
                    //获取机构ID
                    string deptID = GetSafeData.GetStringFromInt(dtDeptInfo.Rows[i], "ID");
                    //获取机构名称
                    string deptName = GetSafeData.ValidateDataRow_String(dtDeptInfo.Rows[i], "DeptName");
                    //获取机构岗位信息
               //     string deptQuarterInfo = GetQuarterInfoWithDeptID(deptID);
                    // 机构岗位信息设置
                    sbDeptQuarterInfo.AppendLine("<td ><div id='divDeptName_" + deptID + "' onMouseOver=\"getColor(this.id)\"   style=\"cursor:pointer; font-size:13px\" onclick=\"GetInfoByDeptID('" + deptID + "')\">" + deptName + "</div></td>");
                 //   sbDeptQuarterInfo.AppendLine("<td><div id='divDept_" + deptID + "'>" + deptQuarterInfo + "</div></td>");

                    //行结束
                    sbDeptQuarterInfo.AppendLine("</tr>");
                }
                //表格结束
                sbDeptQuarterInfo.AppendLine("</table>");
            }
            //不存在时，提示错误信息
            else
            {
                sbDeptQuarterInfo.AppendLine("您还没有录入组织机构信息");
            }

            //显示机构岗位信息
            return sbDeptQuarterInfo.ToString();
        }
        #endregion

        #region 获取机构信息
        /// <summary>
        /// 获取机构信息
        /// </summary>
        /// <returns></returns>
        private static DataTable GetDeptInfo()
        {
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //查询机构信息
            DataTable dtDept = UserDeptSelectDBHelper.GetDeptInfo(companyCD);
            //机构信息不存在时，返回
            if (dtDept == null || dtDept.Rows.Count < 1) return dtDept;

            //定义返回的机构信息变量
            DataTable dtReturn = new DataTable();
            //复制机构信息表结构
            dtReturn = dtDept.Clone();

            #region 机构信息排序处理
            //获取第一级机构信息
            DataRow[] drSuperDept = dtDept.Select("SuperDeptID IS NULL");
            //遍历第一级机构
            for (int i = 0; i < drSuperDept.Length; i++)
            {
                DataRow drFirstDept = (DataRow)drSuperDept[i];
                //获取机构ID
                int deptID = (int)drFirstDept["ID"];
                //导入第一级机构
           

                DataRow[] drSubDept = dtDept.Select("SuperDeptID = " + deptID);
                if (drSubDept.Length == 0)
                {
                    drFirstDept["DeptName"] = "<img  src =\"../../../Images/BaseDataTree/file.gif\" style=\" padding-left:15px\"/>" + drFirstDept["DeptName"];


                    dtReturn.ImportRow(drFirstDept);
                }
                else
                {
                    drFirstDept["DeptName"] = "<img  src =\"../../../Images/BaseDataTree/folderopen.gif\" style=\" padding-left:15px\"/>" + drFirstDept["DeptName"];
                    dtReturn.ImportRow(drFirstDept);
                    dtReturn = ReorderDeptRow(dtReturn, deptID, dtDept, 1);
                }
                //设定子机构
           
            }
            #endregion

            return dtReturn;
        }
        #endregion

        #region 机构信息排序处理
        /// <summary>
        /// 机构信息排序处理
        /// 获取机构的子机构信息，支持无限级的子机构
        /// </summary>
        /// <param name="dtReturn">返回的数据集</param>
        /// <param name="deptID">机构ID</param>
        /// <param name="dtDept">机构信息</param>
        /// <param name="align">对齐位置</param>
        /// <returns></returns>
        private static DataTable ReorderDeptRow(DataTable dtReturn, int deptID, DataTable dtDept, int align)
        {
            //获取机构的子机构
            DataRow[] drSubDept = dtDept.Select("SuperDeptID = " + deptID);
            //遍历所有子机构
            for (int i = 0; i < drSubDept.Length; i++)
            {
                //通过对齐位置，来控制该机构前空格数
                string alignPosition = string.Empty;
                for (int j = 0; j < align; j++)
                {
                    alignPosition += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                //获取子机构数据
                DataRow drSubDeptTemp = (DataRow)drSubDept[i];
                //获取子机构ID
                int subDeptID = (int)drSubDeptTemp["ID"];
           //     drSubDeptTemp["DeptName"] = alignPosition + drSubDeptTemp["DeptName"];


                DataRow[] temp = dtDept.Select("SuperDeptID = " + subDeptID);
                if (temp.Length == 0)
                {
                    drSubDeptTemp["DeptName"] = alignPosition + "<img  src =\"../../../Images/BaseDataTree/file.gif\" style=\" padding-left:15px\"/>" + drSubDeptTemp["DeptName"];
                    dtReturn.ImportRow(drSubDeptTemp);
                }
                else
                {
                    drSubDeptTemp["DeptName"] = alignPosition + "<img  src =\"../../../Images/BaseDataTree/folderopen.gif\" style=\" padding-left:15px\"/>" + drSubDeptTemp["DeptName"];
                    dtReturn.ImportRow(drSubDeptTemp);
                    dtReturn = ReorderDeptRow(dtReturn, subDeptID, dtDept, align + 1);
                }



                ////导入子机构
                //dtReturn.ImportRow(drSubDeptTemp);
                ////生成子机构的子机构信息
                //dtReturn = ReorderDeptRow(dtReturn, subDeptID, dtDept, align + 1);
            }
            return dtReturn;
        }
        #endregion

        #region 获取岗位信息树
        /// <summary>
        /// 获取岗位信息树
        /// </summary>
        /// <param name="dtDeptQuarterInfo">岗位信息</param>
        /// <returns></returns>
        private static string GetQuarterTreeInfo(DataTable dtDeptQuarterInfo)
        {
            //定义变量
            StringBuilder sbDeptQuarterInfo = new StringBuilder();
            //数据存在时
            if (dtDeptQuarterInfo != null && dtDeptQuarterInfo.Rows.Count > 0)
            {
                //获取记录数
                int deptQuarterCount = dtDeptQuarterInfo.Rows.Count;
                //遍历所有机构岗位，以显示数据
                for (int i = 0; i < dtDeptQuarterInfo.Rows.Count; i++)
                {
                    //获取岗位ID
                    string quarterID = GetSafeData.GetStringFromInt(dtDeptQuarterInfo.Rows[i], "ID");
                    //获取岗位名称
                    string quarterName = GetSafeData.ValidateDataRow_String(dtDeptQuarterInfo.Rows[i], "QuarterName");
                    //获取父岗位
                    string superQuarterID = GetSafeData.GetStringFromInt(dtDeptQuarterInfo.Rows[i], "SuperQuarterID");
                    //获取所属机构
                    string deptID = GetSafeData.GetStringFromInt(dtDeptQuarterInfo.Rows[i], "DeptID");
                    //获取是否有子岗位
                    int subCount = GetSafeData.ValidateDataRow_Int(dtDeptQuarterInfo.Rows[i], "SubCount");

                    //样式名称
                    string className = "file";
                    //Javascript事件名
                    string jsAction = "";
                    //样式表名称
                    string showClass = "list";

                    //有子结点时
                    if (subCount > 0)
                    {
                        //最后一个结点
                        if (i == deptQuarterCount - 1)
                        {
                            className = "folder_close_end";
                            showClass = "list_last";
                        }
                        else
                        {
                            className = "folder_close";
                        }
                        jsAction = "onclick=\"ShowDeptQuarterTree('" + quarterID + "');\"";
                    }
                    else if (i == deptQuarterCount - 1)
                    {
                        className = "file_end";
                    }
                    //生成子树代码
                    sbDeptQuarterInfo.Append("<table border='0' cellpadding='0' cellspacing='0'>");
                    sbDeptQuarterInfo.Append("<tr><td><div id='divQSuper_" + quarterID + "' class='" + className + "' " + jsAction
                                    + " alt='" + quarterName + "'><a  href='#' onclick=\"SetSelectValue('"
                                    + quarterID + "','" + quarterName + "','" + superQuarterID + "','"
                                    + deptID + "');\"><div id='divQuarterName_" + quarterID
                                    + "'>" + " " + quarterName + "</div></a></div>");
                    sbDeptQuarterInfo.Append("<div id='divQSub_" + quarterID + "' style='display:none;' class='" + showClass + "'></div></td></tr>");
                    sbDeptQuarterInfo.Append("</table>");
                }
            }
            //返回
            return sbDeptQuarterInfo.ToString();
        }
        #endregion

        #region 获取公司岗位信息
        /// <summary>
        /// 获取公司岗位信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetQuarterInfoWithCompanyCD()
        {
            //公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return DeptQuarterDBHelper.GetQuarterInfoWithCompanyCD(companyCD);
        }
        #endregion

        #region 获取机构岗位分类的方法
        /// <summary>
        /// 获取机构岗位分类的方法
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetQuarterType()
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];            
            return DeptQuarterDBHelper.GetQuarterType(userInfo.CompanyCD);
        }
        #endregion
    }
}
