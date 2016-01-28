var pageCount = 10;//每页计数
var totalRecord = 0;
var pagerStyle = "flickr";//jPagerBar样式
var currentPageIndex = 1;
var action = "";//操作
var orderBy = "";//排序字段
function TurnToPage(pageIndex)
{//检索
    currentPageIndex = pageIndex;
    //检索条件
 
    var CustNo=document.getElementById('hiddenCustIDL').value; //获取客户ID  替代以前的客户编号
    var CustName=document.getElementById('CustName').value;
    var BeginDate=document.getElementById('BeginDate').value;
    var EndDate=document.getElementById('EndDate').value;
    
    if(BeginDate=='')
    {
        popMsgObj.ShowMsg('起始时间不能为空！');
        return false;
    }
    
    if(EndDate=='')
    {
        popMsgObj.ShowMsg('结束时间不能为空！');
        return false;
    }
    currentPageIndex = pageIndex;
     
    var MyData = "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderBy="+orderBy+
                     "&CustNo="+escape(CustNo)+"&CustName="+escape(CustName)+"&BeginDate="+escape(BeginDate)+"&EndDate="+escape(EndDate); 

    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/OperatingModel/SellManager/GetCustSell.ashx',//目标地址
           cache:false,
           data: MyData,//数据
           beforeSend:function(){AddPop();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                 
                    $("#tblDetailInfo tbody").find("tr.newrow").remove();
                    var j =1;
                      $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+item.CustNo+"</td>"+
                        "<td height='22' align='center' style='display:none'>"+(j++)+"</td>"+                                                
                        "<td height='22' align='center'>" +item.CustName+ "</td>"+
                        "<td height='22' align='center'>"+ item.OtherNO+"</td>"+
                        "<td height='22' align='center'>"+item.BillingType+"</td>"+
                        "<td height='22' align='center'>"+item.ConfirmDate.substring(0,10)+"</td>"+      
                        "<td height='22' align='center'>"+ item.EmployeeName+"</td>"+
                        "<td height='22' align='center'>"+parseFloat(item.TotalPrice).toFixed(4)+"</td>").appendTo($("#tblDetailInfo tbody"));                      
                        
                   });
            ShowPageBar(
                "divPageClickInfo",//[containerId]提供装载页码栏的容器标签的客户端ID
                "<%= Request.Url.AbsolutePath %>",//[url]
                {
                    style:pagerStyle,mark:"DetailListMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageCount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上一页",
                    nextWord:"下一页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"
                }
            );
            totalRecord = msg.totalCount;
            $("#txtShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex,$("#pagecount"));
            $("#txtToPage").val(pageIndex);
        },
        error: function() 
        {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        complete:function(){
          document.getElementById('btnPrint').style.display='';
            hidePopup();
            $("#divPageClickInfo").show();
            SetTableRowColor("tblDetailInfo","#E7E7E7","#FFFFFF","#cfc","cfc");
        }
    });
}
/*
* 设置数据明细表的行颜色
*/
function SetTableRowColor(elem,colora,colorb, colorc, colord){
    //获取DIV中 行数据
    var tableTr = document.getElementById(elem).getElementsByTagName("tr");
    for(var i = 0; i < tableTr.length; i++)
    {
        //设置行颜色
        tableTr[i].style.backgroundColor = (tableTr[i].sectionRowIndex%2 == 0) ? colora:colorb;
        //设置鼠标落在行上时的颜色
        tableTr[i].onmouseover = function()
        {
            if(this.x != "1") this.style.backgroundColor = colorc;
        }
        //设置鼠标离开行时的颜色
        tableTr[i].onmouseout = function()
        {
            if(this.x != "1") this.style.backgroundColor = (this.sectionRowIndex%2 == 0) ? colora:colorb;
        }
    }
}

/*
* 改页显示
*/
function ChangePageCountIndex(newPageCount, newPageIndex)
{
    //判断是否是数字
    if (!IsNumber(newPageCount))
    {
        popMsgObj.ShowMsg('请输入正确的显示条数！');
        return;
    }
    if (!IsNumber(newPageIndex))
    {
        popMsgObj.ShowMsg('请输入正确的转到页数！');
        return;
    }
    //判断重置的页数是否超过最大页数
    if(newPageCount <=0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1)/newPageCount) + 1)
    {
        popMsgObj.ShowMsg('转至页数超出查询范围！');
    }
    else
    {
        //设置每页显示记录数
        this.pageCount = parseInt(newPageCount);
        //显示页面数据
        TurnToPage(parseInt(newPageIndex));
    }
}
/*
* 排序处理
*/
function OrderBy(orderColum,orderTip)
{
    if(parseFloat(totalRecord)<=0)
    {
        return false;
    }
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
function PrintSpreadsheetBalanceTotal()
{
    var CustNo=document.getElementById('hiddenCustIDL').value; //客户编号
    var CustName=document.getElementById('CustName').value;//客户名称
    var BeginDate=document.getElementById('BeginDate').value;
    var EndDate=document.getElementById('EndDate').value;
    var searchStr="orderBy="+orderBy+"&CustNo="+CustNo+"&BeginDate="+BeginDate+"&EndDate="+EndDate+"&pageIndex="+currentPageIndex+"&pageCount="+pageCount+"&CustName="+CustName;
    window.open("PrintCustSell.aspx?"+searchStr);
}
