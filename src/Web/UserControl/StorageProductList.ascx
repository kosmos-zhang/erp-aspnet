<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StorageProductList.ascx.cs"
    Inherits="UserControl_StorageProductList" %>
<%@ Register Src="ProductDiyAttr.ascx" TagName="ProductDiyAttr" TagPrefix="uc1" %>
<div id="divModuleProdcutInfo">
    <!--提示信息弹出详情start-->
    <a name="pageProductInfoMark"></a>
    <div id="divGetProductInfo" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 70%; z-index: 1001; position: absolute; display: none;">
        <table width="100%">
            <tr>
                <td>
                    <img alt="关闭" id="btn_Close_a3" src="../../../images/Button/Bottom_btn_close.jpg"
                        style='cursor: pointer;' onclick='closeProductInfodiv();' />
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
                                            物品名称
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <input id="txtProductName" type="text" class="tdinput tboxsize" maxlength="25" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            物品编号
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <input id="txtProdNo" type="text" class="tdinput tboxsize" maxlength="25" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            <span id="spanOther" style="display: none">其他条件</span>
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <uc1:ProductDiyAttr ID="ProductDiyAttr2" runat="server" />
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
                                    <div class="orderClick" onclick="GetProductOrderBy('ProdNo','oSellEmp');return false;">
                                        物品编号<span id="oSellEmp" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="GetProductOrderBy('ProductName','oSEmp1');return false;">
                                        物品名称<span id="oSEmp1" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="GetProductOrderBy('BatchNo','Span5');return false;">
                                        批次<span id="Span5" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="GetProductOrderBy('Specification','oSEmp2');return false;">
                                        规格<span id="oSEmp2" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="GetProductOrderBy('CodeName','Span1');return false;">
                                        基本单位<span id="Span1" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="GetProductOrderBy('ProductCount','Span2');return false;">
                                        现有库存量<span id="Span2" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick" onclick="GetProductOrderBy('StandardCost','Span3');return false;">
                                        借出单价<span id="Span3" class="orderTip"></span></div>
                                </th>
                                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                    <div class="orderClick">
                                        物品信息<span id="Span4" class="orderTip"></span></div>
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
    </div>
    <!--提示信息弹出详情end-->
</div>

<script type="text/javascript">
var popProductInfoObj=new Object();
popProductInfoObj.InputObj = null;
    var pageProductInfocount = 10;//每页计数
    var totalSellEmpRecord = 0;
    var pagerSellEmpStyle = "flickr";//jPagerBar样式
    
    var currentSellEmpPageIndex = 1;
    var actionSellEmp = "";//操作
    var orderSellEmpBy = "";//排序字段
popProductInfoObj.ShowList=function(objInput,para)
{
    popProductInfoObj.InputObj= objInput;
    pageProductInfocount = 10;//每页计数
    currentSellEmpPageIndex = 1;
    document.getElementById('divGetProductInfo').style.display='block';
    CenterToDocument("divGetProductInfo",true);
    ProductInfoTurnToPage(currentSellEmpPageIndex);
}
  
    //jQuery-ajax获取JSON数据
    function ProductInfoTurnToPage(pageIndex)
    {
            //拓展属性
          EFIndex=document.getElementById("selEFIndex").value;
          EFDesc=document.getElementById("txtEFDesc").value;
          
            objInput=document.getElementById("txtProductRowID").value;
            para="BorrowDeptID=-1&StorageID="+document.getElementById("ddlDepot").value+
                      "&ProductName="+escape(document.getElementById("txtProductName").value)+
                      "&ProdNo="+escape( document.getElementById("txtProdNo").value)+"&EFIndex="+escape(EFIndex)+"&EFDesc="+escape(EFDesc);
           currentSellEmpPageIndex = pageIndex; 
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageBorrow_Add.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageProductInfocount="+pageProductInfocount+"&orderby="+orderSellEmpBy+"&"+para,//数据
           beforeSend:function(){$("#getproductlist_Pager").hide();},//发送数据之前
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#divProductInfoList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                         {
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioEmp_\" id=\"radioEmp_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"fnSelectProductInfo("+item.ID+",'"+item.ProdNo+"','"+item.ProductName+"','"+item.Specification+"','"+getValue(item.ProductCount)+"','"+item.CodeName+"','"+item.UnitID+"','"+getValue(item.StandardCost)+"','"+item.ProductID+"','"+objInput+"','"+item.BatchNo+"');\" />"+"</td>"+
                         "<td height='22' align='center'>"+ item.ProdNo+"</td>"+
                         "<td height='22' align='center'>"+ item.ProductName+"</td>"+
                         "<td height='22' align='center'>"+ item.BatchNo+"</td>"+
                         "<td height='22' align='center'>"+item.Specification+"</td>"+
                         "<td height='22' align='center'>"+item.CodeName+"</td>"+
                         "<td height='22' align='center'>"+getValue(item.ProductCount)+"</td>"+
                         "<td height='22' align='center'>"+ getValue(item.StandardCost)+"</td>"+
                         "<td align=\"center\"><a href=\"../../../Pages/Office/SupplyChain/ProductInfoAdd.aspx?intOtherCorpInfoID="+item.ID+"\"  target=\"_blank\">查看</a></td>").appendTo($("#divProductInfoList tbody"));}
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
    
 /**/
 function getValue(value)
 {
    if(value=="")
    {
        return "0.00";
    }
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
    closeRotoscopingDiv(false,"divPageMask","PageMaskIframe");
    document.getElementById("divGetProductInfo").style.display="none";
}


function fnSelectProductInfo(ID,ProdNo,ProductName,Specification,ProductCount,CodeName,UnitID,StandardCost,ProductID,rowid,BatchNo)
{
   document.getElementById("chk_list_"+rowid).value=rowid;
   document.getElementById("tboxProductID_"+rowid).value=ID;
   document.getElementById("tboxProductNo_list_"+rowid).value=ProdNo;
   document.getElementById("tboxProductID_"+rowid).value=ProductID;
   document.getElementById("tboxProcutName_list_"+rowid).value=ProductName;
   document.getElementById("tboxStandard_list_" + rowid).value = Specification;
   document.getElementById("tboxUnit_list_" + rowid).value = CodeName;
   document.getElementById("tboxUnitID_" + rowid).value = UnitID;
   document.getElementById("tboxPrice_list_" + rowid).value = parseFloat(StandardCost).toFixed(2);
   
   //计量单位开启 
   if (document.getElementById("txtIsMoreUnit").value=="1") {
       GetUnitGroupSelect(ProductID, 'StockUnit', 'tboxUsedUnit_list_' + rowid, 'ChangeUnit(this,' + rowid + ')', "tboxUsedUnit_listtd_" + rowid, '');
       document.getElementById("tboxUsedPrice_list_" + rowid).value = parseFloat(StandardCost).toFixed(2);
   }

   document.getElementById("tboxStorageQuantity_list_" + rowid).value = parseFloat(ProductCount).toFixed(2);

   GetBatchList(ProductID, "ddlDepot", "BatchNo_SignItem_TD_Text_" + rowid, document.getElementById("hidIsBatchNo").value == "1" ? true : false,BatchNo);
   
   closeProductInfodiv();
    
}
</script>

