
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

function SearchPage()
{
    var Days =$("#Days").val();
    if(Days=="")
    {
        MsgBox("天数不能为空.");
        return;
    }
    if(!/^[0-9]{1,9}$/.test(Days))
    {
        MsgBox("天数必需为正整数.");
        return;
    }
    TurnToPage(1);   
}

//重新加载页面

function TurnToPage(pageIndex)
{
       currentPageIndex = pageIndex;
       
       var Days =$("#Days").val();
       
       action="lovebydays";
       
       Condition= "orderby="+orderBy+"&Days="+escape(Days);
      
       AjaxHandle(pageIndex,action,Condition,"CustLoveList.ashx");//ajax操作
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
    window.open("CustLoveByDaysPrint.aspx?" + Url);
}