
var pageCount = 10;//每页计数
var totalRecord = 0;//总记录数

var currentPageIndex = 1;//当前页索引
var action = "";//操作
var orderBy = "";//排序字段
var Condition="";//条件变量
        
$(document).ready(function()
{
  // TurnToPage(1);
});

//重新加载页面

function TurnToPage(pageIndex)
{
       currentPageIndex = pageIndex;
       
        var ProductName=$("#ProductName2").val();
        var CustID = $("#hfCustID").val();
       // alert(CustID);
                
        var MinCount =$("#MinCount").val();
        var MaxCount =$("#MaxCount").val();
        var MinPrice =$("#MinPrice").val();
        var MaxPrice =$("#MaxPrice").val();
        
        if(MinCount!="")
        {
            if(!/(^[1-9]\d*(\.\d{1,2})?$)|(^[0]{1}(\.\d{1,2})?$)/.test(MinCount))
            {
                MsgBox("购买数量区间最小值格式不正确");
                return;
            }
        }
        
        if(MaxCount!="")
        {
            if(!/(^[1-9]\d*(\.\d{1,2})?$)|(^[0]{1}(\.\d{1,2})?$)/.test(MaxCount))
            {
                MsgBox("购买数量区间最大值格式不正确");
                return;
            }
        }
        
        if(MinPrice!="")
        {
            if(!/(^[1-9]\d*(\.\d{1,4})?$)|(^[0]{1}(\.\d{1,4})?$)/.test(MinPrice))
            {
                MsgBox("购买金额区间最小值格式不正确");
                return;
            }
        }
        
        if(MaxPrice!="")
        {
            if(!/(^[1-9]\d*(\.\d{1,4})?$)|(^[0]{1}(\.\d{1,4})?$)/.test(MaxPrice))
            {
                MsgBox("购买金额区间最大值格式不正确");
                return;
            }
        }
        
        var StartDate =$("#StartDate").val();
        var EndDate =$("#EndDate").val();   
       
        if(StartDate != ""&&EndDate!= "")
        {
            if( (Date.parse(EndDate.replace(/-/g,"/"))-Date.parse(StartDate.replace(/-/g,"/")))<0)
            {
                MsgBox("开始日期不能大于结束日期");
                return;
            }
        }
        
       action="loadcustorderbyproduct";
       
       Condition= "orderby="+orderBy+"&ProductName="+escape(ProductName)+"&MinCount="+escape(MinCount)+"&MaxCount="+escape(MaxCount)+"&MinPrice="+escape(MinPrice)+
                "&CustID="+escape(CustID)+"&MaxPrice="+escape(MaxPrice)+"&StartDate="+escape(StartDate)+"&EndDate="+escape(EndDate);
      
       AjaxHandle(pageIndex,action,Condition,"CustOrderList.ashx");//ajax操作
}
 
//返回成功数据加载
function DoData(data)
{ 
    $("#pageDataList1 tbody").find("tr.newrow").remove();
    //alert(data.ProdNo);
    //debugger;
    if(data.list[0]["ProdNo"] != null && data.list[0]["ProdNo"] != "" )
    {
        for(var i=0;i<data.list.length;i++)
        {        
             $("<tr class='newrow'>"+           
            "<td height='22' align='center'>" +data.list[i]["ProdNo"]+ "</td>"+
            "<td height='22' align='center'>" +data.list[i]["ProductName"]+ "</td>"+
            "<td height='22' align='center'>" +data.list[i]["ProductCount"]+ "</td>"+
            "<td height='22' align='center'>" +data.list[i]["TotalFee"]+ "</td>"+
            "</tr>"
            ).appendTo($("#pageDataList1 tbody"));
        } 
    }     
}

 
//打印 
function pageSetup()
{   
    var Url =Condition;
     if(Url == "")
     {
        popMsgObj.Show("打印|","请检索数据后再打印|");
        return;
     }
    window.open("CustOrderByProductPrint.aspx?" + Url);
}

function Fun_FillParent_Content(id,no,productname,price,unit,codeName,taxRate,sellTax,discount,specification)
{
      document.getElementById("ProductName1").value=productname;
      document.getElementById("ProductName2").value=id;
}

//排序
function OrderByExport(orderColum,orderTip)
{
     if(Condition == "")
     {
        popMsgObj.Show("排序|","请检索数据后再排序|");
        return;
     }
    ifdel = "0";
    var ordering = "a";
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderBy = orderColum+"_"+ordering;
    $("#hiddExpOrder").val(orderBy);
    TurnToPage(1);
}