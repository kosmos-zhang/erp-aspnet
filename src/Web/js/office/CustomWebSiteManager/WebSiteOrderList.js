var totalRecord = 0;
var pagerStyle = "flickr";//jPagerBar样式
var flag="";
var ActionFlag=""
var str="";
var currentPageIndex = 1;
var action = "";//操作
var orderBy = "";//排序字段
var pageCount=10;
   
// 界面加载时
$(document).ready(function()
{
//    SearchData();
});  


//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex)
{

//    var orderNo = escape($("#txtOrderNo").val());

//    if (!CheckSpecialWord(orderNo)) {
//        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "订单编号不能包含特殊字符");
//        return;
//    }

    var isFlag = true;
    var RetVal = CheckSpecialWords();
    var fieldText = "";
    var msgText = "";
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }

    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return;
        //showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "订单编号不能包含特殊字符");
    }
    


    var para="action=getlist"+
                   "&pageIndex="+pageIndex+
                   "&pageCount="+pageCount+
                   "&orderby="+$("#txtOrderBy").val()+
                   "&OrderNo="+escape($("#txtOrderNo").val())+
                   "&OrderDateStart="+escape($("#txtOrderDateStart").val())+
                   "&OrderDateEnd="+escape($("#txtOrderDateEnd").val())+
                   "&ConsignmentDateStart="+escape($("#txtConsignmentDateStart").val())+
                   "&ConsignmentDateEnd="+escape($("#txtConsignmentDateEnd").val())+
                   "&Status="+escape($("#selOrderStatus").val());
    $.ajax({
        type: "POST",//用POST方式传输
        dataType:"json",//数据格式:JSON
        url:  '../../../Handler/Office/CustomWebSiteManager/WebSiteOrderInfo.ashx',//目标地址
        cache:false,
        data: para,//数据
        beforeSend:function()
        {
            AddPop();
            $("#pageDataList1_Pager").hide();
        },//发送数据之前
        success: function(msg)
        {
  
            //数据获取完毕，填充页面据显示
            //数据列表 createTd( "<input id='Checkbox1' name='Checkbox1'  value='"+item.ID+"' onclick=IfSelectAll('Checkbox1','btnAll') type='checkbox'/>")+
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data,function(i,item){
                  $("<tr class='newrow'></tr>").append( createTd("<a href=\"javascript:void(0);\" onclick=\"getInfo('"+item.OrderNo+"')\">"+ item.OrderNo+"</a>")+
                  createTd( item.CustName )+
                  createTd( item.OrderDate)+
                  createTd(item.ConsignmentDate)+
                  createTd(item.StatusName)).appendTo($("#pageDataList1 tbody"));
            });
            //页码
            ShowPageBar("pageDataList1_PagerList",//[containerId]提供装载页码栏的容器标签的客户端ID
            "<%= Request.Url.AbsolutePath %>",//[url]
            {style:pagerStyle,mark:"pageDataList1Mark",
            totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
            onclick:"TurnToPage({pageindex});return false;"}//[attr]
            );
            totalRecord = msg.totalCount;
            ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
            document.getElementById('Text2').value=msg.totalCount;
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount,pageCount,pageIndex);
            $("#ToPage").val(pageIndex);
        },
        error: function() 
        {   
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        complete:function()
        {
            hidePopup();
            $("#pageDataList1_PagerList").show();
            pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");
            $("#btnAll").attr("checked",false);
        }//接收数据完毕
    });
}



/* 获取订单信息 */
function getInfo(ordeno)
{
    window.location.href="WebSiteOrderInfo.aspx?OrderNo="+ordeno;
}

/*构造td*/
function createTd(value)
{
    return "<td align=\"center\" height='22' >"+value+"</td>";
}

/*设置排序字段*/
function CreateSort(control) {

        var ordering=document.getElementById("txtOrderBy");
        var obj=document.getElementById(control);
        var allOrderTipDOM  = $(".orderTip");
        allOrderTipDOM.empty();
        if(ordering.value==(control+ " ASC"))
        {
            ordering.value=control+ " DESC";
            obj.innerHTML="↓";
        }
        else
        {
            ordering.value=control+ " ASC";
            obj.innerHTML="↑";
        }
        TurnToPage(1);
}


//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount,newPageIndex)
{
    if(!IsZint(newPageCount))
    {
        popMsgObj.ShowMsg('显示条数格式不对，必须是正整数！');
        return;
    }
    if(!IsZint(newPageIndex))
    {
        popMsgObj.ShowMsg('跳转页数格式不对，必须是正整数！');
        return;
    }
    if(newPageCount <=0 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","显示页数超出显示范围！");
        return false;
    }
    if(newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pageCount=parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
        $("#btnAll").attr("checked",false);
    }
}