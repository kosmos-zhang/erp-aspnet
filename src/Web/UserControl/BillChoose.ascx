<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BillChoose.ascx.cs" Inherits="UserControl_BillChoose" %>
<a name="pageDataList1MarkBillChoose"></a>
<div id="layoutBillChoose">
 
    <iframe id="frmBillChoose" style="filter: Alpha(opacity=0);border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 800px; height: 500px; z-index: 1000; position: absolute; display: none; top: 50%; left: 50%;
        margin: 5px 0 0 -400px;"></iframe>
    <div id="divBillChoose" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 800px; z-index: 1001; position: absolute; display: none; top: 50%; left: 50%;
        margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closeBilldiv();" style="text-align: left; cursor: pointer"><img  src="../../../images/Button/Bottom_btn_close.jpg"/></a>
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
                    <input type="text" id="txtProdNoSllBll" class="tdinput" />
                </td>
                <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                    物品名称
                </td>
                <td width="15%" bgcolor="#FFFFFF">
                    <input type="text" id="txtProdNameSllBll" class="tdinput" />
                </td>
                <td width="10%" bgcolor="#E7E7E7" align="right">
                    需求时间
                </td>
                <td bgcolor="#FFFFFF" style="width: 15%">
                    <input type="text" id="txtStartDateSllBll" class="tdinput" readonly="readonly" onclick="WdatePicker()" />
                </td>
                <td width="2%" bgcolor="#E7E7E7" align="right">
                    ~
                </td>
                <td bgcolor="#FFFFFF" style="width: 15%">
                    <input type="text" id="txtEndDateSllBll" class="tdinput" readonly="readonly" onclick="WdatePicker()" />
                    <input type="text" id="hidSllBllSltCnd" style="display: none" class="tdinput" />
                </td>
            </tr>
            <tr>
                <td colspan="8" align="center" bgcolor="#FFFFFF">
                    <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                        onclick='fnGetSllBll()' id="btn_search" />
                    <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;'
                        onclick="fnFillSllBll();" id="imgsure" />
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListBillChoose"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBySllBll('OrderNo','SllBllOrdNo');return false;">
                            单据编号<span id="SllBllOrdNo" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBySllBll('SortNo','SllBllSrtNo');return false;">
                            行号<span id="SllBllSrtNo" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBySllBll('ProductNo','SllBllPrdNo');return false;">
                            物品编号<span id="SllBllPrdNo" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBySllBll('ProductName','SllBllPrdNam');return false;">
                            物品名称<span id="SllBllPrdNam" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBySllBll('Specification','SllBllSpecification');return false;">
                            规格<span id="SllBllSpecification" class="orderTip"></span></div>
                    </th>
                    
                      <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBySllBll('ColorName','SllBllColorName');return false;">
                            颜色<span id="SllBllColorName" class="orderTip"></span></div>
                    </th>
                  
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBySllBll('UnitName','SllBllUnt');return false;">
                       <span id="sspUnit">     基本单位</span><span id="SllBllUnt" class="orderTip"></span></div>
                    </th>
                       <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspUsedUnitName">
                        <div class="orderClick" onclick="OrderBySllBll('UsedUnitName','SllBllUsedUnitName');return false;">
                            单位<span id="SllBllUsedUnitName" class="orderTip"></span></div>
                    </th>
                    
                    
                    
                    
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBySllBll('ProductCount','SllBllPrdCnt');return false;">
                         <span id="sspCount">    基本数量</span><span id="SllBllPrdCnt" class="orderTip"></span></div>
                    </th>
                      <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspUsedUnitCount">
                        <div class="orderClick" onclick="OrderBySllBll('UsedUnitCount','SllBllUsedUnitCount');return false;">
                            数量<span id="SllBllUsedUnitCount" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderBySllBll('SendDate','SllBllRqrDate');return false;">
                            需求日期<span id="SllBllRqrDate" class="orderTip"></span></div>
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
                                <div id="pagecountBillChoose">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_PagerBillChoose" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageBillChoose">
                                    <input type="text" id="TextBillChoose" style="display: none" />
                                    <span id="pageTotalBillChoose"></span>每页显示
                                    <input name="ShowPageCountBillChoose" type="text" id="ShowPageCountBillChoose" style="width: 27px" />条
                                    转到第
                                    <input name="ToPageBillChoose" type="text" id="ToPageBillChoose" style="width: 24px" />页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexBillChoose($('#ShowPageCountBillChoose').val(),$('#ToPageBillChoose').val());" />
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

<script language="javascript" type="text/javascript">
var popBillObj=new Object();
popBillObj.InputObj = null;

    var pageCountBillChoose = 10;//每页计数
    var totalRecordBillChoose = 0;
    var pagerStyleBillChoose = "flickr";//jPagerBar样式
    
    var currentPageIndexBillChoose = 1;
    var actionBillChoose = "Select";//操作
    var orderByBillChoose = "OrderNo";//排序字段
    var orderByTypeBillChoose = "Desc";
    
popBillObj.ShowList = function(objInput)
{
    popBillObj.InputObj = objInput;
    document.getElementById("frmBillChoose").style.display="block";
    document.getElementById('divBillChoose').style.display='block';
    TurnToPageBillChoose(1);
}

function closeBilldiv()
{
    document.getElementById("frmBillChoose").style.display="none";
    document.getElementById("divBillChoose").style.display="none";
}

function fnGetSllBll()
{
    var str = "";
    str += "&ProductNo="+escape($.trim($("#txtProdNoSllBll").val()));
    str += "&ProductName="+escape($.trim($("#txtProdNameSllBll").val()));
    str += "&StartDate="+escape($.trim($("#txtStartDateSllBll").val()));
    str += "&EndDate="+escape($.trim($("#txtEndDateSllBll").val()));
    $("#hidSllBllSltCnd").val(str);
    
    
    TurnToPageBillChoose(1);
}

function fnFillSllBll()
{
    var ck = document.getElementsByName("ChkBoxSllBll");
    for( var i = 0; i < ck.length; i++ )
    {
        if (ck[i].checked)
        { 
            var pro=  $("#SllBllOrdNo"+i).html()
           var bll=  $("#SllBllSrtNo"+i).html();
             if (!ExistFromBill(pro,bll ))
               {
                   var rowID = BillChooseFillSignRow($("#SllBllPrdID" + i).html(), $("#SllBllPrdNo" + i).html(), $("#SllBllPrdNam" + i).html(), $("#SllBllSpecification" + i).html(), $("#SllBllUntID" + i).html(), $("#SllBllUntNam" + i).html(), FormatAfterDotNumber($("#SllBllPrdCnt" + i).html(), 2), $("#SllBllSndDat" + i).html(), 0, $("#SllBllOrdNo" + i).html(), $("#SllBllOrdID" + i).html(), $("#SllBllSrtNo" + i).html(), $("#SllBllSColorName"+i).html())
 if($("#HiddenMoreUnit").val()=="True")
                          {
                          var BaseCount=(parseFloat($("#SllBllPrdCnt"+i).html())).toFixed($("#HiddenPoint").val());
                          var ProuductCount=(parseFloat($("#SllBllPrdUsedUnitCnt"+i).html())).toFixed($("#HiddenPoint").val());
                             var UsedUnitID=$("#SllBllUsedUntID"+i).html();
        GetUnitGroupSelectEx($("#SllBllPrdID"+i).html(),"InUnit","SignItem_TD_UnitID_Select" + rowID,"ChangeUnit(this.id,"+rowID+")","unitdiv" + rowID,'',"FillApplyContent("+rowID+","+BaseCount+","+ProuductCount +",'"+UsedUnitID+"')");//绑定单位组
        
                         }   
                         else
                         {
                            fnCalculateTotal();
                         }
          
             }
        
        }
    }
 
    closeBilldiv();
}


function TurnToPageBillChoose(pageIndexBillChoose)
{
 if($("#HiddenMoreUnit").val()=="True")
 {
      currentPageIndexBillChoose = pageIndexBillChoose;
       $.ajax({
       type: "POST",
       dataType:"json",
       url:  '../../../Handler/Office/PurchaseManager/SellBillChoose.ashx',
       cache:false,
       data: "Action="+actionBillChoose+"&pageIndex="+pageIndexBillChoose+"&pageCount="
             +pageCountBillChoose+"&OrderBy="+orderByBillChoose+"&OrderByType="+orderByTypeBillChoose+$("#hidSllBllSltCnd").val(),
       beforeSend:function(){AddPop();$("#pageDataList1_PagerBillChoose").hide();},
       
       success: function(msg){
                $("#pageDataListBillChoose tbody").find("tr.newrow").remove();
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxSllBll\" id=\"ChkBoxSllBll"+i+"\" value=\""+item.ID+"\"  />"+"</td>"+
                    "<td height='22' id='SllBllOrdID"+i+"' style='display:none' align='center'>" + item.ID + "</td>"+
                    "<td height='22' id='SllBllOrdNo"+i+"' align='center'>" + item.OrderNo + "</td>"+
                    "<td height='22' id='SllBllSrtNo"+i+"' align='center'>" + item.SortNo + "</td>"+
                    "<td height='22' id='SllBllPrdID"+i+"' style='display:none' align='center'>" + item.ProductID + "</td>"+
                    "<td height='22' id='SllBllPrdNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                    "<td height='22' id='SllBllPrdNam"+i+"' align='center'>" + item.ProductName + "</td>"+
                    "<td height='22' id='SllBllSpecification" + i + "' align='center'>" + item.Specification + "</td>" +
                     "<td height='22' id='SllBllSColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                    "<td height='22' id='SllBllUntID"+i+"' style='display:none' align='center'>" + item.UnitID + "</td>"+
                       "<td height='22' id='SllBllUsedUntID"+i+"' style='display:none' align='center'>" + item.UsedUnitID + "</td>"+ 
                         "<td height='22' id='SllBllUsedPrice"+i+"' style='display:none' align='center'>" + (parseFloat(   item.UsedPrice  )).toFixed($("#HiddenPoint").val()) + "</td>"+ 
                           "<td height='22' id='SllBllUnitPrice"+i+"' style='display:none' align='center'>" + (parseFloat(   item.UnitPrice  )).toFixed($("#HiddenPoint").val()) + "</td>"+ 
                    "<td height='22' id='SllBllUntNam"+i+"' align='center' > " + item.UnitName + "</td>"+
                    "<td height='22' id='SllBllUsedUntNam"+i+"' align='center' >" + item.UsedUnitName + "</td>"+
                    "<td height='22' id='SllBllPrdCnt"+i+"' align='center'   >" +(parseFloat(   item.ProductCount  )).toFixed($("#HiddenPoint").val())+ "</td>"+
                      "<td height='22' id='SllBllPrdUsedUnitCnt"+i+"' align='center'>" +(parseFloat(   item.UsedUnitCount  )).toFixed($("#HiddenPoint").val()) + "</td>"+
                    "<td height='22' id='SllBllSndDat"+i+"' align='center'>"+item.SendDate+"</td>").appendTo($("#pageDataListBillChoose tbody"));
                    }
               });
               ShowPageBar("pageDataList1_PagerBillChoose",
               "<%= Request.Url.AbsolutePath %>",
                {
               style:pagerStyleBillChoose,mark:"pageDataList1MarkBillChoose",
              totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountBillChoose,currentPageIndex:pageIndexBillChoose,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
               
               
                onclick:"TurnToPageBillChoose({pageindex});return false;"}
                );
                
              totalRecordBillChoose = msg.totalCount;
              document.getElementById("TextBillChoose").value=msg.totalCount;
              $("#ShowPageCountBillChoose").val(pageCountBillChoose);
              ShowTotalPage(msg.totalCount,pageCountBillChoose,pageIndexBillChoose,$("#pagecountBillChoose"));
              $("#ToPageBillChoose").val(pageIndexBillChoose);
              },
       error: function() {alert(123)}, 
       complete:function(){hidePopup();    
$("#pageDataList1_PagerBillChoose").show();IfshowBillChoose(document.getElementById("TextBillChoose").value);pageDataList1Choose("pageDataListBillChoose","#E7E7E7","#FFFFFF","#cfc","cfc");}
       });
 }
 else {

     document.getElementById("sspUsedUnitCount").style.display = "none";
     document.getElementById("sspUsedUnitName").style.display = "none";
     document.getElementById("sspCount").value = "数量";
     document.getElementById("sspUnit").value = "单位"; 
     
       currentPageIndexBillChoose = pageIndexBillChoose;
       $.ajax({
       type: "POST",
       dataType:"json",
       url:  '../../../Handler/Office/PurchaseManager/SellBillChoose.ashx',
       cache:false,
       data: "Action="+actionBillChoose+"&pageIndex="+pageIndexBillChoose+"&pageCount="
             +pageCountBillChoose+"&OrderBy="+orderByBillChoose+"&OrderByType="+orderByTypeBillChoose+$("#hidSllBllSltCnd").val(),
       beforeSend:function(){AddPop();$("#pageDataList1_PagerBillChoose").hide();},
       
       success: function(msg){
                $("#pageDataListBillChoose tbody").find("tr.newrow").remove();
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxSllBll\" id=\"ChkBoxSllBll"+i+"\" value=\""+item.ID+"\"  />"+"</td>"+
                    "<td height='22' id='SllBllOrdID"+i+"' style='display:none' align='center'>" + item.ID + "</td>"+
                    "<td height='22' id='SllBllOrdNo"+i+"' align='center'>" + item.OrderNo + "</td>"+
                    "<td height='22' id='SllBllSrtNo"+i+"' align='center'>" + item.SortNo + "</td>"+
                    "<td height='22' id='SllBllPrdID"+i+"' style='display:none' align='center'>" + item.ProductID + "</td>"+
                    "<td height='22' id='SllBllPrdNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                    "<td height='22' id='SllBllPrdNam"+i+"' align='center'>" + item.ProductName + "</td>"+
                    "<td height='22' id='SllBllSpecification" + i + "' align='center'>" + item.Specification + "</td>" +
                     "<td height='22' id='SllBllSColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                    "<td height='22' id='SllBllUntID"+i+"' style='display:none' align='center'>" + item.UnitID + "</td>"+
                    "<td height='22' id='SllBllUntNam"+i+"' align='center'>" + item.UnitName + "</td>"+
                 
                    "<td height='22' id='SllBllPrdCnt"+i+"' align='center'>" +     (parseFloat(   item.ProductCount  )).toFixed($("#HiddenPoint").val())  + "</td>"+
                    "<td height='22' id='SllBllSndDat"+i+"' align='center'>"+item.SendDate+"</td>").appendTo($("#pageDataListBillChoose tbody"));
                    }
               });
               ShowPageBar("pageDataList1_PagerBillChoose",
               "<%= Request.Url.AbsolutePath %>",
                {
               style:pagerStyleBillChoose,mark:"pageDataList1MarkBillChoose",
              totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountBillChoose,currentPageIndex:pageIndexBillChoose,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
               
               
                onclick:"TurnToPageBillChoose({pageindex});return false;"}
                );
                
              totalRecordBillChoose = msg.totalCount;
              document.getElementById("TextBillChoose").value=msg.totalCount;
              $("#ShowPageCountBillChoose").val(pageCountBillChoose);
              ShowTotalPage(msg.totalCount,pageCountBillChoose,pageIndexBillChoose,$("#pagecountBillChoose"));
              $("#ToPageBillChoose").val(pageIndexBillChoose);
              },
       error: function() {alert(123)}, 
       complete:function(){hidePopup();    
$("#pageDataList1_PagerBillChoose").show();IfshowBillChoose(document.getElementById("TextBillChoose").value);pageDataList1Choose("pageDataListBillChoose","#E7E7E7","#FFFFFF","#cfc","cfc");}
       });
       }
}

 
//    function ChangePageCountIndexBillChoose(newPageCountProvider,newPageIndexProvider)
//    {1
//        if(newPageCountProvider <=0 || newPageIndexProvider <= 0 ||  newPageIndexProvider  > ((totalRecordBillChoose-1)/newPageCountProvider)+1 )
//        {
//            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
//            return false;
//        }
//        else
//        {
//        ifshow="0";
//            this.pageCountProvider=parseInt(newPageCountProvider);
//            TurnToPageProvider(parseInt(newPageIndexProvider));
//        }
//    }

function pageDataList1Choose(o,a,b,c,d){
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

function Fun_Search_SellBill(aa)
{
      search="1";
      TurnToPageBillChoose(1);
}
function IfshowBillChoose(count)
{
 
        document.getElementById("divBillChoose").style.display = "block";
        document.getElementById("pagecountBillChoose").style.display = "block";
 
}

function SelectSellBill(retval)
{
    alert(retval);
}
 
function ChangePageCountIndexBillChoose(newPageCountBillChoose,newPageIndexBillChoose)
{
    if(newPageCountBillChoose <=0 || newPageIndexBillChoose <= 0 ||  newPageIndexBillChoose  > ((totalRecordBillChoose-1)/newPageCountBillChoose)+1 )
    {
        return false;
    }
    else
    {
        this.pageCountBillChoose=parseInt(newPageCountBillChoose);
        TurnToPageBillChoose(parseInt(newPageIndexBillChoose));
    }
}
//排序
function OrderBySllBll(orderColum,orderTip)
{
    orderByTypeBillChoose = "ASC"
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        orderByTypeBillChoose = "DESC"
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderByBillChoose = orderColum;
    TurnToPageBillChoose(parseInt(currentPageIndexBillChoose));
}

</script>

