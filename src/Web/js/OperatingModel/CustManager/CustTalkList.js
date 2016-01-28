
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
       
       var CustName =FormatStr($("#CustName").val());
       var Priority =document.getElementById("Priority").value;
       var Status =document.getElementById("Status").value;
       var TalkType =document.getElementById("TalkType").value;
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
        
       action="loaddata";
       
       Condition= "orderby="+orderBy+"&CustName="+escape(CustName)+"&Priority="+escape(Priority)+"&Status="+escape(Status)+"&TalkType="+escape(TalkType)+"&StartDate="+escape(StartDate)+"&EndDate="+escape(EndDate);
      
       AjaxHandle(pageIndex,action,Condition,"CustTalkList.ashx");//ajax操作
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
        "<td height='22' align='center'>" +data.list[i]["EmployeeName"]+ "</td>"+
        "<td height='22' align='center'>" +data.list[i]["TalkNO"]+ "</td>"+
        "<td height='22' align='center'>" +data.list[i]["Title"]+ "</td>"+
        "<td height='22' align='center'>" +data.list[i]["TalkType"]+ "</td>"+
        "<td height='22' align='center'>" +data.list[i]["Priority"]+ "</td>"+
        "<td height='22' align='center'>" +data.list[i]["Contents"]+ "</td>"+
        "<td height='22' align='center'>" +data.list[i]["Status"]+ "</td>"+
        "<td height='22' align='center'>" +data.list[i]["linker"]+ "</td>"+
        "<td height='22' align='center'>" +data.list[i]["CompleteDate"]+ "</td>"+
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
    window.open("CustTalkListPrint.aspx?" + Url);
}