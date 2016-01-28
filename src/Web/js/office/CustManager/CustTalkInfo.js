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
    var TalkType = requestobj['TalkType'];
    var Priority = requestobj['Priority'];
    var TalkBegin = requestobj['TalkBegin'];
    var TalkEnd = requestobj['TalkEnd'];
    var Title = requestobj['Title'];
    var Status = requestobj['Status'];
    
    if(typeof(CustName)!="undefined")
    { 
       $("#txtUcCustName").attr("value",CustName);//客户简称
       $("#hfCustID").attr("value",CustID);//客户简称
       $("#ddlTalkType").attr("value",TalkType);
       $("#selePriority").attr("value",Priority);
       $("#txtTalkBegin").attr("value",TalkBegin);
       $("#txtTalkEnd").attr("value",TalkEnd);
       $("#txtTitle").attr("value",Title);
       $("#seleStatus").attr("value",Status);
       currentPageIndex =  requestobj['currentPageIndex'];
       pageCount =  requestobj['pageCount'];
       
       SearchTalkData(currentPageIndex);
    }
});

function TurnToPage(pageIndex)
{   
document.getElementById("checkall").checked = false;

        ifdel="0";        
       currentPageIndex = pageIndex;
       var CustName =document.getElementById("hfCustID").value; //检索条件 客户简称
       var TalkType =document.getElementById("ddlTalkType").value; //检索条件 洽谈类型       
       var Priority =document.getElementById("selePriority").value;//优先级       
       var TalkBegin = document.getElementById("txtTalkBegin").value;//开始时间
       var TalkEnd = document.getElementById("txtTalkEnd").value;//结束时间       
       var Title = document.getElementById("txtTitle").value;//主题       
       var Status = document.getElementById("seleStatus").value;//状态
       
       $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/TalkInfo.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+
                "&CustName="+reescape(CustName)+
                "&TalkType="+reescape(TalkType)+
                "&Priority="+reescape(Priority)+
                "&TalkBegin="+reescape(TalkBegin)+
                "&TalkEnd="+reescape(TalkEnd)+
                "&Title="+reescape(Title)+
                "&Status="+reescape(Status),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var Priority,Status;
                    $.each(msg.data,function(i,item){
                      if(item.id != null && item.id != "")
                      {
                          switch(item.Priority)
                          {
                                case "1":
                                Priority = "暂缓";
                                break;
                                case "2":
                                Priority = "普通";
                                break;
                                case "3":
                                Priority = "尽快";
                                break;
                                case "4":
                                Priority = "立即";
                                break;
                                default:
                                Priority = "";
                          }
                          switch(item.Status)
                          {
                                case "1":
                                Status = "未开始";
                                break;
                                case "2":
                                Status = "进行中";
                                break;
                                case "3":
                                Status = "已完成";
                                break;
                                default:
                                Status = "";
                          }
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  onclick=IfSelectAll('Checkbox1','checkall')  value="+item.id+"  type='checkbox'/>"+"</td>"+
                             "<td height='22' align='center'><a href='#' onclick=SelectTalk('"+item.id+"')>" + item.TalkNo + "</a></td>"+  
                             "<td height='22' align='center'>" + item.title + "</td>"+  
                              "<td height='22' align='center'><a href='#' onclick=SelectCust('"+item.custid+"','"+item.CustNo+"','"+item.CustBig+"','"+item.CanViewUser+"','"+item.Manager+"','"+item.CustCreator+"')>" + item.custnam + "</a></td>"+                       
                            "<td height='22' align='center'>" + item.linkmanname + "</td>"+
                            "<td height='22' align='center'>" + Priority + "</td>"+
                            "<td height='22' align='center'>" + item.typename + "</td>"+
                            "<td height='22' align='center'>" + item.CompleteDate + "</td>"+
                            "<td height='22' align='center'>" + item.LinkerName + "</td>"+
                            "<td height='22' align='center'>" + Status + "</td>"+
                            "<td height='22' align='center'>" + item.EmployeeName + "</td>"+
                            "<td height='22' align='center'>"+ item.CreatedDate +"</td>").appendTo($("#pageDataList1 tbody"));
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
//检索
function SearchTalkData(aa)
{
    if(!CheckInput())
    {
        return;
    }
    ifdel = "0";
    search="1";
    TurnToPage(aa);
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
        
    var TalkBegin = document.getElementById("txtTalkBegin").value;//开始时间
    var TalkEnd = document.getElementById("txtTalkEnd").value;//结束时间 
    var RetVal=CheckSpecialWords();
    
    if(TalkBegin.length>0 && isDate(TalkBegin) == false)
    {
        isFlag = false;
        fieldText = fieldText + "开始时间|";
	    msgText = msgText + "开始时间格式不正确|";
    }
    if(TalkEnd.length>0 && isDate(TalkEnd) == false)
    {
        isFlag = false;
        fieldText = fieldText + "结束时间|";
	    msgText = msgText + "结束时间格式不正确|";
    }
    if(!compareDate(TalkBegin,TalkEnd))
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
        popMsgObj.Show(fieldText,msgText);
        //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
    }
    return isFlag;
}
function CreateTalk()
{
     //window.location.href='CustTalk_Add.aspx';     
     SelectTalk(-1);
}
//转到客户洽谈信息查看
function SelectTalk(Talkid)
{          
    var CustName  = document.getElementById("txtUcCustName").value;
    var CustID  = document.getElementById("hfCustID").value;
    var TalkType = document.getElementById("ddlTalkType").value;    
    var Priority = document.getElementById("selePriority").value;    
    var TalkBegin = document.getElementById("txtTalkBegin").value;
    var TalkEnd = document.getElementById("txtTalkEnd").value;    
    var Title = document.getElementById("txtTitle").value;
    var Status = document.getElementById("seleStatus").value;    
    
    window.location.href='CustTalk_Add.aspx?Talkid='+Talkid+'&Pages=CustTalk_Info.aspx&CustName='+CustName+'&TalkType='+TalkType+'&Priority='+Priority+
    '&TalkBegin='+TalkBegin+'&TalkEnd='+TalkEnd+'&Title='+Title+'&Status='+Status+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+
    '&CustID='+CustID+'&ModuleID=2021401';
}
//根据客户ID转到客户信息查看
function SelectCust(cid,custno,custbig,canuser,manager,creator)
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
    var CustID  = document.getElementById("hfCustID").value;  
    var TalkType = document.getElementById("ddlTalkType").value;    
    var Priority = document.getElementById("selePriority").value;    
    var TalkBegin = document.getElementById("txtTalkBegin").value;
    var TalkEnd = document.getElementById("txtTalkEnd").value;    
    var Title = document.getElementById("txtTitle").value;
    var Status = document.getElementById("seleStatus").value;    
    
    window.location.href='Cust_Add.aspx?custid='+cid+'&custbig='+custbig+'&custno'+custno+'&Pages=CustTalk_Info.aspx&CustName='+CustName+'&TalkType='+TalkType+'&Priority='+Priority+'&TalkBegin='+
        TalkBegin+'&TalkEnd='+TalkEnd+'&Title='+Title+'&Status='+Status+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+'&ModuleID=2021103'+'&CustID='+CustID;
}
//删除
 function DelTalkInfo()
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
        
        var linkids = ck2.substring(0,ck2.length-1);        
        x = ck2.split(',');
        if(x.length-1<=0)
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一条信息后再删除！");
            return;
        }
        else
        {
            //删除
             $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/CustManager/TalkInfoDel.ashx",
                  data:"alltalkid="+reescape(linkids),
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
                        //成功
                        TurnToPage(1);
                        ifdel = "1";
                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除成功！");                    
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