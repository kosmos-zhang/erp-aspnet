<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchasePlanUC.ascx.cs"
    Inherits="UserControl_PurchaseManager_PurchasePlanUC" %>
<div id="divPurchasePlanUC" style="display:none">
<div id="divPurchasePlanUC2" >
<iframe id="frmPurchasePlanUC" >
</iframe></div>
<div  style="border: solid 10px #93BCDD; background: #fff;
     padding: 10px; width: 1000px; z-index: 20; position: absolute;
    top: 50%; left: 40%; margin: 5px 0 0 -400px;">
    <table width="100%">
        <tr>
            <td>
                <a onclick="popPurchasePlanUC.CloseList()" style="text-align: left; cursor: pointer">
                   <img  src="../../../images/Button/Bottom_btn_close.jpg"/></a>
            </td>
        </tr>
    </table>
    <table>
    <tr class="table-item"> 
        <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
            物品编号
        </td>
        <td width="15%" bgcolor="#FFFFFF">
            <input type="text" id="txtProdNoPurPlan" class="tdinput" />
        </td>
        <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
            物品名称
        </td>
        <td width="15%" bgcolor="#FFFFFF">
            <input type="text" id="txtProdNamePurPlan" class="tdinput" />
        </td>
        <td width="10%" bgcolor="#E7E7E7" align="right">
            需求时间
        </td>
        <td bgcolor="#FFFFFF" style="width: 15%">
            <input type="text" id="txtStartDatePurPlan" class="tdinput" readonly="readonly" onclick="WdatePicker()"/>
        </td>
        <td width="2%" bgcolor="#E7E7E7" align="right">
            ~
        </td>
        <td bgcolor="#FFFFFF" style="width: 15%">
            <input type="text" id="txtEndDatePurPlan" class="tdinput" readonly="readonly" onclick="WdatePicker()"/>
            <input type="text" id="hidPurPlanSltCnd" style="display: none" class="tdinput" />
        </td>
    </tr>
    <tr>
            <td colspan="8" align="center" bgcolor="#FFFFFF">
                <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;  float:center '
                    onclick='fnGetPurPlan()' id="btn_search" />
                <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;   '
                    onclick="fnFillPurPln();" id="imgsure2" />
            </td>
        </tr>
    </table>
    
    
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListPurchasePlanUC"
        bgcolor="#999999">
        <tbody>
            <tr>
                <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    选择<input type="checkbox" name="ChkBoxPurPlan" id="ChkBoxPurPlan" onclick="SelectAllPurPlan()" />
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('ProviderName','PurchasePlanUCProviderName');return false;">
                        供应商名称<span id="PurchasePlanUCProviderName" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('ProductNo','PurchasePlanUCProductNo');return false;">
                        物品编号<span id="PurchasePlanUCProductNo" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('ProductName','PurchasePlanUCProductName');return false;">
                        物品名称<span id="PurchasePlanUCProductName" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('Specification','PurchasePlanUCSpecification');return false;">
                        规格<span id="PurchasePlanUCSpecification" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('ColorName','PurchasePlanUCSColorName');return false;">
                        颜色<span id="PurchasePlanUCSColorName" class="orderTip"></span></div>
                </th>
                
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspPurchasePlanUsedUnitCount">
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('UsedUnitCount','PurchaseUsedUnitCount');return false;">
                        数量<span id="PurchaseUsedUnitCount" class="orderTip"></span></div>
                </th>
                  <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('ProductCount','PurchasePlanUCProductCount');return false;">
                      <span id="sspPurchasePlanCount">  基本数量</span><span id="PurchasePlanUCProductCount" class="orderTip"></span></div>
                </th>
                
                
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('OrderCount','PurchasePlanUCOrderCount');return false;">
                        已订购数量<span id="PurchasePlanUCOrderCount" class="orderTip"></span></div>
                </th>
                
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" >
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('UnitName','PurchasePlanUCUnitName');return false;">
                       <span id="sspPurchasePlanUnit">基本单位</span> <span id="PurchasePlanUCUnitName" class="orderTip"></span></div>
                </th>
                
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspPurchasePlanUsedUnitName">
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('UsedUnitName','PurchasePlanUsedUnitName');return false;">
                        单位<span id="PurchasePlanUsedUnitName" class="orderTip"></span></div>
                </th>
                
                 
                  <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('UnitPrice','PurchasePlanUCUnitPrice');return false;">
                   <span id="sspPurchasePlanUniprice">基本单价 </span>    <span id="PurchasePlanUCUnitPrice" class="orderTip"></span></div>
                </th>
                
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="sspPurchasePlanUsedPrice" >
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('UsedPrice','PurchasePlanUCUsedPrice');return false;">
                        单价<span id="PurchasePlanUCUsedPrice" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('TaxPrice','PurchasePlanUCTaxPrice');return false;">
                        含税价<span id="PurchasePlanUCTaxPrice" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('TotalPrice','PurchasePlanUCTotalPrice');return false;">
                        金额<span id="PurchasePlanUCTotalPrice" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('RequireDate','PurchasePlanUCRequireDate');return false;">
                        计划交货日期<span id="PurchasePlanUCRequireDate" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('FromBillNo','PurchasePlanUCFromBillNo');return false;">
                        源单编号<span id="PurchasePlanUCFromBillNo" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick" onclick="popPurchasePlanUC.orderByP('FromLineNo','PurchasePlanUCFromLineNo');return false;">
                        源单序号<span id="PurchasePlanUCFromLineNo" class="orderTip"></span></div>
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
                            <div id="PageCountPurchasePlanUC">
                            </div>
                        </td>
                        <td height="28" align="right">
                            <div id="pageDataList1_PagerPurchasePlanUC" class="jPagerBar">
                            </div>
                        </td>
                        <td height="28" align="right">
                            <div id="divpagePurchasePlanUC">
                                <input name="TotalRecordPurchasePlanUC" type="text" id="TotalRecordPurchasePlanUC"
                                    style="display: none" />
                                <span id="TotalPagePurchasePlanUC"></span>每页显示
                                <input name="PerPageCountPurchasePlanUC" type="text" id="PerPageCountPurchasePlanUC"
                                    style="width: 24px" />条 转到第
                                <input name="ToPagePurchasePlanUC" type="text" value="1" id="ToPagePurchasePlanUC"
                                    style="width: 26px" />页
                                <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                    width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexPurPlan($('#PerPageCountPurchasePlanUC').val(),$('#ToPagePurchasePlanUC').val());" />
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
var popPurchasePlanUC = new Object();
popPurchasePlanUC.Type = null;
popPurchasePlanUC.ProviderID = null;

popPurchasePlanUC.pageCount = 10;
popPurchasePlanUC.totalRecord = 0;
popPurchasePlanUC.pagerStyle = "flickr";
popPurchasePlanUC.currentPageIndex = 1;
popPurchasePlanUC.action = "";
popPurchasePlanUC.orderBy = "RequireDate";
popPurchasePlanUC.orderByType = "DESC "; 

popPurchasePlanUC.orderByP = function(orderColum,orderTip)
{
    var ordering = "d";
    popPurchasePlanUC.orderByType = "ASC"
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
        popPurchasePlanUC.orderByType = "DESC"
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    popPurchasePlanUC.orderBy = orderColum;
    
    popPurchasePlanUC.TurnToPage(1);
}

function fnGetPurPlan() {
    popPurchasePlanUC.TurnToPage(1)
}
//全选
function SelectAllPurPlan()
{
    $.each($("#pageDataListPurchasePlanUC :checkbox"), function(i, obj) {
        obj.checked = $("#ChkBoxPurPlan").attr("checked");
    });
}


popPurchasePlanUC.ShowList = function(Type,ProviderID)
{
    if(ProviderID == "")
    {
        ProviderID=0;
    }
//    if((Type == "Order")&&(document.getElementById("txtProviderID").value == ""))
//    {
//        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择供应商！");
//        return;
//    }
    popPurchasePlanUC.Type = Type;
    popPurchasePlanUC.ProviderID = ProviderID;
    document.getElementById("divPurchasePlanUC").style.display='block';
    openRotoscopingDiv(true,"divPurchasePlanUC2","frmPurchasePlanUC");
    
    popPurchasePlanUC.TurnToPage(1)
}

popPurchasePlanUC.CloseList = function()
{
    document.getElementById("divPurchasePlanUC").style.display='none';
    closeRotoscopingDiv(true,"divPurchasePlanUC2");
}

///遍历选择的是否都是同一个供应商
function CheckIsOneProvider()
{
    var ck = document.getElementsByName("ChkBoxPurPln");
    var  getCheck=new Array ();
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
                  getCheck.push (  $("#PurPlnProviderID"+i).html());
        }
    }  
    if (getCheck.length >1)
    {
         
          for (var a=0;a<getCheck.length;a++)
          {
          if (getCheck [a]==getCheck [0])
          {
          continue ;
          }
          else
          {
          return false;
          break ;
          }
          }
    }
   return true ;
}


 
function fnFillPurPln()
{

   if ( !CheckIsOneProvider())
   {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的明细只能来自一个供应商！");
        return ;
   } 
   
    var ck = document.getElementsByName("ChkBoxPurPln");
    for( var i = 0; i < ck.length; i++ )
    {
        var Type=popPurchasePlanUC.Type;
        if ( ck[i].checked )
        {//选中某行，将该行的值赋到界面上
            if(Type == "AskPrice")                
            { 
                  var PurPlnSortNo=$("#PurPlnFromBillNo"+i).html();
      var PurPlnFromBillNo=$("#PurPlnFromLineNo"+i).html();
            if (AskPriceExistFromApply(PurPlnSortNo,PurPlnFromBillNo))
            {
            continue ;
            }
             
                var rowID = AddAskPriceSignRow("From");
                var DisCount = 100;

                $("#DtlSColor" + rowID).val($("#PurPlnSColorName" + i).html());
                $("#ProductID"+rowID).val($("#PurPlnProductID"+i).html());
                $("#ProductNo"+rowID).val($("#PurPlnProductNo"+i).html());
                $("#ProductName"+rowID).val($("#PurPlnProductName"+i).html());
                $("#Specification"+rowID).val($("#PurPlnSpecification"+i).html());
                $("#ProductCount"+rowID).val(parseFloat($("#PurPlnProductCount"+i).html())-parseFloat($("#PurPlnOrderCount"+i).html()));
                var producount=parseFloat($("#PurPlnProductCount"+i).html())-parseFloat($("#PurPlnOrderCount"+i).html());
                $("#RequireDate"+rowID).val($("#PurPlnRequireDate"+i).html());
                $("#UnitID"+rowID).val($("#PurPlnUnitID"+i).html());
                $("#UnitName"+rowID).val($("#PurPlnUnitName"+i).html());
                $("#UnitPrice"+rowID).val($("#PurPlnUnitPrice"+i).html());
            var unitPrice=$("#PurPlnUnitPrice"+i).html();
                   $("#UnitPriceHide"+rowID).val($("#PurPlnUnitPrice"+i).html());
                $("#TaxPrice"+rowID).val($("#PurPlnTaxPrice"+i).html());
                var taxPrice=$("#PurPlnTaxPrice"+i).html();
                $("#TaxPriceHide"+rowID).val($("#PurPlnTaxPrice"+i).html()); 
                $("#TotalPrice"+rowID).val($("#PurPlnTotalPrice"+i).html());
                $("#TotalFee"+rowID).val($("#PurPlnProductID"+i).html()); 
                var TotalTax = parseFloat($("#PurPlnTaxPrice"+i).html())*parseFloat($("#ProductCount"+rowID).val())*$("#Discount"+rowID).val()/100;
                $("#TotalTax"+rowID).val(TotalTax);
                
                $("#TaxRate"+rowID).val($("#PurPlnInTaxRate"+i).html());       
                $("#TaxRateHide"+rowID).val($("#PurPlnInTaxRate"+i).html());
                $("#FromBillID"+rowID).val($("#PurPlnFromBillID"+i).html());
                $("#FromBillNo"+rowID).val($("#PurPlnFromBillNo"+i).html());
                $("#FromLineNo"+rowID).val($("#PurPlnFromLineNo"+i).html());
                   if($("#HiddenMoreUnit").val()=="False")
            {
            fnMergeDetail();
            }
            else
            {
            var UsedUnitCount  =$("#PurPlnUsedUnitCount"+i).html();
              var UsedUnitID  =$("#PurPlnUsedUnitID"+i).html();
                var UsedPrice  =$("#PurPlnUsedPrice"+i).html();

                GetUnitGroupSelectEx($("#PurPlnProductID" + i).html(), "InUnit", "SignItem_TD_UnitID_Select" + rowID, "ChangeUnit(this.id," + rowID + "," + unitPrice + "," + taxPrice + ")", "unitdiv" + rowID, '', "FillApplyContent(" + rowID + "," + unitPrice + "," + taxPrice + "," + producount + "," + UsedUnitCount + ",'" + UsedUnitID + "'," + UsedPrice + ",'Bill')"); //绑定单位组
            }
                
                
               
                if($("#ddlTypeID_ddlCodeType").val() == "")
                {
                    $("#ddlTypeID_ddlCodeType").val($("#PurPlnTypeID"+i).html());
                }
                if($("#DeptName").val()=="")
                {
                    $("#DeptName").val($("#PurPlnPlanDeptName"+i).html());
                    $("#txtDeptID").val($("#PurPlnPlanDeptID"+i).html());
                }
                $("#txtProviderName").val($("#PurPlnProviderName"+i).html());
                $("#txtProviderID").val($("#PurPlnProviderID"+i).html());
                
                
                
                
            }
            if(Type == "Order")
            {
    
      
              var PurSortNo=$("#PurPlnFromLineNo"+i).html();
      var PurFromBillNo=$("#PurPlnFromBillNo"+i).html();
            if (OrdertExistFrom(PurFromBillNo,PurSortNo))
            {
            continue ;
            }
            
                $("#txtProviderName").val($("#PurPlnProviderName"+i).html());
                $("#txtProviderID").val($("#PurPlnProviderID"+i).html());
                var rowID = AddSignRow();
                $("#DtlSColor" + rowID).val($("#PurPlnSColorName" + i).html());
                document.getElementById("OrderProductID"+rowID).value = $("#PurPlnProductID"+i).html();
                document.getElementById("OrderProductNo"+rowID).value = $("#PurPlnProductNo"+i).html();
                document.getElementById("OrderProductName"+rowID).value = $("#PurPlnProductName"+i).html();
                document.getElementById("OrderSpecification"+rowID).value = $("#PurPlnSpecification"+i).html();
                document.getElementById("OrderProductCount"+rowID).value = parseFloat($("#PurPlnProductCount"+i).html())-parseFloat($("#PurPlnOrderCount"+i).html());
                
                
                    var producount1=(parseFloat($("#PurPlnProductCount"+i).html())-parseFloat($("#PurPlnOrderCount"+i).html())).toFixed($("#HiddenPoint").val());
                document.getElementById("OrderUnitID"+rowID).value = $("#PurPlnUnitID"+i).html();
                document.getElementById("OrderUnitName"+rowID).value = $("#PurPlnUnitName"+i).html();
                document.getElementById("OrderUnitPrice"+rowID).value = $("#PurPlnUnitPrice"+i).html();
                var unitPrice1=(parseFloat ($("#PurPlnUnitPrice"+i).html())).toFixed($("#HiddenPoint").val());
                document.getElementById("OrderTaxPrice"+rowID).value = $("#PurPlnTaxPrice"+i).html();
                       var taxPrice1=(parseFloat ($("#PurPlnTaxPrice"+i).html())).toFixed($("#HiddenPoint").val());
                   document.getElementById("OrderUnitPriceHid"+rowID).value = $("#PurPlnUnitPrice"+i).html();
                document.getElementById("OrderTaxPriceHide"+rowID).value = $("#PurPlnTaxPrice"+i).html();
                
                document.getElementById("OrderRequireDate"+rowID).value = $("#PurPlnRequireDate"+i).html();
                document.getElementById("OrderFromBillID"+rowID).value = $("#PurPlnFromBillID"+i).html();
                document.getElementById("OrderFromBillNo"+rowID).value = $("#PurPlnFromBillNo"+i).html();
                document.getElementById("OrderFromLineNo"+rowID).value = $("#PurPlnFromLineNo"+i).html();
//                document.getElementById("OrderDiscount"+rowID).value = 100;
                if($("#UserPurchaserName").attr("value") == null)
                {
                    $("#UserPurchaserName").val($("#PurPlnPurchaserName").html());
                    $("#txtPurchaserID").val($("#PurPlnPurchaser").html());
//                    $("#UserPurchaserName").attr("disabled","disabled");
                }
                if($("#UserPurchaserName").attr("value") == null)
                {
                    $("#DeptName").val($("#PurPlnPlanDeptName"+i).html());
                    $("#txtDeptID").val($("#PurPlnPlanDeptID"+i).html());
//                    $("#DeptName").attr("disabled","disabled");
                }
                
                if($("#ddlTypeID_ddlCodeType").val() != "")
                {
                    $("#ddlTypeID_ddlCodeType").val($("#PurPlnTypeID").html());
                }
               if($("#HiddenMoreUnit").val()=="False")
            {
            fnMergeDetail();
            }
            else
            {
               var UsedUnitCount  =$("#PurPlnUsedUnitCount"+i).html();
              var UsedUnitID  =$("#PurPlnUsedUnitID"+i).html();
                var UsedPrice  =$("#PurPlnUsedPrice"+i).html();
               
                GetUnitGroupSelectEx($("#PurPlnProductID" + i).html(), "InUnit", "SignItem_TD_UnitID_Select" + rowID, "ChangeUnit(this.id," + rowID + "," + unitPrice1 + "," + taxPrice1 + ")", "unitdiv" + rowID, '', "FillApplyContent(" + rowID + "," + unitPrice1 + "," + taxPrice1 + "," + producount1 + "," + UsedUnitCount + ",'" + UsedUnitID + "'," + UsedPrice + ",'Bill')"); //绑定单位组
            }
            }
        }
    }
    popPurchasePlanUC.CloseList();
}

function PurPlanClickChkBox(rowID)
{
    //判断选择的是不是来自同一个单据
    var ck = document.getElementsByName("ChkBoxPurPln");
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked && i!= rowID )
        {//选中某行
            if(popPurchasePlanUC.ProviderID != 0 && popPurchasePlanUC.ProviderID != $("#PurPlnProviderID"+rowID).html())
            {
                document.getElementById("ChkBoxPurPln"+rowID).checked = false;
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的计划明细只能来自一个供应商！");
                return;
            }
            if($("#PurPlnProviderName"+i).html() != $("#PurPlnProviderName"+rowID).html())
            {
               
                //如果选择的不是一个合同则不让选
                document.getElementById("ChkBoxPurPln"+rowID).checked = false;
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","选择的计划明细只能来自一个供应商！");
                return;
            }
        }
    }
        
    IfSelectAll('ChkBoxPurPln','ChkBoxPurPlan');
}


popPurchasePlanUC.TurnToPage = function(pageIndex)
{
    popPurchasePlanUC.currentPageIndex = pageIndex;

 
        var param = "";
        param += "&ProductNo=" +escape ( $("#txtProdNoPurPlan").val());
        param += "&ProductName=" +escape ( $("#txtProdNamePurPlan").val());
        param += "&StartDate=" +escape ( $("#txtStartDatePurPlan").val());
        param += "&EndDate=" +escape ( $("#txtEndDatePurPlan").val());

        
        //    str += "&CurrencyID="+document.getElementById("PurchaseContractUC21_hidPurConCurrency").value;
        
   
    
    
  if($("#HiddenMoreUnit").val()=="True")
  {
      $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/UC/PurchasePlanUC.ashx',//目标地址
           cache:false,
           data: "pageIndex="+popPurchasePlanUC.currentPageIndex+"&pageCount="+popPurchasePlanUC.pageCount+"&action="+popPurchasePlanUC.action+
                 "&orderby="+popPurchasePlanUC.orderBy+"&OrderByType="+popPurchasePlanUC.orderByType+"&ProviderID="+escape(popPurchasePlanUC.ProviderID)+param ,//数据
           beforeSend:function(){$("#pageDataList1_PagerPurchasePlanUC").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListPurchasePlanUC tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        if(item.ProductID != null && item.ProductID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxPurPln\" id=\"ChkBoxPurPln"+i+"\" onclick=\"PurPlanClickChkBox("+i+");\" value=\""+item.ID+"\"  />"+"</td>"+
                        "<td height='22' id='PurPlnProductID"+i+"' style='display:none' align='center'>" + item.ProductID + "</td>"+
                        "<td height='22' id='PurPlnProviderID"+i+"' style='display:none' align='center'>" + item.ProviderID + "</td>"+
                        "<td height='22' id='PurPlnProviderName"+i+"' align='center'>" + item.ProviderName + "</td>"+
                        "<td height='22' id='PurPlnProductNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                        "<td height='22' id='PurPlnProductName"+i+"' align='center'>" + item.ProductName + "</td>"+
                        "<td height='22' id='PurPlnSpecification" + i + "' align='center'>" + item.Specification + "</td>" +
                         "<td height='22' id='PurPlnSColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                         "<td height='22' id='PurPlnUsedUnitCount"+i+"' align='center'>" + (parseFloat(item.UsedUnitCount)).toFixed($("#HiddenPoint").val())+ "</td>"+ 
                        "<td height='22' id='PurPlnProductCount"+i+"' align='center'    >" +  (parseFloat(item.ProductCount)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurPlnOrderCount"+i+"' align='center'>" +     (parseFloat(item.OrderCount)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurPlnUnitID"+i+"' style='display:none' align='center'>" + item.UnitID + "</td>"+
                        "<td height='22' id='PurPlnUnitName"+i+"' align='center'  >" + item.UnitName + "</td>"+
                         "<td height='22' id='PurPlnUsedUnitName"+i+"' align='center'  >" + item.UsedUnitName + "</td>"+
                           "<td height='22' id='PurPlnUsedUnitID"+i+"' align='center' style='display:none' >" + item.UsedUnitID + "</td>"+
                        "<td height='22' id='PurPlnUnitPrice"+i+"' align='center'   >" +         (parseFloat(item.UnitPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+
                          "<td height='22' id='PurPlnUsedPrice"+i+"' align='center'>" +   (parseFloat(item.UsedPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurPlnTaxPrice"+i+"' align='center'>" +  (parseFloat(item.TaxPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurPlnTotalPrice"+i+"' align='center'>" +   (parseFloat(item.TotalPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurPlnRequireDate"+i+"' align='center'>" + item.RequireDate + "</td>"+
                        "<td height='22' id='PurPlnFromBillID"+i+"' style='display:none' align='center'>" + item.FromBillID + "</td>"+
                        "<td height='22' id='PurPlnPlanDeptID"+i+"' style='display:none' align='center'>" + item.PlanDeptID + "</td>"+
                        "<td height='22' id='PurPlnPlanDeptName"+i+"' style='display:none' align='center'>" + item.PlanDeptName + "</td>"+
                        "<td height='22' id='PurPlnPurchaser"+i+"' style='display:none' align='center'>" + item.Purchaser + "</td>"+
                        "<td height='22' id='PurPlnPurchaserName"+i+"' style='display:none' align='center'>" + item.PurchaserName + "</td>"+
                        "<td height='22' id='PurPlnInTaxRate"+i+"' style='display:none' align='center'>" + item.InTaxRate + "</td>"+
                        "<td height='22' id='PurPlnTypeID"+i+"' style='display:none' align='center'>" + item.TypeID + "</td>"+
                        "<td height='22' id='PurPlnFromBillNo"+i+"' align='center'>" + item.FromBillNo + "</td>"+
                        "<td height='22' id='PurPlnFromLineNo"+i+"' align='center'>"+item.FromLineNo+"</td>").appendTo($("#pageDataListPurchasePlanUC tbody"));

                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerPurchasePlanUC",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:popPurchasePlanUC.pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:popPurchasePlanUC.pageCount,currentPageIndex:popPurchasePlanUC.currentPageIndex,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"popPurchasePlanUC.TurnToPage({pageindex});return false;"}//[attr]
                    );
                  popPurchasePlanUC.totalRecord = msg.totalCount;
                  document.getElementById("TotalRecordPurchasePlanUC").value=msg.totalCount;
                  $("#PerPageCountPurchasePlanUC").val(popPurchasePlanUC.pageCount);
                  ShowTotalPage(msg.totalCount,popPurchasePlanUC.pageCount,pageIndex,$("#PageCountPurchasePlanUC"));
                  
                  $("#ToPageProductInfoUC").val(popPurchasePlanUC.pageIndex);
                  },
           error: function() {},
           complete:function(){hidePopup();$("#pageDataList1_PagerPurchasePlanUC").show();IfshowPurPlan(document.getElementById("TotalRecordPurchasePlanUC").value);pageDataList1("pageDataListPurchasePlanUC","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕 
           
           });
  }
  else
  {
  
  document .getElementById ("sspPurchasePlanUsedUnitCount").style.display ="none"; 
    document .getElementById ("sspPurchasePlanUsedUnitName").style.display ="none"; 
      document .getElementById ("sspPurchasePlanUsedPrice").style.display ="none";
      document.getElementById("sspPurchasePlanCount").innerHTML = "数量";
      document.getElementById("sspPurchasePlanUnit").innerHTML = "单位";
      document.getElementById("sspPurchasePlanUniprice").innerHTML = "单价";
       
     

  
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/UC/PurchasePlanUC.ashx',//目标地址
           cache:false,
           data: "pageIndex="+popPurchasePlanUC.currentPageIndex+"&pageCount="+popPurchasePlanUC.pageCount+"&action="+popPurchasePlanUC.action+
                 "&orderby=" + popPurchasePlanUC.orderBy + "&OrderByType=" + popPurchasePlanUC.orderByType + "&ProviderID=" + escape(popPurchasePlanUC.ProviderID) + param, //数据
           beforeSend:function(){$("#pageDataList1_PagerPurchasePlanUC").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListPurchasePlanUC tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        if(item.ProductID != null && item.ProductID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"checkbox\" name=\"ChkBoxPurPln\" id=\"ChkBoxPurPln"+i+"\" onclick=\"PurPlanClickChkBox("+i+");\" value=\""+item.ID+"\"  />"+"</td>"+
                        "<td height='22' id='PurPlnProductID"+i+"' style='display:none' align='center'>" + item.ProductID + "</td>"+
                        "<td height='22' id='PurPlnProviderID"+i+"' style='display:none' align='center'>" + item.ProviderID + "</td>"+
                        "<td height='22' id='PurPlnProviderName"+i+"' align='center'>" + item.ProviderName + "</td>"+
                        "<td height='22' id='PurPlnProductNo"+i+"' align='center'>" + item.ProductNo + "</td>"+
                        "<td height='22' id='PurPlnProductName"+i+"' align='center'>" + item.ProductName + "</td>"+
                        "<td height='22' id='PurPlnSpecification" + i + "' align='center'>" + item.Specification + "</td>" +
                         "<td height='22' id='PurPlnSColorName" + i + "' align='center'>" + item.ColorName + "</td>" +
                        "<td height='22' id='PurPlnProductCount"+i+"' align='center'>" + (parseFloat(item.ProductCount)).toFixed($("#HiddenPoint").val())+ "</td>"+ 
                        "<td height='22' id='PurPlnOrderCount"+i+"' align='center'>" +  (parseFloat(item.OrderCount)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurPlnUnitID"+i+"' style='display:none' align='center'>" + item.UnitID + "</td>"+
                        "<td height='22' id='PurPlnUnitName"+i+"' align='center'>" + item.UnitName + "</td>"+
                        "<td height='22' id='PurPlnUnitPrice"+i+"' align='center'>" +    (parseFloat(item.UnitPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurPlnTaxPrice"+i+"' align='center'>" +  (parseFloat(item.TaxPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurPlnTotalPrice"+i+"' align='center'>" + (parseFloat(item.TotalPrice)).toFixed($("#HiddenPoint").val())+ "</td>"+
                        "<td height='22' id='PurPlnRequireDate"+i+"' align='center'>" + item.RequireDate + "</td>"+
                        "<td height='22' id='PurPlnFromBillID"+i+"' style='display:none' align='center'>" + item.FromBillID + "</td>"+
                        "<td height='22' id='PurPlnPlanDeptID"+i+"' style='display:none' align='center'>" + item.PlanDeptID + "</td>"+
                        "<td height='22' id='PurPlnPlanDeptName"+i+"' style='display:none' align='center'>" + item.PlanDeptName + "</td>"+
                        "<td height='22' id='PurPlnPurchaser"+i+"' style='display:none' align='center'>" + item.Purchaser + "</td>"+
                        "<td height='22' id='PurPlnPurchaserName"+i+"' style='display:none' align='center'>" + item.PurchaserName + "</td>"+
                        "<td height='22' id='PurPlnInTaxRate"+i+"' style='display:none' align='center'>" + item.InTaxRate + "</td>"+
                        "<td height='22' id='PurPlnTypeID"+i+"' style='display:none' align='center'>" + item.TypeID + "</td>"+
                        "<td height='22' id='PurPlnFromBillNo"+i+"' align='center'>" + item.FromBillNo + "</td>"+
                        "<td height='22' id='PurPlnFromLineNo"+i+"' align='center'>"+item.FromLineNo+"</td>").appendTo($("#pageDataListPurchasePlanUC tbody"));

                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerPurchasePlanUC",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:popPurchasePlanUC.pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:popPurchasePlanUC.pageCount,currentPageIndex:popPurchasePlanUC.currentPageIndex,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"popPurchasePlanUC.TurnToPage({pageindex});return false;"}//[attr]
                    );
                  popPurchasePlanUC.totalRecord = msg.totalCount;
                  document.getElementById("TotalRecordPurchasePlanUC").value=msg.totalCount;
                  $("#PerPageCountPurchasePlanUC").val(popPurchasePlanUC.pageCount);
                  ShowTotalPage(msg.totalCount,popPurchasePlanUC.pageCount,pageIndex,$("#PageCountPurchasePlanUC"));
                  
                  $("#ToPageProductInfoUC").val(popPurchasePlanUC.pageIndex);
                  },
           error: function() {},
           complete:function(){hidePopup();$("#pageDataList1_PagerPurchasePlanUC").show();IfshowPurPlan(document.getElementById("TotalRecordPurchasePlanUC").value);pageDataList1("pageDataListPurchasePlanUC","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕 
           
           });
           }
}

//改变每页记录数及跳至页数
function ChangePageCountIndexPurPlan(newPageCountProvider,newPageIndexProvider)
{
    if(newPageCountProvider <=0 || newPageIndexProvider <= 0 ||  newPageIndexProvider  > ((popPurchasePlanUC.totalRecord -1)/newPageCountProvider)+1)
    
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        popPurchasePlanUC.pageCount=parseInt(newPageCountProvider);
        popPurchasePlanUC.TurnToPage(parseInt(newPageIndexProvider));
    }
}

function IfshowPurPlan(count)
{
    if(count=="0")
    {
        document.getElementById("divpagePurchasePlanUC").style.display = "none";
        document.getElementById("PageCountPurchasePlanUC").style.display = "none";
    }
    else
    {
        document.getElementById("divpagePurchasePlanUC").style.display = "block";
        document.getElementById("PageCountPurchasePlanUC").style.display = "block";
    }
}
</script>

