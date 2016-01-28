<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubSellOrderSelect.ascx.cs"
    Inherits="UserControl_SubSellOrderSelect" %>
<style type="text/css">
    .tdinput
    {
        border-width: 0pt;
        background-color: #ffffff;
        height: 21px;
        margin-left: 2px;
    }
</style>
<div id="divSubSellOrderhhh" style="display: none">
    <iframe id="aProviderLinkMan" style="filter: Alpha(opacity=0); border: solid 10px #93BCDD;
        background: #fff; padding: 10px; width: 1000px; height: 440px; z-index: 999;
        position: absolute; display: block; top: 30%; left: 40%; margin: 5px 0 0 -400px;">
    </iframe>
    <!--提示信息弹出详情start-->
    <div id="divSubSellOrder" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 1000px; z-index: 1000; position: absolute; display: block; top: 30%; left: 40%;
        margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <img onclick="closeSubSellOrderdiv()" src="../../../Images/Button/Bottom_btn_close.jpg"
                        style="text-align: left; cursor: pointer" />
                    <img onclick="clearSubSellOrderdiv()" src="../../../Images/Button/Bottom_btn_del.jpg"
                        style="text-align: left; cursor: pointer" />
                </td>
            </tr>
        </table>
        <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
            <tr>
                <td valign="top">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
                <td rowspan="2" align="right" valign="top">
                    <div id='searchClickSubSellOrder'>
                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('SubSellOrdersearchtable','searchClickSubSellOrder')" /></div>
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
                    <table width="100%" border="0" align="left" cellpadding="0" id="SubSellOrdersearchtable"
                        cellspacing="0" bgcolor="#CCCCCC">
                        <tr>
                            <td bgcolor="#FFFFFF">
                                <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                    class="table">
                                    <tr class="table-item">
                                        <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                            销售订单
                                        </td>
                                        <td width="40%" bgcolor="#FFFFFF">
                                            <input type="text" id="txt_OrderNo" class="tdinput" />
                                        </td>
                                        <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                            发货模式
                                        </td>
                                        <td width="40%" bgcolor="#FFFFFF">
                                            <select name="ddlSendModes" class="tdinput" id="ddlSendModes">
                                                <option value="0" selected="selected">--请选择--</option>
                                                <option value="1">分店发货</option>
                                                <option value="2">总部发货</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center" bgcolor="#FFFFFF">
                                            <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                                onclick='Fun_Search_Provider2()' id="btn_search" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1SubSellBack"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            订单编号<span id="Span12" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            发货模式<span id="Span6" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            发货时间<span id="Span2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            发货人<span id="Span1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            客户名称<span id="Span9" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            客户联系电话<span id="Span10" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            客户手机号<span id="Span14" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            币种<span id="Span4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            汇率<span id="Span5" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            客户地址<span id="Span3" class="orderTip"></span></div>
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
                                <div id="pageOrdercount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_PagerSubSellBack" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divOrderSelect">
                                    <input name="text" type="text" id="Text2Order" style="display: none" />
                                    <span id="pageDataList1_TotalProvider"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountOrder" style="width: 22px" />条 转到第
                                    <input name="text" type="text" id="ToPageOrder" style="width: 35px" />页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexOrder($('#ShowPageCountOrder').val(),$('#ToPageOrder').val());" />
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


var popSubSellOrder=new Object();
popSubSellOrder.InputObj = null;
popSubSellOrder.CurrencyTypeID= null;
popSubSellOrder.Rate = null;

var pageCountSubSellOrder = 10;//每页计数
var totalRecord = 0;
var pagerStyle = "flickr";//jPagerBar样式
var currentPageIndex = 1;
var orderBy = "";//排序字段

var actionSubSellOrder = "";//操作
    
popSubSellOrder.ShowList = function(objInput,CurrencyTypeID,Rate)
{
    openRotoscopingDiv(false,'divSBackShadow','SBackShadowIframe');
    popSubSellOrder.InputObj = objInput;
    popSubSellOrder.CurrencyTypeID= CurrencyTypeID;
    popSubSellOrder.Rate = Rate;
    document.getElementById('divSubSellOrderhhh').style.display='block';
    
    TurnToPageOrder(1);
}
function closeSubSellOrderdiv()
{

    document.getElementById("divSubSellOrderhhh").style.display="none";
    closeRotoscopingDiv(false,'divSBackShadow');
}
    
    
    //jQuery-ajax获取JSON数据
    function TurnToPageOrder(pageIndexProvider)
    {

           currentPageIndexProvider = pageIndexProvider;

           var ProviderID= "";
           var ProviderNo="";
           var ProviderName="";
           
           var OrderNo=document.getElementById("txt_OrderNo").value.Trim(); //$("#txt_OrderNo").val().Trim();
           var SendMode= document.getElementById("ddlSendModes").value.Trim();//$("#ddlSendModes").val().Trim();
           
           if(SendMode == 0)
           {
            SendMode = "";
           }
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SubStoreManager/SubSellOrderSelect.ashx',//目标地址
           cache:false,
           data: "pageIndexSubSellBack="+pageIndexProvider
                    +"&pageCountSubSellBack="+pageCountSubSellOrder
                    +"&actionSubSellOrder="+actionSubSellOrder
                    +"&orderbySubSellOrder="+orderBy
                    +"&OrderNo="+escape(OrderNo)
                    +"&CurrencyTypeID="+popSubSellOrder.CurrencyTypeID
                    +"&Rate="+popSubSellOrder.Rate
                    +"&SendMode="+escape(SendMode)+"",//数据
           beforeSend:function(){$("#pageDataList1_PagerSubSellBack").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1SubSellBack tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"FillFromSubSellOrder('"+item.ID+"','"+item.OrderNo+"','"+item.SendMode+"','"+item.OutDate+"' ,'"+item.OutUserID+"','"+item.OutUserName+"','"+item.CustName+"','"+item.CustTel+"','"+item.CustMobile+"','"+item.CurrencyType+"','"+item.CurrencyTypeName+"','"+item.Rate+"','"+item.CustAddr+"');\" />"+"</td>"+
                    "<td height='22' align='center'>" + item.OrderNo + "</td>"+
                    "<td height='22' align='center'>" + item.SendModeName + "</td>"+
                    "<td height='22' align='center'>" + item.OutDate + "</td>"+
                    "<td height='22' align='center'>" + item.OutUserName + "</td>"+
                    "<td height='22' align='center'>" + item.CustName + "</td>"+
                    "<td height='22' align='center'>" + item.CustTel + "</td>"+
                    "<td height='22' align='center'>" + item.CustMobile + "</td>"+
                    "<td height='22' style='display:none' align='center'>" + item.CurrencyType + "</td>"+
                    "<td height='22' align='center'>" + item.CurrencyTypeName + "</td>"+
                    "<td height='22' align='center'>" + item.Rate + "</td>"+
                    "<td height='22' align='center'>"+item.CustAddr+"</td>").appendTo($("#pageDataList1SubSellBack tbody"));
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerSubSellBack",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkProvider",
                    
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountSubSellOrder,currentPageIndex:pageIndexProvider,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"TurnToPageOrder({pageindex});return false;"}//[attr]
                    );

                  totalRecordProvider = msg.totalCount;
                  document.getElementById("Text2Order").value=msg.totalCount;
                  $("#ShowPageCountOrder").val(pageCountSubSellOrder);
                  ShowTotalPage(msg.totalCount,pageCountSubSellOrder,pageIndexProvider,$("#pageOrdercount"));
                  $("#ToPageOrder").val(pageIndexProvider);
                  },
           error: function() {}, 
           complete:function()
           {
                hidePopup();    
                $("#pageDataList1_PagerSubSellBack").show();IfshowOrderSelect(document.getElementById("Text2Order").value);pageDataList1("pageDataList1SubSellBack","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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

function Fun_Search_Provider2(aa)
{
      TurnToPageOrder(1);
}
function IfshowOrderSelect(count)
    {
        if(count=="0")
        {
            document.getElementById("divOrderSelect").style.display = "none";
            document.getElementById("pageOrdercount").style.display = "none";
        }
        else
        {
            document.getElementById("divOrderSelect").style.display="";
            document.getElementById("pageOrdercount").style.display = "";
        }
    }
    
    function SelectDeptProvider(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexOrder(newPageCountProvider,newPageIndexProvider)
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
            popMsgObj.ShowMsg('转到页数超出查询范围！');
            return;
        }
        else
        {
        ifshow="0";
            this.pageCountSubSellOrder=parseInt(newPageCountProvider);
            TurnToPageOrder(parseInt(newPageIndexProvider));
        }
    }
    
    

function closeProviderdiv()
{
    document.getElementById("divSubSellOrder").style.display="none";
}


</script>

