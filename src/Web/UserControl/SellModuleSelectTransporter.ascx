<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SellModuleSelectTransporter.ascx.cs"
    Inherits="UserControl_SellModuleSelectTransporter" %>
<div id="sellModuleTransporter">
    <!--提示信息弹出详情start-->
    <a name="pageTranDataList1Mark"></a>
    <div id="divSellModuleTranSelect" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 700px; z-index: 21; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px;">
       
         <table width="99%" border="0" align="center" cellpadding="0" id="Table1" cellspacing="0"
            bgcolor="#CCCCCC">
            <tr>
                <td bgcolor="#FFFFFF">
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                        class="table">
                        <tr class="table-item">
                            <td height="25" colspan="6" bgcolor="#E7E7E7" align="left" style="width: 50%;">
                               
                                 <img src="../../../Images/Button/Bottom_btn_close.jpg" alt="关闭" onclick="closeSellModuTrandiv()" />
                            </td>
                           
                        </tr>
                        <tr class="table-item">
                            <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                                运输商编号
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="TranNoUC" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                                运输商名称
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="TranNameUC" class="tdinput" maxlength="50" type="text" />
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                            </td>
                            <td width="21%" bgcolor="#FFFFFF">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" bgcolor="#FFFFFF">
                                <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                    onclick='SellTranTurnToPage(1)' />&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="sellTranList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellTranOrderBy('CustNo','oSelltran');return false;">
                            运输商编号<span id="oSelltran" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellTranOrderBy('CustName','oSTranUC1');return false;">
                            运输商名称<span id="oSTranUC1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellTranOrderBy('CustNote','oSTranUC2');return false;">
                            运输商简介<span id="oSTranUC2" class="orderTip"></span></div>
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
                                <div id="pageSellTran">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="sellTranList_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divSellTranPage">
                                    <span id="pageSellTranList_Total"></span>每页显示
                                    <input name="text" type="text" style="width: 30px;" id="ShowSellTranPageCount" maxlength="3" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 30px;" id="ToSellTranPage" maxlength="7" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeSellTranPageCountIndex($('#ShowSellTranPageCount').val(),$('#ToSellTranPage').val());" />
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
var popSellTranObj=new Object();
popSellTranObj.InputObj1 = null;

popSellTranObj.ShowList=function(objInput1)
{
    popSellTranObj.InputObj1 = objInput1;
    ShowPreventReclickDiv();
    $("#TranNoUC").val('');
    $("#TranNameUC").val('');
    document.getElementById('divSellModuleTranSelect').style.display='block';
    SellTranTurnToPage(currentSellTranPageIndex,objInput1);
}
  
    var pageSellTranCount = 10;//每页计数
    var totalSellTranRecord = 0;
    var pagerSellTranStyle = "flickr";//jPagerBar样式
    
    var currentSellTranPageIndex = 1;
    var actionSellTran = "";//操作
    var orderSellTranBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function SellTranTurnToPage(pageIndex) {
        var Title = $.trim($("#TranNameUC").val());
        var OrderNo = $.trim($("#TranNoUC").val());
           currentSellTranPageIndex = pageIndex;        
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SellManager/SellModuleSelectTransporter.ashx',//目标地址
           cache:false,
           data: "pageIndex=" + pageIndex + "&pageSellTranCount=" + pageSellTranCount + "&orderby=" + orderSellTranBy +
            "&Title=" + escape(Title) + "&orderNo=" + escape(OrderNo) ,
           beforeSend:function(){$("#sellTranList_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#sellTranList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"radio\" name=\"radioTran_\" id=\"radioTran_" + item.ID + "\" value=\"" + item.ID + "\" onclick=\"fnSelectSellTran(" + item.ID + ",'" + item.CustName + "','" + popSellTranObj.InputObj1 + "');\" />" + "</td>" +
                         "<td height='22' align='center'>"+ item.CustNo +"</td>"+
                         "<td height='22' align='center'>"+ item.CustName +"</td>"+
                         "<td height='22' align='center'>"+item.CustNote+"</td>").appendTo($("#sellTranList tbody"));
                   });
                    //页码
                    ShowPageBar("sellTranList_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerSellTranStyle,mark:"pageTranDataList1Mark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageSellTranCount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"SellTranTurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalSellTranRecord = msg.totalCount;
                  $("#ShowSellTranPageCount").val(pageSellTranCount);
                  ShowTotalPage(msg.totalCount,pageSellTranCount,pageIndex,$("#pageSellTran"));
                  $("#ToSellTranPage").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){$("#sellTranList_Pager").show();pageSellTranDataList1("sellTranList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageSellTranDataList1(o,a,b,c,d)
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
function ChangeSellTranPageCountIndex(newPageCount,newPageIndex)
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    $("#checkall").removeAttr("checked")
    if (!IsNumber(newPageIndex) || newPageIndex == 0) {
        isFlag = false;
        fieldText = fieldText + "跳转页面|";
        msgText = msgText + "必须为正整数格式|";
    }
    if (!IsNumber(newPageCount) || newPageCount == 0) {
        isFlag = false;
        fieldText = fieldText + "每页显示|";
        msgText = msgText + "必须为正整数格式|";
    }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    else {
        if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalSellTranRecord - 1) / newPageCount) + 1) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
            return false;
        }
        else {
            this.pageSellTranCount = parseInt(newPageCount);
            SellTranTurnToPage(parseInt(newPageIndex));
        }
    }
}
//排序
function SellTranOrderBy(orderColum,orderTip)
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
    orderSellTranBy = orderColum+"_"+ordering;
    SellTranTurnToPage(1);
}
   
function closeSellModuTrandiv() {
    obj = document.getElementById("divPreventReclick");
    //隐藏遮挡的DIV
    if (obj != null && typeof (obj) != "undefined") obj.style.display = "none";
    document.getElementById("divSellModuleTranSelect").style.display="none";
}

function fnSelectSellTran(ID,CustName,objInput1)
{
    $("#"+objInput1).val(CustName);
    $("#"+objInput1).attr("title",ID);
    closeSellModuTrandiv();
}
</script>

