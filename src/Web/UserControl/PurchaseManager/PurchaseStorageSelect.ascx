<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseStorageSelect.ascx.cs"
    Inherits="UserControl_PurchaseStorageSelect" %>
<style type="text/css">
    .tdinput
    {
        border-width: 0pt;
        background-color: #ffffff;
        height: 21px;
        margin-left: 2px;
    }
</style>
<div id="divPurchaseStoragehhh" style="display: none">
    <!--提示信息弹出详情start-->
    <div id="divPurchaseStorage3">
        <iframe id="frmPurchaseStorage"></iframe>
    </div>
    <div id="divPurchaseStorage" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 1100px; z-index: 20; position: absolute; display: block;
        top: 38%; left: 35%; margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closeMadiv()" style="text-align: left; cursor: pointer">
                        <img src="../../../images/Button/Bottom_btn_close.jpg" /></a>
                </td>
            </tr>
        </table>
        <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
            <tr>
                <td valign="top">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
                <td rowspan="2" align="right" valign="top">
                    <div id='searchClickStorage'>
                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('PurchaseStoragesearchtable','searchClickStorage')" /></div>
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
                    <table width="100%" border="0" align="left" cellpadding="0" id="PurchaseStoragesearchtable"
                        cellspacing="0" bgcolor="#CCCCCC">
                        <tr>
                            <td bgcolor="#FFFFFF">
                                <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                    class="table">
                                    <tr class="table-item">
                                        <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                            物品编号
                                        </td>
                                        <td width="23%" bgcolor="#FFFFFF">
                                            <input type="text" id="txt_StorageProductNo" class="tdinput" />
                                        </td>
                                        <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                            物品名称
                                        </td>
                                        <td width="23%" bgcolor="#FFFFFF">
                                            <input type="text" id="txt_StorageProductName" class="tdinput" />
                                        </td>
                                        <td width="10%" bgcolor="#E7E7E7" align="right">
                                            源单编号
                                        </td>
                                        <td width="24%" bgcolor="#FFFFFF">
                                            <input type="text" id="txt_StorageFromBillNo" class="tdinput" />
                                        </td>
                                    </tr>
                                    <tr id="trStorageNewAttr" style="display: none">
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            <span id="StoragespanOther" style="display: none">其他条件</span>
                                        </td>
                                        <td height="20" class="tdColInput" width="23%">
                                            <div id="divStorageProductDiyAttr" style="display: none">
                                                <select id="selStorageEFIndex" onchange="clearStorageEFDesc();">
                                                </select><input type="text" id="txtStorageEFDesc" style="width: 30%" />
                                            </div>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" colspan="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" align="center" bgcolor="#FFFFFF">
                                            <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                                onclick='Fun_Search_PurchaseStorage()' id="btn_StorageSearch" />
                                            <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;'
                                                onclick="GetValueStorageReject();" id="imgStorageSure" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListPurchaseStorage"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择<input type="checkbox" id="btnPStorageRejectAll" name="btnPStorageRejectAll" onclick="OptionCheckPStorageRejectAll()" />
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('ProductNo','SC1');return false;">
                            物品编号<span id="SC1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('ProductName','SC2');return false;">
                            物品名称<span id="SC2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('standard','SC3');return false;">
                            规格<span id="SC3" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('ColorName','pColorName');return false;">
                            颜色<span id="pColorName" class="orderTip"></span></div>
                    </th>
                    <th id="thUsedUnitName_divPurchaseStorage" align="center" background="../../../images/Main/Table_bg.jpg"
                        bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popOrder.orderByP('UsedUnitName','pc21');return false;">
                            基本单位<span id="pc21" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('UnitName','SC19');return false;">
                            单位<span id="SC19" class="orderTip"></span></div>
                    </th>
                    <th id="thUsedUnitCount_divPurchaseStorage" align="center" background="../../../images/Main/Table_bg.jpg"
                        bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popOrder.orderByP('UsedUnitCount','pc22');return false;">
                            基本数量<span id="pc22" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('ProductCount','SC4');return false;">
                            到货数量<span id="SC4" class="orderTip"></span></div>
                    </th>
                    <th id="thUsedPrice_divPurchaseStorage" align="center" background="../../../images/Main/Table_bg.jpg"
                        bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popOrder.orderByP('UsedPrice','pc23');return false;">
                            基本单价<span id="pc23" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('UnitPrice','SC5');return false;">
                            单价<span id="SC5" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('TaxPrice','SC6');return false;">
                            含税价<span id="SC6" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('Discount','SC7');return false;">
                            折扣(%)<span id="SC7" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('TaxRate','SC8');return false;">
                            税率(%)<span id="SC8" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('TotalPrice','SC9');return false;">
                            金额<span id="SC9" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('Remark','SC12');return false;">
                            备注<span id="SC12" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('FromBillNo','SC13');return false;">
                            源单编号<span id="SC13" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('FromLineNo','SC14');return false;">
                            源单序号<span id="SC14" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('BackCount','SC15');return false;">
                            退货数量<span id="SC15" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('ProviderName','SC16');return false;">
                            供应商<span id="SC16" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('CurrencyTypeName','SC17');return false;">
                            币种<span id="SC17" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="popStorageObject.orderByP('Rate','SC18');return false;">
                            汇率<span id="SC18" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            商品信息<span id="Span1" class="orderTip"></span></div>
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
                                <div id="pagecountStorage">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_PagerPurchaseStorage" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageStorage">
                                    <input name="TextPurchaseStorage" type="text" id="TextPurchaseStorage" style="display: none" />
                                    <span id="pageDataList1_Total"></span>每页显示
                                    <input name="ShowPageCountPurchaseStorage" type="text" id="ShowPageCountPurchaseStorage"
                                        style="width: 22px" />条 转到第
                                    <input name="ToPagePurchaseStorage" type="text" id="ToPagePurchaseStorage" style="width: 35px" />页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexPurchaseStorage($('#ShowPageCountPurchaseStorage').val(),$('#ToPagePurchaseStorage').val());" />
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

<script language="javascript" type="text/javascript">
var popStorageObject=new Object();
popStorageObject.InputObj = null;
popStorageObject.ProviderID= null;
popStorageObject.CurrencyTypeID= null;
popStorageObject.Rate= null;
popStorageObject.isMoreUnit=null;

    var pageCountPurchaseStorage = 10;//每页计数
    var totalRecordPurchaseStorage= 0;
    var pagerStylePurchaseOrder = "flickr";//jPagerBar样式
    
    var currentPageIndexPurchaseOrder = 1;
    var actionPurchaseOrder = "";//操作
    var orderbyPurchaseStorage = "";//排序字段
    var pageCount='';
    
     popStorageObject.orderByP=function(orderColum,orderTip)
{
    var ordering = "a";
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
    orderbyPurchaseStorage = orderColum+"_"+ordering;
    TurnToPagePurchaseStorage(1);
}
function OptionCheckPStorageRejectAll()
{
  if(document.getElementById("btnPStorageRejectAll").checked)
  {
     var ck = document.getElementsByName("ChkStorageReject");
        for( var i = 0; i < ck.length; i++ )
        {
            ck[i].checked=true ;
        }
  }
  else
  {
    var ck = document.getElementsByName("ChkStorageReject");
        for( var i = 0; i < ck.length; i++ )
        {
            ck[i].checked=false ;
        }
  }
}    
popStorageObject.ShowList = function(objInput,ProviderID,CurrencyTypeID,Rate,isMoreUnit)
{
    popStorageObject.InputObj = objInput;
    popStorageObject.ProviderID = ProviderID;
    popStorageObject.CurrencyTypeID = CurrencyTypeID;
    popStorageObject.Rate = Rate;
    popStorageObject.isMoreUnit=isMoreUnit;
    document.getElementById("divPurchaseStoragehhh").style.display="block";
    openRotoscopingDiv(true,"divPurchaseStorage3","frmPurchaseStorage");
    
    SetCloumnHide();
    
    TurnToPagePurchaseStorage(1);
}


// 隐藏列
function SetCloumnHide()
{
    if(!popStorageObject.isMoreUnit)
    {// 隐藏非多计量单位
        $("#thUsedUnitName_divPurchaseStorage").hide();
        $("#thUsedUnitCount_divPurchaseStorage").hide();
        $("#thUsedPrice_divPurchaseStorage").hide();
    }
}

function clearStorageEFDesc()
{
    if($("#selStorageEFIndex").val()=="-1")
   {
    return;
   }
    $("#txtStorageEFDesc").val("");
}

function fnStorageGetExtAttrOther() {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/SupplyChain/TableExtFieldsInfo.ashx", //目标地址
        cache: false,
        data: 'action=all',
        beforeSend: function() { AddPop();  }, //发送数据之前

        success: function(msg) {
  
  
            
            //数据获取完毕，填充页面据显示
            //存在扩展属性显示页面扩展属性表格
            if (parseInt(msg.totalCount) > 0) {
                $("<option value=\" \">--请选择--</option>").appendTo($("#selStorageEFIndex"));
                $("#divStorageProductDiyAttr").show();
                $("#StoragespanOther").show();
                $("#trStorageNewAttr").show();
                $.each(msg.data, function(i, item) 
                {
                    $("<option value=\""+item.EFIndex+"\">"+item.EFDesc+"</option>").appendTo($("#selStorageEFIndex"));
                });
                document.getElementById("selStorageEFIndex").selectedIndex=0;
            }
        },
        error: function() { popMsgObj.ShowMsg('请求发生错误！'); },
        complete: function() { hidePopup(); } //接收数据完毕
    });
} 


function IfshowPurchaseStorage(count)
    {
        if(count=="0")
        {
            document.getElementById("divpageStorage").style.display = "none"; 
            document.getElementById("pagecountStorage").style.display = "none";
        }
        else
        {
            document.getElementById("divpageStorage").style.display = "block"; 
            document.getElementById("pagecountStorage").style.display = "block";
        }
    }

function closeMadiv()
{

    document.getElementById("divPurchaseStoragehhh").style.display="none";
    closeRotoscopingDiv(true,"divPurchaseStorage3")
}

function TurnToPagePurchaseStorage(pageIndexPurchaseStorage)
{
       currentPageIndexPurchaseOrder = pageIndexPurchaseStorage;
       
       
       var ProductNo= $("#txt_StorageProductNo").val().Trim();
       var ProductName= $("#txt_StorageProductName").val().Trim();
       var FromBillNo= $("#txt_StorageFromBillNo").val().Trim();
       var StorageEFIndex="";
       var StorageEFDesc="";
      if(document.getElementById("trStorageNewAttr").style.display!="none")
      {
        StorageEFIndex=document.getElementById("selStorageEFIndex").value;
        StorageEFDesc=document.getElementById("txtStorageEFDesc").value;
      }
 
       $.ajax({
       type: "POST",
       dataType:"json",
       url:  '../../../Handler/Office/PurchaseManager/PurchaseArriveStorage.ashx',
       cache:false,
       data: "pageIndexPurchaseStorage="+pageIndexPurchaseStorage
            +"&pageCountPurchaseStorage="+pageCountPurchaseStorage
            +"&Provider00ID="+popStorageObject.ProviderID
            +"&CurrencyTypeID="+popStorageObject.CurrencyTypeID
            +"&Rate="+popStorageObject.Rate
            +"&StorageEFIndex="+escape(StorageEFIndex)
            +"&StorageEFDesc="+escape(StorageEFDesc)
            +"&ProductNo="+escape(ProductNo)
            +"&ProductName="+escape(ProductName)
            +"&FromBillNo="+escape(FromBillNo)
            +"&actionPurchaseOrder="+actionPurchaseOrder
            +"&orderbyPurchaseStorage="+orderbyPurchaseStorage+"",
       beforeSend:function(){AddPop();$("#pageDataList1_PagerPurchaseStorage").hide();},
       
       success: function(msg){
                $("#pageDataListPurchaseStorage tbody").find("tr.newrow").remove();
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" onclick=\"PurStorageBox("+i+")\" name=\"ChkStorageReject\" id=\"ChkStorageReject"+i+"\" value=\""+item.ID+"\"/>"+"</td>"+
                    "<td height='22' style='display:none' id='PStorageProductID"+i+"' align='center'>" + item.ProductID + "</td>"+
                    "<td height='22' align='center' id='PStorageProductNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                    "<td height='22' align='center' id='PStorageProductName"+i+"' align='center'>" + item.ProductName + "</td>"+
                    "<td height='22' align='center' id='PStorageStandard"+i+"' align='center'>" + item.standard + "</td>"+
                    "<td height='22' align='center' id='PStorageColorName"+i+"' align='center'>" + item.ColorName + "</td>"+
                    (popStorageObject.isMoreUnit?(
                    "<td height='22' style='display:none' id='PStorageUsedUnitID"+i+"' align='center'>" + item.UnitID + "</td>"+
                    "<td height='22' align='center' id='PStorageUsedUnitName"+i+"' align='center'>" + item.UnitName + "</td>"+
                    "<td height='22' style='display:none' id='PStorageUnitID"+i+"' align='center'>" + item.UsedUnitID + "</td>"+
                    "<td height='22' align='center' id='PStorageUnitName"+i+"' align='center'>" + item.UsedUnitName + "</td>"+
                    "<td height='22' align='center' id='PStorageUsedUnitCount"+i+"' align='center'>" + item.ProductCount + "</td>"+
                    "<td height='22' align='center' id='PStorageProductCount"+i+"' align='center'>" + item.UsedUnitCount + "</td>"
                    ):(
                    "<td height='22' style='display:none' id='PStorageUnitID"+i+"' align='center'>" + item.UnitID + "</td>"+
                    "<td height='22' align='center' id='PStorageUnitName"+i+"' align='center'>" + item.UnitName + "</td>"+
                    "<td height='22' align='center' id='PStorageProductCount"+i+"' align='center'>" + item.ProductCount + "</td>"))+
                    (popStorageObject.isMoreUnit?("<td height='22' align='center' id='PStorageUsedPrice"+i+"' align='center'>" +FormatAfterDotNumber(item.UnitPrice,selPoint) + "</td>"+
                    "<td height='22' align='center' id='PStorageUnitPrice"+i+"' align='center'>" +FormatAfterDotNumber(item.UsedPrice,selPoint) + "</td>"):
                    "<td height='22' align='center' id='PStorageUnitPrice"+i+"' align='center'>" +FormatAfterDotNumber(item.UnitPrice,selPoint) + "</td>")+
                    "<td height='22' align='center' id='PStorageTaxPrice"+i+"' align='center'>" + item.TaxPrice + "</td>"+
                    "<td height='22' align='center' id='PStorageDiscount"+i+"' align='center'>" + item.Discount + "</td>"+
                    "<td height='22' align='center' id='PStorageTaxRate"+i+"' align='center'>" + item.TaxRate + "</td>"+
                    "<td height='22' align='center' id='PStorageTotalPrice"+i+"' align='center'>" + item.TotalPrice + "</td>"+
                    "<td height='22' align='center' id='PStorageRemark"+i+"' align='center'>" + item.Remark + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PStorageFromBillID"+i+"' align='center'>" + item.FromBillID + "</td>"+
                    "<td height='22' align='center' id='PStorageFromBillNo"+i+"' align='center'>" + item.FromBillNo + "</td>"+
                    "<td height='22' align='center' id='PStorageFromLineNo"+i+"' align='center'>" + item.FromLineNo + "</td>"+
                    "<td height='22' align='center' id='PStorageBackCount"+i+"' align='center'>" + item.BackCount + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PStorageProviderID"+i+"' align='center'>" + item.ProviderID + "</td>"+
                    "<td height='22' align='center' id='PStorageProviderName"+i+"' align='center'>" + item.ProviderName + "</td>"+
                    "<td height='22' style='display:none' align='center' id='PStorageCurrencyType"+i+"' align='center'>" + item.CurrencyType + "</td>"+
                    "<td height='22' align='center' id='PStorageCurrencyTypeName"+i+"' align='center'>" + item.CurrencyTypeName + "</td>"+
                    "<td height='22' align='center' id='PStorageRejectRate"+i+"' align='center'>"+item.Rate+"</td>"+ 
                    "<td align=\"center\"><a href=\"../../../Pages/Office/SupplyChain/ProductInfoAdd.aspx?intOtherCorpInfoID="+item.ProductID+"\"  target=\"_blank\">查看</a></td>").appendTo($("#pageDataListPurchaseStorage tbody"));
               });
               ShowPageBar("pageDataList1_PagerPurchaseStorage",
               "<%= Request.Url.AbsolutePath %>",
                {style:pagerStylePurchaseOrder,mark:"pageDataList1MarkPurchaseOrder",
                totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountPurchaseStorage,currentPageIndex:pageIndexPurchaseStorage,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页", onclick:"TurnToPagePurchaseStorage({pageindex});return false;"}
                );document.getElementById("btnPStorageRejectAll").checked = false;
                
              totalRecordPurchaseStorage = msg.totalCount;
              document.getElementById ("TextPurchaseStorage") .value=msg.totalCount;
              $("#ShowPageCountPurchaseStorage").val(pageCountPurchaseStorage);
              ShowTotalPage(msg.totalCount,pageCountPurchaseStorage,pageIndexPurchaseStorage,$("#pagecountStorage"));
              $("#ToPagePurchaseStorage").val(pageIndexPurchaseStorage);
              },
       error: function() {}, 
       complete:function(){hidePopup();$("#pageDataList1_PagerPurchaseStorage").show();IfshowPurchaseStorage(document.getElementById("TextPurchaseStorage").value);pageDataList1("pageDataListPurchaseStorage","#E7E7E7","#FFFFFF","#cfc","cfc");}
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

function PurStorageBox(rowID)
{
    var ck = document.getElementsByName("ChkStorageReject");
    for( var i = 0; i < ck.length; i++ )
    {
//        alert(i)
        if ( ck[i].checked && i!= rowID )
        {//选中某行
//            alert($("#PPurRejectProviderID"+i).html())
//            alert($("#PPurRejectProviderID"+rowID).html())
            if($("#PStorageProviderID"+i).html() != $("#PStorageProviderID"+rowID).html())
            {               
                //如果选择的不是一个合同则不让选
                document.getElementById("ChkStorageReject"+rowID).checked = false;
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的明细只能来自一个供应商！");
                return;
            }
            if($("#PStorageCurrencyType"+i).html() != $("#PStorageCurrencyType"+rowID).html() || $("#PStorageRejectRate"+i).html() != $("#PStorageRejectRate"+rowID).html())
            {               
                //如果选择的不是一个合同则不让选
                document.getElementById("ChkBoxPPurStorage"+rowID).checked = false;
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的到货单明细的币种和汇率必须相同！");
                return;
            }
        }
    }
}

function Fun_Search_PurchaseStorage(aa)
{
      search="1";
      TurnToPagePurchaseStorage(1);
}
function Ifshow(count)
{
    if(count=="0")
    { 
           document.getElementById ("divPurchaseStorage").style.display = "none";
        document.getElementById ("pagecountPurchaseOrder").style.display = "none";
    }
    else
    { 
                document.getElementById ("divPurchaseStorage").style.display = "block";
        document.getElementById ("pagecountPurchaseOrder").style.display = "block";
    }
}

function SelectMaterial(retval)
{
    alert(retval);
}

//改变每页记录数及跳至页数
function ChangePageCountIndexPurchaseStorage(newPageCountPurchaseStorage,newPageIndexPurchaseStorage)
{
    if(newPageCountPurchaseStorage <=0 || newPageIndexPurchaseStorage <= 0 ||  newPageIndexPurchaseStorage  > ((totalRecordPurchaseStorage-
1)/newPageCountPurchaseStorage)+1 )
    {
        return false;
    }
    else
    {
        this.pageCountPurchaseStorage=parseInt(newPageCountPurchaseStorage);
        TurnToPagePurchaseStorage(parseInt(newPageIndexPurchaseStorage));
    }
}

</script>

