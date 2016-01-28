    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var ifshow="0";
    var currentPageIndex = 1;
    var orderBy = "";//排序字段
    var reset="1";
    var action="";
    var isSearch=0;
 $(document).ready(function()
 {
    SearchSellChangeData(1);
 });

function SearchSellChangeData(thepageIndex)
{//检索
    //检索条件

     pageIndex=parseFloat(thepageIndex);
     requestQuaobj = GetRequest();   
     var ProductID=requestQuaobj['ProductID'];
     var BeginDate=requestQuaobj['BeginTime'];
     var EndDate=requestQuaobj['EndTime'];
     var ProviderID=requestQuaobj['ProviderID'];
     var XValue=requestQuaobj['XValue'];
     var TimeType=requestQuaobj['TimeType'];
     
     currentPageIndex = pageIndex;
      
    var MyData = "pageIndex="+pageIndex+"&TimeType="+TimeType+"&XValue="+escape(XValue)+"&pageCount="+pageCount+"&ProductID="+ProductID+"&BeginDate="+BeginDate+"&orderby="+orderBy+"&EndDate="+EndDate+"&ProviderID="+ProviderID;             
                 
   
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/OperatingModel/StorageManager/PrintManNoPassTendencyDetail.ashx',//目标地址
           cache:false,
            data: MyData,//数据
           beforeSend:function(){AddPop();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                 
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var j =1;
                    $.each(msg.data,function(i,item){
    
                   if(item.TaskNo!=''){
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+item.TaskNo+"</td>"+
                        "<td height='22' align='center' style='display:none'>"+(j++)+"</td>"+  
                        "<td height='22' align='center'>"+ item.Title + "</td>"+
                           "<td height='22' align='center'>"+ item.ProductName + "</td>"+
                        "<td height='22' align='center'>"+ item.EmployeeName +"</td>"+
                        "<td height='22' align='center'>"+ item.DeptName +"</td>"+
                            "<td height='22' align='center'>"+ item.ManufactureTypeName +"</td>"+
                        "<td height='22' align='center'>"+  item.ProductCount +"</td>"+
                        "<td height='22' align='center'>"+  item.NoPassCount +"</td>").appendTo($("#pageDataList1 tbody"));}
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"SearchSellChangeData({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById('Text2').value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagePurPlancount"));
                  $("#ToPage").val(pageIndex);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  },
           error: function() {}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById('Text2').value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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
    function SelectDept(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
        var fieldText = "";
        var msgText = "";
        var isFlag = true;
         if(!IsNumber(newPageIndex) || newPageIndex==0)
        {
            isFlag = false;
            fieldText = fieldText + "跳转页面|";
   		    msgText = msgText +  "必须为正整数格式|";
        }
        if(!IsNumber(newPageCount) || newPageCount==0)
        {
            isFlag = false;
            fieldText = fieldText + "每页显示|";
   		    msgText = msgText +  "必须为正整数格式|";
        }
        if(!isFlag)
        {
            popMsgObj.Show(fieldText,msgText);
        } 
    
 
        else
        {
            if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
            {
               showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
               return false;
            }
            else
            {
                this.pageCount=parseInt(newPageCount);
                SearchSellChangeData(parseInt(newPageIndex));
            }
        }
    }
    //排序
    function OrderBy11(orderColum,orderTip)
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
        document.getElementById('hiddenOrder').value=orderBy;
        SearchSellChangeData(1);
    }




