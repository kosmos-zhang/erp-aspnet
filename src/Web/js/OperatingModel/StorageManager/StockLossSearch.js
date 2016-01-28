    /*
* 查询
*/
function DoSearch(currPage)
{

   var fieldText = "";
   var msgText = "";
   var isFlag = true;

   var LossNo= document.getElementById("txtLossNo").value;
   var Title=document.getElementById("txtTitle").value;
   var Dept=$("#txtDeptID").val();
   var StorageID=document.getElementById("ddlStorage").value;
   var Executor=$("#txtExecutorID").val();
   var FlowStatus=document.getElementById("sltFlowStatus").value;
   var ReasonType=document.getElementById('ddlReason').value;
   var sltBillStatus=document.getElementById("sltBillStatus").value;
   var LossDateStart=document.getElementById("txtLossDateStart").value;
   var LossDateEnd=document.getElementById("txtLossDateEnd").value;
   var TotalPriceStart=$("#txtTotalPriceStart").val();
   var TotalPriceEnd=$("#txtTotalPriceEnd").val();
   var DeptName=$("#DeptName").val();
   var UserExecutor=$("#UserExecutor").val();
   
   var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
	        msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
   
   if(CompareDate(LossDateStart, LossDateEnd)==1)
   {
        isFlag=false;
        fieldText=fieldText + "查询时间段|";
        msgText = msgText +  "起始时间不能大于终止时间|";
   }
   if(!isFlag)
   {
        popMsgObj.Show(fieldText,msgText);
        return;
   }
   
   
   var UrlParam="&action="+action+
   "&LossNo="+escape(LossNo)+"&Title="+escape(Title)+
   "&Dept="+escape(Dept)+
   "&StorageID="+escape(StorageID)+
   "&Executor="+escape(Executor)+
   "&FlowStatus="+escape(FlowStatus)+
   "&BillStatus="+escape(sltBillStatus)+
   "&ReasonType="+escape(ReasonType)+
   "&LossDateStart="+escape(LossDateStart)+"&LossDateEnd="+escape(LossDateEnd)+
   "&TotalPriceStart="+escape(TotalPriceStart)+"&TotalPriceEnd="+escape(TotalPriceEnd)+
   "&DeptName="+escape(DeptName)+"&UserExecutor="+escape(UserExecutor)+
   "&Flag=1";
    
    //设置检索条件
    document.getElementById("hidSearchCondition").value = UrlParam;
    if (currPage == null || typeof(currPage) == "undefined")
    {
        TurnToPage(1);
    }
    else
    {
        TurnToPage(parseInt(currPage,10));
    }
}
 /*-----------------------------------------DoSearch结束----------------------------------------------*/

    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {   
            $("#checkall").attr("checked",false);    
            currentPageIndex = pageIndex;
           //获取查询条件
           var searchCondition = document.getElementById("hidSearchCondition").value;
           
           var UrlParam = "pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&orderBy=" + orderBy + "&" + searchCondition;
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/OperatingModel/StorageManager/StockLossSearch.ashx',//目标地址
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
                            "<td height='22' align='center' title=\""+item.LossNo+"\">"+ fnjiequ(item.LossNo,10) +"</td>"+
                            "<td height='22' align='center' title=\""+item.Title+"\">"+ fnjiequ(item.Title,10) +"</td>"+
                            "<td height='22' align='center' title=\""+item.Executor+"\">"+fnjiequ(item.Executor,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.DeptName+"\">"+fnjiequ(item.DeptName,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.StorageName+"\">"+fnjiequ(item.StorageName,10)+"</td>"+
                            "<td height='22' align='center'>"+item.LossDate+"</td>"+
                            "<td height='22' align='center' title=\""+item.ReasonTypeName+"\">"+fnjiequ(item.ReasonTypeName,10)+"</td>"+
                            "<td height='22' align='center' title=\""+item.TotalPrice.slice(0,item.TotalPrice.length-2)+"\">"+fnjiequ(item.TotalPrice.slice(0,item.TotalPrice.length-2),10)+"</td>"+
                            "<td id='BillStatusName"+item.ID+"' height='22' align='center'>"+item.BillStatusName+"</td>"+
                            "<td id='FlowStatus"+item.ID+"' height='22' align='center'>"+item.FlowStatus+"</td>").appendTo($("#pageDataList1 tbody"));
                            
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


function Ifshow(count)
    {
        if(count=="0")
        {
            document.getElementById("divpage").style.display = "none";
            document.getElementById("pagecount").style.display = "none";
        }
        else
        {
            document.getElementById("divpage").style.display = "";
            document.getElementById("pagecount").style.display = "";
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
    var ordering = "a";
    function OrderBy(orderColum,orderTip)
    {
        ordering = "a";
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
        DoSearch();
    }


//打印
function fnPrint() {
    if(ordering=="a")
    {
        ordering="asc";
    }
    else
    {
        ordering="desc"
    }
    var order=orderBy.split('_')[0] + " " + ordering;//字段和升降序规则
    var strRul = $("#hidSearchCondition").val();
    window.open("PrintStockLossSearch.aspx?order="+order+"&" + strRul);
}