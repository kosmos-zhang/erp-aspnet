<%@ WebHandler Language="C#" Class="StorageQualityCheckAdd" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using System.Web.SessionState;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;

public class StorageQualityCheckAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    private UserInfoUtil userInfo = null;

    public void ProcessRequest(HttpContext context)
    {
        userInfo = SessionUtil.Session["UserInfo"] as UserInfoUtil;
        string companyCD = userInfo.CompanyCD;
        int loginUser_id = int.Parse(userInfo.EmployeeID.ToString());
        string LoginUserID = userInfo.UserID;
        string EmployeeName = userInfo.EmployeeName;
        JsonClass jc = new JsonClass();
        string action = "";
        int ID = 0;
        if (context.Request.Form["action"] != null)
        {
            action = context.Request.Form["action"];
            ID = int.Parse(context.Request.Form["ID"]);
        }
        if (context.Request.QueryString["action"] != null)
        {
            action = context.Request.QueryString["action"];
            ID = int.Parse(context.Request.QueryString["ID"]);
        }

        #region 添加质检入库
        if (action == "Confirm")  //确认
        {
            if (ID > 0)
            {
                StorageQualityCheckApplay model = new StorageQualityCheckApplay();
                string FromType = context.Request.Form["ddlFromType"].ToString().Trim();
                string ListID = context.Request.Form["DetailFroBillID"].ToString();
                string CheckedCount = context.Request.Form["DetailCheckedCount"].ToString();
                model.ID = ID;
                model.ApplyNO = context.Request.Form["ApplyNO"].ToString().Trim();
                model.CompanyCD = companyCD;
                model.Confirmor = loginUser_id;
                model.ConfirmDate = DateTime.Parse(DateTime.Now.ToString());
                model.ModifiedUserID = LoginUserID;
                model.MdodifiedDate = DateTime.Parse(DateTime.Now.ToString());
                model.FromType = FromType;
                try
                {
                    if (model.FromType != "0")
                    {
                        if (StorageQualityCheckPro.GetCheckedCount(model, ListID, CheckedCount) == false)
                        {

                            jc = new JsonClass("单据的报检数量不能超过源单的未报检数量！", "none", 1);
                            context.Response.Write(jc);
                            return;
                        }
                    }
                    if (StorageQualityCheckPro.ConfirmBill(model))
                    {
                        if (model.FromType != "0")
                        {
                            StorageQualityCheckPro.UpdateBackkData(CheckedCount, ListID, FromType);
                        }
                        jc = new JsonClass("确认成功", EmployeeName + "|" + model.ConfirmDate.ToString("yyyy-MM-dd"), 1);
                    }
                    else
                    {
                        jc = new JsonClass("确认失败", "", 0);
                    }
                }
                catch
                { }
            }
            else
            {
                jc = new JsonClass("请求单据不存在", "", 0);
            }
            context.Response.Write(jc);
            return;

        }
        if (action == "UnConfirm")//取消确认
        {
            if (ID > 0)
            {
                StorageQualityCheckApplay model = new StorageQualityCheckApplay();
                string FromType = context.Request.Form["ddlFromType"].ToString().Trim();
                string ListID = context.Request.Form["DetailFroBillID"].ToString();
                string CheckedCount = context.Request.Form["DetailCheckedCount"].ToString();
                model.ApplyNO = context.Request.Form["ApplyNO"].ToString().Trim();
                model.ID = ID;
                model.MdodifiedDate = DateTime.Now;
                try
                {
                    string IsTransfer = StorageQualityCheckPro.IsTransfer(model.ID, companyCD);
                    if (Convert.ToInt32(IsTransfer) > 0)
                    {
                        jc = new JsonClass("该单据已被其它单据调用了，不允许取消确认！", "no" + "|" + "", 1);
                        context.Response.Write(jc);
                        return;
                    }
                    if (StorageQualityCheckPro.UpBackkData(CheckedCount, ListID, FromType, model))
                    {
                        jc = new JsonClass("取消确认成功", LoginUserID + "|" + model.MdodifiedDate.ToString("yyyy-MM-dd") + "|" + IsTransfer, 1);
                    }
                    else
                    {
                        jc = new JsonClass("取消确认失败", "", 0);
                    }
                }
                catch
                { }
            }
            else
            {
                jc = new JsonClass("请求单据不存在", "", 0);
            }
            context.Response.Write(jc);
            return;
        }
        if (action == "Close")
        {
            if (ID > 0)
            {
                StorageQualityCheckApplay model = new StorageQualityCheckApplay();
                string myMethod = context.Request.Form["myMethod"].ToString();
                model.ID = ID;
                model.Closer = loginUser_id;
                model.CloseDate = DateTime.Now;
                model.ModifiedUserID = LoginUserID;
                model.CompanyCD = companyCD;
                model.MdodifiedDate = DateTime.Now;
                if (myMethod == "0")
                {
                    if (StorageQualityCheckPro.CloseBill(model, myMethod))
                    {
                        jc = new JsonClass("结单成功", EmployeeName + "|" + model.CloseDate.ToString("yyyy-MM-dd"), 1);
                    }
                    else
                    {
                        jc = new JsonClass("结订单失败", "", 0);
                    }
                }
                else
                {
                    if (StorageQualityCheckPro.CloseBill(model, myMethod))
                    {
                        jc = new JsonClass("取消结单成功", EmployeeName + "|" + model.CloseDate.ToString("yyyy-MM-dd"), 1);
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
        else if (action == "updateattachment")
        {// 更新附件
            string attachment = context.Request.Form["Attachment"].ToString().Trim();      // 附件
            StorageQualityCheckPro.UpDateAttachment(ID, attachment);
        }
        else
        {
            string DetailSortNo = context.Request.Form["DetailSortNo"].ToString().Trim();//

            StorageQualityCheckApplay model = new StorageQualityCheckApplay();
            model.FromType = context.Request.Form["ddlFromType"].ToString().Trim();
            string DetailProID = context.Request.Form["DetailProID"].ToString().Trim();//物品ID

            string DetailQuaCheckCount = context.Request.Form["DetailQuaCheckCount"].ToString().Trim();//
            string DetailFromBillID = "0";
            string DetailFromBillNO = "0";
            if (model.FromType != "0")
            {
                DetailFromBillID = context.Request.Form["DetailFromBillID"].ToString().Trim();//
                DetailFromBillNO = context.Request.Form["DetailFromBillNO"].ToString().Trim();//
            }
            string DetailCheckedCount = context.Request.Form["DetailCheckedCount"].ToString().Trim();//
            string DetailRemark = context.Request.Form["DetailRemark"].ToString().Trim();//
            string DetailUnitID = context.Request.Form["DetailUnitID"].ToString();
            string DetailUsedUnitID = context.Request.Form["DetailUsedUnitID"];
            string DetailUsedUnitCount = context.Request.Form["DetailUsedUnitCount"];
            string DetailFromType = null;
            model.CompanyCD = companyCD;
            if (context.Request.Form["bmgz"].ToString().Trim() == "zd")
            {
                model.ApplyNO = ItemCodingRuleBus.GetCodeValue(context.Request.Form["txtInNo"].ToString().Trim());  //单据编号
            }
            else
            {
                model.ApplyNO = context.Request.Form["txtInNo"].ToString().Trim();
            }
            model.Title = context.Request.Form["txtTitle"].ToString().Trim();
            DetailFromType = model.FromType;
            model.Attachment = context.Request.Form["Attachment"].ToString();
            model.BillStatus = context.Request.Form["ddlBillStatus"].ToString();
            if (context.Request.Form["ddlBillStatus"].ToString() == "2")
            {
                model.BillStatus = "3";
            }
            if (context.Request.Form["txtEnterDate"] != null && context.Request.Form["txtEnterDate"].ToString() != "")
            {
                model.CheckDate = Convert.ToDateTime(context.Request.Form["txtEnterDate"].ToString());
            }
            else
            {
                model.CheckDate = Convert.ToDateTime("9999-09-09");
            }
            model.CheckDeptID = context.Request.Form["CheckDeptId"].ToString();
            model.Checker = context.Request.Form["Checker"].ToString();
            model.CheckMode = context.Request.Form["CheckMode"].ToString();
            model.CheckType = context.Request.Form["CheckType"].ToString();
            model.CreateDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            model.Creater = int.Parse(context.Request.Form["txtCreator"].ToString());
            model.CustBigType = context.Request.Form["CustBigType"].ToString();
            model.Principal = int.Parse(context.Request.Form["CustID"].ToString());
            model.CustName = context.Request.Form["txtCustName"].ToString();
            model.CheckDeptID = context.Request.Form["CheckDeptId"].ToString();
            model.MdodifiedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            model.ModifiedUserID = LoginUserID;
            model.DeptID = int.Parse(context.Request.Form["CustBigTypeID"].ToString());
            model.Remark = context.Request.Form["txtRemark"].ToString().Trim();
            model.Principal = int.Parse(context.Request.Form["CustID"].ToString());
            if (context.Request.Form["txtCustNameNo"].ToString() != "0" && context.Request.Form["txtCustNameNo"] != "")
            {
                model.CustID = int.Parse(context.Request.Form["txtCustNameNo"].ToString());
            }
            model.CountTotal = -1;
            if (!string.IsNullOrEmpty(context.Request.Form["myCountTotal"].ToString()))
            {
                string CountTotal = context.Request.Form["myCountTotal"].ToString();
                model.CountTotal = Decimal.Parse(CountTotal);
            }
            Hashtable htExtAttr = GetExtAttr(context);
            List<StorageQualityCheckApplyDetail> detailllist = new List<StorageQualityCheckApplyDetail>();
            string[] sortNo = DetailSortNo.Split(',');
            string[] fromLineNo = DetailFromBillNO.Split(',');
            string[] productID = DetailProID.Split(',');
            string[] detailQuaCheckCount = DetailQuaCheckCount.Split(',');
            string[] detailFromBillID = DetailFromBillID.Split(',');
            string[] detailCheckedCount = DetailCheckedCount.Split(',');
            string[] detailRemark = DetailRemark.Split(',');
            string[] detailUnitID = DetailUnitID.Split(',');
            string[] detailUsedUnitID = DetailUsedUnitID.Split(',');
            string[] detailUsedUnitCount = DetailUsedUnitCount.Split(',');
            if (productID.Length >= 1)
            {
                for (int i = 0; i < productID.Length; i++)
                {
                    if (!string.IsNullOrEmpty(sortNo[i]))
                    {
                        StorageQualityCheckApplyDetail detail = new StorageQualityCheckApplyDetail();
                        if (sortNo[i] != "" && sortNo[i] != null)
                        {
                            detail.SortNo = int.Parse(sortNo[i]);
                        }
                        if (!string.IsNullOrEmpty(detailUnitID[i]))
                        {
                            detail.UnitID = int.Parse(detailUnitID[i].Split('|')[0]);
                        }
                        if (model.FromType != "0")
                        {
                            if (fromLineNo[i] != "" && fromLineNo[i] != null)
                            {
                                detail.FromLineNo = int.Parse(fromLineNo[i]);
                            }
                            if (detailFromBillID[i] != "" && detailFromBillID[i] != null)
                            {
                                detail.FromBillID = int.Parse(detailFromBillID[i]);
                            }
                        }
                        if (productID[i] != "" && productID[i] != null)
                        {
                            detail.ProductID = int.Parse(productID[i]);
                        }
                        if (detailQuaCheckCount[i] != "" && detailQuaCheckCount[i] != null)
                        {
                            detail.ProductCount = decimal.Parse(detailQuaCheckCount[i]);
                        }
                        if (detailCheckedCount[i] != "" && detailCheckedCount[i] != null)
                        {
                            detail.CheckedCount = decimal.Parse(detailCheckedCount[i]);
                        }
                        int unitID = 0;
                        if (detailUsedUnitID.Length == productID.Length && int.TryParse(detailUsedUnitID[i].Split('|')[0], out unitID))
                        {
                            detail.UsedUnitID = unitID;
                        }
                        decimal result = 0m;
                        if (detailUsedUnitCount.Length == productID.Length && decimal.TryParse(detailUsedUnitCount[i], out result))
                        {
                            detail.UsedUnitCount = result;
                        }
                        if (detailUsedUnitID.Length == productID.Length && detailUsedUnitID[i].Split('|').Length == 2 && decimal.TryParse(detailUsedUnitID[i].Split('|')[1], out result))
                        {
                            detail.ExRate = result;
                        }
                        detail.Remark = detailRemark[i];
                        detail.FromType = DetailFromType;
                        detailllist.Add(detail);
                    }
                }
            }
            if (ID > 0)
            {
                model.ID = ID;
                if (StorageQualityCheckPro.UpdateQualityCheck(model, detailllist, productID, htExtAttr))
                {
                    jc = new JsonClass("保存成功", model.ID.ToString() + "|" + model.ApplyNO.ToString() + "|" + model.MdodifiedDate.ToString("yyyy-MM-dd") + "|" + model.ModifiedUserID, 1);
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
                bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("QualityCheckApplay", "ApplyNo", model.ApplyNO);
                if (!isAlready || string.IsNullOrEmpty(model.ApplyNO))
                {
                    if (string.IsNullOrEmpty(model.ApplyNO))
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
                if (StorageQualityCheckPro.AddQualityCheck(model, detailllist, htExtAttr))
                {
                    jc = new JsonClass("保存成功", model.ID.ToString() + "|" + model.ApplyNO.ToString(), 1);
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