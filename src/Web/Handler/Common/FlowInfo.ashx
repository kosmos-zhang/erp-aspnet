<%@ WebHandler Language="C#" Class="FlowInfo" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Model.Office.ProductionManager;
using XBase.Business.Office.ProductionManager;
using XBase.Business.Common;
using System.Data;


public class FlowInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //string companyCD = "AAAAAA";
        if (context.Request.RequestType == "POST")
        {
            int BillTypeFlag = int.Parse(context.Request.QueryString["BillTypeFlag"].ToString().Trim());
            int BillTypeCode = int.Parse(context.Request.QueryString["BillTypeCode"].ToString().Trim());
            int BillID = int.Parse(context.Request.QueryString["BillID"].ToString().Trim());
            int StepCount = 0;//总的审批步骤
            
            DataTable dtRecord = new DataTable();
            DataTable dtStep = new DataTable();
            DataTable dtNew = new DataTable();
            
            dtRecord = FlowBus.GetOperateRecordList(companyCD, BillTypeFlag, BillTypeCode, BillID);
            dtStep = FlowBus.GetFlowStep(companyCD, BillTypeFlag, BillTypeCode, BillID);
            if (dtStep.Rows.Count>0)
            {
                StepCount = dtStep.Rows.Count;
            }
            for (int i = 0; i < dtRecord.Rows.Count; i++)
            {
                //克隆表结构
                if (i == 0)
                {
                    dtNew = dtRecord.Clone();
                }


                if (i == 0)
                {

                    DataTable dtNext = FlowBus.GetNextOperateList(companyCD, BillTypeFlag, BillTypeCode, BillID);
                    //提交审批的
                    if (string.IsNullOrEmpty(dtRecord.Rows[i]["StepNo"].ToString()) && string.IsNullOrEmpty(dtRecord.Rows[i]["State"].ToString()))
                    {
                        if (dtNext.Rows.Count > 0)
                        {

                            //新表导入行
                            DataRow drNext = dtNew.NewRow();
                            drNext["StepNo"] = 0;
                            drNext["State"] = -1;
                            drNext["Operator"] = dtNext.Rows[0]["EmployeeName"].ToString();
                            dtNew.Rows.Add(drNext);
                        }
                    }
                    else
                    {

                        if (int.Parse(dtRecord.Rows[i]["StepNo"].ToString()) > 0)
                        {
                            if (StepCount == int.Parse(dtRecord.Rows[i]["StepNo"].ToString()))
                            {

                            }
                            else
                            {
                                if (int.Parse(dtRecord.Rows[i]["State"].ToString()) < 2)
                                {
                                    if (dtNext.Rows.Count > 0)
                                    {

                                        //新表导入行
                                        DataRow drNextIn = dtNew.NewRow();
                                        drNextIn["StepNo"] = 0;
                                        drNextIn["State"] = -1;
                                        drNextIn["Operator"] = dtNext.Rows[0]["EmployeeName"].ToString();
                                        dtNew.Rows.Add(drNextIn);
                                    }
                                }
                            }
                        }
                    }
                    
                }
                //导入行
                DataRow dr = dtNew.NewRow();
                if (!string.IsNullOrEmpty(dtRecord.Rows[i]["StepNo"].ToString()))
                {
                    dr["StepNo"] = int.Parse(dtRecord.Rows[i]["StepNo"].ToString());
                }
                if (!string.IsNullOrEmpty(dtRecord.Rows[i]["State"].ToString()))
                {
                    dr["State"] = int.Parse(dtRecord.Rows[i]["State"].ToString());
                }

                dr["ApplyUserId"] = dtRecord.Rows[i]["ApplyUserId"].ToString();
                dr["operateDate"] = dtRecord.Rows[i]["operateDate"].ToString();
                dr["Note"] = dtRecord.Rows[i]["Note"].ToString();
                dr["Operator"] = dtRecord.Rows[i]["Operator"].ToString();
                dtNew.Rows.Add(dr);

                ////待提交审批的需要列出下一步待审批的
                //if (string.IsNullOrEmpty(dtRecord.Rows[i]["State"].ToString()))
                //{
                //    //当前要审批的步骤
                //    //判断是否已经审批过
                //    DataTable dtNext = FlowBus.GetNextOperateList(companyCD, BillTypeFlag, BillTypeCode, BillID);
                //    if (dtNext.Rows.Count > 0)
                //    {
                        
                //        //新表导入行
                //        DataRow drNext = dtNew.NewRow();
                //        drNext["StepNo"] = "";
                //        drNext["State"] = "审批";
                //        drNext["ApplyUserID"] = "";
                //        drNext["operateDate"] = "";
                //        drNext["Note"] = "";
                //        drNext["Operator"] = dtNext.Rows[0]["EmployeeName"].ToString();
                //        dtNew.Rows.Add(drNext);
                //    }
                //}
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("dataRecord:");
            sb.Append(JsonClass.DataTable2Json(dtNew));
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}