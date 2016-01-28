$(document).ready(function(){
    TurnToPage(1);
    });    
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var currentPageIndex = 1;
    var action = "action";//操作
    var orderBy = "";//排序字段

    function TurnToPage(pageIndex)
    {
         var search ="&begintime="+document.getElementById("hidbeginTime").value+"&endtime="
        +document.getElementById("hidendTime").value+"&productid="+document.getElementById("HidProductID").value
        +"&storageid="+document.getElementById("HidStorageID").value+"&ByTimeType="+document.getElementById('HiddenField1').value;
        
            $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/OperatingModel/StorageManager/StorageInCompareDetails.ashx',//目标地址

           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&" + search,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                     if($("#IsDisplayPrice").val()=="false")$("#td_price").hide();
                     var td="";
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                  
                if(item.id != null && item.id != "")
                {  //var date=item.OperateDate;
                    if($("#IsDisplayPrice").val()=="true")td="<td height='22' align='center'>"+item.TotalPrice+"</td>";
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+item.InNo
                         +"</td>"+
                        "<td height='22' align='center'>"+item.StorageName+"</td>"+
                        "<td height='22' align='center'>"+item.ProductName+"</td>"+
                        "<td height='22' align='center'>"+item.ProductCount+"</td>"+
                        td+
                        "<td height='22' align='center'>"+item.storagetype+"</td>"+
                        "<td height='22' align='center'>"+item.ModifiedDateCn+"</td>").appendTo($("#pageDataList1 tbody"));                        
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
                        ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pageSellOffcount"));
                      //document.getElementById('tdResult').style.display='block';
                      },
               error: function() {}, 
               complete:function(){hidePopup();$("#pageDataList1_Pager").show();pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");Ifshow(document.getElementById("Text2").value);}//接收数据完毕
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
function CheckDate(startDate,endDate)
{
    startDate=startDate.replace(/-/g,"/");  
    endDate=endDate.replace(/-/g,"/");   
    if(Date.parse(startDate)-Date.parse(endDate)>0)
    {   
        return false;   
    }
    else
    {
        return true;
    }
}

function Fun_Search_LogInfo()
{
    var opendate=document.getElementById("txtOpenDate").value;
    var closedate = document.getElementById("txtCloseDate").value;
    var mod = document.getElementById("btn_mod").value;
   
    if(!CheckDate(opendate,closedate))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","开始时间不能大于结束时间！");
        return;
    }
     
      var search = "";
           var OpenDate=document.getElementById("txtOpenDate").value;
           var CloseDate = document.getElementById("txtCloseDate").value;
           
           var UserID= document.getElementById("uids").value;

          search+="UserID="+escape(UserID);
          search+="&OpenDate="+escape(OpenDate);
          search+="&CloseDate="+escape(CloseDate); 
          search+="&mod="+escape(mod);
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;    
    TurnToPage(1);
}

function Ifshow(count)
    {
        if(count=="0")
        {
            document.getElementById("divpage").style.display = "none";
        }
        else
        {
            document.getElementById('divpage').style.display = "block";
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

