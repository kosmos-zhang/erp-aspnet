    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
    var ifdel="0";//是否删除
    
$(document).ready(function()
{
    requestobj = GetRequest(); 
    var CustName = requestobj['CustName'];
    var CustID = requestobj['CustID'];
    var ServeType = requestobj['ServeType'];
    var Fashion = requestobj['Fashion'];
    var ServiceDateBegin = requestobj['ServiceDateBegin'];
    var ServiceDateEnd = requestobj['ServiceDateEnd'];
    var Title = requestobj['Title'];
    var CustLinkMan = requestobj['CustLinkMan'];
    var Executant = requestobj['Executant'];
    
    if(typeof(CustName)!="undefined")
    { 
       $("#txtUcCustName").attr("value",CustName.Trim());//客户简称
       $("#hfCustID").attr("value",CustID.Trim());
       $("#ddlServeType").attr("value",ServeType.Trim());
       $("#ddlFashion").attr("value",Fashion.Trim());
       $("#txtServiceDateBegin").attr("value",ServiceDateBegin.Trim());
       $("#txtServiceDateEnd").attr("value",ServiceDateEnd.Trim());
       $("#txtTitle").attr("value",Title.Trim());
       $("#txtCustLinkMan").attr("value",CustLinkMan.Trim());  
       $("#txtExecutant").attr("value",Executant.Trim());
       currentPageIndex = requestobj['currentPageIndex'].Trim();
       pageCount = requestobj['pageCount'].Trim();
       SearchServiceData(currentPageIndex);
    }
});

 function TurnToPage(pageIndex)
{
document.getElementById("checkall").checked = false;
       ifdel="0";
       currentPageIndex = pageIndex;
       //var CustName =document.getElementById("txtCustName").value; //检索条件 客户简称
       var CustName =document.getElementById("hfCustID").value;
       var ServeType =document.getElementById("ddlServeType").value;//服务类型
       var Fashion =document.getElementById("ddlFashion").value;//服务方式
       var ServiceDateBegin = document.getElementById("txtServiceDateBegin").value;//服务开始时间
       var ServiceDateEnd = document.getElementById("txtServiceDateEnd").value;//服务结束时间 
       var Title = document.getElementById("txtTitle").value;//服务主题
       var CustLinkMan = document.getElementById("txtCustLinkMan").value;//客户联系人
       var Executant = document.getElementById("txtExecutant").value;//执行人
       
       var ServerPersonW = "";
       try{
          ServerPersonW =   document.getElementById("ServerPerson").value;
       }catch(ee){}
      
      var ServerTypeW = "";
       try{
          ServerTypeW =   document.getElementById("ServerType").value;
       }catch(ee){}
       
       $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/ServiceInfo.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+
                "&CustName="+reescape(CustName)+
                "&ServeType="+reescape(ServeType)+
                "&Fashion="+reescape(Fashion)+
                "&ServiceDateBegin="+reescape(ServiceDateBegin)+
                "&ServiceDateEnd="+reescape(ServiceDateEnd)+
                "&Title="+reescape(Title)+
                "&CustLinkMan="+reescape(CustLinkMan)+
                "&Executant="+reescape(Executant)+
                "&ServerTypeW="+reescape(ServerTypeW)+
                "&ServerPerson="+reescape(ServerPersonW),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var important;  
                   
                    if(ServerTypeW !="" || ServerPersonW !="" ){

                     $.each(msg.data,function(i,item){
                      if(item.id != null && item.id != "")
                        $("<tr class='newrow'></tr>").append(
                        "<td height='22' align='center'> " + item.ServeNo + "</td>"+
                        "<td height='22' align='center'>" + item.title + "</td>"+
                        "<td height='22' align='center'>" + item.BeginDate + "</td>"+
                        "<td height='22' align='center'> " + item.custnam + "</td>"+
                        "<td height='22' align='center'>" + item.ServeType + "</td>"+
                        "<td height='22' align='center'>" + item.Fashion + "</td>"+
                        "<td height='22' align='center'>"+ item.EmployeeName +"</td>"+
                        "<td height='22' align='center'>"+ item.LinkManName +"</td>").appendTo($("#pageDataList1 tbody"));
                      });
                    }else{
                    $.each(msg.data,function(i,item){
                      if(item.id != null && item.id != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  onclick=IfSelectAll('Checkbox1','checkall')  value="+item.id+"  type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center'><a href='#' onclick=SelectService('"+item.id+"')>" + item.ServeNo + "</a></td>"+
                        "<td height='22' align='center'>" + item.title + "</td>"+
                        "<td height='22' align='center'>" + item.BeginDate + "</td>"+
                        "<td height='22' align='center'><a href='#' onclick=SelectCust('"+item.custid+"','"+item.CustNo+"','"+item.CustBig+"','"+item.CanViewUser+"','"+item.Manager+"','"+item.Creator+"')>" + item.custnam + "</a></td>"+
                        "<td height='22' align='center'>" + item.ServeType + "</td>"+
                        "<td height='22' align='center'>" + item.Fashion + "</td>"+
                        "<td height='22' align='center'>"+ item.EmployeeName +"</td>"+
                        "<td height='22' align='center'>"+ item.LinkManName +"</td>").appendTo($("#pageDataList1 tbody"));
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
                  
                  document.getElementById("Text2").value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(ifdel=="0"){hidePopup();}$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
       
}

//检索
function SearchServiceData(aa)
{
    if(!CheckInput())
    {
        return;
    }
    ifdel = "0";
    search="1";
    TurnToPage(aa);
}
function CreateService()
{
    //window.location.href='CustService_Add.aspx';
    SelectService(-1);
}
//根据客户ID转到客户信息查看
function SelectService(serviceid)
{
    var CustName  = document.getElementById("txtUcCustName").value;
    var CustID  = document.getElementById("hfCustID").value;
    var ServeType = document.getElementById("ddlServeType").value;
    var Fashion = document.getElementById("ddlFashion").value;
    var ServiceDateBegin = document.getElementById("txtServiceDateBegin").value;
    var ServiceDateEnd = document.getElementById("txtServiceDateEnd").value;
    var Title = document.getElementById("txtTitle").value;
    var CustLinkMan = document.getElementById("txtCustLinkMan").value;
    var Executant = document.getElementById("txtExecutant").value;
    
    window.location.href='CustService_Add.aspx?serviceid='+escape(serviceid)+'&Pages=CustService_Info.aspx&CustName='+reescape(CustName)+'&ServeType='+reescape(ServeType)+
      '&Fashion='+reescape(Fashion)+'&ServiceDateBegin='+reescape(ServiceDateBegin)+'&ServiceDateEnd='+reescape(ServiceDateEnd)+'&Title='+reescape(Title)+'&CustID='+CustID+
      '&CustLinkMan='+reescape(CustLinkMan)+'&Executant='+reescape(Executant)+'&currentPageIndex='+reescape(currentPageIndex)+'&pageCount='+reescape(pageCount)+'&ModuleID=2021601';
}
//根据客户ID转到客户信息查看
function SelectCust(custid,custno,custbig,canuser,manager,creator)
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
    
    var CustName  = document.getElementById("txtUcCustName").value;
    var CustID = document.getElementById("hfCustID").value;
    var ServeType = document.getElementById("ddlServeType").value;
    var Fashion = document.getElementById("ddlFashion").value;
    var ServiceDateBegin = document.getElementById("txtServiceDateBegin").value;
    var ServiceDateEnd = document.getElementById("txtServiceDateEnd").value;
    var Title = document.getElementById("txtTitle").value;
    var CustLinkMan = document.getElementById("txtCustLinkMan").value;
    var Executant = document.getElementById("txtExecutant").value;
    
    window.location.href='Cust_Add.aspx?custid='+reescape(custid)+'&custno='+custno+'&custbig='+custbig+'&Pages=CustService_Info.aspx&CustName='+reescape(CustName)+'&ServeType='+reescape(ServeType)+
    '&Fashion='+reescape(Fashion)+'&ServiceDateBegin='+reescape(ServiceDateBegin)+'&ServiceDateEnd='+reescape(ServiceDateEnd)+'&Title='+reescape(Title)+'&CustID='+CustID+
    '&CustLinkMan='+reescape(CustLinkMan)+'&Executant='+reescape(Executant)+'&currentPageIndex='+reescape(currentPageIndex)+'&pageCount='+reescape(pageCount)+'&ModuleID=2021103';
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

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount,newPageIndex)
{
if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
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
        ifdel = "0";
        this.pageCount=parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
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
    TurnToPage(1);
}

//是否可以导出
function IfExp() {
    if (totalRecord == 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先检索出数据，才可以导出！");
        return false;
    }
    
    document.getElementById("hiddCustID").value = $("#hfCustID").val(); //客户ID
    return true;
}

//主表单验证
function CheckInput()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
   // var CustName =document.getElementById("txtCustName").value; //检索条件 客户简称
    var ServiceDateBegin = document.getElementById("txtServiceDateBegin").value;//服务开始时间
    var ServiceDateEnd = document.getElementById("txtServiceDateEnd").value;//结束时间 
    var RetVal=CheckSpecialWords();

    var ddlServeType = document.getElementById('ddlServeType').value;//服务类型
    if(ddlServeType == "")
    {
        isFlag = false;
        fieldText = fieldText + "服务类型|";
	    msgText = msgText + "请首先配置服务类型|";
    }
    if(ServiceDateBegin.length>0 && isDate(ServiceDateBegin) == false)
    {
        isFlag = false;
        fieldText = fieldText + "开始时间|";
	    msgText = msgText + "开始时间格式不正确|";
    }
    if(ServiceDateEnd.length>0 && isDate(ServiceDateEnd) == false)
    {
        isFlag = false;
        fieldText = fieldText + "结束日期|";
	    msgText = msgText + "结束时间格式不正确|";
    }
     if(!compareDate(ServiceDateBegin,ServiceDateEnd))
    {
        isFlag = false;
        fieldText = fieldText + "开始日期|";
	    msgText = msgText + "开始日期不能大于结束日期|";
    }
    if(RetVal!="")
    {
        isFlag = false;
        fieldText = fieldText + RetVal+"|";
	    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    if(!isFlag)
    {
        //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}

//删除客户服务
function DelServiceInfo()
{
    if(confirm("删除后不可恢复，确认删除吗！"))
    {
        var ck = document.getElementsByName("Checkbox1");
        var ck2 = "";    
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               ck2 += ck[i].value+',';
            }
        }
        
        var custids = ck2.substring(0,ck2.length-1);
        x = ck2.split(',');
        if(x.length-1<=0)
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一条客户信息后再删除！");
            return;
        }
        else
        {
            //删除
             $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/CustManager/ServiceDel.ashx",
                  data:"allcustid="+reescape(custids),
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                      AddPop();
                  }, 
                //complete :function(){hidePopup();},
                error: function() 
                { 
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
                }, 
                success:function(data) 
                { 
                    if(data.sta==1) 
                    { 
                        //成功
                        TurnToPage(1);
                        ifdel = "1";
                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除成功！");                    
                    }
                    else
                    {
                        //popMsgObj.Show('删除失败！');
                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除失败！");
                    } 
                } 
               });
        }
    }
}