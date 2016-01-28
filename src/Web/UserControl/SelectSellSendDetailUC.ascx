<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectSellSendDetailUC.ascx.cs" Inherits="UserControl_SelectSellSendDetailUC" %>

<script type="text/javascript">
//是否启用多计量单位：True时启用。
  var glb_IsMoreUnit = '<%=((XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString() %>';/*是否启用多计量单位*/
</script>
<div id="divSellSendDetail">
    <a name="pageSendDetailMark"></a>
    <input id="fromBillType" type="hidden" runat="server" value="" /><!--记录是否来自销售退货单-->
    <!--提示信息弹出详情start-->
    <div id="divSendDtailSelect" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 800px; z-index: 21; position: absolute; display: none;
        top: 20%; left: 50%; margin: 5px 0 0 -400px;">
       
          <table width="99%" border="0" align="center" cellpadding="0" id="Table1" cellspacing="0"
            bgcolor="#CCCCCC">
            <tr>
                <td bgcolor="#FFFFFF">
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                        class="table">
                        <tr class="table-item">
                         <td height="25" colspan="6" bgcolor="#E7E7E7" align="left" style="width: 50%;">
                          <img src="../../../Images/Button/Bottom_btn_close.jpg" alt="关闭" onclick="closeSendDetaildiv()" />
                                
                                
                                <img src="../../../Images/Button/Bottom_btn_ok.jpg" alt="确定" onclick="fnSelectSendDetail()" />
                         
                                
                            </td>
                            
                        </tr>
                        <tr class="table-item">
                            <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                                来源单据编号
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="SendDetNoUc" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                                主题
                            </td>
                            <td width="20%" bgcolor="#FFFFFF">
                                <input id="SendDetTitle" class="tdinput" maxlength="50" type="text" />
                            </td>
                            <td width="13%" bgcolor="#E7E7E7" align="right">
                            </td>
                            <td width="21%" bgcolor="#FFFFFF">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" bgcolor="#FFFFFF">
                                
                                <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                    onclick='TurnToSendDetPage(1)' />&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="SendDetailDataList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择<input type="checkbox" visible="false" id="chkSendDetail" onclick="selectAllSendDetail()"
                            value="checkbox" />
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SendDetailBy('SendNo','oGroupSe');return false;">
                            来源单据编号<span id="oGroupSe" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SendDetailBy('ProdNo','oCSe1');return false;">
                            物品编号<span id="oCSe1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SendDetailBy('ProductName','oSe4');return false;">
                             物品名称<span id="oSe4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SendDetailBy('ColorName','oColorName');return false;">
                             颜色<span id="oColorName" class="orderTip"></span></div>
                    </th>
                    <% if (((XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString() == "True")
                       {%>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="SendDetailBy('ProductCount','oDet8');return false;">
                                基本数量<span id="oDet8" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="SendDetailBy('UsedUnitCount','oSe9');return false;">
                               发货数量<span id="oSe9" class="orderTip"></span></div>
                        </th>
                       <%}
                       else
                       {%>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="SendDetailBy('ProductCount','oSe5');return false;">
                               发货数量<span id="oSe5" class="orderTip"></span></div>
                        </th>
                    <%} %>
                    <th id="haveBackedCount" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SendDetailBy('BackedCount','oBackedCount');return false;">
                            已退货数量<span id="oBackedCount" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" >
                        <div class="orderClick" onclick="SendDetailBy('SendDate','oSe6');return false;">
                            发货日期<span id="oSe6" class="orderTip"></span></div>
                    </th>
                    <% if (((XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString() == "True")
                       {%>
                       <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="SendDetailBy('UnitPrice','oSe10');return false;">
                                基本单价<span id="oSe10" class="orderTip"></span></div>
                        </th>
                       <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="SendDetailBy('UsedPrice','oSe11');return false;">
                                单价<span id="oSe11" class="orderTip"></span></div>
                        </th>
                       <%}
                       else
                       { %>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" >
                            <div class="orderClick" onclick="SendDetailBy('UnitPrice','oSe7');return false;">
                                单价<span id="oSe7" class="orderTip"></span></div>
                        </th>
                    <%} %>
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
                                <div id="pageSendDetail">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageSendDetList_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divSendpage">
                                    <span id="pageSendList_Total"></span>每页显示
                                    <input name="text" type="text" id="ShowSendDetailPageCount" style="width: 20px;" maxlength="3" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 20px;" id="SendDetailToPage" maxlength="7" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        align="absmiddle" onclick="ChangeSendDetailPageCountIndex($('#ShowSendDetailPageCount').val(),$('#SendDetailToPage').val());" />
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
var popSendDetailObj=new Object();
popSendDetailObj.CustID = null;
popSendDetailObj.CurrencyType = null;
popSendDetailObj.OrderID = null;
popSendDetailObj.BusType = null;
popSendDetailObj.FromBill=null;

popSendDetailObj.ShowList = function(CustID, BusType, CurrencyType, OrderID, Rate,FromBill)
{
    popSendDetailObj.CustID = CustID;
    popSendDetailObj.CurrencyType = CurrencyType;
    popSendDetailObj.OrderID = OrderID;
    popSendDetailObj.BusType = BusType;
    popSendDetailObj.Rate = Rate;
    popSendDetailObj.FromBill=FromBill;//标识是来自退货单还是来自委托代销页面（sellback则表示来自销售退货单）。
    //document.getElementById('fromBillType').value=FromBill;
    $("#fromBillType").val(FromBill);
    if(FromBill=="sellBack")
    {
        document.getElementById("haveBackedCount").style.display="";
        //$("#haveBackedCount").attr("display","");
    }
    else
    {
        document.getElementById("haveBackedCount").style.display="none";
    }

    $("#SendDetTitle").val('');
    $("#SendDetNoUc").val('');
    ShowPreventReclickDiv();
    document.getElementById('divSendDtailSelect').style.display = 'block';
   
   TurnToSendDetPage(1);
}
  
    var pageSendDetCount = 10;//每页计数
    var totalSendDetRecord = 0;
    var pagerSendDetStyle = "flickr";//jPagerBar样式
    
    var currentSendDetPageIndex = 1;
    var actionSendDet = "";//操作
    var orderBySendDet = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToSendDetPage(pageIndex) {
        var Title = $.trim($("#SendDetTitle").val());
        var OrderNo = $.trim($("#SendDetNoUc").val());
        $("#chkSendDetail").removeAttr("checked");
           currentSendDetPageIndex = pageIndex;
           $.ajax({
               type: "POST", //用POST方式传输
               dataType: "json", //数据格式:JSON
               url: '../../../Handler/Office/SellManager/SelectSellSend.ashx', //目标地址
               cache: false,
               data: "pageIndex=" + pageIndex + "&pageDetCount=" + pageSendDetCount + "&actionSend=detail&orderBySend=" + orderBySendDet + "&CustID=" + popSendDetailObj.CustID + "&busType=" + popSendDetailObj.BusType +
            "&Title=" + escape(Title) + "&orderNo=" + escape(OrderNo) + "&OrderID=" + escape(popSendDetailObj.OrderID) + "&CurrencyType=" + escape(popSendDetailObj.CurrencyType) + '&Rate=' + escape(popSendDetailObj.Rate), //数据
               beforeSend: function() { $("#pageSendDetList_Pager").hide(); }, //发送数据之前

               success: function(msg) {
                   //数据获取完毕，填充页面据显示
                   //数据列表
                   $("#SendDetailDataList tbody").find("tr.newrowSD").remove();
                   $.each(msg.data, function(i, item) {
                       if (item.ID != null && item.ID != "") {
                           var OrderNo = item.SendNo;

                           if (OrderNo != null) {
                               if (OrderNo.length > 20) {
                                   OrderNo = OrderNo.substring(0, 20) + '...';
                               }
                           }
                           var str='';//已退货数量 是否显示
                           if(popSendDetailObj.FromBill=="sellBack")
                           {
                                str=" style=\"display:\"";
                           }
                           else
                           {
                                str=" style=\"display:none\"";
                           }
                           
                           if(glb_IsMoreUnit=="True")
                           {
                                $("<tr class='newrowSD'></tr>").append("<td height='22' align='center'><input id='chkSendDet" + i + "' value=" + item.orderID + " type='checkbox'  /></td>" +
                                "<td height='22' align='center'><span title=\"" + item.SendNo + "\">" + OrderNo + "</td>" +
                                "<td height='22' align='center'>" + item.ProdNo + "</a></td>" +
                                "<td height='22' align='center'>" + item.ProductName + "</td>" +
                                "<td height='22' align='center'>" + item.ColorName + "</td>" +
                                "<td height='22' align='center'>" + item.ProductCount + "</td>" +
                                "<td height='22' align='center'>" + item.UsedUnitCount + "</td>" +
                                "<td height='22' align='center' "+str+">" + item.BackedCount + "</td>" +
                                "<td height='22' align='center'>" + item.SendDate + "</td>" +
                                "<td height='22' align='center'>" + item.UnitPrice + "</td>"+
                                "<td height='22' align='center'>" + item.UsedPrice + "</td>").appendTo($("#SendDetailDataList tbody"));  
                           }
                           else
                           {
                                $("<tr class='newrowSD'></tr>").append("<td height='22' align='center'><input id='chkSendDet" + i + "' value=" + item.orderID + " type='checkbox'  /></td>" +
                                "<td height='22' align='center'><span title=\"" + item.SendNo + "\">" + OrderNo + "</td>" +
                                "<td height='22' align='center'>" + item.ProdNo + "</a></td>" +
                                "<td height='22' align='center'>" + item.ProductName + "</td>" +
                                "<td height='22' align='center'>" + item.ColorName + "</td>" +
                                "<td height='22' align='center'>" + item.ProductCount + "</td>" +
                                "<td height='22' align='center' "+str+">" + item.BackedCount + "</td>" +
                                "<td height='22' align='center'>" + item.SendDate + "</td>" +
                                "<td height='22' align='center'>" + item.UnitPrice + "</td>").appendTo($("#SendDetailDataList tbody"));                                
                           }

                       }
                   });
                   //页码
                   ShowPageBar("pageSendDetList_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerSendDetStyle
                    , mark: "pageOrderDetailMark",
                    totalCount: msg.totalCount,
                    showPageNumber: 3,
                    pageCount: pageSendDetCount,
                    currentPageIndex: pageIndex,
                    noRecordTip: "没有符合条件的记录",
                    preWord: "上页",
                    nextWord: "下页",
                    First: "首页",
                    End: "末页",
                    onclick: "TurnToSendDetPage({pageindex});return false; "}//[attr]
                    );
                   totalSendDetRecord = msg.totalCount;
                   $("#ShowSendDetailPageCount").val(pageSendDetCount);
                   ShowTotalPage(msg.totalCount, pageSendDetCount, pageIndex, $("#pageSendDetail"));
                   $("#SendDetailToPage").val(pageIndex);
               },
               error: function() { },
               complete: function() { $("#pageSendDetList_Pager").show(); SendDetailDataList("SendDetailDataList", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕

           });
           $("#chkSendDetail").removeAttr("checked");
    }
    //table行颜色
function SendDetailDataList(o,a,b,c,d)
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
function ChangeSendDetailPageCountIndex(newPageCount,newPageIndex)
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
        if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalSendDetRecord - 1) / newPageCount) + 1) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
            return false;
        }
        else {
            this.pageSendDetCount = parseInt(newPageCount);
            TurnToSendDetPage(parseInt(newPageIndex));
        }
    }
}
//排序
function SendDetailBy(orderColum,orderTip)
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
    orderBySendDet = orderColum+"_"+ordering;
    TurnToSendDetPage(1);
}


function closeSendDetaildiv() {
    obj = document.getElementById("divPreventReclick");
    //隐藏遮挡的DIV
    if (obj != null && typeof (obj) != "undefined") obj.style.display = "none";
    document.getElementById("divSendDtailSelect").style.display="none";
}

function fnSelectSendDetail()
{
    var DetailID='';
    var SendDetailDataList = findObj("SendDetailDataList",document);
    for(i=0;i<SendDetailDataList.rows.length;i++)
    {               
        if( $("#chkSendDet"+i).attr("checked"))
        {
            DetailID+=$("#chkSendDet"+i).val()+',';
        }  
    }
    if(DetailID=='')
    {
        alert("您没有选择任何数据！");
    }
    else
    {
        $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url: '../../../Handler/Office/SellManager/SelectSellSend.ashx', //目标地址
           cache:false,
           data: "actionSend=getLsit&DetailID=" + DetailID, //数据
           beforeSend:function() {},//发送数据之前      
           success: function(msg){                 
                       if(msg.data.length==0)
                       {
                            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","获取数据失败！");
                       }
                       else
                       {
                           fnSetDetailData(msg.data);
                       }                        
                 },
           error: function() {}, 
           complete:function(){}//接收数据完毕 
        });
                   
    }
}

//全选
function selectAllSendDetail()
{ 
    var SendDetailDataList = findObj("SendDetailDataList",document);
    for(i=0;i<SendDetailDataList.rows.length;i++)
    {
        if ($("#chkSendDetail").attr("checked"))
       {          
            $("#chkSendDet"+i).attr("checked","true");
       }
       else
       {
            $("#chkSendDet"+i).removeAttr("checked");
       }
    }
}
</script>