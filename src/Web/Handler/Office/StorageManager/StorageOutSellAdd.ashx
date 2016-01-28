<%@ WebHandler Language="C#" Class="StorageOutSellAdd" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using System.Web.SessionState;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;


public class StorageOutSellAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//[待修改] 
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string LoginUserName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//登陆用户名
        string Date = System.DateTime.Now.ToString("yyyy-MM-dd");
        int point = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint.ToString());

        bool ifdele = true;
        int delcout = 0;//删除多少项
        JsonClass jc = new JsonClass();
        string action = context.Request.Params["Act"];
        if ("Del".Equals(action.Trim()))
        {
            string strID = context.Request.Params["strID"].ToString();
            string[] IDArray = null;
            IDArray = strID.Split(',');

            for (int i = 0; i < IDArray.Length; i++)
            {
                if (!StorageCommonBus.IsDelete("officedba.StorageOutSell", IDArray[i].ToString(), companyCD))
                {
                    ifdele = false;
                    break;
                }
            }

            if (ifdele == true)
            {
                for (int i = 0; i < IDArray.Length; i++)
                {

                    if (StorageOutSellBus.DeleteStorageOutSell(IDArray[i].ToString(), companyCD))
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
                jc = new JsonClass("已确认后的单据不允许删除！", "", 0);
            }
        }
        else
        {
            int ID = int.Parse(context.Request.Params["ID"].ToString());
            string retval = "";

            if (context.Request.Params["Act"].ToString().Trim() == "Confirm")
            {
                if (ID > 0)
                {
                    StorageOutSellModel model = new StorageOutSellModel();
                    model.ID = ID.ToString();
                    model.CompanyCD = companyCD;
                    model.Confirmor = loginUser_id.ToString();
                    model.ModifiedUserID = LoginUserID;
                    if (!StorageOutSellBus.ISConfirmBill(model))
                    {
                        jc = new JsonClass("出库数量不能大于源单未出库数量", "", 0);
                        context.Response.Write(jc);
                        return;
                    }
                    else if (StorageOutSellBus.ISBigUseCountWhenCant(model) != "") //|| StorageOutSellBus.ISBigUseCountWhenCant(model) != string.Empty
                    {
                        string[] Info = StorageOutSellBus.ISBigUseCountWhenCant(model).Split('|');
                        jc = new JsonClass("第" + Info[0] + "行超过现有库存；现有库存分别为" + Info[1], "", 0);
                        context.Response.Write(jc);
                        return;
                    }
                    else if (context.Request.Params["isqueren"].ToString().Trim() != "Yes")//还没有经过警告提示，那么就去验证是否有需要警告提示的
                    {
                        if (StorageOutSellBus.ISBigUseCountWhenCan(model) != "" || StorageOutSellBus.ISBigUseCountWhenCan(model) != string.Empty)
                        {
                            string Info = StorageOutSellBus.ISBigUseCountWhenCan(model);
                            jc = new JsonClass(Info, "", 2);
                            context.Response.Write(jc);
                            return;
                        }
                    }
                    if (StorageOutSellBus.ConfirmBill(model,out retval))
                    {
                        jc = new JsonClass(retval, LoginUserName + "|" + Date, 1);
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
            else
            {
                if (ID > 0)
                {
                    #region 修改销售出库
                    string DetailProductID = context.Request.Params["DetailProductID"].ToString().Trim();//物品ID
                    string DetailSortNo = context.Request.Params["DetailSortNo"].ToString().Trim();//序号
                    string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();//
                    string DetailUnitPrice = context.Request.Params["DetailUnitPrice"].ToString().Trim();//单价
                    string DetailProductCount = context.Request.Params["DetailProductCount"].ToString().Trim();//数量
                    string DetailTotalPrice = context.Request.Params["DetailTotalPrice"].ToString().Trim();//总价
                    string DetailStorageID = context.Request.Params["DetailStorageID"].ToString().Trim();//
                    string DetailRemark = context.Request.Params["DetailRemark"].ToString().Trim();//备注
                    string DetailPackage = context.Request.Params["DetailPackage"].ToString().Trim();//包装要求

                    string DetailUnitID = "";//实际单位
                    string DetailBaseUnitID = "";//基本单位
                    string DetailBaseCount = "";//基本数量
                    string DetailBasePrice = "";//基本单价
                    string DetailExtRate = "";//比率

                    try
                    {
                        DetailUnitID = context.Request.Params["DetailUnitID"].ToString().Trim();
                    }catch(Exception){}//实际单位
                    try { DetailBaseUnitID = context.Request.Params["DetailBaseUnitID"].ToString().Trim(); }
                    catch(Exception){}//基本单位
                    try { DetailBaseCount = context.Request.Params["DetailBaseCount"].ToString().Trim(); }
                    catch(Exception){}//基本数量
                    try { DetailBasePrice = context.Request.Params["DetailBasePrice"].ToString().Trim(); }
                    catch(Exception){}//基本单价
                    try { DetailExtRate = context.Request.Params["DetailExtRate"].ToString().Trim(); }
                    catch(Exception){}//比率

                    string DetailBatchNo = context.Request.Params["DetailBatchNo"].ToString().Trim();//批次

                   

                    StorageOutSellModel model = new StorageOutSellModel();
                    model.CompanyCD = companyCD;
                    model.ID = ID.ToString();
                    model.OutNo = context.Request.Params["txtOutNo"].ToString().Trim();
                    model.Title = context.Request.Params["txtTitle"].ToString().Trim();
                    model.FromType = context.Request.Params["ddlFromType"].ToString().Trim();
                    if (context.Request.Params["txtFromBillID"].ToString().Trim() != "undefined")
                    {
                        model.FromBillID = context.Request.Params["txtFromBillID"].ToString().Trim();
                    }
                    if (context.Request.Params["txtSender"].ToString().Trim() != "undefined")
                    {
                        model.Sender = context.Request.Params["txtSender"].ToString().Trim();
                    }
                    model.TotalPrice = context.Request.Params["txtTotalPrice"].ToString().Trim();
                    model.CountTotal = context.Request.Params["txtTotalCount"].ToString().Trim();
                    model.Transactor = context.Request.Params["txtTransactor"].ToString().Trim();
                    model.OutDate = context.Request.Params["txtOutDate"].ToString().Trim();
                    model.Summary = context.Request.Params["txtSummary"].ToString().Trim();
                    if (context.Request.Params["txtOutDept"].ToString().Trim() != "undefined")
                    {
                        model.DeptID = context.Request.Params["txtOutDept"].ToString().Trim();
                    }
                    model.BillStatus = context.Request.Params["sltBillStatus"].ToString().Trim();
                    model.Remark = context.Request.Params["txtRemark"].ToString().Trim();
                    model.ModifiedUserID = LoginUserID;
                    //可查看人员
                    string CanViewUser = "," + context.Request.Params["CanViewUser"].ToString().Trim() + ",";
                    string CanViewUserName = context.Request.Params["CanViewUserName"].ToString().Trim();
                    model.CanViewUser = CanViewUser;
                    model.CanViewUserName = CanViewUserName;

                    //获取扩展属性
                    Hashtable ht = GetExtAttr(context);
                    
                    List<StorageOutSellDetailModel> modellist = new List<StorageOutSellDetailModel>();
                    string[] sortNo = DetailSortNo.Split(',');
                    string[] fromLineNo = DetailFromLineNo.Split(',');
                    string[] productID = DetailProductID.Split(',');
                    string[] productCount = DetailProductCount.Split(',');
                    string[] unitPrice = DetailUnitPrice.Split(',');
                    string[] totalPrice = DetailTotalPrice.Split(',');
                    string[] storageID = DetailStorageID.Split(',');
                    string[] remark = DetailRemark.Split(',');
                    string[] Package = DetailPackage.Split(',');

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
                            StorageOutSellDetailModel model1 = new StorageOutSellDetailModel();
                            model1.FromType = model.FromType;//这个跟基表中的FromType一样
                            model1.OutNo = model.OutNo;
                            model1.SortNo = sortNo[i];
                            model1.FromLineNo = fromLineNo[i];
                            model1.ProductID = productID[i];
                            model1.ProductCount = BaseCount[i];//基本数量
                            model1.UnitPrice = BasePrice[i];//基本单价
                            model1.TotalPrice = totalPrice[i];
                            model1.StorageID = storageID[i];
                            model1.Remark = remark[i];
                            model1.Package = Package[i];
                            model1.CompanyCD = companyCD;
                            model1.ModifiedUserID = LoginUserID;

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

                    if (StorageOutSellBus.UpdateStorageOutSell(model,ht, modellist))
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
                    #region 添加销售出库

                    string DetailProductID = context.Request.Params["DetailProductID"].ToString().Trim();//物品ID
                    string DetailSortNo = context.Request.Params["DetailSortNo"].ToString().Trim();//序号
                    string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();//
                    string DetailUnitPrice = context.Request.Params["DetailUnitPrice"].ToString().Trim();//单价
                    string DetailProductCount = context.Request.Params["DetailProductCount"].ToString().Trim();//数量
                    string DetailTotalPrice = context.Request.Params["DetailTotalPrice"].ToString().Trim();//总价
                    string DetailStorageID = context.Request.Params["DetailStorageID"].ToString().Trim();//
                    string DetailRemark = context.Request.Params["DetailRemark"].ToString().Trim();//备注
                    string DetailPackage = context.Request.Params["DetailPackage"].ToString().Trim();//包装要求

                    string DetailBatchNo = context.Request.Params["DetailBatchNo"].ToString().Trim();//批次


                    string DetailUnitID = "";//实际单位
                    string DetailBaseUnitID = "";//基本单位
                    string DetailBaseCount = "";//基本数量
                    string DetailBasePrice = "";//基本单价
                    string DetailExtRate = "";//比率

                    try
                    {
                        DetailUnitID = context.Request.Params["DetailUnitID"].ToString().Trim();
                    }
                    catch (Exception) { }//实际单位
                    try { DetailBaseUnitID = context.Request.Params["DetailBaseUnitID"].ToString().Trim(); }
                    catch (Exception) { }//基本单位
                    try { DetailBaseCount = context.Request.Params["DetailBaseCount"].ToString().Trim(); }
                    catch (Exception) { }//基本数量
                    try { DetailBasePrice = context.Request.Params["DetailBasePrice"].ToString().Trim(); }
                    catch (Exception) { }//基本单价
                    try { DetailExtRate = context.Request.Params["DetailExtRate"].ToString().Trim(); }
                    catch (Exception) { }//比率
                    
                    StorageOutSellModel model = new StorageOutSellModel();
                    model.CompanyCD = companyCD;
                    if (context.Request.Params["bmgz"].ToString().Trim() == "zd")
                    {
                        model.OutNo = ItemCodingRuleBus.GetCodeValue(context.Request.Params["txtOutNo"].ToString().Trim(), "StorageOutSell", "OutNo");
                        if (string.IsNullOrEmpty(model.OutNo))
                        {
                            jc = new JsonClass("该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!", "",6);
                            context.Response.Write(jc);
                            context.Response.End();
                        }
                    }
                    else
                    {
                        model.OutNo = context.Request.Params["txtOutNo"].ToString().Trim();
                    }
                    bool isExsist = PrimekeyVerifyBus.CheckCodeUniq("StorageOutSell", "OutNo", model.OutNo);
                    if (!isExsist)
                    {
                        jc = new JsonClass("该编号已被使用，请输入未使用的编号！", "", 2);
                    }
                    else
                    {
                        model.Title = context.Request.Params["txtTitle"].ToString().Trim();
                        model.FromType = context.Request.Params["ddlFromType"].ToString().Trim();
                        if (context.Request.Params["txtFromBillID"].ToString().Trim() != "undefined")
                        {
                            model.FromBillID = context.Request.Params["txtFromBillID"].ToString().Trim();
                        }
                        if (context.Request.Params["txtSender"].ToString().Trim() != "undefined")
                        {
                            model.Sender = context.Request.Params["txtSender"].ToString().Trim();
                        }
                        model.TotalPrice = context.Request.Params["txtTotalPrice"].ToString().Trim();
                        model.CountTotal = context.Request.Params["txtTotalCount"].ToString().Trim();
                        model.Transactor = context.Request.Params["txtTransactor"].ToString().Trim();
                        model.OutDate = context.Request.Params["txtOutDate"].ToString().Trim();
                        model.Creator = loginUser_id.ToString();
                        model.Summary = context.Request.Params["txtSummary"].ToString().Trim();
                        if (context.Request.Params["txtOutDept"].ToString().Trim() != "undefined")
                        {
                            model.DeptID = context.Request.Params["txtOutDept"].ToString().Trim();
                        }
                        model.BillStatus = context.Request.Params["sltBillStatus"].ToString().Trim();
                        model.Remark = context.Request.Params["txtRemark"].ToString().Trim();
                        model.ModifiedUserID = LoginUserID;

                        //可查看人员
                        string CanViewUser = "," + context.Request.Params["CanViewUser"].ToString().Trim() + ",";
                        string CanViewUserName = context.Request.Params["CanViewUserName"].ToString().Trim();
                        model.CanViewUser = CanViewUser;
                        model.CanViewUserName = CanViewUserName;

                        List<StorageOutSellDetailModel> modellist = new List<StorageOutSellDetailModel>();
                        string[] sortNo = DetailSortNo.Split(',');
                        string[] fromLineNo = DetailFromLineNo.Split(',');
                        string[] productID = DetailProductID.Split(',');
                        string[] productCount = DetailProductCount.Split(',');
                        string[] unitPrice = DetailUnitPrice.Split(',');
                        string[] totalPrice = DetailTotalPrice.Split(',');
                        string[] storageID = DetailStorageID.Split(',');
                        string[] remark = DetailRemark.Split(',');
                        string[] Package = DetailPackage.Split(',');

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
                                StorageOutSellDetailModel model2 = new StorageOutSellDetailModel();
                                model2.FromType = model.FromType;
                                model2.OutNo = model.OutNo;
                                model2.SortNo = sortNo[i];
                                model2.FromLineNo = fromLineNo[i];
                                model2.ProductID = productID[i];
                                model2.ProductCount = BaseCount[i];//基本数量
                                model2.UnitPrice = BasePrice[i];//基本单价
                                model2.TotalPrice = totalPrice[i];
                                model2.StorageID = storageID[i];
                                model2.Remark = remark[i];
                                model2.Package = Package[i];
                                model2.CompanyCD = companyCD;
                                model2.ModifiedUserID = LoginUserID;

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
                        
                        if (StorageOutSellBus.InsertStorageOutSell(model, modellist,ht,out IndexIDentity))
                        {
                            jc = new JsonClass("保存成功", IndexIDentity.ToString() + "|" + model.OutNo.ToString() + "|" + LoginUserName + "|" + Date, 1);
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
            string strKeyList = GetParam(_context,"keyList").Trim();
            string[] arrKey = strKeyList.Split('|');
            //取得扩展属性值
            for (int y = 0; y < arrKey.Length; y++)
            {
                //不为空的字段名才取值
                if (arrKey[y].Trim().Length != 0)
                {
                    ht.Add(arrKey[y].Trim(), GetParam(_context,arrKey[y].Trim()).Trim());//添加keyvalue键值对
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