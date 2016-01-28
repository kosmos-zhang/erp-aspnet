
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
       
      // var CustName =FormatStr($("#CustName").val());//客户管理分类
       var ProductName=document.getElementById("ProductName2").value;
       var StartDate =$("#StartDate").val();//客户营销分类
       var EndDate =$("#EndDate").val();//客户时间分类   
       
        if(StartDate != ""&&EndDate!= "")
        {
            if( (Date.parse(EndDate.replace(/-/g,"/"))-Date.parse(StartDate.replace(/-/g,"/")))<0)
            {
                MsgBox("开始日期不能大于结束日期");
                return;
            }
        }
        
       action="loadcustorderbydate";
       
       Condition= "orderby="+orderBy+"&ProductName="+escape(ProductName)+"&StartDate="+escape(StartDate)+"&EndDate="+escape(EndDate);
      
       AjaxHandle(pageIndex,action,Condition,"CustOrderList.ashx");//ajax操作
}

//返回成功数据加载
function DoData(data)
{  
    $("#pageDataList1 tbody").find("tr.newrow").remove();
    for(var i=0;i<data.list.length;i++)
    {
         $("<tr class='newrow'>"+
        "<td height='22' align='center'>" +data.list[i]["CustNO"] + "</td>"+
        "<td height='22' align='center'>" +data.list[i]["CustName"]+ "</td>"+
        "<td height='22' align='center'>" +data.list[i]["ProdNO"]+ "</td>"+
        "<td height='22' align='center'>" +data.list[i]["ProductName"]+ "</td>"+
        "<td height='22' align='center'>" +data.list[i]["OrderDate"]+ "</td>"+
        "<td height='22' align='center'>" +data.list[i]["ProductCount"]+ "</td>"+
        "<td height='22' align='center'>" +data.list[i]["TotalPrice"]+ "</td>"+
        "</tr>"
        ).appendTo($("#pageDataList1 tbody"));
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
    window.open("CustOrderByDatePrint.aspx?" + Url);
}

function Fun_FillParent_Content(id,no,productname,price,unit,codeName,taxRate,sellTax,discount,specification)
{
      document.getElementById("ProductName1").value=productname;
      document.getElementById("ProductName2").value=id;
}