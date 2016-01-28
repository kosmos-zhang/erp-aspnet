<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BudgetFeeTypeSelect.ascx.cs"
    Inherits="UserControl_BudgetFeeTypeSelect" %>

    <a name="pageFeeTypeMark"></a>
    <div id="divSellModuleFeeTypeSelect" style="border: solid 1px #999999; background: #fff;
        width: 700px; z-index: 20; position: absolute; display: none;
        top: 40%; left: 50%; margin: 5px 0 0 -400px">
<table width="99%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
  <tr>
    <td colspan="2"  >
<table width="100%">
             <tr>
    <td height="20" align="left" valign="center" class="Title">&nbsp;&nbsp;&nbsp;&nbsp;选择费用类别
    </td>
    <td  align="right" valign="center" >
     <img src="../../../Images/Pic/Close.gif" title="关闭" style="CURSOR: pointer"  onclick="closeSellModuFeeTypediv()"/>&nbsp;&nbsp;&nbsp;
    </td>
      
  </tr>
        </table>
    </td>
  </tr>
  <tr>
    <td colspan="2">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1"  id="pageDataList1"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellFeeOrderBy('CodeName','oSellFee');return false;">
                            费用名称<span id="oSellFee" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellFeeOrderBy('TypeName','oSFeeUC1');return false;">
                            费用类别<span id="oSFeeUC1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellFeeOrderBy('Description','oSFeeUC2');return false;">
                            费用描述<span id="oSFeeUC2" class="orderTip"></span></div>
                    </th>
                </tr>
            </tbody>
        </table>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
            class="PageList">
            <tr>
                <td height="28" background="../../../images/Main/PageList_bg.jpg">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                        <tr>
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="28%">
                                <div id="pageSellFeecount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="sellFeeList_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divSellFeePage">
                                    <span id="pageSellFeeList_Total"></span>每页显示
                                    <input name="text" type="text" style="width: 30px;" id="ShowSellFeePageCount"  maxlength="3" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 30px;" id="ToSellFeePage" maxlength="2" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeSellFeePageCountIndex($('#ShowSellFeePageCount').val(),$('#ToSellFeePage').val());" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </td>
  </tr>
</table>
</div>
<script type="text/javascript">
var popSellFeeTypeObj=new Object();
popSellFeeTypeObj.InputObj1 = null;

popSellFeeTypeObj.ShowList=function(objInput1)
{
    popSellFeeTypeObj.InputObj1 = objInput1;
    ShowPreventReclickDiv();
    document.getElementById('divSellModuleFeeTypeSelect').style.display='block';
    SellFeeTurnToPage(currentSellFeePageIndex,objInput1);
}
  
    var pageSellFeeCount = 10;//每页计数
    var totalSellFeeRecord = 0;
    var pagerSellFeeStyle = "flickr";//jPagerBar样式
    
    var currentSellFeePageIndex = 1;
    var actionSellFee = "";//操作
    var orderSellFeeBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function SellFeeTurnToPage(pageIndex,objInput1)
    {
           currentSellFeePageIndex = pageIndex;        
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SellManager/SellModuleSelectFeeType.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageSellFeeCount="+pageSellFeeCount+"&orderby="+orderSellFeeBy,//数据
           beforeSend:function(){$("#sellFeeList_Pager").hide();},//发送数据之前
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioFee_\" id=\"radioFee_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"fnSelectSellFee("+item.ID+",'"+item.CodeName+"','"+objInput1+"');\" />"+"</td>"+
                         "<td height='22' align='center'>"+ item.CodeName +"</td>"+
                         "<td height='22' align='center'>"+ item.TypeName +"</td>"+
                         "<td height='22' align='center'>"+item.Description+"</td>").appendTo($("#pageDataList1 tbody"));
                   });
                    //页码
                    ShowPageBar("sellFeeList_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerSellFeeStyle,mark:"pageFeeTypeMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageSellFeeCount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"SellFeeTurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalSellFeeRecord = msg.totalCount;
                  $("#ShowSellFeePageCount").val(pageSellFeeCount);
                  ShowTotalPage(msg.totalCount,pageSellFeeCount,pageIndex,$("#pageSellFeecount"));
                  $("#ToSellFeePage").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){$("#sellFeeList_Pager").show();pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });


    }
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
function ChangeSellFeePageCountIndex(newPageCount,newPageIndex)
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
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
        if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalSellFeeRecord - 1) / newPageCount) + 1) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
            return false;
        }
        else {
            this.pageSellFeeCount = parseInt(newPageCount);
            SellFeeTurnToPage(parseInt(newPageIndex));
        }
    }
}
//排序
function SellFeeOrderBy(orderColum,orderTip)
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
    orderSellFeeBy = orderColum+"_"+ordering;
    SellFeeTurnToPage(1);
}
   
function closeSellModuFeeTypediv() {
    obj = document.getElementById("divPreventReclick");
    //隐藏遮挡的DIV
    if (obj != null && typeof (obj) != "undefined") obj.style.display = "none";
    document.getElementById("divSellModuleFeeTypeSelect").style.display="none";
}

function fnSelectSellFee(ID,CodeName,objInput1)
{
    $("#"+objInput1).val(CodeName);
    $("#"+objInput1).attr("title",ID);
    closeSellModuFeeTypediv();
}
</script>

