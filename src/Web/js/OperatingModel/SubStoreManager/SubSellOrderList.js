    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var ifshow="0";
    var currentPageIndex = 1;
    var orderBy = "";//排序字段
    var reset="1";
    var action="";
    
$(document).ready(function()
{ 
    var requestobj = GetRequest(location.search);
    var SourcePage = requestobj['Page'];
    if( SourcePage == "Add")
    {//从编辑页面链接过来的
        fnSetSearchCondition(requestobj);
        fnGetSearchCondition();
        TurnToPage(currentPageIndex);
    }
});


//获取url中"?"符后的字串
function GetRequest(url)
{
   var theRequest = new Object();
   if (url.indexOf("?") != -1) 
   {
      var str = url.substr(1);
      strs = str.split("&");
      for(var i = 0; i < strs.length; i ++) 
      {
         theRequest[strs[i].split("=")[0]]=unescape(strs[i].split("=")[1]);
      }
   }
   return theRequest;
}
function IsOut()
{
    if(parseFloat(totalRecord)<1)
    {
        alert('请先检索！');
        return false;
    }
}
//=================================== Start 界 面 取 值 ================================
function fnGetSearchCondition()
{
    var Info = "";
    Info += "OrderNo="+$("#txtOrderNo").val();
    Info += "&Title="+$("#txtOrderTitle").val();
    Info += "&SendMode="+$("#ddlSendMode").val();
    Info += "&CustName="+$("#txtCustName").val();
    Info += "&CustTel="+$("#txtCustTel").val();
    
    Info += "&CustMobile="+$("#txtCustMobile").val();
    Info += "&CustAddr="+$("#txtCustAddr").val();
    Info += "&DeptID="+$("#hidDeptID").val();
    Info += "&DeptName="+$("#DeptName").val();
    Info += "&Seller="+$("#hidUserID").val();
    
    Info += "&SellerName="+$("#UserName").val();
    Info += "&BusiStatus="+$("#ddlBusiStatus").val();
    Info += "&BillStatus="+$("#ddlBillStatus").val();
    $("#SearchCondition").val(Info);  
}
//=================================== End   界 面 取 值 ================================


//=================================== Start 界 面 赋 值 ================================
function fnSetSearchCondition(requestobj)
{
    $("#txtOrderNo").val(requestobj['OrderNo']);
    $("#txtOrderTitle").val(requestobj['OrderTitle']);
    $("#ddlSendMode").val(requestobj['SendMode']);
    $("#txtCustName").val(requestobj['CustName']);
    $("#txtCustTel").val(requestobj['CustTel']);
    
    $("#txtCustMobile").val(requestobj['CustMobile']);
    $("#txtCustAddr").val(requestobj['CustAddr']);    
    $("#hidDeptID").val(requestobj['DeptID']);
    $("#DeptName").val(requestobj['DeptName']);
    $("#hidUserID").val(requestobj['Seller']);
    
    $("#UserName").val(requestobj['SellerName']);
    $("#ddlBusiStatus").val(requestobj['BusiStatus']);
    $("#ddlBillStatus").val(requestobj['BillStatus']);
}
//=================================== End   界 面 赋 值 ================================


//=================================== Start 检       索 ================================
function fnCheck()
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
   } 
   return isFlag;   
}

function fnSearch()
{
    if(!fnCheck())
    return;
    fnGetSearchCondition();
    
    TurnToPage(1);
}


function TurnToPage(pageIndex)
{
currentPageIndex = pageIndex;
$("#checkall").attr("checked",false)
    //获取检索条件
    var URLParams = $("#SearchCondition").val();
    URLParams += "&pageIndex="+pageIndex;
    URLParams += "&pageCount="+pageCount;
    URLParams += "&orderBy="+orderBy;
    URLParams += "&Action=Select";
    URLParams += "&EFIndex=";
    URLParams += "&EFDesc=";
    
    $.ajax(
    {
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SubStoreManager/SubSellOrderList.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg)
           {
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#SubSellOrderInfo tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        var j=i+1;
                        if(item.ID != null && item.ID != "")
                        {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox"+j+"' name='Checkbox'  value="+item.ID+" onclick=\"IfSelectAll('Checkbox','checkall')\"  type='checkbox'/>"+"</td>"+
                            "<td height='22' align='center' id=\"orderno"+j+"\" title=\""+item.OrderNo+"\"><a href='#' onclick=fnToEdit('"+item.ID+"')>"+ GetStandardString(item.OrderNo,15) +"</td>"+
                            "<td height='22' align='center' title=\""+item.Title+"\">"+ GetStandardString(item.Title,15) +"</td>"+
                            "<td height='22' align='center'>"+ item.SendModeName +"</td>"+
                            "<td height='22' align='center'>"+ GetStandardString(item.CustName,15) +"</td>"+
                            "<td height='22' align='center'>"+ GetStandardString(item.CustTel,15) +"</td>"+
                            "<td height='22' align='center'>"+ GetStandardString(item.CustMobile,15) +"</td>"+
                            "<td height='22' align='center'>"+ GetStandardString(item.CustAddr,15) +"</td>"+
                            "<td height='22' align='center'>"+ GetStandardString(item.DeptName,15) +"</td>"+
                            "<td height='22' align='center'>"+ GetStandardString(item.SellerName,15) +"</td>"+
                            "<td height='22' align='center' >"+ item.BusiStatusName +"</td>"+
                            "<td height='22' align='center' >"+ item.BillStatusName +"</td>" ).appendTo($("#SubSellOrderInfo tbody"));
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
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);
           },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("SubSellOrderInfo","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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
        document.getElementById("divpage").style.display = "block";
        document.getElementById("pagecount").style.display = "block";
    }
}

function fnSelectAll()
{
    $.each($("#SubSellOrderInfo :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
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
            if(parseFloat(totalRecord)<1)
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
        fnSearch();
    }

//=================================== End   检       索 ================================

//=================================== Start 跳转至编辑页面 ================================
function fnToEdit(ID)
{
    var URLParams = $("#SearchCondition").val();
    URLParams += "&ID="+ID;
    URLParams += "&pageIndex="+currentPageIndex;
    URLParams += "&pageCount="+pageCount;
    var Page = "List";
    URLParams += "&Page="+Page;
    URLParams += "&orderBy="+orderBy;
    URLParams += "&ModuleID=33333333";
    
    window.location.href = 'SubSellOrderAdd.aspx?'+URLParams;
}

function fnOutSttl(ID)
{
    var URLParams = $("#SearchCondition").val();
    URLParams += "&ID="+ID;
    URLParams += "&pageIndex="+currentPageIndex;
    URLParams += "&pageCount="+pageCount;
    var Page = "List";
    URLParams += "&Page="+Page;
    URLParams += "&orderBy="+orderBy;
    URLParams += "&OutSttl=OutSttl";
    URLParams += "&ModuleID=2121201";
    
    window.location.href = 'SubSellOrderAdd.aspx?'+URLParams;
}



//=================================== End   跳转至编辑页面 ================================