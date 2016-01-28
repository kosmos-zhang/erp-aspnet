 $(document).ready(function(){
       var selctrlaimyear  =document.getElementById("selTaskYear");
    selctrlaimyear.length = 0;
    var startyear =2007 ;
    for( var i = 0;i<=13;i++){        
    selctrlaimyear.options.add(new Option( (startyear+i),startyear+i )); 
   }
      selctrlaimyear.value =     new Date().getYear();       
});
 
 function getMessage()
 {
   var searchinfo = "";
    //部门
        searchinfo += "&DeptID=" + escape ( document.getElementById("txtDeptID").value.Trim());
       // alert (document.getElementById("txtDeptID").value);
    //年度
    searchinfo += "&year=" +escape (  document.getElementById("selTaskYear").value.Trim());
    //设置检索条件
    document.getElementById("hidSearchCondition").value = searchinfo;
 }
 
 
 function PrintRecibal()
       {
       getMessage();
         var searchStr = document.getElementById("hidSearchCondition").value;
         window.open("PrintSalarySamePeriodCompare.aspx?"+searchStr);
       }
           function DoSearchInfo(currPage)
{ 
    var search = "";
    //部门
        search += "&DeptID=" + escape ( document.getElementById("txtDeptID").value.Trim());
       // alert (document.getElementById("txtDeptID").value);
    //年度
    search += "&year=" +escape (  document.getElementById("selTaskYear").value.Trim());
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;
    
    TurnToPage(1);
}

/* 分页相关变量定义 */  

var pageCount = 10;//每页显示记录数
var totalRecord = 0;//总记录数
var pagerStyle = "flickr";//jPagerBar样式
var currentPageIndex = 1;//当前页
var orderBy = "";//排序字段


/*
* 翻页处理
*/
function TurnToPage(pageIndex)
{
    //设置当前页
    currentPageIndex = pageIndex;
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    //设置动作种类
    var action="SearchDetailsInfo";
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&action=" + action + "&OrderBy=" + orderBy + "&" + searchCondition;
  
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/SalaryStandard.ashx?' + postParam,//目标地址
        dataType:"json",//数据格式:JSON
        cache:false,
        beforeSend:function()
        {
            AddPop();
        },//发送数据之前
        success: function(msg)
        {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#tblDetailInfo tbody").find("tr.newrow").remove();
            $.each(msg.data
                ,function(i,item)
                {
                    if(item.BillStatus != null && item.BillStatus != "")
                    $("<tr class='newrow'></tr>").append(
                    "<td height='22' align='center'>" + item.BillStatus + "</td>" 
                    
                     +"<td height='22' align='center'>" + item.CarryType + "</td>" //1月
                    + "<td height='22' align='center'>" + item.CloseDate + "</td>" //考核任务主题
                    
                        + "<td height='22' align='center'>" + item.Closer + "</td>"//2月
                         + "<td height='22' align='center'>" + item.CompanyCD + "</td>"
                         
                          + "<td height='22' align='center'>" + item.ConfirmDate+ "</td>"//3月
                            + "<td height='22' align='center'>" + item.Confirmor+ "</td>"
                            
                             + "<td height='22' align='center'>" + item.CountTotal+ "</td>"//4月
                            + "<td height='22' align='center'>" + item.CreateDate+ "</td>"
                          
                             + "<td height='22' align='center'>" + item.Creator+ "</td>"//5月
                            + "<td height='22' align='center'>" + item.CurrencyType+ "</td>"
                            
                             + "<td height='22' align='center'>" + item.DeptID+ "</td>"//6月
                            + "<td height='22' align='center'>" + item.Discount+ "</td>"
                            
                             + "<td height='22' align='center'>" + item.DiscountTotal+ "</td>"//7月
                            + "<td height='22' align='center'>" + item.ProviderID+ "</td>"
                       
                             + "<td height='22' align='center'>" + item.MoneyType+ "</td>"//8月
                            + "<td height='22' align='center'>" + item.PayType+ "</td>"
                           
                             + "<td height='22' align='center'>" + item.Purchaser+ "</td>"//9月
                            + "<td height='22' align='center'>" + item.Rate+ "</td>"
                          
                             + "<td height='22' align='center'>" + item.RealTotal+ "</td>"//10月
                            + "<td height='22' align='center'>" + item.ReceiveMan+ "</td>"
                       
                             + "<td height='22' align='center'>" + item.FromType+ "</td>"//11月
                            + "<td height='22' align='center'>" + item.ID+ "</td>"
                       
                            + "<td height='22' align='center'>" + item.isAddTax+ "</td>"//12月
                            + "<td height='22' align='center'>" + item.ModifiedDate+ "</td>"
                            + "<td height='22' align='center'>" + item.ModifiedUserID +"</td>").appendTo($("#tblDetailInfo tbody")
                          //启用状态
                    );
            });
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
* 排序处理
*/
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