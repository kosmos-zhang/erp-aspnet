//$(document).ready(function(){
//      TurnToPage(currentPageIndex);
//    });    
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
            var fieldText = "";
            var msgText = "";
            var isFlag = true;
            
            var RetVal=CheckSpecialWords();
            if(RetVal!="")
            {
                    isFlag = false;
                    fieldText = fieldText + RetVal+"|";
   		            msgText = msgText +RetVal+  "不能含有特殊字符|";
            }
            
            if(!isFlag)
            {
                popMsgObj.Show(fieldText,msgText);
                return false;
            }
            
           currentPageIndex = pageIndex;
           var ddlStorage= document.getElementById("ddlStorage").value;
           var txtProductNo=document.getElementById("txtProductNo").value;
           var txtProductName=document.getElementById("txtProductName").value;

           var UrlParam="pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+
           "&orderby="+orderBy+"&ddlStorage="+escape(ddlStorage)+
           "&txtProductNo="+escape(txtProductNo)+
           "&txtProductName="+escape(txtProductName)
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/OperatingModel/StorageManager/StockTotalSearch.ashx',//目标地址
           data:UrlParam,
           cache:false,
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    if (msg.data.length != 0)
                    {
                        $("#btnPrint").css("display", "inline");
                        $.each(msg.data,function(i,item){
                            if(item.ID != null && item.ID != "")
                            $("<tr class='newrow'></tr>").append(
                            "<td height='22' align='center' title=\""+item.StorageNo+"\">"+ fnjiequ(item.StorageNo,10) +"</td>"+
                            "<td height='22' align='center' title=\""+item.StorageName+"\">"+ fnjiequ(item.StorageName,10) +"</td>"+
                            "<td height='22' align='center' title=\""+item.DeptName+"\">"+ fnjiequ(item.DeptName,10) +"</td>"+
                            "<td height='22' align='center' title=\""+item.ProductNo+"\">"+fnjiequ(item.ProductNo,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.ProductName+"\">"+fnjiequ(item.ProductName,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.Specification+"\">"+fnjiequ(item.Specification,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.UnitID+"\">"+fnjiequ(item.UnitID,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.ProductCount+"\">"+NumRound(item.ProductCount,2)+"</td>"+
                            "<td height='22' align='center' title=\""+item.RoadCount+"\">"+fnjiequ(item.RoadCount,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.OrderCount+"\">"+fnjiequ(item.OrderCount,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.OutCount+"\">"+fnjiequ(item.OutCount,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.UseCount+"\">"+NumRound(item.UseCount,2)+"</td>").appendTo($("#pageDataList1 tbody"));
                            
                       });
                     }  
                 
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.all["Text2"].value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);
                  },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.all["Text2"].value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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

function Fun_Search_StorageInfo(aa)
{
      search="1";
      TurnToPage(1);
}

function Ifshow(count)
    {
        if(count=="0")
        {
            document.all["divpage"].style.display = "none";
            document.all["pagecount"].style.display = "none";
        }
        else
        {
            document.all["divpage"].style.display = "block";
            document.all["pagecount"].style.display = "block";
        }
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
    
            //判断是否是数字
        if (!PositiveInteger(newPageCount))
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
            return;
        }
        if (!PositiveInteger(newPageIndex))
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
            this.pageCount=parseInt(newPageCount,10);
            TurnToPage(parseInt(newPageIndex,10));
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
        Fun_Search_StorageInfo();
    }
    
//物品控件
function Fun_FillParent_Content(id,ProNo,ProdName)
{
   document.getElementById('txtProductNo').value=ProNo;
   document.getElementById('txtProductName').value=ProdName;
}