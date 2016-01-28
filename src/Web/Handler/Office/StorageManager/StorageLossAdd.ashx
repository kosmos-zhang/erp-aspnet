<%@ WebHandler Language="C#" Class="StorageLossAdd" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using System.Web.SessionState;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;

public class StorageLossAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//[待修改] 
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string LoginUserName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//登陆用户名
        string Date = System.DateTime.Now.ToString("yyyy-MM-dd");


        bool ifdele = true;
        int delcout = 0;//删除多少项
        JsonClass jc = new JsonClass();
        string action = context.Request.QueryString["Act"];
        if ("Del".Equals(action.Trim()))
        {
            string strID = context.Request.QueryString["strID"].ToString();
            string[] IDArray = null;
            IDArray = strID.Split(',');

            for (int i = 0; i < IDArray.Length; i++)
            {
                if (!StorageLossBus.IsDelStorageLoss(IDArray[i].ToString(), companyCD))
                {
                    ifdele = false;
                    break;
                }
            }

            if (ifdele == true)
            {
                for (int i = 0; i < IDArray.Length; i++)
                {

                    if (StorageLossBus.DeleteStorageLoss(IDArray[i].ToString(), companyCD))
                    {
                        delcout += 1;
                    }
                }
                if (delcout > 0)
                {
                    jc = new JsonClass("删除成功", "", 1);
                }
                else
                {
                    jc = new JsonClass("删除失败", "", 0);
                }
            }
            else
            {
                jc = new JsonClass("已提交审批的单据不允许删除！", "", 0);
            }
        }
        else
        {
            int ID = int.Parse(context.Request.QueryString["ID"].ToString());

            if (context.Request.QueryString["Act"].ToString().Trim() == "Confirm")//确认
            {
                if (ID > 0)
                {
                    StorageLossModel model = new StorageLossModel();
                    model.ID = ID.ToString();
                    model.CompanyCD = companyCD;
                    model.Confirmor = loginUser_id.ToString();
                    model.ModifiedUserID = LoginUserID;
                    model.StorageID = context.Request.QueryString["StorageID"].ToString().Trim();
                    if (StorageLossBus.ConfirmBill(model))
                    {
                        jc = new JsonClass("确认成功", LoginUserName + "|" + Date, 1);
                    }
                    else
                    {
                        jc = new JsonClass("确认失败", "", 0);
                    }
                }
                else
                {
                    jc = new JsonClass("请求单据不存在", "", 0);
                }
            }
            else if (context.Request.QueryString["Act"].ToString().Trim() == "UnConfirm")//取消确认
            {
                if (ID > 0)
                {
                    StorageLossModel model = new StorageLossModel();
                    model.ID = ID.ToString();
                    model.CompanyCD = companyCD;
                    model.Confirmor = loginUser_id.ToString();
                    model.ModifiedUserID = LoginUserID;
                    model.StorageID = context.Request.QueryString["StorageID"].ToString().Trim();
                    if (StorageLossBus.CancelConfirmBill(model))
                    {
                        jc = new JsonClass("取消确认成功", LoginUserName + "|" + Date, 1);
                    }
                    else
                    {
                        jc = new JsonClass("取消确认失败", "", 0);
                    }
                }
                else
                {
                    jc = new JsonClass("请求单据不存在", "", 0);
                }
            }
            else
            {
                if (ID > 0)
                {
                    #region 修改其他出库
                    string DetailProductID = context.Request.QueryString["DetailProductID"].ToString().Trim();//物品ID
                    string DetailSortNo = context.Request.QueryString["DetailSortNo"].ToString().Trim();//序号
                    string DetailUnitPrice = context.Request.QueryString["DetailUnitPrice"].ToString().Trim();//单价
                    string DetailProductCount = context.Request.QueryString["DetailProductCount"].ToString().Trim();//数量
                    string DetailTotalPrice = context.Request.QueryString["DetailTotalPrice"].ToString().Trim();//总价
                    string DetailRemark = context.Request.QueryString["DetailRemark"].ToString().Trim();//备注

                    string DetailUnitID = "";//实际单位
                    string DetailBaseUnitID = "";//基本单位
                    string DetailBaseCount = "";//基本数量
                    string DetailBasePrice = "";//基本单价
                    string DetailExtRate = "";//比率

                    try
                    {
                        DetailUnitID = context.Request.QueryString["DetailUnitID"].ToString().Trim();
                    }
                    catch (Exception) { }//实际单位
                    try { DetailBaseUnitID = context.Request.QueryString["DetailBaseUnitID"].ToString().Trim(); }
                    catch (Exception) { }//基本单位
                    try { DetailBaseCount = context.Request.QueryString["DetailBaseCount"].ToString().Trim(); }
                    catch (Exception) { }//基本数量
                    try { DetailBasePrice = context.Request.QueryString["DetailBasePrice"].ToString().Trim(); }
                    catch (Exception) { }//基本单价
                    try { DetailExtRate = context.Request.QueryString["DetailExtRate"].ToString().Trim(); }
                    catch (Exception) { }//比率

                    string DetailBatchNo = context.Request.QueryString["DetailBatchNo"].ToString().Trim();//批次
                    
                    StorageLossModel model = new StorageLossModel();
                    model.CompanyCD = companyCD;
                    model.ID = ID.ToString();
                    model.LossNo = context.Request.QueryString["txtLossNo"].ToString().Trim();
                    model.Title = context.Request.QueryString["txtTitle"].ToString().Trim();
                    if (context.Request.QueryString["txtExecutor"].ToString().Trim() != "undefined")
                    {
                        model.Executor = context.Request.QueryString["txtExecutor"].ToString().Trim();
                    }
                    if (context.Request.QueryString["txtDept"].ToString().Trim() != "undefined")
                    {
                        model.DeptID = context.Request.QueryString["txtDept"].ToString().Trim();
                    }
                    model.ReasonType = context.Request.QueryString["ddlReason"].ToString().Trim();
                    model.StorageID = context.Request.QueryString["ddlStorage"].ToString().Trim();
                    model.LossDate = context.Request.QueryString["txtLossDate"].ToString().Trim();
                    model.TotalPrice = context.Request.QueryString["txtTotalPrice"].ToString().Trim();
                    model.CountTotal = context.Request.QueryString["txtTotalCount"].ToString().Trim();
                    model.Summary = context.Request.QueryString["txtSummary"].ToString().Trim();
                    model.Attachment = context.Request.QueryString["hfPageAttachment"].ToString().Trim();
                    model.BillStatus = context.Request.QueryString["sltBillStatus"].ToString().Trim();
                    model.Remark = context.Request.QueryString["txtRemark"].ToString().Trim();
                    model.ModifiedUserID = LoginUserID;

                    List<StorageLossDetailModel> modellist = new List<StorageLossDetailModel>();
                    string[] sortNo = DetailSortNo.Split(',');
                    string[] productID = DetailProductID.Split(',');
                    string[] productCount = DetailProductCount.Split(',');
                    string[] unitPrice = DetailUnitPrice.Split(',');
                    string[] totalPrice = DetailTotalPrice.Split(',');
                    string[] remark = DetailRemark.Split(',');

                    //多计量单位、批次（2010.04.12）
                    string[] UnitID = DetailUnitID.Split(',');
                    string[] BaseUnitID = DetailBaseUnitID.Split(',');
                    string[] BaseCount = DetailBaseCount.Split(',');
                    string[] BasePrice = DetailBasePrice.Split(',');
                    string[] ExtRate = DetailExtRate.Split(',');
                    string[] BatchNo = DetailBatchNo.Split(',');
                    if (productID.Length >= 1)
                    {
                        for (int i = 0; i < productID.Length; i++)
                        {
                            StorageLossDetailModel model1 = new StorageLossDetailModel();
                            model1.LossNo = model.LossNo;
                            model1.SortNo = sortNo[i];
                            model1.ProductID = productID[i];
                            model1.ProductCount = BaseCount[i];//基本数量
                            model1.UnitPrice = BasePrice[i];//基本单价
                            model1.CostPrice = totalPrice[i];
                            model1.Remark = remark[i];
                            model1.CompanyCD = companyCD;

                            model1.UnitID = BaseUnitID[i];//基本单位
                            try
                            {
                                model1.UsedUnitID = UnitID[i];
                            }
                            catch (Exception) { }//实际单位
                            try
                            { model1.UsedPrice = unitPrice[i]; }
                            catch (Exception) { }//实际单价
                            try
                            { model1.UsedUnitCount = productCount[i]; }
                            catch (Exception) { }//实际数量
                            try
                            { model1.ExRate = ExtRate[i]; }
                            catch (Exception) { }//比率
                            model1.BatchNo = BatchNo[i];//批次
                            modellist.Add(model1);
                        }
                    }
                    //获取扩展属性
                    Hashtable ht = GetExtAttr(context);
                    
                    if (StorageLossBus.UpdateStorageLoss(model,ht, modellist))
                    {
                        jc = new JsonClass("保存成功", LoginUserName + "|" + Date, 1);
                    }
                    else
                    {
                        jc = new JsonClass("保存失败", "", 0);
                    }
                    #endregion
                }
                else
                {
                    #region 添加其他出库

                    string DetailProductID = context.Request.QueryString["DetailProductID"].ToString().Trim();//物品ID
                    string DetailSortNo = context.Request.QueryString["DetailSortNo"].ToString().Trim();//序号
                    string DetailUnitPrice = context.Request.QueryString["DetailUnitPrice"].ToString().Trim();//单价
                    string DetailProductCount = context.Request.QueryString["DetailProductCount"].ToString().Trim();//数量
                    string DetailTotalPrice = context.Request.QueryString["DetailTotalPrice"].ToString().Trim();//总价
                    string DetailRemark = context.Request.QueryString["DetailRemark"].ToString().Trim();//备注

                    string DetailUnitID = "";//实际单位
                    string DetailBaseUnitID = "";//基本单位
                    string DetailBaseCount = "";//基本数量
                    string DetailBasePrice = "";//基本单价
                    string DetailExtRate = "";//比率

                    try
                    {
                        DetailUnitID = context.Request.QueryString["DetailUnitID"].ToString().Trim();
                    }
                    catch (Exception) { }//实际单位
                    try { DetailBaseUnitID = context.Request.QueryString["DetailBaseUnitID"].ToString().Trim(); }
                    catch (Exception) { }//基本单位
                    try { DetailBaseCount = context.Request.QueryString["DetailBaseCount"].ToString().Trim(); }
                    catch (Exception) { }//基本数量
                    try { DetailBasePrice = context.Request.QueryString["DetailBasePrice"].ToString().Trim(); }
                    catch (Exception) { }//基本单价
                    try { DetailExtRate = context.Request.QueryString["DetailExtRate"].ToString().Trim(); }
                    catch (Exception) { }//比率

                    string DetailBatchNo = context.Request.QueryString["DetailBatchNo"].ToString().Trim();//批次
                    StorageLossModel model = new StorageLossModel();
                    model.CompanyCD = companyCD;
                    if (context.Request.QueryString["bmgz"].ToString().Trim() == "zd")
                    {
                        model.LossNo = ItemCodingRuleBus.GetCodeValue(context.Request.QueryString["txtLossNo"].ToString().Trim(), "StorageLoss", "LossNo");
                        if (string.IsNullOrEmpty(model.LossNo))
                        {
                            jc = new JsonClass("该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!", "", 6);
                            context.Response.Write(jc);
                            context.Response.End();
                        }
                    }
                    else
                    {
                        model.LossNo = context.Request.QueryString["txtLossNo"].ToString().Trim();
                    }
                    bool isExsist = PrimekeyVerifyBus.CheckCodeUniq("StorageLoss", "LossNo", model.LossNo);
                    if (!isExsist)
                    {
                        jc = new JsonClass("该编号已被使用，请输入未使用的编号！", "", 2);
                    }
                    else
                    {
                        model.Title = context.Request.QueryString["txtTitle"].ToString().Trim();
                        if (context.Request.QueryString["txtExecutor"].ToString().Trim() != "undefined")
                        {
                            model.Executor = context.Request.QueryString["txtExecutor"].ToString().Trim();
                        }
                        if (context.Request.QueryString["txtDept"].ToString().Trim() != "undefined")
                        {
                            model.DeptID = context.Request.QueryString["txtDept"].ToString().Trim();
                        }
                        model.ReasonType = context.Request.QueryString["ddlReason"].ToString().Trim();
                        model.StorageID = context.Request.QueryString["ddlStorage"].ToString().Trim();
                        model.LossDate = context.Request.QueryString["txtLossDate"].ToString().Trim();
                        model.TotalPrice = context.Request.QueryString["txtTotalPrice"].ToString().Trim();
                        model.CountTotal = context.Request.QueryString["txtTotalCount"].ToString().Trim();
                        model.Summary = context.Request.QueryString["txtSummary"].ToString().Trim();
                        model.Attachment = context.Request.QueryString["hfPageAttachment"].ToString().Trim();
                        model.BillStatus = context.Request.QueryString["sltBillStatus"].ToString().Trim();
                        model.Remark = context.Request.QueryString["txtRemark"].ToString().Trim();
                        model.ModifiedUserID = LoginUserID;
                        model.Creator = loginUser_id.ToString();

                        List<StorageLossDetailModel> modellist = new List<StorageLossDetailModel>();
                        string[] sortNo = DetailSortNo.Split(',');
                        string[] productID = DetailProductID.Split(',');
                        string[] productCount = DetailProductCount.Split(',');
                        string[] unitPrice = DetailUnitPrice.Split(',');
                        string[] totalPrice = DetailTotalPrice.Split(',');
                        string[] remark = DetailRemark.Split(',');

                        //多计量单位、批次（2010.04.12）
                        string[] UnitID = DetailUnitID.Split(',');
                        string[] BaseUnitID = DetailBaseUnitID.Split(',');
                        string[] BaseCount = DetailBaseCount.Split(',');
                        string[] BasePrice = DetailBasePrice.Split(',');
                        string[] ExtRate = DetailExtRate.Split(',');
                        string[] BatchNo = DetailBatchNo.Split(',');
                        if (productID.Length >= 1)
                        {
                            for (int i = 0; i < productID.Length; i++)
                            {
                                StorageLossDetailModel model2 = new StorageLossDetailModel();
                                model2.LossNo = model.LossNo;
                                model2.SortNo = sortNo[i];
                                model2.ProductID = productID[i];
                                model2.ProductCount = BaseCount[i];//基本数量
                                model2.UnitPrice = BasePrice[i];//基本单价
                                model2.CostPrice = totalPrice[i];
                                model2.Remark = remark[i];
                                model2.CompanyCD = companyCD;

                                model2.UnitID = BaseUnitID[i];//基本单位
                                try
                                {
                                    model2.UsedUnitID = UnitID[i];
                                }
                                catch (Exception) { }//实际单位
                                try
                                { model2.UsedPrice = unitPrice[i]; }
                                catch (Exception) { }//实际单价
                                try
                                { model2.UsedUnitCount = productCount[i]; }
                                catch (Exception) { }//实际数量
                                try
                                { model2.ExRate = ExtRate[i]; }
                                catch (Exception) { }//比率
                                model2.BatchNo = BatchNo[i];//批次
                                modellist.Add(model2);
                            }
                        }
                        int IndexIDentity = 0;
                        //获取扩展属性
                        Hashtable ht = GetExtAttr(context);
                        if (StorageLossBus.InsertStorageLoss(model, modellist,ht, out IndexIDentity))
                        {
                            jc = new JsonClass("保存成功", IndexIDentity.ToString() + "|" + model.LossNo.ToString() + "|" + LoginUserName + "|" + Date, 1);
                        }
                        else
                        {
                            jc = new JsonClass("保存失败", "", 0);
                        }
                    }

                }
            }


                    #endregion
        }
        context.Response.Write(jc);
    }
    /// <summary>
    /// 获取扩展属性值
    /// </summary>
    /// <returns></returns>
    private Hashtable GetExtAttr(HttpContext _context)
    {
        try
        {
            Hashtable ht = new Hashtable();
            string strKeyList = GetParam(_context, "keyList").Trim();
            string[] arrKey = strKeyList.Split('|');
            //取得扩展属性值
            for (int y = 0; y < arrKey.Length; y++)
            {
                //不为空的字段名才取值
                if (arrKey[y].Trim().Length != 0)
                {
                    ht.Add(arrKey[y].Trim(), GetParam(_context, arrKey[y].Trim()).Trim());//添加keyvalue键值对
                }
            }
            return ht;
        }
        catch (Exception)
        { return null; }
    }
    /// <summary>
    /// 获取REQUEST的参数值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected string GetParam(HttpContext _context, string key)
    {
        if (_context.Request[key] == null)
        {
            return string.Empty;
        }
        else
        {
            if (_context.Request[key].ToString().Trim() + "" == "")
            {
                return string.Empty;
            }
            else
            {
                return _context.Request[key].ToString().Trim();
            }
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}