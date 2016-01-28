<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseRequireUC.ascx.cs"
    Inherits="UserControl_PurchaseManager_PurchaseRequireUC" %>
<div id="divPurchaseRequireUC" style="display:none">
<div id="divPurchaseRequireUC2" >
<iframe id="frmPurchaseRequireUC">
</iframe></div>
<div style="border: solid 10px #93BCDD; background: #fff;
    padding: 10px; width: 900px; z-index: 20; position: absolute;top: 50%;
    left: 45%; margin: 5px 0 0 -400px;">
    <a name="pageDataList1Mark"></a>
    <table width="100%">
        <tr>
            <td>
                <a onclick="popPurchaseRequireUC.CloseList()" style="text-align: left; cursor: pointer">
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
                <input type="text" id="txtProdNoPurRqr" class="tdinput" />
            </td>
            <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                物品名称
            </td>
            <td width="15%" bgcolor="#FFFFFF">
                <input type="text" id="txtProdNamePurRqr" class="tdinput" />
            </td>
            <td width="10%" bgcolor="#E7E7E7" align="right">
                需求时间
            </td>
            <td bgcolor="#FFFFFF" style="width: 15%">
                <input type="text" id="txtStartDatePurRqr" readonly="readonly" class="tdinput" onclick="J.calendar.get();"/>
            </td>
            <td width="2%" bgcolor="#E7E7E7" align="right">
                ~
            </td>
            <td bgcolor="#FFFFFF" style="width: 15%">
                <input type="text" id="txtEndDatePurRqr"  class="tdinput" />
                <input type="text" id="hidPurRqrSltCnd" style="display:none"  class="tdinput" />
            </td>
        </tr>
        <tr>
            <td colspan="8" align="center" bgcolor="#FFFFFF">
                <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                    onclick='fnGetPurRqr()' id="btn_search" />
                <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;'
                    onclick="fnFillPurRqr();" id="imgsure3" />
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListPurchaseRequireUC"
        bgcolor="#999999">
        <tbody>
            <tr>
                <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    选择
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseRequireUC.orderByP('ProductNo','RR1');return false;">
                        物品编号<span id="RR1" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseRequireUC.orderByP('ProductName','RR2');return false;">
                        物品名称<span id="RR2" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseRequireUC.orderByP('Specification','RR3');return false;">
                        规格<span id="RR3" class="orderTip"></span></div>
                </th>
                 <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseRequireUC.orderByP('ColorName','RRColorName');return false;">
                        颜色<span id="RRColorName" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseRequireUC.orderByP('UnitName','RR4');return false;">
                        单位<span id="RR4" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseRequireUC.orderByP('UnitPrice','RR5');return false;">
                        单价<span id="RR5" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseRequireUC.orderByP('WantingNum','RR6');return false;">
                        需求数量<span id="RR6" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseRequireUC.orderByP('OrderCount','RR7');return false;">
                        已计划数量<span id="RR7" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseRequireUC.orderByP('RequireDate','RR8');return false;">
                        需求日期<span id="RR8" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchaseRequireUC.orderByP('FromBillNo','RR9');return false;">
                        源单编号<span id="RR9" class="orderTip"></span></div>
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
                            <div id="PageCountPurchaseRequireUC">
                            </div>
                        </td>
                        <td height="28" align="right">
                            <div id="pageDataList1_PagerPurchaseRequireUC" class="jPagerBar">
                            </div>
                        </td>
                        <td height="28" align="right">
                            <div id="divpagePurchaseRequireUC">
                                <input type="text" id="TotalRecordPurchaseRequireUC" style="display: none" />
                                <span id="TotalPagePurchaseRequireUC"></span>每页显示
                                <input type="text" id="PerPageCountPurchaseRequireUC" style="width: 31px" />条 转到第
                                <input name="ToPagePurchaseRequireUC" type="text" id="ToPagePurchaseRequireUC" style="width: 31px" />页
                                <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                    width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexPurchaseRequireUC($('#PerPageCountPurchaseRequireUC').val(),$('#ToPagePurchaseRequireUC').val());" />
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
var popPurchaseRequireUC = new Object();

popPurchaseRequireUCpageCount = 10;
popPurchaseRequireUC.totalRecord = 0;
popPurchaseRequireUCpagerStyle = "flickr";
popPurchaseRequireUCcurrentPageIndex = 1;
popPurchaseRequireUC.action = "";
popPurchaseRequireUC.orderBy = "RequireDate";
popPurchaseRequireUC.orderByType = "desc";

popPurchaseRequireUC.ShowList = function()
{
    document.getElementById("divPurchaseRequireUC").style.display='block';
    openRotoscopingDiv(true,"divPurchaseRequireUC2","frmPurchaseRequireUC");
    
    popPurchaseRequireUC.TurnToPage(1)
}

popPurchaseRequireUC.CloseList = function()
{
    document.getElementById("divPurchaseRequireUC").style.display='none';
    closeRotoscopingDiv(true,"divPurchaseRequireUC2");
}

function IfshowPurchaseRequireUC(count)
{
    if(count=="0")
    {
        document.getElementById("divpagePurchaseRequireUC").style.display = "none";
        document.getElementById("PageCountPurchaseRequireUC").style.display = "none";
    }
    else
    {
        document.getElementById("divpagePurchaseRequireUC").style.display="";
        document.getElementById("PageCountPurchaseRequireUC").style.display = "";
    }
}
popPurchaseRequireUC.orderByP = function(orderColum,orderTip)
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
   orderBy = orderColum+"_"+ordering;
   popPurchaseRequireUC.orderBy=orderColum;
   popPurchaseRequireUC.orderByType=ordering;
  popPurchaseRequireUC.TurnToPage(1);
}

function fnGetPurRqrSltCnd()
{
    var str = "";
    str += "&ProductNo="+escape($("#txtProdNoPurRqr").val());
    str += "&ProductName="+escape($("#txtProdNamePurRqr").val());
    str += "&StartDate="+escape($("#txtStartDatePurRqr").val());
    str += "&EndDate="+escape($("#txtEndDatePurRqr").val());
    $("#hidPurRqrSltCnd").val(str);
    return str;
}

function fnFillPurRqr()
{
    var ck = document.getElementsByName("ChkBoxPurRqr");
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {//选中某行，将该行的值赋到界面上
        var SProductNo=$("#PurRqrProductNo"+i).html();
      var SFromBillNo=$("#PurRqrFromBillNo"+i).html();
        if (ExistFromRequire(SFromBillNo,SProductNo))
        {
        continue ;
        }
            var rowID = AddDtlSSignRow();
            document.getElementById("DtlSProductID"+rowID).value = $("#PurRqrProductID"+i).html();
            document.getElementById("DtlSProductNo"+rowID).value = $("#PurRqrProductNo"+i).html();
            document.getElementById("DtlSProductName"+rowID).value = $("#PurRqrProductName"+i).html();
            document.getElementById("DtlSSpecification"+rowID).value = $("#PurRqrSpecification"+i).html();
            document.getElementById("DtlSUnitID"+rowID).value = $("#PurRqrUnitID"+i).html();
            document.getElementById("DtlSUnitName"+rowID).value = $("#PurRqrUnitName"+i).html();
            document.getElementById("DtlSUnitPrice"+rowID).value = $("#PurRqrUnitPrice"+i).html();
            document.getElementById("DtlSRequireCount"+rowID).value = parseFloat($("#PurRqrRequireCount"+i).html())-parseFloat($("#PurRqrOrderCount"+i).html());
            document.getElementById("DtlSPlanCount"+rowID).value = parseFloat($("#PurRqrRequireCount"+i).html())-parseFloat($("#PurRqrOrderCount"+i).html());
            document.getElementById("DtlSRequireDate"+rowID).value = $("#PurRqrRequireDate"+i).html();
            document.getElementById("DtlSPlanTakeDate"+rowID).value = $("#PurRqrRequireDate"+i).html();
            document.getElementById("DtlSFromBillID"+rowID).value = $("#PurRqrFromBillID"+i).html();
            document.getElementById("DtlSFromBillNo"+rowID).value = $("#PurRqrFromBillNo"+i).html();
            document.getElementById("DtlSColor" + rowID).value = $("#PurRqrColorName" + i).html(); 
            var UnitPrice =  parseFloat ( $("#PurRqrUnitPrice"+i).html()).toFixed($("#HiddenPoint").val());
            var RequireCount =   parseFloat ( $("#PurRqrRequireCount"+i).html()).toFixed($("#HiddenPoint").val());
          
            document.getElementById("DtlSTotalPrice"+rowID).value =   parseFloat (  UnitPrice*RequireCount).toFixed($("#HiddenPoint").val());//总金额

            //合计信息
            document.getElementById("txtPlanCnt").value =       ( parseInt(document.getElementById("txtPlanCnt").value)+parseInt(document.getElementById("DtlSPlanCount"+rowID).value)).toFixed($("#HiddenPoint").val());
            document.getElementById("txtPlanMoney").value =     ( parseFloat(document.getElementById("txtPlanMoney").value)+parseInt(document.getElementById("DtlSPlanCount"+rowID).value)*parseFloat(document.getElementById("DtlSUnitPrice"+rowID).value)).toFixed($("#HiddenPoint").val());
  if($("#HiddenMoreUnit").val()=="False")
            {
                 fnMergeDetail();
            }
            else
            {
            
             GetUnitGroupSelectEx($("#PurRqrProductID"+i).html(),"InUnit","SignItem_TD_UnitID_Select" + rowID,"ChangeUnit(this.id,"+rowID+","+UnitPrice+")","unitdiv" + rowID,'',"FillApplyContent("+rowID+","+UnitPrice+","+RequireCount+",'','','Require')");//绑定单位组
            }

        }
    }
 
    popPurchaseRequireUC.CloseList();
}


//popPurchaseRequireUC.Fill = function(Productid,Productno,Productname,Specification,UnitID,UnitName,UnitPrice
//,RequireCount,RequireDate,RequireReasonID,RequireReasonName,FromBillID,FromBillNo)
//{
//    var rowID = AddDtlSSignRow();
//    
//    document.getElementById("DtlSProductID"+rowID).value = Productid;
//    document.getElementById("DtlSProductNo"+rowID).value = Productno;
//    document.getElementById("DtlSProductName"+rowID).value = Productname;
//    document.getElementById("DtlSSpecification"+rowID).value = Specification;
//    document.getElementById("DtlSUnitID"+rowID).value = UnitID;
//    document.getElementById("DtlSUnitName"+rowID).value = UnitName;
//    document.getElementById("DtlSUnitPrice"+rowID).value = FormatAfterDotNumber(UnitPrice,2);
//    document.getElementById("DtlSRequireCount"+rowID).value = FormatAfterDotNumber(RequireCount,2);
//    document.getElementById("DtlSPlanCount"+rowID).value = FormatAfterDotNumber(RequireCount,2);
//    document.getElementById("DtlSRequireDate"+rowID).value = RequireDate;
//    document.getElementById("DtlSPlanTakeDate"+rowID).value = RequireDate;
//    document.getElementById("DtlSFromBillID"+rowID).value = FromBillID;
//    document.getElementById("DtlSFromBillNo"+rowID).value = FromBillNo;
//    
//    document.getElementById("DtlSTotalPrice"+rowID).value = FormatAfterDotNumber(parseFloat(UnitPrice)*parseFloat(RequireCount),2);//总金额
//    
//    //合计信息
//    document.getElementById("txtPlanCnt").value = FormatAfterDotNumber(parseInt(document.getElementById("txtPlanCnt").value)+parseInt(document.getElementById("DtlSPlanCount"+rowID).value),2);
//    document.getElementById("txtPlanMoney").value = FormatAfterDotNumber(parseFloat(document.getElementById("txtPlanMoney").value)+parseInt(document.getElementById("DtlSPlanCount"+rowID).value)*parseFloat(document.getElementById("DtlSUnitPrice"+rowID).value),2);
////    popPurchaseRequireUC.CloseList();
//fnMergeDetail();
//}

//改变每页记录数及跳至页数
function ChangePageCountIndexPurchaseRequireUC(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((popPurchaseRequireUC.totalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        popPurchaseRequireUCpageCount=parseInt(newPageCount);
        popPurchaseRequireUC.TurnToPage(parseInt(newPageIndex));
    }
}



function fnGetPurRqr()
{
    popPurchaseRequireUC.TurnToPage(1);
}

popPurchaseRequireUC.TurnToPage = function(pageIndex)
{
    popPurchaseRequireUCcurrentPageIndex = pageIndex;
    
    var Params = "pageIndex="+popPurchaseRequireUCcurrentPageIndex
                +"&pageCount="+popPurchaseRequireUCpageCount
                +"&action="+popPurchaseRequireUC.action
                +"&orderby="+popPurchaseRequireUC.orderBy
                +"&OrderByType="+popPurchaseRequireUC.orderByType
                +fnGetPurRqrSltCnd();
    
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/UC/PurchaseRequireUC.ashx',//目标地址
           cache:false,
           data: Params,//数据
           beforeSend:function(){$("#pageDataList1_PagerProductInfoUC").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    
                    $("#pageDataListPurchaseRequireUC tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        if(item.ProductID != null && item.ProductID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxPurRqr\" id=\"ChkBoxPurRqr"+i+"\" value=\""+item.ID+"\"  />"+"</td>"+
                        "<td height='22' style='display:none' id='PurRqrProductID"+i+"' align='center'>" + item.ProductID + "</td>"+
                        "<td height='22' id='PurRqrProductNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                        "<td height='22' id='PurRqrProductName"+i+"' align='center'>" + item.ProductName + "</td>"+
                        "<td height='22' id='PurRqrSpecification" + i + "' align='center'>" + item.Specification + "</td>" +
                        "<td height='22' id='PurRqrColorName" + i + "' align='center'>" + item.ColorName + "</td>" + 
                        "<td height='22' style='display:none' id='PurRqrUnitID"+i+"' align='center'>" + item.UnitID + "</td>"+
                        "<td height='22' id='PurRqrUnitName"+i+"' align='center'>" + item.UnitName + "</td>"+
                        "<td height='22' id='PurRqrUnitPrice"+i+"' align='center'>" + FormatAfterDotNumber(item.UnitPrice,2) + "</td>"+
                        "<td height='22' id='PurRqrRequireCount"+i+"' align='center'>" + FormatAfterDotNumber(item.WantingNum,2) + "</td>"+
                        "<td height='22' id='PurRqrOrderCount"+i+"' align='center'>" + FormatAfterDotNumber(item.OrderCount,2) + "</td>"+
                        "<td height='22' id='PurRqrRequireDate"+i+"' align='center'>" + item.RequireDate + "</td>"+
                        "<td height='22' style='display:none' id='PurRqrFromBillID"+i+"' align='center'>" + item.ID + "</td>"+
                        "<td height='22' id='PurRqrFromBillNo"+i+"' align='center'>" + item.FromBillNo + "</td>").appendTo($("#pageDataListPurchaseRequireUC tbody"));
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerPurchaseRequireUC",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:popPurchaseRequireUCpagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:popPurchaseRequireUCpageCount,currentPageIndex:popPurchaseRequireUCcurrentPageIndex,
                    noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"popPurchaseRequireUC.TurnToPage({pageindex});return false;"}//[attr]
                    );
                  popPurchaseRequireUC.totalRecord = msg.totalCount;
                  document.getElementById("TotalRecordPurchaseRequireUC").value=msg.totalCount;
                  $("#PerPageCountPurchaseRequireUC").val(popPurchaseRequireUCpageCount);
                  ShowTotalPage(msg.totalCount,popPurchaseRequireUCpageCount,popPurchaseRequireUCcurrentPageIndex,$("#PageCountPurchaseRequireUC"));
                  $("#ToPagePurchaseRequireUC").val(popPurchaseRequireUCcurrentPageIndex);
                  },
           error: function() {},
           complete:function(){hidePopup();$("#pageDataList1_PagerPurchaseRequireUC").show();IfshowPurchaseRequireUC(document.getElementById("TotalRecordPurchaseRequireUC").value);pageDataList1("pageDataListPurchaseRequireUC","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}
</script>

