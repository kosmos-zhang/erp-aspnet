<%@ WebHandler Language="C#" Class="StorageInRedAdd" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using System.Web.SessionState;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;

public class StorageInRedAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
        string action = context.Request.Params["Act"];
        if ("Del".Equals(action.Trim()))
        {
            string strID = context.Request.Params["strID"].ToString();
            string[] IDArray = null;
            IDArray = strID.Split(',');


            for (int i = 0; i < IDArray.Length; i++)
            {
                if (!StorageCommonBus.IsDelete("officedba.StorageInRed", IDArray[i].ToString(), companyCD))
                {
                    ifdele = false;
                    break;
                }
            }

            if (ifdele == true)
            {
                for (int i = 0; i < IDArray.Length; i++)
                {

                    if (StorageInRedBus.DeleteStorageInRed(IDArray[i].ToString(), companyCD))
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

            if (context.Request.Params["Act"].ToString().Trim() == "Confirm")
            {
                if (ID > 0)
                {
                    StorageInRedModel model = new StorageInRedModel();
                    model.ID = ID.ToString();
                    model.CompanyCD = companyCD;
                    model.Confirmor = loginUser_id.ToString();
                    model.ModifiedUserID = LoginUserID;
                    if (StorageInRedBus.ISBigUseCountWhenCant(model) != "" || StorageInRedBus.ISBigUseCountWhenCant(model) != string.Empty)
                    {
                        string[] Info = StorageInRedBus.ISBigUseCountWhenCant(model).Split('|');
                        jc = new JsonClass("行" + Info[0] + "超过可用库存；可用库存分别为" + Info[1], "", 0);
                        context.Response.Write(jc);
                        return;
                    }
                    else if (context.Request.Params["isqueren"].ToString().Trim() != "Yes")//还没有经过警告提示，那么就去验证是否有需要警告提示的
                    {
                        if (StorageInRedBus.ISBigUseCountWhenCan(model) != "" || StorageInRedBus.ISBigUseCountWhenCan(model) != string.Empty)
                        {
                            string Info = StorageInRedBus.ISBigUseCountWhenCan(model);
                            jc = new JsonClass(Info, "", 2);
                            context.Response.Write(jc);
                            return;
                        }
                    }

                    if (StorageInRedBus.ConfirmBill(model))
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
            else
            {
                if (ID > 0)
                {
                    //UpdateStorageInRed(StorageInRedModel model, List<StorageInRedDetailModel> modelList, List<StorageInRedDetailModel> ReModelList)
                    #region 修改红冲入库
                    //获取页面初始时明细中的入库数量，源单行号，源单编号
                    string ReNumDetail = context.Request.Params["ReNumDetail"].Trim();
                    //string ReInNo = context.Request.Params["ReInNo"].Trim();
                    string ReLineNo = context.Request.Params["ReLineNo"].Trim();
                    string ReProductID = context.Request.Params["ReProductID"].Trim();
                    string ReStorageID = context.Request.Params["ReStorageID"].Trim();

                    string DetailProductID = context.Request.Params["DetailProductID"].ToString().Trim();//物品ID
                    string DetailSortNo = context.Request.Params["DetailSortNo"].ToString().Trim();//序号
                    //string DetailUnitID = context.Request.Params["DetailUnitID"].ToString().Trim();//单位
                    string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();//
                    //string DetailFromType = context.Request.Params["DetailFromType"].ToString().Trim();//
                    string DetailUnitPrice = context.Request.Params["DetailUnitPrice"].ToString().Trim();//单价
                    string DetailProductCount = context.Request.Params["DetailProductCount"].ToString().Trim();//数量
                    string DetailTotalPrice = context.Request.Params["DetailTotalPrice"].ToString().Trim();//总价
                    string DetailStorageID = context.Request.Params["DetailStorageID"].ToString().Trim();//
                    string DetailRemark = context.Request.Params["DetailRemark"].ToString().Trim();//备注
                    //string BratchNo = context.Request.Params["BratchNo"].ToString().Trim();//pici
                    string DetailUnitZ = context.Request.Params["DetailUnitZ"].ToString().Trim();//单位
                    string DetailProductCountZ = context.Request.Params["DetailProductCountZ"].ToString().Trim();//数量
                    string DetailUsedPrice = context.Request.Params["DetailUsedPrice"].ToString().Trim();//实际单价
                    string DetailExRate = context.Request.Params["DetailExRate"].ToString().Trim();//换算率
                    //string DetailBatchNo = context.Request.Params["DetailBatchNo"].ToString().Trim();//明细批次是否启用
                    string IsMoreUnit = context.Request.Params["IsMoreUnit"].ToString().Trim();//单位组是否启用
                    string BatchNoArr = context.Request.Params["BatchNoArr"].ToString().Trim();//明细批次

                    StorageInRedModel model = new StorageInRedModel();
                    model.CompanyCD = companyCD;
                    model.ID = ID.ToString();
                    model.InNo = context.Request.Params["txtInNo"].ToString().Trim();
                    model.Title = context.Request.Params["txtTitle"].ToString().Trim();
                    model.FromType = context.Request.Params["ddlFromType"].ToString().Trim();
                    model.FromBillID = context.Request.Params["txtFromBillID"].ToString().Trim();
                    model.ReasonType = context.Request.Params["ddlReasonType"];
                    model.TotalPrice = context.Request.Params["txtTotalPrice"].ToString().Trim();
                    model.CountTotal = context.Request.Params["txtTotalCount"].ToString().Trim();
                    model.Executor = context.Request.Params["txtExecutor"].ToString().Trim();
                    model.EnterDate = context.Request.Params["txtEnterDate"].ToString().Trim();
                    model.Summary = context.Request.Params["txtSummary"].ToString().Trim();
                    if (context.Request.Params["txtDept"].ToString().Trim() != "undefined")
                    {
                        model.DeptID = context.Request.Params["txtDept"].ToString().Trim();
                    }
                    model.BillStatus = context.Request.Params["sltBillStatus"].ToString().Trim();
                    model.Remark = context.Request.Params["txtRemark"].ToString().Trim();
                    model.CanViewUserName = context.Request.Params["CanUserName"].ToString().Trim();//可查看人员Name
                    model.CanViewUser = context.Request.Params["CanUserID"].ToString().Trim();//可查看人员ID
                    model.ModifiedUserID = LoginUserID;
                    //获取扩展属性
                    Hashtable ht = GetExtAttr(context);

                    List<StorageInRedDetailModel> modellist = new List<StorageInRedDetailModel>();
                    string[] sortNo = DetailSortNo.Split(',');
                    string[] fromLineNo = DetailFromLineNo.Split(',');
                    string[] productID = DetailProductID.Split(',');
                    string[] productCount = DetailProductCount.Split(',');
                    //string[] unitID = DetailUnitID.Split(',');
                    string[] unitPrice = DetailUnitPrice.Split(',');
                    string[] totalPrice = DetailTotalPrice.Split(',');
                    string[] storageID = DetailStorageID.Split(',');
                    string[] remark = DetailRemark.Split(',');
                    //string[] fromType = DetailFromType.Split(',');
                    //string[] fromBillID = DetailFromBillID.Split(',');
                    string[] UnitZ = DetailUnitZ.Split(',');
                    string[] ProductCountZ = DetailProductCountZ.Split(',');
                    string[] UsedPrice = DetailUsedPrice.Split(',');
                    string[] ExRate = DetailExRate.Split(',');
                    //string[] IsBatchNo = DetailBatchNo.Split(',');//明细批次是否启用
                    //string NewBratchNo = "";
                    string[] BatchNo = BatchNoArr.Split(',');//明细批次
                    
                    if (productID.Length >= 1)
                    {
                        //int BatchShow = 0;
                        for (int i = 0; i < productID.Length; i++)
                        {
                            StorageInRedDetailModel model1 = new StorageInRedDetailModel();
                            model1.FromType = model.FromType;//这个跟基表中的FromType一样
                            model1.InNo = model.InNo;
                            model1.SortNo = sortNo[i];
                            model1.FromLineNo = fromLineNo[i];
                            model1.ProductID = productID[i];
                            model1.ProductCount = productCount[i];
                            //model1.UnitID = unitID[i];
                            model1.UnitPrice = unitPrice[i];
                            model1.TotalPrice = totalPrice[i];
                            model1.StorageID = storageID[i];
                            model1.Remark = remark[i];
                            model1.ModifiedUserID = LoginUserID;
                            model1.CompanyCD = companyCD;
                            model1.BatchNo = BatchNo[i];
                            if (IsMoreUnit == "true")
                            {
                                model1.UsedUnitID = UnitZ[i];
                                model1.UsedUnitCount = ProductCountZ[i];
                                model1.UsedPrice = UsedPrice[i];
                                model1.ExRate = ExRate[i];
                            }                       
                           
                            modellist.Add(model1);
                        }
                    }

                    List<StorageInRedDetailModel> ReModelList = new List<StorageInRedDetailModel>();
                    string[] reNumDetail = ReNumDetail.Split(',');
                    string[] reLineNo = ReLineNo.Split(',');
                    string[] reProductID = ReProductID.Split(',');
                    string[] reStorageID = ReStorageID.Split(',');
                    if (reNumDetail.Length > 0)
                    {
                        for (int i = 0; i < reNumDetail.Length; i++)
                        {
                            StorageInRedDetailModel modelIRD = new StorageInRedDetailModel();
                            modelIRD.ProductCount = reNumDetail[i];
                            modelIRD.ProductID = reProductID[i];
                            modelIRD.StorageID = reStorageID[i];
                            ReModelList.Add(modelIRD);
                        }
                    }

                    if (StorageInRedBus.UpdateStorageInRed(model, modellist, ht))
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
                    #region 添加其他入库
                    string DetailProductID = context.Request.Params["DetailProductID"].ToString().Trim();//物品ID
                    string DetailSortNo = context.Request.Params["DetailSortNo"].ToString().Trim();//序号
                    //string DetailUnitID = context.Request.Params["DetailUnitID"].ToString().Trim();//单位
                    string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();//
                    //string DetailFromBillID = context.Request.Params["DetailFromBillID"].ToString().Trim();//
                    //string DetailFromType = context.Request.Params["DetailFromType"].ToString().Trim();//
                    string DetailUnitPrice = context.Request.Params["DetailUnitPrice"].ToString().Trim();//单价
                    string DetailProductCount = context.Request.Params["DetailProductCount"].ToString().Trim();//数量
                    string DetailTotalPrice = context.Request.Params["DetailTotalPrice"].ToString().Trim();//总价
                    string DetailStorageID = context.Request.Params["DetailStorageID"].ToString().Trim();//
                    string DetailRemark = context.Request.Params["DetailRemark"].ToString().Trim();//备注
                    //string BratchNo = context.Request.Params["BratchNo"].ToString().Trim();//pici
                    string DetailUnitZ = context.Request.Params["DetailUnitZ"].ToString().Trim();//单位
                    string DetailProductCountZ = context.Request.Params["DetailProductCountZ"].ToString().Trim();//数量
                    string DetailUsedPrice = context.Request.Params["DetailUsedPrice"].ToString().Trim();//实际单价
                    string DetailExRate = context.Request.Params["DetailExRate"].ToString().Trim();//换算率
                    //string DetailBatchNo = context.Request.Params["DetailBatchNo"].ToString().Trim();//明细批次是否启用
                    string IsMoreUnit = context.Request.Params["IsMoreUnit"].ToString().Trim();//单位组是否启用
                    string BatchNoArr = context.Request.Params["BatchNoArr"].ToString().Trim();//明细批次
                    
                    StorageInRedModel model = new StorageInRedModel();
                    model.CompanyCD = companyCD;
                    if (context.Request.Params["bmgz"].ToString().Trim() == "zd")
                    {
                        model.InNo = ItemCodingRuleBus.GetCodeValue(context.Request.Params["txtInNo"].ToString().Trim(), "StorageInRed", "InNo");
                        if (model.InNo == "")
                        {
                            jc = new JsonClass("该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置！", "", 2);
                            context.Response.Write(jc);
                            context.Response.End();
                        }
                    }
                    else
                    {
                        model.InNo = context.Request.Params["txtInNo"].ToString().Trim();
                    }

                    //if (context.Request.Params["pcgz"].ToString().Trim() == "zd")
                    //{
                    //    BratchNo = XBase.Business.Office.SystemManager.BatchNoRuleSetBus.GetCodeValue(BratchNo);
                    //}
                    
                    bool isExsist = PrimekeyVerifyBus.CheckCodeUniq("StorageInRed", "InNo", model.InNo);
                    if (!isExsist)
                    {
                        jc = new JsonClass("该编号已被使用，请输入未使用的编号！", "", 2);
                    }
                    else
                    {
                        //if (BratchNo != "null")
                        //{
                            //bool isBratchNo = PrimekeyVerifyBus.CheckCodeUniq("StorageInRedDetail", "BatchNo", BratchNo, "InNo !='" + model.InNo + "'");//不同入库单批次不可以重复
                            //if (!isBratchNo)
                            //{
                            //    jc = new JsonClass("该批次已被使用，请输入未使用的批次！", "", 2);
                            //}
                        //}                        
                        //else
                        //{
                            model.Title = context.Request.Params["txtTitle"].ToString().Trim();
                            model.FromType = context.Request.Params["ddlFromType"].ToString().Trim();
                            model.FromBillID = context.Request.Params["txtFromBillID"].ToString().Trim();
                            model.TotalPrice = context.Request.Params["txtTotalPrice"].ToString().Trim();
                            model.CountTotal = context.Request.Params["txtTotalCount"].ToString().Trim();
                            model.Executor = context.Request.Params["txtExecutor"].ToString().Trim();
                            model.ReasonType = context.Request.Params["ddlReasonType"];
                            model.EnterDate = context.Request.Params["txtEnterDate"].ToString().Trim();
                            model.Creator = loginUser_id.ToString();
                            //model.CreateDate = context.Request.Params["txtEnterDate"].ToString().Trim();
                            model.Summary = context.Request.Params["txtSummary"].ToString().Trim();
                            if (context.Request.Params["txtDept"].ToString().Trim() != "undefined")
                            {
                                model.DeptID = context.Request.Params["txtDept"].ToString().Trim();
                            }
                            model.BillStatus = context.Request.Params["sltBillStatus"].ToString().Trim();
                            model.Remark = context.Request.Params["txtRemark"].ToString().Trim();
                            model.CanViewUserName = context.Request.Params["CanUserName"].ToString().Trim();//可查看人员Name
                            model.CanViewUser = context.Request.Params["CanUserID"].ToString().Trim();//可查看人员ID
                            model.ModifiedDate = DateTime.Now.ToShortDateString();
                            model.ModifiedUserID = LoginUserID;
                            //获取扩展属性
                            Hashtable ht = GetExtAttr(context);

                            List<StorageInRedDetailModel> modellist = new List<StorageInRedDetailModel>();
                            string[] sortNo = DetailSortNo.Split(',');
                            string[] fromLineNo = DetailFromLineNo.Split(',');
                            string[] productID = DetailProductID.Split(',');
                            string[] productCount = DetailProductCount.Split(',');
                            //string[] unitID = DetailUnitID.Split(',');
                            string[] unitPrice = DetailUnitPrice.Split(',');
                            string[] totalPrice = DetailTotalPrice.Split(',');
                            string[] storageID = DetailStorageID.Split(',');
                            string[] remark = DetailRemark.Split(',');
                            //string[] fromType = DetailFromType.Split(',');
                            //string[] fromBillID = DetailFromBillID.Split(',');
                            string[] UnitZ = DetailUnitZ.Split(',');
                            string[] ProductCountZ = DetailProductCountZ.Split(',');
                            string[] UsedPrice = DetailUsedPrice.Split(',');
                            string[] ExRate = DetailExRate.Split(',');
                            //string[] IsBatchNo = DetailBatchNo.Split(',');//明细批次是否启用
                            string[] BatchNo = BatchNoArr.Split(',');//明细批次是否启用
                            //string NewBratchNo = "";
                            
                            if (productID.Length >= 1)
                            {
                                //int BatchShow = 0;
                                for (int i = 0; i < productID.Length; i++)
                                {
                                    StorageInRedDetailModel model2 = new StorageInRedDetailModel();
                                    model2.FromType = model.FromType;
                                    model2.InNo = model.InNo;
                                    model2.SortNo = sortNo[i];
                                    model2.FromLineNo = fromLineNo[i];
                                    model2.ProductID = productID[i];
                                    model2.ProductCount = productCount[i];
                                    //model2.UnitID = unitID[i];
                                    model2.UnitPrice = unitPrice[i]; ;
                                    model2.TotalPrice = totalPrice[i]; ;
                                    model2.StorageID = storageID[i];
                                    model2.Remark = remark[i];
                                    model2.CompanyCD = companyCD;
                                    model2.BatchNo = BatchNo[i];
                                    //model2.FromType = fromType[i];
                                    //model2.FromBillID = fromBillID[i];
                                    model2.ModifiedUserID = LoginUserID;
                                    if (IsMoreUnit == "true")
                                    {
                                        model2.UsedUnitID = UnitZ[i];
                                        model2.UsedUnitCount = ProductCountZ[i];
                                        model2.UsedPrice = UsedPrice[i];
                                        model2.ExRate = ExRate[i];
                                    }
                                    
                                    //if (IsBatchNo[i] == "1")
                                    //{
                                        
                                    //    NewBratchNo = BratchNo;
                                    //    BatchShow = 1;
                                    //}
                                    //else
                                    //{
                                    //    if (BatchShow == 0)
                                    //    {
                                    //        NewBratchNo = "NULL";
                                    //    }
                                    //}
                                    modellist.Add(model2);
                                }
                            }
                            int IndexIDentity = 0;

                            if (StorageInRedBus.InsertStorageInRed(model, modellist, ht, out IndexIDentity))
                            {
                                jc = new JsonClass("保存成功", IndexIDentity.ToString() + "|" + model.InNo.ToString() + "|" + LoginUserName + "|" + Date , 1);
                            }
                            else
                            {
                                jc = new JsonClass("保存失败", "", 0);
                            }
                       // }
                       
                    }//model.InNo = ItemCodingRuleBus.GetCodeValue(context.Request.Params["txtInNo"].ToString().Trim());

                }
            }


                    #endregion
        }
        context.Response.Write(jc);
    }

    #region 获取扩展属性值
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
    #endregion

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}