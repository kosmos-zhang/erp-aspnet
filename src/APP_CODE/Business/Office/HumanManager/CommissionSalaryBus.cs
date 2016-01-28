/**********************************************
 * 类作用：   提成工资录入
 * 建立人：   吴志强
 * 建立时间： 2009/05/14
 ***********************************************/
using System;
using System.Text;
using XBase.Model.Office.HumanManager;
using XBase.Data.Office.HumanManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;
using System.Collections;

namespace XBase.Business.Office.HumanManager
{
    /// <summary>
    /// 类名：CommissionSalaryBus
    /// 描述：提成工资录入
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/14
    /// 最后修改时间：2009/05/14
    /// </summary>
    ///
    public class CommissionSalaryBus
    {
        #region 编辑提成工资项信息
        /// <summary>
        /// 编辑提成工资项信息
        /// </summary>
        /// <param name="lstEdit">提成工资项信息</param>
        /// <returns></returns>
        public static bool SaveCommissionSalaryInfo(ArrayList lstEdit)
        {
            //定义返回变量
            bool isSucc = true;
            //信息存在时，进行操作
            if (lstEdit != null && lstEdit.Count > 0)
            {
                //获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //执行保存操作
                try
                {
                    //执行保存操作
                    isSucc = CommissionSalaryDBHelper.SaveCommissionSalaryInfo(lstEdit, userInfo.CompanyCD, userInfo.UserID);
                }
                catch (Exception ex)
                {
                    //输出系统日志
                    WriteSystemLog(userInfo, ex);
                }
                //操作日志
                LogInfoModel logModel = InitLogInfo(userInfo.CompanyCD);
                //设置关键元素
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

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
            }

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
            logSys.ModuleID = ConstUtil.MODULE_ID_HUMAN_SALARY_INPUT;
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion
        public static bool DeletePerTypeInfo(string elemID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = userInfo.CompanyCD;
            //执行删除操作
            bool isSucc = CommissionSalaryDBHelper.DeletePerTypeInfo(elemID, companyCD);
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
            LogInfoModel logModel = InitLogInfo(elemID);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);

            return isSucc;
        }
        #region 查询提成工资项信息
        /// <summary>
        /// 查询提成工资项信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <param name="itemNo">提成项目编号</param>
        /// <returns></returns>
        public static string GetCommissionSalaryInfo(EmployeeSearchModel model, string itemNo)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //获取满足条件的人员信息查询
            DataTable dtEmplInfo = CommissionSalaryDBHelper.GetEmplInfo(model, itemNo);

            //变量定义
            StringBuilder sbCommissionSalaryInfo = new StringBuilder();
            //数据存在时，设置数据
            if (dtEmplInfo != null && dtEmplInfo.Rows.Count > 0)
            {
                //设置记录数
                model.RecordCount = dtEmplInfo.Rows.Count.ToString();
                //遍历员工获取提成工资信息
                for (int i = 0; i < dtEmplInfo.Rows.Count; i++)
                {
                    //获取人员ID
                    string emplID = GetSafeData.GetStringFromInt(dtEmplInfo.Rows[i], "EmployeeID");
                    //获取人员提成工资
                    DataTable dtCommItemInfo = CommissionSalaryDBHelper.GetCommissionItemInfo(model.CompanyCD
                                                            , emplID, itemNo, model.StartDate, model.EndDate);

                    //变量定义
                    decimal totalMoney = 0;
                    int commItemCount = 0;
                    int totalRowCount = 0;
                    StringBuilder sbEmplFirstRow = new StringBuilder();
                    StringBuilder sbEmplNotFirstRow = new StringBuilder();
                    //获取工资项目总数
                    if (dtCommItemInfo != null && dtCommItemInfo.Rows.Count > 0) commItemCount = dtCommItemInfo.Rows.Count;

                    #region 生成每个提成项目的具体信息
                    //遍历工资项
                    for (int j = 0; j < commItemCount; j++)
                    {
                        //变量定义
                        decimal timeTotalMoney = 0;
                        StringBuilder sbTimeFirstRow = new StringBuilder();
                        StringBuilder sbTimeNotFirstRow = new StringBuilder();
                        //获取工资项编号
                        string commItemNo = GetSafeData.ValidateDataRow_String(dtCommItemInfo.Rows[j], "ItemNo");
                        //获取工资内容
                        DataTable dtCommissionSalaryInfo = CommissionSalaryDBHelper.GetCommissionSalaryInfo(model.CompanyCD
                                                                , emplID, commItemNo, model.StartDate, model.EndDate);
                        //获取工资内容总数
                        int commSalaryCount = 0;
                        if (dtCommissionSalaryInfo != null && dtCommissionSalaryInfo.Rows.Count > 0) commSalaryCount = dtCommissionSalaryInfo.Rows.Count;
                        //提成率
                        decimal rate = decimal.Parse(StringUtil.TrimZero(GetSafeData.GetStringFromDecimal(dtCommItemInfo.Rows[j], "Rate"))) * 100;

                        StringBuilder sbItemTemp = new StringBuilder();

                        //
                        //工资编号
                        sbItemTemp.AppendLine("<td class='tdColInputCenter'  rowspan='" + commSalaryCount.ToString() + "'>"
                            + "<input type='hidden' id='txtSalaryNo_" + (i + 1).ToString() + "_" + (j + 1).ToString() + "' value='" + commItemNo + "' />"
                            + "<input type='hidden' id='txtSalaryCount_" + (i + 1).ToString() + "_" + (j + 1).ToString() + "' value='"
                                        + commSalaryCount + "' />"
                            + GetSafeData.ValidateDataRow_String(dtCommItemInfo.Rows[j], "ItemName") + "</td>");
                        //单价
                        sbItemTemp.AppendLine("<td class='tdColInputCenter'  rowspan='" + commSalaryCount.ToString() + "' id='tdUnitPrice_"
                                                                + (i + 1).ToString() + "_" + (j + 1).ToString() + "' >" + rate + "</td>");
                        //第一行时
                        if (j == 0)
                        {
                            sbEmplFirstRow.AppendLine(sbItemTemp.ToString());
                        }
                        else
                        {
                            //行开始
                            sbEmplNotFirstRow.AppendLine("<tr>");
                            sbEmplNotFirstRow.AppendLine(sbItemTemp.ToString());
                        }
                        //遍历所有项目获取具体
                        for (int x = 0; x < commSalaryCount; x++)
                        {
                            StringBuilder sbTemp = new StringBuilder();
                            //时长
                            string timeCount = StringUtil.TrimZero(GetSafeData.GetStringFromDecimal(dtCommissionSalaryInfo.Rows[x], "Amount"));
                            string FlagName = StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtCommissionSalaryInfo.Rows[x], "FlagName"));

                            sbTemp.AppendLine("<td class='tdColInputCenter' >"
                                + "<input type='text' class='tdinput' readonly='readonly' id='txtAmount_" + (i + 1).ToString() + "_" + (j + 1).ToString()
                                + "_" + (x + 1).ToString() + "' value='" + timeCount + "' style='width=95%;background-color:#FFFFE0;'"
                                + "  onchange='Number_round(this,\"2\");'   maxlength = '10' />" + "</td>");
                            //来源
                            sbTemp.AppendLine("<td class='tdColInputCenter' >"
                            + "<input type='text' class='tdinput' id='txtFlag_" + (i + 1).ToString() + "_" + (j + 1).ToString()
                            + "_" + (x + 1).ToString() + "' value='" + FlagName + "' style='width=95%'"
                            + "   readonly />" + "</td>");
                            //金额
                            string salaryMoney = StringUtil.TrimZero(GetSafeData.GetStringFromDecimal(dtCommissionSalaryInfo.Rows[x], "SalaryMoney"));
                            sbTemp.AppendLine("<td class='tdColInputCenter' id='tdAmountMoney_" + (i + 1).ToString() + "_" + (j + 1).ToString()
                                                + "_" + (x + 1).ToString() + "' >" + salaryMoney + "</td>");
                            //日期
                            sbTemp.AppendLine("<td class='tdColInputCenter' id='tdSalaryDate_" + (i + 1).ToString() + "_" + (j + 1).ToString()
                                                + "_" + (x + 1).ToString() + "'>" + GetSafeData.ValidateDataRow_String(dtCommissionSalaryInfo.Rows[x], "CommDate") + "</td>");
                            //计算小计金额
                            timeTotalMoney += decimal.Parse(salaryMoney);
                            if (x == 0)
                            {
                                //人员的第一行信息时
                                if (j == 0)
                                {
                                    sbEmplFirstRow.AppendLine(sbTemp.ToString());
                                }
                                //不是人员的第一行信息，但是是提成工资项的第一行信息
                                else
                                {
                                    sbTimeFirstRow.AppendLine(sbTemp.ToString());
                                }
                            }
                            else
                            {
                                //行开始
                                sbTimeNotFirstRow.AppendLine("<tr>");
                                sbTimeNotFirstRow.AppendLine(sbTemp.ToString());
                                //行结束
                                sbTimeNotFirstRow.AppendLine("</tr>");
                            }
                        }

                        //工资小计
                        if (j == 0)
                        {
                            sbEmplFirstRow.AppendLine("<td id='tdAmountTotalMoney_" + (i + 1).ToString() + "_" + (j + 1).ToString()
                                + "' class='tdColInputCenter'  rowspan='" + commSalaryCount.ToString() + "'>" + timeTotalMoney.ToString() + "</td>");
                        }
                        else
                        {
                            sbTimeFirstRow.AppendLine("<td id='tdAmountTotalMoney_" + (i + 1).ToString() + "_" + (j + 1).ToString()
                                + "' class='tdColInputCenter'  rowspan='" + commSalaryCount.ToString() + "'>" + timeTotalMoney.ToString() + "</td></tr>");
                        }
                        //计算合计金额
                        totalMoney += timeTotalMoney;
                        //行数统计
                        totalRowCount += commSalaryCount;
                        //非第一行时，添加到非第一行的信息里
                        sbEmplNotFirstRow.AppendLine(sbTimeFirstRow.ToString() + sbTimeNotFirstRow.ToString());
                    }
                    #endregion

                    //行开始
                    sbCommissionSalaryInfo.AppendLine("<tr>");
                    //选择
                    sbCommissionSalaryInfo.AppendLine("<td class='tdColInputCenter'  rowspan='" + totalRowCount.ToString() + "'>"
                     + "<input id='chkSelect_" + (i + 1).ToString() + "' name='chkSelect'  value='" + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "EmployeeNo") + "'  type='checkbox'  onpropertychange='getChage(this)'  />" + "</td>");
                    //人员编号
                    sbCommissionSalaryInfo.AppendLine("<td class='tdColInputCenter'  rowspan='" + totalRowCount.ToString() + "'>"
                        + "<input type='hidden' id='txtEmplID_" + (i + 1).ToString() + "' value='" + emplID + "' />"
                        + "<input type='hidden' id='txtItemCount_" + (i + 1).ToString() + "' value='" + commItemCount + "' />"
                        + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "EmployeeNo") + "</td>");
                    //人员姓名
                    sbCommissionSalaryInfo.AppendLine("<td class='tdColInputCenter'  rowspan='" + totalRowCount.ToString() + "'>"
                        + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "EmployeeName") + "</td>");
                    //所在部门
                    sbCommissionSalaryInfo.AppendLine("<td class='tdColInputCenter'  rowspan='" + totalRowCount.ToString() + "'>"
                        + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "DeptName") + "</td>");
                    //岗位名称
                    sbCommissionSalaryInfo.AppendLine("<td class='tdColInputCenter'  rowspan='" + totalRowCount.ToString() + "'>"
                        + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "QuarterName") + "</td>");
                    //岗位职等
                    sbCommissionSalaryInfo.AppendLine("<td class='tdColInputCenter'  rowspan='" + totalRowCount.ToString() + "'>"
                        + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "AdminLevelName") + "</td>");

                    //具体的提成项目信息
                    sbCommissionSalaryInfo.AppendLine(sbEmplFirstRow.ToString());

                    //工资合计
                    sbCommissionSalaryInfo.AppendLine("<td class='tdColInputCenter' id='tdTotalMoney_" + (i + 1).ToString() + "' rowspan='" + totalRowCount.ToString() + "'>" + totalMoney.ToString() + "</td>");

                    //行结束
                    sbCommissionSalaryInfo.AppendLine("</tr>");
                    //具体的提成项目信息
                    sbCommissionSalaryInfo.AppendLine(sbEmplNotFirstRow.ToString());

                }
            }

            return sbCommissionSalaryInfo.ToString();
        }
        #endregion

        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <param name="no">申请编号</param>
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
            logModel.ModuleID = ConstUtil.MODULE_ID_HUMAN_SALARY_INPUT;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_SALARY_COMMISSION;//操作对象
            //
            logModel.ObjectID = no;

            return logModel;

        }
        #endregion
    }
}
