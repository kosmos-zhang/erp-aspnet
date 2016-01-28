<%@ WebHandler Language="C#" Class="StorageAdjustAdd" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using System.Web.SessionState;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;
public class StorageAdjustAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
      
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
        JsonClass jc = new JsonClass();
        if (context.Request.QueryString["Action"] != null)
        {
            if (context.Request.QueryString["Action"].ToString() == "Del")
            {
                string strID = context.Request.QueryString["strID"].ToString();
                if (StorageAdjustBus.DelAdjust(strID, companyCD))
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
        string action = context.Request.QueryString["myAction"];

        #region 添加质检报告入库
        int ID = int.Parse(context.Request.Params["ID"].ToString());
        if (action == "Confirm" || action == "UnConfirm")  //确认
        {
            if (ID > 0)
            {
                StorageAdjustModel model = new StorageAdjustModel();
                
                string DetailSortNo = context.Request.QueryString["DetailSortNo"].ToString().Trim();//
                string DetailProID = context.Request.QueryString["DetailProID"].ToString().Trim();//..
                string DetailAdjustType = context.Request.QueryString["DetailAdjustType"].ToString().Trim();//
                string DetailAdjustCount = context.Request.QueryString["DetailAdjustCount"].ToString().Trim();// 
                string DetailBatchNo = context.Request.QueryString["DetailBatchNo"].ToString().Trim();//批次
                string DetailUnitPrice = context.Request.QueryString["DetailUnitPrice"].ToString().Trim();//单价
                //string DetailAdjustType = context.Request.QueryString["DetailAdjustType"].ToString().Trim();//类型

                string InfoNo = context.Request.QueryString["InfoNo"].ToString().Trim();//编号
                
                
                string[] myProID = DetailProID.Split(',');
                string[] myAdjustType = DetailAdjustType.Split(',');
                string[] myAdjustCount = DetailAdjustCount.Split(',');
                string[] mySortID = DetailSortNo.Split(',');
                string[] BatchNo = DetailBatchNo.Split(',');
                string[] UnitPrice = DetailUnitPrice.Split(',');
                string[] AdjustType = DetailAdjustType.Split(',');
                model.ID = ID;
                model.StorageID = int.Parse(context.Request.QueryString["StorageID"].ToString());
                model.CompanyCD = companyCD;
                model.Confirmor = loginUser_id;
                model.ConfirmDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                model.ModifiedUserID = LoginUserID;
                model.ModifiedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                model.AdjustNo = InfoNo;
                
                List<StorageAdjustDetail> detailllist = new List<StorageAdjustDetail>();
                if (mySortID.Length >= 1)
                {
                    for (int i = 0; i < mySortID.Length; i++)
                    {
                        if (mySortID[i] != "" && mySortID[i] != null)
                        {
                            StorageAdjustDetail detail = new StorageAdjustDetail();

                            if (myProID[i] != null && myProID[i] != "")
                            {
                                detail.ProductID = int.Parse(myProID[i].ToString());
                            }
                            if (myAdjustType[i] != null && myAdjustType[i] != "")
                            {
                                detail.AdjustType = myAdjustType[i].ToString();
                            }
                            if (myAdjustCount[i] != null && myAdjustCount[i] != "")
                            {
                                detail.AdjustCount = Convert.ToDecimal(myAdjustCount[i].ToString());
                            }
                            detail.BatchNo = BatchNo[i].ToString();
                            detail.CostPrice =Convert.ToDecimal(UnitPrice[i].ToString());
                            detail.AdjustType = AdjustType[i].ToString();
                            detailllist.Add(detail);
                        }
                        
                    }
                }
                if (action == "Confirm")
                {
                    if (StorageAdjustBus.ConfirmBill(model, detailllist))
                    {
                        jc = new JsonClass("确认成功", EmployeeName + "|" + model.ConfirmDate.ToString("yyyy-MM-dd") + "|" + LoginUserID, 1);
                    }
                    else
                    {
                        jc = new JsonClass("确认失败", "", 0);
                    }
                }
                if (action=="UnConfirm")
                {
                    if (StorageAdjustBus.UnConfirmBill(model, detailllist))
                    {
                        jc = new JsonClass("取消确认成功", LoginUserID + "|" + model.ConfirmDate.ToString("yyyy-MM-dd"), 1);
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
                StorageAdjustModel model = new StorageAdjustModel();
                string theMethod=context.Request.QueryString["myMethod"].ToString() ;
                model.ID = ID;
                model.CompanyCD = companyCD;
                model.Closer = loginUser_id;
                model.CloseDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                model.ModifiedUserID = LoginUserID;
                model.ModifiedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                if (theMethod== "0")
                {
                    if (StorageAdjustBus.CloseBill(model,theMethod))
                    {
                        jc = new JsonClass("结单成功", EmployeeName + "|" + model.CloseDate.ToString("yyyy-MM-dd")+"|"+LoginUserID, 1);
                    }
                    else
                    {
                        jc = new JsonClass("结订单失败", "", 0);
                    }
                }
                else
                {
                    if (StorageAdjustBus.CloseBill(model, theMethod))
                    {
                        jc = new JsonClass("取消结单成功", EmployeeName + "|" + model.CloseDate.ToString("yyyy-MM-dd") + "|" + LoginUserID, 1);
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
            string DetailSortNo = context.Request.QueryString["DetailSortNo"].ToString().Trim();//          
           
            string DetailUnitID = context.Request.QueryString["DetailUnitID"].ToString().Trim();//  
            string DetailProID = context.Request.QueryString["DetailProID"].ToString().Trim();//..
            string DetailAdjustType = context.Request.QueryString["DetailAdjustType"].ToString().Trim();//
            string DetailAdjustCount = context.Request.QueryString["DetailAdjustCount"].ToString().Trim();// 
            string DetailCostPrice = context.Request.QueryString["DetailCostPrice"].ToString().Trim();//    ..
            string DetailCostPriceTotal = context.Request.QueryString["DetailCostPriceTotal"].ToString().Trim();//
            string DetailRemark = context.Request.QueryString["DetailRemark"].ToString().Trim();//         
            string ddd = context.Request.QueryString["AdjustNo"].ToString().Trim();

           // string DetailUnitID = "";//实际单位
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
            
            StorageAdjustModel model = new StorageAdjustModel();

            if (context.Request.QueryString["bmgz"].ToString().Trim() == "zd")
            {
                model.AdjustNo = ItemCodingRuleBus.GetCodeValue(context.Request.QueryString["AdjustNo"].ToString().Trim(), "StorageAdjust", "AdjustNo");  //单据编号
                if (string.IsNullOrEmpty(model.AdjustNo))
                {
                    jc = new JsonClass("该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!", "", 6);
                    context.Response.Write(jc);
                    context.Response.End();
                }
            }
            else
            {
                model.AdjustNo = context.Request.QueryString["AdjustNo"].ToString().Trim();
            }
            if (action == "add")
            {
                bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("StorageAdjust", "AdjustNo", model.AdjustNo);
                if (!isAlready)
                {
                    jc = new JsonClass("该编号已被使用，请输入未使用的编号！", "", 0);
                    context.Response.Write(jc);
                    return;

                }
            }
            model.CompanyCD = companyCD;
            model.BillStatus = context.Request.QueryString["myBillStatus"].ToString();
            if (model.BillStatus=="2")
            {
                model.BillStatus = "3";
            }
            model.CreateDate =Convert.ToDateTime(DateTime.Now.ToShortDateString());
            model.Creator = loginUser_id;
            model.Executor = int.Parse(context.Request.QueryString["Executor"].ToString());
            model.ModifiedDate =DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            model.ModifiedUserID = LoginUserID;          
            model.Remark = context.Request.QueryString["Remark"].ToString();
            model.Attachment = context.Request.QueryString["Attachment"].ToString();            
            model.Title = context.Request.QueryString["Title"].ToString();
            model.DeptID = int.Parse(context.Request.QueryString["DeptID"].ToString());
            model.StorageID = int.Parse(context.Request.QueryString["StorageID"].ToString());
            model.ReasonType = int.Parse(context.Request.QueryString["ReasonID"].ToString());
            model.Remark = context.Request.QueryString["Remark"].ToString();
            model.Attachment = context.Request.QueryString["Attachment"].ToString();
            model.AdjustDate = Convert.ToDateTime("9999-9-9");
            if (!string.IsNullOrEmpty(context.Request.QueryString["AdjustDate"]))
            {
                model.AdjustDate = Convert.ToDateTime(context.Request.QueryString["AdjustDate"].ToString());
            }
            model.Summary = context.Request.QueryString["Summary"].ToString();
            model.TotalPrice = 0;
            model.CountTotal = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["TotalPrice"].ToString()))
            {
                model.TotalPrice = Convert.ToDecimal(context.Request.QueryString["TotalPrice"].ToString());
            }
            if (!string.IsNullOrEmpty(context.Request.QueryString["CountTotal"]))
            {
                model.CountTotal = Convert.ToDecimal(context.Request.QueryString["CountTotal"].ToString());
            }
            //获取扩展属性
            Hashtable ht = GetExtAttr(context);
            List<StorageAdjustDetail> detailllist = new List<StorageAdjustDetail>();
            string[] mySortID = DetailSortNo.Split(',');
          
           
            string[] myUnitID = DetailUnitID.Split(',');
            string[] myProID = DetailProID.Split(',');
            string[] myAdjustType = DetailAdjustType.Split(',');
            string[] myAdjustCount = DetailAdjustCount.Split(',');
            string[] myConstPrice = DetailCostPrice.Split(',');
            string[] myCostPriceTotal = DetailCostPriceTotal.Split(',');
            string[] myRemark = DetailRemark.Split(',');


            //多计量单位、批次（2010.04.13）
            //string[] UnitID = DetailUnitID.Split(',');
            string[] BaseUnitID = DetailBaseUnitID.Split(',');
            string[] BaseCount = DetailBaseCount.Split(',');
            string[] BasePrice = DetailBasePrice.Split(',');
            string[] ExtRate = DetailExtRate.Split(',');
            string[] BatchNo = DetailBatchNo.Split(',');
            
            if (mySortID.Length >= 1)
            {
                for (int i = 0; i < mySortID.Length; i++)
                {
                    if (mySortID[i] != "" && mySortID[i] != null)
                    {
                        StorageAdjustDetail detail = new StorageAdjustDetail();

                        if (myProID[i] != null && myProID[i] != "")
                        {
                            detail.ProductID = int.Parse(myProID[i].ToString());
                        }
                        if (myAdjustType[i] != null && myAdjustType[i] != "")
                        {
                            detail.AdjustType = myAdjustType[i].ToString();
                        }
                        if (BaseCount[i] != null && BaseCount[i] != "")
                        {
                            detail.AdjustCount = Convert.ToDecimal(BaseCount[i].ToString());//基本数量
                        }
                        detail.SortNo = int.Parse(mySortID[i]);

                        if (BaseUnitID[i] != null && BaseUnitID[i] != "")
                        {
                            detail.UnitID = int.Parse(BaseUnitID[i].ToString());//基本单位
                        }

                        if (BasePrice[i] != null && BasePrice[i] != "")
                        {
                            detail.CostPrice = Convert.ToDecimal(BasePrice[i].ToString());//基本单价
                        }
                        if (myCostPriceTotal[i] != null && myCostPriceTotal[i] != "")
                        {
                            detail.CostPriceTotal = Convert.ToDecimal(myCostPriceTotal[i].ToString());
                        }

                        if (myRemark[i] != null && myRemark[i] != "")
                        {
                            detail.Remark =myRemark[i].ToString();
                        }
                        try
                        {
                            if (myUnitID[i] != null && myUnitID[i] != "")
                            {
                                try
                                {
                                    detail.UsedUnitID = myUnitID[i];
                                }
                                catch (Exception) { }//实际单位
                            }
                        }catch(Exception){}
                        try
                        {
                            if (myConstPrice[i] != null && myConstPrice[i] != "")
                            {
                                try
                                {
                                    detail.UsedPrice = myConstPrice[i];
                                }
                                catch (Exception) { }//实际单价
                            }
                        }catch(Exception){}
                        try
                        {
                            if (myAdjustCount[i] != null && myAdjustCount[i] != "")
                            {
                                try
                                {
                                    detail.UsedUnitCount = myAdjustCount[i];
                                }
                                catch (Exception) { }//实际数量
                            }
                        }catch(Exception){}
                        try
                        {
                            if (ExtRate[i] != null && ExtRate[i] != "")
                            {
                                try
                                {
                                    detail.ExRate = ExtRate[i];
                                }
                                catch (Exception) { }//比率
                            }
                        }catch(Exception){}
                        detail.BatchNo = BatchNo[i];//批次
                        
                        detailllist.Add(detail);
                    }
                }
            }
            if (action=="edit")
            {
                model.ID = ID;
                if (StorageAdjustBus.UpdateAdjust(model, detailllist, mySortID,ht))
                {
                    jc = new JsonClass("保存成功", model.ID.ToString() + "|" + model.AdjustNo.ToString() + "|" + model.ModifiedDate.ToString("yyyy-MM-dd") + "|" + model.ModifiedUserID, 1);
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
                if (StorageAdjustBus.AddAdjust(model, detailllist,ht) == true)
                {
                    jc = new JsonClass("保存成功", model.ID.ToString() + "| " + model.AdjustNo.ToString() + "|" + model.ModifiedDate.ToString("yyyy-MM-dd") + "|" + model.ModifiedUserID, 1);
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