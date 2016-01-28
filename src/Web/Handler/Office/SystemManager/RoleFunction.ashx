<%@ WebHandler Language="C#" Class="RoleFunction" %>

using System;
using System.Web;

using System.Data;
using System.Text;

using System.Collections;

public class RoleFunction : BaseHandler
{
    protected override void ActionHandler(string action)
    {      
        switch (action.ToLower())
        {           
            case "getrolefunctions":
                GetRoleFunctions();
                break;
            case "setrolefunctions":
                SetRoleFunctions();
                break;                         
            default:
                DefaultAction(action);
                break;
        }
        
    }

    private void SetRoleFunctions()
    {
        string roleID = GetParam("roleID");
        if (roleID == string.Empty)
        {
            OutputResult(false, "角色ID未指定");
            return;
        }
        string functions = GetParam("functions");
        string[] functionList = functions.Split(',');

        int l = functionList.Length;
        
        string flist1 = "";
        string flist2 = "";
        string flist3 = "";
        string flist4 = "";
        
        for (int i = 0; i < l; i++)
        {
            if (i % 4 == 0)
            {
                if (flist1 != "")
                { 
                    flist1+= ",";
                }
                flist1 += functionList[i];
            }
            if (i % 4 == 1)
            {
                if (flist2 != "")
                {
                    flist2 += ",";
                }
                flist2 += functionList[i];
            }
            if (i % 4 == 2)
            {
                if (flist3 != "")
                {
                    flist3 += ",";
                }
                flist3 += functionList[i];
            }
            if (i % 4 == 3)
            {
                if (flist4 != "")
                {
                    flist4 += ",";
                }
                flist4 += functionList[i];
            }
                       
            
        }

       

        XBase.Business.Office.SystemManager.RoleFunctionBus.SetFunctionBatch(UserInfo.CompanyCD, int.Parse(roleID), flist1, flist2, flist3, flist4);

        OutputResult(true, "保存成功");
        return;

        //foreach(string function in functionList)
        //{
        //    if (function.IndexOf("_") == -1)
        //    { 
        //        //INSERT INTO [officedba].[RoleFunction] ([CompanyCD] ,[RoleID] ,[ModuleID] ,[FunctionID]) 
        //        //VALUES(UserInfo.CompanyCD,roleID,function,NULL)
        //        continue;
        //    }

        //    string mid = function.Split("_")[0];
        //    string funct = function.Split("_")[1];
        //    //funcs = select * from pubdba.ModuleFunction where moduleid=mid and functionType=funct,insert into RoleFunction
            
        //}
        
    }

    private void GetRoleFunctions()
    {
        string roleID = GetParam("roleID");
        if (roleID == string.Empty)
        {
            OutputResult(false, "角色ID未指定");
            return;
        }

        DataTable roleFunctions = XBase.Business.Office.SystemManager.RoleFunctionBus.GetRoleFunInfo2(UserInfo.CompanyCD, roleID);

        //ModuleID,FunctionID,FunctionType,FunctionName

        StringBuilder sb = new StringBuilder();
        sb.Append("{result:true,data:");
        sb.Append("[");
        
        ArrayList moduleIDList = new ArrayList();
        foreach (DataRow row in roleFunctions.Rows)
        {           
            string moduleV = row["ModuleID"].ToString();

            if (moduleIDList.Contains(moduleV))
            {
                continue;
            }
            
            bool hasViewRights = roleFunctions.Select("FunctionType='1' AND ModuleID="+moduleV).Length > 0;
            bool hasOperRights = roleFunctions.Select("FunctionType='2' AND ModuleID="+moduleV).Length > 0;
           

            if( !sb.ToString().EndsWith("[") )
            {
                sb.Append(",");
            }

            sb.Append(moduleV);

            if (hasOperRights)
            {
                sb.Append(",\""+moduleV+"_2\"");
            }
            if (hasViewRights)
            {
                sb.Append(",\"" + moduleV + "_1\"");
            }


            moduleIDList.Add(moduleV);
            
        }

        sb.Append("]}");

        Output(sb.ToString());
        
    }
   

}