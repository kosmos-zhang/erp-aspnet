<%@ WebHandler Language="C#" Class="LoveAdd" %>

using System;
using System.Web;
using XBase.Business.Common;
using XBase.Common;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;

public class LoveAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{    
    public void ProcessRequest (HttpContext context) {
        
        string Action = context.Request.Params["action"].ToString().Trim(); //标示添加或修改
        JsonClass jc;
        string LoveNo = context.Request.Params["LoveNo"].ToString().Trim(); //客户关怀单编号
        string LoveNoType = context.Request.Params["LoveNoType"].ToString().Trim(); //客户关怀单编号类型
        
        if (Action == "1") //新建操作的时候判断唯一性
        {            
                string tableName = "CustLove";//关怀表
                string columnName = "LoveNo";//关怀编号
                string codeValue = LoveNo;

                if (LoveNoType != "")
                    LoveNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue(LoveNoType, tableName, columnName);
                else
                {
                     bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq(tableName, columnName, codeValue);
                    if (!ishave)
                    {
                        jc = new JsonClass("faile", "该编号已被使用，请输入未使用的编号！", 2);
                        context.Response.Write(jc);
                        context.Response.End();
                    } 
                }                          
        }  
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码        
        string CustID = context.Request.Params["CustID"].ToString().Trim();
        string CustLinkMan = context.Request.Params["CustLinkMan"].ToString().Trim();
        string Linker = context.Request.Params["Linker"].ToString().Trim();//执行人        
        string Title = context.Request.Params["Title"].ToString().Trim();
        string LoveDate = context.Request.Params["LoveDate"].ToString().Trim();
        string LoveType = context.Request.Params["LoveType"].ToString().Trim();
        //string ModifiedDate = context.Request.Params["ModifiedDate"].ToString().Trim();
        //string ModifiedUserID = context.Request.Params["ModifiedUserID"].ToString().Trim();//更新用户UserID
        string Contents = context.Request.Params["Contents"].ToString().Trim();//投诉内容
        string Feedback = context.Request.Params["Feedback"].ToString().Trim();
        string Remark = context.Request.Params["Remark"].ToString().Trim();//备注
        string ID = context.Request.Params["ID"].ToString().Trim();
        string CanViewUser = "," + context.Request.Params["CanViewUser"].ToString().Trim() + ",";
        string CanViewUserName = context.Request.Params["CanViewUserName"].ToString().Trim();

        CustLoveModel CustLoveM = new CustLoveModel();
        CustLoveM.LoveNo = LoveNo;
        CustLoveM.CompanyCD = CompanyCD;
        CustLoveM.CustID = Convert.ToInt32(CustID);
        CustLoveM.CustLinkMan = Convert.ToInt32(CustLinkMan);
        CustLoveM.Linker = Convert.ToInt32(Linker);
        CustLoveM.Title = Title;        
        if (LoveDate != "" || LoveDate != string.Empty)
        {
            CustLoveM.LoveDate = Convert.ToDateTime(LoveDate);
        }
        CustLoveM.LoveType = Convert.ToInt32(LoveType);
        //if (ModifiedDate != "" || ModifiedDate != string.Empty)
        //{
        CustLoveM.ModifiedDate = DateTime.Now;
        //}
        CustLoveM.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        CustLoveM.Contents = Contents;
        CustLoveM.Feedback = Feedback;
        CustLoveM.remarks = Remark;
        CustLoveM.CanViewUser = CanViewUser;
        CustLoveM.CanViewUserName = CanViewUserName;
        if (ID != "")
            CustLoveM.ID = Convert.ToInt32(ID);


        //判断为添加或修改操作
        if (Action == "1") //添加
        {
            int Loveid = LoveBus.CustLoveAdd(CustLoveM);

            if (Loveid > 0)
                jc = new JsonClass(CustLoveM.LoveNo, Loveid.ToString(), 1);
            else
                jc = new JsonClass("faile", "", 0);
            context.Response.Write(jc);
        }
        else//修改
        {
            if (LoveBus.UpdateLove(CustLoveM))
                jc = new JsonClass(CustLoveM.LoveNo, CustLoveM.ID.ToString(), 1);
            else
                jc = new JsonClass("faile", "", 0);
            context.Response.Write(jc);
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