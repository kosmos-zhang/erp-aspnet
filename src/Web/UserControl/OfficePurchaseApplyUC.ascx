<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OfficePurchaseApplyUC.ascx.cs"
    Inherits="UserControl_PurchaseManager_OfficePurchaseApplyUC" %>
<div id="divPurchaseApplyUC" style="display:none">
   <div id="divPurchaseApplyUC2"> <iframe id="frmPurchaseApplyUC" >
</iframe></div>
<div  style="border: solid 10px #93BCDD; background: #fff;
    padding: 10px; width: 1000px; z-index: 20; position: absolute; top: 50%; left: 40%;
    margin: 5px 0 0 -400px;">
    <table width="100%">
        <tr>
            <td>
          
                <a onclick="popPurchaseApplyUC.CloseList()" style="text-align: left; cursor: pointer">
                    <img  src="../../../images/Button/Bottom_btn_close.jpg"/></a>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
        class="table">
        <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                物品编号
            </td>
            <td width="15%" bgcolor="#FFFFFF">
                <input type="text" id="txtProdNoPurApp" class="tdinput" />
            </td>
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                物品名称
            </td>
            <td width="15%" bgcolor="#FFFFFF">
                <input type="text" id="txtProdNamePurApp" class="tdinput" />
            </td>
            <td width="10%" bgcolor="#E7E7E7" align="right">
                申请时间
            </td>
            <td bgcolor="#FFFFFF" style="width: 15%">
                <input type="text" id="txtStartDatePurApp" class="tdinput" readonly="readonly" onclick="WdatePicker()"/>
            </td>
            <td width="2%" bgcolor="#E7E7E7" align="right">
                ~
            </td>
            <td bgcolor="#FFFFFF" style="width: 15%">
                <input type="text" id="txtEndDatePurApp"  class="tdinput" readonly="readonly" onclick="WdatePicker()"/>
                <input type="text" id="hidPurAppSltCnd" style="display:none"  class="tdinput" />
            </td>
        </tr>
           
        <tr>
            <td colspan="8" align="center" bgcolor="#FFFFFF">
                <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                    onclick='fnGetPurApp()' id="btn_search" />
                <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;'
                    onclick="fnFillPurApp();" id="imgsure" />
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListPurchaseApplyUC"
        bgcolor="#999999">
        <tbody>
            <tr>
                <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    选择
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('ProductNo','PurchaseApplyUCProductNo');return false;">
                        用品编号<span id="PurchaseApplyUCProductNo" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('ProductName','PurchaseApplyUCProductName');return false;">
                        用品名称<span id="PurchaseApplyUCProductName" class="orderTip"></span></div>
                </th>
                     <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('TypeName','PurchaseApplyUCApplyReasonName');return false;">
                        用品分类<span id="PurchaseApplyUCApplyReasonName" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('Specification','PurchaseApplyUCSpecification');return false;">
                        规格<span id="PurchaseApplyUCSpecification" class="orderTip"></span></div>
                </th>
               
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('UnitName','PurchaseApplyUCUnitName');return false;">
                      <span id="sspPurchaseApplyUnit">单位</span>  <span id="PurchaseApplyUCUnitName" class="orderTip"></span></div>
                </th>
              
        
                
                
                
              
                
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('RequireCount','PurchaseApplyUCRequireCount');return false;">
                      <span id="sspPurchaseApplyCount">  需求数量</span><span id="PurchaseApplyUCRequireCount" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('InCount','Span1');return false;">
                        已入库数量<span id="Span1" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('RequireDate','PurchaseApplyUCRequireDate');return false;">
                        需求日期<span id="PurchaseApplyUCRequireDate" class="orderTip"></span></div>
                </th>
              
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('FromBillNo','PurchaseApplyUCFromBillNo');return false;">
                        源单编号<span id="PurchaseApplyUCFromBillNo" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseApplyUC.orderByP('SortNo','PurchaseApplyUCFromSortNo');return false;">
                        源单序号<span id="PurchaseApplyUCFromSortNo" class="orderTip"></span></div>
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
                        <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                            <div id="PageCountPurchaseApplyUC">
                            </div>
                        </td>
                        <td height="28" align="right">
                            <div id="pageDataList1_PagerPurchaseApplyUC" class="jPagerBar">
                            </div>
                        </td>
                        <td height="28" align="right">
                            <div id="divpagePurchaseApplyUC">
                                <input name="TotalRecordPurchaseApplyUC" type="text" id="TotalRecordPurchaseApplyUC"
                                    style="display: none" />
                                <span id="TotalPagePurchaseApplyUC"></span>每页显示
                                <input name="PerPageCountPurchaseApplyUC" type="text" id="PerPageCountPurchaseApplyUC"
                                    style="width: 30px" />条 转到第
                                <input name="ToPagePurchaseApplyUC" type="text" id="ToPagePurchaseApplyUC" style="width: 30px" />页
                                <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                    width="36" height="28" align="absmiddle" onclick="popPurchaseApplyUC.ChangePageCountIndex($('#PerPageCountPurchaseApplyUC').val(),$('#ToPagePurchaseApplyUC').val());" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
</div>
</div>
<script type="text/javascript">
var popPurchaseApplyUC = new Object();
popPurchaseApplyUC.Type = null;
//分页等显示信息
popPurchaseApplyUC.pageCount = 10;
popPurchaseApplyUC.totalRecord = 0;
popPurchaseApplyUC.pagerStyle = "flickr";
popPurchaseApplyUC.currentPageIndex = 1;
popPurchaseApplyUC.action = "";
popPurchaseApplyUC.orderBy = "RequireDate";
popPurchaseApplyUC.orderByType ="desc" 

popPurchaseApplyUC.ShowList = function(Type)
{
    popPurchaseApplyUC.Type = Type;
    openRotoscopingDiv(true,"divPurchaseApplyUC2","frmPurchaseApplyUC")
    document.getElementById("divPurchaseApplyUC").style.display='block';
    
    popPurchaseApplyUC.TurnToPage(1)
}

popPurchaseApplyUC.CloseList = function()
{
    closeRotoscopingDiv(true,"divPurchaseApplyUC2")
    document.getElementById("divPurchaseApplyUC").style.display='none';
}

function fnGetPurApp() {

    if (cheeck_sd()) {

        fnGetPurAppSlt();
        popPurchaseApplyUC.TurnToPage(1);
    }
    else {
        return;
    }

 
}


 
            
            
function cheeck_sd() {

    if (!CheckSpecialWord($("#txtProdNoPurApp").val())) {

        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "物品编号不允许包含特殊字符！");
        return false;
    }
       if (!CheckSpecialWord($("#txtProdNamePurApp").val())) {

           showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "物品名称不允许包含特殊字符！");
           return false;
    }
    return true ;
}

function fnGetPurAppSlt()
{
    
      
     var str = "";
    str += "ProductNo="+escape($("#txtProdNoPurApp").val());
    str += "&ProductName="+escape($("#txtProdNamePurApp").val());
    str += "&StartDate="+escape($("#txtStartDatePurApp").val());
    str += "&EndDate="+escape($("#txtEndDatePurApp").val()); 
    $("#hidPurAppSltCnd").val(str);
    return str;
}





popPurchaseApplyUC.TurnToPage = function(pageIndex) {
    popPurchaseApplyUC.currentPageIndex = pageIndex;

    var strParams = "";
    strParams += "&pageIndex=" + popPurchaseApplyUC.currentPageIndex;
    strParams += "&pageCount=" + popPurchaseApplyUC.pageCount;
    strParams += "&orderby=" + popPurchaseApplyUC.orderBy
    strParams += "&OrderByType=" + popPurchaseApplyUC.orderByType;
    strParams += "&ProductNo=" + escape($("#txtProdNoPurApp").val());
    strParams += "&ProductName=" + escape($("#txtProdNamePurApp").val());
    strParams += "&StartDate=" + escape($("#txtStartDatePurApp").val());
    strParams += "&EndDate=" + escape($("#txtEndDatePurApp").val());

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/AdminManager/OfficePurchaseApplyUC.ashx', //目标地址
        cache: false,
        data: strParams, //数据
        beforeSend: function() { $("#pageDataList1_PagerPurchaseApplyUC").hide(); }, //发送数据之前         
        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataListPurchaseApplyUC tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                if (item.ProductID != null && item.ProductID != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"checkbox\" name=\"ChkBoxPurApp\" id=\"ChkBoxPurApp" + i + "\" value=\"" + item.ID + "\"  />" + "</td>" +
                        "<td height='22' style='display:none' id='PurAppProductID" + i + "' align='center'>" + item.ProductID + "</td>" +
                        "<td height='22' id='PurAppProductNo" + i + "' align='center'>" + item.ProductNo + "</td>" +
                        "<td height='22' id='PurAppProductName" + i + "' align='center'>" + item.ProductName + "</td>" +
                           "<td height='22' id='PurAppTypeName" + i + "' align='center'>" + item.TypeName + "</td>" +
                        "<td height='22' id='PurAppSpecification" + i + "' align='center'>" + item.Specification + "</td>" +

                        "<td height='22' style='display:none' id='PurAppUnitID" + i + "' align='center'>" + item.UnitID + "</td>" +
                        "<td height='22' id='PurAppUnitName" + i + "' align='center'>" + item.UnitName + "</td>" +
                        "<td height='22' id='PurAppRequireCount" + i + "' align='center'>" + (parseFloat(item.RequireCount)).toFixed($("#HiddenPoint").val()) + "</td>" +
                        "<td height='22' id='PurAppPlanedCount" + i + "' align='center'>" + (parseFloat(item.InCount)).toFixed($("#HiddenPoint").val()) + "</td>" +
                        "<td height='22' id='PurAppRequireDate" + i + "' align='center'>" + item.RequireDate + "</td>" +
                        "<td height='22' style='display:none' id='PurAppFromBillID" + i + "' align='center'>" + item.FromBillID + "</td>" +
                        "<td height='22' id='PurAppFromBillNo" + i + "' align='center'>" + item.FromBillNo + "</td>" +
                        "<td height='22' id='PurAppSortNo" + i + "' align='center'>" + item.SortNo + "</td>").appendTo($("#pageDataListPurchaseApplyUC tbody"));
            });
            //页码

            ShowPageBar("pageDataList1_PagerPurchaseApplyUC", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {
                    style: popPurchaseApplyUC.pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount,
                    showPageNumber: 3,
                    pageCount: popPurchaseApplyUC.pageCount,
                    currentPageIndex: popPurchaseApplyUC.currentPageIndex,
                    noRecordTip: "没有符合条件的记录",
                    preWord: "上页",
                    nextWord: "下页",
                    First: "首页",
                    End: "末页",
                    onclick: "popPurchaseApplyUC.TurnToPage({pageindex});return false;"}//[attr]
                    );
            popPurchaseApplyUC.totalRecord = msg.totalCount;
            $("#PerPageCountPurchaseApplyUC").val(popPurchaseApplyUC.pageCount);
            ShowTotalPage(msg.totalCount, popPurchaseApplyUC.pageCount, pageIndex, $("#PageCountPurchaseApplyUC"));
            $("#ToPagePurchaseApplyUC").val(pageIndex);
        },
        error: function() { },
        complete: function() { hidePopup(); $("#pageDataList1_PagerPurchaseApplyUC").show(); popPurchaseApplyUC.pageDataList1("pageDataListPurchaseApplyUC", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
    });




}

popPurchaseApplyUC.pageDataList1= function(o,a,b,c,d)
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


function clearPurchaseArriveEFDesc()
{
 
}


 


popPurchaseApplyUC.ChangePageCountIndex= function(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((popPurchaseApplyUC.totalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        popPurchaseApplyUC.pageCount=parseInt(newPageCount);
        popPurchaseApplyUC.TurnToPage(parseInt(newPageIndex));
    }
}
popPurchaseApplyUC.orderByP = function(orderColum,orderTip)
{
 
    var ordering = "desc";
    var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
        ordering = "asc";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↑");
    }
    else
    {
        
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderPurchaseContractUCBy = orderColum+"_"+ordering;
    popPurchaseApplyUC.orderBy=orderColum;
    popPurchaseApplyUC.orderByType=ordering;
    popPurchaseApplyUC.TurnToPage(1);
}
</script>

