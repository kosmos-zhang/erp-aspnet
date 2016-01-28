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
    var LoveType = requestobj['LoveType'];
    var LoveBegin = requestobj['LoveBegin'];
    var LoveEnd = requestobj['LoveBegin'];
    var Title = requestobj['Title'];
    var CustLinkMan = requestobj['CustLinkMan'];
    
    if(typeof(CustName)!="undefined")
    { 
       $("#txtUcCustName").attr("value",CustName);//客户简称
       $("#hfCustID").attr("value",CustID);
       $("#ddlLoveType").attr("value",LoveType);
       $("#txtCustLinkMan").attr("value",CustLinkMan);
       $("#txtLoveBegin").attr("value",LoveBegin);
       $("#txtLoveEnd").attr("value",LoveEnd);
       $("#txtTitle").attr("value",Title);
       currentPageIndex = requestobj['currentPageIndex'];
       pageCount = requestobj['pageCount'];
       
       SearchLoveData(currentPageIndex);
    }
});
    
function TurnToPage(pageIndex)
{   
document.getElementById("checkall").checked = false;
    ifdel="0";
       currentPageIndex = pageIndex;
       var CustName =document.getElementById("hfCustID").value; //检索条件 客户简称
       var LoveType =document.getElementById("ddlLoveType").value; //检索条件 关怀类型
       var CustLinkMan = document.getElementById("txtCustLinkMan").value;//客户联系人
       var LoveBegin = document.getElementById("txtLoveBegin").value;//开始时间
       var LoveEnd = document.getElementById("txtLoveEnd").value;//结束时间
       var Title = document.getElementById("txtTitle").value;//主题
       
       $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/LoveInfo.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+
                "&CustName="+reescape(CustName)+
                "&LoveType="+reescape(LoveType)+
                "&CustLinkMan="+reescape(CustLinkMan)+
                "&LoveBegin="+reescape(LoveBegin)+
                "&LoveEnd="+reescape(LoveEnd)+
                "&Title="+reescape(Title),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var Critical,State;
                    $.each(msg.data,function(i,item){
                      if(item.id != null && item.id != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  onclick=IfSelectAll('Checkbox1','checkall')   value="+item.id+"  type='checkbox'/>"+"</td>"+
                         "<td height='22' align='center'><a href='#' onclick=SelectLove('"+item.id+"')>" + item.LoveNo + "</a></td>"+
                         "<td height='22' align='center'>" + item.Title + "</td>"+ 
                          "<td height='22' align='center'><a href='#' onclick=SelectCust('"+item.CustID+"','"+item.CustNo+"','"+item.CustBig+"','"+item.CanViewUser+"','"+item.Manager+"','"+item.Creator+"')>" + item.CustNam + "</a></td>"+
                          "<td height='22' align='center'>" + item.LinkManName + "</td>"+
                          "<td height='22' align='center'>" + item.LoveDate + "</td>"+
                          "<td height='22' align='center'>" + item.LoveType + "</td>"+
                        
                        "<td height='22' align='center'>"+ item.EmployeeName +"</td>").appendTo($("#pageDataList1 tbody"));
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
//检索
function SearchLoveData(aa)
{
    if(!CheckInput())
    {
        return;
    }
    ifdel = "0";
    search="1";
    TurnToPage(aa);
}
function CreateLove()
{
    //window.location.href='CustLove_Add.aspx';
    SelectLove(-1);
}
//转到客户投诉信息查看
function SelectLove(Loveid)
{
    var CustName  = document.getElementById("txtUcCustName").value;
    var CustID = document.getElementById("hfCustID").value;
    var LoveType = document.getElementById("ddlLoveType").value;
    var CustLinkMan = document.getElementById("txtCustLinkMan").value;
    var LoveBegin = document.getElementById("txtLoveBegin").value;
    var LoveEnd = document.getElementById("txtLoveEnd").value;
    var Title = document.getElementById("txtTitle").value;
   
    window.location.href='CustLove_Add.aspx?Loveid='+Loveid+'&Pages=CustLove_Info.aspx&CustName='+CustName+'&LoveType='+LoveType+'&LoveBegin='+LoveBegin+
        '&LoveEnd='+LoveEnd+'&Title='+Title+'&CustLinkMan='+CustLinkMan+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+'&CustID='+CustID+'&ModuleID=2021501';
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
    var LoveType = document.getElementById("ddlLoveType").value;
    var CustLinkMan = document.getElementById("txtCustLinkMan").value;
    var LoveBegin = document.getElementById("txtLoveBegin").value;
    var LoveEnd = document.getElementById("txtLoveEnd").value;
    var Title = document.getElementById("txtTitle").value;
   
    window.location.href='Cust_Add.aspx?custid='+custid+'&custno='+custno+'&custbig='+custbig+'&Pages=CustLove_Info.aspx&CustName='+CustName+'&LoveType='+LoveType+'&LoveBegin='+LoveBegin+
        '&LoveEnd='+LoveEnd+'&Title='+Title+'&CustLinkMan='+CustLinkMan+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+'&ModuleID=2021103'+'&CustID='+CustID;
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
//删除客户关怀
function DelLoveInfo()
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
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一条投诉信息后再删除！");
            return;
        }
        else
        {
            //删除
             $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/CustManager/LoveDel.ashx",
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
    
    //var CustName =document.getElementById("txtCustName").value; //检索条件 客户简称
    var LoveBegin = document.getElementById("txtLoveBegin").value;//开始时间
    var LoveEnd = document.getElementById("txtLoveEnd").value;//结束时间 
    var RetVal=CheckSpecialWords();

    var ddlLoveType = document.getElementById('ddlLoveType').value;//关怀类型
    if(ddlLoveType == "")
    {
        isFlag = false;
        fieldText = fieldText + "关怀类型|";
	    msgText = msgText + "请首先配置关怀类型|";
    }
    
    if(LoveBegin.length>0 && isDate(LoveBegin) == false)
    {
        isFlag = false;
        fieldText = fieldText + "开始时间|";
	    msgText = msgText + "开始时间格式不正确|";
    }
    if(LoveEnd.length>0 && isDate(LoveEnd) == false)
    {
        isFlag = false;
        fieldText = fieldText + "结束时间|";
	    msgText = msgText + "结束时间格式不正确|";
    }
    if(!compareDate(LoveBegin,LoveEnd))
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