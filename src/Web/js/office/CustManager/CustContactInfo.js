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
    var CustLinkMan = requestobj['CustLinkMan'];
    var LinkDateBegin = requestobj['LinkDateBegin'];
    var LinkDateEnd = requestobj['LinkDateEnd'];    
    
    if(typeof(CustName)!="undefined")
    { 
       $("#txtUcCustName").attr("value",CustName);//客户简称
        $("#hfCustID").attr("value",CustID);
       $("#txtCustLinkMan").attr("value",CustLinkMan);
       $("#txtLinkDateBegin").attr("value",LinkDateBegin);
       $("#txtLinkDateEnd").attr("value",LinkDateEnd);
       currentPageIndex = requestobj['currentPageIndex'];
       pageCount = requestobj['pageCount'];
       
       SearchContactData(currentPageIndex);
    }
});

 function TurnToPage(pageIndex)
{   
    document.getElementById("checkall").checked = false;
       ifdel="0";
       currentPageIndex = pageIndex;
       //var CustName =document.getElementById("txtCustName").value; //检索条件
       var CustName=document.getElementById("hfCustID").value; //检索条件      

       var CustLinkMan =document.getElementById("txtCustLinkMan").value;//被联络人
       var LinkDateBegin = document.getElementById("txtLinkDateBegin").value;//联络开始时间
       var LinkDateEnd = document.getElementById("txtLinkDateEnd").value;//联络结束时间 
       
       var ContactReason = "";

       try{
          ContactReason =   document.getElementById("reasonid").value;
       }catch(ee){}
        
       $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/ContactInfo.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&CustName="+reescape(CustName)
                 +"&CustLinkMan="+reescape(CustLinkMan)+"&LinkDateBegin="+reescape(LinkDateBegin)+"&LinkDateEnd="+reescape(LinkDateEnd)+"&ReasonId="+ContactReason,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var important;
                    
                   if( ContactReason != "" ){
                   document.getElementById("thcheck").style.display ="none";
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append(
                         "<td height='22' align='center'>" + item.ContactNo + "</td>"+
                         "<td height='22' align='center'>" + item.Title + "</td>"+
                        "<td height='22' align='center'>" + item.CustNam + " </td>"+                        
                        "<td height='22' align='center'>" + item.Linker + "</td>"+
                         "<td height='22' align='center'>"+ item.LinkDate +"</td>"+
                        "<td height='22' align='center'>"+ item.LinkManName +"</td>").appendTo($("#pageDataList1 tbody"));
                      });
                   }else{
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  onclick=IfSelectAll('Checkbox1','checkall')  value="+item.ID+"  type='checkbox'/>"+"</td>"+
                         "<td height='22' align='center'><a href='#' onclick=SelectContact('"+item.ID+"')>" + item.ContactNo + "</a></td>"+
                         "<td height='22' align='center'>" + item.Title + "</td>"+
                        "<td height='22' align='center'><a href='#' onclick=SelectCust('"+item.custid+"','"+item.CustBig+"','"+item.CustNo+"','"+item.CanViewUser+"','"+item.Manager+"','"+item.Creator+"')>" + item.CustNam + "</a></td>"+                        
                        "<td height='22' align='center'>" + item.Linker + "</td>"+
                         "<td height='22' align='center'>"+ item.LinkDate +"</td>"+
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
function SearchContactData(aa)
{
    if(!CheckInput())
    {
        return;
    }
    ifdel = "0";
    search="1";
    TurnToPage(aa);
} 
function CreateLink()
{
    SelectContact(-1);
}

//根据联络ID转到联络信息查看
function SelectContact(contactid)
{
    var CustID =document.getElementById("hfCustID").value; //检索条件
    var CustName = document.getElementById("txtUcCustName").value;
    //var CustClass =document.getElementById("CustClassHidden").value;//客户类型
    var CustLinkMan =document.getElementById("txtCustLinkMan").value;//bei联络人
    var LinkDateBegin = document.getElementById("txtLinkDateBegin").value;//联络开始时间
    var LinkDateEnd = document.getElementById("txtLinkDateEnd").value;//联络结束时间 
    
    window.location.href='CustContactHistory_Add.aspx?contactid='+contactid+'&Pages=CustContact_Info.aspx&CustName='+CustName+'&CustLinkMan='
    +CustLinkMan+'&LinkDateBegin='+LinkDateBegin+'&LinkDateEnd='+LinkDateEnd+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+'&ModuleID=2021301'
    +'&CustID='+CustID;
}
//根据客户ID转到客户信息查看
function SelectCust(custid,custbig,custno,canuser,manager,creator)
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
    
    var CustName =document.getElementById("txtUcCustName").value; //检索条件 客户简称
    var CustID =document.getElementById("hfCustID").value; 
    //var CustClass =document.getElementById("CustClassHidden").value;//客户类型    
    var CustLinkMan =document.getElementById("txtCustLinkMan").value;//bei联络人
    var LinkDateBegin = document.getElementById("txtLinkDateBegin").value;//联络开始时间
    var LinkDateEnd = document.getElementById("txtLinkDateEnd").value;//联络结束时间 
    
    window.location.href='Cust_Add.aspx?custid='+custid+'&custbig='+custbig+'&custno='+custno+'&Pages=CustContact_Info.aspx&CustName='+CustName+'&CustLinkMan='+CustLinkMan+
    '&LinkDateBegin='+LinkDateBegin+'&LinkDateEnd='+LinkDateEnd+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+'&ModuleID=2021103'+'&CustID='+CustID;
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

//删除客户联络
function DelContactInfo()
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
                  url: "../../../Handler/Office/CustManager/ContactInfoDel.ashx",
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
   
    var LinkDateBegin = document.getElementById("txtLinkDateBegin").value;//联络开始时间
    var LinkDateEnd = document.getElementById("txtLinkDateEnd").value;//联络结束时间 
            
    if(LinkDateBegin.length>0 && isDate(LinkDateBegin) == false)
    {
        isFlag = false;
        fieldText = fieldText + "联络开始时间|";
	    msgText = msgText + "联络开始时间格式不正确|";
    }
    if(LinkDateEnd.length>0 && isDate(LinkDateEnd) == false)
    {
        isFlag = false;
        fieldText = fieldText + "联络结束时间|";
	    msgText = msgText + "联络结束时间格式不正确|";
    }
    if(!compareDate(LinkDateBegin,LinkDateEnd))
    {
        isFlag = false;
        fieldText = fieldText + "开始日期|";
	    msgText = msgText + "开始日期不能大于结束日期|";
    }
    
    if(!isFlag)
    {
        //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}

