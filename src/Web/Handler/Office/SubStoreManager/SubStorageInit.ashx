<%@ WebHandler Language="C#" Class="SubStorageInit" %>

using System;
using System.Web;
using XBase.Model.Office.SubStoreManager;
using XBase.Business.Office.SubStoreManager;
using XBase.Common;
using System.Web.SessionState;
using System.Web.UI;
using XBase.Business.Common;
using System.Collections;

public class SubStorageInit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
        string action = context.Request.Params["action"].ToString().Trim();
        int ID = int.Parse(context.Request.Params["ID"].ToString());
        // 获得扩展属性
        Hashtable htExtAttr;
        JsonClass jc;

        if (action == "Add")
        {
            SubStorageInModel model = new SubStorageInModel();
            model.CompanyCD = CompanyCD;

            //基本信息
            string CodeType = context.Request.Params["CodeType"].ToString().Trim();

            if (context.Request.Params["inNo"].Trim().Length == 0)
            {
                model.InNo = ItemCodingRuleBus.GetCodeValue(CodeType, "SubStorageIn", "InNo");
            }
            else
            {
                model.InNo = context.Request.Params["inNo"].Trim();
            }

            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("SubStorageIn", "InNo", model.InNo);
            //存在的场合
            if (!isAlready || string.IsNullOrEmpty(model.InNo))
            {
                if (string.IsNullOrEmpty(model.InNo))
                {//该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!
                    jc = new JsonClass("该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!", "", 0);
                    context.Response.Write(jc);
                }
                else
                {//单据编号已经存在
                    jc = new JsonClass("该编号已被使用，请输入未使用的编号!", "", 0);
                    context.Response.Write(jc);
                }
                return;
            }
            model.Title = context.Request.Params["title"].ToString().Trim(); //主题
            if (context.Request.Params["deptID"] != null && context.Request.Params["deptID"] != "")
            {
                model.DeptID = Convert.ToInt32(context.Request.Params["deptID"].ToString().Trim());
            }

            //备注信息
            if (context.Request.Params["creator"] != null && context.Request.Params["creator"] != "")
            {
                model.Creator = Convert.ToInt32(context.Request.Params["creator"].ToString().Trim());
            }
            if (context.Request.Params["createDate"].Trim() != null && context.Request.Params["createDate"].Trim() != "")
            {
                model.CreateDate = Convert.ToDateTime(context.Request.Params["createDate"].ToString().Trim());
            }
            if (context.Request.Params["billStatus"] != null && context.Request.Params["billStatus"] != "")
            {
                model.BillStatus = context.Request.Params["billStatus"].ToString().Trim();
            }
            if (context.Request.Params["confirmor"] != null && context.Request.Params["confirmor"] != "")
            {
                model.Confirmor = Convert.ToInt32(context.Request.Params["confirmor"].ToString().Trim());
            }
            if (context.Request.Params["confirmDate"].Trim() != null && context.Request.Params["confirmDate"].Trim() != "")
            {
                model.ConfirmDate = Convert.ToDateTime(context.Request.Params["confirmDate"].ToString().Trim());
            }
            if (context.Request.Params["modifiedUserID"] != null && context.Request.Params["modifiedUserID"] != "")
            {
                model.ModifiedUserID = context.Request.Params["modifiedUserID"].ToString().Trim();
            }
            model.Remark = context.Request.Params["remark"].ToString().Trim();

            if (ID > 0)
            {
                model.ID = ID;
            }

            string BatchNo = context.Request.Params["BatchNo"].Trim();
            if (context.Request.Params["pcgz"].Trim() == "zd")
            {
                BatchNo = XBase.Business.Office.SystemManager.BatchNoRuleSetBus.GetCodeValue(BatchNo);
            }


            string DetailProductID = context.Request.Params["DetailProductID"].ToString().Trim();
            string DetailSendCount = context.Request.Params["DetailSendCount"].ToString().Trim();
            string DetailUsedUnitID = context.Request.Params["DetailUsedUnitID"].ToString().Trim();
            string DetailUsedUnitCount = context.Request.Params["DetailUsedUnitCount"].ToString().Trim();
            string DetailUsedPrice = "";
            string DetailExRate = context.Request.Params["DetailExRate"].ToString().Trim();
            string DetailIsBatchNo = context.Request.Params["DetailIsBatchNo"].ToString().Trim();
            string length = context.Request.Params["length"].ToString().Trim();
            string DetailBatchNo = "";
            foreach (string item in DetailIsBatchNo.Split(','))
            {
                if (item == "1")
                {
                    DetailBatchNo += BatchNo + ",";
                }
                else
                {
                    DetailBatchNo += ",";
                }
            }
            DetailBatchNo = DetailBatchNo.Remove(DetailBatchNo.Length - 1);

            // 获得扩展属性
            htExtAttr = GetExtAttr(context);

            string tempID = "0";
            if (SubStorageBus.InsertSubStorageIn(model, DetailProductID, DetailSendCount, DetailUsedUnitID, DetailUsedUnitCount, DetailUsedPrice, DetailExRate, DetailBatchNo, length, out tempID, htExtAttr))
            {
                jc = new JsonClass("保存成功", model.InNo + "|" + BatchNo, int.Parse(tempID));
            }
            else
                jc = new JsonClass("保存失败", "", 0);
            context.Response.Write(jc);
        }
        else if (action == "Update")
        {
            string no = context.Request.Params["cno"].ToString().Trim();
            string CodeType = context.Request.Params["CodeType"].ToString().Trim();
            SubStorageInModel model = new SubStorageInModel();
            model.CompanyCD = CompanyCD;

            model.InNo = context.Request.Params["cno"].ToString().Trim();//合同编号 

            model.Title = context.Request.Params["title"].ToString().Trim(); //主题
            if (context.Request.Params["deptID"] != null && context.Request.Params["deptID"] != "")
            {
                model.DeptID = Convert.ToInt32(context.Request.Params["deptID"].ToString().Trim());
            }

            //备注信息
            if (context.Request.Params["creator"] != null && context.Request.Params["creator"] != "")
            {
                model.Creator = Convert.ToInt32(context.Request.Params["creator"].ToString().Trim());
            }
            if (context.Request.Params["createDate"].Trim() != null && context.Request.Params["createDate"].Trim() != "")
            {
                model.CreateDate = Convert.ToDateTime(context.Request.Params["createDate"].ToString().Trim());
            }
            if (context.Request.Params["billStatus"] != null && context.Request.Params["billStatus"] != "")
            {
                model.BillStatus = context.Request.Params["billStatus"].ToString().Trim();
            }
            if (context.Request.Params["confirmor"] != null && context.Request.Params["confirmor"] != "")
            {
                model.Confirmor = Convert.ToInt32(context.Request.Params["confirmor"].ToString().Trim());
            }
            if (context.Request.Params["confirmDate"].Trim() != null && context.Request.Params["confirmDate"].Trim() != "")
            {
                model.ConfirmDate = Convert.ToDateTime(context.Request.Params["confirmDate"].ToString().Trim());
            }
            if (context.Request.Params["modifiedUserID"] != null && context.Request.Params["modifiedUserID"] != "")
            {
                model.ModifiedUserID = context.Request.Params["modifiedUserID"].ToString().Trim();
            }
            model.Remark = context.Request.Params["remark"].ToString().Trim();

            if (ID > 0)
            {
                model.ID = ID;
            }

            string DetailProductID = context.Request.Params["DetailProductID"].ToString().Trim();
            string DetailSendCount = context.Request.Params["DetailSendCount"].ToString().Trim();
            string DetailUsedUnitID = context.Request.Params["DetailUsedUnitID"].ToString().Trim();
            string DetailUsedUnitCount = context.Request.Params["DetailUsedUnitCount"].ToString().Trim();
            string DetailUsedPrice = "";
            string DetailExRate = context.Request.Params["DetailExRate"].ToString().Trim();
            string length = context.Request.Params["length"].ToString().Trim();
            string BatchNo = context.Request.Params["BatchNo"].Trim();
            string DetailIsBatchNo = context.Request.Params["DetailIsBatchNo"].ToString().Trim();
            string DetailBatchNo = "";
            foreach (string item in DetailIsBatchNo.Split(','))
            {
                if (item == "1")
                {
                    DetailBatchNo += BatchNo + ",";
                }
                else
                {
                    DetailBatchNo += ",";
                }
            }

            DetailBatchNo = DetailBatchNo.Remove(DetailBatchNo.Length - 1);
            // 获得扩展属性
            htExtAttr = GetExtAttr(context);

            if (SubStorageBus.UpdateSubStorageIn(model, DetailProductID, DetailSendCount, DetailUsedUnitID, DetailUsedUnitCount, DetailUsedPrice, DetailExRate, DetailBatchNo, length, no, htExtAttr))
                jc = new JsonClass("保存成功", model.InNo, model.ID);
            else
                jc = new JsonClass("保存失败", "", 0);
            context.Response.Write(jc);
        }
        else if (action == "Confirm")//暂时未做
        {
            SubStorageInModel Model = new SubStorageInModel();
            Model.CompanyCD = CompanyCD;
            Model.ID = ID;
            Model.InNo = context.Request.Params["cno"].ToString().Trim();//合同编号 
            Model.DeptID = Convert.ToInt32(context.Request.Params["deptID"].ToString().Trim());
            Model.Confirmor = Convert.ToInt32(context.Request.Params["confirmor"].ToString().Trim());
            Model.ConfirmDate = DateTime.Parse(System.DateTime.Today.ToShortDateString());
            Model.Remark = context.Request.UrlReferrer.ToString();

            string DetailProductID = context.Request.Params["DetailProductID"].ToString().Trim();
            string DetailSendCount = context.Request.Params["DetailSendCount"].ToString().Trim();
            string DetailUnitPrice = context.Request.Params["DetailUnitPrice"].ToString().Trim();
            string DetailBatchNo = context.Request.Params["DetailBatchNo"].ToString().Trim();
            string length = context.Request.Params["length"].ToString().Trim();

            if (SubStorageBus.ConfirmSubStorageIn(Model, DetailProductID, DetailSendCount, DetailUnitPrice, DetailBatchNo, length))
            {
                jc = new JsonClass("success", "", 1);
                context.Response.Write(jc);
            }
            else
            {
                jc = new JsonClass("faile", "", 0);
                context.Response.Write(jc);
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