<%@ WebHandler Language="C#" Class="SalesAnalysis" %>

using System;
using System.Web;
using System.Data;
using System.Text;

public class SalesAnalysis : SubBaseHandler
{

    public override void ActionHandler(string action)
    {
        switch (action)
        {
            case "get":
                GetData();
                break;
            case "getimage":
                GetDataImage();
                break;
        }
    }

    protected void GetData()
    {
        string sumType = GetRequest("sumType");
        string startDate = GetRequest("startDate");
        string endDate = GetRequest("endDate");

        int pageIndex = Convert.ToInt32(GetRequest("pageIndex"));
        int pageSize = Convert.ToInt32(GetRequest("pageSize"));
        string orderBy = GetRequest("orderBy");
        int totalCount = 0;

        OutputDataTable(XBase.Business.Office.SellReport.SalesSummaryStatisticsBus.GetSalesAnalysis(startDate, endDate, sumType, pageIndex, pageSize, orderBy, ref totalCount), totalCount);
    }



    protected void GetDataImage()
    {
        string sumType = GetRequest("sumType");
        string startDate = GetRequest("startDate");
        string endDate = GetRequest("endDate");
        string type = GetRequest("type");
        string rptName = string.Empty;
        string rptKey = string.Empty;
        switch (sumType)
        {
            case "byWeek":
                rptName = "周数";
                rptKey = "CreateWeek";
                break;
            case "byMonth":
                rptName = "月份";
                rptKey = "CreateMonth";
                break;
            case "byYear":
                rptName = "年份";
                rptKey = "CreateYear";
                break;

        }

        DataTable dt = XBase.Business.Office.SellReport.SalesSummaryStatisticsBus.GetSalesAnalysis(startDate, endDate, sumType);

        StringBuilder sbXml = new StringBuilder();
        sbXml.AppendLine("<?xml version=\"1.0\"?>");
        if (type == "FCF_Line.swf")
        {
            sbXml.AppendLine("<graph   yAxisName=''   xAxisName='" + rptName + "' decimalPrecision='" + UserInfo.SelPoint + "'  baseFontSize='12' formatNumberScale='1'  showNames='1' showValues='1'  showAlternateHGridColor='1' AlternateHGridColor='ff5904' divLineColor='ff5904' divLineAlpha='20' alternateHGridAlpha='5'>");
        }
        else
            sbXml.AppendLine("<graph    yAxisName='' xAxisName='" + rptName + "'  baseFontSize='12' formatNumberScale='1'  decimalPrecision='" + UserInfo.SelPoint + "' formatNumberScale='5' showNames='1' showValues='1' >");
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                string keyName = string.Empty;
                switch (sumType)
                {
                    case "byWeek":
                        keyName ="第"+row[rptKey].ToString()+"周 ";
                        break;
                    case "byMonth":
                        keyName = row[rptKey].ToString() + "月";
                        break;
                    case "byYear":
                        keyName = row[rptKey].ToString() + "年";
                        break;

                }
                
                
                System.Threading.Thread.Sleep(1);
                sbXml.AppendLine("<set name='" + keyName + "' value='" + Math.Round(Convert.ToDecimal(string.IsNullOrEmpty(row["SellPriceTotal"].ToString()) ? "0" : row["SellPriceTotal"].ToString()), Convert.ToInt32(UserInfo.SelPoint)).ToString() + "' color='" + CreateRandomColor() + "'  hoverText='" + keyName + "' />");
            }
        }
        else
        {
            _context.Response.ContentType = "text/xml";
            _context.Response.Write("");
            _context.Response.End();
            return;
        }
        sbXml.AppendLine("</graph>");

        _context.Response.ContentType = "text/xml";
        _context.Response.Write(sbXml.ToString());
        _context.Response.End();
    }

    //随机生成颜色值
    public static string CreateRandomColor()
    {
        string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F";
        string[] allCharArray = allChar.Split(',');
        string randomCode = "";
        int temp = -1;
        Random rand = new Random();
        for (int i = 0; i < 6; i++)
        {
            if (temp != -1)
            {
                rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
            }
            int t = rand.Next(15);
            if (temp == t)
            {
                return CreateRandomColor();
            }
            temp = t;
            randomCode += allCharArray[t];
        }
        return randomCode;
    }
    
}