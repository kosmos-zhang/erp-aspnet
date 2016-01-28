<%@ WebHandler Language="C#" Class="CheckBillQuote" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Collections.Generic;


public class CheckBillQuote :  SubBaseHandler
{

    public override void ActionHandler(string action)
    {
        Check();
    }
    
    /*验证单据是否被引用*/
    protected void Check()
    {
        /*表名*/
        string tableName = GetRequestForm("tableName", false);
        /*字段名*/
        string colName = GetRequestForm("colName", false);
        /*值*/
        string value = GetRequestForm("value", false);
        OutputResult(XBase.Business.Common.CheckQuoteBus.CheckBill(tableName, colName, value, UserInfo.CompanyCD), ""); 
    }
    
}