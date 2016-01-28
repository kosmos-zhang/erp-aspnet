/**********************************************
 * 类作用：   计件工资录入
 * 建立人：   吴志强
 * 建立时间： 2009/05/14
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
using System.Collections;

namespace XBase.Business.Office.HumanManager
{
    /// <summary>
    /// 类名：PieceworkSalaryBus
    /// 描述：计件工资录入
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/14
    /// 最后修改时间：2009/05/14
    /// </summary>
    ///
    public class PieceworkSalaryBus
    {
        #region 编辑计时工资项信息
        /// <summary>
        /// 编辑计时工资项信息
        /// </summary>
        /// <param name="lstEdit">计时工资项信息</param>
        /// <returns></returns>
        public static bool SavePieceworkSalaryInfo(ArrayList lstEdit)
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
                    isSucc = PieceworkSalaryDBHelper.SavePieceworkSalaryInfo(lstEdit, userInfo.CompanyCD, userInfo.UserID);
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

        #region 查询计时工资项信息
        /// <summary>
        /// 查询计时工资项信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <param name="itemNo">计时项目编号</param>
        /// <returns></returns>
        public static string GetPieceworkSalaryInfo(EmployeeSearchModel model, string itemNo)
        {
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.CompanyCD = userInfo.CompanyCD;
            //获取满足条件的人员信息查询
            DataTable dtEmplInfo = PieceworkSalaryDBHelper.GetEmplInfo(model, itemNo);
        
            //变量定义
            StringBuilder sbPieceworkSalaryInfo = new StringBuilder();
            //数据存在时，设置数据
            if (dtEmplInfo != null && dtEmplInfo.Rows.Count > 0)
            {
                //设置记录数
                model.RecordCount = dtEmplInfo.Rows.Count.ToString();
                //遍历员工获取计时工资信息
                for (int i = 0; i < dtEmplInfo.Rows.Count; i++)
                {
                    //获取人员ID
                    string emplID = GetSafeData.GetStringFromInt(dtEmplInfo.Rows[i], "EmployeeID");
                    //获取人员计时工资
                    DataTable dtPeiceItemInfo = PieceworkSalaryDBHelper.GetPeiceItemInfo(model.CompanyCD
                                                            , emplID, itemNo, model.StartDate, model.EndDate);

                    //变量定义
                    decimal totalMoney = 0;
                    int pieceworkItemCount = 0;
                    int totalRowCount = 0;
                    StringBuilder sbEmplFirstRow = new StringBuilder();
                    StringBuilder sbEmplNotFirstRow = new StringBuilder();
                    //获取工资项目总数
                    if (dtPeiceItemInfo != null && dtPeiceItemInfo.Rows.Count > 0) pieceworkItemCount = dtPeiceItemInfo.Rows.Count;

                    #region 生成每个计时项目的具体信息
                    //遍历工资项
                    for (int j = 0; j < pieceworkItemCount; j++)
                    {
                        //变量定义
                        decimal pieceworkTotalMoney = 0;
                        StringBuilder sbPieceworkFirstRow = new StringBuilder();
                        StringBuilder sbPieceworkNotFirstRow = new StringBuilder();
                        //获取工资项编号
                        string pieceworkItemNo = GetSafeData.ValidateDataRow_String(dtPeiceItemInfo.Rows[j], "ItemNo");
                        //获取工资内容
                        DataTable dtPieceworkSalaryInfo = PieceworkSalaryDBHelper.GetPieceworkSalaryInfo(model.CompanyCD
                                                                , emplID, pieceworkItemNo, model.StartDate, model.EndDate);
                        //获取工资内容总数
                        int PieceworkSalaryCount = 0;
                        if (dtPieceworkSalaryInfo != null && dtPieceworkSalaryInfo.Rows.Count > 0) PieceworkSalaryCount = dtPieceworkSalaryInfo.Rows.Count;
                        //单价
                        string price = StringUtil.TrimZero(GetSafeData.GetStringFromDecimal(dtPeiceItemInfo.Rows[j], "UnitPrice"));

                        StringBuilder sbItemTemp = new StringBuilder();

                        //
                        //工资编号
                        sbItemTemp.AppendLine("<td class='tdColInputCenter'  rowspan='" + PieceworkSalaryCount.ToString() + "'>"
                            + "<input type='hidden' id='txtSalaryNo_" + (i + 1).ToString() + "_" + (j + 1).ToString() + "' value='" + pieceworkItemNo + "' />"
                            + "<input type='hidden' id='txtSalaryCount_" + (i + 1).ToString() + "_" + (j + 1).ToString() + "' value='"
                                        + PieceworkSalaryCount + "' />"
                            + GetSafeData.ValidateDataRow_String(dtPeiceItemInfo.Rows[j], "ItemName") + "</td>");
                        //单价
                        sbItemTemp.AppendLine("<td class='tdColInputCenter'   rowspan='" + PieceworkSalaryCount.ToString() + "' id='tdUnitPrice_"
                                                                + (i + 1).ToString() + "_" + (j + 1).ToString() + "' >" + price + "</td>");
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
                        for (int x = 0; x < PieceworkSalaryCount; x++)
                        {
                            StringBuilder sbTemp = new StringBuilder();
                            //数量
                            string amount = StringUtil.TrimZero(GetSafeData.GetStringFromDecimal(dtPieceworkSalaryInfo.Rows[x], "Amount"));
                            sbTemp.AppendLine("<td class='tdColInputCenter' >"
                                + "<input type='text' class='tdinput'   readonly='readonly'   id='txtAmount_" + (i + 1).ToString() + "_" + (j + 1).ToString()
                                + "_" + (x + 1).ToString() + "' value='" + amount + "' style='width=95%;background-color:#FFFFE0;'"
                                + " onblur='CalculateTotalSalary(this, \"" + (i + 1).ToString() + "\", \"" + (j + 1).ToString()
                                + "\", \"" + (x + 1).ToString() + "\");'  onchange='Number_round(this,\"2\");'   maxlength = '8' />" + "</td>");
                            //金额
                            string salaryMoney = StringUtil.TrimZero(GetSafeData.GetStringFromDecimal(dtPieceworkSalaryInfo.Rows[x], "SalaryMoney"));
                            sbTemp.AppendLine("<td class='tdColInputCenter' id='tdAmountMoney_" + (i + 1).ToString() + "_" + (j + 1).ToString()
                                                + "_" + (x + 1).ToString() + "' >" + salaryMoney + "</td>");
                            //日期
                            sbTemp.AppendLine("<td class='tdColInputCenter' id='tdSalaryDate_" + (i + 1).ToString() + "_" + (j + 1).ToString()
                                                + "_" + (x + 1).ToString() + "'>" + GetSafeData.ValidateDataRow_String(dtPieceworkSalaryInfo.Rows[x], "PieceDate") + "</td>");
                            //来源
                            //sbTemp.AppendLine("<td class='tdColInputCenter' id='tdSalaryDate_" + (i + 1).ToString() + "_" + (j + 1).ToString()
                            //                + "_" + (x + 1).ToString() + "'>" + GetSafeData.ValidateDataRow_String(dtPieceworkSalaryInfo.Rows[x], "PieceDate") + "</td>");
                            //计算小计金额
                            pieceworkTotalMoney += decimal.Parse(salaryMoney);
                            if (x == 0)
                            {
                                //人员的第一行信息时
                                if (j == 0)
                                {
                                    sbEmplFirstRow.AppendLine(sbTemp.ToString());
                                }
                                //不是人员的第一行信息，但是是计时工资项的第一行信息
                                else
                                {
                                    sbPieceworkFirstRow.AppendLine(sbTemp.ToString());
                                }
                            }
                            else
                            {
                                //行开始
                                sbPieceworkNotFirstRow.AppendLine("<tr>");
                                sbPieceworkNotFirstRow.AppendLine(sbTemp.ToString());
                                //行结束
                                sbPieceworkNotFirstRow.AppendLine("</tr>");
                            }
                        }

                        //工资小计
                        if (j == 0)
                        {
                            sbEmplFirstRow.AppendLine("<td id='tdAmountTotalMoney_" + (i + 1).ToString() + "_" + (j + 1).ToString()
                                + "' class='tdColInputCenter'  rowspan='" + PieceworkSalaryCount.ToString() + "'>" + pieceworkTotalMoney.ToString() + "</td>");
                        }
                        else
                        {
                            sbPieceworkFirstRow.AppendLine("<td id='tdAmountTotalMoney_" + (i + 1).ToString() + "_" + (j + 1).ToString()
                                + "' class='tdColInputCenter'  rowspan='" + PieceworkSalaryCount.ToString() + "'>" + pieceworkTotalMoney.ToString() + "</td></tr>");
                        }
                        //计算合计金额
                        totalMoney += pieceworkTotalMoney;
                        //行数统计
                        totalRowCount += PieceworkSalaryCount;
                        //非第一行时，添加到非第一行的信息里
                        sbEmplNotFirstRow.AppendLine(sbPieceworkFirstRow.ToString() + sbPieceworkNotFirstRow.ToString());
                    }
                    #endregion

                    //行开始
                    sbPieceworkSalaryInfo.AppendLine("<tr>");
                 
             //选择
                       sbPieceworkSalaryInfo.AppendLine("<td class='tdColInputCenter'  rowspan='" + totalRowCount.ToString() + "'>"
                        + "<input id='chkSelect_"+ (i + 1).ToString() + "' name='chkSelect'  value='" + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "EmployeeNo") + "'  type='checkbox'  onpropertychange='getChage(this)'  />" + "</td>");
       //人员编号
                    sbPieceworkSalaryInfo.AppendLine("<td class='tdColInputCenter'  rowspan='" + totalRowCount.ToString() + "'>"
                        + "<input type='hidden' id='txtEmplID_" + (i + 1).ToString() + "' value='" + emplID + "' />"
                        + "<input type='hidden' id='txtItemCount_" + (i + 1).ToString() + "' value='" + pieceworkItemCount + "' />"
                        + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "EmployeeNo") + "</td>");
                    //人员姓名
                    sbPieceworkSalaryInfo.AppendLine("<td class='tdColInputCenter'  rowspan='" + totalRowCount.ToString() + "'>"
                        + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "EmployeeName") + "</td>");
                    //所在部门
                    sbPieceworkSalaryInfo.AppendLine("<td class='tdColInputCenter'  rowspan='" + totalRowCount.ToString() + "'>"
                        + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "DeptName") + "</td>");
                    //岗位名称
                    sbPieceworkSalaryInfo.AppendLine("<td class='tdColInputCenter'  rowspan='" + totalRowCount.ToString() + "'>"
                        + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "QuarterName") + "</td>");
                    //岗位职等
                    sbPieceworkSalaryInfo.AppendLine("<td class='tdColInputCenter'  rowspan='" + totalRowCount.ToString() + "'>"
                        + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "AdminLevelName") + "</td>");

                    //具体的计时项目信息
                    sbPieceworkSalaryInfo.AppendLine(sbEmplFirstRow.ToString());

                    //工资合计
                    sbPieceworkSalaryInfo.AppendLine("<td class='tdColInputCenter' id='tdTotalMoney_" + (i + 1).ToString() + "' rowspan='" + totalRowCount.ToString() + "'>" + totalMoney.ToString() + "</td>");

                    //行结束
                    sbPieceworkSalaryInfo.AppendLine("</tr>");
                    //具体的计时项目信息
                    sbPieceworkSalaryInfo.AppendLine(sbEmplNotFirstRow.ToString());

                }
            }

            return sbPieceworkSalaryInfo.ToString();
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
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_SALARY_PIECEWORK;//操作对象
            //
            logModel.ObjectID = no;

            return logModel;

        }
        #endregion
    }
}
