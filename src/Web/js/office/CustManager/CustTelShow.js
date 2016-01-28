//******************************* 来电记录列表js Begin*************************//
var pageCount = 10;//每页计数
var totalRecord = 0;
var totalRecord_ts = 0;
var totalRecord_fw = 0;
var totalRecord_ll = 0;
var pagerStyle = "flickr";//jPagerBar样式

var currentPageIndex = 1;
var action = "";//操作
var orderBy = "";//排序字段
var orderBy_ts = "";
var orderBy_fw = "";
var orderBy_ll = "";

var Tel = "";
var CompanyCD = "";
var EmployeeID = "";
var CustID = "";
var SionID = "";
$(document).ready(function()
{
     requestobj = GetRequest();      
     Tel = requestobj['CustTel'];
     CompanyCD = requestobj['CompanyCD'];  
     EmployeeID = requestobj['EmployeeID'];  
     CustID = requestobj['CustID']; 
     
     SionID = requestobj['SessionID']; 
      
    TurnToPage_TelLog(1);//来电记录检索
    
    AddCustCall();//记录此次来点信息
    
    GetCustInfoByID()//读取来电客户信息
    
    GetCust_ts();//读取客户投诉前8条
    
    GetCust_fw();//读取客户服务前8条
    
    GetCust_ll();//读取客户联络前8条
    //
    
});

//jQuery-ajax获取JSON数据
    function TurnToPage_TelLog(pageIndex)
    {
           currentPageIndex = pageIndex;
           action = "TelLogList";
         
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustTelShow.ashx',//目标地址           
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+
           "&CustTel="+escape(Tel)+"&CompanyCD="+escape(CompanyCD)+"&EmployeeID="+escape(EmployeeID),//数据
           beforeSend:function(){$("#pageDataList1_Pager").hide();},//发送数据之前AddPop();
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                  
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append(
                            "<td height='22' align='center'><a href='#' onclick=SeleCall('"+item.ID+"')>" + item.Tel + "</a></td>"+
                            "<td height='22' align='center'>" + item.CallTime + "</td>"+
                            "<td height='22' align='center'>"+ item.EmployeeName +"</td>").appendTo($("#pageDataList1 tbody"));
                       }
                   });                   
                    //页码                    
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage_TelLog({pageindex});return false;"}//[attr]                    
                    );                    
                  totalRecord = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2").value=msg.totalCount;
                  //$("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex);
                 
                  $("#ToPage").val(pageIndex);
                  },
           error: function() 
           {    
                //popMsgObj.ShowMsg("请求发生错误！");
                alert("请求发生错误！");
           }, 
           complete:function()
                {
                    //hidePopup();
                    $("#pageDataList1_Pager").show();
                    Ifshow(document.getElementById("Text2").value);
                    pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");
                }//接收数据完毕
           });
    }

function Ifshow(count)
{
    if(count=="0")
    {
        document.getElementById("divpage").style.display = "none";
        //document.getElementById("pagecount").style.display = "none";
    }
    else
    {
        document.getElementById("divpage").style.display = "block";
        //document.getElementById("pagecount").style.display = "block";
    }
}
//table行颜色
function pageDataList1(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}
//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageIndex)
{
var newPageCount = pageCount;

    if(!PositiveInteger(newPageIndex))
    {
        //popMsgObj.ShowMsg("转到页数应为正整数！");
        alert("转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
    {
        //popMsgObj.ShowMsg("转到页数超出查询范围！");
        alert("共"+totalRecord+"条记录，转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pageCount=parseInt(newPageCount);
        TurnToPage_TelLog(parseInt(newPageIndex));
    }
}

//排序
function OrderBy(orderColum,orderTip)
{
    if (totalRecord == 0) 
     {
        return;
     }

    ifdel = "0";
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
    $("#hiddExpOrder").val(orderBy);
    TurnToPage_TelLog(1);
}
//******************************* 来电记录列表js End*************************//

function SeleCall(ID)
{
//转到客户管理-来电记录页面    
}

//******************************* 自动记录来电信息js Begion*************************//
function AddCustCall()
{
    action = "CallLogAdd";
    $.ajax({
        type:"POST",
        url:  '../../../Handler/Office/CustManager/CustTelShow.ashx',//目标地址
        data: 'action='+escape(action)+
            '&CompanyCD='+escape(CompanyCD)+
            '&EmployeeID='+escape(EmployeeID)+
            '&CustTel='+escape(Tel)+
            '&CustID='+escape(CustID),
            dataType:'json',
        cache:false,
        beforeSend:function()
          { 
          },
          error: function() 
          {
          },
          success:function(data) 
            { 
            }
      });
}
//******************************* 自动记录来电信息js End*************************//

//******************************* 读取来电客户信息js Begain*************************//

function GetCustInfoByID()
{
        action = "GetCustInfo";
       $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:  '../../../Handler/Office/CustManager/CustTelShow.ashx',//目标地址
       data:'CustID='+escape(CustID)+'&action='+escape(action)+'&Tel='+escape(Tel)+'&CompanyCD='+escape(CompanyCD),
       cache:false,
       beforeSend:function(){},//发送数据之前
       success: function(msg){
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
                        document.getElementById("txtCustName").value = item.CustName;
                        document.getElementById("txtLinkManName").value = item.LinkManName;
                        document.getElementById("txtLinkSex").value = item.Sex;
                        document.getElementById("txtLinkAge").value = item.Age;
                        document.getElementById("txtLinkEmail").value = item.MailAddress;
                        document.getElementById("txtLinkQQ").value = item.QQ;
                        
                        document.getElementById("txtTel").value = Tel;
                        document.getElementById("txtCustNo").value = item.CustNo;
                        document.getElementById("txtCustBig").value = item.CustBig;
                        document.getElementById("txtCustTypeManage").value = item.CustTypeManage;
                        document.getElementById("txtCustTypeSell").value = item.CustTypeSell;
                        document.getElementById("txtCustTypeTime").value = item.CustTypeTime;
                        document.getElementById("txtCreditGrade").value = item.CreditGradeName;
                        document.getElementById("txtCustClass").value = item.CustClassName;
                        document.getElementById("txtCustType").value = item.CustTypeName;
                        document.getElementById("txtCountryID").value = item.CountryIDName;
                        document.getElementById("txtManager").value = item.ManagerName;
                    }                      
                        
               });               
              },
       error: function() 
       {    
       }, 
       complete:function(){}//接收数据完毕
       });
}

//******************************* 读取来电客户信息js End*************************//


//读取客户投诉前10条
function GetCust_ts()
{
           action = "GetCust_ts";
         
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustTelShow.ashx',//目标地址           
           cache:false,
           data: "pageIndex_ts=1&pageCount_ts=10&action="+escape(action)+"&orderby_ts="+orderBy_ts+
           "&CustTel="+escape(Tel)+"&CompanyCD="+escape(CompanyCD)+"&EmployeeID="+escape(EmployeeID)+"&CustID="+escape(CustID),//数据
           beforeSend:function(){},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#Table_ts tbody").find("tr.newrow").remove();
                  
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append(
                            "<td height='22' align='center' title='"+ item.Title +"'>" + SubValue(12,item.Title) + "</td>"+
                            "<td height='22' align='center'>" + item.ComplainDate + "</td>"+
                            "<td height='22' align='center'>"+ item.State +"</td>").appendTo($("#Table_ts tbody"));
                       }
                   });
                    totalRecord_ts = msg.totalCount;
                  },
           error: function() 
           {
           }, 
           complete:function()
                {
                    pageDataList1("Table_ts","#E7E7E7","#FFFFFF","#cfc","cfc");
                }//接收数据完毕
           });
}

//投诉排序
function OrderBy_ts(orderColum,orderTip)
{
    if (totalRecord_ts == 0) 
     {
        return;
     }
    var ordering = "a";    
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
    orderBy_ts = orderColum+"_"+ordering;   
    GetCust_ts();
}

//读取客户服务前10条
function GetCust_fw()
{
           action = "GetCust_fw";
         
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustTelShow.ashx',//目标地址           
           cache:false,
           data: "pageIndex_fw=1&pageCount_fw=10&action="+escape(action)+"&orderby_fw="+orderBy_fw+
           "&CustTel="+escape(Tel)+"&CompanyCD="+escape(CompanyCD)+"&EmployeeID="+escape(EmployeeID)+"&CustID="+escape(CustID),//数据
           beforeSend:function(){},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#Table_fw tbody").find("tr.newrow").remove();
                  
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append(
                            "<td height='22' align='center' title='"+ item.Title_fw +"'>" + SubValue(12,item.Title_fw) + "</td>"+
                            "<td height='22' align='center'>" + item.BeginDate + "</td>"+
                            "<td height='22' align='center'>" + item.Fashion + "</td>"+
                            "<td height='22' align='center'>"+ item.LinkManName +"</td>").appendTo($("#Table_fw tbody"));
                       }
                   });
                    totalRecord_fw = msg.totalCount;
                  },
           error: function() 
           {
           }, 
           complete:function()
                {
                    pageDataList1("Table_fw","#E7E7E7","#FFFFFF","#cfc","cfc");
                }//接收数据完毕
           });
}

//投诉排序
function OrderBy_fw(orderColum,orderTip)
{
    if (totalRecord_fw == 0)  
     {
        return;
     }
    var ordering = "a";    
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
    orderBy_fw = orderColum+"_"+ordering;   
    GetCust_fw();
}

//读取客户联络前8条
function GetCust_ll()
{
           action = "GetCust_ll";
         
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustTelShow.ashx',//目标地址           
           cache:false,
           data: "pageIndex_ll=1&pageCount_ll=10&action="+escape(action)+"&orderby_ll="+orderBy_ll+
           "&CustTel="+escape(Tel)+"&CompanyCD="+escape(CompanyCD)+"&EmployeeID="+escape(EmployeeID)+"&CustID="+escape(CustID),//数据
           beforeSend:function(){},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#Table_ll tbody").find("tr.newrow").remove();
                  
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append(
                            "<td height='22' align='center' title='"+ item.Title_ll +"'>" + SubValue(12,item.Title_ll) + "</td>"+
                            "<td height='22' align='center'>" + item.LinkDate + "</td>"+
                            "<td height='22' align='center'>"+ item.LinkManName +"</td>").appendTo($("#Table_ll tbody"));
                       }
                   });
                    totalRecord_ll = msg.totalCount;
                  },
           error: function() 
           {
           }, 
           complete:function()
                {
                    pageDataList1("Table_ll","#E7E7E7","#FFFFFF","#cfc","cfc");
                }//接收数据完毕
           });
}

//投诉排序
function OrderBy_ll(orderColum,orderTip)
{
    if (totalRecord_ll == 0)  
     {
        return;
     }
    var ordering = "a";    
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
    orderBy_ll = orderColum+"_"+ordering;   
    GetCust_ll();
}

function SwitchList(t)
{
    switch(t)
    {
        case "1"://客户投诉显示
        document.getElementById("div_ts").style.display = "block";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")";
        
        document.getElementById("div_fw").style.display = "none";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("div_ll").style.display = "none";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        break;        
        
        case "2"://客户服务显示
        document.getElementById("div_ts").style.display = "none";        
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        
        document.getElementById("div_fw").style.display = "block";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")";
        
        document.getElementById("div_ll").style.display = "none";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        break;
        
        case "3"://客户联络显示
        document.getElementById("div_ts").style.display = "none";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        
        document.getElementById("div_fw").style.display = "none";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        
        document.getElementById("div_ll").style.display = "block";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")";
        break;
        
        default://客户投诉显示
        break;
    }
}

//截取某长度内容的方法
function SubValue(num, value) {
    if (strlen(value) > num) {
        return value.substr(0, num) + "…";
    }
    else {
        return value;
    }
}

function aa()
{
var parentWin=window.dialogArguments;

//      var sessionSection = "";

//        //var url = document.location.href.toLowerCase();
//        var url = parentWin.parent.window.frames["Main"].location.href.toLowerCase();
//        if (url.indexOf("/(s(") != -1) {
//            var sidx = url.indexOf("/(s(") + 1;
//            var eidx = url.indexOf("))") + 2;
//            //alert(sidx+":"+eidx);
//            url = document.location.href;

//            sessionSection = url.substring(sidx, eidx);
//            sessionSection += "/";
//        }

//window.parent.window.frames["Main"].location= 'CustComplain_Info.aspx?ModuleID=2021103&SessionId='+escape(SionID);
 //opener.location = 'CustComplain_Info.aspx?ModuleID=2021103&SessionId='+escape(SionID);
 
 //window.parent.window.frames["Main"].bb();

//alert(parentWin);
//parentWin.aaa();    //调用父窗口中的方法
//parentWin.location = 'Cust_Add.aspx?ModuleID=2021103';

parentWin.parent.window.frames["Main"].location.href = 'CustComplain_Info.aspx?ModuleID=2021702';


//window.parent.location.href = SionID + 'CustComplain_Info.aspx?ModuleID=2021702&SessionId='+escape(SionID);
//parentWin.parent.window.zzz();

//window.parent.location.aaa();
//window.opener.location.aaa();
//parentWin.location.href = 'Pages/Office/CustManager/Cust_Add.aspx?ModuleID=2021103';
window.close();
//window.parent.location = 'Pages/Office/CustManager/Cust_Add.aspx?ModuleID=2021103';
//window.parent.location.href='Pages/Office/CustManager/Cust_Add.aspx?ModuleID=2021103';
//window.parent.window.frames["Main"].location = 'Pages/Office/CustManager/Cust_Add.aspx?ModuleID=2021103';
//    opener=null;
//    self.close(); 
}



