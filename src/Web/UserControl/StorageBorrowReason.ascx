<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StorageBorrowReason.ascx.cs"
    Inherits="UserControl_StorageBorrowReason" %>
<div id="divModuleReasonInfo">
    <!--提示信息弹出详情start-->
    <a name="pageReasonInfoMark"></a>
    <div id="divGetReasonInfo" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 700px; z-index: 1001; position: absolute; display: none; top: 20%; left: 60%;
        margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <img alt="关闭" id="btn_Close_a9" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: pointer;'
                        onclick='closeReasonInfodiv();' />
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="divReasonInfoList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="GetReasonOrderBy('EmployeeNo','oSellEmp');return false;">
                            原因ID<span id="oSellEmp" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="GetReasonOrderBy('EmployeeName','oSEmp1');return false;">
                            原因标题<span id="oSEmp1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="GetReasonOrderBy('DeptName','oSEmp2');return false;">
                            原因内容<span id="oSEmp2" class="orderTip"></span></div>
                    </th>
                </tr>
            </tbody>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
            class="PageList">
            <tr>
                <td height="28" background="../../../images/Main/PageList_bg.jpg">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                        <tr>
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="28%">
                                <div id="pageReasonInfocount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="getReasonlist_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divReasonInfoPage">
                                    <span id="pageReasonInfo_Total"></span>每页显示
                                    <input name="text" type="text" style="width: 30px;" id="ShowReasonInfoPageCount" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 30px;" id="ToReasonInfoPage" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeReasonInfoPageCountIndex($('#ShowReasonInfoPageCount').val(),$('#ToReasonInfoPage').val());" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <!--提示信息弹出详情end-->
</div>

<script type="text/javascript">
var popReasonInfoObj=new Object();
popReasonInfoObj.InputObj = null;

popReasonInfoObj.ShowList=function(objInput,para)
{
    popReasonInfoObj.InputObj= objInput;
    document.getElementById('divGetReasonInfo').style.display='block';
    ReasonInfoTurnToPage(currentSellEmpPageIndex,objInput,para);
}
  
    var pageReasonInfocount = 10;//每页计数
    var totalSellEmpRecord = 0;
    var pagerSellEmpStyle = "flickr";//jPagerBar样式
    
    var currentSellEmpPageIndex = 1;
    var actionSellEmp = "";//操作
    var orderSellEmpBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function ReasonInfoTurnToPage(pageIndex,objInput,para)
    {
           currentSellEmpPageIndex = pageIndex;     
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageBorrow_Add.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageReasonInfocount="+pageReasonInfocount+"&orderby="+orderSellEmpBy+"&"+para,//数据
           beforeSend:function(){$("#getReasonlist_Pager").hide();},//发送数据之前
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#divReasonInfoList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                         {
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioEmp_\" id=\"radioEmp_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"fnSelectReasonInfo("+item.ID+",'"+item.ProdNo+"','"+item.ReasonName+"','"+item.Specification+"','"+item.ReasonCount+"','"+item.CodeName+"','"+item.UnitID+"','"+item.StandardCost+"','"+item.ReasonID+"','"+objInput+"');\" />"+"</td>"+
                         "<td height='22' align='center'>"+ item.ProdNo+"</td>"+
                         "<td height='22' align='center'>"+ item.ReasonName+"</td>"+
                         "<td height='22' align='center'>"+item.Specification+"</td>"+
                        "<td height='22' align='center'>"+item.CodeName+"</td>"+
                         "<td height='22' align='center'>"+item.ReasonCount+"</td>"+
                         "<td height='22' align='center'>"+item.StandardCost+"</td>").appendTo($("#divReasonInfoList tbody"));}
                   });
                    //页码
                    ShowPageBar("getReasonlist_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerSellEmpStyle,mark:"pageReasonInfoMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageReasonInfocount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"ReasonInfoTurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalSellEmpRecord = msg.totalCount;
                  $("#ShowReasonInfoPageCount").val(pageReasonInfocount);
                  ShowTotalPage(msg.totalCount,pageReasonInfocount,pageIndex,$("#pageReasonInfocount"));
                  $("#ToReasonInfoPage").val(pageIndex);
                  },
           error: function(msg) {alert(msg);}, 
           complete:function(){$("#getReasonlist_Pager").show();pageReasonInfoDataList1("divReasonInfoList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageReasonInfoDataList1(o,a,b,c,d)
{
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
function ChangeReasonInfoPageCountIndex(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalSellEmpRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pageReasonInfocount=parseInt(newPageCount);
        ReasonInfoTurnToPage(parseInt(newPageIndex));
    }
}
//排序
function GetReasonOrderBy(orderColum,orderTip)
{
    var ordering = "d";
    var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
        ordering = "a";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↑");
    }
    else
    {
        
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderSellEmpBy = orderColum+"_"+ordering;
    ReasonInfoTurnToPage(1);
}
   
function closeReasonInfodiv()
{
    document.getElementById("divGetReasonInfo").style.display="none";
}

function fnSelectReasonInfo(ID,ProdNo,ReasonName,Specification,ReasonCount,CodeName,UnitID,StandardCost,ReasonID,rowid)
{
   document.getElementById("tboxReasonID_"+rowid).value=ID;
   document.getElementById("tboxReasonNo_list_"+rowid).value=ProdNo;
   document.getElementById("tboxReasonNo_list_"+rowid).title=ReasonID;
   document.getElementById("tboxProcutName_list_"+rowid).value=ReasonName;
  document.getElementById("tboxStandard_list_"+rowid).value=Specification;
  document.getElementById("tboxUnit_list_"+rowid).value=CodeName;
   document.getElementById("tboxUnitID_"+rowid).value=UnitID;
    document.getElementById("tboxStorageQuantity_list_"+rowid).value=ReasonCount;
      document.getElementById("tboxPrice_list_"+rowid).value=StandardCost;
      closeReasonInfodiv();
}
</script>

