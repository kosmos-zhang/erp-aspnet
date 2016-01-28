var pageCount = 10;//每页计数
var totalRecord = 0;
var pagerStyle = "flickr";//jPagerBar样式

var currentPageIndex = 1;
var action = "";//操作
var orderBy = "";//排序字段  

$(document).ready(function()
{
    fnGetExtAttr();
    requestobj = GetRequest(); 
    var CID = requestobj['CID'];
    var CustName = requestobj['CustName'];
    var ProductID = requestobj['ProductID'];
    var ProductName = requestobj['ProductName'];
    var DateBegin = requestobj['DateBegin'];
    var DateEnd = requestobj['DateEnd'];
    
    if(typeof(CID)!="undefined")
    { 
       $("#hfCustID").attr("value",CID);//客户ID
       $("#txtUcCustName").attr("value",CustName);//客户名称       
       $("#hfProductID").attr("value",ProductID);       
       $("#txtProductName").attr("value",ProductName);       
       $("#txtTalkBegin").attr("value",DateBegin);
       $("#txtTalkEnd").attr("value",DateEnd);      
       currentPageIndex = requestobj['currentPageIndex'];
       pageCount = requestobj['pageCount'];
       SearchSell(currentPageIndex);
    }   
});
    
     
function Fun_FillParent_Content(id,no,productname,price,unit,codyt,taxrate,selltax,discount,d,TypeCode,name)
{
    document.getElementById("hfProductID").value = id;
    document.getElementById("txtProductName").value = productname;
}
//清除物品控件带出的信息，并关闭控件
function ClearPkroductInfo()
{
    document.getElementById("hfProductID").value = '';
    document.getElementById("txtProductName").value = '';
    closeProductdiv();
}

 function TurnToPageSell(pageIndex)
{
    if (!CheckInput()) {
        return;
    }
       currentPageIndex = pageIndex;
       var CustID = document.getElementById("hfCustID").value;//客户ID
       var ProductID = document.getElementById("hfProductID").value;//产品ID      
       var DateBegin = document.getElementById("txtTalkBegin").value;//开始时间
       var DateEnd = document.getElementById("txtTalkEnd").value;//结束时间
       action="getlist";
       $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/ServiceSellAnnal.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+
                    "&CustID="+reescape(CustID)+"&ProductID="+reescape(ProductID)+"&DateBegin="+reescape(DateBegin)+"&DateEnd="+reescape(DateEnd),//数据
           beforeSend:function(){AddPop();$("#pageDataListSell_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListSell tbody").find("tr.newrow").remove();
                    var diffdays;
                    $.each(msg.data,function(i,item){
                      if(item.id != null && item.id != "")
                      {
                        if(item.days < 0)
                        {
                            diffdays = "";
                        }
                        else
                        {
                            diffdays = item.days;
                        }
                        var showUnitStr="";
                        //多计量单位
                        if($("#isMoreUnitID").val()=="1")
                        {
                            showUnitStr=" style=\"display:''\"";
                        }
                        else
                        {
                            showUnitStr=" style=\"display:none\"";
                        }
                        $("<tr class='newrow'></tr>").append(                        
                        "<td height='22' align='center'><a title='" + item.custname + "' href='#' onclick=SelectCust('"+item.custid+"','"+item.CustNo+"','"+item.CustBig+"','"+item.CanViewUser+"','"+item.Manager+"','"+item.Creator+"')>" + SubValue(10,item.custname) + "</a></td>"+ 
                        "<td height='22' align='center' title='" + item.ProductName + "'>" + SubValue(10,item.ProductName) + "</td>"+ 
                        "<td height='22' align='center' title='" + item.OrderNo + "'>" + SubValue(10,item.OrderNo) + "</td>"+
                        "<td height='22' align='center' title='" + item.title + "'>" + SubValue(10,item.title) + "</td>"+
                         "<td height='22' align='center'>" + item.orderDate + "</td>"+
                        "<td height='22' align='center'>" + item.EmployeeName + "</td>"+   
                        "<td id='baseUnit' height='22' align='center' "+showUnitStr+">" + item.UnitName + "</td>"+  
                        "<td height='22' align='center'>" + item.UsedUnitName + "</td>"+  
                        "<td height='22' align='center'>" + item.UnitPrice + "</td>"+                      
                        "<td height='22' align='center'>" + item.ProductCount + "</td>"+
                        "<td height='22' align='center'>" + item.SendCount + "</td>"+
                        "<td height='22' align='center'>" + item.OutCount + "</td>"+
                        "<td height='22' align='center'>" + item.BackCount + "</td>"+
                        "<td height='22' align='center'>" + item.TotalPrice + "</td>"+
                        //"<td height='22' align='center'>" + item.YAccounts + "</td>"+
                        //"<td height='22' align='center'>" + item.NAccounts + "</td>"+
                        "<td height='22' align='center'>" + item.MaxCreditDate + "</td>"+
                        "<td height='22' align='center'>" + diffdays + "</td>").appendTo($("#pageDataListSell tbody"));
                        }
                   });
                   
                    //页码
                   ShowPageBar("pageDataListSell_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPageSell({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                  
                  document.getElementById("Text2").value=msg.totalCount;
                  $("#ShowPageCountSell").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecountSell"));
                  $("#ToPageSell").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){hidePopup();$("#pageDataListSell_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataListSell","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
       
}

//检索
function SearchSell(aa)
{
    TurnToPageSell(aa);
}
//根据客户ID转到客户信息查看
function SelectCust(id,custno,custbig,canuser,manager,creator)
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
    var CID = document.getElementById("hfCustID").value;//客户ID
    var CustName = document.getElementById("txtUcCustName").value;//客户名称
    var ProductID = document.getElementById("hfProductID").value;//产品ID
    var ProductName = document.getElementById("txtProductName").value;//产品名    
    var DateBegin = document.getElementById("txtTalkBegin").value;//开始时间
    var DateEnd = document.getElementById("txtTalkEnd").value;//结束时间
   
    window.location.href='Cust_Add.aspx?custid='+reescape(id)+'&Pages=ServiceSellAnnal_Info.aspx&CID='+reescape(CID)+'&CustName='+reescape(CustName)+'&ProductID='+reescape(ProductID)+
        '&ProductName='+reescape(ProductName)+'&DateBegin='+reescape(DateBegin)+'&DateEnd='+reescape(DateEnd)+'&currentPageIndex='+reescape(currentPageIndex)+'&pageCount='+reescape(pageCount)+
        '&ModuleID=2021103';
}

function Ifshow(count)
{
    if(count=="0")
    {
        document.getElementById("divpageSell").style.display = "none";
        document.getElementById("pagecountSell").style.display = "none";
    }
    else
    {
        document.getElementById("divpageSell").style.display = "block";
        document.getElementById("pagecountSell").style.display = "block";
    }
}

//改变每页记录数及跳至页数
function ChangePageCountIndexSell(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pageCount=parseInt(newPageCount);
        TurnToPageSell(parseInt(newPageIndex));
    }
}

 //排序
function OrderBy(orderColum,orderTip)
{
    if (totalRecord == 0) 
     {
        return;
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
    TurnToPageSell(1);
}

//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";

    var BeginDate = $("#txtTalkBegin").val(); //开始日期
    var EndDate = $("#txtTalkEnd").val(); //结束日期
    
    if(BeginDate!="" && EndDate!="")
    {
        if(CompareDate(BeginDate,EndDate)>0)
        {
            isFlag=false;
            fieldText=fieldText+"销售日期|";
            msgText=msgText+"开始日期不能大于结束日期|";
        }
    }
    
    if (!isFlag) {
        //注：两个方法显示弹出提示信息层,方法一是对字段用红色处理，方法二是所有的提示信息传入未处理颜色

        //方法一
        popMsgObj.Show(fieldText, msgText);

        //方法二
        //popMsgObj.ShowMsg('所有的错误信息字符串');
    }

    return isFlag;
} 