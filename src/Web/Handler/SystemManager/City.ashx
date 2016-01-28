<%@ WebHandler Language="C#" Class="City" %>

using System;
using System.Web;
using XBase.Business.SystemManager;
using System.Data;
public class City : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {
        string ProCode = context.Request.QueryString["ProCode"].ToString();
        DataTable dt = CityBus.GetCityByCode(ProCode);
        string CityName = "请选择,0|";
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow Row in dt.Rows)
            {
                CityName += Row["CityName"].ToString();
                CityName+="," + Row["CityCD"] + "|"; 
            }
            CityName=CityName.Remove(CityName.Length - 1);
            context.Response.Write(CityName);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}