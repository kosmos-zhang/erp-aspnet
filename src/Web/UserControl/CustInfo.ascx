<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustInfo.ascx.cs" Inherits="UserControl_CustInfo" %>

<div id="layout">
    <!--提示信息弹出详情start-->
    <a name="pageEmpDataList1Mark"></a>
    <div id="divCustInfo" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 700px; z-index: 1001; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closeCustdiv()" style="text-align: right; cursor: pointer">关闭</a>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1Provider"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div >
                            供应商ID<span id="oSellEmp" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div >
                            供应商编号<span id="oSEmp1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div >
                            供应商名称<span id="oSEmp2" class="orderTip"></span></div>
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
                                <div id="pagecount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_PagerCust" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divSellEmpPage">
                                    <span id="pageDataList1_TotalProvider"></span>每页显示
                                    <input name="text" type="text"  style=" width:30px;" id="ShowPageCountCust" />
                                    条 转到第
                                    <input name="text" type="text" style=" width:30px;" id="ToPageCust" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeCustPageCountIndex($('#ShowPageCountCust').val(),$('#ToPageCust').val());" />
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

//    var pageCount = 10;//每页计数
//    var totalRecord = 0;
//    var pagerStyle = "flickr";//jPagerBar样式
//    var currentPageIndex = 1;
//    var orderBy = "";//排序字段

var popCustObj = new Object();
popCustObj.InputObjID = null;
popCustObj.InputObjName = null;

popCustObj.ShowCustList=function(objProviderID1,objProviderID2)
{

    popCustObj.InputObjID = objProviderID1;
    popCustObj.InputObjName = objProviderID2;
    document.getElementById('divCustInfo').style.display='block';
    TurnToPageCust(currentCustPageIndex,objProviderID1,objProviderID2);
}
 
  
    
    var pageCustCount = 10;//每页计数
    var totaCustRecord = 0;
    var pagerCustStyle = "flickr";//jPagerBar样式
    
    var currentCustPageIndex = 1;
    var actionCust = "";//操作
    var orderCustBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPageCust(pageIndex,objProviderID1,objProviderID2)
    {
            
           currentPageIndexProvider = pageIndex;
   
           var ProviderID= "";
           var ProviderNo="";
           var ProviderName="";
            
            $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/CustInfo.ashx',//目标地址
           cache:false,
           data: "currentPageIndexProvider="+pageIndex+"&pageCountProvider="+pageCustCount+"&orderByProvider="+orderCustBy+"",//数据
           beforeSend:function(){$("#pageDataList1_PagerCust").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1Provider tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ProviderID != null && item.ProviderID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"FillCust('"+item.ProviderID+"','"+item.ProviderName+"','"+item.BigtypeID+"','"+item.BigtypeName+"','"+objProviderID1+"','"+objProviderID2+"');\" />"+"</td>"+
                        "<td height='22' align='center'>" + item.ProviderID + "</td>"+
                        "<td height='22' align='center'>" + item.ProviderNo + "</td>"+
                        "<td height='22' align='center'>"+item.ProviderName+"</td>").appendTo($("#pageDataList1Provider tbody"));
                   });
                    //页码
                    ShowPageBar("pageDataList1_PagerCust",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerCustStyle,mark:"pageEmpDataList1Mark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pagecount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"CustTurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalSellEmpRecord = msg.totalCount;
                  $("#ShowPageCountCust").val(pagecount);
                  ShowTotalPage(msg.totalCount,pagecount,pageIndex,$("#pagecount"));
                  $("#ToPageCust").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){$("#pageDataList1_PagerCust").show();pageSellCustDataList1("pageDataList1Provider","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
          
    }
     //table行颜色
function pageSellCustDataList1(o,a,b,c,d)
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
function ChangeCustPageCountIndex(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalSellEmpRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pagecount=parseInt(newPageCount);
        TurnToPageCust(1);
    }
}
//排序
function SellEmpOrderBy(orderColum,orderTip)
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
    TurnToPageCust(1);
}
   
function closeCustdiv()
{
    document.getElementById("divCustInfo").style.display="none";
}

function FillCust(CustID,CustName,TypeID,TypeName,objProviderID1,objProviderID2)
{
   
    $("#"+objProviderID1).val(CustName);
    $("#"+objProviderID1).attr("title",CustID);
    $("#"+objProviderID2).val(TypeName);
    $("#"+objProviderID2).attr("title",TypeID);
    closeCustdiv();
}




</script>