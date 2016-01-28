/**********************************************
 * 类作用：   通用审批流程事务层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/07
 ***********************************************/

using System;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Data.Common;
using XBase.Common;
using System.Collections;
namespace XBase.Business.Common
{
    public class FlowBus
    {
        /// <summary>
        /// 获取审批流程
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="FlowNo"></param>
        /// <returns></returns>
        public static DataSet GetFlow(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID)
        {
            try
            {
                return FlowDBHelper.GetFlow(CompanyCD, BillTypeFlag, BillTypeCode, BillID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #region 流程步骤
        /// <summary>
        /// 获取流程步骤
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetFlowStep(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID)
        {
            try
            {
                return FlowDBHelper.GetFlowStep(CompanyCD, BillTypeFlag, BillTypeCode, BillID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 提交审批
        /// <summary>
        /// 提交审批
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="FlowNo"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="PageUrl"></param>
        /// <param name="ApplyUserID"></param>
        /// <param name="ModifiedUserID"></param>
        /// <returns></returns>
        public static string FlowApplyAdd(string CompanyCD, string FlowNo, int BillTypeFlag, int BillTypeCode, int BillID, string BillNo, string PageUrl, string ApplyUserID, string ModifiedUserID, int MsgRemind)
        {
            try
            {
                string strRetVal = "";
                strRetVal = FlowDBHelper.FlowApplyAdd(CompanyCD, FlowNo, BillTypeFlag, BillTypeCode, BillID, BillNo, PageUrl, ApplyUserID, ModifiedUserID);
                if (!String.IsNullOrEmpty(strRetVal))
                {
                    string[] retArray = strRetVal.Split('|');
                    if (int.Parse(retArray[0].ToString()) > 0)
                    {
                        if (MsgRemind > 0)
                        {
                            //提交审批成功后发送短信提醒
                            SendMobileMsg(CompanyCD, FlowNo, BillTypeFlag, BillTypeCode, BillID, BillNo, 0, 0);
                        }
                    }
                }
                return strRetVal;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 审批
        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="FlowNo"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="State"></param>
        /// <param name="Note"></param>
        /// <param name="ModifiedUserID"></param>
        /// <returns></returns>
        public static string FlowApproveAdd(string CompanyCD, string FlowNo, int BillTypeFlag, int BillTypeCode, int BillID, string BillNo, string State, string Note, string ModifiedUserID, int ModifiedEmployeeID, int MsgRemind)
        {
            try
            {
                string strRetVal = "";
                strRetVal = FlowDBHelper.FlowApprovalAdd(CompanyCD, FlowNo, BillTypeFlag, BillTypeCode, BillID, State, Note, ModifiedUserID, ModifiedEmployeeID);
                if (!String.IsNullOrEmpty(strRetVal))
                {
                    string[] retArray = strRetVal.Split('|');
                    if (int.Parse(retArray[0].ToString()) > 0)
                    {
                        if (MsgRemind > 0)
                        {
                            //提交审批成功后发送短信提醒
                            SendMobileMsg(CompanyCD, FlowNo, BillTypeFlag, BillTypeCode, BillID, BillNo, int.Parse(State), 1);
                        }
                    }
                }
                return strRetVal;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 撤消审批
        /// <summary>
        /// 撤消审批
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="FlowNo"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="State"></param>
        /// <param name="ModifiedUserID"></param>
        /// <returns></returns>
        public static string FlowApproveUpdate(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID, string ModifiedUserID, int ModifiedEmployeeID)
        {
            try
            {
                return FlowDBHelper.FlowApprovalUpdate(CompanyCD, BillTypeFlag, BillTypeCode, BillID, ModifiedUserID, ModifiedEmployeeID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 单据状态
        /// <summary>
        /// 当前单据处理状态
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="FlowNo"></param>
        /// <returns></returns>
        public static int GetBillUsedStatus(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID)
        {
            try
            {
                return FlowDBHelper.GetBillUsedStatus(CompanyCD, BillTypeFlag, BillTypeCode, BillID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 当前审批的步骤
        /// <summary>
        /// 当前步骤
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <returns></returns>
        public static int CurrentStep(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID)
        {
            try
            {
                return FlowDBHelper.CurrentStep(CompanyCD, BillTypeFlag, BillTypeCode, BillID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 审批操作记录
        /// <summary>
        /// 审批操作记录
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="FlowNo"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="FlowNo"></param>
        /// <returns></returns>
        public static DataTable GetOperateRecordList(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID)
        {
            try
            {
                return FlowDBHelper.GetOperateRecordList(CompanyCD, BillTypeFlag, BillTypeCode, BillID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region
        /// <summary>
        /// 获取下一步即将审批的步骤
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <returns></returns>
        public static DataTable GetNextOperateList(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID)
        {
            try
            {
                return FlowDBHelper.GetNextOperateList(CompanyCD, BillTypeFlag, BillTypeCode, BillID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 手机短信提醒
        /// <summary>
        /// 审批流程手机短信提醒
        /// </summary>
        /// <param name="CompanyCD">公司CD</param>
        /// <param name="FlowNo">流程编号</param>
        /// <param name="BillTypeFlag">Flag</param>
        /// <param name="BillTypeCode">Code</param>
        /// <param name="BillID">单据ID</param>
        /// <param name="State">审批状态(1:通过 0:不通过)</param>
        /// <param name="OperateType">操作类型(0:提交审批时 1:审批时)</param>
        /// <returns></returns>
        public static DataTable GetMsgRemindList(string CompanyCD, string FlowNo, int BillTypeFlag, int BillTypeCode, int BillID, int State, int OperateType)
        {
            try
            {
                return FlowDBHelper.GetMsgRemindList(CompanyCD, FlowNo, BillTypeFlag, BillTypeCode, BillID, State, ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID,OperateType);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 发送手机短信提醒
        public static void SendMobileMsg(string CompanyCD, string FlowNo, int BillTypeFlag, int BillTypeCode, int BillID, string BillNo, int State, int OperateType)
        {
            try
            {
                DataTable dt = XBase.Business.Common.FlowBus.GetMsgRemindList(CompanyCD, FlowNo, BillTypeFlag, BillTypeCode, BillID, State, OperateType);
                XBase.Business.Personal.MessageBox.MobileMsgMonitor bll = new XBase.Business.Personal.MessageBox.MobileMsgMonitor();
                if (dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0]["EmployeeID"].ToString()) && !string.IsNullOrEmpty(dt.Rows[0]["Mobile"].ToString()))
                    {
                        if (int.Parse(dt.Rows[0]["MsgCount"].ToString())>0)
                        {
                            XBase.Model.Personal.MessageBox.MobileMsgMonitor entity;
                            entity = new XBase.Model.Personal.MessageBox.MobileMsgMonitor();
                            entity.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                            entity.Content = dt.Rows[0]["MsgContents"].ToString();
                            entity.CreateDate = DateTime.Now;
                            entity.ReceiveMobile = dt.Rows[0]["Mobile"].ToString();
                            entity.ReceiveUserID = int.Parse(dt.Rows[0]["EmployeeID"].ToString());
                            entity.ReceiveUserName = dt.Rows[0]["EmployeeName"].ToString();
                            entity.SendDate = DateTime.Now;
                            entity.SendUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                            entity.SendUserName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                            entity.Status = "1";
                            entity.MsgType = "0";
                            bll.Add(entity);
                            XBase.Common.SMSender.SendBatch(entity.ReceiveMobile, entity.Content);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 判断是否有权限审批
        /// <summary>
        /// 判断是否有权限审批
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="FlowNo"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="ModifiedEmployeeID"></param>
        /// <returns></returns>
        public static string GetFlowApprovalAuthority(string CompanyCD, int BillTypeFlag, int BillTypeCode, int BillID, int ModifiedEmployeeID)
        {
            try
            {
                return FlowDBHelper.GetFlowApprovalAuthority(CompanyCD, BillTypeFlag, BillTypeCode, BillID, ModifiedEmployeeID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public static ArrayList  GetRecord(string item_BillTypeFlag, string item_BillTypeCode, string item_BillID, string companyCD)
        {
            try
            {

                int BillTypeFlag = Convert.ToInt32(item_BillTypeFlag);
                int BillTypeCode = Convert.ToInt32(item_BillTypeCode);
                int BillID = Convert.ToInt32(item_BillID);
                int StepCount = 0;//总的审批步骤

                DataTable dtRecord = new DataTable();
                DataTable dtStep = new DataTable();
                DataTable dtNew = new DataTable();

                dtRecord = FlowBus.GetOperateRecordList(companyCD, BillTypeFlag, BillTypeCode, BillID);
                dtStep = FlowBus.GetFlowStep(companyCD, BillTypeFlag, BillTypeCode, BillID);
                if (dtStep.Rows.Count > 0)
                {
                    StepCount = dtStep.Rows.Count;
                }
                for (int i = 0; i < dtRecord.Rows.Count; i++)
                {
                    //克隆表结构
                    if (i == 0)
                    {
                        dtNew = dtRecord.Clone();
                    }

                    #region buyao
                    //if (i == 0)
                    //{

                    //    DataTable dtNext = FlowBus.GetNextOperateList(companyCD, BillTypeFlag, BillTypeCode, BillID);
                    //    //提交审批的
                    //    if (string.IsNullOrEmpty(dtRecord.Rows[i]["StepNo"].ToString()) && string.IsNullOrEmpty(dtRecord.Rows[i]["State"].ToString()))
                    //    {
                    //        if (dtNext.Rows.Count > 0)
                    //        {

                    //            //新表导入行
                    //            //DataRow drNext = dtNew.NewRow();
                    //            //drNext["StepNo"] = 0;
                    //            //drNext["State"] = -1;
                    //            //drNext["Operator"] = dtNext.Rows[0]["EmployeeName"].ToString();
                    //            //drNext["EmployeeID"] = dtNext.Rows[0]["EmployeeID"].ToString();
                    //            //dtNew.Rows.Add(drNext);
                    //        }
                    //    }
                    //    else
                    //    {

                    //        if (int.Parse(dtRecord.Rows[i]["StepNo"].ToString()) > 0)
                    //        {
                    //            if (StepCount == int.Parse(dtRecord.Rows[i]["StepNo"].ToString()))
                    //            {

                    //            }
                    //            else
                    //            {
                    //                if (int.Parse(dtRecord.Rows[i]["State"].ToString()) < 2)
                    //                {
                    //                    if (dtNext.Rows.Count > 0)
                    //                    {

                    //                        //新表导入行
                    //                        //DataRow drNextIn = dtNew.NewRow();
                    //                        //drNextIn["StepNo"] = 0;
                    //                        //drNextIn["State"] = -1;
                    //                        //drNextIn["Operator"] = dtNext.Rows[0]["EmployeeName"].ToString();
                    //                        //drNextIn["EmployeeID"] = dtNext.Rows[0]["EmployeeID"].ToString();
                    //                        //dtNew.Rows.Add(drNextIn);
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }

                    //}
#endregion
                    //导入行
                    DataRow dr = dtNew.NewRow();
                    if (!string.IsNullOrEmpty(dtRecord.Rows[i]["StepNo"].ToString()))
                    {
                        dr["StepNo"] = int.Parse(dtRecord.Rows[i]["StepNo"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dtRecord.Rows[i]["State"].ToString()))
                    {
                        dr["State"] = int.Parse(dtRecord.Rows[i]["State"].ToString());
                    }

                    dr["ApplyUserId"] = dtRecord.Rows[i]["ApplyUserId"].ToString();
                    dr["operateDate"] = dtRecord.Rows[i]["operateDate"].ToString();
                    dr["Note"] = dtRecord.Rows[i]["Note"].ToString();
                    dr["Operator"] = dtRecord.Rows[i]["Operator"].ToString();
                    dr["EmployeeID"] = dtRecord.Rows[i]["EmployeeID"].ToString();
                    dtNew.Rows.Add(dr);
                }



                string result = string.Empty;
                ArrayList finalResult = new ArrayList();
                DataTable dtResult = new DataTable();

                dtResult.Columns.Add("FlowNo");
                dtResult.Columns.Add("FlowName");
                dtResult.Columns.Add("ModifiedDate");
                dtResult.Columns.Add("BillTypeName");
                dtResult.Columns.Add("UsedStatus");

                if (dtNew.Rows.Count > 0||dtStep .Rows .Count >0)
                {
                 
                }
                else
                {
                    dtResult = null;
                    result = "待审批";
                    finalResult.Add(dtResult);
                    finalResult.Add(result);
                    return finalResult;
                }


               


                DataTable dtGet = GetNewDataTable(dtNew, "", "operateDate desc");
                DataTable tempTable = new DataTable();
                tempTable = dtGet.Clone();
                for (int i = 0; i < dtGet.Rows.Count ; i++)
                {
                    DataRow drr = tempTable.NewRow();
                    string oldStepNo = dtGet.Rows [i]["StepNo"]==null ?"": dtGet.Rows [i]["StepNo"].ToString();
                    string oldState = dtGet.Rows [i]["State"] == null ? "" : dtGet.Rows [i]["State"].ToString();
                    if (string.IsNullOrEmpty(oldStepNo) && string.IsNullOrEmpty(oldState))
                    {
                        break;
                    }
                    else
                    {
                        drr["StepNo"] = dtGet.Rows [i]["StepNo"] == null ? "" : dtGet.Rows [i]["StepNo"].ToString();
                        drr["State"] = dtGet.Rows [i]["State"] == null ? "" : dtGet.Rows [i]["State"].ToString();
                        drr["ApplyUserId"] = dtGet.Rows [i]["ApplyUserId"] == null ? "" : dtGet.Rows [i]["ApplyUserId"].ToString();
                        drr["operateDate"] =  dtGet.Rows [i]["operateDate"] == null ? "" : dtGet.Rows [i]["operateDate"].ToString();
                        drr["Note"] = dtGet.Rows [i]["Note"] == null ? "" : dtGet.Rows [i]["Note"].ToString();
                        drr["Operator"] = dtGet.Rows [i]["Operator"] == null ? "" : dtGet.Rows [i]["Operator"].ToString();
                        drr["EmployeeID"] = dtGet.Rows[i]["EmployeeID"] == null ? "" : dtGet.Rows[i]["EmployeeID"].ToString();

                        tempTable.Rows.Add(drr);
                      
                    }
                }

                if (tempTable.Rows.Count > 0)
                { }
                else
                {
                    dtResult = null;
                    result = "待审批";
                    finalResult.Add(dtResult);
                    finalResult.Add(result);
                    return finalResult;
                }


                string tem = tempTable.Rows[0]["State"] == null ? "" : tempTable.Rows[0]["State"].ToString();
                if ( tem == "2"|| tem=="")
                {
                    dtResult = null;
                    result = "待审批";
                    finalResult.Add(dtResult);
                    finalResult.Add(result);
                    return finalResult;
 
                }
                dtGet = tempTable;
                if (dtGet.Rows.Count > 0 && dtStep.Rows.Count > 0)//当操作流程大于
                {
               


                    if (dtGet.Rows.Count >= dtStep.Rows.Count)
                    {
                        int sign = 0;
                        bool tongGuo = false;
                        ArrayList emplyeeIDList = new ArrayList();
                        for (int cc = dtStep.Rows.Count - 1; cc >= 0; cc--)
                        {
                            DataRow relation = dtResult.NewRow();

                            if (sign == dtStep.Rows.Count - 1)
                            {
                                relation["FlowNo"] = "签批人:";
                                relation["ModifiedDate"] = dtGet.Rows[cc]["operateDate"] == null ? "" : "签批时间：" + dtGet.Rows[cc]["operateDate"].ToString();
                            }
                            else
                            {
                                relation["FlowNo"] = "第" + GetNum(sign) + "审批人:";
                                relation["ModifiedDate"] = dtGet.Rows[cc]["operateDate"] == null ? "" : "审批时间：" + dtGet.Rows[cc]["operateDate"].ToString();
                            }
                            sign++;
                            relation["UsedStatus"] = dtGet.Rows[cc]["Note"] == null ? "" :  dtGet.Rows[cc]["Note"].ToString();
                            relation["FlowName"] = dtGet.Rows[cc]["Operator"] == null ? "" : dtGet.Rows[cc]["Operator"].ToString();
                            string state=dtGet.Rows[cc]["State"] == null ? "15" : dtGet.Rows[cc]["State"].ToString();
                            relation["BillTypeName"] = GetStateTranslate(Convert.ToInt32(state ));
                            emplyeeIDList.Add(dtGet.Rows[cc]["EmployeeID"] == null ? "" : dtGet.Rows[cc]["EmployeeID"].ToString());
                            if ( state != "0")
                            {
                                tongGuo = true;
                            }
                            dtResult.Rows.Add(relation);
                        }
                        bool isCompelete = false;
                        for (int step = 0; step < dtStep.Rows.Count - 1; step++)
                        {
                            string tempStep = dtStep.Rows[step]["EmployeeID"] == null ? "" : dtStep.Rows[step]["EmployeeID"].ToString();
                            if (emplyeeIDList.Contains(tempStep))
                            {
                                continue;
                            }
                            else
                            {
                                isCompelete = true;
                                DataRow relation = dtResult.NewRow();
                                if (sign == dtStep.Rows.Count - 1)
                                {
                                    relation["FlowNo"] = "签批人";
                                }
                                else
                                {
                                    relation["FlowNo"] = "第" +GetStepNum( Convert .ToInt32 ( dtStep.Rows[step]["StepNo"] == null ? "15" : dtStep.Rows[step]["StepNo"].ToString() ))+ "审批人";
                                }
                                relation["FlowName"] = dtStep.Rows[step]["ActorReal"] == null ? "" : dtStep.Rows[step]["ActorReal"].ToString();
                                relation["ModifiedDate"] = "";
                                relation["BillTypeName"] = "待审批";
                                relation["UsedStatus"] = "";
                                dtResult.Rows.Add(relation);
                                sign++;
                            }
                           
                        }
                     



                        if (tongGuo)
                        {
                            result = "不通过";
                        }
                        else
                        {
                            result = "通过";
                        }
                        if (isCompelete)
                        {
                            result = "审批中 ";
                        }
                    }
                    else
                    {
                        int sign = 0;
                        ArrayList emplyeeIDList = new ArrayList();
                        for (int cc = dtGet.Rows.Count - 1; cc >= 0; cc--)
                        {
                            DataRow relation = dtResult.NewRow();
                             
                            relation["FlowNo"] = "第" + GetNum(sign) + "审批人";

                            relation["FlowName"] = dtGet.Rows[cc]["Operator"] == null ? "" : dtGet.Rows[cc]["Operator"].ToString();
                            relation["ModifiedDate"] = dtGet.Rows[cc]["operateDate"] == null ? "" : dtGet.Rows[cc]["operateDate"].ToString();
                            relation["BillTypeName"] = GetStateTranslate(Convert.ToInt32(dtGet.Rows[cc]["State"] == null ? "15" : dtGet.Rows[cc]["State"].ToString()));
                            relation["UsedStatus"] = dtGet.Rows[cc]["Note"] == null ? "" : dtGet.Rows[cc]["Note"].ToString();
                            emplyeeIDList.Add(dtGet.Rows[cc]["EmployeeID"] == null ? "" : dtGet.Rows[cc]["EmployeeID"].ToString());
                            dtResult.Rows.Add(relation);
                            sign++;
                        }
                        for (int step = 0; step < dtStep.Rows.Count ;step++  )
                        {
                            string tempStep=dtStep .Rows[step ]["EmployeeID"]==null ?"":dtStep .Rows[step ]["EmployeeID"].ToString ();
                            if (emplyeeIDList.Contains(tempStep))
                            {
                                continue;
                            }
                            else
                            {
                                DataRow relation = dtResult.NewRow();
                                if (sign == dtStep.Rows.Count - 1)
                                {
                                    relation["FlowNo"] = "签批人";
                                }
                                else
                                {
                                    relation["FlowNo"] = "第" + GetStepNum( Convert .ToInt32 ( dtStep.Rows[step]["StepNo"] == null ? "15" : dtStep.Rows[step]["StepNo"].ToString() ))+ "审批人";
                                }
                                relation["FlowName"] = dtStep.Rows[step]["ActorReal"] == null ? "" : dtStep.Rows[step]["ActorReal"].ToString();
                                relation["ModifiedDate"] =  "";
                                relation["BillTypeName"] =  "待审批";
                                relation["UsedStatus"] ="" ;
                                dtResult.Rows.Add(relation);
                                sign++;
                            }
                          
                        }

                        result = "审批中";
                    }
                }
                else
                {
                    result = "无";
                }

                finalResult.Add(dtResult );
                finalResult.Add(result );

                return finalResult;
            }
            catch
            {
                return null;
            }
        }
        private static string GetStateTranslate(int num)
        {
            string temp;
            switch (num)
            {
                case 0: temp = "通过"; break;
                case 1: temp = "不通过"; break;
                case 2: temp = "撤销审批"; break;
                case -1: temp = "待审批"; break; 
                default: temp = "无"; break;
            }
            return temp;

        }
        private static string GetStepNum(int num)
        {
            string temp;
            switch (num)
            {
              
                case 1: temp = "一"; break;
                case 2: temp = "二"; break;
                case 3: temp = "三"; break;
                case 4: temp = "四"; break;
                case 5: temp = "五"; break;
                case 6: temp = "六"; break;
                case 7: temp = "七"; break;
                case 8: temp = "八"; break;
                case 9: temp = "九"; break;
                default: temp = "0"; break;
            }
            return temp;

        }
        private static string GetNum(int num)
        {
            string temp;
            switch (num)
            {
                case 0: temp = "一"; break;
                case 1: temp = "二"; break;
                case 2: temp = "三"; break;
                case 3: temp = "四"; break;
                case 4: temp = "五"; break;
                case 5: temp = "六"; break;
                case 6: temp = "七"; break;
                case 7: temp = "八"; break;
                case 8: temp = "九"; break;
                case 9: temp = "十"; break;
                default: temp = "0"; break;
            }
            return temp;

        }
        private static DataTable GetDataTable(DataTable dt, string condition, string sort)
        {
            DataTable newdt = new DataTable();
            newdt = dt.Clone();
            DataRow[] dr = dt.Select(condition, sort);
            for (int i = 0; i < dr.Length; i++)
            {
                newdt.ImportRow((DataRow)dr[i]);
            }
            return newdt;//返回的查询结果
        }
        private static DataTable GetNewDataTable(DataTable dt, string condition, string sort)
        {
            DataTable newdt = new DataTable();
            newdt = dt.Clone();
            DataRow[] dr = dt.Select(condition, sort);
            for (int i = 0; i < dr.Length; i++)
            {
                int leth = newdt.Rows.Count;
                if (leth == 0)
                {
                    newdt.ImportRow((DataRow)dr[i]);
                }
                else
                {
                    if (dr[i]["StepNo"] == null || dr[i]["State"] == null)
                    {
                        continue;
                    }
                    else
                    {
                        string oldStepNo = dr[i]["StepNo"].ToString();
                        string oldState = dr[i]["State"].ToString();
                        if (string.IsNullOrEmpty(oldStepNo) && string.IsNullOrEmpty(oldState))
                        {
                            break ;
                        }
                        if (string.IsNullOrEmpty(oldStepNo) || string.IsNullOrEmpty(oldState))
                        {
                            continue;
                        }
                        bool sign = false;
                        for (int a = 0; a < leth; a++)
                        {
                            string NewStepNo = newdt.Rows[a]["StepNo"].ToString();
                            if (NewStepNo == oldStepNo)
                            {
                                sign = true;
                            }
                            else
                            {
                                continue;
                            }

                        }
                        if (!sign)
                        {
                            newdt.ImportRow((DataRow)dr[i]);
                        }
                        else
                        {
                            continue;
                        }


                    }

                }

            }
            return newdt;//返回的查询结果
        }
    }
}
