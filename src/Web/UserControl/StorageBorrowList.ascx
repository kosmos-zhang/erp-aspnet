<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StorageBorrowList.ascx.cs"
    Inherits="UserControl_StorageBorrowList" %>
   
    
<div id="divModuleProdcutInfo">
    <!--提示信息弹出详情start-->
    <a name="pageProductInfoMark"></a>
    <div id="divGetProductInfo" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 70%; z-index: 1001; position: absolute; display: none;" >
        <table width="100%">
            <tr>
                <td>
                    <img alt="关闭" id="btn_Close_a7" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: pointer;'
                        onclick='closeProductInfodiv();' />
                </td>
            </tr>
        </table>
        <table width="99%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
            <tr>
                <td valign="top">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
                <td rowspan="2" align="right" valign="top">
                    <div id='searchClick'>
                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" />
                    </div>
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top" class="Blue">
                    <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="99%" border="0" align="center" cellpadding="1" id="searchtable" cellspacing="0"
                        bgcolor="#CCCCCC">
                        <tr>
                            <td bgcolor="#FFFFFF">
                                <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                                    id="tblInterviewInfo">
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            借货单编号
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <input id="tboxBorrowNo" type="text" class="tdinput tboxsize" maxlength="25" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            借货部门
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <input type="text" id="DeptBorrowDeptName" readonly onclick="alertdiv('DeptBorrowDeptName,txtBorrowDeptID')"
                                                class="tdinput tboxsize" />
                                            <input type="hidden" id="txtBorrowDeptID" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            被借部门
                                        </td>
                                        <td height="20" class="tdColInput" width="24%">
                                            <input type="hidden" id="txtUCOutDept" />
                                            <input type="text" id="DeptOutDept" onclick="alertdiv('DeptOutDept,txtUCOutDept')"
                                                readonly class="tdinput tboxsize" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle">
                                            被借仓库
                                        </td>
                                        <td height="20" class="tdColInput">
                                            <asp:DropDownList ID="ddlDepot" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">
                                        </td>
                                        <td height="20" class="tdColInput">
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">
                                        </td>
                                        <td height="20" class="tdColInput">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" align="center" bgcolor="#FFFFFF">
                                            <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                                onclick='ProductInfoTurnToPage(1);' id="img_btn_search" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="divProductInfoList"
                        bgcolor="#999999">
                        <tbody>
                            <tr>
                                <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    选择
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="GetProductOrderBy('BorrowNo','BorrowNo');return false;">
                                        借货单编号<span id="BorrowNo" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="GetProductOrderBy('DeptID','DeptID');return false;">
                                        借货部门<span id="DeptID" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="GetProductOrderBy('Borrower','Borrower');return false;">
                                        借货人<span id="Borrower" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="GetProductOrderBy('BorrowDate','BorrowDate');return false;">
                                        借货日期<span id="BorrowDate" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="GetProductOrderBy('OutDeptID','OutDeptID');return false;">
                                        被借部门<span id="OutDeptID" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="GetProductOrderBy('StorageID','StorageID');return false;">
                                        被借仓库<span id="StorageID" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="GetProductOrderBy('CountTotal','CountTotal');return false;">
                                        借货数量<span id="CountTotal" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="GetProductOrderBy('TotalPrice','TotalPrice');return false;">
                                        借货金额<span id="TotalPrice" class="orderTip"></span></div>
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
                                            <div id="pageProductInfocount">
                                            </div>
                                        </td>
                                        <td height="28" align="right">
                                            <div id="getproductlist_Pager" class="jPagerBar">
                                            </div>
                                        </td>
                                        <td height="28" align="right">
                                            <div id="divProductInfoPage">
                                                <span id="pageProductInfo_Total"></span>每页显示
                                                <input name="text" type="text" style="width: 30px;" id="ShowProductInfoPageCount" />
                                                条 转到第
                                                <input name="text" type="text" style="width: 30px;" id="ToProductInfoPage" />
                                                页
                                                <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                    width="36" height="28" align="absmiddle" onclick="ChangeProductInfoPageCountIndex($('#ShowProductInfoPageCount').val(),$('#ToProductInfoPage').val());" />
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
        <br />
        <br />
    </div>
    <!--提示信息弹出详情end-->
</div>

<script type="text/javascript">
var popProductInfoObj=new Object();
popProductInfoObj.InputObj = null;

popProductInfoObj.ShowList=function()
{
    
    document.getElementById('divGetProductInfo').style.display='block';
    CenterToDocument("divGetProductInfo",true);
    ProductInfoTurnToPage(currentSellEmpPageIndex);
}
  
    var pageProductInfocount = 10;//每页计数
    var totalSellEmpRecord = 0;
    var pagerSellEmpStyle = "flickr";//jPagerBar样式
    
    var currentSellEmpPageIndex = 1;
    var actionSellEmp = "";//操作
    var orderSellEmpBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function ProductInfoTurnToPage(pageIndex)
    {
    
        para="BorrowNo="+document.getElementById("tboxBorrowNo").value+
                  "&BorrowDeptID="+document.getElementById("txtBorrowDeptID").value+
                  "&OutDeptID="+document.getElementById("txtUCOutDept").value+
                  "&OutStorageID="+document.getElementById("StorageBorrowList1_ddlDepot").value;
           currentSellEmpPageIndex = pageIndex;     
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageBorrowListForReturn.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pagecount="+pageProductInfocount+"&orderby="+orderSellEmpBy+"&"+para+"&"+Math.random(),//数据
           beforeSend:function(){$("#getproductlist_Pager").hide();openRotoscopingDiv(false,"divPageMask","PageMaskIframe");},//发送数据之前
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表    alert(msg.data);
                    $("#divProductInfoList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioEmp_\" id=\"radioEmp_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"selectedStorageBorrow("+item.ID+",'"+item.BorrowNo+"','"+item.DeptIDText+"','"+item.BorrowerText+"','"+item.BorrowDate+"','"+item.OutDeptIDText+"','"+item.StorageID+"','"+item.StorageName+"',"+item.CountTotal+","+item.TotalPrice+",'"+item.OutDeptIDText+"');\" />"+"</td>"+
                         "<td height='22' align='center'>"+ item.BorrowNo+"</td>"+
                         "<td height='22' align='center'>"+ item.DeptIDText+"</td>"+
                         "<td height='22' align='center'>"+item.BorrowerText+"</td>"+
                        "<td height='22' align='center'>"+(item.BorrowDate=="1753-01-01"?"":item.BorrowDate) +"</td>"+
                         "<td height='22' align='center'>"+item.OutDeptIDText +"</td>"+
                         "<td height='22' align='center'>"+item.StorageName  +"</td>"+
                          "<td height='22' align='center'>"+formatNum(item.CountTotal)  +"</td>"+
                         "<td height='22' align='center'>"+formatNum(item.TotalPrice) +"</td>").appendTo($("#divProductInfoList tbody"));
                        
                   });
                    //页码
                    ShowPageBar("getproductlist_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerSellEmpStyle,mark:"pageProductInfoMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageProductInfocount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"ProductInfoTurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalSellEmpRecord = msg.totalCount;
                  $("#ShowProductInfoPageCount").val(pageProductInfocount);
                  ShowTotalPage(msg.totalCount,pageProductInfocount,pageIndex,$("#pageProductInfocount"));
                  $("#ToProductInfoPage").val(pageIndex);
                  },
           error: function(msg) {}, 
           complete:function(){$("#getproductlist_Pager").show();pageProductInfoDataList1("divProductInfoList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
 
 /*设置数字格式*/
 function  formatNum(value)
 {
    if(value=="")
        return "0.00";
    else
        return parseFloat(value).toFixed(2);
 } 
 
    
    
    
    //table行颜色
function pageProductInfoDataList1(o,a,b,c,d)
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
function ChangeProductInfoPageCountIndex(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalSellEmpRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pageProductInfocount=parseInt(newPageCount);
        ProductInfoTurnToPage(parseInt(newPageIndex));
    }
}
//排序
function GetProductOrderBy(orderColum,orderTip)
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
    ProductInfoTurnToPage(1);
}
   
function closeProductInfodiv()
{
    document.getElementById("divGetProductInfo").style.display="none";
    closeRotoscopingDiv(false,"divPageMask","PageMaskIframe");
}






</script>

