<%@ WebHandler Language="C#" Class="SalesSummaryStatistics" %>

using System;
using System.Web;
using System.Text;
using System.Data;

public class SalesSummaryStatistics :SubBaseHandler
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

        OutputDataTable(XBase.Business.Office.SellReport.SalesSummaryStatisticsBus.GetSalesSummary(startDate, endDate, sumType, pageIndex, pageSize, orderBy, ref totalCount), totalCount);
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
            case "byDept":
                rptName = "部门";
                rptKey = "DeptName";
                break;
            case "bySeller":
                rptName = "业务员";
                rptKey = "EmployeeName";
                break;
            case "byProduct":
                rptName = "物品";
                rptKey = "ProductName";
                break;
                 
        }
        
        DataTable dt = XBase.Business.Office.SellReport.SalesSummaryStatisticsBus.GetSalesSummary(startDate, endDate, sumType);
        
        StringBuilder sbXml = new StringBuilder();
        sbXml.AppendLine("<?xml version=\"1.0\"  encoding=\"Utf-8\"?>");
        if (type == "FCF_Line.swf")
            sbXml.AppendLine("<graph   rotateYAxisName='1'  xAxisName='" + rptName + "'  decimalPrecision='" + UserInfo.SelPoint + "'   showNames='1' showValues='1'  showAlternateHGridColor='1' baseFontSize='12' formatNumberScale='1' AlternateHGridColor='ff5904' divLineColor='ff5904' divLineAlpha='20' alternateHGridAlpha='5'>");
          //  sbXml.AppendLine("<graph  caption=\"Monthly Sales Summary\" subcaption=\"For the year 2004\" xAxisName=\"Month\" yAxisMinValue=\"15000\" yAxisName=\"Sell Cost\" decimalPrecision=\"0\" formatNumberScale=\"0\" numberPrefix=\"$\" showNames=\"1\" showValues=\"0\" showAlternateHGridColor=\"1\" AlternateHGridColor=\"ff5904\" divLineColor=\"ff5904\" divLineAlpha=\"20\" alternateHGridAlpha=\"5\">");
        else
            sbXml.AppendLine("<graph  xAxisName='" + rptName + "' baseFontSize='12'   decimalPrecision='" + UserInfo.SelPoint + "' formatNumberScale='1' showValues='1' rotateYAxisName='1' >");
        if (dt != null && dt.Rows.Count > 0)
        {
                foreach (DataRow row in dt.Rows)
                {
                            System.Threading.Thread.Sleep(1);
                            sbXml.AppendLine("<set name='" + row[rptKey] + "' value='" + Math.Round(Convert.ToDecimal(string.IsNullOrEmpty(row["SellPriceTotal"].ToString()) ? "0" : row["SellPriceTotal"].ToString()), Convert.ToInt32(UserInfo.SelPoint)).ToString() + "' color='" + CreateRandomColor() + "'  hoverText='" + row[rptKey] + "' />");
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
        _context.Response.ContentEncoding = Encoding.UTF8;
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