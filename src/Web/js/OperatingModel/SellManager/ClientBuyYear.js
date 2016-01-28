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
 
    var CustID=document.getElementById('hfCustID').value;
    var OrderDate=document.getElementById('OrderDate').value;
    var MoneyType=document.getElementById('MoneyType').title;
    var CurrencyTypeName=document.getElementById('MoneyType').value;
      if(OrderDate=='')
    {
        popMsgObj.ShowMsg('年度不能为空！');
        return false;
    }
    currentPageIndex = pageIndex;
    var MyData = "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderBy="+orderBy+
                     "&CustID="+escape(CustID)+"&OrderDate="+escape(OrderDate)+"&CurrencyType="+escape(MoneyType)+"&CurrencyTypeName="+escape(CurrencyTypeName); 
    var search = "";
    search+="&CustID="+CustID;//
    search+="&OrderDate="+OrderDate;//
    search+="&orderBy="+orderBy;
    search+="&CurrencyTypeName="+CurrencyTypeName;
    search+="&CurrencyType="+MoneyType;
    search+="";
    document.getElementById("hidSearchCondition").value = search;
    document.getElementById("btnPrint").style.display=""; 
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/OperatingModel/SellManager/ClientBuyYear.ashx',//目标地址
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
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+item.CustName+"</td>"+
                        "<td height='22' align='center' style='display:none'>"+(j++)+"</td>"+                                                
                        "<td height='22' align='right'>" +parseFloat(item.NowMonth).toFixed(2)+ "</td>"+
                        "<td height='22' align='right'>"+parseFloat(item.NowYear).toFixed(2)+"</td>"+
                        "<td height='22' align='right'>"+parseFloat(item.LastMonth).toFixed(2)+"</td>"+
                        "<td height='22' align='right'>"+parseFloat(item.LastYear).toFixed(2)+"</td>"+                       
                        "<td height='22' align='right'>"+ parseFloat(item.AllTotalPrice).toFixed(2)+"</td>").appendTo($("#tblDetailInfo tbody"));                      
                        
                   });
//                     $("<tr class='newrow' ></tr>").append("<td height='22' align='center'>合计</td>"
//                           +"<td height='22' align='center'>"+msg.TotalNowMonth+"</td>"
//                           +"<td height='22' align='center'>"+msg.TotalNowYear+"</td>"
//                           +"<td height='22' align='center'>"+msg.TotalLastMonth+"</td>"
//                             +"<td height='22' align='center'>"+msg.TotalLastYear+"</td><td height='22' align='center'>"+msg.TotalPrice+"</td>").appendTo($("#tblDetailInfo tbody")); 
                             //页码
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
    var searchStr = document.getElementById("hidSearchCondition").value;
    window.open("PrintClientBuyYear.aspx?type=1"+searchStr);
}
function GetFloat(a)
{
   if(a!=null && a!='')
   {
        var testFlaot=a.toString().indexOf('.');
        var myValue=a;
        if(testFlaot!=-1)
        {
            var length=a.length;
            for(var i=0;i<length;i++)
            {
                
            }
            
            var testvalue=a.toString().split('.');
            var myvalue=testvalue[1];
            
            if(myvalue=='00' || myvalue=='0' || myvalue =='000' || myvalue=='0000')
            {
                myValue= testvalue[0];
            } 
            if(myvalue.toString().length>2 && myvalue!='000' && myvalue!='0000')
            {
                myValue= parseFloat(a.toString()).toFixed(2);
            }       
            
        }
        return myValue;
    }
}