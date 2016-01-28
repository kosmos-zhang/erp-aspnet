<%@ WebHandler Language="C#" Class="StorageNoPassAdd" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using System.Web.SessionState;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;
public class StorageNoPassAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
        JsonClass jc = new JsonClass();
        int ID = 0;
        string action = "";
        if (context.Request.Form["ID"] != null)
        {
            ID = int.Parse(context.Request.Form["ID"]);
        }
        if (context.Request.Form["myAction"] != null)
        {
            action = context.Request.Form["myAction"].ToString();
        }
        if (context.Request.QueryString["Action"] != null)
        {
            if (context.Request.QueryString["Action"].ToString() == "Del")
            {
                string strID = context.Request.QueryString["strID"].ToString();
                if (CheckNotPassBus.DelNoPass(strID, companyCD))
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

        #region 添加质检报告入库

        if (action == "Confirm" || action == "UnConfirm")  //确认
        {
            if (ID > 0)
            {
                CheckNotPassModel model = new CheckNotPassModel();
                model.ID = ID;
                model.CompanyCD = companyCD;
                model.Confirmor = loginUser_id;
                model.ConfirmorDate = DateTime.Now;
                model.ModifiedUserID = LoginUserID;
                model.ModifiedDate = DateTime.Now;
                string CheckNum = context.Request.Form["CheckNum"].ToString();
                model.ReportID = int.Parse(context.Request.Form["ReportID"].ToString());
                string AllNum = context.Request.Form["AllNum"].ToString();
                string NotPassNum = CheckNotPassBus.GetNotPassNum(model);

                if (action == "Confirm")
                {
                    if (Convert.ToDecimal(AllNum) > Convert.ToDecimal(NotPassNum))
                    {
                        jc = new JsonClass("单据的处置数量不能大于源单的未处置数量", "none" + "|", 1);
                        context.Response.Write(jc);
                        return;
                    }
                    if (CheckNotPassBus.ConfirmBill(model, CheckNum))
                    {
                        jc = new JsonClass("确认成功", EmployeeName + "|" + model.ModifiedDate.ToString("yyyy-MM-dd"), 1);
                    }
                    else
                    {
                        jc = new JsonClass("确认失败", "", 0);
                    }
                }
                if (action == "UnConfirm")
                {
                    if (CheckNotPassBus.UnConfirmBill(model, CheckNum))
                    {
                        jc = new JsonClass("取消确认成功", LoginUserID + "|" + model.ModifiedDate.ToString("yyyy-MM-dd"), 1);
                    }
                    else
                    {
                        jc = new JsonClass("取消确认失败", "", 0);
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

        if (action == "Close")  //结单
        {
            if (ID > 0)
            {
                CheckNotPassModel model = new CheckNotPassModel();
                string theMethod = context.Request.Form["myMethod"].ToString();
                model.ID = ID;
                model.CompanyCD = companyCD;
                model.Closer = loginUser_id;
                model.CloserDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                model.ModifiedUserID = LoginUserID;
                model.ModifiedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                if (theMethod == "0")
                {
                    if (CheckNotPassBus.CloseBill(model, theMethod))
                    {
                        jc = new JsonClass("结单成功", EmployeeName + "|" + model.ModifiedDate.ToString("yyyy-MM-dd"), 1);
                    }
                    else
                    {
                        jc = new JsonClass("结订单失败", "", 0);
                    }
                }
                else
                {
                    if (CheckNotPassBus.CloseBill(model, theMethod))
                    {

                        jc = new JsonClass("取消结单成功", EmployeeName + "|" + model.ModifiedDate.ToString("yyyy-MM-dd"), 1);
                    }
                    else
                    {
                        jc = new JsonClass("取消结订单失败", "", 0);
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
        else
        {
            string DetailSortNo = context.Request.Form["DetailSortNo"].ToString().Trim();//
            string DetailReason = context.Request.Form["DetailReason"].ToString().Trim();//
            string DetailNum = context.Request.Form["DetailNum"].ToString().Trim();//
            string DetailProcessWay = context.Request.Form["DetailProcessWay"].ToString().Trim();//    ..
            string DetailRate = context.Request.Form["DetailRate"].ToString().Trim();//
            string DetailRemark = context.Request.Form["DetailRemark"].ToString().Trim();//         

            CheckNotPassModel model = new CheckNotPassModel();

            if (context.Request.Form["bmgz"].ToString().Trim() == "zd")
            {
                model.ProcessNo = ItemCodingRuleBus.GetCodeValue(context.Request.Form["ProcessNo"].ToString().Trim());  //单据编号
            }
            else
            {
                model.ProcessNo = context.Request.Form["ProcessNo"].ToString().Trim();
            }
            if (action == "add")
            {
                bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("CheckNotPass", "ProcessNo", model.ProcessNo);
                if (!isAlready || string.IsNullOrEmpty(model.ProcessNo))
                {
                    if (string.IsNullOrEmpty(model.ProcessNo))
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
            model.BillStatus = context.Request.Form["BillStatus"].ToString();
            if (model.BillStatus == "2")
            {
                model.BillStatus = "3";
            }
            model.CreateDate = DateTime.Now;
            model.Creator = loginUser_id;
            model.Executor = int.Parse(context.Request.Form["ExecutorID"].ToString());
            model.FromType = context.Request.Form["FromType"].ToString();
            model.ModifiedDate = DateTime.Now;
            model.ModifiedUserID = LoginUserID;
            model.ProcessDate = Convert.ToDateTime(context.Request.Form["ProcessDate"].ToString());
            model.Remark = context.Request.Form["Remark"].ToString();
            model.Attachment = context.Request.Form["Attachment"].ToString();
            model.ReportID = int.Parse(context.Request.Form["ReportID"].ToString());
            model.Title = context.Request.Form["Title"].ToString();

            // 获得扩展属性
            Hashtable htExtAttr = GetExtAttr(context);

            List<CheckNotPassDetailModel> detailllist = new List<CheckNotPassDetailModel>();
            string[] mySortID = DetailSortNo.Split(',');
            string[] myNum = DetailNum.Split(',');
            string[] myReason = DetailReason.Split(',');
            string[] myProcessWay = DetailProcessWay.Split(',');
            string[] myRate = DetailRate.Split(',');
            string[] myRemark = DetailRemark.Split(',');

            if (mySortID.Length >= 1)
            {
                for (int i = 0; i < mySortID.Length; i++)
                {
                    if (mySortID[i] != "" && mySortID[i] != null)
                    {
                        CheckNotPassDetailModel detail = new CheckNotPassDetailModel();

                        detail.SortNo = int.Parse(mySortID[i]);

                        if (myNum[i] != null && myNum[i] != "")
                        {
                            detail.NotPassNum = Convert.ToDecimal(myNum[i].ToString());
                        }
                        if (myReason[i] != null && myReason[i] != "")
                        {
                            detail.ReasonID = int.Parse(myReason[i].ToString());
                        }
                        if (myProcessWay[i] != null && myProcessWay[i] != "")
                        {
                            detail.ProcessWay = int.Parse(myProcessWay[i].ToString());
                        }
                        if (myRate[i] != null && myRate[i] != "")
                        {
                            detail.Rate = Convert.ToDecimal(myRate[i].ToString());
                        }
                        if (myRemark[i] != null && myRemark[i] != "")
                        {
                            detail.Remark = myRemark[i].ToString();
                        }

                        detailllist.Add(detail);
                    }
                }
            }
            if (action == "edit")
            {
                model.ID = ID;
                if (CheckNotPassBus.UpdateNoPassInfo(model, detailllist, mySortID, htExtAttr))
                {

                    jc = new JsonClass("保存成功", model.ID.ToString() + "|" + model.ProcessNo.ToString() + "|" + model.ModifiedDate.ToString("yyyy-MM-dd") + "|" + EmployeeName, 1);
                }
                else
                {
                    jc = new JsonClass("修改失败", "", 0);
                }
                context.Response.Write(jc);
                return;

            }
            else if (action == "add")
            {
                if (CheckNotPassBus.AddNoPass(model, detailllist, htExtAttr))
                {
                    jc = new JsonClass("保存成功", model.ID.ToString() + "|" + model.ProcessNo.ToString() + "|" + model.ModifiedDate.ToString("yyyy-MM-dd") + "|" + EmployeeName, 1);
                }
                else
                {
                    jc = new JsonClass("保存失败", "", 0);
                }
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