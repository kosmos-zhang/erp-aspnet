<%@ WebHandler Language="C#" Class="PerformanceTemplate_Edit" %>
/**********************************************
 * 作    者： 王保军
 * 创建日期： 2009/04/21
 * 描    述： 考核类型设置
 * 修改日期： 2009/04/23
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using XBase.Common;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;

public class PerformanceTemplate_Edit : BaseHandler
{
    
   
    protected override void ActionHandler(string action)
    {
        /*
          var action="LoadFlowMyApply";
           var action="LoadFlowMyProcess";
           LoadFlowWaitProcess
         */
        switch (action.ToLower())
        {
           
          
            case "loaduserlistwithdepartment":
                LoadUserListWithDepartment();
                break;
                
            default:
                DefaultAction(action);
                break;
        }
    }

   

    private DataTable userlist = new DataTable();
    private DataTable depts = new DataTable();
    
    
    
    
    private void LoadUserListWithDepartment()
    {
        //userlist = bll.GetUserInfo(UserInfo.CompanyCD);
        userlist = PerformanceTemplateBus.GetPerformanceElemList();
        depts = PerformanceTemplateBus.GetPerformanceElemList();
        //SuperDeptID ,ID

        StringBuilder sb = new StringBuilder();
        DataRow[] rows = depts.Select("ParentElemNo IS NULL");

        sb.Append("[");
        foreach (DataRow row in rows)
        {
            LoadSubDept(row, sb);
        }
        sb.Append("]");

        Output("{result:true,data:" + sb.ToString() + "}");
    }

    private void LoadSubDept(DataRow p, StringBuilder sb)
    {
        //do self
        if (sb[sb.Length - 1] == '}')
            sb.Append(",");

        sb.Append("{");
        sb.Append("text:\"" + p["ElemName"].ToString() + "\"");
        sb.Append(",value:\"" + p["ID"].ToString() + "\"");
        sb.Append(",SubNodes:[");

        //do users of this dept
        DataRow[] users = userlist.Select("ParentElemNo='"+p["ID"].ToString()+"'");
        foreach (DataRow row in users)
        {
            if (sb[sb.Length - 1] == '}')
                sb.Append(",");

            sb.Append("{");
            sb.Append("isUser:true,");
            sb.Append("text:\"" + row["ElemName"].ToString() + "\"");
            sb.Append(",value:\"" + row["ID"].ToString() + "\"");
            sb.Append(",SubNodes:[]}");
        }

       // DataRow[] rows = depts.Select("ElemNo=" + p["ElemNo"].ToString());
      //  foreach (DataRow row in rows)
      //  {
       //     LoadSubDept(row, sb);
      //  }

        sb.Append("]}");
    }

 

}