<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubSellOrderSelectUC.ascx.cs"
    Inherits="UserControl_SubSellOrderSelectUC" %>
<div id="divSubSellOrderUChhh" style="display: none">
    <iframe id="aSubSellOrderUC" style="filter: Alpha(opacity=0); border: solid 10px #93BCDD;
        background: #fff; padding: 10px; width: 1000px; height: 120px; z-index: 1000;
        position: absolute; display: block; top: 30%; left: 40%; margin: 5px 0 0 -400px;">
    </iframe>
    <!--提示信息弹出详情start-->
    <div id="divSubSellOrderUC" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 70%; z-index: 1001; position: absolute; display: block;">
        <table width="100%">
            <tr>
                <td>
                    <img onclick="closeSubSellOrderUCdiv()" style="text-align: right; cursor: pointer"
                        src="../../../Images/Button/Bottom_btn_close.jpg" />
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1SubSellBackUC"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            产品编号<span id="Span12" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            产品名称<span id="Span6" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            规格<span id="Span2" class="orderTip"></span></div>
                    </th>
                    <th id="thUsedUnitName_divSubSellOrderUC" align="center" background="../../../images/Main/Table_bg.jpg"
                        bgcolor="#FFFFFF">
                        <div class="orderClick">
                            基本单位<span id="pc21" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            单位<span id="Span1" class="orderTip"></span></div>
                    </th>
                    <th id="thUsedUnitCount_divSubSellOrderUC" align="center" background="../../../images/Main/Table_bg.jpg"
                        bgcolor="#FFFFFF">
                        <div class="orderClick">
                            基本数量<span id="pc22" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            发货数量<span id="Span9" class="orderTip"></span></div>
                    </th>
                    <th id="thUsedPrice_divSubSellOrderUC" align="center" background="../../../images/Main/Table_bg.jpg"
                        bgcolor="#FFFFFF">
                        <div class="orderClick">
                            基本单价<span id="pc23" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            单价<span id="Span14" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            含税价<span id="Span3" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            折扣<span id="Span4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            税率<span id="Span5" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            仓库<span id="Span13" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            源单编号<span id="Span7" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            源单序号<span id="Span8" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            备注<span id="Span15" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            已退货数量<span id="Span10" class="orderTip"></span></div>
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
                                <div id="pageSubSellBackUCcount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_PagerSubSellBackUC" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divOrderSelect">
                                    <input name="text" type="text" id="Text2SubSellBackUCcount" style="display: none" />
                                    <span id="pageDataList1_TotalProvider"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountOrderUC" style="width: 22px" />条
                                    转到第
                                    <input name="text" type="text" id="ToPageOrder" style="width: 35px" />页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexOrderUC($('#ShowPageCountOrderUC').val(),$('#ToPageOrder').val());" />
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

var popSubSellOrderUC=new Object();
popSubSellOrderUC.InputObj = null;
popSubSellOrderUC.OrderNo= null;
popSubSellOrderUC.CurrencyTypeID= null;
popSubSellOrderUC.Rate= null;
popSubSellOrderUC.isMoreUnit= null;

    var pageCountSubSellOrder = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var orderBy = "";//排序字段
    
    var actionSubSellOrder = "";//操作
    
    popSubSellOrderUC.ShowList = function(objInput,OrderNo,CurrencyTypeID,Rate,isMoreUnit)
{
    openRotoscopingDiv(false,'divSBackShadow','SBackShadowIframe');
    popSubSellOrderUC.InputObj = objInput;
    popSubSellOrderUC.OrderNo = OrderNo;
    popSubSellOrderUC.CurrencyTypeID= CurrencyTypeID;
    popSubSellOrderUC.Rate= Rate;
    popSubSellOrderUC.isMoreUnit= isMoreUnit;
    
    document.getElementById('divSubSellOrderUChhh').style.display='block';
    CenterToDocument("divSubSellOrderUC",true);
   
    SSOSetHide();
    
    TurnToPageSubOrder(1);
}

// 隐藏列
function SSOSetHide()
{
    if(!popSubSellOrderUC.isMoreUnit)
    {// 隐藏非多计量单位
        $("#thUsedUnitName_divSubSellOrderUC").hide();
        $("#thUsedUnitCount_divSubSellOrderUC").hide();
        $("#thUsedPrice_divSubSellOrderUC").hide();
    }
}

function closeSubSellOrderUCdiv()
{

    document.getElementById("divSubSellOrderUChhh").style.display="none";
    closeRotoscopingDiv(false,'divSBackShadow');
}
    

    //jQuery-ajax获取JSON数据
    function TurnToPageSubOrder(pageIndexProvider)
    {

           currentPageIndexProvider = pageIndexProvider;
           var pagerStyle = "flickr";//jPagerBar样式

           var ProviderID= "";
           var ProviderNo="";
           var ProviderName="";

           $.ajax({
               type: "POST", //用POST方式传输
               dataType: "json", //数据格式:JSON
               url: '../../../Handler/Office/SubStoreManager/SubSellOrderSelectUC.ashx', //目标地址
               cache: false,
               data: "pageIndexSubSellBack=" + pageIndexProvider 
                        + "&pageCountSubSellBack=" + pageCountSubSellOrder 
                        + "&OrderNo=" + popSubSellOrderUC.OrderNo 
                        + "&CurrencyTypeID=" + popSubSellOrderUC.CurrencyTypeID 
                        + "&Rate=" + popSubSellOrderUC.Rate 
                        + "&actionSubSellOrder=" + actionSubSellOrder 
                        + "&orderbySubSellOrder=" + orderBy + "", //数据
               beforeSend: function() { AddPop(); $("#pageDataList1_PagerSubSellBackUC").hide(); }, //发送数据之前

               success: function(msg) {
                   //数据获取完毕，填充页面据显示
                   //数据列表
                   $("#pageDataList1SubSellBackUC tbody").find("tr.newrow").remove();
                   $.each(msg.data, function(i, item) {
                       if (item.ID != null && item.ID != "")
                           $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + " <input type=\"radio\" name=\"radioTech\" id=\"radioTech_" + item.ID + "\" value=\"" + item.ID + "\" onclick=\"FillFromSubSellOrderUC('" + item.DeptID + "','" + item.DeptName + "','" + item.OrderNo + "' ,'" + item.FromLineNo + "','" + item.FromBillNo + "','" + item.FromBillID + "','" + item.ProductID + "','" + item.ProductNo + "','" + item.ProductName + "','" + item.standard + "','" + item.UnitID + "','" + item.UnitName + "','" + item.UnitPrice + "','" + item.TaxPrice + "','" + item.Discount + "','" + item.TaxRate + "','" + item.StorageID + "','" + item.StorageName + "','" + item.ProductCount + "','" + item.Remark + "','" + item.YBackCount + "','" + item.UsedUnitID + "','" + item.UsedUnitCount + "','" + item.UsedPrice + "','" + item.BatchNo + "');\" />" + "</td>" +
                    "<td height='22' align='center'>" + item.ProductNo + "</td>" +
                    "<td height='22' align='center'>" + item.ProductName + "</td>" +
                    "<td height='22' align='center'>" + item.standard + "</td>" +
                    (popSubSellOrderUC.isMoreUnit?(
                    "<td height='22' align='center'>" + item.UnitName + "</td>" +
                    "<td height='22' align='center'>" + item.UsedUnitName + "</td>" +
                    "<td height='22' align='center'>" + item.ProductCount + "</td>" +
                    "<td height='22' align='center'>" + item.UsedUnitCount + "</td>" +
                    "<td height='22' align='center'>" + item.UnitPrice + "</td>"+
                    "<td height='22' align='center'>" + item.UsedPrice + "</td>"
                    ):(
                    "<td height='22' align='center'>" + item.UnitName + "</td>" +
                    "<td height='22' align='center'>" + item.ProductCount + "</td>" +
                    "<td height='22' align='center'>" + item.UnitPrice + "</td>")) +
                    "<td height='22' align='center'>" + item.TaxPrice + "</td>" +
                    "<td height='22' align='center'>" + item.Discount + "</td>" +
                    "<td height='22' align='center'>" + item.TaxRate + "</td>" +
                    "<td height='22' align='center'>" + item.StorageName + "</td>" +
                    "<td height='22' align='center'>" + item.FromBillNo + "</td>" +
                    "<td height='22' align='center'>" + item.FromLineNo + "</td>" +
                    "<td height='22' align='center'>" + item.Remark + "</td>" +
                    "<td height='22' align='center'>" + item.YBackCount + "</td>").appendTo($("#pageDataList1SubSellBackUC tbody"));
                   });
                   //页码
                   ShowPageBar("pageDataList1_PagerSubSellBackUC", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1MarkProvider",

                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCountSubSellOrder, currentPageIndex: pageIndexProvider, noRecordTip: "没有符合条件的记录", preWord: "上页", nextWord: "下页", First: "首页", End: "末页",
                    onclick: "TurnToPageSubOrder({pageindex});return false;"}//[attr]
                    );

                   totalRecordProvider = msg.totalCount;
                   // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                   document.getElementById("Text2SubSellBackUCcount").value = msg.totalCount;
                   $("#ShowPageCountOrderUC").val(pageCountSubSellOrder);
                   ShowTotalPage(msg.totalCount, pageCountSubSellOrder, pageIndexProvider, $("#pageSubSellBackUCcount"));
                   $("#ToPageOrder").val(pageIndexProvider);
               },
               error: function() { },
               complete: function() {
                   hidePopup();
                   $("#pageDataList1_PagerSubSellBackUC").show(); IfshowOrderSelect(document.getElementById("Text2SubSellBackUCcount").value); pageDataList1("pageDataList1SubSellBackUC", "#E7E7E7", "#FFFFFF", "#cfc", "cfc");
               } //接收数据完毕
           });
    }
    //table行颜色
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

function Fun_Search_Provider(aa)
{
      search="1";
      TurnToPage(1);
}
function IfshowOrderSelect(count)
    {
        if(count=="0")
        {
            document.getElementById("divOrderSelect").style.display = "none";
            document.getElementById("pageSubSellBackUCcount").style.display = "none";
        }
        else
        {
            document.getElementById("divOrderSelect").style.display="";
            document.getElementById("pageSubSellBackUCcount").style.display = "";
        }
    }
    
    function SelectDeptProvider(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexOrderUC(newPageCountProvider,newPageIndexProvider)
    {
        if(!IsZint(newPageCountProvider))
       {
          popMsgObj.ShowMsg('显示条数格式不对，必须输入正整数！');
          return;
       }
       if(!IsZint(newPageIndexProvider))
       {
          popMsgObj.ShowMsg('跳转页数格式不对，必须输入正整数！');
          return;
       }
        if(newPageCountProvider <=0 || newPageIndexProvider <= 0 ||  newPageIndexProvider  > ((totalRecordProvider-1)/newPageCountProvider)+1 )
        {
//            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            popMsgObj.ShowMsg('转到页数超出查询范围！');
            return;
        }
        else
        {
        ifshow="0";
            this.pageCountSubSellOrder=parseInt(newPageCountProvider);
            TurnToPageSubOrder(parseInt(newPageIndexProvider));
        }
    }
    
    

function closeProviderdiv()
{
    document.getElementById("divSubSellOrderUChhh").style.display="none";
}




</script>

