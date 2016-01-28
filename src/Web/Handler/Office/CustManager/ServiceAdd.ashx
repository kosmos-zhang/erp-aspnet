<%@ WebHandler Language="C#" Class="ServiceAdd" %>

using System;
using System.Web;
using XBase.Business.Common;
using XBase.Common;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;

public class ServiceAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest (HttpContext context) {
        string Action = context.Request.Params["action"].ToString().Trim(); //标示添加或修改
        JsonClass jc;
        string ServeNo = context.Request.Params["ServeNo"].ToString().Trim(); //客户服务单编号
        string ServeNoType = context.Request.Params["ServeNoType"].ToString().Trim(); //客户服务单编号类型

        if (Action == "1") //新建操作的时候判断唯一性
        {
            //if (ServeNo != "")
            //{
                string tableName = "CustService";//客户服务表
                string columnName = "ServeNo";//服务单编号
                string codeValue = ServeNo;

                if (ServeNoType != "")
                    ServeNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue(ServeNoType, tableName, columnName);
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
            
                
            //}
        }
      
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码        
        string CustID = context.Request.Params["CustID"].ToString().Trim();
        string CustLinkMan = context.Request.Params["CustLinkMan"].ToString().Trim();
        string OurLinkMan = context.Request.Params["OurLinkMan"].ToString().Trim();
        string Title = context.Request.Params["Title"].ToString().Trim();
        string CustLinkTel = context.Request.Params["CustLinkTel"].ToString().Trim();
        string ServeType = context.Request.Params["ServeType"].ToString().Trim();
        string Fashion = context.Request.Params["Fashion"].ToString().Trim();
        string State = context.Request.Params["State"].ToString().Trim();
        string BeginDate = context.Request.Params["BeginDate"].ToString().Trim();
        string Executant = context.Request.Params["Executant"].ToString().Trim();
        //string SpendTime = context.Request.Params["SpendTime"].ToString().Trim();
        string SpendTime = context.Request.Params["SpendTime"].ToString().Trim() == "" ? "0" : context.Request.Params["SpendTime"].ToString().Trim();
        string DateUnit = context.Request.Params["DateUnit"].ToString().Trim();

        string Contents = context.Request.Params["Contents"].ToString().Trim();
        string Feedback = context.Request.Params["Feedback"].ToString().Trim();
        string LinkQA = context.Request.Params["LinkQA"].ToString().Trim();
        string Remark = context.Request.Params["Remark"].ToString().Trim();//备注
        string ID = context.Request.Params["ID"].ToString().Trim();
        string CanViewUser = "," + context.Request.Params["CanViewUser"].ToString().Trim() + ",";
        string CanViewUserName = context.Request.Params["CanViewUserName"].ToString().Trim();
        
        CustServiceModel CustServiceM = new CustServiceModel();

        CustServiceM.CompanyCD = CompanyCD;
        CustServiceM.CustID = Convert.ToInt32(CustID);
        CustServiceM.CustLinkMan = Convert.ToInt32(CustLinkMan);
        CustServiceM.OurLinkMan = Convert.ToInt32(OurLinkMan);
        CustServiceM.Title = Title;
        CustServiceM.ServeNO = ServeNo;
        CustServiceM.CustLinkTel = CustLinkTel;
        CustServiceM.ServeType = Convert.ToInt32(ServeType);
        CustServiceM.Fashion = Convert.ToInt32(Fashion);
        CustServiceM.State = State;
        if (BeginDate != "" || BeginDate != string.Empty)
        {
            CustServiceM.BeginDate = Convert.ToDateTime(BeginDate);
        }
        CustServiceM.Executant = Convert.ToInt32(Executant);
        CustServiceM.SpendTime = Convert.ToDecimal(SpendTime);
        CustServiceM.DateUnit = DateUnit;
        CustServiceM.Contents = Contents;
        CustServiceM.Feedback = Feedback;
        CustServiceM.LinkQA = LinkQA;
        CustServiceM.Remark = Remark;
        CustServiceM.ModifiedDate = DateTime.Now;
        CustServiceM.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        CustServiceM.CanViewUser = CanViewUser;
        CustServiceM.CanViewUserName = CanViewUserName;
        if (ID != "")
            CustServiceM.ID = Convert.ToInt32(ID);

        //判断为添加或修改操作
        if (Action == "1") //添加
        {
            int Serviceid = ServiceBus.CustServiceAdd(CustServiceM);

            if (Serviceid > 0)
                jc = new JsonClass(CustServiceM.ServeNO, Serviceid.ToString(), 1);
            else
                jc = new JsonClass("faile", "", 0);
            context.Response.Write(jc);
        }
        else//修改
        {
            if (ServiceBus.UpdateService(CustServiceM))
                jc = new JsonClass(CustServiceM.ServeNO, CustServiceM.ID.ToString(), 1);
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