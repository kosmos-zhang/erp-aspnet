<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MaterialChoose.ascx.cs" Inherits="UserControl_MaterialChoose" %>
<a name="pageDataList1MarkMaterialChoose"></a>
<div id="layoutMaterialChoose">
 
    <iframe id="frmMaterialChoose" style="filter: Alpha(opacity=0); height: 500px;border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 800px; z-index: 1000; position: absolute; top: 50%; left: 50%;display:none;
        margin: 5px 0 0 -400px;"></iframe>
    <div id="divMaterialChoose" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 800px; z-index: 1001; position: absolute; top: 50%; left: 50%;display:none;
        margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr align="left">
                <td>
                    <a onclick="closeMaterialdiv()" style="text-align: left; cursor: pointer"><img  src="../../../images/Button/Bottom_btn_close.jpg"/></a>
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
                    <input type="text" id="txtProdNoMtlChs" class="tdinput" />
                </td>
                <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                    物品名称
                </td>
                <td width="15%" bgcolor="#FFFFFF">
                    <input type="text" id="txtProdNameMtlChs" class="tdinput" />
                </td>
                <td width="10%" bgcolor="#E7E7E7" align="right">
                    需求时间
                </td>
                <td bgcolor="#FFFFFF" style="width: 15%">
                    <input type="text" id="txtStartDateMtlChs" class="tdinput" readonly="readonly" onclick="WdatePicker()" />
                </td>
                <td width="2%" bgcolor="#E7E7E7" align="right">
                    ~
                </td>
                <td bgcolor="#FFFFFF" style="width: 15%">
                    <input type="text" id="txtEndDateMtlChs" class="tdinput" readonly="readonly" onclick="WdatePicker()" />
                    <input type="text" id="hidMtlChsSltCnd" style="display: none" class="tdinput" />
                </td>
            </tr>
            <tr>
                <td colspan="8" align="center" bgcolor="#FFFFFF">
                    <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                        onclick='fnGetMtlChs()' id="btn_search" />
                    <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;'
                        onclick="fnFillMtlChs();" id="imgsure" />
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListMaterialChoose"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByMtlChs('MRPNo','MtlChsMRPNo');return false;">
                            物料需求计划编号<span id="MtlChsMRPNo" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByMtlChs('SortNo','MtlChsSrtNo');return false;">
                            行号<span id="MtlChsSrtNo" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByMtlChs('ProductNo','MtlChsPrdNo');return false;">
                            物品编号<span id="MtlChsPrdNo" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByMtlChs('ProductName','MtlChsPrdNam');return false;">
                            物品<span id="MtlChsPrdNam" class="orderTip"></span></div>
                    </th>
                     <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByMtlChs('ColorName','MtlChsColorName');return false;">
                            颜色<span id="MtlChsColorName" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByMtlChs('Unit','MtlChsUntNam');return false;">
                           <span id="sspSDSDUnit">    基本单位</span><span id="MtlChsUntNam" class="orderTip"></span></div>
                    </th>
                    
                     <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"   id="sspDDUsedUnitName">
                        <div class="orderClick" onclick="OrderByMtlChs('UsedUnitName','MtlChsUsedUnitName');return false;">
                            单位<span id="MtlChsUsedUnitName" class="orderTip"></span></div>
                    </th>
                    
                    
                    
                    
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByMtlChs('PlanCount','MtlChsCnt');return false;">
                         <span id="sspDSDSDCount">      基本数量</span><span id="MtlChsCnt" class="orderTip"></span></div>
                    </th>
                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspDDDUsedUnitCount">
                        <div class="orderClick" onclick="OrderByMtlChs('UsedUnitCount','MtlChsUsedUnitCount');return false;">
                            数量<span id="MtlChsUsedUnitCount" class="orderTip"></span></div>
                    </th>
                    
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByMtlChs('ProcessedCount','MtlChsProCnt');return false;">
                            已生成计划数量<span id="MtlChsProCnt" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByMtlChs('PlanDate','MtlChsPlnDat');return false;">
                            需求日期<span id="MtlChsPlnDat" class="orderTip"></span></div>
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
                                <div id="pagecountMaterialC">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_PagerMaterialChoose" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpage">
                                    <input name="TextMaterialChoose" type="text" id="TextMaterialChoose" style="display: none" />
                                    <span id="pageDataList1_Total"></span>每页显示
                                    <input name="ShowPageCountMaterialChoose" type="text" id="ShowPageCountMaterialChoose"
                                        style="width: 26px" />条 转到第
                                    <input name="ToPageMaterialChoose" type="text" id="ToPageMaterialChoose" style="width: 22px" />页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexMaterialChoose($('#ShowPageCountMaterialChoose').val(),$('#ToPageMaterialChoose').val());" />
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

<script language="javascript"  type="text/javascript">
var popMaterialObj=new Object();
popMaterialObj.InputObj = null;

    var pageCountMaterialChoose = 10;//每页计数
    var totalRecordMaterialChoose= 0;
    var pagerStyleMaterialChoose = "flickr";//jPagerBar样式
    
    var currentPageIndexMaterialChoose = 1;
    var actionMaterialChoose = "Select";//操作
    var orderByMaterialChoose = "PlanDate";//排序字段
    var orderByTypeMaterialChoose = "DESC";//排序类型
    
popMaterialObj.ShowList = function(objInput)
{
    popMaterialObj.InputObj = objInput;
    document.getElementById('divMaterialChoose').style.display='block';
    document.getElementById("frmMaterialChoose").style.display="block";
    TurnToPageMaterialChoose(1);
}

function closeMaterialdiv()
{
    document.getElementById("divMaterialChoose").style.display="none";
    document.getElementById("frmMaterialChoose").style.display="none";
}


//排序
function OrderByMtlChs(orderColum,orderTip)
{
    orderByTypeMaterialChoose = "ASC"
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        orderByTypeMaterialChoose = "DESC"
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderByMaterialChoose = orderColum;
    TurnToPageMaterialChoose(parseInt(currentPageIndexMaterialChoose));
}

//改变每页记录数及跳至页数
//    function ChangePageCountIndexMaterialChoose(newPageCountProvider,newPageIndexProvider)
//    {2
//        if(newPageCountProvider <=0 || newPageIndexProvider <= 0 ||  newPageIndexProvider  > ((totalRecordMaterialChoose-1)/newPageCountProvider)+1 )
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


function fnGetMtlChs()
{
    var str = "";
    str += "&ProductNo="+escape($.trim($("#txtProdNoMtlChs").val()));
    str += "&ProductName="+escape($.trim($("#txtProdNameMtlChs").val()));
    str += "&StartDate="+escape($.trim($("#txtStartDateMtlChs").val()));
    str += "&EndDate="+escape($.trim($("#txtEndDateMtlChs").val()));
    $("#hidMtlChsSltCnd").val(str);
    
    
    TurnToPageMaterialChoose(1);
}

function fnFillMtlChs()
{ 
    var ck = document.getElementsByName("ChkBoxMtlChs");
    for( var i = 0; i < ck.length; i++ )
    {
        if (ck[i].checked)
        {//选中某行，将该行的值赋到界面上
           var Mtlpro=  $("#MtlChsMRPNo"+i).html()
           var Mtlbll=  $("#MtlChsSrtNo"+i).html();
              if (ExistFromBill(Mtlpro,Mtlbll ))
              {
         continue ;
              }
              var rowID = BillChooseFillSignRow($("#MtlChsPrdID" + i).html(), $("#MtlChsPrdNo" + i).html(), $("#MtlChsPrdNam" + i).html(), '', $("#MtlChsUntID" + i).html(), $("#MtlChsUnt" + i).html(), FormatAfterDotNumber(parseFloat($("#MtlChsPlnCnt" + i).html()) - parseFloat($("#MtlChsPrcCnt" + i).html()), 2), $("#MtlChsPlnDat" + i).html(), 0, $("#MtlChsMRPNo" + i).html(), $("#MtlChsID" + i).html(), $("#MtlChsSrtNo" + i).html(), $("#MtlChsPrColorName" + i).html())
              if($("#HiddenMoreUnit").val()=="True")
                          {
                  
                   
                   var sd=parseFloat($("#MtlChsPrcCnt"+i).html());
                   
                          var BaseCount=          (         parseFloat($("#MtlChsPlnCnt"+i).html())- parseFloat($("#MtlChsPrcCnt"+i).html())).toFixed($("#HiddenPoint").val());
                     var ProductCount=          (parseFloat($("#MtlChsUsedCount"+i).html())).toFixed($("#HiddenPoint").val());
                          var UsedId=        $("#MtlChsUsedUnitID"+i).html();
        GetUnitGroupSelectEx($("#MtlChsPrdID"+i).html(),"InUnit","SignItem_TD_UnitID_Select" + rowID,"ChangeUnit(this.id,"+rowID+")","unitdiv" + rowID,'',"FillApplyContent("+rowID+","+BaseCount +","+ProductCount +",'Matrial')");//绑定单位组
        
                         }   
                         else
                         {
                            fnCalculateTotal();
                         }
        }
    }
 
    closeMaterialdiv();
        hidePopup();
}
function TurnToPageMaterialChoose(pageIndexMaterialChoose)
{
     if($("#HiddenMoreUnit").val()=="True")
     {
         currentPageIndexMaterialChoose = pageIndexMaterialChoose;

       $.ajax({
       type: "POST",
       dataType:"json",
       url:  '../../../Handler/Office/PurchaseManager/MaterialChoose.ashx',
       cache:false,
       data: "Action="+actionMaterialChoose+"&pageIndex="+pageIndexMaterialChoose+"&pageCount="
             +pageCountMaterialChoose+"&OrderBy="+orderByMaterialChoose+"&OrderByType="+orderByTypeMaterialChoose+$("#hidMtlChsSltCnd").val(),
       beforeSend:function(){AddPop();$("#pageDataList1_PagerMaterialChoose").hide();},
       
       success: function(msg){
                $("#pageDataListMaterialChoose tbody").find("tr.newrow").remove();
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"checkbox\" name=\"ChkBoxMtlChs\" id=\"ChkBoxMtlChs" + i + "\" value=\"" + item.ID + "\" onclick=\"return;FillByMRP('" + item.ID + "','" + item.MRPNo + "','" + item.SortNo + "','" + item.ProductID + "','" + item.ProductNo + "','" + item.ProductName + "','" + item.UnitID + "','" + item.Unit + "','" + item.PlanCount + "','" + item.ProcessedCount + "','" + item.ColorName + "');\" />" + "</td>" +
                    "<td height='22' id='MtlChsID"+i+"'  style='display:none' align='center'>" + item.ID + "</td>"+
                    "<td height='22' id='MtlChsMRPNo"+i+"' align='center'>" + item.MRPNo + "</td>"+
                    "<td height='22' id='MtlChsSrtNo"+i+"' align='center'>" + item.SortNo + "</td>"+
                    "<td height='22' id='MtlChsPrdID"+i+"' style='display:none' align='center'>" + item.ProductID + "</td>"+
                    "<td height='22' id='MtlChsPrdNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                    "<td height='22' id='MtlChsPrdNam" + i + "' align='center'>" + item.ProductName + "</td>" +
                      "<td height='22' id='MtlChsPrColorName" + i + "' align='center'>" + item.ColorName + "</td>" + 
                    "<td height='22' id='MtlChsUntID"+i+"' style='display:none' align='center'>" + item.UnitID + "</td>"+
                       "<td height='22' id='MtlChsUsedUnitID"+i+"' style='display:none' align='center'>" + item.UsedUnitID + "</td>"+
                    "<td height='22' id='MtlChsUnt" + i + "' align='center'>" + item.Unit + "</td>" +
                     "<td height='22' id='MtlChsUsedUnitName" + i + "' align='center'>" + item.UsedUnitName + "</td>" + 
                    "<td height='22' id='MtlChsPlnCnt"+i+"' align='center'   >" +     (parseFloat( item.PlanCount)).toFixed($("#HiddenPoint").val())+ "</td>"+
                       "<td height='22' id='MtlChsUsedCount"+i+"' align='center'  >" +   (parseFloat( item.UsedUnitCount)).toFixed($("#HiddenPoint").val())+ "</td>"+ 
                    "<td height='22' id='MtlChsPrcCnt"+i+"' align='center'>"+   (parseFloat( item.ProcessedCount)).toFixed($("#HiddenPoint").val())+"</td>"+
                    "<td height='22' id='MtlChsPlnDat"+i+"' align='center'>"+item.PlanDate+"</td>").appendTo($("#pageDataListMaterialChoose tbody"));
               });
               ShowPageBar("pageDataList1_PagerMaterialChoose",
               "<%= Request.Url.AbsolutePath %>",
                {style:pagerStyleMaterialChoose,mark:"pageDataList1MarkMaterialChoose",
              totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountMaterialChoose,currentPageIndex:pageIndexMaterialChoose,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
               
                
                
                onclick:"TurnToPageMaterialChoose({pageindex});return false;"}
                );
              totalRecordMaterialChoose = msg.totalCount;
              document.getElementById("TextMaterialChoose").value=msg.totalCount;
              $("#ShowPageCountMaterialChoose").val(pageCountMaterialChoose);
              ShowTotalPage(msg.totalCount,pageCountMaterialChoose,pageIndexMaterialChoose,$("#pagecountMaterialC"));
              $("#ToPageMaterialChoose").val(pageIndexMaterialChoose);
              },
       error: function() {}, 
       complete:function()
       {$("#pageDataList1_PagerMaterialChoose").show();
       IfshowMaterialChoose(document.getElementById("TextMaterialChoose").value);
       pageDataList1("pageDataListMaterialChoose","#E7E7E7","#FFFFFF","#cfc","cfc");
       hidePopup();}
       });
     }
     else {

         document.getElementById("sspDDDUsedUnitCount").style.display = "none";
         document.getElementById("sspDDUsedUnitName").style.display = "none";
         document.getElementById("sspDSDSDCount").innerHTML = "数量";
         document.getElementById("sspSDSDUnit").innerHTML = "单位"; 
       currentPageIndexMaterialChoose = pageIndexMaterialChoose;

       $.ajax({
       type: "POST",
       dataType:"json",
       url:  '../../../Handler/Office/PurchaseManager/MaterialChoose.ashx',
       cache:false,
       data: "Action="+actionMaterialChoose+"&pageIndex="+pageIndexMaterialChoose+"&pageCount="
             +pageCountMaterialChoose+"&OrderBy="+orderByMaterialChoose+"&OrderByType="+orderByTypeMaterialChoose+$("#hidMtlChsSltCnd").val(),
       beforeSend:function(){AddPop();$("#pageDataList1_PagerMaterialChoose").hide();},
       
       success: function(msg){
                $("#pageDataListMaterialChoose tbody").find("tr.newrow").remove();
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"checkbox\" name=\"ChkBoxMtlChs\" id=\"ChkBoxMtlChs" + i + "\" value=\"" + item.ID + "\" onclick=\"return;FillByMRP('" + item.ID + "','" + item.MRPNo + "','" + item.SortNo + "','" + item.ProductID + "','" + item.ProductNo + "','" + item.ProductName + "','" + item.UnitID + "','" + item.Unit + "','" + item.PlanCount + "','" + item.ProcessedCount + "','" + item.ColorName + "');\" />" + "</td>" +
                    "<td height='22' id='MtlChsID"+i+"'  style='display:none' align='center'>" + item.ID + "</td>"+
                    "<td height='22' id='MtlChsMRPNo"+i+"' align='center'>" + item.MRPNo + "</td>"+
                    "<td height='22' id='MtlChsSrtNo"+i+"' align='center'>" + item.SortNo + "</td>"+
                    "<td height='22' id='MtlChsPrdID"+i+"' style='display:none' align='center'>" + item.ProductID + "</td>"+
                    "<td height='22' id='MtlChsPrdNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                    "<td height='22' id='MtlChsPrdNam" + i + "' align='center'>" + item.ProductName + "</td>" +
                       "<td height='22' id='MtlChsPrColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                    "<td height='22' id='MtlChsUntID"+i+"' style='display:none' align='center'>" + item.UnitID + "</td>"+
                    "<td height='22' id='MtlChsUnt"+i+"' align='center'>" + item.Unit + "</td>"+
                    "<td height='22' id='MtlChsPlnCnt"+i+"' align='center'>" +     (parseFloat( item.PlanCount)).toFixed($("#HiddenPoint").val())+ "</td>"+ 
                    "<td height='22' id='MtlChsPrcCnt"+i+"' align='center'>"+  (parseFloat( item.ProcessedCount)).toFixed($("#HiddenPoint").val())+"</td>"+
                    "<td height='22' id='MtlChsPlnDat"+i+"' align='center'>"+item.PlanDate+"</td>").appendTo($("#pageDataListMaterialChoose tbody"));
               });
               ShowPageBar("pageDataList1_PagerMaterialChoose",
               "<%= Request.Url.AbsolutePath %>",
                {style:pagerStyleMaterialChoose,mark:"pageDataList1MarkMaterialChoose",
              totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountMaterialChoose,currentPageIndex:pageIndexMaterialChoose,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
               
                
                
                onclick:"TurnToPageMaterialChoose({pageindex});return false;"}
                );
              totalRecordMaterialChoose = msg.totalCount;
              document.getElementById("TextMaterialChoose").value=msg.totalCount;
              $("#ShowPageCountMaterialChoose").val(pageCountMaterialChoose);
              ShowTotalPage(msg.totalCount,pageCountMaterialChoose,pageIndexMaterialChoose,$("#pagecountMaterialC"));
              $("#ToPageMaterialChoose").val(pageIndexMaterialChoose);
              },
       error: function() {}, 
       complete:function()
       {$("#pageDataList1_PagerMaterialChoose").show();
       IfshowMaterialChoose(document.getElementById("TextMaterialChoose").value);
       pageDataList1("pageDataListMaterialChoose","#E7E7E7","#FFFFFF","#cfc","cfc");
       hidePopup();}
       });
       }
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

function Fun_Search_MaterialChoose(aa)
{
      search="1";
      TurnToPageMaterialChoose(1);
}
function IfshowMaterialChoose(count)
{
//    if(count=="0")
//    {
//        document.getElementById("divMaterialChoose").style.display = "none";
//        document.getElementById("pagecountMaterialC").style.display = "none";
//    }
//    else
//    {
        document.getElementById("divMaterialChoose").style.display = "block";
        document.getElementById("pagecountMaterialC").style.display = "block";
//    }
}

function SelectMaterial(retval)
{
    alert(retval);
}

//改变每页记录数及跳至页数
function ChangePageCountIndexMaterialChoose(newPageCountMaterialChoose,newPageIndexMaterialChoose)
{ 
    if(newPageCountMaterialChoose <=0 || newPageIndexMaterialChoose <= 0 ||  newPageIndexMaterialChoose  > ((totalRecordMaterialChoose-1)/newPageCountMaterialChoose)+1 )
    {
        return false;
    }
    else
    {
        this.pageCountMaterialChoose=parseInt(newPageCountMaterialChoose);
        TurnToPageMaterialChoose(parseInt(newPageIndexMaterialChoose));
    }
}
//排序
function OrderBy(orderColum,orderTip)
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
    orderBy = orderColum+"_"+ordering;
    TurnToPageMaterialChoose(1);
}
</script>

