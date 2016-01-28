var pageCount = 10;//每页计数
var totalRecord = 0;
var pagerStyle = "flickr";//jPagerBar样式

var currentPageIndex = 1;
var action = "";//操作
var orderBy = "";//排序字段
var ifdel="0";//是否删除

$(document).ready(function()
{
    SearLink(1);
    
    requestobj = GetRequest(); 
    var days = requestobj['days'];   
    
    if(typeof(days)!="undefined")
    { 
       $("#seleDays").attr("value",days);
       currentPageIndex = requestobj['currentPageIndex'];
       pageCount = requestobj['pageCount'];
       
       SearLink(currentPageIndex);
    }
});

    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {             
         ifdel="0";
         
           currentPageIndex = pageIndex;
           var Days =document.getElementById("seleDays").value; //检索条件
          
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/LinkRemind.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&Days="+escape(Days),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg)
           {
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();                   
                    $.each(msg.data,function(i,item)
                    {
                      if(item.id != null && item.id != "") {                     
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'><a href='#' onclick=SelectLink('"+item.id+"')>" + item.linkmanname + "</a></td>"+
                        "<td height='22' align='center'><a href='#' onclick=SelectCust('"+item.custid+"','"+item.CustNo+"','"+item.CustBig+"','"+item.CanViewUser+"','"+item.Manager+"','"+item.CustCreator+"')>"+ item.CustName +"</a></td>"+
                        "<td height='22' align='center'>" + item.Appellation + "</td>"+
                        "<td height='22' align='center'>"+item.TypeName+"</td>"+
                        "<td height='22' align='center'>" + item.Important + "</td>"+                        
                         "<td height='22' align='center'>"+ item.WorkTel +"</td>"+
                        "<td height='22' align='center'>"+ item.Handset +"</td>"+
                        "<td height='22' align='center'>"+ item.QQ +"</td>"+                     
                        "<td height='22' align='center'>"+item.Birthday+"</td>").appendTo($("#pageDataList1 tbody"));}
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                  
                  document.getElementById("Text2").value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(ifdel=="0"){hidePopup();}$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }

//table行颜色
function pageDataList1(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

//检索
function SearLink(aa)
{
    ifdel = "0";    
    search="1";
    TurnToPage(aa);
}

function Ifshow(count)
{
    if(count=="0")
    {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pagecount").style.display = "none";
    }
    else
    {
        document.getElementById("divpage").style.display = "block";
        document.getElementById("pagecount").style.display = "block";
    }
}

function SelectLink(linkid)
{
    
    var days  = document.getElementById("seleDays").value;

    window.location.href='LinkMan_Edit.aspx?linkid='+linkid+'&Pages=LinkRemind.aspx&days='+days+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount
    +'&ModuleID=2021201'
}

function SelectCust(custid,custno,custbig,canuser,manager,creator)
{
     var j = 0;
    var UserId = document.getElementById("hiddUserId").value;

    if(UserId != manager && UserId != creator && canuser != ",,")
    {
        var str= new Array();
        str = canuser.split(",");
        for(i=0;i<str.length;i++)
        {
            if(str[i] == UserId)
            {
                j++;
            }        
        }
    }
    else
    {
        j++;
    }
    
    if(j == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","对不起，您没有浏览此客户信息的权限！");
        return;
    }
    
    var days  = document.getElementById("seleDays").value;;//检索条件
        
    window.location.href='Cust_Add.aspx?custid='+custid+'&Pages=LinkRemind.aspx&days='+days+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount
    +'&ModuleID=2021101';
}

    
//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount,newPageIndex)
{
if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        ifdel = "0";
        this.pageCount=parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
}
//排序
function OrderBy(orderColum,orderTip)
{
    if (totalRecord == 0) 
     {
        return;
     }
    ifdel = "0";
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
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
    TurnToPage(1);
}
