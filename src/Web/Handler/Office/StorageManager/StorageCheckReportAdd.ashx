<%@ WebHandler Language="C#" Class="StorageCheckReportAdd" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using System.Web.SessionState;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;

public class StorageCheckReportAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
        JsonClass jc = new JsonClass();
        string action = "";
        int ID = 0;
        if (context.Request.Form["Action"] != null)
        {
            if (context.Request.Form["Action"].ToString() == "Del")
            {
                string strID = context.Request.Form["strID"].ToString();
                if (CheckReportBus.DeleteReport(strID, companyCD))
                {
                    jc = new JsonClass("删除成功", "", 1);
                }
                else
                {
                    jc = new JsonClass("删除失败", "", 0);
                }
                context.Response.Write(jc);
                return;
            }
        }
        if ((context.Request.Form["ID"] != null))
        {
            action = context.Request.Form["myAction"];
            ID = int.Parse(context.Request.Form["ID"].ToString());
        }
        if (context.Request.QueryString["myAction"] != null)
        {
            action = context.Request.QueryString["myAction"].ToString();
            ID = int.Parse(context.Request.QueryString["ID"].ToString());
        }

        #region 添加质检报告入库

        if (action == "Confirm" || action == "UnConfirm")  //确认
        {
            if (ID > 0)
            {
                StorageQualityCheckReportModel model = new StorageQualityCheckReportModel();
                model.ID = ID;
                model.CompanyCD = companyCD;
                model.Confirmor = loginUser_id;
                model.ConfirmDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                model.ModifiedUserID = LoginUserID;
                model.ModifiedDate = DateTime.Now;
                model.CheckNum = Convert.ToDecimal(context.Request.Form["ProductCount"].ToString());
                model.CheckType = context.Request.Form["CheckType"].ToString();            //源单类型 
                model.NoPass = Convert.ToDecimal(context.Request.Form["NotPassNum"].ToString());
                model.PassNum = Convert.ToDecimal(context.Request.Form["PassNum"].ToString());
                model.PassPercent = Convert.ToDecimal(context.Request.Form["PassPercent"].ToString());
                model.FromType = context.Request.Form["FromType"].ToString().Trim();
                model.FromReportNo = context.Request.Form["FromReportNo"].ToString().Trim();
                model.FromDetailID = int.Parse(context.Request.Form["FromDetailID"].ToString());
                model.isRecheck = context.Request.Form["isRecheck"].ToString();
                if (model.FromType != "0")
                {
                    model.ReportID = int.Parse(context.Request.Form["ApplyID"].ToString()); // 源单 ID
                }
                try
                {
                    if (action == "Confirm")  //确认
                    {
                        if (model.isRecheck == "0")
                        {
                            if (model.FromType != "0")
                            {
                                string CheckValue = CheckReportBus.CheckConfirm(model);
                                if (CheckValue == "")
                                {
                                    CheckValue = "0";
                                }
                                if (CheckValue == "none")
                                {
                                    if (CheckValue == "none")
                                        CheckValue = "0";
                                    jc = new JsonClass("当前的报检数量大于源单的未检数量！", "CheckValue" + "|" + "", 1);
                                    context.Response.Write(jc);
                                    return;
                                }
                                if (model.CheckNum > Convert.ToDecimal(CheckValue))
                                {
                                    jc = new JsonClass("当前的报检数量大于源单的未检数量！", "CheckValue" + "|" + "", 1);
                                    context.Response.Write(jc);
                                    return;
                                }
                            }
                        }
                        if (CheckReportBus.ConfirmBill(model))
                        {
                            if (model.isRecheck == "0")
                            {
                                if (model.FromType == "3")  //生产任务
                                {
                                    CheckReportBus.UpdateMan(model);
                                }
                                if (model.FromType == "2")  //质检报告
                                {
                                    CheckReportBus.UpdateReport(model);
                                }
                                if (model.FromType == "1")  //质检申请单
                                {
                                    CheckReportBus.UpdateApply(model);
                                }
                                if (model.FromType == "4")//采购
                                {
                                    CheckReportBus.UpdatePur(model);
                                }
                            }
                            jc = new JsonClass("确认成功", EmployeeName + "|" + model.ModifiedDate.ToString("yyyy-MM-dd"), 1);
                        }
                        else
                        {
                            jc = new JsonClass("确认失败", "", 0);
                        }

                    }
                    if (action == "UnConfirm") //取消确认
                    {
                        if (Convert.ToInt32(CheckReportBus.IsTransfer(model.ID, model.CompanyCD)) > 0)
                        {

                            jc = new JsonClass("该单据已被其它单据调用了，不允许取消确认！", "no" + "|" + "", 1);
                            context.Response.Write(jc);
                            return;
                        }
                        if (CheckReportBus.UnConfirm(model))
                        {
                            jc = new JsonClass("取消确认成功", LoginUserID + "|" + model.ModifiedDate.ToString("yyyy-MM-dd"), 1);
                        }
                        else
                        {
                            jc = new JsonClass("取消确认失败", "", 0);
                        }
                    }
                    context.Response.Write(jc);
                    return;
                }
                catch
                { return; }
            }
            else
            {
                jc = new JsonClass("请求单据不存在", "", 0);
                context.Response.Write(jc);
                return;
            }
        }
        if (action == "Close")  //结单
        {
            if (ID > 0)
            {
                StorageQualityCheckReportModel model = new StorageQualityCheckReportModel();
                string theMethod = context.Request.Form["myMethod"].ToString();
                model.ID = ID;
                model.CompanyCD = companyCD;
                model.Closer = loginUser_id;
                model.CloseDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                model.ModifiedUserID = LoginUserID;
                model.ModifiedDate = DateTime.Now;
                if (theMethod == "0")
                {
                    if (CheckReportBus.CloseBill(model, theMethod))
                    {

                        jc = new JsonClass("结单成功", EmployeeName + "|" + model.ModifiedDate.ToString("yyyy-MM-dd"), 1);
                    }
                    else
                    {
                        jc = new JsonClass("结单失败", "", 0);
                    }
                }
                else
                {
                    if (CheckReportBus.CloseBill(model, theMethod))
                    {

                        jc = new JsonClass("取消结单成功", EmployeeName + "|" + model.ModifiedDate.ToString("yyyy-MM-dd"), 1);
                    }
                    else
                    {
                        jc = new JsonClass("取消订单失败", "", 0);
                    }
                }
            }
            else
            {
                jc = new JsonClass("请求单据不存在", "", 0);
            }
            context.Response.Write(jc);
            return;

        }
        else if (action == "updateattachment")
        {// 更新附件
            string attachment = context.Request.Form["Attachment"].ToString().Trim();      // 附件
            CheckReportBus.UpDateAttachment(ID, attachment);
        }
        else
        {
            int iID = 0;
            string DetailSortNo = context.Request.Form["DetailSortNo"].ToString().Trim();//
            string DetailCheckItem = context.Request.Form["DetailCheckItem"].ToString().Trim();//
            string DetailCheckStandard = context.Request.Form["DetailCheckStandard"].ToString().Trim();//
            string DetailCheckValue = "";
            if (!string.IsNullOrEmpty(context.Request.Form["DetailCheckValue"]))
            {
                DetailCheckValue = context.Request.Form["DetailCheckValue"].ToString().Trim();//    ..
            }

            string DetailCheckResult = context.Request.Form["DetailCheckResult"].ToString().Trim();//
            string DetailIsPass = context.Request.Form["DetailIsPass"].ToString().Trim();//
            string DetailCheckNum = context.Request.Form["DetailCheckNum"].ToString().Trim();//

            string DetailPassNum = context.Request.Form["DetailPassNum"].ToString().Trim();//
            string DetailNoPassNum = context.Request.Form["DetailNoPassNum"].ToString().Trim();//
            string DetailChecker = context.Request.Form["DetailChecker"].ToString().Trim();//    ..
            string DetailDept = context.Request.Form["DetailDept"].ToString().Trim();//
            string DetailStandardValue = "";
            if (!string.IsNullOrEmpty(context.Request.Form["DetailStandardValue"]))
            {
                DetailStandardValue = context.Request.Form["DetailStandardValue"].ToString().Trim();//
            }
            string DetailNormUpLimit = context.Request.Form["DetailNormUpLimit"].ToString().Trim();//

            string DetailLowerLimit = context.Request.Form["DetailLowerLimit"].ToString().Trim();//
            string DetailRemark = context.Request.Form["DetailRemark"].ToString().Trim();//


            StorageQualityCheckReportModel model = new StorageQualityCheckReportModel();

            if (context.Request.Form["bmgz"].ToString().Trim() == "zd")
            {
                model.ReportNo = ItemCodingRuleBus.GetCodeValue(context.Request.Form["ReportNo"].ToString().Trim());  //单据编号
            }
            else
            {
                model.ReportNo = context.Request.Form["ReportNo"].ToString().Trim();
            }
            if (action == "add")
            {
                bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("QualityCheckReport", "ReportNo", model.ReportNo);
                if (!isAlready || string.IsNullOrEmpty(model.ReportNo))
                {
                    if (string.IsNullOrEmpty(model.ReportNo))
                    {//该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!

                        jc = new JsonClass("该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!", "", 0);
                        context.Response.Write(jc);
                    }
                    else
                    {//单据编号已经存在
                        jc = new JsonClass("该编号已被使用，请输入未使用的编号！", "", 0);
                        context.Response.Write(jc);
                    }
                    return;
                }
            }
            model.CompanyCD = companyCD;
            model.ApplyDeptID = int.Parse(context.Request.Form["CheckerDept"].ToString());           //报检部门
            model.ApplyUserID = int.Parse(context.Request.Form["theChecker"].ToString());
            model.Attachment = context.Request.Form["Attachment"].ToString();
            model.BillStatus = context.Request.Form["myReportBillStatus"].ToString();
            if (context.Request.Form["myReportBillStatus"].ToString() == "2")
            {
                model.BillStatus = "3";
            }
            model.CheckDeptId = int.Parse(context.Request.Form["theDept"].ToString());     //检验部门
            model.CheckContent = context.Request.Form["CheckContent"].ToString();
            model.CheckDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            model.Checker = loginUser_id;
            model.CheckMode = context.Request.Form["CheckMode"].ToString();

            model.CheckResult = context.Request.Form["CheckResult"].ToString();
            model.CheckStandard = context.Request.Form["CheckStandard"].ToString();
            model.CheckType = context.Request.Form["CheckType"].ToString();            //源单类型 
            model.CloseDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            model.CreateDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            model.Creator = loginUser_id;
            model.FromLineNo = int.Parse(context.Request.Form["FromLineNo"].ToString());
            model.FromType = context.Request.Form["FromType"].ToString().Trim();
            model.isPass = context.Request.Form["IsPass"].ToString();
            model.isRecheck = context.Request.Form["IsReCheck"].ToString();
            model.CheckNum = Convert.ToDecimal(context.Request.Form["ProductCount"].ToString());
            model.NoPass = Convert.ToDecimal(context.Request.Form["NotPassNum"].ToString());
            model.PassNum = Convert.ToDecimal(context.Request.Form["PassNum"].ToString());
            model.PassPercent = Convert.ToDecimal(context.Request.Form["PassPercent"].ToString());
            model.ProductID = int.Parse(context.Request.Form["ProductID"].ToString());
            model.Remark = context.Request.Form["Remark"].ToString();
            model.FromDetailID = int.Parse(context.Request.Form["FromDetailID"].ToString());
            model.SampleNum = -99999;
            if (!string.IsNullOrEmpty(context.Request.Form["SampleNum"]))
            {
                model.SampleNum = Convert.ToDecimal(context.Request.Form["SampleNum"].ToString());
            }
            model.Title = context.Request.Form["Title"].ToString().Trim();
            model.ModifiedDate = DateTime.Now;
            model.ModifiedUserID = LoginUserID;

            if (model.FromType != "0")
            {
                model.ReportID = int.Parse(context.Request.Form["ApplyID"].ToString()); // 源单 ID
            }
            model.FromReportNo = context.Request.Form["ApplyIDName"].ToString(); //NAME
            model.OtherCorpName = context.Request.Form["theCustName"].ToString();
            model.OtherCorpID = int.Parse(context.Request.Form["theCustID"].ToString());
            model.CorpBigType = context.Request.Form["theBigCustID"].ToString();
            if (int.TryParse(context.Request.Form["Principal"], out iID))
            {
                model.Principal = iID;
            }
            if (int.TryParse(context.Request.Form["Dept"], out iID))
            {
                model.Dept = iID;
            }
            // 获得扩展属性
            Hashtable htExtAttr = GetExtAttr(context);
            List<StorageQualityCheckReportDetailModel> detailllist = new List<StorageQualityCheckReportDetailModel>();
            string[] myChecker = DetailChecker.Split(',');
            string[] myCheckItem = DetailCheckItem.Split(',');
            string[] myCheckNum = DetailCheckNum.Split(',');
            string[] myCheckResult = DetailCheckResult.Split(',');
            string[] myCheckStandard = DetailCheckStandard.Split(',');
            string[] myCheckValue = DetailCheckValue.Split(',');
            string[] myDept = DetailDept.Split(',');
            string[] myIsPass = DetailIsPass.Split(',');
            string[] myLowerLimit = DetailLowerLimit.Split(',');
            string[] myNoPassNum = DetailNoPassNum.Split(',');
            string[] myNormUpLimit = DetailNormUpLimit.Split(',');
            string[] myPassNum = DetailPassNum.Split(',');
            string[] mySortID = DetailSortNo.Split(',');
            string[] myRemark = DetailRemark.Split(',');
            string[] myStandardValue = DetailStandardValue.Split(',');

            if (mySortID.Length >= 1)
            {
                for (int i = 0; i < mySortID.Length; i++)
                {
                    if (mySortID[i] != "" && mySortID[i] != null)
                    {
                        StorageQualityCheckReportDetailModel detail = new StorageQualityCheckReportDetailModel();

                        detail.SortNo = int.Parse(mySortID[i]);
                        if (myChecker[i] != null && myChecker[i] != "")
                        {
                            detail.Checker = int.Parse(myChecker[i].ToString());
                        }
                        if (myCheckItem[i] != null && myCheckItem[i] != "")
                        {
                            detail.CheckItem = int.Parse(myCheckItem[i].ToString());
                        }
                        if (myCheckNum[i] != null && myCheckNum[i] != "")
                        {
                            detail.CheckNum = Convert.ToDecimal(myCheckNum[i].ToString());
                        }
                        if (myCheckResult[i] != null && myCheckResult[i] != "")
                        {
                            detail.CheckResult = myCheckResult[i].ToString();
                        }
                        if (myCheckStandard[i] != null && myCheckStandard[i] != "")
                        {
                            detail.CheckStandard = myCheckStandard[i].ToString();
                        }
                        if (myCheckValue[i] != null && myCheckValue[i] != "")
                        {
                            detail.CheckValue = myCheckValue[i].ToString();
                        }
                        if (myDept[i] != null && myDept[i] != "")
                        {
                            detail.CheckDeptID = int.Parse(myDept[i].ToString());
                        }
                        if (myIsPass[i] != null && myIsPass[i] != "")
                        {
                            detail.isPass = myIsPass[i].ToString();
                        }

                        if (!string.IsNullOrEmpty(myLowerLimit[i]))
                        {
                            detail.LowerLimit = myLowerLimit[i].ToString();
                        }
                        detail.NotPassNum = -1;
                        if (myNoPassNum[i] != null && myNoPassNum[i] != "")
                        {
                            detail.NotPassNum = Convert.ToDecimal(myNoPassNum[i].ToString());
                        }
                        if (!string.IsNullOrEmpty(myNormUpLimit[i]))
                        {
                            detail.NormUpLimit = myNormUpLimit[i].ToString();
                        }
                        detail.PassNum = -1;
                        if (myPassNum[i] != null && myPassNum[i] != "")
                        {
                            detail.PassNum = Convert.ToDecimal(myPassNum[i].ToString());
                        }
                        if (myRemark[i] != null && myRemark[i] != "")
                        {
                            detail.Remark = myRemark[i].ToString();
                        }
                        if (!string.IsNullOrEmpty(myStandardValue[i]))
                        {
                            detail.StandardValue = myStandardValue[i].ToString();
                        }
                        detailllist.Add(detail);
                    }
                }
            }
            if (action == "edit")
            {
                model.ID = ID;
                if (model.FromType == "3" || model.FromType == "4")
                {
                    string CheckValue = CheckReportBus.GetCheckSave(model);
                    if (CheckValue == "")
                    {
                        CheckValue = "0";
                    }
                    if (CheckValue == "none")
                    {
                        if (CheckValue == "none")
                            CheckValue = "0";
                        jc = new JsonClass("当前的报检数量大于源单的未报检数量！", "CheckValue" + "|" + "", 1);
                        context.Response.Write(jc);
                        return;
                    }
                    if (CheckValue != "00")
                    {
                        if (model.CheckNum > Convert.ToDecimal(CheckValue))
                        {
                            jc = new JsonClass("当前的报检数量大于源单的未报检数量！", "CheckValue" + "|" + "", 1);
                            context.Response.Write(jc);
                            return;
                        }
                    }
                }
                if (CheckReportBus.UpdateReport(model, detailllist, mySortID, htExtAttr))
                {
                    try
                    {
                        jc = new JsonClass("保存成功", model.ID.ToString() + "|" + model.ReportNo.ToString() + "|" + model.ModifiedDate.ToString("yyyy-MM-dd") + "|" + EmployeeName, 1);
                    }
                    catch
                    { jc = new JsonClass("保存失败", model.ID.ToString() + "|" + model.ReportNo.ToString(), 1); }
                }
                else
                {
                    jc = new JsonClass("保存失败", "", 0);
                }
                context.Response.Write(jc);
                return;

            }
            else if (action == "add")
            {
                try
                {
                    if (model.FromType == "3" || model.FromType == "4")
                    {
                        string CheckValue = CheckReportBus.GetCheckSave(model);
                        if (CheckValue == "")
                        {
                            CheckValue = "0";
                        }
                        if (CheckValue == "none")
                        {
                            if (CheckValue == "none")
                                CheckValue = "0";
                            jc = new JsonClass("当前的报检数量大于源单的未报检数量！", "CheckValue" + "|" + "", 1);
                            context.Response.Write(jc);
                            return;
                        }
                        if (CheckValue != "00")
                        {
                            if (model.CheckNum > Convert.ToDecimal(CheckValue))
                            {
                                jc = new JsonClass("当前的报检数量大于源单的未报检数量！", "CheckValue" + "|" + "", 1);
                                context.Response.Write(jc);
                                return;
                            }
                        }
                    }
                    if (CheckReportBus.AddReport(model, detailllist, htExtAttr) == true)
                    {

                        jc = new JsonClass("保存成功", model.ID.ToString() + "| " + model.ReportNo.ToString() + "|" + model.ModifiedDate.ToString("yyyy-MM-dd") + "|" + EmployeeName, 1);
                    }
                    else
                    {
                        jc = new JsonClass("保存失败", "", 0);
                    }
                }
                catch
                { }
            }
        }

        #endregion
        context.Response.Write(jc);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    /// <summary>
    /// 获取扩展属性值
    /// </summary>
    /// <returns></returns>
    private Hashtable GetExtAttr(HttpContext context)
    {
        try
        {
            Hashtable ht = new Hashtable();
            string strKeyList = context.Request.Form["keyList"].ToString().Trim();
            string[] arrKey = strKeyList.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //取得扩展属性值
            for (int y = 0; y < arrKey.Length; y++)
            {
                //不为空的字段名才取值
                if (arrKey[y].Trim().Length != 0)
                {
                    ht.Add(arrKey[y].Trim(), context.Request.Form[arrKey[y].Trim()].ToString().Trim());//添加keyvalue键值对
                }
            }
            return ht;
        }
        catch (Exception)
        { return null; }
    }


}