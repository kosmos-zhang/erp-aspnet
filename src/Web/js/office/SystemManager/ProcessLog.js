$(document).ready(function(){
    
    });    
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
      var serch=document.getElementById("hidSearchCondition").value;
            $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SystemManager/ProcessLogList.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&" + serch,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                  
                if(item.UserID != null && item.UserID != "")
                {  var date=item.OperateDate;
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+item.UserID +"</td>"+
                        "<td height='22' align='center'>"+date+"</td>"+
                        "<td height='22' align='center'>"+item.ModuleID+"</td>"+
                        "<td height='22' align='center'>"+item.ObjectID+"</td>"+
                        "<td height='22' align='center'>"+item.ObjectName+"</td>"+
                        "<td height='22' align='center'>"+item.Element+"</td>"+
                        "<td height='22' align='center'>"+item.Remark+"</td>").appendTo($("#pageDataList1 tbody"));
                        }
                   });
                     //页码
                       ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                       "<%= Request.Url.AbsolutePath %>",//[url]
                        {style:pagerStyle,mark:"pageDataList1Mark",
                        totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                        onclick:"TurnToPage({pageindex});return false;"}//[attr]
                        );
                      totalRecord = msg.totalCount;
                     // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                      document.getElementById("Text2").value=msg.totalCount;
                      $("#ShowPageCount").val(pageCount);
                      ShowTotalPage(msg.totalCount,pageCount,pageIndex);
                      $("#ToPage").val(pageIndex);
                        ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                      //document.getElementById('tdResult').style.display='block';
                      },
               error: function() {}, 
               complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
               });
           
    }
    //table行颜色
function pageDataList1(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=1;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Fun_Search_LogInfo(aa)
{

     var OpenDate=document.getElementById("txtOpenDate").value;
      var CloseDate = document.getElementById("txtCloseDate").value;
      if(!compareDate(OpenDate,CloseDate))
      {
       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","开始时间不能大于结束时间！");
       return;
      }
       var RetVal=CheckSpecialWords();

    if(RetVal!="")
    {
             showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","单据编号不能含有特殊字符！");
       return;
    }
    
      var search = "";
           var UserID= document.getElementById("uids").value;
           var ModName=document.getElementById("uds").value;
           var OpenDate=document.getElementById("txtOpenDate").value;
           var CloseDate = document.getElementById("txtCloseDate").value;
           var BillNO = document.getElementById('ds').value; 
          search+="UserID="+escape(UserID);
          search+="&ModName="+escape(ModName); 
          search+="&OpenDate="+escape(OpenDate); 
          search+="&CloseDate="+escape(CloseDate); 
          search+="&BillNO="+escape(BillNO); 
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;
      TurnToPage(1);
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
            document.getElementById('divpage').style.display = "block";
            document.getElementById('pagecount').style.display = "block";
        }
    }
    
    
    function SelectDept(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
       if(!IsNumber(newPageCount))
       {
//          popMsgObj.ShowMsg('显示条数格式不对，必须是数字！');
          showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","显示条数格式不对，必须是数字！");
          return;
       }
       if(!IsNumber(newPageIndex))
       {
//          popMsgObj.ShowMsg('跳转页数格式不对，必须是数字！');
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","跳转页数格式不对，必须是数字！");
          return;
       }
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pageCount=parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
        }
    }
    //排序
    function OrderBy(orderColum,orderTip)
    {
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

