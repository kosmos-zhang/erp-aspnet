<script type="text/javascript">
 var pageCountUc = 10;//每页计数
var totalRecord = 0;
var pagerStyle = "flickr";//jPagerBar样式

var currentPageIndexUc = 1;
var action = "";//操作
var orderBy = "";//排序字段    
var ifdelUc = "0";

function SeaMeetingNo()
{
    TurnToPageUc(1);
    document.getElementById("divMeetingList").style.display= "block";
}

 function TurnToPageUc(pageIndex)
{
       currentPageIndexUc = pageIndex;          
      
       $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:  '../../../Handler/Office/AdminManager/MeetingInfoEdit.ashx',//目标地址
       cache:false,
       data: "pageIndex="+pageIndex+"&pageCount="+pageCountUc+"&action="+action+"&orderby="+orderBy+"&id=-1",//数据
       beforeSend:function(){AddPop();$("#pageDataListUc_Pager").hide();},//发送数据之前
       
       success: function(msg){
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#pageDataListUc tbody").find("tr.newrow").remove();                   
                $.each(msg.data,function(i,item){
                  if(item.ID != null && item.ID != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input onclick=\"GetMeetingByID('"+item.ID+"','"+item.MeetingNo+"')\" id='Checkbox1' value="+item.ID+"  type='radio'/>"+"</td>"+                                                                                                      
                    "<td height='22' align='center'>" + item.MeetingNo + "</td>"+
                    "<td height='22' align='center'>"+item.Title+"</td>").appendTo($("#pageDataListUc tbody"));
               });
                //页码
               ShowPageBar("pageDataListUc_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
               "<%= Request.Url.AbsolutePath %>",//[url]
                {style:pagerStyle,mark:"pageDataList1MarkUc",
                totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountUc,currentPageIndex:currentPageIndexUc,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                onclick:"TurnToPageUc({pageindex});return false;"}//[attr]
                );
              totalRecord = msg.totalCount;
             // $("#pageDataListUc_Total").html(msg.totalCount);//记录总条数
              document.getElementById("TextUc2").value=msg.totalCount;
              $("#ShowPageCountUc").val(pageCountUc);
              ShowTotalPage(msg.totalCount,pageCountUc,pageIndex,$("#pagecountUc"));
              $("#ToPageUc").val(pageIndex);
              },
       error: function() 
       {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
       }, 
       complete:function(){if(ifdelUc=="0"){hidePopup();}$("#pageDataListUc_Pager").show();IfshowUc(document.getElementById("TextUc2").value);pageDataList1("pageDataListUc","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
       });
}
function IfshowUc(count)
{
    if(count=="0")
    {
        document.getElementById("divUcpage").style.display = "none";
        document.getElementById("pagecountUc").style.display = "none";
    }
    else
    {
        document.getElementById("divUcpage").style.display = "block";
        document.getElementById("pagecountUc").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndexUc(newPageCount,newPageIndex)
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
        ifdelUc = "0";
        this.pageCountUc=parseInt(newPageCount);
        TurnToPageUc(parseInt(newPageIndex));
    }
}
//排序
function OrderByUc(orderColum,orderTip)
{
    ifdelUc = "0";
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
    TurnToPageUc(1);
}

function GetMeetingByID(ID,No)
{
    document.getElementById("txtMeetingNo").value = No;
    document.getElementById("divMeetingList").style.display= "none";
    GetInfoByMeetingNo(ID);
}

</script>





<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MeetingNoSel.ascx.cs" Inherits="UserControl_MeetingNoSel" %>

<input onclick="SeaMeetingNo();" id="txtMeetingNo"  type="text" class="tdinput" readonly />


<div id="divMeetingList" style="border: solid 1px #999999; background: #fff;
        width: 750px; z-index: 1001; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px"><!-- -->
<table width="99%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0" bgcolor="#CCCCCC">
      <tr style="background-color:#ffffff">
      <td style="width:20%">&nbsp;&nbsp;会议单号单选</td>
      <td  align="right" style="width:70%">
      <img src="../../../Images/Pic/Close.gif" title="关闭" style="CURSOR: pointer"  onclick="document.getElementById('divMeetingList').style.display='none';"/>&nbsp;&nbsp;&nbsp;&nbsp; 
      </td>
      <tr>
      <td background="../../../images/Main/Table_bg.jpg" style="width:10%"></td>
      </tr>
      </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"></td>
      </tr>
    </table>
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListUc" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择</th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderByUc('MeetingNo','oMeetingNo');return false;">会议通知单号<span id="oMeetingNo" class="orderTip"></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick" onclick="OrderByUc('Title','oTitle');return false;">会议主题<span id="oTitle" class="orderTip"></span></div></th>        
        
      </tr>
    </tbody>
    </table>
    <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pagecountUc"></div></td>
            <td height="28"  align="right"><div id="pageDataListUc_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divUcpage">
              <input name="text" type="text" id="TextUc2" style="display:none" />
              <span id="pageDataListUc_Total"></span>每页显示
              <input name="text" size="3" type="text" id="ShowPageCountUc"/>
              条  转到第
              <input name="text" type="text" id="ToPageUc" size="3" />
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndexUc($('#ShowPageCountUc').val(),$('#ToPageUc').val());" /> </div></td>
          </tr>
        </table><a name="pageDataList1MarkUc"></a>
        <input id="hfCustID" type="hidden" />
<input id="hfCustNo" type="hidden" /></td>
        </tr>
    </table>
</div>