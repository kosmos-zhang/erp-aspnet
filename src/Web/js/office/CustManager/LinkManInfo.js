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
    var UcCustName = requestobj['UcCustName'];
    var LinkManName = requestobj['LinkManName'];
    var Handset = requestobj['Handset'];
    var Important = requestobj['Important']; 
    var LinkType = requestobj['LinkType']; 
    var BeginDate = requestobj['BeginDate']; 
    var EndDate = requestobj['EndDate'];    
    
    if(typeof(CustName)!="undefined")
    { 
       $("#hfCustID").attr("value",CustName);//客户简称
       $("#txtUcCustName").attr("value",UcCustName);
       $("#txtLinkManName").attr("value",LinkManName);
       $("#txtHandset").attr("value",Handset);
       $("#seleImportant").attr("value",Important);
       $("#ddlLinkType").attr("value",LinkType);
       $("#txtDateBegin").attr("value",BeginDate);
       $("#txtDateEnd").attr("value",EndDate);
       currentPageIndex = requestobj['currentPageIndex'];
       pageCount = requestobj['pageCount'];
       //alert(requestobj['ModuleID']);
       
       SearLink(currentPageIndex);
    }
});

    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {    
         document.getElementById("checkall").checked = false;
         ifdel="0";
           currentPageIndex = pageIndex;           
           var CustNam =document.getElementById("hfCustID").value; //检索条件
           var LinkManName =document.getElementById("txtLinkManName").value;//检索条件 联系人姓名
           var Handset =document.getElementById("txtHandset").value;//检索条件 手机
           var Important =$("#seleImportant").val();//检索条件 重要程度
           var LinkType =$("#ddlLinkType").val();//检索条件 联系人类型
           var BeginDate =$("#txtDateBegin").val();//检索条件 开始日期
           var EndDate =$("#txtDateEnd").val();//检索条件 结束日期
           var WorkTel = document.getElementById("txtWorkTel").value;//检索条件电话
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/LinkManInfo.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&CustNam="+reescape(CustNam)+"&LinkManName="+reescape(LinkManName)+
           "&Handset="+reescape(Handset)+"&Important="+reescape(Important)+"&LinkType="+reescape(LinkType)+"&BeginDate="+reescape(BeginDate)+"&EndDate="+reescape(EndDate)+
           "&WorkTel="+reescape(WorkTel),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg)
           {
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    //var importantj;
                    $.each(msg.data,function(i,item)
                    {
                      if(item.id != null && item.id != "") {                     
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  onclick=IfSelectAll('Checkbox1','checkall')  value="+item.id+"  type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center'><a href='#' onclick=SelectLink('"+item.id+"')>" + item.linkmanname + "</a></td>"+
                        "<td height='22' align='center'><a href='#' onclick=SelectCust('"+item.custid+"','"+item.CustBig+"','"+item.CustNo+"','"+item.CanViewUser+"','"+item.Manager+"','"+item.Creator+"')>"+ item.CustName +"</a></td>"+
                        "<td height='22' align='center'>" + item.Appellation + "</td>"+
                        "<td height='22' align='center'>"+item.TypeName+"</td>"+
                        "<td height='22' align='center'>" + item.Important + "</td>"+                        
                         "<td height='22' align='center'>"+ item.WorkTel +"</td>"+
                        "<td height='22' align='center'>"+ item.Handset +"</td>"+
                        "<td height='22' align='center'>"+ item.QQ +"</td>"+                     
                        "<td height='22' align='center'>"+item.Birthday+"</td>").appendTo($("#pageDataList1 tbody"));}
                   });
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

//检索
function SearLink(aa)
{
    if(!CheckInput())
    {
        return;
    }
    ifdel = "0";    
    search="1";
    TurnToPage(aa);
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

function SelectLink(linkid)
{
    var CustName  = document.getElementById("hfCustID").value;
    var UcCustName = document.getElementById("txtUcCustName").value;
    var LinkManName = document.getElementById("txtLinkManName").value;
    var Handset = document.getElementById("txtHandset").value;
    var Important = document.getElementById("seleImportant").value;
    var LinkType =$("#ddlLinkType").val();//检索条件 联系人类型
    var BeginDate =$("#txtDateBegin").val();//检索条件 开始日期
    var EndDate =$("#txtDateEnd").val();//检索条件 结束日期
        
    window.location.href='LinkMan_Edit.aspx?linkid='+linkid+'&Pages=LinkMan_Info.aspx&CustName='+CustName+'&LinkManName='+LinkManName+'&Handset='+Handset+
    '&Important='+Important+'&LinkType='+LinkType+'&BeginDate='+BeginDate+'&EndDate='+EndDate+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+'&ModuleID=2021201'
    +'&UcCustName='+UcCustName;
}

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
    var CustName  = document.getElementById("hfCustID").value;
    var UcCustName = document.getElementById("txtUcCustName").value;
    var LinkManName = document.getElementById("txtLinkManName").value;
    var Handset = document.getElementById("txtHandset").value;
    var Important = document.getElementById("seleImportant").value;
    var LinkType =$("#ddlLinkType").val();//检索条件 联系人类型
    var BeginDate =$("#txtDateBegin").val();//检索条件 开始日期
    var EndDate =$("#txtDateEnd").val();//检索条件 结束日期
        
    window.location.href='Cust_Add.aspx?custid='+custid+'&custbig='+custbig+'&custno='+custno+'&Pages=LinkMan_Info.aspx&CustName='+CustName+'&UcCustName='+UcCustName+'&LinkManName='+LinkManName+'&Handset='+Handset+
    '&Important='+Important+'&LinkType='+LinkType+'&BeginDate='+BeginDate+'&EndDate='+EndDate+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+'&ModuleID=2021101';
}
function CreateLink()
{
    var CustName  = document.getElementById("hfCustID").value;
    var UcCustName = document.getElementById("txtUcCustName").value;
    var LinkManName = document.getElementById("txtLinkManName").value;
    var Handset = document.getElementById("txtHandset").value;
    var Important = document.getElementById("seleImportant").value;
    var LinkType =$("#ddlLinkType").val();//检索条件 联系人类型
    var BeginDate =$("#txtDateBegin").val();//检索条件 开始日期
    var EndDate =$("#txtDateEnd").val();//检索条件 结束日期
        
    window.location.href='LinkMan_Add.aspx?linkid=-1&Pages=LinkMan_Info.aspx&CustName='+CustName+'&LinkManName='+LinkManName+'&Handset='+Handset+'&UcCustName='+UcCustName+
    '&Important='+Important+'&LinkType='+LinkType+'&BeginDate='+BeginDate+'&EndDate='+EndDate+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+'&ModuleID=2021201';
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
   
//删除
 function DelLinkInfo()
{
    if(confirm("删除后不可恢复，确认删除吗！"))
    {
        //var ck = document.getElementsByName("Checkbox1");
        var ck = document.getElementsByName("Checkbox1");
        var ck2 = "";    
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               ck2 += ck[i].value+',';
            }
        }
        
        var linkids = ck2.substring(0,ck2.length-1);       
        x = ck2.split(',');
        if(x.length-1<=0)
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一条联系人信息后再删除！");
            return;
        }
        else
        {
            //删除
             $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/CustManager/LinkInfoDel.ashx",
                  data:"alllinkid="+reescape(linkids),
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                      AddPop();
                  },                 
                error: function() 
                {
                   showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
                }, 
                success:function(data) 
                { 
                    if(data.sta==1) 
                    { 
                        TurnToPage(1);
                        ifdel = "1";
                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除成功！");   
                        return;                 
                    }
                     if(data.sta==2) 
                    { 
                         showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","有关联数据，删除失败！");                     
                    } 
                    else
                    {
                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除失败！");
                    }                     
                } 
               });
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
    
    var txtHandset = document.getElementById('txtHandset').value;//手机号码
    var RetVal=CheckSpecialWords();
    
    var ddlLinkType = document.getElementById('ddlLinkType').value;//区域
    if(ddlLinkType == "")
    {
        isFlag = false;
        fieldText = fieldText + "联系人类型|";
	    msgText = msgText + "请首先配置联系人类型|";
    }
    
    if(txtHandset.length>0 && fucCheckNUM(txtHandset) != 1)
    {        
        isFlag = false;  
        fieldText = fieldText + "手机号|";     
	    msgText = msgText + "手机号码必须为数字|";
    }
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